// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.PillarsDropMore
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.Reflection;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class PillarsDropMore : GlobalNPC
{
  private static void MakeLunarPillarDropRulesBetter(DropOneByOne drop)
  {
    PropertyInfo property = typeof (DropOneByOne).GetProperty("parameters", (BindingFlags) 20);
    DropOneByOne.Parameters parameters1 = new DropOneByOne.Parameters()
    {
      MinimumItemDropsCount = 90,
      MaximumItemDropsCount = 110,
      ChanceNumerator = 1,
      ChanceDenominator = 1,
      MinimumStackPerChunkBase = 1,
      MaximumStackPerChunkBase = 1,
      BonusMinDropsPerChunkPerPlayer = 1,
      BonusMaxDropsPerChunkPerPlayer = 1
    };
    DropOneByOne dropOneByOne = drop;
    DropOneByOne.Parameters parameters2 = (object) parameters1;
    property.SetValue((object) dropOneByOne, (object) parameters2);
  }

  public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
  {
    if (!Common.LunarPillarIDs.Contains(npc.type) || !QoLCompendium.QoLCompendium.mainConfig.LunarPillarsDropMoreFragments)
      return;
    foreach (IItemDropRule iitemDropRule in ((NPCLoot) ref npcLoot).Get(true))
    {
      if (iitemDropRule is DropBasedOnExpertMode basedOnExpertMode)
      {
        if (basedOnExpertMode.ruleForNormalMode is DropOneByOne ruleForNormalMode)
          PillarsDropMore.MakeLunarPillarDropRulesBetter(ruleForNormalMode);
        if (basedOnExpertMode.ruleForExpertMode is DropOneByOne ruleForExpertMode)
          PillarsDropMore.MakeLunarPillarDropRulesBetter(ruleForExpertMode);
      }
    }
  }
}
