using System;
using System.Diagnostics;
using Terraria.Localization;
using Terraria.Social;

namespace Terraria.Initializers
{
	// Token: 0x020000E4 RID: 228
	public static class LaunchInitializer
	{
		// Token: 0x0600157E RID: 5502 RVA: 0x004BC2E3 File Offset: 0x004BA4E3
		public static void LoadParameters(Main game)
		{
			LaunchInitializer.LoadSharedParameters(game);
			if (Main.dedServ)
			{
				LaunchInitializer.LoadServerParameters(game);
			}
			LaunchInitializer.LoadClientParameters(game);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x004BC300 File Offset: 0x004BA500
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
			int listenPort;
			if ((s = LaunchInitializer.TryParameter(new string[]
			{
				"-p",
				"-port"
			})) != null && int.TryParse(s, out listenPort))
			{
				Netplay.ListenPort = listenPort;
			}
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x004BC35C File Offset: 0x004BA55C
		private static void LoadClientParameters(Main game)
		{
			string ip;
			if ((ip = LaunchInitializer.TryParameter(new string[]
			{
				"-j",
				"-join"
			})) != null)
			{
				game.AutoJoin(ip);
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

		// Token: 0x06001581 RID: 5505 RVA: 0x004BC3DC File Offset: 0x004BA5DC
		private static void LoadServerParameters(Main game)
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
					int num;
					if (int.TryParse(s, out num))
					{
						switch (num)
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
			string value;
			if ((value = LaunchInitializer.TryParameter(new string[]
			{
				"-maxplayers",
				"-players"
			})) != null)
			{
				int num2 = Convert.ToInt32(value);
				if (num2 <= 255 && num2 >= 1)
				{
					game.SetNetPlayers(num2);
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
			string text;
			int language;
			if ((text = LaunchInitializer.TryParameter(new string[]
			{
				"-lang"
			})) != null && int.TryParse(text, out language))
			{
				LanguageManager.Instance.SetLanguage(language);
			}
			if ((text = LaunchInitializer.TryParameter(new string[]
			{
				"-language"
			})) != null)
			{
				LanguageManager.Instance.SetLanguage(text);
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

		// Token: 0x06001582 RID: 5506 RVA: 0x004BC710 File Offset: 0x004BA910
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

		// Token: 0x06001583 RID: 5507 RVA: 0x004BC740 File Offset: 0x004BA940
		private static string TryParameter(params string[] keys)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				string text;
				if (Program.LaunchParameters.TryGetValue(keys[i], out text))
				{
					if (text == null)
					{
						text = "";
					}
					return text;
				}
			}
			return null;
		}
	}
}
