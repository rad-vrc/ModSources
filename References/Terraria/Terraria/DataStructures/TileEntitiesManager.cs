using System;
using System.Collections.Generic;
using Terraria.GameContent.Tile_Entities;

namespace Terraria.DataStructures
{
	// Token: 0x0200045B RID: 1115
	public class TileEntitiesManager
	{
		// Token: 0x06002C91 RID: 11409 RVA: 0x005BB6E8 File Offset: 0x005B98E8
		private int AssignNewID()
		{
			int nextEntityID = this._nextEntityID;
			this._nextEntityID = nextEntityID + 1;
			return nextEntityID;
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x005BB706 File Offset: 0x005B9906
		private bool InvalidEntityID(int id)
		{
			return id < 0 || id >= this._nextEntityID;
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x005BB71C File Offset: 0x005B991C
		public void RegisterAll()
		{
			this.Register(new TETrainingDummy());
			this.Register(new TEItemFrame());
			this.Register(new TELogicSensor());
			this.Register(new TEDisplayDoll());
			this.Register(new TEWeaponsRack());
			this.Register(new TEHatRack());
			this.Register(new TEFoodPlatter());
			this.Register(new TETeleportationPylon());
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x005BB784 File Offset: 0x005B9984
		public void Register(TileEntity entity)
		{
			int num = this.AssignNewID();
			this._types[num] = entity;
			entity.RegisterTileEntityID(num);
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x005BB7AC File Offset: 0x005B99AC
		public bool CheckValidTile(int id, int x, int y)
		{
			return !this.InvalidEntityID(id) && this._types[id].IsTileValidForEntity(x, y);
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x005BB7CC File Offset: 0x005B99CC
		public void NetPlaceEntity(int id, int x, int y)
		{
			if (this.InvalidEntityID(id))
			{
				return;
			}
			if (!this._types[id].IsTileValidForEntity(x, y))
			{
				return;
			}
			this._types[id].NetPlaceEntityAttempt(x, y);
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x005BB801 File Offset: 0x005B9A01
		public TileEntity GenerateInstance(int id)
		{
			if (this.InvalidEntityID(id))
			{
				return null;
			}
			return this._types[id].GenerateInstance();
		}

		// Token: 0x040050F7 RID: 20727
		private int _nextEntityID;

		// Token: 0x040050F8 RID: 20728
		private Dictionary<int, TileEntity> _types = new Dictionary<int, TileEntity>();
	}
}
