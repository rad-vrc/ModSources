using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Chat;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002C3 RID: 707
	internal class HelpCommand : ModCommand
	{
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x0052E30C File Offset: 0x0052C50C
		public override string Command
		{
			get
			{
				return "help";
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x0052E313 File Offset: 0x0052C513
		public override string Usage
		{
			get
			{
				return "/help [name]";
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x0052E31A File Offset: 0x0052C51A
		public override CommandType Type
		{
			get
			{
				return CommandType.Chat | CommandType.Server;
			}
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x0052E320 File Offset: 0x0052C520
		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				List<Tuple<string, string>> help = CommandLoader.GetHelp(caller.CommandType);
				caller.Reply(caller.CommandType.ToString() + " Commands:", Color.Yellow);
				foreach (Tuple<string, string> entry in help)
				{
					caller.Reply(entry.Item1 + "   " + entry.Item2, default(Color));
				}
				if (Main.netMode == 1)
				{
					ChatHelper.SendChatMessageFromClient(new ChatMessage(input));
				}
				return;
			}
			ModCommand mc;
			if (!CommandLoader.GetCommand(caller, args[0], out mc))
			{
				throw new UsageException("Unknown command: " + args[0], Color.Red);
			}
			if (mc != null)
			{
				caller.Reply(mc.Usage, default(Color));
				if (!string.IsNullOrEmpty(mc.Description))
				{
					caller.Reply(mc.Description, default(Color));
				}
			}
		}
	}
}
