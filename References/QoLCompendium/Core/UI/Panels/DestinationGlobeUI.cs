// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.DestinationGlobeUI
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

public class DestinationGlobeUI : UIState
{
  public UIPanel GlobePanel;
  public static bool visible;
  public static uint timeStart;

  public virtual void OnInitialize()
  {
    this.GlobePanel = new UIPanel();
    ((StyleDimension) ref ((UIElement) this.GlobePanel).Top).Set((float) (Main.screenHeight / 2), 0.0f);
    ((StyleDimension) ref ((UIElement) this.GlobePanel).Left).Set((float) (Main.screenWidth / 2 - 32 /*0x20*/), 0.0f);
    ((StyleDimension) ref ((UIElement) this.GlobePanel).Width).Set(400f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.GlobePanel).Height).Set(400f, 0.0f);
    UIPanel globePanel1 = this.GlobePanel;
    globePanel1.BackgroundColor = Color.op_Multiply(globePanel1.BackgroundColor, 0.0f);
    UIPanel globePanel2 = this.GlobePanel;
    globePanel2.BorderColor = Color.op_Multiply(globePanel2.BorderColor, 0.0f);
    ((UIElement) this).Append((UIElement) this.GlobePanel);
    BiomeButton.backgroundTexture = 0;
    BiomeButton biomeButton1 = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 0));
    ((StyleDimension) ref biomeButton1.Left).Set(0.0f, 0.0f);
    ((StyleDimension) ref biomeButton1.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref biomeButton1.Width).Set(40f, 0.0f);
    ((StyleDimension) ref biomeButton1.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    biomeButton1.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DesertClicked));
    biomeButton1.Tooltip = UISystem.DesertText;
    ((UIElement) this.GlobePanel).Append((UIElement) biomeButton1);
    BiomeButton.backgroundTexture = 1;
    BiomeButton biomeButton2 = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 1));
    ((StyleDimension) ref biomeButton2.Left).Set(40f, 0.0f);
    ((StyleDimension) ref biomeButton2.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref biomeButton2.Width).Set(40f, 0.0f);
    ((StyleDimension) ref biomeButton2.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    biomeButton2.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SnowClicked));
    biomeButton2.Tooltip = UISystem.SnowText;
    ((UIElement) this.GlobePanel).Append((UIElement) biomeButton2);
    BiomeButton.backgroundTexture = 1;
    BiomeButton biomeButton3 = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 2));
    ((StyleDimension) ref biomeButton3.Left).Set(80f, 0.0f);
    ((StyleDimension) ref biomeButton3.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref biomeButton3.Width).Set(40f, 0.0f);
    ((StyleDimension) ref biomeButton3.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    biomeButton3.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(JungleClicked));
    biomeButton3.Tooltip = UISystem.JungleText;
    ((UIElement) this.GlobePanel).Append((UIElement) biomeButton3);
    BiomeButton.backgroundTexture = 1;
    BiomeButton biomeButton4 = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 3));
    ((StyleDimension) ref biomeButton4.Left).Set(120f, 0.0f);
    ((StyleDimension) ref biomeButton4.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref biomeButton4.Width).Set(40f, 0.0f);
    ((StyleDimension) ref biomeButton4.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    biomeButton4.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MushroomClicked));
    biomeButton4.Tooltip = UISystem.GlowingMushroomText;
    ((UIElement) this.GlobePanel).Append((UIElement) biomeButton4);
    BiomeButton.backgroundTexture = 1;
    BiomeButton biomeButton5 = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 4));
    ((StyleDimension) ref biomeButton5.Left).Set(160f, 0.0f);
    ((StyleDimension) ref biomeButton5.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref biomeButton5.Width).Set(40f, 0.0f);
    ((StyleDimension) ref biomeButton5.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    biomeButton5.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CorruptionClicked));
    biomeButton5.Tooltip = UISystem.CorruptionText;
    ((UIElement) this.GlobePanel).Append((UIElement) biomeButton5);
    BiomeButton.backgroundTexture = 1;
    BiomeButton biomeButton6 = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 5));
    ((StyleDimension) ref biomeButton6.Left).Set(200f, 0.0f);
    ((StyleDimension) ref biomeButton6.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref biomeButton6.Width).Set(40f, 0.0f);
    ((StyleDimension) ref biomeButton6.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    biomeButton6.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CrimsonClicked));
    biomeButton6.Tooltip = UISystem.CrimsonText;
    ((UIElement) this.GlobePanel).Append((UIElement) biomeButton6);
    BiomeButton.backgroundTexture = 1;
    BiomeButton biomeButton7 = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 6));
    ((StyleDimension) ref biomeButton7.Left).Set(240f, 0.0f);
    ((StyleDimension) ref biomeButton7.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref biomeButton7.Width).Set(40f, 0.0f);
    ((StyleDimension) ref biomeButton7.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    biomeButton7.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(HallowClicked));
    biomeButton7.Tooltip = UISystem.HallowText;
    ((UIElement) this.GlobePanel).Append((UIElement) biomeButton7);
    GenericUIButton.backgroundTexture = 1;
    GenericUIButton genericUiButton1 = new GenericUIButton(Common.GetAsset("Buttons", "Button_Small_", GenericUIButton.buttonTexture = 2));
    ((StyleDimension) ref genericUiButton1.Left).Set(280f, 0.0f);
    ((StyleDimension) ref genericUiButton1.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref genericUiButton1.Width).Set(40f, 0.0f);
    ((StyleDimension) ref genericUiButton1.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    genericUiButton1.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ResetBiomeClicked));
    genericUiButton1.Tooltip = UISystem.ResetText;
    ((UIElement) this.GlobePanel).Append((UIElement) genericUiButton1);
    GenericUIButton.backgroundTexture = 2;
    GenericUIButton genericUiButton2 = new GenericUIButton(Common.GetAsset("Buttons", "Button_Small_", GenericUIButton.buttonTexture = 3));
    ((StyleDimension) ref genericUiButton2.Left).Set(320f, 0.0f);
    ((StyleDimension) ref genericUiButton2.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref genericUiButton2.Width).Set(40f, 0.0f);
    ((StyleDimension) ref genericUiButton2.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    genericUiButton2.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CloseButtonClicked));
    genericUiButton2.Tooltip = UISystem.CloseText;
    ((UIElement) this.GlobePanel).Append((UIElement) genericUiButton2);
  }

  private void ResetBiomeClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    DestinationGlobeUI.BiomeClick(0);
  }

  private void DesertClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    DestinationGlobeUI.BiomeClick(1);
  }

  private void SnowClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    DestinationGlobeUI.BiomeClick(2);
  }

  private void JungleClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    DestinationGlobeUI.BiomeClick(3);
  }

  private void MushroomClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    DestinationGlobeUI.BiomeClick(4);
  }

  private void CorruptionClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    DestinationGlobeUI.BiomeClick(5);
  }

  private void CrimsonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    DestinationGlobeUI.BiomeClick(6);
  }

  private void HallowClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    DestinationGlobeUI.BiomeClick(7);
  }

  private void ForestClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    DestinationGlobeUI.BiomeClick(8);
  }

  private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - DestinationGlobeUI.timeStart < 10U)
      return;
    SoundEngine.PlaySound(ref SoundID.MenuClose, new Vector2?(), (SoundUpdateCallback) null);
    DestinationGlobeUI.visible = false;
  }

  public static void BiomeClick(int biome)
  {
    if (Main.GameUpdateCount - DestinationGlobeUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().selectedBiome = biome;
    SoundEngine.PlaySound(ref SoundID.MenuTick, new Vector2?(), (SoundUpdateCallback) null);
  }
}
