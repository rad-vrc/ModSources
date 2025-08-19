// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.TileChanges.VanillaFountainBiomes
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.TileChanges;

public class VanillaFountainBiomes : ModPlayer
{
  public virtual void PostUpdateMiscEffects()
  {
    if (QoLCompendium.QoLCompendium.mainConfig.FountainsCauseBiomes)
    {
      if (Main.SceneMetrics.ActiveFountainColor == 0)
        this.Player.ZoneBeach = true;
      if (Main.SceneMetrics.ActiveFountainColor == 2)
        this.Player.ZoneCorrupt = true;
      if (Main.SceneMetrics.ActiveFountainColor == 3)
        this.Player.ZoneJungle = true;
      if (Main.SceneMetrics.ActiveFountainColor == 4 && Main.hardMode)
        this.Player.ZoneHallow = true;
      if (Main.SceneMetrics.ActiveFountainColor == 5)
        this.Player.ZoneSnow = true;
      if (Main.SceneMetrics.ActiveFountainColor == 6 || Main.SceneMetrics.ActiveFountainColor == 12)
        this.Player.ZoneDesert = true;
      if ((Main.SceneMetrics.ActiveFountainColor == 6 || Main.SceneMetrics.ActiveFountainColor == 12) && (double) ((Entity) this.Player).Center.Y > 3200.0)
        this.Player.ZoneUndergroundDesert = true;
      if (Main.SceneMetrics.ActiveFountainColor == 10)
        this.Player.ZoneCrimson = true;
    }
    if (!QoLCompendium.QoLCompendium.mainConfig.FountainsWorkFromInventories)
      return;
    if (this.Player.HasItemInAnyInventory(Common.VanillaFountains[0]))
      this.Player.ZoneBeach = true;
    if (this.Player.HasItemInAnyInventory(Common.VanillaFountains[1]))
      this.Player.ZoneCorrupt = true;
    if (this.Player.HasItemInAnyInventory(Common.VanillaFountains[2]))
      this.Player.ZoneJungle = true;
    if (this.Player.HasItemInAnyInventory(Common.VanillaFountains[3]) && Main.hardMode)
      this.Player.ZoneHallow = true;
    if (this.Player.HasItemInAnyInventory(Common.VanillaFountains[4]))
      this.Player.ZoneSnow = true;
    if (this.Player.HasItemInAnyInventory(Common.VanillaFountains[5]) || this.Player.HasItemInAnyInventory(Common.VanillaFountains[6]))
      this.Player.ZoneDesert = true;
    if ((this.Player.HasItemInAnyInventory(Common.VanillaFountains[5]) || this.Player.HasItemInAnyInventory(Common.VanillaFountains[6])) && (double) ((Entity) this.Player).Center.Y > 3200.0)
      this.Player.ZoneDesert = true;
    if (!this.Player.HasItemInAnyInventory(Common.VanillaFountains[7]))
      return;
    this.Player.ZoneCrimson = true;
  }
}
