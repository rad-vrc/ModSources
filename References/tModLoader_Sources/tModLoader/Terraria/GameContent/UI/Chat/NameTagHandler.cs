using System;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000543 RID: 1347
	public class NameTagHandler : ITagHandler
	{
		// Token: 0x06004007 RID: 16391 RVA: 0x005DDCB1 File Offset: 0x005DBEB1
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			return new TextSnippet("<" + text.Replace("\\[", "[").Replace("\\]", "]") + ">", baseColor, 1f);
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x005DDCEC File Offset: 0x005DBEEC
		public static string GenerateTag(string name)
		{
			return "[n:" + name.Replace("[", "\\[").Replace("]", "\\]") + "]";
		}
	}
}
