using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003B9 RID: 953
	public abstract class RangeElement : ConfigElement
	{
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060032B0 RID: 12976 RVA: 0x00545050 File Offset: 0x00543250
		// (set) Token: 0x060032B1 RID: 12977 RVA: 0x00545058 File Offset: 0x00543258
		protected Color SliderColor { get; set; } = Color.White;

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060032B2 RID: 12978 RVA: 0x00545061 File Offset: 0x00543261
		// (set) Token: 0x060032B3 RID: 12979 RVA: 0x00545069 File Offset: 0x00543269
		protected Utils.ColorLerpMethod ColorMethod { get; set; }

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060032B4 RID: 12980 RVA: 0x00545072 File Offset: 0x00543272
		// (set) Token: 0x060032B5 RID: 12981 RVA: 0x0054507A File Offset: 0x0054327A
		internal bool DrawTicks { get; set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060032B6 RID: 12982
		public abstract int NumberTicks { get; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060032B7 RID: 12983
		public abstract float TickIncrement { get; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060032B8 RID: 12984
		// (set) Token: 0x060032B9 RID: 12985
		protected abstract float Proportion { get; set; }

		// Token: 0x060032BA RID: 12986 RVA: 0x00545083 File Offset: 0x00543283
		public RangeElement()
		{
			this.ColorMethod = ((float percent) => Color.Lerp(Color.Black, this.SliderColor, percent));
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x005450A8 File Offset: 0x005432A8
		public override void OnBind()
		{
			base.OnBind();
			this.DrawTicks = Attribute.IsDefined(base.MemberInfo.MemberInfo, typeof(DrawTicksAttribute));
			SliderColorAttribute customAttributeFromMemberThenMemberType = ConfigManager.GetCustomAttributeFromMemberThenMemberType<SliderColorAttribute>(base.MemberInfo, base.Item, base.List);
			this.SliderColor = ((customAttributeFromMemberThenMemberType != null) ? customAttributeFromMemberThenMemberType.Color : Color.White);
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x00545108 File Offset: 0x00543308
		public float DrawValueBar(SpriteBatch sb, float scale, float perc, int lockState = 0, Utils.ColorLerpMethod colorMethod = null)
		{
			perc = Utils.Clamp<float>(perc, -0.05f, 1.05f);
			if (colorMethod == null)
			{
				colorMethod = new Utils.ColorLerpMethod(Utils.ColorLerp_BlackToWhite);
			}
			Texture2D colorBarTexture = TextureAssets.ColorBar.Value;
			Vector2 vector = new Vector2((float)colorBarTexture.Width, (float)colorBarTexture.Height) * scale;
			IngameOptions.valuePosition.X = IngameOptions.valuePosition.X - (float)((int)vector.X);
			Rectangle rectangle;
			rectangle..ctor((int)IngameOptions.valuePosition.X, (int)IngameOptions.valuePosition.Y - (int)vector.Y / 2, (int)vector.X, (int)vector.Y);
			Rectangle destinationRectangle = rectangle;
			int num = 167;
			float num2 = (float)rectangle.X + 5f * scale;
			float num3 = (float)rectangle.Y + 4f * scale;
			if (this.DrawTicks)
			{
				int numTicks = this.NumberTicks;
				if (numTicks > 1)
				{
					for (int tick = 0; tick < numTicks; tick++)
					{
						float percent = (float)tick * this.TickIncrement;
						if (percent <= 1f)
						{
							sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)(num2 + (float)num * percent * scale), rectangle.Y - 2, 2, rectangle.Height + 4), Color.White);
						}
					}
				}
			}
			sb.Draw(colorBarTexture, rectangle, Color.White);
			for (float num4 = 0f; num4 < (float)num; num4 += 1f)
			{
				float percent2 = num4 / (float)num;
				sb.Draw(TextureAssets.ColorBlip.Value, new Vector2(num2 + num4 * scale, num3), null, colorMethod(percent2), 0f, Vector2.Zero, scale, 0, 0f);
			}
			rectangle.Inflate((int)(-5f * scale), 2);
			bool flag = rectangle.Contains(new Point(Main.mouseX, Main.mouseY));
			if (lockState == 2)
			{
				flag = false;
			}
			if (flag || lockState == 1)
			{
				sb.Draw(TextureAssets.ColorHighlight.Value, destinationRectangle, Main.OurFavoriteColor);
			}
			Texture2D colorSlider = TextureAssets.ColorSlider.Value;
			sb.Draw(colorSlider, new Vector2(num2 + 167f * scale * perc, num3 + 4f * scale), null, Color.White, 0f, colorSlider.Size() * 0.5f, scale, 0, 0f);
			if (Main.mouseX >= rectangle.X && Main.mouseX <= rectangle.X + rectangle.Width)
			{
				IngameOptions.inBar = flag;
				return (float)(Main.mouseX - rectangle.X) / (float)rectangle.Width;
			}
			IngameOptions.inBar = false;
			if (rectangle.X >= Main.mouseX)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x005453C0 File Offset: 0x005435C0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			float num = 6f;
			int num2 = 0;
			RangeElement.rightHover = null;
			if (!Main.mouseLeft)
			{
				RangeElement.rightLock = null;
			}
			if (RangeElement.rightLock == this)
			{
				num2 = 1;
			}
			else if (RangeElement.rightLock != null)
			{
				num2 = 2;
			}
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 vector3 = new Vector2(dimensions.X, dimensions.Y);
			bool isMouseHovering = base.IsMouseHovering;
			Vector2 vector2 = vector3;
			vector2.X += 8f;
			vector2.Y += 2f + num;
			vector2.X -= 17f;
			vector2..ctor(dimensions.X + dimensions.Width - 10f, dimensions.Y + 10f + num);
			IngameOptions.valuePosition = vector2;
			float obj = this.DrawValueBar(spriteBatch, 1f, this.Proportion, num2, this.ColorMethod);
			if (IngameOptions.inBar || RangeElement.rightLock == this)
			{
				RangeElement.rightHover = this;
				if (PlayerInput.Triggers.Current.MouseLeft && RangeElement.rightLock == this)
				{
					this.Proportion = obj;
				}
			}
			if (RangeElement.rightHover != null && RangeElement.rightLock == null && PlayerInput.Triggers.JustPressed.MouseLeft)
			{
				RangeElement.rightLock = RangeElement.rightHover;
			}
		}

		// Token: 0x04001DB6 RID: 7606
		private static RangeElement rightLock;

		// Token: 0x04001DB7 RID: 7607
		private static RangeElement rightHover;
	}
}
