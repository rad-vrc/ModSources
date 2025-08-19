using System;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x020003A0 RID: 928
	public class NameTagHandler : ITagHandler
	{
		// Token: 0x060029B9 RID: 10681 RVA: 0x00594C75 File Offset: 0x00592E75
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			return new TextSnippet("<" + text.Replace("\\[", "[").Replace("\\]", "]") + ">", baseColor, 1f);
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x00594CB0 File Offset: 0x00592EB0
		public static string GenerateTag(string name)
		{
			return "[n:" + name.Replace("[", "\\[").Replace("]", "\\]") + "]";
		}
	}
}
