using System;
using System.IO;
using ReLogic.OS;
using Steamworks;
using Terraria.ModLoader.UI;
using Terraria.Social;
using Terraria.Social.Steam;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002BA RID: 698
	internal class Steam
	{
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06002D46 RID: 11590 RVA: 0x0052D3D9 File Offset: 0x0052B5D9
		public static AppId_t TMLAppID_t
		{
			get
			{
				return new AppId_t(1281930U);
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x0052D3E5 File Offset: 0x0052B5E5
		public static AppId_t TerrariaAppId_t
		{
			get
			{
				return new AppId_t(105600U);
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x0052D3F1 File Offset: 0x0052B5F1
		public static bool CheckSteamCloudStorageSufficient(ulong input)
		{
			return SocialAPI.Cloud == null || input < Steam.lastAvailableSteamCloudStorage;
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x0052D404 File Offset: 0x0052B604
		public static void RecalculateAvailableSteamCloudStorage()
		{
			if (SocialAPI.Cloud != null)
			{
				ulong num;
				SteamRemoteStorage.GetQuota(ref num, ref Steam.lastAvailableSteamCloudStorage);
			}
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x0052D428 File Offset: 0x0052B628
		internal static void PostInit()
		{
			Steam.RecalculateAvailableSteamCloudStorage();
			Logging.Terraria.Info("Steam Cloud Quota: " + UIMemoryBar.SizeSuffix((long)Steam.lastAvailableSteamCloudStorage, 1) + " available");
			string branchName;
			bool OnBetaBranch = SteamApps.GetCurrentBetaName(ref branchName, 1000);
			Logging.tML.Info("Steam beta branch: " + (OnBetaBranch ? branchName : "None"));
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x0052D48C File Offset: 0x0052B68C
		public static string GetSteamTerrariaInstallDir()
		{
			string terrariaInstallLocation;
			SteamApps.GetAppInstallDir(Steam.TerrariaAppId_t, ref terrariaInstallLocation, 1000U);
			if (terrariaInstallLocation == null)
			{
				terrariaInstallLocation = "../Terraria";
				Logging.Terraria.Warn("Steam reports no terraria install directory. Falling back to " + terrariaInstallLocation);
			}
			if (Platform.IsOSX)
			{
				terrariaInstallLocation = Path.Combine(terrariaInstallLocation, "Terraria.app", "Contents", "Resources");
			}
			Logging.tML.Info("Terraria Steam Install Location assumed to be: " + terrariaInstallLocation);
			return terrariaInstallLocation;
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x0052D4FC File Offset: 0x0052B6FC
		internal static void SetAppId(AppId_t appId)
		{
			string steam_appid_path = "steam_appid.txt";
			if (Environment.GetEnvironmentVariable("SteamClientLaunch") != "1" || SteamedWraps.FamilyShared || InstallVerifier.DistributionPlatform == DistributionPlatform.GoG)
			{
				File.WriteAllText(steam_appid_path, appId.ToString());
				return;
			}
			try
			{
				File.Delete(steam_appid_path);
			}
			catch (IOException)
			{
			}
			if (Environment.GetEnvironmentVariable("SteamAppId") != appId.ToString())
			{
				throw new Exception("Cannot overwrite steam env. SteamAppId=" + Environment.GetEnvironmentVariable("SteamAppId"));
			}
		}

		// Token: 0x04001C3D RID: 7229
		public const uint TMLAppID = 1281930U;

		// Token: 0x04001C3E RID: 7230
		public const uint TerrariaAppID = 105600U;

		// Token: 0x04001C3F RID: 7231
		public static ulong lastAvailableSteamCloudStorage = ulong.MaxValue;
	}
}
