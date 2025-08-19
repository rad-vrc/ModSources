using System;
using System.Collections.Generic;
using System.Linq;
using ReLogic.Content;
using Terraria.IO;
using Terraria.ModLoader;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020004F1 RID: 1265
	public class PlayerResourceSetsManager
	{
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06003D52 RID: 15698 RVA: 0x005CA357 File Offset: 0x005C8557
		// (set) Token: 0x06003D53 RID: 15699 RVA: 0x005CA35F File Offset: 0x005C855F
		public string ActiveSetKeyName { get; private set; }

		// Token: 0x06003D54 RID: 15700 RVA: 0x005CA368 File Offset: 0x005C8568
		public void BindTo(Preferences preferences)
		{
			preferences.OnLoad += this.Configuration_OnLoad;
			preferences.OnSave += this.Configuration_OnSave;
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x005CA38E File Offset: 0x005C858E
		private void Configuration_OnLoad(Preferences obj)
		{
			this._activeSetConfigKey = obj.Get<string>("PlayerResourcesSet", "New");
			this._activeSetConfigKeyOriginal = this._activeSetConfigKey;
			if (this._loadedContent)
			{
				this.SetActiveFromLoadedConfigKey();
			}
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x005CA3C0 File Offset: 0x005C85C0
		private void Configuration_OnSave(Preferences obj)
		{
			obj.Put("PlayerResourcesSet", this._activeSetConfigKey);
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x005CA3D4 File Offset: 0x005C85D4
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

		// Token: 0x06003D58 RID: 15704 RVA: 0x005CA4C6 File Offset: 0x005C86C6
		public void SetActiveFromLoadedConfigKey()
		{
			this.SetActive(this._activeSetConfigKey);
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x005CA4D4 File Offset: 0x005C86D4
		private void SetActive(string name)
		{
			int index = this.accessKeys.FindIndex((string s) => s == name);
			if (index < 0)
			{
				index = 0;
			}
			this.SetActiveFrameFromIndex(index);
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x005CA513 File Offset: 0x005C8713
		private void SetActiveFrame(IPlayerResourcesDisplaySet set)
		{
			this._activeSet = set;
			this._activeSetConfigKey = set.ConfigKey;
			this.ActiveSetKeyName = set.NameKey;
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x005CA534 File Offset: 0x005C8734
		public void TryToHoverOverResources()
		{
			this._activeSet.TryToHover();
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x005CA541 File Offset: 0x005C8741
		public void Draw()
		{
			this._activeSet.Draw();
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x005CA550 File Offset: 0x005C8750
		public void CycleResourceSet()
		{
			int num = this.selectedSet + 1;
			this.selectedSet = num;
			this.SetActiveFrameFromIndex(num % this.accessKeys.Count);
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x005CA580 File Offset: 0x005C8780
		internal void AddModdedDisplaySets()
		{
			if (Main.dedServ)
			{
				return;
			}
			foreach (ModResourceDisplaySet display in ResourceDisplaySetLoader.moddedDisplaySets)
			{
				string key = display.ConfigKey;
				this._sets[key] = display;
				this.accessKeys.Add(key);
			}
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x005CA5F0 File Offset: 0x005C87F0
		internal void SetActiveFromOriginalConfigKey()
		{
			if (Main.dedServ)
			{
				return;
			}
			this.SetActive(this._activeSetConfigKeyOriginal);
			this._activeSetConfigKeyOriginal = this._activeSetConfigKey;
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x005CA612 File Offset: 0x005C8812
		private void SetActiveFrameFromIndex(int index)
		{
			this.selectedSet = index;
			this.SetActiveFrame(this._sets[this.accessKeys[this.selectedSet]]);
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x005CA640 File Offset: 0x005C8840
		internal void ResetToVanilla()
		{
			if (Main.dedServ)
			{
				return;
			}
			this._activeSetConfigKey = this._activeSetConfigKeyOriginal;
			foreach (string key in this.accessKeys.Skip(PlayerResourceSetsManager.vanillaSets.Length))
			{
				this._sets.Remove(key);
			}
			this.accessKeys.Clear();
			this.accessKeys.AddRange(PlayerResourceSetsManager.vanillaSets);
			this.SetActive(this._activeSetConfigKey);
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06003D62 RID: 15714 RVA: 0x005CA6DC File Offset: 0x005C88DC
		public IPlayerResourcesDisplaySet ActiveSet
		{
			get
			{
				return this._activeSet;
			}
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x005CA6E4 File Offset: 0x005C88E4
		public IPlayerResourcesDisplaySet GetDisplaySet(string nameKey)
		{
			IPlayerResourcesDisplaySet set;
			if (!this._sets.TryGetValue(nameKey, out set))
			{
				return null;
			}
			return set;
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x005CA704 File Offset: 0x005C8904
		public ModResourceDisplaySet GetDisplaySet<T>() where T : ModResourceDisplaySet
		{
			return this.GetDisplaySet(ModContent.GetInstance<T>().ConfigKey) as T;
		}

		// Token: 0x04005633 RID: 22067
		private Dictionary<string, IPlayerResourcesDisplaySet> _sets = new Dictionary<string, IPlayerResourcesDisplaySet>();

		// Token: 0x04005634 RID: 22068
		private IPlayerResourcesDisplaySet _activeSet;

		// Token: 0x04005635 RID: 22069
		private string _activeSetConfigKey;

		// Token: 0x04005636 RID: 22070
		private bool _loadedContent;

		// Token: 0x04005638 RID: 22072
		private static readonly string[] vanillaSets = new string[]
		{
			"New",
			"NewWithText",
			"HorizontalBars",
			"HorizontalBarsWithText",
			"HorizontalBarsWithFullText",
			"Default"
		};

		// Token: 0x04005639 RID: 22073
		private readonly List<string> accessKeys = new List<string>(PlayerResourceSetsManager.vanillaSets);

		// Token: 0x0400563A RID: 22074
		private int selectedSet;

		// Token: 0x0400563B RID: 22075
		private string _activeSetConfigKeyOriginal;
	}
}
