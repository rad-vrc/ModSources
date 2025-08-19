using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000219 RID: 537
	public class TELogicSensor : TileEntity
	{
		// Token: 0x06001E70 RID: 7792 RVA: 0x0050AB35 File Offset: 0x00508D35
		public override void RegisterTileEntityID(int assignedID)
		{
			TELogicSensor._myEntityID = (byte)assignedID;
			TileEntity._UpdateStart += TELogicSensor.UpdateStartInternal;
			TileEntity._UpdateEnd += TELogicSensor.UpdateEndInternal;
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0050AB60 File Offset: 0x00508D60
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TELogicSensor.NetPlaceEntity(x, y);
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0050AB6C File Offset: 0x00508D6C
		public static void NetPlaceEntity(int x, int y)
		{
			int num = TELogicSensor.Place(x, y);
			((TELogicSensor)TileEntity.ByID[num]).FigureCheckState();
			NetMessage.SendData(86, -1, -1, null, num, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0050ABAD File Offset: 0x00508DAD
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TELogicSensor.ValidTile(x, y);
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x0050ABB6 File Offset: 0x00508DB6
		public override TileEntity GenerateInstance()
		{
			return new TELogicSensor();
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x0050ABBD File Offset: 0x00508DBD
		private static void UpdateStartInternal()
		{
			TELogicSensor.inUpdateLoop = true;
			TELogicSensor.markedIDsForRemoval.Clear();
			TELogicSensor.playerBox.Clear();
			TELogicSensor.playerBoxFilled = false;
			TELogicSensor.FillPlayerHitboxes();
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0050ABE4 File Offset: 0x00508DE4
		private static void FillPlayerHitboxes()
		{
			if (!TELogicSensor.playerBoxFilled)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active && !player.dead && !player.ghost)
					{
						TELogicSensor.playerBox[i] = player.getRect();
					}
				}
				TELogicSensor.playerBoxFilled = true;
			}
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0050AC40 File Offset: 0x00508E40
		private static void UpdateEndInternal()
		{
			TELogicSensor.inUpdateLoop = false;
			foreach (Tuple<Point16, bool> tuple in TELogicSensor.tripPoints)
			{
				Wiring.blockPlayerTeleportationForOneIteration = tuple.Item2;
				Wiring.HitSwitch((int)tuple.Item1.X, (int)tuple.Item1.Y);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(59, -1, -1, null, (int)tuple.Item1.X, (float)tuple.Item1.Y, 0f, 0f, 0, 0, 0);
				}
			}
			Wiring.blockPlayerTeleportationForOneIteration = false;
			TELogicSensor.tripPoints.Clear();
			foreach (int key in TELogicSensor.markedIDsForRemoval)
			{
				TileEntity tileEntity;
				if (TileEntity.ByID.TryGetValue(key, out tileEntity) && tileEntity.type == TELogicSensor._myEntityID)
				{
					object entityCreationLock = TileEntity.EntityCreationLock;
					lock (entityCreationLock)
					{
						TileEntity.ByID.Remove(key);
						TileEntity.ByPosition.Remove(tileEntity.Position);
					}
				}
			}
			TELogicSensor.markedIDsForRemoval.Clear();
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0050ADAC File Offset: 0x00508FAC
		public override void Update()
		{
			bool state = TELogicSensor.GetState((int)this.Position.X, (int)this.Position.Y, this.logicCheck, this);
			TELogicSensor.LogicCheckType logicCheckType = this.logicCheck;
			if (logicCheckType - TELogicSensor.LogicCheckType.Day > 1)
			{
				if (logicCheckType - TELogicSensor.LogicCheckType.PlayerAbove > 4)
				{
					return;
				}
				if (this.On != state)
				{
					this.ChangeState(state, true);
				}
			}
			else
			{
				if (!this.On && state)
				{
					this.ChangeState(true, true);
				}
				if (this.On && !state)
				{
					this.ChangeState(false, false);
					return;
				}
			}
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0050AE2C File Offset: 0x0050902C
		public void ChangeState(bool onState, bool TripWire)
		{
			if (onState != this.On && !TELogicSensor.SanityCheck((int)this.Position.X, (int)this.Position.Y))
			{
				return;
			}
			Main.tile[(int)this.Position.X, (int)this.Position.Y].frameX = (onState ? 18 : 0);
			this.On = onState;
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, (int)this.Position.X, (int)this.Position.Y, TileChangeType.None);
			}
			if (TripWire && Main.netMode != 1)
			{
				TELogicSensor.tripPoints.Add(Tuple.Create<Point16, bool>(this.Position, this.logicCheck == TELogicSensor.LogicCheckType.PlayerAbove));
			}
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0050AEE4 File Offset: 0x005090E4
		public static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 423 && Main.tile[x, y].frameY % 18 == 0 && Main.tile[x, y].frameX % 18 == 0;
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x0050AF4B File Offset: 0x0050914B
		public TELogicSensor()
		{
			this.logicCheck = TELogicSensor.LogicCheckType.None;
			this.On = false;
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x0050AF64 File Offset: 0x00509164
		public static TELogicSensor.LogicCheckType FigureCheckType(int x, int y, out bool on)
		{
			on = false;
			if (!WorldGen.InWorld(x, y, 0))
			{
				return TELogicSensor.LogicCheckType.None;
			}
			Tile tile = Main.tile[x, y];
			if (tile == null)
			{
				return TELogicSensor.LogicCheckType.None;
			}
			TELogicSensor.LogicCheckType logicCheckType = TELogicSensor.LogicCheckType.None;
			switch (tile.frameY / 18)
			{
			case 0:
				logicCheckType = TELogicSensor.LogicCheckType.Day;
				break;
			case 1:
				logicCheckType = TELogicSensor.LogicCheckType.Night;
				break;
			case 2:
				logicCheckType = TELogicSensor.LogicCheckType.PlayerAbove;
				break;
			case 3:
				logicCheckType = TELogicSensor.LogicCheckType.Water;
				break;
			case 4:
				logicCheckType = TELogicSensor.LogicCheckType.Lava;
				break;
			case 5:
				logicCheckType = TELogicSensor.LogicCheckType.Honey;
				break;
			case 6:
				logicCheckType = TELogicSensor.LogicCheckType.Liquid;
				break;
			}
			on = TELogicSensor.GetState(x, y, logicCheckType, null);
			return logicCheckType;
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x0050AFE8 File Offset: 0x005091E8
		public static bool GetState(int x, int y, TELogicSensor.LogicCheckType type, TELogicSensor instance = null)
		{
			switch (type)
			{
			case TELogicSensor.LogicCheckType.Day:
				return Main.dayTime;
			case TELogicSensor.LogicCheckType.Night:
				return !Main.dayTime;
			case TELogicSensor.LogicCheckType.PlayerAbove:
			{
				bool result = false;
				Rectangle value = new Rectangle(x * 16 - 32 - 1, y * 16 - 160 - 1, 82, 162);
				foreach (KeyValuePair<int, Rectangle> keyValuePair in TELogicSensor.playerBox)
				{
					if (keyValuePair.Value.Intersects(value))
					{
						result = true;
						break;
					}
				}
				return result;
			}
			case TELogicSensor.LogicCheckType.Water:
			case TELogicSensor.LogicCheckType.Lava:
			case TELogicSensor.LogicCheckType.Honey:
			case TELogicSensor.LogicCheckType.Liquid:
			{
				if (instance == null)
				{
					return false;
				}
				Tile tile = Main.tile[x, y];
				bool flag = true;
				if (tile == null || tile.liquid == 0)
				{
					flag = false;
				}
				if (!tile.lava() && type == TELogicSensor.LogicCheckType.Lava)
				{
					flag = false;
				}
				if (!tile.honey() && type == TELogicSensor.LogicCheckType.Honey)
				{
					flag = false;
				}
				if ((tile.honey() || tile.lava()) && type == TELogicSensor.LogicCheckType.Water)
				{
					flag = false;
				}
				if (!flag && instance.On)
				{
					if (instance.CountedData == 0)
					{
						instance.CountedData = 15;
					}
					else if (instance.CountedData > 0)
					{
						instance.CountedData--;
					}
					flag = (instance.CountedData > 0);
				}
				return flag;
			}
			default:
				return false;
			}
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x0050B14C File Offset: 0x0050934C
		public void FigureCheckState()
		{
			this.logicCheck = TELogicSensor.FigureCheckType((int)this.Position.X, (int)this.Position.Y, out this.On);
			TELogicSensor.GetFrame((int)this.Position.X, (int)this.Position.Y, this.logicCheck, this.On);
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0050B1A8 File Offset: 0x005093A8
		public static void GetFrame(int x, int y, TELogicSensor.LogicCheckType type, bool on)
		{
			Main.tile[x, y].frameX = (on ? 18 : 0);
			switch (type)
			{
			case TELogicSensor.LogicCheckType.Day:
				Main.tile[x, y].frameY = 0;
				return;
			case TELogicSensor.LogicCheckType.Night:
				Main.tile[x, y].frameY = 18;
				return;
			case TELogicSensor.LogicCheckType.PlayerAbove:
				Main.tile[x, y].frameY = 36;
				return;
			case TELogicSensor.LogicCheckType.Water:
				Main.tile[x, y].frameY = 54;
				return;
			case TELogicSensor.LogicCheckType.Lava:
				Main.tile[x, y].frameY = 72;
				return;
			case TELogicSensor.LogicCheckType.Honey:
				Main.tile[x, y].frameY = 90;
				return;
			case TELogicSensor.LogicCheckType.Liquid:
				Main.tile[x, y].frameY = 108;
				return;
			default:
				Main.tile[x, y].frameY = 0;
				return;
			}
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x0050B295 File Offset: 0x00509495
		public static bool SanityCheck(int x, int y)
		{
			if (!Main.tile[x, y].active() || Main.tile[x, y].type != 423)
			{
				TELogicSensor.Kill(x, y);
				return false;
			}
			return true;
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x0050B2CC File Offset: 0x005094CC
		public static int Place(int x, int y)
		{
			TELogicSensor telogicSensor = new TELogicSensor();
			telogicSensor.Position = new Point16(x, y);
			telogicSensor.ID = TileEntity.AssignNewID();
			telogicSensor.type = TELogicSensor._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[telogicSensor.ID] = telogicSensor;
				TileEntity.ByPosition[telogicSensor.Position] = telogicSensor;
			}
			return telogicSensor.ID;
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x0050B358 File Offset: 0x00509558
		public static int Hook_AfterPlacement(int x, int y, int type = 423, int style = 0, int direction = 1, int alternate = 0)
		{
			bool on;
			TELogicSensor.LogicCheckType type2 = TELogicSensor.FigureCheckType(x, y, out on);
			TELogicSensor.GetFrame(x, y, type2, on);
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x, y, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x, (float)y, (float)TELogicSensor._myEntityID, 0f, 0, 0, 0);
				return -1;
			}
			int num = TELogicSensor.Place(x, y);
			((TELogicSensor)TileEntity.ByID[num]).FigureCheckState();
			return num;
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x0050B3C8 File Offset: 0x005095C8
		public static void Kill(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TELogicSensor._myEntityID)
			{
				Wiring.blockPlayerTeleportationForOneIteration = (((TELogicSensor)tileEntity).logicCheck == TELogicSensor.LogicCheckType.PlayerAbove);
				bool flag = false;
				if (((TELogicSensor)tileEntity).logicCheck == TELogicSensor.LogicCheckType.PlayerAbove && ((TELogicSensor)tileEntity).On)
				{
					flag = true;
				}
				else if (((TELogicSensor)tileEntity).logicCheck == TELogicSensor.LogicCheckType.Water && ((TELogicSensor)tileEntity).On)
				{
					flag = true;
				}
				else if (((TELogicSensor)tileEntity).logicCheck == TELogicSensor.LogicCheckType.Lava && ((TELogicSensor)tileEntity).On)
				{
					flag = true;
				}
				else if (((TELogicSensor)tileEntity).logicCheck == TELogicSensor.LogicCheckType.Honey && ((TELogicSensor)tileEntity).On)
				{
					flag = true;
				}
				else if (((TELogicSensor)tileEntity).logicCheck == TELogicSensor.LogicCheckType.Liquid && ((TELogicSensor)tileEntity).On)
				{
					flag = true;
				}
				if (flag)
				{
					Wiring.HitSwitch((int)tileEntity.Position.X, (int)tileEntity.Position.Y);
					NetMessage.SendData(59, -1, -1, null, (int)tileEntity.Position.X, (float)tileEntity.Position.Y, 0f, 0f, 0, 0, 0);
				}
				Wiring.blockPlayerTeleportationForOneIteration = false;
				if (TELogicSensor.inUpdateLoop)
				{
					TELogicSensor.markedIDsForRemoval.Add(tileEntity.ID);
					return;
				}
				object entityCreationLock = TileEntity.EntityCreationLock;
				lock (entityCreationLock)
				{
					TileEntity.ByPosition.Remove(new Point16(x, y));
					TileEntity.ByID.Remove(tileEntity.ID);
				}
			}
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x0050B564 File Offset: 0x00509764
		public static int Find(int x, int y)
		{
			TileEntity tileEntity;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && tileEntity.type == TELogicSensor._myEntityID)
			{
				return tileEntity.ID;
			}
			return -1;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x0050B59B File Offset: 0x0050979B
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			if (!networkSend)
			{
				writer.Write((byte)this.logicCheck);
				writer.Write(this.On);
			}
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x0050B5B9 File Offset: 0x005097B9
		public override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			if (!networkSend)
			{
				this.logicCheck = (TELogicSensor.LogicCheckType)reader.ReadByte();
				this.On = reader.ReadBoolean();
			}
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x0050B5D8 File Offset: 0x005097D8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Position.X,
				"x  ",
				this.Position.Y,
				"y ",
				this.logicCheck
			});
		}

		// Token: 0x040045AF RID: 17839
		private static byte _myEntityID;

		// Token: 0x040045B0 RID: 17840
		private static Dictionary<int, Rectangle> playerBox = new Dictionary<int, Rectangle>();

		// Token: 0x040045B1 RID: 17841
		private static List<Tuple<Point16, bool>> tripPoints = new List<Tuple<Point16, bool>>();

		// Token: 0x040045B2 RID: 17842
		private static List<int> markedIDsForRemoval = new List<int>();

		// Token: 0x040045B3 RID: 17843
		private static bool inUpdateLoop;

		// Token: 0x040045B4 RID: 17844
		private static bool playerBoxFilled;

		// Token: 0x040045B5 RID: 17845
		public TELogicSensor.LogicCheckType logicCheck;

		// Token: 0x040045B6 RID: 17846
		public bool On;

		// Token: 0x040045B7 RID: 17847
		public int CountedData;

		// Token: 0x0200062C RID: 1580
		public enum LogicCheckType
		{
			// Token: 0x040060DD RID: 24797
			None,
			// Token: 0x040060DE RID: 24798
			Day,
			// Token: 0x040060DF RID: 24799
			Night,
			// Token: 0x040060E0 RID: 24800
			PlayerAbove,
			// Token: 0x040060E1 RID: 24801
			Water,
			// Token: 0x040060E2 RID: 24802
			Lava,
			// Token: 0x040060E3 RID: 24803
			Honey,
			// Token: 0x040060E4 RID: 24804
			Liquid
		}
	}
}
