using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace Terraria.DataStructures
{
	// Token: 0x02000738 RID: 1848
	public abstract class TileEntity
	{
		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06004AF9 RID: 19193 RVA: 0x00668F98 File Offset: 0x00667198
		// (remove) Token: 0x06004AFA RID: 19194 RVA: 0x00668FCC File Offset: 0x006671CC
		public static event Action _UpdateStart;

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06004AFB RID: 19195 RVA: 0x00669000 File Offset: 0x00667200
		// (remove) Token: 0x06004AFC RID: 19196 RVA: 0x00669034 File Offset: 0x00667234
		public static event Action _UpdateEnd;

		// Token: 0x06004AFD RID: 19197 RVA: 0x00669067 File Offset: 0x00667267
		internal TileEntity()
		{
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x0066906F File Offset: 0x0066726F
		public static int AssignNewID()
		{
			return TileEntity.TileEntitiesNextID++;
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x0066907E File Offset: 0x0066727E
		public static void Clear()
		{
			TileEntity.ByID.Clear();
			TileEntity.ByPosition.Clear();
			TileEntity.TileEntitiesNextID = 0;
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x0066909A File Offset: 0x0066729A
		public static void UpdateStart()
		{
			if (TileEntity._UpdateStart != null)
			{
				TileEntity._UpdateStart();
			}
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x006690AD File Offset: 0x006672AD
		public static void UpdateEnd()
		{
			if (TileEntity._UpdateEnd != null)
			{
				TileEntity._UpdateEnd();
			}
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x006690C0 File Offset: 0x006672C0
		public static void InitializeAll()
		{
			TileEntity.manager = new TileEntitiesManager();
			TileEntity.manager.RegisterAll();
			ModTileEntity.Initialize();
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x006690DB File Offset: 0x006672DB
		public static void PlaceEntityNet(int x, int y, int type)
		{
			if (WorldGen.InWorld(x, y, 0) && !TileEntity.ByPosition.ContainsKey(new Point16(x, y)))
			{
				TileEntity.manager.NetPlaceEntity(type, x, y);
			}
		}

		/// <summary>
		/// Allows logic to execute every game update for this placed Tile Entity. Called on each placed Tile Entity.
		/// <para /> This hook is not called for multiplayer clients. The <see cref="F:Terraria.ID.MessageID.TileEntitySharing" /> network message will need be used to keep clients in sync if necessary. 
		/// </summary>
		// Token: 0x06004B04 RID: 19204 RVA: 0x00669107 File Offset: 0x00667307
		public virtual void Update()
		{
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x00669109 File Offset: 0x00667309
		public static void Write(BinaryWriter writer, TileEntity ent, bool networkSend = false, bool lightSend = false)
		{
			lightSend = (lightSend && networkSend);
			writer.Write(ent.type);
			ent.WriteInner(writer, networkSend, lightSend);
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x00669128 File Offset: 0x00667328
		public static TileEntity Read(BinaryReader reader, bool networkSend = false, bool lightSend = false)
		{
			lightSend = (lightSend && networkSend);
			byte id = reader.ReadByte();
			TileEntity tileEntity = TileEntity.manager.GenerateInstance((int)id);
			tileEntity.type = id;
			tileEntity.ReadInner(reader, networkSend, lightSend);
			return tileEntity;
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x0066915C File Offset: 0x0066735C
		private void WriteInner(BinaryWriter writer, bool networkSend, bool lightSend)
		{
			if (!lightSend)
			{
				writer.Write(this.ID);
			}
			writer.Write(this.Position.X);
			writer.Write(this.Position.Y);
			if (networkSend)
			{
				this.NetSend(writer);
				return;
			}
			this.WriteExtraData(writer, lightSend);
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x006691AD File Offset: 0x006673AD
		private void ReadInner(BinaryReader reader, bool networkSend, bool lightSend)
		{
			if (!lightSend)
			{
				this.ID = reader.ReadInt32();
			}
			this.Position = new Point16(reader.ReadInt16(), reader.ReadInt16());
			if (networkSend)
			{
				this.NetReceive(reader);
				return;
			}
			this.ReadExtraData(reader, lightSend);
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x006691E8 File Offset: 0x006673E8
		public virtual void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x006691EA File Offset: 0x006673EA
		public virtual void ReadExtraData(BinaryReader reader, bool networkSend)
		{
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x006691EC File Offset: 0x006673EC
		public virtual void OnPlayerUpdate(Player player)
		{
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x006691F0 File Offset: 0x006673F0
		public static bool IsOccupied(int id, out int interactingPlayer)
		{
			interactingPlayer = -1;
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active && !player.dead && player.tileEntityAnchor.interactEntityID == id)
				{
					interactingPlayer = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x0066923C File Offset: 0x0066743C
		public virtual void OnInventoryDraw(Player player, SpriteBatch spriteBatch)
		{
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x0066923E File Offset: 0x0066743E
		public virtual string GetItemGamepadInstructions(int slot = 0)
		{
			return "";
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x00669245 File Offset: 0x00667445
		public virtual bool TryGetItemGamepadOverrideInstructions(Item[] inv, int context, int slot, out string instruction)
		{
			instruction = null;
			return false;
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x0066924C File Offset: 0x0066744C
		public virtual bool OverrideItemSlotHover(Item[] inv, int context = 0, int slot = 0)
		{
			return false;
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x0066924F File Offset: 0x0066744F
		public virtual bool OverrideItemSlotLeftClick(Item[] inv, int context = 0, int slot = 0)
		{
			return false;
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x00669254 File Offset: 0x00667454
		public static void BasicOpenCloseInteraction(Player player, int x, int y, int id)
		{
			player.CloseSign();
			int interactingPlayer;
			if (Main.netMode != 1)
			{
				Main.stackSplit = 600;
				player.GamepadEnableGrappleCooldown();
				if (TileEntity.IsOccupied(id, out interactingPlayer))
				{
					if (interactingPlayer == player.whoAmI)
					{
						Recipe.FindRecipes(false);
						SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
						player.tileEntityAnchor.Clear();
						return;
					}
				}
				else
				{
					TileEntity.SetInteractionAnchor(player, x, y, id);
				}
				return;
			}
			Main.stackSplit = 600;
			player.GamepadEnableGrappleCooldown();
			if (TileEntity.IsOccupied(id, out interactingPlayer))
			{
				if (interactingPlayer == player.whoAmI)
				{
					Recipe.FindRecipes(false);
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
					player.tileEntityAnchor.Clear();
					NetMessage.SendData(122, -1, -1, null, -1, (float)Main.myPlayer, 0f, 0f, 0, 0, 0);
					return;
				}
			}
			else
			{
				NetMessage.SendData(122, -1, -1, null, id, (float)Main.myPlayer, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x0066934C File Offset: 0x0066754C
		public static void SetInteractionAnchor(Player player, int x, int y, int id)
		{
			player.chest = -1;
			player.SetTalkNPC(-1, false);
			if (player.whoAmI == Main.myPlayer)
			{
				Main.playerInventory = true;
				Main.recBigList = false;
				Main.CreativeMenu.CloseMenu();
				if (PlayerInput.GrappleAndInteractAreShared)
				{
					PlayerInput.Triggers.JustPressed.Grapple = false;
				}
				if (player.tileEntityAnchor.interactEntityID != -1)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
				else
				{
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				}
			}
			player.tileEntityAnchor.Set(id, x, y);
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x006693EA File Offset: 0x006675EA
		public virtual void RegisterTileEntityID(int assignedID)
		{
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x006693EC File Offset: 0x006675EC
		public virtual void NetPlaceEntityAttempt(int x, int y)
		{
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x006693EE File Offset: 0x006675EE
		public virtual bool IsTileValidForEntity(int x, int y)
		{
			return false;
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x006693F1 File Offset: 0x006675F1
		public virtual TileEntity GenerateInstance()
		{
			return null;
		}

		/// <summary>
		/// Allows you to save custom data for this tile entity.
		/// <br />
		/// <br /><b>NOTE:</b> The provided tag is always empty by default, and is provided as an argument only for the sake of convenience and optimization.
		/// <br /><b>NOTE:</b> Try to only save data that isn't default values.
		/// </summary>
		/// <param name="tag"> The TagCompound to save data into. Note that this is always empty by default, and is provided as an argument only for the sake of convenience and optimization. </param>
		// Token: 0x06004B18 RID: 19224 RVA: 0x006693F4 File Offset: 0x006675F4
		public virtual void SaveData(TagCompound tag)
		{
		}

		/// <summary>
		/// Allows you to load custom data that you have saved for this tile entity.
		/// <br /><b>Try to write defensive loading code that won't crash if something's missing.</b>
		/// </summary>
		/// <param name="tag"> The TagCompound to load data from. </param>
		// Token: 0x06004B19 RID: 19225 RVA: 0x006693F6 File Offset: 0x006675F6
		public virtual void LoadData(TagCompound tag)
		{
		}

		/// <summary>
		/// Allows you to send custom data for this tile entity between client and server, which will be handled in <see cref="M:Terraria.DataStructures.TileEntity.NetReceive(System.IO.BinaryReader)" />.
		/// <br />Called while sending tile data (!lightSend) and when <see cref="F:Terraria.ID.MessageID.TileEntitySharing" /> is sent (lightSend).
		/// <br />Only called on the server.
		/// </summary>
		/// <param name="writer">The writer.</param>
		// Token: 0x06004B1A RID: 19226 RVA: 0x006693F8 File Offset: 0x006675F8
		public virtual void NetSend(BinaryWriter writer)
		{
			this.WriteExtraData(writer, true);
		}

		/// <summary>
		/// Receives custom data sent in the <see cref="M:Terraria.DataStructures.TileEntity.NetSend(System.IO.BinaryWriter)" /> hook.
		/// <para /> Called while receiving tile data (!lightReceive) and when <see cref="F:Terraria.ID.MessageID.TileEntitySharing" /> is received (lightReceive).
		/// <para /> Note that this is called on a new instance that will replace the existing instance at the <see cref="F:Terraria.DataStructures.TileEntity.Position" />, if any. <see cref="F:Terraria.DataStructures.TileEntity.ID" /> is not necessarily assigned yet when this is called.
		/// <para /> Only called on the client.
		/// </summary>
		/// <param name="reader">The reader.</param>
		// Token: 0x06004B1B RID: 19227 RVA: 0x00669402 File Offset: 0x00667602
		public virtual void NetReceive(BinaryReader reader)
		{
			this.ReadExtraData(reader, true);
		}

		/// <summary>
		/// Attempts to retrieve the TileEntity at the given coordinates of the specified Type (<typeparamref name="T" />). Works with any provided coordinate belonging to the multitile. Note that this method assumes the TileEntity is placed in the top left corner of the multitile.
		/// </summary>
		/// <typeparam name="T">The type to get the entity as</typeparam>
		/// <param name="i">The tile X-coordinate</param>
		/// <param name="j">The tile Y-coordinate</param>
		/// <param name="entity">The found <typeparamref name="T" /> instance, if there was one.</param>
		/// <returns><see langword="true" /> if there was a <typeparamref name="T" /> instance, or <see langword="false" /> if there was no entity present OR the entity was not a <typeparamref name="T" /> instance.</returns>
		// Token: 0x06004B1C RID: 19228 RVA: 0x0066940C File Offset: 0x0066760C
		public static bool TryGet<T>(int i, int j, out T entity) where T : TileEntity
		{
			Point16 topLeft = TileObjectData.TopLeft(i, j);
			TileEntity existing;
			if (TileEntity.ByPosition.TryGetValue(topLeft, out existing))
			{
				T existingAsT = existing as T;
				if (existingAsT != null)
				{
					entity = existingAsT;
					return true;
				}
			}
			entity = default(T);
			return false;
		}

		/// <inheritdoc cref="M:Terraria.DataStructures.TileEntity.TryGet``1(System.Int32,System.Int32,``0@)" />
		// Token: 0x06004B1D RID: 19229 RVA: 0x00669455 File Offset: 0x00667655
		public static bool TryGet<T>(Point16 point, out T entity) where T : TileEntity
		{
			return TileEntity.TryGet<T>((int)point.X, (int)point.Y, out entity);
		}

		// Token: 0x0400603A RID: 24634
		public static TileEntitiesManager manager;

		// Token: 0x0400603B RID: 24635
		public const int MaxEntitiesPerChunk = 1000;

		// Token: 0x0400603C RID: 24636
		public static object EntityCreationLock = new object();

		/// <summary> Maps <see cref="F:Terraria.DataStructures.TileEntity.ID" /> to TileEntity instances. </summary>
		// Token: 0x0400603D RID: 24637
		public static Dictionary<int, TileEntity> ByID = new Dictionary<int, TileEntity>();

		/// <summary> Maps tile coordinate locations to the TileEntity at that location. </summary>
		// Token: 0x0400603E RID: 24638
		public static Dictionary<Point16, TileEntity> ByPosition = new Dictionary<Point16, TileEntity>();

		// Token: 0x0400603F RID: 24639
		public static int TileEntitiesNextID;

		/// <summary> A unique ID for each TileEntity instance in the world. This ID is consistent in multiplayer. </summary>
		// Token: 0x04006040 RID: 24640
		public int ID;

		/// <summary> The tile coordinate location of this TileEntity. Will typically be the top left corner of the corresponding multitile, but not necessarily. </summary>
		// Token: 0x04006041 RID: 24641
		public Point16 Position;

		// Token: 0x04006042 RID: 24642
		public byte type;
	}
}
