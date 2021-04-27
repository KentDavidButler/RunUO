using Server.Items;

namespace Server.Mobiles
{
    public class TestPlayer : BaseCreature 
	{ 
		[Constructable] 
		public TestPlayer() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			SpeechHue = Utility.RandomDyedHue(); 
			Title = "Test Player"; 
			Hue = Utility.RandomSkinHue(); 

 
			this.Body = 0x190; 
			this.Name = NameList.RandomName( "male" ); 
			AddItem( new ShortPants( Utility.RandomRedHue() ) ); 


			SetStr( 99, 100 );
			SetDex( 99, 100 );
			SetInt( 25, 26 );

			SetDamage( 20, 25 );

			SetSkill( SkillName.Anatomy, 100.0 );
			SetSkill( SkillName.Macing, 100.0 );
			SetSkill( SkillName.Swords, 100.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Healing, 100.0);
			SetSkill( SkillName.Parry, 100.0);
			SetSkill(SkillName.MagicResist, 100.0);


			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 40;

			AddItem( new ThighBoots( Utility.RandomRedHue() ) ); 
			AddItem( new Surcoat( Utility.RandomRedHue() ) );    
			AddItem( new WarAxe());
			AddItem( new HeaterShield());

			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Meager );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public TestPlayer( Serial serial ) : base( serial ) 
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

		public override OppositionGroup OppositionGroup
		{
			get { return OppositionGroup.TestGroup; }
		}
	} 
}