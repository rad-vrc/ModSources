using System;
using System.ComponentModel;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// TileDefinition represents a Tile identity.
	/// <para /> <inheritdoc />
	/// </summary>
	// Token: 0x02000390 RID: 912
	[TypeConverter(typeof(ToFromStringConverter<TileDefinition>))]
	public class TileDefinition : EntityDefinition
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x0053F1F0 File Offset: 0x0053D3F0
		public override bool IsUnloaded
		{
			get
			{
				return this.Type < 0 && (!(this.Mod == "Terraria") || !(this.Name == "None")) && (!(this.Mod == "") || !(this.Name == ""));
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x0053F258 File Offset: 0x0053D458
		public override int Type
		{
			get
			{
				int id;
				if (!TileID.Search.TryGetId((this.Mod != "Terraria") ? (this.Mod + "/" + this.Name) : this.Name, ref id))
				{
					return -1;
				}
				return id;
			}
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x0053F2A6 File Offset: 0x0053D4A6
		public TileDefinition()
		{
		}

		/// <summary><b>Note: </b>As ModConfig loads before other content, make sure to only use <see cref="M:Terraria.ModLoader.Config.TileDefinition.#ctor(System.String,System.String)" /> for modded content in ModConfig classes. </summary>
		// Token: 0x06003143 RID: 12611 RVA: 0x0053F2AE File Offset: 0x0053D4AE
		public TileDefinition(int type) : base(TileID.Search.GetName(type))
		{
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x0053F2C1 File Offset: 0x0053D4C1
		public TileDefinition(string key) : base(key)
		{
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x0053F2CA File Offset: 0x0053D4CA
		public TileDefinition(string mod, string name) : base(mod, name)
		{
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x0053F2D4 File Offset: 0x0053D4D4
		public static TileDefinition FromString(string s)
		{
			return new TileDefinition(s);
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x0053F2DC File Offset: 0x0053D4DC
		public static TileDefinition Load(TagCompound tag)
		{
			return new TileDefinition(tag.GetString("mod"), tag.GetString("name"));
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06003148 RID: 12616 RVA: 0x0053F2FC File Offset: 0x0053D4FC
		public override string DisplayName
		{
			get
			{
				if (this.IsUnloaded || this.Type == -1)
				{
					return Language.GetTextValue("Mods.ModLoader.Unloaded");
				}
				if (!Main.dedServ && !string.IsNullOrEmpty(Lang.GetMapObjectName(MapHelper.TileToLookup(this.Type, 0))))
				{
					return this.Name + " \"" + Lang.GetMapObjectName(MapHelper.TileToLookup(this.Type, 0)) + "\"";
				}
				return this.Name;
			}
		}

		// Token: 0x04001D40 RID: 7488
		public static readonly Func<TagCompound, TileDefinition> DESERIALIZER = new Func<TagCompound, TileDefinition>(TileDefinition.Load);
	}
}
