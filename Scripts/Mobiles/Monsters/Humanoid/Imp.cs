namespace Server.Mobiles
{
    [CorpseName( "an imp corpse" )]
	public class Imp : BaseCreature
	{
		[Constructable]
		public Imp() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an imp";
			Body = 74;
			BaseSoundID = 422;

			SetStr( 91, 115 );
			SetDex( 61, 80 );
			SetInt( 86, 105 );

			SetHits( 55, 70 );

			SetDamage( 10, 14 );

			SetSkill( SkillName.EvalInt, 20.1, 30.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 30.1, 50.0 );
			SetSkill( SkillName.Tactics, 42.1, 50.0 );
			SetSkill( SkillName.Wrestling, 40.1, 44.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 30;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 83.1;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 6; } }
		public override HideType HideType{ get{ return HideType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanFly { get { return true; } }

		public Imp( Serial serial ) : base( serial )
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

	public class ImpThief : BaseCreature
	{
		[Constructable]
		public ImpThief() : base( AIType.AI_Thief, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a thieving imp";
			Body = 74;
			BaseSoundID = 422;

			SetStr( 91, 115 );
			SetDex( 61, 80 );
			SetInt( 86, 105 );

			SetHits( 55, 70 );

			SetDamage( 10, 14 );
			Hidden = true;

			SetSkill( SkillName.EvalInt, 20.1, 30.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 30.1, 50.0 );
			SetSkill( SkillName.Tactics, 42.1, 50.0 );
			SetSkill( SkillName.Wrestling, 40.1, 44.0 );
			SetSkill( SkillName.Stealing, 100.1, 120.0 );


			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 30;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 83.1;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 6; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override HideType HideType{ get{ return HideType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanFly { get { return true; } }

		public ImpThief( Serial serial ) : base( serial )
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

		public override void RevealingAction()
		{
			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if (this.IsEnemy(m) && !(m.AccessLevel > AccessLevel.Player))
				{
					Hidden = false;
					return;
				} 
			}
			Hidden = true;
		}
		public override void OnThink()
		{
			bool atLeastOneOpponent = false;
			foreach ( Mobile m in this.GetMobilesInRange( 3 ) )
			{
				if (this.IsEnemy(m) && !(m.AccessLevel > AccessLevel.Player)){
					atLeastOneOpponent = true;
				}
				if (m == null ){ 
					Hidden = true;
					break;
					}
				
				Hidden = true;
			}

			if (atLeastOneOpponent){
				Hidden = false;
			}
		}
	}
}