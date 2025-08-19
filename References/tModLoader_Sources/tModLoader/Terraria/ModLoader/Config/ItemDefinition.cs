using System;
using System.ComponentModel;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// ItemDefinition represents an Item identity. A typical use for this class is usage in ModConfig, perhaps to facilitate an Item tweaking mod.
	/// <para /> <inheritdoc />
	/// </summary>
	// Token: 0x0200038B RID: 907
	[TypeConverter(typeof(ToFromStringConverter<ItemDefinition>))]
	public class ItemDefinition : EntityDefinition
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x0053ECEC File Offset: 0x0053CEEC
		public override int Type
		{
			get
			{
				int id;
				if (!ItemID.Search.TryGetId((this.Mod != "Terraria") ? (this.Mod + "/" + this.Name) : this.Name, ref id))
				{
					return -1;
				}
				return id;
			}
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x0053ED3A File Offset: 0x0053CF3A
		public ItemDefinition()
		{
		}

		/// <summary><b>Note: </b>As ModConfig loads before other content, make sure to only use <see cref="M:Terraria.ModLoader.Config.ItemDefinition.#ctor(System.String,System.String)" /> for modded content in ModConfig classes. </summary>
		// Token: 0x06003114 RID: 12564 RVA: 0x0053ED42 File Offset: 0x0053CF42
		public ItemDefinition(int type) : base(ItemID.Search.GetName(type))
		{
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x0053ED55 File Offset: 0x0053CF55
		public ItemDefinition(string key) : base(key)
		{
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x0053ED5E File Offset: 0x0053CF5E
		public ItemDefinition(string mod, string name) : base(mod, name)
		{
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x0053ED68 File Offset: 0x0053CF68
		public static ItemDefinition FromString(string s)
		{
			return new ItemDefinition(s);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x0053ED70 File Offset: 0x0053CF70
		public static ItemDefinition Load(TagCompound tag)
		{
			return new ItemDefinition(tag.GetString("mod"), tag.GetString("name"));
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x0053ED8D File Offset: 0x0053CF8D
		public override string DisplayName
		{
			get
			{
				if (!this.IsUnloaded)
				{
					return Lang.GetItemNameValue(this.Type);
				}
				return Language.GetTextValue("Mods.ModLoader.Items.UnloadedItem.DisplayName");
			}
		}

		// Token: 0x04001D3B RID: 7483
		public static readonly Func<TagCompound, ItemDefinition> DESERIALIZER = new Func<TagCompound, ItemDefinition>(ItemDefinition.Load);
	}
}
