using System;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a wisp corpse" )]
	public class Wisp : BaseCreature
	{
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Wisp; } }

		public override TimeSpan ReacquireDelay { get { return TimeSpan.FromSeconds( 1.0 ); } }

		[Constructable]
		public Wisp() : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a wisp";
			Body = 58;
			BaseSoundID = 466;

			SetStr( 196, 225 );
			SetDex( 196, 225 );
			SetInt( 196, 225 );

			SetHits( 476, 552 );

			SetDamage( 18, 25 );

			SetSkill( SkillName.EvalInt, 80.0 );
			SetSkill( SkillName.Magery, 80.0 );
			SetSkill( SkillName.MagicResist, 80.0 );
			SetSkill( SkillName.Tactics, 80.0 );
			SetSkill( SkillName.Wrestling, 80.0 );

			Fame = 4000;
			Karma = 0;

			VirtualArmor = 55;

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 2 );
			AddLoot( LootPack.HighScrolls, 2 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 3; } }		

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public Wisp( Serial serial ) : base( serial )
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