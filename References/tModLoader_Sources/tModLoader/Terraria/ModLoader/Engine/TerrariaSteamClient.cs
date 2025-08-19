using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using log4net;
using Steamworks;
using Terraria.Social.Steam;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002BB RID: 699
	internal class TerrariaSteamClient
	{
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06002D4F RID: 11599 RVA: 0x0052D5AD File Offset: 0x0052B7AD
		private static ILog Logger { get; } = LogManager.GetLogger("TerrariaSteamClient");

		// Token: 0x06002D50 RID: 11600 RVA: 0x0052D5B4 File Offset: 0x0052B7B4
		internal static TerrariaSteamClient.LaunchResult Launch()
		{
			if (Environment.GetEnvironmentVariable("SteamClientLaunch") != "1")
			{
				TerrariaSteamClient.Logger.Debug("Disabled. Launched outside steam client.");
				return TerrariaSteamClient.LaunchResult.Ok;
			}
			TerrariaSteamClient.serverPipe = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);
			string tMLName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
			Process proc = new Process
			{
				StartInfo = 
				{
					FileName = Environment.ProcessPath,
					Arguments = tMLName + " -terrariasteamclient " + TerrariaSteamClient.serverPipe.GetClientHandleAsString(),
					UseShellExecute = false,
					RedirectStandardOutput = true
				}
			};
			foreach (string i in ((IEnumerable<string>)proc.StartInfo.EnvironmentVariables.Keys).ToArray<string>())
			{
				if (i.StartsWith("steam", StringComparison.InvariantCultureIgnoreCase))
				{
					proc.StartInfo.EnvironmentVariables.Remove(i);
				}
			}
			proc.Start();
			for (;;)
			{
				string text = proc.StandardOutput.ReadLine();
				string line = (text != null) ? text.Trim() : null;
				if (line == null)
				{
					if (proc.HasExited)
					{
						break;
					}
				}
				else
				{
					TerrariaSteamClient.Logger.Debug("Recv: " + line);
					if (line == TerrariaSteamClient.MsgInitFailed)
					{
						return TerrariaSteamClient.LaunchResult.ErrSteamInitFailed;
					}
					if (line == TerrariaSteamClient.MsgNotInstalled)
					{
						return TerrariaSteamClient.LaunchResult.ErrNotInstalled;
					}
					if (line == TerrariaSteamClient.MsgInstallOutOfDate)
					{
						return TerrariaSteamClient.LaunchResult.ErrInstallOutOfDate;
					}
					if (line == TerrariaSteamClient.MsgInstallLegacyVersion)
					{
						return TerrariaSteamClient.LaunchResult.ErrInstallLegacyVersion;
					}
					if (line == TerrariaSteamClient.MsgInitSuccess)
					{
						goto IL_18E;
					}
					if (line == TerrariaSteamClient.MsgFamilyShared)
					{
						SteamedWraps.FamilyShared = true;
					}
				}
			}
			return TerrariaSteamClient.LaunchResult.ErrClientProcDied;
			IL_18E:
			TerrariaSteamClient.SendCmd(TerrariaSteamClient.MsgAck);
			Thread.Sleep(300);
			return TerrariaSteamClient.LaunchResult.Ok;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x0052D764 File Offset: 0x0052B964
		private static void SendCmd(string cmd)
		{
			if (TerrariaSteamClient.serverPipe == null)
			{
				return;
			}
			TerrariaSteamClient.Logger.Debug("Send: " + cmd);
			using (StreamWriter sw = new StreamWriter(TerrariaSteamClient.serverPipe, null, -1, true))
			{
				sw.WriteLine(cmd);
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x0052D7C0 File Offset: 0x0052B9C0
		internal static void Run()
		{
			Logging.Init(Logging.LogFile.TerrariaSteamClient);
			TerrariaSteamClient.Logger.InfoFormat("Working Directory: {0}", Path.GetFullPath(Directory.GetCurrentDirectory()));
			TerrariaSteamClient.Logger.InfoFormat("Args: {0}", string.Join(' ', Environment.GetCommandLineArgs()));
			TerrariaSteamClient.Logger.Info("Setting steam app id to " + Steam.TerrariaAppId_t.ToString());
			Steam.SetAppId(Steam.TerrariaAppId_t);
			bool steamInit = false;
			try
			{
				using (AnonymousPipeClientStream clientPipe = new AnonymousPipeClientStream(PipeDirection.In, Program.LaunchParameters["-terrariasteamclient"]))
				{
					using (StreamReader sr = new StreamReader(clientPipe, null, true, -1, true))
					{
						Func<string> Recv = delegate()
						{
							string text = sr.ReadLine();
							string s = (text != null) ? text.Trim() : null;
							if (s == null)
							{
								throw new EndOfStreamException();
							}
							TerrariaSteamClient.Logger.Debug("Recv: " + s);
							return s;
						};
						Action<string> Send = delegate(string s)
						{
							TerrariaSteamClient.Logger.Debug("Send: " + s);
							Console.WriteLine(s);
						};
						TerrariaSteamClient.Logger.Info("SteamAPI.Init()");
						steamInit = SteamAPI.Init();
						if (!steamInit)
						{
							TerrariaSteamClient.Logger.Fatal("SteamAPI.Init() failed");
							Send(TerrariaSteamClient.MsgInitFailed);
							return;
						}
						if (!SteamApps.BIsAppInstalled(Steam.TerrariaAppId_t))
						{
							ILog logger = TerrariaSteamClient.Logger;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 1);
							defaultInterpolatedStringHandler.AppendLiteral("SteamApps.BIsAppInstalled(");
							defaultInterpolatedStringHandler.AppendFormatted<AppId_t>(Steam.TerrariaAppId_t);
							defaultInterpolatedStringHandler.AppendLiteral("): false");
							logger.Fatal(defaultInterpolatedStringHandler.ToStringAndClear());
							Send(TerrariaSteamClient.MsgNotInstalled);
							TerrariaSteamClient.SteamShutdown();
							return;
						}
						int TerrariaBuildID = SteamApps.GetAppBuildId();
						TerrariaSteamClient.Logger.Info("Terraria BuildID: " + TerrariaBuildID.ToString());
						if (TerrariaSteamClient.LegacyTerrariaBuildIDs.Contains(TerrariaBuildID))
						{
							TerrariaSteamClient.Logger.Fatal("Terraria is on a legacy version, you need to switch back to the normal Terraria version in Steam for tModLoader to load.");
							Send(TerrariaSteamClient.MsgInstallLegacyVersion);
							TerrariaSteamClient.SteamShutdown();
							return;
						}
						if (TerrariaBuildID < 9965506)
						{
							TerrariaSteamClient.Logger.Fatal("Terraria is out of date, you need to update Terraria in Steam.");
							Send(TerrariaSteamClient.MsgInstallOutOfDate);
							TerrariaSteamClient.SteamShutdown();
							return;
						}
						if (SteamApps.BIsSubscribedFromFamilySharing())
						{
							TerrariaSteamClient.Logger.Info("Terraria is installed via Family Share. Re-pathing tModLoader required");
							Send(TerrariaSteamClient.MsgFamilyShared);
						}
						Send(TerrariaSteamClient.MsgInitSuccess);
						while (Recv() != TerrariaSteamClient.MsgAck)
						{
						}
						for (;;)
						{
							Thread.Sleep(250);
							string nextCMD = Recv();
							if (nextCMD == TerrariaSteamClient.MsgShutdown)
							{
								break;
							}
							if (nextCMD.StartsWith(TerrariaSteamClient.MsgGrant))
							{
								string achievement = nextCMD.Substring(TerrariaSteamClient.MsgGrant.Length);
								bool pbAchieved;
								SteamUserStats.GetAchievement(achievement, ref pbAchieved);
								if (!pbAchieved)
								{
									SteamUserStats.SetAchievement(achievement);
								}
							}
							if (nextCMD == TerrariaSteamClient.MsgInvalidateTerrariaInstall)
							{
								SteamApps.MarkContentCorrupt(false);
							}
						}
					}
				}
			}
			catch (EndOfStreamException)
			{
				TerrariaSteamClient.Logger.Info("The connection to tML was closed unexpectedly. Look in client.log or server.log for details");
			}
			catch (Exception ex)
			{
				TerrariaSteamClient.Logger.Fatal("Unhandled error", ex);
			}
			if (steamInit)
			{
				TerrariaSteamClient.SteamShutdown();
			}
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x0052DB18 File Offset: 0x0052BD18
		private static void SteamShutdown()
		{
			try
			{
				TerrariaSteamClient.Logger.Info("SteamAPI.Shutdown()");
				SteamAPI.Shutdown();
			}
			catch (Exception ex)
			{
				TerrariaSteamClient.Logger.Error("Error shutting down SteamAPI", ex);
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x0052DB60 File Offset: 0x0052BD60
		private static void MarkSteamTerrariaInstallCorrupt()
		{
			try
			{
				TerrariaSteamClient.Logger.Info("Marking Steam Terraria Installation as Corrupt to force 'Verify Local Files' on next run");
				TerrariaSteamClient.SendCmd(TerrariaSteamClient.MsgInvalidateTerrariaInstall);
			}
			catch
			{
			}
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x0052DB9C File Offset: 0x0052BD9C
		internal static void Shutdown()
		{
			try
			{
				TerrariaSteamClient.SendCmd(TerrariaSteamClient.MsgShutdown);
			}
			catch
			{
			}
		}

		// Token: 0x04001C41 RID: 7233
		private const int LatestTerrariaBuildID = 9965506;

		// Token: 0x04001C42 RID: 7234
		private static int[] LegacyTerrariaBuildIDs = new int[]
		{
			14381608,
			14381510,
			16547892,
			18467854
		};

		// Token: 0x04001C43 RID: 7235
		private static AnonymousPipeServerStream serverPipe;

		// Token: 0x04001C44 RID: 7236
		private static string MsgInitFailed = "init_failed";

		// Token: 0x04001C45 RID: 7237
		private static string MsgInitSuccess = "init_success";

		// Token: 0x04001C46 RID: 7238
		private static string MsgFamilyShared = "family_shared";

		// Token: 0x04001C47 RID: 7239
		private static string MsgNotInstalled = "not_installed";

		// Token: 0x04001C48 RID: 7240
		private static string MsgInstallOutOfDate = "install_out_of_date";

		// Token: 0x04001C49 RID: 7241
		private static string MsgInstallLegacyVersion = "install_legacy_version";

		// Token: 0x04001C4A RID: 7242
		private static string MsgGrant = "grant:";

		// Token: 0x04001C4B RID: 7243
		private static string MsgAck = "acknowledged";

		// Token: 0x04001C4C RID: 7244
		private static string MsgShutdown = "shutdown";

		// Token: 0x04001C4D RID: 7245
		private static string MsgInvalidateTerrariaInstall = "corrupt_install";

		// Token: 0x02000A6F RID: 2671
		public enum LaunchResult
		{
			// Token: 0x04006D10 RID: 27920
			ErrClientProcDied,
			// Token: 0x04006D11 RID: 27921
			ErrSteamInitFailed,
			// Token: 0x04006D12 RID: 27922
			ErrNotInstalled,
			// Token: 0x04006D13 RID: 27923
			ErrInstallOutOfDate,
			// Token: 0x04006D14 RID: 27924
			ErrInstallLegacyVersion,
			// Token: 0x04006D15 RID: 27925
			Ok
		}
	}
}
