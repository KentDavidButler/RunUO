using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefTaxidermy : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tailoring; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044010; } // <CENTER>TAXIDERMY KIT MENU</CENTER>
		}

		public override string GumpTitleString
		{
		    get{ return "TAXIDERMY KIT MENU"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTaxidermy();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.1; // 10%
		}

		private DefTaxidermy() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x23D );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{	
				return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;
			
			//Start Common Animals
			index = AddCraft( typeof( TrophyBearHead ), "Trophy", "Bear Head", 99.0, 102.0, typeof( Cotton ), "Bale Of Cotton", 10, "" );
			AddRes( index, typeof( Hides ), "Leather Hides", 5, "" );
			AddRes( index, typeof( Bone ), "Bones", 25, "" );
			AddRes( index, typeof( Board ), "Boards", 5, "" );
			
			index = AddCraft( typeof( TrophyDeerHead ), "Trophy", "Dear Head", 99.0, 102.0, typeof( Cotton ), "Bale Of Cotton", 10, "" );
			AddRes( index, typeof( Hides ), "Leather Hides", 5, "" );
			AddRes( index, typeof( Bone ), "Bones", 25, "" );
			AddRes( index, typeof( Board ), "Boards", 5, "" );

			index = AddCraft( typeof( TrophyPolarBearHead), "Trophy", "Polar Bear Head", 99.0, 102.0, typeof( Cotton ), "Bale Of Cotton", 10, "" );
			AddRes( index, typeof( Hides ), "Leather Hides", 5, "" );
			AddRes( index, typeof( Bone ), "Bones", 25, "" );
			AddRes( index, typeof( Board ), "Boards", 5, "" );

			index = AddCraft( typeof( TrophyGorillaHead), "Trophy", "Gorilla Head", 99.0, 102.0, typeof( Cotton ), "Bale Of Cotton", 10, "" );
			AddRes( index, typeof( Hides ), "Leather Hides", 5, "" );
			AddRes( index, typeof( Bone ), "Bones", 25, "" );
			AddRes( index, typeof( Board ), "Boards", 5, "" );
			
			index = AddCraft( typeof( TrophyBigFish ), "Trophy", "Mounted Big Fish", 99.0, 105.0, typeof( Cotton ), "Bale Of Cotton", 10, "" );
			AddRes( index, typeof( BigFish ), "A Big Fish", 1, "" );
			AddRes( index, typeof( Board ), "Boards", 15, "" );

			index = AddCraft( typeof( TrophyTrollHead ), "Trophy", "Troll Head", 99.0, 110.0, typeof( Cotton ), "Bale Of Cotton", 15, "" );;
			AddRes( index, typeof( Hides ), "Leather Hides", 10, "" );
			AddRes( index, typeof( Bone ), "Bones", 25, "" );
			AddRes( index, typeof( Board ), "Boards", 5, "" );
			
			index = AddCraft( typeof( TrophyOrcHead ), "Trophy", "Orc Head", 99.0, 110.0, typeof( Cotton ), "Bale Of Cotton", 10, "" );
			AddRes( index, typeof( Hides ), "Leather Hides", 5, "" );
			AddRes( index, typeof( Bone ), "Bones", 25, "" );
			AddRes( index, typeof( Board ), "Boards", 5, "" );

			index = AddCraft( typeof( TrophyDragonHead ), "Trophy", "Dragon Head", 99.0, 190.0, typeof( Cotton ), "Bale Of Cotton", 24, "" );
			AddRes( index, typeof( Hides ), "Leather Hides", 45, "" );
			AddRes( index, typeof( Bone ), "Bones", 75, "" );
			AddRes( index, typeof( Board ), "Boards", 5, "" );
			
			// Set the overridable material
			SetSubRes( typeof( Leather ), 1049150 );

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes( typeof( Leather ),		1049150, 00.0, 1044462, 1049311 );
			//AddSubRes( typeof( SpinedLeather ),	1049151, 65.0, 1044462, 1049311 );
			//AddSubRes( typeof( HornedLeather ),	1049152, 80.0, 1044462, 1049311 );
			//AddSubRes( typeof( BarbedLeather ),	1049153, 100.0, 1044462, 1049311 );
			//AddSubRes( typeof( DragonLeather ),	//"DRAGON LEATHER / HIDES", 105.0, 1049311 );
			//AddSubRes( typeof( DaemonLeather ),	//"DAEMON LEATHER / HIDES", 110.0, 1049311 );
			//AddSubRes( typeof( FrostLeather ),	//"FROST LEATHER / HIDES", 115.0, 1049311 );
			//AddSubRes( typeof( EtherealLeather ),	//"ETHEREAL LEATHER / HIDES", 117.0, 1049311 );

			MarkOption = true;
			//Repair = Core.AOS;
			//CanEnhance = Core.AOS;
		}
	}
}