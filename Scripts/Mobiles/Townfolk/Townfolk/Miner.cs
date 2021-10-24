using System.Collections.Generic;
using Server.Items;
using System;
using Server.Engines.Harvest;


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
		public DateTime m_digDelay = DateTime.Now;
		HarvestDefinition oreAndStone = new HarvestDefinition();

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
			PackItem( new Shovel() );

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

			this.DoMining();

			//bool hasOre = false;
			//Item ore = null;
			
			//if (DateTime.Now > m_digDelay)
            //{
			//	m_digDelay = DateTime.Now + TimeSpan.FromSeconds(Utility.Random(26));
			//	this.Animate((int)WeaponAnimation.Wrestle, 11, 1, true, false, 1);
			//	Effects.PlaySound( this, this.Map, 0x125 );
			//	this.Freeze(TimeSpan.FromSeconds(2));
			//
			//	// 5 percent chance of digging up ore
			//	int chance = Utility.Random(1001);
			//	if(chance > 995)
			//	{
			//		PackItem(new GoldOre());
			//		hasOre = true;
			//		ore = Backpack.FindItemByType(typeof(GoldOre));
			//	}
			//	else if (chance > 990)
			//	{
			//		PackItem(new ShadowIronOre());
			//		hasOre = true;
			//		ore = Backpack.FindItemByType(typeof(ShadowIronOre));
			//	}
			//	else if (chance > 980)
			//	{
			//		PackItem(new IronOre());
			//		hasOre = true;
			//		ore = Backpack.FindItemByType(typeof(IronOre));
			//	}
			//
			//	// drop ore at feet
			//	if(hasOre)
			//	{
			//		ore.MoveToWorld(this.Location);
			//	}
			//}
        }
    }
}