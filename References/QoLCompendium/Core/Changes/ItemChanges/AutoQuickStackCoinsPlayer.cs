// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ItemChanges.AutoQuickStackCoinsPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ItemChanges;

public class AutoQuickStackCoinsPlayer : ModPlayer
{
  private int cooldown;

  public virtual void PostUpdate()
  {
    ++this.cooldown;
    if (Main.myPlayer != ((Entity) this.Player).whoAmI || !QoLCompendium.QoLCompendium.mainConfig.AutoMoneyQuickStack)
      return;
    if (this.cooldown % 10 == 0)
      this.DetectCoins();
    if (this.cooldown % 30 != 0)
      return;
    Common.PlatinumMaxStack = new Item(74, 1, 0).maxStack;
  }

  private void DetectCoins()
  {
    bool flag = false;
    for (int index = 50; index <= 53; ++index)
    {
      Item obj = this.Player.inventory[index];
      if (!obj.IsAir && obj.IsACoin && AutoQuickStackCoins.TryDepositACoin(obj, this.Player))
      {
        flag = true;
        obj.TurnToAir(false);
      }
    }
    if (!flag)
      return;
    Recipe.FindRecipes(false);
  }
}
