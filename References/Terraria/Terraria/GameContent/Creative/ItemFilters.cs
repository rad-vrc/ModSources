using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
	// Token: 0x020002BB RID: 699
	public static class ItemFilters
	{
		// Token: 0x040047C8 RID: 18376
		private const int framesPerRow = 11;

		// Token: 0x040047C9 RID: 18377
		private const int framesPerColumn = 1;

		// Token: 0x040047CA RID: 18378
		private const int frameSizeOffsetX = -2;

		// Token: 0x040047CB RID: 18379
		private const int frameSizeOffsetY = 0;

		// Token: 0x0200069A RID: 1690
		public class BySearch : IItemEntryFilter, IEntryFilter<Item>, ISearchFilter<Item>
		{
			// Token: 0x06003531 RID: 13617 RVA: 0x006097A0 File Offset: 0x006079A0
			public bool FitsFilter(Item entry)
			{
				if (this._search == null)
				{
					return true;
				}
				int num = 1;
				float knockBack = entry.knockBack;
				Main.MouseText_DrawItemTooltip_GetLinesInfo(entry, ref this._unusedYoyoLogo, ref this._unusedResearchLine, knockBack, ref num, this._toolTipLines, this._unusedPrefixLine, this._unusedBadPrefixLines);
				for (int i = 0; i < num; i++)
				{
					if (this._toolTipLines[i].ToLower().IndexOf(this._search, StringComparison.OrdinalIgnoreCase) != -1)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06003532 RID: 13618 RVA: 0x00609812 File Offset: 0x00607A12
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabSearch";
			}

			// Token: 0x06003533 RID: 13619 RVA: 0x00609819 File Offset: 0x00607A19
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Rank_Light", 1);
				return new UIImageFramed(asset, asset.Frame(1, 1, 0, 0, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x06003534 RID: 13620 RVA: 0x00609852 File Offset: 0x00607A52
			public void SetSearch(string searchText)
			{
				this._search = searchText;
			}

			// Token: 0x040061D7 RID: 25047
			private const int _tooltipMaxLines = 30;

			// Token: 0x040061D8 RID: 25048
			private string[] _toolTipLines = new string[30];

			// Token: 0x040061D9 RID: 25049
			private bool[] _unusedPrefixLine = new bool[30];

			// Token: 0x040061DA RID: 25050
			private bool[] _unusedBadPrefixLines = new bool[30];

			// Token: 0x040061DB RID: 25051
			private int _unusedYoyoLogo;

			// Token: 0x040061DC RID: 25052
			private int _unusedResearchLine;

			// Token: 0x040061DD RID: 25053
			private string _search;
		}

		// Token: 0x0200069B RID: 1691
		public class BuildingBlock : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06003535 RID: 13621 RVA: 0x0060985B File Offset: 0x00607A5B
			public bool FitsFilter(Item entry)
			{
				return entry.createWall != -1 || entry.tileWand != -1 || (entry.createTile != -1 && !Main.tileFrameImportant[entry.createTile]);
			}

			// Token: 0x06003536 RID: 13622 RVA: 0x0060988D File Offset: 0x00607A8D
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabBlocks";
			}

			// Token: 0x06003537 RID: 13623 RVA: 0x00609894 File Offset: 0x00607A94
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 4, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x0200069C RID: 1692
		public class Furniture : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06003539 RID: 13625 RVA: 0x006098E4 File Offset: 0x00607AE4
			public bool FitsFilter(Item entry)
			{
				int createTile = entry.createTile;
				return createTile != -1 && Main.tileFrameImportant[createTile];
			}

			// Token: 0x0600353A RID: 13626 RVA: 0x0060990B File Offset: 0x00607B0B
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabFurniture";
			}

			// Token: 0x0600353B RID: 13627 RVA: 0x00609914 File Offset: 0x00607B14
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 7, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x0200069D RID: 1693
		public class Tools : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x0600353D RID: 13629 RVA: 0x00609964 File Offset: 0x00607B64
			public bool FitsFilter(Item entry)
			{
				return entry.pick > 0 || entry.axe > 0 || entry.hammer > 0 || entry.fishingPole > 0 || entry.tileWand != -1 || this._itemIdsThatAreAccepted.Contains(entry.type);
			}

			// Token: 0x0600353E RID: 13630 RVA: 0x006099BE File Offset: 0x00607BBE
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabTools";
			}

			// Token: 0x0600353F RID: 13631 RVA: 0x006099C8 File Offset: 0x00607BC8
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 6, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x040061DE RID: 25054
			private HashSet<int> _itemIdsThatAreAccepted = new HashSet<int>
			{
				509,
				850,
				851,
				3612,
				3625,
				3611,
				510,
				849,
				3620,
				1071,
				1543,
				1072,
				1544,
				1100,
				1545,
				50,
				3199,
				3124,
				5358,
				5359,
				5360,
				5361,
				5437,
				1326,
				5335,
				3384,
				4263,
				4819,
				4262,
				946,
				4707,
				205,
				206,
				207,
				1128,
				3031,
				4820,
				5302,
				5364,
				4460,
				4608,
				4872,
				3032,
				5303,
				5304,
				1991,
				4821,
				3183,
				779,
				5134,
				1299,
				4711,
				4049,
				114
			};
		}

		// Token: 0x0200069E RID: 1694
		public class Weapon : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06003541 RID: 13633 RVA: 0x00609CB8 File Offset: 0x00607EB8
			public bool FitsFilter(Item entry)
			{
				return entry.damage > 0;
			}

			// Token: 0x06003542 RID: 13634 RVA: 0x00609CC3 File Offset: 0x00607EC3
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabWeapons";
			}

			// Token: 0x06003543 RID: 13635 RVA: 0x00609CCC File Offset: 0x00607ECC
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 0, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x0200069F RID: 1695
		public abstract class AArmor
		{
			// Token: 0x06003545 RID: 13637 RVA: 0x00609D19 File Offset: 0x00607F19
			public bool IsAnArmorThatMatchesSocialState(Item entry, bool shouldBeSocial)
			{
				return (entry.bodySlot != -1 || entry.headSlot != -1 || entry.legSlot != -1) && entry.vanity == shouldBeSocial;
			}
		}

		// Token: 0x020006A0 RID: 1696
		public class Armor : ItemFilters.AArmor, IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06003547 RID: 13639 RVA: 0x00609D49 File Offset: 0x00607F49
			public bool FitsFilter(Item entry)
			{
				return base.IsAnArmorThatMatchesSocialState(entry, false);
			}

			// Token: 0x06003548 RID: 13640 RVA: 0x00609D53 File Offset: 0x00607F53
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabArmor";
			}

			// Token: 0x06003549 RID: 13641 RVA: 0x00609D5C File Offset: 0x00607F5C
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 2, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x020006A1 RID: 1697
		public class Vanity : ItemFilters.AArmor, IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x0600354B RID: 13643 RVA: 0x00609DB1 File Offset: 0x00607FB1
			public bool FitsFilter(Item entry)
			{
				return base.IsAnArmorThatMatchesSocialState(entry, true);
			}

			// Token: 0x0600354C RID: 13644 RVA: 0x00609DBB File Offset: 0x00607FBB
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabVanity";
			}

			// Token: 0x0600354D RID: 13645 RVA: 0x00609DC4 File Offset: 0x00607FC4
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 8, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x020006A2 RID: 1698
		public abstract class AAccessories
		{
			// Token: 0x0600354F RID: 13647 RVA: 0x00609E14 File Offset: 0x00608014
			public bool IsAnAccessoryOfType(Item entry, ItemFilters.AAccessories.AccessoriesCategory categoryType)
			{
				bool flag = ItemSlot.IsMiscEquipment(entry);
				return (flag && categoryType == ItemFilters.AAccessories.AccessoriesCategory.Misc) || (!flag && categoryType == ItemFilters.AAccessories.AccessoriesCategory.NonMisc && entry.accessory);
			}

			// Token: 0x0200083F RID: 2111
			public enum AccessoriesCategory
			{
				// Token: 0x040065D7 RID: 26071
				Misc,
				// Token: 0x040065D8 RID: 26072
				NonMisc
			}
		}

		// Token: 0x020006A3 RID: 1699
		public class Accessories : ItemFilters.AAccessories, IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06003551 RID: 13649 RVA: 0x00609E42 File Offset: 0x00608042
			public bool FitsFilter(Item entry)
			{
				return base.IsAnAccessoryOfType(entry, ItemFilters.AAccessories.AccessoriesCategory.NonMisc);
			}

			// Token: 0x06003552 RID: 13650 RVA: 0x00609E4C File Offset: 0x0060804C
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabAccessories";
			}

			// Token: 0x06003553 RID: 13651 RVA: 0x00609E54 File Offset: 0x00608054
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 1, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x020006A4 RID: 1700
		public class MiscAccessories : ItemFilters.AAccessories, IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06003555 RID: 13653 RVA: 0x00609EA9 File Offset: 0x006080A9
			public bool FitsFilter(Item entry)
			{
				return base.IsAnAccessoryOfType(entry, ItemFilters.AAccessories.AccessoriesCategory.Misc);
			}

			// Token: 0x06003556 RID: 13654 RVA: 0x00609EB3 File Offset: 0x006080B3
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabAccessoriesMisc";
			}

			// Token: 0x06003557 RID: 13655 RVA: 0x00609EBC File Offset: 0x006080BC
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 9, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x020006A5 RID: 1701
		public class Consumables : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06003559 RID: 13657 RVA: 0x00609F0C File Offset: 0x0060810C
			public bool FitsFilter(Item entry)
			{
				int type = entry.type;
				if (type == 267 || type == 1307)
				{
					return true;
				}
				bool flag = entry.createTile != -1 || entry.createWall != -1 || entry.tileWand != -1;
				return entry.consumable && !flag;
			}

			// Token: 0x0600355A RID: 13658 RVA: 0x00609F62 File Offset: 0x00608162
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabConsumables";
			}

			// Token: 0x0600355B RID: 13659 RVA: 0x00609F6C File Offset: 0x0060816C
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 3, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x020006A6 RID: 1702
		public class GameplayItems : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x0600355D RID: 13661 RVA: 0x00609FB9 File Offset: 0x006081B9
			public bool FitsFilter(Item entry)
			{
				return ItemID.Sets.SortingPriorityBossSpawns[entry.type] != -1;
			}

			// Token: 0x0600355E RID: 13662 RVA: 0x00609FCD File Offset: 0x006081CD
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabMisc";
			}

			// Token: 0x0600355F RID: 13663 RVA: 0x00609FD4 File Offset: 0x006081D4
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 5, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x020006A7 RID: 1703
		public class MiscFallback : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06003561 RID: 13665 RVA: 0x0060A024 File Offset: 0x00608224
			public MiscFallback(List<IItemEntryFilter> otherFiltersToCheckAgainst)
			{
				short count = ItemID.Count;
				this._fitsFilterByItemType = new bool[(int)count];
				for (int i = 1; i < (int)count; i++)
				{
					this._fitsFilterByItemType[i] = true;
					Item entry = ContentSamples.ItemsByType[i];
					int num;
					if (!CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(i, out num))
					{
						this._fitsFilterByItemType[i] = false;
					}
					else
					{
						for (int j = 0; j < otherFiltersToCheckAgainst.Count; j++)
						{
							if (otherFiltersToCheckAgainst[j].FitsFilter(entry))
							{
								this._fitsFilterByItemType[i] = false;
								break;
							}
						}
					}
				}
			}

			// Token: 0x06003562 RID: 13666 RVA: 0x0060A0B4 File Offset: 0x006082B4
			public bool FitsFilter(Item entry)
			{
				return this._fitsFilterByItemType.IndexInRange(entry.type) && this._fitsFilterByItemType[entry.type];
			}

			// Token: 0x06003563 RID: 13667 RVA: 0x00609FCD File Offset: 0x006081CD
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabMisc";
			}

			// Token: 0x06003564 RID: 13668 RVA: 0x0060A0D8 File Offset: 0x006082D8
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 5, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x040061DF RID: 25055
			private bool[] _fitsFilterByItemType;
		}

		// Token: 0x020006A8 RID: 1704
		public class Materials : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06003565 RID: 13669 RVA: 0x0060A125 File Offset: 0x00608325
			public bool FitsFilter(Item entry)
			{
				return entry.material;
			}

			// Token: 0x06003566 RID: 13670 RVA: 0x0060A12D File Offset: 0x0060832D
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabMaterials";
			}

			// Token: 0x06003567 RID: 13671 RVA: 0x0060A134 File Offset: 0x00608334
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 10, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}
	}
}
