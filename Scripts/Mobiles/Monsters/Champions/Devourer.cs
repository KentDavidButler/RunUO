using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "an devourer corpse" )]
	public class Devourer : BaseCreature
	{
		[Constructable]
		public Devourer () : base( AIType.AI_Melee, FightMode.Closest, 20, 1, 0.2, 0.4 )
		{
			Name = "a devourer";
			Body = 256;
			BaseSoundID = 427;

			SetStr( 1200, 1500 );
			SetDex( 177, 255 );
			SetInt( 151, 250 );

			SetHits( 1300, 1500 );

			SetDamage( 29, 35 );

			SetSkill( SkillName.Anatomy, 100.0, 150.0 );
			SetSkill( SkillName.MagicResist, 125.1, 140.0 );
			SetSkill( SkillName.Tactics, 100.1, 150.0 );
			SetSkill( SkillName.Wrestling, 100.1, 150.0 );
			SetSkill( SkillName.Parry, 75.0, 90.0 );

			Fame = 25000;
			Karma = -25000;

			VirtualArmor = 100;

			PackItem( new Club() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Champion, 1 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 4 );
			AddLoot( LootPack.HighScrolls, 8 );
			AddLoot( LootPack.Gems, 8 );

			if ( 0.02 > Utility.RandomDouble() )
			{
				switch ( Utility.Random( 5 ) )
				{
					case 0:	PackItem( new RangerArms() );	break;
					case 1:	PackItem( new RangerChest() );	break;
					case 2:	PackItem( new RangerGloves() );	break;
					case 3:	PackItem( new RangerLegs() );	break;
					case 4:	PackItem( new RangerGorget() );	break;
				}
			}
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 2; } }

		public Devourer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}