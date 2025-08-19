using System;
using System.Collections.Generic;
using ReLogic.OS;
using Terraria.Social.Base;
using Terraria.Social.Steam;
using Terraria.Social.WeGame;

namespace Terraria.Social
{
	// Token: 0x02000152 RID: 338
	public static class SocialAPI
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x004E02B3 File Offset: 0x004DE4B3
		public static SocialMode Mode
		{
			get
			{
				return SocialAPI._mode;
			}
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x004E02BC File Offset: 0x004DE4BC
		public static void Initialize(SocialMode? mode = null)
		{
			if (mode == null)
			{
				mode = new SocialMode?(SocialMode.None);
				if (Main.dedServ)
				{
					if (Program.LaunchParameters.ContainsKey("-steam"))
					{
						mode = new SocialMode?(SocialMode.Steam);
					}
				}
				else
				{
					mode = new SocialMode?(SocialMode.Steam);
				}
			}
			SocialAPI._mode = mode.Value;
			SocialAPI._modules = new List<ISocialModule>();
			SocialAPI.JoinRequests = new ServerJoinRequestsManager();
			Main.OnTickForInternalCodeOnly += SocialAPI.JoinRequests.Update;
			SocialMode mode2 = SocialAPI.Mode;
			if (mode2 != SocialMode.Steam)
			{
				if (mode2 == SocialMode.WeGame)
				{
					SocialAPI.LoadWeGame();
				}
			}
			else
			{
				SocialAPI.LoadSteam();
			}
			foreach (ISocialModule socialModule in SocialAPI._modules)
			{
				socialModule.Initialize();
			}
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x004E0398 File Offset: 0x004DE598
		public static void Shutdown()
		{
			SocialAPI._modules.Reverse();
			foreach (ISocialModule socialModule in SocialAPI._modules)
			{
				socialModule.Shutdown();
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x004E03F4 File Offset: 0x004DE5F4
		private static T LoadModule<T>() where T : ISocialModule, new()
		{
			T t = Activator.CreateInstance<T>();
			SocialAPI._modules.Add(t);
			return t;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x004E0418 File Offset: 0x004DE618
		private static T LoadModule<T>(T module) where T : ISocialModule
		{
			SocialAPI._modules.Add(module);
			return module;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x004E042B File Offset: 0x004DE62B
		private static void LoadDiscord()
		{
			if (Main.dedServ)
			{
				return;
			}
			if (ReLogic.OS.Platform.IsWindows || Environment.Is64BitOperatingSystem)
			{
				bool is64BitProcess = Environment.Is64BitProcess;
			}
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x004E044C File Offset: 0x004DE64C
		private static void LoadSteam()
		{
			SocialAPI.LoadModule<Terraria.Social.Steam.CoreSocialModule>();
			SocialAPI.Friends = SocialAPI.LoadModule<Terraria.Social.Steam.FriendsSocialModule>();
			SocialAPI.Achievements = SocialAPI.LoadModule<Terraria.Social.Steam.AchievementsSocialModule>();
			SocialAPI.Cloud = SocialAPI.LoadModule<Terraria.Social.Steam.CloudSocialModule>();
			SocialAPI.Overlay = SocialAPI.LoadModule<Terraria.Social.Steam.OverlaySocialModule>();
			SocialAPI.Workshop = SocialAPI.LoadModule<Terraria.Social.Steam.WorkshopSocialModule>();
			SocialAPI.Platform = SocialAPI.LoadModule<Terraria.Social.Steam.PlatformSocialModule>();
			if (Main.dedServ)
			{
				SocialAPI.Network = SocialAPI.LoadModule<Terraria.Social.Steam.NetServerSocialModule>();
			}
			else
			{
				SocialAPI.Network = SocialAPI.LoadModule<Terraria.Social.Steam.NetClientSocialModule>();
			}
			WeGameHelper.WriteDebugString("LoadSteam modules", new object[0]);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x004E04C8 File Offset: 0x004DE6C8
		private static void LoadWeGame()
		{
			SocialAPI.LoadModule<Terraria.Social.WeGame.CoreSocialModule>();
			SocialAPI.Cloud = SocialAPI.LoadModule<Terraria.Social.WeGame.CloudSocialModule>();
			SocialAPI.Friends = SocialAPI.LoadModule<Terraria.Social.WeGame.FriendsSocialModule>();
			SocialAPI.Overlay = SocialAPI.LoadModule<Terraria.Social.WeGame.OverlaySocialModule>();
			if (Main.dedServ)
			{
				SocialAPI.Network = SocialAPI.LoadModule<Terraria.Social.WeGame.NetServerSocialModule>();
			}
			else
			{
				SocialAPI.Network = SocialAPI.LoadModule<Terraria.Social.WeGame.NetClientSocialModule>();
			}
			WeGameHelper.WriteDebugString("LoadWeGame modules", new object[0]);
		}

		// Token: 0x0400152A RID: 5418
		private static SocialMode _mode;

		// Token: 0x0400152B RID: 5419
		public static Terraria.Social.Base.FriendsSocialModule Friends;

		// Token: 0x0400152C RID: 5420
		public static Terraria.Social.Base.AchievementsSocialModule Achievements;

		// Token: 0x0400152D RID: 5421
		public static Terraria.Social.Base.CloudSocialModule Cloud;

		// Token: 0x0400152E RID: 5422
		public static Terraria.Social.Base.NetSocialModule Network;

		// Token: 0x0400152F RID: 5423
		public static Terraria.Social.Base.OverlaySocialModule Overlay;

		// Token: 0x04001530 RID: 5424
		public static Terraria.Social.Base.WorkshopSocialModule Workshop;

		// Token: 0x04001531 RID: 5425
		public static ServerJoinRequestsManager JoinRequests;

		// Token: 0x04001532 RID: 5426
		public static Terraria.Social.Base.PlatformSocialModule Platform;

		// Token: 0x04001533 RID: 5427
		private static List<ISocialModule> _modules;
	}
}
