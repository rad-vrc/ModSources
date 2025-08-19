using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	// Token: 0x020001AC RID: 428
	internal class ConsoleCommandCaller : CommandCaller
	{
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x004E2F6A File Offset: 0x004E116A
		public CommandType CommandType
		{
			get
			{
				return CommandType.Console;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x004E2F6D File Offset: 0x004E116D
		public Player Player
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x004E2F70 File Offset: 0x004E1170
		public void Reply(string text, Color color = default(Color))
		{
			string[] array = text.Split('\n', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				Console.WriteLine(array[i]);
			}
		}
	}
}
