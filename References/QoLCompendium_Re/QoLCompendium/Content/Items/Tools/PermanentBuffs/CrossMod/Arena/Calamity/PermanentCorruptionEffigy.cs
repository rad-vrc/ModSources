using System;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Arena.Calamity
{
	// Token: 0x02000123 RID: 291
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PermanentCorruptionEffigy : IPermanentModdedBuffItem
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00010B85 File Offset: 0x0000ED85
		public override string Texture
		{
			get
			{
				return Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CorruptionEffigyBuff"));
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00010BA0 File Offset: 0x0000EDA0
		public override void UpdateInventory(Player player)
		{
			PermanentBuffPlayer pBuffPlayer;
			if (player.TryGetModPlayer<PermanentBuffPlayer>(out pBuffPlayer))
			{
				this.ApplyBuff(pBuffPlayer);
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00010BC0 File Offset: 0x0000EDC0
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PermanentBuffs, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "CorruptionEffigy"), 3);
			itemRecipe.AddTile(96);
			itemRecipe.Register();
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00010C2B File Offset: 0x0000EE2B
		internal override void ApplyBuff(PermanentBuffPlayer player)
		{
			player.buffActive = true;
			player.modPotionEffects.Add(new CorruptionEffigyEffect());
		}
	}
}
