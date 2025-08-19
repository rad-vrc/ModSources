// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.ModFailBreak
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

public class ModFailBreak : GlobalTile
{
  public virtual void KillTile(
    int i,
    int j,
    int type,
    ref bool fail,
    ref bool effectOnly,
    ref bool noItem)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.RegrowthAutoReplant || Main.LocalPlayer.HeldItem.type != 213 && Main.LocalPlayer.HeldItem.type != 5295 && (Main.mouseItem == null || Main.mouseItem.type != 213 && Main.mouseItem.type != 5295))
      return;
    if (!new HashSet<int>()
    {
      Common.GetModTile(ModConditions.depthsMod, "ShadowShrub"),
      Common.GetModTile(ModConditions.redemptionMod, "NightshadeTile"),
      Common.GetModTile(ModConditions.shadowsOfAbaddonMod, "Welkinbell"),
      Common.GetModTile(ModConditions.shadowsOfAbaddonMod, "Illumifern"),
      Common.GetModTile(ModConditions.shadowsOfAbaddonMod, "Enduflora"),
      Common.GetModTile(ModConditions.spiritMod, "Cloudstalk"),
      Common.GetModTile(ModConditions.spiritMod, "SoulBloomTile"),
      Common.GetModTile(ModConditions.thoriumMod, "MarineKelp2")
    }.Contains(type))
      return;
    fail = true;
  }
}
