// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.ExpertOnlyDropCondition
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using CalamityMod.World;
using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core;

public class ExpertOnlyDropCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
  [JITWhenModsEnabled(new string[] {"CalamityMod"})]
  public static bool RevengeanceMode => CalamityWorld.revenge;

  [JITWhenModsEnabled(new string[] {"CalamityMod"})]
  public static bool DeathMode => CalamityWorld.death;

  [JITWhenModsEnabled(new string[] {"FargowiltasSouls"})]
  public static bool EternityMode => WorldSavingSystem.EternityMode;

  public bool CanDrop(DropAttemptInfo info)
  {
    return info.npc.boss && QoLCompendium.QoLCompendium.mainConfig.RelicsInExpert && !Main.masterMode && !ExpertOnlyDropCondition.CalamityDifficultyEnabled() && !ExpertOnlyDropCondition.FargoSoulsDifficultyEnabled() && Main.expertMode;
  }

  public bool CanShowItemDropInUI()
  {
    return Main.expertMode && QoLCompendium.QoLCompendium.mainConfig.RelicsInExpert && !Main.masterMode && !ExpertOnlyDropCondition.CalamityDifficultyEnabled() && !ExpertOnlyDropCondition.FargoSoulsDifficultyEnabled();
  }

  public string GetConditionDescription()
  {
    return Language.GetTextValue("Mods.QoLCompendium.NPCDropConditions.ExpertNotMaster");
  }

  public static bool CalamityDifficultyEnabled()
  {
    return Terraria.ModLoader.ModLoader.HasMod("CalamityMod") && (ExpertOnlyDropCondition.RevengeanceMode || ExpertOnlyDropCondition.DeathMode);
  }

  public static bool FargoSoulsDifficultyEnabled()
  {
    return Terraria.ModLoader.ModLoader.HasMod("FargowiltasSouls") && ExpertOnlyDropCondition.EternityMode;
  }
}
