using System;
using System.Collections.Generic;
using System.IO;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002D3 RID: 723
	[LegacyName(new string[]
	{
		"MysteryItem"
	})]
	public sealed class UnloadedItem : ModLoaderModItem
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x0053068F File Offset: 0x0052E88F
		// (set) Token: 0x06002DEA RID: 11754 RVA: 0x00530697 File Offset: 0x0052E897
		public string ModName { get; private set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x005306A0 File Offset: 0x0052E8A0
		// (set) Token: 0x06002DEC RID: 11756 RVA: 0x005306A8 File Offset: 0x0052E8A8
		public string ItemName { get; private set; }

		// Token: 0x06002DED RID: 11757 RVA: 0x005306B1 File Offset: 0x0052E8B1
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.rare = 1;
			base.Item.maxStack = int.MaxValue;
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x005306E9 File Offset: 0x0052E8E9
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 0;
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x005306F7 File Offset: 0x0052E8F7
		internal void Setup(TagCompound tag)
		{
			this.ModName = tag.GetString("mod");
			this.ItemName = tag.GetString("name");
			this.data = tag;
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x00530724 File Offset: 0x0052E924
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			for (int i = 0; i < tooltips.Count; i++)
			{
				if (tooltips[i].Name == "Tooltip0")
				{
					tooltips[i].Text = Language.GetTextValue(this.GetLocalizationKey("UnloadedItemModTooltip"), this.ModName);
				}
				else if (tooltips[i].Name == "Tooltip1")
				{
					tooltips[i].Text = Language.GetTextValue(this.GetLocalizationKey("UnloadedItemItemNameTooltip"), this.ItemName);
				}
			}
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x005307BB File Offset: 0x0052E9BB
		public override bool CanStack(Item source)
		{
			return false;
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x005307C0 File Offset: 0x0052E9C0
		public override void SaveData(TagCompound tag)
		{
			foreach (KeyValuePair<string, object> keyValuePair in this.data)
			{
				string text;
				object obj;
				keyValuePair.Deconstruct(out text, out obj);
				string key = text;
				object value = obj;
				tag[key] = value;
			}
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x00530820 File Offset: 0x0052EA20
		public override void LoadData(TagCompound tag)
		{
			this.Setup(tag);
			ModItem modItem;
			if (!ModContent.TryFind<ModItem>(this.ModName, this.ItemName, out modItem))
			{
				return;
			}
			if (modItem is UnloadedItem)
			{
				this.LoadData(tag.GetCompound("data"));
				return;
			}
			TagCompound modData = tag.GetCompound("data");
			base.Item.SetDefaults(modItem.Type);
			ItemIO.LoadModdedPrefix(base.Item, tag);
			if (modData != null && modData.Count > 0)
			{
				base.Item.ModItem.LoadData(modData);
			}
			if (tag.ContainsKey("globalData"))
			{
				ItemIO.LoadGlobals(base.Item, tag.GetList<TagCompound>("globalData"));
			}
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x005308CD File Offset: 0x0052EACD
		public override void NetSend(BinaryWriter writer)
		{
			TagIO.Write(this.data ?? new TagCompound(), writer);
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x005308E4 File Offset: 0x0052EAE4
		public override void NetReceive(BinaryReader reader)
		{
			this.Setup(TagIO.Read(reader));
		}

		// Token: 0x04001C6A RID: 7274
		[CloneByReference]
		private TagCompound data;
	}
}
