using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;

namespace QoLCompendium.Core
{
	// Token: 0x02000204 RID: 516
	public static class TextHelper
	{
		// Token: 0x06000BC7 RID: 3015 RVA: 0x0004EB27 File Offset: 0x0004CD27
		public static void PrintText(string text)
		{
			TextHelper.PrintText(text, Color.White, false);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0004EB38 File Offset: 0x0004CD38
		public static void PrintText(string text, Color color, bool myPlayerOnly = false)
		{
			if (Main.netMode == 0)
			{
				Main.NewText(text, new Color?(color));
				return;
			}
			if (Main.netMode == 2)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), color, -1);
				return;
			}
			if (Main.netMode == 2 && myPlayerOnly)
			{
				ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(text), color, Main.myPlayer);
			}
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0004EB8C File Offset: 0x0004CD8C
		public static void PrintText(string text, int r, int g, int b)
		{
			TextHelper.PrintText(text, new Color(r, g, b), false);
		}
	}
}
