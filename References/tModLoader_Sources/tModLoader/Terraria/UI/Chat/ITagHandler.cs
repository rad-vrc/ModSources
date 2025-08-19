using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI.Chat
{
	// Token: 0x020000C3 RID: 195
	public interface ITagHandler
	{
		// Token: 0x0600168F RID: 5775
		TextSnippet Parse(string text, Color baseColor = default(Color), string options = null);
	}
}
