using System.IO;
using Terraria;
using Terraria.ModLoader;
using TranslateTest2.Content.Buffs; // WhipTag

namespace TranslateTest2.Common.GlobalItems
{
    public class PrefixGlobalItem : GlobalItem
    {
        public float WhipRangeMult = 1f;
        public WhipTag wTag;
        public bool CanGiveTag;
        public float MinionSlotMult = 1f;
        public float MinionSpeedMult = 1f;
        public float MinionScaleMult = 1f;
        public float MinionKnockbackMult = 1f;
        public float MinionLifeSteal;
        public float MinionCritAdd;
        public bool electrified;

        public override bool InstancePerEntity => true;

        public override void NetSend(Item item, BinaryWriter writer) => writer.Write(WhipRangeMult);

        public override void NetReceive(Item item, BinaryReader reader) => WhipRangeMult = reader.ReadSingle();

        public override void SetDefaults(Item entity)
        {
            CanGiveTag = false;
            wTag = null;
            if (entity.shoot > Terraria.ID.ProjectileID.None && Terraria.ID.ProjectileID.Sets.IsAWhip[entity.shoot])
                wTag = new WhipTag(entity.Name, 0);
            WhipRangeMult = 1f;
        }
    }
}
