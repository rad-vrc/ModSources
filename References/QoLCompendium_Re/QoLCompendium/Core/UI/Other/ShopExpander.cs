using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using QoLCompendium.Core.UI.Buttons;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x0200028C RID: 652
	public class ShopExpander : ModSystem
	{
		// Token: 0x060010E3 RID: 4323 RVA: 0x00086064 File Offset: 0x00084264
		public override bool IsLoadingEnabled(Mod mod)
		{
			Mod VanillaQoL;
			ModLoader.TryGetMod("VanillaQoL", out VanillaQoL);
			return VanillaQoL == null;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00086082 File Offset: 0x00084282
		public override void Load()
		{
			ShopExpander.instance = this;
			IL_Chest.SetupShop_string_NPC += new ILContext.Manipulator(this.shopExpanderPatch);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0008609B File Offset: 0x0008429B
		public override void Unload()
		{
			ShopExpander.instance = null;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x000860A4 File Offset: 0x000842A4
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int index = layers.FindIndex((GameInterfaceLayer layer) => layer.Name.Equals("Vanilla: Inventory"));
			if (index == -1)
			{
				return;
			}
			layers.Insert(index + 1, new ShopButtons());
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000860EA File Offset: 0x000842EA
		public void shopExpanderPatch(ILContext il)
		{
			ILCursor ilcursor = new ILCursor(il);
			ilcursor.EmitLdarg0();
			ilcursor.EmitLdarg1();
			ilcursor.EmitLdarg2();
			ilcursor.Emit<ShopExpander>(OpCodes.Call, "hijackSetupShop");
			ilcursor.EmitRet();
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00086120 File Offset: 0x00084320
		public static void HijackSetupShop(Chest self, string shopName, NPC npc)
		{
			Array.Fill<Item>(self.item, null);
			List<Item> items = new List<Item>();
			AbstractNPCShop shop;
			if (NPCShopDatabase.TryGetNPCShop(shopName, out shop))
			{
				shop.FillShop(items, npc);
			}
			int rem = 40 - items.Count;
			for (int i = 0; i < rem; i++)
			{
				items.Add(new Item());
			}
			Item[] itemsToModify = items.ToArray();
			if (npc != null)
			{
				NPCLoader.ModifyActiveShop(npc, shopName, itemsToModify);
			}
			items = new List<Item>(itemsToModify);
			Span<Item> span = CollectionsMarshal.AsSpan<Item>(items);
			for (int j = 0; j < span.Length; j++)
			{
				ref Item item = ref span[j];
				if (item == null)
				{
					item = new Item();
				}
				item.isAShopItem = true;
			}
			ShopExpander.instance.items = items;
			ShopExpander.instance.LoadPage();
			self.item = ShopExpander.instance.currentItems;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000861F4 File Offset: 0x000843F4
		private void LoadPage()
		{
			this.page = 0;
			this.pageCount = (int)Math.Ceiling((double)this.items.Count / 40.0);
			if (this.items.Count % 40 == 0 && this.items[39].type != 0)
			{
				this.pageCount++;
			}
			this.Refresh();
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00086264 File Offset: 0x00084464
		public void Refresh()
		{
			int num = this.page * 40;
			int end = num + 40;
			int ctr = 0;
			for (int i = num; i < end; i++)
			{
				if (i < this.items.Count)
				{
					this.currentItems[ctr] = this.items[i];
				}
				else
				{
					this.currentItems[ctr] = new Item();
				}
				ctr++;
			}
		}

		// Token: 0x0400072D RID: 1837
		public static ShopExpander instance;

		// Token: 0x0400072E RID: 1838
		public int page;

		// Token: 0x0400072F RID: 1839
		public int pageCount;

		// Token: 0x04000730 RID: 1840
		public List<Item> items;

		// Token: 0x04000731 RID: 1841
		public Item[] currentItems = new Item[40];
	}
}
