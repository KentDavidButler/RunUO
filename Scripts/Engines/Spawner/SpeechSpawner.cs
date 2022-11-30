using Internal;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class SpeechSpawner : Spawner
	{
		private int m_TriggerRange;

		private string m_SpeechTrigger;
		private TextDefinition m_SpawnMessage;
		private bool m_InstantFlag;

		private bool m_AllowGhostTriggering;

		[CommandProperty( AccessLevel.GameMaster )]
		public int TriggerRange
		{
			get { return m_TriggerRange; }
			set { m_TriggerRange = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string SpeechTrigger
		{
			get { return m_SpeechTrigger; }
			set { m_SpeechTrigger = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowGhostTriggering
		{
			get { return m_AllowGhostTriggering; }
			set { m_AllowGhostTriggering = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TextDefinition SpawnMessage
		{
			get { return m_SpawnMessage; }
			set { m_SpawnMessage = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool InstantFlag
		{
			get { return m_InstantFlag; }
			set { m_InstantFlag = value; }
		}

		[Constructable]
		public SpeechSpawner()
		{
		}

		[Constructable]
		public SpeechSpawner( string spawnName )
			: base( spawnName )
		{
		}

		[Constructable]
		public SpeechSpawner( int amount, int minDelay, int maxDelay, int team, int homeRange, string spawnName )
			: base( amount, minDelay, maxDelay, team, homeRange, spawnName )
		{
		}

		[Constructable]
		public SpeechSpawner( int amount, int minDelay, int maxDelay, int team, int homeRange, string spawnName, int triggerRange, string spawnMessage, bool instantFlag )
			: base( amount, minDelay, maxDelay, team, homeRange, spawnName )
		{
			m_TriggerRange = triggerRange;
			m_SpawnMessage = TextDefinition.Parse( spawnMessage );
			m_InstantFlag = instantFlag;
		}

		public SpeechSpawner( int amount, TimeSpan minDelay, TimeSpan maxDelay, int team, int homeRange, List<string> spawnNames )
			: base( amount, minDelay, maxDelay, team, homeRange, spawnNames )
		{
		}

		public SpeechSpawner( int amount, TimeSpan minDelay, TimeSpan maxDelay, int team, int homeRange, List<string> spawnNames, int triggerRange, TextDefinition spawnMessage, bool instantFlag )
			: base( amount, minDelay, maxDelay, team, homeRange, spawnNames )
		{
			m_TriggerRange = triggerRange;
			m_SpawnMessage = spawnMessage;
			m_InstantFlag = instantFlag;
		}

		public override string DefaultName
		{
			get { return "Speech Spawner"; }
		}

		public override void DoTimer( TimeSpan delay )
		{
			if ( !Running )
				return;

			
			End = DateTime.Now + delay;
		}

		public override void Respawn()
		{
			RemoveSpawned();
			
			End = DateTime.Now;
		}

		public override void Spawn()
		{
			for ( int i = 0; i < SpawnNamesCount; ++i )
			{
				for ( int j = 0; j < Count; ++j )
					Spawn( i );
			}
		}

		public override void OnTick()
		{
			// removes the time spawn functionality
		}


		public virtual bool ValidTrigger( Mobile m )
		{
			if ( m is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)m;

				if ( !bc.Controlled && !bc.Summoned )
					return false;
			}
			else if ( !m.Player )
			{
				return false;
			}

			return m.Alive && m.CanBeDamaged() && !m.Hidden;
		}

		public SpeechSpawner( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_TriggerRange );
			TextDefinition.Serialize( writer, m_SpawnMessage );
			writer.Write( m_InstantFlag );
			writer.Write( m_SpeechTrigger);
			writer.Write( m_AllowGhostTriggering);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_TriggerRange = reader.ReadInt();
			m_SpawnMessage = TextDefinition.Deserialize( reader );
			m_InstantFlag = reader.ReadBool();
			m_SpeechTrigger = reader.ReadString();
			m_AllowGhostTriggering = reader.ReadBool();
		}

		public override bool HandlesOnSpeech { get { return (Running && m_SpeechTrigger != null && m_SpeechTrigger.Length > 0); } }

        public override void OnSpeech(SpeechEventArgs e)
        {
			// is spawn full?
			if( this.IsFull ){
				// full - do nothing - return
				return;
			}

            if ( Running && e.Mobile.InRange( GetWorldLocation(), m_TriggerRange ) && ValidPlayerTrig(e.Mobile) )
            {
                //m_speechTriggerActivated = false;

                if (!Utility.InRange(e.Mobile.Location, this.Location, m_TriggerRange))
				{
					return;
				}

                if (m_SpeechTrigger != null && e.Speech.ToLower().IndexOf(m_SpeechTrigger.ToLower()) >= 0)
                {
                    e.Handled = true;

                    TextDefinition.SendMessageTo( e.Mobile, m_SpawnMessage );

					DoTimer();
					Spawn();

					if ( m_InstantFlag )
					{
						foreach ( ISpawnable spawned in Spawned )
						{
							if ( spawned is Mobile )
								((Mobile)spawned).Combatant = e.Mobile;
						}
					}
                }
            }

        }

		private bool ValidPlayerTrig(Mobile m)
        {
            if (m == null || m.Deleted){
				return false;
			}

            return (m.Player  && ((!m.Body.IsGhost && !m_AllowGhostTriggering) || (m.Body.IsGhost && m_AllowGhostTriggering)));
        }
	}
}
