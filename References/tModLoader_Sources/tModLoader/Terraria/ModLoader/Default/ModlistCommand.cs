using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.Localization;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002C6 RID: 710
	internal class ModlistCommand : ModCommand
	{
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06002D9D RID: 11677 RVA: 0x0052F4AF File Offset: 0x0052D6AF
		public override string Command
		{
			get
			{
				return "modlist";
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06002D9E RID: 11678 RVA: 0x0052F4B6 File Offset: 0x0052D6B6
		public override CommandType Type
		{
			get
			{
				return CommandType.Chat | CommandType.Server | CommandType.Console;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x0052F4B9 File Offset: 0x0052D6B9
		public override string Description
		{
			get
			{
				return Language.GetTextValue("tModLoader.CommandModListDescription");
			}
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x0052F4C8 File Offset: 0x0052D6C8
		public override void Action(CommandCaller caller, string input, string[] args)
		{
			IEnumerable<Mod> mods = ModLoader.Mods.Skip(1);
			if (Main.netMode == 1)
			{
				ChatHelper.SendChatMessageFromClient(new ChatMessage(input));
				Mod[] client = (from m in mods
				where m.Side == ModSide.Client || m.Side == ModSide.NoSync
				select m).ToArray<Mod>();
				if (client.Length != 0)
				{
					caller.Reply(Language.GetTextValue("tModLoader.CommandModListClientMods"), Color.Yellow);
					foreach (Mod mod in client)
					{
						caller.Reply(mod.DisplayName, default(Color));
					}
					return;
				}
			}
			else
			{
				if (caller.CommandType == CommandType.Server)
				{
					Mod[] server = (from m in mods
					where m.Side == ModSide.Server || m.Side == ModSide.NoSync
					select m).ToArray<Mod>();
					if (server.Length != 0)
					{
						caller.Reply(Language.GetTextValue("tModLoader.CommandModListServerMods"), Color.Yellow);
						foreach (Mod mod2 in server)
						{
							caller.Reply(mod2.DisplayName, default(Color));
						}
					}
					caller.Reply(Language.GetTextValue("tModLoader.CommandModListSyncedMods"), Color.Yellow);
					using (IEnumerator<Mod> enumerator = (from m in mods
					where m.Side == ModSide.Both
					select m).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Mod mod3 = enumerator.Current;
							caller.Reply(mod3.DisplayName, default(Color));
						}
						return;
					}
				}
				if (caller.CommandType == CommandType.Chat)
				{
					caller.Reply(Language.GetTextValue("tModLoader.CommandModListModlist"), Color.Yellow);
				}
				foreach (Mod mod4 in mods)
				{
					caller.Reply((caller.CommandType == CommandType.Chat) ? mod4.DisplayName : mod4.DisplayNameClean, default(Color));
				}
			}
		}
	}
}
