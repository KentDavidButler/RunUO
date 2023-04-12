using System.Threading.Tasks;
using Internal;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using Server.Items;
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
    public class MagePlayerAI : MageAI
	{
		public MagePlayerAI(BaseCreature m) : base (m)
		{
		}

		public override bool Think()
		{
			if( m_Mobile.Deleted )
				return false;

			if( ProcessTarget() )
				return true;
			else
				return base.Think();
		}

		public override bool DoActionWander()
		{
			m_Mobile.DebugSay( "I have no combatant" );
			if ( AcquireFocusMob( (m_Mobile.RangePerception * 2), m_Mobile.FightMode, false, false, true ) )
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

			// Check if backpack is full
			if (m_Mobile.TotalGold > 5000 || m_Mobile.TotalWeight > 550)
			{
				RecallAway();
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

			// checking if should flee or backoff
			m_Mobile.DebugSay( "Checking if I should flee" );
			double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;
			if ( hitPercent < 0.6 && hitPercent > 0.4)
			{
				if ( m_Mobile.Hits < combatant.Hits )
				{
					// they are winning, backup and heal
					m_Mobile.DebugSay( "They are winning, backoff" );
					DoActionBackoff();
				}

			} 
			else if ( hitPercent <= 0.4)
			{
				m_Mobile.DebugSay( "I'm Low on life backing off" );
				DoActionBackoff();
			}
			else if (Utility.Random( 0, 100 ) < 10)
			{
				// Always have a 10% chance to BackOff
				m_Mobile.DebugSay( "Hit Random Chance to backoff" );
				DoActionBackoff();
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
			double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;
			Mobile enemy = m_Mobile.FocusMob;

			// Already hidden, Determine what to do. Recall away, stay hidding or continue fighting
			// Heavily weighted to recalling away and hidding/ staying hidden.
			if (m_Mobile.Hidden)
			{
				if (hitPercent > 0.25)
				{
					DoActionCombat();
				}

				if (m_Mobile.Mana > 11 && hitPercent < 0.20)
				{
					RecallAway();
				}

				DoActionFlee();
				return true;
			}

			if (hitPercent > 0.15 && m_Mobile.Mana > 11)
			{
				// recall away, all is lost
				RecallAway();
			}
			// If hiding is higher than 75 then try hiding
			if (m_Mobile.Skills.Hiding.Value > 75.0)
			{
				m_Mobile.UseSkill( SkillName.Hiding );
				DoActionFlee();
				return true;
			}

			// Finally all else fails, keep running
			Mobile combatant = m_Mobile.Combatant;
			Direction d = combatant.GetDirectionTo(m_Mobile);

			d = (Direction)((int)d + Utility.RandomMinMax(-1, +1));

			m_Mobile.Direction = d;
			m_Mobile.Move(d);

			return true;

		}

        public override bool DoActionBackoff()
		// Backoff will keep the NPC engauged in the fight, while allowing them to Rest or heal befor re-enguaging 
		{
			double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;

			// really hurt, lets run away.
			if ( hitPercent < 0.1 )
			{
				// A little too hurt, Run away.
				m_Mobile.DebugSay( "Started backing off, now fleeing!" );
				DoActionFlee();
				return true;
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
						base.DoActionBackoff();
					}					
				}
				else if (m_Mobile.Combatant != null)
				{
					if ( WalkMobileRange(m_Mobile.Combatant, 1, true, m_Mobile.RangePerception * 1, m_Mobile.RangePerception * 1) )
					{
						m_Mobile.DebugSay( "I am trying to rest" );
						Rest();
						base.DoActionCombat();
					}
					base.DoActionBackoff();
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

			// Make sure we are a safe distance away
			if ( WalkMobileRange(m_Mobile.Combatant, 1, false, m_Mobile.RangePerception, m_Mobile.RangePerception * 2) || m_Mobile.Combatant == null )
			{
				m_Mobile.DebugSay( "Well, here I am safe" );
				if( m_Mobile.Poisoned )
				{
					// note to self, determine how to unequip and re-equip
					m_Mobile.DebugSay( "I am going to cure myself" );
					Spell spell = new CureSpell( m_Mobile, null );
					spell.Cast();
					return;
				}

				double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;
				double manaPercent = (double)m_Mobile.Mana / m_Mobile.ManaMax;
				if ( hitPercent < 0.75 && manaPercent > 0.15 && !m_Mobile.Poisoned)
				{
					m_Mobile.DebugSay( "I am going to heal myself" );
					Spell spell = new GreaterHealSpell( m_Mobile, null );
					m_Mobile.DebugSay( "Casting Greater heal " + spell );
					spell.Cast();
					return;
				}
				
				if ( hitPercent > 0.50 && manaPercent < 0.50 && !m_Mobile.Poisoned)
				{
					// Restore Mana
					m_Mobile.DebugSay( "I am going to meditate" );
					m_Mobile.UseSkill( SkillName.Meditation );
					return;
				}
			}
			else
			{
				Rest();
			}				
		}


		private void RecallAway()
		{
			if( m_Mobile.Poisoned )
			{
				// note to self, determine how to unequip and re-equip
				m_Mobile.DebugSay( "I am going to cure myself" );
				Spell spell = new CureSpell( m_Mobile, null );
				spell.Cast();
				return;
			}

			if (m_Mobile.Mana > 11){
				m_Mobile.ClearHands();
				m_Mobile.Frozen = true;
				m_Mobile.Say("Kal Ort Por");
				if ( m_Mobile.Body.Type == BodyType.Human && !m_Mobile.Mounted )
					m_Mobile.Animate( 239, 24, 1, true, false, 0 );
				Task.Delay(1500).ContinueWith(t=> m_Mobile.PlaySound( 0x1FC ));
				Task.Delay(1510).ContinueWith(t=> m_Mobile.Delete());
			}

		}

	}
}