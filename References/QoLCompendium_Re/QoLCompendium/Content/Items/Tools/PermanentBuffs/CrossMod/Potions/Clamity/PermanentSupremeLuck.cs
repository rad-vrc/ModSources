using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Clamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Clamity
{
	// Token: 0x020000E8 RID: 232
	[JITWhenModsEnabled(new string[]
	{
		"Clamity"
	})]
	[ExtendsFromMod(new string[]
	{
		"Clamity"
	})]
	public class PermanentSupremeLuck : IPermanentModdedBuffItem
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000DE06 File Offset: 0x0000C006
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.clamityAddonMod, Common.GetModBuff(ModConditions.clamityAddonMod, "SupremeLucky"));
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000DE24 File Offset: 0x0000C024
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000DE44 File Offset: 0x0000C044
		public override void AddRecipes()
		{
			if (!ModConditions.clamityAddonLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.clamityAddonMod, "SupremeLuckPotion"), 30);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new SupremeLuckEffect());
		}
	}
}
