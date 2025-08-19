// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.StarterBag
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Usables;

public class StarterBag : ModItem
{
  public int type;
  public int curItem;
  public bool loadCount;

  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.StarterBag;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 15;
    ((Entity) this.Item).height = 12;
    this.Item.SetShopValues((ItemRarityColor) 0, Item.buyPrice(0, 0, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.StarterBag);
  }

  public virtual bool CanRightClick() => QoLCompendium.QoLCompendium.itemConfig.CustomItems != null;

  public virtual void RightClick(Player player)
  {
    if (QoLCompendium.QoLCompendium.itemConfig.CustomItems == null)
      return;
    if (QoLCompendium.QoLCompendium.itemConfig.CustomItemQuantities != null)
      this.loadCount = true;
    for (int index = 0; index < QoLCompendium.QoLCompendium.itemConfig.CustomItems.Count; ++index)
    {
      this.type = ((EntityDefinition) QoLCompendium.QoLCompendium.itemConfig.CustomItems[index]).Type;
      if (this.loadCount)
      {
        if (index <= QoLCompendium.QoLCompendium.itemConfig.CustomItemQuantities.Count - 1)
          player.QuickSpawnItem((IEntitySource) null, this.type, QoLCompendium.QoLCompendium.itemConfig.CustomItemQuantities[index]);
        else
          player.QuickSpawnItem((IEntitySource) null, this.type, 1);
      }
      else
        player.QuickSpawnItem((IEntitySource) null, this.type, 1);
    }
  }
}
