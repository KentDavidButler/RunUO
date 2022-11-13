using System;
using System.Globalization;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class InnKeeper : BaseVendor 
	{ 
		private List<SBInfo> m_SBInfos = new List<SBInfo>(); 
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } } 

		[Constructable]
		public InnKeeper() : base( "the innkeeper" ) 
		{ 
		} 

		public override void InitSBInfo() 
		{ 
			m_SBInfos.Add( new SBInnKeeper() ); 
		} 

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Sandals : VendorShoeType.Shoes; }
		}

		public InnKeeper( Serial serial ) : base( serial ) 
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

			if (Utility.Random(60) > 55)
			{
				// 51 is for Innkeep, vendor, funny , aggresive 
				List<int> OptionalSpeechText = new List<int> { 51 };
				this.Say(this.NPCRandomSpeech(this.Female, true, false, false, optionalStatements: OptionalSpeechText));
			}
		}
	} 
}