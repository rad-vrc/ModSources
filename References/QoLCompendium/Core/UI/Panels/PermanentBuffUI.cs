// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.PermanentBuffUI
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

public class PermanentBuffUI : UIState
{
  public UIPanel BuffPanel;
  public static bool visible;
  public static uint timeStart;
  private PermanentBuffButton BastStatueButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 215.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton CampfireButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 87.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton GardenGnomeButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentGardenGnome", (AssetRequestMode) 2));
  private PermanentBuffButton HeartLanternButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 89.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton HoneyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 48 /*0x30*/.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton PeaceCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 157.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton ShadowCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 350.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton StarInABottleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 158.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton SunflowerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 146.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton WaterCandleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 86.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton AmmoReservationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 112 /*0x70*/.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton ArcheryButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 16 /*0x10*/.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton BattleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 13.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton BiomeSightButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 343.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton BuilderButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 107.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton CalmButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 106.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton CrateButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 123.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton DangersenseButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 111.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton EnduranceButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 114.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton ExquisitelyStuffedButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 207.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton FeatherfallButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 8.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton FishingButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 121.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton FlipperButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 109.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton GillsButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 4.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton GravitationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 18.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton HeartreachButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 105.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton HunterButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 17.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton InfernoButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 116.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton InvisibilityButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 10.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton IronskinButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 5.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton LifeforceButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 113.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton LuckyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 257.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton MagicPowerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 7.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton ManaRegenerationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 6.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton MiningButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 104.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton NightOwlButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 12.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton ObsidianSkinButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 1.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton PlentySatisfiedButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 206.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton RageButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 115.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton RegenerationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 2.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton ShineButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 11.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton SonarButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 122.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton SpelunkerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 9.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton SummoningButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 110.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton SwiftnessButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 3.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton ThornsButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 14.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton TipsyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 25.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton TitanButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 108.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton WarmthButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 124.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton WaterWalkingButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 15.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton WellFedButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 26.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton WrathButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 117.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton AmmoBoxButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 93.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton BewitchingTableButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 150.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton CrystalBallButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 29.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton SharpeningStationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 159.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton SliceOfCakeButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 192 /*0xC0*/.ToString(), (AssetRequestMode) 2));
  private PermanentBuffButton WarTableButton = new PermanentBuffButton(ModContent.Request<Texture2D>("Terraria/Images/Buff_" + 348.ToString(), (AssetRequestMode) 2));
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
    ((StyleDimension) ref ((UIElement) this.BuffPanel).Height).Set(336f, 0.0f);
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
    UIText uiText3 = new UIText(UISystem.StationText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText3).Left).Set(16f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Top).Set(264f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Width).Set(64f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Height).Set(32f, 0.0f);
    ((UIElement) this.BuffPanel).Append((UIElement) uiText3);
    PermanentBuffUI.CreateBuffButton(this.BastStatueButton, 16f, 32f);
    // ISSUE: method pointer
    this.BastStatueButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BastStatueClicked));
    this.BastStatueButton.Tooltip = Lang.GetBuffName(215);
    ((UIElement) this.BuffPanel).Append((UIElement) this.BastStatueButton);
    PermanentBuffUI.CreateBuffButton(this.CampfireButton, 48f, 32f);
    // ISSUE: method pointer
    this.CampfireButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CampfireClicked));
    this.CampfireButton.Tooltip = Lang.GetBuffName(87);
    ((UIElement) this.BuffPanel).Append((UIElement) this.CampfireButton);
    PermanentBuffUI.CreateBuffButton(this.GardenGnomeButton, 80f, 32f);
    // ISSUE: method pointer
    this.GardenGnomeButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(GardenGnomeClicked));
    this.GardenGnomeButton.Tooltip = Lang.GetItemName(4609).ToString();
    ((UIElement) this.BuffPanel).Append((UIElement) this.GardenGnomeButton);
    PermanentBuffUI.CreateBuffButton(this.HeartLanternButton, 112f, 32f);
    // ISSUE: method pointer
    this.HeartLanternButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(HeartLanternClicked));
    this.HeartLanternButton.Tooltip = Lang.GetBuffName(89);
    ((UIElement) this.BuffPanel).Append((UIElement) this.HeartLanternButton);
    PermanentBuffUI.CreateBuffButton(this.HoneyButton, 144f, 32f);
    // ISSUE: method pointer
    this.HoneyButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(HoneyClicked));
    this.HoneyButton.Tooltip = Lang.GetBuffName(48 /*0x30*/);
    ((UIElement) this.BuffPanel).Append((UIElement) this.HoneyButton);
    PermanentBuffUI.CreateBuffButton(this.PeaceCandleButton, 176f, 32f);
    // ISSUE: method pointer
    this.PeaceCandleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(PeaceCandleClicked));
    this.PeaceCandleButton.Tooltip = Lang.GetBuffName(157);
    ((UIElement) this.BuffPanel).Append((UIElement) this.PeaceCandleButton);
    PermanentBuffUI.CreateBuffButton(this.ShadowCandleButton, 208f, 32f);
    // ISSUE: method pointer
    this.ShadowCandleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ShadowCandleClicked));
    this.ShadowCandleButton.Tooltip = Lang.GetBuffName(350);
    ((UIElement) this.BuffPanel).Append((UIElement) this.ShadowCandleButton);
    PermanentBuffUI.CreateBuffButton(this.StarInABottleButton, 240f, 32f);
    // ISSUE: method pointer
    this.StarInABottleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(StarInABottleClicked));
    this.StarInABottleButton.Tooltip = Lang.GetBuffName(158);
    ((UIElement) this.BuffPanel).Append((UIElement) this.StarInABottleButton);
    PermanentBuffUI.CreateBuffButton(this.SunflowerButton, 272f, 32f);
    // ISSUE: method pointer
    this.SunflowerButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SunflowerClicked));
    this.SunflowerButton.Tooltip = Lang.GetBuffName(146);
    ((UIElement) this.BuffPanel).Append((UIElement) this.SunflowerButton);
    PermanentBuffUI.CreateBuffButton(this.WaterCandleButton, 304f, 32f);
    // ISSUE: method pointer
    this.WaterCandleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WaterCandleClicked));
    this.WaterCandleButton.Tooltip = Lang.GetBuffName(86);
    ((UIElement) this.BuffPanel).Append((UIElement) this.WaterCandleButton);
    PermanentBuffUI.CreateBuffButton(this.AmmoReservationButton, 16f, 96f);
    // ISSUE: method pointer
    this.AmmoReservationButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(AmmoReservationClicked));
    this.AmmoReservationButton.Tooltip = Lang.GetBuffName(112 /*0x70*/);
    ((UIElement) this.BuffPanel).Append((UIElement) this.AmmoReservationButton);
    PermanentBuffUI.CreateBuffButton(this.ArcheryButton, 48f, 96f);
    // ISSUE: method pointer
    this.ArcheryButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ArcheryClicked));
    this.ArcheryButton.Tooltip = Lang.GetBuffName(16 /*0x10*/);
    ((UIElement) this.BuffPanel).Append((UIElement) this.ArcheryButton);
    PermanentBuffUI.CreateBuffButton(this.BattleButton, 80f, 96f);
    // ISSUE: method pointer
    this.BattleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BattleClicked));
    this.BattleButton.Tooltip = Lang.GetBuffName(13);
    ((UIElement) this.BuffPanel).Append((UIElement) this.BattleButton);
    PermanentBuffUI.CreateBuffButton(this.BiomeSightButton, 112f, 96f);
    // ISSUE: method pointer
    this.BiomeSightButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BiomeSightClicked));
    this.BiomeSightButton.Tooltip = Lang.GetBuffName(343);
    ((UIElement) this.BuffPanel).Append((UIElement) this.BiomeSightButton);
    PermanentBuffUI.CreateBuffButton(this.BuilderButton, 144f, 96f);
    // ISSUE: method pointer
    this.BuilderButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BuilderClicked));
    this.BuilderButton.Tooltip = Lang.GetBuffName(107);
    ((UIElement) this.BuffPanel).Append((UIElement) this.BuilderButton);
    PermanentBuffUI.CreateBuffButton(this.CalmButton, 176f, 96f);
    // ISSUE: method pointer
    this.CalmButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CalmClicked));
    this.CalmButton.Tooltip = Lang.GetBuffName(106);
    ((UIElement) this.BuffPanel).Append((UIElement) this.CalmButton);
    PermanentBuffUI.CreateBuffButton(this.CrateButton, 208f, 96f);
    // ISSUE: method pointer
    this.CrateButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CrateClicked));
    this.CrateButton.Tooltip = Lang.GetBuffName(123);
    ((UIElement) this.BuffPanel).Append((UIElement) this.CrateButton);
    PermanentBuffUI.CreateBuffButton(this.DangersenseButton, 240f, 96f);
    // ISSUE: method pointer
    this.DangersenseButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DangersenseClicked));
    this.DangersenseButton.Tooltip = Lang.GetBuffName(111);
    ((UIElement) this.BuffPanel).Append((UIElement) this.DangersenseButton);
    PermanentBuffUI.CreateBuffButton(this.EnduranceButton, 272f, 96f);
    // ISSUE: method pointer
    this.EnduranceButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(EnduranceClicked));
    this.EnduranceButton.Tooltip = Lang.GetBuffName(114);
    ((UIElement) this.BuffPanel).Append((UIElement) this.EnduranceButton);
    PermanentBuffUI.CreateBuffButton(this.ExquisitelyStuffedButton, 304f, 96f);
    // ISSUE: method pointer
    this.ExquisitelyStuffedButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ExquisitelyStuffedClicked));
    this.ExquisitelyStuffedButton.Tooltip = Lang.GetBuffName(207);
    ((UIElement) this.BuffPanel).Append((UIElement) this.ExquisitelyStuffedButton);
    PermanentBuffUI.CreateBuffButton(this.FeatherfallButton, 16f, 128f);
    // ISSUE: method pointer
    this.FeatherfallButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(FeatherfallClicked));
    this.FeatherfallButton.Tooltip = Lang.GetBuffName(8);
    ((UIElement) this.BuffPanel).Append((UIElement) this.FeatherfallButton);
    PermanentBuffUI.CreateBuffButton(this.FishingButton, 48f, 128f);
    // ISSUE: method pointer
    this.FishingButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(FishingClicked));
    this.FishingButton.Tooltip = Lang.GetBuffName(121);
    ((UIElement) this.BuffPanel).Append((UIElement) this.FishingButton);
    PermanentBuffUI.CreateBuffButton(this.FlipperButton, 80f, 128f);
    // ISSUE: method pointer
    this.FlipperButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(FlipperClicked));
    this.FlipperButton.Tooltip = Lang.GetBuffName(109);
    ((UIElement) this.BuffPanel).Append((UIElement) this.FlipperButton);
    PermanentBuffUI.CreateBuffButton(this.GillsButton, 112f, 128f);
    // ISSUE: method pointer
    this.GillsButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(GillsClicked));
    this.GillsButton.Tooltip = Lang.GetBuffName(4);
    ((UIElement) this.BuffPanel).Append((UIElement) this.GillsButton);
    PermanentBuffUI.CreateBuffButton(this.GravitationButton, 144f, 128f);
    // ISSUE: method pointer
    this.GravitationButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(GravitationClicked));
    this.GravitationButton.Tooltip = Lang.GetBuffName(18);
    ((UIElement) this.BuffPanel).Append((UIElement) this.GravitationButton);
    PermanentBuffUI.CreateBuffButton(this.HeartreachButton, 176f, 128f);
    // ISSUE: method pointer
    this.HeartreachButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(HeartreachClicked));
    this.HeartreachButton.Tooltip = Lang.GetBuffName(105);
    ((UIElement) this.BuffPanel).Append((UIElement) this.HeartreachButton);
    PermanentBuffUI.CreateBuffButton(this.HunterButton, 208f, 128f);
    // ISSUE: method pointer
    this.HunterButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(HunterClicked));
    this.HunterButton.Tooltip = Lang.GetBuffName(17);
    ((UIElement) this.BuffPanel).Append((UIElement) this.HunterButton);
    PermanentBuffUI.CreateBuffButton(this.InfernoButton, 240f, 128f);
    // ISSUE: method pointer
    this.InfernoButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(InfernoClicked));
    this.InfernoButton.Tooltip = Lang.GetBuffName(116);
    ((UIElement) this.BuffPanel).Append((UIElement) this.InfernoButton);
    PermanentBuffUI.CreateBuffButton(this.InvisibilityButton, 272f, 128f);
    // ISSUE: method pointer
    this.InvisibilityButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(InvisibilityClicked));
    this.InvisibilityButton.Tooltip = Lang.GetBuffName(10);
    ((UIElement) this.BuffPanel).Append((UIElement) this.InvisibilityButton);
    PermanentBuffUI.CreateBuffButton(this.IronskinButton, 304f, 128f);
    // ISSUE: method pointer
    this.IronskinButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(IronskinClicked));
    this.IronskinButton.Tooltip = Lang.GetBuffName(5);
    ((UIElement) this.BuffPanel).Append((UIElement) this.IronskinButton);
    PermanentBuffUI.CreateBuffButton(this.LifeforceButton, 16f, 160f);
    // ISSUE: method pointer
    this.LifeforceButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(LifeforceClicked));
    this.LifeforceButton.Tooltip = Lang.GetBuffName(113);
    ((UIElement) this.BuffPanel).Append((UIElement) this.LifeforceButton);
    PermanentBuffUI.CreateBuffButton(this.LuckyButton, 48f, 160f);
    // ISSUE: method pointer
    this.LuckyButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(LuckyClicked));
    this.LuckyButton.Tooltip = Lang.GetBuffName(257);
    ((UIElement) this.BuffPanel).Append((UIElement) this.LuckyButton);
    PermanentBuffUI.CreateBuffButton(this.MagicPowerButton, 80f, 160f);
    // ISSUE: method pointer
    this.MagicPowerButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MagicPowerClicked));
    this.MagicPowerButton.Tooltip = Lang.GetBuffName(7);
    ((UIElement) this.BuffPanel).Append((UIElement) this.MagicPowerButton);
    PermanentBuffUI.CreateBuffButton(this.ManaRegenerationButton, 112f, 160f);
    // ISSUE: method pointer
    this.ManaRegenerationButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ManaRegenerationClicked));
    this.ManaRegenerationButton.Tooltip = Lang.GetBuffName(6);
    ((UIElement) this.BuffPanel).Append((UIElement) this.ManaRegenerationButton);
    PermanentBuffUI.CreateBuffButton(this.MiningButton, 144f, 160f);
    // ISSUE: method pointer
    this.MiningButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MiningClicked));
    this.MiningButton.Tooltip = Lang.GetBuffName(104);
    ((UIElement) this.BuffPanel).Append((UIElement) this.MiningButton);
    PermanentBuffUI.CreateBuffButton(this.NightOwlButton, 176f, 160f);
    // ISSUE: method pointer
    this.NightOwlButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(NightOwlClicked));
    this.NightOwlButton.Tooltip = Lang.GetBuffName(12);
    ((UIElement) this.BuffPanel).Append((UIElement) this.NightOwlButton);
    PermanentBuffUI.CreateBuffButton(this.ObsidianSkinButton, 208f, 160f);
    // ISSUE: method pointer
    this.ObsidianSkinButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ObsidianSkinClicked));
    this.ObsidianSkinButton.Tooltip = Lang.GetBuffName(1);
    ((UIElement) this.BuffPanel).Append((UIElement) this.ObsidianSkinButton);
    PermanentBuffUI.CreateBuffButton(this.PlentySatisfiedButton, 240f, 160f);
    // ISSUE: method pointer
    this.PlentySatisfiedButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(PlentySatisfiedClicked));
    this.PlentySatisfiedButton.Tooltip = Lang.GetBuffName(206);
    ((UIElement) this.BuffPanel).Append((UIElement) this.PlentySatisfiedButton);
    PermanentBuffUI.CreateBuffButton(this.RageButton, 272f, 160f);
    // ISSUE: method pointer
    this.RageButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(RageClicked));
    this.RageButton.Tooltip = Lang.GetBuffName(115);
    ((UIElement) this.BuffPanel).Append((UIElement) this.RageButton);
    PermanentBuffUI.CreateBuffButton(this.RegenerationButton, 304f, 160f);
    // ISSUE: method pointer
    this.RegenerationButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(RegenerationClicked));
    this.RegenerationButton.Tooltip = Lang.GetBuffName(2);
    ((UIElement) this.BuffPanel).Append((UIElement) this.RegenerationButton);
    PermanentBuffUI.CreateBuffButton(this.ShineButton, 16f, 192f);
    // ISSUE: method pointer
    this.ShineButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ShineClicked));
    this.ShineButton.Tooltip = Lang.GetBuffName(11);
    ((UIElement) this.BuffPanel).Append((UIElement) this.ShineButton);
    PermanentBuffUI.CreateBuffButton(this.SonarButton, 48f, 192f);
    // ISSUE: method pointer
    this.SonarButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SonarClicked));
    this.SonarButton.Tooltip = Lang.GetBuffName(122);
    ((UIElement) this.BuffPanel).Append((UIElement) this.SonarButton);
    PermanentBuffUI.CreateBuffButton(this.SpelunkerButton, 80f, 192f);
    // ISSUE: method pointer
    this.SpelunkerButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SpelunkerClicked));
    this.SpelunkerButton.Tooltip = Lang.GetBuffName(9);
    ((UIElement) this.BuffPanel).Append((UIElement) this.SpelunkerButton);
    PermanentBuffUI.CreateBuffButton(this.SummoningButton, 112f, 192f);
    // ISSUE: method pointer
    this.SummoningButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SummoningClicked));
    this.SummoningButton.Tooltip = Lang.GetBuffName(110);
    ((UIElement) this.BuffPanel).Append((UIElement) this.SummoningButton);
    PermanentBuffUI.CreateBuffButton(this.SwiftnessButton, 144f, 192f);
    // ISSUE: method pointer
    this.SwiftnessButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SwiftnessClicked));
    this.SwiftnessButton.Tooltip = Lang.GetBuffName(3);
    ((UIElement) this.BuffPanel).Append((UIElement) this.SwiftnessButton);
    PermanentBuffUI.CreateBuffButton(this.ThornsButton, 176f, 192f);
    // ISSUE: method pointer
    this.ThornsButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ThornsClicked));
    this.ThornsButton.Tooltip = Lang.GetBuffName(14);
    ((UIElement) this.BuffPanel).Append((UIElement) this.ThornsButton);
    PermanentBuffUI.CreateBuffButton(this.TipsyButton, 208f, 192f);
    // ISSUE: method pointer
    this.TipsyButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(TipsyClicked));
    this.TipsyButton.Tooltip = Lang.GetBuffName(25);
    ((UIElement) this.BuffPanel).Append((UIElement) this.TipsyButton);
    PermanentBuffUI.CreateBuffButton(this.TitanButton, 240f, 192f);
    // ISSUE: method pointer
    this.TitanButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(TitanClicked));
    this.TitanButton.Tooltip = Lang.GetBuffName(108);
    ((UIElement) this.BuffPanel).Append((UIElement) this.TitanButton);
    PermanentBuffUI.CreateBuffButton(this.WarmthButton, 272f, 192f);
    // ISSUE: method pointer
    this.WarmthButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WarmthClicked));
    this.WarmthButton.Tooltip = Lang.GetBuffName(124);
    ((UIElement) this.BuffPanel).Append((UIElement) this.WarmthButton);
    PermanentBuffUI.CreateBuffButton(this.WaterWalkingButton, 304f, 192f);
    // ISSUE: method pointer
    this.WaterWalkingButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WaterWalkingClicked));
    this.WaterWalkingButton.Tooltip = Lang.GetBuffName(15);
    ((UIElement) this.BuffPanel).Append((UIElement) this.WaterWalkingButton);
    PermanentBuffUI.CreateBuffButton(this.WellFedButton, 16f, 224f);
    // ISSUE: method pointer
    this.WellFedButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WellFedClicked));
    this.WellFedButton.Tooltip = Lang.GetBuffName(26);
    ((UIElement) this.BuffPanel).Append((UIElement) this.WellFedButton);
    PermanentBuffUI.CreateBuffButton(this.WrathButton, 48f, 224f);
    // ISSUE: method pointer
    this.WrathButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WrathClicked));
    this.WrathButton.Tooltip = Lang.GetBuffName(117);
    ((UIElement) this.BuffPanel).Append((UIElement) this.WrathButton);
    PermanentBuffUI.CreateBuffButton(this.AmmoBoxButton, 16f, 288f);
    // ISSUE: method pointer
    this.AmmoBoxButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(AmmoBoxClicked));
    this.AmmoBoxButton.Tooltip = Lang.GetBuffName(93);
    ((UIElement) this.BuffPanel).Append((UIElement) this.AmmoBoxButton);
    PermanentBuffUI.CreateBuffButton(this.BewitchingTableButton, 48f, 288f);
    // ISSUE: method pointer
    this.BewitchingTableButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BewitchingTableClicked));
    this.BewitchingTableButton.Tooltip = Lang.GetBuffName(150);
    ((UIElement) this.BuffPanel).Append((UIElement) this.BewitchingTableButton);
    PermanentBuffUI.CreateBuffButton(this.CrystalBallButton, 80f, 288f);
    // ISSUE: method pointer
    this.CrystalBallButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CrystalBallClicked));
    this.CrystalBallButton.Tooltip = Lang.GetBuffName(29);
    ((UIElement) this.BuffPanel).Append((UIElement) this.CrystalBallButton);
    PermanentBuffUI.CreateBuffButton(this.SharpeningStationButton, 112f, 288f);
    // ISSUE: method pointer
    this.SharpeningStationButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SharpeningStationClicked));
    this.SharpeningStationButton.Tooltip = Lang.GetBuffName(159);
    ((UIElement) this.BuffPanel).Append((UIElement) this.SharpeningStationButton);
    PermanentBuffUI.CreateBuffButton(this.SliceOfCakeButton, 144f, 288f);
    // ISSUE: method pointer
    this.SliceOfCakeButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SliceOfCakeClicked));
    this.SliceOfCakeButton.Tooltip = Lang.GetBuffName(192 /*0xC0*/);
    ((UIElement) this.BuffPanel).Append((UIElement) this.SliceOfCakeButton);
    PermanentBuffUI.CreateBuffButton(this.WarTableButton, 176f, 288f);
    // ISSUE: method pointer
    this.WarTableButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WarTableClicked));
    this.WarTableButton.Tooltip = Lang.GetBuffName(348);
    ((UIElement) this.BuffPanel).Append((UIElement) this.WarTableButton);
    this.allBuffButtons.UnionWith((IEnumerable<PermanentBuffButton>) new HashSet<PermanentBuffButton>()
    {
      this.BastStatueButton,
      this.CampfireButton,
      this.GardenGnomeButton,
      this.HeartLanternButton,
      this.HoneyButton,
      this.PeaceCandleButton,
      this.ShadowCandleButton,
      this.StarInABottleButton,
      this.SunflowerButton,
      this.WaterCandleButton,
      this.AmmoReservationButton,
      this.ArcheryButton,
      this.BattleButton,
      this.BiomeSightButton,
      this.BuilderButton,
      this.CalmButton,
      this.CrateButton,
      this.DangersenseButton,
      this.EnduranceButton,
      this.ExquisitelyStuffedButton,
      this.FeatherfallButton,
      this.FishingButton,
      this.FlipperButton,
      this.GillsButton,
      this.GravitationButton,
      this.HeartreachButton,
      this.HunterButton,
      this.InfernoButton,
      this.InvisibilityButton,
      this.IronskinButton,
      this.LifeforceButton,
      this.LuckyButton,
      this.MagicPowerButton,
      this.ManaRegenerationButton,
      this.MiningButton,
      this.NightOwlButton,
      this.ObsidianSkinButton,
      this.PlentySatisfiedButton,
      this.RageButton,
      this.RegenerationButton,
      this.ShineButton,
      this.SonarButton,
      this.SpelunkerButton,
      this.SummoningButton,
      this.SwiftnessButton,
      this.ThornsButton,
      this.TipsyButton,
      this.TitanButton,
      this.WarmthButton,
      this.WaterWalkingButton,
      this.WellFedButton,
      this.WrathButton,
      this.AmmoBoxButton,
      this.BewitchingTableButton,
      this.CrystalBallButton,
      this.SharpeningStationButton,
      this.SliceOfCakeButton,
      this.WarTableButton
    });
    ((UIElement) this).Append((UIElement) this.BuffPanel);
  }

  private void BastStatueClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(0, this.BastStatueButton, Lang.GetBuffName(215));
  }

  private void CampfireClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(1, this.CampfireButton, Lang.GetBuffName(87));
  }

  private void GardenGnomeClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(2, this.GardenGnomeButton, Lang.GetItemName(4609).ToString());
  }

  private void HeartLanternClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(3, this.HeartLanternButton, Lang.GetBuffName(89));
  }

  private void HoneyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(4, this.HoneyButton, Lang.GetBuffName(48 /*0x30*/));
  }

  private void PeaceCandleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(5, this.PeaceCandleButton, Lang.GetBuffName(157));
  }

  private void ShadowCandleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(6, this.ShadowCandleButton, Lang.GetBuffName(350));
  }

  private void StarInABottleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(7, this.StarInABottleButton, Lang.GetBuffName(158));
  }

  private void SunflowerClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(8, this.SunflowerButton, Lang.GetBuffName(146));
  }

  private void WaterCandleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(9, this.WaterCandleButton, Lang.GetBuffName(86));
  }

  private void AmmoReservationClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(10, this.AmmoReservationButton, Lang.GetBuffName(112 /*0x70*/));
  }

  private void ArcheryClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(11, this.ArcheryButton, Lang.GetBuffName(16 /*0x10*/));
  }

  private void BattleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(12, this.BattleButton, Lang.GetBuffName(13));
  }

  private void BiomeSightClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(13, this.BiomeSightButton, Lang.GetBuffName(343));
  }

  private void BuilderClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(14, this.BuilderButton, Lang.GetBuffName(107));
  }

  private void CalmClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(15, this.CalmButton, Lang.GetBuffName(106));
  }

  private void CrateClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(16 /*0x10*/, this.CrateButton, Lang.GetBuffName(123));
  }

  private void DangersenseClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(17, this.DangersenseButton, Lang.GetBuffName(111));
  }

  private void EnduranceClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(18, this.EnduranceButton, Lang.GetBuffName(114));
  }

  private void ExquisitelyStuffedClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(19, this.ExquisitelyStuffedButton, Lang.GetBuffName(207));
  }

  private void FeatherfallClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(20, this.FeatherfallButton, Lang.GetBuffName(8));
  }

  private void FishingClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(21, this.FishingButton, Lang.GetBuffName(121));
  }

  private void FlipperClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(22, this.FlipperButton, Lang.GetBuffName(109));
  }

  private void GillsClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(23, this.GillsButton, Lang.GetBuffName(4));
  }

  private void GravitationClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(24, this.GravitationButton, Lang.GetBuffName(18));
  }

  private void HeartreachClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(25, this.HeartreachButton, Lang.GetBuffName(105));
  }

  private void HunterClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(26, this.HunterButton, Lang.GetBuffName(17));
  }

  private void InfernoClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(27, this.InfernoButton, Lang.GetBuffName(116));
  }

  private void InvisibilityClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(28, this.InvisibilityButton, Lang.GetBuffName(10));
  }

  private void IronskinClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(29, this.IronskinButton, Lang.GetBuffName(5));
  }

  private void LifeforceClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(30, this.LifeforceButton, Lang.GetBuffName(113));
  }

  private void LuckyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(31 /*0x1F*/, this.LuckyButton, Lang.GetBuffName(257));
  }

  private void MagicPowerClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(32 /*0x20*/, this.MagicPowerButton, Lang.GetBuffName(7));
  }

  private void ManaRegenerationClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(33, this.ManaRegenerationButton, Lang.GetBuffName(6));
  }

  private void MiningClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(34, this.MiningButton, Lang.GetBuffName(104));
  }

  private void NightOwlClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(35, this.NightOwlButton, Lang.GetBuffName(12));
  }

  private void ObsidianSkinClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(36, this.ObsidianSkinButton, Lang.GetBuffName(1));
  }

  private void PlentySatisfiedClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(37, this.PlentySatisfiedButton, Lang.GetBuffName(206));
  }

  private void RageClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(38, this.RageButton, Lang.GetBuffName(115));
  }

  private void RegenerationClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(39, this.RegenerationButton, Lang.GetBuffName(2));
  }

  private void ShineClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(40, this.ShineButton, Lang.GetBuffName(11));
  }

  private void SonarClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(41, this.SonarButton, Lang.GetBuffName(122));
  }

  private void SpelunkerClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(42, this.SpelunkerButton, Lang.GetBuffName(9));
  }

  private void SummoningClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(43, this.SummoningButton, Lang.GetBuffName(110));
  }

  private void SwiftnessClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(44, this.SwiftnessButton, Lang.GetBuffName(3));
  }

  private void ThornsClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(45, this.ThornsButton, Lang.GetBuffName(14));
  }

  private void TipsyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(46, this.TipsyButton, Lang.GetBuffName(25));
  }

  private void TitanClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(47, this.TitanButton, Lang.GetBuffName(108));
  }

  private void WarmthClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(48 /*0x30*/, this.WarmthButton, Lang.GetBuffName(124));
  }

  private void WaterWalkingClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(49, this.WaterWalkingButton, Lang.GetBuffName(15));
  }

  private void WellFedClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(50, this.WellFedButton, Lang.GetBuffName(26));
  }

  private void WrathClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(51, this.WrathButton, Lang.GetBuffName(117));
  }

  private void AmmoBoxClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(52, this.AmmoBoxButton, Lang.GetBuffName(93));
  }

  private void BewitchingTableClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(53, this.BewitchingTableButton, Lang.GetBuffName(150));
  }

  private void CrystalBallClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(54, this.CrystalBallButton, Lang.GetBuffName(29));
  }

  private void SharpeningStationClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(55, this.SharpeningStationButton, Lang.GetBuffName(159));
  }

  private void SliceOfCakeClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(56, this.SliceOfCakeButton, Lang.GetBuffName(192 /*0xC0*/));
  }

  private void WarTableClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffUI.BuffClick(57, this.WarTableButton, Lang.GetBuffName(348));
  }

  public virtual void Update(GameTime gameTime)
  {
    if (!PermanentBuffUI.visible)
      return;
    for (int index = 0; index < PermanentBuffPlayer.PermanentBuffsBools.Length; ++index)
      this.allBuffButtons.ElementAt<PermanentBuffButton>(index).disabled = PermanentBuffPlayer.PermanentBuffsBools[index];
  }

  public static void BuffClick(int buff, PermanentBuffButton button, string name)
  {
    if (Main.GameUpdateCount - PermanentBuffUI.timeStart < 10U)
      return;
    PermanentBuffPlayer.PermanentBuffsBools[buff] = !PermanentBuffPlayer.PermanentBuffsBools[buff];
    button.disabled = PermanentBuffPlayer.PermanentBuffsBools[buff];
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
