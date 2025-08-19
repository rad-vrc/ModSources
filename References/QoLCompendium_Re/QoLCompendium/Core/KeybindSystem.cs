using System;
using System.Runtime.CompilerServices;
using QoLCompendium.Core.Changes.PlayerChanges;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core
{
	// Token: 0x02000206 RID: 518
	public class KeybindSystem : ModSystem
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0004F1D9 File Offset: 0x0004D3D9
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x0004F1E0 File Offset: 0x0004D3E0
		public static ModKeybind SendNPCsHome { get; private set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0004F1E8 File Offset: 0x0004D3E8
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x0004F1EF File Offset: 0x0004D3EF
		public static ModKeybind QuickRecall { get; private set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0004F1F7 File Offset: 0x0004D3F7
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x0004F1FE File Offset: 0x0004D3FE
		public static ModKeybind QuickMosaicMirror { get; private set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0004F206 File Offset: 0x0004D406
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0004F20D File Offset: 0x0004D40D
		public static ModKeybind Dash { get; private set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0004F215 File Offset: 0x0004D415
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0004F21C File Offset: 0x0004D41C
		public static ModKeybind AddTileToWhitelist { get; private set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0004F224 File Offset: 0x0004D424
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0004F22B File Offset: 0x0004D42B
		public static ModKeybind RemoveTileFromWhitelist { get; private set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0004F233 File Offset: 0x0004D433
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x0004F23A File Offset: 0x0004D43A
		public static ModKeybind PermanentBuffUIToggle { get; private set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0004F242 File Offset: 0x0004D442
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x0004F249 File Offset: 0x0004D449
		public static ModKeybind QuickRod { get; private set; }

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0004F254 File Offset: 0x0004D454
		public override void Load()
		{
			KeybindSystem.SendNPCsHome = KeybindLoader.RegisterKeybind(base.Mod, "SendNPCsHomeBind", "I");
			KeybindSystem.QuickRecall = KeybindLoader.RegisterKeybind(base.Mod, "RecallBind", "K");
			KeybindSystem.QuickMosaicMirror = KeybindLoader.RegisterKeybind(base.Mod, "MosaicMirrorBind", "L");
			KeybindSystem.Dash = KeybindLoader.RegisterKeybind(base.Mod, "DashBind", "C");
			KeybindSystem.AddTileToWhitelist = KeybindLoader.RegisterKeybind(base.Mod, "WhitelistTileBind", "O");
			KeybindSystem.RemoveTileFromWhitelist = KeybindLoader.RegisterKeybind(base.Mod, "RemoveWhitelistedTileBind", "P");
			KeybindSystem.PermanentBuffUIToggle = KeybindLoader.RegisterKeybind(base.Mod, "PermanentBuffUIToggleBind", "L");
			KeybindSystem.QuickRod = KeybindLoader.RegisterKeybind(base.Mod, "QuickRodBind", "Z");
			On_Player.hook_DoCommonDashHandle hook_DoCommonDashHandle;
			if ((hook_DoCommonDashHandle = KeybindSystem.<>O.<0>__OnVanillaDash) == null)
			{
				hook_DoCommonDashHandle = (KeybindSystem.<>O.<0>__OnVanillaDash = new On_Player.hook_DoCommonDashHandle(KeybindSystem.OnVanillaDash));
			}
			On_Player.DoCommonDashHandle += hook_DoCommonDashHandle;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0004F354 File Offset: 0x0004D554
		public override void Unload()
		{
			KeybindSystem.SendNPCsHome = null;
			KeybindSystem.QuickRecall = null;
			KeybindSystem.QuickMosaicMirror = null;
			KeybindSystem.Dash = null;
			KeybindSystem.AddTileToWhitelist = null;
			KeybindSystem.RemoveTileFromWhitelist = null;
			KeybindSystem.PermanentBuffUIToggle = null;
			KeybindSystem.QuickRod = null;
			On_Player.hook_DoCommonDashHandle hook_DoCommonDashHandle;
			if ((hook_DoCommonDashHandle = KeybindSystem.<>O.<0>__OnVanillaDash) == null)
			{
				hook_DoCommonDashHandle = (KeybindSystem.<>O.<0>__OnVanillaDash = new On_Player.hook_DoCommonDashHandle(KeybindSystem.OnVanillaDash));
			}
			On_Player.DoCommonDashHandle -= hook_DoCommonDashHandle;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0004F3B4 File Offset: 0x0004D5B4
		public static void OnVanillaDash(On_Player.orig_DoCommonDashHandle orig, Player player, out int dir, out bool dashing, Player.DashStartAction dashStartAction)
		{
			if (QoLCompendium.mainClientConfig.DisableDashDoubleTap)
			{
				player.dashTime = 0;
			}
			orig.Invoke(player, ref dir, ref dashing, dashStartAction);
			if (player.whoAmI == Main.myPlayer && KeybindSystem.Dash.JustPressed && !player.CCed)
			{
				DashPlayer modPlayer = player.GetModPlayer<DashPlayer>();
				if (player.controlRight && player.controlLeft)
				{
					dir = modPlayer.latestXDirPressed;
				}
				else if (player.controlRight)
				{
					dir = 1;
				}
				else if (player.controlLeft)
				{
					dir = -1;
				}
				if (dir == 0)
				{
					return;
				}
				player.direction = dir;
				dashing = true;
				if (player.dashTime > 0)
				{
					player.dashTime--;
				}
				if (player.dashTime < 0)
				{
					player.dashTime++;
				}
				if ((player.dashTime <= 0 && player.direction == -1) || (player.dashTime >= 0 && player.direction == 1))
				{
					player.dashTime = 15;
					return;
				}
				dashing = true;
				player.dashTime = 0;
				player.timeSinceLastDashStarted = 0;
				if (dashStartAction != null && dashStartAction != null)
				{
					dashStartAction(dir);
				}
			}
		}

		// Token: 0x0200052F RID: 1327
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000B9D RID: 2973
			public static On_Player.hook_DoCommonDashHandle <0>__OnVanillaDash;
		}
	}
}
