using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Classes implementing EntityDefinition serve to function as a way to save and load the identities of various Terraria objects. Only the identity is preserved, no other data such as stack size, damage, etc. These classes are well suited for ModConfig, but can be saved and loaded in a TagCompound as well.
	/// <para /> An EntityDefinition can potentially refer to content removed from a mod or content from a mod not currently enabled. Use <see cref="P:Terraria.ModLoader.Config.EntityDefinition.IsUnloaded" /> to check if this is the case, especially before using <see cref="P:Terraria.ModLoader.Config.EntityDefinition.Type" /> as an index into an array. It is usually desirable to preserve all unloaded EntityDefinition entries so user data is not lost, so structure your code accordingly.
	/// </summary>
	// Token: 0x0200038A RID: 906
	public abstract class EntityDefinition : TagSerializable
	{
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x0053EB68 File Offset: 0x0053CD68
		public virtual bool IsUnloaded
		{
			get
			{
				return this.Type <= 0 && (!(this.Mod == "Terraria") || !(this.Name == "None")) && (!(this.Mod == "") || !(this.Name == ""));
			}
		}

		/// <summary>
		/// The content ID of the content this EntityDefinition represents. Will be -1 for <see cref="P:Terraria.ModLoader.Config.EntityDefinition.IsUnloaded" /> EntityDefinition.
		/// </summary>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06003109 RID: 12553
		[JsonIgnore]
		public abstract int Type { get; }

		// Token: 0x0600310A RID: 12554 RVA: 0x0053EBCD File Offset: 0x0053CDCD
		public EntityDefinition()
		{
			this.Mod = "Terraria";
			this.Name = "None";
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x0053EBEB File Offset: 0x0053CDEB
		public EntityDefinition(string mod, string name)
		{
			this.Mod = mod;
			this.Name = name;
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x0053EC04 File Offset: 0x0053CE04
		public EntityDefinition(string key)
		{
			this.Mod = "Terraria";
			this.Name = key;
			string[] parts = key.Split('/', 2, StringSplitOptions.None);
			if (parts.Length == 2)
			{
				this.Mod = parts[0];
				this.Name = parts[1];
			}
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x0053EC4C File Offset: 0x0053CE4C
		public override bool Equals(object obj)
		{
			EntityDefinition p = obj as EntityDefinition;
			return p != null && this.Mod == p.Mod && this.Name == p.Name;
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x0053EC8B File Offset: 0x0053CE8B
		public override string ToString()
		{
			return this.Mod + "/" + this.Name;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x0053ECA3 File Offset: 0x0053CEA3
		public virtual string DisplayName
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x0053ECAB File Offset: 0x0053CEAB
		public override int GetHashCode()
		{
			return new
			{
				this.Mod,
				this.Name
			}.GetHashCode();
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x0053ECC3 File Offset: 0x0053CEC3
		public TagCompound SerializeData()
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound["mod"] = this.Mod;
			tagCompound["name"] = this.Name;
			return tagCompound;
		}

		// Token: 0x04001D39 RID: 7481
		[DefaultValue("Terraria")]
		public string Mod;

		// Token: 0x04001D3A RID: 7482
		[DefaultValue("None")]
		public string Name;
	}
}
