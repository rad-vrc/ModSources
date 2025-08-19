// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.PermanentBuffSelectorUI
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Core.UI.Buttons;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI.Panels;

public class PermanentBuffSelectorUI : UIState
{
  public UIPanel SelectorPanel;
  public static bool visible;
  public static uint timeStart;
  private PermanentUpgradedBuffButton VanillaButton = new PermanentUpgradedBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentEverything", (AssetRequestMode) 2));
  private PermanentUpgradedBuffButton CalamityButton = new PermanentUpgradedBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentCalamity", (AssetRequestMode) 2));
  private PermanentUpgradedBuffButton MartinsOrderButton = new PermanentUpgradedBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentMartinsOrder", (AssetRequestMode) 2));
  private PermanentUpgradedBuffButton SOTSButton = new PermanentUpgradedBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentSecretsOfTheShadows", (AssetRequestMode) 2));
  private PermanentUpgradedBuffButton SpiritButton = new PermanentUpgradedBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentSpiritClassic", (AssetRequestMode) 2));
  private PermanentUpgradedBuffButton ThoriumButton = new PermanentUpgradedBuffButton(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentThorium", (AssetRequestMode) 2));

  public virtual void OnInitialize()
  {
    this.SelectorPanel = new UIPanel();
    ((UIElement) this.SelectorPanel).SetPadding(0.0f);
    ((StyleDimension) ref ((UIElement) this.SelectorPanel).Top).Set((float) (Main.screenHeight / 2), 0.0f);
    ((StyleDimension) ref ((UIElement) this.SelectorPanel).Left).Set((float) (Main.screenWidth / 2) + 10f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.SelectorPanel).Width).Set(304f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.SelectorPanel).Height).Set(64f, 0.0f);
    this.SelectorPanel.BackgroundColor = new Color(73, 94, 171);
    PermanentBuffSelectorUI.CreateBuffButton(this.VanillaButton, 16f, 16f);
    // ISSUE: method pointer
    this.VanillaButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(VanillaClicked));
    this.VanillaButton.Tooltip = UISystem.VanillaText;
    ((UIElement) this.SelectorPanel).Append((UIElement) this.VanillaButton);
    PermanentBuffSelectorUI.CreateBuffButton(this.CalamityButton, 64f, 16f);
    // ISSUE: method pointer
    this.CalamityButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CalamityClicked));
    this.CalamityButton.Tooltip = UISystem.CalamityText;
    ((UIElement) this.SelectorPanel).Append((UIElement) this.CalamityButton);
    PermanentBuffSelectorUI.CreateBuffButton(this.MartinsOrderButton, 112f, 16f);
    // ISSUE: method pointer
    this.MartinsOrderButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MartinsOrderClicked));
    this.MartinsOrderButton.Tooltip = UISystem.MartinsOrderText;
    ((UIElement) this.SelectorPanel).Append((UIElement) this.MartinsOrderButton);
    PermanentBuffSelectorUI.CreateBuffButton(this.SOTSButton, 160f, 16f);
    // ISSUE: method pointer
    this.SOTSButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SOTSClicked));
    this.SOTSButton.Tooltip = UISystem.SOTSText;
    ((UIElement) this.SelectorPanel).Append((UIElement) this.SOTSButton);
    PermanentBuffSelectorUI.CreateBuffButton(this.SpiritButton, 208f, 16f);
    // ISSUE: method pointer
    this.SpiritButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SpiritClicked));
    this.SpiritButton.Tooltip = UISystem.SpiritClassicText;
    ((UIElement) this.SelectorPanel).Append((UIElement) this.SpiritButton);
    PermanentBuffSelectorUI.CreateBuffButton(this.ThoriumButton, 256f, 16f);
    // ISSUE: method pointer
    this.ThoriumButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(ThoriumClicked));
    this.ThoriumButton.Tooltip = UISystem.ThoriumText;
    ((UIElement) this.SelectorPanel).Append((UIElement) this.ThoriumButton);
    ((UIElement) this).Append((UIElement) this.SelectorPanel);
  }

  private void VanillaClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffSelectorUI.BuffClick(0);
  }

  private void CalamityClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffSelectorUI.BuffClick(1);
  }

  private void MartinsOrderClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffSelectorUI.BuffClick(2);
  }

  private void SOTSClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffSelectorUI.BuffClick(3);
  }

  private void SpiritClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffSelectorUI.BuffClick(4);
  }

  private void ThoriumClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    PermanentBuffSelectorUI.BuffClick(5);
  }

  public static void BuffClick(int ui)
  {
    if (Main.GameUpdateCount - PermanentBuffSelectorUI.timeStart < 10U)
      return;
    SoundEngine.PlaySound(ref SoundID.MenuOpen, new Vector2?(((Entity) Main.LocalPlayer).position), (SoundUpdateCallback) null);
    if (ui == 0)
    {
      PermanentBuffUI.timeStart = Main.GameUpdateCount;
      PermanentBuffUI.visible = !PermanentBuffUI.visible;
    }
    else if (ui == 1 && ModConditions.calamityLoaded)
    {
      PermanentCalamityBuffUI.timeStart = Main.GameUpdateCount;
      PermanentCalamityBuffUI.visible = !PermanentCalamityBuffUI.visible;
    }
    else if (ui == 2 && ModConditions.martainsOrderLoaded)
    {
      PermanentMartinsOrderBuffUI.timeStart = Main.GameUpdateCount;
      PermanentMartinsOrderBuffUI.visible = !PermanentMartinsOrderBuffUI.visible;
    }
    else if (ui == 3 && ModConditions.secretsOfTheShadowsLoaded)
    {
      PermanentSOTSBuffUI.timeStart = Main.GameUpdateCount;
      PermanentSOTSBuffUI.visible = !PermanentSOTSBuffUI.visible;
    }
    else if (ui == 4 && ModConditions.spiritLoaded)
    {
      PermanentSpiritClassicBuffUI.timeStart = Main.GameUpdateCount;
      PermanentSpiritClassicBuffUI.visible = !PermanentSpiritClassicBuffUI.visible;
    }
    else
    {
      if (ui != 5 || !ModConditions.thoriumLoaded)
        return;
      PermanentThoriumBuffUI.timeStart = Main.GameUpdateCount;
      PermanentThoriumBuffUI.visible = !PermanentThoriumBuffUI.visible;
    }
  }

  private static void CreateBuffButton(PermanentUpgradedBuffButton button, float left, float top)
  {
    ((StyleDimension) ref button.Left).Set(left, 0.0f);
    ((StyleDimension) ref button.Top).Set(top, 0.0f);
    ((StyleDimension) ref button.Width).Set(32f, 0.0f);
    ((StyleDimension) ref button.Height).Set(32f, 0.0f);
  }
}
