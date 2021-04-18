namespace Server.Items
{
    [Furniture]
	[Flipable( 0x1E60, 0x1E67 )]
	public class TrophyBearHead : Item
	{
		[Constructable]
		public TrophyBearHead() : base(0x1E60)
		{
			Weight = 20.0;
		}

		public TrophyBearHead(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 20.0;
		}
	}

	[Furniture]
	[Flipable( 0x1E61, 0x1E68 )]
	public class TrophyDeerHead : Item
	{
		[Constructable]
		public TrophyDeerHead() : base(0x1E61)
		{
			Weight = 20.0;
		}

		public TrophyDeerHead(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 20.0;
		}
	}

	[Furniture]
	[Flipable(  0x1E62, 0x1E69 )]
	public class TrophyBigFish : Item
	{
		[Constructable]
		public TrophyBigFish() : base(0x1E62)
		{
			Weight = 20.0;
		}

		public TrophyBigFish(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 20.0;
		}
	}

	[Furniture]
	[Flipable(  0x1E63, 0x1E66 )]
	public class TrophyGorillaHead : Item
	{
		[Constructable]
		public TrophyGorillaHead() : base(0x1E63)
		{
			Weight = 20.0;
		}

		public TrophyGorillaHead(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 20.0;
		}
	}

	[DynamicFliping]
	[Flipable( 0x1E64, 0x1E6B )]
	public class TrophyOrcHead : Item
	{
		[Constructable]
		public TrophyOrcHead() : base(0x1E64)
		{
			Weight = 20;
		}

		public TrophyOrcHead(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 20.0;
		}
	}

	[DynamicFliping]
	[Flipable( 0x1E65, 0x1E6C )]
	public class TrophyPolarBearHead : Item
	{
		[Constructable]
		public TrophyPolarBearHead() : base( 0x1E65 )
		{
			Weight = 20.0;
		}

		public TrophyPolarBearHead( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 20.0;
		}
	}

	[DynamicFliping]
	[Flipable( 0x1E66, 0x1E6D )]
	public class TrophyTrollHead : Item
	{
		[Constructable]
		public TrophyTrollHead() : base( 0x1E65 )
		{
			Weight = 20.0;
		}

		public TrophyTrollHead( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 20.0;
		}
	}

	[DynamicFliping]
	[Flipable( 0x2234, 0x2235 )]
	public class TrophyDragonHead : Item
	{
		[Constructable]
		public TrophyDragonHead() : base( 0x2234 )
		{
			Weight = 200.0;
		}

		public TrophyDragonHead( Serial serial ) : base( serial )
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