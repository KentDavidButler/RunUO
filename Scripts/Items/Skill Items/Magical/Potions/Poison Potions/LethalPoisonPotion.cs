namespace Server.Items
{
    public class LethalPoisonPotion : BasePoisonPotion
	{
		public override int LabelNumber{ get{ return 1041312; } } // an unknown yellow potion

		public override Poison Poison{ get{ return Poison.Lethal; } }

		public override double MinPoisoningSkill{ get{ return 98.0; } }
		public override double MaxPoisoningSkill{ get{ return 100.0; } }

		[Constructable]
		public LethalPoisonPotion() : base( PotionEffect.PoisonLethal )
		{
		}

		public LethalPoisonPotion( Serial serial ) : base( serial )
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