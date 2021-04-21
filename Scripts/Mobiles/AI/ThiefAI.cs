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
					PerformSteal(combatant);

                    m_Mobile.NextSkillTime = DateTime.Now + TimeSpan.FromSeconds( 10 );
                    m_Mobile.DebugSay( "Setting the delay 10 seconds." );
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
			//fleeing after stealing for 15 seconds
			if ( m_Mobile.Hits > m_Mobile.HitsMax/2 && 
				(m_Mobile.NextSkillTime + TimeSpan.FromSeconds( 5 )) <= DateTime.Now )
			{
				m_Mobile.DebugSay( "I am stronger now, so I will continue fighting" );
				m_StealCounter = 0;
				Action = ActionType.Combat;
			}
			else
			{
			    m_Mobile.DebugSay( "I am fleeing from opponent" );
				m_Mobile.FocusMob = m_Mobile.Combatant;
				try 
				{	        
					if ( (int) m_Mobile.GetDistanceToSqrt( m_Mobile.Combatant ) > 6 )
					{
						m_Mobile.DebugSay( "I am trying to hide" );
						PerformHide();
					}
				}
				catch (Exception)
				{
					Action = ActionType.Guard;
				}
				base.DoActionFlee();
			}

			return true;
		}

		private void HideSelf()
        {
				m_Mobile.DebugSay( "I'm Hiding you can't see me." );
                m_Mobile.Hidden = true;
				m_Mobile.Paralyze(TimeSpan.FromSeconds( 15 ));

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

		private void PerformSteal( Mobile combatant)
        {
			Container opponent_pack = combatant.Backpack;

            if ( opponent_pack != null && opponent_pack.Items.Count > 0 )
            {
				m_Mobile.DebugSay( "Trying to steal something from combatant." );
				Item item;
				switch (Utility.Random(3))
				{
					case 0:
						item = opponent_pack.FindItemByType( typeof ( Bandage ) );
						if ( item != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, item );
						}
						else
							goto case 1;
						break;
					case 1:
						item = opponent_pack.FindItemByType( typeof ( BlackPearl  ) );
						if ( item != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, item );
						}
						else
							goto case 2;
						break;
					case 2:
						item = opponent_pack.FindItemByType( typeof ( MandrakeRoot   ) );
						if ( item != null ) 
						{
							m_Mobile.DebugSay( "Trying to steal from combatant." );
							m_Mobile.UseSkill( SkillName.Stealing );
							if ( m_Mobile.Target != null )
								m_Mobile.Target.Invoke( m_Mobile, item );
						}
						else
							goto default;
						break;
					default:                        
						int randomIndex = Utility.Random( opponent_pack.Items.Count );
						m_Mobile.UseSkill( SkillName.Stealing );
						if ( m_Mobile.Target != null )
						{
							m_Mobile.Target.Invoke( m_Mobile, opponent_pack.Items[randomIndex] );
						}
						break;
				}

				m_StealCounter += 1;
            }
			else
            {	
					m_Mobile.DebugSay( "You have nothing to steal, I'm leaving" );
				m_StealCounter = 10;
            }
        }
	}
}