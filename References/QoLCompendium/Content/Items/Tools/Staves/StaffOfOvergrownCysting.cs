// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Staves.StaffOfOvergrownCysting
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Staves;

public class StaffOfOvergrownCysting : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.RegrowthStaves;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.useStyle = 1;
    this.Item.useTurn = true;
    this.Item.useAnimation = 25;
    this.Item.useTime = 13;
    this.Item.autoReuse = true;
    ((Entity) this.Item).width = 24;
    ((Entity) this.Item).height = 28;
    this.Item.damage = 7;
    this.Item.createTile = 661;
    this.Item.UseSound = new SoundStyle?(SoundID.Item1);
    this.Item.knockBack = 3f;
    this.Item.SetShopValues((ItemRarityColor) 3, Item.sellPrice(0, 0, 50, 0));
    this.Item.DamageType = DamageClass.Melee;
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    TooltipLine tooltipLine1 = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "Placeable"));
    TooltipLine tooltipLine2 = new TooltipLine(((ModType) this).Mod, "StaffOfOvergrownCystingEffect", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.StaffOfOvergrownCystingPlaceable"));
    tooltips.Insert(tooltips.IndexOf(tooltipLine1), tooltipLine2);
    tooltips.RemoveAll((Predicate<TooltipLine>) (x => x.Name == "Placeable" && x.Mod == "Terraria"));
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.RegrowthStaves);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.RegrowthStaves), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(176 /*0xB0*/, 12);
    itemRecipe.AddIngredient(68, 3);
    itemRecipe.AddIngredient(59, 1);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
