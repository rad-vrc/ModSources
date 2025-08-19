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
	// Token: 0x02000276 RID: 630
	public class PermanentMartinsOrderBuffUI : UIState
	{
		// Token: 0x06000FEB RID: 4075 RVA: 0x0007D118 File Offset: 0x0007B318
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
			UIText potionText = new UIText(UISystem.PotionText, 1f, false);
			potionText.Left.Set(16f, 0f);
			potionText.Top.Set(8f, 0f);
			potionText.Width.Set(64f, 0f);
			potionText.Height.Set(32f, 0f);
			this.BuffPanel.Append(potionText);
			UIText stationsText = new UIText(UISystem.StationText, 1f, false);
			stationsText.Left.Set(16f, 0f);
			stationsText.Top.Set(104f, 0f);
			stationsText.Width.Set(64f, 0f);
			stationsText.Height.Set(32f, 0f);
			this.BuffPanel.Append(stationsText);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.BlackHoleButton, 16f, 32f);
			PermanentMartinsOrderBuffUI.BlackHoleButton.OnLeftClick += this.BlackHoleClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.BlackHoleButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.ChargingButton, 48f, 32f);
			PermanentMartinsOrderBuffUI.ChargingButton.OnLeftClick += this.ChargingClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.ChargingButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.DefenderButton, 80f, 32f);
			PermanentMartinsOrderBuffUI.DefenderButton.OnLeftClick += this.DefenderClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.DefenderButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.EmpowermentButton, 112f, 32f);
			PermanentMartinsOrderBuffUI.EmpowermentButton.OnLeftClick += this.EmpowermentClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.EmpowermentButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.EvocationButton, 144f, 32f);
			PermanentMartinsOrderBuffUI.EvocationButton.OnLeftClick += this.EvocationClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.EvocationButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.GourmetFlavorButton, 176f, 32f);
			PermanentMartinsOrderBuffUI.GourmetFlavorButton.OnLeftClick += this.GourmetFlavorClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.GourmetFlavorButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.HasteButton, 208f, 32f);
			PermanentMartinsOrderBuffUI.HasteButton.OnLeftClick += this.HasteClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.HasteButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.HealingButton, 240f, 32f);
			PermanentMartinsOrderBuffUI.HealingButton.OnLeftClick += this.HealingClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.HealingButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.RockskinButton, 272f, 32f);
			PermanentMartinsOrderBuffUI.RockskinButton.OnLeftClick += this.RockskinClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.RockskinButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.ShooterButton, 304f, 32f);
			PermanentMartinsOrderBuffUI.ShooterButton.OnLeftClick += this.ShooterClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.ShooterButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.SoulButton, 16f, 64f);
			PermanentMartinsOrderBuffUI.SoulButton.OnLeftClick += this.SoulClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.SoulButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.SpellCasterButton, 48f, 64f);
			PermanentMartinsOrderBuffUI.SpellCasterButton.OnLeftClick += this.SpellCasterClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.SpellCasterButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.StarreachButton, 80f, 64f);
			PermanentMartinsOrderBuffUI.StarreachButton.OnLeftClick += this.StarreachClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.StarreachButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.SweeperButton, 112f, 64f);
			PermanentMartinsOrderBuffUI.SweeperButton.OnLeftClick += this.SweeperClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.SweeperButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.ThrowerButton, 144f, 64f);
			PermanentMartinsOrderBuffUI.ThrowerButton.OnLeftClick += this.ThrowerClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.ThrowerButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.WhipperButton, 176f, 64f);
			PermanentMartinsOrderBuffUI.WhipperButton.OnLeftClick += this.WhipperClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.WhipperButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.ZincPillButton, 208f, 64f);
			PermanentMartinsOrderBuffUI.ZincPillButton.OnLeftClick += this.ZincPillClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.ZincPillButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.ArcheologyButton, 16f, 128f);
			PermanentMartinsOrderBuffUI.ArcheologyButton.OnLeftClick += this.ArcheologyClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.ArcheologyButton);
			PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.SporeFarmButton, 48f, 128f);
			PermanentMartinsOrderBuffUI.SporeFarmButton.OnLeftClick += this.SporeFarmClicked;
			this.BuffPanel.Append(PermanentMartinsOrderBuffUI.SporeFarmButton);
			HashSet<PermanentBuffButton> hashSet = new HashSet<PermanentBuffButton>();
			hashSet.Add(PermanentMartinsOrderBuffUI.BlackHoleButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.ChargingButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.DefenderButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.EmpowermentButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.EvocationButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.GourmetFlavorButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.HasteButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.HealingButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.RockskinButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.ShooterButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.SoulButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.SpellCasterButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.StarreachButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.SweeperButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.ThrowerButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.WhipperButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.ZincPillButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.ArcheologyButton);
			hashSet.Add(PermanentMartinsOrderBuffUI.SporeFarmButton);
			HashSet<PermanentBuffButton> buttons = hashSet;
			this.allBuffButtons.UnionWith(buttons);
			base.Append(this.BuffPanel);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0007D821 File Offset: 0x0007BA21
		private void BlackHoleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(0, PermanentMartinsOrderBuffUI.BlackHoleButton);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0007D82E File Offset: 0x0007BA2E
		private void ChargingClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(1, PermanentMartinsOrderBuffUI.ChargingButton);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0007D83B File Offset: 0x0007BA3B
		private void DefenderClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(2, PermanentMartinsOrderBuffUI.DefenderButton);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0007D848 File Offset: 0x0007BA48
		private void EmpowermentClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(3, PermanentMartinsOrderBuffUI.EmpowermentButton);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0007D855 File Offset: 0x0007BA55
		private void EvocationClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(4, PermanentMartinsOrderBuffUI.EvocationButton);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0007D862 File Offset: 0x0007BA62
		private void GourmetFlavorClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(5, PermanentMartinsOrderBuffUI.GourmetFlavorButton);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0007D86F File Offset: 0x0007BA6F
		private void HasteClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(6, PermanentMartinsOrderBuffUI.HasteButton);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0007D87C File Offset: 0x0007BA7C
		private void HealingClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(7, PermanentMartinsOrderBuffUI.HealingButton);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0007D889 File Offset: 0x0007BA89
		private void RockskinClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(8, PermanentMartinsOrderBuffUI.RockskinButton);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0007D896 File Offset: 0x0007BA96
		private void ShooterClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(9, PermanentMartinsOrderBuffUI.ShooterButton);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0007D8A4 File Offset: 0x0007BAA4
		private void SoulClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(10, PermanentMartinsOrderBuffUI.SoulButton);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0007D8B2 File Offset: 0x0007BAB2
		private void SpellCasterClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(11, PermanentMartinsOrderBuffUI.SpellCasterButton);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0007D8C0 File Offset: 0x0007BAC0
		private void StarreachClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(12, PermanentMartinsOrderBuffUI.StarreachButton);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0007D8CE File Offset: 0x0007BACE
		private void SweeperClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(13, PermanentMartinsOrderBuffUI.SweeperButton);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0007D8DC File Offset: 0x0007BADC
		private void ThrowerClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(14, PermanentMartinsOrderBuffUI.ThrowerButton);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0007D8EA File Offset: 0x0007BAEA
		private void WhipperClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(15, PermanentMartinsOrderBuffUI.WhipperButton);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0007D8F8 File Offset: 0x0007BAF8
		private void ZincPillClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(16, PermanentMartinsOrderBuffUI.ZincPillButton);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0007D906 File Offset: 0x0007BB06
		private void ArcheologyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(17, PermanentMartinsOrderBuffUI.ArcheologyButton);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0007D914 File Offset: 0x0007BB14
		private void SporeFarmClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentMartinsOrderBuffUI.BuffClick(18, PermanentMartinsOrderBuffUI.SporeFarmButton);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0007D924 File Offset: 0x0007BB24
		public static void GetMartinsOrderBuffData()
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			PermanentMartinsOrderBuffUI.BlackHoleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "BlackHoleBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.ChargingButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "Charging")).DisplayName;
			PermanentMartinsOrderBuffUI.DefenderButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "TurretBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.EmpowermentButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "EmpowermentBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.EvocationButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "SummonSpeedBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.GourmetFlavorButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "Gourmet")).DisplayName;
			PermanentMartinsOrderBuffUI.HasteButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "HasteBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.HealingButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "Healing")).DisplayName;
			PermanentMartinsOrderBuffUI.RockskinButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "RockskinBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.ShooterButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "ShooterBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.SoulButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "SoulBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.SpellCasterButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "CasterBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.StarreachButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "Starreach")).DisplayName;
			PermanentMartinsOrderBuffUI.SweeperButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "SweepBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.ThrowerButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "ThrowerBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.WhipperButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "WhipperBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.ZincPillButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "ZincPillBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.ArcheologyButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "ReschBuff")).DisplayName;
			PermanentMartinsOrderBuffUI.SporeFarmButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "SporeSave")).DisplayName;
			PermanentMartinsOrderBuffUI.BlackHoleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "BlackHoleBuff")), 2);
			PermanentMartinsOrderBuffUI.ChargingButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "Charging")), 2);
			PermanentMartinsOrderBuffUI.DefenderButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "TurretBuff")), 2);
			PermanentMartinsOrderBuffUI.EmpowermentButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "EmpowermentBuff")), 2);
			PermanentMartinsOrderBuffUI.EvocationButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "SummonSpeedBuff")), 2);
			PermanentMartinsOrderBuffUI.GourmetFlavorButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "Gourmet")), 2);
			PermanentMartinsOrderBuffUI.HasteButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "HasteBuff")), 2);
			PermanentMartinsOrderBuffUI.HealingButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "Healing")), 2);
			PermanentMartinsOrderBuffUI.RockskinButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "RockskinBuff")), 2);
			PermanentMartinsOrderBuffUI.ShooterButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "ShooterBuff")), 2);
			PermanentMartinsOrderBuffUI.SoulButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "SoulBuff")), 2);
			PermanentMartinsOrderBuffUI.SpellCasterButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "CasterBuff")), 2);
			PermanentMartinsOrderBuffUI.StarreachButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "Starreach")), 2);
			PermanentMartinsOrderBuffUI.SweeperButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "SweepBuff")), 2);
			PermanentMartinsOrderBuffUI.ThrowerButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "ThrowerBuff")), 2);
			PermanentMartinsOrderBuffUI.WhipperButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "WhipperBuff")), 2);
			PermanentMartinsOrderBuffUI.ZincPillButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "ZincPillBuff")), 2);
			PermanentMartinsOrderBuffUI.ArcheologyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "ReschBuff")), 2);
			PermanentMartinsOrderBuffUI.SporeFarmButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "SporeSave")), 2);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0007DEE0 File Offset: 0x0007C0E0
		public override void Update(GameTime gameTime)
		{
			if (!PermanentMartinsOrderBuffUI.visible)
			{
				return;
			}
			for (int i = 0; i < PermanentBuffPlayer.PermanentMartinsOrderBuffsBools.Length; i++)
			{
				this.allBuffButtons.ElementAt(i).disabled = PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[i];
				this.allBuffButtons.ElementAt(i).moddedBuff = true;
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0007DF31 File Offset: 0x0007C131
		public static void BuffClick(int buff, PermanentBuffButton button)
		{
			if (Main.GameUpdateCount - PermanentMartinsOrderBuffUI.timeStart >= 10U)
			{
				PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[buff] = !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[buff];
				button.disabled = PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[buff];
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0007DF60 File Offset: 0x0007C160
		private static void CreateBuffButton(PermanentBuffButton button, float left, float top)
		{
			button.Left.Set(left, 0f);
			button.Top.Set(top, 0f);
			button.Width.Set(32f, 0f);
			button.Height.Set(32f, 0f);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0007DFBC File Offset: 0x0007C1BC
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			this.offset = new Vector2(evt.MousePosition.X - this.BuffPanel.Left.Pixels, evt.MousePosition.Y - this.BuffPanel.Top.Pixels);
			this.dragging = true;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0007E014 File Offset: 0x0007C214
		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			this.dragging = false;
			this.BuffPanel.Left.Set(end.X - this.offset.X, 0f);
			this.BuffPanel.Top.Set(end.Y - this.offset.Y, 0f);
			this.Recalculate();
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0007E084 File Offset: 0x0007C284
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

		// Token: 0x040006AF RID: 1711
		public UIPanel BuffPanel;

		// Token: 0x040006B0 RID: 1712
		public static bool visible = false;

		// Token: 0x040006B1 RID: 1713
		public static uint timeStart;

		// Token: 0x040006B2 RID: 1714
		public static PermanentBuffButton BlackHoleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006B3 RID: 1715
		public static PermanentBuffButton ChargingButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006B4 RID: 1716
		public static PermanentBuffButton DefenderButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006B5 RID: 1717
		public static PermanentBuffButton EmpowermentButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006B6 RID: 1718
		public static PermanentBuffButton EvocationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006B7 RID: 1719
		public static PermanentBuffButton GourmetFlavorButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006B8 RID: 1720
		public static PermanentBuffButton HasteButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006B9 RID: 1721
		public static PermanentBuffButton HealingButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006BA RID: 1722
		public static PermanentBuffButton RockskinButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006BB RID: 1723
		public static PermanentBuffButton ShooterButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006BC RID: 1724
		public static PermanentBuffButton SoulButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006BD RID: 1725
		public static PermanentBuffButton SpellCasterButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006BE RID: 1726
		public static PermanentBuffButton StarreachButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006BF RID: 1727
		public static PermanentBuffButton SweeperButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006C0 RID: 1728
		public static PermanentBuffButton ThrowerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006C1 RID: 1729
		public static PermanentBuffButton WhipperButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006C2 RID: 1730
		public static PermanentBuffButton ZincPillButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006C3 RID: 1731
		public static PermanentBuffButton ArcheologyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006C4 RID: 1732
		public static PermanentBuffButton SporeFarmButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006C5 RID: 1733
		private HashSet<PermanentBuffButton> allBuffButtons = new HashSet<PermanentBuffButton>();

		// Token: 0x040006C6 RID: 1734
		private Vector2 offset;

		// Token: 0x040006C7 RID: 1735
		public bool dragging;
	}
}
