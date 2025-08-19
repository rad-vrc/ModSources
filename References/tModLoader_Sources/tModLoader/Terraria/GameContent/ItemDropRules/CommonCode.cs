using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005EF RID: 1519
	public static class CommonCode
	{
		// Token: 0x06004383 RID: 17283 RVA: 0x005FFE20 File Offset: 0x005FE020
		[Obsolete("Use DropItem(DropAttemptInfo, ...)", true)]
		public static void DropItemFromNPC(NPC npc, int itemId, int stack, bool scattered = false)
		{
			CommonCode._DropItemFromNPC(npc, itemId, stack, scattered);
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x005FFE2C File Offset: 0x005FE02C
		private static void _DropItemFromNPC(NPC npc, int itemId, int stack, bool scattered = false)
		{
			if (itemId > 0 && itemId < ItemLoader.ItemCount)
			{
				int itemIndex = CommonCode.DropItem(npc.Hitbox, npc.GetItemSource_Loot(), itemId, stack, scattered);
				CommonCode.ModifyItemDropFromNPC(npc, itemIndex);
			}
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x005FFE61 File Offset: 0x005FE061
		public static void DropItem(DropAttemptInfo info, int item, int stack, bool scattered = false)
		{
			if (info.npc != null)
			{
				CommonCode._DropItemFromNPC(info.npc, item, stack, scattered);
				return;
			}
			CommonCode.DropItem(info.player.Hitbox, info.player.GetItemSource_OpenItem(info.item), item, stack, scattered);
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x005FFEA0 File Offset: 0x005FE0A0
		public static int DropItem(Rectangle rectangle, IEntitySource entitySource, int itemId, int stack, bool scattered)
		{
			return CommonCode.DropItem(scattered ? new Vector2((float)(rectangle.X + Main.rand.Next(rectangle.Width + 1)), (float)(rectangle.Y + Main.rand.Next(rectangle.Height + 1))) : new Vector2((float)rectangle.X + (float)rectangle.Width / 2f, (float)rectangle.Y + (float)rectangle.Height / 2f), entitySource, itemId, stack);
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x005FFF24 File Offset: 0x005FE124
		public static int DropItem(Vector2 position, IEntitySource entitySource, int itemId, int stack)
		{
			int number = Item.NewItem(entitySource, position, itemId, stack, false, -1, false, false);
			if (Main.netMode == 1)
			{
				NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
			}
			return number;
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x005FFF68 File Offset: 0x005FE168
		public static void DropItemLocalPerClientAndSetNPCMoneyTo0(NPC npc, int itemId, int stack, bool interactionRequired = true)
		{
			if (itemId <= 0 || itemId >= ItemLoader.ItemCount)
			{
				return;
			}
			if (Main.netMode == 2)
			{
				int num = Item.NewItem(npc.GetItemSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, itemId, stack, true, -1, false, false);
				Main.timeItemSlotCannotBeReusedFor[num] = 54000;
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active && (npc.playerInteraction[i] || !interactionRequired))
					{
						NetMessage.SendData(90, i, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				Main.item[num].active = false;
			}
			else
			{
				CommonCode._DropItemFromNPC(npc, itemId, stack, false);
			}
			npc.value = 0f;
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x0060003C File Offset: 0x005FE23C
		public static void DropItemForEachInteractingPlayerOnThePlayer(NPC npc, int itemId, UnifiedRandom rng, int chanceNumerator, int chanceDenominator, int stack = 1, bool interactionRequired = true)
		{
			if (itemId <= 0 || itemId >= ItemLoader.ItemCount)
			{
				return;
			}
			if (Main.netMode == 2)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active && (npc.playerInteraction[i] || !interactionRequired) && rng.Next(chanceDenominator) < chanceNumerator)
					{
						int itemIndex = Item.NewItem(npc.GetItemSource_Loot(), player.position, player.Size, itemId, stack, false, -1, false, false);
						CommonCode.ModifyItemDropFromNPC(npc, itemIndex);
					}
				}
			}
			else if (rng.Next(chanceDenominator) < chanceNumerator)
			{
				CommonCode._DropItemFromNPC(npc, itemId, stack, false);
			}
			npc.value = 0f;
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x006000E0 File Offset: 0x005FE2E0
		public static void ModifyItemDropFromNPC(NPC npc, int itemIndex)
		{
			Item item = Main.item[itemIndex];
			int type = item.type;
			if (type != 23)
			{
				if (type != 319)
				{
					return;
				}
				switch (npc.netID)
				{
				case 542:
					item.color = new Color(189, 148, 96, 255);
					NetMessage.SendData(88, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
					return;
				case 543:
					item.color = new Color(112, 85, 89, 255);
					NetMessage.SendData(88, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
					return;
				case 544:
					item.color = new Color(145, 27, 40, 255);
					NetMessage.SendData(88, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
					return;
				case 545:
					item.color = new Color(158, 113, 164, 255);
					NetMessage.SendData(88, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
					break;
				default:
					return;
				}
			}
			else
			{
				if (npc.type == 1 && npc.netID != -1 && npc.netID != -2 && npc.netID != -5 && npc.netID != -6)
				{
					item.color = npc.color;
					NetMessage.SendData(88, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
				}
				if (Main.remixWorld && npc.type == 59)
				{
					item.color = new Color(255, 127, 0);
					NetMessage.SendData(88, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
					return;
				}
			}
		}
	}
}
