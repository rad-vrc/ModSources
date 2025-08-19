using System;
using System.ComponentModel;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// BuffDefinition represents an Buff identity.
	/// <para /> <inheritdoc />
	/// </summary>
	// Token: 0x0200038F RID: 911
	[TypeConverter(typeof(ToFromStringConverter<BuffDefinition>))]
	public class BuffDefinition : EntityDefinition
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06003137 RID: 12599 RVA: 0x0053F0F4 File Offset: 0x0053D2F4
		public override int Type
		{
			get
			{
				if (this.Mod == "Terraria" && this.Name == "None")
				{
					return 0;
				}
				int id;
				if (!BuffID.Search.TryGetId((this.Mod != "Terraria") ? (this.Mod + "/" + this.Name) : this.Name, ref id))
				{
					return -1;
				}
				return id;
			}
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x0053F168 File Offset: 0x0053D368
		public BuffDefinition()
		{
		}

		/// <summary><b>Note: </b>As ModConfig loads before other content, make sure to only use <see cref="M:Terraria.ModLoader.Config.BuffDefinition.#ctor(System.String,System.String)" /> for modded content in ModConfig classes. </summary>
		// Token: 0x06003139 RID: 12601 RVA: 0x0053F170 File Offset: 0x0053D370
		public BuffDefinition(int type) : base(BuffID.Search.GetName(type))
		{
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x0053F183 File Offset: 0x0053D383
		public BuffDefinition(string key) : base(key)
		{
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x0053F18C File Offset: 0x0053D38C
		public BuffDefinition(string mod, string name) : base(mod, name)
		{
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x0053F196 File Offset: 0x0053D396
		public static BuffDefinition FromString(string s)
		{
			return new BuffDefinition(s);
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x0053F19E File Offset: 0x0053D39E
		public static BuffDefinition Load(TagCompound tag)
		{
			return new BuffDefinition(tag.GetString("mod"), tag.GetString("name"));
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600313E RID: 12606 RVA: 0x0053F1BB File Offset: 0x0053D3BB
		public override string DisplayName
		{
			get
			{
				if (!this.IsUnloaded)
				{
					return Lang.GetBuffName(this.Type);
				}
				return Language.GetTextValue("Mods.ModLoader.Unloaded");
			}
		}

		// Token: 0x04001D3F RID: 7487
		public static readonly Func<TagCompound, BuffDefinition> DESERIALIZER = new Func<TagCompound, BuffDefinition>(BuffDefinition.Load);
	}
}
