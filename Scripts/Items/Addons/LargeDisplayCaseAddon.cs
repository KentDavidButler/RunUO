namespace Server.Items
{
    public class LargeDisplayCaseEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new LargeDisplayCaseEastDeed(); } }

		[Constructable]
		public LargeDisplayCaseEastAddon()
		{
			AddComponent( new AddonComponent( 0x0B08 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0x0B06 ), 0, 1, 0 );
		}

		public LargeDisplayCaseEastAddon( Serial serial ) : base( serial )
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

	public class LargeDisplayCaseEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new LargeDisplayCaseEastAddon(); } }
		public override int LabelNumber{ get{ return 1022719; } } // large forge (east)

		[Constructable]
		public LargeDisplayCaseEastDeed()
		{
		}

		public LargeDisplayCaseEastDeed( Serial serial ) : base( serial )
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

	public class LargeDisplayCaseSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new LargeDisplayCaseSouthDeed(); } }

		[Constructable]
		public LargeDisplayCaseSouthAddon()
		{
			AddComponent(new ForgeComponent(0x0B02), 0, 0, 0);
			AddComponent(new ForgeComponent(0x0B00), 1, 0, 0);
		}

		public LargeDisplayCaseSouthAddon(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	public class LargeDisplayCaseSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new LargeDisplayCaseSouthAddon(); } }
		public override int LabelNumber { get { return 1022719; } } // large forge (south)

		[Constructable]
		public LargeDisplayCaseSouthDeed()
		{
		}

		public LargeDisplayCaseSouthDeed(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	public class ExtraLargeDisplayCaseEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new ExtraLargeDisplayCaseEastDeed(); } }

		[Constructable]
		public ExtraLargeDisplayCaseEastAddon()
		{
			AddComponent(new AddonComponent(0x0B08), 0, 0, 0);
			AddComponent(new AddonComponent(0x0B07), 0, 1, 0);
			AddComponent(new AddonComponent(0x0B06), 0, 2, 0);
		}

		public ExtraLargeDisplayCaseEastAddon(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	public class ExtraLargeDisplayCaseEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new ExtraLargeDisplayCaseEastAddon(); } }
		public override int LabelNumber { get { return 1022719; } } // large forge (east)

		[Constructable]
		public ExtraLargeDisplayCaseEastDeed()
		{
		}

		public ExtraLargeDisplayCaseEastDeed(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	public class ExtraLargeDisplayCaseSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new ExtraLargeDisplayCaseSouthDeed(); } }

		[Constructable]
		public ExtraLargeDisplayCaseSouthAddon()
		{
			AddComponent(new ForgeComponent(0x0B02), 0, 0, 0);
			AddComponent(new ForgeComponent(0x0B01), 1, 0, 0);
			AddComponent(new ForgeComponent(0x0B00), 2, 0, 0);
		}

		public ExtraLargeDisplayCaseSouthAddon(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	public class ExtraLargeDisplayCaseSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new ExtraLargeDisplayCaseSouthAddon(); } }
		public override int LabelNumber { get { return 1022719; } } // large forge (south)

		[Constructable]
		public ExtraLargeDisplayCaseSouthDeed()
		{
		}

		public ExtraLargeDisplayCaseSouthDeed(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}