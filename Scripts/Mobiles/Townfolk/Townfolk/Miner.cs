using System.Collections.Generic;
using Server.Items;


namespace Server.Mobiles
{
    public class Miner : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public Miner() : base( "the miner" )
		{
			SetSkill( SkillName.Mining, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBMiner() );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new FancyShirt( 0x3E4 ) );
			AddItem( new LongPants( 0x192 ) );
			AddItem( new Pickaxe() );
			AddItem( new ThighBoots( 0x283 ) );
		}

		public Miner( Serial serial ) : base( serial )
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
    }

	    public class FieldMiner : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }
		public override bool PlayerRangeSensitive { get { return false; } }

		[Constructable]
		public FieldMiner() : base( "the miner" )
		{
			SetSkill( SkillName.Mining, 75.0, 110.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBFieldMiner() );
		}

		public override void InitOutfit()
		{

			AddItem( new FancyShirt( Utility.RandomMetalHue() ) );
			AddItem( new Pickaxe() );
			AddItem( new ThighBoots( Utility.RandomMetalHue() ) );

			int hairHue = GetHairHue();

			Utility.AssignRandomHair( this, hairHue );
			Utility.AssignRandomFacialHair( this, hairHue );

			if ( Female )
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0: AddItem( new ShortPants( Utility.RandomMetalHue() ) ); break;
					case 1: AddItem( new Kilt( Utility.RandomMetalHue() ) ); break;
					case 2: AddItem( new Skirt( Utility.RandomMetalHue() ) ); break;
				}
			}
			else
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: AddItem( new LongPants( Utility.RandomMetalHue() ) ); break;
					case 1: AddItem( new ShortPants( Utility.RandomMetalHue() ) ); break;
				}
			}

			PackGold( 100, 200 );
		}

		public FieldMiner( Serial serial ) : base( serial )
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
			//if surface under miner is cave floor? the mine
        }
    }
}