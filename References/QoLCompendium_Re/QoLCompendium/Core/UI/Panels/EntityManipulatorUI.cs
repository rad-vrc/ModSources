using System;
using QoLCompendium.Core.UI.Buttons;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace QoLCompendium.Core.UI.Panels
{
	// Token: 0x02000271 RID: 625
	public class EntityManipulatorUI : UIState
	{
		// Token: 0x06000F59 RID: 3929 RVA: 0x00076FD4 File Offset: 0x000751D4
		public override void OnInitialize()
		{
			this.ManipulatorPanel = new UIPanel();
			this.ManipulatorPanel.Top.Set((float)(Main.screenHeight / 2), 0f);
			this.ManipulatorPanel.Left.Set((float)(Main.screenWidth / 2 + 7), 0f);
			this.ManipulatorPanel.Width.Set(400f, 0f);
			this.ManipulatorPanel.Height.Set(400f, 0f);
			this.ManipulatorPanel.BackgroundColor *= 0f;
			this.ManipulatorPanel.BorderColor *= 0f;
			base.Append(this.ManipulatorPanel);
			SpawnModifierButton.backgroundTexture = 0;
			SpawnModifierButton increasedSpawnsButton = new SpawnModifierButton(Common.GetAsset("SpawnModifiers", "Modifier_", SpawnModifierButton.modifierTexture = 0));
			increasedSpawnsButton.Left.Set(0f, 0f);
			increasedSpawnsButton.Top.Set(0f, 0f);
			increasedSpawnsButton.Width.Set(40f, 0f);
			increasedSpawnsButton.Height.Set(40f, 0f);
			increasedSpawnsButton.OnLeftClick += this.IncreasedSpawnsClicked;
			increasedSpawnsButton.Tooltip = UISystem.IncreaseSpawnText;
			this.ManipulatorPanel.Append(increasedSpawnsButton);
			SpawnModifierButton.backgroundTexture = 1;
			SpawnModifierButton decreasedSpawnsButton = new SpawnModifierButton(Common.GetAsset("SpawnModifiers", "Modifier_", SpawnModifierButton.modifierTexture = 1));
			decreasedSpawnsButton.Left.Set(40f, 0f);
			decreasedSpawnsButton.Top.Set(0f, 0f);
			decreasedSpawnsButton.Width.Set(40f, 0f);
			decreasedSpawnsButton.Height.Set(40f, 0f);
			decreasedSpawnsButton.OnLeftClick += this.DecreasedSpawnsClicked;
			decreasedSpawnsButton.Tooltip = UISystem.DecreaseSpawnText;
			this.ManipulatorPanel.Append(decreasedSpawnsButton);
			SpawnModifierButton.backgroundTexture = 1;
			SpawnModifierButton noSpawnsButton = new SpawnModifierButton(Common.GetAsset("SpawnModifiers", "Modifier_", SpawnModifierButton.modifierTexture = 2));
			noSpawnsButton.Left.Set(80f, 0f);
			noSpawnsButton.Top.Set(0f, 0f);
			noSpawnsButton.Width.Set(40f, 0f);
			noSpawnsButton.Height.Set(40f, 0f);
			noSpawnsButton.OnLeftClick += this.NoSpawnsClicked;
			noSpawnsButton.Tooltip = UISystem.CancelSpawnText;
			this.ManipulatorPanel.Append(noSpawnsButton);
			SpawnModifierButton.backgroundTexture = 1;
			SpawnModifierButton noEventsButton = new SpawnModifierButton(Common.GetAsset("SpawnModifiers", "Modifier_", SpawnModifierButton.modifierTexture = 3));
			noEventsButton.Left.Set(120f, 0f);
			noEventsButton.Top.Set(0f, 0f);
			noEventsButton.Width.Set(40f, 0f);
			noEventsButton.Height.Set(40f, 0f);
			noEventsButton.OnLeftClick += this.NoEventsClicked;
			noEventsButton.Tooltip = UISystem.CancelEventText;
			this.ManipulatorPanel.Append(noEventsButton);
			SpawnModifierButton.backgroundTexture = 1;
			SpawnModifierButton noSpawnsOrEventsButton = new SpawnModifierButton(Common.GetAsset("SpawnModifiers", "Modifier_", SpawnModifierButton.modifierTexture = 4));
			noSpawnsOrEventsButton.Left.Set(160f, 0f);
			noSpawnsOrEventsButton.Top.Set(0f, 0f);
			noSpawnsOrEventsButton.Width.Set(40f, 0f);
			noSpawnsOrEventsButton.Height.Set(40f, 0f);
			noSpawnsOrEventsButton.OnLeftClick += this.NoSpawnsOrEventsClicked;
			noSpawnsOrEventsButton.Tooltip = UISystem.CancelSpawnAndEventText;
			this.ManipulatorPanel.Append(noSpawnsOrEventsButton);
			GenericUIButton.backgroundTexture = 1;
			GenericUIButton revertButton = new GenericUIButton(Common.GetAsset("Buttons", "Button_Small_", GenericUIButton.buttonTexture = 2));
			revertButton.Left.Set(200f, 0f);
			revertButton.Top.Set(0f, 0f);
			revertButton.Width.Set(40f, 0f);
			revertButton.Height.Set(40f, 0f);
			revertButton.OnLeftClick += this.RevertButtonClicked;
			revertButton.Tooltip = UISystem.ResetText;
			this.ManipulatorPanel.Append(revertButton);
			GenericUIButton.backgroundTexture = 2;
			GenericUIButton closeButton = new GenericUIButton(Common.GetAsset("Buttons", "Button_Small_", GenericUIButton.buttonTexture = 3));
			closeButton.Left.Set(240f, 0f);
			closeButton.Top.Set(0f, 0f);
			closeButton.Width.Set(40f, 0f);
			closeButton.Height.Set(40f, 0f);
			closeButton.OnLeftClick += this.CloseButtonClicked;
			closeButton.Tooltip = UISystem.CloseText;
			this.ManipulatorPanel.Append(closeButton);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0007750F File Offset: 0x0007570F
		private void RevertButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			EntityManipulatorUI.SpawnChangeClick(0);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00077517 File Offset: 0x00075717
		private void IncreasedSpawnsClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			EntityManipulatorUI.SpawnChangeClick(1);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0007751F File Offset: 0x0007571F
		private void DecreasedSpawnsClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			EntityManipulatorUI.SpawnChangeClick(2);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00077527 File Offset: 0x00075727
		private void NoSpawnsClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			EntityManipulatorUI.SpawnChangeClick(3);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0007752F File Offset: 0x0007572F
		private void NoEventsClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			EntityManipulatorUI.SpawnChangeClick(4);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00077537 File Offset: 0x00075737
		private void NoSpawnsOrEventsClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			EntityManipulatorUI.SpawnChangeClick(5);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00077540 File Offset: 0x00075740
		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EntityManipulatorUI.timeStart >= 10U)
			{
				SoundEngine.PlaySound(SoundID.MenuClose, null, null);
				EntityManipulatorUI.visible = false;
			}
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00077578 File Offset: 0x00075778
		public static void SpawnChangeClick(int modifier)
		{
			if (Main.GameUpdateCount - EntityManipulatorUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().selectedSpawnModifier = modifier;
				SoundEngine.PlaySound(SoundID.MenuTick, null, null);
			}
		}

		// Token: 0x04000637 RID: 1591
		public UIPanel ManipulatorPanel;

		// Token: 0x04000638 RID: 1592
		public static bool visible;

		// Token: 0x04000639 RID: 1593
		public static uint timeStart;
	}
}
