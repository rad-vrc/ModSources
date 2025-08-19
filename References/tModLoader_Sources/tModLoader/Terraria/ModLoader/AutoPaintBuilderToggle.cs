using System;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x02000205 RID: 517
	public class AutoPaintBuilderToggle : VanillaBuilderToggle
	{
		// Token: 0x060027E2 RID: 10210 RVA: 0x00509378 File Offset: 0x00507578
		public override bool Active()
		{
			return Main.player[Main.myPlayer].autoPaint;
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x0050938C File Offset: 0x0050758C
		public override string DisplayValue()
		{
			string text = "";
			int currentState = base.CurrentState;
			if (currentState != 0)
			{
				if (currentState == 1)
				{
					text = Language.GetTextValue("GameUI.PaintSprayerOff");
				}
			}
			else
			{
				text = Language.GetTextValue("GameUI.PaintSprayerOn");
			}
			return text;
		}
	}
}
