//
// This is a first simple AI
//
//

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
		}

		public override bool DoActionInteract()
		{
            // Are there phrases that they should react too? 
		}

        public override bool DoActionCombat()
		{

		}

        public override bool DoActionFlee()
		{

            // Things that may be done during fleeing
            // Run away and heal/cure
            // Run away to a safe distance and 'recall home'
            // Run away to a safe distance and hide

		}

		public override bool DoActionGuard()
		{
			m_Mobile.FocusMob = m_Mobile.Combatant;
			return base.DoActionGuard();
		}

		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( m_Mobile, 4 ) )
				return true;

			return base.HandlesOnSpeech( from );
		}

		// Temporary 
		public override void OnSpeech( SpeechEventArgs e )
		{
			base.OnSpeech( e );
 
			Mobile from = e.Mobile;
 
            // add any specific speech stuff here. File EventSink.cs should have all speech specific functions
            // OnSpeech = e.Speech; // should provide access to what was said


		}
	}
}