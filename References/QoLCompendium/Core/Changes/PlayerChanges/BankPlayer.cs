// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.PlayerChanges.BankPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Projectiles.MobileStorages;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.PlayerChanges;

public class BankPlayer : ModPlayer
{
  internal bool chests;
  internal int pig = -1;
  internal int safe = -1;
  internal int defenders = -1;
  internal int vault = -1;

  public virtual void SetControls()
  {
    if (((Entity) this.Player).whoAmI != Main.myPlayer || !this.chests)
      return;
    Main.SmartCursorShowing = false;
    Player.tileRangeX = 9999;
    Player.tileRangeY = 5000;
    if (this.Player.chest >= -1)
    {
      this.pig = -1;
      this.safe = -1;
      this.defenders = -1;
      this.vault = -1;
      this.chests = false;
    }
    if (this.safe != -1 && Main.projectile[this.safe].type != ModContent.ProjectileType<FlyingSafeProjectile>())
    {
      this.safe = -1;
      this.chests = false;
    }
    if (this.defenders == -1 || Main.projectile[this.defenders].type == ModContent.ProjectileType<EtherianConstructProjectile>())
      return;
    this.defenders = -1;
    this.chests = false;
  }

  public virtual void UpdateEquips()
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.UtilityAccessoriesWorkInBanks)
      return;
    for (int index = 0; index < this.Player.bank.item.Length; ++index)
      this.CheckForBankItems(this.Player.bank.item[index]);
    for (int index = 0; index < this.Player.bank2.item.Length; ++index)
      this.CheckForBankItems(this.Player.bank2.item[index]);
    for (int index = 0; index < this.Player.bank3.item.Length; ++index)
      this.CheckForBankItems(this.Player.bank3.item[index]);
    for (int index = 0; index < this.Player.bank4.item.Length; ++index)
      this.CheckForBankItems(this.Player.bank4.item[index]);
  }

  public void CheckForBankItems(Item item)
  {
    if (!Common.BankItems.Contains(item.type) || item.type == Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ElectromagneticDeterrent"))
      return;
    this.Player.GetModPlayer<QoLCPlayer>().activeItems.Add(item.type);
    this.Player.ApplyEquipFunctional(item, true);
    this.Player.RefreshInfoAccsFromItemType(item);
    this.Player.RefreshMechanicalAccsFromItemType(item.type);
    if (item.type != 3090)
      return;
    this.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "Clot")] = true;
    this.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "GelatinousCube")] = true;
    this.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "GelatinousSludge")] = true;
    this.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "GildedSlime")] = true;
    this.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "GildedSlimeling")] = true;
    this.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "GraniteFusedSlime")] = true;
    this.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "LivingHemorrhage")] = true;
    this.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "SpaceSlime")] = true;
    this.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "CrownofThorns")] = true;
    this.Player.npcTypeNoAggro[Common.GetModNPC(ModConditions.thoriumMod, "BloodDrop")] = true;
  }
}
