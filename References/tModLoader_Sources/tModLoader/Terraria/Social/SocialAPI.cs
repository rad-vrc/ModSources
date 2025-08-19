using System;
using System.Collections.Generic;
using ReLogic.OS;
using Terraria.ModLoader;
using Terraria.ModLoader.Engine;
using Terraria.Social.Base;
using Terraria.Social.Steam;
using Terraria.Social.WeGame;

namespace Terraria.Social
{
	// Token: 0x020000C9 RID: 201
	public static class SocialAPI
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x004B54DD File Offset: 0x004B36DD
		public static SocialMode Mode
		{
			get
			{
				return SocialAPI._mode;
			}
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x004B54E4 File Offset: 0x004B36E4
		public static void Initialize(SocialMode? mode = null)
		{
			if (mode == null)
			{
				mode = new SocialMode?(SocialMode.None);
				if (!Main.dedServ && !SteamedWraps.FamilyShared)
				{
					if (InstallVerifier.DistributionPlatform == DistributionPlatform.Steam)
					{
						mode = new SocialMode?(SocialMode.Steam);
					}
				}
				else if (Program.LaunchParameters.ContainsKey("-steam"))
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
			SteamedWraps.Initialize();
			Logging.tML.Info("Current 1281930 Workshop Folder Directory: " + WorkshopHelper.GetWorkshopFolder(Steam.TMLAppID_t));
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x004B55B0 File Offset: 0x004B37B0
		public static void Shutdown()
		{
			SocialAPI._modules.Reverse();
			foreach (ISocialModule socialModule in SocialAPI._modules)
			{
				socialModule.Shutdown();
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x004B560C File Offset: 0x004B380C
		private static T LoadModule<T>() where T : ISocialModule, new()
		{
			T val = Activator.CreateInstance<T>();
			SocialAPI._modules.Add(val);
			return val;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x004B5630 File Offset: 0x004B3830
		private static T LoadModule<T>(T module) where T : ISocialModule
		{
			SocialAPI._modules.Add(module);
			return module;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x004B5643 File Offset: 0x004B3843
		private static void LoadDiscord()
		{
			if (!Main.dedServ && (ReLogic.OS.Platform.IsWindows || Environment.Is64BitOperatingSystem))
			{
				bool is64BitProcess = Environment.Is64BitProcess;
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x004B5660 File Offset: 0x004B3860
		internal static void LoadSteam()
		{
			if (SocialAPI._steamAPILoaded)
			{
				return;
			}
			SocialAPI.LoadModule<Terraria.Social.Steam.CoreSocialModule>();
			SocialAPI.Friends = SocialAPI.LoadModule<Terraria.Social.Steam.FriendsSocialModule>();
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
			foreach (ISocialModule socialModule in SocialAPI._modules)
			{
				socialModule.Initialize();
			}
			SocialAPI._steamAPILoaded = true;
			Steam.PostInit();
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x004B5718 File Offset: 0x004B3918
		private static void LoadWeGame()
		{
			if (SocialAPI._wegameAPILoaded)
			{
				return;
			}
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
			WeGameHelper.WriteDebugString("LoadWeGame modules", Array.Empty<object>());
			foreach (ISocialModule socialModule in SocialAPI._modules)
			{
				socialModule.Initialize();
			}
			SocialAPI._wegameAPILoaded = true;
		}

		// Token: 0x040012B7 RID: 4791
		private static SocialMode _mode;

		// Token: 0x040012B8 RID: 4792
		public static Terraria.Social.Base.FriendsSocialModule Friends;

		// Token: 0x040012B9 RID: 4793
		public static Terraria.Social.Base.AchievementsSocialModule Achievements;

		// Token: 0x040012BA RID: 4794
		public static Terraria.Social.Base.CloudSocialModule Cloud;

		// Token: 0x040012BB RID: 4795
		public static Terraria.Social.Base.NetSocialModule Network;

		// Token: 0x040012BC RID: 4796
		public static Terraria.Social.Base.OverlaySocialModule Overlay;

		// Token: 0x040012BD RID: 4797
		public static Terraria.Social.Base.WorkshopSocialModule Workshop;

		// Token: 0x040012BE RID: 4798
		public static ServerJoinRequestsManager JoinRequests;

		// Token: 0x040012BF RID: 4799
		public static Terraria.Social.Base.PlatformSocialModule Platform;

		// Token: 0x040012C0 RID: 4800
		private static List<ISocialModule> _modules;

		// Token: 0x040012C1 RID: 4801
		private static bool _steamAPILoaded;

		// Token: 0x040012C2 RID: 4802
		private static bool _wegameAPILoaded;
	}
}
