using Server.Items;

namespace Server.Mobiles
{
    public class Villager : BaseCreature
	{


		[Constructable]
		public Villager()
			: base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			InitStats( 31, 41, 51 );

			SpeechHue = Utility.RandomDyedHue();

			SetSkill( SkillName.Cooking, 65, 88 );
			SetSkill( SkillName.Snooping, 65, 88 );
			SetSkill( SkillName.Stealing, 65, 88 );

			Hue = Utility.RandomSkinHue();

			if( this.Female = Utility.RandomBool() )
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
				AddItem( new Kilt( Utility.RandomDyedHue() ) );
				AddItem( new Shirt( Utility.RandomDyedHue() ) );
				AddItem( new ThighBoots() );
			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
				AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
				AddItem( new Shirt( Utility.RandomDyedHue() ) );
				AddItem( new Sandals() );
			}

			AddItem( new Bandana( Utility.RandomDyedHue() ) );
			AddItem( new Dagger() );

			Utility.AssignRandomHair( this );

			Container pack = new Backpack();

			pack.DropItem( new Gold( 250, 300 ) );

			pack.Movable = false;

			AddItem( pack );
		}


		public override bool CanTeach { get { return true; } }
		public override bool ClickTitle { get { return false; } }

		public Villager( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnThink()
		{
			base.OnThink();

			if (Utility.Random(60) > 59)
			{
				// no optinal talk, vendor, funny , aggresive 
				this.Say(this.NPCRandomSpeech(this.Female, false, true, true));
			}
		}
	}

	public class Drunk : BaseCreature
	{


		[Constructable]
		public Drunk()
			: base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			InitStats( 31, 41, 51 );

			SpeechHue = Utility.RandomDyedHue();

			Hue = Utility.RandomSkinHue();

			if( this.Female = Utility.RandomBool() )
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );

			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
			}


			Utility.AssignRandomHair( this );

			Container pack = new Backpack();

			pack.DropItem( new Gold( 10, 15 ) );

			pack.Movable = false;

			AddItem( pack );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();
		}

		public override bool CanTeach { get { return false; } }
		public override bool ClickTitle { get { return false; } }

		public Drunk( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnThink()
		{
			base.OnThink();

			if (Utility.Random(60) > 57)
			{
				// 30 is for drunk, vendor, funny , aggresive 
				List<int> OptionalSpeechText = new List<int> { 30 };
				this.Say(this.NPCRandomSpeech(this.Female, false, true, true, optionalStatements: OptionalSpeechText));
			}
		}
	}
}
