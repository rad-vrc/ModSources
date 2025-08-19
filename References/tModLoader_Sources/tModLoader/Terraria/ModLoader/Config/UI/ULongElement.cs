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
	// Token: 0x020003A5 RID: 933
	internal class ULongElement : ConfigElement
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x00542B46 File Offset: 0x00540D46
		// (set) Token: 0x0600323D RID: 12861 RVA: 0x00542B4E File Offset: 0x00540D4E
		public IList<ulong> ULongList { get; set; }

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x00542B57 File Offset: 0x00540D57
		// (set) Token: 0x0600323F RID: 12863 RVA: 0x00542B5F File Offset: 0x00540D5F
		public ulong Min { get; set; }

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x00542B68 File Offset: 0x00540D68
		// (set) Token: 0x06003241 RID: 12865 RVA: 0x00542B70 File Offset: 0x00540D70
		public ulong Max { get; set; } = ulong.MaxValue;

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06003242 RID: 12866 RVA: 0x00542B79 File Offset: 0x00540D79
		// (set) Token: 0x06003243 RID: 12867 RVA: 0x00542B81 File Offset: 0x00540D81
		public ulong Increment { get; set; } = 1UL;

		// Token: 0x06003244 RID: 12868 RVA: 0x00542B8C File Offset: 0x00540D8C
		public override void OnBind()
		{
			base.OnBind();
			this.ULongList = (IList<ulong>)base.List;
			if (this.ULongList != null)
			{
				base.TextDisplayFunction = (() => (this.Index + 1).ToString() + ": " + this.ULongList[this.Index].ToString());
			}
			if (this.RangeAttribute != null && this.RangeAttribute.Min is ulong && this.RangeAttribute.Max is ulong)
			{
				this.Min = (ulong)this.RangeAttribute.Min;
				this.Max = (ulong)this.RangeAttribute.Max;
			}
			if (this.IncrementAttribute != null && this.IncrementAttribute.Increment is ulong)
			{
				this.Increment = (ulong)this.IncrementAttribute.Increment;
			}
			UIPanel textBoxBackground = new UIPanel();
			textBoxBackground.SetPadding(0f);
			UIFocusInputTextField uIInputTextField = new UIFocusInputTextField(Language.GetTextValue("tModLoader.ModConfigTypeHere"));
			textBoxBackground.Top.Set(0f, 0f);
			textBoxBackground.Left.Set(-236f, 1f);
			textBoxBackground.Width.Set(226f, 0f);
			textBoxBackground.Height.Set(30f, 0f);
			base.Append(textBoxBackground);
			uIInputTextField.SetText(this.GetValue().ToString());
			uIInputTextField.Top.Set(5f, 0f);
			uIInputTextField.Left.Set(10f, 0f);
			uIInputTextField.Width.Set(-42f, 1f);
			uIInputTextField.Height.Set(20f, 0f);
			uIInputTextField.OnTextChange += delegate(object a, EventArgs b)
			{
				ulong val;
				if (ulong.TryParse(uIInputTextField.CurrentString, out val))
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
					this.SetValue(Utils.Clamp<ulong>(this.GetValue() + this.Increment, this.Min, this.Max));
				}
				else
				{
					this.SetValue(Utils.Clamp<ulong>(this.GetValue() - this.Increment, this.Min, this.Max));
				}
				uIInputTextField.SetText(this.GetValue().ToString());
			};
			textBoxBackground.Append(upDownButton);
			this.Recalculate();
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x00542EE4 File Offset: 0x005410E4
		protected virtual ulong GetValue()
		{
			return (ulong)this.GetObject();
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x00542EF1 File Offset: 0x005410F1
		protected virtual void SetValue(ulong value)
		{
			this.SetObject(Utils.Clamp<ulong>(value, this.Min, this.Max));
		}
	}
}
