using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
	// Token: 0x0200064C RID: 1612
	public static class ItemFilters
	{
		// Token: 0x04005B84 RID: 23428
		private const int framesPerRow = 11;

		// Token: 0x04005B85 RID: 23429
		private const int framesPerColumn = 1;

		// Token: 0x04005B86 RID: 23430
		private const int frameSizeOffsetX = -2;

		// Token: 0x04005B87 RID: 23431
		private const int frameSizeOffsetY = 0;

		// Token: 0x02000CF3 RID: 3315
		public class BySearch : IItemEntryFilter, IEntryFilter<Item>, ISearchFilter<Item>
		{
			// Token: 0x06006293 RID: 25235 RVA: 0x006D6C98 File Offset: 0x006D4E98
			public bool FitsFilter(Item entry)
			{
				if (this._search == null)
				{
					return true;
				}
				this._toolTipNames = new string[30];
				this._toolTipLines = new string[30];
				this._unusedPrefixLine = new bool[30];
				this._unusedBadPrefixLines = new bool[30];
				int numLines = 1;
				float knockBack = entry.knockBack;
				int num;
				Main.MouseText_DrawItemTooltip_GetLinesInfo(entry, ref this._unusedYoyoLogo, ref this._unusedResearchLine, knockBack, ref numLines, this._toolTipLines, this._unusedPrefixLine, this._unusedBadPrefixLines, this._toolTipNames, out num);
				Color?[] array;
				using (List<TooltipLine>.Enumerator enumerator = ItemLoader.ModifyTooltips(entry, ref numLines, this._toolTipNames, ref this._toolTipLines, ref this._unusedPrefixLine, ref this._unusedBadPrefixLines, ref this._unusedYoyoLogo, out array, -1).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Text.ToLower().IndexOf(this._search, StringComparison.OrdinalIgnoreCase) != -1)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x06006294 RID: 25236 RVA: 0x006D6DA0 File Offset: 0x006D4FA0
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabSearch";
			}

			// Token: 0x06006295 RID: 25237 RVA: 0x006D6DA7 File Offset: 0x006D4FA7
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Rank_Light");
				return new UIImageFramed(asset, asset.Frame(1, 1, 0, 0, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x06006296 RID: 25238 RVA: 0x006D6DDF File Offset: 0x006D4FDF
			public void SetSearch(string searchText)
			{
				this._search = searchText;
			}

			// Token: 0x04007A94 RID: 31380
			private const int _tooltipMaxLines = 30;

			// Token: 0x04007A95 RID: 31381
			private string[] _toolTipLines = new string[30];

			// Token: 0x04007A96 RID: 31382
			private bool[] _unusedPrefixLine = new bool[30];

			// Token: 0x04007A97 RID: 31383
			private bool[] _unusedBadPrefixLines = new bool[30];

			// Token: 0x04007A98 RID: 31384
			private int _unusedYoyoLogo;

			// Token: 0x04007A99 RID: 31385
			private int _unusedResearchLine;

			// Token: 0x04007A9A RID: 31386
			private string _search;

			// Token: 0x04007A9B RID: 31387
			private string[] _toolTipNames = new string[30];
		}

		// Token: 0x02000CF4 RID: 3316
		public class BuildingBlock : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06006298 RID: 25240 RVA: 0x006D6E24 File Offset: 0x006D5024
			public bool FitsFilter(Item entry)
			{
				return entry.createWall != -1 || entry.tileWand != -1 || (entry.createTile != -1 && !Main.tileFrameImportant[entry.createTile]);
			}

			// Token: 0x06006299 RID: 25241 RVA: 0x006D6E56 File Offset: 0x006D5056
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabBlocks";
			}

			// Token: 0x0600629A RID: 25242 RVA: 0x006D6E60 File Offset: 0x006D5060
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons");
				return new UIImageFramed(asset, asset.Frame(11, 1, 4, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000CF5 RID: 3317
		public class Furniture : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x0600629C RID: 25244 RVA: 0x006D6EB4 File Offset: 0x006D50B4
			public bool FitsFilter(Item entry)
			{
				int createTile = entry.createTile;
				return createTile != -1 && Main.tileFrameImportant[createTile];
			}

			// Token: 0x0600629D RID: 25245 RVA: 0x006D6ED5 File Offset: 0x006D50D5
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabFurniture";
			}

			// Token: 0x0600629E RID: 25246 RVA: 0x006D6EDC File Offset: 0x006D50DC
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons");
				return new UIImageFramed(asset, asset.Frame(11, 1, 7, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000CF6 RID: 3318
		public class Tools : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060062A0 RID: 25248 RVA: 0x006D6F30 File Offset: 0x006D5130
			public bool FitsFilter(Item entry)
			{
				return entry.pick > 0 || entry.axe > 0 || entry.hammer > 0 || entry.fishingPole > 0 || entry.tileWand != -1 || ItemID.Sets.DuplicationMenuToolsFilter[entry.type];
			}

			// Token: 0x060062A1 RID: 25249 RVA: 0x006D6F85 File Offset: 0x006D5185
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabTools";
			}

			// Token: 0x060062A2 RID: 25250 RVA: 0x006D6F8C File Offset: 0x006D518C
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons");
				return new UIImageFramed(asset, asset.Frame(11, 1, 6, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000CF7 RID: 3319
		public class Weapon : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060062A4 RID: 25252 RVA: 0x006D6FE0 File Offset: 0x006D51E0
			public bool FitsFilter(Item entry)
			{
				return entry.damage > 0;
			}

			// Token: 0x060062A5 RID: 25253 RVA: 0x006D6FEB File Offset: 0x006D51EB
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabWeapons";
			}

			// Token: 0x060062A6 RID: 25254 RVA: 0x006D6FF4 File Offset: 0x006D51F4
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons");
				return new UIImageFramed(asset, asset.Frame(11, 1, 0, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000CF8 RID: 3320
		public abstract class AArmor
		{
			// Token: 0x060062A8 RID: 25256 RVA: 0x006D7048 File Offset: 0x006D5248
			public bool IsAnArmorThatMatchesSocialState(Item entry, bool shouldBeSocial)
			{
				return (entry.bodySlot != -1 || entry.headSlot != -1 || entry.legSlot != -1) && entry.vanity == shouldBeSocial;
			}
		}

		// Token: 0x02000CF9 RID: 3321
		public class Armor : ItemFilters.AArmor, IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060062AA RID: 25258 RVA: 0x006D7078 File Offset: 0x006D5278
			public bool FitsFilter(Item entry)
			{
				return base.IsAnArmorThatMatchesSocialState(entry, false);
			}

			// Token: 0x060062AB RID: 25259 RVA: 0x006D7082 File Offset: 0x006D5282
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabArmor";
			}

			// Token: 0x060062AC RID: 25260 RVA: 0x006D708C File Offset: 0x006D528C
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons");
				return new UIImageFramed(asset, asset.Frame(11, 1, 2, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000CFA RID: 3322
		public class Vanity : ItemFilters.AArmor, IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060062AE RID: 25262 RVA: 0x006D70E0 File Offset: 0x006D52E0
			public bool FitsFilter(Item entry)
			{
				return base.IsAnArmorThatMatchesSocialState(entry, true);
			}

			// Token: 0x060062AF RID: 25263 RVA: 0x006D70EA File Offset: 0x006D52EA
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabVanity";
			}

			// Token: 0x060062B0 RID: 25264 RVA: 0x006D70F4 File Offset: 0x006D52F4
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons");
				return new UIImageFramed(asset, asset.Frame(11, 1, 8, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000CFB RID: 3323
		public abstract class AAccessories
		{
			// Token: 0x060062B2 RID: 25266 RVA: 0x006D7148 File Offset: 0x006D5348
			public bool IsAnAccessoryOfType(Item entry, ItemFilters.AAccessories.AccessoriesCategory categoryType)
			{
				bool flag = ItemSlot.IsMiscEquipment(entry);
				return (flag && categoryType == ItemFilters.AAccessories.AccessoriesCategory.Misc) || (!flag && categoryType == ItemFilters.AAccessories.AccessoriesCategory.NonMisc && entry.accessory);
			}

			// Token: 0x02000E61 RID: 3681
			public enum AccessoriesCategory
			{
				// Token: 0x04007D5F RID: 32095
				Misc,
				// Token: 0x04007D60 RID: 32096
				NonMisc
			}
		}

		// Token: 0x02000CFC RID: 3324
		public class Accessories : ItemFilters.AAccessories, IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060062B4 RID: 25268 RVA: 0x006D717E File Offset: 0x006D537E
			public bool FitsFilter(Item entry)
			{
				return base.IsAnAccessoryOfType(entry, ItemFilters.AAccessories.AccessoriesCategory.NonMisc);
			}

			// Token: 0x060062B5 RID: 25269 RVA: 0x006D7188 File Offset: 0x006D5388
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabAccessories";
			}

			// Token: 0x060062B6 RID: 25270 RVA: 0x006D7190 File Offset: 0x006D5390
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons");
				return new UIImageFramed(asset, asset.Frame(11, 1, 1, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000CFD RID: 3325
		public class MiscAccessories : ItemFilters.AAccessories, IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060062B8 RID: 25272 RVA: 0x006D71E4 File Offset: 0x006D53E4
			public bool FitsFilter(Item entry)
			{
				return base.IsAnAccessoryOfType(entry, ItemFilters.AAccessories.AccessoriesCategory.Misc);
			}

			// Token: 0x060062B9 RID: 25273 RVA: 0x006D71EE File Offset: 0x006D53EE
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabAccessoriesMisc";
			}

			// Token: 0x060062BA RID: 25274 RVA: 0x006D71F8 File Offset: 0x006D53F8
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons");
				return new UIImageFramed(asset, asset.Frame(11, 1, 9, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000CFE RID: 3326
		public class Consumables : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060062BC RID: 25276 RVA: 0x006D7250 File Offset: 0x006D5450
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

			// Token: 0x060062BD RID: 25277 RVA: 0x006D72A6 File Offset: 0x006D54A6
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabConsumables";
			}

			// Token: 0x060062BE RID: 25278 RVA: 0x006D72B0 File Offset: 0x006D54B0
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons");
				return new UIImageFramed(asset, asset.Frame(11, 1, 3, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000CFF RID: 3327
		public class GameplayItems : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060062C0 RID: 25280 RVA: 0x006D7304 File Offset: 0x006D5504
			public bool FitsFilter(Item entry)
			{
				return ItemID.Sets.SortingPriorityBossSpawns[entry.type] != -1;
			}

			// Token: 0x060062C1 RID: 25281 RVA: 0x006D7318 File Offset: 0x006D5518
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabMisc";
			}

			// Token: 0x060062C2 RID: 25282 RVA: 0x006D7320 File Offset: 0x006D5520
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons");
				return new UIImageFramed(asset, asset.Frame(11, 1, 5, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000D00 RID: 3328
		public class MiscFallback : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060062C4 RID: 25284 RVA: 0x006D7374 File Offset: 0x006D5574
			public MiscFallback(List<IItemEntryFilter> otherFiltersToCheckAgainst)
			{
				int count = ItemLoader.ItemCount;
				this._fitsFilterByItemType = new bool[count];
				for (int i = 1; i < count; i++)
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

			// Token: 0x060062C5 RID: 25285 RVA: 0x006D7404 File Offset: 0x006D5604
			public bool FitsFilter(Item entry)
			{
				return this._fitsFilterByItemType.IndexInRange(entry.type) && this._fitsFilterByItemType[entry.type];
			}

			// Token: 0x060062C6 RID: 25286 RVA: 0x006D7428 File Offset: 0x006D5628
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabMisc";
			}

			// Token: 0x060062C7 RID: 25287 RVA: 0x006D7430 File Offset: 0x006D5630
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons");
				return new UIImageFramed(asset, asset.Frame(11, 1, 5, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x04007A9C RID: 31388
			private bool[] _fitsFilterByItemType;
		}

		// Token: 0x02000D01 RID: 3329
		public class Materials : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060062C8 RID: 25288 RVA: 0x006D747C File Offset: 0x006D567C
			public bool FitsFilter(Item entry)
			{
				return entry.material;
			}

			// Token: 0x060062C9 RID: 25289 RVA: 0x006D7484 File Offset: 0x006D5684
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabMaterials";
			}

			// Token: 0x060062CA RID: 25290 RVA: 0x006D748C File Offset: 0x006D568C
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons");
				return new UIImageFramed(asset, asset.Frame(11, 1, 10, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}
	}
}
