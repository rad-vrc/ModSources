using System;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000544 RID: 1348
	public class PlainTagHandler : ITagHandler
	{
		// Token: 0x0600400A RID: 16394 RVA: 0x005DDD24 File Offset: 0x005DBF24
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			return new PlainTagHandler.PlainSnippet(text);
		}

		// Token: 0x02000C29 RID: 3113
		public class PlainSnippet : TextSnippet
		{
			// Token: 0x06005F33 RID: 24371 RVA: 0x006CD671 File Offset: 0x006CB871
			public PlainSnippet(string text = "") : base(text)
			{
			}

			// Token: 0x06005F34 RID: 24372 RVA: 0x006CD67A File Offset: 0x006CB87A
			public PlainSnippet(string text, Color color, float scale = 1f) : base(text, color, scale)
			{
			}

			// Token: 0x06005F35 RID: 24373 RVA: 0x006CD685 File Offset: 0x006CB885
			public override Color GetVisibleColor()
			{
				return this.Color;
			}
		}
	}
}
