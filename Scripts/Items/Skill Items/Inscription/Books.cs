using System;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute(0x0FBD, 0x0FBE)]
	public class LargOpenBook : Item
	{
		[Constructable]
		public LargOpenBook() : base(0x0FBD)
		{
			Weight = 5.0;
			Movable = true;
		}

		public LargOpenBook(Serial serial) : base(serial)
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


	public class DamagedBooks : Item
	{
		[Constructable]
		public DamagedBooks() : base(0x0C16)
		{
			Weight = 100.0;
			Movable = true;
		}

		public DamagedBooks(Serial serial) : base(serial)
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

	public class BookOpenSmall : Item
	{
		[Constructable]
		public BookOpenSmall() : base(0x0FF3)
		{
			Weight = 1.0;
			Movable = true;
		}

		public BookOpenSmall(Serial serial) : base(serial)
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

	public class BookOpenMedium : Item
	{
		[Constructable]
		public BookOpenMedium() : base(0x0FF4)
		{
			Weight = 3.0;
			Movable = true;
		}

		public BookOpenMedium(Serial serial) : base(serial)
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

	public class BookOpenUpsideDown : Item
	{
		[Constructable]
		public BookOpenUpsideDown() : base(0x1E20)
		{
			Weight = 5.0;
			Movable = true;
		}

		public BookOpenUpsideDown(Serial serial) : base(serial)
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

	public class BooksOpenUpsideDown : Item
	{
		[Constructable]
		public BooksOpenUpsideDown() : base(0x1E21)
		{
			Weight = 10.0;
			Movable = true;
		}

		public BooksOpenUpsideDown(Serial serial) : base(serial)
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

	[FlipableAttribute(0x1E22, 0x1E23)]
	public class BooksOpenLargeStacked : Item
	{
		[Constructable]
		public BooksOpenLargeStacked() : base(0x1E22)
		{
			Weight = 15.0;
			Movable = true;
		}

		public BooksOpenLargeStacked(Serial serial) : base(serial)
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

	[FlipableAttribute(0x1E24, 0x1E25)]
	public class BooksStacked : Item
	{
		[Constructable]
		public BooksStacked() : base(0x1E24)
		{
			Weight = 45.0;
			Movable = true;
		}

		public BooksStacked(Serial serial) : base(serial)
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

	[FlipableAttribute(0x2D4B, 0x2D4C)]
	public class bookstand1 : Item
	{
		[Constructable]
		public bookstand1() : base(0x2D4B)
		{
			Weight = 45.0;
			Movable = true;
		}

		public bookstand1(Serial serial) : base(serial)
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

	[FlipableAttribute(0x2D4D, 0x2D4E)]
	public class bookstand2 : Item
	{
		[Constructable]
		public bookstand2() : base(0x2D4D)
		{
			Weight = 45.0;
			Movable = true;
		}

		public bookstand2(Serial serial) : base(serial)
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

	[FlipableAttribute(0x2DDD, 0x2DDE)]
	public class bookstand3 : Item
	{
		[Constructable]
		public bookstand3() : base(0x2DDD)
		{
			Weight = 45.0;
			Movable = true;
		}

		public bookstand3(Serial serial) : base(serial)
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
