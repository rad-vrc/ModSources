using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using log4net;
using ReLogic.OS;
using Terraria.Localization;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002B4 RID: 692
	internal static class InstallVerifier
	{
		// Token: 0x06002D1D RID: 11549 RVA: 0x0052C38C File Offset: 0x0052A58C
		static InstallVerifier()
		{
			if (Platform.IsWindows)
			{
				if (IntPtr.Size == 4)
				{
					InstallVerifier.steamAPIPath = "Libraries/Native/Windows32/steam_api.dll";
					InstallVerifier.steamAPIHash = InstallVerifier.ToByteArray("56d9f94d37cb8f03049a1cc3062bffaf", true);
				}
				else
				{
					InstallVerifier.steamAPIPath = "Libraries/Native/Windows/steam_api64.dll";
					InstallVerifier.steamAPIHash = InstallVerifier.ToByteArray("500475b20083ccdc64f12d238cab687a", true);
				}
				InstallVerifier.vanillaSteamAPI = "steam_api.dll";
				InstallVerifier.gogHash = InstallVerifier.ToByteArray("efccd835e6b54697e05e8a4b72d935cd", true);
				InstallVerifier.steamHash = InstallVerifier.ToByteArray("4530e0acfa4c789f462addb77b405ccb", true);
				return;
			}
			if (Platform.IsOSX)
			{
				InstallVerifier.steamAPIPath = "Libraries/Native/OSX/libsteam_api64.dylib";
				InstallVerifier.steamAPIHash = InstallVerifier.ToByteArray("801e9bf5e5899a41c5999811d870b1ca", true);
				InstallVerifier.vanillaSteamAPI = "libsteam_api.dylib";
				InstallVerifier.gogHash = InstallVerifier.ToByteArray("da2b740b4c6031df3a8b1f68b40cb82b", true);
				InstallVerifier.steamHash = InstallVerifier.ToByteArray("4512beef5d7607fa1771c3fdf6cdc712", true);
				return;
			}
			if (Platform.IsLinux)
			{
				InstallVerifier.steamAPIPath = "Libraries/Native/Linux/libsteam_api64.so";
				InstallVerifier.steamAPIHash = InstallVerifier.ToByteArray("ccdf20f0b2f9abbe1fea8314b9fab096", true);
				InstallVerifier.vanillaSteamAPI = "libsteam_api.so";
				InstallVerifier.gogHash = InstallVerifier.ToByteArray("9db40ef7cd4b37794cfe29e8866bb6b4", true);
				InstallVerifier.steamHash = InstallVerifier.ToByteArray("2ff21c600897a9485ca5ae645a06202d", true);
				return;
			}
			ErrorReporting.FatalExit(Language.GetTextValue("tModLoader.UnknownVerificationOS"));
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x0052C4C4 File Offset: 0x0052A6C4
		private static bool HashMatchesFile(string path, byte[] hash)
		{
			bool result;
			using (MD5 md5 = MD5.Create())
			{
				using (FileStream stream = File.OpenRead(path))
				{
					result = hash.SequenceEqual(md5.ComputeHash(stream));
				}
			}
			return result;
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x0052C520 File Offset: 0x0052A720
		private static byte[] ToByteArray(string hexString, bool forceLowerCase = true)
		{
			if (forceLowerCase)
			{
				hexString = hexString.ToLower();
			}
			byte[] retval = new byte[hexString.Length / 2];
			for (int i = 0; i < hexString.Length; i += 2)
			{
				retval[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
			}
			return retval;
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x0052C56C File Offset: 0x0052A76C
		internal static void Startup()
		{
			string detectionDetails;
			InstallVerifier.DistributionPlatform = InstallVerifier.DetectPlatform(out detectionDetails);
			ILog tML = Logging.tML;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Distribution Platform: ");
			defaultInterpolatedStringHandler.AppendFormatted<DistributionPlatform>(InstallVerifier.DistributionPlatform);
			defaultInterpolatedStringHandler.AppendLiteral(". Detection method: ");
			defaultInterpolatedStringHandler.AppendFormatted(detectionDetails);
			tML.Info(defaultInterpolatedStringHandler.ToStringAndClear());
			if (InstallVerifier.DistributionPlatform == DistributionPlatform.GoG)
			{
				InstallVerifier.CheckGoG();
				return;
			}
			InstallVerifier.CheckSteam();
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x0052C5E0 File Offset: 0x0052A7E0
		private static DistributionPlatform DetectPlatform(out string detectionDetails)
		{
			if (Program.LaunchParameters.ContainsKey("-steam"))
			{
				detectionDetails = "-steam launch parameter";
				return DistributionPlatform.Steam;
			}
			if (Directory.GetCurrentDirectory().Contains("steamapps", StringComparison.OrdinalIgnoreCase))
			{
				detectionDetails = "CWD is /steamapps/";
				return DistributionPlatform.Steam;
			}
			string vanillaSteamAPIDir;
			if (!InstallVerifier.ObtainVanillaExePath(out vanillaSteamAPIDir, out InstallVerifier.vanillaExePath))
			{
				detectionDetails = InstallVerifier.VanillaExe + " or " + InstallVerifier.CheckExe + " not found nearby";
				return DistributionPlatform.Steam;
			}
			if (Platform.IsOSX)
			{
				vanillaSteamAPIDir = Path.Combine(Directory.GetParent(vanillaSteamAPIDir).FullName, "MacOS", "osx");
			}
			if (File.Exists(Path.Combine(vanillaSteamAPIDir, InstallVerifier.vanillaSteamAPI)))
			{
				detectionDetails = InstallVerifier.vanillaSteamAPI + " found";
				return DistributionPlatform.Steam;
			}
			detectionDetails = Path.GetFileName(InstallVerifier.vanillaExePath) + " found, no steam files or directories nearby.";
			return DistributionPlatform.GoG;
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x0052C6AC File Offset: 0x0052A8AC
		private static bool ObtainVanillaExePath(out string vanillaPath, out string exePath)
		{
			foreach (string possibleVanillaInstallFolder in InstallVerifier.GetPossibleVanillaInstallFolders())
			{
				if (InstallVerifier.CheckForExe(possibleVanillaInstallFolder, out exePath))
				{
					vanillaPath = possibleVanillaInstallFolder;
					return true;
				}
			}
			string text;
			exePath = (text = null);
			vanillaPath = text;
			return false;
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x0052C70C File Offset: 0x0052A90C
		private static IEnumerable<string> GetPossibleVanillaInstallFolders()
		{
			string vanillaPath = Directory.GetCurrentDirectory();
			yield return vanillaPath;
			vanillaPath = Directory.GetParent(vanillaPath).FullName;
			yield return vanillaPath;
			vanillaPath = Path.Combine(vanillaPath, "Terraria");
			if (Platform.IsOSX)
			{
				if (Directory.Exists("../Terraria/Terraria.app/"))
				{
					vanillaPath = "../Terraria/Terraria.app/Contents/Resources/";
				}
				else if (Directory.Exists("../Terraria.app/"))
				{
					vanillaPath = "../Terraria.app/Contents/Resources/";
				}
			}
			yield return vanillaPath;
			if (Platform.IsLinux)
			{
				yield return Path.Combine(vanillaPath, "game");
			}
			if (Platform.IsWindows)
			{
				yield return Path.Combine(new string[]
				{
					"c:\\",
					"Program Files (x86)",
					"GOG Galaxy",
					"Games",
					"Terraria"
				});
				yield return Path.Combine("c:\\", "GOG Games", "Terraria");
			}
			else if (Platform.IsLinux)
			{
				yield return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "GOG Games", "Terraria");
			}
			else
			{
				yield return Path.Combine("/Applications", "Terraria.app", "Contents", "Resources");
			}
			yield break;
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x0052C715 File Offset: 0x0052A915
		private static bool CheckForExe(string vanillaPath, out string exePath)
		{
			exePath = Path.Combine(vanillaPath, InstallVerifier.CheckExe);
			if (File.Exists(exePath))
			{
				return true;
			}
			exePath = Path.Combine(vanillaPath, InstallVerifier.VanillaExe);
			return File.Exists(exePath);
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x0052C748 File Offset: 0x0052A948
		private static void CheckSteam()
		{
			if (!InstallVerifier.HashMatchesFile(InstallVerifier.steamAPIPath, InstallVerifier.steamAPIHash))
			{
				Utils.OpenToURL("https://terraria.org");
				ErrorReporting.FatalExit(Language.GetTextValue("tModLoader.SteamAPIHashMismatch"));
				return;
			}
			if (Main.dedServ)
			{
				return;
			}
			TerrariaSteamClient.LaunchResult result = TerrariaSteamClient.Launch();
			switch (result)
			{
			case TerrariaSteamClient.LaunchResult.ErrClientProcDied:
				ErrorReporting.FatalExit("The terraria steam client process exited unexpectedly");
				return;
			case TerrariaSteamClient.LaunchResult.ErrSteamInitFailed:
				ErrorReporting.FatalExit(Language.GetTextValue("tModLoader.SteamInitFailed"));
				return;
			case TerrariaSteamClient.LaunchResult.ErrNotInstalled:
				ErrorReporting.FatalExit(Language.GetTextValue("tModLoader.TerrariaNotInstalled"));
				return;
			case TerrariaSteamClient.LaunchResult.ErrInstallOutOfDate:
				Utils.OpenToURL("https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Usage-FAQ#terraria-is-out-of-date-or-terraria-is-on-a-legacy-version");
				ErrorReporting.FatalExit(Language.GetTextValue("tModLoader.TerrariaOutOfDateMessage"));
				return;
			case TerrariaSteamClient.LaunchResult.ErrInstallLegacyVersion:
				Utils.OpenToURL("https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Usage-FAQ#terraria-is-out-of-date-or-terraria-is-on-a-legacy-version");
				ErrorReporting.FatalExit(Language.GetTextValue("tModLoader.TerrariaLegacyBranchMessage"));
				return;
			case TerrariaSteamClient.LaunchResult.Ok:
				return;
			default:
				throw new Exception("Unsupported result type: " + result.ToString());
			}
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x0052C82C File Offset: 0x0052AA2C
		private static void CheckGoG()
		{
			if (!InstallVerifier.HashMatchesFile(InstallVerifier.vanillaExePath, InstallVerifier.gogHash) && !InstallVerifier.HashMatchesFile(InstallVerifier.vanillaExePath, InstallVerifier.steamHash))
			{
				ErrorReporting.FatalExit(Language.GetTextValue("tModLoader.GOGHashMismatch", InstallVerifier.vanillaExePath, "1.4.4.9", InstallVerifier.CheckExe));
			}
			if (Path.GetFileName(InstallVerifier.vanillaExePath) != InstallVerifier.CheckExe)
			{
				string pathToCheckExe = Path.Combine(Path.GetDirectoryName(InstallVerifier.vanillaExePath), InstallVerifier.CheckExe);
				Logging.tML.Info("Backing up " + Path.GetFileName(InstallVerifier.vanillaExePath) + " to " + InstallVerifier.CheckExe);
				File.Copy(InstallVerifier.vanillaExePath, pathToCheckExe);
			}
		}

		// Token: 0x04001C25 RID: 7205
		private static string VanillaExe = "Terraria.exe";

		// Token: 0x04001C26 RID: 7206
		private const string TerrariaVersion = "1.4.4.9";

		// Token: 0x04001C27 RID: 7207
		private static string CheckExe = "Terraria_v1.4.4.9.exe";

		// Token: 0x04001C28 RID: 7208
		internal static string vanillaExePath;

		// Token: 0x04001C29 RID: 7209
		public static DistributionPlatform DistributionPlatform;

		// Token: 0x04001C2A RID: 7210
		private static string steamAPIPath;

		// Token: 0x04001C2B RID: 7211
		private static string vanillaSteamAPI;

		// Token: 0x04001C2C RID: 7212
		private static byte[] steamAPIHash;

		// Token: 0x04001C2D RID: 7213
		private static byte[] gogHash;

		// Token: 0x04001C2E RID: 7214
		private static byte[] steamHash;
	}
}
