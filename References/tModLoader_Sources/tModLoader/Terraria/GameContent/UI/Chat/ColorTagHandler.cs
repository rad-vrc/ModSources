using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x0200053E RID: 1342
	public class ColorTagHandler : ITagHandler
	{
		// Token: 0x06003FE8 RID: 16360 RVA: 0x005DD154 File Offset: 0x005DB354
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			TextSnippet textSnippet = new TextSnippet(text);
			int result;
			if (!int.TryParse(options, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out result))
			{
				return textSnippet;
			}
			textSnippet.Color = new Color(result >> 16 & 255, result >> 8 & 255, result & 255);
			return textSnippet;
		}
	}
}
