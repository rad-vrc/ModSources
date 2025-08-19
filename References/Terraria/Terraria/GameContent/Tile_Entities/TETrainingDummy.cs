using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x0200021B RID: 539
	public class TETrainingDummy : TileEntity
	{
		// Token: 0x06001E9D RID: 7837 RVA: 0x0050BD81 File Offset: 0x00509F81
		public override void RegisterTileEntityID(int assignedID)
		{
			TETrainingDummy._myEntityID = (byte)assignedID;
			TileEntity._UpdateStart += TETrainingDummy.ClearBoxes;
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0050BD9B File Offset: 0x00509F9B
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TETrainingDummy.NetPlaceEntity(x, y);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x0050BDA4 File Offset: 0x00509FA4
		public static void NetPlaceEntity(int x, int y)
		{
			TETrainingDummy.Place(x, y);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0050BDAE File Offset: 0x00509FAE
		public override TileEntity GenerateInstance()
		{
			return new TETrainingDummy();
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x0050BDB5 File Offset: 0x00509FB5
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TETrainingDummy.ValidTile(x, y);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x0050BDBE File Offset: 0x00509FBE
		public static void ClearBoxes()
		{
			TETrainingDummy.playerBox.Clear();
			TETrainingDummy.playerBoxFilled = false;
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0050BDD0 File Offset: 0x00509FD0
		public override void Update()
		{
			Rectangle rectangle = new Rectangle(0, 0, 32, 48);
			rectangle.Inflate(1600, 1600);
			int x = rectangle.X;
			int y = rectangle.Y;
			if (this.npc != -1)
			{
				if (!Main.npc[this.npc].active || Main.npc[this.npc].type != 488 || Main.npc[this.npc].ai[0] != (float)this.Position.X || Main.npc[this.npc].ai[1] != (float)this.Position.Y)
				{
					this.Deactivate();
					return;
				}
			}
			else
			{
				TETrainingDummy.FillPlayerHitboxes();
				rectangle.X = (int)(this.Position.X * 16) + x;
				rectangle.Y = (int)(this.Position.Y * 16) + y;
				bool flag = false;
				foreach (KeyValuePair<int, Rectangle> keyValuePair in TETrainingDummy.playerBox)
				{
					if (keyValuePair.Value.Intersects(rectangle))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					this.Activate();
				}
			}
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x0050BF20 File Offset: 0x0050A120
		private static void FillPlayerHitboxes()
		{
			if (!TETrainingDummy.playerBoxFilled)
			{
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active)
					{
						TETrainingDummy.playerBox[i] = Main.player[i].getRect();
					}
				}
				TETrainingDummy.playerBoxFilled = true;
			}
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0050BF70 File Offset: 0x0050A170
		public static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 378 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 36 == 0;
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0050BFD4 File Offset: 0x0050A1D4
		public TETrainingDummy()
		{
			this.npc = -1;
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x0050BFE4 File Offset: 0x0050A1E4
		public static int Place(int x, int y)
		{
			TETrainingDummy tetrainingDummy = new TETrainingDummy();
			tetrainingDummy.Position = new Point16(x, y);
			tetrainingDummy.ID = TileEntity.AssignNewID();
			tetrainingDummy.type = TETrainingDummy._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[tetrainingDummy.ID] = tetrainingDummy;
				TileEntity.ByPosition[tetrainingDummy.Position] = tetrainingDummy;
			}
			return tetrainingDummy.ID;
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x0050C070 File Offset: 0x0050A270
		public static int Hook_AfterPlacement(int x, int y, int type = 378, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x - 1, y - 2, 2, 3, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x - 1, (float)(y - 2), (float)TETrainingDummy._myEntityID, 0f, 0, 0, 0);
				return -1;
			}
			return TETrainingDummy.Place(x - 1, y - 2);
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x0050C0C4 File Offset: 0x0050A2C4
		public static void Kill(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TETrainingDummy._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(tileEntity.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
			}
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0050C144 File Offset: 0x0050A344
		public static int Find(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TETrainingDummy._myEntityID)
			{
				return tileEntity.ID;
			}
			return -1;
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0050C17B File Offset: 0x0050A37B
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			writer.Write((short)this.npc);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x0050C18A File Offset: 0x0050A38A
		public override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			this.npc = (int)reader.ReadInt16();
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0050C198 File Offset: 0x0050A398
		public void Activate()
		{
			int num = NPC.NewNPC(new EntitySource_TileEntity(this), (int)(this.Position.X * 16 + 16), (int)(this.Position.Y * 16 + 48), 488, 100, 0f, 0f, 0f, 0f, 255);
			Main.npc[num].ai[0] = (float)this.Position.X;
			Main.npc[num].ai[1] = (float)this.Position.Y;
			Main.npc[num].netUpdate = true;
			this.npc = num;
			if (Main.netMode != 1)
			{
				NetMessage.SendData(86, -1, -1, null, this.ID, (float)this.Position.X, (float)this.Position.Y, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0050C274 File Offset: 0x0050A474
		public void Deactivate()
		{
			if (this.npc != -1)
			{
				Main.npc[this.npc].active = false;
			}
			this.npc = -1;
			if (Main.netMode != 1)
			{
				NetMessage.SendData(86, -1, -1, null, this.ID, (float)this.Position.X, (float)this.Position.Y, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0050C2DC File Offset: 0x0050A4DC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Position.X,
				"x  ",
				this.Position.Y,
				"y npc: ",
				this.npc
			});
		}

		// Token: 0x040045BA RID: 17850
		private static Dictionary<int, Rectangle> playerBox = new Dictionary<int, Rectangle>();

		// Token: 0x040045BB RID: 17851
		private static bool playerBoxFilled;

		// Token: 0x040045BC RID: 17852
		private static byte _myEntityID;

		// Token: 0x040045BD RID: 17853
		public int npc;
	}
}
