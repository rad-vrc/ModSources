using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000CD RID: 205
	public class CoreSocialModule : ISocialModule
	{
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060016CF RID: 5839 RVA: 0x004B5D9C File Offset: 0x004B3F9C
		// (remove) Token: 0x060016D0 RID: 5840 RVA: 0x004B5DD0 File Offset: 0x004B3FD0
		public static event Action OnTick;

		// Token: 0x060016D1 RID: 5841
		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern uint GetCurrentThreadId();

		// Token: 0x060016D2 RID: 5842 RVA: 0x004B5E04 File Offset: 0x004B4004
		public void Initialize()
		{
			RailGameID railGameID = new RailGameID();
			railGameID.id_ = 2000328UL;
			string[] array2;
			if (Main.dedServ)
			{
				array2 = Environment.GetCommandLineArgs();
			}
			else
			{
				(array2 = new string[1])[0] = " ";
			}
			string[] array = array2;
			if (rail_api.RailNeedRestartAppForCheckingEnvironment(railGameID, array.Length, array))
			{
				Environment.Exit(1);
			}
			if (!rail_api.RailInitialize())
			{
				Environment.Exit(1);
			}
			RailCallBackHelper callbackHelper = this._callbackHelper;
			RAILEventID raileventID = 2;
			RailEventCallBackHandler railEventCallBackHandler;
			if ((railEventCallBackHandler = CoreSocialModule.<>O.<0>__RailEventCallBack) == null)
			{
				railEventCallBackHandler = (CoreSocialModule.<>O.<0>__RailEventCallBack = new RailEventCallBackHandler(CoreSocialModule.RailEventCallBack));
			}
			callbackHelper.RegisterCallback(raileventID, railEventCallBackHandler);
			this.isRailValid = true;
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.TickThread), null);
			Action value;
			if ((value = CoreSocialModule.<>O.<1>__RailEventTick) == null)
			{
				value = (CoreSocialModule.<>O.<1>__RailEventTick = new Action(CoreSocialModule.RailEventTick));
			}
			Main.OnTickForThirdPartySoftwareOnly += value;
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x004B5EC0 File Offset: 0x004B40C0
		public static void RailEventTick()
		{
			rail_api.RailFireEvents();
			if (Monitor.TryEnter(CoreSocialModule._railTickLock))
			{
				Monitor.Pulse(CoreSocialModule._railTickLock);
				Monitor.Exit(CoreSocialModule._railTickLock);
			}
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x004B5EE7 File Offset: 0x004B40E7
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

		// Token: 0x060016D5 RID: 5845 RVA: 0x004B5F23 File Offset: 0x004B4123
		public void Shutdown()
		{
			this.isRailValid = false;
			AppDomain.CurrentDomain.ProcessExit += delegate([Nullable(2)] object <p0>, EventArgs <p1>)
			{
				this.isRailValid = false;
			};
			this._callbackHelper.UnregisterAllCallback();
			rail_api.RailFinalize();
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x004B5F52 File Offset: 0x004B4152
		public static void RailEventCallBack(RAILEventID eventId, EventBase data)
		{
			if (eventId == 2)
			{
				CoreSocialModule.ProcessRailSystemStateChange(((RailSystemStateChanged)data).state);
			}
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x004B5F68 File Offset: 0x004B4168
		public static void SaveAndQuitCallBack()
		{
			Main.WeGameRequireExitGame();
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x004B5F6F File Offset: 0x004B416F
		private static void ProcessRailSystemStateChange(RailSystemState state)
		{
			if (state == 2 || state == 3)
			{
				MessageBox.Show("检测到WeGame异常，游戏将自动保存进度并退出游戏", "Terraria--WeGame Error", MessageBoxButtons.OK, MessageBoxIcon.None);
				Action callback;
				if ((callback = CoreSocialModule.<>O.<2>__SaveAndQuitCallBack) == null)
				{
					callback = (CoreSocialModule.<>O.<2>__SaveAndQuitCallBack = new Action(CoreSocialModule.SaveAndQuitCallBack));
				}
				WorldGen.SaveAndQuit(callback);
			}
		}

		// Token: 0x040012CE RID: 4814
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x040012CF RID: 4815
		private static object _railTickLock = new object();

		// Token: 0x040012D0 RID: 4816
		private bool isRailValid;

		// Token: 0x0200087C RID: 2172
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040069CD RID: 27085
			public static RailEventCallBackHandler <0>__RailEventCallBack;

			// Token: 0x040069CE RID: 27086
			public static Action <1>__RailEventTick;

			// Token: 0x040069CF RID: 27087
			public static Action <2>__SaveAndQuitCallBack;
		}
	}
}
