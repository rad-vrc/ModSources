using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI.Chat
{
	// Token: 0x0200009E RID: 158
	public interface ITagHandler
	{
		// Token: 0x0600132B RID: 4907
		TextSnippet Parse(string text, Color baseColor = default(Color), string options = null);
	}
}
