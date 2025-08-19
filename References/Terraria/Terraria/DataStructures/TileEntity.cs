using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameInput;

namespace Terraria.DataStructures
{
	// Token: 0x0200045C RID: 1116
	public abstract class TileEntity
	{
		// Token: 0x06002C99 RID: 11417 RVA: 0x005BB832 File Offset: 0x005B9A32
		public static int AssignNewID()
		{
			return TileEntity.TileEntitiesNextID++;
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06002C9A RID: 11418 RVA: 0x005BB844 File Offset: 0x005B9A44
		// (remove) Token: 0x06002C9B RID: 11419 RVA: 0x005BB878 File Offset: 0x005B9A78
		public static event Action _UpdateStart;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06002C9C RID: 11420 RVA: 0x005BB8AC File Offset: 0x005B9AAC
		// (remove) Token: 0x06002C9D RID: 11421 RVA: 0x005BB8E0 File Offset: 0x005B9AE0
		public static event Action _UpdateEnd;

		// Token: 0x06002C9E RID: 11422 RVA: 0x005BB913 File Offset: 0x005B9B13
		public static void Clear()
		{
			TileEntity.ByID.Clear();
			TileEntity.ByPosition.Clear();
			TileEntity.TileEntitiesNextID = 0;
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x005BB92F File Offset: 0x005B9B2F
		public static void UpdateStart()
		{
			if (TileEntity._UpdateStart != null)
			{
				TileEntity._UpdateStart();
			}
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x005BB942 File Offset: 0x005B9B42
		public static void UpdateEnd()
		{
			if (TileEntity._UpdateEnd != null)
			{
				TileEntity._UpdateEnd();
			}
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x005BB955 File Offset: 0x005B9B55
		public static void InitializeAll()
		{
			TileEntity.manager = new TileEntitiesManager();
			TileEntity.manager.RegisterAll();
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x005BB96B File Offset: 0x005B9B6B
		public static void PlaceEntityNet(int x, int y, int type)
		{
			if (!WorldGen.InWorld(x, y, 0))
			{
				return;
			}
			if (TileEntity.ByPosition.ContainsKey(new Point16(x, y)))
			{
				return;
			}
			TileEntity.manager.NetPlaceEntity(type, x, y);
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void Update()
		{
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x005BB999 File Offset: 0x005B9B99
		public static void Write(BinaryWriter writer, TileEntity ent, bool networkSend = false)
		{
			writer.Write(ent.type);
			ent.WriteInner(writer, networkSend);
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x005BB9B0 File Offset: 0x005B9BB0
		public static TileEntity Read(BinaryReader reader, bool networkSend = false)
		{
			byte id = reader.ReadByte();
			TileEntity tileEntity = TileEntity.manager.GenerateInstance((int)id);
			tileEntity.type = id;
			tileEntity.ReadInner(reader, networkSend);
			return tileEntity;
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x005BB9DE File Offset: 0x005B9BDE
		private void WriteInner(BinaryWriter writer, bool networkSend)
		{
			if (!networkSend)
			{
				writer.Write(this.ID);
			}
			writer.Write(this.Position.X);
			writer.Write(this.Position.Y);
			this.WriteExtraData(writer, networkSend);
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x005BBA19 File Offset: 0x005B9C19
		private void ReadInner(BinaryReader reader, bool networkSend)
		{
			if (!networkSend)
			{
				this.ID = reader.ReadInt32();
			}
			this.Position = new Point16(reader.ReadInt16(), reader.ReadInt16());
			this.ReadExtraData(reader, networkSend);
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void ReadExtraData(BinaryReader reader, bool networkSend)
		{
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void OnPlayerUpdate(Player player)
		{
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x005BBA4C File Offset: 0x005B9C4C
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

		// Token: 0x06002CAC RID: 11436 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void OnInventoryDraw(Player player, SpriteBatch spriteBatch)
		{
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x004E2992 File Offset: 0x004E0B92
		public virtual string GetItemGamepadInstructions(int slot = 0)
		{
			return "";
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x005BBA98 File Offset: 0x005B9C98
		public virtual bool TryGetItemGamepadOverrideInstructions(Item[] inv, int context, int slot, out string instruction)
		{
			instruction = null;
			return false;
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public virtual bool OverrideItemSlotHover(Item[] inv, int context = 0, int slot = 0)
		{
			return false;
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public virtual bool OverrideItemSlotLeftClick(Item[] inv, int context = 0, int slot = 0)
		{
			return false;
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x005BBAA0 File Offset: 0x005B9CA0
		public static void BasicOpenCloseInteraction(Player player, int x, int y, int id)
		{
			player.CloseSign();
			if (Main.netMode != 1)
			{
				Main.stackSplit = 600;
				player.GamepadEnableGrappleCooldown();
				int num;
				if (!TileEntity.IsOccupied(id, out num))
				{
					TileEntity.SetInteractionAnchor(player, x, y, id);
					return;
				}
				if (num == player.whoAmI)
				{
					Recipe.FindRecipes(false);
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
					player.tileEntityAnchor.Clear();
					return;
				}
			}
			else
			{
				Main.stackSplit = 600;
				player.GamepadEnableGrappleCooldown();
				int num;
				if (TileEntity.IsOccupied(id, out num))
				{
					if (num == player.whoAmI)
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
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x005BBB98 File Offset: 0x005B9D98
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

		// Token: 0x06002CB3 RID: 11443 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void RegisterTileEntityID(int assignedID)
		{
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void NetPlaceEntityAttempt(int x, int y)
		{
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public virtual bool IsTileValidForEntity(int x, int y)
		{
			return false;
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public virtual TileEntity GenerateInstance()
		{
			return null;
		}

		// Token: 0x040050F9 RID: 20729
		public static TileEntitiesManager manager;

		// Token: 0x040050FA RID: 20730
		public const int MaxEntitiesPerChunk = 1000;

		// Token: 0x040050FB RID: 20731
		public static object EntityCreationLock = new object();

		// Token: 0x040050FC RID: 20732
		public static Dictionary<int, TileEntity> ByID = new Dictionary<int, TileEntity>();

		// Token: 0x040050FD RID: 20733
		public static Dictionary<Point16, TileEntity> ByPosition = new Dictionary<Point16, TileEntity>();

		// Token: 0x040050FE RID: 20734
		public static int TileEntitiesNextID;

		// Token: 0x04005101 RID: 20737
		public int ID;

		// Token: 0x04005102 RID: 20738
		public Point16 Position;

		// Token: 0x04005103 RID: 20739
		public byte type;
	}
}
