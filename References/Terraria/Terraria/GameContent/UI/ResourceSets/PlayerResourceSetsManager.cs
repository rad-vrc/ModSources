using System;
using System.Collections.Generic;
using System.Linq;
using ReLogic.Content;
using Terraria.IO;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003BB RID: 955
	public class PlayerResourceSetsManager
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06002A3C RID: 10812 RVA: 0x00598943 File Offset: 0x00596B43
		// (set) Token: 0x06002A3D RID: 10813 RVA: 0x0059894B File Offset: 0x00596B4B
		public string ActiveSetKeyName { get; private set; }

		// Token: 0x06002A3E RID: 10814 RVA: 0x00598954 File Offset: 0x00596B54
		public void BindTo(Preferences preferences)
		{
			preferences.OnLoad += this.Configuration_OnLoad;
			preferences.OnSave += this.Configuration_OnSave;
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x0059897A File Offset: 0x00596B7A
		private void Configuration_OnLoad(Preferences obj)
		{
			this._activeSetConfigKey = obj.Get<string>("PlayerResourcesSet", "New");
			if (this._loadedContent)
			{
				this.SetActiveFromLoadedConfigKey();
			}
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x005989A0 File Offset: 0x00596BA0
		private void Configuration_OnSave(Preferences obj)
		{
			obj.Put("PlayerResourcesSet", this._activeSetConfigKey);
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x005989B4 File Offset: 0x00596BB4
		public void LoadContent(AssetRequestMode mode)
		{
			this._sets["New"] = new FancyClassicPlayerResourcesDisplaySet("New", "New", "FancyClassic", mode);
			this._sets["Default"] = new ClassicPlayerResourcesDisplaySet("Default", "Default");
			this._sets["HorizontalBarsWithFullText"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithFullText", "HorizontalBarsWithFullText", "HorizontalBars", mode);
			this._sets["HorizontalBarsWithText"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithText", "HorizontalBarsWithText", "HorizontalBars", mode);
			this._sets["HorizontalBars"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBars", "HorizontalBars", "HorizontalBars", mode);
			this._sets["NewWithText"] = new FancyClassicPlayerResourcesDisplaySet("NewWithText", "NewWithText", "FancyClassic", mode);
			this._loadedContent = true;
			this.SetActiveFromLoadedConfigKey();
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x00598AA6 File Offset: 0x00596CA6
		public void SetActiveFromLoadedConfigKey()
		{
			this.SetActive(this._activeSetConfigKey);
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x00598AB4 File Offset: 0x00596CB4
		private void SetActive(string name)
		{
			IPlayerResourcesDisplaySet playerResourcesDisplaySet = this._sets.FirstOrDefault((KeyValuePair<string, IPlayerResourcesDisplaySet> pair) => pair.Key == name).Value;
			if (playerResourcesDisplaySet == null)
			{
				playerResourcesDisplaySet = this._sets.Values.First<IPlayerResourcesDisplaySet>();
			}
			this.SetActiveFrame(playerResourcesDisplaySet);
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x00598B09 File Offset: 0x00596D09
		private void SetActiveFrame(IPlayerResourcesDisplaySet set)
		{
			this._activeSet = set;
			this._activeSetConfigKey = set.ConfigKey;
			this.ActiveSetKeyName = set.NameKey;
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x00598B2A File Offset: 0x00596D2A
		public void TryToHoverOverResources()
		{
			this._activeSet.TryToHover();
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x00598B37 File Offset: 0x00596D37
		public void Draw()
		{
			this._activeSet.Draw();
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x00598B44 File Offset: 0x00596D44
		public void CycleResourceSet()
		{
			IPlayerResourcesDisplaySet lastFrame = null;
			this._sets.Values.FirstOrDefault(delegate(IPlayerResourcesDisplaySet frame)
			{
				if (frame == this._activeSet)
				{
					return true;
				}
				lastFrame = frame;
				return false;
			});
			if (lastFrame == null)
			{
				lastFrame = this._sets.Values.Last<IPlayerResourcesDisplaySet>();
			}
			this.SetActiveFrame(lastFrame);
		}

		// Token: 0x04004D10 RID: 19728
		private Dictionary<string, IPlayerResourcesDisplaySet> _sets = new Dictionary<string, IPlayerResourcesDisplaySet>();

		// Token: 0x04004D11 RID: 19729
		private IPlayerResourcesDisplaySet _activeSet;

		// Token: 0x04004D12 RID: 19730
		private string _activeSetConfigKey;

		// Token: 0x04004D13 RID: 19731
		private bool _loadedContent;
	}
}
