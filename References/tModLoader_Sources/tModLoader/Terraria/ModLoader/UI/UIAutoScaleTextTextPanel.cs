using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI.Chat;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200023C RID: 572
	public class UIAutoScaleTextTextPanel<T> : UIPanel
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x0050FFCC File Offset: 0x0050E1CC
		public string Text
		{
			get
			{
				ref T ptr = ref this._text;
				T t = default(T);
				string text;
				if (t == null)
				{
					t = this._text;
					ptr = ref t;
					if (t == null)
					{
						text = null;
						goto IL_35;
					}
				}
				text = ptr.ToString();
				IL_35:
				return text ?? string.Empty;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x00510017 File Offset: 0x0050E217
		// (set) Token: 0x060028ED RID: 10477 RVA: 0x0051001F File Offset: 0x0050E21F
		public bool IsLarge { get; private set; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x00510028 File Offset: 0x0050E228
		// (set) Token: 0x060028EF RID: 10479 RVA: 0x00510030 File Offset: 0x0050E230
		public bool DrawPanel { get; set; } = true;

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x00510039 File Offset: 0x0050E239
		// (set) Token: 0x060028F1 RID: 10481 RVA: 0x00510041 File Offset: 0x0050E241
		public float TextScaleMax { get; set; } = 1f;

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x0051004A File Offset: 0x0050E24A
		// (set) Token: 0x060028F3 RID: 10483 RVA: 0x00510052 File Offset: 0x0050E252
		public float TextScale { get; set; } = 1f;

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x0051005B File Offset: 0x0050E25B
		// (set) Token: 0x060028F5 RID: 10485 RVA: 0x00510063 File Offset: 0x0050E263
		public Vector2 TextSize { get; private set; } = Vector2.Zero;

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x0051006C File Offset: 0x0050E26C
		// (set) Token: 0x060028F7 RID: 10487 RVA: 0x00510074 File Offset: 0x0050E274
		public Color TextColor { get; set; } = Color.White;

		// Token: 0x060028F8 RID: 10488 RVA: 0x00510080 File Offset: 0x0050E280
		public UIAutoScaleTextTextPanel(T text, float textScaleMax = 1f, bool large = false)
		{
			this.SetText(text, textScaleMax, large);
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x005100E5 File Offset: 0x0050E2E5
		public override void Recalculate()
		{
			base.Recalculate();
			this.SetText(this._text, this.TextScaleMax, this.IsLarge);
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x00510105 File Offset: 0x0050E305
		public void SetText(T text)
		{
			this.SetText(text, this.TextScaleMax, this.IsLarge);
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x0051011C File Offset: 0x0050E31C
		public virtual void SetText(T text, float textScaleMax, bool large)
		{
			if (this.ScalePanel)
			{
				Vector2 textSize = ChatManager.GetStringSize(this.IsLarge ? FontAssets.DeathText.Value : FontAssets.MouseText.Value, this.Text, new Vector2(this.TextScaleMax), -1f);
				this.Width.Set(this.PaddingLeft + textSize.X + this.PaddingRight, 0f);
				this.Height.Set(this.PaddingTop + (this.IsLarge ? 32f : 16f) + this.PaddingBottom, 0f);
				base.Recalculate();
			}
			Rectangle innerDimensionsRectangle = this.UseInnerDimensions ? base.GetInnerDimensions().ToRectangle() : base.GetDimensions().ToRectangle();
			if (text.ToString() != this.oldText || this.oldInnerDimensions != innerDimensionsRectangle)
			{
				this.oldInnerDimensions = innerDimensionsRectangle;
				this.TextScaleMax = textScaleMax;
				DynamicSpriteFont dynamicSpriteFont = large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value;
				Vector2 textSize2 = ChatManager.GetStringSize(dynamicSpriteFont, text.ToString(), new Vector2(this.TextScaleMax), -1f);
				if (this.UseInnerDimensions)
				{
					innerDimensionsRectangle.Inflate(0, 6);
				}
				else
				{
					innerDimensionsRectangle.Inflate(-4, 0);
				}
				Vector2 availableSpace;
				availableSpace..ctor((float)innerDimensionsRectangle.Width, (float)innerDimensionsRectangle.Height);
				if (textSize2.X > availableSpace.X || textSize2.Y > availableSpace.Y)
				{
					float scale = (textSize2.X / availableSpace.X > textSize2.Y / availableSpace.Y) ? (availableSpace.X / textSize2.X) : (availableSpace.Y / textSize2.Y);
					this.TextScale = scale;
					textSize2 = ChatManager.GetStringSize(dynamicSpriteFont, text.ToString(), new Vector2(this.TextScale), -1f);
				}
				else
				{
					this.TextScale = this.TextScaleMax;
				}
				if (!this.UseInnerDimensions)
				{
					innerDimensionsRectangle.Y += 8;
					innerDimensionsRectangle.Height -= 8;
				}
				this._text = text;
				ref T ptr = ref this._text;
				T t = default(T);
				string text2;
				if (t == null)
				{
					t = this._text;
					ptr = ref t;
					if (t == null)
					{
						text2 = null;
						goto IL_26F;
					}
				}
				text2 = ptr.ToString();
				IL_26F:
				this.oldText = text2;
				this.TextSize = textSize2;
				this.IsLarge = large;
				this.textStrings = text.ToString().Split('\n', StringSplitOptions.None);
				this.drawOffsets = new Vector2[this.textStrings.Length];
				for (int i = 0; i < this.textStrings.Length; i++)
				{
					Vector2 size = ChatManager.GetStringSize(dynamicSpriteFont, this.textStrings[i], new Vector2(this.TextScale), -1f);
					if (this.UseInnerDimensions)
					{
						size.Y = (this.IsLarge ? 32f : 16f);
					}
					float x = ((float)innerDimensionsRectangle.Width - size.X) * this.TextOriginX;
					float y = (float)(-(float)this.textStrings.Length) * size.Y * this.TextOriginY + (float)i * size.Y + (float)innerDimensionsRectangle.Height * this.TextOriginY;
					if (this.UseInnerDimensions)
					{
						y -= 2f;
					}
					this.drawOffsets[i] = new Vector2(x, y);
				}
			}
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x005104B4 File Offset: 0x0050E6B4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (float.IsNaN(this.TextScale))
			{
				this.Recalculate();
			}
			if (this.DrawPanel)
			{
				base.DrawSelf(spriteBatch);
			}
			Rectangle innerDimensions = this.UseInnerDimensions ? base.GetInnerDimensions().ToRectangle() : base.GetDimensions().ToRectangle();
			if (this.UseInnerDimensions)
			{
				innerDimensions.Inflate(0, 6);
			}
			else
			{
				innerDimensions.Inflate(-4, -8);
			}
			for (int i = 0; i < this.textStrings.Length; i++)
			{
				Vector2 pos = innerDimensions.TopLeft() + this.drawOffsets[i];
				if (this.IsLarge)
				{
					Utils.DrawBorderStringBig(spriteBatch, this.textStrings[i], pos, this.TextColor, this.TextScale, 0f, 0f, -1);
				}
				else
				{
					Utils.DrawBorderString(spriteBatch, this.textStrings[i], pos, this.TextColor, this.TextScale, 0f, 0f, -1);
				}
			}
		}

		// Token: 0x040019F5 RID: 6645
		public bool ScalePanel;

		// Token: 0x040019F6 RID: 6646
		public bool UseInnerDimensions;

		// Token: 0x040019F7 RID: 6647
		public float TextOriginX = 0.5f;

		// Token: 0x040019F8 RID: 6648
		public float TextOriginY = 0.5f;

		// Token: 0x040019F9 RID: 6649
		private Rectangle oldInnerDimensions;

		// Token: 0x040019FA RID: 6650
		private T _text;

		// Token: 0x040019FB RID: 6651
		private string oldText;

		// Token: 0x040019FC RID: 6652
		private string[] textStrings;

		// Token: 0x040019FD RID: 6653
		private Vector2[] drawOffsets;
	}
}
