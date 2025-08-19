using System;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x02000203 RID: 515
	public class RulerGridBuilderToggle : VanillaBuilderToggle
	{
		// Token: 0x060027DC RID: 10204 RVA: 0x005092C8 File Offset: 0x005074C8
		public override bool Active()
		{
			return Main.player[Main.myPlayer].rulerGrid;
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x005092DC File Offset: 0x005074DC
		public override string DisplayValue()
		{
			string text = "";
			int currentState = base.CurrentState;
			if (currentState != 0)
			{
				if (currentState == 1)
				{
					text = Language.GetTextValue("GameUI.MechanicalRulerOff");
				}
			}
			else
			{
				text = Language.GetTextValue("GameUI.MechanicalRulerOn");
			}
			return text;
		}
	}
}
