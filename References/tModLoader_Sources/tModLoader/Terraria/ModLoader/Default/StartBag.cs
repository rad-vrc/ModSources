using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002CC RID: 716
	public class StartBag : ModLoaderModItem
	{
		// Token: 0x06002DBD RID: 11709 RVA: 0x0052FF18 File Offset: 0x0052E118
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.rare = 1;
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x0052FF40 File Offset: 0x0052E140
		internal void AddItem(Item item)
		{
			this.items.Add(item);
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x0052FF4E File Offset: 0x0052E14E
		public override bool CanRightClick()
		{
			return true;
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x0052FF54 File Offset: 0x0052E154
		public override void RightClick(Player player)
		{
			IEntitySource itemSource = player.GetItemSource_OpenItem(base.Type);
			foreach (Item item in this.items)
			{
				int i = Item.NewItem(itemSource, player.getRect(), item.type, item.stack, false, item.prefix, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, null, i, 1f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x0052FFF4 File Offset: 0x0052E1F4
		public override void SaveData(TagCompound tag)
		{
			tag["items"] = this.items;
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x00530007 File Offset: 0x0052E207
		public override void LoadData(TagCompound tag)
		{
			this.items = tag.Get<List<Item>>("items");
		}

		// Token: 0x04001C64 RID: 7268
		[CloneByReference]
		private List<Item> items = new List<Item>();
	}
}
