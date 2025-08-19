using System;
using System.Collections.Generic;
using System.Linq;
using ReLogic.Content;
using Terraria.IO;

namespace Terraria.DataStructures
{
	// Token: 0x02000413 RID: 1043
	public abstract class SelectionHolder<TCycleType> where TCycleType : class, IConfigKeyHolder
	{
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x0059DF2A File Offset: 0x0059C12A
		// (set) Token: 0x06002B48 RID: 11080 RVA: 0x0059DF32 File Offset: 0x0059C132
		public string ActiveSelectionKeyName { get; private set; }

		// Token: 0x06002B49 RID: 11081 RVA: 0x0059DF3B File Offset: 0x0059C13B
		public void BindTo(Preferences preferences)
		{
			preferences.OnLoad += this.Wrapped_Configuration_OnLoad;
			preferences.OnSave += this.Configuration_Save;
		}

		// Token: 0x06002B4A RID: 11082
		protected abstract void Configuration_Save(Preferences obj);

		// Token: 0x06002B4B RID: 11083
		protected abstract void Configuration_OnLoad(Preferences obj);

		// Token: 0x06002B4C RID: 11084 RVA: 0x0059DF62 File Offset: 0x0059C162
		protected void Wrapped_Configuration_OnLoad(Preferences obj)
		{
			this.Configuration_OnLoad(obj);
			if (this.LoadedContent)
			{
				this.SetActiveMinimapFromLoadedConfigKey();
			}
		}

		// Token: 0x06002B4D RID: 11085
		protected abstract void PopulateOptionsAndLoadContent(AssetRequestMode mode);

		// Token: 0x06002B4E RID: 11086 RVA: 0x0059DF79 File Offset: 0x0059C179
		public void LoadContent(AssetRequestMode mode)
		{
			this.PopulateOptionsAndLoadContent(mode);
			this.LoadedContent = true;
			this.SetActiveMinimapFromLoadedConfigKey();
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0059DF90 File Offset: 0x0059C190
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

		// Token: 0x06002B50 RID: 11088 RVA: 0x0059E002 File Offset: 0x0059C202
		public void SetActiveMinimapFromLoadedConfigKey()
		{
			this.SetActiveFrame(this.ActiveSelectionConfigKey);
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x0059E010 File Offset: 0x0059C210
		private void SetActiveFrame(string frameName)
		{
			TCycleType tcycleType = this.Options.FirstOrDefault((KeyValuePair<string, TCycleType> pair) => pair.Key == frameName).Value;
			if (tcycleType == null)
			{
				tcycleType = this.Options.Values.First<TCycleType>();
			}
			this.SetActiveFrame(tcycleType);
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0059E06A File Offset: 0x0059C26A
		private void SetActiveFrame(TCycleType frame)
		{
			this.ActiveSelection = frame;
			this.ActiveSelectionConfigKey = frame.ConfigKey;
			this.ActiveSelectionKeyName = frame.NameKey;
		}

		// Token: 0x04004F6A RID: 20330
		protected Dictionary<string, TCycleType> Options = new Dictionary<string, TCycleType>();

		// Token: 0x04004F6B RID: 20331
		protected TCycleType ActiveSelection;

		// Token: 0x04004F6C RID: 20332
		protected string ActiveSelectionConfigKey;

		// Token: 0x04004F6D RID: 20333
		protected bool LoadedContent;
	}
}
