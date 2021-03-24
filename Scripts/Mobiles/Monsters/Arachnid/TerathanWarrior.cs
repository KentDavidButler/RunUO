namespace Server.Mobiles
{
    [CorpseName( "a terathan warrior corpse" )]
	public class TerathanWarrior : BaseCreature
	{
		[Constructable]
		public TerathanWarrior() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a terathan warrior";
			Body = 70;
			BaseSoundID = 589;

			SetStr( 166, 215 );
			SetDex( 96, 145 );
			SetInt( 41, 65 );

			SetHits( 100, 129 );
			SetMana( 0 );

			SetDamage( 7, 17 );

			SetSkill( SkillName.Poisoning, 60.1, 80.0 );
			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 30;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override int TreasureMapLevel{ get{ return 1; } }
		public override int Meat{ get{ return 4; } }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.TerathansAndOphidians; }
		}

		public TerathanWarrior( Serial serial ) : base( serial )
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