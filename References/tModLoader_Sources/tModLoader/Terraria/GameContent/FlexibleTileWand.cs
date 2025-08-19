using System;
using System.Collections.Generic;
using Terraria.Utilities;

namespace Terraria.GameContent
{
	// Token: 0x02000499 RID: 1177
	public class FlexibleTileWand
	{
		// Token: 0x06003927 RID: 14631 RVA: 0x00596964 File Offset: 0x00594B64
		public void AddVariation(int itemType, int tileIdToPlace, int tileStyleToPlace)
		{
			FlexibleTileWand.OptionBucket value;
			if (!this._options.TryGetValue(itemType, out value))
			{
				value = (this._options[itemType] = new FlexibleTileWand.OptionBucket(itemType));
			}
			value.Options.Add(new FlexibleTileWand.PlacementOption
			{
				TileIdToPlace = tileIdToPlace,
				TileStyleToPlace = tileStyleToPlace
			});
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x005969B8 File Offset: 0x00594BB8
		public void AddVariations(int itemType, int tileIdToPlace, params int[] stylesToPlace)
		{
			foreach (int tileStyleToPlace in stylesToPlace)
			{
				this.AddVariation(itemType, tileIdToPlace, tileStyleToPlace);
			}
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x005969E4 File Offset: 0x00594BE4
		public void AddVariations_ByRow(int itemType, int tileIdToPlace, int variationsPerRow, params int[] rows)
		{
			for (int i = 0; i < rows.Length; i++)
			{
				for (int j = 0; j < variationsPerRow; j++)
				{
					int tileStyleToPlace = rows[i] * variationsPerRow + j;
					this.AddVariation(itemType, tileIdToPlace, tileStyleToPlace);
				}
			}
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x00596A20 File Offset: 0x00594C20
		public bool TryGetPlacementOption(Player player, int randomSeed, int selectCycleOffset, out FlexibleTileWand.PlacementOption option, out Item itemToConsume)
		{
			option = null;
			itemToConsume = null;
			Item[] inventory = player.inventory;
			for (int i = 0; i < 58; i++)
			{
				if (i < 50 || i >= 54)
				{
					Item item = inventory[i];
					FlexibleTileWand.OptionBucket value;
					if (!item.IsAir && this._options.TryGetValue(item.type, out value))
					{
						this._random.SetSeed(randomSeed);
						option = value.GetOptionWithCycling(selectCycleOffset);
						itemToConsume = item;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x00596A94 File Offset: 0x00594C94
		public static FlexibleTileWand CreateRubblePlacerLarge()
		{
			FlexibleTileWand flexibleTileWand = new FlexibleTileWand();
			int tileIdToPlace = 647;
			flexibleTileWand.AddVariations(154, tileIdToPlace, new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6
			});
			flexibleTileWand.AddVariations(3, tileIdToPlace, new int[]
			{
				7,
				8,
				9,
				10,
				11,
				12,
				13,
				14,
				15
			});
			flexibleTileWand.AddVariations(71, tileIdToPlace, new int[]
			{
				16,
				17
			});
			flexibleTileWand.AddVariations(72, tileIdToPlace, new int[]
			{
				18,
				19
			});
			flexibleTileWand.AddVariations(73, tileIdToPlace, new int[]
			{
				20,
				21
			});
			flexibleTileWand.AddVariations(9, tileIdToPlace, new int[]
			{
				22,
				23,
				24,
				25
			});
			flexibleTileWand.AddVariations(593, tileIdToPlace, new int[]
			{
				26,
				27,
				28,
				29,
				30,
				31
			});
			flexibleTileWand.AddVariations(183, tileIdToPlace, new int[]
			{
				32,
				33,
				34
			});
			tileIdToPlace = 648;
			flexibleTileWand.AddVariations(195, tileIdToPlace, new int[]
			{
				0,
				1,
				2
			});
			flexibleTileWand.AddVariations(195, tileIdToPlace, new int[]
			{
				3,
				4,
				5
			});
			flexibleTileWand.AddVariations(174, tileIdToPlace, new int[]
			{
				6,
				7,
				8
			});
			flexibleTileWand.AddVariations(150, tileIdToPlace, new int[]
			{
				9,
				10,
				11,
				12,
				13
			});
			flexibleTileWand.AddVariations(3, tileIdToPlace, new int[]
			{
				14,
				15,
				16
			});
			flexibleTileWand.AddVariations(989, tileIdToPlace, new int[]
			{
				17
			});
			flexibleTileWand.AddVariations(1101, tileIdToPlace, new int[]
			{
				18,
				19,
				20
			});
			flexibleTileWand.AddVariations(9, tileIdToPlace, new int[]
			{
				21,
				22
			});
			flexibleTileWand.AddVariations(9, tileIdToPlace, new int[]
			{
				23,
				24,
				25,
				26,
				27,
				28
			});
			flexibleTileWand.AddVariations(3271, tileIdToPlace, new int[]
			{
				29,
				30,
				31,
				32,
				33,
				34
			});
			flexibleTileWand.AddVariations(3086, tileIdToPlace, new int[]
			{
				35,
				36,
				37,
				38,
				39,
				40
			});
			flexibleTileWand.AddVariations(3081, tileIdToPlace, new int[]
			{
				41,
				42,
				43,
				44,
				45,
				46
			});
			flexibleTileWand.AddVariations(62, tileIdToPlace, new int[]
			{
				47,
				48,
				49
			});
			flexibleTileWand.AddVariations(62, tileIdToPlace, new int[]
			{
				50,
				51
			});
			flexibleTileWand.AddVariations(154, tileIdToPlace, new int[]
			{
				52,
				53,
				54
			});
			tileIdToPlace = 651;
			flexibleTileWand.AddVariations(195, tileIdToPlace, new int[]
			{
				0,
				1,
				2
			});
			flexibleTileWand.AddVariations(62, tileIdToPlace, new int[]
			{
				3,
				4,
				5
			});
			flexibleTileWand.AddVariations(331, tileIdToPlace, new int[]
			{
				6,
				7,
				8
			});
			return flexibleTileWand;
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x00596D7C File Offset: 0x00594F7C
		public static FlexibleTileWand CreateRubblePlacerMedium()
		{
			FlexibleTileWand flexibleTileWand = new FlexibleTileWand();
			ushort tileIdToPlace = 652;
			flexibleTileWand.AddVariations(195, (int)tileIdToPlace, new int[]
			{
				0,
				1,
				2
			});
			flexibleTileWand.AddVariations(62, (int)tileIdToPlace, new int[]
			{
				3,
				4,
				5
			});
			flexibleTileWand.AddVariations(331, (int)tileIdToPlace, new int[]
			{
				6,
				7,
				8,
				9,
				10,
				11
			});
			tileIdToPlace = 649;
			flexibleTileWand.AddVariations(3, (int)tileIdToPlace, new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5
			});
			flexibleTileWand.AddVariations(154, (int)tileIdToPlace, new int[]
			{
				6,
				7,
				8,
				9,
				10
			});
			flexibleTileWand.AddVariations(154, (int)tileIdToPlace, new int[]
			{
				11,
				12,
				13,
				14,
				15
			});
			flexibleTileWand.AddVariations(71, (int)tileIdToPlace, new int[]
			{
				16
			});
			flexibleTileWand.AddVariations(72, (int)tileIdToPlace, new int[]
			{
				17
			});
			flexibleTileWand.AddVariations(73, (int)tileIdToPlace, new int[]
			{
				18
			});
			flexibleTileWand.AddVariations(181, (int)tileIdToPlace, new int[]
			{
				19
			});
			flexibleTileWand.AddVariations(180, (int)tileIdToPlace, new int[]
			{
				20
			});
			flexibleTileWand.AddVariations(177, (int)tileIdToPlace, new int[]
			{
				21
			});
			flexibleTileWand.AddVariations(179, (int)tileIdToPlace, new int[]
			{
				22
			});
			flexibleTileWand.AddVariations(178, (int)tileIdToPlace, new int[]
			{
				23
			});
			flexibleTileWand.AddVariations(182, (int)tileIdToPlace, new int[]
			{
				24
			});
			flexibleTileWand.AddVariations(593, (int)tileIdToPlace, new int[]
			{
				25,
				26,
				27,
				28,
				29,
				30
			});
			flexibleTileWand.AddVariations(9, (int)tileIdToPlace, new int[]
			{
				31,
				32,
				33
			});
			flexibleTileWand.AddVariations(150, (int)tileIdToPlace, new int[]
			{
				34,
				35,
				36,
				37
			});
			flexibleTileWand.AddVariations(3, (int)tileIdToPlace, new int[]
			{
				38,
				39,
				40
			});
			flexibleTileWand.AddVariations(3271, (int)tileIdToPlace, new int[]
			{
				41,
				42,
				43,
				44,
				45,
				46
			});
			flexibleTileWand.AddVariations(3086, (int)tileIdToPlace, new int[]
			{
				47,
				48,
				49,
				50,
				51,
				52
			});
			flexibleTileWand.AddVariations(3081, (int)tileIdToPlace, new int[]
			{
				53,
				54,
				55,
				56,
				57,
				58
			});
			flexibleTileWand.AddVariations(62, (int)tileIdToPlace, new int[]
			{
				59,
				60,
				61
			});
			flexibleTileWand.AddVariations(169, (int)tileIdToPlace, new int[]
			{
				62,
				63,
				64
			});
			return flexibleTileWand;
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x00597004 File Offset: 0x00595204
		public static FlexibleTileWand CreateRubblePlacerSmall()
		{
			FlexibleTileWand flexibleTileWand = new FlexibleTileWand();
			ushort tileIdToPlace = 650;
			flexibleTileWand.AddVariations(3, (int)tileIdToPlace, new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5
			});
			flexibleTileWand.AddVariations(2, (int)tileIdToPlace, new int[]
			{
				6,
				7,
				8,
				9,
				10,
				11
			});
			flexibleTileWand.AddVariations(154, (int)tileIdToPlace, new int[]
			{
				12,
				13,
				14,
				15,
				16,
				17,
				18,
				19
			});
			flexibleTileWand.AddVariations(154, (int)tileIdToPlace, new int[]
			{
				20,
				21,
				22,
				23,
				24,
				25,
				26,
				27
			});
			flexibleTileWand.AddVariations(9, (int)tileIdToPlace, new int[]
			{
				28,
				29,
				30,
				31,
				32
			});
			flexibleTileWand.AddVariations(9, (int)tileIdToPlace, new int[]
			{
				33,
				34,
				35
			});
			flexibleTileWand.AddVariations(593, (int)tileIdToPlace, new int[]
			{
				36,
				37,
				38,
				39,
				40,
				41
			});
			flexibleTileWand.AddVariations(664, (int)tileIdToPlace, new int[]
			{
				42,
				43,
				44,
				45,
				46,
				47
			});
			flexibleTileWand.AddVariations(150, (int)tileIdToPlace, new int[]
			{
				48,
				49,
				50,
				51,
				52,
				53
			});
			flexibleTileWand.AddVariations(3271, (int)tileIdToPlace, new int[]
			{
				54,
				55,
				56,
				57,
				58,
				59
			});
			flexibleTileWand.AddVariations(3086, (int)tileIdToPlace, new int[]
			{
				60,
				61,
				62,
				63,
				64,
				65
			});
			flexibleTileWand.AddVariations(3081, (int)tileIdToPlace, new int[]
			{
				66,
				67,
				68,
				69,
				70,
				71
			});
			flexibleTileWand.AddVariations(62, (int)tileIdToPlace, new int[]
			{
				72
			});
			flexibleTileWand.AddVariations(169, (int)tileIdToPlace, new int[]
			{
				73,
				74,
				75,
				76
			});
			return flexibleTileWand;
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x005971A0 File Offset: 0x005953A0
		public static void ForModders_AddPotsToWand(FlexibleTileWand wand, ref int echoPileStyle, ref ushort tileType)
		{
			int variationsPerRow = 3;
			echoPileStyle = 0;
			tileType = 653;
			wand.AddVariations_ByRow(133, (int)tileType, variationsPerRow, new int[]
			{
				0,
				1,
				2,
				3
			});
			wand.AddVariations_ByRow(664, (int)tileType, variationsPerRow, new int[]
			{
				4,
				5,
				6
			});
			wand.AddVariations_ByRow(176, (int)tileType, variationsPerRow, new int[]
			{
				7,
				8,
				9
			});
			wand.AddVariations_ByRow(154, (int)tileType, variationsPerRow, new int[]
			{
				10,
				11,
				12
			});
			wand.AddVariations_ByRow(173, (int)tileType, variationsPerRow, new int[]
			{
				13,
				14,
				15
			});
			wand.AddVariations_ByRow(61, (int)tileType, variationsPerRow, new int[]
			{
				16,
				17,
				18
			});
			wand.AddVariations_ByRow(150, (int)tileType, variationsPerRow, new int[]
			{
				19,
				20,
				21
			});
			wand.AddVariations_ByRow(836, (int)tileType, variationsPerRow, new int[]
			{
				22,
				23,
				24
			});
			wand.AddVariations_ByRow(607, (int)tileType, variationsPerRow, new int[]
			{
				25,
				26,
				27
			});
			wand.AddVariations_ByRow(1101, (int)tileType, variationsPerRow, new int[]
			{
				28,
				29,
				30
			});
			wand.AddVariations_ByRow(3081, (int)tileType, variationsPerRow, new int[]
			{
				31,
				32,
				33
			});
			wand.AddVariations_ByRow(607, (int)tileType, variationsPerRow, new int[]
			{
				34,
				35,
				36
			});
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x0059732A File Offset: 0x0059552A
		public static void Reload()
		{
			FlexibleTileWand.RubblePlacementSmall = FlexibleTileWand.CreateRubblePlacerSmall();
			FlexibleTileWand.RubblePlacementMedium = FlexibleTileWand.CreateRubblePlacerMedium();
			FlexibleTileWand.RubblePlacementLarge = FlexibleTileWand.CreateRubblePlacerLarge();
		}

		/// <summary> Used by the Rubblemaker item to place fake versions of 1x1 rubble tiles. Technically it can place any size rubble. </summary>
		// Token: 0x04005241 RID: 21057
		public static FlexibleTileWand RubblePlacementSmall = FlexibleTileWand.CreateRubblePlacerSmall();

		/// <summary> Used by the Rubblemaker item to place fake versions of 2x1 rubble tiles. Technically it can place any size rubble. </summary>
		// Token: 0x04005242 RID: 21058
		public static FlexibleTileWand RubblePlacementMedium = FlexibleTileWand.CreateRubblePlacerMedium();

		/// <summary> Used by the Rubblemaker item to place fake versions of 3x2 rubble tiles. Technically it can place any size rubble. </summary>
		// Token: 0x04005243 RID: 21059
		public static FlexibleTileWand RubblePlacementLarge = FlexibleTileWand.CreateRubblePlacerLarge();

		// Token: 0x04005244 RID: 21060
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x04005245 RID: 21061
		private Dictionary<int, FlexibleTileWand.OptionBucket> _options = new Dictionary<int, FlexibleTileWand.OptionBucket>();

		// Token: 0x02000BA7 RID: 2983
		private class OptionBucket
		{
			// Token: 0x06005D7B RID: 23931 RVA: 0x006C8B81 File Offset: 0x006C6D81
			public OptionBucket(int itemTypeToConsume)
			{
				this.ItemTypeToConsume = itemTypeToConsume;
				this.Options = new List<FlexibleTileWand.PlacementOption>();
			}

			// Token: 0x06005D7C RID: 23932 RVA: 0x006C8B9B File Offset: 0x006C6D9B
			public FlexibleTileWand.PlacementOption GetRandomOption(UnifiedRandom random)
			{
				return this.Options[random.Next(this.Options.Count)];
			}

			// Token: 0x06005D7D RID: 23933 RVA: 0x006C8BBC File Offset: 0x006C6DBC
			public FlexibleTileWand.PlacementOption GetOptionWithCycling(int cycleOffset)
			{
				int count = this.Options.Count;
				int index = (cycleOffset % count + count) % count;
				return this.Options[index];
			}

			// Token: 0x040076B4 RID: 30388
			public int ItemTypeToConsume;

			// Token: 0x040076B5 RID: 30389
			public List<FlexibleTileWand.PlacementOption> Options;
		}

		// Token: 0x02000BA8 RID: 2984
		public class PlacementOption
		{
			// Token: 0x040076B6 RID: 30390
			public int TileIdToPlace;

			// Token: 0x040076B7 RID: 30391
			public int TileStyleToPlace;
		}
	}
}
