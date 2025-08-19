// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Explosives.Superbomber
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Projectiles.Explosives;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Explosives;

public class Superbomber : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.AutoStructures;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 29;
    ((Entity) this.Item).height = 29;
    this.Item.maxStack = 1;
    this.Item.consumable = false;
    this.Item.useStyle = 1;
    this.Item.UseSound = new SoundStyle?(SoundID.Item1);
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.noUseGraphic = true;
    this.Item.noMelee = true;
    this.Item.shoot = ModContent.ProjectileType<SuperbomberProj>();
    this.Item.shootSpeed = 5f;
    this.Item.SetShopValues((ItemRarityColor) 2, Item.buyPrice(0, 5, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.AutoStructures);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.AutoStructures), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(166, 50);
    itemRecipe.AddIngredient(182, 5);
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
