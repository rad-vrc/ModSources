using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000674 RID: 1652
	public class BestiaryDatabaseNPCsPopulator
	{
		// Token: 0x06004795 RID: 18325 RVA: 0x0063E7A0 File Offset: 0x0063C9A0
		public static BestiaryEntry FindEntryByNPCID(int npcNetId)
		{
			return BestiaryDatabaseNPCsPopulator._currentDatabase.FindEntryByNPCID(npcNetId);
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x0063E7AD File Offset: 0x0063C9AD
		private BestiaryEntry Register(BestiaryEntry entry)
		{
			return BestiaryDatabaseNPCsPopulator._currentDatabase.Register(entry);
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x0063E7BA File Offset: 0x0063C9BA
		private IBestiaryEntryFilter Register(IBestiaryEntryFilter filter)
		{
			return BestiaryDatabaseNPCsPopulator._currentDatabase.Register(filter);
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x0063E7C7 File Offset: 0x0063C9C7
		public void Populate(BestiaryDatabase database)
		{
			BestiaryDatabaseNPCsPopulator._currentDatabase = database;
			this.AddEmptyEntries_CrittersAndEnemies_Automated();
			this.AddTownNPCs_Manual();
			this.AddNPCBiomeRelationships_Automated();
			this.AddNPCBiomeRelationships_Manual();
			this.AddNPCBiomeRelationships_AddDecorations_Automated();
			this.ModifyEntriesThatNeedIt();
			this.RegisterFilters();
			this.RegisterSortSteps();
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x0063E7FF File Offset: 0x0063C9FF
		private void RegisterTestEntries()
		{
			string nameLanguageKey = "Bestiary_Biomes.Hallow";
			string texturePath = "Images/UI/Bestiary/Biome_Hallow";
			Func<bool> unlockCondition;
			if ((unlockCondition = BestiaryDatabaseNPCsPopulator.<>O.<0>__ReachHardMode) == null)
			{
				unlockCondition = (BestiaryDatabaseNPCsPopulator.<>O.<0>__ReachHardMode = new Func<bool>(BestiaryDatabaseNPCsPopulator.Conditions.ReachHardMode));
			}
			this.Register(BestiaryEntry.Biome(nameLanguageKey, texturePath, unlockCondition));
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x0063E834 File Offset: 0x0063CA34
		private void RegisterSortSteps()
		{
			foreach (IBestiarySortStep item in new List<IBestiarySortStep>
			{
				new SortingSteps.ByUnlockState(),
				new SortingSteps.ByBestiarySortingId(),
				new SortingSteps.Alphabetical(),
				new SortingSteps.ByNetId(),
				new SortingSteps.ByAttack(),
				new SortingSteps.ByDefense(),
				new SortingSteps.ByCoins(),
				new SortingSteps.ByHP(),
				new SortingSteps.ByBestiaryRarity()
			})
			{
				BestiaryDatabaseNPCsPopulator._currentDatabase.Register(item);
			}
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x0063E8F0 File Offset: 0x0063CAF0
		private void RegisterFilters()
		{
			this.Register(new Filters.ByUnlockState());
			this.Register(new Filters.ByBoss());
			this.Register(new Filters.ByRareCreature());
			List<IBestiaryInfoElement> commonInfoElementsForFilters = BestiaryDatabaseNPCsPopulator.CommonTags.GetCommonInfoElementsForFilters();
			for (int i = 0; i < commonInfoElementsForFilters.Count; i++)
			{
				this.Register(new Filters.ByInfoElement(commonInfoElementsForFilters[i]));
			}
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x0063E94C File Offset: 0x0063CB4C
		private void ModifyEntriesThatNeedIt_NameOverride(int npcID, string newNameKey)
		{
			BestiaryEntry bestiaryEntry = BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(npcID);
			bestiaryEntry.Info.RemoveAll((IBestiaryInfoElement x) => x is NamePlateInfoElement);
			bestiaryEntry.Info.Add(new NamePlateInfoElement(newNameKey, npcID));
			bestiaryEntry.Icon = new UnlockableNPCEntryIcon(npcID, 0f, 0f, 0f, 0f, newNameKey);
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x0063E9BC File Offset: 0x0063CBBC
		private void ModifyEntriesThatNeedIt()
		{
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(258).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(-1).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(81).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(121).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(7).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(98).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(6).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(94).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(173).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(181).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(183).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(242).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(241).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(174).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(240).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(175).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(153).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(52).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(58).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(102).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(157).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(51).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(169).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(510).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(69).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(580).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(581).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(78).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(79).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptDesert)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(630).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonDesert)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(80).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.HallowDesert)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(533).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptUndergroundDesert, BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonUndergroundDesert)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(528).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(529).AddTags(new IBestiaryInfoElement[]
			{
				new BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptUndergroundDesert, BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonUndergroundDesert)
			});
			BestiaryDatabaseNPCsPopulator._currentDatabase.ApplyPass(new BestiaryDatabase.BestiaryEntriesPass(this.TryGivingEntryFlavorTextIfItIsMissing));
			BestiaryEntry bestiaryEntry = BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(398);
			bestiaryEntry.Info.Add(new MoonLordPortraitBackgroundProviderBestiaryInfoElement());
			bestiaryEntry.Info.RemoveAll((IBestiaryInfoElement x) => x is NamePlateInfoElement);
			bestiaryEntry.Info.Add(new NamePlateInfoElement("Enemies.MoonLord", 398));
			bestiaryEntry.Icon = new UnlockableNPCEntryIcon(398, 0f, 0f, 0f, 0f, "Enemies.MoonLord");
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(664).Info.RemoveAll((IBestiaryInfoElement x) => x is NPCKillCounterInfoElement);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(687).Info.RemoveAll((IBestiaryInfoElement x) => x is NPCKillCounterInfoElement);
			this.ModifyEntriesThatNeedIt_NameOverride(637, "Friends.TownCat");
			this.ModifyEntriesThatNeedIt_NameOverride(638, "Friends.TownDog");
			this.ModifyEntriesThatNeedIt_NameOverride(656, "Friends.TownBunny");
			for (int i = 494; i <= 506; i++)
			{
				BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(i).UIInfoProvider = new SalamanderShellyDadUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[i]);
			}
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(534).UIInfoProvider = new HighestOfMultipleUICollectionInfoProvider(new IBestiaryUICollectionInfoProvider[]
			{
				new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[534], false),
				new TownNPCUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[441])
			});
			foreach (NPCStatsReportInfoElement npcstatsReportInfoElement in from x in BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(13).Info
			select x as NPCStatsReportInfoElement into x
			where x != null
			select x)
			{
				npcstatsReportInfoElement.OnRefreshStats += this.AdjustEaterOfWorldStats;
			}
			foreach (NPCStatsReportInfoElement npcstatsReportInfoElement2 in from x in BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(491).Info
			select x as NPCStatsReportInfoElement into x
			where x != null
			select x)
			{
				npcstatsReportInfoElement2.OnRefreshStats += this.AdjustPirateShipStats;
			}
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(68).UIInfoProvider = new HighestOfMultipleUICollectionInfoProvider(new IBestiaryUICollectionInfoProvider[]
			{
				new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[68], true),
				new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[35], true),
				new TownNPCUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[54])
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(35).UIInfoProvider = new HighestOfMultipleUICollectionInfoProvider(new IBestiaryUICollectionInfoProvider[]
			{
				new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[35], true),
				new TownNPCUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[54])
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(37).UIInfoProvider = new HighestOfMultipleUICollectionInfoProvider(new IBestiaryUICollectionInfoProvider[]
			{
				new TownNPCUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[37]),
				new TownNPCUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[54]),
				new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[35], true)
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(565).UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[565], true);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(577).UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[577], true);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(551).UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[551], true);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(491).UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[491], true);
			foreach (KeyValuePair<int, int> item3 in new Dictionary<int, int>
			{
				{
					5,
					4
				},
				{
					267,
					266
				},
				{
					115,
					113
				},
				{
					116,
					113
				},
				{
					117,
					113
				},
				{
					139,
					134
				},
				{
					372,
					370
				},
				{
					658,
					657
				},
				{
					659,
					657
				},
				{
					660,
					657
				},
				{
					454,
					439
				},
				{
					521,
					439
				}
			})
			{
				int key = item3.Key;
				int value = item3.Value;
				BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(key).UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[value], true);
			}
			foreach (KeyValuePair<int, int[]> item4 in new Dictionary<int, int[]>
			{
				{
					443,
					new int[]
					{
						46
					}
				},
				{
					442,
					new int[]
					{
						74
					}
				},
				{
					592,
					new int[]
					{
						55
					}
				},
				{
					444,
					new int[]
					{
						356
					}
				},
				{
					601,
					new int[]
					{
						599
					}
				},
				{
					445,
					new int[]
					{
						361
					}
				},
				{
					446,
					new int[]
					{
						377
					}
				},
				{
					605,
					new int[]
					{
						604
					}
				},
				{
					447,
					new int[]
					{
						300
					}
				},
				{
					627,
					new int[]
					{
						626
					}
				},
				{
					613,
					new int[]
					{
						612
					}
				},
				{
					448,
					new int[]
					{
						357
					}
				},
				{
					539,
					new int[]
					{
						299,
						538
					}
				}
			})
			{
				BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(item4.Key).UIInfoProvider = new GoldCritterUICollectionInfoProvider(item4.Value, ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[item4.Key]);
			}
			foreach (KeyValuePair<int, int> item5 in new Dictionary<int, int>
			{
				{
					362,
					363
				},
				{
					364,
					365
				},
				{
					602,
					603
				},
				{
					608,
					609
				}
			})
			{
				BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(item5.Key).UIInfoProvider = new HighestOfMultipleUICollectionInfoProvider(new IBestiaryUICollectionInfoProvider[]
				{
					new CritterUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[item5.Key]),
					new CritterUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[item5.Value])
				});
			}
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(4).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("eoc")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(13).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("eow")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(266).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("boc")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(113).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("wof")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(50).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("slime king")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(125).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("the twins")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(126).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("the twins")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(222).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("qb")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(222).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("bee queen")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(398).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("moonlord")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(398).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("cthulhu")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(398).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("ml")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(125).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("mech boss")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(126).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("mech boss")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(127).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("mech boss")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(134).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("mech boss")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(657).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("slime queen")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(636).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("eol")
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(636).AddTags(new IBestiaryInfoElement[]
			{
				new SearchAliasInfoElement("fairy")
			});
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x0063F86C File Offset: 0x0063DA6C
		private void AdjustEaterOfWorldStats(NPCStatsReportInfoElement element)
		{
			element.LifeMax *= NPC.GetEaterOfWorldsSegmentsCountByGamemode(Main.GameMode);
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x0063F888 File Offset: 0x0063DA88
		private void AdjustPirateShipStats(NPCStatsReportInfoElement element)
		{
			NPC nPC = new NPC();
			int num = 4;
			nPC.SetDefaults(492, new NPCSpawnParams
			{
				strengthMultiplierOverride = new float?(1f),
				playerCountForMultiplayerDifficultyOverride = new int?(1),
				sizeScaleOverride = null,
				gameModeData = new GameModeData?(Main.GameModeInfo)
			});
			element.LifeMax = num * nPC.lifeMax;
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x0063F8FC File Offset: 0x0063DAFC
		private void TryGivingEntryFlavorTextIfItIsMissing(BestiaryEntry entry)
		{
			if (entry.Info.Any((IBestiaryInfoElement x) => x is FlavorTextBestiaryInfoElement))
			{
				return;
			}
			SpawnConditionBestiaryInfoElement spawnConditionBestiaryInfoElement = null;
			int? num = null;
			foreach (IBestiaryInfoElement item in entry.Info)
			{
				BestiaryPortraitBackgroundProviderPreferenceInfoElement bestiaryPortraitBackgroundProviderPreferenceInfoElement = item as BestiaryPortraitBackgroundProviderPreferenceInfoElement;
				if (bestiaryPortraitBackgroundProviderPreferenceInfoElement != null)
				{
					SpawnConditionBestiaryInfoElement spawnConditionBestiaryInfoElement2 = bestiaryPortraitBackgroundProviderPreferenceInfoElement.GetPreferredProvider() as SpawnConditionBestiaryInfoElement;
					if (spawnConditionBestiaryInfoElement2 != null)
					{
						spawnConditionBestiaryInfoElement = spawnConditionBestiaryInfoElement2;
						break;
					}
				}
				SpawnConditionBestiaryInfoElement spawnConditionBestiaryInfoElement3 = item as SpawnConditionBestiaryInfoElement;
				if (spawnConditionBestiaryInfoElement3 != null)
				{
					int displayTextPriority = spawnConditionBestiaryInfoElement3.DisplayTextPriority;
					if (num != null)
					{
						int num2 = displayTextPriority;
						int? num3 = num;
						if (!(num2 >= num3.GetValueOrDefault() & num3 != null))
						{
							continue;
						}
					}
					spawnConditionBestiaryInfoElement = spawnConditionBestiaryInfoElement3;
					num = new int?(displayTextPriority);
				}
			}
			if (spawnConditionBestiaryInfoElement != null)
			{
				string displayNameKey = spawnConditionBestiaryInfoElement.GetDisplayNameKey();
				string text = "Bestiary_BiomeText.biome_";
				string text2 = displayNameKey.Substring(displayNameKey.IndexOf('.') + 1);
				text += text2;
				entry.Info.Add(new FlavorTextBestiaryInfoElement(text));
			}
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x0063FA24 File Offset: 0x0063DC24
		private void AddTownNPCs_Manual()
		{
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x0063FA26 File Offset: 0x0063DC26
		private void AddMultiEntryNPCS_Manual()
		{
			this.Register(BestiaryEntry.Enemy(85)).Icon = new UnlockableNPCEntryIcon(85, 0f, 0f, 0f, 3f, null);
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x0063FA58 File Offset: 0x0063DC58
		private void AddEmptyEntries_CrittersAndEnemies_Automated()
		{
			HashSet<int> exclusions = BestiaryDatabaseNPCsPopulator.GetExclusions();
			foreach (KeyValuePair<int, NPC> item in ContentSamples.NpcsByNetId)
			{
				if (!exclusions.Contains(item.Key))
				{
					BestiaryEntry bestiaryEntry;
					if (item.Value.CountsAsACritter)
					{
						bestiaryEntry = this.Register(BestiaryEntry.Critter(item.Key));
					}
					else if (item.Value.isLikeATownNPC)
					{
						bestiaryEntry = this.Register(BestiaryEntry.TownNPC(item.Key));
					}
					else
					{
						bestiaryEntry = this.Register(BestiaryEntry.Enemy(item.Key));
					}
					NPCLoader.SetBestiary(item.Value, BestiaryDatabaseNPCsPopulator._currentDatabase, bestiaryEntry);
				}
			}
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x0063FB2C File Offset: 0x0063DD2C
		private static HashSet<int> GetExclusions()
		{
			HashSet<int> hashSet = new HashSet<int>();
			List<int> list = new List<int>();
			foreach (KeyValuePair<int, NPCID.Sets.NPCBestiaryDrawModifiers> item in NPCID.Sets.NPCBestiaryDrawOffset)
			{
				if (item.Value.Hide)
				{
					list.Add(item.Key);
				}
			}
			foreach (int item2 in list)
			{
				hashSet.Add(item2);
			}
			return hashSet;
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x0063FBE0 File Offset: 0x0063DDE0
		private void AddNPCBiomeRelationships_Automated()
		{
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(357).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(448).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(606).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(211).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(377).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(446).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(595).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(596).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(597).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(598).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(599).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(600).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(601).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(612).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(613).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(25).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(30).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Goblins
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(665).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(33).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(112).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(666).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(300).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(355).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(358).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(447).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(610).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(210).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(261).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(402).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.StardustPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(403).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.StardustPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(485).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(486).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(487).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(359).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(410).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.StardustPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(604).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.WindyDay
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(605).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.WindyDay
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(218).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(361).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(404).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.StardustPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(445).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(626).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(627).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(2).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(74).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(190).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(191).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(192).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(193).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(194).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(217).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(297).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(298).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(671).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(672).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(673).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(674).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(675).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(356).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(360).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(655).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(653).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(654).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(442).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(444).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(669).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(677).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(676).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(582).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(583).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(584).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(585).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(1).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(59).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(138).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(147).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(265).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(367).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(616).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(617).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(23).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Meteor
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(55).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(57).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(58).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(102).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(157).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(219).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(220).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(236).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(302).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Halloween
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(366).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(465).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(537).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(592).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(607).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(10).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(11).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(12).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(34).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(117).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(118).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(119).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(163).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SpiderNest
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(164).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SpiderNest
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(230).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(241).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(406).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.StardustPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(496).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(497).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(519).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(593).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(625).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(49).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(51).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(60).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(93).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(137).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(184).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(204).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(224).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(259).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(299).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(317).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(318).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(378).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(393).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(494).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(495).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(513).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(514).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(515).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(538).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(539).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(580).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(587).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(16).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(71).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(81).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(183).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(67).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(70).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(75).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(239).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(267).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(288).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(394).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(408).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.StardustPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(428).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.VortexPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(43).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(56).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(72).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(141).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(185).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(374).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(375).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(661).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(388).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(602).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(603).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(115).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(232).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(258).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(409).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.StardustPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(462).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(516).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(42).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(46).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(47).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(69).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(231).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(235).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(247).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(248).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(303).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Halloween,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(304).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(337).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Christmas,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(354).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SpiderNest
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(362).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(363).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(364).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(365).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(395).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(443).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(464).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(508).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(532).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(540).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Party,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(578).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(608).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(609).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(611).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(264).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(101).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(121).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(122).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(132).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(148).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(149).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(168).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(234).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(250).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(257).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(421).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.NebulaPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(470).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(472).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Goblins
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(478).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(546).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(581).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(615).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(256).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(133).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(221).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(252).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Pirates
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(329).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(385).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(427).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.VortexPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(490).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(548).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(63).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(64).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(85).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(629).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(103).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(152).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(174).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(195).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(254).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(260).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(382).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(383).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(386).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(389).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(466).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(467).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(489).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(530).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(175).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(176).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(188).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(3).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(7).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(8).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(9).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(95).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(96).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(97).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(98).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(99).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(100).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(120).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(150).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(151).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(153).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(154).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(158).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(161).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(186).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(187).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(189).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(223).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(233).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(251).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(319).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(320).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(321).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(331).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(332).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(338).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(339).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(340).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(341).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(342).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(350).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(381).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(492).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Pirates
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(510).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(511).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(512).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(552).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(553).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(554).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(590).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(82).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(116).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(166).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(199).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(263).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(371).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(461).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(463).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(523).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(52).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(200).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(244).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(255).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(384).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(387).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(390).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(418).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(420).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.NebulaPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(460).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(468).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(524).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(525).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptUndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(526).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonUndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(527).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.HallowUndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(536).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(566).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(567).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(53).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(169).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(301).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(391).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(405).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.StardustPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(423).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.NebulaPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(438).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(498).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(499).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(500).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(501).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(502).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(503).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(504).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(505).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(506).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(534).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(568).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(569).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(21).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(24).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(26).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Goblins
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(27).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Goblins
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(28).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Goblins
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(29).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Goblins
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(31).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(32).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(44).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(73).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(77).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(78).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(79).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(630).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(80).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.HallowDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(104).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(111).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Goblins
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(140).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(159).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(162).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(196).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(198).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(201).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(202).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(203).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(212).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Pirates
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(213).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Pirates
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(242).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(269).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(270).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(272).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(273).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(275).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(276).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(277).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(278).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(279).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(280).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(281).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(282).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(283).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(284).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(285).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(286).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(287).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(294).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(295).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(296).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(310).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(311).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(312).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(313).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(316).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(326).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(415).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(449).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(450).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(451).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(452).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(471).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Goblins
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(482).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Granite
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(572).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(573).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(143).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostLegion
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(144).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostLegion
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(145).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostLegion
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(155).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(271).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(274).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(314).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(352).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(379).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(509).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(555).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(556).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(557).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(61).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(110).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(206).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(214).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Pirates
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(215).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Pirates
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(216).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Pirates
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(225).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(291).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(292).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(293).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(347).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(412).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(413).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(414).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(469).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(473).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(474).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(475).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(476).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(483).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Granite
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(586).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(62).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(131).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(165).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SpiderNest
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(167).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(197).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(226).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(237).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(238).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SpiderNest
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(480).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Marble
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(528).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(529).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(289).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(439).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(440).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(533).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(170).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptIce
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(171).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.HallowIce
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(179).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(180).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonIce
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(181).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(205).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(411).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.StardustPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(424).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.NebulaPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(429).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.VortexPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(481).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Marble
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(240).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(290).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(430).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(431).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(432).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(433).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(434).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(435).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(436).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(479).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(518).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(591).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(45).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(130).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(172).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(305).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(306).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(307).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(308).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(309).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(425).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.VortexPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(426).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.VortexPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(570).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(571).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(417).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(419).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(65).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(372).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(373).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(407).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.StardustPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(542).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(543).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptDesert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(544).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonDesert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(545).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.HallowDesert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(619).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(621).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(622).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(623).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(128).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(177).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(561).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(562).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(563).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(594).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.WindyDay
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(253).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(129).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(6).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(173).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(399).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(416).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(531).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(83).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(84).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(86).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(330).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(620).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(48).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(268).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(328).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(66).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(182).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(13).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(14).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(15).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(39).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(40).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(41).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(315).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(343).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(94).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(392).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(558).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(559).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(560).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(348).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(349).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(156).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(35).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(68).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(134).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(136).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(135).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(454).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(455).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(456).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(457).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(458).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(459).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(113).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(114).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(564).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(565).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(327).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(520).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(574).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(575).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(246).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(50).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(477).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(541).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(109).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(243).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Blizzard
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(618).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(351).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(249).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(222).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(262).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(87).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(88).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(89).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(90).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(91).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(92).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(127).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(346).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(370).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(4).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(551).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(245).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(576).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(577).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(266).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(325).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(344).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(125).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(126).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(549).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(345).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(668).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Blizzard
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(422).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.VortexPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(493).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.StardustPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(507).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.NebulaPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(517).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(491).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Pirates
			});
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x00644A5C File Offset: 0x00642C5C
		private void AddNPCBiomeRelationships_Manual()
		{
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(628).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.WindyDay
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(-4).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(-3).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(-7).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(1).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(-10).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(-8).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(-9).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(-6).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(-5).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(-2).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(-1).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(81).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(121).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(7).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(8).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(9).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(98).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(99).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(100).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(6).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(94).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(173).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(181).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(183).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(242).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(241).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(174).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(240).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(175).Info.Remove(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(175).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(153).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(52).Info.Remove(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(52).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(58).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(102).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(157).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(51).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(169).Info.Remove(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(169).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(510).Info.Remove(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(510).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(511).Info.Remove(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(511).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(512).Info.Remove(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(512).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(69).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(580).Info.Remove(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(580).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(581).Info.Remove(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(581).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(78).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(79).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptUndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(630).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonUndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(80).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.HallowUndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(533).Info.Remove(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(533).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptUndergroundDesert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonUndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(528).Info.Remove(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(528).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.HallowUndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(529).Info.Remove(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert);
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(529).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptUndergroundDesert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonUndergroundDesert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(624).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(5).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(139).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(484).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(317).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Halloween
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(318).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Halloween
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(320).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Halloween
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(321).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Halloween
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(319).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Halloween
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(324).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Halloween,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(322).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Halloween,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(323).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Halloween,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(302).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(521).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(332).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Christmas
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(331).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Christmas
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(335).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Christmas,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(336).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Christmas,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(333).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Christmas,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(334).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Christmas,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(535).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(614).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(225).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(224).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(250).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(632).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(631).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(634).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(635).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(636).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(639).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(640).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(641).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(642).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(643).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(644).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(645).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(646).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(647).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(648).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(649).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(650).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(651).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(652).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(657).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(658).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(660).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(659).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(22).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(17).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(588).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(441).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(124).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(209).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(142).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Christmas
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(207).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(19).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(178).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(20).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(228).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(227).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(369).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(229).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(353).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(38).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(107).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(54).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(108).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(18).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(208).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(550).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(633).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(663).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(160).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(637).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(638).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(656).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(670).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(678).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(679).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(680).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(681).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(682).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(683).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(684).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(687).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(368).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(37).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(453).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground
			});
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(664).Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns
			});
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x00645ECC File Offset: 0x006440CC
		private void AddNPCBiomeRelationships_AddDecorations_Automated()
		{
			foreach (KeyValuePair<int, NPC> item in ContentSamples.NpcsByNetId)
			{
				BestiaryEntry bestiaryEntry = BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(item.Key);
				if (bestiaryEntry.Info.Contains(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain))
				{
					if (bestiaryEntry.Info.Contains(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow))
					{
						bestiaryEntry.AddTags(new IBestiaryInfoElement[]
						{
							BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Blizzard
						});
					}
					else
					{
						bestiaryEntry.AddTags(new IBestiaryInfoElement[]
						{
							BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Rain
						});
					}
				}
				else
				{
					if (bestiaryEntry.Info.Contains(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse))
					{
						bestiaryEntry.AddTags(new IBestiaryInfoElement[]
						{
							BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.EclipseSun
						});
					}
					if (bestiaryEntry.Info.Contains(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime))
					{
						bestiaryEntry.AddTags(new IBestiaryInfoElement[]
						{
							BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Moon
						});
					}
					if (bestiaryEntry.Info.Contains(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime))
					{
						bestiaryEntry.AddTags(new IBestiaryInfoElement[]
						{
							BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Sun
						});
					}
					if (bestiaryEntry.Info.Contains(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon))
					{
						bestiaryEntry.AddTags(new IBestiaryInfoElement[]
						{
							BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.PumpkinMoon
						});
					}
					if (bestiaryEntry.Info.Contains(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon))
					{
						bestiaryEntry.AddTags(new IBestiaryInfoElement[]
						{
							BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.FrostMoon
						});
					}
					if (bestiaryEntry.Info.Contains(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Meteor))
					{
						bestiaryEntry.AddTags(new IBestiaryInfoElement[]
						{
							BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Moon
						});
						bestiaryEntry.AddTags(new IBestiaryInfoElement[]
						{
							BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Meteor
						});
					}
				}
			}
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x00646084 File Offset: 0x00644284
		public void AddDropOverrides(BestiaryDatabase bestiaryDatabase)
		{
			BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(121).Info.AddRange(BestiaryDatabaseNPCsPopulator.FindEntryByNPCID(81).Info.OfType<ItemDropBestiaryInfoElement>());
		}

		// Token: 0x04005BE3 RID: 23523
		private static BestiaryDatabase _currentDatabase;

		// Token: 0x02000D20 RID: 3360
		public static class CommonTags
		{
			// Token: 0x06006323 RID: 25379 RVA: 0x006D8044 File Offset: 0x006D6244
			public static List<IBestiaryInfoElement> GetCommonInfoElementsForFilters()
			{
				List<IBestiaryInfoElement> list = new List<IBestiaryInfoElement>();
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Party);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.WindyDay);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Granite);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Marble);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SpiderNest);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Meteor);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptIce);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptDesert);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptUndergroundDesert);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonIce);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonDesert);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonUndergroundDesert);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundHallow);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.HallowIce);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.HallowDesert);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.HallowUndergroundDesert);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Goblins);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.OldOnesArmy);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Pirates);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.Martian);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Eclipse);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.PumpkinMoon);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostMoon);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Halloween);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Christmas);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions.FrostLegion);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.NebulaPillar);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SolarPillar);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.VortexPillar);
				list.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.StardustPillar);
				return list.Concat(from mod in ModLoader.Mods
				where NPCLoader.npcs.Any(delegate(ModNPC npc)
				{
					NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers;
					return npc.Mod == mod && (!NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(npc.Type, out drawModifiers) || !drawModifiers.Hide);
				})
				select mod.ModSourceBestiaryInfoElement).Concat(from biome in LoaderManager.Get<BiomeLoader>().list
				where NPCLoader.npcs.Any((ModNPC npc) => npc.SpawnModBiomes.Contains(biome.Type))
				select biome.ModBiomeBestiaryInfoElement).ToList<IBestiaryInfoElement>();
			}

			// Token: 0x02000E62 RID: 3682
			public static class SpawnConditions
			{
				// Token: 0x02000E94 RID: 3732
				public static class Invasions
				{
					// Token: 0x04007DE7 RID: 32231
					public static SpawnConditionBestiaryInfoElement Goblins = new SpawnConditionBestiaryInfoElement("Bestiary_Invasions.Goblins", 49, "Images/MapBG1", null);

					// Token: 0x04007DE8 RID: 32232
					public static SpawnConditionBestiaryInfoElement Pirates = new SpawnConditionBestiaryInfoElement("Bestiary_Invasions.Pirates", 50, "Images/MapBG11", null);

					// Token: 0x04007DE9 RID: 32233
					public static SpawnConditionBestiaryInfoElement Martian = new SpawnConditionBestiaryInfoElement("Bestiary_Invasions.Martian", 53, "Images/MapBG1", new Color?(new Color(35, 40, 40)));

					// Token: 0x04007DEA RID: 32234
					public static SpawnConditionBestiaryInfoElement OldOnesArmy = new SpawnConditionBestiaryInfoElement("Bestiary_Invasions.OldOnesArmy", 55, "Images/MapBG1", null);

					// Token: 0x04007DEB RID: 32235
					public static SpawnConditionBestiaryInfoElement PumpkinMoon = new SpawnConditionBestiaryInfoElement("Bestiary_Invasions.PumpkinMoon", 51, "Images/MapBG1", new Color?(new Color(35, 40, 40)));

					// Token: 0x04007DEC RID: 32236
					public static SpawnConditionBestiaryInfoElement FrostMoon = new SpawnConditionBestiaryInfoElement("Bestiary_Invasions.FrostMoon", 52, "Images/MapBG12", new Color?(new Color(35, 40, 40)));

					// Token: 0x04007DED RID: 32237
					public static SpawnConditionBestiaryInfoElement FrostLegion = new SpawnConditionBestiaryInfoElement("Bestiary_Invasions.FrostLegion", 54, "Images/MapBG12", null);
				}

				// Token: 0x02000E95 RID: 3733
				public static class Events
				{
					// Token: 0x04007DEE RID: 32238
					public static SpawnConditionBestiaryInfoElement SlimeRain = new SpawnConditionBestiaryInfoElement("Bestiary_Events.SlimeRain", 47, "Images/MapBG1", null)
					{
						DisplayTextPriority = 1,
						OrderPriority = -2f
					};

					// Token: 0x04007DEF RID: 32239
					public static SpawnConditionBestiaryInfoElement WindyDay = new SpawnConditionBestiaryInfoElement("Bestiary_Events.WindyDay", 41, "Images/MapBG1", null)
					{
						DisplayTextPriority = 1,
						OrderPriority = -2f
					};

					// Token: 0x04007DF0 RID: 32240
					public static SpawnConditionBestiaryInfoElement BloodMoon = new SpawnConditionBestiaryInfoElement("Bestiary_Events.BloodMoon", 38, "Images/MapBG26", new Color?(new Color(200, 190, 180)))
					{
						DisplayTextPriority = 1,
						OrderPriority = -2f
					};

					// Token: 0x04007DF1 RID: 32241
					public static SpawnConditionBestiaryInfoElement Halloween = new SpawnConditionBestiaryInfoElement("Bestiary_Events.Halloween", 45, "Images/MapBG1", null)
					{
						DisplayTextPriority = 1,
						OrderPriority = -2f
					};

					// Token: 0x04007DF2 RID: 32242
					public static SpawnConditionBestiaryOverlayInfoElement Rain = new SpawnConditionBestiaryOverlayInfoElement("Bestiary_Events.Rain", 40, null, null)
					{
						DisplayTextPriority = 1,
						OrderPriority = -2f
					};

					// Token: 0x04007DF3 RID: 32243
					public static SpawnConditionBestiaryInfoElement Christmas = new SpawnConditionBestiaryInfoElement("Bestiary_Events.Christmas", 46, "Images/MapBG12", null)
					{
						DisplayTextPriority = 1,
						OrderPriority = -2f
					};

					// Token: 0x04007DF4 RID: 32244
					public static SpawnConditionBestiaryInfoElement Eclipse = new SpawnConditionBestiaryInfoElement("Bestiary_Events.Eclipse", 39, "Images/MapBG1", new Color?(new Color(60, 30, 0)))
					{
						DisplayTextPriority = 1,
						OrderPriority = -2f
					};

					// Token: 0x04007DF5 RID: 32245
					public static SpawnConditionBestiaryInfoElement Party = new SpawnConditionBestiaryInfoElement("Bestiary_Events.Party", 48, "Images/MapBG1", null)
					{
						DisplayTextPriority = 1,
						OrderPriority = -2f
					};

					// Token: 0x04007DF6 RID: 32246
					public static SpawnConditionBestiaryOverlayInfoElement Blizzard = new SpawnConditionBestiaryOverlayInfoElement("Bestiary_Events.Blizzard", 42, null, null)
					{
						DisplayTextPriority = 1,
						HideInPortraitInfo = true,
						OrderPriority = -2f
					};

					// Token: 0x04007DF7 RID: 32247
					public static SpawnConditionBestiaryOverlayInfoElement Sandstorm = new SpawnConditionBestiaryOverlayInfoElement("Bestiary_Events.Sandstorm", 43, "Images/MapBGOverlay1", new Color?(Color.White))
					{
						DisplayTextPriority = 1,
						OrderPriority = -2f
					};
				}

				// Token: 0x02000E96 RID: 3734
				public static class Biomes
				{
					// Token: 0x04007DF8 RID: 32248
					public static SpawnConditionBestiaryInfoElement TheCorruption = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.TheCorruption", 7, "Images/MapBG6", new Color?(new Color(200, 200, 200)));

					// Token: 0x04007DF9 RID: 32249
					public static SpawnConditionBestiaryInfoElement TheCrimson = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Crimson", 12, "Images/MapBG7", new Color?(new Color(200, 200, 200)));

					// Token: 0x04007DFA RID: 32250
					public static SpawnConditionBestiaryInfoElement Surface = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Surface", 0, "Images/MapBG1", null);

					// Token: 0x04007DFB RID: 32251
					public static SpawnConditionBestiaryInfoElement Graveyard = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Graveyard", 35, "Images/MapBG27", null);

					// Token: 0x04007DFC RID: 32252
					public static SpawnConditionBestiaryInfoElement UndergroundJungle = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.UndergroundJungle", 23, "Images/MapBG13", null);

					// Token: 0x04007DFD RID: 32253
					public static SpawnConditionBestiaryInfoElement TheUnderworld = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.TheUnderworld", 33, "Images/MapBG3", null);

					// Token: 0x04007DFE RID: 32254
					public static SpawnConditionBestiaryInfoElement TheDungeon = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.TheDungeon", 32, "Images/MapBG5", null);

					// Token: 0x04007DFF RID: 32255
					public static SpawnConditionBestiaryInfoElement Underground = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Underground", 1, "Images/MapBG2", null);

					// Token: 0x04007E00 RID: 32256
					public static SpawnConditionBestiaryInfoElement TheHallow = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.TheHallow", 17, "Images/MapBG8", null);

					// Token: 0x04007E01 RID: 32257
					public static SpawnConditionBestiaryInfoElement UndergroundMushroom = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.UndergroundMushroom", 25, "Images/MapBG21", null);

					// Token: 0x04007E02 RID: 32258
					public static SpawnConditionBestiaryInfoElement Jungle = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Jungle", 22, "Images/MapBG9", null);

					// Token: 0x04007E03 RID: 32259
					public static SpawnConditionBestiaryInfoElement Caverns = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Caverns", 2, "Images/MapBG32", null);

					// Token: 0x04007E04 RID: 32260
					public static SpawnConditionBestiaryInfoElement UndergroundSnow = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.UndergroundSnow", 6, "Images/MapBG4", null);

					// Token: 0x04007E05 RID: 32261
					public static SpawnConditionBestiaryInfoElement Ocean = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Ocean", 28, "Images/MapBG11", null);

					// Token: 0x04007E06 RID: 32262
					public static SpawnConditionBestiaryInfoElement SurfaceMushroom = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.SurfaceMushroom", 24, "Images/MapBG20", null);

					// Token: 0x04007E07 RID: 32263
					public static SpawnConditionBestiaryInfoElement UndergroundDesert = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.UndergroundDesert", 4, "Images/MapBG15", null);

					// Token: 0x04007E08 RID: 32264
					public static SpawnConditionBestiaryInfoElement Snow = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Snow", 5, "Images/MapBG12", null);

					// Token: 0x04007E09 RID: 32265
					public static SpawnConditionBestiaryInfoElement Desert = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Desert", 3, "Images/MapBG10", null);

					// Token: 0x04007E0A RID: 32266
					public static SpawnConditionBestiaryInfoElement Meteor = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Meteor", 44, "Images/MapBG1", new Color?(new Color(35, 40, 40)));

					// Token: 0x04007E0B RID: 32267
					public static SpawnConditionBestiaryInfoElement Oasis = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Oasis", 27, "Images/MapBG10", null);

					// Token: 0x04007E0C RID: 32268
					public static SpawnConditionBestiaryInfoElement SpiderNest = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.SpiderNest", 34, "Images/MapBG19", null);

					// Token: 0x04007E0D RID: 32269
					public static SpawnConditionBestiaryInfoElement TheTemple = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.TheTemple", 31, "Images/MapBG14", null);

					// Token: 0x04007E0E RID: 32270
					public static SpawnConditionBestiaryInfoElement CorruptUndergroundDesert = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.CorruptUndergroundDesert", 10, "Images/MapBG40", null);

					// Token: 0x04007E0F RID: 32271
					public static SpawnConditionBestiaryInfoElement CrimsonUndergroundDesert = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.CrimsonUndergroundDesert", 15, "Images/MapBG41", null);

					// Token: 0x04007E10 RID: 32272
					public static SpawnConditionBestiaryInfoElement HallowUndergroundDesert = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.HallowUndergroundDesert", 20, "Images/MapBG42", null);

					// Token: 0x04007E11 RID: 32273
					public static SpawnConditionBestiaryInfoElement CorruptDesert = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.CorruptDesert", 9, "Images/MapBG37", null);

					// Token: 0x04007E12 RID: 32274
					public static SpawnConditionBestiaryInfoElement CrimsonDesert = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.CrimsonDesert", 14, "Images/MapBG38", null);

					// Token: 0x04007E13 RID: 32275
					public static SpawnConditionBestiaryInfoElement HallowDesert = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.HallowDesert", 19, "Images/MapBG39", null);

					// Token: 0x04007E14 RID: 32276
					public static SpawnConditionBestiaryInfoElement Granite = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Granite", 30, "Images/MapBG17", new Color?(new Color(100, 100, 100)));

					// Token: 0x04007E15 RID: 32277
					public static SpawnConditionBestiaryInfoElement UndergroundCorruption = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.UndergroundCorruption", 8, "Images/MapBG23", null);

					// Token: 0x04007E16 RID: 32278
					public static SpawnConditionBestiaryInfoElement UndergroundCrimson = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.UndergroundCrimson", 13, "Images/MapBG24", null);

					// Token: 0x04007E17 RID: 32279
					public static SpawnConditionBestiaryInfoElement UndergroundHallow = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.UndergroundHallow", 18, "Images/MapBG22", null);

					// Token: 0x04007E18 RID: 32280
					public static SpawnConditionBestiaryInfoElement Marble = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Marble", 29, "Images/MapBG18", null);

					// Token: 0x04007E19 RID: 32281
					public static SpawnConditionBestiaryInfoElement CorruptIce = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.CorruptIce", 11, "Images/MapBG34", new Color?(new Color(200, 200, 200)));

					// Token: 0x04007E1A RID: 32282
					public static SpawnConditionBestiaryInfoElement HallowIce = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.HallowIce", 21, "Images/MapBG36", new Color?(new Color(200, 200, 200)));

					// Token: 0x04007E1B RID: 32283
					public static SpawnConditionBestiaryInfoElement CrimsonIce = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.CrimsonIce", 16, "Images/MapBG35", new Color?(new Color(200, 200, 200)));

					// Token: 0x04007E1C RID: 32284
					public static SpawnConditionBestiaryInfoElement Sky = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.Sky", 26, "Images/MapBG33", null);

					// Token: 0x04007E1D RID: 32285
					public static SpawnConditionBestiaryInfoElement NebulaPillar = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.NebulaPillar", 58, "Images/MapBG28", null);

					// Token: 0x04007E1E RID: 32286
					public static SpawnConditionBestiaryInfoElement SolarPillar = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.SolarPillar", 56, "Images/MapBG29", null);

					// Token: 0x04007E1F RID: 32287
					public static SpawnConditionBestiaryInfoElement VortexPillar = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.VortexPillar", 57, "Images/MapBG30", null);

					// Token: 0x04007E20 RID: 32288
					public static SpawnConditionBestiaryInfoElement StardustPillar = new SpawnConditionBestiaryInfoElement("Bestiary_Biomes.StardustPillar", 59, "Images/MapBG31", null);
				}

				// Token: 0x02000E97 RID: 3735
				public static class Times
				{
					// Token: 0x04007E21 RID: 32289
					public static SpawnConditionBestiaryInfoElement DayTime = new SpawnConditionBestiaryInfoElement("Bestiary_Times.DayTime", 36, null, null)
					{
						DisplayTextPriority = -1,
						OrderPriority = -1f
					};

					// Token: 0x04007E22 RID: 32290
					public static SpawnConditionBestiaryInfoElement NightTime = new SpawnConditionBestiaryInfoElement("Bestiary_Times.NightTime", 37, "Images/MapBG1", new Color?(new Color(35, 40, 40)))
					{
						DisplayTextPriority = -1,
						OrderPriority = -1f
					};
				}

				// Token: 0x02000E98 RID: 3736
				public static class Visuals
				{
					// Token: 0x04007E23 RID: 32291
					public static SpawnConditionDecorativeOverlayInfoElement Sun = new SpawnConditionDecorativeOverlayInfoElement("Images/MapBGOverlay3", new Color?(Color.White))
					{
						DisplayPriority = 1f
					};

					// Token: 0x04007E24 RID: 32292
					public static SpawnConditionDecorativeOverlayInfoElement Moon = new SpawnConditionDecorativeOverlayInfoElement("Images/MapBGOverlay4", new Color?(Color.White))
					{
						DisplayPriority = 1f
					};

					// Token: 0x04007E25 RID: 32293
					public static SpawnConditionDecorativeOverlayInfoElement EclipseSun = new SpawnConditionDecorativeOverlayInfoElement("Images/MapBGOverlay5", new Color?(Color.White))
					{
						DisplayPriority = 1f
					};

					// Token: 0x04007E26 RID: 32294
					public static SpawnConditionDecorativeOverlayInfoElement PumpkinMoon = new SpawnConditionDecorativeOverlayInfoElement("Images/MapBGOverlay8", new Color?(Color.White))
					{
						DisplayPriority = 1f
					};

					// Token: 0x04007E27 RID: 32295
					public static SpawnConditionDecorativeOverlayInfoElement FrostMoon = new SpawnConditionDecorativeOverlayInfoElement("Images/MapBGOverlay9", new Color?(Color.White))
					{
						DisplayPriority = 1f
					};

					// Token: 0x04007E28 RID: 32296
					public static SpawnConditionDecorativeOverlayInfoElement Meteor = new SpawnConditionDecorativeOverlayInfoElement("Images/MapBGOverlay7", new Color?(Color.White))
					{
						DisplayPriority = 1f
					};

					// Token: 0x04007E29 RID: 32297
					public static SpawnConditionDecorativeOverlayInfoElement Rain = new SpawnConditionDecorativeOverlayInfoElement("Images/MapBGOverlay2", new Color?(new Color(200, 200, 200)))
					{
						DisplayPriority = 1f
					};

					// Token: 0x04007E2A RID: 32298
					public static SpawnConditionDecorativeOverlayInfoElement Blizzard = new SpawnConditionDecorativeOverlayInfoElement("Images/MapBGOverlay6", new Color?(Color.White))
					{
						DisplayPriority = 1f
					};
				}
			}
		}

		// Token: 0x02000D21 RID: 3361
		public static class Conditions
		{
			// Token: 0x06006324 RID: 25380 RVA: 0x006D8377 File Offset: 0x006D6577
			public static bool ReachHardMode()
			{
				return Main.hardMode;
			}
		}

		// Token: 0x02000D22 RID: 3362
		public static class CrownosIconIndexes
		{
			// Token: 0x04007ADA RID: 31450
			public const int Surface = 0;

			// Token: 0x04007ADB RID: 31451
			public const int Underground = 1;

			// Token: 0x04007ADC RID: 31452
			public const int Cave = 2;

			// Token: 0x04007ADD RID: 31453
			public const int Desert = 3;

			// Token: 0x04007ADE RID: 31454
			public const int UndergroundDesert = 4;

			// Token: 0x04007ADF RID: 31455
			public const int Snow = 5;

			// Token: 0x04007AE0 RID: 31456
			public const int UndergroundIce = 6;

			// Token: 0x04007AE1 RID: 31457
			public const int Corruption = 7;

			// Token: 0x04007AE2 RID: 31458
			public const int CorruptionUnderground = 8;

			// Token: 0x04007AE3 RID: 31459
			public const int CorruptionDesert = 9;

			// Token: 0x04007AE4 RID: 31460
			public const int CorruptionUndergroundDesert = 10;

			// Token: 0x04007AE5 RID: 31461
			public const int CorruptionIce = 11;

			// Token: 0x04007AE6 RID: 31462
			public const int Crimson = 12;

			// Token: 0x04007AE7 RID: 31463
			public const int CrimsonUnderground = 13;

			// Token: 0x04007AE8 RID: 31464
			public const int CrimsonDesert = 14;

			// Token: 0x04007AE9 RID: 31465
			public const int CrimsonUndergroundDesert = 15;

			// Token: 0x04007AEA RID: 31466
			public const int CrimsonIce = 16;

			// Token: 0x04007AEB RID: 31467
			public const int Hallow = 17;

			// Token: 0x04007AEC RID: 31468
			public const int HallowUnderground = 18;

			// Token: 0x04007AED RID: 31469
			public const int HallowDesert = 19;

			// Token: 0x04007AEE RID: 31470
			public const int HallowUndergroundDesert = 20;

			// Token: 0x04007AEF RID: 31471
			public const int HallowIce = 21;

			// Token: 0x04007AF0 RID: 31472
			public const int Jungle = 22;

			// Token: 0x04007AF1 RID: 31473
			public const int UndergroundJungle = 23;

			// Token: 0x04007AF2 RID: 31474
			public const int SurfaceMushroom = 24;

			// Token: 0x04007AF3 RID: 31475
			public const int UndergroundMushroom = 25;

			// Token: 0x04007AF4 RID: 31476
			public const int Sky = 26;

			// Token: 0x04007AF5 RID: 31477
			public const int Oasis = 27;

			// Token: 0x04007AF6 RID: 31478
			public const int Ocean = 28;

			// Token: 0x04007AF7 RID: 31479
			public const int Marble = 29;

			// Token: 0x04007AF8 RID: 31480
			public const int Granite = 30;

			// Token: 0x04007AF9 RID: 31481
			public const int JungleTemple = 31;

			// Token: 0x04007AFA RID: 31482
			public const int Dungeon = 32;

			// Token: 0x04007AFB RID: 31483
			public const int Underworld = 33;

			// Token: 0x04007AFC RID: 31484
			public const int SpiderNest = 34;

			// Token: 0x04007AFD RID: 31485
			public const int Graveyard = 35;

			// Token: 0x04007AFE RID: 31486
			public const int Day = 36;

			// Token: 0x04007AFF RID: 31487
			public const int Night = 37;

			// Token: 0x04007B00 RID: 31488
			public const int BloodMoon = 38;

			// Token: 0x04007B01 RID: 31489
			public const int Eclipse = 39;

			// Token: 0x04007B02 RID: 31490
			public const int Rain = 40;

			// Token: 0x04007B03 RID: 31491
			public const int WindyDay = 41;

			// Token: 0x04007B04 RID: 31492
			public const int Blizzard = 42;

			// Token: 0x04007B05 RID: 31493
			public const int Sandstorm = 43;

			// Token: 0x04007B06 RID: 31494
			public const int Meteor = 44;

			// Token: 0x04007B07 RID: 31495
			public const int Halloween = 45;

			// Token: 0x04007B08 RID: 31496
			public const int Christmas = 46;

			// Token: 0x04007B09 RID: 31497
			public const int SlimeRain = 47;

			// Token: 0x04007B0A RID: 31498
			public const int Party = 48;

			// Token: 0x04007B0B RID: 31499
			public const int GoblinInvasion = 49;

			// Token: 0x04007B0C RID: 31500
			public const int PirateInvasion = 50;

			// Token: 0x04007B0D RID: 31501
			public const int PumpkinMoon = 51;

			// Token: 0x04007B0E RID: 31502
			public const int FrostMoon = 52;

			// Token: 0x04007B0F RID: 31503
			public const int AlienInvasion = 53;

			// Token: 0x04007B10 RID: 31504
			public const int FrostLegion = 54;

			// Token: 0x04007B11 RID: 31505
			public const int OldOnesArmy = 55;

			// Token: 0x04007B12 RID: 31506
			public const int SolarTower = 56;

			// Token: 0x04007B13 RID: 31507
			public const int VortexTower = 57;

			// Token: 0x04007B14 RID: 31508
			public const int NebulaTower = 58;

			// Token: 0x04007B15 RID: 31509
			public const int StardustTower = 59;

			// Token: 0x04007B16 RID: 31510
			public const int Hardmode = 60;

			// Token: 0x04007B17 RID: 31511
			public const int ItemSpawn = 61;
		}

		// Token: 0x02000D23 RID: 3363
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007B18 RID: 31512
			public static Func<bool> <0>__ReachHardMode;
		}
	}
}
