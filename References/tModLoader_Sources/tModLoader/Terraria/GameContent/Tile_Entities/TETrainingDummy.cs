using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000562 RID: 1378
	public sealed class TETrainingDummy : TileEntity
	{
		// Token: 0x06004108 RID: 16648 RVA: 0x005E42A7 File Offset: 0x005E24A7
		public override void RegisterTileEntityID(int assignedID)
		{
			TETrainingDummy._myEntityID = (byte)assignedID;
			Action value;
			if ((value = TETrainingDummy.<>O.<0>__ClearBoxes) == null)
			{
				value = (TETrainingDummy.<>O.<0>__ClearBoxes = new Action(TETrainingDummy.ClearBoxes));
			}
			TileEntity._UpdateStart += value;
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x005E42D0 File Offset: 0x005E24D0
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TETrainingDummy.NetPlaceEntity(x, y);
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x005E42D9 File Offset: 0x005E24D9
		public static void NetPlaceEntity(int x, int y)
		{
			TETrainingDummy.Place(x, y);
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x005E42E3 File Offset: 0x005E24E3
		public override TileEntity GenerateInstance()
		{
			return new TETrainingDummy();
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x005E42EA File Offset: 0x005E24EA
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TETrainingDummy.ValidTile(x, y);
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x005E42F3 File Offset: 0x005E24F3
		public static void ClearBoxes()
		{
			TETrainingDummy.playerBox.Clear();
			TETrainingDummy.playerBoxFilled = false;
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x005E4308 File Offset: 0x005E2508
		public override void Update()
		{
			Rectangle value;
			value..ctor(0, 0, 32, 48);
			value.Inflate(1600, 1600);
			int x = value.X;
			int y = value.Y;
			if (this.npc != -1)
			{
				if (!Main.npc[this.npc].active || Main.npc[this.npc].type != 488 || Main.npc[this.npc].ai[0] != (float)this.Position.X || Main.npc[this.npc].ai[1] != (float)this.Position.Y)
				{
					this.Deactivate();
				}
				return;
			}
			TETrainingDummy.FillPlayerHitboxes();
			value.X = (int)(this.Position.X * 16) + x;
			value.Y = (int)(this.Position.Y * 16) + y;
			bool flag = false;
			foreach (KeyValuePair<int, Rectangle> item in TETrainingDummy.playerBox)
			{
				if (item.Value.Intersects(value))
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

		// Token: 0x0600410F RID: 16655 RVA: 0x005E4454 File Offset: 0x005E2654
		private static void FillPlayerHitboxes()
		{
			if (TETrainingDummy.playerBoxFilled)
			{
				return;
			}
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					TETrainingDummy.playerBox[i] = Main.player[i].getRect();
				}
			}
			TETrainingDummy.playerBoxFilled = true;
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x005E44A4 File Offset: 0x005E26A4
		public unsafe static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && *Main.tile[x, y].type == 378 && *Main.tile[x, y].frameY == 0 && *Main.tile[x, y].frameX % 36 == 0;
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x005E4517 File Offset: 0x005E2717
		public TETrainingDummy()
		{
			this.npc = -1;
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x005E4528 File Offset: 0x005E2728
		public static int Place(int x, int y)
		{
			TETrainingDummy tETrainingDummy = new TETrainingDummy();
			tETrainingDummy.Position = new Point16(x, y);
			tETrainingDummy.ID = TileEntity.AssignNewID();
			tETrainingDummy.type = TETrainingDummy._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[tETrainingDummy.ID] = tETrainingDummy;
				TileEntity.ByPosition[tETrainingDummy.Position] = tETrainingDummy;
			}
			return tETrainingDummy.ID;
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x005E45B4 File Offset: 0x005E27B4
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

		// Token: 0x06004114 RID: 16660 RVA: 0x005E4608 File Offset: 0x005E2808
		public static void Kill(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TETrainingDummy._myEntityID)
			{
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByID.Remove(value.ID);
					TileEntity.ByPosition.Remove(new Point16(x, y));
				}
			}
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x005E4688 File Offset: 0x005E2888
		public static int Find(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TETrainingDummy._myEntityID)
			{
				return value.ID;
			}
			return -1;
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x005E46BF File Offset: 0x005E28BF
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			writer.Write((short)this.npc);
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x005E46CE File Offset: 0x005E28CE
		public override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			this.npc = (int)reader.ReadInt16();
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x005E46DC File Offset: 0x005E28DC
		public void Activate()
		{
			int num = NPC.NewNPC(new EntitySource_TileEntity(this, null), (int)(this.Position.X * 16 + 16), (int)(this.Position.Y * 16 + 48), 488, 100, 0f, 0f, 0f, 0f, 255);
			Main.npc[num].ai[0] = (float)this.Position.X;
			Main.npc[num].ai[1] = (float)this.Position.Y;
			Main.npc[num].netUpdate = true;
			this.npc = num;
			if (Main.netMode != 1)
			{
				NetMessage.SendData(86, -1, -1, null, this.ID, (float)this.Position.X, (float)this.Position.Y, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x005E47B8 File Offset: 0x005E29B8
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

		// Token: 0x0600411A RID: 16666 RVA: 0x005E4820 File Offset: 0x005E2A20
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.Position.X.ToString(),
				"x  ",
				this.Position.Y.ToString(),
				"y npc: ",
				this.npc.ToString()
			});
		}

		// Token: 0x040058A0 RID: 22688
		private static Dictionary<int, Rectangle> playerBox = new Dictionary<int, Rectangle>();

		// Token: 0x040058A1 RID: 22689
		private static bool playerBoxFilled;

		// Token: 0x040058A2 RID: 22690
		private static byte _myEntityID;

		// Token: 0x040058A3 RID: 22691
		public int npc;

		// Token: 0x02000C2C RID: 3116
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400789E RID: 30878
			public static Action <0>__ClearBoxes;
		}
	}
}
