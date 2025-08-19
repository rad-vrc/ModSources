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
	// Token: 0x02000279 RID: 633
	public class PermanentThoriumBuffUI : UIState
	{
		// Token: 0x06001037 RID: 4151 RVA: 0x0007FE78 File Offset: 0x0007E078
		public override void OnInitialize()
		{
			this.BuffPanel = new UIPanel();
			this.BuffPanel.SetPadding(0f);
			this.BuffPanel.Left.Set(575f, 0f);
			this.BuffPanel.Top.Set(275f, 0f);
			this.BuffPanel.Width.Set(352f, 0f);
			this.BuffPanel.Height.Set(368f, 0f);
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
			UIText repellentText = new UIText(UISystem.RepellentText, 1f, false);
			repellentText.Left.Set(16f, 0f);
			repellentText.Top.Set(168f, 0f);
			repellentText.Width.Set(64f, 0f);
			repellentText.Height.Set(32f, 0f);
			this.BuffPanel.Append(repellentText);
			UIText stationsText = new UIText(UISystem.StationText, 1f, false);
			stationsText.Left.Set(16f, 0f);
			stationsText.Top.Set(232f, 0f);
			stationsText.Width.Set(64f, 0f);
			stationsText.Height.Set(32f, 0f);
			this.BuffPanel.Append(stationsText);
			UIText addonsText = new UIText(UISystem.AddonText, 1f, false);
			addonsText.Left.Set(16f, 0f);
			addonsText.Top.Set(296f, 0f);
			addonsText.Width.Set(64f, 0f);
			addonsText.Height.Set(32f, 0f);
			this.BuffPanel.Append(addonsText);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.MistletoeButton, 16f, 32f);
			PermanentThoriumBuffUI.MistletoeButton.OnLeftClick += this.MistletoeClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.MistletoeButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.AquaAffinityButton, 16f, 96f);
			PermanentThoriumBuffUI.AquaAffinityButton.OnLeftClick += this.AquaAffinityClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.AquaAffinityButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.ArcaneButton, 48f, 96f);
			PermanentThoriumBuffUI.ArcaneButton.OnLeftClick += this.ArcaneClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.ArcaneButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.ArtilleryButton, 80f, 96f);
			PermanentThoriumBuffUI.ArtilleryButton.OnLeftClick += this.ArtilleryClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.ArtilleryButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.AssassinButton, 112f, 96f);
			PermanentThoriumBuffUI.AssassinButton.OnLeftClick += this.AssassinClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.AssassinButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.BloodRushButton, 144f, 96f);
			PermanentThoriumBuffUI.BloodRushButton.OnLeftClick += this.BloodRushClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.BloodRushButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.BouncingFlameButton, 176f, 96f);
			PermanentThoriumBuffUI.BouncingFlameButton.OnLeftClick += this.BouncingFlameClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.BouncingFlameButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.CactusFruitButton, 208f, 96f);
			PermanentThoriumBuffUI.CactusFruitButton.OnLeftClick += this.CactusFruitClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.CactusFruitButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.ConflagrationButton, 240f, 96f);
			PermanentThoriumBuffUI.ConflagrationButton.OnLeftClick += this.ConflagrationClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.ConflagrationButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.CreativityButton, 272f, 96f);
			PermanentThoriumBuffUI.CreativityButton.OnLeftClick += this.CreativityClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.CreativityButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.EarwormButton, 304f, 96f);
			PermanentThoriumBuffUI.EarwormButton.OnLeftClick += this.EarwormClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.EarwormButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.FrenzyButton, 16f, 128f);
			PermanentThoriumBuffUI.FrenzyButton.OnLeftClick += this.FrenzyClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.FrenzyButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.GlowingButton, 48f, 128f);
			PermanentThoriumBuffUI.GlowingButton.OnLeftClick += this.GlowingClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.GlowingButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.HolyButton, 80f, 128f);
			PermanentThoriumBuffUI.HolyButton.OnLeftClick += this.HolyClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.HolyButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.HydrationButton, 112f, 128f);
			PermanentThoriumBuffUI.HydrationButton.OnLeftClick += this.HydrationClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.HydrationButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.InspirationalReachButton, 144f, 128f);
			PermanentThoriumBuffUI.InspirationalReachButton.OnLeftClick += this.InspirationalReachClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.InspirationalReachButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.KineticButton, 176f, 128f);
			PermanentThoriumBuffUI.KineticButton.OnLeftClick += this.KineticClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.KineticButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.WarmongerButton, 208f, 128f);
			PermanentThoriumBuffUI.WarmongerButton.OnLeftClick += this.WarmongerClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.WarmongerButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.BatRepellentButton, 16f, 192f);
			PermanentThoriumBuffUI.BatRepellentButton.OnLeftClick += this.BatRepellentClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.BatRepellentButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.FishRepellentButton, 48f, 192f);
			PermanentThoriumBuffUI.FishRepellentButton.OnLeftClick += this.FishRepellentClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.FishRepellentButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.InsectRepellentButton, 80f, 192f);
			PermanentThoriumBuffUI.InsectRepellentButton.OnLeftClick += this.InsectRepellentClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.InsectRepellentButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.SkeletonRepellentButton, 112f, 192f);
			PermanentThoriumBuffUI.SkeletonRepellentButton.OnLeftClick += this.SkeletonRepellentClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.SkeletonRepellentButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.ZombieRepellentButton, 144f, 192f);
			PermanentThoriumBuffUI.ZombieRepellentButton.OnLeftClick += this.ZombieRepellentClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.ZombieRepellentButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.AltarButton, 16f, 256f);
			PermanentThoriumBuffUI.AltarButton.OnLeftClick += this.AltarClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.AltarButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.ConductorsStandButton, 48f, 256f);
			PermanentThoriumBuffUI.ConductorsStandButton.OnLeftClick += this.ConductorsStandClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.ConductorsStandButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.NinjaRackButton, 80f, 256f);
			PermanentThoriumBuffUI.NinjaRackButton.OnLeftClick += this.NinjaRackClicked;
			this.BuffPanel.Append(PermanentThoriumBuffUI.NinjaRackButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.DeathsingerButton, 16f, 320f);
			PermanentThoriumBuffUI.DeathsingerButton.OnLeftClick += this.DeathsingerClicked;
			PermanentThoriumBuffUI.DeathsingerButton.ModTooltip = UISystem.UnloadedText;
			this.BuffPanel.Append(PermanentThoriumBuffUI.DeathsingerButton);
			PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.InspirationRegenerationButton, 48f, 320f);
			PermanentThoriumBuffUI.InspirationRegenerationButton.OnLeftClick += this.InspirationRegenerationClicked;
			PermanentThoriumBuffUI.InspirationRegenerationButton.ModTooltip = UISystem.UnloadedText;
			this.BuffPanel.Append(PermanentThoriumBuffUI.InspirationRegenerationButton);
			HashSet<PermanentBuffButton> hashSet = new HashSet<PermanentBuffButton>();
			hashSet.Add(PermanentThoriumBuffUI.MistletoeButton);
			hashSet.Add(PermanentThoriumBuffUI.AquaAffinityButton);
			hashSet.Add(PermanentThoriumBuffUI.ArcaneButton);
			hashSet.Add(PermanentThoriumBuffUI.ArtilleryButton);
			hashSet.Add(PermanentThoriumBuffUI.AssassinButton);
			hashSet.Add(PermanentThoriumBuffUI.BloodRushButton);
			hashSet.Add(PermanentThoriumBuffUI.BouncingFlameButton);
			hashSet.Add(PermanentThoriumBuffUI.CactusFruitButton);
			hashSet.Add(PermanentThoriumBuffUI.ConflagrationButton);
			hashSet.Add(PermanentThoriumBuffUI.CreativityButton);
			hashSet.Add(PermanentThoriumBuffUI.EarwormButton);
			hashSet.Add(PermanentThoriumBuffUI.FrenzyButton);
			hashSet.Add(PermanentThoriumBuffUI.GlowingButton);
			hashSet.Add(PermanentThoriumBuffUI.HolyButton);
			hashSet.Add(PermanentThoriumBuffUI.HydrationButton);
			hashSet.Add(PermanentThoriumBuffUI.InspirationalReachButton);
			hashSet.Add(PermanentThoriumBuffUI.KineticButton);
			hashSet.Add(PermanentThoriumBuffUI.WarmongerButton);
			hashSet.Add(PermanentThoriumBuffUI.BatRepellentButton);
			hashSet.Add(PermanentThoriumBuffUI.FishRepellentButton);
			hashSet.Add(PermanentThoriumBuffUI.InsectRepellentButton);
			hashSet.Add(PermanentThoriumBuffUI.SkeletonRepellentButton);
			hashSet.Add(PermanentThoriumBuffUI.ZombieRepellentButton);
			hashSet.Add(PermanentThoriumBuffUI.AltarButton);
			hashSet.Add(PermanentThoriumBuffUI.ConductorsStandButton);
			hashSet.Add(PermanentThoriumBuffUI.NinjaRackButton);
			hashSet.Add(PermanentThoriumBuffUI.DeathsingerButton);
			hashSet.Add(PermanentThoriumBuffUI.InspirationRegenerationButton);
			HashSet<PermanentBuffButton> buttons = hashSet;
			this.allBuffButtons.UnionWith(buttons);
			base.Append(this.BuffPanel);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00080970 File Offset: 0x0007EB70
		private void MistletoeClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(0, PermanentThoriumBuffUI.MistletoeButton);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0008097D File Offset: 0x0007EB7D
		private void AquaAffinityClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(1, PermanentThoriumBuffUI.AquaAffinityButton);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0008098A File Offset: 0x0007EB8A
		private void ArcaneClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(2, PermanentThoriumBuffUI.ArcaneButton);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00080997 File Offset: 0x0007EB97
		private void ArtilleryClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(3, PermanentThoriumBuffUI.ArtilleryButton);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x000809A4 File Offset: 0x0007EBA4
		private void AssassinClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(4, PermanentThoriumBuffUI.AssassinButton);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000809B1 File Offset: 0x0007EBB1
		private void BloodRushClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(5, PermanentThoriumBuffUI.BloodRushButton);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000809BE File Offset: 0x0007EBBE
		private void BouncingFlameClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(6, PermanentThoriumBuffUI.BouncingFlameButton);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x000809CB File Offset: 0x0007EBCB
		private void CactusFruitClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(7, PermanentThoriumBuffUI.CactusFruitButton);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x000809D8 File Offset: 0x0007EBD8
		private void ConflagrationClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(8, PermanentThoriumBuffUI.ConflagrationButton);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000809E5 File Offset: 0x0007EBE5
		private void CreativityClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(9, PermanentThoriumBuffUI.CreativityButton);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000809F3 File Offset: 0x0007EBF3
		private void EarwormClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(10, PermanentThoriumBuffUI.EarwormButton);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00080A01 File Offset: 0x0007EC01
		private void FrenzyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(11, PermanentThoriumBuffUI.FrenzyButton);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00080A0F File Offset: 0x0007EC0F
		private void GlowingClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(12, PermanentThoriumBuffUI.GlowingButton);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00080A1D File Offset: 0x0007EC1D
		private void HolyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(13, PermanentThoriumBuffUI.HolyButton);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00080A2B File Offset: 0x0007EC2B
		private void HydrationClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(14, PermanentThoriumBuffUI.HydrationButton);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00080A39 File Offset: 0x0007EC39
		private void InspirationalReachClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(15, PermanentThoriumBuffUI.InspirationalReachButton);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00080A47 File Offset: 0x0007EC47
		private void KineticClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(16, PermanentThoriumBuffUI.KineticButton);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00080A55 File Offset: 0x0007EC55
		private void WarmongerClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(17, PermanentThoriumBuffUI.WarmongerButton);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00080A63 File Offset: 0x0007EC63
		private void BatRepellentClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(18, PermanentThoriumBuffUI.BatRepellentButton);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00080A71 File Offset: 0x0007EC71
		private void FishRepellentClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(19, PermanentThoriumBuffUI.FishRepellentButton);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00080A7F File Offset: 0x0007EC7F
		private void InsectRepellentClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(20, PermanentThoriumBuffUI.InsectRepellentButton);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00080A8D File Offset: 0x0007EC8D
		private void SkeletonRepellentClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(21, PermanentThoriumBuffUI.SkeletonRepellentButton);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00080A9B File Offset: 0x0007EC9B
		private void ZombieRepellentClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(22, PermanentThoriumBuffUI.ZombieRepellentButton);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00080AA9 File Offset: 0x0007ECA9
		private void AltarClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(23, PermanentThoriumBuffUI.AltarButton);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00080AB7 File Offset: 0x0007ECB7
		private void ConductorsStandClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(24, PermanentThoriumBuffUI.ConductorsStandButton);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00080AC5 File Offset: 0x0007ECC5
		private void NinjaRackClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(25, PermanentThoriumBuffUI.NinjaRackButton);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00080AD3 File Offset: 0x0007ECD3
		private void DeathsingerClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(26, PermanentThoriumBuffUI.DeathsingerButton);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00080AE1 File Offset: 0x0007ECE1
		private void InspirationRegenerationClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentThoriumBuffUI.BuffClick(27, PermanentThoriumBuffUI.InspirationRegenerationButton);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00080AF0 File Offset: 0x0007ECF0
		public static void GetThoriumBuffData()
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			PermanentThoriumBuffUI.MistletoeButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "MistletoeBuff")).DisplayName;
			PermanentThoriumBuffUI.AquaAffinityButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "AquaAffinity")).DisplayName;
			PermanentThoriumBuffUI.ArcaneButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "ArcanePotionBuff")).DisplayName;
			PermanentThoriumBuffUI.ArtilleryButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "ArtilleryBuff")).DisplayName;
			PermanentThoriumBuffUI.AssassinButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "AssassinPotionBuff")).DisplayName;
			PermanentThoriumBuffUI.BloodRushButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "BloodRush")).DisplayName;
			PermanentThoriumBuffUI.BouncingFlameButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "BouncingFlamePotionBuff")).DisplayName;
			PermanentThoriumBuffUI.CactusFruitButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "CactusFruitBuff")).DisplayName;
			PermanentThoriumBuffUI.ConflagrationButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "ConflagrationPotionBuff")).DisplayName;
			PermanentThoriumBuffUI.CreativityButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "CreativityPotionBuff")).DisplayName;
			PermanentThoriumBuffUI.EarwormButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "EarwormPotionBuff")).DisplayName;
			PermanentThoriumBuffUI.FrenzyButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "FrenzyPotionBuff")).DisplayName;
			PermanentThoriumBuffUI.GlowingButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "GlowingPotionBuff")).DisplayName;
			PermanentThoriumBuffUI.HolyButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "HolyPotionBuff")).DisplayName;
			PermanentThoriumBuffUI.HydrationButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "HydrationBuff")).DisplayName;
			PermanentThoriumBuffUI.InspirationalReachButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "InspirationReachPotionBuff")).DisplayName;
			PermanentThoriumBuffUI.KineticButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "KineticPotionBuff")).DisplayName;
			PermanentThoriumBuffUI.WarmongerButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "WarmongerBuff")).DisplayName;
			PermanentThoriumBuffUI.BatRepellentButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "BatRepellentBuff")).DisplayName;
			PermanentThoriumBuffUI.FishRepellentButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "FishRepellentBuff")).DisplayName;
			PermanentThoriumBuffUI.InsectRepellentButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "InsectRepellentBuff")).DisplayName;
			PermanentThoriumBuffUI.SkeletonRepellentButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "SkeletonRepellentBuff")).DisplayName;
			PermanentThoriumBuffUI.ZombieRepellentButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "ZombieRepellentBuff")).DisplayName;
			PermanentThoriumBuffUI.AltarButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "AltarBuff")).DisplayName;
			PermanentThoriumBuffUI.ConductorsStandButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "ConductorsStandBuff")).DisplayName;
			PermanentThoriumBuffUI.NinjaRackButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "NinjaBuff")).DisplayName;
			if (ModConditions.thoriumBossReworkLoaded)
			{
				PermanentThoriumBuffUI.DeathsingerButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumBossReworkMod, "Deathsinger")).DisplayName;
				PermanentThoriumBuffUI.InspirationRegenerationButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumBossReworkMod, "Inspired")).DisplayName;
			}
			PermanentThoriumBuffUI.MistletoeButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "MistletoeBuff")), 2);
			PermanentThoriumBuffUI.AquaAffinityButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "AquaAffinity")), 2);
			PermanentThoriumBuffUI.ArcaneButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "ArcanePotionBuff")), 2);
			PermanentThoriumBuffUI.ArtilleryButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "ArtilleryBuff")), 2);
			PermanentThoriumBuffUI.AssassinButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "AssassinPotionBuff")), 2);
			PermanentThoriumBuffUI.BloodRushButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "BloodRush")), 2);
			PermanentThoriumBuffUI.BouncingFlameButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "BouncingFlamePotionBuff")), 2);
			PermanentThoriumBuffUI.CactusFruitButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "CactusFruitBuff")), 2);
			PermanentThoriumBuffUI.ConflagrationButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "ConflagrationPotionBuff")), 2);
			PermanentThoriumBuffUI.CreativityButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "CreativityPotionBuff")), 2);
			PermanentThoriumBuffUI.EarwormButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "EarwormPotionBuff")), 2);
			PermanentThoriumBuffUI.FrenzyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "FrenzyPotionBuff")), 2);
			PermanentThoriumBuffUI.GlowingButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "GlowingPotionBuff")), 2);
			PermanentThoriumBuffUI.HolyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "HolyPotionBuff")), 2);
			PermanentThoriumBuffUI.HydrationButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "HydrationBuff")), 2);
			PermanentThoriumBuffUI.InspirationalReachButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "InspirationReachPotionBuff")), 2);
			PermanentThoriumBuffUI.KineticButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "KineticPotionBuff")), 2);
			PermanentThoriumBuffUI.WarmongerButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "WarmongerBuff")), 2);
			PermanentThoriumBuffUI.BatRepellentButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "BatRepellentBuff")), 2);
			PermanentThoriumBuffUI.FishRepellentButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "FishRepellentBuff")), 2);
			PermanentThoriumBuffUI.InsectRepellentButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "InsectRepellentBuff")), 2);
			PermanentThoriumBuffUI.SkeletonRepellentButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "SkeletonRepellentBuff")), 2);
			PermanentThoriumBuffUI.ZombieRepellentButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "ZombieRepellentBuff")), 2);
			PermanentThoriumBuffUI.AltarButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "AltarBuff")), 2);
			PermanentThoriumBuffUI.ConductorsStandButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "ConductorsStandBuff")), 2);
			PermanentThoriumBuffUI.NinjaRackButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "NinjaBuff")), 2);
			if (ModConditions.thoriumBossReworkLoaded)
			{
				PermanentThoriumBuffUI.DeathsingerButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumBossReworkMod, Common.GetModBuff(ModConditions.thoriumBossReworkMod, "Deathsinger")), 2);
				PermanentThoriumBuffUI.InspirationRegenerationButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumBossReworkMod, Common.GetModBuff(ModConditions.thoriumBossReworkMod, "Inspired")), 2);
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00081364 File Offset: 0x0007F564
		public override void Update(GameTime gameTime)
		{
			if (!PermanentThoriumBuffUI.visible)
			{
				return;
			}
			for (int i = 0; i < PermanentBuffPlayer.PermanentThoriumBuffsBools.Length; i++)
			{
				this.allBuffButtons.ElementAt(i).disabled = PermanentBuffPlayer.PermanentThoriumBuffsBools[i];
				this.allBuffButtons.ElementAt(i).moddedBuff = true;
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x000813B5 File Offset: 0x0007F5B5
		public static void BuffClick(int buff, PermanentBuffButton button)
		{
			if (Main.GameUpdateCount - PermanentThoriumBuffUI.timeStart >= 10U)
			{
				PermanentBuffPlayer.PermanentThoriumBuffsBools[buff] = !PermanentBuffPlayer.PermanentThoriumBuffsBools[buff];
				button.disabled = PermanentBuffPlayer.PermanentThoriumBuffsBools[buff];
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x000813E4 File Offset: 0x0007F5E4
		private static void CreateBuffButton(PermanentBuffButton button, float left, float top)
		{
			button.Left.Set(left, 0f);
			button.Top.Set(top, 0f);
			button.Width.Set(32f, 0f);
			button.Height.Set(32f, 0f);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00081440 File Offset: 0x0007F640
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			this.offset = new Vector2(evt.MousePosition.X - this.BuffPanel.Left.Pixels, evt.MousePosition.Y - this.BuffPanel.Top.Pixels);
			this.dragging = true;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00081498 File Offset: 0x0007F698
		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			this.dragging = false;
			this.BuffPanel.Left.Set(end.X - this.offset.X, 0f);
			this.BuffPanel.Top.Set(end.Y - this.offset.Y, 0f);
			this.Recalculate();
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00081508 File Offset: 0x0007F708
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

		// Token: 0x040006EF RID: 1775
		public UIPanel BuffPanel;

		// Token: 0x040006F0 RID: 1776
		public static bool visible = false;

		// Token: 0x040006F1 RID: 1777
		public static uint timeStart;

		// Token: 0x040006F2 RID: 1778
		public static PermanentBuffButton MistletoeButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006F3 RID: 1779
		public static PermanentBuffButton AquaAffinityButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006F4 RID: 1780
		public static PermanentBuffButton ArcaneButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006F5 RID: 1781
		public static PermanentBuffButton ArtilleryButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006F6 RID: 1782
		public static PermanentBuffButton AssassinButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006F7 RID: 1783
		public static PermanentBuffButton BloodRushButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006F8 RID: 1784
		public static PermanentBuffButton BouncingFlameButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006F9 RID: 1785
		public static PermanentBuffButton CactusFruitButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006FA RID: 1786
		public static PermanentBuffButton ConflagrationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006FB RID: 1787
		public static PermanentBuffButton CreativityButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006FC RID: 1788
		public static PermanentBuffButton EarwormButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006FD RID: 1789
		public static PermanentBuffButton FrenzyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006FE RID: 1790
		public static PermanentBuffButton GlowingButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x040006FF RID: 1791
		public static PermanentBuffButton HolyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x04000700 RID: 1792
		public static PermanentBuffButton HydrationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x04000701 RID: 1793
		public static PermanentBuffButton InspirationalReachButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x04000702 RID: 1794
		public static PermanentBuffButton KineticButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x04000703 RID: 1795
		public static PermanentBuffButton WarmongerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x04000704 RID: 1796
		public static PermanentBuffButton BatRepellentButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x04000705 RID: 1797
		public static PermanentBuffButton FishRepellentButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x04000706 RID: 1798
		public static PermanentBuffButton InsectRepellentButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x04000707 RID: 1799
		public static PermanentBuffButton SkeletonRepellentButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x04000708 RID: 1800
		public static PermanentBuffButton ZombieRepellentButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x04000709 RID: 1801
		public static PermanentBuffButton AltarButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x0400070A RID: 1802
		public static PermanentBuffButton ConductorsStandButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x0400070B RID: 1803
		public static PermanentBuffButton NinjaRackButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x0400070C RID: 1804
		public static PermanentBuffButton DeathsingerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x0400070D RID: 1805
		public static PermanentBuffButton InspirationRegenerationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2));

		// Token: 0x0400070E RID: 1806
		private HashSet<PermanentBuffButton> allBuffButtons = new HashSet<PermanentBuffButton>();

		// Token: 0x0400070F RID: 1807
		private Vector2 offset;

		// Token: 0x04000710 RID: 1808
		public bool dragging;
	}
}
