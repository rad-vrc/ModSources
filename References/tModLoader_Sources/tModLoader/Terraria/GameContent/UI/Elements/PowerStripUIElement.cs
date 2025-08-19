using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000500 RID: 1280
	public class PowerStripUIElement : UIElement
	{
		// Token: 0x06003DBE RID: 15806 RVA: 0x005CC194 File Offset: 0x005CA394
		public PowerStripUIElement(string gamepadGroupName, List<UIElement> buttons)
		{
			this._buttonsBySorting = new List<UIElement>(buttons);
			this._gamepadPointGroupname = gamepadGroupName;
			int count = buttons.Count;
			int num = 4;
			int num2 = 40;
			int num3 = 40;
			int num4 = num3 + num;
			UIPanel uIPanel = new UIPanel
			{
				Width = new StyleDimension((float)(num2 + num * 2), 0f),
				Height = new StyleDimension((float)(num3 * count + num * (1 + count)), 0f)
			};
			base.SetPadding(0f);
			this.Width = uIPanel.Width;
			this.Height = uIPanel.Height;
			uIPanel.BorderColor = new Color(89, 116, 213, 255) * 0.9f;
			uIPanel.BackgroundColor = new Color(73, 94, 171) * 0.9f;
			uIPanel.SetPadding(0f);
			base.Append(uIPanel);
			for (int i = 0; i < count; i++)
			{
				UIElement uIElement = buttons[i];
				uIElement.HAlign = 0.5f;
				uIElement.Top = new StyleDimension((float)(num + num4 * i), 0f);
				uIElement.SetSnapPoint(this._gamepadPointGroupname, i, null, null);
				uIPanel.Append(uIElement);
				this._buttonsBySorting.Add(uIElement);
			}
		}

		// Token: 0x04005684 RID: 22148
		private List<UIElement> _buttonsBySorting;

		// Token: 0x04005685 RID: 22149
		private string _gamepadPointGroupname;
	}
}
