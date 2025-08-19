using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000456 RID: 1110
	public struct PlayerInteractionAnchor
	{
		// Token: 0x06002C7E RID: 11390 RVA: 0x005BB4F2 File Offset: 0x005B96F2
		public PlayerInteractionAnchor(int entityID, int x = -1, int y = -1)
		{
			this.interactEntityID = entityID;
			this.X = x;
			this.Y = y;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x005BB509 File Offset: 0x005B9709
		public bool InUse
		{
			get
			{
				return this.interactEntityID != -1;
			}
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x005BB517 File Offset: 0x005B9717
		public void Clear()
		{
			this.interactEntityID = -1;
			this.X = -1;
			this.Y = -1;
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x005BB4F2 File Offset: 0x005B96F2
		public void Set(int entityID, int x, int y)
		{
			this.interactEntityID = entityID;
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x005BB52E File Offset: 0x005B972E
		public bool IsInValidUseTileEntity()
		{
			return this.InUse && TileEntity.ByID.ContainsKey(this.interactEntityID);
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x005BB54A File Offset: 0x005B974A
		public TileEntity GetTileEntity()
		{
			if (!this.IsInValidUseTileEntity())
			{
				return null;
			}
			return TileEntity.ByID[this.interactEntityID];
		}

		// Token: 0x040050D1 RID: 20689
		public int interactEntityID;

		// Token: 0x040050D2 RID: 20690
		public int X;

		// Token: 0x040050D3 RID: 20691
		public int Y;
	}
}
