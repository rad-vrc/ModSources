using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003A3 RID: 931
	internal class IntInputElement : ConfigElement
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x0054242B File Offset: 0x0054062B
		// (set) Token: 0x06003225 RID: 12837 RVA: 0x00542433 File Offset: 0x00540633
		public IList<int> IntList { get; set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x0054243C File Offset: 0x0054063C
		// (set) Token: 0x06003227 RID: 12839 RVA: 0x00542444 File Offset: 0x00540644
		public int Min { get; set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x0054244D File Offset: 0x0054064D
		// (set) Token: 0x06003229 RID: 12841 RVA: 0x00542455 File Offset: 0x00540655
		public int Max { get; set; } = 100;

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x0054245E File Offset: 0x0054065E
		// (set) Token: 0x0600322B RID: 12843 RVA: 0x00542466 File Offset: 0x00540666
		public int Increment { get; set; } = 1;

		// Token: 0x0600322C RID: 12844 RVA: 0x00542470 File Offset: 0x00540670
		public override void OnBind()
		{
			base.OnBind();
			this.IntList = (IList<int>)base.List;
			if (this.IntList != null)
			{
				base.TextDisplayFunction = (() => (this.Index + 1).ToString() + ": " + this.IntList[this.Index].ToString());
			}
			if (this.RangeAttribute != null && this.RangeAttribute.Min is int && this.RangeAttribute.Max is int)
			{
				this.Min = (int)this.RangeAttribute.Min;
				this.Max = (int)this.RangeAttribute.Max;
			}
			if (this.IncrementAttribute != null && this.IncrementAttribute.Increment is int)
			{
				this.Increment = (int)this.IncrementAttribute.Increment;
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
				int val;
				if (int.TryParse(uIInputTextField.CurrentString, out val))
				{
					this.SetValue(val);
				}
			};
			uIInputTextField.OnUnfocus += delegate(object a, EventArgs b)
			{
				uIInputTextField.SetText(this.GetValue().ToString());
			};
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
					this.SetValue(Utils.Clamp<int>(this.GetValue() + this.Increment, this.Min, this.Max));
				}
				else
				{
					this.SetValue(Utils.Clamp<int>(this.GetValue() - this.Increment, this.Min, this.Max));
				}
				uIInputTextField.SetText(this.GetValue().ToString());
			};
			textBoxBackground.Append(upDownButton);
			this.Recalculate();
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x0054270A File Offset: 0x0054090A
		protected virtual int GetValue()
		{
			return (int)this.GetObject();
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x00542717 File Offset: 0x00540917
		protected virtual void SetValue(int value)
		{
			this.SetObject(Utils.Clamp<int>(value, this.Min, this.Max));
		}
	}
}
