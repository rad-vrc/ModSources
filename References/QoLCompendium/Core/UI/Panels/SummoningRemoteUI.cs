// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Panels.SummoningRemoteUI
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI.Panels;

public class SummoningRemoteUI : UIState
{
  public UIPanel BossPanel;
  public static bool visible;
  public static uint timeStart;
  private Vector2 offset;
  public bool dragging;

  public virtual void OnInitialize()
  {
    this.BossPanel = new UIPanel();
    ((UIElement) this.BossPanel).SetPadding(0.0f);
    ((StyleDimension) ref ((UIElement) this.BossPanel).Left).Set(575f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.BossPanel).Top).Set(275f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.BossPanel).Width).Set(390f, 0.0f);
    ((StyleDimension) ref ((UIElement) this.BossPanel).Height).Set(510f, 0.0f);
    this.BossPanel.BackgroundColor = new Color(204, 43, 43);
    // ISSUE: method pointer
    ((UIElement) this.BossPanel).OnLeftMouseDown += new UIElement.MouseEvent((object) this, __methodptr(DragStart));
    // ISSUE: method pointer
    ((UIElement) this.BossPanel).OnLeftMouseUp += new UIElement.MouseEvent((object) this, __methodptr(DragEnd));
    UIText uiText1 = new UIText(UISystem.BossText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText1).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText1).Top).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText1).Width).Set(50f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText1).Height).Set(22f, 0.0f);
    ((UIElement) this.BossPanel).Append((UIElement) uiText1);
    Asset<Texture2D> asset1 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/KingSlime", (AssetRequestMode) 2);
    Asset<Texture2D> asset2 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/EyeOfCthulhu", (AssetRequestMode) 2);
    Asset<Texture2D> asset3 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/EaterOfWorlds", (AssetRequestMode) 2);
    Asset<Texture2D> asset4 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/BrainOfCthulhu", (AssetRequestMode) 2);
    Asset<Texture2D> asset5 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/QueenBee", (AssetRequestMode) 2);
    Asset<Texture2D> asset6 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/Skeletron", (AssetRequestMode) 2);
    Asset<Texture2D> asset7 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/Deerclops", (AssetRequestMode) 2);
    Asset<Texture2D> asset8 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/WallOfFlesh", (AssetRequestMode) 2);
    Asset<Texture2D> asset9 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/QueenSlime", (AssetRequestMode) 2);
    Asset<Texture2D> asset10 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/Twins", (AssetRequestMode) 2);
    Asset<Texture2D> asset11 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/Destroyer", (AssetRequestMode) 2);
    Asset<Texture2D> asset12 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/SkeletronPrime", (AssetRequestMode) 2);
    Asset<Texture2D> asset13 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/Plantera", (AssetRequestMode) 2);
    Asset<Texture2D> asset14 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/Golem", (AssetRequestMode) 2);
    Asset<Texture2D> asset15 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/DukeFishron", (AssetRequestMode) 2);
    Asset<Texture2D> asset16 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/EmpressOfLight", (AssetRequestMode) 2);
    Asset<Texture2D> asset17 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/LunaticCultist", (AssetRequestMode) 2);
    Asset<Texture2D> asset18 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Bosses/MoonLord", (AssetRequestMode) 2);
    Asset<Texture2D> asset19 = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete", (AssetRequestMode) 2);
    UIImageButton uiImageButton1 = new UIImageButton(asset1);
    ((StyleDimension) ref ((UIElement) uiImageButton1).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton1).Top).Set(50f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton1).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton1).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton1).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(KingSlimeClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton1);
    UIImageButton uiImageButton2 = new UIImageButton(asset2);
    ((StyleDimension) ref ((UIElement) uiImageButton2).Left).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton2).Top).Set(50f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton2).Width).Set(26f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton2).Height).Set(28f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton2).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(EyeOfCthulhuClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton2);
    UIImageButton uiImageButton3 = new UIImageButton(asset3);
    ((StyleDimension) ref ((UIElement) uiImageButton3).Left).Set(110f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton3).Top).Set(50f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton3).Width).Set(24f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton3).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton3).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(EaterOfWorldsClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton3);
    UIImageButton uiImageButton4 = new UIImageButton(asset4);
    ((StyleDimension) ref ((UIElement) uiImageButton4).Left).Set(160f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton4).Top).Set(50f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton4).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton4).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton4).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BrainOfCthulhuClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton4);
    UIImageButton uiImageButton5 = new UIImageButton(asset5);
    ((StyleDimension) ref ((UIElement) uiImageButton5).Left).Set(210f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton5).Top).Set(50f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton5).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton5).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton5).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(QueenBeeClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton5);
    UIImageButton uiImageButton6 = new UIImageButton(asset6);
    ((StyleDimension) ref ((UIElement) uiImageButton6).Left).Set(260f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton6).Top).Set(50f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton6).Width).Set(26f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton6).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton6).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SkeletronClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton6);
    UIImageButton uiImageButton7 = new UIImageButton(asset7);
    ((StyleDimension) ref ((UIElement) uiImageButton7).Left).Set(310f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton7).Top).Set(50f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton7).Width).Set(48f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton7).Height).Set(40f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton7).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DeerclopsClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton7);
    UIImageButton uiImageButton8 = new UIImageButton(asset8);
    ((StyleDimension) ref ((UIElement) uiImageButton8).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton8).Top).Set(100f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton8).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton8).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton8).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WallOfFleshClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton8);
    UIImageButton uiImageButton9 = new UIImageButton(asset9);
    ((StyleDimension) ref ((UIElement) uiImageButton9).Left).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton9).Top).Set(100f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton9).Width).Set(28f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton9).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton9).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(QueenSlimeClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton9);
    UIImageButton uiImageButton10 = new UIImageButton(asset10);
    ((StyleDimension) ref ((UIElement) uiImageButton10).Left).Set(110f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton10).Top).Set(100f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton10).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton10).Height).Set(28f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton10).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(TwinsClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton10);
    UIImageButton uiImageButton11 = new UIImageButton(asset11);
    ((StyleDimension) ref ((UIElement) uiImageButton11).Left).Set(160f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton11).Top).Set(100f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton11).Width).Set(24f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton11).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton11).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DestroyerClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton11);
    UIImageButton uiImageButton12 = new UIImageButton(asset12);
    ((StyleDimension) ref ((UIElement) uiImageButton12).Left).Set(210f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton12).Top).Set(100f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton12).Width).Set(26f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton12).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton12).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SkeletronPrimeClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton12);
    UIImageButton uiImageButton13 = new UIImageButton(asset13);
    ((StyleDimension) ref ((UIElement) uiImageButton13).Left).Set(260f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton13).Top).Set(100f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton13).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton13).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton13).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(PlanteraClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton13);
    UIImageButton uiImageButton14 = new UIImageButton(asset14);
    ((StyleDimension) ref ((UIElement) uiImageButton14).Left).Set(310f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton14).Top).Set(100f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton14).Width).Set(28f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton14).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton14).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(GolemClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton14);
    UIImageButton uiImageButton15 = new UIImageButton(asset15);
    ((StyleDimension) ref ((UIElement) uiImageButton15).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton15).Top).Set(150f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton15).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton15).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton15).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DukeFishronClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton15);
    UIImageButton uiImageButton16 = new UIImageButton(asset16);
    ((StyleDimension) ref ((UIElement) uiImageButton16).Left).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton16).Top).Set(150f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton16).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton16).Height).Set(32f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton16).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(EmpressOfLightClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton16);
    UIImageButton uiImageButton17 = new UIImageButton(asset17);
    ((StyleDimension) ref ((UIElement) uiImageButton17).Left).Set(110f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton17).Top).Set(150f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton17).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton17).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton17).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(LunaticCultistClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton17);
    UIImageButton uiImageButton18 = new UIImageButton(asset18);
    ((StyleDimension) ref ((UIElement) uiImageButton18).Left).Set(160f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton18).Top).Set(150f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton18).Width).Set(34f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton18).Height).Set(36f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton18).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MoonLordClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton18);
    UIText uiText2 = new UIText(UISystem.EventText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText2).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Top).Set(190f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Width).Set(50f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText2).Height).Set(22f, 0.0f);
    ((UIElement) this.BossPanel).Append((UIElement) uiText2);
    Asset<Texture2D> asset20 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Rain", (AssetRequestMode) 2);
    Asset<Texture2D> asset21 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Wind", (AssetRequestMode) 2);
    Asset<Texture2D> asset22 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Sandstorm", (AssetRequestMode) 2);
    Asset<Texture2D> asset23 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Party", (AssetRequestMode) 2);
    Asset<Texture2D> asset24 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/SlimeRain", (AssetRequestMode) 2);
    Asset<Texture2D> asset25 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/BloodMoon", (AssetRequestMode) 2);
    Asset<Texture2D> asset26 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/GoblinArmy", (AssetRequestMode) 2);
    Asset<Texture2D> asset27 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/FrostLegion", (AssetRequestMode) 2);
    Asset<Texture2D> asset28 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Pirates", (AssetRequestMode) 2);
    Asset<Texture2D> asset29 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Eclipse", (AssetRequestMode) 2);
    Asset<Texture2D> asset30 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/PumpkinMoon", (AssetRequestMode) 2);
    Asset<Texture2D> asset31 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/FrostMoon", (AssetRequestMode) 2);
    Asset<Texture2D> asset32 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/Martians", (AssetRequestMode) 2);
    Asset<Texture2D> asset33 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/NebulaPillar", (AssetRequestMode) 2);
    Asset<Texture2D> asset34 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/SolarPillar", (AssetRequestMode) 2);
    Asset<Texture2D> asset35 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/StardustPillar", (AssetRequestMode) 2);
    Asset<Texture2D> asset36 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/VortexPillar", (AssetRequestMode) 2);
    Asset<Texture2D> asset37 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Events/LunarEvent", (AssetRequestMode) 2);
    UIImageButton uiImageButton19 = new UIImageButton(asset20);
    ((StyleDimension) ref ((UIElement) uiImageButton19).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton19).Top).Set(230f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton19).Width).Set(26f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton19).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton19).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(RainClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton19);
    UIImageButton uiImageButton20 = new UIImageButton(asset21);
    ((StyleDimension) ref ((UIElement) uiImageButton20).Left).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton20).Top).Set(230f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton20).Width).Set(28f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton20).Height).Set(26f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton20).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(WindClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton20);
    UIImageButton uiImageButton21 = new UIImageButton(asset22);
    ((StyleDimension) ref ((UIElement) uiImageButton21).Left).Set(110f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton21).Top).Set(230f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton21).Width).Set(24f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton21).Height).Set(24f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton21).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SandstormClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton21);
    UIImageButton uiImageButton22 = new UIImageButton(asset23);
    ((StyleDimension) ref ((UIElement) uiImageButton22).Left).Set(160f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton22).Top).Set(230f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton22).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton22).Height).Set(28f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton22).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(PartyClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton22);
    UIImageButton uiImageButton23 = new UIImageButton(asset24);
    ((StyleDimension) ref ((UIElement) uiImageButton23).Left).Set(210f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton23).Top).Set(230f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton23).Width).Set(20f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton23).Height).Set(26f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton23).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SlimeRainClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton23);
    UIImageButton uiImageButton24 = new UIImageButton(asset25);
    ((StyleDimension) ref ((UIElement) uiImageButton24).Left).Set(260f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton24).Top).Set(230f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton24).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton24).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton24).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BloodMoonClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton24);
    UIImageButton uiImageButton25 = new UIImageButton(asset26);
    ((StyleDimension) ref ((UIElement) uiImageButton25).Left).Set(310f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton25).Top).Set(230f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton25).Width).Set(26f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton25).Height).Set(24f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton25).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(GoblinArmyClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton25);
    UIImageButton uiImageButton26 = new UIImageButton(asset27);
    ((StyleDimension) ref ((UIElement) uiImageButton26).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton26).Top).Set(280f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton26).Width).Set(20f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton26).Height).Set(24f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton26).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(FrostLegionClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton26);
    UIImageButton uiImageButton27 = new UIImageButton(asset28);
    ((StyleDimension) ref ((UIElement) uiImageButton27).Left).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton27).Top).Set(280f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton27).Width).Set(24f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton27).Height).Set(24f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton27).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(PiratesClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton27);
    UIImageButton uiImageButton28 = new UIImageButton(asset29);
    ((StyleDimension) ref ((UIElement) uiImageButton28).Left).Set(110f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton28).Top).Set(280f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton28).Width).Set(24f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton28).Height).Set(24f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton28).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(EclipseClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton28);
    UIImageButton uiImageButton29 = new UIImageButton(asset30);
    ((StyleDimension) ref ((UIElement) uiImageButton29).Left).Set(160f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton29).Top).Set(280f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton29).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton29).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton29).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(PumpkinMoonClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton29);
    UIImageButton uiImageButton30 = new UIImageButton(asset31);
    ((StyleDimension) ref ((UIElement) uiImageButton30).Left).Set(210f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton30).Top).Set(280f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton30).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton30).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton30).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(FrostMoonClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton30);
    UIImageButton uiImageButton31 = new UIImageButton(asset32);
    ((StyleDimension) ref ((UIElement) uiImageButton31).Left).Set(260f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton31).Top).Set(280f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton31).Width).Set(26f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton31).Height).Set(24f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton31).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MartiansClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton31);
    UIImageButton uiImageButton32 = new UIImageButton(asset33);
    ((StyleDimension) ref ((UIElement) uiImageButton32).Left).Set(310f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton32).Top).Set(280f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton32).Width).Set(24f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton32).Height).Set(26f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton32).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(NebulaPillarClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton32);
    UIImageButton uiImageButton33 = new UIImageButton(asset34);
    ((StyleDimension) ref ((UIElement) uiImageButton33).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton33).Top).Set(330f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton33).Width).Set(24f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton33).Height).Set(26f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton33).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SolarPillarClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton33);
    UIImageButton uiImageButton34 = new UIImageButton(asset35);
    ((StyleDimension) ref ((UIElement) uiImageButton34).Left).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton34).Top).Set(330f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton34).Width).Set(24f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton34).Height).Set(26f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton34).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(StardustPillarClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton34);
    UIImageButton uiImageButton35 = new UIImageButton(asset36);
    ((StyleDimension) ref ((UIElement) uiImageButton35).Left).Set(110f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton35).Top).Set(330f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton35).Width).Set(24f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton35).Height).Set(26f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton35).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(VortexPillarClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton35);
    UIImageButton uiImageButton36 = new UIImageButton(asset37);
    ((StyleDimension) ref ((UIElement) uiImageButton36).Left).Set(160f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton36).Top).Set(330f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton36).Width).Set(24f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton36).Height).Set(26f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton36).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(LunarEventClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton36);
    UIText uiText3 = new UIText(UISystem.MinibossText, 1f, false);
    ((StyleDimension) ref ((UIElement) uiText3).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Top).Set(370f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Width).Set(50f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiText3).Height).Set(22f, 0.0f);
    ((UIElement) this.BossPanel).Append((UIElement) uiText3);
    Asset<Texture2D> asset38 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/DarkMage", (AssetRequestMode) 2);
    Asset<Texture2D> asset39 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/Dreadnautilus", (AssetRequestMode) 2);
    Asset<Texture2D> asset40 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/FlyingDutchman", (AssetRequestMode) 2);
    Asset<Texture2D> asset41 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/Ogre", (AssetRequestMode) 2);
    Asset<Texture2D> asset42 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/MourningWood", (AssetRequestMode) 2);
    Asset<Texture2D> asset43 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/Pumpking", (AssetRequestMode) 2);
    Asset<Texture2D> asset44 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/Everscream", (AssetRequestMode) 2);
    Asset<Texture2D> asset45 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/Santank", (AssetRequestMode) 2);
    Asset<Texture2D> asset46 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/IceQueen", (AssetRequestMode) 2);
    Asset<Texture2D> asset47 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/MartianSaucer", (AssetRequestMode) 2);
    Asset<Texture2D> asset48 = ModContent.Request<Texture2D>("QoLCompendium/Assets/Minibosses/Betsy", (AssetRequestMode) 2);
    UIImageButton uiImageButton37 = new UIImageButton(asset38);
    ((StyleDimension) ref ((UIElement) uiImageButton37).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton37).Top).Set(410f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton37).Width).Set(32f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton37).Height).Set(32f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton37).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DarkMageClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton37);
    UIImageButton uiImageButton38 = new UIImageButton(asset39);
    ((StyleDimension) ref ((UIElement) uiImageButton38).Left).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton38).Top).Set(410f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton38).Width).Set(40f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton38).Height).Set(34f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton38).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DreadnautilusClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton38);
    UIImageButton uiImageButton39 = new UIImageButton(asset40);
    ((StyleDimension) ref ((UIElement) uiImageButton39).Left).Set(110f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton39).Top).Set(410f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton39).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton39).Height).Set(28f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton39).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(FlyingDutchmanClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton39);
    UIImageButton uiImageButton40 = new UIImageButton(asset41);
    ((StyleDimension) ref ((UIElement) uiImageButton40).Left).Set(160f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton40).Top).Set(410f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton40).Width).Set(26f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton40).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton40).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(OgreClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton40);
    UIImageButton uiImageButton41 = new UIImageButton(asset42);
    ((StyleDimension) ref ((UIElement) uiImageButton41).Left).Set(210f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton41).Top).Set(410f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton41).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton41).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton41).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MourningWoodClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton41);
    UIImageButton uiImageButton42 = new UIImageButton(asset43);
    ((StyleDimension) ref ((UIElement) uiImageButton42).Left).Set(260f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton42).Top).Set(410f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton42).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton42).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton42).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(PumpkingClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton42);
    UIImageButton uiImageButton43 = new UIImageButton(asset44);
    ((StyleDimension) ref ((UIElement) uiImageButton43).Left).Set(310f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton43).Top).Set(410f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton43).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton43).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton43).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(EverscreamClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton43);
    UIImageButton uiImageButton44 = new UIImageButton(asset45);
    ((StyleDimension) ref ((UIElement) uiImageButton44).Left).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton44).Top).Set(460f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton44).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton44).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton44).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(SantankClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton44);
    UIImageButton uiImageButton45 = new UIImageButton(asset46);
    ((StyleDimension) ref ((UIElement) uiImageButton45).Left).Set(60f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton45).Top).Set(460f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton45).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton45).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton45).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(IceQueenClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton45);
    UIImageButton uiImageButton46 = new UIImageButton(asset47);
    ((StyleDimension) ref ((UIElement) uiImageButton46).Left).Set(110f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton46).Top).Set(460f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton46).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton46).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton46).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(MartianSaucerClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton46);
    UIImageButton uiImageButton47 = new UIImageButton(asset48);
    ((StyleDimension) ref ((UIElement) uiImageButton47).Left).Set(160f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton47).Top).Set(460f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton47).Width).Set(30f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton47).Height).Set(30f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton47).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(BetsyClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton47);
    UIImageButton uiImageButton48 = new UIImageButton(asset19);
    ((StyleDimension) ref ((UIElement) uiImageButton48).Left).Set(360f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton48).Top).Set(10f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton48).Width).Set(22f, 0.0f);
    ((StyleDimension) ref ((UIElement) uiImageButton48).Height).Set(22f, 0.0f);
    // ISSUE: method pointer
    ((UIElement) uiImageButton48).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(CloseButtonClicked));
    ((UIElement) this.BossPanel).Append((UIElement) uiImageButton48);
    ((UIElement) this).Append((UIElement) this.BossPanel);
  }

  private void KingSlimeClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 50;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
    SummoningRemoteUI.visible = false;
  }

  private void EyeOfCthulhuClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 4;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
    SummoningRemoteUI.visible = false;
  }

  private void EaterOfWorldsClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedBoss1)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 13;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[4].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void BrainOfCthulhuClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedBoss1)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 266;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[4].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void QueenBeeClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedBoss2)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 222;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBossWithTwoOptions"), new object[2]
      {
        (object) ContentSamples.NpcsByNetId[13].FullName,
        (object) ContentSamples.NpcsByNetId[266].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void SkeletronClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedBoss2)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 35;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBossWithTwoOptions"), new object[2]
      {
        (object) ContentSamples.NpcsByNetId[13].FullName,
        (object) ContentSamples.NpcsByNetId[266].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void DeerclopsClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 668;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
    SummoningRemoteUI.visible = false;
  }

  private void WallOfFleshClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedBoss3)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 113;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[35].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void QueenSlimeClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (Main.hardMode)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 657;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void TwinsClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (Main.hardMode)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 125;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void DestroyerClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (Main.hardMode)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 134;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void SkeletronPrimeClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (Main.hardMode)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = (int) sbyte.MaxValue;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void PlanteraClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 262;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedThreeMechs"), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void GolemClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedPlantBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 245;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[262].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void DukeFishronClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (Main.hardMode)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 370;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void EmpressOfLightClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedPlantBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 636;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[262].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void LunaticCultistClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedGolemBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 439;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[245].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void MoonLordClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedAncientCultist)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 398;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[439].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void DarkMageClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedBoss2)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 564;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBossWithTwoOptions"), new object[2]
      {
        (object) ContentSamples.NpcsByNetId[13].FullName,
        (object) ContentSamples.NpcsByNetId[266].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void DreadnautilusClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (Main.hardMode)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 618;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void FlyingDutchmanClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (Main.hardMode)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 491;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void OgreClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedMechBossAny)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 576;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedOneMech"), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void MourningWoodClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedPlantBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 325;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[262].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void PumpkingClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedPlantBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 327;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[262].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void EverscreamClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedPlantBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 344;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[262].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void SantankClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedPlantBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 346;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[262].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void IceQueenClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedPlantBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 345;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[262].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void MartianSaucerClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedGolemBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 395;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[245].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void BetsyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedGolemBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = 551;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[245].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void RainClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 1;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
    SummoningRemoteUI.visible = false;
  }

  private void WindClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 2;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
    SummoningRemoteUI.visible = false;
  }

  private void SandstormClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 3;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
    SummoningRemoteUI.visible = false;
  }

  private void PartyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 4;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
    SummoningRemoteUI.visible = false;
  }

  private void SlimeRainClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 5;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
    SummoningRemoteUI.visible = false;
  }

  private void BloodMoonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 6;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
    SummoningRemoteUI.visible = false;
  }

  private void GoblinArmyClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 7;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
    SummoningRemoteUI.visible = false;
  }

  private void FrostLegionClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (Main.hardMode)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 8;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void PiratesClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (Main.hardMode)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 9;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedHardmode"), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void EclipseClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedMechBossAny)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 10;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedOneMech"), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void PumpkinMoonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedPlantBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 11;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[262].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void FrostMoonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedPlantBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 12;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[262].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void MartiansClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedGolemBoss)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 13;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[245].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void NebulaPillarClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedAncientCultist)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 14;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[439].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void SolarPillarClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedAncientCultist)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 15;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[439].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void StardustPillarClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedAncientCultist)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 16 /*0x10*/;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[439].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void VortexPillarClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedAncientCultist)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 17;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[439].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void LunarEventClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    if (NPC.downedAncientCultist)
    {
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = 18;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
      Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
      SummoningRemoteUI.visible = false;
    }
    else
      TextHelper.PrintText(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.LockedBoss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[439].FullName
      }), new Color((int) byte.MaxValue, 240 /*0xF0*/, 20), true);
  }

  private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    SoundEngine.PlaySound(ref SoundID.MenuClose, new Vector2?(), (SoundUpdateCallback) null);
    SummoningRemoteUI.visible = false;
  }

  private void DragStart(UIMouseEvent evt, UIElement listeningElement)
  {
    this.offset = new Vector2(evt.MousePosition.X - ((UIElement) this.BossPanel).Left.Pixels, evt.MousePosition.Y - ((UIElement) this.BossPanel).Top.Pixels);
    this.dragging = true;
  }

  private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
  {
    Vector2 mousePosition = evt.MousePosition;
    this.dragging = false;
    ((StyleDimension) ref ((UIElement) this.BossPanel).Left).Set(mousePosition.X - this.offset.X, 0.0f);
    ((StyleDimension) ref ((UIElement) this.BossPanel).Top).Set(mousePosition.Y - this.offset.Y, 0.0f);
    ((UIElement) this).Recalculate();
  }

  protected virtual void DrawSelf(SpriteBatch spriteBatch)
  {
    Vector2 vector2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2).\u002Ector((float) Main.mouseX, (float) Main.mouseY);
    if (((UIElement) this.BossPanel).ContainsPoint(vector2))
      Main.LocalPlayer.mouseInterface = true;
    if (!this.dragging)
      return;
    ((StyleDimension) ref ((UIElement) this.BossPanel).Left).Set(vector2.X - this.offset.X, 0.0f);
    ((StyleDimension) ref ((UIElement) this.BossPanel).Top).Set(vector2.Y - this.offset.Y, 0.0f);
    ((UIElement) this).Recalculate();
  }

  public static void BossClick(int bossID, SoundStyle sound)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossToSpawn = bossID;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = true;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = false;
  }

  public static void EventClick(int eventID, SoundStyle sound)
  {
    if (Main.GameUpdateCount - SummoningRemoteUI.timeStart < 10U)
      return;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventToSpawn = eventID;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bossSpawn = false;
    Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eventSpawn = true;
  }
}
