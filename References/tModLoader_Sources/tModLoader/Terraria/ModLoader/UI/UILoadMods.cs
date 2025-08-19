using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Terraria.Localization;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200024C RID: 588
	internal class UILoadMods : UIProgress
	{
		// Token: 0x060029B6 RID: 10678 RVA: 0x005140AF File Offset: 0x005122AF
		public override void OnActivate()
		{
			base.OnActivate();
			this._cts = new CancellationTokenSource();
			base.OnCancel += delegate()
			{
				this.SetLoadStage("tModLoader.LoadingCancelled", -1);
				this._cts.Cancel();
			};
			this.gotoMenu = 888;
			ModLoader.BeginLoad(this._cts.Token);
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x005140EF File Offset: 0x005122EF
		public override void OnDeactivate()
		{
			base.OnDeactivate();
			CancellationTokenSource cts = this._cts;
			if (cts != null)
			{
				cts.Dispose();
			}
			this._cts = null;
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x0051410F File Offset: 0x0051230F
		public void SetLoadStage(string stageText, int modCount = -1)
		{
			this.stageText = stageText;
			this.modCount = modCount;
			if (modCount < 0)
			{
				this.SetProgressText(Language.GetTextValue(stageText), null);
			}
			base.Progress = 0f;
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x0051413C File Offset: 0x0051233C
		private void SetProgressText(string text, string logText = null)
		{
			string cleanText = Utils.CleanChatTags(text);
			Logging.tML.Info((logText != null) ? Utils.CleanChatTags(logText) : cleanText);
			if (Main.dedServ)
			{
				Console.WriteLine(cleanText);
			}
			else
			{
				this.DisplayText = text;
			}
			base.SubProgressText = "";
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x00514188 File Offset: 0x00512388
		public void SetCurrentMod(int i, string modName, string displayName, Version version)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
			defaultInterpolatedStringHandler.AppendFormatted(displayName);
			defaultInterpolatedStringHandler.AppendLiteral(" v");
			defaultInterpolatedStringHandler.AppendFormatted<Version>(version);
			string display = defaultInterpolatedStringHandler.ToStringAndClear();
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
			defaultInterpolatedStringHandler.AppendFormatted(modName);
			defaultInterpolatedStringHandler.AppendLiteral(" (");
			defaultInterpolatedStringHandler.AppendFormatted(displayName);
			defaultInterpolatedStringHandler.AppendLiteral(") v");
			defaultInterpolatedStringHandler.AppendFormatted<Version>(version);
			string log = defaultInterpolatedStringHandler.ToStringAndClear();
			this.SetProgressText(Language.GetTextValue(this.stageText, display), Language.GetTextValue(this.stageText, log));
			base.Progress = (float)i / (float)this.modCount;
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x00514233 File Offset: 0x00512433
		public void SetCurrentMod(int i, Mod mod)
		{
			this.SetCurrentMod(i, mod.Name, mod.DisplayName, mod.Version);
		}

		// Token: 0x04001A84 RID: 6788
		public int modCount;

		// Token: 0x04001A85 RID: 6789
		private string stageText;

		// Token: 0x04001A86 RID: 6790
		private CancellationTokenSource _cts;
	}
}
