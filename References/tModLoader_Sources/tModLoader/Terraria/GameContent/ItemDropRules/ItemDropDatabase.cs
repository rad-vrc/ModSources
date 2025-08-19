using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200060A RID: 1546
	public class ItemDropDatabase
	{
		// Token: 0x060043F1 RID: 17393 RVA: 0x006015F4 File Offset: 0x005FF7F4
		public void PrepareNPCNetIDsByTypeDictionary()
		{
			this._npcNetIdsByType.Clear();
			foreach (KeyValuePair<int, NPC> item in from x in ContentSamples.NpcsByNetId
			where x.Key < 0
			select x)
			{
				if (!this._npcNetIdsByType.ContainsKey(item.Value.type))
				{
					this._npcNetIdsByType[item.Value.type] = new List<int>();
				}
				this._npcNetIdsByType[item.Value.type].Add(item.Value.netID);
			}
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x006016C8 File Offset: 0x005FF8C8
		public void TrimDuplicateRulesForNegativeIDs()
		{
			for (int i = -65; i < 0; i++)
			{
				List<IItemDropRule> value;
				if (this._entriesByNpcNetId.TryGetValue(i, out value))
				{
					this._entriesByNpcNetId[i] = value.Distinct<IItemDropRule>().ToList<IItemDropRule>();
				}
			}
		}

		/// <summary>
		/// Retrieves all the registered <see cref="T:Terraria.GameContent.ItemDropRules.IItemDropRule" /> for this specific NPC type.
		/// </summary>
		/// <param name="npcNetId"></param>
		/// <param name="includeGlobalDrops">If true then global drop rules will also be retrieved</param>
		// Token: 0x060043F3 RID: 17395 RVA: 0x0060170C File Offset: 0x005FF90C
		public List<IItemDropRule> GetRulesForNPCID(int npcNetId, bool includeGlobalDrops = true)
		{
			List<IItemDropRule> list = new List<IItemDropRule>();
			if (includeGlobalDrops)
			{
				list.AddRange(this._globalEntries);
			}
			List<IItemDropRule> value;
			if (this._entriesByNpcNetId.TryGetValue(npcNetId, out value))
			{
				list.AddRange(value);
			}
			return list;
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x00601746 File Offset: 0x005FF946
		public IItemDropRule RegisterToGlobal(IItemDropRule entry)
		{
			this._globalEntries.Add(entry);
			return entry;
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x00601758 File Offset: 0x005FF958
		public IItemDropRule RegisterToNPC(int type, IItemDropRule entry)
		{
			this.RegisterToNPCNetId(type, entry);
			List<int> value;
			if (type > 0 && this._npcNetIdsByType.TryGetValue(type, out value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					this.RegisterToNPCNetId(value[i], entry);
				}
			}
			return entry;
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x006017A1 File Offset: 0x005FF9A1
		public void RegisterToNPCNetId(int npcNetId, IItemDropRule entry)
		{
			if (!this._entriesByNpcNetId.ContainsKey(npcNetId))
			{
				this._entriesByNpcNetId[npcNetId] = new List<IItemDropRule>();
			}
			this._entriesByNpcNetId[npcNetId].Add(entry);
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x006017D4 File Offset: 0x005FF9D4
		public IItemDropRule RegisterToMultipleNPCs(IItemDropRule entry, params int[] npcNetIds)
		{
			for (int i = 0; i < npcNetIds.Length; i++)
			{
				this.RegisterToNPC(npcNetIds[i], entry);
			}
			return entry;
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x006017FC File Offset: 0x005FF9FC
		public IItemDropRule RegisterToMultipleNPCsNotRemixSeed(IItemDropRule entry, params int[] npcNetIds)
		{
			for (int i = 0; i < npcNetIds.Length; i++)
			{
				this.RegisterToNPC(npcNetIds[i], new LeadingConditionRule(new Conditions.NotRemixSeed())).OnSuccess(entry, false);
			}
			return entry;
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x00601834 File Offset: 0x005FFA34
		public IItemDropRule RegisterToMultipleNPCsRemixSeed(IItemDropRule entry, params int[] npcNetIds)
		{
			for (int i = 0; i < npcNetIds.Length; i++)
			{
				this.RegisterToNPC(npcNetIds[i], new LeadingConditionRule(new Conditions.RemixSeed())).OnSuccess(entry, false);
			}
			return entry;
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x0060186B File Offset: 0x005FFA6B
		public void RemoveFromNPCNetId(int npcNetId, IItemDropRule entry)
		{
			if (this._entriesByNpcNetId.ContainsKey(npcNetId))
			{
				this._entriesByNpcNetId[npcNetId].Remove(entry);
			}
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x00601890 File Offset: 0x005FFA90
		public IItemDropRule RemoveFromNPC(int type, IItemDropRule entry)
		{
			this.RemoveFromNPCNetId(type, entry);
			List<int> value;
			if (type > 0 && this._npcNetIdsByType.TryGetValue(type, out value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					this.RemoveFromNPCNetId(value[i], entry);
				}
			}
			return entry;
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x006018DC File Offset: 0x005FFADC
		public IItemDropRule RemoveFromMultipleNPCs(IItemDropRule entry, params int[] npcNetIds)
		{
			for (int i = 0; i < npcNetIds.Length; i++)
			{
				this.RemoveFromNPC(npcNetIds[i], entry);
			}
			return entry;
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x00601904 File Offset: 0x005FFB04
		public void Populate()
		{
			this.PrepareNPCNetIDsByTypeDictionary();
			this.RegisterGlobalRules();
			this.RegisterFoodDrops();
			this.RegisterWeirdRules();
			this.RegisterTownNPCDrops();
			this.RegisterDD2EventDrops();
			this.RegisterMiscDrops();
			this.RegisterHardmodeFeathers();
			this.RegisterYoyos();
			this.RegisterStatusImmunityItems();
			this.RegisterPirateDrops();
			this.RegisterBloodMoonFishingEnemies();
			this.RegisterMartianDrops();
			this.RegisterBossTrophies();
			this.RegisterBosses();
			this.RegisterHardmodeDungeonDrops();
			this.RegisterMimic();
			this.RegisterEclipse();
			this.RegisterBloodMoonFishing();
			this.RegisterBossBags();
			this.RegisterCrateDrops();
			this.RegisterObsidianLockbox();
			this.RegisterLockbox();
			this.RegisterHerbBag();
			this.RegisterGoodieBag();
			this.RegisterPresent();
			this.RegisterCanOfWorms();
			this.RegisterOyster();
			this.RegisterCapricorns();
			NPCLoader.ModifyGlobalLoot(new GlobalLoot(this));
			foreach (KeyValuePair<int, NPC> pair in ContentSamples.NpcsByNetId)
			{
				NPCLoader.ModifyNPCLoot(pair.Value, new NPCLoot(pair.Key, this));
			}
			foreach (KeyValuePair<int, Item> pair2 in ContentSamples.ItemsByType)
			{
				ItemLoader.ModifyItemLoot(pair2.Value, new ItemLoot(pair2.Key, this));
			}
			this.TrimDuplicateRulesForNegativeIDs();
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x00601A7C File Offset: 0x005FFC7C
		private void RegisterBloodMoonFishing()
		{
			this.RegisterToMultipleNPCs(ItemDropRule.Common(4608, 2, 4, 6), new int[]
			{
				587,
				586
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Common(4608, 2, 7, 10), new int[]
			{
				620,
				621,
				618
			});
			this.RegisterToMultipleNPCs(ItemDropRule.OneFromOptions(8, new int[]
			{
				4273
			}), new int[]
			{
				587,
				586
			});
			this.RegisterToMultipleNPCs(ItemDropRule.OneFromOptions(8, new int[]
			{
				4381
			}), new int[]
			{
				587,
				586
			});
			this.RegisterToMultipleNPCs(ItemDropRule.OneFromOptions(8, new int[]
			{
				4325
			}), new int[]
			{
				587,
				586
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Common(3213, 15, 1, 1), new int[]
			{
				587,
				586
			});
			this.RegisterToNPC(620, ItemDropRule.Common(4270, 8, 1, 1));
			this.RegisterToNPC(620, ItemDropRule.Common(4317, 8, 1, 1));
			this.RegisterToNPC(621, ItemDropRule.Common(4272, 8, 1, 1));
			this.RegisterToNPC(621, ItemDropRule.Common(4317, 8, 1, 1));
			this.RegisterToNPC(618, ItemDropRule.NormalvsExpert(4269, 2, 1));
			this.RegisterToNPC(618, ItemDropRule.Common(4054, 10, 1, 1));
			this.RegisterToNPC(618, ItemDropRule.NormalvsExpert(4271, 2, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(4271, 5, 1, 1), new int[]
			{
				53,
				536
			});
			Conditions.IsBloodMoonAndNotFromStatue condition = new Conditions.IsBloodMoonAndNotFromStatue();
			this.RegisterToMultipleNPCs(ItemDropRule.ByCondition(condition, 4271, 100, 1, 1, 1), new int[]
			{
				489,
				490
			});
			this.RegisterToMultipleNPCs(ItemDropRule.ByCondition(condition, 4271, 25, 1, 1, 1), new int[]
			{
				587,
				586,
				621,
				620
			});
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x00601CC8 File Offset: 0x005FFEC8
		private void RegisterEclipse()
		{
			this.RegisterToNPC(461, ItemDropRule.ExpertGetsRerolls(497, 50, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(900, 35, 1), new int[]
			{
				159,
				158
			});
			this.RegisterToNPC(251, ItemDropRule.ExpertGetsRerolls(1311, 15, 1));
			this.RegisterToNPC(251, ItemDropRule.Common(5239, 15, 1, 1));
			this.RegisterToNPC(251, ItemDropRule.Common(5236, 15, 1, 1));
			this.RegisterToNPC(477, ItemDropRule.Common(5237, 15, 1, 1));
			this.RegisterToNPC(253, ItemDropRule.Common(5223, 60, 1, 1));
			this.RegisterToNPC(460, ItemDropRule.Common(5227, 60, 1, 1));
			this.RegisterToNPC(469, ItemDropRule.Common(5260, 60, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(5261, 450, 1, 1), new int[]
			{
				166,
				162
			});
			this.RegisterToNPC(462, ItemDropRule.Common(5262, 60, 1, 1));
			Conditions.DownedAllMechBosses condition = new Conditions.DownedAllMechBosses();
			Conditions.DownedPlantera condition2 = new Conditions.DownedPlantera();
			IItemDropRule rule3 = this.RegisterToNPC(477, new LeadingConditionRule(condition));
			IItemDropRule rule2 = rule3.OnSuccess(new LeadingConditionRule(condition2), false);
			rule3.OnSuccess(ItemDropRule.ExpertGetsRerolls(1570, 4, 1), false);
			rule2.OnSuccess(ItemDropRule.ExpertGetsRerolls(2770, 20, 1), false);
			rule2.OnSuccess(ItemDropRule.ExpertGetsRerolls(3292, 3, 1), false);
			this.RegisterToNPC(253, new LeadingConditionRule(condition)).OnSuccess(ItemDropRule.ExpertGetsRerolls(1327, 40, 1), false);
			this.RegisterToNPC(460, new LeadingConditionRule(condition2)).OnSuccess(ItemDropRule.ExpertGetsRerolls(3098, 40, 1), false);
			this.RegisterToNPC(460, ItemDropRule.ExpertGetsRerolls(4740, 50, 1));
			this.RegisterToNPC(460, ItemDropRule.ExpertGetsRerolls(4741, 50, 1));
			this.RegisterToNPC(460, ItemDropRule.ExpertGetsRerolls(4742, 50, 1));
			this.RegisterToNPC(468, new LeadingConditionRule(condition2)).OnSuccess(ItemDropRule.ExpertGetsRerolls(3105, 40, 1), false);
			this.RegisterToNPC(468, ItemDropRule.ExpertGetsRerolls(4738, 50, 1));
			this.RegisterToNPC(468, ItemDropRule.ExpertGetsRerolls(4739, 50, 1));
			this.RegisterToNPC(466, new LeadingConditionRule(condition2)).OnSuccess(ItemDropRule.ExpertGetsRerolls(3106, 40, 1), false);
			this.RegisterToNPC(467, new LeadingConditionRule(condition2)).OnSuccess(ItemDropRule.ExpertGetsRerolls(3249, 30, 1), false);
			IItemDropRule itemDropRule = ItemDropRule.Common(3107, 25, 1, 1);
			IItemDropRule itemDropRule2 = ItemDropRule.WithRerolls(3107, 1, 25, 1, 1);
			itemDropRule.OnSuccess(ItemDropRule.Common(3108, 1, 100, 200), true);
			itemDropRule2.OnSuccess(ItemDropRule.Common(3108, 1, 100, 200), true);
			this.RegisterToNPC(463, new LeadingConditionRule(condition2)).OnSuccess(new DropBasedOnExpertMode(itemDropRule, itemDropRule2), false);
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x00602028 File Offset: 0x00600228
		private void RegisterMimic()
		{
			this.RegisterToNPC(85, new LeadingConditionRule(new Conditions.NotRemixSeed())).OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				437,
				517,
				535,
				536,
				532,
				554
			}), false);
			this.RegisterToNPC(85, new LeadingConditionRule(new Conditions.RemixSeedHardmode())).OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				437,
				3069,
				535,
				536,
				532,
				554
			}), false);
			this.RegisterToNPC(85, new LeadingConditionRule(new Conditions.RemixSeedEasymode())).OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				49,
				50,
				53,
				54,
				5011,
				975
			}), false);
			IItemDropRule itemDropRule = ItemDropRule.Common(1312, 20, 1, 1);
			itemDropRule.OnFailedRoll(new LeadingConditionRule(new Conditions.NotRemixSeed()), false).OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				676,
				725,
				1264
			}), false);
			itemDropRule.OnFailedRoll(new LeadingConditionRule(new Conditions.RemixSeedHardmode()), false).OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				676,
				1319,
				1264
			}), false);
			itemDropRule.OnFailedRoll(new LeadingConditionRule(new Conditions.RemixSeedEasymode()), false).OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				670,
				724,
				950,
				725,
				987,
				1579
			}), false);
			this.RegisterToNPC(629, itemDropRule);
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x00602170 File Offset: 0x00600370
		private void RegisterHardmodeDungeonDrops()
		{
			int[] npcNetIds = new int[]
			{
				269,
				270,
				271,
				272,
				273,
				274,
				275,
				276,
				277,
				278,
				279,
				280
			};
			this.RegisterToNPC(290, ItemDropRule.ExpertGetsRerolls(1513, 15, 1));
			this.RegisterToNPC(290, ItemDropRule.ExpertGetsRerolls(938, 10, 1));
			this.RegisterToNPC(287, ItemDropRule.ExpertGetsRerolls(977, 12, 1));
			this.RegisterToNPC(287, ItemDropRule.ExpertGetsRerolls(963, 12, 1));
			this.RegisterToNPC(291, ItemDropRule.ExpertGetsRerolls(1300, 12, 1));
			this.RegisterToNPC(291, ItemDropRule.ExpertGetsRerolls(1254, 12, 1));
			this.RegisterToNPC(292, ItemDropRule.ExpertGetsRerolls(1514, 12, 1));
			this.RegisterToNPC(292, ItemDropRule.ExpertGetsRerolls(679, 12, 1));
			this.RegisterToNPC(293, ItemDropRule.ExpertGetsRerolls(759, 18, 1));
			this.RegisterToNPC(289, ItemDropRule.ExpertGetsRerolls(4789, 25, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(1446, 20, 1), new int[]
			{
				281,
				282
			});
			this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(1444, 20, 1), new int[]
			{
				283,
				284
			});
			this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(1445, 20, 1), new int[]
			{
				285,
				286
			});
			this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(1183, 400, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(1266, 300, 1), npcNetIds);
			this.RegisterToMultipleNPCsNotRemixSeed(ItemDropRule.ExpertGetsRerolls(671, 200, 1), npcNetIds);
			this.RegisterToMultipleNPCsRemixSeed(ItemDropRule.ExpertGetsRerolls(2273, 200, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(4679, 200, 1), npcNetIds);
			this.RegisterToNPC(288, ItemDropRule.Common(1508, 1, 1, 2));
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x0060239C File Offset: 0x0060059C
		private void RegisterBosses()
		{
			this.RegisterBoss_EOC();
			this.RegisterBoss_BOC();
			this.RegisterBoss_EOW();
			this.RegisterBoss_QueenBee();
			this.RegisterBoss_Skeletron();
			this.RegisterBoss_WOF();
			this.RegisterBoss_AncientCultist();
			this.RegisterBoss_MoonLord();
			this.RegisterBoss_LunarTowers();
			this.RegisterBoss_Betsy();
			this.RegisterBoss_Golem();
			this.RegisterBoss_DukeFishron();
			this.RegisterBoss_SkeletronPrime();
			this.RegisterBoss_TheDestroyer();
			this.RegisterBoss_Twins();
			this.RegisterBoss_Plantera();
			this.RegisterBoss_KingSlime();
			this.RegisterBoss_FrostMoon();
			this.RegisterBoss_PumpkinMoon();
			this.RegisterBoss_HallowBoss();
			this.RegisterBoss_QueenSlime();
			this.RegisterBoss_Deerclops();
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x00602430 File Offset: 0x00600630
		private void RegisterBoss_QueenSlime()
		{
			short type = 657;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(4957));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4950));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4960, this._masterModeDropRng));
			LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new Conditions.NotExpert());
			this.RegisterToNPC((int)type, leadingConditionRule);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(4986, 1, 25, 75), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(4959, 7, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				4982,
				4983,
				4984
			}), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(4758, 4, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(4981, 4, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.NotScalingWithLuck(4980, 3, 1, 1), false);
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x00602520 File Offset: 0x00600720
		private void RegisterBoss_HallowBoss()
		{
			short type = 636;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(4782));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4949));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4811, this._masterModeDropRng));
			LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new Conditions.NotExpert());
			this.RegisterToNPC((int)type, leadingConditionRule).OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				4923,
				4952,
				4953,
				4914
			}), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(4823, 15, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(4778, 4, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(4715, 50, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(4784, 7, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(5075, 20, 1, 1), false);
			LeadingConditionRule entry = new LeadingConditionRule(new Conditions.EmpressOfLightIsGenuinelyEnraged());
			this.RegisterToNPC((int)type, entry).OnSuccess(ItemDropRule.Common(5005, 1, 1, 1), false);
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x00602634 File Offset: 0x00600834
		private void RegisterBoss_PumpkinMoon()
		{
			Conditions.PumpkinMoonDropGatingChance condition = new Conditions.PumpkinMoonDropGatingChance();
			Conditions.PumpkinMoonDropGateForTrophies condition2 = new Conditions.PumpkinMoonDropGateForTrophies();
			new Conditions.IsPumpkinMoon();
			new Conditions.FromCertainWaveAndAbove(15);
			this.RegisterToNPC(315, ItemDropRule.ByCondition(condition, 1857, 20, 1, 1, 1));
			int[] npcNetIds = new int[]
			{
				305,
				306,
				307,
				308,
				309,
				310,
				311,
				312,
				313,
				314
			};
			this.RegisterToMultipleNPCs(new LeadingConditionRule(condition), npcNetIds).OnSuccess(ItemDropRule.OneFromOptions(10, new int[]
			{
				1788,
				1789,
				1790
			}), false);
			IItemDropRule rule = this.RegisterToNPC(325, new LeadingConditionRule(condition));
			IItemDropRule itemDropRule = ItemDropRule.Common(1835, 1, 1, 1);
			itemDropRule.OnSuccess(ItemDropRule.Common(1836, 1, 30, 60), true);
			rule.OnSuccess(new OneFromRulesRule(1, new IItemDropRule[]
			{
				ItemDropRule.Common(1829, 1, 1, 1),
				ItemDropRule.Common(1831, 1, 1, 1),
				itemDropRule,
				ItemDropRule.Common(1837, 1, 1, 1),
				ItemDropRule.Common(1845, 1, 1, 1)
			}), false);
			rule.OnSuccess(ItemDropRule.ByCondition(condition2, 1855, 1, 1, 1, 1), false);
			rule.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), 4444, 5, 1, 1, 1), false);
			rule.OnSuccess(ItemDropRule.MasterModeCommonDrop(4941), false);
			rule.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4793, this._masterModeDropRng), false);
			IItemDropRule itemDropRule2 = ItemDropRule.Common(1782, 1, 1, 1);
			itemDropRule2.OnSuccess(ItemDropRule.Common(1783, 1, 50, 100), true);
			IItemDropRule itemDropRule3 = ItemDropRule.Common(1784, 1, 1, 1);
			itemDropRule3.OnSuccess(ItemDropRule.Common(1785, 1, 25, 50), true);
			IItemDropRule rule2 = this.RegisterToNPC(327, new LeadingConditionRule(condition));
			rule2.OnSuccess(new OneFromRulesRule(1, new IItemDropRule[]
			{
				itemDropRule2,
				itemDropRule3,
				ItemDropRule.Common(1811, 1, 1, 1),
				ItemDropRule.Common(1826, 1, 1, 1),
				ItemDropRule.Common(1801, 1, 1, 1),
				ItemDropRule.Common(1802, 1, 1, 1),
				ItemDropRule.Common(4680, 1, 1, 1),
				ItemDropRule.Common(1798, 1, 1, 1)
			}), false);
			rule2.OnSuccess(ItemDropRule.ByCondition(condition2, 1856, 1, 1, 1, 1), false);
			rule2.OnSuccess(ItemDropRule.MasterModeCommonDrop(4942), false);
			rule2.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4812, this._masterModeDropRng), false);
			this.RegisterToNPC(326, new DropBasedOnMasterAndExpertMode(new CommonDrop(1729, 1, 1, 3, 1), new CommonDrop(1729, 1, 1, 4, 1), new CommonDrop(1729, 1, 2, 4, 1)));
			this.RegisterToNPC(325, new DropBasedOnMasterAndExpertMode(new CommonDrop(1729, 1, 15, 30, 1), new CommonDrop(1729, 1, 25, 40, 1), new CommonDrop(1729, 1, 30, 50, 1)));
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x00602944 File Offset: 0x00600B44
		private void RegisterBoss_FrostMoon()
		{
			Conditions.FrostMoonDropGatingChance condition = new Conditions.FrostMoonDropGatingChance();
			Conditions.FrostMoonDropGateForTrophies condition2 = new Conditions.FrostMoonDropGateForTrophies();
			Conditions.FromCertainWaveAndAbove condition3 = new Conditions.FromCertainWaveAndAbove(15);
			IItemDropRule rule = this.RegisterToNPC(344, new LeadingConditionRule(condition));
			rule.OnSuccess(ItemDropRule.ByCondition(condition2, 1962, 1, 1, 1, 1), false);
			rule.OnSuccess(ItemDropRule.Common(1871, 15, 1, 1), false).OnFailedRoll(ItemDropRule.OneFromOptions(1, new int[]
			{
				1916,
				1928,
				1930
			}), false);
			rule.OnSuccess(ItemDropRule.MasterModeCommonDrop(4944), false);
			rule.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4813, this._masterModeDropRng), false);
			IItemDropRule rule2 = this.RegisterToNPC(345, new LeadingConditionRule(condition));
			rule2.OnSuccess(ItemDropRule.ByCondition(condition2, 1960, 1, 1, 1, 1), false);
			rule2.OnSuccess(ItemDropRule.ByCondition(condition3, 1914, 15, 1, 1, 1), false);
			rule2.OnSuccess(ItemDropRule.Common(1959, 15, 1, 1), false).OnFailedRoll(ItemDropRule.OneFromOptions(1, new int[]
			{
				1931,
				1946,
				1947
			}), false);
			rule2.OnSuccess(ItemDropRule.MasterModeCommonDrop(4943), false);
			rule2.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4814, this._masterModeDropRng), false);
			IItemDropRule rule3 = this.RegisterToNPC(346, new LeadingConditionRule(condition));
			rule3.OnSuccess(ItemDropRule.ByCondition(condition2, 1961, 1, 1, 1, 1), false);
			rule3.OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				1910,
				1929
			}), false);
			rule3.OnSuccess(ItemDropRule.MasterModeCommonDrop(4945), false);
			rule3.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4794, this._masterModeDropRng), false);
			int[] npcNetIds = new int[]
			{
				338,
				339,
				340
			};
			this.RegisterToMultipleNPCs(ItemDropRule.OneFromOptions(200, new int[]
			{
				1943,
				1944,
				1945
			}), npcNetIds);
			this.RegisterToNPC(341, ItemDropRule.ByCondition(new Conditions.IsChristmas(), 1869, 1, 1, 1, 1));
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x00602B64 File Offset: 0x00600D64
		private void RegisterBoss_KingSlime()
		{
			short type = 50;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3318));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4929));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4797, this._masterModeDropRng));
			LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new Conditions.NotExpert());
			this.RegisterToNPC((int)type, leadingConditionRule);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(2430, 4, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(2493, 7, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				256,
				257,
				258
			}), false);
			leadingConditionRule.OnSuccess(ItemDropRule.NotScalingWithLuck(2585, 3, 1, 1), false).OnFailedRoll(ItemDropRule.Common(2610, 1, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(998, 1, 1, 1), false);
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x00602C4C File Offset: 0x00600E4C
		private void RegisterBoss_Plantera()
		{
			short type = 262;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3328));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4934));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4806, this._masterModeDropRng));
			LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new Conditions.NotExpert());
			this.RegisterToNPC((int)type, leadingConditionRule);
			LeadingConditionRule leadingConditionRule2 = new LeadingConditionRule(new Conditions.FirstTimeKillingPlantera());
			leadingConditionRule.OnSuccess(leadingConditionRule2, false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(2109, 7, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(1141, 1, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(1182, 20, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(1305, 50, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(1157, 4, 1, 1), false);
			leadingConditionRule.OnSuccess(ItemDropRule.Common(3021, 10, 1, 1), false);
			IItemDropRule itemDropRule = ItemDropRule.Common(758, 1, 1, 1);
			itemDropRule.OnSuccess(ItemDropRule.Common(771, 1, 50, 150), true);
			leadingConditionRule2.OnSuccess(itemDropRule, true);
			leadingConditionRule2.OnFailedConditions(new OneFromRulesRule(1, new IItemDropRule[]
			{
				itemDropRule,
				ItemDropRule.Common(1255, 1, 1, 1),
				ItemDropRule.Common(788, 1, 1, 1),
				ItemDropRule.Common(1178, 1, 1, 1),
				ItemDropRule.Common(1259, 1, 1, 1),
				ItemDropRule.Common(1155, 1, 1, 1),
				ItemDropRule.Common(3018, 1, 1, 1)
			}), false);
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x00602DF0 File Offset: 0x00600FF0
		private void RegisterBoss_SkeletronPrime()
		{
			Conditions.NotExpert condition = new Conditions.NotExpert();
			short type = 127;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3327));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4933));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4805, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 2107, 7, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 1225, 1, 15, 30, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 547, 1, 25, 40, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(new Conditions.MechdusaKill(), 5382, 1, 1, 1, 1));
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x00602EA8 File Offset: 0x006010A8
		private void RegisterBoss_TheDestroyer()
		{
			Conditions.NotExpert condition = new Conditions.NotExpert();
			short type = 134;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3325));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4932));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4803, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 2113, 7, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 1225, 1, 15, 30, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 548, 1, 25, 40, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(new Conditions.MechdusaKill(), 5382, 1, 1, 1, 1));
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x00602F64 File Offset: 0x00601164
		private void RegisterBoss_Twins()
		{
			LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new Conditions.MissingTwin());
			LeadingConditionRule leadingConditionRule2 = new LeadingConditionRule(new Conditions.NotExpert());
			leadingConditionRule.OnSuccess(ItemDropRule.BossBag(3326), false);
			leadingConditionRule.OnSuccess(leadingConditionRule2, false);
			leadingConditionRule2.OnSuccess(ItemDropRule.Common(2106, 7, 1, 1), false);
			leadingConditionRule2.OnSuccess(ItemDropRule.Common(1225, 1, 15, 30), false);
			leadingConditionRule2.OnSuccess(ItemDropRule.Common(549, 1, 25, 40), false);
			leadingConditionRule.OnSuccess(ItemDropRule.MasterModeCommonDrop(4931), false);
			leadingConditionRule.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4804, this._masterModeDropRng), false);
			this.RegisterToMultipleNPCs(leadingConditionRule, new int[]
			{
				126,
				125
			});
			this.RegisterToMultipleNPCs(ItemDropRule.ByCondition(new Conditions.MechdusaKill(), 5382, 1, 1, 1, 1), new int[]
			{
				126,
				125
			});
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x00603054 File Offset: 0x00601254
		private void RegisterBoss_EOC()
		{
			Conditions.NotExpert condition = new Conditions.NotExpert();
			Conditions.IsCrimsonAndNotExpert condition2 = new Conditions.IsCrimsonAndNotExpert();
			Conditions.IsCorruptionAndNotExpert condition3 = new Conditions.IsCorruptionAndNotExpert();
			short type = 4;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3319));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4924));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(3763));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4798, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 2112, 7, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 1299, 40, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition2, 880, 1, 30, 90, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition2, 2171, 1, 1, 3, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition3, 47, 1, 20, 50, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition3, 56, 1, 30, 90, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition3, 59, 1, 1, 3, 1));
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x00603164 File Offset: 0x00601364
		private void RegisterBoss_BOC()
		{
			Conditions.NotExpert condition = new Conditions.NotExpert();
			short type = 266;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3321));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4926));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4800, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 880, 1, 40, 90, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 2104, 7, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 3060, 20, 1, 1, 1));
			short type2 = 267;
			this.RegisterToNPC((int)type2, new DropBasedOnMasterAndExpertMode(new CommonDrop(1329, 3, 2, 5, 2), new CommonDrop(1329, 3, 1, 3, 2), new CommonDrop(1329, 4, 1, 2, 2)));
			this.RegisterToNPC((int)type2, new DropBasedOnMasterAndExpertMode(new CommonDrop(880, 3, 5, 12, 2), new CommonDrop(880, 3, 5, 7, 2), new CommonDrop(880, 3, 2, 4, 2)));
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x00603278 File Offset: 0x00601478
		private void RegisterBoss_EOW()
		{
			Conditions.LegacyHack_IsBossAndExpert condition = new Conditions.LegacyHack_IsBossAndExpert();
			Conditions.LegacyHack_IsBossAndNotExpert condition2 = new Conditions.LegacyHack_IsBossAndNotExpert();
			int[] npcNetIds = new int[]
			{
				13,
				14,
				15
			};
			this.RegisterToMultipleNPCs(new DropBasedOnMasterAndExpertMode(ItemDropRule.Common(86, 2, 1, 2), ItemDropRule.Common(86, 5, 1, 2), ItemDropRule.Common(86, 10, 1, 2)), npcNetIds);
			this.RegisterToMultipleNPCs(new DropBasedOnMasterAndExpertMode(ItemDropRule.Common(56, 2, 2, 5), ItemDropRule.Common(56, 2, 1, 3), ItemDropRule.Common(56, 3, 1, 2)), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.BossBagByCondition(condition, 3320), npcNetIds);
			IItemDropRule rule = this.RegisterToMultipleNPCs(new LeadingConditionRule(new Conditions.LegacyHack_IsABoss()), npcNetIds);
			rule.OnSuccess(ItemDropRule.MasterModeCommonDrop(4925), false);
			rule.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4799, this._masterModeDropRng), false);
			this.RegisterToMultipleNPCs(ItemDropRule.ByCondition(condition2, 56, 1, 20, 60, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.ByCondition(condition2, 994, 20, 1, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.ByCondition(condition2, 2111, 7, 1, 1, 1), npcNetIds);
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x0060338C File Offset: 0x0060158C
		private void RegisterBoss_Deerclops()
		{
			Conditions.NotExpert condition = new Conditions.NotExpert();
			short type = 668;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(5111));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(5110));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(5090, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 5109, 7, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 5098, 3, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 5101, 3, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 5113, 3, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 5385, 14, 1, 1, 1));
			this.RegisterToNPC((int)type, new LeadingConditionRule(condition)).OnSuccess(new OneFromRulesRule(1, new IItemDropRule[]
			{
				ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
				{
					5117,
					5118,
					5119,
					5095
				})
			}), false);
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x00603490 File Offset: 0x00601690
		private void RegisterBoss_QueenBee()
		{
			Conditions.NotExpert condition = new Conditions.NotExpert();
			short type = 222;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3322));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4928));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4802, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 2108, 7, 1, 1, 1));
			this.RegisterToNPC((int)type, new DropBasedOnExpertMode(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				1121,
				1123,
				2888
			}), ItemDropRule.DropNothing()));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 1132, 3, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 1170, 15, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 2502, 20, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 1129, 3, 1, 1, 1)).OnFailedRoll(ItemDropRule.OneFromOptionsNotScalingWithLuck(2, new int[]
			{
				842,
				843,
				844
			}), false);
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 1130, 4, 10, 30, 3));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 2431, 1, 16, 26, 1));
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x006035D4 File Offset: 0x006017D4
		private void RegisterBoss_Skeletron()
		{
			Conditions.NotExpert condition = new Conditions.NotExpert();
			short type = 35;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3323));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4927));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4801, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 1281, 7, 1, 1, 1)).OnFailedRoll(ItemDropRule.Common(1273, 7, 1, 1), false).OnFailedRoll(ItemDropRule.Common(1313, 7, 1, 1), false);
			this.RegisterToNPC((int)type, ItemDropRule.Common(4993, 7, 1, 1));
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x00603678 File Offset: 0x00601878
		private void RegisterBoss_WOF()
		{
			Conditions.NotExpert condition = new Conditions.NotExpert();
			short type = 113;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3324));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4930));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4795, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 2105, 7, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 367, 1, 1, 1, 1));
			this.RegisterToNPC((int)type, new LeadingConditionRule(condition)).OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				490,
				491,
				489,
				2998
			}), false);
			this.RegisterToNPC((int)type, new LeadingConditionRule(condition)).OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				426,
				434,
				514,
				4912
			}), false);
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x00603750 File Offset: 0x00601950
		private void RegisterBoss_AncientCultist()
		{
			short type = 439;
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4937));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4809, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.Common(3372, 7, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.Common(3549, 1, 1, 1));
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x006037B8 File Offset: 0x006019B8
		private void RegisterBoss_MoonLord()
		{
			Conditions.NotExpert condition = new Conditions.NotExpert();
			short type = 398;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3332));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4938));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4810, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 3373, 7, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 4469, 10, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 3384, 1, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 3460, 1, 70, 90, 1));
			this.RegisterToNPC((int)type, new LeadingConditionRule(condition)).OnSuccess(new FromOptionsWithoutRepeatsDropRule(2, new int[]
			{
				3063,
				3389,
				3065,
				1553,
				3930,
				3541,
				3570,
				3571,
				3569
			}), false);
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x00603898 File Offset: 0x00601A98
		private void RegisterBoss_LunarTowers()
		{
			DropOneByOne.Parameters parameters2 = new DropOneByOne.Parameters
			{
				MinimumItemDropsCount = 12,
				MaximumItemDropsCount = 20,
				ChanceNumerator = 1,
				ChanceDenominator = 1,
				MinimumStackPerChunkBase = 1,
				MaximumStackPerChunkBase = 3,
				BonusMinDropsPerChunkPerPlayer = 0,
				BonusMaxDropsPerChunkPerPlayer = 0
			};
			DropOneByOne.Parameters parameters3 = parameters2;
			parameters3.BonusMinDropsPerChunkPerPlayer = 1;
			parameters3.BonusMaxDropsPerChunkPerPlayer = 1;
			parameters3.MinimumStackPerChunkBase = (int)((float)parameters2.MinimumStackPerChunkBase * 1.5f);
			parameters3.MaximumStackPerChunkBase = (int)((float)parameters2.MaximumStackPerChunkBase * 1.5f);
			this.RegisterToNPC(517, new DropBasedOnExpertMode(new DropOneByOne(3458, parameters2), new DropOneByOne(3458, parameters3)));
			this.RegisterToNPC(422, new DropBasedOnExpertMode(new DropOneByOne(3456, parameters2), new DropOneByOne(3456, parameters3)));
			this.RegisterToNPC(507, new DropBasedOnExpertMode(new DropOneByOne(3457, parameters2), new DropOneByOne(3457, parameters3)));
			this.RegisterToNPC(493, new DropBasedOnExpertMode(new DropOneByOne(3459, parameters2), new DropOneByOne(3459, parameters3)));
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x006039CC File Offset: 0x00601BCC
		private void RegisterBoss_Betsy()
		{
			Conditions.NotExpert condition = new Conditions.NotExpert();
			short type = 551;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3860));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4948));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4817, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 3863, 7, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 3883, 4, 1, 1, 1));
			this.RegisterToNPC((int)type, new LeadingConditionRule(condition)).OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				3827,
				3859,
				3870,
				3858
			}), false);
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x00603A7C File Offset: 0x00601C7C
		private void RegisterBoss_Golem()
		{
			Conditions.NotExpert condition = new Conditions.NotExpert();
			short type = 245;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3329));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4935));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4807, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 2110, 7, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 1294, 4, 1, 1, 1));
			IItemDropRule itemDropRule = ItemDropRule.Common(1258, 1, 1, 1);
			itemDropRule.OnSuccess(ItemDropRule.Common(1261, 1, 60, 180), true);
			this.RegisterToNPC((int)type, new LeadingConditionRule(condition)).OnSuccess(new OneFromRulesRule(1, new IItemDropRule[]
			{
				itemDropRule,
				ItemDropRule.Common(1122, 1, 1, 1),
				ItemDropRule.Common(899, 1, 1, 1),
				ItemDropRule.Common(1248, 1, 1, 1),
				ItemDropRule.Common(1295, 1, 1, 1),
				ItemDropRule.Common(1296, 1, 1, 1),
				ItemDropRule.Common(1297, 1, 1, 1)
			}), false);
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 2218, 1, 4, 8, 1));
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x00603BC4 File Offset: 0x00601DC4
		private void RegisterBoss_DukeFishron()
		{
			Conditions.NotExpert condition = new Conditions.NotExpert();
			short type = 370;
			this.RegisterToNPC((int)type, ItemDropRule.BossBag(3330));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeCommonDrop(4936));
			this.RegisterToNPC((int)type, ItemDropRule.MasterModeDropOnAllPlayers(4808, this._masterModeDropRng));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 2588, 7, 1, 1, 1));
			this.RegisterToNPC((int)type, ItemDropRule.ByCondition(condition, 2609, 15, 1, 1, 1));
			this.RegisterToNPC((int)type, new LeadingConditionRule(new Conditions.NotRemixSeed())).OnSuccess(new LeadingConditionRule(condition), false).OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				2611,
				2624,
				2622,
				2621,
				2623
			}), false);
			this.RegisterToNPC((int)type, new LeadingConditionRule(new Conditions.RemixSeed())).OnSuccess(new LeadingConditionRule(condition), false).OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				2611,
				2624,
				2622,
				2621,
				157
			}), false);
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x00603CBE File Offset: 0x00601EBE
		private void RegisterWeirdRules()
		{
			this.RegisterToMultipleNPCs(ItemDropRule.NormalvsExpert(3260, 40, 30), new int[]
			{
				86
			});
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x00603CE0 File Offset: 0x00601EE0
		private void RegisterGlobalRules()
		{
			this.RegisterToGlobal(new MechBossSpawnersDropRule());
			this.RegisterToGlobal(new SlimeBodyItemDropRule());
			this.RegisterToGlobal(ItemDropRule.ByCondition(new Conditions.HalloweenWeapons(), 1825, 2000, 1, 1, 1)).OnFailedRoll(ItemDropRule.Common(1827, 2000, 1, 1), false);
			this.RegisterToGlobal(new ItemDropWithConditionRule(1533, 2500, 1, 1, new Conditions.JungleKeyCondition(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(1534, 2500, 1, 1, new Conditions.CorruptKeyCondition(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(1535, 2500, 1, 1, new Conditions.CrimsonKeyCondition(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(1536, 2500, 1, 1, new Conditions.HallowKeyCondition(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(1537, 2500, 1, 1, new Conditions.FrozenKeyCondition(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(4714, 2500, 1, 1, new Conditions.DesertKeyCondition(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(1774, 80, 1, 1, new Conditions.HalloweenGoodieBagDrop(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(1869, 13, 1, 1, new Conditions.XmasPresentDrop(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(2701, 50, 20, 50, new Conditions.LivingFlames(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(520, 5, 1, 1, new Conditions.SoulOfLight(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(521, 5, 1, 1, new Conditions.SoulOfNight(), 1));
			this.RegisterToGlobal(ItemDropRule.ByCondition(new Conditions.PirateMap(), 1315, 100, 1, 1, 1));
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x00603E90 File Offset: 0x00602090
		private void RegisterFoodDrops()
		{
			this.RegisterToNPC(48, ItemDropRule.Food(4016, 50, 1, 1));
			this.RegisterToNPC(224, ItemDropRule.Food(4021, 50, 1, 1));
			this.RegisterToNPC(44, ItemDropRule.Food(4037, 10, 1, 1));
			this.RegisterToNPC(469, ItemDropRule.Food(4037, 100, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4020, 30, 1, 1), new int[]
			{
				163,
				238,
				164,
				165,
				530,
				531
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4029, 50, 1, 1), new int[]
			{
				480,
				481
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4030, 75, 1, 1), new int[]
			{
				498,
				499,
				500,
				501,
				502,
				503,
				504,
				505,
				506,
				496,
				497,
				494,
				495
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4036, 50, 1, 1), new int[]
			{
				482,
				483
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4015, 100, 1, 1), new int[]
			{
				6,
				173
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4026, 150, 1, 1), new int[]
			{
				150,
				147,
				184
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4027, 75, 1, 1), new int[]
			{
				154,
				206
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(3532, 15, 1, 1), new int[]
			{
				170,
				180,
				171
			});
			this.RegisterToNPC(289, ItemDropRule.Food(4018, 35, 1, 1));
			this.RegisterToNPC(34, ItemDropRule.Food(4018, 70, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4013, 21, 1, 1), new int[]
			{
				293,
				291,
				292
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(5042, 30, 1, 1), new int[]
			{
				43,
				175,
				56
			});
			this.RegisterToNPC(287, ItemDropRule.Food(5042, 10, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Food(5041, 150, 1, 1), new int[]
			{
				21,
				201,
				202,
				203,
				322,
				323,
				324,
				635,
				449,
				450,
				451,
				452
			});
			this.RegisterToNPC(290, ItemDropRule.Food(4013, 7, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4025, 30, 1, 1), new int[]
			{
				39,
				156
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4023, 40, 1, 1), new int[]
			{
				177,
				152
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4012, 50, 1, 1), new int[]
			{
				581,
				509,
				580,
				508,
				69
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4028, 30, 1, 1), new int[]
			{
				546,
				542,
				544,
				543,
				545
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4035, 50, 1, 1), new int[]
			{
				67,
				65
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Food(4011, 150, 1, 1), new int[]
			{
				120,
				137,
				138
			});
			this.RegisterToNPC(122, ItemDropRule.Food(4017, 75, 1, 1));
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x00604224 File Offset: 0x00602424
		private void RegisterTownNPCDrops()
		{
			this.RegisterToNPC(22, new ItemDropWithConditionRule(867, 1, 1, 1, new Conditions.NamedNPC("Andrew"), 1));
			this.RegisterToNPC(178, new ItemDropWithConditionRule(4372, 1, 1, 1, new Conditions.NamedNPC("Whitney"), 1));
			this.RegisterToNPC(227, new ItemDropWithConditionRule(5290, 1, 1, 1, new Conditions.NamedNPC("Jim"), 1));
			this.RegisterToNPC(353, ItemDropRule.Common(3352, 8, 1, 1));
			this.RegisterToNPC(441, ItemDropRule.Common(3351, 8, 1, 1));
			this.RegisterToNPC(227, ItemDropRule.Common(3350, 10, 1, 1));
			this.RegisterToNPC(550, ItemDropRule.Common(3821, 6, 1, 1));
			this.RegisterToNPC(208, ItemDropRule.Common(3548, 4, 30, 60));
			this.RegisterToNPC(207, ItemDropRule.Common(3349, 8, 1, 1));
			this.RegisterToNPC(124, ItemDropRule.Common(4818, 8, 1, 1));
			this.RegisterToNPC(663, ItemDropRule.ByCondition(new Conditions.IsHardmode(), 5065, 8, 1, 1, 1));
			this.RegisterToNPC(54, ItemDropRule.Common(260, 1, 1, 1));
			this.RegisterToNPC(368, ItemDropRule.Common(2222, 1, 1, 1));
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x00604398 File Offset: 0x00602598
		private void RegisterDD2EventDrops()
		{
			new Conditions.IsExpert();
			this.RegisterToNPC(576, new CommonDropNotScalingWithLuck(3865, 7, 1, 1));
			this.RegisterToNPC(576, ItemDropRule.NormalvsExpertOneFromOptionsNotScalingWithLuck(3, 2, new int[]
			{
				3811,
				3812
			}));
			this.RegisterToNPC(576, ItemDropRule.NormalvsExpertOneFromOptionsNotScalingWithLuck(2, 1, new int[]
			{
				3852,
				3854,
				3823,
				3835,
				3836
			}));
			this.RegisterToNPC(576, ItemDropRule.NormalvsExpertNotScalingWithLuck(3856, 5, 4));
			this.RegisterToNPC(577, new CommonDropNotScalingWithLuck(3865, 14, 1, 1));
			this.RegisterToNPC(577, ItemDropRule.MasterModeCommonDrop(4947));
			this.RegisterToNPC(577, ItemDropRule.MasterModeDropOnAllPlayers(4816, this._masterModeDropRng));
			this.RegisterToNPC(577, ItemDropRule.OneFromOptionsNotScalingWithLuck(6, new int[]
			{
				3811,
				3812
			}));
			this.RegisterToNPC(577, ItemDropRule.OneFromOptionsNotScalingWithLuck(4, new int[]
			{
				3852,
				3854,
				3823,
				3835,
				3836
			}));
			this.RegisterToNPC(577, ItemDropRule.Common(3856, 10, 1, 1));
			this.RegisterToNPC(564, ItemDropRule.Common(3864, 7, 1, 1));
			this.RegisterToNPC(564, ItemDropRule.MasterModeDropOnAllPlayers(4796, this._masterModeDropRng));
			this.RegisterToNPC(564, ItemDropRule.NormalvsExpertOneFromOptionsNotScalingWithLuck(2, 1, new int[]
			{
				3810,
				3809
			}));
			this.RegisterToNPC(564, new DropBasedOnExpertMode(ItemDropRule.NotScalingWithLuck(3814, 2, 1, 1), ItemDropRule.NotScalingWithLuck(3814, 1, 1, 1)));
			this.RegisterToNPC(564, new DropBasedOnExpertMode(ItemDropRule.NotScalingWithLuck(3815, 2, 4, 4), ItemDropRule.NotScalingWithLuck(3815, 1, 4, 4)));
			this.RegisterToNPC(564, ItemDropRule.NormalvsExpertOneFromOptionsNotScalingWithLuck(3, 2, new int[]
			{
				3857,
				3855
			}));
			this.RegisterToNPC(565, ItemDropRule.Common(3864, 14, 1, 1));
			this.RegisterToNPC(565, ItemDropRule.MasterModeCommonDrop(4946));
			this.RegisterToNPC(565, ItemDropRule.MasterModeDropOnAllPlayers(4796, this._masterModeDropRng));
			this.RegisterToNPC(565, ItemDropRule.OneFromOptionsNotScalingWithLuck(6, new int[]
			{
				3810,
				3809
			}));
			this.RegisterToNPC(565, new DropBasedOnExpertMode(ItemDropRule.NotScalingWithLuck(3814, 8, 1, 1), ItemDropRule.NotScalingWithLuck(3814, 4, 1, 1)));
			this.RegisterToNPC(565, new DropBasedOnExpertMode(ItemDropRule.NotScalingWithLuck(3815, 8, 4, 4), ItemDropRule.NotScalingWithLuck(3815, 4, 4, 4)));
			this.RegisterToNPC(565, ItemDropRule.OneFromOptionsNotScalingWithLuck(6, new int[]
			{
				3857,
				3855
			}));
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x006046AC File Offset: 0x006028AC
		private void RegisterHardmodeFeathers()
		{
			this.RegisterToNPC(156, ItemDropRule.Common(1518, 50, 1, 1));
			this.RegisterToNPC(243, ItemDropRule.Common(1519, 3, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1517, 300, 1, 1), new int[]
			{
				269,
				270,
				271,
				272,
				273,
				274,
				275,
				276,
				277,
				278,
				279,
				280
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1520, 40, 1, 1), new int[]
			{
				159,
				158
			});
			this.RegisterToNPC(48, ItemDropRule.Common(1516, 150, 1, 1));
			this.RegisterToNPC(176, new ItemDropWithConditionRule(1521, 100, 1, 1, new Conditions.BeatAnyMechBoss(), 1));
			this.RegisterToNPC(205, new ItemDropWithConditionRule(1611, 2, 1, 1, new Conditions.BeatAnyMechBoss(), 1));
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x0060479C File Offset: 0x0060299C
		private void RegisterYoyos()
		{
			this.RegisterToGlobal(new ItemDropWithConditionRule(3282, 400, 1, 1, new Conditions.YoyoCascade(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(3289, 300, 1, 1, new Conditions.YoyosAmarok(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(3286, 200, 1, 1, new Conditions.YoyosYelets(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(3291, 400, 1, 1, new Conditions.YoyosKraken(), 1));
			this.RegisterToGlobal(new ItemDropWithConditionRule(3290, 400, 1, 1, new Conditions.YoyosHelFire(), 1));
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x00604840 File Offset: 0x00602A40
		private void RegisterStatusImmunityItems()
		{
			this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(885, 100), new int[]
			{
				104,
				102,
				269,
				270,
				271,
				272
			});
			this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(886, 100), new int[]
			{
				77,
				273,
				274,
				275,
				276
			});
			this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(887, 100), new int[]
			{
				141,
				176,
				42,
				231,
				232,
				233,
				234,
				235
			});
			this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(888, 100), new int[]
			{
				81,
				79,
				183,
				630
			});
			this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(889, 100), new int[]
			{
				78,
				82,
				75
			});
			this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(890, 100), new int[]
			{
				103,
				75,
				79,
				630
			});
			this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(891, 100), new int[]
			{
				34,
				83,
				84,
				179,
				289
			});
			this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(892, 100), new int[]
			{
				94,
				182
			});
			this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(893, 100), new int[]
			{
				93,
				109,
				80
			});
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x00604994 File Offset: 0x00602B94
		private void RegisterPirateDrops()
		{
			int[] npcNetIds = new int[]
			{
				212,
				213,
				214,
				215
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(905, 4000, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(855, 2000, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(854, 1000, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2584, 1000, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(3033, 500, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(672, 200, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1277, 500, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1278, 500, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1279, 500, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1280, 500, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1704, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1705, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1710, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1716, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1720, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2379, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2389, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2405, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2843, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(3885, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2663, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(3904, 150, 80, 130), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(3910, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2238, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2133, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2137, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2143, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2147, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2151, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2155, 300, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(3263, 500, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(3264, 500, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(3265, 500, 1, 1), npcNetIds);
			this.RegisterToNPC(216, ItemDropRule.Common(905, 1000, 1, 1));
			this.RegisterToNPC(216, ItemDropRule.Common(855, 500, 1, 1));
			this.RegisterToNPC(216, ItemDropRule.Common(854, 250, 1, 1));
			this.RegisterToNPC(216, ItemDropRule.Common(2584, 250, 1, 1));
			this.RegisterToNPC(216, ItemDropRule.Common(3033, 125, 1, 1));
			this.RegisterToNPC(216, ItemDropRule.Common(672, 50, 1, 1));
			this.RegisterToNPC(491, ItemDropRule.Common(905, 50, 1, 1));
			this.RegisterToNPC(491, ItemDropRule.Common(855, 15, 1, 1));
			this.RegisterToNPC(491, ItemDropRule.Common(854, 15, 1, 1));
			this.RegisterToNPC(491, ItemDropRule.Common(2584, 15, 1, 1));
			this.RegisterToNPC(491, ItemDropRule.Common(3033, 15, 1, 1));
			this.RegisterToNPC(491, ItemDropRule.Common(4471, 20, 1, 1));
			this.RegisterToNPC(491, ItemDropRule.Common(672, 10, 1, 1));
			this.RegisterToNPC(491, ItemDropRule.MasterModeCommonDrop(4940));
			this.RegisterToNPC(491, ItemDropRule.MasterModeDropOnAllPlayers(4792, this._masterModeDropRng));
			this.RegisterToNPC(491, ItemDropRule.OneFromOptions(1, new int[]
			{
				1704,
				1705,
				1710,
				1716,
				1720,
				2379,
				2389,
				2405,
				2843,
				3885,
				2663,
				3910,
				2238,
				2133,
				2137,
				2143,
				2147,
				2151,
				2155
			}));
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x00604EA5 File Offset: 0x006030A5
		private void RegisterBloodMoonFishingEnemies()
		{
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x00604EA8 File Offset: 0x006030A8
		private void RegisterBossTrophies()
		{
			Conditions.LegacyHack_IsABoss condition = new Conditions.LegacyHack_IsABoss();
			this.RegisterToNPC(4, ItemDropRule.ByCondition(condition, 1360, 10, 1, 1, 1));
			this.RegisterToNPC(13, ItemDropRule.ByCondition(condition, 1361, 10, 1, 1, 1));
			this.RegisterToNPC(14, ItemDropRule.ByCondition(condition, 1361, 10, 1, 1, 1));
			this.RegisterToNPC(15, ItemDropRule.ByCondition(condition, 1361, 10, 1, 1, 1));
			this.RegisterToNPC(266, ItemDropRule.ByCondition(condition, 1362, 10, 1, 1, 1));
			this.RegisterToNPC(35, ItemDropRule.ByCondition(condition, 1363, 10, 1, 1, 1));
			this.RegisterToNPC(222, ItemDropRule.ByCondition(condition, 1364, 10, 1, 1, 1));
			this.RegisterToNPC(113, ItemDropRule.ByCondition(condition, 1365, 10, 1, 1, 1));
			this.RegisterToNPC(134, ItemDropRule.ByCondition(condition, 1366, 10, 1, 1, 1));
			this.RegisterToNPC(127, ItemDropRule.ByCondition(condition, 1367, 10, 1, 1, 1));
			this.RegisterToNPC(262, ItemDropRule.ByCondition(condition, 1370, 10, 1, 1, 1));
			this.RegisterToNPC(245, ItemDropRule.ByCondition(condition, 1371, 10, 1, 1, 1));
			this.RegisterToNPC(50, ItemDropRule.ByCondition(condition, 2489, 10, 1, 1, 1));
			this.RegisterToNPC(370, ItemDropRule.ByCondition(condition, 2589, 10, 1, 1, 1));
			this.RegisterToNPC(439, ItemDropRule.ByCondition(condition, 3357, 10, 1, 1, 1));
			this.RegisterToNPC(395, ItemDropRule.ByCondition(condition, 3358, 10, 1, 1, 1));
			this.RegisterToNPC(398, ItemDropRule.ByCondition(condition, 3595, 10, 1, 1, 1));
			this.RegisterToNPC(636, ItemDropRule.ByCondition(condition, 4783, 10, 1, 1, 1));
			this.RegisterToNPC(657, ItemDropRule.ByCondition(condition, 4958, 10, 1, 1, 1));
			this.RegisterToNPC(668, ItemDropRule.ByCondition(condition, 5108, 10, 1, 1, 1));
			this.RegisterToNPC(125, ItemDropRule.Common(1368, 10, 1, 1));
			this.RegisterToNPC(126, ItemDropRule.Common(1369, 10, 1, 1));
			this.RegisterToNPC(491, ItemDropRule.Common(3359, 10, 1, 1));
			this.RegisterToNPC(551, ItemDropRule.Common(3866, 10, 1, 1));
			this.RegisterToNPC(564, ItemDropRule.Common(3867, 10, 1, 1));
			this.RegisterToNPC(565, ItemDropRule.Common(3867, 10, 1, 1));
			this.RegisterToNPC(576, ItemDropRule.Common(3868, 10, 1, 1));
			this.RegisterToNPC(577, ItemDropRule.Common(3868, 10, 1, 1));
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x0060519C File Offset: 0x0060339C
		private void RegisterMartianDrops()
		{
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2860, 8, 8, 20), new int[]
			{
				520,
				383,
				389,
				385,
				382,
				381,
				390,
				386
			});
			int[] npcNetIds = new int[]
			{
				520,
				383,
				389,
				385,
				382,
				381,
				390,
				386
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2798, 800, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2800, 800, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2882, 800, 1, 1), npcNetIds);
			int[] npcNetIds2 = new int[]
			{
				383,
				389,
				386
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2806, 200, 1, 1), npcNetIds2);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2807, 200, 1, 1), npcNetIds2);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2808, 200, 1, 1), npcNetIds2);
			int[] npcNetIds3 = new int[]
			{
				385,
				382,
				381,
				390
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2803, 200, 1, 1), npcNetIds3);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2804, 200, 1, 1), npcNetIds3);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2805, 200, 1, 1), npcNetIds3);
			this.RegisterToNPC(395, ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				2797,
				2749,
				2795,
				2796,
				2880,
				2769
			}));
			this.RegisterToNPC(395, ItemDropRule.MasterModeCommonDrop(4939));
			this.RegisterToNPC(395, ItemDropRule.MasterModeDropOnAllPlayers(4815, this._masterModeDropRng));
			this.RegisterToNPC(390, ItemDropRule.Common(2771, 30, 1, 1));
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x00605358 File Offset: 0x00603558
		private void RegisterMiscDrops()
		{
			this.RegisterToNPC(68, ItemDropRule.Common(1169, 1, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(3086, 1, 5, 10), new int[]
			{
				483,
				482
			});
			this.RegisterToNPC(77, ItemDropRule.Common(723, 150, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.NormalvsExpert(3102, 2, 1), new int[]
			{
				195,
				196
			});
			this.RegisterToNPC(471, ItemDropRule.NormalvsExpertOneFromOptions(2, 1, new int[]
			{
				3052,
				3053,
				3054
			}));
			this.RegisterToNPC(153, ItemDropRule.Common(1328, 12, 1, 1));
			this.RegisterToNPC(59, new LeadingConditionRule(new Conditions.RemixSeed())).OnSuccess(ItemDropRule.Common(23, 1, 1, 2), false);
			this.RegisterToNPC(59, new LeadingConditionRule(new Conditions.RemixSeed())).OnSuccess(ItemDropRule.NormalvsExpert(1309, 8000, 5600), false);
			this.RegisterToNPC(120, new LeadingConditionRule(new Conditions.TenthAnniversaryIsUp())).OnSuccess(ItemDropRule.Common(1326, 100, 1, 1), false);
			this.RegisterToNPC(120, new LeadingConditionRule(new Conditions.TenthAnniversaryIsNotUp())).OnSuccess(ItemDropRule.NormalvsExpert(1326, 500, 400), false);
			this.RegisterToNPC(49, new LeadingConditionRule(new Conditions.NotRemixSeed())).OnSuccess(ItemDropRule.Common(1325, 250, 1, 1), false);
			this.RegisterToNPC(49, new LeadingConditionRule(new Conditions.RemixSeed())).OnSuccess(ItemDropRule.Common(1314, 250, 1, 1), false);
			this.RegisterToNPC(109, new LeadingConditionRule(new Conditions.NotRemixSeed())).OnSuccess(ItemDropRule.Common(1314, 5, 1, 1), false);
			this.RegisterToNPC(109, new LeadingConditionRule(new Conditions.RemixSeed())).OnSuccess(ItemDropRule.Common(1325, 5, 1, 1), false);
			this.RegisterToNPC(156, new LeadingConditionRule(new Conditions.NotRemixSeed())).OnSuccess(ItemDropRule.Common(683, 30, 1, 1), false);
			this.RegisterToNPC(156, new LeadingConditionRule(new Conditions.RemixSeed())).OnSuccess(ItemDropRule.Common(112, 30, 1, 1), false);
			this.RegisterToNPC(634, ItemDropRule.Common(4764, 40, 1, 1));
			this.RegisterToNPC(185, ItemDropRule.Common(951, 150, 1, 1));
			this.RegisterToNPC(185, new DropBasedOnExpertMode(ItemDropRule.Common(5070, 1, 1, 2), new CommonDrop(5070, 1, 1, 3, 1)));
			this.RegisterToNPC(44, ItemDropRule.Common(1320, 20, 1, 1));
			this.RegisterToNPC(44, ItemDropRule.Common(88, 20, 1, 1));
			this.RegisterToNPC(60, ItemDropRule.Common(1322, 150, 1, 1));
			this.RegisterToNPC(151, ItemDropRule.Common(1322, 50, 1, 1));
			this.RegisterToNPC(24, ItemDropRule.Common(1323, 20, 1, 1));
			this.RegisterToNPC(109, ItemDropRule.Common(1324, 10, 1, 1));
			this.RegisterToNPC(109, ItemDropRule.Common(4271, 10, 1, 1));
			int[] npcNetIds = new int[]
			{
				163,
				238
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1308, 40, 1, 1), npcNetIds);
			this.RegisterToMultipleNPCs(new DropBasedOnExpertMode(ItemDropRule.Common(2607, 2, 1, 3), new CommonDrop(2607, 10, 1, 3, 9)), npcNetIds);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1306, 100, 1, 1), new int[]
			{
				197,
				206,
				169,
				154
			});
			this.RegisterToNPC(244, ItemDropRule.Common(23, 1, 1, 20));
			this.RegisterToNPC(244, ItemDropRule.Common(662, 1, 30, 60));
			this.RegisterToNPC(250, ItemDropRule.Common(1244, 15, 1, 1));
			this.RegisterToNPC(172, ItemDropRule.Common(754, 1, 1, 1));
			this.RegisterToNPC(172, ItemDropRule.Common(755, 1, 1, 1));
			this.RegisterToNPC(110, ItemDropRule.Common(682, 200, 1, 1));
			this.RegisterToNPC(110, ItemDropRule.Common(1321, 40, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(4428, 100, 1, 1), new int[]
			{
				170,
				180,
				171
			});
			this.RegisterToMultipleNPCs(new ItemDropWithConditionRule(4613, 25, 1, 1, new Conditions.WindyEnoughForKiteDrops(), 1), new int[]
			{
				170,
				180,
				171
			});
			this.RegisterToMultipleNPCs(new ItemDropWithConditionRule(5096, 10, 1, 1, new Conditions.DontStarveIsUp(), 1), new int[]
			{
				170,
				180,
				171
			});
			this.RegisterToMultipleNPCs(new ItemDropWithConditionRule(5096, 25, 1, 1, new Conditions.DontStarveIsNotUp(), 1), new int[]
			{
				170,
				180,
				171
			});
			this.RegisterToNPC(154, ItemDropRule.Common(1253, 50, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(726, 50, 1, 1), new int[]
			{
				169,
				206
			});
			this.RegisterToNPC(243, ItemDropRule.Common(2161, 1, 1, 1));
			this.RegisterToNPC(155, ItemDropRule.NormalvsExpert(5130, 30, 25));
			this.RegisterToNPC(480, ItemDropRule.Common(3269, 25, 1, 1));
			this.RegisterToNPC(480, ItemDropRule.NormalvsExpert(3781, 40, 20));
			int[] npcNetIds2 = new int[]
			{
				198,
				199,
				226
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1172, 1000, 1, 1), npcNetIds2);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1293, 50, 1, 1), npcNetIds2);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(2766, 7, 1, 2), npcNetIds2);
			int[] npcNetIds3 = new int[]
			{
				78,
				79,
				80,
				630
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(870, 75, 1, 1), npcNetIds3);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(871, 75, 1, 1), npcNetIds3);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(872, 75, 1, 1), npcNetIds3);
			this.RegisterToNPC(473, ItemDropRule.OneFromOptions(1, new int[]
			{
				3008,
				3014,
				3012,
				3015,
				3023
			}));
			this.RegisterToNPC(474, ItemDropRule.OneFromOptions(1, new int[]
			{
				3006,
				3007,
				3013,
				3016,
				3020
			}));
			this.RegisterToNPC(475, ItemDropRule.OneFromOptions(1, new int[]
			{
				3029,
				3030,
				3051,
				3022
			}));
			this.RegisterToNPC(476, ItemDropRule.Common(52, 3, 1, 1));
			this.RegisterToNPC(476, ItemDropRule.Common(1724, 3, 1, 1));
			this.RegisterToNPC(476, ItemDropRule.Common(2353, 3, 5, 10));
			this.RegisterToNPC(476, ItemDropRule.Common(1922, 3, 1, 1));
			this.RegisterToNPC(476, ItemDropRule.Common(678, 3, 3, 5));
			this.RegisterToNPC(476, ItemDropRule.Common(1336, 3, 1, 1));
			this.RegisterToNPC(476, ItemDropRule.Common(2676, 3, 2, 4));
			this.RegisterToNPC(476, ItemDropRule.Common(2272, 3, 1, 1));
			this.RegisterToNPC(476, ItemDropRule.Common(5395, 3, 1, 1));
			this.RegisterToNPC(476, ItemDropRule.Common(4986, 3, 69, 69));
			int[] npcNetIds4 = new int[]
			{
				473,
				474,
				475
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(499, 1, 5, 10), npcNetIds4);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(500, 1, 5, 15), npcNetIds4);
			this.RegisterToNPC(87, new ItemDropWithConditionRule(4379, 25, 1, 1, new Conditions.WindyEnoughForKiteDrops(), 1));
			this.RegisterToNPC(87, new DropBasedOnExpertMode(ItemDropRule.Common(575, 1, 5, 10), ItemDropRule.Common(575, 1, 10, 20)));
			this.RegisterToMultipleNPCs(ItemDropRule.OneFromOptions(10, new int[]
			{
				803,
				804,
				805
			}), new int[]
			{
				161,
				431
			});
			this.RegisterToNPC(217, ItemDropRule.Common(1115, 1, 1, 1));
			this.RegisterToNPC(218, ItemDropRule.Common(1116, 1, 1, 1));
			this.RegisterToNPC(219, ItemDropRule.Common(1117, 1, 1, 1));
			this.RegisterToNPC(220, ItemDropRule.Common(1118, 1, 1, 1));
			this.RegisterToNPC(221, ItemDropRule.Common(1119, 1, 1, 1));
			this.RegisterToNPC(167, ItemDropRule.Common(879, 50, 1, 1));
			this.RegisterToNPC(628, ItemDropRule.Common(313, 2, 1, 2));
			int[] npcNetIds5 = new int[]
			{
				143,
				144,
				145
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(593, 1, 5, 10), npcNetIds5);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(527, 10, 1, 1), new int[]
			{
				79,
				630
			});
			this.RegisterToNPC(80, ItemDropRule.Common(528, 10, 1, 1));
			this.RegisterToNPC(524, ItemDropRule.Common(3794, 10, 1, 3));
			this.RegisterToNPC(525, ItemDropRule.Common(3794, 10, 1, 1));
			this.RegisterToNPC(525, ItemDropRule.Common(522, 3, 1, 3));
			this.RegisterToNPC(525, ItemDropRule.Common(527, 15, 1, 1));
			this.RegisterToNPC(526, ItemDropRule.Common(3794, 10, 1, 1));
			this.RegisterToNPC(526, ItemDropRule.Common(1332, 3, 1, 3));
			this.RegisterToNPC(526, ItemDropRule.Common(527, 15, 1, 1));
			this.RegisterToNPC(527, ItemDropRule.Common(3794, 10, 1, 1));
			this.RegisterToNPC(527, ItemDropRule.Common(528, 15, 1, 1));
			this.RegisterToNPC(513, ItemDropRule.Common(3380, 2, 1, 2));
			this.RegisterToNPC(532, ItemDropRule.Common(3380, 1, 1, 3));
			this.RegisterToNPC(532, ItemDropRule.Common(3771, 50, 1, 1));
			this.RegisterToNPC(528, ItemDropRule.Common(2802, 25, 1, 1));
			this.RegisterToNPC(528, ItemDropRule.OneFromOptions(60, new int[]
			{
				3786,
				3785,
				3784
			}));
			this.RegisterToNPC(529, ItemDropRule.Common(2801, 25, 1, 1));
			this.RegisterToNPC(529, ItemDropRule.OneFromOptions(40, new int[]
			{
				3786,
				3785,
				3784
			}));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(18, 100, 1, 1), new int[]
			{
				49,
				51,
				150,
				93,
				634
			});
			this.RegisterToMultipleNPCs(new ItemDropWithConditionRule(5097, 250, 1, 1, new Conditions.DontStarveIsNotUp(), 1), new int[]
			{
				49,
				51,
				150,
				93,
				634,
				151,
				60,
				137,
				152
			});
			this.RegisterToMultipleNPCs(new ItemDropWithConditionRule(5097, 100, 1, 1, new Conditions.DontStarveIsUp(), 1), new int[]
			{
				49,
				51,
				150,
				93,
				634,
				151,
				60,
				137,
				152
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Common(393, 50, 1, 1), new int[]
			{
				16,
				185,
				167,
				197
			});
			this.RegisterToNPC(58, ItemDropRule.Common(393, 75, 1, 1));
			int[] npcNetIds6 = new int[]
			{
				494,
				495,
				496,
				497,
				498,
				499,
				500,
				501,
				502,
				503,
				504,
				505,
				506
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(18, 80, 1, 1), npcNetIds6).OnFailedRoll(ItemDropRule.Common(393, 80, 1, 1), false).OnFailedRoll(ItemDropRule.Common(3285, 25, 1, 1), false);
			int[] npcNetIds7 = new int[]
			{
				21,
				201,
				202,
				203,
				322,
				323,
				324,
				635,
				449,
				450,
				451,
				452
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(954, 100, 1, 1), npcNetIds7).OnFailedRoll(ItemDropRule.Common(955, 200, 1, 1), false).OnFailedRoll(ItemDropRule.Common(1166, 200, 1, 1), false).OnFailedRoll(ItemDropRule.Common(1274, 500, 1, 1), false);
			this.RegisterToNPC(6, ItemDropRule.OneFromOptions(175, new int[]
			{
				956,
				957,
				958
			}));
			int[] npcNetIds8 = new int[]
			{
				42,
				43,
				231,
				232,
				233,
				234,
				235
			};
			this.RegisterToMultipleNPCs(ItemDropRule.OneFromOptions(100, new int[]
			{
				960,
				961,
				962
			}), npcNetIds8);
			int[] npcNetIds9 = new int[]
			{
				31,
				32,
				294,
				295,
				296
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(959, 450, 1, 1), npcNetIds9);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1307, 300, 1, 1), npcNetIds9);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(996, 200, 1, 1), new int[]
			{
				174,
				179,
				182,
				183,
				98,
				83,
				94,
				81,
				101
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Common(522, 1, 2, 5), new int[]
			{
				101,
				98
			});
			this.RegisterToNPC(98, ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), 4611, 25, 1, 1, 1));
			this.RegisterToNPC(86, ItemDropRule.Common(526, 1, 1, 1));
			this.RegisterToNPC(86, ItemDropRule.Common(856, 100, 1, 1));
			this.RegisterToNPC(86, ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), 4684, 25, 1, 1, 1));
			this.RegisterToNPC(224, ItemDropRule.Common(4057, 100, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(40, 1, 1, 9), new int[]
			{
				186,
				432
			});
			this.RegisterToNPC(225, ItemDropRule.Common(1243, 45, 1, 1)).OnFailedRoll(ItemDropRule.Common(23, 1, 2, 6), false);
			this.RegisterToNPC(537, ItemDropRule.Common(23, 1, 2, 3));
			this.RegisterToNPC(537, ItemDropRule.NormalvsExpert(1309, 8000, 5600));
			int[] npcNetIds10 = new int[]
			{
				335,
				336,
				333,
				334
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1906, 20, 1, 1), npcNetIds10);
			this.RegisterToNPC(-4, ItemDropRule.Common(3111, 1, 25, 50));
			this.RegisterToNPC(-4, ItemDropRule.NormalvsExpert(1309, 100, 70));
			int[] npcNetIds11 = new int[]
			{
				1,
				16,
				138,
				141,
				147,
				184,
				187,
				433,
				204,
				302,
				333,
				334,
				335,
				336,
				535,
				658,
				659,
				660
			};
			int[] npcNetIds12 = new int[]
			{
				-6,
				-7,
				-8,
				-9
			};
			int[] npcNetIds13 = new int[]
			{
				-6,
				-7,
				-8,
				-9,
				-4
			};
			IItemDropRule entry = this.RegisterToMultipleNPCs(ItemDropRule.Common(23, 1, 1, 2), npcNetIds11);
			this.RemoveFromMultipleNPCs(entry, npcNetIds13);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(23, 1, 2, 5), npcNetIds12);
			IItemDropRule entry2 = this.RegisterToMultipleNPCs(ItemDropRule.NormalvsExpert(1309, 10000, 7000), npcNetIds11);
			this.RemoveFromMultipleNPCs(entry2, npcNetIds13);
			this.RegisterToMultipleNPCs(ItemDropRule.NormalvsExpert(1309, 10000, 7000), npcNetIds12);
			this.RegisterToNPC(75, ItemDropRule.Common(501, 1, 1, 3));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(23, 1, 2, 4), new int[]
			{
				81,
				183
			});
			this.RegisterToNPC(122, ItemDropRule.Common(23, 1, 5, 10));
			this.RegisterToNPC(71, ItemDropRule.Common(327, 1, 1, 1));
			int[] npcNetIds14 = new int[]
			{
				2,
				317,
				318,
				190,
				191,
				192,
				193,
				194,
				133
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(236, 100, 1, 1), npcNetIds14).OnFailedRoll(ItemDropRule.Common(38, 3, 1, 1), false);
			this.RegisterToNPC(133, ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), 4683, 25, 1, 1, 1));
			this.RegisterToNPC(104, ItemDropRule.Common(485, 60, 1, 1));
			this.RegisterToNPC(58, ItemDropRule.Common(263, 250, 1, 1)).OnFailedRoll(ItemDropRule.Common(118, 30, 1, 1), false);
			this.RegisterToNPC(102, ItemDropRule.Common(263, 250, 1, 1));
			int[] npcNetIds15 = new int[]
			{
				3,
				591,
				590,
				331,
				332,
				132,
				161,
				186,
				187,
				188,
				189,
				200,
				223,
				319,
				320,
				321,
				430,
				431,
				432,
				433,
				434,
				435,
				436
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(216, 50, 1, 1), npcNetIds15);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1304, 250, 1, 1), npcNetIds15);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(5332, 1500, 1, 1), npcNetIds15);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(8, 1, 5, 20), new int[]
			{
				590,
				591
			});
			this.RegisterToMultipleNPCs(ItemDropRule.NormalvsExpert(3212, 150, 75), new int[]
			{
				489,
				490
			});
			this.RegisterToMultipleNPCs(ItemDropRule.NormalvsExpert(3213, 200, 100), new int[]
			{
				489,
				490
			});
			this.RegisterToNPC(223, ItemDropRule.OneFromOptions(20, new int[]
			{
				1135,
				1136
			}));
			this.RegisterToNPC(66, ItemDropRule.Common(267, 1, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(272, 35, 1, 1), new int[]
			{
				62,
				66
			});
			this.RegisterToNPC(52, ItemDropRule.Common(251, 1, 1, 1));
			this.RegisterToNPC(53, ItemDropRule.Common(239, 1, 1, 1));
			this.RegisterToNPC(536, ItemDropRule.Common(3478, 1, 1, 1));
			this.RegisterToNPC(536, ItemDropRule.Common(3479, 1, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(323, 3, 1, 2), new int[]
			{
				69,
				581,
				580,
				508,
				509
			});
			this.RegisterToNPC(582, ItemDropRule.Common(323, 6, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(3772, 50, 1, 1), new int[]
			{
				581,
				580,
				508,
				509
			});
			this.RegisterToNPC(73, ItemDropRule.Common(362, 1, 1, 2));
			int[] npcNetIds16 = new int[]
			{
				483,
				482
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(3109, 30, 1, 1), npcNetIds16);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(4400, 20, 1, 1), npcNetIds16);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(68, 3, 1, 1), new int[]
			{
				6,
				94
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1330, 3, 1, 1), new int[]
			{
				181,
				173,
				239,
				182,
				240
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Common(68, 3, 1, 2), new int[]
			{
				7,
				8,
				9
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Common(69, 1, 3, 8), new int[]
			{
				7,
				8,
				9
			});
			this.RegisterToMultipleNPCs(new ItemDropWithConditionRule(5094, 100, 1, 1, new Conditions.DontStarveIsUp(), 1), new int[]
			{
				6,
				7,
				8,
				9,
				173,
				181,
				239,
				240
			});
			this.RegisterToMultipleNPCs(new ItemDropWithConditionRule(5094, 525, 1, 1, new Conditions.DontStarveIsNotUp(), 1), new int[]
			{
				6,
				7,
				8,
				9,
				173,
				181,
				239,
				240
			});
			this.RegisterToMultipleNPCs(new ItemDropWithConditionRule(5091, 500, 1, 1, new Conditions.DontStarveIsUp(), 1), new int[]
			{
				6,
				7,
				8,
				9,
				94,
				81,
				101,
				173,
				181,
				239,
				240,
				174,
				183,
				242,
				241,
				268,
				182,
				98,
				99,
				100
			});
			this.RegisterToMultipleNPCs(new ItemDropWithConditionRule(5091, 1500, 1, 1, new Conditions.DontStarveIsNotUp(), 1), new int[]
			{
				6,
				7,
				8,
				9,
				94,
				81,
				101,
				173,
				181,
				239,
				240,
				174,
				183,
				242,
				241,
				268,
				182,
				98,
				99,
				100
			});
			this.RegisterToMultipleNPCs(new DropBasedOnExpertMode(ItemDropRule.Common(215, 50, 1, 1), ItemDropRule.WithRerolls(215, 1, 50, 1, 1)), new int[]
			{
				10,
				11,
				12,
				95,
				96,
				97
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Common(243, 75, 1, 1), new int[]
			{
				47,
				464
			});
			this.RegisterToMultipleNPCs(ItemDropRule.OneFromOptions(50, new int[]
			{
				3757,
				3758,
				3759
			}), new int[]
			{
				168,
				470
			});
			this.RegisterToNPC(533, ItemDropRule.Common(3795, 40, 1, 1)).OnFailedRoll(ItemDropRule.Common(3770, 30, 1, 1), false);
			int[] npcNetIds17 = new int[]
			{
				63,
				103,
				64
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(1303, 100, 1, 1), npcNetIds17);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(282, 1, 1, 4), npcNetIds17);
			this.RegisterToMultipleNPCs(ItemDropRule.Common(282, 1, 1, 4), new int[]
			{
				223
			});
			this.RegisterToMultipleNPCs(ItemDropRule.Common(282, 1, 1, 4), new int[]
			{
				224
			});
			this.RegisterToNPC(63, ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), 4649, 50, 1, 1, 1));
			this.RegisterToNPC(64, ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), 4650, 50, 1, 1, 1));
			this.RegisterToNPC(481, ItemDropRule.Common(3094, 2, 40, 80));
			this.RegisterToNPC(481, ItemDropRule.OneFromOptions(7, new int[]
			{
				3187,
				3188,
				3189
			}));
			this.RegisterToNPC(481, ItemDropRule.Common(4463, 20, 1, 1));
			int[] npcNetIds18 = new int[]
			{
				21,
				167,
				201,
				202,
				481,
				203,
				322,
				323,
				324,
				449,
				450,
				451,
				452
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(118, 25, 1, 1), npcNetIds18);
			this.RegisterToNPC(44, ItemDropRule.Common(118, 25, 1, 1)).OnFailedRoll(ItemDropRule.OneFromOptions(4, new int[]
			{
				410,
				411
			}), false).OnFailedRoll(ItemDropRule.Common(166, 1, 1, 3), false);
			this.RegisterToNPC(45, ItemDropRule.Common(238, 1, 1, 1));
			this.RegisterToNPC(23, ItemDropRule.Common(116, 50, 1, 1));
			this.RegisterToNPC(24, ItemDropRule.Common(244, 250, 1, 1));
			int[] npcNetIds19 = new int[]
			{
				31,
				32,
				34,
				294,
				295,
				296
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(932, 250, 1, 1), npcNetIds19).OnFailedRoll(ItemDropRule.Common(3095, 100, 1, 1), false).OnFailedRoll(ItemDropRule.Common(327, 65, 1, 1), false).OnFailedRoll(ItemDropRule.ByCondition(new Conditions.NotExpert(), 154, 1, 1, 3, 1), false);
			this.RegisterToMultipleNPCs(ItemDropRule.ByCondition(new Conditions.IsExpert(), 154, 1, 2, 6, 1), npcNetIds19);
			int[] npcNetIds20 = new int[]
			{
				26,
				27,
				28,
				29,
				111
			};
			this.RegisterToMultipleNPCs(ItemDropRule.Common(160, 200, 1, 1), npcNetIds20).OnFailedRoll(ItemDropRule.Common(161, 2, 1, 5), false);
			this.RegisterToNPC(175, ItemDropRule.Common(1265, 100, 1, 1));
			this.RegisterToNPC(175, ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), 4675, 25, 1, 1, 1));
			this.RegisterToMultipleNPCs(new DropBasedOnExpertMode(new CommonDrop(209, 3, 1, 1, 2), ItemDropRule.Common(209, 1, 1, 1)), new int[]
			{
				42,
				231,
				232,
				233,
				234,
				235
			});
			this.RegisterToNPC(176, ItemDropRule.Common(209, 6, 1, 1));
			this.RegisterToNPC(177, new ItemDropWithConditionRule(5089, 100, 1, 1, new Conditions.DontStarveIsNotUp(), 1));
			this.RegisterToNPC(177, new ItemDropWithConditionRule(5089, 40, 1, 1, new Conditions.DontStarveIsUp(), 1));
			this.RegisterToNPC(204, ItemDropRule.NormalvsExpert(209, 2, 1));
			this.RegisterToNPC(43, ItemDropRule.NormalvsExpert(210, 2, 1));
			this.RegisterToNPC(43, ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), 4648, 25, 1, 1, 1));
			this.RegisterToNPC(39, ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), 4610, 15, 1, 1, 1));
			this.RegisterToNPC(65, ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), 4651, 25, 1, 1, 1));
			this.RegisterToNPC(65, ItemDropRule.Common(268, 20, 1, 1)).OnFailedRoll(ItemDropRule.Common(319, 1, 1, 1), false);
			this.RegisterToNPC(48, ItemDropRule.NotScalingWithLuck(320, 2, 1, 1));
			this.RegisterToNPC(541, ItemDropRule.Common(3783, 1, 1, 1));
			this.RegisterToMultipleNPCs(ItemDropRule.Common(319, 8, 1, 1), new int[]
			{
				542,
				543,
				544,
				545
			});
			this.RegisterToMultipleNPCs(ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), 4669, 25, 1, 1, 1), new int[]
			{
				542,
				543,
				544,
				545
			});
			this.RegisterToNPC(543, ItemDropRule.Common(527, 25, 1, 1));
			this.RegisterToNPC(544, ItemDropRule.Common(527, 25, 1, 1));
			this.RegisterToNPC(545, ItemDropRule.Common(528, 25, 1, 1));
			this.RegisterToNPC(47, ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), 4670, 25, 1, 1, 1));
			this.RegisterToNPC(464, ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), 4671, 25, 1, 1, 1));
			this.RegisterToNPC(268, ItemDropRule.Common(1332, 1, 2, 5));
			this.RegisterToNPC(631, ItemDropRule.Common(3, 1, 10, 20));
			this.RegisterToNPC(631, ItemDropRule.Common(4761, 10, 1, 1));
			int[] npcNetIds21 = new int[]
			{
				594
			};
			LeadingConditionRule leadingConditionRule = new LeadingConditionRule(new Conditions.NeverTrue());
			int[] options = new int[0];
			IItemDropRule rule = leadingConditionRule.OnSuccess(ItemDropRule.OneFromOptions(8, options), false);
			int chanceDenominator = 9;
			rule.OnSuccess(new CommonDrop(4367, chanceDenominator, 1, 1, 1), false);
			rule.OnSuccess(new CommonDrop(4368, chanceDenominator, 1, 1, 1), false);
			rule.OnSuccess(new CommonDrop(4369, chanceDenominator, 1, 1, 1), false);
			rule.OnSuccess(new CommonDrop(4370, chanceDenominator, 1, 1, 1), false);
			rule.OnSuccess(new CommonDrop(4371, chanceDenominator, 1, 1, 1), false);
			rule.OnSuccess(new CommonDrop(4612, chanceDenominator, 1, 1, 1), false);
			rule.OnSuccess(new CommonDrop(4674, chanceDenominator, 1, 1, 1), false);
			rule.OnSuccess(new CommonDrop(4343, chanceDenominator, 2, 5, 1), false);
			rule.OnSuccess(new CommonDrop(4344, chanceDenominator, 2, 5, 1), false);
			this.RegisterToMultipleNPCs(leadingConditionRule, npcNetIds21);
		}

		/// <summary>
		/// Retrieves all the registered <see cref="T:Terraria.GameContent.ItemDropRules.IItemDropRule" /> for this specific item type.
		/// </summary>
		// Token: 0x06004426 RID: 17446 RVA: 0x00606F50 File Offset: 0x00605150
		public List<IItemDropRule> GetRulesForItemID(int itemID)
		{
			List<IItemDropRule> list = new List<IItemDropRule>();
			List<IItemDropRule> value;
			if (this._entriesByItemId.TryGetValue(itemID, out value))
			{
				list.AddRange(value);
			}
			return list;
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x00606F7C File Offset: 0x0060517C
		public IItemDropRule RegisterToItem(int type, IItemDropRule entry)
		{
			this.RegisterToItemId(type, entry);
			List<int> value;
			if (type > 0 && this._itemIdsByType.TryGetValue(type, out value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					this.RegisterToItemId(value[i], entry);
				}
			}
			return entry;
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x00606FC8 File Offset: 0x006051C8
		public IItemDropRule RegisterToMultipleItems(IItemDropRule entry, params int[] itemIds)
		{
			for (int i = 0; i < itemIds.Length; i++)
			{
				this.RegisterToItem(itemIds[i], entry);
			}
			return entry;
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x00606FEF File Offset: 0x006051EF
		public void RegisterToItemId(int itemId, IItemDropRule entry)
		{
			if (!this._entriesByItemId.ContainsKey(itemId))
			{
				this._entriesByItemId[itemId] = new List<IItemDropRule>();
			}
			this._entriesByItemId[itemId].Add(entry);
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x00607022 File Offset: 0x00605222
		private void RemoveFromItemId(int itemId, IItemDropRule entry)
		{
			if (this._entriesByItemId.ContainsKey(itemId))
			{
				this._entriesByItemId[itemId].Remove(entry);
			}
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x00607048 File Offset: 0x00605248
		public IItemDropRule RemoveFromItem(int type, IItemDropRule entry)
		{
			this.RemoveFromItemId(type, entry);
			List<int> value;
			if (type > 0 && this._itemIdsByType.TryGetValue(type, out value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					this.RemoveFromItemId(value[i], entry);
				}
			}
			return entry;
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x00607094 File Offset: 0x00605294
		private void RegisterBossBags()
		{
			short item = 4957;
			this.RegisterToItem((int)item, ItemDropRule.Common(4987, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(4986, 1, 25, 74));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(4959, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(4981, 2, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(4980, 2, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(4758, 3, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.FewFromOptionsNotScalingWithLuckWithX(2, 1, 1, new int[]
			{
				4982,
				4983,
				4984
			}));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(657));
			item = 4782;
			this.RegisterToItem((int)item, ItemDropRule.Common(4989, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(4784, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(4823, 10, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				4923,
				4952,
				4953,
				4914
			}));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(4778, 4, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(4715, 20, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(5075, 20, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(636));
			item = 3318;
			this.RegisterToItem((int)item, ItemDropRule.Common(3090, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2493, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2430, 2, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.FewFromOptionsNotScalingWithLuckWithX(2, 1, 1, new int[]
			{
				256,
				257,
				258
			}));
			this.RegisterToItem((int)item, ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				2610,
				2585
			}));
			this.RegisterToItem((int)item, ItemDropRule.Common(998, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(50));
			item = 3328;
			this.RegisterToItem((int)item, ItemDropRule.Common(3336, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2109, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.Common(1141, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(1182, 15, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(1305, 20, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(1157, 2, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(3021, 10, 1, 1));
			IItemDropRule itemDropRuleGrenadeLauncher = ItemDropRule.Common(758, 1, 1, 1);
			itemDropRuleGrenadeLauncher.OnSuccess(ItemDropRule.Common(771, 1, 50, 149), true);
			this.RegisterToItem((int)item, new OneFromRulesRule(1, new IItemDropRule[]
			{
				itemDropRuleGrenadeLauncher,
				ItemDropRule.Common(1255, 1, 1, 1),
				ItemDropRule.Common(788, 1, 1, 1),
				ItemDropRule.Common(1178, 1, 1, 1),
				ItemDropRule.Common(1259, 1, 1, 1),
				ItemDropRule.Common(1155, 1, 1, 1),
				ItemDropRule.Common(3018, 1, 1, 1)
			}));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(262));
			item = 3327;
			this.RegisterToItem((int)item, ItemDropRule.Common(3356, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2107, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.Common(547, 1, 25, 40));
			this.RegisterToItem((int)item, ItemDropRule.Common(1225, 1, 20, 35));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(127));
			item = 3325;
			this.RegisterToItem((int)item, ItemDropRule.Common(3355, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2113, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.Common(548, 1, 25, 40));
			this.RegisterToItem((int)item, ItemDropRule.Common(1225, 1, 20, 35));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(134));
			item = 3326;
			this.RegisterToItem((int)item, ItemDropRule.Common(3354, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2106, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.Common(549, 1, 25, 40));
			this.RegisterToItem((int)item, ItemDropRule.Common(1225, 1, 20, 35));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(125));
			item = 3319;
			Conditions.IsCrimson conditionIsCrimson = new Conditions.IsCrimson();
			Conditions.IsCorruption conditionIsCorruption = new Conditions.IsCorruption();
			this.RegisterToItem((int)item, ItemDropRule.Common(3097, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2112, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(1299, 30, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsCrimson, 880, 1, 30, 87, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsCrimson, 2171, 1, 1, 3, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsCorruption, 47, 1, 20, 49, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsCorruption, 56, 1, 30, 87, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsCorruption, 59, 1, 1, 3, 1));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(4));
			item = 3321;
			Conditions.NotMasterMode conditionIsNotMaster = new Conditions.NotMasterMode();
			Conditions.IsMasterMode conditionIsMaster = new Conditions.IsMasterMode();
			this.RegisterToItem((int)item, ItemDropRule.Common(3223, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2104, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsNotMaster, 880, 1, 80, 110, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsNotMaster, 1329, 1, 20, 40, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsMaster, 880, 1, 110, 135, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsMaster, 1329, 1, 30, 50, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(3060, 20, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(266));
			item = 3320;
			this.RegisterToItem((int)item, ItemDropRule.Common(3224, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2111, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsNotMaster, 56, 1, 80, 110, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsNotMaster, 86, 1, 20, 40, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsMaster, 56, 1, 110, 135, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(conditionIsMaster, 86, 1, 30, 50, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(994, 20, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(13));
			item = 5111;
			this.RegisterToItem((int)item, ItemDropRule.Common(5100, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(5109, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(5385, 14, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(5098, 3, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(5101, 3, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(5113, 3, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				5117,
				5118,
				5119,
				5095
			}));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(668));
			item = 3322;
			this.RegisterToItem((int)item, ItemDropRule.Common(3333, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2108, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				1121,
				1123,
				2888
			}));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(1132, 3, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(1170, 9, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2502, 9, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.Common(1129, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				842,
				843,
				844
			}));
			this.RegisterToItem((int)item, ItemDropRule.Common(1130, 1, 10, 29));
			this.RegisterToItem((int)item, ItemDropRule.Common(2431, 1, 17, 29));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(222));
			item = 3323;
			this.RegisterToItem((int)item, ItemDropRule.Common(3245, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				1281,
				1273,
				1313
			}));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(35));
			item = 3324;
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(new Conditions.NotUsedDemonHeart(), 3335, 1, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2105, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.Common(367, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				2998,
				489,
				490,
				491
			}));
			this.RegisterToItem((int)item, ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				514,
				426,
				434,
				4912
			}));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(113));
			item = 3331;
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(3372, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(439));
			item = 3332;
			this.RegisterToItem((int)item, ItemDropRule.Common(1131, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.Common(3577, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.Common(4954, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(3373, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.Common(3460, 1, 90, 110));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(4469, 10, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.ByCondition(new Conditions.NoPortalGun(), 3384, 1, 1, 1, 1));
			this.RegisterToItem((int)item, new FromOptionsWithoutRepeatsDropRule(2, new int[]
			{
				3063,
				3389,
				3065,
				1553,
				3930,
				3541,
				3570,
				3571,
				3569
			}));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(398));
			item = 3860;
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(3863, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				3827,
				3859,
				3870,
				3858
			}));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(3883, 4, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.Common(3817, 1, 30, 49));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(551));
			item = 3329;
			this.RegisterToItem((int)item, ItemDropRule.Common(3337, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2110, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(1294, 3, 1, 1));
			IItemDropRule itemDropRuleStynger = ItemDropRule.Common(1258, 1, 1, 1);
			itemDropRuleStynger.OnSuccess(ItemDropRule.Common(1261, 1, 60, 99), true);
			this.RegisterToItem((int)item, new OneFromRulesRule(1, new IItemDropRule[]
			{
				itemDropRuleStynger,
				ItemDropRule.Common(1122, 1, 1, 1),
				ItemDropRule.Common(899, 1, 1, 1),
				ItemDropRule.Common(1248, 1, 1, 1),
				ItemDropRule.Common(1295, 1, 1, 1),
				ItemDropRule.Common(1296, 1, 1, 1),
				ItemDropRule.Common(1297, 1, 1, 1)
			}));
			this.RegisterToItem((int)item, ItemDropRule.Common(2218, 1, 18, 23));
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(245));
			item = 3330;
			this.RegisterToItem((int)item, ItemDropRule.Common(3367, 1, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2588, 7, 1, 1));
			this.RegisterToItem((int)item, ItemDropRule.NotScalingWithLuck(2609, 10, 1, 1));
			this.RegisterToItem((int)item, new LeadingConditionRule(new Conditions.NotRemixSeed())).OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				2611,
				2624,
				2622,
				2621,
				2623
			}), false);
			this.RegisterToItem((int)item, new LeadingConditionRule(new Conditions.RemixSeed())).OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				2611,
				2624,
				2622,
				2621,
				157
			}), false);
			this.RegisterToItem((int)item, ItemDropRule.CoinsBasedOnNPCValue(370));
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x00607D88 File Offset: 0x00605F88
		private void RegisterCrateDrops()
		{
			IItemDropRule[] themed = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(3200, 40, 1, 1),
				ItemDropRule.NotScalingWithLuck(3201, 40, 1, 1),
				ItemDropRule.NotScalingWithLuck(997, 50, 1, 1)
			};
			IItemDropRule[] hardmodeThemed = new IItemDropRule[]
			{
				ItemDropRule.ByCondition(new Conditions.IsHardmode(), 3064, 200, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(3200, 40, 1, 1),
				ItemDropRule.NotScalingWithLuck(3201, 40, 1, 1),
				ItemDropRule.NotScalingWithLuck(2424, 25, 1, 1)
			};
			IItemDropRule[] coin = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(73, 3, 1, 5),
				ItemDropRule.NotScalingWithLuck(72, 1, 20, 90)
			};
			IItemDropRule[] ores = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(12, 1, 4, 15),
				ItemDropRule.NotScalingWithLuck(699, 1, 4, 15),
				ItemDropRule.NotScalingWithLuck(11, 1, 4, 15),
				ItemDropRule.NotScalingWithLuck(700, 1, 4, 15)
			};
			IItemDropRule[] hardmodeOres = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(364, 1, 4, 15),
				ItemDropRule.NotScalingWithLuck(1104, 1, 4, 15)
			};
			IItemDropRule[] bars = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(20, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(703, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(22, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(704, 1, 2, 5)
			};
			IItemDropRule[] hardmodeBars = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(381, 1, 2, 3),
				ItemDropRule.NotScalingWithLuck(1184, 1, 2, 3)
			};
			IItemDropRule[] potions = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(288, 1, 1, 3),
				ItemDropRule.NotScalingWithLuck(290, 1, 1, 3),
				ItemDropRule.NotScalingWithLuck(292, 1, 1, 3),
				ItemDropRule.NotScalingWithLuck(299, 1, 1, 3),
				ItemDropRule.NotScalingWithLuck(298, 1, 1, 3),
				ItemDropRule.NotScalingWithLuck(304, 1, 1, 3),
				ItemDropRule.NotScalingWithLuck(291, 1, 1, 3),
				ItemDropRule.NotScalingWithLuck(2322, 1, 1, 3),
				ItemDropRule.NotScalingWithLuck(2323, 1, 1, 3),
				ItemDropRule.NotScalingWithLuck(2329, 1, 1, 3)
			};
			IItemDropRule[] extraPotions = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(28, 1, 5, 15),
				ItemDropRule.NotScalingWithLuck(110, 1, 5, 15)
			};
			IItemDropRule[] extraBait = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(2675, 3, 1, 4),
				ItemDropRule.NotScalingWithLuck(2674, 1, 1, 4)
			};
			IItemDropRule bc_surfaceLoot = ItemDropRule.OneFromOptionsNotScalingWithLuck(20, new int[]
			{
				285,
				953,
				4341,
				3068,
				3084
			});
			IItemDropRule[] woodenCrateDrop = new IItemDropRule[]
			{
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, themed),
				bc_surfaceLoot,
				ItemDropRule.SequentialRulesNotScalingWithLuck(7, coin),
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, new IItemDropRule[]
				{
					new OneFromRulesRule(7, ores),
					new OneFromRulesRule(8, bars)
				}),
				new OneFromRulesRule(7, potions)
			};
			IItemDropRule[] pearlwoodCrateDrop = new IItemDropRule[]
			{
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, hardmodeThemed),
				bc_surfaceLoot,
				ItemDropRule.SequentialRulesNotScalingWithLuck(7, coin),
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, new IItemDropRule[]
				{
					ItemDropRule.SequentialRulesNotScalingWithLuck(7, new IItemDropRule[]
					{
						new OneFromRulesRule(2, hardmodeOres),
						new OneFromRulesRule(1, ores)
					}),
					ItemDropRule.SequentialRulesNotScalingWithLuck(8, new IItemDropRule[]
					{
						new OneFromRulesRule(2, hardmodeBars),
						new OneFromRulesRule(1, bars)
					})
				}),
				new OneFromRulesRule(7, potions)
			};
			this.RegisterToItem(2334, ItemDropRule.AlwaysAtleastOneSuccess(woodenCrateDrop));
			this.RegisterToItem(3979, ItemDropRule.AlwaysAtleastOneSuccess(pearlwoodCrateDrop));
			this.RegisterToMultipleItems(new OneFromRulesRule(3, extraPotions), new int[]
			{
				2334,
				3979
			});
			this.RegisterToMultipleItems(ItemDropRule.SequentialRulesNotScalingWithLuck(3, extraBait), new int[]
			{
				2334,
				3979
			});
			themed = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(2501, 25, 1, 1),
				ItemDropRule.NotScalingWithLuck(2587, 20, 1, 1),
				ItemDropRule.NotScalingWithLuck(2608, 15, 1, 1),
				ItemDropRule.NotScalingWithLuck(3200, 20, 1, 1),
				ItemDropRule.NotScalingWithLuck(3201, 20, 1, 1)
			};
			hardmodeThemed = new IItemDropRule[]
			{
				ItemDropRule.ByCondition(new Conditions.IsHardmode(), 3064, 60, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(2501, 25, 1, 1),
				ItemDropRule.NotScalingWithLuck(2587, 20, 1, 1),
				ItemDropRule.NotScalingWithLuck(2608, 15, 1, 1),
				ItemDropRule.NotScalingWithLuck(3200, 20, 1, 1),
				ItemDropRule.NotScalingWithLuck(3201, 20, 1, 1)
			};
			ores = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(12, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(699, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(11, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(700, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(14, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(701, 1, 12, 21)
			};
			hardmodeOres = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(364, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(1104, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(365, 1, 12, 21),
				ItemDropRule.NotScalingWithLuck(1105, 1, 12, 21)
			};
			bars = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(20, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(703, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(22, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(704, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(21, 1, 4, 7),
				ItemDropRule.NotScalingWithLuck(705, 1, 4, 7)
			};
			hardmodeBars = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(381, 1, 3, 7),
				ItemDropRule.NotScalingWithLuck(1184, 1, 3, 7),
				ItemDropRule.NotScalingWithLuck(382, 1, 3, 7),
				ItemDropRule.NotScalingWithLuck(1191, 1, 3, 7)
			};
			potions = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(288, 1, 2, 4),
				ItemDropRule.NotScalingWithLuck(296, 1, 2, 4),
				ItemDropRule.NotScalingWithLuck(304, 1, 2, 4),
				ItemDropRule.NotScalingWithLuck(305, 1, 2, 4),
				ItemDropRule.NotScalingWithLuck(2322, 1, 2, 4),
				ItemDropRule.NotScalingWithLuck(2323, 1, 2, 4),
				ItemDropRule.NotScalingWithLuck(2324, 1, 2, 4),
				ItemDropRule.NotScalingWithLuck(2327, 1, 2, 4)
			};
			extraPotions = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(188, 1, 5, 15),
				ItemDropRule.NotScalingWithLuck(189, 1, 5, 15)
			};
			extraBait = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(2676, 3, 2, 4),
				ItemDropRule.NotScalingWithLuck(2675, 1, 2, 4)
			};
			IItemDropRule[] ironCrate = new IItemDropRule[]
			{
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, themed),
				ItemDropRule.NotScalingWithLuck(73, 4, 5, 10),
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, new IItemDropRule[]
				{
					new OneFromRulesRule(6, ores),
					new OneFromRulesRule(4, bars)
				}),
				new OneFromRulesRule(4, potions)
			};
			IItemDropRule[] mythrilCrate = new IItemDropRule[]
			{
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, hardmodeThemed),
				ItemDropRule.NotScalingWithLuck(73, 4, 5, 10),
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, new IItemDropRule[]
				{
					ItemDropRule.SequentialRulesNotScalingWithLuck(6, new IItemDropRule[]
					{
						new OneFromRulesRule(2, hardmodeOres),
						new OneFromRulesRule(1, ores)
					}),
					ItemDropRule.SequentialRulesNotScalingWithLuck(4, new IItemDropRule[]
					{
						new OneFromRulesRule(3, 2, hardmodeBars),
						new OneFromRulesRule(1, bars)
					})
				}),
				new OneFromRulesRule(4, potions)
			};
			this.RegisterToItem(2335, ItemDropRule.AlwaysAtleastOneSuccess(ironCrate));
			this.RegisterToItem(3980, ItemDropRule.AlwaysAtleastOneSuccess(mythrilCrate));
			this.RegisterToMultipleItems(new OneFromRulesRule(2, extraPotions), new int[]
			{
				2335,
				3980
			});
			this.RegisterToMultipleItems(ItemDropRule.SequentialRulesNotScalingWithLuck(2, extraBait), new int[]
			{
				2335,
				3980
			});
			themed = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(29, 8, 1, 1),
				ItemDropRule.NotScalingWithLuck(2491, 10, 1, 1)
			};
			hardmodeThemed = new IItemDropRule[]
			{
				ItemDropRule.ByCondition(new Conditions.IsHardmode(), 3064, 20, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(29, 8, 1, 1),
				ItemDropRule.NotScalingWithLuck(2491, 10, 1, 1)
			};
			ores = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(14, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(701, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(13, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(702, 1, 25, 34)
			};
			hardmodeOres = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(365, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(1105, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(366, 1, 25, 34),
				ItemDropRule.NotScalingWithLuck(1106, 1, 25, 34)
			};
			bars = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(21, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(705, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(19, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(706, 1, 8, 11)
			};
			hardmodeBars = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(382, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(1191, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(391, 1, 8, 11),
				ItemDropRule.NotScalingWithLuck(1198, 1, 8, 11)
			};
			potions = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(288, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(296, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(305, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(2322, 1, 2, 5),
				ItemDropRule.NotScalingWithLuck(2323, 1, 2, 5)
			};
			extraPotions = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(188, 1, 5, 20),
				ItemDropRule.NotScalingWithLuck(189, 1, 5, 20)
			};
			IItemDropRule[] goldCrate = new IItemDropRule[]
			{
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, themed),
				ItemDropRule.NotScalingWithLuck(73, 3, 8, 20),
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, new IItemDropRule[]
				{
					new OneFromRulesRule(5, ores),
					new OneFromRulesRule(3, 1, bars)
				}),
				new OneFromRulesRule(3, potions),
				ItemDropRule.NotScalingWithLuck(989, 30, 1, 1)
			};
			IItemDropRule[] titaniumCrate = new IItemDropRule[]
			{
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, hardmodeThemed),
				ItemDropRule.NotScalingWithLuck(73, 3, 8, 20),
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, new IItemDropRule[]
				{
					ItemDropRule.SequentialRulesNotScalingWithLuck(5, new IItemDropRule[]
					{
						new OneFromRulesRule(2, hardmodeOres),
						new OneFromRulesRule(1, ores)
					}),
					ItemDropRule.SequentialRulesNotScalingWithLuckWithNumerator(3, 1, new IItemDropRule[]
					{
						new OneFromRulesRule(3, 2, hardmodeBars),
						new OneFromRulesRule(1, bars)
					})
				}),
				new OneFromRulesRule(3, potions),
				ItemDropRule.NotScalingWithLuck(989, 15, 1, 1)
			};
			this.RegisterToItem(2336, ItemDropRule.AlwaysAtleastOneSuccess(goldCrate));
			this.RegisterToItem(3981, ItemDropRule.AlwaysAtleastOneSuccess(titaniumCrate));
			this.RegisterToMultipleItems(new OneFromRulesRule(2, extraPotions), new int[]
			{
				2336,
				3981
			});
			this.RegisterToMultipleItems(new CommonDrop(2676, 3, 3, 7, 2), new int[]
			{
				2336,
				3981
			});
			IItemDropRule[] bc_jungle = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(3017, 20, 1, 1),
				ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
				{
					212,
					964,
					211,
					213,
					2292
				})
			};
			IItemDropRule bc_bamboo = ItemDropRule.NotScalingWithLuck(4564, 3, 20, 50);
			IItemDropRule bc_seaweed = ItemDropRule.NotScalingWithLuck(753, 20, 1, 1);
			IItemDropRule bc_sky = ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				158,
				2219,
				65,
				159
			});
			IItemDropRule bc_cloud = ItemDropRule.NotScalingWithLuck(751, 2, 50, 100);
			IItemDropRule bc_fledgeWings = ItemDropRule.NotScalingWithLuck(4978, 40, 1, 1);
			IItemDropRule bc_skyPaintings = ItemDropRule.OneFromOptionsNotScalingWithLuck(2, new int[]
			{
				5226,
				5254,
				5238,
				5258,
				5255,
				5388
			});
			IItemDropRule bc_son = ItemDropRule.NotScalingWithLuck(521, 2, 2, 5);
			IItemDropRule bc_corrupt = ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				162,
				111,
				96,
				115,
				64
			});
			IItemDropRule bc_crimson = ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				800,
				802,
				1256,
				1290,
				3062
			});
			IItemDropRule bc_cursed = ItemDropRule.NotScalingWithLuck(522, 2, 2, 5);
			IItemDropRule bc_ichor = ItemDropRule.NotScalingWithLuck(1332, 2, 2, 5);
			IItemDropRule bc_sol = ItemDropRule.NotScalingWithLuck(520, 2, 2, 5);
			IItemDropRule bc_shard = ItemDropRule.NotScalingWithLuck(502, 2, 4, 10);
			IItemDropRule bc_lockbox = ItemDropRule.Common(3085, 1, 1, 1);
			IItemDropRule bc_book = ItemDropRule.NotScalingWithLuck(149, 2, 5, 15);
			IItemDropRule ruleSnowballCannonIceBow = ItemDropRule.ByCondition(new Conditions.NotRemixSeed(), 1319, 1, 1, 1, 1);
			ruleSnowballCannonIceBow.OnFailedConditions(ItemDropRule.NotScalingWithLuck(725, 1, 1, 1), true);
			IItemDropRule[] bc_iceList = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(670, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(724, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(950, 1, 1, 1),
				ruleSnowballCannonIceBow,
				ItemDropRule.NotScalingWithLuck(987, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(1579, 1, 1, 1)
			};
			IItemDropRule bc_ice = new OneFromRulesRule(1, bc_iceList);
			IItemDropRule bc_fish = ItemDropRule.NotScalingWithLuck(669, 20, 1, 1);
			IItemDropRule bc_scarab = ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
			{
				4056,
				4442,
				4055,
				4061,
				4062,
				4276,
				4262,
				4263
			});
			IItemDropRule bc_bomb = ItemDropRule.NotScalingWithLuck(4423, 4, 4, 6);
			IItemDropRule bc_fossil = ItemDropRule.NotScalingWithLuck(3380, 4, 10, 16);
			IItemDropRule bc_sandstormBottle = ItemDropRule.NotScalingWithLuck(857, 35, 1, 1);
			IItemDropRule[] bc_lava = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(906, 20, 1, 1),
				ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
				{
					4822,
					4828,
					4880,
					4881,
					4868
				})
			};
			IItemDropRule bc_pot = ItemDropRule.NotScalingWithLuck(4858, 4, 2, 2);
			IItemDropRule bc_obsi = ItemDropRule.Common(4879, 1, 1, 1);
			IItemDropRule bc_wet = ItemDropRule.NotScalingWithLuck(4824, 3, 7, 10);
			IItemDropRule bc_plant = ItemDropRule.OneFromOptionsNotScalingWithLuck(2, new int[]
			{
				4902,
				4903,
				4904,
				4905,
				4906
			});
			IItemDropRule bc_ornate = ItemDropRule.NotScalingWithLuck(4737, 20, 1, 1);
			IItemDropRule bc_hellcart = ItemDropRule.NotScalingWithLuck(4443, 20, 1, 1);
			IItemDropRule bc_cake = ItemDropRule.NotScalingWithLuck(4551, 20, 1, 1);
			IItemDropRule[] bc_sea = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(863, 10, 1, 1),
				ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
				{
					186,
					4404,
					277,
					187
				})
			};
			IItemDropRule bc_pile = ItemDropRule.NotScalingWithLuck(4090, 3, 20, 50);
			IItemDropRule bc_sharkbait = ItemDropRule.NotScalingWithLuck(4425, 10, 1, 1);
			IItemDropRule bc_sand = ItemDropRule.NotScalingWithLuck(4460, 10, 1, 1);
			IItemDropRule bc_goldCoin = ItemDropRule.NotScalingWithLuck(73, 4, 5, 12);
			ores = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(12, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(699, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(11, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(700, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(14, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(701, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(13, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(702, 1, 20, 35)
			};
			hardmodeOres = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(364, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(1104, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(365, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(1105, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(366, 1, 20, 35),
				ItemDropRule.NotScalingWithLuck(1106, 1, 20, 35)
			};
			bars = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(22, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(704, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(21, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(705, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(19, 1, 6, 16),
				ItemDropRule.NotScalingWithLuck(706, 1, 6, 16)
			};
			hardmodeBars = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(381, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(1184, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(382, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(1191, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(391, 1, 5, 16),
				ItemDropRule.NotScalingWithLuck(1198, 1, 5, 16)
			};
			potions = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(288, 1, 2, 4),
				ItemDropRule.NotScalingWithLuck(296, 1, 2, 4),
				ItemDropRule.NotScalingWithLuck(304, 1, 2, 4),
				ItemDropRule.NotScalingWithLuck(305, 1, 2, 4),
				ItemDropRule.NotScalingWithLuck(2322, 1, 2, 4),
				ItemDropRule.NotScalingWithLuck(2323, 1, 2, 4)
			};
			extraPotions = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(188, 1, 5, 17),
				ItemDropRule.NotScalingWithLuck(189, 1, 5, 17)
			};
			extraBait = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(2676, 2, 2, 6),
				ItemDropRule.NotScalingWithLuck(2675, 1, 2, 6)
			};
			IItemDropRule hardmodeBiomeCrateOres = ItemDropRule.SequentialRulesNotScalingWithLuck(7, new IItemDropRule[]
			{
				new OneFromRulesRule(2, hardmodeOres),
				new OneFromRulesRule(1, ores)
			});
			IItemDropRule hardmodeBiomeCrateBars = ItemDropRule.SequentialRulesNotScalingWithLuck(4, new IItemDropRule[]
			{
				new OneFromRulesRule(3, 2, hardmodeBars),
				new OneFromRulesRule(1, bars)
			});
			IItemDropRule[] jungle = new IItemDropRule[]
			{
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, bc_jungle),
				bc_goldCoin,
				new OneFromRulesRule(7, ores),
				new OneFromRulesRule(4, bars),
				new OneFromRulesRule(4, potions),
				bc_bamboo,
				bc_seaweed
			};
			IItemDropRule[] bramble = new IItemDropRule[]
			{
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, bc_jungle),
				bc_goldCoin,
				hardmodeBiomeCrateOres,
				hardmodeBiomeCrateBars,
				new OneFromRulesRule(4, potions),
				bc_bamboo,
				bc_seaweed
			};
			IItemDropRule[] sky = new IItemDropRule[]
			{
				bc_sky,
				bc_fledgeWings,
				bc_cloud,
				bc_skyPaintings,
				bc_goldCoin,
				new OneFromRulesRule(7, ores),
				new OneFromRulesRule(4, bars),
				new OneFromRulesRule(4, potions)
			};
			IItemDropRule[] azure = new IItemDropRule[]
			{
				bc_sky,
				bc_fledgeWings,
				bc_cloud,
				bc_skyPaintings,
				bc_goldCoin,
				hardmodeBiomeCrateOres,
				hardmodeBiomeCrateBars,
				new OneFromRulesRule(4, potions)
			};
			IItemDropRule[] corrupt = new IItemDropRule[]
			{
				bc_corrupt,
				bc_goldCoin,
				new OneFromRulesRule(7, ores),
				new OneFromRulesRule(4, bars),
				new OneFromRulesRule(4, potions)
			};
			IItemDropRule[] defiled = new IItemDropRule[]
			{
				bc_corrupt,
				bc_goldCoin,
				hardmodeBiomeCrateOres,
				hardmodeBiomeCrateBars,
				new OneFromRulesRule(4, potions),
				bc_son,
				bc_cursed
			};
			IItemDropRule[] crimson = new IItemDropRule[]
			{
				bc_crimson,
				bc_goldCoin,
				new OneFromRulesRule(7, ores),
				new OneFromRulesRule(4, bars),
				new OneFromRulesRule(4, potions)
			};
			IItemDropRule[] hematic = new IItemDropRule[]
			{
				bc_crimson,
				bc_goldCoin,
				hardmodeBiomeCrateOres,
				hardmodeBiomeCrateBars,
				new OneFromRulesRule(4, potions),
				bc_son,
				bc_ichor
			};
			IItemDropRule[] hallowed = new IItemDropRule[]
			{
				bc_goldCoin,
				new OneFromRulesRule(7, ores),
				new OneFromRulesRule(4, bars),
				new OneFromRulesRule(4, potions)
			};
			IItemDropRule[] divine = new IItemDropRule[]
			{
				bc_goldCoin,
				hardmodeBiomeCrateOres,
				hardmodeBiomeCrateBars,
				new OneFromRulesRule(4, potions),
				bc_sol,
				bc_shard
			};
			IItemDropRule[] dungeon = new IItemDropRule[]
			{
				bc_lockbox,
				bc_book,
				bc_goldCoin,
				new OneFromRulesRule(7, ores),
				new OneFromRulesRule(4, bars),
				new OneFromRulesRule(4, potions)
			};
			IItemDropRule[] stockade = new IItemDropRule[]
			{
				bc_lockbox,
				bc_book,
				bc_goldCoin,
				hardmodeBiomeCrateOres,
				hardmodeBiomeCrateBars,
				new OneFromRulesRule(4, potions)
			};
			IItemDropRule[] frozen = new IItemDropRule[]
			{
				bc_ice,
				bc_goldCoin,
				new OneFromRulesRule(7, ores),
				new OneFromRulesRule(4, bars),
				new OneFromRulesRule(4, potions),
				bc_fish
			};
			IItemDropRule[] boreal = new IItemDropRule[]
			{
				bc_ice,
				bc_goldCoin,
				hardmodeBiomeCrateOres,
				hardmodeBiomeCrateBars,
				new OneFromRulesRule(4, potions),
				bc_fish
			};
			IItemDropRule[] oasis = new IItemDropRule[]
			{
				bc_scarab,
				bc_bomb,
				bc_sandstormBottle,
				bc_goldCoin,
				bc_fossil,
				new OneFromRulesRule(7, ores),
				new OneFromRulesRule(4, bars),
				new OneFromRulesRule(4, potions)
			};
			IItemDropRule[] mirage = new IItemDropRule[]
			{
				bc_scarab,
				bc_bomb,
				bc_sandstormBottle,
				bc_goldCoin,
				bc_fossil,
				hardmodeBiomeCrateOres,
				hardmodeBiomeCrateBars,
				new OneFromRulesRule(4, potions)
			};
			IItemDropRule[] obsidian = new IItemDropRule[]
			{
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, bc_lava),
				bc_pot,
				bc_obsi,
				bc_wet,
				bc_plant,
				bc_hellcart,
				bc_goldCoin,
				new OneFromRulesRule(7, ores),
				new OneFromRulesRule(4, bars),
				new OneFromRulesRule(4, potions),
				bc_ornate,
				bc_cake
			};
			IItemDropRule[] hellstone = new IItemDropRule[]
			{
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, bc_lava),
				bc_pot,
				bc_obsi,
				bc_wet,
				bc_plant,
				bc_hellcart,
				bc_goldCoin,
				hardmodeBiomeCrateOres,
				hardmodeBiomeCrateBars,
				new OneFromRulesRule(4, potions),
				bc_ornate,
				bc_cake
			};
			IItemDropRule[] ocean = new IItemDropRule[]
			{
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, bc_sea),
				bc_sharkbait,
				bc_goldCoin,
				new OneFromRulesRule(7, ores),
				new OneFromRulesRule(4, bars),
				new OneFromRulesRule(4, potions),
				bc_pile,
				bc_sand
			};
			IItemDropRule[] seaside = new IItemDropRule[]
			{
				ItemDropRule.SequentialRulesNotScalingWithLuck(1, bc_sea),
				bc_sharkbait,
				bc_goldCoin,
				hardmodeBiomeCrateOres,
				hardmodeBiomeCrateBars,
				new OneFromRulesRule(4, potions),
				bc_pile,
				bc_sand
			};
			this.RegisterToItem(3208, ItemDropRule.AlwaysAtleastOneSuccess(jungle));
			this.RegisterToItem(3987, ItemDropRule.AlwaysAtleastOneSuccess(bramble));
			this.RegisterToItem(3206, ItemDropRule.AlwaysAtleastOneSuccess(sky));
			this.RegisterToItem(3985, ItemDropRule.AlwaysAtleastOneSuccess(azure));
			this.RegisterToItem(3203, ItemDropRule.AlwaysAtleastOneSuccess(corrupt));
			this.RegisterToItem(3982, ItemDropRule.AlwaysAtleastOneSuccess(defiled));
			this.RegisterToItem(3204, ItemDropRule.AlwaysAtleastOneSuccess(crimson));
			this.RegisterToItem(3983, ItemDropRule.AlwaysAtleastOneSuccess(hematic));
			this.RegisterToItem(3207, ItemDropRule.AlwaysAtleastOneSuccess(hallowed));
			this.RegisterToItem(3986, ItemDropRule.AlwaysAtleastOneSuccess(divine));
			this.RegisterToItem(3205, ItemDropRule.AlwaysAtleastOneSuccess(dungeon));
			this.RegisterToItem(3984, ItemDropRule.AlwaysAtleastOneSuccess(stockade));
			this.RegisterToItem(4405, ItemDropRule.AlwaysAtleastOneSuccess(frozen));
			this.RegisterToItem(4406, ItemDropRule.AlwaysAtleastOneSuccess(boreal));
			this.RegisterToItem(4407, ItemDropRule.AlwaysAtleastOneSuccess(oasis));
			this.RegisterToItem(4408, ItemDropRule.AlwaysAtleastOneSuccess(mirage));
			this.RegisterToItem(4877, ItemDropRule.AlwaysAtleastOneSuccess(obsidian));
			this.RegisterToItem(4878, ItemDropRule.AlwaysAtleastOneSuccess(hellstone));
			this.RegisterToItem(5002, ItemDropRule.AlwaysAtleastOneSuccess(ocean));
			this.RegisterToItem(5003, ItemDropRule.AlwaysAtleastOneSuccess(seaside));
			int[] allCrates = new int[]
			{
				3208,
				3987,
				3206,
				3985,
				3203,
				3982,
				3204,
				3983,
				3207,
				3986,
				3205,
				3984,
				4405,
				4406,
				4407,
				4408,
				4877,
				4878,
				5002,
				5003
			};
			this.RegisterToMultipleItems(new OneFromRulesRule(2, extraPotions), allCrates);
			this.RegisterToMultipleItems(ItemDropRule.SequentialRulesNotScalingWithLuck(2, extraBait), allCrates);
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x00609590 File Offset: 0x00607790
		private void RegisterObsidianLockbox()
		{
			IItemDropRule ruleFlowerOfFireUnholyTrident = ItemDropRule.ByCondition(new Conditions.NotRemixSeed(), 112, 1, 1, 1, 1);
			ruleFlowerOfFireUnholyTrident.OnFailedConditions(ItemDropRule.NotScalingWithLuck(683, 1, 1, 1), true);
			IItemDropRule[] obsidianLockBoxList = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(274, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(220, 1, 1, 1),
				ruleFlowerOfFireUnholyTrident,
				ItemDropRule.NotScalingWithLuck(218, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(3019, 1, 1, 1)
			};
			this.RegisterToItem(4879, new OneFromRulesRule(1, obsidianLockBoxList));
			this.RegisterToItem(4879, ItemDropRule.NotScalingWithLuck(5010, 5, 1, 1));
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x0060963C File Offset: 0x0060783C
		private void RegisterLockbox()
		{
			IItemDropRule ruleAquaScepterBubbleGun = ItemDropRule.ByCondition(new Conditions.NotRemixSeed(), 157, 1, 1, 1, 1);
			ruleAquaScepterBubbleGun.OnFailedConditions(ItemDropRule.NotScalingWithLuck(2623, 1, 1, 1), true);
			IItemDropRule[] goldenLockBoxList = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(3317, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(155, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(156, 1, 1, 1),
				ruleAquaScepterBubbleGun,
				ItemDropRule.NotScalingWithLuck(163, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(113, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(164, 1, 1, 1)
			};
			this.RegisterToItem(3085, new OneFromRulesRule(1, goldenLockBoxList));
			this.RegisterToItem(3085, ItemDropRule.NotScalingWithLuck(329, 3, 1, 1));
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x00609706 File Offset: 0x00607906
		private void RegisterHerbBag()
		{
			this.RegisterToItem(3093, new HerbBagDropsItemDropRule(new int[]
			{
				313,
				314,
				315,
				317,
				316,
				318,
				2358,
				307,
				308,
				309,
				311,
				310,
				312,
				2357
			}));
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x0060972C File Offset: 0x0060792C
		private void RegisterGoodieBag()
		{
			IItemDropRule[] paintings = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(1846, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(1847, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(1848, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(1849, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(1850, 1, 1, 1)
			};
			IItemDropRule catSet = ItemDropRule.NotScalingWithLuck(1749, 1, 1, 1);
			catSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1750, 1, 1, 1), false);
			catSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1751, 1, 1, 1), false);
			IItemDropRule creeperSet = ItemDropRule.NotScalingWithLuck(1746, 1, 1, 1);
			creeperSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1747, 1, 1, 1), false);
			creeperSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1748, 1, 1, 1), false);
			IItemDropRule ghostSet = ItemDropRule.NotScalingWithLuck(1752, 1, 1, 1);
			ghostSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1753, 1, 1, 1), false);
			IItemDropRule leprechaunSet = ItemDropRule.NotScalingWithLuck(1767, 1, 1, 1);
			leprechaunSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1768, 1, 1, 1), false);
			leprechaunSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1769, 1, 1, 1), false);
			IItemDropRule pixieSet = ItemDropRule.NotScalingWithLuck(1770, 1, 1, 1);
			pixieSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1771, 1, 1, 1), false);
			IItemDropRule princessSet = ItemDropRule.NotScalingWithLuck(1772, 1, 1, 1);
			princessSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1773, 1, 1, 1), false);
			IItemDropRule pumpkinSet = ItemDropRule.NotScalingWithLuck(1754, 1, 1, 1);
			pumpkinSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1755, 1, 1, 1), false);
			pumpkinSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1756, 1, 1, 1), false);
			IItemDropRule robotSet = ItemDropRule.NotScalingWithLuck(1757, 1, 1, 1);
			robotSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1758, 1, 1, 1), false);
			robotSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1759, 1, 1, 1), false);
			IItemDropRule unicornSet = ItemDropRule.NotScalingWithLuck(1760, 1, 1, 1);
			unicornSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1761, 1, 1, 1), false);
			unicornSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1762, 1, 1, 1), false);
			IItemDropRule vampireSet = ItemDropRule.NotScalingWithLuck(1763, 1, 1, 1);
			vampireSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1764, 1, 1, 1), false);
			vampireSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1765, 1, 1, 1), false);
			IItemDropRule witchSet = ItemDropRule.NotScalingWithLuck(1766, 1, 1, 1);
			witchSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1775, 1, 1, 1), false);
			witchSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1776, 1, 1, 1), false);
			IItemDropRule aintTypingThatSet = ItemDropRule.NotScalingWithLuck(1777, 1, 1, 1);
			aintTypingThatSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1778, 1, 1, 1), false);
			IItemDropRule karateTortoiseSet = ItemDropRule.NotScalingWithLuck(1779, 1, 1, 1);
			karateTortoiseSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1780, 1, 1, 1), false);
			karateTortoiseSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1781, 1, 1, 1), false);
			IItemDropRule reaperSet = ItemDropRule.NotScalingWithLuck(1819, 1, 1, 1);
			reaperSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1820, 1, 1, 1), false);
			IItemDropRule foxSet = ItemDropRule.NotScalingWithLuck(1821, 1, 1, 1);
			foxSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1822, 1, 1, 1), false);
			foxSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1823, 1, 1, 1), false);
			IItemDropRule spaceCreatureSet = ItemDropRule.NotScalingWithLuck(1838, 1, 1, 1);
			spaceCreatureSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1839, 1, 1, 1), false);
			spaceCreatureSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1840, 1, 1, 1), false);
			IItemDropRule wolfSet = ItemDropRule.NotScalingWithLuck(1841, 1, 1, 1);
			wolfSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1842, 1, 1, 1), false);
			wolfSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1843, 1, 1, 1), false);
			IItemDropRule treasureHunterSet = ItemDropRule.NotScalingWithLuck(1851, 1, 1, 1);
			treasureHunterSet.OnSuccess(ItemDropRule.NotScalingWithLuck(1852, 1, 1, 1), false);
			IItemDropRule[] vanitySets = new IItemDropRule[]
			{
				catSet,
				creeperSet,
				ghostSet,
				leprechaunSet,
				pixieSet,
				princessSet,
				pumpkinSet,
				robotSet,
				unicornSet,
				vampireSet,
				witchSet,
				aintTypingThatSet,
				karateTortoiseSet,
				reaperSet,
				foxSet,
				ItemDropRule.NotScalingWithLuck(1824, 1, 1, 1),
				spaceCreatureSet,
				wolfSet,
				treasureHunterSet
			};
			IItemDropRule[] rules = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(1810, 150, 1, 1),
				ItemDropRule.NotScalingWithLuck(1800, 150, 1, 1),
				ItemDropRule.NotScalingWithLuck(1809, 4, 10, 40),
				new OneFromRulesRule(10, paintings),
				new OneFromRulesRule(1, vanitySets)
			};
			this.RegisterToItem(1774, new SequentialRulesNotScalingWithLuckRule(1, rules));
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x00609C10 File Offset: 0x00607E10
		private void RegisterPresent()
		{
			IItemDropRule snowGlobeRule = ItemDropRule.ByCondition(new Conditions.IsHardmode(), 602, 15, 1, 1, 1);
			IItemDropRule redRyderRule = ItemDropRule.NotScalingWithLuck(1870, 150, 1, 1);
			redRyderRule.OnSuccess(ItemDropRule.NotScalingWithLuck(97, 1, 30, 60), false);
			IItemDropRule mrsClauseRule = ItemDropRule.NotScalingWithLuck(1932, 1, 1, 1);
			mrsClauseRule.OnSuccess(ItemDropRule.NotScalingWithLuck(1933, 1, 1, 1), false);
			mrsClauseRule.OnSuccess(ItemDropRule.NotScalingWithLuck(1934, 1, 1, 1), false);
			IItemDropRule parkaRule = ItemDropRule.NotScalingWithLuck(1935, 1, 1, 1);
			parkaRule.OnSuccess(ItemDropRule.NotScalingWithLuck(1936, 1, 1, 1), false);
			parkaRule.OnSuccess(ItemDropRule.NotScalingWithLuck(1937, 1, 1, 1), false);
			IItemDropRule treeRule = ItemDropRule.NotScalingWithLuck(1940, 1, 1, 1);
			treeRule.OnSuccess(ItemDropRule.NotScalingWithLuck(1941, 1, 1, 1), false);
			treeRule.OnSuccess(ItemDropRule.NotScalingWithLuck(1942, 1, 1, 1), false);
			IItemDropRule[] vanityRules = new IItemDropRule[]
			{
				mrsClauseRule,
				parkaRule,
				treeRule,
				ItemDropRule.NotScalingWithLuck(1938, 1, 1, 1),
				ItemDropRule.NotScalingWithLuck(1939, 1, 1, 1)
			};
			IItemDropRule vanityRule = new OneFromRulesRule(15, vanityRules);
			IItemDropRule foodRule = ItemDropRule.OneFromOptionsNotScalingWithLuck(7, new int[]
			{
				1911,
				1919,
				1920
			});
			IItemDropRule blockRule = new OneFromRulesRule(1, new IItemDropRule[]
			{
				ItemDropRule.Common(1872, 1, 20, 49),
				ItemDropRule.Common(586, 1, 20, 49),
				ItemDropRule.Common(591, 1, 20, 49)
			});
			IItemDropRule[] rules = new IItemDropRule[]
			{
				snowGlobeRule,
				ItemDropRule.NotScalingWithLuck(1922, 30, 1, 1),
				ItemDropRule.NotScalingWithLuck(1927, 400, 1, 1),
				redRyderRule,
				ItemDropRule.NotScalingWithLuck(1909, 150, 1, 1),
				ItemDropRule.NotScalingWithLuck(1917, 150, 1, 1),
				ItemDropRule.NotScalingWithLuck(1915, 150, 1, 1),
				ItemDropRule.NotScalingWithLuck(1918, 150, 1, 1),
				ItemDropRule.NotScalingWithLuck(1921, 150, 1, 1),
				ItemDropRule.NotScalingWithLuck(1923, 300, 1, 1),
				ItemDropRule.NotScalingWithLuck(1907, 40, 1, 1),
				ItemDropRule.NotScalingWithLuck(1908, 10, 1, 1),
				vanityRule,
				foodRule,
				ItemDropRule.NotScalingWithLuck(1912, 8, 1, 3),
				ItemDropRule.NotScalingWithLuck(1913, 9, 20, 40),
				blockRule
			};
			this.RegisterToItem(1869, new SequentialRulesNotScalingWithLuckRule(1, rules));
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x00609EC0 File Offset: 0x006080C0
		private void RegisterCanOfWorms()
		{
			this.RegisterToItem(4345, ItemDropRule.Common(2002, 1, 5, 8));
			this.RegisterToItem(4345, new CommonDrop(3191, 10, 1, 3, 3));
			this.RegisterToItem(4345, ItemDropRule.Common(2895, 20, 1, 1));
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x00609F1C File Offset: 0x0060811C
		private void RegisterOyster()
		{
			IItemDropRule[] rules = new IItemDropRule[]
			{
				ItemDropRule.NotScalingWithLuck(4414, 15, 1, 1),
				ItemDropRule.NotScalingWithLuck(4413, 3, 1, 1),
				ItemDropRule.NotScalingWithLuck(4412, 1, 1, 1)
			};
			this.RegisterToItem(4410, ItemDropRule.SequentialRulesNotScalingWithLuck(5, rules));
			this.RegisterToItem(4410, ItemDropRule.Common(4411, 1, 1, 1));
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x00609F8D File Offset: 0x0060818D
		private void RegisterCapricorns()
		{
			this.RegisterToItem(5059, ItemDropRule.Common(5060, 1, 1, 1));
			this.RegisterToItem(5060, ItemDropRule.Common(5059, 1, 1, 1));
		}

		// Token: 0x04005A6F RID: 23151
		internal List<IItemDropRule> _globalEntries = new List<IItemDropRule>();

		// Token: 0x04005A70 RID: 23152
		private Dictionary<int, List<IItemDropRule>> _entriesByNpcNetId = new Dictionary<int, List<IItemDropRule>>();

		// Token: 0x04005A71 RID: 23153
		private Dictionary<int, List<int>> _npcNetIdsByType = new Dictionary<int, List<int>>();

		// Token: 0x04005A72 RID: 23154
		private int _masterModeDropRng = 4;

		// Token: 0x04005A73 RID: 23155
		private Dictionary<int, List<IItemDropRule>> _entriesByItemId = new Dictionary<int, List<IItemDropRule>>();

		// Token: 0x04005A74 RID: 23156
		private Dictionary<int, List<int>> _itemIdsByType = new Dictionary<int, List<int>>();
	}
}
