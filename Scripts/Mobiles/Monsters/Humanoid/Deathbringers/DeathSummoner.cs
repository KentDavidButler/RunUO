using Server.Items;

namespace Server.Mobiles
{
    public class DeathSummoner : BaseCreature
    {
		[Constructable]
        public DeathSummoner() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
        {
            Body = 0x190;
            Name = NameList.RandomName( "male" + " a Deathbringer");
            Title = ", a Summoner of Death";

            SetStr(351, 400);
            SetDex(101, 150);
            SetInt(502, 700);

            SetHits(421, 480);

            SetDamage(5, 15);

            SetSkill(SkillName.Wrestling, 90.1, 100.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 90.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Meditation, 120.1, 130.0);

            VirtualArmor = 36;
            Fame = 10000;
            Karma = -10000;

            var gloves = new LeatherGloves();
            gloves.Hue = 0x66D;
            AddItem(gloves);

            var helm = new BoneHelm();
            helm.Hue = 0x835;
            AddItem(helm);

            var necklace = new Necklace();
            necklace.Hue = 0x66D;
            AddItem(necklace);

            var cloak = new Cloak();
            cloak.Hue = 0x66D;
            AddItem(cloak);

            var kilt = new Kilt();
            kilt.Hue = 0x66D;
            AddItem(kilt);

            var sandals = new Sandals();
            sandals.Hue = 0x66D;
            AddItem(sandals);
        }

        public DeathSummoner(Serial serial) : base(serial)
        {
        }

        public override bool ClickTitle{ get{ return false; } }
        public override bool ShowFameTitle{ get{ return false; } }

        public override bool AlwaysMurderer{ get{ return true; } }
        public override bool Unprovokable{ get{ return true; } }

        public override int GetIdleSound()
		{
			return 0x184;
		}

        public override int GetAngerSound()
		{
			return 0x286;
		}

        public override int GetDeathSound()
		{
			return 0x288;
		}

        public override int GetHurtSound()
		{
			return 0x19F;
		}

        public override bool OnBeforeDeath()
        {
            var rm = new BoneMagi();
            rm.Team = Team;
            rm.Combatant = Combatant;
            rm.NoKillAwards = true;

            if (rm.Backpack == null)
            {
                var pack = new Backpack();
                pack.Movable = false;
                rm.AddItem(pack);
            }

            for (var i = 0; i < 2; i++)
            {
                LootPack.FilthyRich.Generate(this, rm.Backpack, true, LootPack.GetLuckChanceForKiller(this));
                LootPack.FilthyRich.Generate(this, rm.Backpack, false, LootPack.GetLuckChanceForKiller(this));
            }

            Effects.PlaySound( this, this.Map, GetDeathSound() );
            Effects.SendLocationEffect(this, this.Map, 0x3709, 30, 10, 0x835, 0);
            rm.MoveToWorld(Location, Map);

            Delete();
            return false;
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
