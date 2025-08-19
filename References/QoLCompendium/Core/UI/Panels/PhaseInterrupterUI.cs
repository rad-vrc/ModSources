// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.PhaseInterrupterUI
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core.UI.Buttons;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI.Panels;

public class PhaseInterrupterUI : UIState
{
  public UIPanel MoonPanel;
  public static bool visible;
  public static uint timeStart;

  public virtual void OnInitialize()
  {
    this.MoonPanel = new UIPanel();
    ((StyleDimension) ref ((UIElement) this.MoonPanel).Top).Set((float) (Main.screenHeight / 2), 0.0f);
    ((StyleDimension) ref ((UIElement) this.MoonPanel).Left).Set((float) (Main.screenWidth / 2 - 32 /*0x20*/), 0.0f);
    ((StyleDimension) ref ((UIElement) this.MoonPanel).Width).Set(400f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.MoonPanel).Height).Set(400f, 0.0f);
    UIPanel moonPanel1 = this.MoonPanel;
    moonPanel1.BackgroundColor = Color.op_Multiply(moonPanel1.BackgroundColor, 0.0f);
    UIPanel moonPanel2 = this.MoonPanel;
    moonPanel2.BorderColor = Color.op_Multiply(moonPanel2.BorderColor, 0.0f);
    ((UIElement) this).Append((UIElement) this.MoonPanel);
    MoonPhaseButton.backgroundTexture = 0;
    MoonPhaseButton moonPhaseButton1 = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 0));
    ((StyleDimension) ref moonPhaseButton1.Left).Set(0.0f, 0.0f);
    ((StyleDimension) ref moonPhaseButton1.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref moonPhaseButton1.Width).Set(40f, 0.0f);
    ((StyleDimension) ref moonPhaseButton1.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    moonPhaseButton1.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(FullMoonClicked));
    moonPhaseButton1.Tooltip = UISystem.FullMoonText;
    ((UIElement) this.MoonPanel).Append((UIElement) moonPhaseButton1);
    MoonPhaseButton.backgroundTexture = 1;
    MoonPhaseButton moonPhaseButton2 = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 1));
    ((StyleDimension) ref moonPhaseButton2.Left).Set(40f, 0.0f);
    ((StyleDimension) ref moonPhaseButton2.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref moonPhaseButton2.Width).Set(40f, 0.0f);
    ((StyleDimension) ref moonPhaseButton2.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    moonPhaseButton2.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WaningGibbousClicked));
    moonPhaseButton2.Tooltip = UISystem.WaningGibbousText;
    ((UIElement) this.MoonPanel).Append((UIElement) moonPhaseButton2);
    MoonPhaseButton.backgroundTexture = 1;
    MoonPhaseButton moonPhaseButton3 = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 2));
    ((StyleDimension) ref moonPhaseButton3.Left).Set(80f, 0.0f);
    ((StyleDimension) ref moonPhaseButton3.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref moonPhaseButton3.Width).Set(40f, 0.0f);
    ((StyleDimension) ref moonPhaseButton3.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    moonPhaseButton3.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ThirdQuarterClicked));
    moonPhaseButton3.Tooltip = UISystem.ThirdQuarterText;
    ((UIElement) this.MoonPanel).Append((UIElement) moonPhaseButton3);
    MoonPhaseButton.backgroundTexture = 1;
    MoonPhaseButton moonPhaseButton4 = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 3));
    ((StyleDimension) ref moonPhaseButton4.Left).Set(120f, 0.0f);
    ((StyleDimension) ref moonPhaseButton4.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref moonPhaseButton4.Width).Set(40f, 0.0f);
    ((StyleDimension) ref moonPhaseButton4.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    moonPhaseButton4.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WaningCrescentClicked));
    moonPhaseButton4.Tooltip = UISystem.WaningCrescentText;
    ((UIElement) this.MoonPanel).Append((UIElement) moonPhaseButton4);
    MoonPhaseButton.backgroundTexture = 1;
    MoonPhaseButton moonPhaseButton5 = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 4));
    ((StyleDimension) ref moonPhaseButton5.Left).Set(160f, 0.0f);
    ((StyleDimension) ref moonPhaseButton5.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref moonPhaseButton5.Width).Set(40f, 0.0f);
    ((StyleDimension) ref moonPhaseButton5.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    moonPhaseButton5.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(NewMoonClicked));
    moonPhaseButton5.Tooltip = UISystem.NewMoonText;
    ((UIElement) this.MoonPanel).Append((UIElement) moonPhaseButton5);
    MoonPhaseButton.backgroundTexture = 1;
    MoonPhaseButton moonPhaseButton6 = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 5));
    ((StyleDimension) ref moonPhaseButton6.Left).Set(200f, 0.0f);
    ((StyleDimension) ref moonPhaseButton6.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref moonPhaseButton6.Width).Set(40f, 0.0f);
    ((StyleDimension) ref moonPhaseButton6.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    moonPhaseButton6.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WaxingCrescentClicked));
    moonPhaseButton6.Tooltip = UISystem.WaxingCrescentText;
    ((UIElement) this.MoonPanel).Append((UIElement) moonPhaseButton6);
    MoonPhaseButton.backgroundTexture = 1;
    MoonPhaseButton moonPhaseButton7 = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 6));
    ((StyleDimension) ref moonPhaseButton7.Left).Set(240f, 0.0f);
    ((StyleDimension) ref moonPhaseButton7.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref moonPhaseButton7.Width).Set(40f, 0.0f);
    ((StyleDimension) ref moonPhaseButton7.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    moonPhaseButton7.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(FirstQuarterClicked));
    moonPhaseButton7.Tooltip = UISystem.FirstQuarterText;
    ((UIElement) this.MoonPanel).Append((UIElement) moonPhaseButton7);
    MoonPhaseButton.backgroundTexture = 1;
    MoonPhaseButton moonPhaseButton8 = new MoonPhaseButton(Common.GetAsset("Moons", "Moon_", MoonPhaseButton.moonTexture = 7));
    ((StyleDimension) ref moonPhaseButton8.Left).Set(280f, 0.0f);
    ((StyleDimension) ref moonPhaseButton8.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref moonPhaseButton8.Width).Set(40f, 0.0f);
    ((StyleDimension) ref moonPhaseButton8.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    moonPhaseButton8.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WaxingGibbousClicked));
    moonPhaseButton8.Tooltip = UISystem.WaxingGibbousText;
    ((UIElement) this.MoonPanel).Append((UIElement) moonPhaseButton8);
    GenericUIButton.backgroundTexture = 2;
    GenericUIButton genericUiButton = new GenericUIButton(Common.GetAsset("Buttons", "Button_Small_", GenericUIButton.buttonTexture = 3));
    ((StyleDimension) ref genericUiButton.Left).Set(320f, 0.0f);
    ((StyleDimension) ref genericUiButton.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref genericUiButton.Width).Set(40f, 0.0f);
    ((StyleDimension) ref genericUiButton.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    genericUiButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CloseButtonClicked));
    genericUiButton.Tooltip = UISystem.CloseText;
    ((UIElement) this.MoonPanel).Append((UIElement) genericUiButton);
  }

  private void FullMoonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PhaseInterrupterUI.PhaseClick(0);
  }

  private void WaningGibbousClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PhaseInterrupterUI.PhaseClick(1);
  }

  private void ThirdQuarterClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PhaseInterrupterUI.PhaseClick(2);
  }

  private void WaningCrescentClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PhaseInterrupterUI.PhaseClick(3);
  }

  private void NewMoonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PhaseInterrupterUI.PhaseClick(4);
  }

  private void WaxingCrescentClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PhaseInterrupterUI.PhaseClick(5);
  }

  private void FirstQuarterClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PhaseInterrupterUI.PhaseClick(6);
  }

  private void WaxingGibbousClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PhaseInterrupterUI.PhaseClick(7);
  }

  private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - PhaseInterrupterUI.timeStart < 10U)
      return;
    SoundEngine.PlaySound(ref SoundID.MenuClose, new Vector2?(), (SoundUpdateCallback) null);
    PhaseInterrupterUI.visible = false;
  }

  public static void PhaseClick(int phase)
  {
    if (Main.GameUpdateCount - PhaseInterrupterUI.timeStart < 10U)
      return;
    Main.moonPhase = phase;
    SoundEngine.PlaySound(ref SoundID.MenuTick, new Vector2?(), (SoundUpdateCallback) null);
  }
}
