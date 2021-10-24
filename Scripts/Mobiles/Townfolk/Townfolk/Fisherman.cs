using System.Collections.Generic;
using Server.Items;
using System;

namespace Server.Mobiles
{
    public class Fisherman : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.FishermensGuild; } }

		[Constructable]
		public Fisherman() : base( "the fisher" )
		{
			SetSkill( SkillName.Fishing, 75.0, 98.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBFisherman() );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new FishingPole() );
		}

		public Fisherman( Serial serial ) : base( serial )
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
	}

	public class DockFisherman : BaseCreature
	{
		public DateTime m_fishDelay = DateTime.Now;

		[Constructable]
		public DockFisherman() : base( AIType.AI_Vendor, FightMode.None, 10, 1, 0.2, 0.4 ) 
		{
			
			this.Body = 0x190; 
			this.Name = NameList.RandomName( "male" );
			Title = "the fisher";
			SpeechHue = Utility.RandomDyedHue();
			SetSkill( SkillName.Fishing, 75.0, 98.0 );

			Utility.AssignRandomHair( this, Utility.RandomHairHue() );
			Utility.AssignRandomFacialHair( this, Utility.RandomHairHue() );
			Hue = Utility.RandomSkinHue(); 

			
			AddItem( new Shoes( Utility.RandomRedHue() ) ); 
			AddItem( new ShortPants( Utility.RandomRedHue() ) );
			AddItem( new FishingPole());
			switch (Utility.Random(6))
			{
				case 0: AddItem( new Shirt( Utility.RandomRedHue() ) ); break;
				case 1: AddItem( new FancyShirt( Utility.RandomRedHue() ) ); break;
				case 3: AddItem( new BodySash( Utility.RandomRedHue() ) ); break;
				case 4: AddItem( new FullApron( Utility.RandomRedHue() ) ); break;
				case 5: break;
			}
		}


		public DockFisherman( Serial serial ) : base( serial )
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

		public override void OnThink()
        {
            base.OnThink();

			Item fishpole = null;
			if (DateTime.Now > this.m_fishDelay)
            {
				foreach (Item element in this.Items)
                { 
					if (element.ToString().Contains("FishingPole")){
						Console.WriteLine("found the fishpole");
						fishpole = element;
                    }
				}
				Console.WriteLine("I Should be finishing");
				m_fishDelay = DateTime.Now + TimeSpan.FromSeconds(Utility.Random(31));
				if(fishpole != null)
                {
					Console.WriteLine("lets start fishing!");
					Server.Engines.Harvest.Fishing.System.BeginHarvesting(this, fishpole);
                }

			}
        }

	}
	
}