// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.NPCDrops
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class NPCDrops : GlobalNPC
{
  public virtual void SetDefaults(NPC npc)
  {
    npc.value *= (float) QoLCompendium.QoLCompendium.mainConfig.EnemiesDropMoreCoins;
  }

  public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
  {
    if (npc.type == 564 && QoLCompendium.QoLCompendium.mainConfig.ExtraDefenderMedalDrops)
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(3817, 1, 10, 10));
    if (npc.type == 565 && QoLCompendium.QoLCompendium.mainConfig.ExtraDefenderMedalDrops)
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(3817, 1, 25, 25));
    if (npc.type == 576 && QoLCompendium.QoLCompendium.mainConfig.ExtraDefenderMedalDrops)
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(3817, 1, 25, 25));
    if (npc.type == 577 && QoLCompendium.QoLCompendium.mainConfig.ExtraDefenderMedalDrops)
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(3817, 1, 30, 30));
    if (npc.type == 551 && QoLCompendium.QoLCompendium.mainConfig.ExtraDefenderMedalDrops)
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(3817, 1, 50, 50));
    if ((npc.type == 421 || npc.type == 423 || npc.type == 420 || npc.type == 424) && QoLCompendium.QoLCompendium.mainConfig.LunarEnemiesDropFragments)
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(3457, 1, 1, 3));
    if ((npc.type == 418 || npc.type == 412 || npc.type == 518 || npc.type == 415 || npc.type == 416 || npc.type == 419 || npc.type == 417) && QoLCompendium.QoLCompendium.mainConfig.LunarEnemiesDropFragments)
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(3458, 1, 1, 3));
    if ((npc.type == 407 || npc.type == 402 || npc.type == 405 || npc.type == 411 || npc.type == 409) && QoLCompendium.QoLCompendium.mainConfig.LunarEnemiesDropFragments)
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(3459, 1, 1, 3));
    if ((npc.type == 427 || npc.type == 426 || npc.type == 425 || npc.type == 429) && QoLCompendium.QoLCompendium.mainConfig.LunarEnemiesDropFragments)
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(3456, 1, 1, 3));
    int allowedRecursionDepth = 10;
    foreach (IItemDropRule dropRule in ((NPCLoot) ref npcLoot).Get(true))
      CheckMasterDropRule(dropRule);

    void AddDrop(IItemDropRule dropRule)
    {
      if (npc.type == 125 || npc.type == 126)
      {
        LeadingConditionRule leadingConditionRule = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.MissingTwin());
        Chains.OnSuccess((IItemDropRule) leadingConditionRule, dropRule, false);
        ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule);
      }
      else if (npc.type == 14 || npc.type == 13 || npc.type == 15)
      {
        LeadingConditionRule leadingConditionRule = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.LegacyHack_IsABoss());
        Chains.OnSuccess((IItemDropRule) leadingConditionRule, dropRule, false);
        ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule);
      }
      else
        ((NPCLoot) ref npcLoot).Add(dropRule);
    }

    void CheckMasterDropRule(IItemDropRule dropRule)
    {
      if (--allowedRecursionDepth > 0)
      {
        if (dropRule != null && dropRule.ChainedRules != null)
        {
          foreach (IItemDropRuleChainAttempt chainedRule in dropRule.ChainedRules)
          {
            if (chainedRule != null && chainedRule.RuleToChain != null)
              CheckMasterDropRule(chainedRule.RuleToChain);
          }
        }
        DropBasedOnMasterMode basedOnMasterMode = dropRule is DropBasedOnMasterMode ? (DropBasedOnMasterMode) dropRule : (DropBasedOnMasterMode) null;
        if (basedOnMasterMode != null && basedOnMasterMode != null && basedOnMasterMode.ruleForMasterMode != null)
          CheckMasterDropRule(basedOnMasterMode.ruleForMasterMode);
      }
      allowedRecursionDepth++;
      ItemDropWithConditionRule withConditionRule = dropRule is ItemDropWithConditionRule ? (ItemDropWithConditionRule) dropRule : (ItemDropWithConditionRule) null;
      if (withConditionRule != null && withConditionRule.condition is Conditions.IsMasterMode)
      {
        AddDrop(ItemDropRule.ByCondition((IItemDropRuleCondition) new ExpertOnlyDropCondition(), ((CommonDrop) withConditionRule).itemId, ((CommonDrop) withConditionRule).chanceDenominator, ((CommonDrop) withConditionRule).amountDroppedMinimum, ((CommonDrop) withConditionRule).amountDroppedMaximum, ((CommonDrop) withConditionRule).chanceNumerator));
      }
      else
      {
        DropPerPlayerOnThePlayer playerOnThePlayer = dropRule is DropPerPlayerOnThePlayer ? (DropPerPlayerOnThePlayer) dropRule : (DropPerPlayerOnThePlayer) null;
        if (playerOnThePlayer == null || !(playerOnThePlayer.condition is Conditions.IsMasterMode))
          return;
        AddDrop(ItemDropRule.ByCondition((IItemDropRuleCondition) new ExpertOnlyDropCondition(), ((CommonDrop) playerOnThePlayer).itemId, ((CommonDrop) playerOnThePlayer).chanceDenominator, ((CommonDrop) playerOnThePlayer).amountDroppedMinimum, ((CommonDrop) playerOnThePlayer).amountDroppedMaximum, ((CommonDrop) playerOnThePlayer).chanceNumerator));
      }
    }
  }
}
