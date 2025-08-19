using System;
using System.IO;
using System.Threading;
using ReLogic.OS;
using Steamworks;
using Terraria.Localization;
using Terraria.ModLoader.Engine;

namespace Terraria.Social.Steam
{
	// Token: 0x020000E5 RID: 229
	public class CoreSocialModule : ISocialModule
	{
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060017D2 RID: 6098 RVA: 0x004B968C File Offset: 0x004B788C
		// (remove) Token: 0x060017D3 RID: 6099 RVA: 0x004B96C0 File Offset: 0x004B78C0
		public static event Action OnTick;

		// Token: 0x060017D4 RID: 6100 RVA: 0x004B96F3 File Offset: 0x004B78F3
		public static void SetSkipPulsing(bool shouldSkipPausing)
		{
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x004B96F8 File Offset: 0x004B78F8
		public void Initialize()
		{
			CoreSocialModule._instance = this;
			Steam.SetAppId(Steam.TMLAppID_t);
			if (!SteamAPI.Init())
			{
				ErrorReporting.FatalExit(Language.GetTextValue("Error.LaunchFromSteam"));
			}
			CoreSocialModule.PortFilesToCurrentStructure();
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

		// Token: 0x060017D6 RID: 6102 RVA: 0x004B97B8 File Offset: 0x004B79B8
		public void PulseSteamTick()
		{
			if (Monitor.TryEnter(this._steamTickLock))
			{
				Monitor.Pulse(this._steamTickLock);
				Monitor.Exit(this._steamTickLock);
			}
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x004B97DD File Offset: 0x004B79DD
		public void PulseSteamCallback()
		{
			if (Monitor.TryEnter(this._steamCallbackLock))
			{
				Monitor.Pulse(this._steamCallbackLock);
				Monitor.Exit(this._steamCallbackLock);
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x004B9802 File Offset: 0x004B7A02
		public static void Pulse()
		{
			CoreSocialModule._instance.PulseSteamTick();
			CoreSocialModule._instance.PulseSteamCallback();
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x004B9818 File Offset: 0x004B7A18
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

		// Token: 0x060017DA RID: 6106 RVA: 0x004B9878 File Offset: 0x004B7A78
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

		// Token: 0x060017DB RID: 6107 RVA: 0x004B98D1 File Offset: 0x004B7AD1
		public void Shutdown()
		{
			this.IsSteamValid = false;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x004B98DA File Offset: 0x004B7ADA
		public void OnOverlayActivated(GameOverlayActivated_t result)
		{
			Main.instance.IsMouseVisible = (result.m_bActive == 1);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x004B98EF File Offset: 0x004B7AEF
		private static void PortFilesToCurrentStructure()
		{
			Program.PortFilesMaster(CoreSocialModule.GetCloudSaveLocation(), true);
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x004B98FC File Offset: 0x004B7AFC
		internal static string GetCloudSaveLocation()
		{
			string steamCloudFolder;
			SteamUser.GetUserDataFolder(ref steamCloudFolder, 512);
			return Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(steamCloudFolder)), "105600", "remote");
		}

		// Token: 0x04001329 RID: 4905
		private static CoreSocialModule _instance;

		// Token: 0x0400132A RID: 4906
		private bool IsSteamValid;

		// Token: 0x0400132B RID: 4907
		private object _steamTickLock = new object();

		// Token: 0x0400132C RID: 4908
		private object _steamCallbackLock = new object();

		// Token: 0x0400132D RID: 4909
		private Callback<GameOverlayActivated_t> _onOverlayActivated;

		// Token: 0x0400132E RID: 4910
		private bool _skipPulsing;
	}
}
