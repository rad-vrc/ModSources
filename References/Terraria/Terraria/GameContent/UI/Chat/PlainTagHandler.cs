using System;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x020003A1 RID: 929
	public class PlainTagHandler : ITagHandler
	{
		// Token: 0x060029BC RID: 10684 RVA: 0x00594CE0 File Offset: 0x00592EE0
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			return new PlainTagHandler.PlainSnippet(text);
		}

		// Token: 0x0200075A RID: 1882
		public class PlainSnippet : TextSnippet
		{
			// Token: 0x060038C2 RID: 14530 RVA: 0x00614415 File Offset: 0x00612615
			public PlainSnippet(string text = "") : base(text)
			{
			}

			// Token: 0x060038C3 RID: 14531 RVA: 0x0061441E File Offset: 0x0061261E
			public PlainSnippet(string text, Color color, float scale = 1f) : base(text, color, scale)
			{
			}

			// Token: 0x060038C4 RID: 14532 RVA: 0x00614429 File Offset: 0x00612629
			public override Color GetVisibleColor()
			{
				return this.Color;
			}
		}
	}
}
