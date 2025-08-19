using System;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.IO;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003BA RID: 954
	public class PlayerResourceSetsManager2 : SelectionHolder<IPlayerResourcesDisplaySet>
	{
		// Token: 0x06002A36 RID: 10806 RVA: 0x0059880B File Offset: 0x00596A0B
		protected override void Configuration_Save(Preferences obj)
		{
			obj.Put("PlayerResourcesSet", this.ActiveSelectionConfigKey);
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x0059881E File Offset: 0x00596A1E
		protected override void Configuration_OnLoad(Preferences obj)
		{
			this.ActiveSelectionConfigKey = Main.Configuration.Get<string>("PlayerResourcesSet", "New");
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x0059883C File Offset: 0x00596A3C
		protected override void PopulateOptionsAndLoadContent(AssetRequestMode mode)
		{
			this.Options["New"] = new FancyClassicPlayerResourcesDisplaySet("New", "New", "FancyClassic", mode);
			this.Options["Default"] = new ClassicPlayerResourcesDisplaySet("Default", "Default");
			this.Options["HorizontalBarsWithFullText"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithFullText", "HorizontalBarsWithFullText", "HorizontalBars", mode);
			this.Options["HorizontalBarsWithText"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithText", "HorizontalBarsWithText", "HorizontalBars", mode);
			this.Options["HorizontalBars"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBars", "HorizontalBars", "HorizontalBars", mode);
			this.Options["NewWithText"] = new FancyClassicPlayerResourcesDisplaySet("NewWithText", "NewWithText", "FancyClassic", mode);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x00598921 File Offset: 0x00596B21
		public void TryToHoverOverResources()
		{
			this.ActiveSelection.TryToHover();
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x0059892E File Offset: 0x00596B2E
		public void Draw()
		{
			this.ActiveSelection.Draw();
		}
	}
}
