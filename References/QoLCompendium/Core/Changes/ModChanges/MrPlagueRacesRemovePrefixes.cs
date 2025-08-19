// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.MrPlagueRacesRemovePrefixes
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using MrPlagueRaces;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

[ExtendsFromMod(new string[] {"MrPlagueRaces"})]
[JITWhenModsEnabled(new string[] {"MrPlagueRaces"})]
public class MrPlagueRacesRemovePrefixes : GlobalItem
{
  private static int infiniteLoopHackFix;

  public virtual bool AllowPrefix(Item item, int pre)
  {
    HashSet<int> intSet = new HashSet<int>()
    {
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Accelerative"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Bewitched"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Bombarding"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Combustible"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Constructive"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Explosive"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Flawless"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Fortunate"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Hexed"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Immolating"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Impactful"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Luminescent"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Recreational"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Regenerative"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Reinforced"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Resilient"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Revitalizing"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Streamlined"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Trailblazing"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Tranquilizing"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Undying"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Volatile"),
      Common.GetModPrefix(ModConditions.mrPlagueRacesMod, "Warping")
    };
    if (!Main.gameMenu && ((Entity) Main.LocalPlayer).active && ModConditions.mrPlagueRacesLoaded && !MrPlagueRacesConfig.Instance.raceStats && intSet.Contains(pre) && ++MrPlagueRacesRemovePrefixes.infiniteLoopHackFix < 30)
      return false;
    MrPlagueRacesRemovePrefixes.infiniteLoopHackFix = 0;
    return base.AllowPrefix(item, pre);
  }
}
