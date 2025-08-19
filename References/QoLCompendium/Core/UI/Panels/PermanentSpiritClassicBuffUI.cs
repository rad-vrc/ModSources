// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.PermanentSpiritClassicBuffUI
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

public class PermanentSpiritClassicBuffUI : UIState
{
  public UIPanel BuffPanel;
  public static bool visible = false;
  public static uint timeStart;
  public static PermanentBuffButton CoiledEnergizerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton KoiTotemButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton SunPotButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton TheCouchButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton JumpButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton MirrorCoatButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton MoonJellyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton RunescribeButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton SoulguardButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton SpiritButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton SoaringButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton SporecoidButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton StarburnButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton SteadfastButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton ToxinButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton ZephyrButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
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
    ((StyleDimension) ref ((UIElement) this.BuffPanel).Height).Set(176f, 0.0f);
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
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.CoiledEnergizerButton, 16f, 32f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.CoiledEnergizerButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CoiledEnergizerClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.CoiledEnergizerButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.KoiTotemButton, 48f, 32f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.KoiTotemButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(KoiTotemClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.KoiTotemButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.SunPotButton, 80f, 32f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.SunPotButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SunPotClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.SunPotButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.TheCouchButton, 112f, 32f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.TheCouchButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(TheCouchClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.TheCouchButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.JumpButton, 16f, 96f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.JumpButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(JumpClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.JumpButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.MirrorCoatButton, 48f, 96f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.MirrorCoatButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MirrorCoatClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.MirrorCoatButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.MoonJellyButton, 80f, 96f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.MoonJellyButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MoonJellyClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.MoonJellyButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.RunescribeButton, 112f, 96f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.RunescribeButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(RunescribeClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.RunescribeButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.SoulguardButton, 144f, 96f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.SoulguardButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SoulguardClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.SoulguardButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.SpiritButton, 176f, 96f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.SpiritButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SpiritClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.SpiritButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.SoaringButton, 208f, 96f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.SoaringButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SoaringClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.SoaringButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.SporecoidButton, 240f, 96f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.SporecoidButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SporecoidClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.SporecoidButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.StarburnButton, 272f, 96f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.StarburnButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(StarburnClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.StarburnButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.SteadfastButton, 304f, 96f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.SteadfastButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SteadfastClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.SteadfastButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.ToxinButton, 16f, 128f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.ToxinButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ToxinClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.ToxinButton);
    PermanentSpiritClassicBuffUI.CreateBuffButton(PermanentSpiritClassicBuffUI.ZephyrButton, 48f, 128f);
    // ISSUE: method pointer
    PermanentSpiritClassicBuffUI.ZephyrButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ZephyrClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSpiritClassicBuffUI.ZephyrButton);
    this.allBuffButtons.UnionWith((IEnumerable<PermanentBuffButton>) new HashSet<PermanentBuffButton>()
    {
      PermanentSpiritClassicBuffUI.CoiledEnergizerButton,
      PermanentSpiritClassicBuffUI.KoiTotemButton,
      PermanentSpiritClassicBuffUI.SunPotButton,
      PermanentSpiritClassicBuffUI.TheCouchButton,
      PermanentSpiritClassicBuffUI.JumpButton,
      PermanentSpiritClassicBuffUI.MirrorCoatButton,
      PermanentSpiritClassicBuffUI.MoonJellyButton,
      PermanentSpiritClassicBuffUI.RunescribeButton,
      PermanentSpiritClassicBuffUI.SoulguardButton,
      PermanentSpiritClassicBuffUI.SpiritButton,
      PermanentSpiritClassicBuffUI.SoaringButton,
      PermanentSpiritClassicBuffUI.SporecoidButton,
      PermanentSpiritClassicBuffUI.StarburnButton,
      PermanentSpiritClassicBuffUI.SteadfastButton,
      PermanentSpiritClassicBuffUI.ToxinButton,
      PermanentSpiritClassicBuffUI.ZephyrButton
    });
    ((UIElement) this).Append((UIElement) this.BuffPanel);
  }

  private void CoiledEnergizerClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(0, PermanentSpiritClassicBuffUI.CoiledEnergizerButton);
  }

  private void KoiTotemClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(1, PermanentSpiritClassicBuffUI.KoiTotemButton);
  }

  private void SunPotClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(2, PermanentSpiritClassicBuffUI.SunPotButton);
  }

  private void TheCouchClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(3, PermanentSpiritClassicBuffUI.TheCouchButton);
  }

  private void JumpClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(4, PermanentSpiritClassicBuffUI.JumpButton);
  }

  private void MirrorCoatClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(5, PermanentSpiritClassicBuffUI.MirrorCoatButton);
  }

  private void MoonJellyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(6, PermanentSpiritClassicBuffUI.MoonJellyButton);
  }

  private void RunescribeClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(7, PermanentSpiritClassicBuffUI.RunescribeButton);
  }

  private void SoulguardClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(8, PermanentSpiritClassicBuffUI.SoulguardButton);
  }

  private void SpiritClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(9, PermanentSpiritClassicBuffUI.SpiritButton);
  }

  private void SoaringClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(10, PermanentSpiritClassicBuffUI.SoaringButton);
  }

  private void SporecoidClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(11, PermanentSpiritClassicBuffUI.SporecoidButton);
  }

  private void StarburnClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(12, PermanentSpiritClassicBuffUI.StarburnButton);
  }

  private void SteadfastClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(13, PermanentSpiritClassicBuffUI.SteadfastButton);
  }

  private void ToxinClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(14, PermanentSpiritClassicBuffUI.ToxinButton);
  }

  private void ZephyrClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSpiritClassicBuffUI.BuffClick(15, PermanentSpiritClassicBuffUI.ZephyrButton);
  }

  public static void GetSpiritClassicBuffData()
  {
    if (!ModConditions.spiritLoaded)
      return;
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
    PermanentSpiritClassicBuffUI.CoiledEnergizerButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "OverDrive")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.KoiTotemButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "KoiTotemBuff")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.SunPotButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "SunPotBuff")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.TheCouchButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "CouchPotato")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.JumpButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "PinkPotionBuff")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.MirrorCoatButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "MirrorCoatBuff")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.MoonJellyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "MoonBlessing")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.RunescribeButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "RunePotionBuff")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.SoulguardButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "SoulPotionBuff")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.SpiritButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "SpiritBuff")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.SoaringButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "FlightPotionBuff")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.SporecoidButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "MushroomPotionBuff")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.StarburnButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "StarPotionBuff")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.SteadfastButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "TurtlePotionBuff")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.ToxinButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "BismitePotionBuff")), (AssetRequestMode) 2);
    PermanentSpiritClassicBuffUI.ZephyrButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.spiritMod, Common.GetModBuff(ModConditions.spiritMod, "DoubleJumpPotionBuff")), (AssetRequestMode) 2);
  }

  public virtual void Update(GameTime gameTime)
  {
    if (!PermanentSpiritClassicBuffUI.visible)
      return;
    for (int index = 0; index < PermanentBuffPlayer.PermanentSpiritClassicBuffsBools.Length; ++index)
    {
      this.allBuffButtons.ElementAt<PermanentBuffButton>(index).disabled = PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[index];
      this.allBuffButtons.ElementAt<PermanentBuffButton>(index).moddedBuff = true;
    }
  }

  public static void BuffClick(int buff, PermanentBuffButton button)
  {
    if (Main.GameUpdateCount - PermanentSpiritClassicBuffUI.timeStart < 10U)
      return;
    PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[buff] = !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[buff];
    button.disabled = PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[buff];
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
