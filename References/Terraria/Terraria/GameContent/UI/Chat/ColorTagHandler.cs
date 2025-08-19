using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x0200039E RID: 926
	public class ColorTagHandler : ITagHandler
	{
		// Token: 0x060029B4 RID: 10676 RVA: 0x00594A64 File Offset: 0x00592C64
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			TextSnippet textSnippet = new TextSnippet(text);
			int num;
			if (!int.TryParse(options, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out num))
			{
				return textSnippet;
			}
			textSnippet.Color = new Color(num >> 16 & 255, num >> 8 & 255, num & 255);
			return textSnippet;
		}
	}
}
