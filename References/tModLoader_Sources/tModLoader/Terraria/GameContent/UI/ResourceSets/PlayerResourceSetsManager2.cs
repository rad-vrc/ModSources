using System;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.IO;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020004F2 RID: 1266
	public class PlayerResourceSetsManager2 : SelectionHolder<IPlayerResourcesDisplaySet>
	{
		// Token: 0x06003D67 RID: 15719 RVA: 0x005CA78A File Offset: 0x005C898A
		protected override void Configuration_Save(Preferences obj)
		{
			obj.Put("PlayerResourcesSet", this.ActiveSelectionConfigKey);
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x005CA79D File Offset: 0x005C899D
		protected override void Configuration_OnLoad(Preferences obj)
		{
			this.ActiveSelectionConfigKey = Main.Configuration.Get<string>("PlayerResourcesSet", "New");
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x005CA7BC File Offset: 0x005C89BC
		protected override void PopulateOptionsAndLoadContent(AssetRequestMode mode)
		{
			this.Options["New"] = new FancyClassicPlayerResourcesDisplaySet("New", "New", "FancyClassic", mode);
			this.Options["Default"] = new ClassicPlayerResourcesDisplaySet("Default", "Default");
			this.Options["HorizontalBarsWithFullText"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithFullText", "HorizontalBarsWithFullText", "HorizontalBars", mode);
			this.Options["HorizontalBarsWithText"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithText", "HorizontalBarsWithText", "HorizontalBars", mode);
			this.Options["HorizontalBars"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBars", "HorizontalBars", "HorizontalBars", mode);
			this.Options["NewWithText"] = new FancyClassicPlayerResourcesDisplaySet("NewWithText", "NewWithText", "FancyClassic", mode);
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x005CA8A1 File Offset: 0x005C8AA1
		public void TryToHoverOverResources()
		{
			this.ActiveSelection.TryToHover();
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x005CA8AE File Offset: 0x005C8AAE
		public void Draw()
		{
			this.ActiveSelection.Draw();
		}
	}
}
