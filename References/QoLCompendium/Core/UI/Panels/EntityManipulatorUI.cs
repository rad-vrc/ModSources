// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.EntityManipulatorUI
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

public class EntityManipulatorUI : UIState
{
  public UIPanel ManipulatorPanel;
  public static bool visible;
  public static uint timeStart;

  public virtual void OnInitialize()
  {
    this.ManipulatorPanel = new UIPanel();
    ((StyleDimension) ref ((UIElement) this.ManipulatorPanel).Top).Set((float) (Main.screenHeight / 2), 0.0f);
    ((StyleDimension) ref ((UIElement) this.ManipulatorPanel).Left).Set((float) (Main.screenWidth / 2 + 7), 0.0f);
    ((StyleDimension) ref ((UIElement) this.ManipulatorPanel).Width).Set(400f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.ManipulatorPanel).Height).Set(400f, 0.0f);
    UIPanel manipulatorPanel1 = this.ManipulatorPanel;
    manipulatorPanel1.BackgroundColor = Color.op_Multiply(manipulatorPanel1.BackgroundColor, 0.0f);
    UIPanel manipulatorPanel2 = this.ManipulatorPanel;
    manipulatorPanel2.BorderColor = Color.op_Multiply(manipulatorPanel2.BorderColor, 0.0f);
    ((UIElement) this).Append((UIElement) this.ManipulatorPanel);
    SpawnModifierButton.backgroundTexture = 0;
    SpawnModifierButton spawnModifierButton1 = new SpawnModifierButton(Common.GetAsset("SpawnModifiers", "Modifier_", SpawnModifierButton.modifierTexture = 0));
    ((StyleDimension) ref spawnModifierButton1.Left).Set(0.0f, 0.0f);
    ((StyleDimension) ref spawnModifierButton1.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref spawnModifierButton1.Width).Set(40f, 0.0f);
    ((StyleDimension) ref spawnModifierButton1.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    spawnModifierButton1.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(IncreasedSpawnsClicked));
    spawnModifierButton1.Tooltip = UISystem.IncreaseSpawnText;
    ((UIElement) this.ManipulatorPanel).Append((UIElement) spawnModifierButton1);
    SpawnModifierButton.backgroundTexture = 1;
    SpawnModifierButton spawnModifierButton2 = new SpawnModifierButton(Common.GetAsset("SpawnModifiers", "Modifier_", SpawnModifierButton.modifierTexture = 1));
    ((StyleDimension) ref spawnModifierButton2.Left).Set(40f, 0.0f);
    ((StyleDimension) ref spawnModifierButton2.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref spawnModifierButton2.Width).Set(40f, 0.0f);
    ((StyleDimension) ref spawnModifierButton2.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    spawnModifierButton2.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DecreasedSpawnsClicked));
    spawnModifierButton2.Tooltip = UISystem.DecreaseSpawnText;
    ((UIElement) this.ManipulatorPanel).Append((UIElement) spawnModifierButton2);
    SpawnModifierButton.backgroundTexture = 1;
    SpawnModifierButton spawnModifierButton3 = new SpawnModifierButton(Common.GetAsset("SpawnModifiers", "Modifier_", SpawnModifierButton.modifierTexture = 2));
    ((StyleDimension) ref spawnModifierButton3.Left).Set(80f, 0.0f);
    ((StyleDimension) ref spawnModifierButton3.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref spawnModifierButton3.Width).Set(40f, 0.0f);
    ((StyleDimension) ref spawnModifierButton3.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    spawnModifierButton3.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(NoSpawnsClicked));
    spawnModifierButton3.Tooltip = UISystem.CancelSpawnText;
    ((UIElement) this.ManipulatorPanel).Append((UIElement) spawnModifierButton3);
    SpawnModifierButton.backgroundTexture = 1;
    SpawnModifierButton spawnModifierButton4 = new SpawnModifierButton(Common.GetAsset("SpawnModifiers", "Modifier_", SpawnModifierButton.modifierTexture = 3));
    ((StyleDimension) ref spawnModifierButton4.Left).Set(120f, 0.0f);
    ((StyleDimension) ref spawnModifierButton4.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref spawnModifierButton4.Width).Set(40f, 0.0f);
    ((StyleDimension) ref spawnModifierButton4.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    spawnModifierButton4.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(NoEventsClicked));
    spawnModifierButton4.Tooltip = UISystem.CancelEventText;
    ((UIElement) this.ManipulatorPanel).Append((UIElement) spawnModifierButton4);
    SpawnModifierButton.backgroundTexture = 1;
    SpawnModifierButton spawnModifierButton5 = new SpawnModifierButton(Common.GetAsset("SpawnModifiers", "Modifier_", SpawnModifierButton.modifierTexture = 4));
    ((StyleDimension) ref spawnModifierButton5.Left).Set(160f, 0.0f);
    ((StyleDimension) ref spawnModifierButton5.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref spawnModifierButton5.Width).Set(40f, 0.0f);
    ((StyleDimension) ref spawnModifierButton5.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    spawnModifierButton5.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(NoSpawnsOrEventsClicked));
    spawnModifierButton5.Tooltip = UISystem.CancelSpawnAndEventText;
    ((UIElement) this.ManipulatorPanel).Append((UIElement) spawnModifierButton5);
    GenericUIButton.backgroundTexture = 1;
    GenericUIButton genericUiButton1 = new GenericUIButton(Common.GetAsset("Buttons", "Button_Small_", GenericUIButton.buttonTexture = 2));
    ((StyleDimension) ref genericUiButton1.Left).Set(200f, 0.0f);
    ((StyleDimension) ref genericUiButton1.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref genericUiButton1.Width).Set(40f, 0.0f);
    ((StyleDimension) ref genericUiButton1.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    genericUiButton1.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(RevertButtonClicked));
    genericUiButton1.Tooltip = UISystem.ResetText;
    ((UIElement) this.ManipulatorPanel).Append((UIElement) genericUiButton1);
    GenericUIButton.backgroundTexture = 2;
    GenericUIButton genericUiButton2 = new GenericUIButton(Common.GetAsset("Buttons", "Button_Small_", GenericUIButton.buttonTexture = 3));
    ((StyleDimension) ref genericUiButton2.Left).Set(240f, 0.0f);
    ((StyleDimension) ref genericUiButton2.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref genericUiButton2.Width).Set(40f, 0.0f);
    ((StyleDimension) ref genericUiButton2.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    genericUiButton2.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CloseButtonClicked));
    genericUiButton2.Tooltip = UISystem.CloseText;
    ((UIElement) this.ManipulatorPanel).Append((UIElement) genericUiButton2);
  }

  private void RevertButtonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    EntityManipulatorUI.SpawnChangeClick(0);
  }

  private void IncreasedSpawnsClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    EntityManipulatorUI.SpawnChangeClick(1);
  }

  private void DecreasedSpawnsClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    EntityManipulatorUI.SpawnChangeClick(2);
  }

  private void NoSpawnsClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    EntityManipulatorUI.SpawnChangeClick(3);
  }

  private void NoEventsClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    EntityManipulatorUI.SpawnChangeClick(4);
  }

  private void NoSpawnsOrEventsClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    EntityManipulatorUI.SpawnChangeClick(5);
  }

  private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - EntityManipulatorUI.timeStart < 10U)
      return;
    SoundEngine.PlaySound(ref SoundID.MenuClose, new Vector2?(), (SoundUpdateCallback) null);
    EntityManipulatorUI.visible = false;
  }

  public static void SpawnChangeClick(int modifier)
  {
    if (Main.GameUpdateCount - EntityManipulatorUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().selectedSpawnModifier = modifier;
    SoundEngine.PlaySound(ref SoundID.MenuTick, new Vector2?(), (SoundUpdateCallback) null);
  }
}
