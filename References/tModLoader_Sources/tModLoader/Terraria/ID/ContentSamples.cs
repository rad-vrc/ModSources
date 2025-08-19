﻿using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Terraria.ID
{
	/// <summary>
	/// This class stores instances of various content types in a number of dictionaries. These instances serve as references that can be queried for information. For example, a modder could check ItemsByType[ItemID.Gel].value to check the value of the Gel item rather than creating a new Item instance and then checking. Care must be taken by modders to not use these instances for anything other than as a reference. Modders should not edit the instances in any way. <br />
	/// ContentSamples should only be accessed in code that runs during <see cref="M:Terraria.ModLoader.Mod.PostSetupContent" /> or later, as the entries are not filled in until then. <br />
	/// </summary>
	// Token: 0x020003FE RID: 1022
	public static class ContentSamples
	{
		// Token: 0x0600350F RID: 13583 RVA: 0x0056C444 File Offset: 0x0056A644
		public static void Initialize()
		{
			if (Main.rand == null)
			{
				Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
			}
			ContentSamples.NpcsByNetId.Clear();
			ContentSamples.NpcNetIdsByPersistentIds.Clear();
			ContentSamples.NpcPersistentIdsByNetIds.Clear();
			ContentSamples.NpcBestiarySortingId.Clear();
			for (int i = -65; i < NPCLoader.NPCCount; i++)
			{
				NPC nPC = new NPC();
				nPC.SetDefaults(i, default(NPCSpawnParams));
				ContentSamples.NpcsByNetId[i] = nPC;
				string name = NPCID.Search.GetName(nPC.netID);
				ContentSamples.NpcPersistentIdsByNetIds[i] = name;
				ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[i] = name;
				ContentSamples.NpcNetIdsByPersistentIds[name] = i;
			}
			ContentSamples.ModifyNPCIds();
			ContentSamples.ProjectilesByType.Clear();
			for (int j = 0; j < ProjectileLoader.ProjectileCount; j++)
			{
				Projectile projectile = new Projectile();
				projectile.SetDefaults(j);
				ContentSamples.ProjectilesByType[j] = projectile;
			}
			ContentSamples.ItemsByType.Clear();
			for (int k = 0; k < ItemLoader.ItemCount; k++)
			{
				Item item = new Item();
				item.SetDefaults(k);
				ContentSamples.ItemsByType[k] = item;
				string name2 = ItemID.Search.GetName(item.netID);
				ContentSamples.ItemPersistentIdsByNetIds[k] = name2;
				ContentSamples.ItemNetIdsByPersistentIds[name2] = k;
			}
			foreach (int item2 in ItemID.Sets.ItemsThatAreProcessedAfterNormalContentSample)
			{
				Item item3 = new Item();
				item3.SetDefaults(item2);
				ContentSamples.ItemsByType[item2] = item3;
				string name3 = ItemID.Search.GetName(item3.netID);
				ContentSamples.ItemPersistentIdsByNetIds[item2] = name3;
				ContentSamples.ItemNetIdsByPersistentIds[name3] = item2;
			}
			ContentSamples.FillResearchItemOverrides();
			ContentSamples.FillNpcRarities();
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x0056C648 File Offset: 0x0056A848
		private static void FillResearchItemOverrides()
		{
			ContentSamples.AddItemResearchOverride(4131, new int[]
			{
				5325
			});
			ContentSamples.AddItemResearchOverride(5324, new int[]
			{
				5329,
				5330
			});
			ContentSamples.AddItemResearchOverride(5437, new int[]
			{
				5358,
				5359,
				5360,
				5361
			});
			ContentSamples.AddItemResearchOverride(4346, new int[]
			{
				5391
			});
			ContentSamples.AddItemResearchOverride(4767, new int[]
			{
				5453
			});
			ContentSamples.AddItemResearchOverride(5309, new int[]
			{
				5454
			});
			ContentSamples.AddItemResearchOverride(5323, new int[]
			{
				5455
			});
		}

		/// <summary>
		/// Registers any number of item types (<see cref="F:Terraria.Item.type" />) with the item type it shares research values with.
		/// </summary>
		// Token: 0x06003511 RID: 13585 RVA: 0x0056C708 File Offset: 0x0056A908
		public static void AddItemResearchOverride(int itemTypeToUnlock, params int[] itemsThatWillResearchTheItemToUnlock)
		{
			for (int i = 0; i < itemsThatWillResearchTheItemToUnlock.Length; i++)
			{
				ContentSamples.AddItemResearchOverride_Inner(itemsThatWillResearchTheItemToUnlock[i], itemTypeToUnlock);
			}
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x0056C72C File Offset: 0x0056A92C
		private static void AddItemResearchOverride_Inner(int itemTypeToSacrifice, int itemTypeToUnlock)
		{
			ContentSamples.CreativeResearchItemPersistentIdOverride[itemTypeToSacrifice] = itemTypeToUnlock;
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x0056C73C File Offset: 0x0056A93C
		public static void FixItemsAfterRecipesAreAdded()
		{
			foreach (KeyValuePair<int, Item> item in ContentSamples.ItemsByType)
			{
				item.Value.material = ItemID.Sets.IsAMaterial[item.Key];
			}
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x0056C7A0 File Offset: 0x0056A9A0
		public static void RebuildBestiarySortingIDsByBestiaryDatabaseContents(BestiaryDatabase database)
		{
			ContentSamples.NpcBestiarySortingId.Clear();
			ContentSamples.CreateBestiarySortingIds(database);
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x0056C7B2 File Offset: 0x0056A9B2
		public static void RebuildItemCreativeSortingIDsAfterRecipesAreSetUp()
		{
			ContentSamples.ItemCreativeSortingId.Clear();
			ContentSamples.CreateCreativeItemSortingIds();
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x0056C7C4 File Offset: 0x0056A9C4
		private static void ModifyNPCIds()
		{
			Dictionary<int, string> npcBestiaryCreditIdsByNpcNetIds = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds;
			npcBestiaryCreditIdsByNpcNetIds[-65] = npcBestiaryCreditIdsByNpcNetIds[42];
			npcBestiaryCreditIdsByNpcNetIds[-64] = npcBestiaryCreditIdsByNpcNetIds[42];
			npcBestiaryCreditIdsByNpcNetIds[-63] = npcBestiaryCreditIdsByNpcNetIds[42];
			npcBestiaryCreditIdsByNpcNetIds[-62] = npcBestiaryCreditIdsByNpcNetIds[42];
			npcBestiaryCreditIdsByNpcNetIds[-61] = npcBestiaryCreditIdsByNpcNetIds[42];
			npcBestiaryCreditIdsByNpcNetIds[-60] = npcBestiaryCreditIdsByNpcNetIds[42];
			npcBestiaryCreditIdsByNpcNetIds[-59] = npcBestiaryCreditIdsByNpcNetIds[42];
			npcBestiaryCreditIdsByNpcNetIds[-58] = npcBestiaryCreditIdsByNpcNetIds[42];
			npcBestiaryCreditIdsByNpcNetIds[-57] = npcBestiaryCreditIdsByNpcNetIds[42];
			npcBestiaryCreditIdsByNpcNetIds[-56] = npcBestiaryCreditIdsByNpcNetIds[42];
			npcBestiaryCreditIdsByNpcNetIds[-55] = npcBestiaryCreditIdsByNpcNetIds[223];
			npcBestiaryCreditIdsByNpcNetIds[-54] = npcBestiaryCreditIdsByNpcNetIds[223];
			npcBestiaryCreditIdsByNpcNetIds[-53] = npcBestiaryCreditIdsByNpcNetIds[21];
			npcBestiaryCreditIdsByNpcNetIds[-52] = npcBestiaryCreditIdsByNpcNetIds[21];
			npcBestiaryCreditIdsByNpcNetIds[-51] = npcBestiaryCreditIdsByNpcNetIds[21];
			npcBestiaryCreditIdsByNpcNetIds[-50] = npcBestiaryCreditIdsByNpcNetIds[21];
			npcBestiaryCreditIdsByNpcNetIds[-49] = npcBestiaryCreditIdsByNpcNetIds[21];
			npcBestiaryCreditIdsByNpcNetIds[-48] = npcBestiaryCreditIdsByNpcNetIds[21];
			npcBestiaryCreditIdsByNpcNetIds[-47] = npcBestiaryCreditIdsByNpcNetIds[21];
			npcBestiaryCreditIdsByNpcNetIds[-46] = npcBestiaryCreditIdsByNpcNetIds[21];
			npcBestiaryCreditIdsByNpcNetIds[-45] = npcBestiaryCreditIdsByNpcNetIds[3];
			npcBestiaryCreditIdsByNpcNetIds[-44] = npcBestiaryCreditIdsByNpcNetIds[3];
			npcBestiaryCreditIdsByNpcNetIds[-43] = npcBestiaryCreditIdsByNpcNetIds[2];
			npcBestiaryCreditIdsByNpcNetIds[-42] = npcBestiaryCreditIdsByNpcNetIds[2];
			npcBestiaryCreditIdsByNpcNetIds[-41] = npcBestiaryCreditIdsByNpcNetIds[2];
			npcBestiaryCreditIdsByNpcNetIds[-40] = npcBestiaryCreditIdsByNpcNetIds[2];
			npcBestiaryCreditIdsByNpcNetIds[-39] = npcBestiaryCreditIdsByNpcNetIds[2];
			npcBestiaryCreditIdsByNpcNetIds[-38] = npcBestiaryCreditIdsByNpcNetIds[2];
			npcBestiaryCreditIdsByNpcNetIds[-37] = npcBestiaryCreditIdsByNpcNetIds[3];
			npcBestiaryCreditIdsByNpcNetIds[-36] = npcBestiaryCreditIdsByNpcNetIds[3];
			npcBestiaryCreditIdsByNpcNetIds[-35] = npcBestiaryCreditIdsByNpcNetIds[3];
			npcBestiaryCreditIdsByNpcNetIds[-34] = npcBestiaryCreditIdsByNpcNetIds[3];
			npcBestiaryCreditIdsByNpcNetIds[-33] = npcBestiaryCreditIdsByNpcNetIds[3];
			npcBestiaryCreditIdsByNpcNetIds[-32] = npcBestiaryCreditIdsByNpcNetIds[3];
			npcBestiaryCreditIdsByNpcNetIds[-31] = npcBestiaryCreditIdsByNpcNetIds[186];
			npcBestiaryCreditIdsByNpcNetIds[-30] = npcBestiaryCreditIdsByNpcNetIds[186];
			npcBestiaryCreditIdsByNpcNetIds[-27] = npcBestiaryCreditIdsByNpcNetIds[3];
			npcBestiaryCreditIdsByNpcNetIds[-26] = npcBestiaryCreditIdsByNpcNetIds[3];
			npcBestiaryCreditIdsByNpcNetIds[-23] = npcBestiaryCreditIdsByNpcNetIds[173];
			npcBestiaryCreditIdsByNpcNetIds[-22] = npcBestiaryCreditIdsByNpcNetIds[173];
			npcBestiaryCreditIdsByNpcNetIds[-25] = npcBestiaryCreditIdsByNpcNetIds[183];
			npcBestiaryCreditIdsByNpcNetIds[-24] = npcBestiaryCreditIdsByNpcNetIds[183];
			npcBestiaryCreditIdsByNpcNetIds[-21] = npcBestiaryCreditIdsByNpcNetIds[176];
			npcBestiaryCreditIdsByNpcNetIds[-20] = npcBestiaryCreditIdsByNpcNetIds[176];
			npcBestiaryCreditIdsByNpcNetIds[-19] = npcBestiaryCreditIdsByNpcNetIds[176];
			npcBestiaryCreditIdsByNpcNetIds[-18] = npcBestiaryCreditIdsByNpcNetIds[176];
			npcBestiaryCreditIdsByNpcNetIds[-17] = npcBestiaryCreditIdsByNpcNetIds[42];
			npcBestiaryCreditIdsByNpcNetIds[-16] = npcBestiaryCreditIdsByNpcNetIds[42];
			npcBestiaryCreditIdsByNpcNetIds[-15] = npcBestiaryCreditIdsByNpcNetIds[77];
			npcBestiaryCreditIdsByNpcNetIds[-14] = npcBestiaryCreditIdsByNpcNetIds[31];
			npcBestiaryCreditIdsByNpcNetIds[-13] = npcBestiaryCreditIdsByNpcNetIds[31];
			npcBestiaryCreditIdsByNpcNetIds[-12] = npcBestiaryCreditIdsByNpcNetIds[6];
			npcBestiaryCreditIdsByNpcNetIds[-11] = npcBestiaryCreditIdsByNpcNetIds[6];
			npcBestiaryCreditIdsByNpcNetIds[497] = npcBestiaryCreditIdsByNpcNetIds[496];
			npcBestiaryCreditIdsByNpcNetIds[495] = npcBestiaryCreditIdsByNpcNetIds[494];
			short key = 499;
			for (int i = 498; i <= 506; i++)
			{
				npcBestiaryCreditIdsByNpcNetIds[i] = npcBestiaryCreditIdsByNpcNetIds[(int)key];
			}
			npcBestiaryCreditIdsByNpcNetIds[591] = npcBestiaryCreditIdsByNpcNetIds[590];
			npcBestiaryCreditIdsByNpcNetIds[430] = npcBestiaryCreditIdsByNpcNetIds[3];
			npcBestiaryCreditIdsByNpcNetIds[436] = npcBestiaryCreditIdsByNpcNetIds[200];
			npcBestiaryCreditIdsByNpcNetIds[431] = npcBestiaryCreditIdsByNpcNetIds[161];
			npcBestiaryCreditIdsByNpcNetIds[432] = npcBestiaryCreditIdsByNpcNetIds[186];
			npcBestiaryCreditIdsByNpcNetIds[433] = npcBestiaryCreditIdsByNpcNetIds[187];
			npcBestiaryCreditIdsByNpcNetIds[434] = npcBestiaryCreditIdsByNpcNetIds[188];
			npcBestiaryCreditIdsByNpcNetIds[435] = npcBestiaryCreditIdsByNpcNetIds[189];
			npcBestiaryCreditIdsByNpcNetIds[164] = npcBestiaryCreditIdsByNpcNetIds[165];
			npcBestiaryCreditIdsByNpcNetIds[236] = npcBestiaryCreditIdsByNpcNetIds[237];
			npcBestiaryCreditIdsByNpcNetIds[163] = npcBestiaryCreditIdsByNpcNetIds[238];
			npcBestiaryCreditIdsByNpcNetIds[239] = npcBestiaryCreditIdsByNpcNetIds[240];
			npcBestiaryCreditIdsByNpcNetIds[530] = npcBestiaryCreditIdsByNpcNetIds[531];
			npcBestiaryCreditIdsByNpcNetIds[449] = npcBestiaryCreditIdsByNpcNetIds[21];
			npcBestiaryCreditIdsByNpcNetIds[450] = npcBestiaryCreditIdsByNpcNetIds[201];
			npcBestiaryCreditIdsByNpcNetIds[451] = npcBestiaryCreditIdsByNpcNetIds[202];
			npcBestiaryCreditIdsByNpcNetIds[452] = npcBestiaryCreditIdsByNpcNetIds[203];
			npcBestiaryCreditIdsByNpcNetIds[595] = npcBestiaryCreditIdsByNpcNetIds[599];
			npcBestiaryCreditIdsByNpcNetIds[596] = npcBestiaryCreditIdsByNpcNetIds[599];
			npcBestiaryCreditIdsByNpcNetIds[597] = npcBestiaryCreditIdsByNpcNetIds[599];
			npcBestiaryCreditIdsByNpcNetIds[598] = npcBestiaryCreditIdsByNpcNetIds[599];
			npcBestiaryCreditIdsByNpcNetIds[600] = npcBestiaryCreditIdsByNpcNetIds[599];
			npcBestiaryCreditIdsByNpcNetIds[230] = npcBestiaryCreditIdsByNpcNetIds[55];
			npcBestiaryCreditIdsByNpcNetIds[593] = npcBestiaryCreditIdsByNpcNetIds[592];
			npcBestiaryCreditIdsByNpcNetIds[-2] = npcBestiaryCreditIdsByNpcNetIds[121];
			npcBestiaryCreditIdsByNpcNetIds[195] = npcBestiaryCreditIdsByNpcNetIds[196];
			npcBestiaryCreditIdsByNpcNetIds[198] = npcBestiaryCreditIdsByNpcNetIds[199];
			npcBestiaryCreditIdsByNpcNetIds[158] = npcBestiaryCreditIdsByNpcNetIds[159];
			npcBestiaryCreditIdsByNpcNetIds[568] = npcBestiaryCreditIdsByNpcNetIds[569];
			npcBestiaryCreditIdsByNpcNetIds[566] = npcBestiaryCreditIdsByNpcNetIds[567];
			npcBestiaryCreditIdsByNpcNetIds[576] = npcBestiaryCreditIdsByNpcNetIds[577];
			npcBestiaryCreditIdsByNpcNetIds[558] = npcBestiaryCreditIdsByNpcNetIds[560];
			npcBestiaryCreditIdsByNpcNetIds[559] = npcBestiaryCreditIdsByNpcNetIds[560];
			npcBestiaryCreditIdsByNpcNetIds[552] = npcBestiaryCreditIdsByNpcNetIds[554];
			npcBestiaryCreditIdsByNpcNetIds[553] = npcBestiaryCreditIdsByNpcNetIds[554];
			npcBestiaryCreditIdsByNpcNetIds[564] = npcBestiaryCreditIdsByNpcNetIds[565];
			npcBestiaryCreditIdsByNpcNetIds[570] = npcBestiaryCreditIdsByNpcNetIds[571];
			npcBestiaryCreditIdsByNpcNetIds[555] = npcBestiaryCreditIdsByNpcNetIds[557];
			npcBestiaryCreditIdsByNpcNetIds[556] = npcBestiaryCreditIdsByNpcNetIds[557];
			npcBestiaryCreditIdsByNpcNetIds[574] = npcBestiaryCreditIdsByNpcNetIds[575];
			npcBestiaryCreditIdsByNpcNetIds[561] = npcBestiaryCreditIdsByNpcNetIds[563];
			npcBestiaryCreditIdsByNpcNetIds[562] = npcBestiaryCreditIdsByNpcNetIds[563];
			npcBestiaryCreditIdsByNpcNetIds[572] = npcBestiaryCreditIdsByNpcNetIds[573];
			npcBestiaryCreditIdsByNpcNetIds[14] = npcBestiaryCreditIdsByNpcNetIds[13];
			npcBestiaryCreditIdsByNpcNetIds[15] = npcBestiaryCreditIdsByNpcNetIds[13];
			npcBestiaryCreditIdsByNpcNetIds[306] = npcBestiaryCreditIdsByNpcNetIds[305];
			npcBestiaryCreditIdsByNpcNetIds[307] = npcBestiaryCreditIdsByNpcNetIds[305];
			npcBestiaryCreditIdsByNpcNetIds[308] = npcBestiaryCreditIdsByNpcNetIds[305];
			npcBestiaryCreditIdsByNpcNetIds[309] = npcBestiaryCreditIdsByNpcNetIds[305];
			npcBestiaryCreditIdsByNpcNetIds[310] = npcBestiaryCreditIdsByNpcNetIds[305];
			npcBestiaryCreditIdsByNpcNetIds[311] = npcBestiaryCreditIdsByNpcNetIds[305];
			npcBestiaryCreditIdsByNpcNetIds[312] = npcBestiaryCreditIdsByNpcNetIds[305];
			npcBestiaryCreditIdsByNpcNetIds[313] = npcBestiaryCreditIdsByNpcNetIds[305];
			npcBestiaryCreditIdsByNpcNetIds[314] = npcBestiaryCreditIdsByNpcNetIds[305];
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x0056D018 File Offset: 0x0056B218
		private static void CreateBestiarySortingIds(BestiaryDatabase database)
		{
			List<KeyValuePair<int, NPC>> sortedBestiaryEntriesList = ContentSamples.BestiaryHelper.GetSortedBestiaryEntriesList(database);
			int num = 1;
			foreach (KeyValuePair<int, NPC> item in sortedBestiaryEntriesList)
			{
				ContentSamples.NpcBestiarySortingId[item.Key] = num;
				num++;
			}
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x0056D07C File Offset: 0x0056B27C
		private static void FillNpcRarities()
		{
			NPCSpawnParams spawnparams = new NPCSpawnParams
			{
				gameModeData = new GameModeData?(Main.RegisteredGameModes[0])
			};
			for (int i = -65; i < NPCLoader.NPCCount; i++)
			{
				NPC nPC = new NPC();
				nPC.SetDefaults(i, spawnparams);
				ContentSamples.NpcBestiaryRarityStars[i] = ContentSamples.GetNPCBestiaryRarityStarsCount(nPC);
			}
			ContentSamples.NpcBestiaryRarityStars[22] = 1;
			ContentSamples.NpcBestiaryRarityStars[17] = 1;
			ContentSamples.NpcBestiaryRarityStars[18] = 1;
			ContentSamples.NpcBestiaryRarityStars[38] = 1;
			ContentSamples.NpcBestiaryRarityStars[369] = 2;
			ContentSamples.NpcBestiaryRarityStars[20] = 3;
			ContentSamples.NpcBestiaryRarityStars[19] = 1;
			ContentSamples.NpcBestiaryRarityStars[227] = 2;
			ContentSamples.NpcBestiaryRarityStars[353] = 2;
			ContentSamples.NpcBestiaryRarityStars[550] = 2;
			ContentSamples.NpcBestiaryRarityStars[588] = 2;
			ContentSamples.NpcBestiaryRarityStars[107] = 3;
			ContentSamples.NpcBestiaryRarityStars[228] = 2;
			ContentSamples.NpcBestiaryRarityStars[124] = 2;
			ContentSamples.NpcBestiaryRarityStars[54] = 2;
			ContentSamples.NpcBestiaryRarityStars[108] = 3;
			ContentSamples.NpcBestiaryRarityStars[178] = 3;
			ContentSamples.NpcBestiaryRarityStars[216] = 3;
			ContentSamples.NpcBestiaryRarityStars[160] = 5;
			ContentSamples.NpcBestiaryRarityStars[441] = 5;
			ContentSamples.NpcBestiaryRarityStars[209] = 3;
			ContentSamples.NpcBestiaryRarityStars[208] = 4;
			ContentSamples.NpcBestiaryRarityStars[142] = 5;
			ContentSamples.NpcBestiaryRarityStars[368] = 3;
			ContentSamples.NpcBestiaryRarityStars[453] = 4;
			ContentSamples.NpcBestiaryRarityStars[37] = 2;
			ContentSamples.NpcBestiaryRarityStars[633] = 5;
			ContentSamples.NpcBestiaryRarityStars[663] = 5;
			ContentSamples.NpcBestiaryRarityStars[638] = 3;
			ContentSamples.NpcBestiaryRarityStars[637] = 3;
			ContentSamples.NpcBestiaryRarityStars[656] = 3;
			ContentSamples.NpcBestiaryRarityStars[670] = 3;
			ContentSamples.NpcBestiaryRarityStars[678] = 3;
			ContentSamples.NpcBestiaryRarityStars[679] = 3;
			ContentSamples.NpcBestiaryRarityStars[680] = 3;
			ContentSamples.NpcBestiaryRarityStars[681] = 3;
			ContentSamples.NpcBestiaryRarityStars[682] = 3;
			ContentSamples.NpcBestiaryRarityStars[683] = 3;
			ContentSamples.NpcBestiaryRarityStars[684] = 3;
			ContentSamples.NpcBestiaryRarityStars[664] = 5;
			ContentSamples.NpcBestiaryRarityStars[484] = 5;
			ContentSamples.NpcBestiaryRarityStars[614] = 4;
			ContentSamples.NpcBestiaryRarityStars[303] = 4;
			ContentSamples.NpcBestiaryRarityStars[337] = 4;
			ContentSamples.NpcBestiaryRarityStars[360] = 3;
			ContentSamples.NpcBestiaryRarityStars[655] = 2;
			ContentSamples.NpcBestiaryRarityStars[374] = 3;
			ContentSamples.NpcBestiaryRarityStars[661] = 3;
			ContentSamples.NpcBestiaryRarityStars[362] = 2;
			ContentSamples.NpcBestiaryRarityStars[364] = 2;
			ContentSamples.NpcBestiaryRarityStars[616] = 2;
			ContentSamples.NpcBestiaryRarityStars[298] = 2;
			ContentSamples.NpcBestiaryRarityStars[671] = 3;
			ContentSamples.NpcBestiaryRarityStars[672] = 3;
			ContentSamples.NpcBestiaryRarityStars[673] = 3;
			ContentSamples.NpcBestiaryRarityStars[674] = 3;
			ContentSamples.NpcBestiaryRarityStars[675] = 3;
			ContentSamples.NpcBestiaryRarityStars[599] = 3;
			ContentSamples.NpcBestiaryRarityStars[355] = 2;
			ContentSamples.NpcBestiaryRarityStars[358] = 3;
			ContentSamples.NpcBestiaryRarityStars[654] = 3;
			ContentSamples.NpcBestiaryRarityStars[653] = 2;
			ContentSamples.NpcBestiaryRarityStars[540] = 2;
			ContentSamples.NpcBestiaryRarityStars[604] = 3;
			ContentSamples.NpcBestiaryRarityStars[611] = 3;
			ContentSamples.NpcBestiaryRarityStars[612] = 2;
			ContentSamples.NpcBestiaryRarityStars[608] = 2;
			ContentSamples.NpcBestiaryRarityStars[607] = 2;
			ContentSamples.NpcBestiaryRarityStars[615] = 3;
			ContentSamples.NpcBestiaryRarityStars[626] = 2;
			ContentSamples.NpcBestiaryRarityStars[486] = 2;
			ContentSamples.NpcBestiaryRarityStars[487] = 3;
			ContentSamples.NpcBestiaryRarityStars[669] = 3;
			ContentSamples.NpcBestiaryRarityStars[677] = 5;
			ContentSamples.NpcBestiaryRarityStars[676] = 5;
			ContentSamples.NpcBestiaryRarityStars[149] = 2;
			ContentSamples.NpcBestiaryRarityStars[366] = 2;
			ContentSamples.NpcBestiaryRarityStars[47] = 3;
			ContentSamples.NpcBestiaryRarityStars[57] = 3;
			ContentSamples.NpcBestiaryRarityStars[168] = 3;
			ContentSamples.NpcBestiaryRarityStars[464] = 3;
			ContentSamples.NpcBestiaryRarityStars[465] = 3;
			ContentSamples.NpcBestiaryRarityStars[470] = 3;
			ContentSamples.NpcBestiaryRarityStars[301] = 2;
			ContentSamples.NpcBestiaryRarityStars[316] = 3;
			ContentSamples.NpcBestiaryRarityStars[546] = 2;
			ContentSamples.NpcBestiaryRarityStars[170] = 3;
			ContentSamples.NpcBestiaryRarityStars[180] = 3;
			ContentSamples.NpcBestiaryRarityStars[171] = 3;
			ContentSamples.NpcBestiaryRarityStars[29] = 2;
			ContentSamples.NpcBestiaryRarityStars[471] = 4;
			ContentSamples.NpcBestiaryRarityStars[66] = 3;
			ContentSamples.NpcBestiaryRarityStars[223] = 2;
			ContentSamples.NpcBestiaryRarityStars[161] = 2;
			ContentSamples.NpcBestiaryRarityStars[491] = 4;
			ContentSamples.NpcBestiaryRarityStars[-9] = 3;
			ContentSamples.NpcBestiaryRarityStars[594] = 2;
			ContentSamples.NpcBestiaryRarityStars[628] = 2;
			ContentSamples.NpcBestiaryRarityStars[225] = 2;
			ContentSamples.NpcBestiaryRarityStars[224] = 2;
			ContentSamples.NpcBestiaryRarityStars[250] = 3;
			ContentSamples.NpcBestiaryRarityStars[16] = 2;
			ContentSamples.NpcBestiaryRarityStars[481] = 2;
			ContentSamples.NpcBestiaryRarityStars[483] = 2;
			ContentSamples.NpcBestiaryRarityStars[184] = 2;
			ContentSamples.NpcBestiaryRarityStars[185] = 3;
			ContentSamples.NpcBestiaryRarityStars[206] = 3;
			ContentSamples.NpcBestiaryRarityStars[541] = 4;
			ContentSamples.NpcBestiaryRarityStars[537] = 2;
			ContentSamples.NpcBestiaryRarityStars[205] = 4;
			ContentSamples.NpcBestiaryRarityStars[499] = 2;
			ContentSamples.NpcBestiaryRarityStars[494] = 2;
			ContentSamples.NpcBestiaryRarityStars[496] = 2;
			ContentSamples.NpcBestiaryRarityStars[302] = 3;
			ContentSamples.NpcBestiaryRarityStars[317] = 3;
			ContentSamples.NpcBestiaryRarityStars[318] = 3;
			ContentSamples.NpcBestiaryRarityStars[319] = 3;
			ContentSamples.NpcBestiaryRarityStars[320] = 3;
			ContentSamples.NpcBestiaryRarityStars[321] = 3;
			ContentSamples.NpcBestiaryRarityStars[331] = 3;
			ContentSamples.NpcBestiaryRarityStars[332] = 3;
			ContentSamples.NpcBestiaryRarityStars[322] = 3;
			ContentSamples.NpcBestiaryRarityStars[323] = 3;
			ContentSamples.NpcBestiaryRarityStars[324] = 3;
			ContentSamples.NpcBestiaryRarityStars[335] = 3;
			ContentSamples.NpcBestiaryRarityStars[336] = 3;
			ContentSamples.NpcBestiaryRarityStars[333] = 3;
			ContentSamples.NpcBestiaryRarityStars[334] = 3;
			ContentSamples.NpcBestiaryRarityStars[4] = 2;
			ContentSamples.NpcBestiaryRarityStars[50] = 2;
			ContentSamples.NpcBestiaryRarityStars[35] = 3;
			ContentSamples.NpcBestiaryRarityStars[13] = 3;
			ContentSamples.NpcBestiaryRarityStars[134] = 4;
			ContentSamples.NpcBestiaryRarityStars[262] = 4;
			ContentSamples.NpcBestiaryRarityStars[668] = 3;
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x0056D90C File Offset: 0x0056BB0C
		private static int GetNPCBestiaryRarityStarsCount(NPC npc)
		{
			float num = 1f;
			num += (float)npc.rarity;
			if (npc.rarity == 1)
			{
				num += 1f;
			}
			else if (npc.rarity == 2)
			{
				num += 1.5f;
			}
			else if (npc.rarity == 3)
			{
				num += 2f;
			}
			else if (npc.rarity == 4)
			{
				num += 2.5f;
			}
			else if (npc.rarity == 5)
			{
				num += 3f;
			}
			else if (npc.rarity > 0)
			{
				num += 3.5f;
			}
			if (npc.boss)
			{
				num += 0.5f;
			}
			int num2 = npc.damage + npc.defense + npc.lifeMax / 4;
			if (num2 > 10000)
			{
				num += 3.5f;
			}
			else if (num2 > 5000)
			{
				num += 3f;
			}
			else if (num2 > 1000)
			{
				num += 2.5f;
			}
			else if (num2 > 500)
			{
				num += 2f;
			}
			else if (num2 > 150)
			{
				num += 1.5f;
			}
			else if (num2 > 50)
			{
				num += 1f;
			}
			if (num > 5f)
			{
				num = 5f;
			}
			return (int)num;
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x0056DA37 File Offset: 0x0056BC37
		private static void CreateCreativeItemSortingIds()
		{
			ContentSamples.CreativeHelper.SetCreativeMenuOrder();
		}

		/// <summary>
		/// The default <see cref="T:Terraria.NPC" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />). <br />
		/// Note that these entries do not take into account game mode specific changes so they shouldn't be used for querying that data. <br />
		/// </summary>
		// Token: 0x040020D2 RID: 8402
		public static Dictionary<int, NPC> NpcsByNetId = new Dictionary<int, NPC>();

		/// <summary>
		/// The default <see cref="T:Terraria.Projectile" /> for a given projectile type (<see cref="F:Terraria.Projectile.type" />).
		/// </summary>
		// Token: 0x040020D3 RID: 8403
		public static Dictionary<int, Projectile> ProjectilesByType = new Dictionary<int, Projectile>();

		/// <summary>
		/// The default <see cref="T:Terraria.Item" /> for a given item type (<see cref="F:Terraria.Item.type" />).
		/// </summary>
		// Token: 0x040020D4 RID: 8404
		public static Dictionary<int, Item> ItemsByType = new Dictionary<int, Item>();

		/// <summary>
		/// The item type (<see cref="F:Terraria.Item.type" />) for a given persistent ID.
		/// <br /> Vanilla persistent IDs are the names in <see cref="T:Terraria.ID.ItemID" />. Modded persistent IDs are the value of <see cref="P:Terraria.ModLoader.ModType.FullName" />.
		/// </summary>
		/// <remarks>
		/// You can get the persistent ID of a given item type using either <see cref="F:Terraria.ID.ContentSamples.ItemPersistentIdsByNetIds" /> or <see cref="F:Terraria.ID.ItemID.Search" />.
		/// </remarks>
		// Token: 0x040020D5 RID: 8405
		public static Dictionary<string, int> ItemNetIdsByPersistentIds = new Dictionary<string, int>();

		/// <summary>
		/// The persistent ID for a given item type (<see cref="F:Terraria.Item.type" />).
		/// </summary>
		// Token: 0x040020D6 RID: 8406
		public static Dictionary<int, string> ItemPersistentIdsByNetIds = new Dictionary<int, string>();

		/// <summary>
		/// Associates an item type (<see cref="F:Terraria.Item.type" />) with the item type it shares research values with.
		/// </summary>
		// Token: 0x040020D7 RID: 8407
		public static Dictionary<int, int> CreativeResearchItemPersistentIdOverride = new Dictionary<int, int>();

		/// <summary>
		/// The NPC type (<see cref="F:Terraria.NPC.type" />) for a given persistent ID.
		/// <br /> Vanilla persistent IDs are the names in <see cref="T:Terraria.ID.NPCID" />. Modded persistent IDs are the value of <see cref="P:Terraria.ModLoader.ModType.FullName" />.
		/// </summary>
		/// <remarks>
		/// You can get the persistent ID of a given NPC type using either <see cref="F:Terraria.ID.ContentSamples.NpcPersistentIdsByNetIds" /> or <see cref="F:Terraria.ID.NPCID.Search" />.
		/// </remarks>
		// Token: 0x040020D8 RID: 8408
		public static Dictionary<string, int> NpcNetIdsByPersistentIds = new Dictionary<string, int>();

		/// <summary>
		/// The persistent ID for a given NPC type (<see cref="F:Terraria.NPC.type" />).
		/// </summary>
		// Token: 0x040020D9 RID: 8409
		public static Dictionary<int, string> NpcPersistentIdsByNetIds = new Dictionary<int, string>();

		/// <summary>
		/// The entry number in the Bestiary for a given NPC type (<see cref="F:Terraria.NPC.type" />).
		/// </summary>
		// Token: 0x040020DA RID: 8410
		public static Dictionary<int, int> NpcBestiarySortingId = new Dictionary<int, int>();

		/// <summary>
		/// The number of stars a given NPC type (<see cref="F:Terraria.NPC.type" />) shows in the Bestiary.
		/// <br /> Set in <see cref="M:Terraria.ModLoader.ModNPC.SetBestiary(Terraria.GameContent.Bestiary.BestiaryDatabase,Terraria.GameContent.Bestiary.BestiaryEntry)" /> or <see cref="M:Terraria.ModLoader.GlobalNPC.SetBestiary(Terraria.NPC,Terraria.GameContent.Bestiary.BestiaryDatabase,Terraria.GameContent.Bestiary.BestiaryEntry)" />.
		/// </summary>
		// Token: 0x040020DB RID: 8411
		public static Dictionary<int, int> NpcBestiaryRarityStars = new Dictionary<int, int>();

		/// <summary>
		/// Associates an NPC type (<see cref="F:Terraria.NPC.type" />) with the persistent ID used for the Bestiary.
		/// </summary>
		// Token: 0x040020DC RID: 8412
		public static Dictionary<int, string> NpcBestiaryCreditIdsByNpcNetIds = new Dictionary<int, string>();

		/// <summary>
		/// Associates an item type (<see cref="F:Terraria.Item.type" />) with its <see cref="T:Terraria.ID.ContentSamples.CreativeHelper.ItemGroup" /> and sorting order in the Research menu.
		/// </summary>
		// Token: 0x040020DD RID: 8413
		public static Dictionary<int, ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup> ItemCreativeSortingId = new Dictionary<int, ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup>();

		// Token: 0x02000B4E RID: 2894
		public static class CommonlyUsedContentSamples
		{
			// Token: 0x06005C62 RID: 23650 RVA: 0x006ABE09 File Offset: 0x006AA009
			public static void PrepareAfterEverythingElseLoaded()
			{
				ContentSamples.CommonlyUsedContentSamples.TeamDyeShaderIndex = GameShaders.Hair.GetShaderIdFromItemId(1984);
				ContentSamples.CommonlyUsedContentSamples.ColorOnlyShaderIndex = GameShaders.Armor.GetShaderIdFromItemId(3978);
			}

			// Token: 0x04007412 RID: 29714
			public static int TeamDyeShaderIndex = -1;

			// Token: 0x04007413 RID: 29715
			public static int ColorOnlyShaderIndex = -1;
		}

		// Token: 0x02000B4F RID: 2895
		public static class CreativeHelper
		{
			// Token: 0x06005C64 RID: 23652 RVA: 0x006ABE44 File Offset: 0x006AA044
			public static ContentSamples.CreativeHelper.ItemGroup GetItemGroup(Item item, out int orderInGroup)
			{
				orderInGroup = 0;
				int num = ContentSamples.CreativeHelper._manualBossSpawnItemsOrder.IndexOf(item.type);
				if (num != -1)
				{
					orderInGroup = num;
					return ContentSamples.CreativeHelper.ItemGroup.BossItem;
				}
				int num2 = ContentSamples.CreativeHelper._manualGolfItemsOrder.IndexOf(item.type);
				if (num2 != -1)
				{
					orderInGroup = num2;
					return ContentSamples.CreativeHelper.ItemGroup.Golf;
				}
				int num3 = ItemID.Sets.SortingPriorityWiring[item.type];
				if (num3 != -1)
				{
					orderInGroup = -num3;
					return ContentSamples.CreativeHelper.ItemGroup.Wiring;
				}
				if (item.type == 3620)
				{
					return ContentSamples.CreativeHelper.ItemGroup.Wiring;
				}
				if (item.type == 327 || item.type == 329 || item.type == 1141 || item.type == 1533 || item.type == 1537 || item.type == 1536 || item.type == 1534 || item.type == 1535 || item.type == 3092 || item.type == 3091 || item.type == 4714)
				{
					orderInGroup = -item.rare;
					return ContentSamples.CreativeHelper.ItemGroup.Keys;
				}
				if (item.type == 985 || item.type == 3079 || item.type == 3005 || item.type == 3080)
				{
					return ContentSamples.CreativeHelper.ItemGroup.Rope;
				}
				if (item.type == 781 || item.type == 783 || item.type == 780 || item.type == 782 || item.type == 784)
				{
					return ContentSamples.CreativeHelper.ItemGroup.Solutions;
				}
				if (ItemID.Sets.Glowsticks[item.type])
				{
					if (item.type == 282)
					{
						orderInGroup = -1;
					}
					return ContentSamples.CreativeHelper.ItemGroup.Glowsticks;
				}
				if (item.type == 166 || item.type == 3115 || item.type == 235 || item.type == 167 || item.type == 3547 || item.type == 2896 || item.type == 3196 || item.type == 4908 || item.type == 4909 || item.type == 4827 || item.type == 4826 || item.type == 4825 || item.type == 4423 || item.type == 4824)
				{
					return ContentSamples.CreativeHelper.ItemGroup.Bombs;
				}
				if (item.createTile == 376)
				{
					return ContentSamples.CreativeHelper.ItemGroup.Crates;
				}
				if (item.type == 1774 || item.type == 1869 || item.type == 4345 || item.type == 3093 || item.type == 4410)
				{
					return ContentSamples.CreativeHelper.ItemGroup.GoodieBags;
				}
				if (ItemID.Sets.BossBag[item.type])
				{
					return ContentSamples.CreativeHelper.ItemGroup.BossBags;
				}
				if (item.type == 1115 || item.type == 1114 || item.type == 1110 || item.type == 1112 || item.type == 1108 || item.type == 1107 || item.type == 1116 || item.type == 1109 || item.type == 1111 || item.type == 1118 || item.type == 1117 || item.type == 1113 || item.type == 1119)
				{
					return ContentSamples.CreativeHelper.ItemGroup.DyeMaterial;
				}
				if (item.type == 3385 || item.type == 3386 || item.type == 3387 || item.type == 3388)
				{
					orderInGroup = -1;
					return ContentSamples.CreativeHelper.ItemGroup.DyeMaterial;
				}
				if (item.dye != 0)
				{
					return ContentSamples.CreativeHelper.ItemGroup.Dye;
				}
				if (item.hairDye != -1)
				{
					return ContentSamples.CreativeHelper.ItemGroup.HairDye;
				}
				if (item.IsACoin)
				{
					if (item.type == 71)
					{
						orderInGroup = 4;
					}
					else if (item.type == 72)
					{
						orderInGroup = 3;
					}
					else if (item.type == 73)
					{
						orderInGroup = 2;
					}
					else if (item.type == 74)
					{
						orderInGroup = 1;
					}
					return ContentSamples.CreativeHelper.ItemGroup.Coin;
				}
				if (item.createWall > 0)
				{
					return ContentSamples.CreativeHelper.ItemGroup.Walls;
				}
				if (item.createTile == 82)
				{
					return ContentSamples.CreativeHelper.ItemGroup.AlchemySeeds;
				}
				if (item.type == 315 || item.type == 313 || item.type == 316 || item.type == 318 || item.type == 314 || item.type == 2358 || item.type == 317)
				{
					return ContentSamples.CreativeHelper.ItemGroup.AlchemyPlants;
				}
				if (item.createTile == 30 || item.createTile == 321 || item.createTile == 322 || item.createTile == 157 || item.createTile == 158 || item.createTile == 208 || item.createTile == 159 || item.createTile == 253 || item.createTile == 311 || item.createTile == 635)
				{
					if (item.createTile == 30)
					{
						orderInGroup = 0;
					}
					else if (item.createTile == 311)
					{
						orderInGroup = 100;
					}
					else
					{
						orderInGroup = 50;
					}
					return ContentSamples.CreativeHelper.ItemGroup.Wood;
				}
				if (item.createTile >= 0)
				{
					if (item.type == 213)
					{
						orderInGroup = -1;
						return ContentSamples.CreativeHelper.ItemGroup.Pickaxe;
					}
					if (item.tileWand >= 0)
					{
						return ContentSamples.CreativeHelper.ItemGroup.Wands;
					}
					if (item.createTile == 213 || item.createTile == 353 || item.createTile == 365 || item.createTile == 366 || item.createTile == 214)
					{
						return ContentSamples.CreativeHelper.ItemGroup.Rope;
					}
					if (Main.tileSolid[item.createTile] && !Main.tileSolidTop[item.createTile] && item.createTile != 10)
					{
						if (TileID.Sets.Conversion.Grass[item.createTile] || item.type == 194)
						{
							orderInGroup = 5;
						}
						else
						{
							orderInGroup = 10000;
						}
						if (item.type == 2)
						{
							orderInGroup = 10;
						}
						else if (item.type == 3)
						{
							orderInGroup = 20;
						}
						else if (item.type == 133)
						{
							orderInGroup = 30;
						}
						else if (item.type == 424)
						{
							orderInGroup = 40;
						}
						else if (item.type == 1103)
						{
							orderInGroup = 50;
						}
						else if (item.type == 169)
						{
							orderInGroup = 60;
						}
						else if (item.type == 170)
						{
							orderInGroup = 70;
						}
						else if (item.type == 176)
						{
							orderInGroup = 80;
						}
						else if (item.type == 276)
						{
							orderInGroup = 80;
						}
						return ContentSamples.CreativeHelper.ItemGroup.Blocks;
					}
					int num4 = ContentSamples.CreativeHelper._manualCraftingStations.IndexOf(item.type);
					if (num4 != -1)
					{
						orderInGroup = num4;
						return ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
					}
					if (item.createTile == 4)
					{
						if (item.placeStyle == 0)
						{
							orderInGroup = 5;
						}
						else
						{
							orderInGroup = 10;
						}
						return ContentSamples.CreativeHelper.ItemGroup.Torches;
					}
					if (TileID.Sets.Torch[item.createTile])
					{
						return ContentSamples.CreativeHelper.ItemGroup.Torches;
					}
					if (item.createTile == 178)
					{
						orderInGroup = 5;
					}
					else if (item.createTile == 239)
					{
						orderInGroup = 7;
					}
					else if (item.type == 27 || item.type == 4857 || item.type == 4852 || item.type == 4856 || item.type == 4854 || item.type == 4855 || item.type == 4853 || item.type == 4851)
					{
						orderInGroup = 8;
					}
					else if (TileID.Sets.Platforms[item.createTile])
					{
						if (item.placeStyle == 0)
						{
							orderInGroup = 19;
						}
						else
						{
							orderInGroup = 20;
						}
					}
					else if (item.createTile == 18)
					{
						if (item.placeStyle == 0)
						{
							orderInGroup = 29;
						}
						else
						{
							orderInGroup = 30;
						}
					}
					else if (item.createTile == 16 || item.createTile == 134)
					{
						if (item.placeStyle == 0)
						{
							orderInGroup = 39;
						}
						else
						{
							orderInGroup = 40;
						}
					}
					else if (item.createTile == 133 || item.createTile == 17)
					{
						if (item.placeStyle == 0)
						{
							orderInGroup = 49;
						}
						else
						{
							orderInGroup = 50;
						}
					}
					else if (item.createTile == 10)
					{
						if (item.placeStyle == 0)
						{
							orderInGroup = 59;
						}
						else
						{
							orderInGroup = 60;
						}
					}
					else if (item.createTile == 15)
					{
						if (item.placeStyle == 0)
						{
							orderInGroup = 69;
						}
						else
						{
							orderInGroup = 70;
						}
					}
					else if (item.createTile == 497)
					{
						orderInGroup = 72;
					}
					else if (item.createTile == 79)
					{
						if (item.placeStyle == 0)
						{
							orderInGroup = 74;
						}
						else
						{
							orderInGroup = 75;
						}
					}
					else if (item.createTile == 14)
					{
						if (item.placeStyle == 0)
						{
							orderInGroup = 79;
						}
						else
						{
							orderInGroup = 80;
						}
					}
					else if (item.createTile == 469)
					{
						orderInGroup = 90;
					}
					else if (item.createTile == 21)
					{
						if (item.placeStyle == 0)
						{
							orderInGroup = 99;
						}
						else
						{
							orderInGroup = 100;
						}
					}
					else if (item.createTile == 467)
					{
						orderInGroup = 110;
					}
					else if (item.createTile == 441)
					{
						orderInGroup = 120;
					}
					else if (item.createTile == 468)
					{
						orderInGroup = 130;
					}
					else
					{
						orderInGroup = item.createTile + 1000;
					}
					return ContentSamples.CreativeHelper.ItemGroup.PlacableObjects;
				}
				else if (item.mountType != -1)
				{
					if (MountID.Sets.Cart[item.mountType])
					{
						return ContentSamples.CreativeHelper.ItemGroup.Minecart;
					}
					return ContentSamples.CreativeHelper.ItemGroup.Mount;
				}
				else
				{
					if (item.bait > 0)
					{
						orderInGroup = -item.bait;
						return ContentSamples.CreativeHelper.ItemGroup.FishingBait;
					}
					if (item.makeNPC > 0)
					{
						return ContentSamples.CreativeHelper.ItemGroup.Critters;
					}
					if (item.fishingPole > 1)
					{
						orderInGroup = -item.fishingPole;
						return ContentSamples.CreativeHelper.ItemGroup.FishingRods;
					}
					if (item.questItem)
					{
						return ContentSamples.CreativeHelper.ItemGroup.FishingQuestFish;
					}
					if ((item.type >= 2297 && item.type <= 2321) || item.type == 4402 || item.type == 4401 || item.type == 2290)
					{
						orderInGroup = -item.rare;
						return ContentSamples.CreativeHelper.ItemGroup.FishingQuestFish;
					}
					int num5 = ItemID.Sets.SortingPriorityPainting[item.type];
					if (num5 != -1 || item.PaintOrCoating)
					{
						orderInGroup = -num5;
						return ContentSamples.CreativeHelper.ItemGroup.Paint;
					}
					int num6 = ContentSamples.CreativeHelper._manualEventItemsOrder.IndexOf(item.type);
					if (num6 != -1)
					{
						orderInGroup = num6;
						return ContentSamples.CreativeHelper.ItemGroup.EventItem;
					}
					if (item.shoot != 0 && Main.projHook[item.shoot])
					{
						return ContentSamples.CreativeHelper.ItemGroup.Hook;
					}
					if (item.type == 2756 || item.type == 2351 || item.type == 4870 || item.type == 2350 || item.type == 2997 || item.type == 2352 || item.type == 2353)
					{
						return ContentSamples.CreativeHelper.ItemGroup.BuffPotion;
					}
					if (item.buffType != 0)
					{
						if (BuffID.Sets.IsWellFed[item.buffType])
						{
							orderInGroup = -item.buffType * 10000000 - item.buffTime;
							return ContentSamples.CreativeHelper.ItemGroup.Food;
						}
						if (BuffID.Sets.IsAFlaskBuff[item.buffType])
						{
							return ContentSamples.CreativeHelper.ItemGroup.Flask;
						}
						if (Main.vanityPet[item.buffType])
						{
							return ContentSamples.CreativeHelper.ItemGroup.VanityPet;
						}
						if (Main.lightPet[item.buffType])
						{
							return ContentSamples.CreativeHelper.ItemGroup.VanityPet;
						}
						if (item.damage == -1)
						{
							return ContentSamples.CreativeHelper.ItemGroup.BuffPotion;
						}
					}
					if (item.headSlot >= 0)
					{
						orderInGroup = -item.defense;
						orderInGroup -= item.rare * 1000;
						if (item.vanity)
						{
							orderInGroup += 100000;
						}
						return ContentSamples.CreativeHelper.ItemGroup.Headgear;
					}
					if (item.bodySlot >= 0)
					{
						orderInGroup = -item.defense;
						orderInGroup -= item.rare * 1000;
						if (item.vanity)
						{
							orderInGroup += 100000;
						}
						return ContentSamples.CreativeHelper.ItemGroup.Torso;
					}
					if (item.legSlot >= 0)
					{
						orderInGroup = -item.defense;
						orderInGroup -= item.rare * 1000;
						if (item.vanity)
						{
							orderInGroup += 100000;
						}
						return ContentSamples.CreativeHelper.ItemGroup.Pants;
					}
					if (item.accessory)
					{
						orderInGroup = item.vanity.ToInt() - item.expert.ToInt();
						if (item.type >= 3293 && item.type <= 3308)
						{
							orderInGroup -= 200000;
						}
						else if (item.type >= 3309 && item.type <= 3314)
						{
							orderInGroup -= 100000;
						}
						orderInGroup -= item.rare * 10000;
						if (item.vanity)
						{
							orderInGroup += 100000;
						}
						return ContentSamples.CreativeHelper.ItemGroup.Accessories;
					}
					if (item.pick > 0)
					{
						orderInGroup = -item.pick;
						return ContentSamples.CreativeHelper.ItemGroup.Pickaxe;
					}
					if (item.axe > 0)
					{
						orderInGroup = -item.axe;
						return ContentSamples.CreativeHelper.ItemGroup.Axe;
					}
					if (item.hammer > 0)
					{
						orderInGroup = -item.hammer;
						return ContentSamples.CreativeHelper.ItemGroup.Hammer;
					}
					if (item.healLife > 0)
					{
						if (item.type == 3544)
						{
							orderInGroup = 0;
						}
						else if (item.type == 499)
						{
							orderInGroup = 1;
						}
						else if (item.type == 188)
						{
							orderInGroup = 2;
						}
						else if (item.type == 28)
						{
							orderInGroup = 3;
						}
						else
						{
							orderInGroup = -item.healLife + 1000;
						}
						return ContentSamples.CreativeHelper.ItemGroup.LifePotions;
					}
					if (item.healMana > 0)
					{
						orderInGroup = -item.healMana;
						return ContentSamples.CreativeHelper.ItemGroup.ManaPotions;
					}
					if (item.ammo != AmmoID.None && !item.notAmmo && item.type != 23 && item.type != 75)
					{
						orderInGroup = -item.ammo * 10000;
						orderInGroup += -item.damage;
						return ContentSamples.CreativeHelper.ItemGroup.Ammo;
					}
					if (item.consumable)
					{
						if (item.damage > 0)
						{
							if (item.type == 422 || item.type == 423 || item.type == 3477)
							{
								orderInGroup = -100000;
							}
							else
							{
								orderInGroup = -item.damage;
							}
							return ContentSamples.CreativeHelper.ItemGroup.ConsumableThatDamages;
						}
						if (item.type == 4910 || item.type == 4829 || item.type == 4830)
						{
							orderInGroup = 10;
						}
						else if (item.type == 66 || item.type == 2886 || item.type == 67)
						{
							orderInGroup = -10;
						}
						else if (item.type >= 1874 && item.type <= 1905)
						{
							orderInGroup = 5;
						}
						return ContentSamples.CreativeHelper.ItemGroup.ConsumableThatDoesNotDamage;
					}
					else
					{
						if (item.damage > 0)
						{
							orderInGroup = -item.damage;
							if (item.melee)
							{
								return ContentSamples.CreativeHelper.ItemGroup.MeleeWeapon;
							}
							if (item.ranged)
							{
								return ContentSamples.CreativeHelper.ItemGroup.RangedWeapon;
							}
							if (item.magic)
							{
								return ContentSamples.CreativeHelper.ItemGroup.MagicWeapon;
							}
							if (item.summon)
							{
								return ContentSamples.CreativeHelper.ItemGroup.SummonWeapon;
							}
						}
						orderInGroup = -item.rare;
						if (item.useStyle > 0)
						{
							return ContentSamples.CreativeHelper.ItemGroup.RemainingUseItems;
						}
						if (item.material)
						{
							return ContentSamples.CreativeHelper.ItemGroup.Material;
						}
						return ContentSamples.CreativeHelper.ItemGroup.EverythingElse;
					}
				}
			}

			// Token: 0x06005C65 RID: 23653 RVA: 0x006ACD30 File Offset: 0x006AAF30
			public static void SetCreativeMenuOrder()
			{
				List<Item> list = new List<Item>();
				for (int i = 1; i < ItemLoader.ItemCount; i++)
				{
					Item item = new Item();
					item.SetDefaults(i);
					list.Add(item);
				}
				IOrderedEnumerable<IGrouping<ContentSamples.CreativeHelper.ItemGroup, ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup>> orderedEnumerable = from x in list
				select new ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup(x) into x
				group x by x.Group into @group
				orderby (int)@group.Key
				select @group;
				foreach (IGrouping<ContentSamples.CreativeHelper.ItemGroup, ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup> grouping in orderedEnumerable)
				{
					foreach (ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup item2 in grouping)
					{
						ContentSamples.ItemCreativeSortingId[item2.ItemType] = item2;
					}
				}
				orderedEnumerable.SelectMany((IGrouping<ContentSamples.CreativeHelper.ItemGroup, ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup> group) => group.ToList<ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup>()).ToList<ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup>();
			}

			// Token: 0x06005C66 RID: 23654 RVA: 0x006ACE80 File Offset: 0x006AB080
			public static bool ShouldRemoveFromList(Item item)
			{
				return ItemID.Sets.Deprecated[item.type];
			}

			// Token: 0x04007414 RID: 29716
			private static List<int> _manualEventItemsOrder = new List<int>
			{
				361,
				1315,
				2767,
				602,
				1844,
				1958
			};

			// Token: 0x04007415 RID: 29717
			private static List<int> _manualBossSpawnItemsOrder = new List<int>
			{
				43,
				560,
				70,
				1331,
				1133,
				5120,
				1307,
				267,
				3828,
				4988,
				5334,
				544,
				557,
				556,
				1293,
				2673,
				4961,
				3601
			};

			// Token: 0x04007416 RID: 29718
			public static List<int> _manualCraftingStations = new List<int>
			{
				33,
				35,
				716,
				221,
				524,
				1221,
				525,
				1220,
				3549,
				398,
				1120,
				1430,
				1551,
				345,
				1791,
				5008,
				332,
				352,
				487,
				995,
				363,
				2172,
				2196,
				2194,
				2198,
				2204,
				998,
				2197,
				996,
				4142,
				2193,
				2192,
				2203,
				2195
			};

			// Token: 0x04007417 RID: 29719
			private static List<int> _manualGolfItemsOrder = new List<int>
			{
				4095,
				4596,
				4597,
				4595,
				4598,
				4592,
				4593,
				4591,
				4594,
				4092,
				4093,
				4039,
				4094,
				4588,
				4589,
				4587,
				4590,
				3989,
				4242,
				4243,
				4244,
				4245,
				4246,
				4247,
				4248,
				4249,
				4250,
				4251,
				4252,
				4253,
				4254,
				4255,
				4040,
				4086,
				4085,
				4088,
				4084,
				4083,
				4087
			};

			// Token: 0x02000E3F RID: 3647
			public enum ItemGroup
			{
				// Token: 0x04007CA6 RID: 31910
				Coin = 10,
				// Token: 0x04007CA7 RID: 31911
				CraftingObjects,
				// Token: 0x04007CA8 RID: 31912
				Torches = 20,
				// Token: 0x04007CA9 RID: 31913
				Glowsticks = 25,
				// Token: 0x04007CAA RID: 31914
				Wood = 30,
				// Token: 0x04007CAB RID: 31915
				Bombs = 40,
				// Token: 0x04007CAC RID: 31916
				LifePotions = 50,
				// Token: 0x04007CAD RID: 31917
				ManaPotions,
				// Token: 0x04007CAE RID: 31918
				BuffPotion,
				// Token: 0x04007CAF RID: 31919
				Flask,
				// Token: 0x04007CB0 RID: 31920
				Food,
				// Token: 0x04007CB1 RID: 31921
				Crates = 60,
				// Token: 0x04007CB2 RID: 31922
				BossBags = 70,
				// Token: 0x04007CB3 RID: 31923
				GoodieBags = 80,
				// Token: 0x04007CB4 RID: 31924
				AlchemyPlants = 83,
				// Token: 0x04007CB5 RID: 31925
				AlchemySeeds,
				// Token: 0x04007CB6 RID: 31926
				DyeMaterial = 87,
				// Token: 0x04007CB7 RID: 31927
				BossItem = 90,
				// Token: 0x04007CB8 RID: 31928
				EventItem,
				// Token: 0x04007CB9 RID: 31929
				ConsumableThatDoesNotDamage = 94,
				// Token: 0x04007CBA RID: 31930
				Solutions,
				// Token: 0x04007CBB RID: 31931
				Ammo,
				// Token: 0x04007CBC RID: 31932
				ConsumableThatDamages,
				// Token: 0x04007CBD RID: 31933
				PlacableObjects = 100,
				// Token: 0x04007CBE RID: 31934
				Blocks = 120,
				// Token: 0x04007CBF RID: 31935
				Wands = 130,
				// Token: 0x04007CC0 RID: 31936
				Rope = 140,
				// Token: 0x04007CC1 RID: 31937
				Walls = 150,
				// Token: 0x04007CC2 RID: 31938
				Wiring = 200,
				// Token: 0x04007CC3 RID: 31939
				Pickaxe = 500,
				// Token: 0x04007CC4 RID: 31940
				Axe = 510,
				// Token: 0x04007CC5 RID: 31941
				Hammer = 520,
				// Token: 0x04007CC6 RID: 31942
				MeleeWeapon = 530,
				// Token: 0x04007CC7 RID: 31943
				RangedWeapon = 540,
				// Token: 0x04007CC8 RID: 31944
				MagicWeapon = 550,
				// Token: 0x04007CC9 RID: 31945
				SummonWeapon = 560,
				// Token: 0x04007CCA RID: 31946
				Headgear = 600,
				// Token: 0x04007CCB RID: 31947
				Torso = 610,
				// Token: 0x04007CCC RID: 31948
				Pants = 620,
				// Token: 0x04007CCD RID: 31949
				Accessories = 630,
				// Token: 0x04007CCE RID: 31950
				Hook = 700,
				// Token: 0x04007CCF RID: 31951
				Mount = 710,
				// Token: 0x04007CD0 RID: 31952
				Minecart = 720,
				// Token: 0x04007CD1 RID: 31953
				VanityPet = 800,
				// Token: 0x04007CD2 RID: 31954
				LightPet = 810,
				// Token: 0x04007CD3 RID: 31955
				Golf = 900,
				// Token: 0x04007CD4 RID: 31956
				BossSpawners,
				// Token: 0x04007CD5 RID: 31957
				Dye = 910,
				// Token: 0x04007CD6 RID: 31958
				HairDye = 920,
				// Token: 0x04007CD7 RID: 31959
				Paint = 930,
				// Token: 0x04007CD8 RID: 31960
				FishingRods = 1000,
				// Token: 0x04007CD9 RID: 31961
				FishingQuestFish = 1010,
				// Token: 0x04007CDA RID: 31962
				Fish = 1015,
				// Token: 0x04007CDB RID: 31963
				FishingBait = 1020,
				// Token: 0x04007CDC RID: 31964
				Critters = 1030,
				// Token: 0x04007CDD RID: 31965
				Keys = 2000,
				// Token: 0x04007CDE RID: 31966
				RemainingUseItems = 5000,
				// Token: 0x04007CDF RID: 31967
				Material = 10000,
				// Token: 0x04007CE0 RID: 31968
				EverythingElse = 11000
			}

			// Token: 0x02000E40 RID: 3648
			public struct ItemGroupAndOrderInGroup
			{
				// Token: 0x06006597 RID: 26007 RVA: 0x006DFC54 File Offset: 0x006DDE54
				public ItemGroupAndOrderInGroup(Item item)
				{
					this.ItemType = item.type;
					try
					{
						this.Group = ContentSamples.CreativeHelper.GetItemGroup(item, out this.OrderInGroup);
					}
					catch (Exception e)
					{
						throw new Exception("An exception occurred while processing " + (ItemLoader.IsModItem(item.type) ? item.ModItem.FullName : item.Name), e);
					}
					ItemLoader.ModifyResearchSorting(item, ref this.Group);
				}

				// Token: 0x04007CE1 RID: 31969
				public int ItemType;

				// Token: 0x04007CE2 RID: 31970
				public ContentSamples.CreativeHelper.ItemGroup Group;

				// Token: 0x04007CE3 RID: 31971
				public int OrderInGroup;
			}
		}

		// Token: 0x02000B50 RID: 2896
		public static class BestiaryHelper
		{
			// Token: 0x06005C68 RID: 23656 RVA: 0x006AD2E4 File Offset: 0x006AB4E4
			public static List<KeyValuePair<int, NPC>> GetSortedBestiaryEntriesList(BestiaryDatabase database)
			{
				List<IBestiaryInfoElement> commonFilters = BestiaryDatabaseNPCsPopulator.CommonTags.GetCommonInfoElementsForFilters();
				List<KeyValuePair<int, NPC>> list = ContentSamples.NpcsByNetId.ToList<KeyValuePair<int, NPC>>().OrderBy(delegate(KeyValuePair<int, NPC> x)
				{
					KeyValuePair<int, NPC> keyValuePair = x;
					return ContentSamples.BestiaryHelper.GetBestiaryTownPriority(keyValuePair.Value);
				}).ThenBy(delegate(KeyValuePair<int, NPC> x)
				{
					KeyValuePair<int, NPC> keyValuePair = x;
					return !keyValuePair.Value.isLikeATownNPC;
				}).ThenBy(delegate(KeyValuePair<int, NPC> x)
				{
					KeyValuePair<int, NPC> keyValuePair = x;
					return ContentSamples.BestiaryHelper.GetBestiaryNormalGoldCritterPriority(keyValuePair.Value);
				}).ThenBy(delegate(KeyValuePair<int, NPC> x)
				{
					KeyValuePair<int, NPC> keyValuePair = x;
					return !keyValuePair.Value.CountsAsACritter;
				}).ThenBy(delegate(KeyValuePair<int, NPC> x)
				{
					KeyValuePair<int, NPC> keyValuePair = x;
					return ContentSamples.BestiaryHelper.GetBestiaryBossPriority(keyValuePair.Value);
				}).ThenBy(delegate(KeyValuePair<int, NPC> x)
				{
					KeyValuePair<int, NPC> keyValuePair = x;
					if (!keyValuePair.Value.boss)
					{
						bool[] shouldBeCountedAsBoss = NPCID.Sets.ShouldBeCountedAsBoss;
						keyValuePair = x;
						return shouldBeCountedAsBoss[keyValuePair.Value.type];
					}
					return true;
				}).ThenBy(delegate(KeyValuePair<int, NPC> x)
				{
					KeyValuePair<int, NPC> keyValuePair = x;
					return ContentSamples.BestiaryHelper.GetLowestBiomeGroupIndex(keyValuePair.Value, database, commonFilters);
				}).ThenBy(delegate(KeyValuePair<int, NPC> x)
				{
					KeyValuePair<int, NPC> keyValuePair = x;
					return keyValuePair.Value.aiStyle;
				}).ThenBy(delegate(KeyValuePair<int, NPC> x)
				{
					KeyValuePair<int, NPC> keyValuePair = x;
					return ContentSamples.BestiaryHelper.GetBestiaryPowerLevel(keyValuePair.Value);
				}).ThenBy(delegate(KeyValuePair<int, NPC> x)
				{
					KeyValuePair<int, NPC> keyValuePair = x;
					return ContentSamples.BestiaryHelper.GetBestiaryStarsPriority(keyValuePair.Value);
				}).ToList<KeyValuePair<int, NPC>>();
				list.RemoveAll((KeyValuePair<int, NPC> x) => ContentSamples.BestiaryHelper.ShouldHideBestiaryEntry(x.Value));
				return list;
			}

			// Token: 0x06005C69 RID: 23657 RVA: 0x006AD494 File Offset: 0x006AB694
			public static int GetLowestBiomeGroupIndex(NPC npc, BestiaryDatabase database, List<IBestiaryInfoElement> commonElements)
			{
				List<IBestiaryInfoElement> info = database.FindEntryByNPCID(npc.netID).Info;
				for (int num = commonElements.Count - 1; num >= 0; num--)
				{
					if (info.IndexOf(commonElements[num]) != -1)
					{
						return num;
					}
				}
				return int.MaxValue;
			}

			// Token: 0x06005C6A RID: 23658 RVA: 0x006AD4E0 File Offset: 0x006AB6E0
			public static bool ShouldHideBestiaryEntry(NPC npc)
			{
				NPCID.Sets.NPCBestiaryDrawModifiers value;
				return NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(npc.netID, out value) && value.Hide;
			}

			// Token: 0x06005C6B RID: 23659 RVA: 0x006AD509 File Offset: 0x006AB709
			public static float GetBestiaryPowerLevel(NPC npc)
			{
				return (float)(npc.damage + npc.defense + npc.lifeMax / 4);
			}

			// Token: 0x06005C6C RID: 23660 RVA: 0x006AD524 File Offset: 0x006AB724
			public static int GetBestiaryTownPriority(NPC npc)
			{
				int num = NPCID.Sets.TownNPCBestiaryPriority.IndexOf(npc.netID);
				if (num == -1)
				{
					num = int.MaxValue;
				}
				return num;
			}

			// Token: 0x06005C6D RID: 23661 RVA: 0x006AD550 File Offset: 0x006AB750
			public static int GetBestiaryNormalGoldCritterPriority(NPC npc)
			{
				int num = NPCID.Sets.NormalGoldCritterBestiaryPriority.IndexOf(npc.netID);
				if (num == -1)
				{
					num = int.MaxValue;
				}
				return num;
			}

			// Token: 0x06005C6E RID: 23662 RVA: 0x006AD579 File Offset: 0x006AB779
			public static int GetBestiaryBossPriority(NPC npc)
			{
				return NPCID.Sets.BossBestiaryPriority.IndexOf(npc.netID);
			}

			// Token: 0x06005C6F RID: 23663 RVA: 0x006AD58B File Offset: 0x006AB78B
			public static int GetBestiaryStarsPriority(NPC npc)
			{
				return ContentSamples.NpcBestiaryRarityStars[npc.type];
			}
		}
	}
}
