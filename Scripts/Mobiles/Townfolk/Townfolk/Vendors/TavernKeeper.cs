using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class TavernKeeper : BaseVendor 
	{ 
		private List<SBInfo> m_SBInfos = new List<SBInfo>(); 
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } } 

		[Constructable]
		public TavernKeeper() : base( "the tavern keeper" ) 
		{ 
		} 

		public override void InitSBInfo() 
		{ 
			m_SBInfos.Add( new SBTavernKeeper() ); 
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new HalfApron() );
		}

		public TavernKeeper( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		}

		public override void OnThink()
		{
			base.OnThink();

			if (Utility.Random(60) > 57)
			{
				// 50 is for Barkeep, vendor, funny , aggresive 
				List<int> OptionalSpeechText = new List<int> { 50 };
				this.Say(this.NPCRandomSpeech(this.Female, true, this.IsActiveVendor, true, optionalStatements: OptionalSpeechText));
			}
		}
	} 
}