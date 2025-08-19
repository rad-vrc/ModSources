// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Dedicated.Burger
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Content.Tiles.Dedicated;
using QoLCompendium.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Dedicated;

public class Burger : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.DedicatedItems;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 100;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 8;
    ((Entity) this.Item).height = 8;
    this.Item.maxStack = Item.CommonMaxStack;
    this.Item.consumable = true;
    this.Item.useStyle = 2;
    this.Item.UseSound = new SoundStyle?(SoundID.Item2);
    this.Item.useTurn = true;
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.autoReuse = true;
    this.Item.noMelee = true;
    this.Item.createTile = ModContent.TileType<BurgerTile>();
    this.Item.SetShopValues((ItemRarityColor) 10, Item.buyPrice(0, 1, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "Dedicated", Language.GetTextValue("Mods.QoLCompendium.DedicatedTooltips.BladeBurger"))
    {
      OverrideColor = new Color?(Common.ColorSwap(Color.LightSeaGreen, Color.Aquamarine, 3f))
    };
    tooltips.Add(tooltipLine);
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.DedicatedItems);
  }
}
