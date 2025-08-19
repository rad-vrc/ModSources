// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.BlackMarketDealerNPCUI
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Content.NPCs;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI.Panels;

public class BlackMarketDealerNPCUI : UIState
{
  public UIPanel ShopPanel;
  public static bool visible;
  public static uint timeStart;
  private Vector2 offset;
  public bool dragging;

  public virtual void OnInitialize()
  {
    this.ShopPanel = new UIPanel();
    ((UIElement) this.ShopPanel).SetPadding(0.0f);
    ((StyleDimension) ref ((UIElement) this.ShopPanel).Left).Set(575f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.ShopPanel).Top).Set(275f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.ShopPanel).Width).Set(385f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.ShopPanel).Height).Set(460f, 0.0f);
    this.ShopPanel.BackgroundColor = new Color(73, 94, 171);
    // ISSUE: method pointer
    ((UIElement) this.ShopPanel).OnLeftMouseDown += new UIElement.MouseEvent((object) this, __methodptr(DragStart));
    // ISSUE: method pointer
    ((UIElement) this.ShopPanel).OnLeftMouseUp += new UIElement.MouseEvent((object) this, __methodptr(DragEnd));
    UIText uiText1 = new UIText(UISystem.BMPotionText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText1).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText1).Top).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText1).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText1).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText1);
    UIText uiText2 = new UIText(UISystem.BMStationText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText2).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Top).Set(40f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText2);
    UIText uiText3 = new UIText(UISystem.BMMaterialText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText3).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Top).Set(70f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText3);
    UIText uiText4 = new UIText(UISystem.BMMovementAccessoryText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText4).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText4).Top).Set(100f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText4).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText4).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText4);
    UIText uiText5 = new UIText(UISystem.BMCombatAccessoryText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText5).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText5).Top).Set(130f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText5).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText5).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText5);
    UIText uiText6 = new UIText(UISystem.BMInformativeText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText6).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText6).Top).Set(160f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText6).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText6).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText6);
    UIText uiText7 = new UIText(UISystem.BMBagText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText7).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText7).Top).Set(190f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText7).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText7).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText7);
    UIText uiText8 = new UIText(UISystem.BMCrateText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText8).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText8).Top).Set(220f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText8).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText8).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText8);
    UIText uiText9 = new UIText(UISystem.BMOreText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText9).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText9).Top).Set(250f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText9).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText9).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText9);
    UIText uiText10 = new UIText(UISystem.BMNaturalBlockText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText10).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText10).Top).Set(280f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText10).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText10).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText10);
    UIText uiText11 = new UIText(UISystem.BMBuildingBlockText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText11).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText11).Top).Set(310f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText11).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText11).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText11);
    UIText uiText12 = new UIText(UISystem.BMHerbText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText12).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText12).Top).Set(340f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText12).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText12).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText12);
    UIText uiText13 = new UIText(UISystem.BMFishText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText13).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText13).Top).Set(370f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText13).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText13).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText13);
    UIText uiText14 = new UIText(UISystem.BMMountText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText14).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText14).Top).Set(400f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText14).Width).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText14).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText14);
    UIText uiText15 = new UIText(UISystem.BMAmmoText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText15).Left).Set(35f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText15).Top).Set(430f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText15).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText15).Height).Set(22f, 0.0f);
    ((UIElement) this.ShopPanel).Append((UIElement) uiText15);
    Asset<Texture2D> asset1 = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonPlay", (AssetRequestMode) 2);
    Asset<Texture2D> asset2 = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete", (AssetRequestMode) 2);
    UIImageButton uiImageButton1 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMPotionShop)
      uiImageButton1 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton1).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton1).Top).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton1).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton1).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton1).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(PotionShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton1);
    UIImageButton uiImageButton2 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMMaterialShop)
      uiImageButton2 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton2).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton2).Top).Set(40f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton2).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton2).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton2).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(StationShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton2);
    UIImageButton uiImageButton3 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMMaterialShop)
      uiImageButton3 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton3).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton3).Top).Set(70f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton3).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton3).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton3).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MaterialShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton3);
    UIImageButton uiImageButton4 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMMovementAccessoryShop)
      uiImageButton4 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton4).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton4).Top).Set(100f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton4).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton4).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton4).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MovementShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton4);
    UIImageButton uiImageButton5 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMCombatAccessoryShop)
      uiImageButton5 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton5).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton5).Top).Set(130f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton5).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton5).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton5).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CombatShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton5);
    UIImageButton uiImageButton6 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMInformationShop)
      uiImageButton6 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton6).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton6).Top).Set(160f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton6).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton6).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton6).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(InfoShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton6);
    UIImageButton uiImageButton7 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMBagShop)
      uiImageButton7 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton7).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton7).Top).Set(190f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton7).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton7).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton7).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BagShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton7);
    UIImageButton uiImageButton8 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMCrateShop)
      uiImageButton8 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton8).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton8).Top).Set(220f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton8).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton8).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton8).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CrateShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton8);
    UIImageButton uiImageButton9 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMOreShop)
      uiImageButton9 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton9).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton9).Top).Set(250f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton9).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton9).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton9).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(OreShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton9);
    UIImageButton uiImageButton10 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMNaturalBlockShop)
      uiImageButton10 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton10).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton10).Top).Set(280f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton10).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton10).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton10).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(NaturalShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton10);
    UIImageButton uiImageButton11 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMBuildingBlockShop)
      uiImageButton11 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton11).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton11).Top).Set(310f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton11).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton11).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton11).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BuildingShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton11);
    UIImageButton uiImageButton12 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMHerbShop)
      uiImageButton12 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton12).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton12).Top).Set(340f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton12).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton12).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton12).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(HerbShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton12);
    UIImageButton uiImageButton13 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMFishShop)
      uiImageButton13 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton13).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton13).Top).Set(370f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton13).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton13).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton13).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(FishShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton13);
    UIImageButton uiImageButton14 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMMountShop)
      uiImageButton14 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton14).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton14).Top).Set(400f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton14).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton14).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton14).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MountShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton14);
    UIImageButton uiImageButton15 = new UIImageButton(asset1);
    if (!QoLCompendium.QoLCompendium.shopConfig.BMAmmoShop)
      uiImageButton15 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton15).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton15).Top).Set(430f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton15).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton15).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton15).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(AmmoShopClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton15);
    UIImageButton uiImageButton16 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton16).Left).Set(350f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton16).Top).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton16).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton16).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton16).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CloseButtonClicked));
    ((UIElement) this.ShopPanel).Append((UIElement) uiImageButton16);
    ((UIElement) this).Append((UIElement) this.ShopPanel);
  }

  private void PotionShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMPotionShop)
      return;
    BMDealerNPC.shopNum = 0;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void StationShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMStationShop)
      return;
    BMDealerNPC.shopNum = 1;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void MaterialShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMMaterialShop)
      return;
    BMDealerNPC.shopNum = 2;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void MovementShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMMovementAccessoryShop)
      return;
    BMDealerNPC.shopNum = 3;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void CombatShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMCombatAccessoryShop)
      return;
    BMDealerNPC.shopNum = 4;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void InfoShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMInformationShop)
      return;
    BMDealerNPC.shopNum = 5;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void BagShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMBagShop)
      return;
    BMDealerNPC.shopNum = 6;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void CrateShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMCrateShop)
      return;
    BMDealerNPC.shopNum = 7;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void OreShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMOreShop)
      return;
    BMDealerNPC.shopNum = 8;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void NaturalShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMNaturalBlockShop)
      return;
    BMDealerNPC.shopNum = 9;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void BuildingShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMBuildingBlockShop)
      return;
    BMDealerNPC.shopNum = 10;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void HerbShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMHerbShop)
      return;
    BMDealerNPC.shopNum = 11;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void FishShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMFishShop)
      return;
    BMDealerNPC.shopNum = 12;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void MountShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMMountShop)
      return;
    BMDealerNPC.shopNum = 13;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void AmmoShopClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U || !QoLCompendium.QoLCompendium.shopConfig.BMAmmoShop)
      return;
    BMDealerNPC.shopNum = 14;
    BlackMarketDealerNPCUI.visible = false;
  }

  private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart < 10U)
      return;
    SoundEngine.PlaySound(ref SoundID.MenuClose, new Vector2?(), (SoundUpdateCallback) null);
    BlackMarketDealerNPCUI.visible = false;
  }

  private void DragStart(UIMouseEvent evt, UIElement listeningElement)
  {
    this.offset = new Vector2(evt.MousePosition.X - ((UIElement) this.ShopPanel).Left.Pixels, evt.MousePosition.Y - ((UIElement) this.ShopPanel).Top.Pixels);
    this.dragging = true;
  }

  private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
  {
    Vector2 mousePosition = evt.MousePosition;
    this.dragging = false;
    ((StyleDimension) ref ((UIElement) this.ShopPanel).Left).Set(mousePosition.X - this.offset.X, 0.0f);
    ((StyleDimension) ref ((UIElement) this.ShopPanel).Top).Set(mousePosition.Y - this.offset.Y, 0.0f);
    ((UIElement) this).Recalculate();
  }

  protected virtual void DrawSelf(SpriteBatch spriteBatch)
  {
    Vector2 vector2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2).\u002Ector((float) Main.mouseX, (float) Main.mouseY);
    if (((UIElement) this.ShopPanel).ContainsPoint(vector2))
      Main.LocalPlayer.mouseInterface = true;
    if (!this.dragging)
      return;
    ((StyleDimension) ref ((UIElement) this.ShopPanel).Left).Set(vector2.X - this.offset.X, 0.0f);
    ((StyleDimension) ref ((UIElement) this.ShopPanel).Top).Set(vector2.Y - this.offset.Y, 0.0f);
    ((UIElement) this).Recalculate();
  }
}
