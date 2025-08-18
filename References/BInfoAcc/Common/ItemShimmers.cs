using BInfoAcc.Content;
using Terraria.ID;
using Terraria.ModLoader;

namespace BInfoAcc.Common
{
    public class ItemShimmers : GlobalItem 
    {
        public override void SetStaticDefaults()
        {
            // Magic Mirror < > Fortune Mirror
            ItemID.Sets.ShimmerTransformToItem[ItemID.MagicMirror] = ModContent.ItemType<FortuneMirror>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<FortuneMirror>()] = ItemID.MagicMirror;

            // Band of Regeneration < > Smart Heart
            ItemID.Sets.ShimmerTransformToItem[ItemID.BandofRegeneration] = ModContent.ItemType<SmartHeart>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<SmartHeart>()] = ItemID.BandofRegeneration;

            // Forest Pylon < > Biome Crystal
            ItemID.Sets.ShimmerTransformToItem[ItemID.TeleportationPylonPurity] = ModContent.ItemType<BiomeCrystal>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<BiomeCrystal>()] = ItemID.TeleportationPylonPurity;
        }
    }
}