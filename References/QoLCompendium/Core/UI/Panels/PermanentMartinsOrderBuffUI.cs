// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.PermanentMartinsOrderBuffUI
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

public class PermanentMartinsOrderBuffUI : UIState
{
  public UIPanel BuffPanel;
  public static bool visible = false;
  public static uint timeStart;
  public static PermanentBuffButton BlackHoleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton ChargingButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton DefenderButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton EmpowermentButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton EvocationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton GourmetFlavorButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton HasteButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton HealingButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton RockskinButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton ShooterButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton SoulButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton SpellCasterButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton StarreachButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton SweeperButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton ThrowerButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton WhipperButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton ZincPillButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton ArcheologyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton SporeFarmButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
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
    UIText uiText1 = new UIText(UISystem.PotionText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText1).Left).Set(16f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText1).Top).Set(8f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText1).Width).Set(64f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText1).Height).Set(32f, 0.0f);
    ((UIElement) this.BuffPanel).Append((UIElement) uiText1);
    UIText uiText2 = new UIText(UISystem.StationText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText2).Left).Set(16f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Top).Set(104f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Width).Set(64f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Height).Set(32f, 0.0f);
    ((UIElement) this.BuffPanel).Append((UIElement) uiText2);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.BlackHoleButton, 16f, 32f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.BlackHoleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BlackHoleClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.BlackHoleButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.ChargingButton, 48f, 32f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.ChargingButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ChargingClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.ChargingButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.DefenderButton, 80f, 32f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.DefenderButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DefenderClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.DefenderButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.EmpowermentButton, 112f, 32f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.EmpowermentButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(EmpowermentClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.EmpowermentButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.EvocationButton, 144f, 32f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.EvocationButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(EvocationClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.EvocationButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.GourmetFlavorButton, 176f, 32f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.GourmetFlavorButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(GourmetFlavorClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.GourmetFlavorButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.HasteButton, 208f, 32f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.HasteButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(HasteClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.HasteButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.HealingButton, 240f, 32f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.HealingButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(HealingClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.HealingButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.RockskinButton, 272f, 32f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.RockskinButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(RockskinClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.RockskinButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.ShooterButton, 304f, 32f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.ShooterButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ShooterClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.ShooterButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.SoulButton, 16f, 64f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.SoulButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SoulClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.SoulButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.SpellCasterButton, 48f, 64f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.SpellCasterButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SpellCasterClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.SpellCasterButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.StarreachButton, 80f, 64f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.StarreachButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(StarreachClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.StarreachButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.SweeperButton, 112f, 64f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.SweeperButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SweeperClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.SweeperButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.ThrowerButton, 144f, 64f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.ThrowerButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ThrowerClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.ThrowerButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.WhipperButton, 176f, 64f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.WhipperButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WhipperClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.WhipperButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.ZincPillButton, 208f, 64f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.ZincPillButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ZincPillClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.ZincPillButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.ArcheologyButton, 16f, 128f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.ArcheologyButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ArcheologyClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.ArcheologyButton);
    PermanentMartinsOrderBuffUI.CreateBuffButton(PermanentMartinsOrderBuffUI.SporeFarmButton, 48f, 128f);
    // ISSUE: method pointer
    PermanentMartinsOrderBuffUI.SporeFarmButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SporeFarmClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentMartinsOrderBuffUI.SporeFarmButton);
    this.allBuffButtons.UnionWith((IEnumerable<PermanentBuffButton>) new HashSet<PermanentBuffButton>()
    {
      PermanentMartinsOrderBuffUI.BlackHoleButton,
      PermanentMartinsOrderBuffUI.ChargingButton,
      PermanentMartinsOrderBuffUI.DefenderButton,
      PermanentMartinsOrderBuffUI.EmpowermentButton,
      PermanentMartinsOrderBuffUI.EvocationButton,
      PermanentMartinsOrderBuffUI.GourmetFlavorButton,
      PermanentMartinsOrderBuffUI.HasteButton,
      PermanentMartinsOrderBuffUI.HealingButton,
      PermanentMartinsOrderBuffUI.RockskinButton,
      PermanentMartinsOrderBuffUI.ShooterButton,
      PermanentMartinsOrderBuffUI.SoulButton,
      PermanentMartinsOrderBuffUI.SpellCasterButton,
      PermanentMartinsOrderBuffUI.StarreachButton,
      PermanentMartinsOrderBuffUI.SweeperButton,
      PermanentMartinsOrderBuffUI.ThrowerButton,
      PermanentMartinsOrderBuffUI.WhipperButton,
      PermanentMartinsOrderBuffUI.ZincPillButton,
      PermanentMartinsOrderBuffUI.ArcheologyButton,
      PermanentMartinsOrderBuffUI.SporeFarmButton
    });
    ((UIElement) this).Append((UIElement) this.BuffPanel);
  }

  private void BlackHoleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(0, PermanentMartinsOrderBuffUI.BlackHoleButton);
  }

  private void ChargingClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(1, PermanentMartinsOrderBuffUI.ChargingButton);
  }

  private void DefenderClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(2, PermanentMartinsOrderBuffUI.DefenderButton);
  }

  private void EmpowermentClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(3, PermanentMartinsOrderBuffUI.EmpowermentButton);
  }

  private void EvocationClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(4, PermanentMartinsOrderBuffUI.EvocationButton);
  }

  private void GourmetFlavorClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(5, PermanentMartinsOrderBuffUI.GourmetFlavorButton);
  }

  private void HasteClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(6, PermanentMartinsOrderBuffUI.HasteButton);
  }

  private void HealingClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(7, PermanentMartinsOrderBuffUI.HealingButton);
  }

  private void RockskinClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(8, PermanentMartinsOrderBuffUI.RockskinButton);
  }

  private void ShooterClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(9, PermanentMartinsOrderBuffUI.ShooterButton);
  }

  private void SoulClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(10, PermanentMartinsOrderBuffUI.SoulButton);
  }

  private void SpellCasterClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(11, PermanentMartinsOrderBuffUI.SpellCasterButton);
  }

  private void StarreachClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(12, PermanentMartinsOrderBuffUI.StarreachButton);
  }

  private void SweeperClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(13, PermanentMartinsOrderBuffUI.SweeperButton);
  }

  private void ThrowerClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(14, PermanentMartinsOrderBuffUI.ThrowerButton);
  }

  private void WhipperClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(15, PermanentMartinsOrderBuffUI.WhipperButton);
  }

  private void ZincPillClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(16 /*0x10*/, PermanentMartinsOrderBuffUI.ZincPillButton);
  }

  private void ArcheologyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(17, PermanentMartinsOrderBuffUI.ArcheologyButton);
  }

  private void SporeFarmClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentMartinsOrderBuffUI.BuffClick(18, PermanentMartinsOrderBuffUI.SporeFarmButton);
  }

  public static void GetMartinsOrderBuffData()
  {
    if (!ModConditions.martainsOrderLoaded)
      return;
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
    PermanentMartinsOrderBuffUI.BlackHoleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "BlackHoleBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.ChargingButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "Charging")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.DefenderButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "TurretBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.EmpowermentButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "EmpowermentBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.EvocationButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "SummonSpeedBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.GourmetFlavorButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "Gourmet")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.HasteButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "HasteBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.HealingButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "Healing")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.RockskinButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "RockskinBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.ShooterButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "ShooterBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.SoulButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "SoulBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.SpellCasterButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "CasterBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.StarreachButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "Starreach")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.SweeperButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "SweepBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.ThrowerButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "ThrowerBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.WhipperButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "WhipperBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.ZincPillButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "ZincPillBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.ArcheologyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "ReschBuff")), (AssetRequestMode) 2);
    PermanentMartinsOrderBuffUI.SporeFarmButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.martainsOrderMod, Common.GetModBuff(ModConditions.martainsOrderMod, "SporeSave")), (AssetRequestMode) 2);
  }

  public virtual void Update(GameTime gameTime)
  {
    if (!PermanentMartinsOrderBuffUI.visible)
      return;
    for (int index = 0; index < PermanentBuffPlayer.PermanentMartinsOrderBuffsBools.Length; ++index)
    {
      this.allBuffButtons.ElementAt<PermanentBuffButton>(index).disabled = PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[index];
      this.allBuffButtons.ElementAt<PermanentBuffButton>(index).moddedBuff = true;
    }
  }

  public static void BuffClick(int buff, PermanentBuffButton button)
  {
    if (Main.GameUpdateCount - PermanentMartinsOrderBuffUI.timeStart < 10U)
      return;
    PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[buff] = !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[buff];
    button.disabled = PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[buff];
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
