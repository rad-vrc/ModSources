using System;
using System.Collections.Generic;
using System.Linq;
using ReLogic.Content;
using Terraria.IO;

namespace Terraria.DataStructures
{
	// Token: 0x0200072F RID: 1839
	public abstract class SelectionHolder<TCycleType> where TCycleType : class, IConfigKeyHolder
	{
		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06004AB4 RID: 19124 RVA: 0x0066819B File Offset: 0x0066639B
		// (set) Token: 0x06004AB5 RID: 19125 RVA: 0x006681A3 File Offset: 0x006663A3
		public string ActiveSelectionKeyName { get; private set; }

		// Token: 0x06004AB6 RID: 19126 RVA: 0x006681AC File Offset: 0x006663AC
		public void BindTo(Preferences preferences)
		{
			preferences.OnLoad += this.Wrapped_Configuration_OnLoad;
			preferences.OnSave += this.Configuration_Save;
		}

		// Token: 0x06004AB7 RID: 19127
		protected abstract void Configuration_Save(Preferences obj);

		// Token: 0x06004AB8 RID: 19128
		protected abstract void Configuration_OnLoad(Preferences obj);

		// Token: 0x06004AB9 RID: 19129 RVA: 0x006681D3 File Offset: 0x006663D3
		protected void Wrapped_Configuration_OnLoad(Preferences obj)
		{
			this.Configuration_OnLoad(obj);
			if (this.LoadedContent)
			{
				this.SetActiveMinimapFromLoadedConfigKey();
			}
		}

		// Token: 0x06004ABA RID: 19130
		protected abstract void PopulateOptionsAndLoadContent(AssetRequestMode mode);

		// Token: 0x06004ABB RID: 19131 RVA: 0x006681EA File Offset: 0x006663EA
		public void LoadContent(AssetRequestMode mode)
		{
			this.PopulateOptionsAndLoadContent(mode);
			this.LoadedContent = true;
			this.SetActiveMinimapFromLoadedConfigKey();
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x00668200 File Offset: 0x00666400
		public void CycleSelection()
		{
			TCycleType lastFrame = default(TCycleType);
			this.Options.Values.FirstOrDefault(delegate(TCycleType frame)
			{
				if (frame == this.ActiveSelection)
				{
					return true;
				}
				lastFrame = frame;
				return false;
			});
			if (lastFrame == null)
			{
				lastFrame = this.Options.Values.Last<TCycleType>();
			}
			this.SetActiveFrame(lastFrame);
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x00668272 File Offset: 0x00666472
		public void SetActiveMinimapFromLoadedConfigKey()
		{
			this.SetActiveFrame(this.ActiveSelectionConfigKey);
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x00668280 File Offset: 0x00666480
		private void SetActiveFrame(string frameName)
		{
			TCycleType val = this.Options.FirstOrDefault((KeyValuePair<string, TCycleType> pair) => pair.Key == frameName).Value;
			if (val == null)
			{
				val = this.Options.Values.First<TCycleType>();
			}
			this.SetActiveFrame(val);
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x006682DA File Offset: 0x006664DA
		private void SetActiveFrame(TCycleType frame)
		{
			this.ActiveSelection = frame;
			this.ActiveSelectionConfigKey = frame.ConfigKey;
			this.ActiveSelectionKeyName = frame.NameKey;
		}

		// Token: 0x04005FFB RID: 24571
		protected Dictionary<string, TCycleType> Options = new Dictionary<string, TCycleType>();

		// Token: 0x04005FFC RID: 24572
		protected TCycleType ActiveSelection;

		// Token: 0x04005FFD RID: 24573
		protected string ActiveSelectionConfigKey;

		// Token: 0x04005FFE RID: 24574
		protected bool LoadedContent;
	}
}
