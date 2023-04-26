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
		private DateTime m_NextCastTime;
		private DateTime m_RunAwayTimer;
		private Mobile m_RunFrom;

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
			// Check if it is time to recall away
			if (m_Mobile.TotalGold > 5000 || m_Mobile.TotalWeight > 550)
			{
				RecallAway();
				base.DoActionWander();
			}

			// check for combatant, if no one, check Rest(), reset combo, and continue to wander
			m_Mobile.DebugSay( "I have no combatant" );
			if ( AcquireFocusMob( (m_Mobile.RangePerception * 2), m_Mobile.FightMode, false, false, true ) )
			{
				m_Mobile.DebugSay( "I have detected {0}, attacking", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				m_RunFrom = m_Mobile.Combatant;
				Action = ActionType.Combat;
			}
			else
			{
				Rest();
				m_MegaCombo = 0;
				base.DoActionWander();
			}

			return true;
		}

        public override bool DoActionCombat()
		{
			m_Mobile.DebugSay( "I'm in active combat" );
            // Need to determine what type of "Player that is covered" because a bard
            // would react a lot different than a dex melee, vs a pk build
            Mobile combatant = m_Mobile.Combatant;
			m_Mobile.Warmode = true;
			bool isPlayer = combatant.Player || combatant.Name == "an ettin";

			int fleeNumb = Checking_Flee_or_Backoff();
			if (fleeNumb != 0){
				switch (fleeNumb)
				{
					case 1:
						Action = ActionType.Backoff;
						return true;
					default:
						Action = ActionType.Flee;
						return true;
				}
			}

            // Check to see if Combatant has left, if so put guard up
			if ( combatant == null || combatant.Deleted || combatant.Map != m_Mobile.Map || !combatant.Alive )
			{
				m_Mobile.DebugSay( "My combatant is gone, so my guard is up" );
				m_RunFrom = null;

				Action = ActionType.Guard;
				return true;
			}


			if (isPlayer)
			{
				if (m_Mobile.Mana > 24 && m_MegaCombo != -1 && m_Mobile.Combatant != null && m_NextCastTime < DateTime.Now)
				{
					// MMMMMMEGA Combo!
					m_Mobile.DebugSay( "Mega Combo!" );
					Spell spell = null;
					spell = DoMegaCombo( m_Mobile.Combatant );

					if( spell != null )
					{
						spell.Cast();
						m_NextCastTime = DateTime.Now + TimeSpan.FromSeconds( 3.0 );
						return true;
					}

				}
			}

			if ( !m_Mobile.InRange( combatant, m_Mobile.RangePerception ) )
			{
				// They are somewhat far away, can we find something else?
				m_Mobile.DebugSay( "They are far away?! Maybe that is why this is happening" );

				if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
				{
					m_Mobile.Combatant = m_Mobile.FocusMob;
					m_RunFrom = m_Mobile.Combatant;
					m_Mobile.FocusMob = null;
				}
				else if ( !m_Mobile.InRange( combatant, m_Mobile.RangePerception * 3 ) )
				{
					m_Mobile.Combatant = null;
					m_RunFrom = null;
				}

				combatant = m_Mobile.Combatant;
				if ( combatant == null )
				{
					m_Mobile.DebugSay( "My combatant has fled, so I am on guard" );
					m_RunFrom = null;
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
				m_RunFrom = m_Mobile.Combatant;
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
			Mobile enemy;
			if (m_RunFrom != null)
			{
				enemy = m_RunFrom;
			}
			else{
				Action = ActionType.Guard;
				return true;
			}
			
			double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;
			m_Mobile.DebugSay( "I flee from {0}", enemy.Name );
			// Already hidden, Determine what to do. Recall away, stay hidding or continue fighting
			// Heavily weighted to recalling away and hidding/ staying hidden.
			if (m_Mobile.Hidden)
			{
				if (hitPercent > 0.25 && m_RunAwayTimer < DateTime.Now)
				{
					Action = ActionType.Guard;
					return true;
				}

				if (m_Mobile.Mana > 11 && hitPercent < 0.20)
				{
					RecallAway();
				}

				Action = ActionType.Flee;
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
				Action = ActionType.Flee;
				return true;
			}

			if ( (int) m_Mobile.GetDistanceToSqrt( m_RunFrom ) > 25 )
				{
					m_Mobile.DebugSay( "Far Enough to Rest" );
					Action = ActionType.Guard;
					return true;
				}

			// Finally all else fails, keep running
			RunFrom(m_RunFrom);
			return true;

		}

        public override bool DoActionBackoff()
		// Backoff will keep the NPC engauged in the fight, while allowing them to Rest or heal befor re-enguaging 
		{
			if (m_RunFrom != null)
			{
				Mobile combatant = m_RunFrom;
			}
			else{
				Action = ActionType.Guard;
				m_Mobile.DebugSay( "Breaking out of Backoff" );
				return true;
			}


			double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;
			m_Mobile.CurrentSpeed = m_Mobile.ActiveSpeed;
			m_Mobile.Warmode = true;

			m_Mobile.DebugSay( "I backoff from {0}", m_RunFrom.Name );

			// really hurt, lets run away.
			if ( hitPercent < 0.1 )
			{
				// A little too hurt, Run away.
				m_Mobile.DebugSay( "Started backing off, now fleeing!" );
				m_RunAwayTimer = DateTime.Now.Add(TimeSpan.FromSeconds(10));
				Action = ActionType.Flee;
				return true;
			}
			else
			{
				// Run away timer is still going, RUN!
				if (m_RunAwayTimer > DateTime.Now){
					// walk away and try to rest
					m_Mobile.DebugSay( "Timer Still going");
					if ( (int) m_Mobile.GetDistanceToSqrt( m_RunFrom ) > 6 )
					{
						m_Mobile.DebugSay( "Far Enough to Rest" );
						Rest();
					}
					else{
						m_Mobile.DebugSay( "Keep Running!" );
						RunFrom(m_RunFrom);
						Action = ActionType.Backoff;
						return true;
					}

				}
				else{
					int fleeNumb = Checking_Flee_or_Backoff();
					if (fleeNumb != 0){
						switch (fleeNumb)
						{
					case 1:
						Action = ActionType.Backoff;
						return true;
					case 2:
						Action = ActionType.Flee;
						return true;
					default:
						Action = ActionType.Guard;
						return true;
						}
					}

				}
			}

			Action = ActionType.Guard;
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

		public void RunFrom( Mobile m )
		{
			Run( ( m_Mobile.GetDirectionTo( m ) - 4 ) & Direction.Mask );
		}
		public void Run( Direction d )
		{
			if( m_Mobile.Spell != null && m_Mobile.Spell.IsCasting || m_Mobile.Paralyzed || m_Mobile.Frozen || m_Mobile.DisallowAllMoves )
				return;

			m_Mobile.Direction = d | Direction.Running;

			if( !DoMove( m_Mobile.Direction, true ) )
				OnFailedMove();
		}

		// 0 means, we don't need to back off or flee, 1 means backoff, and 2 means flee
		private int Checking_Flee_or_Backoff()
		{
			// checking if should flee or backoff
			m_Mobile.DebugSay( "Checking if I should flee" );
			Mobile combatant = m_Mobile.Combatant;
			double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;
			if ( hitPercent >= 0.6){
				return 0;
			}
			else if ( hitPercent < 0.6 && hitPercent > 0.4)
			{
				// if my hit percentage is below combatant hit percent, lets back off
				if ( hitPercent < (double)combatant.Hits / combatant.HitsMax )
				{
					// they are winning, backup and heal
					m_Mobile.DebugSay( "They are winning, backoff" );
					m_RunAwayTimer = DateTime.Now.Add(TimeSpan.FromSeconds( 5 ));
					return 1;
				}

			} 
			else if ( hitPercent <= 0.4)
			{
				m_Mobile.DebugSay( "I'm Low on life backing off" );
				m_RunAwayTimer = DateTime.Now.Add(TimeSpan.FromSeconds( 8 ));
				return 1;
			}
			else if ( hitPercent <= 0.2)
			{
				m_RunAwayTimer = DateTime.Now.Add(TimeSpan.FromSeconds( 11 ));
				m_Mobile.DebugSay( "I'm REALLY low on life, fleeing" );
				return 2;
			}
			return 0;
		}

		// Rest only contains abilities that allow the player to rest
		// abilities like healing, regen mana, and curing
		private void Rest()
		{

			m_Mobile.DebugSay( "Taking a moment to rest" );
			if( m_Mobile.Poisoned )
			{
				// note to self, determine how to unequip and re-equip
				m_Mobile.DebugSay( "I am going to cure myself" );
				Spell spell = new CureSpell( m_Mobile, null );
				// todo: set cast timer
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
				// todo: set cast timer
				spell.Cast();
				return;
			}
			
			if ( hitPercent > 0.50 && manaPercent < 0.50 && !m_Mobile.Poisoned)
			{
				// Restore Mana
				m_Mobile.DebugSay( "I am going to meditate" );
				m_Mobile.UseSkill( SkillName.Meditation );
				m_Mobile.Paralyze(TimeSpan.FromSeconds( 3 ));
				return;
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

		protected int m_MegaCombo = -1;
		public virtual Spell DoMegaCombo( Mobile c )
		{
			Spell spell = null;

			if( m_MegaCombo == 0 )
			{
				spell = new MagicArrowSpell( m_Mobile, null );
				m_MegaCombo = 1; // Move to next spell
			}
			else if( m_MegaCombo == 1 )
			{
				spell = new ExplosionSpell( m_Mobile, null );
				m_MegaCombo = 2; // Move to next spell
			}
			else if( m_MegaCombo == 2 )
			{
				if( !c.Poisoned )
					spell = new EnergyBoltSpell( m_Mobile, null );

				m_MegaCombo = 3; // Move to next spell
			}
			else if( m_MegaCombo == 3 )
			{
				if( !c.Poisoned )
					spell = new PoisonSpell( m_Mobile, null );

				m_MegaCombo = -1;
			}

			return spell;
		}


	}
}
