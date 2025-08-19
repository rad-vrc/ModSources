using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.UI.Buttons;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace QoLCompendium.Core.UI.Panels
{
	// Token: 0x02000277 RID: 631
	public class PermanentSOTSBuffUI : UIState
	{
		// Token: 0x06001008 RID: 4104 RVA: 0x0007E2D0 File Offset: 0x0007C4D0
		public override void OnInitialize()
		{
			this.BuffPanel = new UIPanel();
			this.BuffPanel.SetPadding(0f);
			this.BuffPanel.Left.Set(575f, 0f);
			this.BuffPanel.Top.Set(275f, 0f);
			this.BuffPanel.Width.Set(352f, 0f);
			this.BuffPanel.Height.Set(144f, 0f);
			this.BuffPanel.BackgroundColor = new Color(73, 94, 171);
			this.BuffPanel.OnLeftMouseDown += this.DragStart;
			this.BuffPanel.OnLeftMouseUp += this.DragEnd;
			UIText potionText = new UIText(UISystem.PotionText, 1f, false);
			potionText.Left.Set(16f, 0f);
			potionText.Top.Set(8f, 0f);
			potionText.Width.Set(64f, 0f);
			potionText.Height.Set(32f, 0f);
			this.BuffPanel.Append(potionText);
			UIText stationsText = new UIText(UISystem.StationText, 1f, false);
			stationsText.Left.Set(16f, 0f);
			stationsText.Top.Set(72f, 0f);
			stationsText.Width.Set(64f, 0f);
			stationsText.Height.Set(32f, 0f);
			this.BuffPanel.Append(stationsText);
			PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.AssassinationButton, 16f, 32f);
			PermanentSOTSBuffUI.AssassinationButton.OnLeftClick += this.AssassinationClicked;
			this.BuffPanel.Append(PermanentSOTSBuffUI.AssassinationButton);
			PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.BluefireButton, 48f, 32f);
			PermanentSOTSBuffUI.BluefireButton.OnLeftClick += this.BluefireClicked;
			this.BuffPanel.Append(PermanentSOTSBuffUI.BluefireButton);
			PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.BrittleButton, 80f, 32f);
			PermanentSOTSBuffUI.BrittleButton.OnLeftClick += this.BrittleClicked;
			this.BuffPanel.Append(PermanentSOTSBuffUI.BrittleButton);
			PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.DoubleVisionButton, 112f, 32f);
			PermanentSOTSBuffUI.DoubleVisionButton.OnLeftClick += this.DoubleVisionClicked;
			this.BuffPanel.Append(PermanentSOTSBuffUI.DoubleVisionButton);
			PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.HarmonyButton, 144f, 32f);
			PermanentSOTSBuffUI.HarmonyButton.OnLeftClick += this.HarmonyClicked;
			this.BuffPanel.Append(PermanentSOTSBuffUI.HarmonyButton);
			PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.NightmareButton, 176f, 32f);
			PermanentSOTSBuffUI.NightmareButton.OnLeftClick += this.NightmareClicked;
			this.BuffPanel.Append(PermanentSOTSBuffUI.NightmareButton);
			PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.RippleButton, 208f, 32f);
			PermanentSOTSBuffUI.RippleButton.OnLeftClick += this.RippleClicked;
			this.BuffPanel.Append(PermanentSOTSBuffUI.RippleButton);
			PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.RoughskinButton, 240f, 32f);
			PermanentSOTSBuffUI.RoughskinButton.OnLeftClick += this.RoughskinClicked;
			this.BuffPanel.Append(PermanentSOTSBuffUI.RoughskinButton);
			PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.SoulAccessButton, 272f, 32f);
			PermanentSOTSBuffUI.SoulAccessButton.OnLeftClick += this.SoulAccessClicked;
			this.BuffPanel.Append(PermanentSOTSBuffUI.SoulAccessButton);
			PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.VibeButton, 304f, 32f);
			PermanentSOTSBuffUI.VibeButton.OnLeftClick += this.VibeClicked;
			this.BuffPanel.Append(PermanentSOTSBuffUI.VibeButton);
			PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.DigitalDisplayButton, 16f, 96f);
			PermanentSOTSBuffUI.DigitalDisplayButton.OnLeftClick += this.DigitalDisplayClicked;
			this.BuffPanel.Append(PermanentSOTSBuffUI.DigitalDisplayButton);
			HashSet<PermanentBuffButton> hashSet = new HashSet<PermanentBuffButton>();
			hashSet.Add(PermanentSOTSBuffUI.AssassinationButton);
			hashSet.Add(PermanentSOTSBuffUI.BluefireButton);
			hashSet.Add(PermanentSOTSBuffUI.BrittleButton);
			hashSet.Add(PermanentSOTSBuffUI.DoubleVisionButton);
			hashSet.Add(PermanentSOTSBuffUI.HarmonyButton);
			hashSet.Add(PermanentSOTSBuffUI.NightmareButton);
			hashSet.Add(PermanentSOTSBuffUI.RippleButton);
			hashSet.Add(PermanentSOTSBuffUI.RoughskinButton);
			hashSet.Add(PermanentSOTSBuffUI.SoulAccessButton);
			hashSet.Add(PermanentSOTSBuffUI.VibeButton);
			hashSet.Add(PermanentSOTSBuffUI.DigitalDisplayButton);
			HashSet<PermanentBuffButton> buttons = hashSet;
			this.allBuffButtons.UnionWith(buttons);
			base.Append(this.BuffPanel);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0007E7A9 File Offset: 0x0007C9A9
		private void AssassinationClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSOTSBuffUI.BuffClick(0, PermanentSOTSBuffUI.AssassinationButton);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0007E7B6 File Offset: 0x0007C9B6
		private void BluefireClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSOTSBuffUI.BuffClick(1, PermanentSOTSBuffUI.BluefireButton);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0007E7C3 File Offset: 0x0007C9C3
		private void BrittleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSOTSBuffUI.BuffClick(2, PermanentSOTSBuffUI.BrittleButton);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0007E7D0 File Offset: 0x0007C9D0
		private void DoubleVisionClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSOTSBuffUI.BuffClick(3, PermanentSOTSBuffUI.DoubleVisionButton);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0007E7DD File Offset: 0x0007C9DD
		private void HarmonyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSOTSBuffUI.BuffClick(4, PermanentSOTSBuffUI.HarmonyButton);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0007E7EA File Offset: 0x0007C9EA
		private void NightmareClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSOTSBuffUI.BuffClick(5, PermanentSOTSBuffUI.NightmareButton);
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0007E7F7 File Offset: 0x0007C9F7
		private void RippleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSOTSBuffUI.BuffClick(6, PermanentSOTSBuffUI.RippleButton);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0007E804 File Offset: 0x0007CA04
		private void RoughskinClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSOTSBuffUI.BuffClick(7, PermanentSOTSBuffUI.RoughskinButton);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0007E811 File Offset: 0x0007CA11
		private void SoulAccessClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSOTSBuffUI.BuffClick(8, PermanentSOTSBuffUI.SoulAccessButton);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0007E81E File Offset: 0x0007CA1E
		private void VibeClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSOTSBuffUI.BuffClick(9, PermanentSOTSBuffUI.VibeButton);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0007E82C File Offset: 0x0007CA2C
		private void DigitalDisplayClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSOTSBuffUI.BuffClick(10, PermanentSOTSBuffUI.DigitalDisplayButton);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0007E83C File Offset: 0x0007CA3C
		public static void GetSOTSBuffData()
		{
			if (!ModConditions.secretsOfTheShadowsLoaded)
			{
				return;
			}
			PermanentSOTSBuffUI.AssassinationButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Assassination")).DisplayName;
			PermanentSOTSBuffUI.BluefireButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Bluefire")).DisplayName;
			PermanentSOTSBuffUI.BrittleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Brittle")).DisplayName;
			PermanentSOTSBuffUI.DoubleVisionButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "DoubleVision")).DisplayName;
			PermanentSOTSBuffUI.HarmonyButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Harmony")).DisplayName;
			PermanentSOTSBuffUI.NightmareButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Nightmare")).DisplayName;
			PermanentSOTSBuffUI.RippleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "RippleBuff")).DisplayName;
			PermanentSOTSBuffUI.RoughskinButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Roughskin")).DisplayName;
			PermanentSOTSBuffUI.SoulAccessButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "SoulAccess")).DisplayName;
			PermanentSOTSBuffUI.VibeButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "GoodVibes")).DisplayName;
			PermanentSOTSBuffUI.DigitalDisplayButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "CyberneticEnhancements")).DisplayName;
			PermanentSOTSBuffUI.AssassinationButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Assassination")), 2);
			PermanentSOTSBuffUI.BluefireButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Bluefire")), 2);
			PermanentSOTSBuffUI.BrittleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Brittle")), 2);
			PermanentSOTSBuffUI.DoubleVisionButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "DoubleVision")), 2);
			PermanentSOTSBuffUI.HarmonyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Harmony")), 2);
			PermanentSOTSBuffUI.NightmareButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Nightmare")), 2);
			PermanentSOTSBuffUI.RippleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "RippleBuff")), 2);
			PermanentSOTSBuffUI.RoughskinButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Roughskin")), 2);
			PermanentSOTSBuffUI.SoulAccessButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "SoulAccess")), 2);
			PermanentSOTSBuffUI.VibeButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "GoodVibes")), 2);
			PermanentSOTSBuffUI.DigitalDisplayButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "CyberneticEnhancements")), 2);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0007EB98 File Offset: 0x0007CD98
		public override void Update(GameTime gameTime)
		{
			if (!PermanentSOTSBuffUI.visible)
			{
				return;
			}
			for (int i = 0; i < PermanentBuffPlayer.PermanentSOTSBuffsBools.Length; i++)
			{
				this.allBuffButtons.ElementAt(i).disabled = PermanentBuffPlayer.PermanentSOTSBuffsBools[i];
				this.allBuffButtons.ElementAt(i).moddedBuff = true;
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0007EBE9 File Offset: 0x0007CDE9
		public static void BuffClick(int buff, PermanentBuffButton button)
		{
			if (Main.GameUpdateCount - PermanentSOTSBuffUI.timeStart >= 10U)
			{
				PermanentBuffPlayer.PermanentSOTSBuffsBools[buff] = !PermanentBuffPlayer.PermanentSOTSBuffsBools[buff];
				button.disabled = PermanentBuffPlayer.PermanentSOTSBuffsBools[buff];
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0007EC18 File Offset: 0x0007CE18
		private static void CreateBuffButton(PermanentBuffButton button, float left, float top)
		{
			button.Left.Set(left, 0f);
			button.Top.Set(top, 0f);
			button.Width.Set(32f, 0f);
			button.Height.Set(32f, 0f);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0007EC74 File Offset: 0x0007CE74
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			this.offset = new Vector2(evt.MousePosition.X - this.BuffPanel.Left.Pixels, evt.MousePosition.Y - this.BuffPanel.Top.Pixels);
			this.dragging = true;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0007ECCC File Offset: 0x0007CECC
		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			this.dragging = false;
			this.BuffPanel.Left.Set(end.X - this.offset.X, 0f);
			this.BuffPanel.Top.Set(end.Y - this.offset.Y, 0f);
			this.Recalculate();
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0007ED3C File Offset: 0x0007CF3C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Vector2 MousePosition;
			MousePosition..ctor((float)Main.mouseX, (float)Main.mouseY);
			if (this.BuffPanel.ContainsPoint(MousePosition))
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			if (this.dragging)
			{
				this.BuffPanel.Left.Set(MousePosition.X - this.offset.X, 0f);
				this.BuffPanel.Top.Set(MousePosition.Y - this.offset.Y, 0f);
				this.Recalculate();
			}
		}

		// Token: 0x040006C8 RID: 1736
		public UIPanel BuffPanel;

		// Token: 0x040006C9 RID: 1737
		public static bool visible = false;

		// Token: 0x040006CA RID: 1738
		public static uint timeStart;

		// Token: 0x040006CB RID: 1739
		public static PermanentBuffButton AssassinationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006CC RID: 1740
		public static PermanentBuffButton BluefireButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006CD RID: 1741
		public static PermanentBuffButton BrittleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006CE RID: 1742
		public static PermanentBuffButton DoubleVisionButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006CF RID: 1743
		public static PermanentBuffButton HarmonyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006D0 RID: 1744
		public static PermanentBuffButton NightmareButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006D1 RID: 1745
		public static PermanentBuffButton RippleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006D2 RID: 1746
		public static PermanentBuffButton RoughskinButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006D3 RID: 1747
		public static PermanentBuffButton SoulAccessButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006D4 RID: 1748
		public static PermanentBuffButton VibeButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006D5 RID: 1749
		public static PermanentBuffButton DigitalDisplayButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006D6 RID: 1750
		private HashSet<PermanentBuffButton> allBuffButtons = new HashSet<PermanentBuffButton>();

		// Token: 0x040006D7 RID: 1751
		private Vector2 offset;

		// Token: 0x040006D8 RID: 1752
		public bool dragging;
	}
}
