using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[Flipable( 0x1EBA, 0x1EBB )]
	public class TaxidermyTool : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefTaxidermy.CraftSystem; } }

		[Constructable]
		public TaxidermyTool() : base( 0x1EBA )
		{
			Name = "Taxidermy Tool";
			Weight = 1.0;
			Hue = 1366;
		}

		[Constructable]
		public TaxidermyTool( int uses ) : base( uses, 0x1028 )
		{
			Weight = 1.0;
		}

		public TaxidermyTool( Serial serial ) : base( serial )
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
}