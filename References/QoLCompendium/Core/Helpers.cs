// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.ShopHelper
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core;

public static class ShopHelper
{
  public static NPCShop AddModItemToShop(this NPCShop shop, Mod mod, string itemName, int price)
  {
    ModItem modItem;
    if (mod != null && mod.TryFind<ModItem>(itemName, ref modItem))
    {
      NPCShop npcShop = shop;
      Item obj = new Item(modItem.Type, 1, 0);
      obj.shopCustomPrice = new int?(price);
      Condition[] conditionArray = Array.Empty<Condition>();
      npcShop.Add(obj, conditionArray);
    }
    return shop;
  }

  public static NPCShop AddModItemToShop(
    this NPCShop shop,
    Mod mod,
    string itemName,
    int price,
    params Condition[] condition)
  {
    ModItem modItem;
    if (mod != null && mod.TryFind<ModItem>(itemName, ref modItem))
    {
      NPCShop npcShop = shop;
      Item obj = new Item(modItem.Type, 1, 0);
      obj.shopCustomPrice = new int?(price);
      Condition[] conditionArray = condition;
      npcShop.Add(obj, conditionArray);
    }
    return shop;
  }

  public static NPCShop AddModItemToShop(
    this NPCShop shop,
    Mod mod,
    string itemName,
    int price,
    Func<bool> predicate)
  {
    ModItem modItem;
    if (mod != null && mod.TryFind<ModItem>(itemName, ref modItem))
    {
      NPCShop npcShop = shop;
      Item obj = new Item(modItem.Type, 1, 0);
      obj.shopCustomPrice = new int?(price);
      Condition[] conditionArray = new Condition[1]
      {
        new Condition("", predicate)
      };
      npcShop.Add(obj, conditionArray);
    }
    return shop;
  }

  public static NPCShop AddModItemToShop<T>(this NPCShop shop, int price) where T : ModItem
  {
    NPCShop npcShop = shop;
    Item obj = new Item(ModContent.ItemType<T>(), 1, 0);
    obj.shopCustomPrice = new int?(price);
    Condition[] conditionArray = Array.Empty<Condition>();
    npcShop.Add(obj, conditionArray);
    return shop;
  }

  public static NPCShop AddModItemToShop<T>(
    this NPCShop shop,
    int price,
    params Condition[] condition)
    where T : ModItem
  {
    NPCShop npcShop = shop;
    Item obj = new Item(ModContent.ItemType<T>(), 1, 0);
    obj.shopCustomPrice = new int?(price);
    Condition[] conditionArray = condition;
    npcShop.Add(obj, conditionArray);
    return shop;
  }

  public static NPCShop AddModItemToShop<T>(this NPCShop shop, int price, Func<bool> predicate) where T : ModItem
  {
    NPCShop npcShop = shop;
    Item obj = new Item(ModContent.ItemType<T>(), 1, 0);
    obj.shopCustomPrice = new int?(price);
    Condition[] conditionArray = new Condition[1]
    {
      new Condition("", predicate)
    };
    npcShop.Add(obj, conditionArray);
    return shop;
  }

  public static void OpenShop(ref string Shop, string shop, ref bool visible)
  {
    Shop = shop;
    visible = false;
    NPC npc = Main.npc[Main.LocalPlayer.talkNPC];
    string str = "";
    npc.ModNPC.SetChatButtons(ref str, ref str);
  }
}
