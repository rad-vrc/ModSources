// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Other.ShopExpander
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Mono.Cecil.Cil;
using MonoMod.Cil;
using QoLCompendium.Core.UI.Buttons;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI.Other;

public class ShopExpander : ModSystem
{
  public static ShopExpander instance;
  public int page;
  public int pageCount;
  public List<Item> items;
  public Item[] currentItems = new Item[40];

  public virtual bool IsLoadingEnabled(Mod mod)
  {
    Mod mod1;
    Terraria.ModLoader.ModLoader.TryGetMod("VanillaQoL", ref mod1);
    return mod1 == null;
  }

  public virtual void Load()
  {
    ShopExpander.instance = this;
    // ISSUE: method pointer
    IL_Chest.SetupShop_string_NPC += new ILContext.Manipulator((object) this, __methodptr(shopExpanderPatch));
  }

  public virtual void Unload() => ShopExpander.instance = (ShopExpander) null;

  public virtual void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
  {
    int index = layers.FindIndex((Predicate<GameInterfaceLayer>) (layer => layer.Name.Equals("Vanilla: Inventory")));
    if (index == -1)
      return;
    layers.Insert(index + 1, (GameInterfaceLayer) new ShopButtons());
  }

  public void shopExpanderPatch(ILContext il)
  {
    ILCursor ilCursor = new ILCursor(il);
    ilCursor.EmitLdarg0();
    ilCursor.EmitLdarg1();
    ilCursor.EmitLdarg2();
    ilCursor.Emit<ShopExpander>(OpCodes.Call, "hijackSetupShop");
    ilCursor.EmitRet();
  }

  public static void HijackSetupShop(Chest self, string shopName, NPC npc)
  {
    Array.Fill<Item>(self.item, (Item) null);
    List<Item> objList1 = new List<Item>();
    AbstractNPCShop abstractNpcShop;
    if (NPCShopDatabase.TryGetNPCShop(shopName, ref abstractNpcShop))
      abstractNpcShop.FillShop((ICollection<Item>) objList1, npc);
    int num = 40 - objList1.Count;
    for (int index = 0; index < num; ++index)
      objList1.Add(new Item());
    Item[] array = objList1.ToArray();
    if (npc != null)
      NPCLoader.ModifyActiveShop(npc, shopName, array);
    List<Item> objList2 = new List<Item>((IEnumerable<Item>) array);
    Span<Item> span = CollectionsMarshal.AsSpan<Item>(objList2);
    for (int index = 0; index < span.Length; ++index)
    {
      ref Item local = ref span[index];
      if (local == null)
        local = new Item();
      local.isAShopItem = true;
    }
    ShopExpander.instance.items = objList2;
    ShopExpander.instance.LoadPage();
    self.item = ShopExpander.instance.currentItems;
  }

  private void LoadPage()
  {
    this.page = 0;
    this.pageCount = (int) Math.Ceiling((double) this.items.Count / 40.0);
    if (this.items.Count % 40 == 0 && this.items[39].type != 0)
      ++this.pageCount;
    this.Refresh();
  }

  public void Refresh()
  {
    int num1 = this.page * 40;
    int num2 = num1 + 40;
    int index1 = 0;
    for (int index2 = num1; index2 < num2; ++index2)
    {
      this.currentItems[index1] = index2 >= this.items.Count ? new Item() : this.items[index2];
      ++index1;
    }
  }
}
