using System;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x02000202 RID: 514
	public class RulerLineBuilderToggle : VanillaBuilderToggle
	{
		// Token: 0x060027D9 RID: 10201 RVA: 0x00509272 File Offset: 0x00507472
		public override bool Active()
		{
			return Main.player[Main.myPlayer].rulerLine;
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x00509284 File Offset: 0x00507484
		public override string DisplayValue()
		{
			string text = "";
			int currentState = base.CurrentState;
			if (currentState != 0)
			{
				if (currentState == 1)
				{
					text = Language.GetTextValue("GameUI.RulerOff");
				}
			}
			else
			{
				text = Language.GetTextValue("GameUI.RulerOn");
			}
			return text;
		}
	}
}
