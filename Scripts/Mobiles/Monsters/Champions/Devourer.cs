using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "an devourer corpse" )]
	public class Devourer : BaseCreature
	{
		[Constructable]
		public Devourer () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an devourer lord";
			Body = 256;
			BaseSoundID = 427;

			SetStr( 1200, 1500 );
			SetDex( 66, 75 );
			SetInt( 46, 70 );

			SetHits( 476, 552 );

			SetDamage( 20, 25 );

			SetSkill( SkillName.MagicResist, 125.1, 140.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 25000;
			Karma = -25000;

			VirtualArmor = 50;

			PackItem( new Club() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 10 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
			AddLoot( LootPack.HighScrolls, 2 );
			AddLoot( TreasureMapLevel{ get{ return 5; } } );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 3; } }
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