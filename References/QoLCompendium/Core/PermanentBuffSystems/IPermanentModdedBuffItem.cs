// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.IPermanentModdedBuffItem
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems;

public abstract class IPermanentModdedBuffItem : ModItem, IComparable
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.PermanentBuffs;
  }

  public int CompareTo(object obj) => ((object) this).GetType().Name.CompareTo(obj.GetType().Name);

  internal abstract void ApplyBuff(PermanentBuffPlayer player);

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults() => Common.SetDefaultsToPermanentBuff(this.Item);

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    if (this.Texture == "QoLCompendium/Assets/Items/PermanentBuff")
    {
      TooltipLine element = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "ItemName"));
      TooltipLine tooltipLine = new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "AssetNotFound", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.AssetNotFound"));
      tooltips.AddAfter<TooltipLine>(element, tooltipLine);
    }
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.PermanentBuffs);
  }
}
