using System;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003BD RID: 957
	internal class StringInputElement : ConfigElement<string>
	{
		// Token: 0x060032CC RID: 13004 RVA: 0x00545920 File Offset: 0x00543B20
		public override void OnBind()
		{
			base.OnBind();
			UIPanel textBoxBackground = new UIPanel();
			textBoxBackground.SetPadding(0f);
			UIFocusInputTextField uIInputTextField = new UIFocusInputTextField(Language.GetTextValue("tModLoader.ModConfigTypeHere"));
			textBoxBackground.Top.Set(0f, 0f);
			textBoxBackground.Left.Set(-190f, 1f);
			textBoxBackground.Width.Set(180f, 0f);
			textBoxBackground.Height.Set(30f, 0f);
			base.Append(textBoxBackground);
			uIInputTextField.SetText(this.Value);
			uIInputTextField.Top.Set(5f, 0f);
			uIInputTextField.Left.Set(10f, 0f);
			uIInputTextField.Width.Set(-20f, 1f);
			uIInputTextField.Height.Set(20f, 0f);
			uIInputTextField.OnTextChange += delegate(object a, EventArgs b)
			{
				this.Value = uIInputTextField.CurrentString;
			};
			textBoxBackground.Append(uIInputTextField);
		}
	}
}
