using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003A4 RID: 932
	internal class LongElement : ConfigElement
	{
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x0054274D File Offset: 0x0054094D
		// (set) Token: 0x06003231 RID: 12849 RVA: 0x00542755 File Offset: 0x00540955
		public IList<long> LongList { get; set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x0054275E File Offset: 0x0054095E
		// (set) Token: 0x06003233 RID: 12851 RVA: 0x00542766 File Offset: 0x00540966
		public long Min { get; set; } = long.MinValue;

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x0054276F File Offset: 0x0054096F
		// (set) Token: 0x06003235 RID: 12853 RVA: 0x00542777 File Offset: 0x00540977
		public long Max { get; set; } = long.MaxValue;

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x00542780 File Offset: 0x00540980
		// (set) Token: 0x06003237 RID: 12855 RVA: 0x00542788 File Offset: 0x00540988
		public long Increment { get; set; } = 1L;

		// Token: 0x06003238 RID: 12856 RVA: 0x00542794 File Offset: 0x00540994
		public override void OnBind()
		{
			base.OnBind();
			this.LongList = (IList<long>)base.List;
			if (this.LongList != null)
			{
				base.TextDisplayFunction = (() => (this.Index + 1).ToString() + ": " + this.LongList[this.Index].ToString());
			}
			if (this.RangeAttribute != null && this.RangeAttribute.Min is long && this.RangeAttribute.Max is long)
			{
				this.Min = (long)this.RangeAttribute.Min;
				this.Max = (long)this.RangeAttribute.Max;
			}
			if (this.IncrementAttribute != null && this.IncrementAttribute.Increment is long)
			{
				this.Increment = (long)this.IncrementAttribute.Increment;
			}
			UIPanel textBoxBackground = new UIPanel();
			textBoxBackground.SetPadding(0f);
			UIFocusInputTextField uIInputTextField = new UIFocusInputTextField(Language.GetTextValue("tModLoader.ModConfigTypeHere"));
			textBoxBackground.Top.Set(0f, 0f);
			textBoxBackground.Left.Set(-190f, 1f);
			textBoxBackground.Width.Set(180f, 0f);
			textBoxBackground.Height.Set(30f, 0f);
			base.Append(textBoxBackground);
			uIInputTextField.SetText(this.GetValue().ToString());
			uIInputTextField.Top.Set(5f, 0f);
			uIInputTextField.Left.Set(10f, 0f);
			uIInputTextField.Width.Set(-42f, 1f);
			uIInputTextField.Height.Set(20f, 0f);
			uIInputTextField.OnTextChange += delegate(object a, EventArgs b)
			{
				long val;
				if (long.TryParse(uIInputTextField.CurrentString, out val))
				{
					this.SetValue(val);
				}
				float t2 = MathHelper.Clamp((FontAssets.MouseText.Value.MeasureString(uIInputTextField.CurrentString).X - 100f) / 150f, 0f, 1f);
				UIPanel textBoxBackground3 = textBoxBackground;
				if (textBoxBackground3 != null)
				{
					textBoxBackground3.Left.Set(MathHelper.Lerp(-190f, -300f, t2), 1f);
				}
				UIPanel textBoxBackground4 = textBoxBackground;
				if (textBoxBackground4 == null)
				{
					return;
				}
				textBoxBackground4.Width.Set(MathHelper.Lerp(180f, 290f, t2), 0f);
			};
			uIInputTextField.OnUnfocus += delegate(object a, EventArgs b)
			{
				uIInputTextField.SetText(this.GetValue().ToString());
			};
			float t = MathHelper.Clamp((FontAssets.MouseText.Value.MeasureString(uIInputTextField.CurrentString).X - 100f) / 150f, 0f, 1f);
			UIPanel textBoxBackground5 = textBoxBackground;
			if (textBoxBackground5 != null)
			{
				textBoxBackground5.Left.Set(MathHelper.Lerp(-190f, -300f, t), 1f);
			}
			UIPanel textBoxBackground2 = textBoxBackground;
			if (textBoxBackground2 != null)
			{
				textBoxBackground2.Width.Set(MathHelper.Lerp(180f, 290f, t), 0f);
			}
			textBoxBackground.Append(uIInputTextField);
			UIModConfigHoverImageSplit upDownButton = new UIModConfigHoverImageSplit(base.UpDownTexture, "+" + this.Increment.ToString(), "-" + this.Increment.ToString());
			upDownButton.Recalculate();
			upDownButton.Top.Set(4f, 0f);
			upDownButton.Left.Set(-30f, 1f);
			upDownButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				Rectangle r = b.GetDimensions().ToRectangle();
				if (a.MousePosition.Y < (float)(r.Y + r.Height / 2))
				{
					this.SetValue(Utils.Clamp<long>(this.GetValue() + this.Increment, this.Min, this.Max));
				}
				else
				{
					this.SetValue(Utils.Clamp<long>(this.GetValue() - this.Increment, this.Min, this.Max));
				}
				uIInputTextField.SetText(this.GetValue().ToString());
			};
			textBoxBackground.Append(upDownButton);
			this.Recalculate();
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x00542AEC File Offset: 0x00540CEC
		protected virtual long GetValue()
		{
			return (long)this.GetObject();
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x00542AF9 File Offset: 0x00540CF9
		protected virtual void SetValue(long value)
		{
			this.SetObject(Utils.Clamp<long>(value, this.Min, this.Max));
		}
	}
}
