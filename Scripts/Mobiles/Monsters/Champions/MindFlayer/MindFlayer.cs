using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Globalization;
using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a mind flayer's corpse" )]
	public class MindFlayer : BaseCreature
	{
		private DateTime m_NextBarrierTime;
		private double m_RespawnTimeLength;

		public override bool DisallowAllMoves{ get{ return true; } }
		private bool m_ActiveBarrier;

		[Constructable]
		public MindFlayer() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Mind Flayer";
			Body = 79;
			Hue = 1175;
			BaseSoundID = 412;
			

			SetStr( 416, 505 );
			SetDex( 146, 165 );
			SetInt( 566, 655 );

			SetHits( 250, 303 );

			SetDamage( 11, 13 );

			SetSkill( SkillName.Necromancy, 90, 110.0 );
			SetSkill( SkillName.SpiritSpeak, 90.0, 110.0 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 150.5, 200.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 18000;
			Karma = -18000;

			VirtualArmor = 50;
			PackItem( new GnarledStaff() );
			m_NextBarrierTime = DateTime.UtcNow;

		}

		//If shields are up, take no damage
		public override void Damage(int amount, Mobile from, bool willKill)
		{
			if (amount > 0) {	
				if (m_ActiveBarrier){
					amount = 0;
					willKill = false;
					return;
				}else{
					base.Damage(amount,from,willKill);
				}
			}

		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.TestGroup; }
		}

		public override void GenerateLoot()
		{
			AddLoot(LootPack.MagicItemsChampion, 1);
			AddLoot( LootPack.MedScrolls, 2 );
			AddLoot( LootPack.HighScrolls, 8 );
			AddLoot( LootPack.Gems, 8 );
			AddLoot( LootPack.UniqueItem, 1);
			AddLoot(LootPack.UniqueItem, 1);
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 4; } }

		public MindFlayer( Serial serial ) : base( serial )
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

		public override void OnThink()
		{
			base.OnThink();
			m_ActiveBarrier = IsActiveShield;

			// Shorten lenght between ShieldSpawns depending how much life is left
			if (this.Hits < (this.HitsMax/4)){
			// If less than 1/4 health
				m_RespawnTimeLength = 0.6;
			}else if (this.Hits < (this.HitsMax/2)){
			// If less than 1/2 health
				m_RespawnTimeLength = 1.0;
			}else{
				m_RespawnTimeLength = 1.5;
			}

			if ( Combatant != null  && DateTime.UtcNow > this.m_NextBarrierTime )
			{
				this.m_NextBarrierTime = DateTime.UtcNow.AddMinutes(m_RespawnTimeLength);
				Map map = this.Map;
				BaseCreature bc = (BaseCreature)Activator.CreateInstance(m_Creatures[0]);

				if( bc != null )
				{

					this.Say("Summoning a shield to protect me!");
					Point3D spawnLoc = GetSpawnPosition();

					DoEffect( spawnLoc, map );

					Timer.DelayCall( TimeSpan.FromSeconds( 1 ), delegate()
					{
						bc.Home = Location;
						bc.RangeHome = 3;

						bc.MoveToWorld( spawnLoc, map );

						DoEffect( spawnLoc, map );

						bc.ForceReacquire();
					} );

				}

			}

			// Chance to restore Mana off shield
			if (0.3 > Utility.RandomDouble() && Combatant != null && m_ActiveBarrier && this.Mana < (this.ManaMax/2)) {
				if (LeechShield){
					Map map = this.Map;
					this.Mana = this.Mana + (this.ManaMax/2);
					DoEffect(this.Location, map );

					this.Say("Sourcing energy from a shield for mana!");
				}
				
			}

			// Chance to restore Mana off shield
			if (0.3 > Utility.RandomDouble() && Combatant != null && m_ActiveBarrier && this.Hits < (this.HitsMax/2)) {
				if (LeechShield){
					Map map = this.Map;
					this.Hits = (this.HitsMax/3) + this.Hits;
					DoEffect(this.Location, map );

					this.Say("Sourcing energy from a shield for health!");
				}
				
			}

		}

		private static Type[] m_Creatures = new Type[]
		{
			typeof( MindFlayerShield ),
		};

		public Point3D GetSpawnPosition()
		{
			Map map = Map;
			int m_SpawnRange = 3;

			if( map == null )
				return Location;

			// Try 10 times to find a Spawnable location.
			for( int i = 0; i < 10; i++ )
			{
				int x = Location.X + (Utility.Random( m_SpawnRange * 2 + 1 ) - m_SpawnRange);
				int y = Location.Y + (Utility.Random( m_SpawnRange * 2 + 1 ) - m_SpawnRange);
				int z = Map.GetAverageZ( x, y );

				if( Map.CanSpawnMobile( new Point2D( x, y ), this.Z ) )
					return new Point3D( x, y, this.Z );
				else if( Map.CanSpawnMobile( new Point2D( x, y ), z ) )
					return new Point3D( x, y, z );
			}

			return this.Location;
		}

		public virtual void DoEffect( Point3D loc, Map map )
		{
			Effects.SendLocationParticles( EffectItem.Create( loc, map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
			Effects.PlaySound( loc, map, 0x225 );
		}


		bool IsActiveShield{
			get {

				foreach (Mobile mob in this.GetMobilesInRange(20))
				{
					if (mob.RawName == "Mind Flayer's shield" ){
						return true;
					}
				}
				
				return false;
			}

		}

		bool LeechShield{
			get {

				foreach (Mobile mob in this.GetMobilesInRange(20))
				{
					if (mob.RawName == "Mind Flayer's shield" ){
						mob.Damage(50);
						return true;
					}
				}
				return false;
			}

		}

	}
}

