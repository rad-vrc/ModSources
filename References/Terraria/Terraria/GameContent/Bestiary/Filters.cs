using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002ED RID: 749
	public static class Filters
	{
		// Token: 0x020006DB RID: 1755
		public class BySearch : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>, ISearchFilter<BestiaryEntry>
		{
			// Token: 0x170003F5 RID: 1013
			// (get) Token: 0x060036BB RID: 14011 RVA: 0x0060D334 File Offset: 0x0060B534
			public bool? ForcedDisplay
			{
				get
				{
					return new bool?(true);
				}
			}

			// Token: 0x060036BD RID: 14013 RVA: 0x0060D33C File Offset: 0x0060B53C
			public bool FitsFilter(BestiaryEntry entry)
			{
				if (this._search == null)
				{
					return true;
				}
				BestiaryUICollectionInfo entryUICollectionInfo = entry.UIInfoProvider.GetEntryUICollectionInfo();
				for (int i = 0; i < entry.Info.Count; i++)
				{
					IProvideSearchFilterString provideSearchFilterString = entry.Info[i] as IProvideSearchFilterString;
					if (provideSearchFilterString != null)
					{
						string searchString = provideSearchFilterString.GetSearchString(ref entryUICollectionInfo);
						if (searchString != null && searchString.ToLower().IndexOf(this._search, StringComparison.OrdinalIgnoreCase) != -1)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x060036BE RID: 14014 RVA: 0x0060D3AE File Offset: 0x0060B5AE
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.IfSearched";
			}

			// Token: 0x060036BF RID: 14015 RVA: 0x00609819 File Offset: 0x00607A19
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Rank_Light", 1);
				return new UIImageFramed(asset, asset.Frame(1, 1, 0, 0, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x060036C0 RID: 14016 RVA: 0x0060D3B5 File Offset: 0x0060B5B5
			public void SetSearch(string searchText)
			{
				this._search = searchText;
			}

			// Token: 0x04006285 RID: 25221
			private string _search;
		}

		// Token: 0x020006DC RID: 1756
		public class ByUnlockState : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>
		{
			// Token: 0x170003F6 RID: 1014
			// (get) Token: 0x060036C1 RID: 14017 RVA: 0x0060D334 File Offset: 0x0060B534
			public bool? ForcedDisplay
			{
				get
				{
					return new bool?(true);
				}
			}

			// Token: 0x060036C2 RID: 14018 RVA: 0x0060D3C0 File Offset: 0x0060B5C0
			public bool FitsFilter(BestiaryEntry entry)
			{
				BestiaryUICollectionInfo entryUICollectionInfo = entry.UIInfoProvider.GetEntryUICollectionInfo();
				return entry.Icon.GetUnlockState(entryUICollectionInfo);
			}

			// Token: 0x060036C3 RID: 14019 RVA: 0x0060D3E5 File Offset: 0x0060B5E5
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.IfUnlocked";
			}

			// Token: 0x060036C4 RID: 14020 RVA: 0x0060D3EC File Offset: 0x0060B5EC
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow", 1);
				return new UIImageFramed(asset, asset.Frame(16, 5, 14, 3, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x020006DD RID: 1757
		public class ByRareCreature : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>
		{
			// Token: 0x170003F7 RID: 1015
			// (get) Token: 0x060036C6 RID: 14022 RVA: 0x0060D428 File Offset: 0x0060B628
			public bool? ForcedDisplay
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060036C7 RID: 14023 RVA: 0x0060D440 File Offset: 0x0060B640
			public bool FitsFilter(BestiaryEntry entry)
			{
				for (int i = 0; i < entry.Info.Count; i++)
				{
					if (entry.Info[i] is RareSpawnBestiaryInfoElement)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060036C8 RID: 14024 RVA: 0x0060D479 File Offset: 0x0060B679
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.IsRare";
			}

			// Token: 0x060036C9 RID: 14025 RVA: 0x00609819 File Offset: 0x00607A19
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Rank_Light", 1);
				return new UIImageFramed(asset, asset.Frame(1, 1, 0, 0, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x020006DE RID: 1758
		public class ByBoss : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>
		{
			// Token: 0x170003F8 RID: 1016
			// (get) Token: 0x060036CB RID: 14027 RVA: 0x0060D480 File Offset: 0x0060B680
			public bool? ForcedDisplay
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060036CC RID: 14028 RVA: 0x0060D498 File Offset: 0x0060B698
			public bool FitsFilter(BestiaryEntry entry)
			{
				for (int i = 0; i < entry.Info.Count; i++)
				{
					if (entry.Info[i] is BossBestiaryInfoElement)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060036CD RID: 14029 RVA: 0x0060D4D1 File Offset: 0x0060B6D1
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.IsBoss";
			}

			// Token: 0x060036CE RID: 14030 RVA: 0x0060D4D8 File Offset: 0x0060B6D8
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow", 1);
				return new UIImageFramed(asset, asset.Frame(16, 5, 15, 3, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x020006DF RID: 1759
		public class ByInfoElement : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>
		{
			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x060036D0 RID: 14032 RVA: 0x0060D514 File Offset: 0x0060B714
			public bool? ForcedDisplay
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060036D1 RID: 14033 RVA: 0x0060D52A File Offset: 0x0060B72A
			public ByInfoElement(IBestiaryInfoElement element)
			{
				this._element = element;
			}

			// Token: 0x060036D2 RID: 14034 RVA: 0x0060D539 File Offset: 0x0060B739
			public bool FitsFilter(BestiaryEntry entry)
			{
				return entry.Info.Contains(this._element);
			}

			// Token: 0x060036D3 RID: 14035 RVA: 0x0060D54C File Offset: 0x0060B74C
			public string GetDisplayNameKey()
			{
				IFilterInfoProvider filterInfoProvider = this._element as IFilterInfoProvider;
				if (filterInfoProvider == null)
				{
					return null;
				}
				return filterInfoProvider.GetDisplayNameKey();
			}

			// Token: 0x060036D4 RID: 14036 RVA: 0x0060D570 File Offset: 0x0060B770
			public UIElement GetImage()
			{
				IFilterInfoProvider filterInfoProvider = this._element as IFilterInfoProvider;
				if (filterInfoProvider == null)
				{
					return null;
				}
				return filterInfoProvider.GetFilterImage();
			}

			// Token: 0x04006286 RID: 25222
			private IBestiaryInfoElement _element;
		}
	}
}
