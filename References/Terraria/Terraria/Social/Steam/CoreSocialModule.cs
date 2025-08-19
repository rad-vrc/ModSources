using System;
using System.Threading;
using System.Windows.Forms;
using ReLogic.OS;
using Steamworks;
using Terraria.Localization;

namespace Terraria.Social.Steam
{
	// Token: 0x02000175 RID: 373
	public class CoreSocialModule : ISocialModule
	{
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06001A7F RID: 6783 RVA: 0x004E4C80 File Offset: 0x004E2E80
		// (remove) Token: 0x06001A80 RID: 6784 RVA: 0x004E4CB4 File Offset: 0x004E2EB4
		public static event Action OnTick;

		// Token: 0x06001A81 RID: 6785 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public static void SetSkipPulsing(bool shouldSkipPausing)
		{
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x004E4CE8 File Offset: 0x004E2EE8
		public void Initialize()
		{
			CoreSocialModule._instance = this;
			if (!Main.dedServ && SteamAPI.RestartAppIfNecessary(new AppId_t(105600U)))
			{
				Environment.Exit(1);
				return;
			}
			if (!SteamAPI.Init())
			{
				MessageBox.Show(Language.GetTextValue("Error.LaunchFromSteam"), Language.GetTextValue("Error.Error"));
				Environment.Exit(1);
			}
			this.IsSteamValid = true;
			new Thread(new ParameterizedThreadStart(this.SteamCallbackLoop))
			{
				IsBackground = true
			}.Start();
			new Thread(new ParameterizedThreadStart(this.SteamTickLoop))
			{
				IsBackground = true
			}.Start();
			Main.OnTickForThirdPartySoftwareOnly += this.PulseSteamTick;
			Main.OnTickForThirdPartySoftwareOnly += this.PulseSteamCallback;
			if (Platform.IsOSX && !Main.dedServ)
			{
				this._onOverlayActivated = Callback<GameOverlayActivated_t>.Create(new Callback<GameOverlayActivated_t>.DispatchDelegate(this.OnOverlayActivated));
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x004E4DC9 File Offset: 0x004E2FC9
		public void PulseSteamTick()
		{
			if (Monitor.TryEnter(this._steamTickLock))
			{
				Monitor.Pulse(this._steamTickLock);
				Monitor.Exit(this._steamTickLock);
			}
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x004E4DEE File Offset: 0x004E2FEE
		public void PulseSteamCallback()
		{
			if (Monitor.TryEnter(this._steamCallbackLock))
			{
				Monitor.Pulse(this._steamCallbackLock);
				Monitor.Exit(this._steamCallbackLock);
			}
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x004E4E13 File Offset: 0x004E3013
		public static void Pulse()
		{
			CoreSocialModule._instance.PulseSteamTick();
			CoreSocialModule._instance.PulseSteamCallback();
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x004E4E2C File Offset: 0x004E302C
		private void SteamTickLoop(object context)
		{
			Monitor.Enter(this._steamTickLock);
			while (this.IsSteamValid)
			{
				if (this._skipPulsing)
				{
					Monitor.Wait(this._steamCallbackLock);
				}
				else
				{
					if (CoreSocialModule.OnTick != null)
					{
						CoreSocialModule.OnTick();
					}
					Monitor.Wait(this._steamTickLock);
				}
			}
			Monitor.Exit(this._steamTickLock);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x004E4E8C File Offset: 0x004E308C
		private void SteamCallbackLoop(object context)
		{
			Monitor.Enter(this._steamCallbackLock);
			while (this.IsSteamValid)
			{
				if (this._skipPulsing)
				{
					Monitor.Wait(this._steamCallbackLock);
				}
				else
				{
					SteamAPI.RunCallbacks();
					Monitor.Wait(this._steamCallbackLock);
				}
			}
			Monitor.Exit(this._steamCallbackLock);
			SteamAPI.Shutdown();
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x004E4EE5 File Offset: 0x004E30E5
		public void Shutdown()
		{
			this.IsSteamValid = false;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x004E4EEE File Offset: 0x004E30EE
		public void OnOverlayActivated(GameOverlayActivated_t result)
		{
			Main.instance.IsMouseVisible = (result.m_bActive == 1);
		}

		// Token: 0x0400159F RID: 5535
		private static CoreSocialModule _instance;

		// Token: 0x040015A0 RID: 5536
		public const int SteamAppId = 105600;

		// Token: 0x040015A1 RID: 5537
		private bool IsSteamValid;

		// Token: 0x040015A3 RID: 5539
		private object _steamTickLock = new object();

		// Token: 0x040015A4 RID: 5540
		private object _steamCallbackLock = new object();

		// Token: 0x040015A5 RID: 5541
		private Callback<GameOverlayActivated_t> _onOverlayActivated;

		// Token: 0x040015A6 RID: 5542
		private bool _skipPulsing;
	}
}
