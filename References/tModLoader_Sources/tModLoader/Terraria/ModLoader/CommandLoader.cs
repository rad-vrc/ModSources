using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as the central class from which ModCommand functions are supported and carried out.
	/// </summary>
	// Token: 0x02000151 RID: 337
	public static class CommandLoader
	{
		// Token: 0x06001B81 RID: 7041 RVA: 0x004D0614 File Offset: 0x004CE814
		public static bool Matches(CommandType commandType, CommandType callerType)
		{
			if ((commandType & CommandType.World) != (CommandType)0)
			{
				if (Main.netMode == 2)
				{
					commandType |= CommandType.Server;
				}
				else if (Main.netMode == 0)
				{
					commandType |= CommandType.Chat;
				}
			}
			return (callerType & commandType) > (CommandType)0;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x004D063C File Offset: 0x004CE83C
		internal static void Add(ModCommand cmd)
		{
			List<ModCommand> cmdList;
			if (!CommandLoader.Commands.TryGetValue(cmd.Command, out cmdList))
			{
				CommandLoader.Commands.Add(cmd.Command, cmdList = new List<ModCommand>());
			}
			cmdList.Add(cmd);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x004D067B File Offset: 0x004CE87B
		internal static void Unload()
		{
			CommandLoader.Commands.Clear();
		}

		/// <summary>
		/// Finds a command by name. Handles mod prefixing. Replies with error messages.
		/// </summary>
		/// <param name="caller">Handles relaying the results of the command and narrows down the search by CommandType context</param>
		/// <param name="name">The name of the command to retrieve</param>
		/// <param name="mc">The found command, or null if an error was encountered.</param>
		/// <returns>True if a ModCommand was found, or an error message was replied. False if the command is unrecognized.</returns>
		// Token: 0x06001B84 RID: 7044 RVA: 0x004D0688 File Offset: 0x004CE888
		internal static bool GetCommand(CommandCaller caller, string name, out ModCommand mc)
		{
			string modName = null;
			if (name.Contains(':'))
			{
				string[] array = name.Split(':', StringSplitOptions.None);
				modName = array[0];
				name = array[1];
			}
			mc = null;
			List<ModCommand> cmdList;
			if (!CommandLoader.Commands.TryGetValue(name, out cmdList))
			{
				return false;
			}
			cmdList = (from c in cmdList
			where CommandLoader.Matches(c.Type, caller.CommandType)
			select c).ToList<ModCommand>();
			if (cmdList.Count == 0)
			{
				return false;
			}
			if (modName != null)
			{
				Mod mod;
				if (!ModLoader.TryGetMod(modName, out mod))
				{
					caller.Reply("Unknown Mod: " + modName, Color.Red);
				}
				else
				{
					mc = cmdList.SingleOrDefault((ModCommand c) => c.Mod == mod);
					if (mc == null)
					{
						caller.Reply(string.Concat(new string[]
						{
							"Mod: ",
							modName,
							" does not have a ",
							name,
							" command."
						}), Color.Red);
					}
				}
			}
			else
			{
				if (cmdList.Count > 1)
				{
					caller.Reply("Multiple definitions of command /" + name + ". Try:", Color.Red);
					using (List<ModCommand>.Enumerator enumerator = cmdList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ModCommand c2 = enumerator.Current;
							caller.Reply(c2.Mod.Name + ":" + c2.Command, Color.LawnGreen);
						}
						return true;
					}
				}
				mc = cmdList[0];
			}
			return true;
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x004D0828 File Offset: 0x004CEA28
		internal static bool HandleCommand(string input, CommandCaller caller)
		{
			int sep = input.IndexOf(" ");
			string name = ((sep >= 0) ? input.Substring(0, sep) : input).ToLower();
			if (caller.CommandType != CommandType.Console)
			{
				if (name == "" || name[0] != '/')
				{
					return false;
				}
				name = name.Substring(1);
			}
			ModCommand mc;
			if (!CommandLoader.GetCommand(caller, name, out mc))
			{
				return false;
			}
			if (mc == null)
			{
				return true;
			}
			if (!mc.IsCaseSensitive)
			{
				input = input.ToLower();
			}
			string[] args = input.TrimEnd().Split(' ', StringSplitOptions.None);
			args = args.Skip(1).ToArray<string>();
			try
			{
				mc.Action(caller, input, args);
			}
			catch (Exception ex)
			{
				UsageException ue = ex as UsageException;
				if (ue != null && ue.msg != null)
				{
					caller.Reply(ue.msg, ue.color);
				}
				else
				{
					caller.Reply("Usage: " + mc.Usage, Color.Red);
				}
			}
			return true;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x004D0924 File Offset: 0x004CEB24
		public static List<Tuple<string, string>> GetHelp(CommandType type)
		{
			List<Tuple<string, string>> list = new List<Tuple<string, string>>();
			Func<ModCommand, bool> <>9__0;
			foreach (KeyValuePair<string, List<ModCommand>> entry in CommandLoader.Commands)
			{
				IEnumerable<ModCommand> value = entry.Value;
				Func<ModCommand, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((ModCommand mc) => CommandLoader.Matches(mc.Type, type)));
				}
				List<ModCommand> cmdList = value.Where(predicate).ToList<ModCommand>();
				foreach (ModCommand mc2 in cmdList)
				{
					string cmd = mc2.Command;
					if (cmdList.Count > 1)
					{
						cmd = mc2.Mod.Name + ":" + cmd;
					}
					list.Add(Tuple.Create<string, string>(cmd, mc2.Description));
				}
			}
			return list;
		}

		// Token: 0x040014BE RID: 5310
		internal static readonly IDictionary<string, List<ModCommand>> Commands = new Dictionary<string, List<ModCommand>>(StringComparer.OrdinalIgnoreCase);
	}
}
