using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core
{
	// Token: 0x02000203 RID: 515
	public static class ShopHelper
	{
		// Token: 0x06000BC0 RID: 3008 RVA: 0x0004E998 File Offset: 0x0004CB98
		public static NPCShop AddModItemToShop(this NPCShop shop, Mod mod, string itemName, int price)
		{
			ModItem currItem;
			if (mod != null && mod.TryFind<ModItem>(itemName, out currItem))
			{
				shop.Add(new Item(currItem.Type, 1, 0)
				{
					shopCustomPrice = new int?(price)
				}, Array.Empty<Condition>());
			}
			return shop;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0004E9DC File Offset: 0x0004CBDC
		public static NPCShop AddModItemToShop(this NPCShop shop, Mod mod, string itemName, int price, params Condition[] condition)
		{
			ModItem currItem;
			if (mod != null && mod.TryFind<ModItem>(itemName, out currItem))
			{
				shop.Add(new Item(currItem.Type, 1, 0)
				{
					shopCustomPrice = new int?(price)
				}, condition);
			}
			return shop;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0004EA1C File Offset: 0x0004CC1C
		public static NPCShop AddModItemToShop(this NPCShop shop, Mod mod, string itemName, int price, Func<bool> predicate)
		{
			ModItem currItem;
			if (mod != null && mod.TryFind<ModItem>(itemName, out currItem))
			{
				shop.Add(new Item(currItem.Type, 1, 0)
				{
					shopCustomPrice = new int?(price)
				}, new Condition[]
				{
					new Condition("", predicate)
				});
			}
			return shop;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0004EA6D File Offset: 0x0004CC6D
		public static NPCShop AddModItemToShop<T>(this NPCShop shop, int price) where T : ModItem
		{
			shop.Add(new Item(ModContent.ItemType<T>(), 1, 0)
			{
				shopCustomPrice = new int?(price)
			}, Array.Empty<Condition>());
			return shop;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0004EA94 File Offset: 0x0004CC94
		public static NPCShop AddModItemToShop<T>(this NPCShop shop, int price, params Condition[] condition) where T : ModItem
		{
			shop.Add(new Item(ModContent.ItemType<T>(), 1, 0)
			{
				shopCustomPrice = new int?(price)
			}, condition);
			return shop;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0004EAB7 File Offset: 0x0004CCB7
		public static NPCShop AddModItemToShop<T>(this NPCShop shop, int price, Func<bool> predicate) where T : ModItem
		{
			shop.Add(new Item(ModContent.ItemType<T>(), 1, 0)
			{
				shopCustomPrice = new int?(price)
			}, new Condition[]
			{
				new Condition("", predicate)
			});
			return shop;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0004EAF0 File Offset: 0x0004CCF0
		public static void OpenShop(ref string Shop, string shop, ref bool visible)
		{
			Shop = shop;
			visible = false;
			NPC npc = Main.npc[Main.LocalPlayer.talkNPC];
			string _ = "";
			npc.ModNPC.SetChatButtons(ref _, ref _);
		}
	}
}
