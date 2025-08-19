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
	// Token: 0x02000274 RID: 628
	public class PermanentBuffUI : UIState
	{
		// Token: 0x06000F7E RID: 3966 RVA: 0x00078AB8 File Offset: 0x00076CB8
		public override void OnInitialize()
		{
			this.BuffPanel = new UIPanel();
			this.BuffPanel.SetPadding(0f);
			this.BuffPanel.Left.Set(575f, 0f);
			this.BuffPanel.Top.Set(275f, 0f);
			this.BuffPanel.Width.Set(352f, 0f);
			this.BuffPanel.Height.Set(336f, 0f);
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
			UIText stationText = new UIText(UISystem.StationText, 1f, false);
			stationText.Left.Set(16f, 0f);
			stationText.Top.Set(264f, 0f);
			stationText.Width.Set(64f, 0f);
			stationText.Height.Set(32f, 0f);
			this.BuffPanel.Append(stationText);
			PermanentBuffUI.CreateBuffButton(this.BastStatueButton, 16f, 32f);
			this.BastStatueButton.OnLeftClick += this.BastStatueClicked;
			this.BastStatueButton.Tooltip = Lang.GetBuffName(215);
			this.BuffPanel.Append(this.BastStatueButton);
			PermanentBuffUI.CreateBuffButton(this.CampfireButton, 48f, 32f);
			this.CampfireButton.OnLeftClick += this.CampfireClicked;
			this.CampfireButton.Tooltip = Lang.GetBuffName(87);
			this.BuffPanel.Append(this.CampfireButton);
			PermanentBuffUI.CreateBuffButton(this.GardenGnomeButton, 80f, 32f);
			this.GardenGnomeButton.OnLeftClick += this.GardenGnomeClicked;
			this.GardenGnomeButton.Tooltip = Lang.GetItemName(4609).ToString();
			this.BuffPanel.Append(this.GardenGnomeButton);
			PermanentBuffUI.CreateBuffButton(this.HeartLanternButton, 112f, 32f);
			this.HeartLanternButton.OnLeftClick += this.HeartLanternClicked;
			this.HeartLanternButton.Tooltip = Lang.GetBuffName(89);
			this.BuffPanel.Append(this.HeartLanternButton);
			PermanentBuffUI.CreateBuffButton(this.HoneyButton, 144f, 32f);
			this.HoneyButton.OnLeftClick += this.HoneyClicked;
			this.HoneyButton.Tooltip = Lang.GetBuffName(48);
			this.BuffPanel.Append(this.HoneyButton);
			PermanentBuffUI.CreateBuffButton(this.PeaceCandleButton, 176f, 32f);
			this.PeaceCandleButton.OnLeftClick += this.PeaceCandleClicked;
			this.PeaceCandleButton.Tooltip = Lang.GetBuffName(157);
			this.BuffPanel.Append(this.PeaceCandleButton);
			PermanentBuffUI.CreateBuffButton(this.ShadowCandleButton, 208f, 32f);
			this.ShadowCandleButton.OnLeftClick += this.ShadowCandleClicked;
			this.ShadowCandleButton.Tooltip = Lang.GetBuffName(350);
			this.BuffPanel.Append(this.ShadowCandleButton);
			PermanentBuffUI.CreateBuffButton(this.StarInABottleButton, 240f, 32f);
			this.StarInABottleButton.OnLeftClick += this.StarInABottleClicked;
			this.StarInABottleButton.Tooltip = Lang.GetBuffName(158);
			this.BuffPanel.Append(this.StarInABottleButton);
			PermanentBuffUI.CreateBuffButton(this.SunflowerButton, 272f, 32f);
			this.SunflowerButton.OnLeftClick += this.SunflowerClicked;
			this.SunflowerButton.Tooltip = Lang.GetBuffName(146);
			this.BuffPanel.Append(this.SunflowerButton);
			PermanentBuffUI.CreateBuffButton(this.WaterCandleButton, 304f, 32f);
			this.WaterCandleButton.OnLeftClick += this.WaterCandleClicked;
			this.WaterCandleButton.Tooltip = Lang.GetBuffName(86);
			this.BuffPanel.Append(this.WaterCandleButton);
			PermanentBuffUI.CreateBuffButton(this.AmmoReservationButton, 16f, 96f);
			this.AmmoReservationButton.OnLeftClick += this.AmmoReservationClicked;
			this.AmmoReservationButton.Tooltip = Lang.GetBuffName(112);
			this.BuffPanel.Append(this.AmmoReservationButton);
			PermanentBuffUI.CreateBuffButton(this.ArcheryButton, 48f, 96f);
			this.ArcheryButton.OnLeftClick += this.ArcheryClicked;
			this.ArcheryButton.Tooltip = Lang.GetBuffName(16);
			this.BuffPanel.Append(this.ArcheryButton);
			PermanentBuffUI.CreateBuffButton(this.BattleButton, 80f, 96f);
			this.BattleButton.OnLeftClick += this.BattleClicked;
			this.BattleButton.Tooltip = Lang.GetBuffName(13);
			this.BuffPanel.Append(this.BattleButton);
			PermanentBuffUI.CreateBuffButton(this.BiomeSightButton, 112f, 96f);
			this.BiomeSightButton.OnLeftClick += this.BiomeSightClicked;
			this.BiomeSightButton.Tooltip = Lang.GetBuffName(343);
			this.BuffPanel.Append(this.BiomeSightButton);
			PermanentBuffUI.CreateBuffButton(this.BuilderButton, 144f, 96f);
			this.BuilderButton.OnLeftClick += this.BuilderClicked;
			this.BuilderButton.Tooltip = Lang.GetBuffName(107);
			this.BuffPanel.Append(this.BuilderButton);
			PermanentBuffUI.CreateBuffButton(this.CalmButton, 176f, 96f);
			this.CalmButton.OnLeftClick += this.CalmClicked;
			this.CalmButton.Tooltip = Lang.GetBuffName(106);
			this.BuffPanel.Append(this.CalmButton);
			PermanentBuffUI.CreateBuffButton(this.CrateButton, 208f, 96f);
			this.CrateButton.OnLeftClick += this.CrateClicked;
			this.CrateButton.Tooltip = Lang.GetBuffName(123);
			this.BuffPanel.Append(this.CrateButton);
			PermanentBuffUI.CreateBuffButton(this.DangersenseButton, 240f, 96f);
			this.DangersenseButton.OnLeftClick += this.DangersenseClicked;
			this.DangersenseButton.Tooltip = Lang.GetBuffName(111);
			this.BuffPanel.Append(this.DangersenseButton);
			PermanentBuffUI.CreateBuffButton(this.EnduranceButton, 272f, 96f);
			this.EnduranceButton.OnLeftClick += this.EnduranceClicked;
			this.EnduranceButton.Tooltip = Lang.GetBuffName(114);
			this.BuffPanel.Append(this.EnduranceButton);
			PermanentBuffUI.CreateBuffButton(this.ExquisitelyStuffedButton, 304f, 96f);
			this.ExquisitelyStuffedButton.OnLeftClick += this.ExquisitelyStuffedClicked;
			this.ExquisitelyStuffedButton.Tooltip = Lang.GetBuffName(207);
			this.BuffPanel.Append(this.ExquisitelyStuffedButton);
			PermanentBuffUI.CreateBuffButton(this.FeatherfallButton, 16f, 128f);
			this.FeatherfallButton.OnLeftClick += this.FeatherfallClicked;
			this.FeatherfallButton.Tooltip = Lang.GetBuffName(8);
			this.BuffPanel.Append(this.FeatherfallButton);
			PermanentBuffUI.CreateBuffButton(this.FishingButton, 48f, 128f);
			this.FishingButton.OnLeftClick += this.FishingClicked;
			this.FishingButton.Tooltip = Lang.GetBuffName(121);
			this.BuffPanel.Append(this.FishingButton);
			PermanentBuffUI.CreateBuffButton(this.FlipperButton, 80f, 128f);
			this.FlipperButton.OnLeftClick += this.FlipperClicked;
			this.FlipperButton.Tooltip = Lang.GetBuffName(109);
			this.BuffPanel.Append(this.FlipperButton);
			PermanentBuffUI.CreateBuffButton(this.GillsButton, 112f, 128f);
			this.GillsButton.OnLeftClick += this.GillsClicked;
			this.GillsButton.Tooltip = Lang.GetBuffName(4);
			this.BuffPanel.Append(this.GillsButton);
			PermanentBuffUI.CreateBuffButton(this.GravitationButton, 144f, 128f);
			this.GravitationButton.OnLeftClick += this.GravitationClicked;
			this.GravitationButton.Tooltip = Lang.GetBuffName(18);
			this.BuffPanel.Append(this.GravitationButton);
			PermanentBuffUI.CreateBuffButton(this.HeartreachButton, 176f, 128f);
			this.HeartreachButton.OnLeftClick += this.HeartreachClicked;
			this.HeartreachButton.Tooltip = Lang.GetBuffName(105);
			this.BuffPanel.Append(this.HeartreachButton);
			PermanentBuffUI.CreateBuffButton(this.HunterButton, 208f, 128f);
			this.HunterButton.OnLeftClick += this.HunterClicked;
			this.HunterButton.Tooltip = Lang.GetBuffName(17);
			this.BuffPanel.Append(this.HunterButton);
			PermanentBuffUI.CreateBuffButton(this.InfernoButton, 240f, 128f);
			this.InfernoButton.OnLeftClick += this.InfernoClicked;
			this.InfernoButton.Tooltip = Lang.GetBuffName(116);
			this.BuffPanel.Append(this.InfernoButton);
			PermanentBuffUI.CreateBuffButton(this.InvisibilityButton, 272f, 128f);
			this.InvisibilityButton.OnLeftClick += this.InvisibilityClicked;
			this.InvisibilityButton.Tooltip = Lang.GetBuffName(10);
			this.BuffPanel.Append(this.InvisibilityButton);
			PermanentBuffUI.CreateBuffButton(this.IronskinButton, 304f, 128f);
			this.IronskinButton.OnLeftClick += this.IronskinClicked;
			this.IronskinButton.Tooltip = Lang.GetBuffName(5);
			this.BuffPanel.Append(this.IronskinButton);
			PermanentBuffUI.CreateBuffButton(this.LifeforceButton, 16f, 160f);
			this.LifeforceButton.OnLeftClick += this.LifeforceClicked;
			this.LifeforceButton.Tooltip = Lang.GetBuffName(113);
			this.BuffPanel.Append(this.LifeforceButton);
			PermanentBuffUI.CreateBuffButton(this.LuckyButton, 48f, 160f);
			this.LuckyButton.OnLeftClick += this.LuckyClicked;
			this.LuckyButton.Tooltip = Lang.GetBuffName(257);
			this.BuffPanel.Append(this.LuckyButton);
			PermanentBuffUI.CreateBuffButton(this.MagicPowerButton, 80f, 160f);
			this.MagicPowerButton.OnLeftClick += this.MagicPowerClicked;
			this.MagicPowerButton.Tooltip = Lang.GetBuffName(7);
			this.BuffPanel.Append(this.MagicPowerButton);
			PermanentBuffUI.CreateBuffButton(this.ManaRegenerationButton, 112f, 160f);
			this.ManaRegenerationButton.OnLeftClick += this.ManaRegenerationClicked;
			this.ManaRegenerationButton.Tooltip = Lang.GetBuffName(6);
			this.BuffPanel.Append(this.ManaRegenerationButton);
			PermanentBuffUI.CreateBuffButton(this.MiningButton, 144f, 160f);
			this.MiningButton.OnLeftClick += this.MiningClicked;
			this.MiningButton.Tooltip = Lang.GetBuffName(104);
			this.BuffPanel.Append(this.MiningButton);
			PermanentBuffUI.CreateBuffButton(this.NightOwlButton, 176f, 160f);
			this.NightOwlButton.OnLeftClick += this.NightOwlClicked;
			this.NightOwlButton.Tooltip = Lang.GetBuffName(12);
			this.BuffPanel.Append(this.NightOwlButton);
			PermanentBuffUI.CreateBuffButton(this.ObsidianSkinButton, 208f, 160f);
			this.ObsidianSkinButton.OnLeftClick += this.ObsidianSkinClicked;
			this.ObsidianSkinButton.Tooltip = Lang.GetBuffName(1);
			this.BuffPanel.Append(this.ObsidianSkinButton);
			PermanentBuffUI.CreateBuffButton(this.PlentySatisfiedButton, 240f, 160f);
			this.PlentySatisfiedButton.OnLeftClick += this.PlentySatisfiedClicked;
			this.PlentySatisfiedButton.Tooltip = Lang.GetBuffName(206);
			this.BuffPanel.Append(this.PlentySatisfiedButton);
			PermanentBuffUI.CreateBuffButton(this.RageButton, 272f, 160f);
			this.RageButton.OnLeftClick += this.RageClicked;
			this.RageButton.Tooltip = Lang.GetBuffName(115);
			this.BuffPanel.Append(this.RageButton);
			PermanentBuffUI.CreateBuffButton(this.RegenerationButton, 304f, 160f);
			this.RegenerationButton.OnLeftClick += this.RegenerationClicked;
			this.RegenerationButton.Tooltip = Lang.GetBuffName(2);
			this.BuffPanel.Append(this.RegenerationButton);
			PermanentBuffUI.CreateBuffButton(this.ShineButton, 16f, 192f);
			this.ShineButton.OnLeftClick += this.ShineClicked;
			this.ShineButton.Tooltip = Lang.GetBuffName(11);
			this.BuffPanel.Append(this.ShineButton);
			PermanentBuffUI.CreateBuffButton(this.SonarButton, 48f, 192f);
			this.SonarButton.OnLeftClick += this.SonarClicked;
			this.SonarButton.Tooltip = Lang.GetBuffName(122);
			this.BuffPanel.Append(this.SonarButton);
			PermanentBuffUI.CreateBuffButton(this.SpelunkerButton, 80f, 192f);
			this.SpelunkerButton.OnLeftClick += this.SpelunkerClicked;
			this.SpelunkerButton.Tooltip = Lang.GetBuffName(9);
			this.BuffPanel.Append(this.SpelunkerButton);
			PermanentBuffUI.CreateBuffButton(this.SummoningButton, 112f, 192f);
			this.SummoningButton.OnLeftClick += this.SummoningClicked;
			this.SummoningButton.Tooltip = Lang.GetBuffName(110);
			this.BuffPanel.Append(this.SummoningButton);
			PermanentBuffUI.CreateBuffButton(this.SwiftnessButton, 144f, 192f);
			this.SwiftnessButton.OnLeftClick += this.SwiftnessClicked;
			this.SwiftnessButton.Tooltip = Lang.GetBuffName(3);
			this.BuffPanel.Append(this.SwiftnessButton);
			PermanentBuffUI.CreateBuffButton(this.ThornsButton, 176f, 192f);
			this.ThornsButton.OnLeftClick += this.ThornsClicked;
			this.ThornsButton.Tooltip = Lang.GetBuffName(14);
			this.BuffPanel.Append(this.ThornsButton);
			PermanentBuffUI.CreateBuffButton(this.TipsyButton, 208f, 192f);
			this.TipsyButton.OnLeftClick += this.TipsyClicked;
			this.TipsyButton.Tooltip = Lang.GetBuffName(25);
			this.BuffPanel.Append(this.TipsyButton);
			PermanentBuffUI.CreateBuffButton(this.TitanButton, 240f, 192f);
			this.TitanButton.OnLeftClick += this.TitanClicked;
			this.TitanButton.Tooltip = Lang.GetBuffName(108);
			this.BuffPanel.Append(this.TitanButton);
			PermanentBuffUI.CreateBuffButton(this.WarmthButton, 272f, 192f);
			this.WarmthButton.OnLeftClick += this.WarmthClicked;
			this.WarmthButton.Tooltip = Lang.GetBuffName(124);
			this.BuffPanel.Append(this.WarmthButton);
			PermanentBuffUI.CreateBuffButton(this.WaterWalkingButton, 304f, 192f);
			this.WaterWalkingButton.OnLeftClick += this.WaterWalkingClicked;
			this.WaterWalkingButton.Tooltip = Lang.GetBuffName(15);
			this.BuffPanel.Append(this.WaterWalkingButton);
			PermanentBuffUI.CreateBuffButton(this.WellFedButton, 16f, 224f);
			this.WellFedButton.OnLeftClick += this.WellFedClicked;
			this.WellFedButton.Tooltip = Lang.GetBuffName(26);
			this.BuffPanel.Append(this.WellFedButton);
			PermanentBuffUI.CreateBuffButton(this.WrathButton, 48f, 224f);
			this.WrathButton.OnLeftClick += this.WrathClicked;
			this.WrathButton.Tooltip = Lang.GetBuffName(117);
			this.BuffPanel.Append(this.WrathButton);
			PermanentBuffUI.CreateBuffButton(this.AmmoBoxButton, 16f, 288f);
			this.AmmoBoxButton.OnLeftClick += this.AmmoBoxClicked;
			this.AmmoBoxButton.Tooltip = Lang.GetBuffName(93);
			this.BuffPanel.Append(this.AmmoBoxButton);
			PermanentBuffUI.CreateBuffButton(this.BewitchingTableButton, 48f, 288f);
			this.BewitchingTableButton.OnLeftClick += this.BewitchingTableClicked;
			this.BewitchingTableButton.Tooltip = Lang.GetBuffName(150);
			this.BuffPanel.Append(this.BewitchingTableButton);
			PermanentBuffUI.CreateBuffButton(this.CrystalBallButton, 80f, 288f);
			this.CrystalBallButton.OnLeftClick += this.CrystalBallClicked;
			this.CrystalBallButton.Tooltip = Lang.GetBuffName(29);
			this.BuffPanel.Append(this.CrystalBallButton);
			PermanentBuffUI.CreateBuffButton(this.SharpeningStationButton, 112f, 288f);
			this.SharpeningStationButton.OnLeftClick += this.SharpeningStationClicked;
			this.SharpeningStationButton.Tooltip = Lang.GetBuffName(159);
			this.BuffPanel.Append(this.SharpeningStationButton);
			PermanentBuffUI.CreateBuffButton(this.SliceOfCakeButton, 144f, 288f);
			this.SliceOfCakeButton.OnLeftClick += this.SliceOfCakeClicked;
			this.SliceOfCakeButton.Tooltip = Lang.GetBuffName(192);
			this.BuffPanel.Append(this.SliceOfCakeButton);
			PermanentBuffUI.CreateBuffButton(this.WarTableButton, 176f, 288f);
			this.WarTableButton.OnLeftClick += this.WarTableClicked;
			this.WarTableButton.Tooltip = Lang.GetBuffName(348);
			this.BuffPanel.Append(this.WarTableButton);
			HashSet<PermanentBuffButton> hashSet = new HashSet<PermanentBuffButton>();
			hashSet.Add(this.BastStatueButton);
			hashSet.Add(this.CampfireButton);
			hashSet.Add(this.GardenGnomeButton);
			hashSet.Add(this.HeartLanternButton);
			hashSet.Add(this.HoneyButton);
			hashSet.Add(this.PeaceCandleButton);
			hashSet.Add(this.ShadowCandleButton);
			hashSet.Add(this.StarInABottleButton);
			hashSet.Add(this.SunflowerButton);
			hashSet.Add(this.WaterCandleButton);
			hashSet.Add(this.AmmoReservationButton);
			hashSet.Add(this.ArcheryButton);
			hashSet.Add(this.BattleButton);
			hashSet.Add(this.BiomeSightButton);
			hashSet.Add(this.BuilderButton);
			hashSet.Add(this.CalmButton);
			hashSet.Add(this.CrateButton);
			hashSet.Add(this.DangersenseButton);
			hashSet.Add(this.EnduranceButton);
			hashSet.Add(this.ExquisitelyStuffedButton);
			hashSet.Add(this.FeatherfallButton);
			hashSet.Add(this.FishingButton);
			hashSet.Add(this.FlipperButton);
			hashSet.Add(this.GillsButton);
			hashSet.Add(this.GravitationButton);
			hashSet.Add(this.HeartreachButton);
			hashSet.Add(this.HunterButton);
			hashSet.Add(this.InfernoButton);
			hashSet.Add(this.InvisibilityButton);
			hashSet.Add(this.IronskinButton);
			hashSet.Add(this.LifeforceButton);
			hashSet.Add(this.LuckyButton);
			hashSet.Add(this.MagicPowerButton);
			hashSet.Add(this.ManaRegenerationButton);
			hashSet.Add(this.MiningButton);
			hashSet.Add(this.NightOwlButton);
			hashSet.Add(this.ObsidianSkinButton);
			hashSet.Add(this.PlentySatisfiedButton);
			hashSet.Add(this.RageButton);
			hashSet.Add(this.RegenerationButton);
			hashSet.Add(this.ShineButton);
			hashSet.Add(this.SonarButton);
			hashSet.Add(this.SpelunkerButton);
			hashSet.Add(this.SummoningButton);
			hashSet.Add(this.SwiftnessButton);
			hashSet.Add(this.ThornsButton);
			hashSet.Add(this.TipsyButton);
			hashSet.Add(this.TitanButton);
			hashSet.Add(this.WarmthButton);
			hashSet.Add(this.WaterWalkingButton);
			hashSet.Add(this.WellFedButton);
			hashSet.Add(this.WrathButton);
			hashSet.Add(this.AmmoBoxButton);
			hashSet.Add(this.BewitchingTableButton);
			hashSet.Add(this.CrystalBallButton);
			hashSet.Add(this.SharpeningStationButton);
			hashSet.Add(this.SliceOfCakeButton);
			hashSet.Add(this.WarTableButton);
			HashSet<PermanentBuffButton> buttons = hashSet;
			this.allBuffButtons.UnionWith(buttons);
			base.Append(this.BuffPanel);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0007A1FF File Offset: 0x000783FF
		private void BastStatueClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(0, this.BastStatueButton, Lang.GetBuffName(215));
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0007A217 File Offset: 0x00078417
		private void CampfireClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(1, this.CampfireButton, Lang.GetBuffName(87));
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0007A22C File Offset: 0x0007842C
		private void GardenGnomeClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(2, this.GardenGnomeButton, Lang.GetItemName(4609).ToString());
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0007A249 File Offset: 0x00078449
		private void HeartLanternClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(3, this.HeartLanternButton, Lang.GetBuffName(89));
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0007A25E File Offset: 0x0007845E
		private void HoneyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(4, this.HoneyButton, Lang.GetBuffName(48));
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0007A273 File Offset: 0x00078473
		private void PeaceCandleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(5, this.PeaceCandleButton, Lang.GetBuffName(157));
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0007A28B File Offset: 0x0007848B
		private void ShadowCandleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(6, this.ShadowCandleButton, Lang.GetBuffName(350));
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0007A2A3 File Offset: 0x000784A3
		private void StarInABottleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(7, this.StarInABottleButton, Lang.GetBuffName(158));
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0007A2BB File Offset: 0x000784BB
		private void SunflowerClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(8, this.SunflowerButton, Lang.GetBuffName(146));
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0007A2D3 File Offset: 0x000784D3
		private void WaterCandleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(9, this.WaterCandleButton, Lang.GetBuffName(86));
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0007A2E9 File Offset: 0x000784E9
		private void AmmoReservationClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(10, this.AmmoReservationButton, Lang.GetBuffName(112));
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0007A2FF File Offset: 0x000784FF
		private void ArcheryClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(11, this.ArcheryButton, Lang.GetBuffName(16));
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0007A315 File Offset: 0x00078515
		private void BattleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(12, this.BattleButton, Lang.GetBuffName(13));
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0007A32B File Offset: 0x0007852B
		private void BiomeSightClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(13, this.BiomeSightButton, Lang.GetBuffName(343));
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0007A344 File Offset: 0x00078544
		private void BuilderClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(14, this.BuilderButton, Lang.GetBuffName(107));
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0007A35A File Offset: 0x0007855A
		private void CalmClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(15, this.CalmButton, Lang.GetBuffName(106));
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0007A370 File Offset: 0x00078570
		private void CrateClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(16, this.CrateButton, Lang.GetBuffName(123));
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0007A386 File Offset: 0x00078586
		private void DangersenseClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(17, this.DangersenseButton, Lang.GetBuffName(111));
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0007A39C File Offset: 0x0007859C
		private void EnduranceClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(18, this.EnduranceButton, Lang.GetBuffName(114));
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0007A3B2 File Offset: 0x000785B2
		private void ExquisitelyStuffedClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(19, this.ExquisitelyStuffedButton, Lang.GetBuffName(207));
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0007A3CB File Offset: 0x000785CB
		private void FeatherfallClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(20, this.FeatherfallButton, Lang.GetBuffName(8));
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0007A3E0 File Offset: 0x000785E0
		private void FishingClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(21, this.FishingButton, Lang.GetBuffName(121));
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0007A3F6 File Offset: 0x000785F6
		private void FlipperClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(22, this.FlipperButton, Lang.GetBuffName(109));
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0007A40C File Offset: 0x0007860C
		private void GillsClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(23, this.GillsButton, Lang.GetBuffName(4));
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0007A421 File Offset: 0x00078621
		private void GravitationClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(24, this.GravitationButton, Lang.GetBuffName(18));
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0007A437 File Offset: 0x00078637
		private void HeartreachClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(25, this.HeartreachButton, Lang.GetBuffName(105));
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0007A44D File Offset: 0x0007864D
		private void HunterClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(26, this.HunterButton, Lang.GetBuffName(17));
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0007A463 File Offset: 0x00078663
		private void InfernoClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(27, this.InfernoButton, Lang.GetBuffName(116));
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0007A479 File Offset: 0x00078679
		private void InvisibilityClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(28, this.InvisibilityButton, Lang.GetBuffName(10));
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0007A48F File Offset: 0x0007868F
		private void IronskinClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(29, this.IronskinButton, Lang.GetBuffName(5));
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0007A4A4 File Offset: 0x000786A4
		private void LifeforceClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(30, this.LifeforceButton, Lang.GetBuffName(113));
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0007A4BA File Offset: 0x000786BA
		private void LuckyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(31, this.LuckyButton, Lang.GetBuffName(257));
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0007A4D3 File Offset: 0x000786D3
		private void MagicPowerClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(32, this.MagicPowerButton, Lang.GetBuffName(7));
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0007A4E8 File Offset: 0x000786E8
		private void ManaRegenerationClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(33, this.ManaRegenerationButton, Lang.GetBuffName(6));
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0007A4FD File Offset: 0x000786FD
		private void MiningClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(34, this.MiningButton, Lang.GetBuffName(104));
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0007A513 File Offset: 0x00078713
		private void NightOwlClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(35, this.NightOwlButton, Lang.GetBuffName(12));
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0007A529 File Offset: 0x00078729
		private void ObsidianSkinClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(36, this.ObsidianSkinButton, Lang.GetBuffName(1));
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0007A53E File Offset: 0x0007873E
		private void PlentySatisfiedClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(37, this.PlentySatisfiedButton, Lang.GetBuffName(206));
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0007A557 File Offset: 0x00078757
		private void RageClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(38, this.RageButton, Lang.GetBuffName(115));
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0007A56D File Offset: 0x0007876D
		private void RegenerationClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(39, this.RegenerationButton, Lang.GetBuffName(2));
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0007A582 File Offset: 0x00078782
		private void ShineClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(40, this.ShineButton, Lang.GetBuffName(11));
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0007A598 File Offset: 0x00078798
		private void SonarClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(41, this.SonarButton, Lang.GetBuffName(122));
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0007A5AE File Offset: 0x000787AE
		private void SpelunkerClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(42, this.SpelunkerButton, Lang.GetBuffName(9));
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0007A5C4 File Offset: 0x000787C4
		private void SummoningClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(43, this.SummoningButton, Lang.GetBuffName(110));
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0007A5DA File Offset: 0x000787DA
		private void SwiftnessClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(44, this.SwiftnessButton, Lang.GetBuffName(3));
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0007A5EF File Offset: 0x000787EF
		private void ThornsClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(45, this.ThornsButton, Lang.GetBuffName(14));
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0007A605 File Offset: 0x00078805
		private void TipsyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(46, this.TipsyButton, Lang.GetBuffName(25));
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0007A61B File Offset: 0x0007881B
		private void TitanClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(47, this.TitanButton, Lang.GetBuffName(108));
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0007A631 File Offset: 0x00078831
		private void WarmthClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(48, this.WarmthButton, Lang.GetBuffName(124));
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0007A647 File Offset: 0x00078847
		private void WaterWalkingClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(49, this.WaterWalkingButton, Lang.GetBuffName(15));
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0007A65D File Offset: 0x0007885D
		private void WellFedClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(50, this.WellFedButton, Lang.GetBuffName(26));
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0007A673 File Offset: 0x00078873
		private void WrathClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(51, this.WrathButton, Lang.GetBuffName(117));
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0007A689 File Offset: 0x00078889
		private void AmmoBoxClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(52, this.AmmoBoxButton, Lang.GetBuffName(93));
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0007A69F File Offset: 0x0007889F
		private void BewitchingTableClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(53, this.BewitchingTableButton, Lang.GetBuffName(150));
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0007A6B8 File Offset: 0x000788B8
		private void CrystalBallClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(54, this.CrystalBallButton, Lang.GetBuffName(29));
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0007A6CE File Offset: 0x000788CE
		private void SharpeningStationClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(55, this.SharpeningStationButton, Lang.GetBuffName(159));
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0007A6E7 File Offset: 0x000788E7
		private void SliceOfCakeClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(56, this.SliceOfCakeButton, Lang.GetBuffName(192));
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0007A700 File Offset: 0x00078900
		private void WarTableClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentBuffUI.BuffClick(57, this.WarTableButton, Lang.GetBuffName(348));
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0007A71C File Offset: 0x0007891C
		public override void Update(GameTime gameTime)
		{
			if (!PermanentBuffUI.visible)
			{
				return;
			}
			for (int i = 0; i < PermanentBuffPlayer.PermanentBuffsBools.Length; i++)
			{
				this.allBuffButtons.ElementAt(i).disabled = PermanentBuffPlayer.PermanentBuffsBools[i];
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0007A75B File Offset: 0x0007895B
		public static void BuffClick(int buff, PermanentBuffButton button, string name)
		{
			if (Main.GameUpdateCount - PermanentBuffUI.timeStart >= 10U)
			{
				PermanentBuffPlayer.PermanentBuffsBools[buff] = !PermanentBuffPlayer.PermanentBuffsBools[buff];
				button.disabled = PermanentBuffPlayer.PermanentBuffsBools[buff];
			}
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0007A78C File Offset: 0x0007898C
		private static void CreateBuffButton(PermanentBuffButton button, float left, float top)
		{
			button.Left.Set(left, 0f);
			button.Top.Set(top, 0f);
			button.Width.Set(32f, 0f);
			button.Height.Set(32f, 0f);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0007A7E8 File Offset: 0x000789E8
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			this.offset = new Vector2(evt.MousePosition.X - this.BuffPanel.Left.Pixels, evt.MousePosition.Y - this.BuffPanel.Top.Pixels);
			this.dragging = true;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0007A840 File Offset: 0x00078A40
		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			this.dragging = false;
			this.BuffPanel.Left.Set(end.X - this.offset.X, 0f);
			this.BuffPanel.Top.Set(end.Y - this.offset.Y, 0f);
			this.Recalculate();
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0007A8B0 File Offset: 0x00078AB0
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

		// Token: 0x04000648 RID: 1608
		public UIPanel BuffPanel;

		// Token: 0x04000649 RID: 1609
		public static bool visible;

		// Token: 0x0400064A RID: 1610
		public static uint timeStart;

		// Token: 0x0400064B RID: 1611
		private PermanentBuffButton BastStatueButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 215.ToString(), 2));

		// Token: 0x0400064C RID: 1612
		private PermanentBuffButton CampfireButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 87.ToString(), 2));

		// Token: 0x0400064D RID: 1613
		private PermanentBuffButton GardenGnomeButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentGardenGnome", 2));

		// Token: 0x0400064E RID: 1614
		private PermanentBuffButton HeartLanternButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 89.ToString(), 2));

		// Token: 0x0400064F RID: 1615
		private PermanentBuffButton HoneyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 48.ToString(), 2));

		// Token: 0x04000650 RID: 1616
		private PermanentBuffButton PeaceCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 157.ToString(), 2));

		// Token: 0x04000651 RID: 1617
		private PermanentBuffButton ShadowCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 350.ToString(), 2));

		// Token: 0x04000652 RID: 1618
		private PermanentBuffButton StarInABottleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 158.ToString(), 2));

		// Token: 0x04000653 RID: 1619
		private PermanentBuffButton SunflowerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 146.ToString(), 2));

		// Token: 0x04000654 RID: 1620
		private PermanentBuffButton WaterCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 86.ToString(), 2));

		// Token: 0x04000655 RID: 1621
		private PermanentBuffButton AmmoReservationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 112.ToString(), 2));

		// Token: 0x04000656 RID: 1622
		private PermanentBuffButton ArcheryButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 16.ToString(), 2));

		// Token: 0x04000657 RID: 1623
		private PermanentBuffButton BattleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 13.ToString(), 2));

		// Token: 0x04000658 RID: 1624
		private PermanentBuffButton BiomeSightButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 343.ToString(), 2));

		// Token: 0x04000659 RID: 1625
		private PermanentBuffButton BuilderButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 107.ToString(), 2));

		// Token: 0x0400065A RID: 1626
		private PermanentBuffButton CalmButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 106.ToString(), 2));

		// Token: 0x0400065B RID: 1627
		private PermanentBuffButton CrateButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 123.ToString(), 2));

		// Token: 0x0400065C RID: 1628
		private PermanentBuffButton DangersenseButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 111.ToString(), 2));

		// Token: 0x0400065D RID: 1629
		private PermanentBuffButton EnduranceButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 114.ToString(), 2));

		// Token: 0x0400065E RID: 1630
		private PermanentBuffButton ExquisitelyStuffedButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 207.ToString(), 2));

		// Token: 0x0400065F RID: 1631
		private PermanentBuffButton FeatherfallButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 8.ToString(), 2));

		// Token: 0x04000660 RID: 1632
		private PermanentBuffButton FishingButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 121.ToString(), 2));

		// Token: 0x04000661 RID: 1633
		private PermanentBuffButton FlipperButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 109.ToString(), 2));

		// Token: 0x04000662 RID: 1634
		private PermanentBuffButton GillsButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 4.ToString(), 2));

		// Token: 0x04000663 RID: 1635
		private PermanentBuffButton GravitationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 18.ToString(), 2));

		// Token: 0x04000664 RID: 1636
		private PermanentBuffButton HeartreachButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 105.ToString(), 2));

		// Token: 0x04000665 RID: 1637
		private PermanentBuffButton HunterButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 17.ToString(), 2));

		// Token: 0x04000666 RID: 1638
		private PermanentBuffButton InfernoButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 116.ToString(), 2));

		// Token: 0x04000667 RID: 1639
		private PermanentBuffButton InvisibilityButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 10.ToString(), 2));

		// Token: 0x04000668 RID: 1640
		private PermanentBuffButton IronskinButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 5.ToString(), 2));

		// Token: 0x04000669 RID: 1641
		private PermanentBuffButton LifeforceButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 113.ToString(), 2));

		// Token: 0x0400066A RID: 1642
		private PermanentBuffButton LuckyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 257.ToString(), 2));

		// Token: 0x0400066B RID: 1643
		private PermanentBuffButton MagicPowerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 7.ToString(), 2));

		// Token: 0x0400066C RID: 1644
		private PermanentBuffButton ManaRegenerationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 6.ToString(), 2));

		// Token: 0x0400066D RID: 1645
		private PermanentBuffButton MiningButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 104.ToString(), 2));

		// Token: 0x0400066E RID: 1646
		private PermanentBuffButton NightOwlButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 12.ToString(), 2));

		// Token: 0x0400066F RID: 1647
		private PermanentBuffButton ObsidianSkinButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 1.ToString(), 2));

		// Token: 0x04000670 RID: 1648
		private PermanentBuffButton PlentySatisfiedButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 206.ToString(), 2));

		// Token: 0x04000671 RID: 1649
		private PermanentBuffButton RageButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 115.ToString(), 2));

		// Token: 0x04000672 RID: 1650
		private PermanentBuffButton RegenerationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 2.ToString(), 2));

		// Token: 0x04000673 RID: 1651
		private PermanentBuffButton ShineButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 11.ToString(), 2));

		// Token: 0x04000674 RID: 1652
		private PermanentBuffButton SonarButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 122.ToString(), 2));

		// Token: 0x04000675 RID: 1653
		private PermanentBuffButton SpelunkerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 9.ToString(), 2));

		// Token: 0x04000676 RID: 1654
		private PermanentBuffButton SummoningButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 110.ToString(), 2));

		// Token: 0x04000677 RID: 1655
		private PermanentBuffButton SwiftnessButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 3.ToString(), 2));

		// Token: 0x04000678 RID: 1656
		private PermanentBuffButton ThornsButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 14.ToString(), 2));

		// Token: 0x04000679 RID: 1657
		private PermanentBuffButton TipsyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 25.ToString(), 2));

		// Token: 0x0400067A RID: 1658
		private PermanentBuffButton TitanButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 108.ToString(), 2));

		// Token: 0x0400067B RID: 1659
		private PermanentBuffButton WarmthButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 124.ToString(), 2));

		// Token: 0x0400067C RID: 1660
		private PermanentBuffButton WaterWalkingButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 15.ToString(), 2));

		// Token: 0x0400067D RID: 1661
		private PermanentBuffButton WellFedButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 26.ToString(), 2));

		// Token: 0x0400067E RID: 1662
		private PermanentBuffButton WrathButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 117.ToString(), 2));

		// Token: 0x0400067F RID: 1663
		private PermanentBuffButton AmmoBoxButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 93.ToString(), 2));

		// Token: 0x04000680 RID: 1664
		private PermanentBuffButton BewitchingTableButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 150.ToString(), 2));

		// Token: 0x04000681 RID: 1665
		private PermanentBuffButton CrystalBallButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 29.ToString(), 2));

		// Token: 0x04000682 RID: 1666
		private PermanentBuffButton SharpeningStationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 159.ToString(), 2));

		// Token: 0x04000683 RID: 1667
		private PermanentBuffButton SliceOfCakeButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 192.ToString(), 2));

		// Token: 0x04000684 RID: 1668
		private PermanentBuffButton WarTableButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 348.ToString(), 2));

		// Token: 0x04000685 RID: 1669
		private HashSet<PermanentBuffButton> allBuffButtons = new HashSet<PermanentBuffButton>();

		// Token: 0x04000686 RID: 1670
		private Vector2 offset;

		// Token: 0x04000687 RID: 1671
		public bool dragging;
	}
}
