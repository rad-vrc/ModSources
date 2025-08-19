// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.PermanentThoriumBuffUI
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

public class PermanentThoriumBuffUI : UIState
{
  public UIPanel BuffPanel;
  public static bool visible = false;
  public static uint timeStart;
  public static PermanentBuffButton MistletoeButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton AquaAffinityButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton ArcaneButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton ArtilleryButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton AssassinButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton BloodRushButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton BouncingFlameButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton CactusFruitButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton ConflagrationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton CreativityButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton EarwormButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton FrenzyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton GlowingButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton HolyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton HydrationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton InspirationalReachButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton KineticButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton WarmongerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton BatRepellentButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton FishRepellentButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton InsectRepellentButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton SkeletonRepellentButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton ZombieRepellentButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton AltarButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton ConductorsStandButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton NinjaRackButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton DeathsingerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton InspirationRegenerationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
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
    ((StyleDimension) ref ((UIElement) this.BuffPanel).Height).Set(368f, 0.0f);
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
    UIText uiText3 = new UIText(UISystem.RepellentText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText3).Left).Set(16f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Top).Set(168f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Width).Set(64f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Height).Set(32f, 0.0f);
    ((UIElement) this.BuffPanel).Append((UIElement) uiText3);
    UIText uiText4 = new UIText(UISystem.StationText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText4).Left).Set(16f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText4).Top).Set(232f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText4).Width).Set(64f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText4).Height).Set(32f, 0.0f);
    ((UIElement) this.BuffPanel).Append((UIElement) uiText4);
    UIText uiText5 = new UIText(UISystem.AddonText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText5).Left).Set(16f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText5).Top).Set(296f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText5).Width).Set(64f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText5).Height).Set(32f, 0.0f);
    ((UIElement) this.BuffPanel).Append((UIElement) uiText5);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.MistletoeButton, 16f, 32f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.MistletoeButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MistletoeClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.MistletoeButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.AquaAffinityButton, 16f, 96f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.AquaAffinityButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(AquaAffinityClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.AquaAffinityButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.ArcaneButton, 48f, 96f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.ArcaneButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ArcaneClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.ArcaneButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.ArtilleryButton, 80f, 96f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.ArtilleryButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ArtilleryClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.ArtilleryButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.AssassinButton, 112f, 96f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.AssassinButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(AssassinClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.AssassinButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.BloodRushButton, 144f, 96f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.BloodRushButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BloodRushClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.BloodRushButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.BouncingFlameButton, 176f, 96f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.BouncingFlameButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BouncingFlameClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.BouncingFlameButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.CactusFruitButton, 208f, 96f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.CactusFruitButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CactusFruitClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.CactusFruitButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.ConflagrationButton, 240f, 96f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.ConflagrationButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ConflagrationClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.ConflagrationButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.CreativityButton, 272f, 96f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.CreativityButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CreativityClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.CreativityButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.EarwormButton, 304f, 96f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.EarwormButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(EarwormClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.EarwormButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.FrenzyButton, 16f, 128f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.FrenzyButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(FrenzyClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.FrenzyButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.GlowingButton, 48f, 128f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.GlowingButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(GlowingClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.GlowingButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.HolyButton, 80f, 128f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.HolyButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(HolyClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.HolyButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.HydrationButton, 112f, 128f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.HydrationButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(HydrationClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.HydrationButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.InspirationalReachButton, 144f, 128f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.InspirationalReachButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(InspirationalReachClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.InspirationalReachButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.KineticButton, 176f, 128f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.KineticButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(KineticClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.KineticButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.WarmongerButton, 208f, 128f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.WarmongerButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WarmongerClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.WarmongerButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.BatRepellentButton, 16f, 192f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.BatRepellentButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BatRepellentClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.BatRepellentButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.FishRepellentButton, 48f, 192f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.FishRepellentButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(FishRepellentClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.FishRepellentButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.InsectRepellentButton, 80f, 192f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.InsectRepellentButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(InsectRepellentClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.InsectRepellentButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.SkeletonRepellentButton, 112f, 192f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.SkeletonRepellentButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SkeletonRepellentClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.SkeletonRepellentButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.ZombieRepellentButton, 144f, 192f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.ZombieRepellentButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ZombieRepellentClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.ZombieRepellentButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.AltarButton, 16f, 256f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.AltarButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(AltarClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.AltarButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.ConductorsStandButton, 48f, 256f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.ConductorsStandButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ConductorsStandClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.ConductorsStandButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.NinjaRackButton, 80f, 256f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.NinjaRackButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(NinjaRackClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.NinjaRackButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.DeathsingerButton, 16f, 320f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.DeathsingerButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DeathsingerClicked));
    PermanentThoriumBuffUI.DeathsingerButton.ModTooltip = UISystem.UnloadedText;
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.DeathsingerButton);
    PermanentThoriumBuffUI.CreateBuffButton(PermanentThoriumBuffUI.InspirationRegenerationButton, 48f, 320f);
    // ISSUE: method pointer
    PermanentThoriumBuffUI.InspirationRegenerationButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(InspirationRegenerationClicked));
    PermanentThoriumBuffUI.InspirationRegenerationButton.ModTooltip = UISystem.UnloadedText;
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentThoriumBuffUI.InspirationRegenerationButton);
    this.allBuffButtons.UnionWith((IEnumerable<PermanentBuffButton>) new HashSet<PermanentBuffButton>()
    {
      PermanentThoriumBuffUI.MistletoeButton,
      PermanentThoriumBuffUI.AquaAffinityButton,
      PermanentThoriumBuffUI.ArcaneButton,
      PermanentThoriumBuffUI.ArtilleryButton,
      PermanentThoriumBuffUI.AssassinButton,
      PermanentThoriumBuffUI.BloodRushButton,
      PermanentThoriumBuffUI.BouncingFlameButton,
      PermanentThoriumBuffUI.CactusFruitButton,
      PermanentThoriumBuffUI.ConflagrationButton,
      PermanentThoriumBuffUI.CreativityButton,
      PermanentThoriumBuffUI.EarwormButton,
      PermanentThoriumBuffUI.FrenzyButton,
      PermanentThoriumBuffUI.GlowingButton,
      PermanentThoriumBuffUI.HolyButton,
      PermanentThoriumBuffUI.HydrationButton,
      PermanentThoriumBuffUI.InspirationalReachButton,
      PermanentThoriumBuffUI.KineticButton,
      PermanentThoriumBuffUI.WarmongerButton,
      PermanentThoriumBuffUI.BatRepellentButton,
      PermanentThoriumBuffUI.FishRepellentButton,
      PermanentThoriumBuffUI.InsectRepellentButton,
      PermanentThoriumBuffUI.SkeletonRepellentButton,
      PermanentThoriumBuffUI.ZombieRepellentButton,
      PermanentThoriumBuffUI.AltarButton,
      PermanentThoriumBuffUI.ConductorsStandButton,
      PermanentThoriumBuffUI.NinjaRackButton,
      PermanentThoriumBuffUI.DeathsingerButton,
      PermanentThoriumBuffUI.InspirationRegenerationButton
    });
    ((UIElement) this).Append((UIElement) this.BuffPanel);
  }

  private void MistletoeClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(0, PermanentThoriumBuffUI.MistletoeButton);
  }

  private void AquaAffinityClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(1, PermanentThoriumBuffUI.AquaAffinityButton);
  }

  private void ArcaneClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(2, PermanentThoriumBuffUI.ArcaneButton);
  }

  private void ArtilleryClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(3, PermanentThoriumBuffUI.ArtilleryButton);
  }

  private void AssassinClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(4, PermanentThoriumBuffUI.AssassinButton);
  }

  private void BloodRushClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(5, PermanentThoriumBuffUI.BloodRushButton);
  }

  private void BouncingFlameClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(6, PermanentThoriumBuffUI.BouncingFlameButton);
  }

  private void CactusFruitClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(7, PermanentThoriumBuffUI.CactusFruitButton);
  }

  private void ConflagrationClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(8, PermanentThoriumBuffUI.ConflagrationButton);
  }

  private void CreativityClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(9, PermanentThoriumBuffUI.CreativityButton);
  }

  private void EarwormClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(10, PermanentThoriumBuffUI.EarwormButton);
  }

  private void FrenzyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(11, PermanentThoriumBuffUI.FrenzyButton);
  }

  private void GlowingClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(12, PermanentThoriumBuffUI.GlowingButton);
  }

  private void HolyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(13, PermanentThoriumBuffUI.HolyButton);
  }

  private void HydrationClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(14, PermanentThoriumBuffUI.HydrationButton);
  }

  private void InspirationalReachClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(15, PermanentThoriumBuffUI.InspirationalReachButton);
  }

  private void KineticClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(16 /*0x10*/, PermanentThoriumBuffUI.KineticButton);
  }

  private void WarmongerClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(17, PermanentThoriumBuffUI.WarmongerButton);
  }

  private void BatRepellentClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(18, PermanentThoriumBuffUI.BatRepellentButton);
  }

  private void FishRepellentClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(19, PermanentThoriumBuffUI.FishRepellentButton);
  }

  private void InsectRepellentClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(20, PermanentThoriumBuffUI.InsectRepellentButton);
  }

  private void SkeletonRepellentClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(21, PermanentThoriumBuffUI.SkeletonRepellentButton);
  }

  private void ZombieRepellentClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(22, PermanentThoriumBuffUI.ZombieRepellentButton);
  }

  private void AltarClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(23, PermanentThoriumBuffUI.AltarButton);
  }

  private void ConductorsStandClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(24, PermanentThoriumBuffUI.ConductorsStandButton);
  }

  private void NinjaRackClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(25, PermanentThoriumBuffUI.NinjaRackButton);
  }

  private void DeathsingerClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(26, PermanentThoriumBuffUI.DeathsingerButton);
  }

  private void InspirationRegenerationClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentThoriumBuffUI.BuffClick(27, PermanentThoriumBuffUI.InspirationRegenerationButton);
  }

  public static void GetThoriumBuffData()
  {
    if (!ModConditions.thoriumLoaded)
      return;
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
    PermanentThoriumBuffUI.MistletoeButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "MistletoeBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.AquaAffinityButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "AquaAffinity")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.ArcaneButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "ArcanePotionBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.ArtilleryButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "ArtilleryBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.AssassinButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "AssassinPotionBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.BloodRushButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "BloodRush")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.BouncingFlameButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "BouncingFlamePotionBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.CactusFruitButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "CactusFruitBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.ConflagrationButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "ConflagrationPotionBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.CreativityButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "CreativityPotionBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.EarwormButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "EarwormPotionBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.FrenzyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "FrenzyPotionBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.GlowingButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "GlowingPotionBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.HolyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "HolyPotionBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.HydrationButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "HydrationBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.InspirationalReachButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "InspirationReachPotionBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.KineticButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "KineticPotionBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.WarmongerButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "WarmongerBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.BatRepellentButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "BatRepellentBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.FishRepellentButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "FishRepellentBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.InsectRepellentButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "InsectRepellentBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.SkeletonRepellentButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "SkeletonRepellentBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.ZombieRepellentButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "ZombieRepellentBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.AltarButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "AltarBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.ConductorsStandButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "ConductorsStandBuff")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.NinjaRackButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumMod, Common.GetModBuff(ModConditions.thoriumMod, "NinjaBuff")), (AssetRequestMode) 2);
    if (!ModConditions.thoriumBossReworkLoaded)
      return;
    PermanentThoriumBuffUI.DeathsingerButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumBossReworkMod, Common.GetModBuff(ModConditions.thoriumBossReworkMod, "Deathsinger")), (AssetRequestMode) 2);
    PermanentThoriumBuffUI.InspirationRegenerationButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.thoriumBossReworkMod, Common.GetModBuff(ModConditions.thoriumBossReworkMod, "Inspired")), (AssetRequestMode) 2);
  }

  public virtual void Update(GameTime gameTime)
  {
    if (!PermanentThoriumBuffUI.visible)
      return;
    for (int index = 0; index < PermanentBuffPlayer.PermanentThoriumBuffsBools.Length; ++index)
    {
      this.allBuffButtons.ElementAt<PermanentBuffButton>(index).disabled = PermanentBuffPlayer.PermanentThoriumBuffsBools[index];
      this.allBuffButtons.ElementAt<PermanentBuffButton>(index).moddedBuff = true;
    }
  }

  public static void BuffClick(int buff, PermanentBuffButton button)
  {
    if (Main.GameUpdateCount - PermanentThoriumBuffUI.timeStart < 10U)
      return;
    PermanentBuffPlayer.PermanentThoriumBuffsBools[buff] = !PermanentBuffPlayer.PermanentThoriumBuffsBools[buff];
    button.disabled = PermanentBuffPlayer.PermanentThoriumBuffsBools[buff];
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
