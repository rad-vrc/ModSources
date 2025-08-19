using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Engine;
using Terraria.ModLoader.UI;
using Terraria.Social;

namespace Terraria.Initializers
{
	// Token: 0x020003ED RID: 1005
	public static class LaunchInitializer
	{
		// Token: 0x060034D8 RID: 13528 RVA: 0x00566291 File Offset: 0x00564491
		public static void LoadParameters(Main game)
		{
			LaunchInitializer.LoadSharedParameters(game);
			if (Main.dedServ)
			{
				LaunchInitializer.LoadServerParameters(game);
			}
			LaunchInitializer.LoadClientParameters(game);
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x005662AC File Offset: 0x005644AC
		private static void LoadSharedParameters(Main game)
		{
			string path;
			if ((path = LaunchInitializer.TryParameter(new string[]
			{
				"-loadlib"
			})) != null)
			{
				game.loadLib(path);
			}
			string s;
			int result;
			if ((s = LaunchInitializer.TryParameter(new string[]
			{
				"-p",
				"-port"
			})) != null && int.TryParse(s, out result))
			{
				Netplay.ListenPort = result;
			}
			string modPack = LaunchInitializer.TryParameter(new string[]
			{
				"-modpack"
			});
			if (modPack != null)
			{
				ModOrganizer.commandLineModPack = modPack;
			}
			if (LaunchInitializer.HasParameter(new string[]
			{
				"-accuratememorytracking"
			}))
			{
				MemoryTracking.accurate = true;
			}
			if (LaunchInitializer.HasParameter(new string[]
			{
				"-skipselect"
			}))
			{
				string playerName = null;
				string worldName = null;
				string skipSelectArgs = LaunchInitializer.TryParameter(new string[]
				{
					"-skipselect"
				});
				if (skipSelectArgs != null)
				{
					Match i = new Regex("(?<name>.*?):(?<val>.*)").Match(skipSelectArgs);
					if (i.Success)
					{
						playerName = i.Groups["name"].Value;
						worldName = i.Groups["val"].Value;
					}
				}
				Func<WorldFileData, bool> <>9__1;
				Func<PlayerFileData, bool> <>9__2;
				ModLoader.OnSuccessfulLoad = (Action)Delegate.Combine(ModLoader.OnSuccessfulLoad, new Action(delegate()
				{
					WorldGen.clearWorld();
					Main.LoadWorlds();
					IEnumerable<WorldFileData> worldList = Main.WorldList;
					Func<WorldFileData, bool> predicate;
					if ((predicate = <>9__1) == null)
					{
						predicate = (<>9__1 = ((WorldFileData x) => x.Name == worldName));
					}
					(worldList.FirstOrDefault(predicate) ?? Main.WorldList[0]).SetAsActive();
					if (Main.dedServ)
					{
						return;
					}
					Main.LoadPlayers();
					IEnumerable<PlayerFileData> playerList = Main.PlayerList;
					Func<PlayerFileData, bool> predicate2;
					if ((predicate2 = <>9__2) == null)
					{
						predicate2 = (<>9__2 = ((PlayerFileData x) => x.Name == playerName));
					}
					(playerList.FirstOrDefault(predicate2) ?? Main.PlayerList[0]).SetAsActive();
					if (!UIWorldSelect.CanWorldBePlayed(Main.ActiveWorldFileData))
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(182, 2);
						defaultInterpolatedStringHandler.AppendLiteral("The selected character ");
						defaultInterpolatedStringHandler.AppendFormatted(playerName);
						defaultInterpolatedStringHandler.AppendLiteral(" can not be used with the selected world ");
						defaultInterpolatedStringHandler.AppendFormatted(worldName);
						defaultInterpolatedStringHandler.AppendLiteral(".\n");
						defaultInterpolatedStringHandler.AppendLiteral("This could be due to mismatched Journey Mode or other mod specific changes.");
						defaultInterpolatedStringHandler.AppendLiteral("Check in Game Menus for more information.");
						throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					WorldGen.playWorld();
				}));
			}
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x00566400 File Offset: 0x00564600
		private static void LoadClientParameters(Main game)
		{
			string iP;
			if ((iP = LaunchInitializer.TryParameter(new string[]
			{
				"-j",
				"-join"
			})) != null)
			{
				game.AutoJoin(iP, LaunchInitializer.TryParameter(new string[]
				{
					"-plr",
					"-player"
				}));
			}
			string arg;
			if ((arg = LaunchInitializer.TryParameter(new string[]
			{
				"-pass",
				"-password"
			})) != null)
			{
				Netplay.ServerPassword = Main.ConvertFromSafeArgument(arg);
				game.AutoPass();
			}
			if (LaunchInitializer.HasParameter(new string[]
			{
				"-host"
			}))
			{
				game.AutoHost();
			}
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x0056649C File Offset: 0x0056469C
		private static void LoadServerParameters(Main game)
		{
			if (!OperatingSystem.IsWindows())
			{
				if (LaunchInitializer.TryParameter(new string[]
				{
					"-forcepriority"
				}) != null)
				{
					Logging.tML.Warn("The -forcepriority command line parameter has no effect on non-Windows OS. You'll have to set priority manually within your OS.");
				}
			}
			else
			{
				try
				{
					string s;
					if ((s = LaunchInitializer.TryParameter(new string[]
					{
						"-forcepriority"
					})) != null)
					{
						Process currentProcess = Process.GetCurrentProcess();
						int result;
						if (int.TryParse(s, out result))
						{
							switch (result)
							{
							case 0:
								currentProcess.PriorityClass = ProcessPriorityClass.RealTime;
								break;
							case 1:
								currentProcess.PriorityClass = ProcessPriorityClass.High;
								break;
							case 2:
								currentProcess.PriorityClass = ProcessPriorityClass.AboveNormal;
								break;
							case 3:
								currentProcess.PriorityClass = ProcessPriorityClass.Normal;
								break;
							case 4:
								currentProcess.PriorityClass = ProcessPriorityClass.BelowNormal;
								break;
							case 5:
								currentProcess.PriorityClass = ProcessPriorityClass.Idle;
								break;
							default:
								currentProcess.PriorityClass = ProcessPriorityClass.High;
								break;
							}
						}
						else
						{
							currentProcess.PriorityClass = ProcessPriorityClass.High;
						}
					}
					else
					{
						Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
					}
				}
				catch
				{
				}
			}
			string value;
			if ((value = LaunchInitializer.TryParameter(new string[]
			{
				"-maxplayers",
				"-players"
			})) != null)
			{
				int num = Convert.ToInt32(value);
				if (num <= 255 && num >= 1)
				{
					game.SetNetPlayers(num);
				}
			}
			string arg;
			if ((arg = LaunchInitializer.TryParameter(new string[]
			{
				"-pass",
				"-password"
			})) != null)
			{
				Netplay.ServerPassword = Main.ConvertFromSafeArgument(arg);
			}
			string s2;
			int result2;
			if ((s2 = LaunchInitializer.TryParameter(new string[]
			{
				"-lang"
			})) != null && int.TryParse(s2, out result2))
			{
				LanguageManager.Instance.SetLanguage(result2);
			}
			if ((s2 = LaunchInitializer.TryParameter(new string[]
			{
				"-language"
			})) != null)
			{
				LanguageManager.Instance.SetLanguage(s2);
			}
			string publish = LaunchInitializer.TryParameter(new string[]
			{
				"-publish"
			});
			if (publish != null)
			{
				UIModSourceItem.PublishModCommandLine(publish);
			}
			string install = LaunchInitializer.TryParameter(new string[]
			{
				"-install"
			});
			if (install != null)
			{
				FileAssociationSupport.HandleFileAssociation(install);
			}
			string worldName;
			if ((worldName = LaunchInitializer.TryParameter(new string[]
			{
				"-worldname"
			})) != null)
			{
				game.SetWorldName(worldName);
			}
			string newMOTD;
			if ((newMOTD = LaunchInitializer.TryParameter(new string[]
			{
				"-motd"
			})) != null)
			{
				game.NewMOTD(newMOTD);
			}
			string modPath = LaunchInitializer.TryParameter(new string[]
			{
				"-modpath"
			});
			if (modPath != null)
			{
				ModOrganizer.modPath = modPath;
			}
			if (LaunchInitializer.HasParameter(new string[]
			{
				"-showserverconsole"
			}))
			{
				Main.showServerConsole = true;
			}
			string banFilePath;
			if ((banFilePath = LaunchInitializer.TryParameter(new string[]
			{
				"-banlist"
			})) != null)
			{
				Netplay.BanFilePath = banFilePath;
			}
			if (LaunchInitializer.HasParameter(new string[]
			{
				"-autoshutdown"
			}))
			{
				game.EnableAutoShutdown();
			}
			if (LaunchInitializer.HasParameter(new string[]
			{
				"-secure"
			}))
			{
				Netplay.SpamCheck = true;
			}
			string serverWorldRollbacks;
			if ((serverWorldRollbacks = LaunchInitializer.TryParameter(new string[]
			{
				"-worldrollbackstokeep"
			})) != null)
			{
				game.setServerWorldRollbacks(serverWorldRollbacks);
			}
			string worldSize;
			if ((worldSize = LaunchInitializer.TryParameter(new string[]
			{
				"-autocreate"
			})) != null)
			{
				game.autoCreate(worldSize);
			}
			if (LaunchInitializer.HasParameter(new string[]
			{
				"-noupnp"
			}))
			{
				Netplay.UseUPNP = false;
			}
			if (LaunchInitializer.HasParameter(new string[]
			{
				"-experimental"
			}))
			{
				Main.UseExperimentalFeatures = true;
			}
			string world;
			if ((world = LaunchInitializer.TryParameter(new string[]
			{
				"-world"
			})) != null)
			{
				game.SetWorld(world, false);
			}
			else if (SocialAPI.Mode == SocialMode.Steam && (world = LaunchInitializer.TryParameter(new string[]
			{
				"-cloudworld"
			})) != null)
			{
				game.SetWorld(world, true);
			}
			string configPath;
			if ((configPath = LaunchInitializer.TryParameter(new string[]
			{
				"-config"
			})) != null)
			{
				game.LoadDedConfig(configPath);
			}
			string autogenSeedName;
			if ((autogenSeedName = LaunchInitializer.TryParameter(new string[]
			{
				"-seed"
			})) != null)
			{
				Main.AutogenSeedName = autogenSeedName;
			}
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x00566880 File Offset: 0x00564A80
		private static bool HasParameter(params string[] keys)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				if (Program.LaunchParameters.ContainsKey(keys[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x005668B0 File Offset: 0x00564AB0
		public static string TryParameter(params string[] keys)
		{
			int i = 0;
			while (i < keys.Length)
			{
				string value;
				if (Program.LaunchParameters.TryGetValue(keys[i], out value))
				{
					if (value == null)
					{
						return "";
					}
					return value;
				}
				else
				{
					i++;
				}
			}
			return null;
		}
	}
}
