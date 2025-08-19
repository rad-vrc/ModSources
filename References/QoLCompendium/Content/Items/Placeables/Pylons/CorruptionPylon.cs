// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Placeables.Pylons.CorruptionPylon
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Tiles.Pylons;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Placeables.Pylons;

public class CorruptionPylon : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.Pylons;
  }

  public virtual void SetStaticDefaults()
  {
    ItemID.Sets.ShimmerTransformToItem[this.Item.type] = ModContent.ItemType<CrimsonPylon>();
    this.Item.ResearchUnlockCount = 1;
  }

  public virtual void SetDefaults()
  {
    this.Item.DefaultToPlaceableTile(ModContent.TileType<CorruptionPylonTile>(), 0);
    this.Item.SetShopValues((ItemRarityColor) 1, Item.buyPrice(0, 10, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.Pylons);
  }
}
