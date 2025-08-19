using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI.DownloadManager;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.Social.Steam;
using Terraria.UI.Chat;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200023A RID: 570
	internal static class Interface
	{
		// Token: 0x060028D3 RID: 10451 RVA: 0x0050E734 File Offset: 0x0050C934
		internal static void AddMenuButtons(Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons)
		{
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x0050E736 File Offset: 0x0050C936
		internal static void ResetData()
		{
			Interface.modBrowser.Reset();
			GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x0050E750 File Offset: 0x0050C950
		internal static void ModLoaderMenus(Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, int[] buttonVerticalSpacing, ref int offY, ref int spacing, ref int numButtons, ref bool backButtonDown)
		{
			if (Main.menuMode == 10002)
			{
				if (ModLoader.ShowFirstLaunchWelcomeMessage)
				{
					ModLoader.ShowFirstLaunchWelcomeMessage = false;
					Interface.infoMessage.Show(Language.GetTextValue("tModLoader.FirstLaunchWelcomeMessage"), Main.menuMode, null, "", null, null);
				}
				else if (SteamedWraps.FamilyShared && !ModLoader.WarnedFamilyShare && !ModLoader.WarnedFamilyShareDontShowAgain)
				{
					ModLoader.WarnedFamilyShare = true;
					Interface.infoMessage.Show(Language.GetTextValue("tModLoader.SteamFamilyShareWarning"), Main.menuMode, null, Language.GetTextValue("tModLoader.DontShowAgain"), delegate
					{
						ModLoader.WarnedFamilyShareDontShowAgain = true;
						Main.SaveSettings();
					}, null);
				}
				else if (ModLoader.ShowWhatsNew)
				{
					ModLoader.ShowWhatsNew = false;
					if (File.Exists("RecentGitHubCommits.txt"))
					{
						bool LastLaunchedShaInRecentGitHubCommits = false;
						StringBuilder messages = new StringBuilder();
						foreach (string text in File.ReadLines("RecentGitHubCommits.txt"))
						{
							string[] parts = text.Split(' ', 2, StringSplitOptions.None);
							if (parts.Length == 2)
							{
								string sha = parts[0];
								string message = parts[1];
								if (sha != ModLoader.LastLaunchedTModLoaderAlphaSha)
								{
									messages.Append("\n  " + message);
								}
								if (sha == ModLoader.LastLaunchedTModLoaderAlphaSha)
								{
									LastLaunchedShaInRecentGitHubCommits = true;
									break;
								}
							}
						}
						string compareUrl = ModLoader.LastLaunchedTModLoaderAlphaSha + "...preview";
						if (!LastLaunchedShaInRecentGitHubCommits)
						{
							messages.Append("\n...and more");
							compareUrl = "stable...preview";
						}
						Interface.infoMessage.Show(Language.GetTextValue("tModLoader.WhatsNewMessage") + messages.ToString(), Main.menuMode, null, Language.GetTextValue("tModLoader.ViewOnGitHub"), delegate
						{
							Utils.OpenToURL("https://github.com/tModLoader/tModLoader/compare/" + compareUrl);
						}, null);
					}
					else
					{
						Interface.infoMessage.Show(Language.GetTextValue("tModLoader.WhatsNewMessage") + "Unknown, somehow RecentGitHubCommits.txt is missing.", Main.menuMode, null, Language.GetTextValue("tModLoader.ViewOnGitHub"), delegate
						{
							Utils.OpenToURL("https://github.com/tModLoader/tModLoader/compare/stable...preview");
						}, null);
					}
				}
				else if (ModLoader.PreviewFreezeNotification)
				{
					ModLoader.PreviewFreezeNotification = false;
					ModLoader.LastPreviewFreezeNotificationSeen = BuildInfo.tMLVersion.MajorMinor();
					Interface.infoMessage.Show(Language.GetTextValue("tModLoader.WelcomeMessagePreview"), Main.menuMode, null, Language.GetTextValue("tModLoader.ModsMoreInfo"), delegate
					{
						Utils.OpenToURL("https://github.com/tModLoader/tModLoader/wiki/tModLoader-Release-Cycle#144");
					}, null);
					Main.SaveSettings();
				}
				else if (!ModLoader.DownloadedDependenciesOnStartup)
				{
					Interface.<>c__DisplayClass42_1 CS$<>8__locals2 = new Interface.<>c__DisplayClass42_1();
					ModLoader.DownloadedDependenciesOnStartup = true;
					CS$<>8__locals2.missingDeps = ModOrganizer.IdentifyMissingWorkshopDependencies().ToList<string>();
					string message2 = ModOrganizer.DetectModChangesForInfoMessage(out CS$<>8__locals2.removedMods) ?? "";
					if (CS$<>8__locals2.missingDeps.Any<string>())
					{
						message2 = message2 + Language.GetTextValue("tModLoader.DependenciesNeededForOtherMods") + "\n  " + string.Join("\n  ", CS$<>8__locals2.missingDeps);
					}
					message2 = message2.Trim('\n');
					bool flag = CS$<>8__locals2.missingDeps.Any<string>() || CS$<>8__locals2.removedMods.Any<string>();
					string cancelButton = flag ? Language.GetTextValue("tModLoader.ContinueAnyway") : null;
					string continueButton = flag ? Language.GetTextValue("tModLoader.InstallDependencies") : "";
					Action downloadAction = delegate()
					{
						Interface.<>c__DisplayClass42_1.<<ModLoaderMenus>b__4>d <<ModLoaderMenus>b__4>d;
						<<ModLoaderMenus>b__4>d.<>t__builder = AsyncVoidMethodBuilder.Create();
						<<ModLoaderMenus>b__4>d.<>4__this = CS$<>8__locals2;
						<<ModLoaderMenus>b__4>d.<>1__state = -1;
						<<ModLoaderMenus>b__4>d.<>t__builder.Start<Interface.<>c__DisplayClass42_1.<<ModLoaderMenus>b__4>d>(ref <<ModLoaderMenus>b__4>d);
					};
					if (!string.IsNullOrWhiteSpace(message2))
					{
						Logging.tML.Info("Mod Changes since last launch:\n" + message2);
						Interface.infoMessage.Show(message2, Main.menuMode, null, continueButton, downloadAction, cancelButton);
					}
				}
			}
			if (Main.menuMode == 10000)
			{
				Main.MenuUI.SetState(Interface.modsMenu);
				Main.menuMode = 888;
				return;
			}
			if (Main.menuMode == 10001)
			{
				Main.menuMode = 888;
				Main.MenuUI.SetState(Interface.modSources);
				return;
			}
			if (Main.menuMode == 10025)
			{
				Main.MenuUI.SetState(Interface.createMod);
				Main.menuMode = 888;
				return;
			}
			if (Main.menuMode == 10002)
			{
				Main.menuMode = 888;
				Main.MenuUI.SetState(Interface.loadMods);
				return;
			}
			if (Main.menuMode == 10003)
			{
				Main.MenuUI.SetState(Interface.buildMod);
				Main.menuMode = 888;
				return;
			}
			if (Main.menuMode == 10005)
			{
				Main.MenuUI.SetState(Interface.errorMessage);
				Main.menuMode = 888;
				return;
			}
			if (Main.menuMode == 10006)
			{
				ModLoader.Reload();
				return;
			}
			if (Main.menuMode == 10007)
			{
				Main.MenuUI.SetState(Interface.modBrowser);
				Main.menuMode = 888;
				return;
			}
			if (Main.menuMode == 10008)
			{
				Main.MenuUI.SetState(Interface.modInfo);
				Main.menuMode = 888;
				return;
			}
			if (Main.menuMode == 10012)
			{
				Main.MenuUI.SetState(Interface.updateMessage);
				Main.menuMode = 888;
				return;
			}
			if (Main.menuMode == 10013)
			{
				Main.MenuUI.SetState(Interface.infoMessage);
				Main.menuMode = 888;
				return;
			}
			if (Main.menuMode == 10016)
			{
				Main.MenuUI.SetState(Interface.modPacksMenu);
				Main.menuMode = 888;
				return;
			}
			if (Main.menuMode == 10019)
			{
				Main.MenuUI.SetState(Interface.extractMod);
				Main.menuMode = 888;
				return;
			}
			if (Main.menuMode == 10023)
			{
				Main.MenuUI.SetState(Interface.progress);
				Main.menuMode = 888;
				return;
			}
			if (Main.menuMode == 10020)
			{
				Main.MenuUI.SetState(Interface.downloadProgress);
				Main.menuMode = 888;
				return;
			}
			if (Main.menuMode == 10017)
			{
				offY = 210;
				spacing = 42;
				numButtons = 8;
				buttonVerticalSpacing[numButtons - 1] = 18;
				for (int i = 0; i < numButtons; i++)
				{
					buttonScales[i] = 0.75f;
				}
				int buttonIndex = 0;
				buttonNames[buttonIndex] = (ModNet.downloadModsFromServers ? Language.GetTextValue("tModLoader.DownloadFromServersYes") : Language.GetTextValue("tModLoader.DownloadFromServersNo"));
				if (selectedMenu == buttonIndex)
				{
					SoundEngine.PlaySound(SoundID.MenuTick, null, null);
					ModNet.downloadModsFromServers = !ModNet.downloadModsFromServers;
				}
				buttonIndex++;
				buttonNames[buttonIndex] = (ModLoader.autoReloadRequiredModsLeavingModsScreen ? Language.GetTextValue("tModLoader.AutomaticallyReloadRequiredModsLeavingModsScreenYes") : Language.GetTextValue("tModLoader.AutomaticallyReloadRequiredModsLeavingModsScreenNo"));
				if (selectedMenu == buttonIndex)
				{
					SoundEngine.PlaySound(SoundID.MenuTick, null, null);
					ModLoader.autoReloadRequiredModsLeavingModsScreen = !ModLoader.autoReloadRequiredModsLeavingModsScreen;
				}
				buttonIndex++;
				buttonNames[buttonIndex] = Language.GetTextValue("tModLoader.RemoveForcedMinimumZoom" + (ModLoader.removeForcedMinimumZoom ? "Yes" : "No"));
				if (selectedMenu == buttonIndex)
				{
					SoundEngine.PlaySound(SoundID.MenuTick, null, null);
					ModLoader.removeForcedMinimumZoom = !ModLoader.removeForcedMinimumZoom;
				}
				buttonIndex++;
				int num = buttonIndex;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 1);
				defaultInterpolatedStringHandler.AppendLiteral("tModLoader.AttackSpeedScalingTooltipVisibility");
				defaultInterpolatedStringHandler.AppendFormatted<int>(ModLoader.attackSpeedScalingTooltipVisibility);
				buttonNames[num] = Language.GetTextValue(defaultInterpolatedStringHandler.ToStringAndClear());
				if (selectedMenu == buttonIndex)
				{
					SoundEngine.PlaySound(SoundID.MenuTick, null, null);
					ModLoader.attackSpeedScalingTooltipVisibility = (ModLoader.attackSpeedScalingTooltipVisibility + 1) % 3;
				}
				buttonIndex++;
				buttonNames[buttonIndex] = Language.GetTextValue("tModLoader.ShowModMenuNotifications" + (ModLoader.notifyNewMainMenuThemes ? "Yes" : "No"));
				if (selectedMenu == buttonIndex)
				{
					SoundEngine.PlaySound(SoundID.MenuTick, null, null);
					ModLoader.notifyNewMainMenuThemes = !ModLoader.notifyNewMainMenuThemes;
				}
				buttonIndex++;
				buttonNames[buttonIndex] = Language.GetTextValue("tModLoader.ShowNewUpdatedModsInfo" + (ModLoader.showNewUpdatedModsInfo ? "Yes" : "No"));
				if (selectedMenu == buttonIndex)
				{
					SoundEngine.PlaySound(SoundID.MenuTick, null, null);
					ModLoader.showNewUpdatedModsInfo = !ModLoader.showNewUpdatedModsInfo;
				}
				buttonIndex++;
				buttonNames[buttonIndex] = Language.GetTextValue("tModLoader.ShowConfirmationWindowWhenEnableDisableAllMods" + (ModLoader.showConfirmationWindowWhenEnableDisableAllMods ? "Yes" : "No"));
				if (selectedMenu == buttonIndex)
				{
					SoundEngine.PlaySound(SoundID.MenuTick, null, null);
					ModLoader.showConfirmationWindowWhenEnableDisableAllMods = !ModLoader.showConfirmationWindowWhenEnableDisableAllMods;
				}
				buttonIndex++;
				buttonNames[buttonIndex] = Lang.menu[5].Value;
				if (selectedMenu == buttonIndex | backButtonDown)
				{
					backButtonDown = false;
					Main.menuMode = 11;
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
					return;
				}
			}
			else
			{
				if (Main.menuMode == 10024)
				{
					Main.MenuUI.SetState(Interface.modConfig);
					Main.menuMode = 888;
					return;
				}
				if (Main.menuMode == 10027)
				{
					Main.MenuUI.SetState(Interface.modConfigList);
					Main.menuMode = 888;
					return;
				}
				if (Main.menuMode == 10028)
				{
					Main.MenuUI.SetState(Interface.serverModsDifferMessage);
					Main.menuMode = 888;
					return;
				}
				if (Main.menuMode == 10026)
				{
					Environment.Exit(0);
				}
			}
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x0050F078 File Offset: 0x0050D278
		internal static void ServerModMenu(out bool reloadMods)
		{
			bool exit = false;
			reloadMods = false;
			while (!exit)
			{
				Console.WriteLine("Terraria Server " + Main.versionNumber2 + " - " + ModLoader.versionedName);
				Console.WriteLine();
				LocalMod[] mods = ModOrganizer.FindMods(true);
				for (int i = 0; i < mods.Length; i++)
				{
					Console.Write((i + 1).ToString() + "\t\t" + mods[i].DisplayNameClean);
					Console.WriteLine(" (" + (mods[i].Enabled ? "enabled" : "disabled") + ")");
				}
				if (mods.Length == 0)
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine(Language.GetTextValue("tModLoader.ModsNotFoundServer", ModLoader.ModPath));
					Console.ResetColor();
				}
				Console.WriteLine();
				Console.WriteLine("e\t\t" + Language.GetTextValue("tModLoader.ModsEnableAll"));
				Console.WriteLine("d\t\t" + Language.GetTextValue("tModLoader.ModsDisableAll"));
				Console.WriteLine("c <number>\t" + Language.GetTextValue("tModLoader.DedConfigEditServerConfigsForMod"));
				Console.WriteLine("r\t\t" + Language.GetTextValue("tModLoader.ModsReloadAndReturn"));
				Console.WriteLine(Language.GetTextValue("tModLoader.AskForModIndex"));
				Console.WriteLine();
				Console.WriteLine(Language.GetTextValue("tModLoader.AskForCommand"));
				string command = Console.ReadLine() ?? "";
				command = command.ToLower();
				Console.Clear();
				int value;
				if (command == "e")
				{
					LocalMod[] array = mods;
					for (int j = 0; j < array.Length; j++)
					{
						array[j].Enabled = true;
					}
				}
				else if (command == "d")
				{
					LocalMod[] array = mods;
					for (int j = 0; j < array.Length; j++)
					{
						array[j].Enabled = false;
					}
				}
				else if (command == "r")
				{
					reloadMods = true;
					exit = true;
				}
				else if (command.StartsWith("c"))
				{
					Match match = new Regex("c\\s*(\\d+)").Match(command);
					if (match.Success)
					{
						int modIndex = Convert.ToInt32(match.Groups[1].Value) - 1;
						Mod mod;
						if (modIndex < 0 || modIndex >= mods.Length)
						{
							Interface.WriteColoredLine(ConsoleColor.Yellow, Language.GetTextValue("tModLoader.DedErrorModOOB"));
						}
						else if (!ModLoader.TryGetMod(mods[modIndex].Name, out mod))
						{
							Interface.WriteColoredLine(ConsoleColor.Yellow, Language.GetTextValue("tModLoader.DedErrorNotEnabled"));
						}
						else
						{
							List<ModConfig> configs;
							if (ConfigManager.Configs.TryGetValue(mod, out configs))
							{
								if (configs.Any((ModConfig config) => config.Mode == ConfigScope.ServerSide))
								{
									Interface.ConfigureMod(mod, configs);
									continue;
								}
							}
							Interface.WriteColoredLine(ConsoleColor.Yellow, Language.GetTextValue("tModLoader.DedErrorNoConfig"));
						}
					}
				}
				else if (int.TryParse(command, out value) && value > 0 && value <= mods.Length)
				{
					LocalMod mod2 = mods[value - 1];
					LocalMod localMod = mod2;
					localMod.Enabled = !localMod.Enabled;
					if (mod2.Enabled)
					{
						List<string> missingRefs = new List<string>();
						Interface.EnableDepsRecursive(mod2, mods, missingRefs);
						if (missingRefs.Any<string>())
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine(Language.GetTextValue("tModLoader.ModDependencyModsNotFound", string.Join(", ", missingRefs)) + "\n");
							Console.ResetColor();
						}
					}
				}
			}
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x0050F3D4 File Offset: 0x0050D5D4
		internal static void ConfigureMod(Mod mod, List<ModConfig> configs)
		{
			Dictionary<int, ValueTuple<PropertyFieldWrapper, ModConfig>> properties = new Dictionary<int, ValueTuple<PropertyFieldWrapper, ModConfig>>();
			int index = 1;
			foreach (ModConfig config in (from x in configs
			orderby x.DisplayName.Value
			select x).ToList<ModConfig>())
			{
				if (config.Mode == ConfigScope.ServerSide)
				{
					foreach (PropertyFieldWrapper variable in ConfigManager.GetFieldsAndProperties(config))
					{
						if ((!variable.IsProperty || !(variable.Name == "Mode")) && (!Attribute.IsDefined(variable.MemberInfo, typeof(JsonIgnoreAttribute)) || Attribute.IsDefined(variable.MemberInfo, typeof(ShowDespiteJsonIgnoreAttribute))))
						{
							properties.Add(index++, new ValueTuple<PropertyFieldWrapper, ModConfig>(variable, config));
						}
					}
				}
			}
			for (;;)
			{
				Interface.PrintConfigValues(mod, properties);
				Console.WriteLine();
				Console.WriteLine("m <number> <new config> :\t\t\t\t" + Language.GetTextValue("tModLoader.DedConfigEditConfig"));
				Interface.WriteColoredLine(ConsoleColor.DarkYellow, Language.GetTextValue("tModLoader.DedConfigEditConfigNote"));
				Console.WriteLine("d :\t\t\t\t\t\t\t" + Language.GetTextValue("tModLoader.DedConfigRestoreConfig"));
				Console.WriteLine("e :\t\t\t\t\t\t\t" + Language.GetTextValue("tModLoader.Exit"));
				Console.WriteLine();
				Console.WriteLine(Language.GetTextValue("tModLoader.AskForCommand"));
				string command = Console.ReadLine();
				Console.Clear();
				if (command == null)
				{
					command = "";
				}
				Match match = new Regex("m\\s*(\\d+) (.*)").Match(command);
				if (match.Success)
				{
					Interface.HandleEditConfigValueCommand(properties, match);
				}
				else
				{
					if (command == "d")
					{
						using (List<ModConfig>.Enumerator enumerator = configs.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ModConfig config2 = enumerator.Current;
								if (config2.Mode == ConfigScope.ServerSide)
								{
									ConfigManager.Reset(config2);
									ConfigManager.Save(config2);
									config2.OnChanged();
								}
							}
							continue;
						}
					}
					if (command == "e")
					{
						break;
					}
				}
			}
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x0050F62C File Offset: 0x0050D82C
		private static void PrintConfigValues(Mod mod, Dictionary<int, ValueTuple<PropertyFieldWrapper, ModConfig>> properties)
		{
			Interface.WriteColoredLine(ConsoleColor.White, mod.DisplayName);
			ModConfig currentConfig = null;
			foreach (KeyValuePair<int, ValueTuple<PropertyFieldWrapper, ModConfig>> keyValuePair in properties)
			{
				int num;
				ValueTuple<PropertyFieldWrapper, ModConfig> valueTuple;
				keyValuePair.Deconstruct(out num, out valueTuple);
				ValueTuple<PropertyFieldWrapper, ModConfig> valueTuple2 = valueTuple;
				int key = num;
				PropertyFieldWrapper variable = valueTuple2.Item1;
				ModConfig config = valueTuple2.Item2;
				if (currentConfig != config)
				{
					currentConfig = config;
					ConsoleColor color = ConsoleColor.Green;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
					defaultInterpolatedStringHandler.AppendFormatted<LocalizedText>(config.DisplayName);
					defaultInterpolatedStringHandler.AppendLiteral(":");
					Interface.WriteColoredLine(color, defaultInterpolatedStringHandler.ToStringAndClear());
				}
				HeaderAttribute header = ConfigManager.GetLocalizedHeader(variable.MemberInfo);
				if (header != null)
				{
					Interface.WriteColoredLine(ConsoleColor.Yellow, "    " + Interface.ConvertChatTagsToText(header.Header));
				}
				string text = Interface.ConvertChatTagsToText(ConfigManager.GetLocalizedLabel(variable)) + ":";
				int size = text.Length;
				object obj = variable.CanWrite ? key : "-";
				text = ((obj != null) ? obj.ToString() : null) + "\t" + text + new string('\t', Math.Max((55 - size) / 8, 1));
				if (!variable.CanWrite)
				{
					Console.ForegroundColor = ((Console.BackgroundColor == ConsoleColor.DarkGray || Console.BackgroundColor == ConsoleColor.Gray) ? ConsoleColor.Blue : ConsoleColor.DarkGray);
				}
				text += JsonConvert.SerializeObject(variable.GetValue(config));
				MethodInfo methodInfo = variable.Type.GetMethod("ToString", Array.Empty<Type>());
				bool hasToString = methodInfo != null && methodInfo.DeclaringType != typeof(object);
				if (!variable.Type.IsPrimitive && hasToString && variable.Type != typeof(string))
				{
					string str = text;
					string str2 = "\t\t--> ";
					object value = variable.GetValue(config);
					text = str + str2 + ((value != null) ? value.ToString() : null);
				}
				Console.WriteLine(text);
				if (!variable.CanWrite)
				{
					Console.ResetColor();
				}
				string tooltip = Interface.ConvertChatTagsToText(ConfigManager.GetLocalizedTooltip(variable));
				if (!string.IsNullOrWhiteSpace(tooltip))
				{
					Interface.WriteColoredLine(ConsoleColor.Cyan, "\t" + tooltip.Replace("\n", "\n\t"));
				}
			}
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x0050F88C File Offset: 0x0050DA8C
		private static void HandleEditConfigValueCommand(Dictionary<int, ValueTuple<PropertyFieldWrapper, ModConfig>> properties, Match match)
		{
			int configIndex = Convert.ToInt32(match.Groups[1].Value);
			ValueTuple<PropertyFieldWrapper, ModConfig> value;
			if (!properties.TryGetValue(configIndex, out value))
			{
				Interface.WriteColoredLine(ConsoleColor.Yellow, Language.GetTextValue("tModLoader.DedConfigConfigIndexOOB", configIndex));
				return;
			}
			ValueTuple<PropertyFieldWrapper, ModConfig> valueTuple = value;
			PropertyFieldWrapper variable = valueTuple.Item1;
			ModConfig config = valueTuple.Item2;
			if (!variable.CanWrite)
			{
				Interface.WriteColoredLine(ConsoleColor.Yellow, Language.GetTextValue("tModLoader.DedConfigReadOnly", ConfigManager.GetLocalizedLabel(variable)));
				return;
			}
			try
			{
				string inputString = match.Groups[2].Value;
				Type type = variable.Type;
				if (type == typeof(bool))
				{
					inputString = inputString.ToLower();
				}
				object originalObject = variable.GetValue(config);
				string originalRepresentation = JsonConvert.SerializeObject(originalObject);
				int num;
				if ((originalRepresentation.StartsWith('"') || type == typeof(string)) && !inputString.StartsWith('"'))
				{
					inputString = "\"" + inputString + "\"";
				}
				else if (type.IsArray || (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>) || type.GetGenericTypeDefinition() == typeof(HashSet<>))))
				{
					if (!inputString.StartsWith("["))
					{
						inputString = "[" + inputString + "]";
					}
				}
				else if (type.IsClass && originalRepresentation.StartsWith('{') && !inputString.StartsWith('{'))
				{
					inputString = "{" + inputString + "}";
				}
				else if (type.IsEnum && !int.TryParse(inputString, out num))
				{
					inputString = "\"" + inputString + "\"";
				}
				object newValue = JsonConvert.DeserializeObject(inputString, type);
				OptionStringsAttribute optionStringsAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<OptionStringsAttribute>(variable, null, null);
				RangeAttribute rangeAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<RangeAttribute>(variable, null, null);
				if (optionStringsAttribute != null && !optionStringsAttribute.OptionLabels.Any((string s) => s.Equals(newValue)))
				{
					string text = Language.GetTextValue("tModLoader.DedConfigErrorOutOfOptionStrings", string.Join(", ", optionStringsAttribute.OptionLabels));
					Interface.WriteColoredLine(ConsoleColor.Yellow, text);
				}
				else
				{
					if (rangeAttribute != null)
					{
						IComparable comparable = newValue as IComparable;
						if (comparable != null && (comparable.CompareTo(rangeAttribute.Min) < 0 || comparable.CompareTo(rangeAttribute.Max) > 0))
						{
							Interface.WriteColoredLine(ConsoleColor.Yellow, Language.GetTextValue("tModLoader.DedConfigErrorOutOfRange", rangeAttribute.Min, rangeAttribute.Max));
							goto IL_318;
						}
					}
					if (type.IsArray)
					{
						Array originalArray = originalObject as Array;
						if (originalArray != null)
						{
							Array newArray = newValue as Array;
							if (newArray != null && originalArray.Length != newArray.Length)
							{
								Interface.WriteColoredLine(ConsoleColor.Yellow, Language.GetTextValue("tModLoader.DedConfigArrayLengthCantChange", ConfigManager.GetLocalizedLabel(variable)));
								goto IL_318;
							}
						}
					}
					if (rangeAttribute != null && !(newValue is IComparable))
					{
						Interface.WriteColoredLine(ConsoleColor.Yellow, Language.GetTextValue("tModLoader.DedConfigRangeCantBeValidated", ConfigManager.GetLocalizedLabel(variable)));
					}
					variable.SetValue(config, newValue);
					ConfigManager.Save(config);
					config.OnChanged();
				}
				IL_318:;
			}
			catch
			{
				Interface.WriteColoredLine(ConsoleColor.Yellow, Language.GetTextValue("tModLoader.DedConfigErrorNotParsable"));
			}
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x0050FBE4 File Offset: 0x0050DDE4
		private static void WriteColoredLine(ConsoleColor color, string text)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			Console.ResetColor();
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x0050FBF7 File Offset: 0x0050DDF7
		internal static string ConvertChatTagsToText(string text)
		{
			return string.Join("", from x in ChatManager.ParseMessage(text, Color.White)
			select x.Text);
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0050FC34 File Offset: 0x0050DE34
		private static void EnableDepsRecursive(LocalMod mod, LocalMod[] mods, List<string> missingRefs)
		{
			string[] array = (from x in mod.properties.modReferences
			select x.mod).ToArray<string>();
			for (int i = 0; i < array.Length; i++)
			{
				string name = array[i];
				LocalMod dep = mods.FirstOrDefault((LocalMod x) => x.Name == name);
				if (dep == null)
				{
					missingRefs.Add(name);
				}
				else
				{
					Interface.EnableDepsRecursive(dep, mods, missingRefs);
					if (!dep.Enabled)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Automatically enabling " + dep.DisplayNameClean + " required by " + mod.DisplayNameClean);
						Console.ResetColor();
					}
					LocalMod localMod = dep;
					localMod.Enabled = !localMod.Enabled;
				}
			}
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x0050FD08 File Offset: 0x0050DF08
		internal static void ServerModBrowserMenu()
		{
		}

		// Token: 0x040019BC RID: 6588
		internal const int modsMenuID = 10000;

		// Token: 0x040019BD RID: 6589
		internal const int modSourcesID = 10001;

		// Token: 0x040019BE RID: 6590
		internal const int loadModsID = 10002;

		// Token: 0x040019BF RID: 6591
		internal const int buildModID = 10003;

		// Token: 0x040019C0 RID: 6592
		internal const int errorMessageID = 10005;

		// Token: 0x040019C1 RID: 6593
		internal const int reloadModsID = 10006;

		// Token: 0x040019C2 RID: 6594
		internal const int modBrowserID = 10007;

		// Token: 0x040019C3 RID: 6595
		internal const int modInfoID = 10008;

		// Token: 0x040019C4 RID: 6596
		internal const int updateMessageID = 10012;

		// Token: 0x040019C5 RID: 6597
		internal const int infoMessageID = 10013;

		// Token: 0x040019C6 RID: 6598
		internal const int infoMessageDelayedID = 10014;

		// Token: 0x040019C7 RID: 6599
		internal const int modPacksMenuID = 10016;

		// Token: 0x040019C8 RID: 6600
		internal const int tModLoaderSettingsID = 10017;

		// Token: 0x040019C9 RID: 6601
		internal const int extractModID = 10019;

		// Token: 0x040019CA RID: 6602
		internal const int downloadProgressID = 10020;

		// Token: 0x040019CB RID: 6603
		internal const int progressID = 10023;

		// Token: 0x040019CC RID: 6604
		internal const int modConfigID = 10024;

		// Token: 0x040019CD RID: 6605
		internal const int createModID = 10025;

		// Token: 0x040019CE RID: 6606
		internal const int exitID = 10026;

		// Token: 0x040019CF RID: 6607
		internal const int modConfigListID = 10027;

		// Token: 0x040019D0 RID: 6608
		internal const int serverModsDifferMessageID = 10028;

		// Token: 0x040019D1 RID: 6609
		internal static UIMods modsMenu = new UIMods();

		// Token: 0x040019D2 RID: 6610
		internal static UILoadMods loadMods = new UILoadMods();

		// Token: 0x040019D3 RID: 6611
		internal static UIModSources modSources = new UIModSources();

		// Token: 0x040019D4 RID: 6612
		internal static UIBuildMod buildMod = new UIBuildMod();

		// Token: 0x040019D5 RID: 6613
		internal static UIErrorMessage errorMessage = new UIErrorMessage();

		// Token: 0x040019D6 RID: 6614
		internal static UIModBrowser modBrowser = new UIModBrowser(WorkshopBrowserModule.Instance);

		// Token: 0x040019D7 RID: 6615
		internal static UIModInfo modInfo = new UIModInfo();

		// Token: 0x040019D8 RID: 6616
		internal static UIForcedDelayInfoMessage infoMessageDelayed = new UIForcedDelayInfoMessage();

		// Token: 0x040019D9 RID: 6617
		internal static UIUpdateMessage updateMessage = new UIUpdateMessage();

		// Token: 0x040019DA RID: 6618
		internal static UIInfoMessage infoMessage = new UIInfoMessage();

		// Token: 0x040019DB RID: 6619
		internal static UIModPacks modPacksMenu = new UIModPacks();

		// Token: 0x040019DC RID: 6620
		internal static UIExtractMod extractMod = new UIExtractMod();

		// Token: 0x040019DD RID: 6621
		internal static UIModConfig modConfig = new UIModConfig();

		// Token: 0x040019DE RID: 6622
		internal static UIModConfigList modConfigList = new UIModConfigList();

		// Token: 0x040019DF RID: 6623
		internal static UIServerModsDifferMessage serverModsDifferMessage = new UIServerModsDifferMessage();

		// Token: 0x040019E0 RID: 6624
		internal static UICreateMod createMod = new UICreateMod();

		// Token: 0x040019E1 RID: 6625
		internal static UIProgress progress = new UIProgress();

		// Token: 0x040019E2 RID: 6626
		internal static UIDownloadProgress downloadProgress = new UIDownloadProgress();

		/// <summary> Collection of error messages that will be shown one at a time once the main menu is reached. Useful for error messages during player and world saving happening on another thread. </summary>
		// Token: 0x040019E3 RID: 6627
		internal static Stack<string> pendingErrorMessages = new Stack<string>();
	}
}
