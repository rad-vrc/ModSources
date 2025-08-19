using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using log4net.Core;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Exceptions;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200023E RID: 574
	internal class UIBuildMod : UIProgress, ModCompile.IBuildStatus
	{
		// Token: 0x06002900 RID: 10496 RVA: 0x00510AC6 File Offset: 0x0050ECC6
		public void SetProgress(int i, int n = -1)
		{
			if (n >= 0)
			{
				this.numProgressItems = n;
			}
			base.Progress = (float)i / (float)this.numProgressItems;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x00510AE3 File Offset: 0x0050ECE3
		public void SetStatus(string msg)
		{
			Logging.tML.Info(msg);
			this.DisplayText = msg;
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x00510AF7 File Offset: 0x0050ECF7
		public void LogCompilerLine(string msg, Level level)
		{
			Logging.tML.Logger.Log(null, level, msg, null);
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x00510B0C File Offset: 0x0050ED0C
		internal void Build(string mod, bool reload)
		{
			this.Build(delegate(ModCompile mc)
			{
				mc.Build(mod);
			}, reload);
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x00510B39 File Offset: 0x0050ED39
		internal void BuildAll(bool reload)
		{
			this.Build(delegate(ModCompile mc)
			{
				mc.BuildAll();
			}, reload);
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x00510B61 File Offset: 0x0050ED61
		internal void Build(Action<ModCompile> buildAction, bool reload)
		{
			Main.menuMode = 10003;
			Task.Run(() => this.BuildMod(buildAction, reload));
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x00510B98 File Offset: 0x0050ED98
		public override void OnInitialize()
		{
			base.OnInitialize();
			this._cancelButton.Remove();
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x00510BAB File Offset: 0x0050EDAB
		public override void OnActivate()
		{
			base.OnActivate();
			this._cts = new CancellationTokenSource();
			base.OnCancel += delegate()
			{
				this._cts.Cancel();
			};
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x00510BD0 File Offset: 0x0050EDD0
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

		// Token: 0x06002909 RID: 10505 RVA: 0x00510BF0 File Offset: 0x0050EDF0
		private Task BuildMod(Action<ModCompile> buildAction, bool reload)
		{
			while (this._progressBar == null || this._cts == null)
			{
				Task.Delay(1);
			}
			try
			{
				buildAction(new ModCompile(this));
				Main.menuMode = (reload ? 10006 : 10001);
			}
			catch (OperationCanceledException)
			{
				Logging.tML.Info("Mod building was cancelled.");
				return Task.FromResult<bool>(false);
			}
			catch (Exception e)
			{
				Logging.tML.Error(e.Message, e);
				object mod = e.Data.Contains("mod") ? e.Data["mod"] : null;
				string msg = Language.GetTextValue("tModLoader.BuildError", mod ?? "");
				Action retry = null;
				if (e is BuildException)
				{
					string[] array = new string[5];
					array[0] = msg;
					array[1] = "\n";
					array[2] = e.Message;
					array[3] = "\n\n";
					int num = 4;
					Exception innerException = e.InnerException;
					array[num] = ((innerException != null) ? innerException.ToString() : null);
					msg = string.Concat(array);
					retry = delegate()
					{
						Interface.buildMod.Build(buildAction, reload);
					};
				}
				else
				{
					string str = msg;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
					defaultInterpolatedStringHandler.AppendLiteral("\n\n");
					defaultInterpolatedStringHandler.AppendFormatted<Exception>(e);
					msg = str + defaultInterpolatedStringHandler.ToStringAndClear();
				}
				if (e.Data.Contains("showTModPorterHint"))
				{
					msg += "\nSome of these errors can be fixed automatically by running tModPorter from the Mod Sources menu.";
				}
				Interface.errorMessage.Show(msg, 10001, null, e.HelpLink, false, false, retry);
				return Task.FromResult<bool>(false);
			}
			return Task.FromResult<bool>(true);
		}

		// Token: 0x04001A01 RID: 6657
		private CancellationTokenSource _cts;

		// Token: 0x04001A02 RID: 6658
		private int numProgressItems;
	}
}
