using System.Collections.Generic;
using System;
using Server.Items;

namespace Server.Mobiles
{
    public class Carpenter : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.TinkersGuild; } }

		[Constructable]
		public Carpenter() : base( "the carpenter" )
		{
			SetSkill( SkillName.Carpentry, 85.0, 100.0 );
			SetSkill( SkillName.Lumberjacking, 60.0, 83.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBStavesWeapon() );
			m_SBInfos.Add( new SBCarpenter() );
			m_SBInfos.Add( new SBWoodenShields() );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new HalfApron() );
		}

		public Carpenter( Serial serial ) : base( serial )
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

	public class Lumberjack : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public DateTime m_chopDelay = DateTime.Now;

		[Constructable]
		public Lumberjack() : base( "the lumberjack" )
		{
			SetSkill( SkillName.Carpentry, 85.0, 100.0 );
			SetSkill( SkillName.Lumberjacking, 80.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBStavesWeapon() );
			m_SBInfos.Add( new SBCarpenter() );
			m_SBInfos.Add( new SBWoodenShields() );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new HalfApron() );
			AddItem( new Axe());
		}

		public Lumberjack( Serial serial ) : base( serial )
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

			if (DateTime.Now > m_chopDelay)
            {
				// trying to set timespan lower because of low probability of chopping
				m_chopDelay = DateTime.Now + TimeSpan.FromSeconds(Utility.Random(15));
				this.DoLumberjacking();	
				this.Freeze(TimeSpan.FromSeconds(5));
			}
        }
    }
}