// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.AllInOneAccessUI
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

public class AllInOneAccessUI : UIState
{
  public UIPanel StoragePanel;
  public static bool visible;
  public static uint timeStart;

  public virtual void OnInitialize()
  {
    this.StoragePanel = new UIPanel();
    ((StyleDimension) ref ((UIElement) this.StoragePanel).Top).Set((float) (Main.screenHeight / 2), 0.0f);
    ((StyleDimension) ref ((UIElement) this.StoragePanel).Left).Set((float) (Main.screenWidth / 2 + 47), 0.0f);
    ((StyleDimension) ref ((UIElement) this.StoragePanel).Width).Set(200f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.StoragePanel).Height).Set(200f, 0.0f);
    UIPanel storagePanel1 = this.StoragePanel;
    storagePanel1.BackgroundColor = Color.op_Multiply(storagePanel1.BackgroundColor, 0.0f);
    UIPanel storagePanel2 = this.StoragePanel;
    storagePanel2.BorderColor = Color.op_Multiply(storagePanel2.BorderColor, 0.0f);
    ((UIElement) this).Append((UIElement) this.StoragePanel);
    AllInOneAccessButton.backgroundTexture = 0;
    AllInOneAccessButton inOneAccessButton1 = new AllInOneAccessButton(Common.GetAsset("Storages", "Storage_", AllInOneAccessButton.storageTexture = 0));
    ((StyleDimension) ref inOneAccessButton1.Left).Set(0.0f, 0.0f);
    ((StyleDimension) ref inOneAccessButton1.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref inOneAccessButton1.Width).Set(40f, 0.0f);
    ((StyleDimension) ref inOneAccessButton1.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    inOneAccessButton1.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(PiggyBankClicked));
    inOneAccessButton1.Tooltip = UISystem.PiggyBankText;
    ((UIElement) this.StoragePanel).Append((UIElement) inOneAccessButton1);
    AllInOneAccessButton.backgroundTexture = 1;
    AllInOneAccessButton inOneAccessButton2 = new AllInOneAccessButton(Common.GetAsset("Storages", "Storage_", AllInOneAccessButton.storageTexture = 1));
    ((StyleDimension) ref inOneAccessButton2.Left).Set(40f, 0.0f);
    ((StyleDimension) ref inOneAccessButton2.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref inOneAccessButton2.Width).Set(40f, 0.0f);
    ((StyleDimension) ref inOneAccessButton2.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    inOneAccessButton2.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SafeClicked));
    inOneAccessButton2.Tooltip = UISystem.SafeText;
    ((UIElement) this.StoragePanel).Append((UIElement) inOneAccessButton2);
    AllInOneAccessButton.backgroundTexture = 1;
    AllInOneAccessButton inOneAccessButton3 = new AllInOneAccessButton(Common.GetAsset("Storages", "Storage_", AllInOneAccessButton.storageTexture = 2));
    ((StyleDimension) ref inOneAccessButton3.Left).Set(80f, 0.0f);
    ((StyleDimension) ref inOneAccessButton3.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref inOneAccessButton3.Width).Set(40f, 0.0f);
    ((StyleDimension) ref inOneAccessButton3.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    inOneAccessButton3.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DefendersForgeClicked));
    inOneAccessButton3.Tooltip = UISystem.DefendersForgeText;
    ((UIElement) this.StoragePanel).Append((UIElement) inOneAccessButton3);
    AllInOneAccessButton.backgroundTexture = 1;
    AllInOneAccessButton inOneAccessButton4 = new AllInOneAccessButton(Common.GetAsset("Storages", "Storage_", AllInOneAccessButton.storageTexture = 3));
    ((StyleDimension) ref inOneAccessButton4.Left).Set(120f, 0.0f);
    ((StyleDimension) ref inOneAccessButton4.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref inOneAccessButton4.Width).Set(40f, 0.0f);
    ((StyleDimension) ref inOneAccessButton4.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    inOneAccessButton4.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(VoidVaultClicked));
    inOneAccessButton4.Tooltip = UISystem.VoidVaultText;
    ((UIElement) this.StoragePanel).Append((UIElement) inOneAccessButton4);
    GenericUIButton.backgroundTexture = 2;
    GenericUIButton genericUiButton = new GenericUIButton(Common.GetAsset("Buttons", "Button_Small_", GenericUIButton.buttonTexture = 3));
    ((StyleDimension) ref genericUiButton.Left).Set(160f, 0.0f);
    ((StyleDimension) ref genericUiButton.Top).Set(0.0f, 0.0f);
    ((StyleDimension) ref genericUiButton.Width).Set(40f, 0.0f);
    ((StyleDimension) ref genericUiButton.Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    genericUiButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CloseButtonClicked));
    genericUiButton.Tooltip = UISystem.CloseText;
    ((UIElement) this.StoragePanel).Append((UIElement) genericUiButton);
  }

  private void PiggyBankClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    AllInOneAccessUI.StorageClick(-2, SoundID.Item59);
  }

  private void SafeClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    AllInOneAccessUI.StorageClick(-3, SoundID.MenuOpen);
  }

  private void DefendersForgeClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    AllInOneAccessUI.StorageClick(-4, SoundID.MenuOpen);
  }

  private void VoidVaultClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    AllInOneAccessUI.StorageClick(-5, SoundID.Item130);
  }

  private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - AllInOneAccessUI.timeStart < 10U)
      return;
    SoundEngine.PlaySound(ref SoundID.MenuClose, new Vector2?(), (SoundUpdateCallback) null);
    AllInOneAccessUI.visible = false;
  }

  public static void StorageClick(int chest, SoundStyle sound)
  {
    if (Main.GameUpdateCount - AllInOneAccessUI.timeStart < 10U)
      return;
    Main.playerInventory = true;
    Main.LocalPlayer.chest = chest;
    QoLCompendium.QoLCompendium.LastOpenedBank = new int?(chest);
    Point tileCoordinates = Utils.ToTileCoordinates(((Entity) Main.LocalPlayer).Center);
    Main.LocalPlayer.chestX = tileCoordinates.X;
    Main.LocalPlayer.chestY = tileCoordinates.Y;
    Main.oldNPCShop = 0;
    Main.LocalPlayer.SetTalkNPC(-1, false);
    Main.SetNPCShopIndex(0);
    SoundEngine.PlaySound(ref sound, new Vector2?(((Entity) Main.LocalPlayer).position), (SoundUpdateCallback) null);
    Recipe.FindRecipes(false);
  }
}
