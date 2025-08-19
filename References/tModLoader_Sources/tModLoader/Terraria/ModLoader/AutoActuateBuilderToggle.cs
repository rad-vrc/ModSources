using System;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x02000204 RID: 516
	public class AutoActuateBuilderToggle : VanillaBuilderToggle
	{
		// Token: 0x060027DF RID: 10207 RVA: 0x00509320 File Offset: 0x00507520
		public override bool Active()
		{
			return Main.player[Main.myPlayer].autoActuator;
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x00509334 File Offset: 0x00507534
		public override string DisplayValue()
		{
			string text = "";
			int currentState = base.CurrentState;
			if (currentState != 0)
			{
				if (currentState == 1)
				{
					text = Language.GetTextValue("GameUI.ActuationDeviceOff");
				}
			}
			else
			{
				text = Language.GetTextValue("GameUI.ActuationDeviceOn");
			}
			return text;
		}
	}
}
