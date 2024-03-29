using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public class OppositionGroup
	{
		private Type[][] m_Types;

		public OppositionGroup( Type[][] types )
		{
			m_Types = types;
		}

		public bool IsEnemy( object from, object target )
		{
			int fromGroup = IndexOf( from );
			int targGroup = IndexOf( target );

			return fromGroup != -1 && targGroup != -1 && fromGroup != targGroup;
		}

		public int IndexOf( object obj )
		{
			if ( obj == null )
				return -1;

			Type type = obj.GetType();

			for ( int i = 0; i < m_Types.Length; ++i )
			{
				Type[] group = m_Types[i];

				bool contains = false;

				for ( int j = 0; !contains && j < group.Length; ++j )
					contains = group[j].IsAssignableFrom( type );

				if ( contains )
					return i;
			}

			return -1;
		}

		private static OppositionGroup m_TestGroup = new OppositionGroup( new Type[][]
			{
				new Type[]
				{

				},
				new Type[]
				{
					typeof( TestPlayer ),
				}
			} );

		public static OppositionGroup TestGroup
		{
			get{ return m_TestGroup; }
		}

		private static OppositionGroup m_TerathansAndOphidians = new OppositionGroup( new Type[][]
			{
				new Type[]
				{
					typeof( TerathanAvenger ),
					typeof( TerathanDrone ),
					typeof( TerathanMatriarch ),
					typeof( TerathanWarrior )
				},
				new Type[]
				{
					typeof( OphidianArchmage ),
					typeof( OphidianKnight ),
					typeof( OphidianMage ),
					typeof( OphidianMatriarch ),
					typeof( OphidianWarrior )
				}
			} );

		public static OppositionGroup TerathansAndOphidians
		{
			get{ return m_TerathansAndOphidians; }
		}

		private static OppositionGroup m_SavagesAndOrcs = new OppositionGroup( new Type[][]
			{
				new Type[]
				{
					typeof( Orc ),
					typeof( OrcCaptain ),
					typeof( OrcishLord ),
					typeof( OrcishMage ),
					typeof( OrcishBrute )
				},
				new Type[]
				{
					typeof( Ratman ),
					typeof( RatmanArcher ),
					typeof( RatmanMage ),
					typeof( RatmanThief)
				}
			} );

		public static OppositionGroup SavagesAndOrcs
		{
			get{ return m_SavagesAndOrcs; }
		}
		
		private static OppositionGroup m_FeyAndUndead = new OppositionGroup( new Type[][]
			{
				new Type[]
				{
					typeof( Wisp )
                },
				new Type[]
				{
					typeof( LichLord ),
					typeof( AncientLich ),
					typeof( Shade ),
					typeof( Spectre ),
					typeof( Wraith ),
					typeof( BoneKnight ),
					typeof( Ghoul ),
					typeof( Mummy ),
					typeof( SkeletalKnight ),
					typeof( Skeleton ),
					typeof( Zombie ),
					typeof( RottingCorpse ),
					typeof( Lich ),
					typeof( ShadowWisp ),
					typeof( DarkWisp )
				}
			} );

		public static OppositionGroup FeyAndUndead
		{
			get{ return m_FeyAndUndead; }
		}
	}
}