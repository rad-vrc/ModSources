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
	// Token: 0x02000275 RID: 629
	public class PermanentCalamityBuffUI : UIState
	{
		// Token: 0x06000FC0 RID: 4032 RVA: 0x0007B1D8 File Offset: 0x000793D8
		public override void OnInitialize()
		{
			this.BuffPanel = new UIPanel();
			this.BuffPanel.SetPadding(0f);
			this.BuffPanel.Left.Set(575f, 0f);
			this.BuffPanel.Top.Set(275f, 0f);
			this.BuffPanel.Width.Set(352f, 0f);
			this.BuffPanel.Height.Set(240f, 0f);
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
			UIText addonsText = new UIText(UISystem.AddonText, 1f, false);
			addonsText.Left.Set(16f, 0f);
			addonsText.Top.Set(168f, 0f);
			addonsText.Width.Set(64f, 0f);
			addonsText.Height.Set(32f, 0f);
			this.BuffPanel.Append(addonsText);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.ChaosCandleButton, 16f, 32f);
			PermanentCalamityBuffUI.ChaosCandleButton.OnLeftClick += this.ChaosCandleClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.ChaosCandleButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.CorruptionEffigyButton, 48f, 32f);
			PermanentCalamityBuffUI.CorruptionEffigyButton.OnLeftClick += this.CorruptionEffigyClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.CorruptionEffigyButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.CrimsonEffigyButton, 80f, 32f);
			PermanentCalamityBuffUI.CrimsonEffigyButton.OnLeftClick += this.CrimsonEffigyClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.CrimsonEffigyButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.EffigyOfDecayButton, 112f, 32f);
			PermanentCalamityBuffUI.EffigyOfDecayButton.OnLeftClick += this.EffigyOfDecayClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.EffigyOfDecayButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.ResilientCandleButton, 144f, 32f);
			PermanentCalamityBuffUI.ResilientCandleButton.OnLeftClick += this.ResilientCandleClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.ResilientCandleButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.SpitefulCandleButton, 176f, 32f);
			PermanentCalamityBuffUI.SpitefulCandleButton.OnLeftClick += this.SpitefulCandleClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.SpitefulCandleButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.TranquilityCandleButton, 208f, 32f);
			PermanentCalamityBuffUI.TranquilityCandleButton.OnLeftClick += this.TranquilityCandleClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.TranquilityCandleButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.VigorousCandleButton, 240f, 32f);
			PermanentCalamityBuffUI.VigorousCandleButton.OnLeftClick += this.VigorousCandleClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.VigorousCandleButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.WeightlessCandleButton, 272f, 32f);
			PermanentCalamityBuffUI.WeightlessCandleButton.OnLeftClick += this.WeightlessCandleClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.WeightlessCandleButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.AnechoicCoatingButton, 16f, 96f);
			PermanentCalamityBuffUI.AnechoicCoatingButton.OnLeftClick += this.AnechoicCoatingClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.AnechoicCoatingButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.AstralInjectionButton, 48f, 96f);
			PermanentCalamityBuffUI.AstralInjectionButton.OnLeftClick += this.AstralInjectionClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.AstralInjectionButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.BaguetteButton, 80f, 96f);
			PermanentCalamityBuffUI.BaguetteButton.OnLeftClick += this.BaguetteClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.BaguetteButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.BloodfinButton, 112f, 96f);
			PermanentCalamityBuffUI.BloodfinButton.OnLeftClick += this.BloodfinClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.BloodfinButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.BoundingButton, 144f, 96f);
			PermanentCalamityBuffUI.BoundingButton.OnLeftClick += this.BoundingClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.BoundingButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.CalciumButton, 176f, 96f);
			PermanentCalamityBuffUI.CalciumButton.OnLeftClick += this.CalciumClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.CalciumButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.CeaselessHungerButton, 208f, 96f);
			PermanentCalamityBuffUI.CeaselessHungerButton.OnLeftClick += this.CeaselessHungerClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.CeaselessHungerButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.GravityNormalizerButton, 240f, 96f);
			PermanentCalamityBuffUI.GravityNormalizerButton.OnLeftClick += this.GravityNormalizerClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.GravityNormalizerButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.OmniscienceButton, 272f, 96f);
			PermanentCalamityBuffUI.OmniscienceButton.OnLeftClick += this.OmniscienceClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.OmniscienceButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.PhotosynthesisButton, 304f, 96f);
			PermanentCalamityBuffUI.PhotosynthesisButton.OnLeftClick += this.PhotosynthesisClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.PhotosynthesisButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.ShadowButton, 16f, 128f);
			PermanentCalamityBuffUI.ShadowButton.OnLeftClick += this.ShadowClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.ShadowButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.SoaringButton, 48f, 128f);
			PermanentCalamityBuffUI.SoaringButton.OnLeftClick += this.SoaringClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.SoaringButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.SulphurskinButton, 80f, 128f);
			PermanentCalamityBuffUI.SulphurskinButton.OnLeftClick += this.SulphurskinClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.SulphurskinButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.TeslaButton, 112f, 128f);
			PermanentCalamityBuffUI.TeslaButton.OnLeftClick += this.TeslaClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.TeslaButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.ZenButton, 144f, 128f);
			PermanentCalamityBuffUI.ZenButton.OnLeftClick += this.ZenClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.ZenButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.ZergButton, 176f, 128f);
			PermanentCalamityBuffUI.ZergButton.OnLeftClick += this.ZergClicked;
			this.BuffPanel.Append(PermanentCalamityBuffUI.ZergButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.AstraJellyButton, 16f, 192f);
			PermanentCalamityBuffUI.AstraJellyButton.OnLeftClick += this.AstraJellyClicked;
			PermanentCalamityBuffUI.AstraJellyButton.ModTooltip = UISystem.UnloadedText;
			this.BuffPanel.Append(PermanentCalamityBuffUI.AstraJellyButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.AstracolaButton, 48f, 192f);
			PermanentCalamityBuffUI.AstracolaButton.OnLeftClick += this.AstracolaClicked;
			PermanentCalamityBuffUI.AstracolaButton.ModTooltip = UISystem.UnloadedText;
			this.BuffPanel.Append(PermanentCalamityBuffUI.AstracolaButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.ExoBaguetteButton, 80f, 192f);
			PermanentCalamityBuffUI.ExoBaguetteButton.OnLeftClick += this.ExoBaguetteClicked;
			PermanentCalamityBuffUI.ExoBaguetteButton.ModTooltip = UISystem.UnloadedText;
			this.BuffPanel.Append(PermanentCalamityBuffUI.ExoBaguetteButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.SupremeLuckButton, 112f, 192f);
			PermanentCalamityBuffUI.SupremeLuckButton.OnLeftClick += this.SupremeLuckClicked;
			PermanentCalamityBuffUI.SupremeLuckButton.ModTooltip = UISystem.UnloadedText;
			this.BuffPanel.Append(PermanentCalamityBuffUI.SupremeLuckButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.TitanScaleButton, 144f, 192f);
			PermanentCalamityBuffUI.TitanScaleButton.OnLeftClick += this.TitanScaleClicked;
			PermanentCalamityBuffUI.TitanScaleButton.ModTooltip = UISystem.UnloadedText;
			this.BuffPanel.Append(PermanentCalamityBuffUI.TitanScaleButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.VoidCandleButton, 176f, 192f);
			PermanentCalamityBuffUI.VoidCandleButton.OnLeftClick += this.VoidCandleClicked;
			PermanentCalamityBuffUI.VoidCandleButton.ModTooltip = UISystem.UnloadedText;
			this.BuffPanel.Append(PermanentCalamityBuffUI.VoidCandleButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.SoyMilkButton, 208f, 192f);
			PermanentCalamityBuffUI.SoyMilkButton.OnLeftClick += this.SoyMilkClicked;
			PermanentCalamityBuffUI.SoyMilkButton.ModTooltip = UISystem.UnloadedText;
			this.BuffPanel.Append(PermanentCalamityBuffUI.SoyMilkButton);
			PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.YharimsStimulantsButton, 240f, 192f);
			PermanentCalamityBuffUI.YharimsStimulantsButton.OnLeftClick += this.YharimsStimulantsClicked;
			PermanentCalamityBuffUI.YharimsStimulantsButton.ModTooltip = UISystem.UnloadedText;
			this.BuffPanel.Append(PermanentCalamityBuffUI.YharimsStimulantsButton);
			HashSet<PermanentBuffButton> hashSet = new HashSet<PermanentBuffButton>();
			hashSet.Add(PermanentCalamityBuffUI.ChaosCandleButton);
			hashSet.Add(PermanentCalamityBuffUI.CorruptionEffigyButton);
			hashSet.Add(PermanentCalamityBuffUI.CrimsonEffigyButton);
			hashSet.Add(PermanentCalamityBuffUI.EffigyOfDecayButton);
			hashSet.Add(PermanentCalamityBuffUI.ResilientCandleButton);
			hashSet.Add(PermanentCalamityBuffUI.SpitefulCandleButton);
			hashSet.Add(PermanentCalamityBuffUI.TranquilityCandleButton);
			hashSet.Add(PermanentCalamityBuffUI.VigorousCandleButton);
			hashSet.Add(PermanentCalamityBuffUI.WeightlessCandleButton);
			hashSet.Add(PermanentCalamityBuffUI.AnechoicCoatingButton);
			hashSet.Add(PermanentCalamityBuffUI.AstralInjectionButton);
			hashSet.Add(PermanentCalamityBuffUI.BaguetteButton);
			hashSet.Add(PermanentCalamityBuffUI.BloodfinButton);
			hashSet.Add(PermanentCalamityBuffUI.BoundingButton);
			hashSet.Add(PermanentCalamityBuffUI.CalciumButton);
			hashSet.Add(PermanentCalamityBuffUI.CeaselessHungerButton);
			hashSet.Add(PermanentCalamityBuffUI.GravityNormalizerButton);
			hashSet.Add(PermanentCalamityBuffUI.OmniscienceButton);
			hashSet.Add(PermanentCalamityBuffUI.PhotosynthesisButton);
			hashSet.Add(PermanentCalamityBuffUI.ShadowButton);
			hashSet.Add(PermanentCalamityBuffUI.SoaringButton);
			hashSet.Add(PermanentCalamityBuffUI.SulphurskinButton);
			hashSet.Add(PermanentCalamityBuffUI.TeslaButton);
			hashSet.Add(PermanentCalamityBuffUI.ZenButton);
			hashSet.Add(PermanentCalamityBuffUI.ZergButton);
			hashSet.Add(PermanentCalamityBuffUI.AstraJellyButton);
			hashSet.Add(PermanentCalamityBuffUI.AstracolaButton);
			hashSet.Add(PermanentCalamityBuffUI.ExoBaguetteButton);
			hashSet.Add(PermanentCalamityBuffUI.SupremeLuckButton);
			hashSet.Add(PermanentCalamityBuffUI.TitanScaleButton);
			hashSet.Add(PermanentCalamityBuffUI.VoidCandleButton);
			hashSet.Add(PermanentCalamityBuffUI.SoyMilkButton);
			hashSet.Add(PermanentCalamityBuffUI.YharimsStimulantsButton);
			HashSet<PermanentBuffButton> buttons = hashSet;
			this.allBuffButtons.UnionWith(buttons);
			base.Append(this.BuffPanel);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0007BD9E File Offset: 0x00079F9E
		private void ChaosCandleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(0, PermanentCalamityBuffUI.ChaosCandleButton);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0007BDAB File Offset: 0x00079FAB
		private void CorruptionEffigyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(1, PermanentCalamityBuffUI.CorruptionEffigyButton);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0007BDB8 File Offset: 0x00079FB8
		private void CrimsonEffigyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(2, PermanentCalamityBuffUI.CrimsonEffigyButton);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0007BDC5 File Offset: 0x00079FC5
		private void EffigyOfDecayClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(3, PermanentCalamityBuffUI.EffigyOfDecayButton);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0007BDD2 File Offset: 0x00079FD2
		private void ResilientCandleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(4, PermanentCalamityBuffUI.ResilientCandleButton);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0007BDDF File Offset: 0x00079FDF
		private void SpitefulCandleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(5, PermanentCalamityBuffUI.SpitefulCandleButton);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0007BDEC File Offset: 0x00079FEC
		private void TranquilityCandleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(6, PermanentCalamityBuffUI.TranquilityCandleButton);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0007BDF9 File Offset: 0x00079FF9
		private void VigorousCandleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(7, PermanentCalamityBuffUI.VigorousCandleButton);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0007BE06 File Offset: 0x0007A006
		private void WeightlessCandleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(8, PermanentCalamityBuffUI.WeightlessCandleButton);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0007BE13 File Offset: 0x0007A013
		private void AnechoicCoatingClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(9, PermanentCalamityBuffUI.AnechoicCoatingButton);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0007BE21 File Offset: 0x0007A021
		private void AstralInjectionClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(10, PermanentCalamityBuffUI.AstralInjectionButton);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0007BE2F File Offset: 0x0007A02F
		private void BaguetteClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(11, PermanentCalamityBuffUI.BaguetteButton);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0007BE3D File Offset: 0x0007A03D
		private void BloodfinClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(12, PermanentCalamityBuffUI.BloodfinButton);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0007BE4B File Offset: 0x0007A04B
		private void BoundingClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(13, PermanentCalamityBuffUI.BoundingButton);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0007BE59 File Offset: 0x0007A059
		private void CalciumClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(14, PermanentCalamityBuffUI.CalciumButton);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0007BE67 File Offset: 0x0007A067
		private void CeaselessHungerClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(15, PermanentCalamityBuffUI.CeaselessHungerButton);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0007BE75 File Offset: 0x0007A075
		private void GravityNormalizerClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(16, PermanentCalamityBuffUI.GravityNormalizerButton);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0007BE83 File Offset: 0x0007A083
		private void OmniscienceClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(17, PermanentCalamityBuffUI.OmniscienceButton);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0007BE91 File Offset: 0x0007A091
		private void PhotosynthesisClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(18, PermanentCalamityBuffUI.PhotosynthesisButton);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0007BE9F File Offset: 0x0007A09F
		private void ShadowClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(19, PermanentCalamityBuffUI.ShadowButton);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0007BEAD File Offset: 0x0007A0AD
		private void SoaringClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(20, PermanentCalamityBuffUI.SoaringButton);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0007BEBB File Offset: 0x0007A0BB
		private void SulphurskinClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(21, PermanentCalamityBuffUI.SulphurskinButton);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0007BEC9 File Offset: 0x0007A0C9
		private void TeslaClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(22, PermanentCalamityBuffUI.TeslaButton);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0007BED7 File Offset: 0x0007A0D7
		private void ZenClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(23, PermanentCalamityBuffUI.ZenButton);
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0007BEE5 File Offset: 0x0007A0E5
		private void ZergClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(24, PermanentCalamityBuffUI.ZergButton);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0007BEF3 File Offset: 0x0007A0F3
		private void AstraJellyClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(25, PermanentCalamityBuffUI.AstraJellyButton);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0007BF01 File Offset: 0x0007A101
		private void AstracolaClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(26, PermanentCalamityBuffUI.AstracolaButton);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0007BF0F File Offset: 0x0007A10F
		private void ExoBaguetteClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(27, PermanentCalamityBuffUI.ExoBaguetteButton);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0007BF1D File Offset: 0x0007A11D
		private void SupremeLuckClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(28, PermanentCalamityBuffUI.SupremeLuckButton);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0007BF2B File Offset: 0x0007A12B
		private void TitanScaleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(29, PermanentCalamityBuffUI.TitanScaleButton);
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0007BF39 File Offset: 0x0007A139
		private void VoidCandleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(30, PermanentCalamityBuffUI.VoidCandleButton);
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0007BF47 File Offset: 0x0007A147
		private void SoyMilkClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(31, PermanentCalamityBuffUI.SoyMilkButton);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0007BF55 File Offset: 0x0007A155
		private void YharimsStimulantsClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			PermanentCalamityBuffUI.BuffClick(32, PermanentCalamityBuffUI.YharimsStimulantsButton);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0007BF64 File Offset: 0x0007A164
		public static void GetCalamityBuffData()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			PermanentCalamityBuffUI.ChaosCandleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "ChaosCandleBuff")).DisplayName;
			PermanentCalamityBuffUI.CorruptionEffigyButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "CorruptionEffigyBuff")).DisplayName;
			PermanentCalamityBuffUI.CrimsonEffigyButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "CrimsonEffigyBuff")).DisplayName;
			PermanentCalamityBuffUI.EffigyOfDecayButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "EffigyOfDecayBuff")).DisplayName;
			PermanentCalamityBuffUI.ResilientCandleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "PurpleCandleBuff")).DisplayName;
			PermanentCalamityBuffUI.SpitefulCandleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "YellowCandleBuff")).DisplayName;
			PermanentCalamityBuffUI.TranquilityCandleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "TranquilityCandleBuff")).DisplayName;
			PermanentCalamityBuffUI.VigorousCandleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "PinkCandleBuff")).DisplayName;
			PermanentCalamityBuffUI.WeightlessCandleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "BlueCandleBuff")).DisplayName;
			PermanentCalamityBuffUI.AnechoicCoatingButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "AnechoicCoatingBuff")).DisplayName;
			PermanentCalamityBuffUI.AstralInjectionButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "AstralInjectionBuff")).DisplayName;
			PermanentCalamityBuffUI.BaguetteButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "BaguetteBuff")).DisplayName;
			PermanentCalamityBuffUI.BloodfinButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "BloodfinBoost")).DisplayName;
			PermanentCalamityBuffUI.BoundingButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "BoundingBuff")).DisplayName;
			PermanentCalamityBuffUI.CalciumButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "CalciumBuff")).DisplayName;
			PermanentCalamityBuffUI.CeaselessHungerButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "CeaselessHunger")).DisplayName;
			PermanentCalamityBuffUI.GravityNormalizerButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "GravityNormalizerBuff")).DisplayName;
			PermanentCalamityBuffUI.OmniscienceButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "Omniscience")).DisplayName;
			PermanentCalamityBuffUI.PhotosynthesisButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "PhotosynthesisBuff")).DisplayName;
			PermanentCalamityBuffUI.ShadowButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "ShadowBuff")).DisplayName;
			PermanentCalamityBuffUI.SoaringButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "Soaring")).DisplayName;
			PermanentCalamityBuffUI.SulphurskinButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "SulphurskinBuff")).DisplayName;
			PermanentCalamityBuffUI.TeslaButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff")).DisplayName;
			PermanentCalamityBuffUI.ZenButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "Zen")).DisplayName;
			PermanentCalamityBuffUI.ZergButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "Zerg")).DisplayName;
			if (ModConditions.catalystLoaded)
			{
				PermanentCalamityBuffUI.AstraJellyButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")).DisplayName;
				PermanentCalamityBuffUI.AstracolaButton.ModTooltip = ItemLoader.GetItem(Common.GetModItem(ModConditions.catalystMod, "Lean")).DisplayName;
			}
			if (ModConditions.clamityAddonLoaded)
			{
				PermanentCalamityBuffUI.ExoBaguetteButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.clamityAddonMod, "ExoBaguetteBuff")).DisplayName;
				PermanentCalamityBuffUI.SupremeLuckButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.clamityAddonMod, "SupremeLucky")).DisplayName;
				PermanentCalamityBuffUI.TitanScaleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.clamityAddonMod, "TitanScalePotionBuff")).DisplayName;
			}
			if (ModConditions.calamityEntropyLoaded)
			{
				PermanentCalamityBuffUI.VoidCandleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityEntropyMod, "VoidCandleBuff")).DisplayName;
				PermanentCalamityBuffUI.SoyMilkButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityEntropyMod, "SoyMilkBuff")).DisplayName;
				PermanentCalamityBuffUI.YharimsStimulantsButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityEntropyMod, "YharimPower")).DisplayName;
			}
			PermanentCalamityBuffUI.ChaosCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "ChaosCandleBuff")), 2);
			PermanentCalamityBuffUI.CorruptionEffigyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CorruptionEffigyBuff")), 2);
			PermanentCalamityBuffUI.CrimsonEffigyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CrimsonEffigyBuff")), 2);
			PermanentCalamityBuffUI.EffigyOfDecayButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "EffigyOfDecayBuff")), 2);
			PermanentCalamityBuffUI.ResilientCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "PurpleCandleBuff")), 2);
			PermanentCalamityBuffUI.SpitefulCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "YellowCandleBuff")), 2);
			PermanentCalamityBuffUI.TranquilityCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "TranquilityCandleBuff")), 2);
			PermanentCalamityBuffUI.VigorousCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "PinkCandleBuff")), 2);
			PermanentCalamityBuffUI.WeightlessCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BlueCandleBuff")), 2);
			PermanentCalamityBuffUI.AnechoicCoatingButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "AnechoicCoatingBuff")), 2);
			PermanentCalamityBuffUI.AstralInjectionButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "AstralInjectionBuff")), 2);
			PermanentCalamityBuffUI.BaguetteButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BaguetteBuff")), 2);
			PermanentCalamityBuffUI.BloodfinButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BloodfinBoost")), 2);
			PermanentCalamityBuffUI.BoundingButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BoundingBuff")), 2);
			PermanentCalamityBuffUI.CalciumButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CalciumBuff")), 2);
			PermanentCalamityBuffUI.CeaselessHungerButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CeaselessHunger")), 2);
			PermanentCalamityBuffUI.GravityNormalizerButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "GravityNormalizerBuff")), 2);
			PermanentCalamityBuffUI.OmniscienceButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Omniscience")), 2);
			PermanentCalamityBuffUI.PhotosynthesisButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "PhotosynthesisBuff")), 2);
			PermanentCalamityBuffUI.ShadowButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "ShadowBuff")), 2);
			PermanentCalamityBuffUI.SoaringButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Soaring")), 2);
			PermanentCalamityBuffUI.SulphurskinButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "SulphurskinBuff")), 2);
			PermanentCalamityBuffUI.TeslaButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff")), 2);
			PermanentCalamityBuffUI.ZenButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Zen")), 2);
			PermanentCalamityBuffUI.ZergButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Zerg")), 2);
			if (ModConditions.catalystLoaded)
			{
				PermanentCalamityBuffUI.AstraJellyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")), 2);
				PermanentCalamityBuffUI.AstracolaButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")), 2);
			}
			if (ModConditions.clamityAddonLoaded)
			{
				PermanentCalamityBuffUI.ExoBaguetteButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.clamityAddonMod, "ExoBaguetteBuff")), 2);
				PermanentCalamityBuffUI.SupremeLuckButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.clamityAddonMod, "SupremeLucky")), 2);
				PermanentCalamityBuffUI.TitanScaleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.clamityAddonMod, "TitanScalePotionBuff")), 2);
			}
			if (ModConditions.calamityEntropyLoaded)
			{
				PermanentCalamityBuffUI.VoidCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "VoidCandleBuff")), 2);
				PermanentCalamityBuffUI.SoyMilkButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "SoyMilkBuff")), 2);
				PermanentCalamityBuffUI.YharimsStimulantsButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "YharimPower")), 2);
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0007C970 File Offset: 0x0007AB70
		public override void Update(GameTime gameTime)
		{
			if (!PermanentCalamityBuffUI.visible)
			{
				return;
			}
			for (int i = 0; i < PermanentBuffPlayer.PermanentCalamityBuffsBools.Length; i++)
			{
				this.allBuffButtons.ElementAt(i).disabled = PermanentBuffPlayer.PermanentCalamityBuffsBools[i];
				this.allBuffButtons.ElementAt(i).moddedBuff = true;
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0007C9C1 File Offset: 0x0007ABC1
		public static void BuffClick(int buff, PermanentBuffButton button)
		{
			if (Main.GameUpdateCount - PermanentCalamityBuffUI.timeStart >= 10U)
			{
				PermanentBuffPlayer.PermanentCalamityBuffsBools[buff] = !PermanentBuffPlayer.PermanentCalamityBuffsBools[buff];
				button.disabled = PermanentBuffPlayer.PermanentCalamityBuffsBools[buff];
			}
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0007C9F0 File Offset: 0x0007ABF0
		private static void CreateBuffButton(PermanentBuffButton button, float left, float top)
		{
			button.Left.Set(left, 0f);
			button.Top.Set(top, 0f);
			button.Width.Set(32f, 0f);
			button.Height.Set(32f, 0f);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0007CA4C File Offset: 0x0007AC4C
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			this.offset = new Vector2(evt.MousePosition.X - this.BuffPanel.Left.Pixels, evt.MousePosition.Y - this.BuffPanel.Top.Pixels);
			this.dragging = true;
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0007CAA4 File Offset: 0x0007ACA4
		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			this.dragging = false;
			this.BuffPanel.Left.Set(end.X - this.offset.X, 0f);
			this.BuffPanel.Top.Set(end.Y - this.offset.Y, 0f);
			this.Recalculate();
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0007CB14 File Offset: 0x0007AD14
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

		// Token: 0x04000688 RID: 1672
		public UIPanel BuffPanel;

		// Token: 0x04000689 RID: 1673
		public static bool visible = false;

		// Token: 0x0400068A RID: 1674
		public static uint timeStart;

		// Token: 0x0400068B RID: 1675
		public static PermanentBuffButton ChaosCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "ChaosCandleBuff")), 2));

		// Token: 0x0400068C RID: 1676
		public static PermanentBuffButton CorruptionEffigyButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CorruptionEffigyBuff")), 2));

		// Token: 0x0400068D RID: 1677
		public static PermanentBuffButton CrimsonEffigyButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CrimsonEffigyBuff")), 2));

		// Token: 0x0400068E RID: 1678
		public static PermanentBuffButton EffigyOfDecayButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "EffigyOfDecayBuff")), 2));

		// Token: 0x0400068F RID: 1679
		public static PermanentBuffButton ResilientCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "PurpleCandleBuff")), 2));

		// Token: 0x04000690 RID: 1680
		public static PermanentBuffButton SpitefulCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "YellowCandleBuff")), 2));

		// Token: 0x04000691 RID: 1681
		public static PermanentBuffButton TranquilityCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "TranquilityCandleBuff")), 2));

		// Token: 0x04000692 RID: 1682
		public static PermanentBuffButton VigorousCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "PinkCandleBuff")), 2));

		// Token: 0x04000693 RID: 1683
		public static PermanentBuffButton WeightlessCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BlueCandleBuff")), 2));

		// Token: 0x04000694 RID: 1684
		public static PermanentBuffButton AnechoicCoatingButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "AnechoicCoatingBuff")), 2));

		// Token: 0x04000695 RID: 1685
		public static PermanentBuffButton AstralInjectionButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "AstralInjectionBuff")), 2));

		// Token: 0x04000696 RID: 1686
		public static PermanentBuffButton BaguetteButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BaguetteBuff")), 2));

		// Token: 0x04000697 RID: 1687
		public static PermanentBuffButton BloodfinButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BloodfinBoost")), 2));

		// Token: 0x04000698 RID: 1688
		public static PermanentBuffButton BoundingButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BoundingBuff")), 2));

		// Token: 0x04000699 RID: 1689
		public static PermanentBuffButton CalciumButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CalciumBuff")), 2));

		// Token: 0x0400069A RID: 1690
		public static PermanentBuffButton CeaselessHungerButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CeaselessHunger")), 2));

		// Token: 0x0400069B RID: 1691
		public static PermanentBuffButton GravityNormalizerButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "GravityNormalizerBuff")), 2));

		// Token: 0x0400069C RID: 1692
		public static PermanentBuffButton OmniscienceButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Omniscience")), 2));

		// Token: 0x0400069D RID: 1693
		public static PermanentBuffButton PhotosynthesisButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "PhotosynthesisBuff")), 2));

		// Token: 0x0400069E RID: 1694
		public static PermanentBuffButton ShadowButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "ShadowBuff")), 2));

		// Token: 0x0400069F RID: 1695
		public static PermanentBuffButton SoaringButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Soaring")), 2));

		// Token: 0x040006A0 RID: 1696
		public static PermanentBuffButton SulphurskinButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "SulphurskinBuff")), 2));

		// Token: 0x040006A1 RID: 1697
		public static PermanentBuffButton TeslaButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff")), 2));

		// Token: 0x040006A2 RID: 1698
		public static PermanentBuffButton ZenButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Zen")), 2));

		// Token: 0x040006A3 RID: 1699
		public static PermanentBuffButton ZergButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Zerg")), 2));

		// Token: 0x040006A4 RID: 1700
		public static PermanentBuffButton AstraJellyButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.catalystMod, Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")), 2));

		// Token: 0x040006A5 RID: 1701
		public static PermanentBuffButton AstracolaButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.catalystMod, Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")), 2));

		// Token: 0x040006A6 RID: 1702
		public static PermanentBuffButton ExoBaguetteButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.clamityAddonMod, Common.GetModBuff(ModConditions.clamityAddonMod, "ExoBaguetteBuff")), 2));

		// Token: 0x040006A7 RID: 1703
		public static PermanentBuffButton SupremeLuckButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.clamityAddonMod, Common.GetModBuff(ModConditions.clamityAddonMod, "SupremeLucky")), 2));

		// Token: 0x040006A8 RID: 1704
		public static PermanentBuffButton TitanScaleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.clamityAddonMod, Common.GetModBuff(ModConditions.clamityAddonMod, "TitanScalePotionBuff")), 2));

		// Token: 0x040006A9 RID: 1705
		public static PermanentBuffButton VoidCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "VoidCandleBuff")), 2));

		// Token: 0x040006AA RID: 1706
		public static PermanentBuffButton SoyMilkButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "SoyMilkBuff")), 2));

		// Token: 0x040006AB RID: 1707
		public static PermanentBuffButton YharimsStimulantsButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "YharimPower")), 2));

		// Token: 0x040006AC RID: 1708
		private HashSet<PermanentBuffButton> allBuffButtons = new HashSet<PermanentBuffButton>();

		// Token: 0x040006AD RID: 1709
		private Vector2 offset;

		// Token: 0x040006AE RID: 1710
		public bool dragging;
	}
}
