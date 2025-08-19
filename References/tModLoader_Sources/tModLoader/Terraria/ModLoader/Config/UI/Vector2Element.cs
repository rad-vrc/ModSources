using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003C3 RID: 963
	internal class Vector2Element : ConfigElement
	{
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x00548AF6 File Offset: 0x00546CF6
		// (set) Token: 0x06003316 RID: 13078 RVA: 0x00548AFE File Offset: 0x00546CFE
		public IList<Vector2> Vector2List { get; set; }

		// Token: 0x06003317 RID: 13079 RVA: 0x00548B08 File Offset: 0x00546D08
		public override void OnBind()
		{
			base.OnBind();
			this.Vector2List = (IList<Vector2>)base.List;
			if (this.Vector2List != null)
			{
				base.DrawLabel = false;
				this.height = 30;
				this.c = new Vector2Element.Vector2Object(this.Vector2List, base.Index);
			}
			else
			{
				this.height = 30;
				this.c = new Vector2Element.Vector2Object(base.MemberInfo, base.Item);
			}
			if (this.RangeAttribute != null && this.RangeAttribute.Min is float && this.RangeAttribute.Max is float)
			{
				this.max = (float)this.RangeAttribute.Max;
				this.min = (float)this.RangeAttribute.Min;
			}
			if (this.IncrementAttribute != null && this.IncrementAttribute.Increment is float)
			{
				this.increment = (float)this.IncrementAttribute.Increment;
			}
			int order = 0;
			foreach (PropertyFieldWrapper variable in ConfigManager.GetFieldsAndProperties(this.c))
			{
				Tuple<UIElement, UIElement> wrapped = UIModConfig.WrapIt(this, ref this.height, variable, this.c, order++, null, null, -1);
				FloatElement floatElement = wrapped.Item2 as FloatElement;
				if (floatElement != null)
				{
					floatElement.Min = this.min;
					floatElement.Max = this.max;
					floatElement.Increment = this.increment;
					floatElement.DrawTicks = Attribute.IsDefined(base.MemberInfo.MemberInfo, typeof(DrawTicksAttribute));
				}
				if (this.Vector2List != null)
				{
					UIElement item = wrapped.Item1;
					item.Left.Pixels = item.Left.Pixels - 20f;
					UIElement item2 = wrapped.Item1;
					item2.Width.Pixels = item2.Width.Pixels + 20f;
				}
			}
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x00548CF8 File Offset: 0x00546EF8
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			Rectangle rectangle = base.GetInnerDimensions().ToRectangle();
			rectangle..ctor(rectangle.Right - 30, rectangle.Y, 30, 30);
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, rectangle, Color.AliceBlue);
			float x = (this.c.X - this.min) / (this.max - this.min);
			float y = (this.c.Y - this.min) / (this.max - this.min);
			Vector2 position = rectangle.TopLeft();
			position.X += x * (float)rectangle.Width;
			position.Y += y * (float)rectangle.Height;
			Rectangle blipRectangle;
			blipRectangle..ctor((int)position.X - 2, (int)position.Y - 2, 4, 4);
			if (x >= 0f && x <= 1f && y >= 0f && y <= 1f)
			{
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, blipRectangle, Color.Black);
			}
			if (base.IsMouseHovering && rectangle.Contains(Main.MouseScreen.ToPoint()) && Main.mouseLeft)
			{
				float newPerc = (float)(Main.mouseX - rectangle.X) / (float)rectangle.Width;
				newPerc = Utils.Clamp<float>(newPerc, 0f, 1f);
				this.c.X = (float)Math.Round((double)((newPerc * (this.max - this.min) + this.min) * (1f / this.increment))) * this.increment;
				newPerc = (float)(Main.mouseY - rectangle.Y) / (float)rectangle.Height;
				newPerc = Utils.Clamp<float>(newPerc, 0f, 1f);
				this.c.Y = (float)Math.Round((double)((newPerc * (this.max - this.min) + this.min) * (1f / this.increment))) * this.increment;
			}
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x00548F0B File Offset: 0x0054710B
		internal float GetHeight()
		{
			return (float)this.height;
		}

		// Token: 0x04001DEC RID: 7660
		private int height;

		// Token: 0x04001DED RID: 7661
		private Vector2Element.Vector2Object c;

		// Token: 0x04001DEE RID: 7662
		private float min;

		// Token: 0x04001DEF RID: 7663
		private float max = 1f;

		// Token: 0x04001DF0 RID: 7664
		private float increment = 0.01f;

		// Token: 0x02000B0E RID: 2830
		private class Vector2Object
		{
			// Token: 0x1700093C RID: 2364
			// (get) Token: 0x06005B42 RID: 23362 RVA: 0x006A5625 File Offset: 0x006A3825
			// (set) Token: 0x06005B43 RID: 23363 RVA: 0x006A5632 File Offset: 0x006A3832
			[LabelKey("$Config.Vector2.X.Label")]
			public float X
			{
				get
				{
					return this.current.X;
				}
				set
				{
					this.current.X = value;
					this.Update();
				}
			}

			// Token: 0x1700093D RID: 2365
			// (get) Token: 0x06005B44 RID: 23364 RVA: 0x006A5646 File Offset: 0x006A3846
			// (set) Token: 0x06005B45 RID: 23365 RVA: 0x006A5653 File Offset: 0x006A3853
			[LabelKey("$Config.Vector2.Y.Label")]
			public float Y
			{
				get
				{
					return this.current.Y;
				}
				set
				{
					this.current.Y = value;
					this.Update();
				}
			}

			// Token: 0x06005B46 RID: 23366 RVA: 0x006A5668 File Offset: 0x006A3868
			private void Update()
			{
				if (this.array == null)
				{
					this.memberInfo.SetValue(this.item, this.current);
				}
				else
				{
					this.array[this.index] = this.current;
				}
				Interface.modConfig.SetPendingChanges(true);
			}

			// Token: 0x06005B47 RID: 23367 RVA: 0x006A56BD File Offset: 0x006A38BD
			public Vector2Object(PropertyFieldWrapper memberInfo, object item)
			{
				this.item = item;
				this.memberInfo = memberInfo;
				this.current = (Vector2)memberInfo.GetValue(item);
			}

			// Token: 0x06005B48 RID: 23368 RVA: 0x006A56E5 File Offset: 0x006A38E5
			public Vector2Object(IList<Vector2> array, int index)
			{
				this.current = array[index];
				this.array = array;
				this.index = index;
			}

			// Token: 0x04006EDB RID: 28379
			private readonly PropertyFieldWrapper memberInfo;

			// Token: 0x04006EDC RID: 28380
			private readonly object item;

			// Token: 0x04006EDD RID: 28381
			private readonly IList<Vector2> array;

			// Token: 0x04006EDE RID: 28382
			private readonly int index;

			// Token: 0x04006EDF RID: 28383
			private Vector2 current;
		}
	}
}
