// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Dedicated.SillySlapper
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Content.Projectiles.Dedicated;
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
namespace QoLCompendium.Content.Items.Dedicated;

public class SillySlapper : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.DedicatedItems;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 14;
    ((Entity) this.Item).height = 19;
    this.Item.accessory = true;
    this.Item.autoReuse = true;
    this.Item.useStyle = 1;
    this.Item.useAnimation = 30;
    this.Item.useTime = 30;
    this.Item.noMelee = true;
    this.Item.noUseGraphic = true;
    this.Item.UseSound = new SoundStyle?(SoundID.Item152);
    this.Item.damage = 100;
    this.Item.DamageType = DamageClass.Generic;
    this.Item.knockBack = 2f;
    this.Item.shootSpeed = 5f;
    this.Item.shoot = ModContent.ProjectileType<SillySlapperWhip>();
    this.Item.SetShopValues((ItemRarityColor) 10, Item.buyPrice(0, 8, 0, 0));
  }

  public virtual void UpdateAccessory(Player player, bool hideVisual)
  {
    ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
    local = StatModifier.op_Multiply(local, 2f);
    player.GetModPlayer<QoLCPlayer>().sillySlapper = true;
  }

  public virtual void UpdateVanity(Player player)
  {
    ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
    local = StatModifier.op_Multiply(local, 2f);
    player.GetModPlayer<QoLCPlayer>().sillySlapper = true;
  }

  public virtual void HoldItem(Player player)
  {
    ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
    local = StatModifier.op_Multiply(local, 2f);
    player.GetModPlayer<QoLCPlayer>().sillySlapper = true;
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "Dedicated", Language.GetTextValue("Mods.QoLCompendium.DedicatedTooltips.Quinn"))
    {
      OverrideColor = new Color?(Common.ColorSwap(Color.LightSeaGreen, Color.Aquamarine, 3f))
    };
    tooltips.Add(tooltipLine);
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.DedicatedItems);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.DedicatedItems), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(23, 100);
    itemRecipe.AddIngredient(75, 50);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
