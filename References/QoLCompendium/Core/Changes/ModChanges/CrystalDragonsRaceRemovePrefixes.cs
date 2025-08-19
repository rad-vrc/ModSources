// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.CrystalDragonsRaceRemovePrefixes
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using CrystalDragons.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

[ExtendsFromMod(new string[] {"CrystalDragons"})]
[JITWhenModsEnabled(new string[] {"CrystalDragons"})]
public class CrystalDragonsRaceRemovePrefixes : GlobalItem
{
  private static int infiniteLoopHackFix;

  public virtual bool AllowPrefix(Item item, int pre)
  {
    HashSet<int> intSet = new HashSet<int>()
    {
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazHardened"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazWarding"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazBulwark"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazGenin"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazShinobi"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazIai"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazDiverse"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazBalanced"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazEqualized"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazMartial"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazStriking"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazEviscerating"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazViolent"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazDestructive"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazAnnihilating"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazFit"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazStrong"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazMighty"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazBrisk"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazFleet"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "TopazQuantum"),
      Common.GetModPrefix(ModConditions.crystalDragonsMod, "L A K E")
    };
    if (!Main.gameMenu && ((Entity) Main.LocalPlayer).active && ModConditions.mrPlagueRacesLoaded && !Main.LocalPlayer.GetModPlayer<CrystalDragonPlayer>().topaz && intSet.Contains(pre) && ++CrystalDragonsRaceRemovePrefixes.infiniteLoopHackFix < 30)
      return false;
    CrystalDragonsRaceRemovePrefixes.infiniteLoopHackFix = 0;
    return base.AllowPrefix(item, pre);
  }
}
