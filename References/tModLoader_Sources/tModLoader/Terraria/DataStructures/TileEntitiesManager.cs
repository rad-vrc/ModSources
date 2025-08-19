using System;
using System.Collections.Generic;
using Terraria.GameContent.Tile_Entities;

namespace Terraria.DataStructures
{
	// Token: 0x02000737 RID: 1847
	public class TileEntitiesManager
	{
		// Token: 0x06004AED RID: 19181 RVA: 0x00668DC4 File Offset: 0x00666FC4
		private int AssignNewID()
		{
			int nextEntityID = this._nextEntityID;
			this._nextEntityID = nextEntityID + 1;
			return nextEntityID;
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x00668DE2 File Offset: 0x00666FE2
		private bool InvalidEntityID(int id)
		{
			return id < 0 || id >= this._nextEntityID;
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x00668DF8 File Offset: 0x00666FF8
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
			TileEntitiesManager.VanillaTypeCount = this._nextEntityID;
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x00668E68 File Offset: 0x00667068
		public void Register(TileEntity entity)
		{
			int num = this.AssignNewID();
			this._types[num] = entity;
			entity.RegisterTileEntityID(num);
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x00668E90 File Offset: 0x00667090
		public bool CheckValidTile(int id, int x, int y)
		{
			return !this.InvalidEntityID(id) && this._types[id].IsTileValidForEntity(x, y);
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x00668EB0 File Offset: 0x006670B0
		public void NetPlaceEntity(int id, int x, int y)
		{
			if (!this.InvalidEntityID(id) && this._types[id].IsTileValidForEntity(x, y))
			{
				this._types[id].NetPlaceEntityAttempt(x, y);
			}
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x00668EE3 File Offset: 0x006670E3
		public TileEntity GenerateInstance(int id)
		{
			if (this.InvalidEntityID(id))
			{
				return null;
			}
			return this._types[id].GenerateInstance();
		}

		/// <summary> Gets the template TileEntity object with the given id (not the new instance which gets added to the world as the game is played). This method will throw exceptions on failure. </summary>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException" />
		// Token: 0x06004AF4 RID: 19188 RVA: 0x00668F01 File Offset: 0x00667101
		public TileEntity GetTileEntity<T>(int id) where T : TileEntity
		{
			return this._types[id] as T;
		}

		/// <summary> Attempts to get the template TileEntity object with the given id (not the new instance which gets added to the world as the game is played). </summary>
		// Token: 0x06004AF5 RID: 19189 RVA: 0x00668F20 File Offset: 0x00667120
		public bool TryGetTileEntity<T>(int id, out T tileEntity) where T : TileEntity
		{
			TileEntity entity;
			if (!this._types.TryGetValue(id, out entity))
			{
				tileEntity = default(T);
				return false;
			}
			return (tileEntity = (entity as T)) != null;
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x00668F62 File Offset: 0x00667162
		public IReadOnlyDictionary<int, TileEntity> EnumerateEntities()
		{
			return this._types;
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x00668F6A File Offset: 0x0066716A
		internal void Reset()
		{
			this._types.Clear();
			this._nextEntityID = 0;
			this.RegisterAll();
		}

		// Token: 0x04006037 RID: 24631
		private int _nextEntityID;

		// Token: 0x04006038 RID: 24632
		private Dictionary<int, TileEntity> _types = new Dictionary<int, TileEntity>();

		// Token: 0x04006039 RID: 24633
		public static int VanillaTypeCount;
	}
}
