using System;
using System.ComponentModel;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// PrefixDefinition represents a Prefix identity.
	/// <para /> <inheritdoc />
	/// </summary>
	// Token: 0x0200038E RID: 910
	[TypeConverter(typeof(ToFromStringConverter<PrefixDefinition>))]
	public class PrefixDefinition : EntityDefinition
	{
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x0053EFDC File Offset: 0x0053D1DC
		public override int Type
		{
			get
			{
				if (this.Mod == "Terraria" && this.Name == "None")
				{
					return 0;
				}
				int id;
				if (!PrefixID.Search.TryGetId((this.Mod != "Terraria") ? (this.Mod + "/" + this.Name) : this.Name, ref id))
				{
					return -1;
				}
				return id;
			}
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x0053F050 File Offset: 0x0053D250
		public PrefixDefinition()
		{
		}

		/// <summary><b>Note: </b>As ModConfig loads before other content, make sure to only use <see cref="M:Terraria.ModLoader.Config.PrefixDefinition.#ctor(System.String,System.String)" /> for modded content in ModConfig classes. </summary>
		// Token: 0x06003130 RID: 12592 RVA: 0x0053F058 File Offset: 0x0053D258
		public PrefixDefinition(int type) : base(PrefixID.Search.GetName(type))
		{
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x0053F06B File Offset: 0x0053D26B
		public PrefixDefinition(string key) : base(key)
		{
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x0053F074 File Offset: 0x0053D274
		public PrefixDefinition(string mod, string name) : base(mod, name)
		{
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x0053F07E File Offset: 0x0053D27E
		public static PrefixDefinition FromString(string s)
		{
			return new PrefixDefinition(s);
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x0053F086 File Offset: 0x0053D286
		public static PrefixDefinition Load(TagCompound tag)
		{
			return new PrefixDefinition(tag.GetString("mod"), tag.GetString("name"));
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06003135 RID: 12597 RVA: 0x0053F0A3 File Offset: 0x0053D2A3
		public override string DisplayName
		{
			get
			{
				if (this.IsUnloaded)
				{
					return Language.GetTextValue("Mods.ModLoader.Unloaded");
				}
				if (this.Type == 0)
				{
					return Lang.inter[23].Value;
				}
				return Lang.prefix[this.Type].Value;
			}
		}

		// Token: 0x04001D3E RID: 7486
		public static readonly Func<TagCompound, PrefixDefinition> DESERIALIZER = new Func<TagCompound, PrefixDefinition>(PrefixDefinition.Load);
	}
}
