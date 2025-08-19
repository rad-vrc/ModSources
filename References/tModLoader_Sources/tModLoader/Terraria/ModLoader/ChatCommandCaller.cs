using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	// Token: 0x020001AA RID: 426
	internal class ChatCommandCaller : CommandCaller
	{
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x004E2E85 File Offset: 0x004E1085
		public CommandType CommandType
		{
			get
			{
				return CommandType.Chat;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x004E2E88 File Offset: 0x004E1088
		public Player Player
		{
			get
			{
				return Main.player[Main.myPlayer];
			}
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x004E2E98 File Offset: 0x004E1098
		public void Reply(string text, Color color = default(Color))
		{
			if (color == default(Color))
			{
				color = Color.White;
			}
			string[] array = text.Split('\n', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				Main.NewText(array[i], color.R, color.G, color.B);
			}
		}
	}
}
