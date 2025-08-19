using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.All;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.All
{
	// Token: 0x02000178 RID: 376
	public class PermanentEverything : IPermanentBuffItem
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000148E3 File Offset: 0x00012AE3
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Items/PermanentEverything";
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000148EC File Offset: 0x00012AEC
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001490C File Offset: 0x00012B0C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<PermanentVanilla>(), 1);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00014965 File Offset: 0x00012B65
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new EverythingEffect());
		}
	}
}
