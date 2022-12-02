using Server.Items;
using System;

namespace Server.Mobiles
{
    [CorpseName( "mind flayer's shield" )]
	public class MindFlayerShield : BaseCreature
	{

		public override bool DeleteCorpseOnDeath { get { return true; } }

		public override bool DisallowAllMoves { get { return true; } }

		[Constructable]
		public MindFlayerShield() : base( AIType.AI_Archer, FightMode.None, 10, 20, 0.2, 0.4 )
		{
			Name = "Mind Flayer's shield";
			Body = 165;
			Hue = 1174;
			BaseSoundID = 0x482;

			SetStr( 50, 60 );
			SetDex( 50, 60 );
			SetInt( 50, 60 );

			SetHits( 100, 120 );

			SetDamage( 0, 0 );

			SetSkill( SkillName.EvalInt, 0.0, 1.1 );
			SetSkill( SkillName.Magery, 0.0, 1.1 );
			SetSkill( SkillName.MagicResist, 0.0, 1.1 );
			SetSkill( SkillName.Tactics, 0.0, 1.1 );
			SetSkill( SkillName.Wrestling, 0.0, 1.1 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 10;
			this.BeginDeleteTimer(1.0);
		}


		public MindFlayerShield( Serial serial ) : base( serial )
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.TestGroup; }
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

			Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5022 );

		}

	}
}