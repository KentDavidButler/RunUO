using System;
using Server.Items;

namespace Server.Mobiles
{
    public class ThiefAI : BaseAI
	{
	    private int m_StealCounter;

		public ThiefAI(BaseCreature m) : base (m)
		{
		}

		public override bool DoActionWander()
		{
			m_Mobile.DebugSay( "I have no combatant" );

			if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				m_Mobile.DebugSay( "I have detected {0}, attacking", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			else
			{
				base.DoActionWander();
				m_StealCounter = 0;
			}

			return true;
		}

		public override bool DoActionCombat()
		{
			Mobile combatant = m_Mobile.Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != m_Mobile.Map )
			{
				m_Mobile.DebugSay( "My combatant is gone, so my guard is up" );

				Action = ActionType.Guard;

				return true;
			}

			if ( WalkMobileRange( combatant, 1, true, m_Mobile.RangeFight, m_Mobile.RangeFight ) )
			{
				m_Mobile.Direction = m_Mobile.GetDirectionTo( combatant );

				if ( m_Mobile.NextSkillTime < DateTime.Now )
				{
					Container opponent_pack = combatant.Backpack;

                    if ( opponent_pack != null && opponent_pack.Items.Count > 0 )
                    {
                        m_Mobile.DebugSay( "Trying to steal something from combatant." );
                        int randomIndex = Utility.Random( opponent_pack.Items.Count );
                        m_Mobile.UseSkill( SkillName.Stealing );
                        if ( m_Mobile.Target != null )
                        {
                            m_StealCounter += 1;
                            m_Mobile.Target.Invoke( m_Mobile, opponent_pack.Items[randomIndex] );
                        }
                    }

                    m_Mobile.NextSkillTime = DateTime.Now + TimeSpan.FromSeconds( 10 );
					m_Mobile.NextSkillTimeTick = Core.TickCount + 10000;
                    m_Mobile.DebugSay( "Setting the delay 15 seconds." );
                    if ( m_StealCounter >= 3 )
                        Action = ActionType.Flee;

				}
			}
			else
			{
				m_Mobile.DebugSay( "I should be closer to {0}", combatant.Name );
			}

			if ( m_Mobile.Hits < m_Mobile.HitsMax * 20/100 && m_Mobile.CanFlee )
			{
				// We are low on health, should we flee?

				bool flee = false;

				if ( m_Mobile.Hits < combatant.Hits )
				{
					// We are more hurt than them

					int diff = combatant.Hits - m_Mobile.Hits;

					flee = Utility.Random( 0, 100 ) > 20 + diff; // (20 + diff)% chance to flee
				}
				else
				{
					flee = Utility.Random( 0, 100 ) > 20; // 20% chance to flee
				}

				if ( flee )
				{
					m_Mobile.DebugSay( "I am going to flee from {0}", combatant.Name );

					Action = ActionType.Flee;
				}
			}

			return true;
		}

		public override bool DoActionGuard()
		{
			if ( AcquireFocusMob(m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				m_Mobile.DebugSay( "I have detected {0}, attacking", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			else
			{
				base.DoActionGuard();
			}

			return true;
		}

		public override bool DoActionFlee()
		{
			if ( m_Mobile.Hits > m_Mobile.HitsMax/2 && m_Mobile.NextSkillTime <= DateTime.Now )
			{
				m_Mobile.DebugSay( "I am stronger now, so I will continue fighting" );
				m_StealCounter = 0;
				Action = ActionType.Combat;
			}
			else
			{
			    m_Mobile.DebugSay( "I am fleeing from opponent" );
				m_Mobile.FocusMob = m_Mobile.Combatant;
				if ( (int) m_Mobile.GetDistanceToSqrt( m_Mobile.Combatant ) > 4 )
				{
					m_Mobile.DebugSay( "I am Hiding Now!" );
				    PerformHide();
				}
				base.DoActionFlee();
			}

			return true;
		}

		private void HideSelf()
        {
            if (Core.TickCount >= m_Mobile.NextSkillTimeTick)
            {
				m_Mobile.DebugSay( "I'm Hiding you can't see me." );
                m_Mobile.Hidden = true;

                //m_Mobile.UseSkill(SkillName.Stealth);
				Action = ActionType.Guard;
            }
        }

        private void PerformHide()
        {
            if (!m_Mobile.Alive || m_Mobile.Deleted)
            {
                return;
            }

            if (!m_Mobile.Hidden)
            {
				m_Mobile.DebugSay( "Perform Hide" );
                HideSelf();
            }
		}
	}
}