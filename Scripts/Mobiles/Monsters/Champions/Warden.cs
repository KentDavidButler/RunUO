using System;
using Server.Items;
namespace Server.Mobiles
{
    [CorpseName( "the warden's corpse" )]
	public class Warden : BaseCreature
	{
		[Constructable]
		public Warden () : base( AIType.AI_Berserk, FightMode.Strongest , 20, 1, 0.1, 0.2 )
		{
			Name = "the Warden";
			Body = 281;
			BaseSoundID = 427;

			SetStr( 1096, 1185 );
			SetDex( 177, 255 );
			SetInt( 151, 250 );

			SetHits( 1750, 2000 );

			SetDamage( 10, 15 );

			SetSkill( SkillName.Anatomy, 100.0, 150.0 );
			SetSkill( SkillName.MagicResist, 80.1, 95.0 );
			SetSkill( SkillName.Tactics, 100.1, 150.0 );
			SetSkill( SkillName.Wrestling, 100.1, 150.0 );
			SetSkill( SkillName.Parry, 75.0, 90.0 );

			Fame = 25000;
			Karma = -25000;

			VirtualArmor = 110;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Champion );
			AddLoot( LootPack.AosSuperBoss );
			AddLoot( LootPack.MedScrolls, 10 );
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

		public override bool Unprovokable{ get{ return true; } }
		public override bool AreaPeaceImmune{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 2; } }
		public override bool AutoDispel{ get{ return true; } }

		// physical damage reduced by 10 percent
		public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
		{
			if (damage != null){
				damage = damage - (damage/10);
			}
		}

		public Warden( Serial serial ) : base( serial )
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

		public override OppositionGroup OppositionGroup
		{
			get { return OppositionGroup.TestGroup; }
		}
	}
}