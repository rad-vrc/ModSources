using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000560 RID: 1376
	public sealed class TELogicSensor : TileEntity
	{
		// Token: 0x060040DD RID: 16605 RVA: 0x005E311C File Offset: 0x005E131C
		public override void RegisterTileEntityID(int assignedID)
		{
			TELogicSensor._myEntityID = (byte)assignedID;
			Action value;
			if ((value = TELogicSensor.<>O.<0>__UpdateStartInternal) == null)
			{
				value = (TELogicSensor.<>O.<0>__UpdateStartInternal = new Action(TELogicSensor.UpdateStartInternal));
			}
			TileEntity._UpdateStart += value;
			Action value2;
			if ((value2 = TELogicSensor.<>O.<1>__UpdateEndInternal) == null)
			{
				value2 = (TELogicSensor.<>O.<1>__UpdateEndInternal = new Action(TELogicSensor.UpdateEndInternal));
			}
			TileEntity._UpdateEnd += value2;
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x005E3170 File Offset: 0x005E1370
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TELogicSensor.NetPlaceEntity(x, y);
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x005E317C File Offset: 0x005E137C
		public static void NetPlaceEntity(int x, int y)
		{
			int num = TELogicSensor.Place(x, y);
			((TELogicSensor)TileEntity.ByID[num]).FigureCheckState();
			NetMessage.SendData(86, -1, -1, null, num, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x005E31BD File Offset: 0x005E13BD
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TELogicSensor.ValidTile(x, y);
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x005E31C6 File Offset: 0x005E13C6
		public override TileEntity GenerateInstance()
		{
			return new TELogicSensor();
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x005E31CD File Offset: 0x005E13CD
		private static void UpdateStartInternal()
		{
			TELogicSensor.inUpdateLoop = true;
			TELogicSensor.markedIDsForRemoval.Clear();
			TELogicSensor.playerBox.Clear();
			TELogicSensor.playerBoxFilled = false;
			TELogicSensor.FillPlayerHitboxes();
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x005E31F4 File Offset: 0x005E13F4
		private static void FillPlayerHitboxes()
		{
			if (TELogicSensor.playerBoxFilled)
			{
				return;
			}
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

		// Token: 0x060040E4 RID: 16612 RVA: 0x005E3250 File Offset: 0x005E1450
		private static void UpdateEndInternal()
		{
			TELogicSensor.inUpdateLoop = false;
			foreach (Tuple<Point16, bool> tripPoint in TELogicSensor.tripPoints)
			{
				Wiring.blockPlayerTeleportationForOneIteration = tripPoint.Item2;
				Wiring.HitSwitch((int)tripPoint.Item1.X, (int)tripPoint.Item1.Y);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(59, -1, -1, null, (int)tripPoint.Item1.X, (float)tripPoint.Item1.Y, 0f, 0f, 0, 0, 0);
				}
			}
			Wiring.blockPlayerTeleportationForOneIteration = false;
			TELogicSensor.tripPoints.Clear();
			foreach (int item in TELogicSensor.markedIDsForRemoval)
			{
				TileEntity value;
				if (TileEntity.ByID.TryGetValue(item, out value) && value.type == TELogicSensor._myEntityID)
				{
					object entityCreationLock = TileEntity.EntityCreationLock;
					lock (entityCreationLock)
					{
						TileEntity.ByID.Remove(item);
						TileEntity.ByPosition.Remove(value.Position);
					}
				}
			}
			TELogicSensor.markedIDsForRemoval.Clear();
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x005E33BC File Offset: 0x005E15BC
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

		// Token: 0x060040E6 RID: 16614 RVA: 0x005E343C File Offset: 0x005E163C
		public unsafe void ChangeState(bool onState, bool TripWire)
		{
			if (onState == this.On || TELogicSensor.SanityCheck((int)this.Position.X, (int)this.Position.Y))
			{
				*Main.tile[(int)this.Position.X, (int)this.Position.Y].frameX = (onState ? 18 : 0);
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
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x005E34FC File Offset: 0x005E16FC
		public unsafe static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && *Main.tile[x, y].type == 423 && *Main.tile[x, y].frameY % 18 == 0 && *Main.tile[x, y].frameX % 18 == 0;
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x005E3572 File Offset: 0x005E1772
		public TELogicSensor()
		{
			this.logicCheck = TELogicSensor.LogicCheckType.None;
			this.On = false;
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x005E3588 File Offset: 0x005E1788
		public unsafe static TELogicSensor.LogicCheckType FigureCheckType(int x, int y, out bool on)
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
			TELogicSensor.LogicCheckType result = TELogicSensor.LogicCheckType.None;
			switch (*tile.frameY / 18)
			{
			case 0:
				result = TELogicSensor.LogicCheckType.Day;
				break;
			case 1:
				result = TELogicSensor.LogicCheckType.Night;
				break;
			case 2:
				result = TELogicSensor.LogicCheckType.PlayerAbove;
				break;
			case 3:
				result = TELogicSensor.LogicCheckType.Water;
				break;
			case 4:
				result = TELogicSensor.LogicCheckType.Lava;
				break;
			case 5:
				result = TELogicSensor.LogicCheckType.Honey;
				break;
			case 6:
				result = TELogicSensor.LogicCheckType.Liquid;
				break;
			}
			on = TELogicSensor.GetState(x, y, result, null);
			return result;
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x005E3614 File Offset: 0x005E1814
		public unsafe static bool GetState(int x, int y, TELogicSensor.LogicCheckType type, TELogicSensor instance = null)
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
				Rectangle value;
				value..ctor(x * 16 - 32 - 1, y * 16 - 160 - 1, 82, 162);
				foreach (KeyValuePair<int, Rectangle> item in TELogicSensor.playerBox)
				{
					if (item.Value.Intersects(value))
					{
						return true;
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
				if (tile == null || *tile.liquid == 0)
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

		// Token: 0x060040EB RID: 16619 RVA: 0x005E3788 File Offset: 0x005E1988
		public void FigureCheckState()
		{
			this.logicCheck = TELogicSensor.FigureCheckType((int)this.Position.X, (int)this.Position.Y, out this.On);
			TELogicSensor.GetFrame((int)this.Position.X, (int)this.Position.Y, this.logicCheck, this.On);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x005E37E4 File Offset: 0x005E19E4
		public unsafe static void GetFrame(int x, int y, TELogicSensor.LogicCheckType type, bool on)
		{
			*Main.tile[x, y].frameX = (on ? 18 : 0);
			switch (type)
			{
			case TELogicSensor.LogicCheckType.Day:
				*Main.tile[x, y].frameY = 0;
				return;
			case TELogicSensor.LogicCheckType.Night:
				*Main.tile[x, y].frameY = 18;
				return;
			case TELogicSensor.LogicCheckType.PlayerAbove:
				*Main.tile[x, y].frameY = 36;
				return;
			case TELogicSensor.LogicCheckType.Water:
				*Main.tile[x, y].frameY = 54;
				return;
			case TELogicSensor.LogicCheckType.Lava:
				*Main.tile[x, y].frameY = 72;
				return;
			case TELogicSensor.LogicCheckType.Honey:
				*Main.tile[x, y].frameY = 90;
				return;
			case TELogicSensor.LogicCheckType.Liquid:
				*Main.tile[x, y].frameY = 108;
				return;
			default:
				*Main.tile[x, y].frameY = 0;
				return;
			}
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x005E38F8 File Offset: 0x005E1AF8
		public unsafe static bool SanityCheck(int x, int y)
		{
			if (!Main.tile[x, y].active() || *Main.tile[x, y].type != 423)
			{
				TELogicSensor.Kill(x, y);
				return false;
			}
			return true;
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x005E3944 File Offset: 0x005E1B44
		public static int Place(int x, int y)
		{
			TELogicSensor tELogicSensor = new TELogicSensor();
			tELogicSensor.Position = new Point16(x, y);
			tELogicSensor.ID = TileEntity.AssignNewID();
			tELogicSensor.type = TELogicSensor._myEntityID;
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByID[tELogicSensor.ID] = tELogicSensor;
				TileEntity.ByPosition[tELogicSensor.Position] = tELogicSensor;
			}
			return tELogicSensor.ID;
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x005E39D0 File Offset: 0x005E1BD0
		public static int Hook_AfterPlacement(int x, int y, int type = 423, int style = 0, int direction = 1, int alternate = 0)
		{
			bool on;
			TELogicSensor.LogicCheckType logicCheckType = TELogicSensor.FigureCheckType(x, y, out on);
			TELogicSensor.GetFrame(x, y, logicCheckType, on);
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

		// Token: 0x060040F0 RID: 16624 RVA: 0x005E3A40 File Offset: 0x005E1C40
		public static void Kill(int x, int y)
		{
			TileEntity value;
			if (!TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) || value.type != TELogicSensor._myEntityID)
			{
				return;
			}
			Wiring.blockPlayerTeleportationForOneIteration = (((TELogicSensor)value).logicCheck == TELogicSensor.LogicCheckType.PlayerAbove);
			bool flag = false;
			if (((TELogicSensor)value).logicCheck == TELogicSensor.LogicCheckType.PlayerAbove && ((TELogicSensor)value).On)
			{
				flag = true;
			}
			else if (((TELogicSensor)value).logicCheck == TELogicSensor.LogicCheckType.Water && ((TELogicSensor)value).On)
			{
				flag = true;
			}
			else if (((TELogicSensor)value).logicCheck == TELogicSensor.LogicCheckType.Lava && ((TELogicSensor)value).On)
			{
				flag = true;
			}
			else if (((TELogicSensor)value).logicCheck == TELogicSensor.LogicCheckType.Honey && ((TELogicSensor)value).On)
			{
				flag = true;
			}
			else if (((TELogicSensor)value).logicCheck == TELogicSensor.LogicCheckType.Liquid && ((TELogicSensor)value).On)
			{
				flag = true;
			}
			if (flag)
			{
				Wiring.HitSwitch((int)value.Position.X, (int)value.Position.Y);
				NetMessage.SendData(59, -1, -1, null, (int)value.Position.X, (float)value.Position.Y, 0f, 0f, 0, 0, 0);
			}
			Wiring.blockPlayerTeleportationForOneIteration = false;
			if (TELogicSensor.inUpdateLoop)
			{
				TELogicSensor.markedIDsForRemoval.Add(value.ID);
				return;
			}
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				TileEntity.ByPosition.Remove(new Point16(x, y));
				TileEntity.ByID.Remove(value.ID);
			}
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x005E3BD8 File Offset: 0x005E1DD8
		public static int Find(int x, int y)
		{
			TileEntity value;
			if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out value) && value.type == TELogicSensor._myEntityID)
			{
				return value.ID;
			}
			return -1;
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x005E3C0F File Offset: 0x005E1E0F
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			if (!networkSend)
			{
				writer.Write((byte)this.logicCheck);
				writer.Write(this.On);
			}
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x005E3C2D File Offset: 0x005E1E2D
		public override void ReadExtraData(BinaryReader reader, bool networkSend)
		{
			if (!networkSend)
			{
				this.logicCheck = (TELogicSensor.LogicCheckType)reader.ReadByte();
				this.On = reader.ReadBoolean();
			}
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x005E3C4C File Offset: 0x005E1E4C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.Position.X.ToString(),
				"x  ",
				this.Position.Y.ToString(),
				"y ",
				this.logicCheck.ToString()
			});
		}

		// Token: 0x04005893 RID: 22675
		private static byte _myEntityID;

		// Token: 0x04005894 RID: 22676
		private static Dictionary<int, Rectangle> playerBox = new Dictionary<int, Rectangle>();

		// Token: 0x04005895 RID: 22677
		private static List<Tuple<Point16, bool>> tripPoints = new List<Tuple<Point16, bool>>();

		// Token: 0x04005896 RID: 22678
		private static List<int> markedIDsForRemoval = new List<int>();

		// Token: 0x04005897 RID: 22679
		private static bool inUpdateLoop;

		// Token: 0x04005898 RID: 22680
		private static bool playerBoxFilled;

		// Token: 0x04005899 RID: 22681
		public TELogicSensor.LogicCheckType logicCheck;

		// Token: 0x0400589A RID: 22682
		public bool On;

		// Token: 0x0400589B RID: 22683
		public int CountedData;

		// Token: 0x02000C2A RID: 3114
		public enum LogicCheckType
		{
			// Token: 0x04007894 RID: 30868
			None,
			// Token: 0x04007895 RID: 30869
			Day,
			// Token: 0x04007896 RID: 30870
			Night,
			// Token: 0x04007897 RID: 30871
			PlayerAbove,
			// Token: 0x04007898 RID: 30872
			Water,
			// Token: 0x04007899 RID: 30873
			Lava,
			// Token: 0x0400789A RID: 30874
			Honey,
			// Token: 0x0400789B RID: 30875
			Liquid
		}

		// Token: 0x02000C2B RID: 3115
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400789C RID: 30876
			public static Action <0>__UpdateStartInternal;

			// Token: 0x0400789D RID: 30877
			public static Action <1>__UpdateEndInternal;
		}
	}
}
