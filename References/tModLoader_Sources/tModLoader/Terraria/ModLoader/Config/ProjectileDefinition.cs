using System;
using System.ComponentModel;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// ProjectileDefinition represents a Projectile identity.
	/// <para /> <inheritdoc />
	/// </summary>
	// Token: 0x0200038C RID: 908
	[TypeConverter(typeof(ToFromStringConverter<ProjectileDefinition>))]
	public class ProjectileDefinition : EntityDefinition
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x0053EDC0 File Offset: 0x0053CFC0
		public override int Type
		{
			get
			{
				int id;
				if (!ProjectileID.Search.TryGetId((this.Mod != "Terraria") ? (this.Mod + "/" + this.Name) : this.Name, ref id))
				{
					return -1;
				}
				return id;
			}
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x0053EE0E File Offset: 0x0053D00E
		public ProjectileDefinition()
		{
		}

		/// <summary><b>Note: </b>As ModConfig loads before other content, make sure to only use <see cref="M:Terraria.ModLoader.Config.ProjectileDefinition.#ctor(System.String,System.String)" /> for modded content in ModConfig classes. </summary>
		// Token: 0x0600311D RID: 12573 RVA: 0x0053EE16 File Offset: 0x0053D016
		public ProjectileDefinition(int type) : base(ProjectileID.Search.GetName(type))
		{
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x0053EE29 File Offset: 0x0053D029
		public ProjectileDefinition(string key) : base(key)
		{
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x0053EE32 File Offset: 0x0053D032
		public ProjectileDefinition(string mod, string name) : base(mod, name)
		{
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x0053EE3C File Offset: 0x0053D03C
		public static ProjectileDefinition FromString(string s)
		{
			return new ProjectileDefinition(s);
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x0053EE44 File Offset: 0x0053D044
		public static ProjectileDefinition Load(TagCompound tag)
		{
			return new ProjectileDefinition(tag.GetString("mod"), tag.GetString("name"));
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x0053EE61 File Offset: 0x0053D061
		public override string DisplayName
		{
			get
			{
				if (!this.IsUnloaded)
				{
					return Lang.GetProjectileName(this.Type).Value;
				}
				return Language.GetTextValue("Mods.ModLoader.Unloaded");
			}
		}

		// Token: 0x04001D3C RID: 7484
		public static readonly Func<TagCompound, ProjectileDefinition> DESERIALIZER = new Func<TagCompound, ProjectileDefinition>(ProjectileDefinition.Load);
	}
}
