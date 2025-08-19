// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Fishing.NoBaitUse
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Fishing;

public class NoBaitUse : GlobalItem
{
  public virtual bool? CanConsumeBait(Player player, Item bait)
  {
    return player.HeldItem.type == ModContent.ItemType<LegendaryCatcher>() && bait.bait > 0 || bait.type == ModContent.ItemType<Eightworm>() ? new bool?(false) : base.CanConsumeBait(player, bait);
  }

  public virtual bool ConsumeItem(Item item, Player player)
  {
    return (player.HeldItem.type != ModContent.ItemType<LegendaryCatcher>() || item.bait <= 0) && item.type != ModContent.ItemType<Eightworm>() && base.ConsumeItem(item, player);
  }
}
