using System;
using System.Collections.Generic;
using Server.Spells;
using Server.Spells.Fifth;
using Server.Spells.First;
using Server.Spells.Fourth;
using Server.Spells.Second;
using Server.Spells.Seventh;
using Server.Spells.Sixth;
using Server.Spells.Third;
using Server.Targeting;

namespace Server.Mobiles
{
    public class DungeonCrawlerAI : BaseAI
	{
		public DungeonCrawlerAI(BaseCreature m) : base (m)
		{
		}

		public override bool DoActionWander()
		{
            // Do action Wander is what the AI does between fighting and moving. 
            // We should check the health of the NPC and make sure they are healthy
            // We should check the backpack for weight and gold, and 'send them back' once they are at specific count
            // Try to have them scan, justs outside of screen view for next opponent
            // Finally determine where to send them next, they should never move 'backwards' in the dungeon, unless
            // they hit a dead end or are flee'ing

			m_Mobile.DebugSay( "I have no combatant" );

			if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				m_Mobile.DebugSay( "I have detected {0}, attacking", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			else
			{
				Rest();
				base.DoActionWander();
			}

			return true;
		}

		public override bool DoActionInteract()
		{
            // What is this used for, how is this different?

			return true;
		}

        public override bool DoActionCombat()
		{
			m_Mobile.DebugSay( "I'm in active combat" );
            // Need to determine what type of "Player that is covered" because a bard
            // would react a lot different than a dex melee, vs a pk build
            Mobile combatant = m_Mobile.Combatant;
			m_Mobile.Warmode = true;

            // Check to see if Combatant has left, if so put guard up
			if ( combatant == null || combatant.Deleted || combatant.Map != m_Mobile.Map || !combatant.Alive )
			{
				m_Mobile.DebugSay( "My combatant is gone, so my guard is up" );

				Action = ActionType.Guard;
				return true;
			}

            if ( m_Mobile.CanFlee )
			{
				m_Mobile.DebugSay( "Checking if I should flee" );
				double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;
				if ( hitPercent < 0.2 )
				{
					// We are low on health, should we flee?
					bool flee = false;

					if ( m_Mobile.Hits < combatant.Hits )
					{
						// We are more hurt than them

						int diff = combatant.Hits - m_Mobile.Hits;

						flee = Utility.Random( 0, 100 ) < 10 + diff; // (10 + diff)% chance to flee
					}
					else
					{
						flee = Utility.Random( 0, 100 ) < 10; // 10% chance to flee
					}

					if ( flee )
					{
						if ( m_Mobile.Debug )
							m_Mobile.DebugSay( "I am going to flee from {0}", combatant.Name );

						Action = ActionType.Flee;
					}
				}
			}

			if ( !m_Mobile.InRange( combatant, m_Mobile.RangePerception ) )
			{
				// They are somewhat far away, can we find something else?

				if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
				{
					m_Mobile.Combatant = m_Mobile.FocusMob;
					m_Mobile.FocusMob = null;
				}
				else if ( !m_Mobile.InRange( combatant, m_Mobile.RangePerception * 3 ) )
				{
					m_Mobile.Combatant = null;
				}

				combatant = m_Mobile.Combatant;

				if ( combatant == null )
				{
					m_Mobile.DebugSay( "My combatant has fled, so I am on guard" );
					Action = ActionType.Guard;

					return true;
				}
			}

			if ( MoveTo( combatant, true, m_Mobile.RangeFight ) )
			{
				m_Mobile.Direction = m_Mobile.GetDirectionTo( combatant );
			}
			else if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "My move is blocked, so I am going to attack {0}", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;

				return true;
			}
			else if ( m_Mobile.GetDistanceToSqrt( combatant ) > m_Mobile.RangePerception + 1 )
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "I cannot find {0}, so my guard is up", combatant.Name );

				Action = ActionType.Guard;

				return true;
			}
			else
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "I should be closer to {0}", combatant.Name );
			}

			return true;
		}

        public override bool DoActionFlee()
		{

            // Things that may be done during fleeing
            // Run away and heal/cure
            // Run away to a safe distance and 'recall home'
            // Run away to a safe distance and hide
			return true;

		}

        public override bool DoActionBackoff()
		// Backoff will keep the NPC engauged in the fight, while allowing them to Rest or heal befor re-enguaging 
		{
			double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;

			if ( !m_Mobile.Summoned && !m_Mobile.Controlled && hitPercent < 0.1 && m_Mobile.CanFlee ) // Less than 10% health
			{
				// A little too hurt, Run away.
				Action = ActionType.Flee;
			}
			else
			{
				if (AcquireFocusMob(m_Mobile.RangePerception * 2, FightMode.Closest, true, false , true))
				{
					if ( WalkMobileRange(m_Mobile.FocusMob, 1, false, m_Mobile.RangePerception, m_Mobile.RangePerception * 2) )
					{
						m_Mobile.DebugSay( "Well, here I am safe" );
						//heal, cure, Rest
						Rest();
					}					
				}
				else
				{
					m_Mobile.DebugSay( "I have lost my focus, lets relax" );
					Action = ActionType.Wander;
				}
			}

			return true;
		}

		public override bool DoActionGuard()
		{
			m_Mobile.FocusMob = m_Mobile.Combatant;
			Rest();
			return base.DoActionGuard();
		}

		// public override bool HandlesOnSpeech( Mobile from )
		// {
		// 	if ( from.InRange( m_Mobile, 4 ) )
		// 		return true;

		// 	return base.HandlesOnSpeech( from );
		// }

		// Temporary 
		// public override void OnSpeech( SpeechEventArgs e )
		// {
		// 	base.OnSpeech( e );
 
		// 	Mobile from = e.Mobile;
 
        //     // add any specific speech stuff here. File EventSink.cs should have all speech specific functions
        //     // OnSpeech = e.Speech; // should provide access to what was said


		// }

		private void Rest()
		{
			if( m_Mobile.Poisoned )
			{
				m_Mobile.DebugSay( "I am going to cure myself" );
				new CureSpell( m_Mobile, null ).Cast();
			}

			double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;
			double manaPercent = (double)m_Mobile.Mana / m_Mobile.ManaMax;
			if ( hitPercent < 0.75 && manaPercent > 0.15 && !m_Mobile.Poisoned)
			{
				m_Mobile.DebugSay( "I am going to heal myself" );
				new GreaterHealSpell( m_Mobile, null ).Cast();

			}
			if ( hitPercent > 0.50 && manaPercent < 0.50 && !m_Mobile.Poisoned)
			{
				// Restore Mana
				m_Mobile.DebugSay( "I am going to meditate" );
				m_Mobile.UseSkill( SkillName.Meditation );
			}

		}
	}
}