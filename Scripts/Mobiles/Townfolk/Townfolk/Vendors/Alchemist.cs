using System.Collections.Generic;
using Server.Items;
using System;

namespace Server.Mobiles
{
    public class Alchemist : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.MagesGuild; } }

		[Constructable]
		public Alchemist() : base( "the alchemist" )
		{
			SetSkill( SkillName.Alchemy, 85.0, 100.0 );
			SetSkill( SkillName.TasteID, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBAlchemist() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Robe( Utility.RandomPinkHue() ) );
		}

		public Alchemist( Serial serial ) : base( serial )
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
				// 40 is for Magic, vendor, funny , aggresive 
				List<int> OptionalSpeechText = new List<int> { 40 };
				this.Say(this.NPCRandomSpeech(this.Female, true, false, false, optionalStatements: OptionalSpeechText));
			}
		}
	}
}