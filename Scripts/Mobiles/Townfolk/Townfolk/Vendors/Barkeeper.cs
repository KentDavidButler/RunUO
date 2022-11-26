using System.Collections.Generic;
using Server.Items;
using System;

namespace Server.Mobiles
{
    public class Barkeeper : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>(); 
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } } 

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBBarkeeper() ); 
		}

		public override VendorShoeType ShoeType{ get{ return Utility.RandomBool() ? VendorShoeType.ThighBoots : VendorShoeType.Boots; } }

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new HalfApron( Utility.RandomBrightHue() ) );
		}

		[Constructable]
		public Barkeeper() : base( "the barkeeper" )
		{
		}

		public Barkeeper( Serial serial ) : base( serial )
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
				this.Say(this.NPCRandomSpeech(this.Female, true, true, true, optionalStatements: OptionalSpeechText));
			}
		}
	}
}