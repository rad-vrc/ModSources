// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.PermanentSOTSBuffUI
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

public class PermanentSOTSBuffUI : UIState
{
  public UIPanel BuffPanel;
  public static bool visible = false;
  public static uint timeStart;
  public static PermanentBuffButton AssassinationButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton BluefireButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton BrittleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton DoubleVisionButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton HarmonyButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton NightmareButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton RippleButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton RoughskinButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton SoulAccessButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton VibeButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
  public static PermanentBuffButton DigitalDisplayButton = new PermanentBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2));
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
    ((StyleDimension) ref ((UIElement) this.BuffPanel).Height).Set(144f, 0.0f);
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
    ((StyleDimension) ref ((UIElement) uiText2).Top).Set(72f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Width).Set(64f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Height).Set(32f, 0.0f);
    ((UIElement) this.BuffPanel).Append((UIElement) uiText2);
    PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.AssassinationButton, 16f, 32f);
    // ISSUE: method pointer
    PermanentSOTSBuffUI.AssassinationButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(AssassinationClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSOTSBuffUI.AssassinationButton);
    PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.BluefireButton, 48f, 32f);
    // ISSUE: method pointer
    PermanentSOTSBuffUI.BluefireButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BluefireClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSOTSBuffUI.BluefireButton);
    PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.BrittleButton, 80f, 32f);
    // ISSUE: method pointer
    PermanentSOTSBuffUI.BrittleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BrittleClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSOTSBuffUI.BrittleButton);
    PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.DoubleVisionButton, 112f, 32f);
    // ISSUE: method pointer
    PermanentSOTSBuffUI.DoubleVisionButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DoubleVisionClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSOTSBuffUI.DoubleVisionButton);
    PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.HarmonyButton, 144f, 32f);
    // ISSUE: method pointer
    PermanentSOTSBuffUI.HarmonyButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(HarmonyClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSOTSBuffUI.HarmonyButton);
    PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.NightmareButton, 176f, 32f);
    // ISSUE: method pointer
    PermanentSOTSBuffUI.NightmareButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(NightmareClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSOTSBuffUI.NightmareButton);
    PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.RippleButton, 208f, 32f);
    // ISSUE: method pointer
    PermanentSOTSBuffUI.RippleButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(RippleClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSOTSBuffUI.RippleButton);
    PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.RoughskinButton, 240f, 32f);
    // ISSUE: method pointer
    PermanentSOTSBuffUI.RoughskinButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(RoughskinClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSOTSBuffUI.RoughskinButton);
    PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.SoulAccessButton, 272f, 32f);
    // ISSUE: method pointer
    PermanentSOTSBuffUI.SoulAccessButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SoulAccessClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSOTSBuffUI.SoulAccessButton);
    PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.VibeButton, 304f, 32f);
    // ISSUE: method pointer
    PermanentSOTSBuffUI.VibeButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(VibeClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSOTSBuffUI.VibeButton);
    PermanentSOTSBuffUI.CreateBuffButton(PermanentSOTSBuffUI.DigitalDisplayButton, 16f, 96f);
    // ISSUE: method pointer
    PermanentSOTSBuffUI.DigitalDisplayButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DigitalDisplayClicked));
    ((UIElement) this.BuffPanel).Append((UIElement) PermanentSOTSBuffUI.DigitalDisplayButton);
    this.allBuffButtons.UnionWith((IEnumerable<PermanentBuffButton>) new HashSet<PermanentBuffButton>()
    {
      PermanentSOTSBuffUI.AssassinationButton,
      PermanentSOTSBuffUI.BluefireButton,
      PermanentSOTSBuffUI.BrittleButton,
      PermanentSOTSBuffUI.DoubleVisionButton,
      PermanentSOTSBuffUI.HarmonyButton,
      PermanentSOTSBuffUI.NightmareButton,
      PermanentSOTSBuffUI.RippleButton,
      PermanentSOTSBuffUI.RoughskinButton,
      PermanentSOTSBuffUI.SoulAccessButton,
      PermanentSOTSBuffUI.VibeButton,
      PermanentSOTSBuffUI.DigitalDisplayButton
    });
    ((UIElement) this).Append((UIElement) this.BuffPanel);
  }

  private void AssassinationClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSOTSBuffUI.BuffClick(0, PermanentSOTSBuffUI.AssassinationButton);
  }

  private void BluefireClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSOTSBuffUI.BuffClick(1, PermanentSOTSBuffUI.BluefireButton);
  }

  private void BrittleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSOTSBuffUI.BuffClick(2, PermanentSOTSBuffUI.BrittleButton);
  }

  private void DoubleVisionClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSOTSBuffUI.BuffClick(3, PermanentSOTSBuffUI.DoubleVisionButton);
  }

  private void HarmonyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSOTSBuffUI.BuffClick(4, PermanentSOTSBuffUI.HarmonyButton);
  }

  private void NightmareClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSOTSBuffUI.BuffClick(5, PermanentSOTSBuffUI.NightmareButton);
  }

  private void RippleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSOTSBuffUI.BuffClick(6, PermanentSOTSBuffUI.RippleButton);
  }

  private void RoughskinClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSOTSBuffUI.BuffClick(7, PermanentSOTSBuffUI.RoughskinButton);
  }

  private void SoulAccessClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSOTSBuffUI.BuffClick(8, PermanentSOTSBuffUI.SoulAccessButton);
  }

  private void VibeClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSOTSBuffUI.BuffClick(9, PermanentSOTSBuffUI.VibeButton);
  }

  private void DigitalDisplayClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentSOTSBuffUI.BuffClick(10, PermanentSOTSBuffUI.DigitalDisplayButton);
  }

  public static void GetSOTSBuffData()
  {
    if (!ModConditions.secretsOfTheShadowsLoaded)
      return;
    PermanentSOTSBuffUI.AssassinationButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Assassination")).DisplayName;
    PermanentSOTSBuffUI.BluefireButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Bluefire")).DisplayName;
    PermanentSOTSBuffUI.BrittleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Brittle")).DisplayName;
    PermanentSOTSBuffUI.DoubleVisionButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "DoubleVision")).DisplayName;
    PermanentSOTSBuffUI.HarmonyButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Harmony")).DisplayName;
    PermanentSOTSBuffUI.NightmareButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Nightmare")).DisplayName;
    PermanentSOTSBuffUI.RippleButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "RippleBuff")).DisplayName;
    PermanentSOTSBuffUI.RoughskinButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Roughskin")).DisplayName;
    PermanentSOTSBuffUI.SoulAccessButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "SoulAccess")).DisplayName;
    PermanentSOTSBuffUI.VibeButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "GoodVibes")).DisplayName;
    PermanentSOTSBuffUI.DigitalDisplayButton.ModTooltip = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "CyberneticEnhancements")).DisplayName;
    PermanentSOTSBuffUI.AssassinationButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Assassination")), (AssetRequestMode) 2);
    PermanentSOTSBuffUI.BluefireButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Bluefire")), (AssetRequestMode) 2);
    PermanentSOTSBuffUI.BrittleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Brittle")), (AssetRequestMode) 2);
    PermanentSOTSBuffUI.DoubleVisionButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "DoubleVision")), (AssetRequestMode) 2);
    PermanentSOTSBuffUI.HarmonyButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Harmony")), (AssetRequestMode) 2);
    PermanentSOTSBuffUI.NightmareButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Nightmare")), (AssetRequestMode) 2);
    PermanentSOTSBuffUI.RippleButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "RippleBuff")), (AssetRequestMode) 2);
    PermanentSOTSBuffUI.RoughskinButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Roughskin")), (AssetRequestMode) 2);
    PermanentSOTSBuffUI.SoulAccessButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "SoulAccess")), (AssetRequestMode) 2);
    PermanentSOTSBuffUI.VibeButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "GoodVibes")), (AssetRequestMode) 2);
    PermanentSOTSBuffUI.DigitalDisplayButton.drawTexture = ModContent.Request<Texture2D>(Common.ModBuffAsset(ModConditions.secretsOfTheShadowsMod, Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "CyberneticEnhancements")), (AssetRequestMode) 2);
  }

  public virtual void Update(GameTime gameTime)
  {
    if (!PermanentSOTSBuffUI.visible)
      return;
    for (int index = 0; index < PermanentBuffPlayer.PermanentSOTSBuffsBools.Length; ++index)
    {
      this.allBuffButtons.ElementAt<PermanentBuffButton>(index).disabled = PermanentBuffPlayer.PermanentSOTSBuffsBools[index];
      this.allBuffButtons.ElementAt<PermanentBuffButton>(index).moddedBuff = true;
    }
  }

  public static void BuffClick(int buff, PermanentBuffButton button)
  {
    if (Main.GameUpdateCount - PermanentSOTSBuffUI.timeStart < 10U)
      return;
    PermanentBuffPlayer.PermanentSOTSBuffsBools[buff] = !PermanentBuffPlayer.PermanentSOTSBuffsBools[buff];
    button.disabled = PermanentBuffPlayer.PermanentSOTSBuffsBools[buff];
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
