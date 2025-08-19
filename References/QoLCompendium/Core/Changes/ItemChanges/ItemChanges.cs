// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ItemChanges.ItemChanges
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ItemChanges;

public class ItemChanges : GlobalItem
{
  public virtual void Load()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_Player.ItemCheck_UseMiningTools_TryHittingWall += QoLCompendium.Core.Changes.ItemChanges.ItemChanges.\u003C\u003Ec.\u003C\u003E9__0_0 ?? (QoLCompendium.Core.Changes.ItemChanges.ItemChanges.\u003C\u003Ec.\u003C\u003E9__0_0 = new On_Player.hook_ItemCheck_UseMiningTools_TryHittingWall((object) QoLCompendium.Core.Changes.ItemChanges.ItemChanges.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CLoad\u003Eb__0_0)));
  }

  public virtual void SetDefaults(Item item)
  {
    if (QoLCompendium.QoLCompendium.mainConfig.NoDeveloperSetsFromBossBags && ItemID.Sets.BossBag[item.type])
      ItemID.Sets.PreHardmodeLikeBossBag[item.type] = true;
    if (QoLCompendium.QoLCompendium.mainConfig.IncreaseMaxStack > 0 && item.maxStack > 10 && item.maxStack != 100 && !Common.IsCoin(item.type))
      item.maxStack = QoLCompendium.QoLCompendium.mainConfig.IncreaseMaxStack;
    if (QoLCompendium.QoLCompendium.mainConfig.StackableQuestItems && item.questItem && QoLCompendium.QoLCompendium.mainConfig.IncreaseMaxStack > 0)
      item.maxStack = QoLCompendium.QoLCompendium.mainConfig.IncreaseMaxStack;
    if (!QoLCompendium.QoLCompendium.mainConfig.AutoReuseUpgrades || !Common.PermanentMultiUseUpgrades.Contains(item.type))
      return;
    item.autoReuse = true;
  }

  public virtual void ExtractinatorUse(
    int extractType,
    int extractinatorBlockType,
    ref int resultType,
    ref int resultStack)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.FasterExtractinator)
      return;
    Main.LocalPlayer.itemAnimation = 1;
    Main.LocalPlayer.itemTime = 1;
    Main.LocalPlayer.itemTimeMax = 1;
  }

  public virtual float UseTimeMultiplier(Item item, Player player)
  {
    return item.pick > 0 || item.hammer > 0 || item.axe > 0 || item.type == 510 ? 1f - QoLCompendium.QoLCompendium.mainConfig.IncreaseToolSpeed : base.UseTimeMultiplier(item, player);
  }

  public virtual bool ReforgePrice(Item item, ref int reforgePrice, ref bool canApplyDiscount)
  {
    reforgePrice = (int) ((double) reforgePrice * (1.0 - (double) QoLCompendium.QoLCompendium.mainConfig.ReforgePriceChange * 0.0099999997764825821));
    return true;
  }

  public virtual bool ConsumeItem(Item item, Player player)
  {
    return (!item.consumable || item.damage != -1 || item.stack < QoLCompendium.QoLCompendium.mainConfig.EndlessItemAmount || !QoLCompendium.QoLCompendium.mainConfig.EndlessConsumables) && (!item.consumable || item.damage <= 0 || item.stack < QoLCompendium.QoLCompendium.mainConfig.EndlessWeaponAmount || !QoLCompendium.QoLCompendium.mainConfig.EndlessWeapons) && base.ConsumeItem(item, player);
  }

  public virtual bool CanBeConsumedAsAmmo(Item ammo, Item weapon, Player player)
  {
    return (ammo.ammo <= AmmoID.None || ammo.stack < QoLCompendium.QoLCompendium.mainConfig.EndlessAmmoAmount || !QoLCompendium.QoLCompendium.mainConfig.EndlessAmmo) && base.CanBeConsumedAsAmmo(ammo, weapon, player);
  }

  public virtual bool? CanConsumeBait(Player player, Item bait)
  {
    return bait.bait > 0 && bait.stack >= QoLCompendium.QoLCompendium.mainConfig.EndlessBaitAmount && QoLCompendium.QoLCompendium.mainConfig.EndlessBait ? new bool?(false) : base.CanConsumeBait(player, bait);
  }
}
