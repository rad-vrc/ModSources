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
	// Token: 0x02000278 RID: 632
	public class PermanentSpiritClassicBuffUI : UIState
	{
		// Token: 0x0600101D RID: 4125 RVA: 0x0007EEE0 File Offset: 0x0007D0E0
		public override void OnInitialize()
		{
			this.BuffPanel = new UIPanel();
			this.BuffPanel.SetPadding(0f);
			this.BuffPanel.Left.Set(575f, 0f);
			this.BuffPanel.Top.Set(275f, 0f);
			this.BuffPanel.Width.Set(352f, 0f);
			this.BuffPanel.Height.Set(176f, 0f);
			this.BuffPanel.BackgroundColor = new Color(73, 94, 171);
			this.BuffPanel.OnLeftMouseDown += this.DragStart;
			this.BuffPanel.OnLeftMouseUp += this.DragEnd;
			UIText arenaText = new UIText(UISystem.ArenaText, 1f, false);
			arenaText.Left.Set(16f, 0f);
			arenaText.Top.Set(8f, 0f);
			arenaText.Width.Set(64f, 0f);
			arenaText.Height.Set(32f, 0f);
			this.BuffPanel.Append(arenaText);
			UIText potionText = new UIText(UISystem.PotionText, 1f, false);
			potionText.Left.Set(16f, 0f);
			potionText.Top.Set(72f, 0f);
			potionText.Width.Set(64f, 0f);
			potionText.Height.Set(32f, 0f);
			this.BuffPanel.Append(potionText);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.CoiledEnergizerButton, 16f, 32f);
			PermanentSpiritClassicBuffUI.CoiledEnergizerButton.OnLeftClick += this.CoiledEnergizerClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.CoiledEnergizerButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.KoiTotemButton, 48f, 32f);
			PermanentSpiritClassicBuffUI.KoiTotemButton.OnLeftClick += this.KoiTotemClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.KoiTotemButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.SunPotButton, 80f, 32f);
			PermanentSpiritClassicBuffUI.SunPotButton.OnLeftClick += this.SunPotClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.SunPotButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.TheCouchButton, 112f, 32f);
			PermanentSpiritClassicBuffUI.TheCouchButton.OnLeftClick += this.TheCouchClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.TheCouchButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.JumpButton, 16f, 96f);
			PermanentSpiritClassicBuffUI.JumpButton.OnLeftClick += this.JumpClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.JumpButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.MirrorCoatButton, 48f, 96f);
			PermanentSpiritClassicBuffUI.MirrorCoatButton.OnLeftClick += this.MirrorCoatClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.MirrorCoatButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.MoonJellyButton, 80f, 96f);
			PermanentSpiritClassicBuffUI.MoonJellyButton.OnLeftClick += this.MoonJellyClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.MoonJellyButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.RunescribeButton, 112f, 96f);
			PermanentSpiritClassicBuffUI.RunescribeButton.OnLeftClick += this.RunescribeClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.RunescribeButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.SoulguardButton, 144f, 96f);
			PermanentSpiritClassicBuffUI.SoulguardButton.OnLeftClick += this.SoulguardClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.SoulguardButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.SpiritButton, 176f, 96f);
			PermanentSpiritClassicBuffUI.SpiritButton.OnLeftClick += this.SpiritClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.SpiritButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.SoaringButton, 208f, 96f);
			PermanentSpiritClassicBuffUI.SoaringButton.OnLeftClick += this.SoaringClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.SoaringButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.SporecoidButton, 240f, 96f);
			PermanentSpiritClassicBuffUI.SporecoidButton.OnLeftClick += this.SporecoidClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.SporecoidButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.StarburnButton, 272f, 96f);
			PermanentSpiritClassicBuffUI.StarburnButton.OnLeftClick += this.StarburnClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.StarburnButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.SteadfastButton, 304f, 96f);
			PermanentSpiritClassicBuffUI.SteadfastButton.OnLeftClick += this.SteadfastClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.SteadfastButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.ToxinButton, 16f, 128f);
			PermanentSpiritClassicBuffUI.ToxinButton.OnLeftClick += this.ToxinClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.ToxinButton);
			PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.ZephyrButton, 48f, 128f);
			PermanentSpiritClassicBuffUI.ZephyrButton.OnLeftClick += this.ZephyrClicked;
			this.BuffPanel.Append(PermanentSpiritClassicBuffUI.ZephyrButton);
			HashSet<PermanentBuffButton> hashSet = new HashSet<PermanentBuffButton>();
			hashSet.Add(PermanentSpiritClassicBuffUI.CoiledEnergizerButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.KoiTotemButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.SunPotButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.TheCouchButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.JumpButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.MirrorCoatButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.MoonJellyButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.RunescribeButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.SoulguardButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.SpiritButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.SoaringButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.SporecoidButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.StarburnButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.SteadfastButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.ToxinButton);
			hashSet.Add(PermanentSpiritClassicBuffUI.ZephyrButton);
			HashSet<PermanentBuffButton> buttons = hashSet;
			this.allBuffButtons.UnionWith(buttons);
			base.Append(this.BuffPanel);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0007F517 File Offset: 0x0007D717
		private void CoiledEnergizerClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(0, PermanentSpiritClassicBuffUI.CoiledEnergizerButton);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0007F524 File Offset: 0x0007D724
		private void KoiTotemClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(1, PermanentSpiritClassicBuffUI.KoiTotemButton);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0007F531 File Offset: 0x0007D731
		private void SunPotClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(2, PermanentSpiritClassicBuffUI.SunPotButton);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0007F53E File Offset: 0x0007D73E
		private void TheCouchClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(3, PermanentSpiritClassicBuffUI.TheCouchButton);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0007F54B File Offset: 0x0007D74B
		private void JumpClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(4, PermanentSpiritClassicBuffUI.JumpButton);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0007F558 File Offset: 0x0007D758
		private void MirrorCoatClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(5, PermanentSpiritClassicBuffUI.MirrorCoatButton);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0007F565 File Offset: 0x0007D765
		private void MoonJellyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(6, PermanentSpiritClassicBuffUI.MoonJellyButton);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0007F572 File Offset: 0x0007D772
		private void RunescribeClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(7, PermanentSpiritClassicBuffUI.RunescribeButton);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0007F57F File Offset: 0x0007D77F
		private void SoulguardClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(8, PermanentSpiritClassicBuffUI.SoulguardButton);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0007F58C File Offset: 0x0007D78C
		private void SpiritClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(9, PermanentSpiritClassicBuffUI.SpiritButton);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0007F59A File Offset: 0x0007D79A
		private void SoaringClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(10, PermanentSpiritClassicBuffUI.SoaringButton);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0007F5A8 File Offset: 0x0007D7A8
		private void SporecoidClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(11, PermanentSpiritClassicBuffUI.SporecoidButton);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0007F5B6 File Offset: 0x0007D7B6
		private void StarburnClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(12, PermanentSpiritClassicBuffUI.StarburnButton);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0007F5C4 File Offset: 0x0007D7C4
		private void SteadfastClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(13, PermanentSpiritClassicBuffUI.SteadfastButton);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0007F5D2 File Offset: 0x0007D7D2
		private void ToxinClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(14, PermanentSpiritClassicBuffUI.ToxinButton);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0007F5E0 File Offset: 0x0007D7E0
		private void ZephyrClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentSpiritClassicBuffUI.BuffClick(15, PermanentSpiritClassicBuffUI.ZephyrButton);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0007F5F0 File Offset: 0x0007D7F0
		public static void GetSpiritClassicBuffData()
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			PermanentSpiritClassicBuffUI.CoiledEnergizerButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "OverDrive")).DisplayName;
			PermanentSpiritClassicBuffUI.KoiTotemButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "KoiTotemBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.SunPotButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "SunPotBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.TheCouchButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "CouchPotato")).DisplayName;
			PermanentSpiritClassicBuffUI.JumpButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "PinkPotionBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.MirrorCoatButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "MirrorCoatBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.MoonJellyButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "MoonBlessing")).DisplayName;
			PermanentSpiritClassicBuffUI.RunescribeButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "RunePotionBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.SoulguardButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "SoulPotionBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.SpiritButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "SpiritBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.SoaringButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "FlightPotionBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.SporecoidButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "MushroomPotionBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.StarburnButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "StarPotionBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.SteadfastButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "TurtlePotionBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.ToxinButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "BismitePotionBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.ZephyrButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "DoubleJumpPotionBuff")).DisplayName;
			PermanentSpiritClassicBuffUI.CoiledEnergizerButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "OverDrive")), 2);
			PermanentSpiritClassicBuffUI.KoiTotemButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "KoiTotemBuff")), 2);
			PermanentSpiritClassicBuffUI.SunPotButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "SunPotBuff")), 2);
			PermanentSpiritClassicBuffUI.TheCouchButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "CouchPotato")), 2);
			PermanentSpiritClassicBuffUI.JumpButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "PinkPotionBuff")), 2);
			PermanentSpiritClassicBuffUI.MirrorCoatButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "MirrorCoatBuff")), 2);
			PermanentSpiritClassicBuffUI.MoonJellyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "MoonBlessing")), 2);
			PermanentSpiritClassicBuffUI.RunescribeButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "RunePotionBuff")), 2);
			PermanentSpiritClassicBuffUI.SoulguardButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "SoulPotionBuff")), 2);
			PermanentSpiritClassicBuffUI.SpiritButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "SpiritBuff")), 2);
			PermanentSpiritClassicBuffUI.SoaringButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "FlightPotionBuff")), 2);
			PermanentSpiritClassicBuffUI.SporecoidButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "MushroomPotionBuff")), 2);
			PermanentSpiritClassicBuffUI.StarburnButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "StarPotionBuff")), 2);
			PermanentSpiritClassicBuffUI.SteadfastButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "TurtlePotionBuff")), 2);
			PermanentSpiritClassicBuffUI.ToxinButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "BismitePotionBuff")), 2);
			PermanentSpiritClassicBuffUI.ZephyrButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "DoubleJumpPotionBuff")), 2);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0007FAC8 File Offset: 0x0007DCC8
		public override void Update(GameTime gameTime)
		{
			if (!PermanentSpiritClassicBuffUI.visible)
			{
				return;
			}
			for (int i = 0; i < PermanentBuffPlayer.PermanentSpiritClassicBuffsBools.Length; i++)
			{
				this.allBuffButtons.ElementAt(i).disabled = PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[i];
				this.allBuffButtons.ElementAt(i).moddedBuff = true;
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0007FB19 File Offset: 0x0007DD19
		public static void BuffClick(int buff, PermanentBuffButton button)
		{
			if (Main.GameUpdateCount - PermanentSpiritClassicBuffUI.timeStart >= 10U)
			{
				PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[buff] = !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[buff];
				button.disabled = PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[buff];
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0007FB48 File Offset: 0x0007DD48
		private static void CreateBuffButton(PermanentBuffButton button, float left, float top)
		{
			button.Left.Set(left, 0f);
			button.Top.Set(top, 0f);
			button.Width.Set(32f, 0f);
			button.Height.Set(32f, 0f);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0007FBA4 File Offset: 0x0007DDA4
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			this.offset = new Vector2(evt.MousePosition.X - this.BuffPanel.Left.Pixels, evt.MousePosition.Y - this.BuffPanel.Top.Pixels);
			this.dragging = true;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0007FBFC File Offset: 0x0007DDFC
		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			this.dragging = false;
			this.BuffPanel.Left.Set(end.X - this.offset.X, 0f);
			this.BuffPanel.Top.Set(end.Y - this.offset.Y, 0f);
			this.Recalculate();
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0007FC6C File Offset: 0x0007DE6C
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

		// Token: 0x040006D9 RID: 1753
		public UIPanel BuffPanel;

		// Token: 0x040006DA RID: 1754
		public static bool visible = false;

		// Token: 0x040006DB RID: 1755
		public static uint timeStart;

		// Token: 0x040006DC RID: 1756
		public static PermanentBuffButton CoiledEnergizerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006DD RID: 1757
		public static PermanentBuffButton KoiTotemButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006DE RID: 1758
		public static PermanentBuffButton SunPotButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006DF RID: 1759
		public static PermanentBuffButton TheCouchButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006E0 RID: 1760
		public static PermanentBuffButton JumpButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006E1 RID: 1761
		public static PermanentBuffButton MirrorCoatButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006E2 RID: 1762
		public static PermanentBuffButton MoonJellyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006E3 RID: 1763
		public static PermanentBuffButton RunescribeButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006E4 RID: 1764
		public static PermanentBuffButton SoulguardButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006E5 RID: 1765
		public static PermanentBuffButton SpiritButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006E6 RID: 1766
		public static PermanentBuffButton SoaringButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006E7 RID: 1767
		public static PermanentBuffButton SporecoidButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006E8 RID: 1768
		public static PermanentBuffButton StarburnButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006E9 RID: 1769
		public static PermanentBuffButton SteadfastButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006EA RID: 1770
		public static PermanentBuffButton ToxinButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006EB RID: 1771
		public static PermanentBuffButton ZephyrButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006EC RID: 1772
		private HashSet<PermanentBuffButton> allBuffButtons = new HashSet<PermanentBuffButton>();

		// Token: 0x040006ED RID: 1773
		private Vector2 offset;

		// Token: 0x040006EE RID: 1774
		public bool dragging;
	}
}
