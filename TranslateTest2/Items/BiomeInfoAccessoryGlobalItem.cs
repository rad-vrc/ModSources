using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TranslateTest2.Items
{
    public class BiomeInfoAccessoryGlobalItem : GlobalItem
    {
        public override void UpdateInfoAccessory(Item item, Player player)
        {
            switch (item.type)
            {
                case ItemID.PDA:
                case ItemID.CellPhone:
                case ItemID.Shellphone:
                case ItemID.ShellphoneSpawn:
                case ItemID.ShellphoneHell:
                case ItemID.ShellphoneOcean:
                    player.GetModPlayer<Core.InfoPlayer>().biomeDisplay = true;
                    break;
            }
        }
    }
}
