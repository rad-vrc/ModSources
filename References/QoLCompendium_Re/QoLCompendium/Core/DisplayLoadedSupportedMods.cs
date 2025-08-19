using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core
{
	// Token: 0x02000200 RID: 512
	public class DisplayLoadedSupportedMods : ModCommand
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00002430 File Offset: 0x00000630
		public override CommandType Type
		{
			get
			{
				return CommandType.Chat;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0004E7B0 File Offset: 0x0004C9B0
		public override string Command
		{
			get
			{
				return "displayLoadedMods";
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0004E7B7 File Offset: 0x0004C9B7
		public override string Description
		{
			get
			{
				return "Displays loaded mods that Quality of Life Compendium supports";
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0004E7C0 File Offset: 0x0004C9C0
		public override void Action(CommandCaller caller, string input, string[] args)
		{
			foreach (KeyValuePair<string, Mod> entry in LoadModSupport.LoadedMods)
			{
				Main.NewText(entry.Value.DisplayName + " is loaded", byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}
	}
}
