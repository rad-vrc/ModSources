using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Core.UI.Buttons;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace QoLCompendium.Core.UI.Panels
{
	// Token: 0x02000273 RID: 627
	public class PermanentBuffSelectorUI : UIState
	{
		// Token: 0x06000F74 RID: 3956 RVA: 0x00078600 File Offset: 0x00076800
		public override void OnInitialize()
		{
			this.SelectorPanel = new UIPanel();
			this.SelectorPanel.SetPadding(0f);
			this.SelectorPanel.Top.Set((float)(Main.screenHeight / 2), 0f);
			this.SelectorPanel.Left.Set((float)(Main.screenWidth / 2) + 10f, 0f);
			this.SelectorPanel.Width.Set(304f, 0f);
			this.SelectorPanel.Height.Set(64f, 0f);
			this.SelectorPanel.BackgroundColor = new Color(73, 94, 171);
			PermanentBuffSelectorUI.CreateBuffButton(this.VanillaButton, 16f, 16f);
			this.VanillaButton.OnLeftClick += this.VanillaClicked;
			this.VanillaButton.Tooltip = UISystem.VanillaText;
			this.SelectorPanel.Append(this.VanillaButton);
			PermanentBuffSelectorUI.CreateBuffButton(this.CalamityButton, 64f, 16f);
			this.CalamityButton.OnLeftClick += this.CalamityClicked;
			this.CalamityButton.Tooltip = UISystem.CalamityText;
			this.SelectorPanel.Append(this.CalamityButton);
			PermanentBuffSelectorUI.CreateBuffButton(this.MartinsOrderButton, 112f, 16f);
			this.MartinsOrderButton.OnLeftClick += this.MartinsOrderClicked;
			this.MartinsOrderButton.Tooltip = UISystem.MartinsOrderText;
			this.SelectorPanel.Append(this.MartinsOrderButton);
			PermanentBuffSelectorUI.CreateBuffButton(this.SOTSButton, 160f, 16f);
			this.SOTSButton.OnLeftClick += this.SOTSClicked;
			this.SOTSButton.Tooltip = UISystem.SOTSText;
			this.SelectorPanel.Append(this.SOTSButton);
			PermanentBuffSelectorUI.CreateBuffButton(this.SpiritButton, 208f, 16f);
			this.SpiritButton.OnLeftClick += this.SpiritClicked;
			this.SpiritButton.Tooltip = UISystem.SpiritClassicText;
			this.SelectorPanel.Append(this.SpiritButton);
			PermanentBuffSelectorUI.CreateBuffButton(this.ThoriumButton, 256f, 16f);
			this.ThoriumButton.OnLeftClick += this.ThoriumClicked;
			this.ThoriumButton.Tooltip = UISystem.ThoriumText;
			this.SelectorPanel.Append(this.ThoriumButton);
			base.Append(this.SelectorPanel);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0007888F File Offset: 0x00076A8F
		private void VanillaClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffSelectorUI.BuffClick(0);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00078897 File Offset: 0x00076A97
		private void CalamityClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffSelectorUI.BuffClick(1);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0007889F File Offset: 0x00076A9F
		private void MartinsOrderClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffSelectorUI.BuffClick(2);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x000788A7 File Offset: 0x00076AA7
		private void SOTSClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffSelectorUI.BuffClick(3);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x000788AF File Offset: 0x00076AAF
		private void SpiritClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffSelectorUI.BuffClick(4);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000788B7 File Offset: 0x00076AB7
		private void ThoriumClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffSelectorUI.BuffClick(5);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x000788C0 File Offset: 0x00076AC0
		public static void BuffClick(int ui)
		{
			if (Main.GameUpdateCount - PermanentBuffSelectorUI.timeStart >= 10U)
			{
				SoundEngine.PlaySound(SoundID.MenuOpen, new Vector2?(Main.LocalPlayer.position), null);
				if (ui == 0)
				{
					PermanentBuffUI.timeStart = Main.GameUpdateCount;
					PermanentBuffUI.visible = !PermanentBuffUI.visible;
					return;
				}
				if (ui == 1 && ModConditions.calamityLoaded)
				{
					PermanentCalamityBuffUI.timeStart = Main.GameUpdateCount;
					PermanentCalamityBuffUI.visible = !PermanentCalamityBuffUI.visible;
					return;
				}
				if (ui == 2 && ModConditions.martainsOrderLoaded)
				{
					PermanentMartinsOrderBuffUI.timeStart = Main.GameUpdateCount;
					PermanentMartinsOrderBuffUI.visible = !PermanentMartinsOrderBuffUI.visible;
					return;
				}
				if (ui == 3 && ModConditions.secretsOfTheShadowsLoaded)
				{
					PermanentSOTSBuffUI.timeStart = Main.GameUpdateCount;
					PermanentSOTSBuffUI.visible = !PermanentSOTSBuffUI.visible;
					return;
				}
				if (ui == 4 && ModConditions.spiritLoaded)
				{
					PermanentSpiritClassicBuffUI.timeStart = Main.GameUpdateCount;
					PermanentSpiritClassicBuffUI.visible = !PermanentSpiritClassicBuffUI.visible;
					return;
				}
				if (ui == 5 && ModConditions.thoriumLoaded)
				{
					PermanentThoriumBuffUI.timeStart = Main.GameUpdateCount;
					PermanentThoriumBuffUI.visible = !PermanentThoriumBuffUI.visible;
				}
			}
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x000789C4 File Offset: 0x00076BC4
		private static void CreateBuffButton(PermanentUpgradedBuffButton button, float left, float top)
		{
			button.Left.Set(left, 0f);
			button.Top.Set(top, 0f);
			button.Width.Set(32f, 0f);
			button.Height.Set(32f, 0f);
		}

		// Token: 0x0400063F RID: 1599
		public UIPanel SelectorPanel;

		// Token: 0x04000640 RID: 1600
		public static bool visible;

		// Token: 0x04000641 RID: 1601
		public static uint timeStart;

		// Token: 0x04000642 RID: 1602
		private PermanentUpgradedBuffButton VanillaButton = new PermanentUpgradedBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentEverything", 2));

		// Token: 0x04000643 RID: 1603
		private PermanentUpgradedBuffButton CalamityButton = new PermanentUpgradedBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentCalamity", 2));

		// Token: 0x04000644 RID: 1604
		private PermanentUpgradedBuffButton MartinsOrderButton = new PermanentUpgradedBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentMartinsOrder", 2));

		// Token: 0x04000645 RID: 1605
		private PermanentUpgradedBuffButton SOTSButton = new PermanentUpgradedBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentSecretsOfTheShadows", 2));

		// Token: 0x04000646 RID: 1606
		private PermanentUpgradedBuffButton SpiritButton = new PermanentUpgradedBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentSpiritClassic", 2));

		// Token: 0x04000647 RID: 1607
		private PermanentUpgradedBuffButton ThoriumButton = new PermanentUpgradedBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentThorium", 2));
	}
}
