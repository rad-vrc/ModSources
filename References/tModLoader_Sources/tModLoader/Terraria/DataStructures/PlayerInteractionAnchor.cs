using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000728 RID: 1832
	public struct PlayerInteractionAnchor
	{
		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06004A8F RID: 19087 RVA: 0x00667C65 File Offset: 0x00665E65
		public bool InUse
		{
			get
			{
				return this.interactEntityID != -1;
			}
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x00667C73 File Offset: 0x00665E73
		public PlayerInteractionAnchor(int entityID, int x = -1, int y = -1)
		{
			this.interactEntityID = entityID;
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x00667C8A File Offset: 0x00665E8A
		public void Clear()
		{
			this.interactEntityID = -1;
			this.X = -1;
			this.Y = -1;
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x00667CA1 File Offset: 0x00665EA1
		public void Set(int entityID, int x, int y)
		{
			this.interactEntityID = entityID;
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x00667CB8 File Offset: 0x00665EB8
		public bool IsInValidUseTileEntity()
		{
			return this.InUse && TileEntity.ByID.ContainsKey(this.interactEntityID);
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x00667CD4 File Offset: 0x00665ED4
		public TileEntity GetTileEntity()
		{
			if (!this.IsInValidUseTileEntity())
			{
				return null;
			}
			return TileEntity.ByID[this.interactEntityID];
		}

		// Token: 0x04005FE2 RID: 24546
		public int interactEntityID;

		// Token: 0x04005FE3 RID: 24547
		public int X;

		// Token: 0x04005FE4 RID: 24548
		public int Y;
	}
}
