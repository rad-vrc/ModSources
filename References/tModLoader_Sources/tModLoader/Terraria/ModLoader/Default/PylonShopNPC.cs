using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.ModLoader.Default
{
	/// <summary>
	/// This is a GlobalNPC native to tML that handles adding Pylon items to NPC's shops, to save on patch size within vanilla.
	/// </summary>
	// Token: 0x020002CB RID: 715
	public sealed class PylonShopNPC : GlobalNPC
	{
		// Token: 0x06002DB8 RID: 11704 RVA: 0x0052FDB4 File Offset: 0x0052DFB4
		public override void ModifyShop(NPCShop shop)
		{
			if (PylonShopNPC._pylonEntries == null)
			{
				PylonShopNPC._pylonEntries = NPCShopDatabase.GetPylonEntries().ToList<NPCShop.Entry>();
			}
			if (NPCShopDatabase.NoPylons.Contains(shop.FullName))
			{
				return;
			}
			foreach (NPCShop.Entry entry in PylonShopNPC._pylonEntries)
			{
				shop.Add(new NPCShop.Entry[]
				{
					entry
				});
			}
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x0052FE3C File Offset: 0x0052E03C
		public override void Unload()
		{
			PylonShopNPC._pylonEntries = null;
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x0052FE44 File Offset: 0x0052E044
		public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
		{
			if (shopName == NPCShopDatabase.GetShopName(550, "Shop"))
			{
				this.AddPylonsToBartenderShop(npc, items);
			}
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x0052FE68 File Offset: 0x0052E068
		private void AddPylonsToBartenderShop(NPC npc, Item[] items)
		{
			int slot;
			if (items[4].IsAir)
			{
				slot = 4;
			}
			else
			{
				if (!items[30].IsAir)
				{
					return;
				}
				slot = 30;
			}
			using (List<NPCShop.Entry>.Enumerator enumerator = PylonShopNPC._pylonEntries.GetEnumerator())
			{
				IL_72:
				while (enumerator.MoveNext())
				{
					NPCShop.Entry entry = enumerator.Current;
					if (!entry.Disabled && entry.ConditionsMet())
					{
						items[slot] = entry.Item.Clone();
						entry.OnShopOpen(items[slot], npc);
						while (++slot < items.Length)
						{
							if (items[slot].IsAir)
							{
								goto IL_72;
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x04001C63 RID: 7267
		private static List<NPCShop.Entry> _pylonEntries;
	}
}
