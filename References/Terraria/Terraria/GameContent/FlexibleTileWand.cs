using System;
using System.Collections.Generic;
using Terraria.Utilities;

namespace Terraria.GameContent
{
	// Token: 0x020001C9 RID: 457
	public class FlexibleTileWand
	{
		// Token: 0x06001BCA RID: 7114 RVA: 0x004EF8F8 File Offset: 0x004EDAF8
		public void AddVariation(int itemType, int tileIdToPlace, int tileStyleToPlace)
		{
			FlexibleTileWand.OptionBucket optionBucket;
			if (!this._options.TryGetValue(itemType, out optionBucket))
			{
				optionBucket = (this._options[itemType] = new FlexibleTileWand.OptionBucket(itemType));
			}
			optionBucket.Options.Add(new FlexibleTileWand.PlacementOption
			{
				TileIdToPlace = tileIdToPlace,
				TileStyleToPlace = tileStyleToPlace
			});
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x004EF94C File Offset: 0x004EDB4C
		public void AddVariations(int itemType, int tileIdToPlace, params int[] stylesToPlace)
		{
			foreach (int tileStyleToPlace in stylesToPlace)
			{
				this.AddVariation(itemType, tileIdToPlace, tileStyleToPlace);
			}
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x004EF974 File Offset: 0x004EDB74
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

		// Token: 0x06001BCD RID: 7117 RVA: 0x004EF9B0 File Offset: 0x004EDBB0
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
					FlexibleTileWand.OptionBucket optionBucket;
					if (!item.IsAir && this._options.TryGetValue(item.type, out optionBucket))
					{
						this._random.SetSeed(randomSeed);
						option = optionBucket.GetOptionWithCycling(selectCycleOffset);
						itemToConsume = item;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x004EFA24 File Offset: 0x004EDC24
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

		// Token: 0x06001BCF RID: 7119 RVA: 0x004EFD0C File Offset: 0x004EDF0C
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

		// Token: 0x06001BD0 RID: 7120 RVA: 0x004EFF94 File Offset: 0x004EE194
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

		// Token: 0x06001BD1 RID: 7121 RVA: 0x004F0130 File Offset: 0x004EE330
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

		// Token: 0x0400434A RID: 17226
		public static FlexibleTileWand RubblePlacementSmall = FlexibleTileWand.CreateRubblePlacerSmall();

		// Token: 0x0400434B RID: 17227
		public static FlexibleTileWand RubblePlacementMedium = FlexibleTileWand.CreateRubblePlacerMedium();

		// Token: 0x0400434C RID: 17228
		public static FlexibleTileWand RubblePlacementLarge = FlexibleTileWand.CreateRubblePlacerLarge();

		// Token: 0x0400434D RID: 17229
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x0400434E RID: 17230
		private Dictionary<int, FlexibleTileWand.OptionBucket> _options = new Dictionary<int, FlexibleTileWand.OptionBucket>();

		// Token: 0x020005EF RID: 1519
		private class OptionBucket
		{
			// Token: 0x06003304 RID: 13060 RVA: 0x00604CC7 File Offset: 0x00602EC7
			public OptionBucket(int itemTypeToConsume)
			{
				this.ItemTypeToConsume = itemTypeToConsume;
				this.Options = new List<FlexibleTileWand.PlacementOption>();
			}

			// Token: 0x06003305 RID: 13061 RVA: 0x00604CE1 File Offset: 0x00602EE1
			public FlexibleTileWand.PlacementOption GetRandomOption(UnifiedRandom random)
			{
				return this.Options[random.Next(this.Options.Count)];
			}

			// Token: 0x06003306 RID: 13062 RVA: 0x00604D00 File Offset: 0x00602F00
			public FlexibleTileWand.PlacementOption GetOptionWithCycling(int cycleOffset)
			{
				int count = this.Options.Count;
				int index = (cycleOffset % count + count) % count;
				return this.Options[index];
			}

			// Token: 0x04005FFC RID: 24572
			public int ItemTypeToConsume;

			// Token: 0x04005FFD RID: 24573
			public List<FlexibleTileWand.PlacementOption> Options;
		}

		// Token: 0x020005F0 RID: 1520
		public class PlacementOption
		{
			// Token: 0x04005FFE RID: 24574
			public int TileIdToPlace;

			// Token: 0x04005FFF RID: 24575
			public int TileStyleToPlace;
		}
	}
}
