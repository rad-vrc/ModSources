using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x02000398 RID: 920
	internal class ColorElement : ConfigElement
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x0054067E File Offset: 0x0053E87E
		// (set) Token: 0x06003193 RID: 12691 RVA: 0x00540686 File Offset: 0x0053E886
		public IList<Color> ColorList { get; set; }

		// Token: 0x06003194 RID: 12692 RVA: 0x00540690 File Offset: 0x0053E890
		public override void OnBind()
		{
			base.OnBind();
			this.ColorList = (IList<Color>)base.List;
			if (this.ColorList != null)
			{
				base.DrawLabel = false;
				this.height = 30;
				this.c = new ColorElement.ColorObject(this.ColorList, base.Index);
			}
			else
			{
				this.height = 30;
				this.c = new ColorElement.ColorObject(base.MemberInfo, base.Item);
			}
			ColorHSLSliderAttribute customAttributeFromMemberThenMemberType = ConfigManager.GetCustomAttributeFromMemberThenMemberType<ColorHSLSliderAttribute>(base.MemberInfo, base.Item, base.List);
			bool useHue = customAttributeFromMemberThenMemberType != null;
			bool showSaturationAndLightness = customAttributeFromMemberThenMemberType != null && customAttributeFromMemberThenMemberType.ShowSaturationAndLightness;
			bool flag = ConfigManager.GetCustomAttributeFromMemberThenMemberType<ColorNoAlphaAttribute>(base.MemberInfo, base.Item, base.List) != null;
			List<string> skip = new List<string>();
			if (flag)
			{
				skip.Add("A");
			}
			if (useHue)
			{
				skip.AddRange(new string[]
				{
					"R",
					"G",
					"B"
				});
			}
			else
			{
				skip.AddRange(new string[]
				{
					"Hue",
					"Saturation",
					"Lightness"
				});
			}
			if (useHue && !showSaturationAndLightness)
			{
				skip.AddRange(new string[]
				{
					"Saturation",
					"Lightness"
				});
			}
			int order = 0;
			foreach (PropertyFieldWrapper variable in ConfigManager.GetFieldsAndProperties(this.c))
			{
				if (!skip.Contains(variable.Name))
				{
					Tuple<UIElement, UIElement> wrapped = UIModConfig.WrapIt(this, ref this.height, variable, this.c, order++, null, null, -1);
					if (this.ColorList != null)
					{
						UIElement item = wrapped.Item1;
						item.Left.Pixels = item.Left.Pixels - 20f;
						UIElement item2 = wrapped.Item1;
						item2.Width.Pixels = item2.Width.Pixels + 20f;
					}
				}
			}
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x00540878 File Offset: 0x0053EA78
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			Rectangle hitbox = base.GetInnerDimensions().ToRectangle();
			hitbox..ctor(hitbox.X + hitbox.Width / 2, hitbox.Y, hitbox.Width / 2, 30);
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, hitbox, this.c.current);
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x005408E1 File Offset: 0x0053EAE1
		internal float GetHeight()
		{
			return (float)this.height;
		}

		// Token: 0x04001D52 RID: 7506
		private int height;

		// Token: 0x04001D53 RID: 7507
		private ColorElement.ColorObject c;

		// Token: 0x02000AF5 RID: 2805
		private class ColorObject
		{
			// Token: 0x17000935 RID: 2357
			// (get) Token: 0x06005AEA RID: 23274 RVA: 0x006A48E7 File Offset: 0x006A2AE7
			// (set) Token: 0x06005AEB RID: 23275 RVA: 0x006A48F4 File Offset: 0x006A2AF4
			[LabelKey("$Config.Color.Red.Label")]
			public byte R
			{
				get
				{
					return this.current.R;
				}
				set
				{
					this.current.R = value;
					this.Update();
				}
			}

			// Token: 0x17000936 RID: 2358
			// (get) Token: 0x06005AEC RID: 23276 RVA: 0x006A4908 File Offset: 0x006A2B08
			// (set) Token: 0x06005AED RID: 23277 RVA: 0x006A4915 File Offset: 0x006A2B15
			[LabelKey("$Config.Color.Green.Label")]
			public byte G
			{
				get
				{
					return this.current.G;
				}
				set
				{
					this.current.G = value;
					this.Update();
				}
			}

			// Token: 0x17000937 RID: 2359
			// (get) Token: 0x06005AEE RID: 23278 RVA: 0x006A4929 File Offset: 0x006A2B29
			// (set) Token: 0x06005AEF RID: 23279 RVA: 0x006A4936 File Offset: 0x006A2B36
			[LabelKey("$Config.Color.Blue.Label")]
			public byte B
			{
				get
				{
					return this.current.B;
				}
				set
				{
					this.current.B = value;
					this.Update();
				}
			}

			// Token: 0x17000938 RID: 2360
			// (get) Token: 0x06005AF0 RID: 23280 RVA: 0x006A494A File Offset: 0x006A2B4A
			// (set) Token: 0x06005AF1 RID: 23281 RVA: 0x006A495C File Offset: 0x006A2B5C
			[LabelKey("$Config.Color.Hue.Label")]
			public float Hue
			{
				get
				{
					return Main.rgbToHsl(this.current).X;
				}
				set
				{
					byte a = this.A;
					this.current = Main.hslToRgb(value, this.Saturation, this.Lightness, byte.MaxValue);
					this.current.A = a;
					this.Update();
				}
			}

			// Token: 0x17000939 RID: 2361
			// (get) Token: 0x06005AF2 RID: 23282 RVA: 0x006A499F File Offset: 0x006A2B9F
			// (set) Token: 0x06005AF3 RID: 23283 RVA: 0x006A49B4 File Offset: 0x006A2BB4
			[LabelKey("$Config.Color.Saturation.Label")]
			public float Saturation
			{
				get
				{
					return Main.rgbToHsl(this.current).Y;
				}
				set
				{
					byte a = this.A;
					this.current = Main.hslToRgb(this.Hue, value, this.Lightness, byte.MaxValue);
					this.current.A = a;
					this.Update();
				}
			}

			// Token: 0x1700093A RID: 2362
			// (get) Token: 0x06005AF4 RID: 23284 RVA: 0x006A49F7 File Offset: 0x006A2BF7
			// (set) Token: 0x06005AF5 RID: 23285 RVA: 0x006A4A0C File Offset: 0x006A2C0C
			[LabelKey("$Config.Color.Lightness.Label")]
			public float Lightness
			{
				get
				{
					return Main.rgbToHsl(this.current).Z;
				}
				set
				{
					byte a = this.A;
					this.current = Main.hslToRgb(this.Hue, this.Saturation, value, byte.MaxValue);
					this.current.A = a;
					this.Update();
				}
			}

			// Token: 0x1700093B RID: 2363
			// (get) Token: 0x06005AF6 RID: 23286 RVA: 0x006A4A4F File Offset: 0x006A2C4F
			// (set) Token: 0x06005AF7 RID: 23287 RVA: 0x006A4A5C File Offset: 0x006A2C5C
			[LabelKey("$Config.Color.Alpha.Label")]
			public byte A
			{
				get
				{
					return this.current.A;
				}
				set
				{
					this.current.A = value;
					this.Update();
				}
			}

			// Token: 0x06005AF8 RID: 23288 RVA: 0x006A4A70 File Offset: 0x006A2C70
			private void Update()
			{
				if (this.array == null)
				{
					this.memberInfo.SetValue(this.item, this.current);
					return;
				}
				this.array[this.index] = this.current;
			}

			// Token: 0x06005AF9 RID: 23289 RVA: 0x006A4AAE File Offset: 0x006A2CAE
			public ColorObject(PropertyFieldWrapper memberInfo, object item)
			{
				this.item = item;
				this.memberInfo = memberInfo;
				this.current = (Color)memberInfo.GetValue(item);
			}

			// Token: 0x06005AFA RID: 23290 RVA: 0x006A4AD6 File Offset: 0x006A2CD6
			public ColorObject(IList<Color> array, int index)
			{
				this.current = array[index];
				this.array = array;
				this.index = index;
			}

			// Token: 0x04006E9A RID: 28314
			private readonly PropertyFieldWrapper memberInfo;

			// Token: 0x04006E9B RID: 28315
			private readonly object item;

			// Token: 0x04006E9C RID: 28316
			private readonly IList<Color> array;

			// Token: 0x04006E9D RID: 28317
			private readonly int index;

			// Token: 0x04006E9E RID: 28318
			internal Color current;
		}
	}
}
