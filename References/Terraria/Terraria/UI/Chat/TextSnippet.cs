using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace Terraria.UI.Chat
{
	// Token: 0x0200009F RID: 159
	public class TextSnippet
	{
		// Token: 0x0600132C RID: 4908 RVA: 0x0049E346 File Offset: 0x0049C546
		public TextSnippet(string text = "")
		{
			this.Text = text;
			this.TextOriginal = text;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0049E372 File Offset: 0x0049C572
		public TextSnippet(string text, Color color, float scale = 1f)
		{
			this.Text = text;
			this.TextOriginal = text;
			this.Color = color;
			this.Scale = scale;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void Update()
		{
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void OnHover()
		{
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void OnClick()
		{
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0049E3AC File Offset: 0x0049C5AC
		public virtual Color GetVisibleColor()
		{
			return ChatManager.WaveColor(this.Color);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0049E3B9 File Offset: 0x0049C5B9
		public virtual bool UniqueDraw(bool justCheckingString, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = default(Vector2), Color color = default(Color), float scale = 1f)
		{
			size = Vector2.Zero;
			return false;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0049E3C7 File Offset: 0x0049C5C7
		public virtual TextSnippet CopyMorph(string newText)
		{
			TextSnippet textSnippet = (TextSnippet)base.MemberwiseClone();
			textSnippet.Text = newText;
			return textSnippet;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0049E3DB File Offset: 0x0049C5DB
		public virtual float GetStringLength(DynamicSpriteFont font)
		{
			return font.MeasureString(this.Text).X * this.Scale;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0049E3F5 File Offset: 0x0049C5F5
		public override string ToString()
		{
			return "Text: " + this.Text + " | OriginalText: " + this.TextOriginal;
		}

		// Token: 0x04001077 RID: 4215
		public string Text;

		// Token: 0x04001078 RID: 4216
		public string TextOriginal;

		// Token: 0x04001079 RID: 4217
		public Color Color = Color.White;

		// Token: 0x0400107A RID: 4218
		public float Scale = 1f;

		// Token: 0x0400107B RID: 4219
		public bool CheckForHover;

		// Token: 0x0400107C RID: 4220
		public bool DeleteWhole;
	}
}
