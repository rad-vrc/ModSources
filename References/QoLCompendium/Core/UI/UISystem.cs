// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.UISystem
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core.UI.Panels;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI;

public class UISystem : ModSystem
{
  public GameTime oldUiGameTime;
  public BlackMarketDealerNPCUI blackMarketDealerNPCShopUI;
  public UserInterface blackMarketDealerNPCInterface;
  public EtherealCollectorNPCUI etherealCollectorNPCShopUI;
  public UserInterface etherealCollectorNPCInterface;
  public AllInOneAccessUI allInOneAccessUI;
  public UserInterface allInOneAccessInterface;
  public DestinationGlobeUI destinationGlobeUI;
  public UserInterface destinationGlobeInterface;
  public EntityManipulatorUI entityManipulatorUI;
  public UserInterface entityManipulatorInterface;
  public PhaseInterrupterUI phaseInterrupterUI;
  public UserInterface phaseInterrupterInterface;
  public SummoningRemoteUI summoningRemoteUI;
  public UserInterface summoningRemoteInterface;
  public PermanentBuffSelectorUI permanentBuffSelectorUI;
  public UserInterface permanentBuffSelectorInterface;
  public PermanentBuffUI permanentBuffUI;
  public UserInterface permanentBuffInterface;
  public PermanentCalamityBuffUI permanentCalamityBuffUI;
  public UserInterface permanentCalamityBuffInterface;
  public PermanentMartinsOrderBuffUI permanentMartinsOrderBuffUI;
  public UserInterface permanentMartinsOrderBuffInterface;
  public PermanentSOTSBuffUI permanentSOTSBuffUI;
  public UserInterface permanentSOTSBuffInterface;
  public PermanentSpiritClassicBuffUI permanentSpiritClassicBuffUI;
  public UserInterface permanentSpiritClassicBuffInterface;
  public PermanentThoriumBuffUI permanentThoriumBuffUI;
  public UserInterface permanentThoriumBuffInterface;

  public static LocalizedText BMPotionText { get; private set; }

  public static LocalizedText BMStationText { get; private set; }

  public static LocalizedText BMMaterialText { get; private set; }

  public static LocalizedText BMMovementAccessoryText { get; private set; }

  public static LocalizedText BMCombatAccessoryText { get; private set; }

  public static LocalizedText BMInformativeText { get; private set; }

  public static LocalizedText BMBagText { get; private set; }

  public static LocalizedText BMCrateText { get; private set; }

  public static LocalizedText BMOreText { get; private set; }

  public static LocalizedText BMNaturalBlockText { get; private set; }

  public static LocalizedText BMBuildingBlockText { get; private set; }

  public static LocalizedText BMHerbText { get; private set; }

  public static LocalizedText BMFishText { get; private set; }

  public static LocalizedText BMMountText { get; private set; }

  public static LocalizedText BMAmmoText { get; private set; }

  public static LocalizedText ECPotionText { get; private set; }

  public static LocalizedText ECStationText { get; private set; }

  public static LocalizedText ECMaterialText { get; private set; }

  public static LocalizedText ECMaterial2Text { get; private set; }

  public static LocalizedText ECBagText { get; private set; }

  public static LocalizedText ECCrateText { get; private set; }

  public static LocalizedText ECOreText { get; private set; }

  public static LocalizedText ECNaturalBlockText { get; private set; }

  public static LocalizedText ECBuildingBlockText { get; private set; }

  public static LocalizedText ECHerbText { get; private set; }

  public static LocalizedText ECFishText { get; private set; }

  public static LocalizedText PiggyBankText { get; private set; }

  public static LocalizedText SafeText { get; private set; }

  public static LocalizedText DefendersForgeText { get; private set; }

  public static LocalizedText VoidVaultText { get; private set; }

  public static LocalizedText DesertText { get; private set; }

  public static LocalizedText SnowText { get; private set; }

  public static LocalizedText JungleText { get; private set; }

  public static LocalizedText GlowingMushroomText { get; private set; }

  public static LocalizedText CorruptionText { get; private set; }

  public static LocalizedText CrimsonText { get; private set; }

  public static LocalizedText HallowText { get; private set; }

  public static LocalizedText PurityText { get; private set; }

  public static LocalizedText BossText { get; private set; }

  public static LocalizedText EventText { get; private set; }

  public static LocalizedText MinibossText { get; private set; }

  public static LocalizedText IncreaseSpawnText { get; private set; }

  public static LocalizedText DecreaseSpawnText { get; private set; }

  public static LocalizedText CancelSpawnText { get; private set; }

  public static LocalizedText CancelEventText { get; private set; }

  public static LocalizedText CancelSpawnAndEventText { get; private set; }

  public static LocalizedText RevertText { get; private set; }

  public static LocalizedText FullMoonText { get; private set; }

  public static LocalizedText WaningGibbousText { get; private set; }

  public static LocalizedText ThirdQuarterText { get; private set; }

  public static LocalizedText WaningCrescentText { get; private set; }

  public static LocalizedText NewMoonText { get; private set; }

  public static LocalizedText WaxingCrescentText { get; private set; }

  public static LocalizedText FirstQuarterText { get; private set; }

  public static LocalizedText WaxingGibbousText { get; private set; }

  public static LocalizedText VanillaText { get; private set; }

  public static LocalizedText CalamityText { get; private set; }

  public static LocalizedText MartinsOrderText { get; private set; }

  public static LocalizedText SOTSText { get; private set; }

  public static LocalizedText SpiritClassicText { get; private set; }

  public static LocalizedText ThoriumText { get; private set; }

  public static LocalizedText ArenaText { get; private set; }

  public static LocalizedText PotionText { get; private set; }

  public static LocalizedText StationText { get; private set; }

  public static LocalizedText AddonText { get; private set; }

  public static LocalizedText RepellentText { get; private set; }

  public static LocalizedText CloseText { get; private set; }

  public static LocalizedText ResetText { get; private set; }

  public static LocalizedText UnloadedText { get; private set; }

  public virtual void Load()
  {
    UISystem.BMPotionText = ((ModType) this).Mod.GetLocalization("UIText.BMPotionText", (Func<string>) null);
    UISystem.BMStationText = ((ModType) this).Mod.GetLocalization("UIText.BMStationText", (Func<string>) null);
    UISystem.BMMaterialText = ((ModType) this).Mod.GetLocalization("UIText.BMMaterialText", (Func<string>) null);
    UISystem.BMMovementAccessoryText = ((ModType) this).Mod.GetLocalization("UIText.BMMovementAccessoryText", (Func<string>) null);
    UISystem.BMCombatAccessoryText = ((ModType) this).Mod.GetLocalization("UIText.BMCombatAccessoryText", (Func<string>) null);
    UISystem.BMInformativeText = ((ModType) this).Mod.GetLocalization("UIText.BMInformativeText", (Func<string>) null);
    UISystem.BMBagText = ((ModType) this).Mod.GetLocalization("UIText.BMBagText", (Func<string>) null);
    UISystem.BMCrateText = ((ModType) this).Mod.GetLocalization("UIText.BMCrateText", (Func<string>) null);
    UISystem.BMOreText = ((ModType) this).Mod.GetLocalization("UIText.BMOreText", (Func<string>) null);
    UISystem.BMNaturalBlockText = ((ModType) this).Mod.GetLocalization("UIText.BMNaturalBlockText", (Func<string>) null);
    UISystem.BMBuildingBlockText = ((ModType) this).Mod.GetLocalization("UIText.BMBuildingBlockText", (Func<string>) null);
    UISystem.BMHerbText = ((ModType) this).Mod.GetLocalization("UIText.BMHerbText", (Func<string>) null);
    UISystem.BMFishText = ((ModType) this).Mod.GetLocalization("UIText.BMFishText", (Func<string>) null);
    UISystem.BMMountText = ((ModType) this).Mod.GetLocalization("UIText.BMMountText", (Func<string>) null);
    UISystem.BMAmmoText = ((ModType) this).Mod.GetLocalization("UIText.BMAmmoText", (Func<string>) null);
    UISystem.ECPotionText = ((ModType) this).Mod.GetLocalization("UIText.ECPotionText", (Func<string>) null);
    UISystem.ECStationText = ((ModType) this).Mod.GetLocalization("UIText.ECStationText", (Func<string>) null);
    UISystem.ECMaterialText = ((ModType) this).Mod.GetLocalization("UIText.ECMaterialText", (Func<string>) null);
    UISystem.ECMaterial2Text = ((ModType) this).Mod.GetLocalization("UIText.ECMaterial2Text", (Func<string>) null);
    UISystem.ECBagText = ((ModType) this).Mod.GetLocalization("UIText.ECBagText", (Func<string>) null);
    UISystem.ECCrateText = ((ModType) this).Mod.GetLocalization("UIText.ECCrateText", (Func<string>) null);
    UISystem.ECOreText = ((ModType) this).Mod.GetLocalization("UIText.ECOreText", (Func<string>) null);
    UISystem.ECNaturalBlockText = ((ModType) this).Mod.GetLocalization("UIText.ECNaturalBlockText", (Func<string>) null);
    UISystem.ECBuildingBlockText = ((ModType) this).Mod.GetLocalization("UIText.ECBuildingBlockText", (Func<string>) null);
    UISystem.ECHerbText = ((ModType) this).Mod.GetLocalization("UIText.ECHerbText", (Func<string>) null);
    UISystem.ECFishText = ((ModType) this).Mod.GetLocalization("UIText.ECFishText", (Func<string>) null);
    UISystem.PiggyBankText = ((ModType) this).Mod.GetLocalization("UIText.PiggyBankText", (Func<string>) null);
    UISystem.SafeText = ((ModType) this).Mod.GetLocalization("UIText.SafeText", (Func<string>) null);
    UISystem.DefendersForgeText = ((ModType) this).Mod.GetLocalization("UIText.DefendersForgeText", (Func<string>) null);
    UISystem.VoidVaultText = ((ModType) this).Mod.GetLocalization("UIText.VoidVaultText", (Func<string>) null);
    UISystem.DesertText = ((ModType) this).Mod.GetLocalization("UIText.DesertText", (Func<string>) null);
    UISystem.SnowText = ((ModType) this).Mod.GetLocalization("UIText.SnowText", (Func<string>) null);
    UISystem.JungleText = ((ModType) this).Mod.GetLocalization("UIText.JungleText", (Func<string>) null);
    UISystem.GlowingMushroomText = ((ModType) this).Mod.GetLocalization("UIText.GlowingMushroomText", (Func<string>) null);
    UISystem.CorruptionText = ((ModType) this).Mod.GetLocalization("UIText.CorruptionText", (Func<string>) null);
    UISystem.CrimsonText = ((ModType) this).Mod.GetLocalization("UIText.CrimsonText", (Func<string>) null);
    UISystem.HallowText = ((ModType) this).Mod.GetLocalization("UIText.HallowText", (Func<string>) null);
    UISystem.PurityText = ((ModType) this).Mod.GetLocalization("UIText.PurityText", (Func<string>) null);
    UISystem.BossText = ((ModType) this).Mod.GetLocalization("UIText.BossText", (Func<string>) null);
    UISystem.EventText = ((ModType) this).Mod.GetLocalization("UIText.EventText", (Func<string>) null);
    UISystem.MinibossText = ((ModType) this).Mod.GetLocalization("UIText.MinibossText", (Func<string>) null);
    UISystem.IncreaseSpawnText = ((ModType) this).Mod.GetLocalization("UIText.IncreaseSpawnText", (Func<string>) null);
    UISystem.DecreaseSpawnText = ((ModType) this).Mod.GetLocalization("UIText.DecreaseSpawnText", (Func<string>) null);
    UISystem.CancelSpawnText = ((ModType) this).Mod.GetLocalization("UIText.CancelSpawnText", (Func<string>) null);
    UISystem.CancelEventText = ((ModType) this).Mod.GetLocalization("UIText.CancelEventText", (Func<string>) null);
    UISystem.CancelSpawnAndEventText = ((ModType) this).Mod.GetLocalization("UIText.CancelSpawnAndEventText", (Func<string>) null);
    UISystem.RevertText = ((ModType) this).Mod.GetLocalization("UIText.RevertText", (Func<string>) null);
    UISystem.FullMoonText = ((ModType) this).Mod.GetLocalization("UIText.FullMoonText", (Func<string>) null);
    UISystem.WaningGibbousText = ((ModType) this).Mod.GetLocalization("UIText.WaningGibbousText", (Func<string>) null);
    UISystem.ThirdQuarterText = ((ModType) this).Mod.GetLocalization("UIText.ThirdQuarterText", (Func<string>) null);
    UISystem.WaningCrescentText = ((ModType) this).Mod.GetLocalization("UIText.WaningCrescentText", (Func<string>) null);
    UISystem.NewMoonText = ((ModType) this).Mod.GetLocalization("UIText.NewMoonText", (Func<string>) null);
    UISystem.WaxingCrescentText = ((ModType) this).Mod.GetLocalization("UIText.WaxingCrescentText", (Func<string>) null);
    UISystem.FirstQuarterText = ((ModType) this).Mod.GetLocalization("UIText.FirstQuarterText", (Func<string>) null);
    UISystem.WaxingGibbousText = ((ModType) this).Mod.GetLocalization("UIText.WaxingGibbousText", (Func<string>) null);
    UISystem.VanillaText = ((ModType) this).Mod.GetLocalization("UIText.VanillaText", (Func<string>) null);
    UISystem.CalamityText = ((ModType) this).Mod.GetLocalization("UIText.CalamityText", (Func<string>) null);
    UISystem.MartinsOrderText = ((ModType) this).Mod.GetLocalization("UIText.MartinsOrderText", (Func<string>) null);
    UISystem.SOTSText = ((ModType) this).Mod.GetLocalization("UIText.SOTSText", (Func<string>) null);
    UISystem.SpiritClassicText = ((ModType) this).Mod.GetLocalization("UIText.SpiritClassicText", (Func<string>) null);
    UISystem.ThoriumText = ((ModType) this).Mod.GetLocalization("UIText.ThoriumText", (Func<string>) null);
    UISystem.ArenaText = ((ModType) this).Mod.GetLocalization("UIText.ArenaText", (Func<string>) null);
    UISystem.PotionText = ((ModType) this).Mod.GetLocalization("UIText.PotionText", (Func<string>) null);
    UISystem.StationText = ((ModType) this).Mod.GetLocalization("UIText.StationText", (Func<string>) null);
    UISystem.AddonText = ((ModType) this).Mod.GetLocalization("UIText.AddonText", (Func<string>) null);
    UISystem.RepellentText = ((ModType) this).Mod.GetLocalization("UIText.RepellentText", (Func<string>) null);
    UISystem.CloseText = ((ModType) this).Mod.GetLocalization("UIText.CloseText", (Func<string>) null);
    UISystem.ResetText = ((ModType) this).Mod.GetLocalization("UIText.ResetText", (Func<string>) null);
    UISystem.UnloadedText = ((ModType) this).Mod.GetLocalization("UIText.UnloadedText", (Func<string>) null);
    if (Main.dedServ)
      return;
    this.blackMarketDealerNPCShopUI = new BlackMarketDealerNPCUI();
    ((UIElement) this.blackMarketDealerNPCShopUI).Activate();
    this.blackMarketDealerNPCInterface = new UserInterface();
    this.blackMarketDealerNPCInterface.SetState((UIState) this.blackMarketDealerNPCShopUI);
    this.etherealCollectorNPCShopUI = new EtherealCollectorNPCUI();
    ((UIElement) this.etherealCollectorNPCShopUI).Activate();
    this.etherealCollectorNPCInterface = new UserInterface();
    this.etherealCollectorNPCInterface.SetState((UIState) this.etherealCollectorNPCShopUI);
    this.allInOneAccessUI = new AllInOneAccessUI();
    ((UIElement) this.allInOneAccessUI).Activate();
    this.allInOneAccessInterface = new UserInterface();
    this.allInOneAccessInterface.SetState((UIState) this.allInOneAccessUI);
    this.destinationGlobeUI = new DestinationGlobeUI();
    ((UIElement) this.destinationGlobeUI).Activate();
    this.destinationGlobeInterface = new UserInterface();
    this.destinationGlobeInterface.SetState((UIState) this.destinationGlobeUI);
    this.entityManipulatorUI = new EntityManipulatorUI();
    ((UIElement) this.entityManipulatorUI).Activate();
    this.entityManipulatorInterface = new UserInterface();
    this.entityManipulatorInterface.SetState((UIState) this.entityManipulatorUI);
    this.phaseInterrupterUI = new PhaseInterrupterUI();
    ((UIElement) this.phaseInterrupterUI).Activate();
    this.phaseInterrupterInterface = new UserInterface();
    this.phaseInterrupterInterface.SetState((UIState) this.phaseInterrupterUI);
    this.summoningRemoteUI = new SummoningRemoteUI();
    ((UIElement) this.summoningRemoteUI).Activate();
    this.summoningRemoteInterface = new UserInterface();
    this.summoningRemoteInterface.SetState((UIState) this.summoningRemoteUI);
    this.permanentBuffUI = new PermanentBuffUI();
    ((UIElement) this.permanentBuffUI).Activate();
    this.permanentBuffInterface = new UserInterface();
    this.permanentBuffInterface.SetState((UIState) this.permanentBuffUI);
    this.permanentCalamityBuffUI = new PermanentCalamityBuffUI();
    ((UIElement) this.permanentCalamityBuffUI).Activate();
    this.permanentCalamityBuffInterface = new UserInterface();
    this.permanentCalamityBuffInterface.SetState((UIState) this.permanentCalamityBuffUI);
    this.permanentMartinsOrderBuffUI = new PermanentMartinsOrderBuffUI();
    ((UIElement) this.permanentMartinsOrderBuffUI).Activate();
    this.permanentMartinsOrderBuffInterface = new UserInterface();
    this.permanentMartinsOrderBuffInterface.SetState((UIState) this.permanentMartinsOrderBuffUI);
    this.permanentSOTSBuffUI = new PermanentSOTSBuffUI();
    ((UIElement) this.permanentSOTSBuffUI).Activate();
    this.permanentSOTSBuffInterface = new UserInterface();
    this.permanentSOTSBuffInterface.SetState((UIState) this.permanentSOTSBuffUI);
    this.permanentSpiritClassicBuffUI = new PermanentSpiritClassicBuffUI();
    ((UIElement) this.permanentSpiritClassicBuffUI).Activate();
    this.permanentSpiritClassicBuffInterface = new UserInterface();
    this.permanentSpiritClassicBuffInterface.SetState((UIState) this.permanentSpiritClassicBuffUI);
    this.permanentThoriumBuffUI = new PermanentThoriumBuffUI();
    ((UIElement) this.permanentThoriumBuffUI).Activate();
    this.permanentThoriumBuffInterface = new UserInterface();
    this.permanentThoriumBuffInterface.SetState((UIState) this.permanentThoriumBuffUI);
    this.permanentBuffSelectorUI = new PermanentBuffSelectorUI();
    ((UIElement) this.permanentBuffSelectorUI).Activate();
    this.permanentBuffSelectorInterface = new UserInterface();
    this.permanentBuffSelectorInterface.SetState((UIState) this.permanentBuffSelectorUI);
  }

  public virtual void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
  {
    int index = layers.FindIndex((Predicate<GameInterfaceLayer>) (layer => layer.Name.Equals("Vanilla: Mouse Text")));
    if (index == -1)
      return;
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Shop Selector", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_1)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Modded Shop Selector", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_2)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Storage Selector", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_3)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Biome Selector", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_4)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Spawn Selector", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_5)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Moon Selector", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_6)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Summon Selector", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_7)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Buff Selector", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_8)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Buff Toggles", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_9)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Calamity Buff Toggles", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_10)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Martins Order Buff Toggles", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_11)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: SOTS Buff Toggles", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_12)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Spirit Classic Buff Toggles", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_13)), (InterfaceScaleType) 1));
    // ISSUE: method pointer
    layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("QoLC: Thorium Buff Toggles", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__306_14)), (InterfaceScaleType) 1));
  }

  public virtual void UpdateUI(GameTime gameTime)
  {
    this.oldUiGameTime = gameTime;
    if (this.blackMarketDealerNPCInterface.CurrentState != null && BlackMarketDealerNPCUI.visible)
      this.blackMarketDealerNPCInterface.Update(gameTime);
    if (this.etherealCollectorNPCInterface.CurrentState != null && EtherealCollectorNPCUI.visible)
      this.etherealCollectorNPCInterface.Update(gameTime);
    if (this.allInOneAccessInterface.CurrentState != null && AllInOneAccessUI.visible)
      this.allInOneAccessInterface.Update(gameTime);
    if (this.destinationGlobeInterface.CurrentState != null && DestinationGlobeUI.visible)
      this.destinationGlobeInterface.Update(gameTime);
    if (this.entityManipulatorInterface.CurrentState != null && EntityManipulatorUI.visible)
      this.entityManipulatorInterface.Update(gameTime);
    if (this.phaseInterrupterInterface.CurrentState != null && PhaseInterrupterUI.visible)
      this.phaseInterrupterInterface.Update(gameTime);
    if (this.summoningRemoteInterface.CurrentState != null && SummoningRemoteUI.visible)
      this.summoningRemoteInterface.Update(gameTime);
    if (this.permanentBuffInterface.CurrentState != null && PermanentBuffUI.visible)
      this.permanentBuffInterface.Update(gameTime);
    if (this.permanentCalamityBuffInterface.CurrentState != null && PermanentCalamityBuffUI.visible)
      this.permanentCalamityBuffInterface.Update(gameTime);
    if (this.permanentMartinsOrderBuffInterface.CurrentState != null && PermanentMartinsOrderBuffUI.visible)
      this.permanentMartinsOrderBuffInterface.Update(gameTime);
    if (this.permanentSOTSBuffInterface.CurrentState != null && PermanentSOTSBuffUI.visible)
      this.permanentSOTSBuffInterface.Update(gameTime);
    if (this.permanentSpiritClassicBuffInterface.CurrentState != null && PermanentSpiritClassicBuffUI.visible)
      this.permanentSpiritClassicBuffInterface.Update(gameTime);
    if (this.permanentThoriumBuffInterface.CurrentState != null && PermanentThoriumBuffUI.visible)
      this.permanentThoriumBuffInterface.Update(gameTime);
    if (this.permanentBuffSelectorInterface.CurrentState == null || !PermanentBuffSelectorUI.visible)
      return;
    this.permanentBuffSelectorInterface.Update(gameTime);
  }
}
