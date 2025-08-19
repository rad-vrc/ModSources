using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core
{
	// Token: 0x02000201 RID: 513
	public class DisplayDownedBosses : ModCommand
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00002430 File Offset: 0x00000630
		public override CommandType Type
		{
			get
			{
				return CommandType.Chat;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0004E840 File Offset: 0x0004CA40
		public override string Command
		{
			get
			{
				return "displayDownedBosses";
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0004E847 File Offset: 0x0004CA47
		public override string Description
		{
			get
			{
				return "Displays all downed bosses from mods that Quality of Life Compendium supports";
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0004E850 File Offset: 0x0004CA50
		public override void Action(CommandCaller caller, string input, string[] args)
		{
			for (int i = 0; i < ModConditions.DownedBoss.Length; i++)
			{
				Main.NewText(string.Concat(new string[]
				{
					"Boss: ",
					Enum.GetName(typeof(ModConditions.Downed), i),
					" | Downed: ",
					ModConditions.DownedBoss[i].ToString(),
					" | Saved at: ",
					i.ToString()
				}), byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}
	}
}
