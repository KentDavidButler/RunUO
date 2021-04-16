using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a shiny corpse" )]
	public class Effusion : BaseCreature
	{

		[Constructable]
		public Effusion() : base( AIType.AI_Mage, FightMode.Closest, 20, 1, 0.3, 0.4 )
		{
			Name = "a shimmering effusion";
			Body = 261;
			BaseSoundID = 466;

			Hue = Utility.RandomSlimeHue();

            SetStr( 767, 945 );
			SetDex( 255, 310 );
			SetInt( 450, 650 );

			SetHits( 900, 1000 );

			SetDamage( 22, 29 );

			SetSkill( SkillName.Anatomy, 25.1, 50.0 );
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 100.0, 120.0 );
			SetSkill( SkillName.Meditation, 25.1, 50.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 85;
		}

		public override void GenerateLoot()
		{
		    AddLoot( LootPack.Champion, 1 );
			AddLoot( LootPack.FilthyRich, 1 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
			AddLoot( LootPack.HighScrolls, 8 );
			AddLoot( LootPack.Gems, 8 );
			AddLoot( LootPack.UniqueItem, 1);
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish | FoodType.FruitsAndVegies | FoodType.GrainsAndHay | FoodType.Eggs; } }

		public Effusion( Serial serial ) : base( serial )
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
