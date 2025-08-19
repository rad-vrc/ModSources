using System;
using QoLCompendium.Core.UI.Buttons;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace QoLCompendium.Core.UI.Panels
{
	// Token: 0x0200027A RID: 634
	public class PhaseInterrupterUI : UIState
	{
		// Token: 0x0600105D RID: 4189 RVA: 0x00081810 File Offset: 0x0007FA10
		public override void OnInitialize()
		{
			this.MoonPanel = new UIPanel();
			this.MoonPanel.Top.Set((float)(Main.screenHeight / 2), 0f);
			this.MoonPanel.Left.Set((float)(Main.screenWidth / 2 - 32), 0f);
			this.MoonPanel.Width.Set(400f, 0f);
			this.MoonPanel.Height.Set(400f, 0f);
			this.MoonPanel.BackgroundColor *= 0f;
			this.MoonPanel.BorderColor *= 0f;
			base.Append(this.MoonPanel);
			MoonPhaseButton.backgroundTexture = 0;
			MoonPhaseButton fullMoon = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 0));
			fullMoon.Left.Set(0f, 0f);
			fullMoon.Top.Set(0f, 0f);
			fullMoon.Width.Set(40f, 0f);
			fullMoon.Height.Set(40f, 0f);
			fullMoon.OnLeftClick += this.FullMoonClicked;
			fullMoon.Tooltip = UISystem.FullMoonText;
			this.MoonPanel.Append(fullMoon);
			MoonPhaseButton.backgroundTexture = 1;
			MoonPhaseButton waningGibbous = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 1));
			waningGibbous.Left.Set(40f, 0f);
			waningGibbous.Top.Set(0f, 0f);
			waningGibbous.Width.Set(40f, 0f);
			waningGibbous.Height.Set(40f, 0f);
			waningGibbous.OnLeftClick += this.WaningGibbousClicked;
			waningGibbous.Tooltip = UISystem.WaningGibbousText;
			this.MoonPanel.Append(waningGibbous);
			MoonPhaseButton.backgroundTexture = 1;
			MoonPhaseButton thirdQuarter = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 2));
			thirdQuarter.Left.Set(80f, 0f);
			thirdQuarter.Top.Set(0f, 0f);
			thirdQuarter.Width.Set(40f, 0f);
			thirdQuarter.Height.Set(40f, 0f);
			thirdQuarter.OnLeftClick += this.ThirdQuarterClicked;
			thirdQuarter.Tooltip = UISystem.ThirdQuarterText;
			this.MoonPanel.Append(thirdQuarter);
			MoonPhaseButton.backgroundTexture = 1;
			MoonPhaseButton waningCrescent = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 3));
			waningCrescent.Left.Set(120f, 0f);
			waningCrescent.Top.Set(0f, 0f);
			waningCrescent.Width.Set(40f, 0f);
			waningCrescent.Height.Set(40f, 0f);
			waningCrescent.OnLeftClick += this.WaningCrescentClicked;
			waningCrescent.Tooltip = UISystem.WaningCrescentText;
			this.MoonPanel.Append(waningCrescent);
			MoonPhaseButton.backgroundTexture = 1;
			MoonPhaseButton newMoon = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 4));
			newMoon.Left.Set(160f, 0f);
			newMoon.Top.Set(0f, 0f);
			newMoon.Width.Set(40f, 0f);
			newMoon.Height.Set(40f, 0f);
			newMoon.OnLeftClick += this.NewMoonClicked;
			newMoon.Tooltip = UISystem.NewMoonText;
			this.MoonPanel.Append(newMoon);
			MoonPhaseButton.backgroundTexture = 1;
			MoonPhaseButton waxingCrescent = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 5));
			waxingCrescent.Left.Set(200f, 0f);
			waxingCrescent.Top.Set(0f, 0f);
			waxingCrescent.Width.Set(40f, 0f);
			waxingCrescent.Height.Set(40f, 0f);
			waxingCrescent.OnLeftClick += this.WaxingCrescentClicked;
			waxingCrescent.Tooltip = UISystem.WaxingCrescentText;
			this.MoonPanel.Append(waxingCrescent);
			MoonPhaseButton.backgroundTexture = 1;
			MoonPhaseButton firstQuarter = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 6));
			firstQuarter.Left.Set(240f, 0f);
			firstQuarter.Top.Set(0f, 0f);
			firstQuarter.Width.Set(40f, 0f);
			firstQuarter.Height.Set(40f, 0f);
			firstQuarter.OnLeftClick += this.FirstQuarterClicked;
			firstQuarter.Tooltip = UISystem.FirstQuarterText;
			this.MoonPanel.Append(firstQuarter);
			MoonPhaseButton.backgroundTexture = 1;
			MoonPhaseButton waxingGibbous = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 7));
			waxingGibbous.Left.Set(280f, 0f);
			waxingGibbous.Top.Set(0f, 0f);
			waxingGibbous.Width.Set(40f, 0f);
			waxingGibbous.Height.Set(40f, 0f);
			waxingGibbous.OnLeftClick += this.WaxingGibbousClicked;
			waxingGibbous.Tooltip = UISystem.WaxingGibbousText;
			this.MoonPanel.Append(waxingGibbous);
			GenericUIButton.backgroundTexture = 2;
			GenericUIButton closeButton = new GenericUIButton(Common.GetAsset("Buttons", "Button_Small_", GenericUIButton.buttonTexture = 3));
			closeButton.Left.Set(320f, 0f);
			closeButton.Top.Set(0f, 0f);
			closeButton.Width.Set(40f, 0f);
			closeButton.Height.Set(40f, 0f);
			closeButton.OnLeftClick += this.CloseButtonClicked;
			closeButton.Tooltip = UISystem.CloseText;
			this.MoonPanel.Append(closeButton);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00081E9A File Offset: 0x0008009A
		private void FullMoonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PhaseInterrupterUI.PhaseClick(0);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00081EA2 File Offset: 0x000800A2
		private void WaningGibbousClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PhaseInterrupterUI.PhaseClick(1);
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00081EAA File Offset: 0x000800AA
		private void ThirdQuarterClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PhaseInterrupterUI.PhaseClick(2);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00081EB2 File Offset: 0x000800B2
		private void WaningCrescentClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PhaseInterrupterUI.PhaseClick(3);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00081EBA File Offset: 0x000800BA
		private void NewMoonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PhaseInterrupterUI.PhaseClick(4);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00081EC2 File Offset: 0x000800C2
		private void WaxingCrescentClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PhaseInterrupterUI.PhaseClick(5);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00081ECA File Offset: 0x000800CA
		private void FirstQuarterClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PhaseInterrupterUI.PhaseClick(6);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00081ED2 File Offset: 0x000800D2
		private void WaxingGibbousClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PhaseInterrupterUI.PhaseClick(7);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00081EDC File Offset: 0x000800DC
		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - PhaseInterrupterUI.timeStart >= 10U)
			{
				SoundEngine.PlaySound(SoundID.MenuClose, null, null);
				PhaseInterrupterUI.visible = false;
			}
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00081F14 File Offset: 0x00080114
		public static void PhaseClick(int phase)
		{
			if (Main.GameUpdateCount - PhaseInterrupterUI.timeStart >= 10U)
			{
				Main.moonPhase = phase;
				SoundEngine.PlaySound(SoundID.MenuTick, null, null);
			}
		}

		// Token: 0x04000711 RID: 1809
		public UIPanel MoonPanel;

		// Token: 0x04000712 RID: 1810
		public static bool visible;

		// Token: 0x04000713 RID: 1811
		public static uint timeStart;
	}
}
