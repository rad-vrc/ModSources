using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using rail;
using ReLogic.OS;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000155 RID: 341
	public class CoreSocialModule : ISocialModule
	{
		// Token: 0x06001953 RID: 6483
		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern uint GetCurrentThreadId();

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06001954 RID: 6484 RVA: 0x004E0B20 File Offset: 0x004DED20
		// (remove) Token: 0x06001955 RID: 6485 RVA: 0x004E0B54 File Offset: 0x004DED54
		public static event Action OnTick;

		// Token: 0x06001956 RID: 6486 RVA: 0x004E0B88 File Offset: 0x004DED88
		public void Initialize()
		{
			RailGameID railGameID = new RailGameID();
			railGameID.id_ = 2000328UL;
			string[] array;
			if (Main.dedServ)
			{
				array = Environment.GetCommandLineArgs();
			}
			else
			{
				array = new string[]
				{
					" "
				};
			}
			if (rail_api.RailNeedRestartAppForCheckingEnvironment(railGameID, array.Length, array))
			{
				Environment.Exit(1);
			}
			if (!rail_api.RailInitialize())
			{
				Environment.Exit(1);
			}
			this._callbackHelper.RegisterCallback(2, new RailEventCallBackHandler(CoreSocialModule.RailEventCallBack));
			this.isRailValid = true;
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.TickThread), null);
			Main.OnTickForThirdPartySoftwareOnly += CoreSocialModule.RailEventTick;
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x004E0C27 File Offset: 0x004DEE27
		public static void RailEventTick()
		{
			rail_api.RailFireEvents();
			if (Monitor.TryEnter(CoreSocialModule._railTickLock))
			{
				Monitor.Pulse(CoreSocialModule._railTickLock);
				Monitor.Exit(CoreSocialModule._railTickLock);
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x004E0C4E File Offset: 0x004DEE4E
		private void TickThread(object context)
		{
			Monitor.Enter(CoreSocialModule._railTickLock);
			while (this.isRailValid)
			{
				if (CoreSocialModule.OnTick != null)
				{
					CoreSocialModule.OnTick();
				}
				Monitor.Wait(CoreSocialModule._railTickLock);
			}
			Monitor.Exit(CoreSocialModule._railTickLock);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x004E0C8C File Offset: 0x004DEE8C
		public void Shutdown()
		{
			if (Platform.IsWindows)
			{
				Application.ApplicationExit += delegate(object obj, EventArgs evt)
				{
					this.isRailValid = false;
				};
			}
			else
			{
				this.isRailValid = false;
				AppDomain.CurrentDomain.ProcessExit += delegate(object obj, EventArgs evt)
				{
					this.isRailValid = false;
				};
			}
			this._callbackHelper.UnregisterAllCallback();
			rail_api.RailFinalize();
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x004E0CE0 File Offset: 0x004DEEE0
		public static void RailEventCallBack(RAILEventID eventId, EventBase data)
		{
			if (eventId == 2)
			{
				CoreSocialModule.ProcessRailSystemStateChange(((RailSystemStateChanged)data).state);
			}
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x004E0CF6 File Offset: 0x004DEEF6
		public static void SaveAndQuitCallBack()
		{
			Main.WeGameRequireExitGame();
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x004E0CFD File Offset: 0x004DEEFD
		private static void ProcessRailSystemStateChange(RailSystemState state)
		{
			if (state == 2 || state == 3)
			{
				MessageBox.Show("检测到WeGame异常，游戏将自动保存进度并退出游戏", "Terraria--WeGame Error");
				WorldGen.SaveAndQuit(new Action(CoreSocialModule.SaveAndQuitCallBack));
			}
		}

		// Token: 0x0400153B RID: 5435
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x0400153D RID: 5437
		private static object _railTickLock = new object();

		// Token: 0x0400153E RID: 5438
		private bool isRailValid;
	}
}
