using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000682 RID: 1666
	public static class Filters
	{
		// Token: 0x02000D25 RID: 3365
		public class BySearch : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>, ISearchFilter<BestiaryEntry>
		{
			// Token: 0x17000992 RID: 2450
			// (get) Token: 0x06006330 RID: 25392 RVA: 0x006D83E5 File Offset: 0x006D65E5
			public bool? ForcedDisplay
			{
				get
				{
					return new bool?(true);
				}
			}

			// Token: 0x06006331 RID: 25393 RVA: 0x006D83F0 File Offset: 0x006D65F0
			public bool FitsFilter(BestiaryEntry entry)
			{
				if (this._search == null)
				{
					return true;
				}
				BestiaryUICollectionInfo info = entry.UIInfoProvider.GetEntryUICollectionInfo();
				for (int i = 0; i < entry.Info.Count; i++)
				{
					IProvideSearchFilterString provideSearchFilterString = entry.Info[i] as IProvideSearchFilterString;
					if (provideSearchFilterString != null)
					{
						string searchString = provideSearchFilterString.GetSearchString(ref info);
						if (searchString != null && searchString.ToLower().IndexOf(this._search, StringComparison.OrdinalIgnoreCase) != -1)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x06006332 RID: 25394 RVA: 0x006D8462 File Offset: 0x006D6662
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.IfSearched";
			}

			// Token: 0x06006333 RID: 25395 RVA: 0x006D8469 File Offset: 0x006D6669
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Rank_Light");
				return new UIImageFramed(asset, asset.Frame(1, 1, 0, 0, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x06006334 RID: 25396 RVA: 0x006D84A1 File Offset: 0x006D66A1
			public void SetSearch(string searchText)
			{
				this._search = searchText;
			}

			// Token: 0x04007B23 RID: 31523
			private string _search;
		}

		// Token: 0x02000D26 RID: 3366
		public class ByUnlockState : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>
		{
			// Token: 0x17000993 RID: 2451
			// (get) Token: 0x06006336 RID: 25398 RVA: 0x006D84B2 File Offset: 0x006D66B2
			public bool? ForcedDisplay
			{
				get
				{
					return new bool?(true);
				}
			}

			// Token: 0x06006337 RID: 25399 RVA: 0x006D84BC File Offset: 0x006D66BC
			public bool FitsFilter(BestiaryEntry entry)
			{
				BestiaryUICollectionInfo entryUICollectionInfo = entry.UIInfoProvider.GetEntryUICollectionInfo();
				return entry.Icon.GetUnlockState(entryUICollectionInfo);
			}

			// Token: 0x06006338 RID: 25400 RVA: 0x006D84E1 File Offset: 0x006D66E1
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.IfUnlocked";
			}

			// Token: 0x06006339 RID: 25401 RVA: 0x006D84E8 File Offset: 0x006D66E8
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow");
				return new UIImageFramed(asset, asset.Frame(16, 5, 14, 3, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000D27 RID: 3367
		public class ByRareCreature : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>
		{
			// Token: 0x17000994 RID: 2452
			// (get) Token: 0x0600633B RID: 25403 RVA: 0x006D852C File Offset: 0x006D672C
			public bool? ForcedDisplay
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600633C RID: 25404 RVA: 0x006D8544 File Offset: 0x006D6744
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

			// Token: 0x0600633D RID: 25405 RVA: 0x006D857D File Offset: 0x006D677D
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.IsRare";
			}

			// Token: 0x0600633E RID: 25406 RVA: 0x006D8584 File Offset: 0x006D6784
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Rank_Light");
				return new UIImageFramed(asset, asset.Frame(1, 1, 0, 0, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000D28 RID: 3368
		public class ByBoss : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>
		{
			// Token: 0x17000995 RID: 2453
			// (get) Token: 0x06006340 RID: 25408 RVA: 0x006D85C4 File Offset: 0x006D67C4
			public bool? ForcedDisplay
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006341 RID: 25409 RVA: 0x006D85DC File Offset: 0x006D67DC
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

			// Token: 0x06006342 RID: 25410 RVA: 0x006D8615 File Offset: 0x006D6815
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.IsBoss";
			}

			// Token: 0x06006343 RID: 25411 RVA: 0x006D861C File Offset: 0x006D681C
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow");
				return new UIImageFramed(asset, asset.Frame(16, 5, 15, 3, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}
		}

		// Token: 0x02000D29 RID: 3369
		public class ByInfoElement : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>
		{
			// Token: 0x17000996 RID: 2454
			// (get) Token: 0x06006345 RID: 25413 RVA: 0x006D8660 File Offset: 0x006D6860
			public bool? ForcedDisplay
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006346 RID: 25414 RVA: 0x006D8676 File Offset: 0x006D6876
			public ByInfoElement(IBestiaryInfoElement element)
			{
				this._element = element;
			}

			// Token: 0x06006347 RID: 25415 RVA: 0x006D8685 File Offset: 0x006D6885
			public bool FitsFilter(BestiaryEntry entry)
			{
				return entry.Info.Contains(this._element);
			}

			// Token: 0x06006348 RID: 25416 RVA: 0x006D8698 File Offset: 0x006D6898
			public string GetDisplayNameKey()
			{
				IFilterInfoProvider filterInfoProvider = this._element as IFilterInfoProvider;
				if (filterInfoProvider == null)
				{
					return null;
				}
				return filterInfoProvider.GetDisplayNameKey();
			}

			// Token: 0x06006349 RID: 25417 RVA: 0x006D86BC File Offset: 0x006D68BC
			public UIElement GetImage()
			{
				IFilterInfoProvider filterInfoProvider = this._element as IFilterInfoProvider;
				if (filterInfoProvider == null)
				{
					return null;
				}
				return filterInfoProvider.GetFilterImage();
			}

			// Token: 0x04007B24 RID: 31524
			private IBestiaryInfoElement _element;
		}
	}
}
