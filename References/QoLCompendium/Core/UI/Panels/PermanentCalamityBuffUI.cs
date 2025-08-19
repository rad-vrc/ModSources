// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.PermanentCalamityBuffUI
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.UI.Buttons;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI.Panels;

public class PermanentCalamityBuffUI : UIState
{
  public UIPanel BuffPanel;
  public static bool visible = false;
  public static uint timeStart;
  public static PermanentBuffButton ChaosCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "ChaosCandleBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton CorruptionEffigyButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CorruptionEffigyBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton CrimsonEffigyButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CrimsonEffigyBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton EffigyOfDecayButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "EffigyOfDecayBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton ResilientCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "PurpleCandleBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton SpitefulCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "YellowCandleBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton TranquilityCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "TranquilityCandleBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton VigorousCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "PinkCandleBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton WeightlessCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BlueCandleBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton AnechoicCoatingButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "AnechoicCoatingBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton AstralInjectionButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "AstralInjectionBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton BaguetteButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BaguetteBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton BloodfinButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BloodfinBoost")), (AssetRequestMode) 2));
  public static PermanentBuffButton BoundingButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BoundingBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton CalciumButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CalciumBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton CeaselessHungerButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CeaselessHunger")), (AssetRequestMode) 2));
  public static PermanentBuffButton GravityNormalizerButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "GravityNormalizerBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton OmniscienceButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Omniscience")), (AssetRequestMode) 2));
  public static PermanentBuffButton PhotosynthesisButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "PhotosynthesisBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton ShadowButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "ShadowBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton SoaringButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Soaring")), (AssetRequestMode) 2));
  public static PermanentBuffButton SulphurskinButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "SulphurskinBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton TeslaButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton ZenButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Zen")), (AssetRequestMode) 2));
  public static PermanentBuffButton ZergButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Zerg")), (AssetRequestMode) 2));
  public static PermanentBuffButton AstraJellyButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.catalystMod, Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton AstracolaButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.catalystMod, Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton ExoBaguetteButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.clamityAddonMod, Common.GetModBuff(ModConditions.clamityAddonMod, "ExoBaguetteBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton SupremeLuckButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.clamityAddonMod, Common.GetModBuff(ModConditions.clamityAddonMod, "SupremeLucky")), (AssetRequestMode) 2));
  public static PermanentBuffButton TitanScaleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.clamityAddonMod, Common.GetModBuff(ModConditions.clamityAddonMod, "TitanScalePotionBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton VoidCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "VoidCandleBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton SoyMilkButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "SoyMilkBuff")), (AssetRequestMode) 2));
  public static PermanentBuffButton YharimsStimulantsButton = new PermanentBuffButton(ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "YharimPower")), (AssetRequestMode) 2));
  private HashSet<PermanentBuffButton> allBuffButtons = new HashSet<PermanentBuffButton>();
  private Vector2 offset;
  public bool dragging;

  public virtual void OnInitialize()
  {
    this.BuffPanel = new UIPanel();
    ((UIElement) this.BuffPanel).SetPadding(0.0f);
    ((StyleDimension) ref ((UIElement) this.BuffPanel).Left).Set(575f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.BuffPanel).Top).Set(275f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.BuffPanel).Width).Set(352f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.BuffPanel).Height).Set(240f, 0.0f);
    this.BuffPanel.BackgroundColor = new Color(73, 94, 171);
    // ISSUE: method pointer
    ((UIElement) this.BuffPanel).OnLeftMouseDown += new UIElement.MouseEvent((object) this, __methodptr(DragStart));
    // ISSUE: method pointer
    ((UIElement) this.BuffPanel).OnLeftMouseUp += new UIElement.MouseEvent((object) this, __methodptr(DragEnd));
    UIText uiText1 = new UIText(UISystem.ArenaText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText1).Left).Set(16f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText1).Top).Set(8f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText1).Width).Set(64f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText1).Height).Set(32f, 0.0f);
    ((UIElement) this.BuffPanel).Append((UIElement) uiText1);
    UIText uiText2 = new UIText(UISystem.PotionText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText2).Left).Set(16f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Top).Set(72f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Width).Set(64f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Height).Set(32f, 0.0f);
    ((UIElement) this.BuffPanel).Append((UIElement) uiText2);
    UIText uiText3 = new UIText(UISystem.AddonText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText3).Left).Set(16f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Top).Set(168f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Width).Set(64f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Height).Set(32f, 0.0f);
    ((UIElement) this.BuffPanel).Append((UIElement) uiText3);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.ChaosCandleButton, 16f, 32f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.ChaosCandleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ChaosCandleClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.ChaosCandleButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.CorruptionEffigyButton, 48f, 32f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.CorruptionEffigyButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CorruptionEffigyClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.CorruptionEffigyButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.CrimsonEffigyButton, 80f, 32f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.CrimsonEffigyButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CrimsonEffigyClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.CrimsonEffigyButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.EffigyOfDecayButton, 112f, 32f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.EffigyOfDecayButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(EffigyOfDecayClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.EffigyOfDecayButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.ResilientCandleButton, 144f, 32f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.ResilientCandleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ResilientCandleClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.ResilientCandleButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.SpitefulCandleButton, 176f, 32f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.SpitefulCandleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SpitefulCandleClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.SpitefulCandleButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.TranquilityCandleButton, 208f, 32f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.TranquilityCandleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(TranquilityCandleClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.TranquilityCandleButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.VigorousCandleButton, 240f, 32f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.VigorousCandleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(VigorousCandleClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.VigorousCandleButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.WeightlessCandleButton, 272f, 32f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.WeightlessCandleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WeightlessCandleClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.WeightlessCandleButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.AnechoicCoatingButton, 16f, 96f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.AnechoicCoatingButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(AnechoicCoatingClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.AnechoicCoatingButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.AstralInjectionButton, 48f, 96f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.AstralInjectionButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(AstralInjectionClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.AstralInjectionButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.BaguetteButton, 80f, 96f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.BaguetteButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BaguetteClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.BaguetteButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.BloodfinButton, 112f, 96f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.BloodfinButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BloodfinClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.BloodfinButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.BoundingButton, 144f, 96f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.BoundingButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BoundingClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.BoundingButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.CalciumButton, 176f, 96f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.CalciumButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CalciumClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.CalciumButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.CeaselessHungerButton, 208f, 96f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.CeaselessHungerButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CeaselessHungerClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.CeaselessHungerButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.GravityNormalizerButton, 240f, 96f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.GravityNormalizerButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(GravityNormalizerClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.GravityNormalizerButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.OmniscienceButton, 272f, 96f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.OmniscienceButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(OmniscienceClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.OmniscienceButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.PhotosynthesisButton, 304f, 96f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.PhotosynthesisButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(PhotosynthesisClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.PhotosynthesisButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.ShadowButton, 16f, 128f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.ShadowButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ShadowClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.ShadowButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.SoaringButton, 48f, 128f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.SoaringButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SoaringClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.SoaringButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.SulphurskinButton, 80f, 128f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.SulphurskinButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SulphurskinClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.SulphurskinButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.TeslaButton, 112f, 128f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.TeslaButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(TeslaClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.TeslaButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.ZenButton, 144f, 128f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.ZenButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ZenClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.ZenButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.ZergButton, 176f, 128f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.ZergButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ZergClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.ZergButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.AstraJellyButton, 16f, 192f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.AstraJellyButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(AstraJellyClicked));
    PermanentCalamityBuffUI.AstraJellyButton.ModTooltip = UISystem.UnloadedText;
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.AstraJellyButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.AstracolaButton, 48f, 192f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.AstracolaButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(AstracolaClicked));
    PermanentCalamityBuffUI.AstracolaButton.ModTooltip = UISystem.UnloadedText;
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.AstracolaButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.ExoBaguetteButton, 80f, 192f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.ExoBaguetteButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ExoBaguetteClicked));
    PermanentCalamityBuffUI.ExoBaguetteButton.ModTooltip = UISystem.UnloadedText;
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.ExoBaguetteButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.SupremeLuckButton, 112f, 192f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.SupremeLuckButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SupremeLuckClicked));
    PermanentCalamityBuffUI.SupremeLuckButton.ModTooltip = UISystem.UnloadedText;
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.SupremeLuckButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.TitanScaleButton, 144f, 192f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.TitanScaleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(TitanScaleClicked));
    PermanentCalamityBuffUI.TitanScaleButton.ModTooltip = UISystem.UnloadedText;
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.TitanScaleButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.VoidCandleButton, 176f, 192f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.VoidCandleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(VoidCandleClicked));
    PermanentCalamityBuffUI.VoidCandleButton.ModTooltip = UISystem.UnloadedText;
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.VoidCandleButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.SoyMilkButton, 208f, 192f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.SoyMilkButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SoyMilkClicked));
    PermanentCalamityBuffUI.SoyMilkButton.ModTooltip = UISystem.UnloadedText;
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.SoyMilkButton);
    PermanentCalamityBuffUI.CreateBuffButton(PermanentCalamityBuffUI.YharimsStimulantsButton, 240f, 192f);
    // ISSUE: method pointer
    PermanentCalamityBuffUI.YharimsStimulantsButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(YharimsStimulantsClicked));
    PermanentCalamityBuffUI.YharimsStimulantsButton.ModTooltip = UISystem.UnloadedText;
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentCalamityBuffUI.YharimsStimulantsButton);
    this.allBuffButtons.UnionWith((IEnumerable<PermanentBuffButton>) new HashSet<PermanentBuffButton>()
    {
      PermanentCalamityBuffUI.ChaosCandleButton,
      PermanentCalamityBuffUI.CorruptionEffigyButton,
      PermanentCalamityBuffUI.CrimsonEffigyButton,
      PermanentCalamityBuffUI.EffigyOfDecayButton,
      PermanentCalamityBuffUI.ResilientCandleButton,
      PermanentCalamityBuffUI.SpitefulCandleButton,
      PermanentCalamityBuffUI.TranquilityCandleButton,
      PermanentCalamityBuffUI.VigorousCandleButton,
      PermanentCalamityBuffUI.WeightlessCandleButton,
      PermanentCalamityBuffUI.AnechoicCoatingButton,
      PermanentCalamityBuffUI.AstralInjectionButton,
      PermanentCalamityBuffUI.BaguetteButton,
      PermanentCalamityBuffUI.BloodfinButton,
      PermanentCalamityBuffUI.BoundingButton,
      PermanentCalamityBuffUI.CalciumButton,
      PermanentCalamityBuffUI.CeaselessHungerButton,
      PermanentCalamityBuffUI.GravityNormalizerButton,
      PermanentCalamityBuffUI.OmniscienceButton,
      PermanentCalamityBuffUI.PhotosynthesisButton,
      PermanentCalamityBuffUI.ShadowButton,
      PermanentCalamityBuffUI.SoaringButton,
      PermanentCalamityBuffUI.SulphurskinButton,
      PermanentCalamityBuffUI.TeslaButton,
      PermanentCalamityBuffUI.ZenButton,
      PermanentCalamityBuffUI.ZergButton,
      PermanentCalamityBuffUI.AstraJellyButton,
      PermanentCalamityBuffUI.AstracolaButton,
      PermanentCalamityBuffUI.ExoBaguetteButton,
      PermanentCalamityBuffUI.SupremeLuckButton,
      PermanentCalamityBuffUI.TitanScaleButton,
      PermanentCalamityBuffUI.VoidCandleButton,
      PermanentCalamityBuffUI.SoyMilkButton,
      PermanentCalamityBuffUI.YharimsStimulantsButton
    });
    ((UIElement) this).Append((UIElement) this.BuffPanel);
  }

  private void ChaosCandleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(0, PermanentCalamityBuffUI.ChaosCandleButton);
  }

  private void CorruptionEffigyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(1, PermanentCalamityBuffUI.CorruptionEffigyButton);
  }

  private void CrimsonEffigyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(2, PermanentCalamityBuffUI.CrimsonEffigyButton);
  }

  private void EffigyOfDecayClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(3, PermanentCalamityBuffUI.EffigyOfDecayButton);
  }

  private void ResilientCandleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(4, PermanentCalamityBuffUI.ResilientCandleButton);
  }

  private void SpitefulCandleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(5, PermanentCalamityBuffUI.SpitefulCandleButton);
  }

  private void TranquilityCandleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(6, PermanentCalamityBuffUI.TranquilityCandleButton);
  }

  private void VigorousCandleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(7, PermanentCalamityBuffUI.VigorousCandleButton);
  }

  private void WeightlessCandleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(8, PermanentCalamityBuffUI.WeightlessCandleButton);
  }

  private void AnechoicCoatingClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(9, PermanentCalamityBuffUI.AnechoicCoatingButton);
  }

  private void AstralInjectionClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(10, PermanentCalamityBuffUI.AstralInjectionButton);
  }

  private void BaguetteClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(11, PermanentCalamityBuffUI.BaguetteButton);
  }

  private void BloodfinClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(12, PermanentCalamityBuffUI.BloodfinButton);
  }

  private void BoundingClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(13, PermanentCalamityBuffUI.BoundingButton);
  }

  private void CalciumClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(14, PermanentCalamityBuffUI.CalciumButton);
  }

  private void CeaselessHungerClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(15, PermanentCalamityBuffUI.CeaselessHungerButton);
  }

  private void GravityNormalizerClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(16 /*0x10*/, PermanentCalamityBuffUI.GravityNormalizerButton);
  }

  private void OmniscienceClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(17, PermanentCalamityBuffUI.OmniscienceButton);
  }

  private void PhotosynthesisClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(18, PermanentCalamityBuffUI.PhotosynthesisButton);
  }

  private void ShadowClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(19, PermanentCalamityBuffUI.ShadowButton);
  }

  private void SoaringClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(20, PermanentCalamityBuffUI.SoaringButton);
  }

  private void SulphurskinClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(21, PermanentCalamityBuffUI.SulphurskinButton);
  }

  private void TeslaClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(22, PermanentCalamityBuffUI.TeslaButton);
  }

  private void ZenClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(23, PermanentCalamityBuffUI.ZenButton);
  }

  private void ZergClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(24, PermanentCalamityBuffUI.ZergButton);
  }

  private void AstraJellyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(25, PermanentCalamityBuffUI.AstraJellyButton);
  }

  private void AstracolaClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(26, PermanentCalamityBuffUI.AstracolaButton);
  }

  private void ExoBaguetteClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(27, PermanentCalamityBuffUI.ExoBaguetteButton);
  }

  private void SupremeLuckClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(28, PermanentCalamityBuffUI.SupremeLuckButton);
  }

  private void TitanScaleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(29, PermanentCalamityBuffUI.TitanScaleButton);
  }

  private void VoidCandleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(30, PermanentCalamityBuffUI.VoidCandleButton);
  }

  private void SoyMilkClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(31 /*0x1F*/, PermanentCalamityBuffUI.SoyMilkButton);
  }

  private void YharimsStimulantsClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentCalamityBuffUI.BuffClick(32 /*0x20*/, PermanentCalamityBuffUI.YharimsStimulantsButton);
  }

  public static void GetCalamityBuffData()
  {
    if (!ModConditions.calamityLoaded)
      return;
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
    PermanentCalamityBuffUI.ChaosCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "ChaosCandleBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.CorruptionEffigyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CorruptionEffigyBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.CrimsonEffigyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CrimsonEffigyBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.EffigyOfDecayButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "EffigyOfDecayBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.ResilientCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "PurpleCandleBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.SpitefulCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "YellowCandleBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.TranquilityCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "TranquilityCandleBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.VigorousCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "PinkCandleBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.WeightlessCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BlueCandleBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.AnechoicCoatingButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "AnechoicCoatingBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.AstralInjectionButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "AstralInjectionBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.BaguetteButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BaguetteBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.BloodfinButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BloodfinBoost")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.BoundingButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "BoundingBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.CalciumButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CalciumBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.CeaselessHungerButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "CeaselessHunger")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.GravityNormalizerButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "GravityNormalizerBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.OmniscienceButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Omniscience")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.PhotosynthesisButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "PhotosynthesisBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.ShadowButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "ShadowBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.SoaringButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Soaring")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.SulphurskinButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "SulphurskinBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.TeslaButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.ZenButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Zen")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.ZergButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Zerg")), (AssetRequestMode) 2);
    if (ModConditions.catalystLoaded)
    {
      PermanentCalamityBuffUI.AstraJellyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")), (AssetRequestMode) 2);
      PermanentCalamityBuffUI.AstracolaButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")), (AssetRequestMode) 2);
    }
    if (ModConditions.clamityAddonLoaded)
    {
      PermanentCalamityBuffUI.ExoBaguetteButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.clamityAddonMod, "ExoBaguetteBuff")), (AssetRequestMode) 2);
      PermanentCalamityBuffUI.SupremeLuckButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.clamityAddonMod, "SupremeLucky")), (AssetRequestMode) 2);
      PermanentCalamityBuffUI.TitanScaleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.clamityAddonMod, "TitanScalePotionBuff")), (AssetRequestMode) 2);
    }
    if (!ModConditions.calamityEntropyLoaded)
      return;
    PermanentCalamityBuffUI.VoidCandleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "VoidCandleBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.SoyMilkButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "SoyMilkBuff")), (AssetRequestMode) 2);
    PermanentCalamityBuffUI.YharimsStimulantsButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.calamityEntropyMod, Common.GetModBuff(ModConditions.calamityEntropyMod, "YharimPower")), (AssetRequestMode) 2);
  }

  public virtual void Update(GameTime gameTime)
  {
    if (!PermanentCalamityBuffUI.visible)
      return;
    for (int index = 0; index < PermanentBuffPlayer.PermanentCalamityBuffsBools.Length; ++index)
    {
      this.allBuffButtons.ElementAt<PermanentBuffButton>(index).disabled = PermanentBuffPlayer.PermanentCalamityBuffsBools[index];
      this.allBuffButtons.ElementAt<PermanentBuffButton>(index).moddedBuff = true;
    }
  }

  public static void BuffClick(int buff, PermanentBuffButton button)
  {
    if (Main.GameUpdateCount - PermanentCalamityBuffUI.timeStart < 10U)
      return;
    PermanentBuffPlayer.PermanentCalamityBuffsBools[buff] = !PermanentBuffPlayer.PermanentCalamityBuffsBools[buff];
    button.disabled = PermanentBuffPlayer.PermanentCalamityBuffsBools[buff];
  }

  private static void CreateBuffButton(PermanentBuffButton button, float left, float top)
  {
    ((StyleDimension) ref button.Left).Set(left, 0.0f);
    ((StyleDimension) ref button.Top).Set(top, 0.0f);
    ((StyleDimension) ref button.Width).Set(32f, 0.0f);
    ((StyleDimension) ref button.Height).Set(32f, 0.0f);
  }

  private void DragStart(UIMouseEvent evt, UIElement listeningElement)
  {
    this.offset = new Vector2(evt.MousePosition.X - ((UIElement) this.BuffPanel).Left.Pixels, evt.MousePosition.Y - ((UIElement) this.BuffPanel).Top.Pixels);
    this.dragging = true;
  }

  private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
  {
    Vector2 mousePosition = evt.MousePosition;
    this.dragging = false;
    ((StyleDimension) ref ((UIElement) this.BuffPanel).Left).Set(mousePosition.X - this.offset.X, 0.0f);
    ((StyleDimension) ref ((UIElement) this.BuffPanel).Top).Set(mousePosition.Y - this.offset.Y, 0.0f);
    ((UIElement) this).Recalculate();
  }

  protected virtual void DrawSelf(SpriteBatch spriteBatch)
  {
    Vector2 vector2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2).\u002Ector((float) Main.mouseX, (float) Main.mouseY);
    if (((UIElement) this.BuffPanel).ContainsPoint(vector2))
      Main.LocalPlayer.mouseInterface = true;
    if (!this.dragging)
      return;
    ((StyleDimension) ref ((UIElement) this.BuffPanel).Left).Set(vector2.X - this.offset.X, 0.0f);
    ((StyleDimension) ref ((UIElement) this.BuffPanel).Top).Set(vector2.Y - this.offset.Y, 0.0f);
    ((UIElement) this).Recalculate();
  }
}
