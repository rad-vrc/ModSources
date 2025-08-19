using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace Terraria.UI.Chat
{
	// Token: 0x020000C4 RID: 196
	public class TextSnippet
	{
		// Token: 0x06001690 RID: 5776 RVA: 0x004B502D File Offset: 0x004B322D
		public TextSnippet(string text = "")
		{
			this.Text = text;
			this.TextOriginal = text;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x004B5059 File Offset: 0x004B3259
		public TextSnippet(string text, Color color, float scale = 1f)
		{
			this.Text = text;
			this.TextOriginal = text;
			this.Color = color;
			this.Scale = scale;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x004B5093 File Offset: 0x004B3293
		public virtual void Update()
		{
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x004B5095 File Offset: 0x004B3295
		public virtual void OnHover()
		{
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x004B5097 File Offset: 0x004B3297
		public virtual void OnClick()
		{
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x004B5099 File Offset: 0x004B3299
		public virtual Color GetVisibleColor()
		{
			return ChatManager.WaveColor(this.Color);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x004B50A6 File Offset: 0x004B32A6
		public virtual bool UniqueDraw(bool justCheckingString, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = default(Vector2), Color color = default(Color), float scale = 1f)
		{
			size = Vector2.Zero;
			return false;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x004B50B4 File Offset: 0x004B32B4
		public virtual TextSnippet CopyMorph(string newText)
		{
			TextSnippet textSnippet = (TextSnippet)base.MemberwiseClone();
			textSnippet.Text = newText;
			return textSnippet;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x004B50C8 File Offset: 0x004B32C8
		public virtual float GetStringLength(DynamicSpriteFont font)
		{
			return font.MeasureString(this.Text).X * this.Scale;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x004B50E2 File Offset: 0x004B32E2
		public override string ToString()
		{
			return "Text: " + this.Text + " | OriginalText: " + this.TextOriginal;
		}

		// Token: 0x040012A9 RID: 4777
		public string Text;

		// Token: 0x040012AA RID: 4778
		public string TextOriginal;

		// Token: 0x040012AB RID: 4779
		public Color Color = Color.White;

		// Token: 0x040012AC RID: 4780
		public float Scale = 1f;

		// Token: 0x040012AD RID: 4781
		public bool CheckForHover;

		// Token: 0x040012AE RID: 4782
		public bool DeleteWhole;
	}
}
