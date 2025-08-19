using System;
using System.ComponentModel;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// NPCDefinition represents an NPC identity.
	/// <para /> <inheritdoc />
	/// </summary>
	// Token: 0x0200038D RID: 909
	[TypeConverter(typeof(ToFromStringConverter<NPCDefinition>))]
	public class NPCDefinition : EntityDefinition
	{
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06003124 RID: 12580 RVA: 0x0053EE9C File Offset: 0x0053D09C
		public override bool IsUnloaded
		{
			get
			{
				return this.Type <= -66 && (!(this.Mod == "Terraria") || !(this.Name == "None")) && (!(this.Mod == "") || !(this.Name == ""));
			}
		}

		/// <summary>
		/// The NPCID of the NPC this NPCDefinition represents. Will be -66 (<see cref="F:Terraria.ID.NPCID.NegativeIDCount" />) for <see cref="P:Terraria.ModLoader.Config.NPCDefinition.IsUnloaded" /> NPCDefinition.
		/// </summary>
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06003125 RID: 12581 RVA: 0x0053EF04 File Offset: 0x0053D104
		public override int Type
		{
			get
			{
				int id;
				if (!NPCID.Search.TryGetId((this.Mod != "Terraria") ? (this.Mod + "/" + this.Name) : this.Name, ref id))
				{
					return -66;
				}
				return id;
			}
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x0053EF53 File Offset: 0x0053D153
		public NPCDefinition()
		{
		}

		/// <summary><b>Note: </b>As ModConfig loads before other content, make sure to only use <see cref="M:Terraria.ModLoader.Config.NPCDefinition.#ctor(System.String,System.String)" /> for modded content in ModConfig classes. </summary>
		// Token: 0x06003127 RID: 12583 RVA: 0x0053EF5B File Offset: 0x0053D15B
		public NPCDefinition(int type) : base(NPCID.Search.GetName(type))
		{
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x0053EF6E File Offset: 0x0053D16E
		public NPCDefinition(string key) : base(key)
		{
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x0053EF77 File Offset: 0x0053D177
		public NPCDefinition(string mod, string name) : base(mod, name)
		{
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x0053EF81 File Offset: 0x0053D181
		public static NPCDefinition FromString(string s)
		{
			return new NPCDefinition(s);
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x0053EF89 File Offset: 0x0053D189
		public static NPCDefinition Load(TagCompound tag)
		{
			return new NPCDefinition(tag.GetString("mod"), tag.GetString("name"));
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x0053EFA6 File Offset: 0x0053D1A6
		public override string DisplayName
		{
			get
			{
				if (!this.IsUnloaded)
				{
					return Lang.GetNPCNameValue(this.Type);
				}
				return Language.GetTextValue("Mods.ModLoader.Unloaded");
			}
		}

		// Token: 0x04001D3D RID: 7485
		public static readonly Func<TagCompound, NPCDefinition> DESERIALIZER = new Func<TagCompound, NPCDefinition>(NPCDefinition.Load);
	}
}
