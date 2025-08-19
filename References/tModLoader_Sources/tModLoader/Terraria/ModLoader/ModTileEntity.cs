using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ObjectData;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Tile Entities are Entities tightly coupled with tiles, allowing the possibility of tiles to exhibit cool behavior. TileEntity.Update is called in SP and on Server, not on Clients.
	/// </summary>
	/// <seealso cref="T:Terraria.DataStructures.TileEntity" />
	// Token: 0x020001CF RID: 463
	public abstract class ModTileEntity : TileEntity, IModType, ILoadable
	{
		/// <summary>
		/// The mod that added this ModTileEntity.
		/// </summary>
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x004E9900 File Offset: 0x004E7B00
		// (set) Token: 0x06002467 RID: 9319 RVA: 0x004E9908 File Offset: 0x004E7B08
		public Mod Mod { get; internal set; }

		/// <summary>
		/// The internal name of this ModTileEntity.
		/// </summary>
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x004E9911 File Offset: 0x004E7B11
		public virtual string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x004E991E File Offset: 0x004E7B1E
		public string FullName
		{
			get
			{
				return this.Mod.Name + "/" + this.Name;
			}
		}

		/// <summary>
		/// The numeric type used to identify this kind of tile entity.
		/// </summary>
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x004E993B File Offset: 0x004E7B3B
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x004E9943 File Offset: 0x004E7B43
		public int Type { get; internal set; }

		// Token: 0x0600246C RID: 9324 RVA: 0x004E994C File Offset: 0x004E7B4C
		public ModTileEntity()
		{
		}

		/// <summary>
		/// Returns the number of modded tile entities that exist in the world currently being played.
		/// </summary>
		// Token: 0x0600246D RID: 9325 RVA: 0x004E9954 File Offset: 0x004E7B54
		public static int CountInWorld()
		{
			return TileEntity.ByID.Count((KeyValuePair<int, TileEntity> pair) => (int)pair.Value.type >= ModTileEntity.NumVanilla);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x004E9980 File Offset: 0x004E7B80
		internal static void Initialize()
		{
			Action value;
			if ((value = ModTileEntity.<>O.<0>__UpdateStartInternal) == null)
			{
				value = (ModTileEntity.<>O.<0>__UpdateStartInternal = new Action(ModTileEntity.UpdateStartInternal));
			}
			TileEntity._UpdateStart += value;
			Action value2;
			if ((value2 = ModTileEntity.<>O.<1>__UpdateEndInternal) == null)
			{
				value2 = (ModTileEntity.<>O.<1>__UpdateEndInternal = new Action(ModTileEntity.UpdateEndInternal));
			}
			TileEntity._UpdateEnd += value2;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x004E99D0 File Offset: 0x004E7BD0
		private static void UpdateStartInternal()
		{
			foreach (ModTileEntity modTileEntity in TileEntity.manager.EnumerateEntities().Values.OfType<ModTileEntity>())
			{
				modTileEntity.PreGlobalUpdate();
			}
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x004E9A28 File Offset: 0x004E7C28
		private static void UpdateEndInternal()
		{
			foreach (ModTileEntity modTileEntity in TileEntity.manager.EnumerateEntities().Values.OfType<ModTileEntity>())
			{
				modTileEntity.PostGlobalUpdate();
			}
		}

		/// <summary>
		/// You should never use this. It is only included here for completion's sake.
		/// </summary>
		// Token: 0x06002471 RID: 9329 RVA: 0x004E9A80 File Offset: 0x004E7C80
		public override void NetPlaceEntityAttempt(int i, int j)
		{
			ModTileEntity modTileEntity;
			if (!TileEntity.manager.TryGetTileEntity<ModTileEntity>(this.Type, out modTileEntity))
			{
				return;
			}
			int id = modTileEntity.Place(i, j);
			((ModTileEntity)TileEntity.ByID[id]).OnNetPlace();
			NetMessage.SendData(86, -1, -1, null, id, (float)i, (float)j, 0f, 0, 0, 0);
		}

		/// <summary>
		/// Returns a new ModTileEntity with the same class, mod, name, and type as the ModTileEntity with the given type. It is very rare that you should have to use this.
		/// </summary>
		// Token: 0x06002472 RID: 9330 RVA: 0x004E9AD8 File Offset: 0x004E7CD8
		public static ModTileEntity ConstructFromType(int type)
		{
			ModTileEntity modTileEntity;
			if (!TileEntity.manager.TryGetTileEntity<ModTileEntity>(type, out modTileEntity))
			{
				return null;
			}
			return ModTileEntity.ConstructFromBase(modTileEntity);
		}

		/// <summary>
		/// Returns a new ModTileEntity with the same class, mod, name, and type as the parameter. It is very rare that you should have to use this.
		/// </summary>
		// Token: 0x06002473 RID: 9331 RVA: 0x004E9AFC File Offset: 0x004E7CFC
		public static ModTileEntity ConstructFromBase(ModTileEntity tileEntity)
		{
			ModTileEntity modTileEntity = (ModTileEntity)Activator.CreateInstance(tileEntity.GetType(), true);
			modTileEntity.Mod = tileEntity.Mod;
			modTileEntity.Type = tileEntity.Type;
			return modTileEntity;
		}

		/// <summary>
		/// A helper method that places this kind of tile entity in the given coordinates for you.
		/// </summary>
		// Token: 0x06002474 RID: 9332 RVA: 0x004E9B28 File Offset: 0x004E7D28
		public int Place(int i, int j)
		{
			ModTileEntity newEntity = ModTileEntity.ConstructFromBase(this);
			newEntity.Position = new Point16(i, j);
			newEntity.ID = TileEntity.AssignNewID();
			newEntity.type = (byte)this.Type;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[newEntity.ID] = newEntity;
				TileEntity.ByPosition[newEntity.Position] = newEntity;
			}
			return newEntity.ID;
		}

		/// <summary>
		/// A helper method that removes this kind of tile entity from the given coordinates for you.
		/// <para /> This is typically used in <see cref="M:Terraria.ModLoader.ModTile.KillMultiTile(System.Int32,System.Int32,System.Int32,System.Int32)" />.
		/// </summary>
		// Token: 0x06002475 RID: 9333 RVA: 0x004E9BB8 File Offset: 0x004E7DB8
		public void Kill(int i, int j)
		{
			Point16 pos = new Point16(i, j);
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(pos, out tileEntity) && (int)tileEntity.type == this.Type)
			{
				((ModTileEntity)tileEntity).OnKill();
				TileEntity.ByID.Remove(tileEntity.ID);
				TileEntity.ByPosition.Remove(pos);
			}
		}

		/// <summary>
		/// Returns the entity ID of this kind of tile entity at the given coordinates for you.
		/// </summary>
		// Token: 0x06002476 RID: 9334 RVA: 0x004E9C14 File Offset: 0x004E7E14
		public int Find(int i, int j)
		{
			Point16 pos = new Point16(i, j);
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(pos, out tileEntity) && (int)tileEntity.type == this.Type)
			{
				return tileEntity.ID;
			}
			return -1;
		}

		/// <summary>
		/// Should never be called on ModTileEntity. Replaced by NetSend and SaveData.
		/// Would make the base method internal if not for patch size
		/// </summary>
		// Token: 0x06002477 RID: 9335 RVA: 0x004E9C4F File Offset: 0x004E7E4F
		public sealed override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Should never be called on ModTileEntity. Replaced by NetReceive and LoadData
		/// Would make the base method internal if not for patch size
		/// </summary>
		// Token: 0x06002478 RID: 9336 RVA: 0x004E9C56 File Offset: 0x004E7E56
		public sealed override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x004E9C5D File Offset: 0x004E7E5D
		public override void NetSend(BinaryWriter writer)
		{
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x004E9C5F File Offset: 0x004E7E5F
		public override void NetReceive(BinaryReader reader)
		{
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x004E9C61 File Offset: 0x004E7E61
		public sealed override TileEntity GenerateInstance()
		{
			return ModTileEntity.ConstructFromBase(this);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x004E9C69 File Offset: 0x004E7E69
		public sealed override void RegisterTileEntityID(int assignedID)
		{
			this.Type = assignedID;
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x004E9C74 File Offset: 0x004E7E74
		void ILoadable.Load(Mod mod)
		{
			this.Mod = mod;
			if (!this.Mod.loading)
			{
				throw new Exception(Language.GetTextValue("tModLoader.LoadErrorNotLoading"));
			}
			this.Load();
			this.Load_Obsolete(mod);
			TileEntity.manager.Register(this);
			ModTypeLookup<ModTileEntity>.Register(this);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x004E9CC3 File Offset: 0x004E7EC3
		[Obsolete]
		private void Load_Obsolete(Mod mod)
		{
			this.Load(mod);
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x004E9CCC File Offset: 0x004E7ECC
		[Obsolete("Override the parameterless Load() overload instead.", true)]
		public virtual void Load(Mod mod)
		{
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x004E9CCE File Offset: 0x004E7ECE
		public virtual void Load()
		{
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x004E9CD0 File Offset: 0x004E7ED0
		public virtual bool IsLoadingEnabled(Mod mod)
		{
			return true;
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x004E9CD3 File Offset: 0x004E7ED3
		public virtual void Unload()
		{
		}

		/// <summary>
		/// This method does not get called by tModLoader, and is only included for you convenience so you do not have to cast the result of Mod.GetTileEntity.
		/// </summary>
		// Token: 0x06002483 RID: 9347 RVA: 0x004E9CD5 File Offset: 0x004E7ED5
		public virtual int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
		{
			return -1;
		}

		/// <summary>
		/// A generic <see cref="T:Terraria.DataStructures.PlacementHook" /> that should work for the <see cref="P:Terraria.ObjectData.TileObjectData.HookPostPlaceMyPlayer" /> of any typical ModTileEntity. Will result in this ModTileEntity being placed in the top left corner of the multitile.
		/// </summary>
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x004E9CD8 File Offset: 0x004E7ED8
		public PlacementHook Generic_HookPostPlaceMyPlayer
		{
			get
			{
				return new PlacementHook(new Func<int, int, int, int, int, int, int>(this.Generic_Hook_AfterPlacement), -1, 0, true);
			}
		}

		/// <summary>
		/// A generic implementation of <see cref="M:Terraria.ModLoader.ModTileEntity.Hook_AfterPlacement(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" /> that should work for the <see cref="P:Terraria.ObjectData.TileObjectData.HookPostPlaceMyPlayer" /> of any typical ModTileEntity. Will result in this ModTileEntity being placed in the top left corner of the multitile.
		/// <para /> Use <see cref="P:Terraria.ModLoader.ModTileEntity.Generic_HookPostPlaceMyPlayer" /> directly or pair this with <c>-1, 0, true</c> as the remaining parameters of <see cref="T:Terraria.DataStructures.PlacementHook" />.
		/// </summary>
		// Token: 0x06002485 RID: 9349 RVA: 0x004E9CF0 File Offset: 0x004E7EF0
		public int Generic_Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
		{
			TileObjectData tileData = TileObjectData.GetTileData(type, style, alternate);
			Point16 topLeft = TileObjectData.TopLeft(i, j);
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, (int)topLeft.X, (int)topLeft.Y, tileData.Width, tileData.Height, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, (int)topLeft.X, (float)topLeft.Y, (float)this.Type, 0f, 0, 0, 0);
				return -1;
			}
			return this.Place((int)topLeft.X, (int)topLeft.Y);
		}

		/// <summary>
		/// Code that should be run when this tile entity is placed by means of server-syncing. Called on Server only.
		/// </summary>
		// Token: 0x06002486 RID: 9350 RVA: 0x004E9D75 File Offset: 0x004E7F75
		public virtual void OnNetPlace()
		{
		}

		/// <summary>
		/// Code that should be run before all tile entities in the world update.
		/// </summary>
		// Token: 0x06002487 RID: 9351 RVA: 0x004E9D77 File Offset: 0x004E7F77
		public virtual void PreGlobalUpdate()
		{
		}

		/// <summary>
		/// Code that should be run after all tile entities in the world update.
		/// </summary>
		// Token: 0x06002488 RID: 9352 RVA: 0x004E9D79 File Offset: 0x004E7F79
		public virtual void PostGlobalUpdate()
		{
		}

		/// <summary>
		/// This method only gets called in the Kill method. If you plan to use that, you can put code here to make things happen when it is called.
		/// </summary>
		// Token: 0x06002489 RID: 9353 RVA: 0x004E9D7B File Offset: 0x004E7F7B
		public virtual void OnKill()
		{
		}

		/// <summary>
		/// Whether or not this tile entity is allowed to survive at the given coordinates. You should check whether the tile is active, as well as the tile's type and optionally the frame:
		/// <code>
		/// Tile tile = Main.tile[x, y];
		/// return tile.HasTile &amp;&amp; tile.TileType == ModContent.TileType&lt;BasicTileEntityTile&gt;();
		/// </code>
		/// <para /> This will be called during world loading and placing the entity on the server. It will not be automatically called when the host tile is killed, so using <see cref="M:Terraria.ModLoader.ModTile.KillMultiTile(System.Int32,System.Int32,System.Int32,System.Int32)" /> to <see cref="M:Terraria.ModLoader.ModTileEntity.Kill(System.Int32,System.Int32)" /> this entity is necessary to ensure the tile entity doesn't mistakenly persist without the host tile.
		/// </summary>
		// Token: 0x0600248A RID: 9354
		public abstract override bool IsTileValidForEntity(int x, int y);

		// Token: 0x04001733 RID: 5939
		public static readonly int NumVanilla = Assembly.GetExecutingAssembly().GetTypes().Count((Type t) => !t.IsAbstract && t.IsSubclassOf(typeof(TileEntity)) && !typeof(ModTileEntity).IsAssignableFrom(t));

		// Token: 0x02000940 RID: 2368
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006B25 RID: 27429
			public static Action <0>__UpdateStartInternal;

			// Token: 0x04006B26 RID: 27430
			public static Action <1>__UpdateEndInternal;
		}
	}
}
