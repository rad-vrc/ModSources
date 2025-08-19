using System;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002DC RID: 732
	public class UnloadedTileEntity : ModTileEntity
	{
		// Token: 0x06002E12 RID: 11794 RVA: 0x00530EBC File Offset: 0x0052F0BC
		internal void SetData(TagCompound tag)
		{
			this.modName = tag.GetString("mod");
			this.tileEntityName = tag.GetString("name");
			if (tag.ContainsKey("data"))
			{
				this.data = tag.GetCompound("data");
			}
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x00530F0C File Offset: 0x0052F10C
		public unsafe override bool IsTileValidForEntity(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			return tile.HasTile && TileLoader.GetTile((int)(*tile.TileType)) is UnloadedTile;
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x00530F48 File Offset: 0x0052F148
		public override void SaveData(TagCompound tag)
		{
			tag["mod"] = this.modName;
			tag["name"] = this.tileEntityName;
			TagCompound tagCompound = this.data;
			if (tagCompound != null && tagCompound.Count > 0)
			{
				tag["data"] = this.data;
			}
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x00530F9F File Offset: 0x0052F19F
		public override void LoadData(TagCompound tag)
		{
			this.SetData(tag);
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x00530FA8 File Offset: 0x0052F1A8
		internal bool TryRestore(out ModTileEntity newEntity)
		{
			newEntity = null;
			ModTileEntity tileEntity;
			if (ModContent.TryFind<ModTileEntity>(this.modName, this.tileEntityName, out tileEntity))
			{
				newEntity = ModTileEntity.ConstructFromBase(tileEntity);
				newEntity.type = (byte)tileEntity.Type;
				newEntity.Position = this.Position;
				TagCompound tagCompound = this.data;
				if (tagCompound != null && tagCompound.Count > 0)
				{
					newEntity.LoadData(this.data);
				}
			}
			return newEntity != null;
		}

		// Token: 0x04001C75 RID: 7285
		private string modName;

		// Token: 0x04001C76 RID: 7286
		private string tileEntityName;

		// Token: 0x04001C77 RID: 7287
		private TagCompound data;
	}
}
