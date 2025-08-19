﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000357 RID: 855
	public class PowerStripUIElement : UIElement
	{
		// Token: 0x0600276F RID: 10095 RVA: 0x00583624 File Offset: 0x00581824
		public PowerStripUIElement(string gamepadGroupName, List<UIElement> buttons)
		{
			this._buttonsBySorting = new List<UIElement>(buttons);
			this._gamepadPointGroupname = gamepadGroupName;
			int count = buttons.Count;
			int num = 4;
			int num2 = 40;
			int num3 = 40;
			int num4 = num3 + num;
			UIPanel uipanel = new UIPanel
			{
				Width = new StyleDimension((float)(num2 + num * 2), 0f),
				Height = new StyleDimension((float)(num3 * count + num * (1 + count)), 0f)
			};
			base.SetPadding(0f);
			this.Width = uipanel.Width;
			this.Height = uipanel.Height;
			uipanel.BorderColor = new Color(89, 116, 213, 255) * 0.9f;
			uipanel.BackgroundColor = new Color(73, 94, 171) * 0.9f;
			uipanel.SetPadding(0f);
			base.Append(uipanel);
			for (int i = 0; i < count; i++)
			{
				UIElement uielement = buttons[i];
				uielement.HAlign = 0.5f;
				uielement.Top = new StyleDimension((float)(num + num4 * i), 0f);
				uielement.SetSnapPoint(this._gamepadPointGroupname, i, null, null);
				uipanel.Append(uielement);
				this._buttonsBySorting.Add(uielement);
			}
		}

		// Token: 0x04004ADE RID: 19166
		private List<UIElement> _buttonsBySorting;

		// Token: 0x04004ADF RID: 19167
		private string _gamepadPointGroupname;
	}
}
