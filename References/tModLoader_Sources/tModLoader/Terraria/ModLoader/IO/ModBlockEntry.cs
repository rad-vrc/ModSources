using System;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000284 RID: 644
	internal abstract class ModBlockEntry : TagSerializable
	{
		// Token: 0x06002BDE RID: 11230 RVA: 0x00524934 File Offset: 0x00522B34
		protected ModBlockEntry(ModBlockType block)
		{
			this.type = (this.loadedType = block.Type);
			this.modName = block.Mod.Name;
			this.name = block.Name;
			this.vanillaReplacementType = block.VanillaFallbackOnModDeletion;
			this.unloadedType = this.GetUnloadedPlaceholder(block.Type).FullName;
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x0052499C File Offset: 0x00522B9C
		protected virtual ModBlockType GetUnloadedPlaceholder(ushort type)
		{
			return this.DefaultUnloadedPlaceholder;
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x005249A4 File Offset: 0x00522BA4
		protected ModBlockEntry(TagCompound tag)
		{
			this.type = tag.Get<ushort>("value");
			this.modName = tag.Get<string>("mod");
			this.name = tag.Get<string>("name");
			this.vanillaReplacementType = tag.Get<ushort>("fallbackID");
			this.unloadedType = tag.Get<string>("uType");
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x00524A0C File Offset: 0x00522C0C
		public bool IsUnloaded
		{
			get
			{
				return this.loadedType != this.type;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06002BE2 RID: 11234
		public abstract ModBlockType DefaultUnloadedPlaceholder { get; }

		// Token: 0x06002BE3 RID: 11235 RVA: 0x00524A20 File Offset: 0x00522C20
		public virtual TagCompound SerializeData()
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound["value"] = this.type;
			tagCompound["mod"] = this.modName;
			tagCompound["name"] = this.name;
			tagCompound["fallbackID"] = this.vanillaReplacementType;
			tagCompound["uType"] = this.unloadedType;
			return tagCompound;
		}

		// Token: 0x04001BF1 RID: 7153
		public ushort type;

		// Token: 0x04001BF2 RID: 7154
		public string modName;

		// Token: 0x04001BF3 RID: 7155
		public string name;

		// Token: 0x04001BF4 RID: 7156
		public ushort vanillaReplacementType;

		// Token: 0x04001BF5 RID: 7157
		public string unloadedType;

		// Token: 0x04001BF6 RID: 7158
		public ushort loadedType;
	}
}
