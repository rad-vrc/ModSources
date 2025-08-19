using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terraria
{
	// Token: 0x0200006A RID: 106
	public static class Wiring
	{
		// Token: 0x060010F9 RID: 4345 RVA: 0x004061E7 File Offset: 0x004043E7
		public static void SetCurrentUser(int plr = -1)
		{
			if (plr < 0 || plr > 255)
			{
				plr = 255;
			}
			if (Main.netMode == 0)
			{
				plr = Main.myPlayer;
			}
			Wiring.CurrentUser = plr;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00406210 File Offset: 0x00404410
		public static void Initialize()
		{
			Wiring._wireSkip = new Dictionary<Point16, bool>();
			Wiring._wireList = new DoubleStack<Point16>(1024, 0);
			Wiring._wireDirectionList = new DoubleStack<byte>(1024, 0);
			Wiring._toProcess = new Dictionary<Point16, byte>();
			Wiring._GatesCurrent = new Queue<Point16>();
			Wiring._GatesNext = new Queue<Point16>();
			Wiring._GatesDone = new Dictionary<Point16, bool>();
			Wiring._LampsToCheck = new Queue<Point16>();
			Wiring._PixelBoxTriggers = new Dictionary<Point16, byte>();
			Wiring._inPumpX = new int[20];
			Wiring._inPumpY = new int[20];
			Wiring._outPumpX = new int[20];
			Wiring._outPumpY = new int[20];
			Wiring._teleport = new Vector2[]
			{
				Vector2.One * -1f,
				Vector2.One * -1f
			};
			Wiring._mechX = new int[1000];
			Wiring._mechY = new int[1000];
			Wiring._mechTime = new int[1000];
		}

		/// <summary>
		/// Use to prevent wire signals from running for the provided coordinates. Typically used in multi-tiles to prevent a wire touching multiple tiles of the multi-tile from erroneously running wire code multiple times. In <see cref="M:Terraria.ModLoader.ModTile.HitWire(System.Int32,System.Int32)" />, call SkipWire for all the coordinates the tile covers.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		// Token: 0x060010FB RID: 4347 RVA: 0x00406317 File Offset: 0x00404517
		public static void SkipWire(int x, int y)
		{
			Wiring._wireSkip[new Point16(x, y)] = true;
		}

		/// <inheritdoc cref="M:Terraria.Wiring.SkipWire(System.Int32,System.Int32)" />
		// Token: 0x060010FC RID: 4348 RVA: 0x0040632B File Offset: 0x0040452B
		public static void SkipWire(Point16 point)
		{
			Wiring._wireSkip[point] = true;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0040633C File Offset: 0x0040453C
		public static void ClearAll()
		{
			for (int i = 0; i < 20; i++)
			{
				Wiring._inPumpX[i] = 0;
				Wiring._inPumpY[i] = 0;
				Wiring._outPumpX[i] = 0;
				Wiring._outPumpY[i] = 0;
			}
			Wiring._numInPump = 0;
			Wiring._numOutPump = 0;
			for (int j = 0; j < 1000; j++)
			{
				Wiring._mechTime[j] = 0;
				Wiring._mechX[j] = 0;
				Wiring._mechY[j] = 0;
			}
			Wiring._numMechs = 0;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x004063B0 File Offset: 0x004045B0
		public unsafe static void UpdateMech()
		{
			Wiring.SetCurrentUser(-1);
			for (int num = Wiring._numMechs - 1; num >= 0; num--)
			{
				Wiring._mechTime[num]--;
				int num2 = Wiring._mechX[num];
				int num3 = Wiring._mechY[num];
				if (!WorldGen.InWorld(num2, num3, 1))
				{
					Wiring._numMechs--;
				}
				else
				{
					Tile tile = Main.tile[num2, num3];
					if (tile == null)
					{
						Wiring._numMechs--;
					}
					else
					{
						if (tile.active() && *tile.type == 144)
						{
							if (*tile.frameY == 0)
							{
								Wiring._mechTime[num] = 0;
							}
							else
							{
								int num4 = (int)(*tile.frameX / 18);
								switch (num4)
								{
								case 0:
									num4 = 60;
									break;
								case 1:
									num4 = 180;
									break;
								case 2:
									num4 = 300;
									break;
								case 3:
									num4 = 30;
									break;
								case 4:
									num4 = 15;
									break;
								}
								if (Math.IEEERemainder((double)Wiring._mechTime[num], (double)num4) == 0.0)
								{
									Wiring._mechTime[num] = 18000;
									Wiring.TripWire(Wiring._mechX[num], Wiring._mechY[num], 1, 1);
								}
							}
						}
						if (Wiring._mechTime[num] <= 0)
						{
							if (tile.active() && *tile.type == 144)
							{
								*tile.frameY = 0;
								NetMessage.SendTileSquare(-1, Wiring._mechX[num], Wiring._mechY[num], TileChangeType.None);
							}
							if (tile.active() && *tile.type == 411)
							{
								int num5 = (int)(*tile.frameX % 36 / 18);
								int num6 = (int)(*tile.frameY % 36 / 18);
								int num7 = Wiring._mechX[num] - num5;
								int num8 = Wiring._mechY[num] - num6;
								int num9 = 36;
								if (*Main.tile[num7, num8].frameX >= 36)
								{
									num9 = -36;
								}
								for (int i = num7; i < num7 + 2; i++)
								{
									for (int j = num8; j < num8 + 2; j++)
									{
										if (WorldGen.InWorld(i, j, 1))
										{
											Tile tile2 = Main.tile[i, j];
											if (tile2 != null)
											{
												*tile2.frameX = (short)((int)(*tile2.frameX) + num9);
											}
										}
									}
								}
								NetMessage.SendTileSquare(-1, num7, num8, 2, 2, TileChangeType.None);
							}
							for (int k = num; k < Wiring._numMechs; k++)
							{
								Wiring._mechX[k] = Wiring._mechX[k + 1];
								Wiring._mechY[k] = Wiring._mechY[k + 1];
								Wiring._mechTime[k] = Wiring._mechTime[k + 1];
							}
							Wiring._numMechs--;
						}
					}
				}
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0040667C File Offset: 0x0040487C
		public unsafe static void HitSwitch(int i, int j)
		{
			if (!WorldGen.InWorld(i, j, 0) || Main.tile[i, j] == null)
			{
				return;
			}
			if (*Main.tile[i, j].type == 135 || *Main.tile[i, j].type == 314 || *Main.tile[i, j].type == 423 || *Main.tile[i, j].type == 428 || *Main.tile[i, j].type == 442 || *Main.tile[i, j].type == 476)
			{
				SoundEngine.PlaySound(28, i * 16, j * 16, 0, 1f, 0f);
				Wiring.TripWire(i, j, 1, 1);
				return;
			}
			if (*Main.tile[i, j].type == 440)
			{
				SoundEngine.PlaySound(28, i * 16 + 16, j * 16 + 16, 0, 1f, 0f);
				Wiring.TripWire(i, j, 3, 3);
				return;
			}
			if (*Main.tile[i, j].type == 136)
			{
				if (*Main.tile[i, j].frameY == 0)
				{
					*Main.tile[i, j].frameY = 18;
				}
				else
				{
					*Main.tile[i, j].frameY = 0;
				}
				SoundEngine.PlaySound(28, i * 16, j * 16, 0, 1f, 0f);
				Wiring.TripWire(i, j, 1, 1);
				return;
			}
			if (*Main.tile[i, j].type == 443)
			{
				Wiring.GeyserTrap(i, j);
				return;
			}
			if (*Main.tile[i, j].type == 144)
			{
				if (*Main.tile[i, j].frameY == 0)
				{
					*Main.tile[i, j].frameY = 18;
					if (Main.netMode != 1)
					{
						Wiring.CheckMech(i, j, 18000);
					}
				}
				else
				{
					*Main.tile[i, j].frameY = 0;
				}
				SoundEngine.PlaySound(28, i * 16, j * 16, 0, 1f, 0f);
				return;
			}
			if (*Main.tile[i, j].type == 441 || *Main.tile[i, j].type == 468)
			{
				int num = (int)(*Main.tile[i, j].frameX / 18 * -1);
				int num2 = (int)(*Main.tile[i, j].frameY / 18 * -1);
				num %= 4;
				if (num < -1)
				{
					num += 2;
				}
				num += i;
				num2 += j;
				SoundEngine.PlaySound(28, i * 16, j * 16, 0, 1f, 0f);
				Wiring.TripWire(num, num2, 2, 2);
				return;
			}
			if (*Main.tile[i, j].type == 467)
			{
				if (*Main.tile[i, j].frameX / 36 == 4)
				{
					int num3 = (int)(*Main.tile[i, j].frameX / 18 * -1);
					int num4 = (int)(*Main.tile[i, j].frameY / 18 * -1);
					num3 %= 4;
					if (num3 < -1)
					{
						num3 += 2;
					}
					num3 += i;
					num4 += j;
					SoundEngine.PlaySound(28, i * 16, j * 16, 0, 1f, 0f);
					Wiring.TripWire(num3, num4, 2, 2);
					return;
				}
			}
			else
			{
				if (*Main.tile[i, j].type != 132 && *Main.tile[i, j].type != 411)
				{
					return;
				}
				short num5 = 36;
				int num6 = (int)(*Main.tile[i, j].frameX / 18 * -1);
				int num7 = (int)(*Main.tile[i, j].frameY / 18 * -1);
				num6 %= 4;
				if (num6 < -1)
				{
					num6 += 2;
					num5 = -36;
				}
				num6 += i;
				num7 += j;
				if (Main.netMode != 1 && *Main.tile[num6, num7].type == 411)
				{
					Wiring.CheckMech(num6, num7, 60);
				}
				for (int k = num6; k < num6 + 2; k++)
				{
					for (int l = num7; l < num7 + 2; l++)
					{
						if (*Main.tile[k, l].type == 132 || *Main.tile[k, l].type == 411)
						{
							ref short frameX = ref Main.tile[k, l].frameX;
							frameX += num5;
						}
					}
				}
				WorldGen.TileFrame(num6, num7, false, false);
				SoundEngine.PlaySound(28, i * 16, j * 16, 0, 1f, 0f);
				Wiring.TripWire(num6, num7, 2, 2);
			}
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00406BCE File Offset: 0x00404DCE
		public static void PokeLogicGate(int lampX, int lampY)
		{
			if (Main.netMode != 1)
			{
				Wiring._LampsToCheck.Enqueue(new Point16(lampX, lampY));
				Wiring.LogicGatePass();
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00406BF0 File Offset: 0x00404DF0
		public static bool Actuate(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			if (!tile.actuator())
			{
				return false;
			}
			if (tile.inActive())
			{
				Wiring.ReActive(i, j);
			}
			else
			{
				Wiring.DeActive(i, j);
			}
			return true;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00406C30 File Offset: 0x00404E30
		public static void ActuateForced(int i, int j)
		{
			if (Main.tile[i, j].inActive())
			{
				Wiring.ReActive(i, j);
				return;
			}
			Wiring.DeActive(i, j);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00406C64 File Offset: 0x00404E64
		public static void MassWireOperation(Point ps, Point pe, Player master)
		{
			int wireCount = 0;
			int actuatorCount = 0;
			for (int i = 0; i < 58; i++)
			{
				if (master.inventory[i].type == 530)
				{
					wireCount += master.inventory[i].stack;
				}
				if (master.inventory[i].type == 849)
				{
					actuatorCount += master.inventory[i].stack;
				}
			}
			int num5 = wireCount;
			int num2 = actuatorCount;
			Wiring.MassWireOperationInner(master, ps, pe, master.Center, master.direction == 1, ref wireCount, ref actuatorCount);
			int num3 = num5 - wireCount;
			int num4 = num2 - actuatorCount;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(110, master.whoAmI, -1, null, 530, (float)num3, (float)master.whoAmI, 0f, 0, 0, 0);
				NetMessage.SendData(110, master.whoAmI, -1, null, 849, (float)num4, (float)master.whoAmI, 0f, 0, 0, 0);
				return;
			}
			for (int j = 0; j < num3; j++)
			{
				master.ConsumeItem(530, false, false);
			}
			for (int k = 0; k < num4; k++)
			{
				master.ConsumeItem(849, false, false);
			}
		}

		/// <summary>
		/// Returns true if the wiring cooldown has been reached for the provided tile coordinates. The <paramref name="time" /> parameter will set the cooldown if wiring cooldown has elapsed. Use larger values for less frequent activations. Use this method to limit how often mechanisms can be triggerd. <see cref="M:Terraria.Item.MechSpawn(System.Single,System.Single,System.Int32)" /> and <see cref="M:Terraria.NPC.MechSpawn(System.Single,System.Single,System.Int32)" /> should also be checked for mechanisms spawning items or NPC, such as Statues.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		// Token: 0x06001104 RID: 4356 RVA: 0x00406D8C File Offset: 0x00404F8C
		public static bool CheckMech(int i, int j, int time)
		{
			for (int k = 0; k < Wiring._numMechs; k++)
			{
				if (Wiring._mechX[k] == i && Wiring._mechY[k] == j)
				{
					return false;
				}
			}
			if (Wiring._numMechs < 999)
			{
				Wiring._mechX[Wiring._numMechs] = i;
				Wiring._mechY[Wiring._numMechs] = j;
				Wiring._mechTime[Wiring._numMechs] = time;
				Wiring._numMechs++;
				return true;
			}
			return false;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00406E00 File Offset: 0x00405000
		private unsafe static void XferWater()
		{
			for (int i = 0; i < Wiring._numInPump; i++)
			{
				int num = Wiring._inPumpX[i];
				int num2 = Wiring._inPumpY[i];
				int liquid = (int)(*Main.tile[num, num2].liquid);
				if (liquid > 0)
				{
					byte b = Main.tile[num, num2].liquidType();
					for (int j = 0; j < Wiring._numOutPump; j++)
					{
						int num3 = Wiring._outPumpX[j];
						int num4 = Wiring._outPumpY[j];
						int liquid2 = (int)(*Main.tile[num3, num4].liquid);
						if (liquid2 < 255)
						{
							byte b2 = Main.tile[num3, num4].liquidType();
							if (liquid2 == 0)
							{
								b2 = b;
							}
							if (b2 == b)
							{
								int num5 = liquid;
								if (num5 + liquid2 > 255)
								{
									num5 = 255 - liquid2;
								}
								ref byte liquid3 = ref Main.tile[num3, num4].liquid;
								liquid3 += (byte)num5;
								ref byte liquid4 = ref Main.tile[num, num2].liquid;
								liquid4 -= (byte)num5;
								liquid = (int)(*Main.tile[num, num2].liquid);
								Main.tile[num3, num4].liquidType((int)b);
								WorldGen.SquareTileFrame(num3, num4, true);
								if (*Main.tile[num, num2].liquid == 0)
								{
									Main.tile[num, num2].liquidType(0);
									WorldGen.SquareTileFrame(num, num2, true);
									break;
								}
							}
						}
					}
					WorldGen.SquareTileFrame(num, num2, true);
				}
			}
		}

		/// <summary>
		/// Used to send a single to wiring wired up to the specified area. The parameters represent the tile coordinates where wire signals will be sent. Mechanism tiles such as <see cref="F:Terraria.ID.TileID.Switches" />, <see cref="F:Terraria.ID.TileID.PressurePlates" />, <see cref="F:Terraria.ID.TileID.Timers" />, and <see cref="F:Terraria.ID.TileID.LogicSensor" /> use this method as part of their interaction code.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="top"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		// Token: 0x06001106 RID: 4358 RVA: 0x00406FB8 File Offset: 0x004051B8
		public static void TripWire(int left, int top, int width, int height)
		{
			if (Main.netMode == 1)
			{
				return;
			}
			Wiring.running = true;
			if (Wiring._wireList.Count != 0)
			{
				Wiring._wireList.Clear(true);
			}
			if (Wiring._wireDirectionList.Count != 0)
			{
				Wiring._wireDirectionList.Clear(true);
			}
			Vector2[] array = new Vector2[8];
			int num = 0;
			for (int i = left; i < left + width; i++)
			{
				for (int j = top; j < top + height; j++)
				{
					Point16 back = new Point16(i, j);
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.wire())
					{
						Wiring._wireList.PushBack(back);
					}
				}
			}
			Wiring._teleport[0].X = -1f;
			Wiring._teleport[0].Y = -1f;
			Wiring._teleport[1].X = -1f;
			Wiring._teleport[1].Y = -1f;
			if (Wiring._wireList.Count > 0)
			{
				Wiring._numInPump = 0;
				Wiring._numOutPump = 0;
				Wiring.HitWire(Wiring._wireList, 1);
				if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
				{
					Wiring.XferWater();
				}
			}
			array[num++] = Wiring._teleport[0];
			array[num++] = Wiring._teleport[1];
			for (int k = left; k < left + width; k++)
			{
				for (int l = top; l < top + height; l++)
				{
					Point16 back2 = new Point16(k, l);
					Tile tile2 = Main.tile[k, l];
					if (tile2 != null && tile2.wire2())
					{
						Wiring._wireList.PushBack(back2);
					}
				}
			}
			Wiring._teleport[0].X = -1f;
			Wiring._teleport[0].Y = -1f;
			Wiring._teleport[1].X = -1f;
			Wiring._teleport[1].Y = -1f;
			if (Wiring._wireList.Count > 0)
			{
				Wiring._numInPump = 0;
				Wiring._numOutPump = 0;
				Wiring.HitWire(Wiring._wireList, 2);
				if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
				{
					Wiring.XferWater();
				}
			}
			array[num++] = Wiring._teleport[0];
			array[num++] = Wiring._teleport[1];
			Wiring._teleport[0].X = -1f;
			Wiring._teleport[0].Y = -1f;
			Wiring._teleport[1].X = -1f;
			Wiring._teleport[1].Y = -1f;
			for (int m = left; m < left + width; m++)
			{
				for (int n = top; n < top + height; n++)
				{
					Point16 back3 = new Point16(m, n);
					Tile tile3 = Main.tile[m, n];
					if (tile3 != null && tile3.wire3())
					{
						Wiring._wireList.PushBack(back3);
					}
				}
			}
			if (Wiring._wireList.Count > 0)
			{
				Wiring._numInPump = 0;
				Wiring._numOutPump = 0;
				Wiring.HitWire(Wiring._wireList, 3);
				if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
				{
					Wiring.XferWater();
				}
			}
			array[num++] = Wiring._teleport[0];
			array[num++] = Wiring._teleport[1];
			Wiring._teleport[0].X = -1f;
			Wiring._teleport[0].Y = -1f;
			Wiring._teleport[1].X = -1f;
			Wiring._teleport[1].Y = -1f;
			for (int num2 = left; num2 < left + width; num2++)
			{
				for (int num3 = top; num3 < top + height; num3++)
				{
					Point16 back4 = new Point16(num2, num3);
					Tile tile4 = Main.tile[num2, num3];
					if (tile4 != null && tile4.wire4())
					{
						Wiring._wireList.PushBack(back4);
					}
				}
			}
			if (Wiring._wireList.Count > 0)
			{
				Wiring._numInPump = 0;
				Wiring._numOutPump = 0;
				Wiring.HitWire(Wiring._wireList, 4);
				if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
				{
					Wiring.XferWater();
				}
			}
			array[num++] = Wiring._teleport[0];
			array[num++] = Wiring._teleport[1];
			Wiring.running = false;
			for (int num4 = 0; num4 < 8; num4 += 2)
			{
				Wiring._teleport[0] = array[num4];
				Wiring._teleport[1] = array[num4 + 1];
				if (Wiring._teleport[0].X >= 0f && Wiring._teleport[1].X >= 0f)
				{
					Wiring.Teleport();
				}
			}
			Wiring.PixelBoxPass();
			Wiring.LogicGatePass();
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x004074D8 File Offset: 0x004056D8
		private unsafe static void PixelBoxPass()
		{
			foreach (KeyValuePair<Point16, byte> pixelBoxTrigger in Wiring._PixelBoxTriggers)
			{
				if (pixelBoxTrigger.Value == 3)
				{
					Tile tile = Main.tile[(int)pixelBoxTrigger.Key.X, (int)pixelBoxTrigger.Key.Y];
					*tile.frameX = ((*tile.frameX != 18) ? 18 : 0);
					NetMessage.SendTileSquare(-1, (int)pixelBoxTrigger.Key.X, (int)pixelBoxTrigger.Key.Y, TileChangeType.None);
				}
			}
			Wiring._PixelBoxTriggers.Clear();
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00407594 File Offset: 0x00405794
		private static void LogicGatePass()
		{
			if (Wiring._GatesCurrent.Count != 0)
			{
				return;
			}
			Wiring._GatesDone.Clear();
			while (Wiring._LampsToCheck.Count > 0)
			{
				while (Wiring._LampsToCheck.Count > 0)
				{
					Point16 point = Wiring._LampsToCheck.Dequeue();
					Wiring.CheckLogicGate((int)point.X, (int)point.Y);
				}
				while (Wiring._GatesNext.Count > 0)
				{
					Utils.Swap<Queue<Point16>>(ref Wiring._GatesCurrent, ref Wiring._GatesNext);
					while (Wiring._GatesCurrent.Count > 0)
					{
						Point16 key = Wiring._GatesCurrent.Peek();
						bool value;
						if (Wiring._GatesDone.TryGetValue(key, out value) && value)
						{
							Wiring._GatesCurrent.Dequeue();
						}
						else
						{
							Wiring._GatesDone.Add(key, true);
							Wiring.TripWire((int)key.X, (int)key.Y, 1, 1);
							Wiring._GatesCurrent.Dequeue();
						}
					}
				}
			}
			Wiring._GatesDone.Clear();
			if (Wiring.blockPlayerTeleportationForOneIteration)
			{
				Wiring.blockPlayerTeleportationForOneIteration = false;
			}
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00407690 File Offset: 0x00405890
		private unsafe static void CheckLogicGate(int lampX, int lampY)
		{
			if (!WorldGen.InWorld(lampX, lampY, 1))
			{
				return;
			}
			int i = lampY;
			while (i < Main.maxTilesY)
			{
				Tile tile = Main.tile[lampX, i];
				if (!tile.active())
				{
					break;
				}
				if (*tile.type == 420)
				{
					bool value;
					Wiring._GatesDone.TryGetValue(new Point16(lampX, i), out value);
					int num = (int)(*tile.frameY / 18);
					bool flag = *tile.frameX == 18;
					bool flag2 = *tile.frameX == 36;
					if (num < 0)
					{
						break;
					}
					int num2 = 0;
					int num3 = 0;
					bool flag3 = false;
					for (int num4 = i - 1; num4 > 0; num4--)
					{
						Tile tile2 = Main.tile[lampX, num4];
						if (!tile2.active() || *tile2.type != 419)
						{
							break;
						}
						if (*tile2.frameX == 36)
						{
							flag3 = true;
							break;
						}
						num2++;
						num3 += (*tile2.frameX == 18).ToInt();
					}
					bool flag4;
					switch (num)
					{
					case 0:
						flag4 = (num2 == num3);
						break;
					case 1:
						flag4 = (num3 > 0);
						break;
					case 2:
						flag4 = (num2 != num3);
						break;
					case 3:
						flag4 = (num3 == 0);
						break;
					case 4:
						flag4 = (num3 == 1);
						break;
					case 5:
						flag4 = (num3 != 1);
						break;
					default:
						return;
					}
					bool flag5 = !flag3 && flag2;
					bool flag6 = false;
					if (flag3 && *Framing.GetTileSafely(lampX, lampY).frameX == 36)
					{
						flag6 = true;
					}
					if (flag4 == flag && !flag5 && !flag6)
					{
						break;
					}
					short num5 = *tile.frameX % 18 / 18;
					*tile.frameX = (short)(18 * flag4.ToInt());
					if (flag3)
					{
						*tile.frameX = 36;
					}
					Wiring.SkipWire(lampX, i);
					WorldGen.SquareTileFrame(lampX, i, true);
					NetMessage.SendTileSquare(-1, lampX, i, TileChangeType.None);
					bool flag7 = !flag3 || flag6;
					if (flag6)
					{
						if (num3 == 0 || num2 == 0)
						{
						}
						flag7 = (Main.rand.NextFloat() < (float)num3 / (float)num2);
					}
					if (flag5)
					{
						flag7 = false;
					}
					if (!flag7)
					{
						break;
					}
					if (!value)
					{
						Wiring._GatesNext.Enqueue(new Point16(lampX, i));
						return;
					}
					Vector2 position = new Vector2((float)lampX, (float)i) * 16f - new Vector2(10f);
					Utils.PoofOfSmoke(position);
					NetMessage.SendData(106, -1, -1, null, (int)position.X, position.Y, 0f, 0f, 0, 0, 0);
					return;
				}
				else
				{
					if (*tile.type != 419)
					{
						break;
					}
					i++;
				}
			}
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00407930 File Offset: 0x00405B30
		private unsafe static void HitWire(DoubleStack<Point16> next, int wireType)
		{
			Wiring._wireDirectionList.Clear(true);
			for (int i = 0; i < next.Count; i++)
			{
				Point16 point = next.PopFront();
				Wiring.SkipWire(point);
				Wiring._toProcess.Add(point, 4);
				next.PushBack(point);
				Wiring._wireDirectionList.PushBack(0);
			}
			Wiring._currentWireColor = wireType;
			while (next.Count > 0)
			{
				Point16 key = next.PopFront();
				int num = (int)Wiring._wireDirectionList.PopFront();
				int x = (int)key.X;
				int y = (int)key.Y;
				if (!Wiring._wireSkip.ContainsKey(key))
				{
					Wiring.HitWireSingle(x, y);
				}
				for (int j = 0; j < 4; j++)
				{
					int num2;
					int num3;
					switch (j)
					{
					case 0:
						num2 = x;
						num3 = y + 1;
						break;
					case 1:
						num2 = x;
						num3 = y - 1;
						break;
					case 2:
						num2 = x + 1;
						num3 = y;
						break;
					case 3:
						num2 = x - 1;
						num3 = y;
						break;
					default:
						num2 = x;
						num3 = y + 1;
						break;
					}
					if (num2 >= 2 && num2 < Main.maxTilesX - 2 && num3 >= 2 && num3 < Main.maxTilesY - 2)
					{
						Tile tile = Main.tile[num2, num3];
						if (!(tile == null))
						{
							Tile tile2 = Main.tile[x, y];
							if (!(tile2 == null))
							{
								byte b = 3;
								if (*tile.type == 424 || *tile.type == 445)
								{
									b = 0;
								}
								if (*tile2.type == 424)
								{
									switch (*tile2.frameX / 18)
									{
									case 0:
										if (j != num)
										{
											goto IL_321;
										}
										break;
									case 1:
										if ((num != 0 || j != 3) && (num != 3 || j != 0) && (num != 1 || j != 2))
										{
											if (num != 2)
											{
												goto IL_321;
											}
											if (j != 1)
											{
												goto IL_321;
											}
										}
										break;
									case 2:
										if ((num != 0 || j != 2) && (num != 2 || j != 0) && (num != 1 || j != 3) && (num != 3 || j != 1))
										{
											goto IL_321;
										}
										break;
									}
								}
								if (*tile2.type == 445)
								{
									if (j != num)
									{
										goto IL_321;
									}
									if (Wiring._PixelBoxTriggers.ContainsKey(key))
									{
										Dictionary<Point16, byte> pixelBoxTriggers = Wiring._PixelBoxTriggers;
										Point16 key2 = key;
										pixelBoxTriggers[key2] |= ((j != 0 && j != 1) ? 1 : 2);
									}
									else
									{
										Wiring._PixelBoxTriggers[key] = ((j != 0 && j != 1) ? 1 : 2);
									}
								}
								bool flag;
								switch (wireType)
								{
								case 1:
									flag = tile.wire();
									break;
								case 2:
									flag = tile.wire2();
									break;
								case 3:
									flag = tile.wire3();
									break;
								case 4:
									flag = tile.wire4();
									break;
								default:
									flag = false;
									break;
								}
								if (flag)
								{
									Point16 point2 = new Point16(num2, num3);
									byte value;
									if (Wiring._toProcess.TryGetValue(point2, out value))
									{
										value -= 1;
										if (value == 0)
										{
											Wiring._toProcess.Remove(point2);
										}
										else
										{
											Wiring._toProcess[point2] = value;
										}
									}
									else
									{
										next.PushBack(point2);
										Wiring._wireDirectionList.PushBack((byte)j);
										if (b > 0)
										{
											Wiring._toProcess.Add(point2, b);
										}
									}
								}
							}
						}
					}
					IL_321:;
				}
			}
			Wiring._wireSkip.Clear();
			Wiring._toProcess.Clear();
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00407C8C File Offset: 0x00405E8C
		public static IEntitySource GetProjectileSource(int sourceTileX, int sourceTileY)
		{
			return new EntitySource_Wiring(sourceTileX, sourceTileY, null);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00407C96 File Offset: 0x00405E96
		public static IEntitySource GetNPCSource(int sourceTileX, int sourceTileY)
		{
			return new EntitySource_Wiring(sourceTileX, sourceTileY, null);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00407CA0 File Offset: 0x00405EA0
		public static IEntitySource GetItemSource(int sourceTileX, int sourceTileY)
		{
			return new EntitySource_Wiring(sourceTileX, sourceTileY, null);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00407CAC File Offset: 0x00405EAC
		private unsafe static void HitWireSingle(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			bool? forcedStateWhereTrueIsOn = null;
			bool doSkipWires = true;
			int type = (int)(*tile.type);
			if (tile.actuator())
			{
				Wiring.ActuateForced(i, j);
			}
			if (!tile.active())
			{
				return;
			}
			if (!TileLoader.PreHitWire(i, j, type))
			{
				return;
			}
			if (type != 144)
			{
				if (type != 421)
				{
					if (type == 422)
					{
						if (!tile.actuator())
						{
							*tile.type = 421;
							WorldGen.SquareTileFrame(i, j, true);
							NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
						}
					}
				}
				else if (!tile.actuator())
				{
					*tile.type = 422;
					WorldGen.SquareTileFrame(i, j, true);
					NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
				}
			}
			else
			{
				Wiring.HitSwitch(i, j);
				WorldGen.SquareTileFrame(i, j, true);
				NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
			}
			if (type >= 255 && type <= 268)
			{
				if (!tile.actuator())
				{
					if (type >= 262)
					{
						ref ushort type5 = ref tile.type;
						type5 -= 7;
					}
					else
					{
						ref ushort type6 = ref tile.type;
						type6 += 7;
					}
					WorldGen.SquareTileFrame(i, j, true);
					NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
				}
				return;
			}
			if (type == 419)
			{
				int num = 18;
				if ((int)(*tile.frameX) >= num)
				{
					num = -num;
				}
				if (*tile.frameX == 36)
				{
					num = 0;
				}
				Wiring.SkipWire(i, j);
				*tile.frameX = (short)((int)(*tile.frameX) + num);
				WorldGen.SquareTileFrame(i, j, true);
				NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
				Wiring._LampsToCheck.Enqueue(new Point16(i, j));
				return;
			}
			if (type == 406)
			{
				int num2 = (int)(*tile.frameX % 54 / 18);
				int num3 = (int)(*tile.frameY % 54 / 18);
				int num4 = i - num2;
				int num5 = j - num3;
				int num6 = 54;
				if (*Main.tile[num4, num5].frameY >= 108)
				{
					num6 = -108;
				}
				for (int k = num4; k < num4 + 3; k++)
				{
					for (int l = num5; l < num5 + 3; l++)
					{
						Wiring.SkipWire(k, l);
						*Main.tile[k, l].frameY = (short)((int)(*Main.tile[k, l].frameY) + num6);
					}
				}
				NetMessage.SendTileSquare(-1, num4 + 1, num5 + 1, 3, TileChangeType.None);
				return;
			}
			if (type == 452)
			{
				int num7 = (int)(*tile.frameX % 54 / 18);
				int num8 = (int)(*tile.frameY % 54 / 18);
				int num9 = i - num7;
				int num10 = j - num8;
				int num11 = 54;
				if (*Main.tile[num9, num10].frameX >= 54)
				{
					num11 = -54;
				}
				for (int m = num9; m < num9 + 3; m++)
				{
					for (int n = num10; n < num10 + 3; n++)
					{
						Wiring.SkipWire(m, n);
						*Main.tile[m, n].frameX = (short)((int)(*Main.tile[m, n].frameX) + num11);
					}
				}
				NetMessage.SendTileSquare(-1, num9 + 1, num10 + 1, 3, TileChangeType.None);
				return;
			}
			if (type == 411)
			{
				int num12 = (int)(*tile.frameX % 36 / 18);
				int num13 = (int)(*tile.frameY % 36 / 18);
				int num14 = i - num12;
				int num15 = j - num13;
				int num16 = 36;
				if (*Main.tile[num14, num15].frameX >= 36)
				{
					num16 = -36;
				}
				for (int num17 = num14; num17 < num14 + 2; num17++)
				{
					for (int num18 = num15; num18 < num15 + 2; num18++)
					{
						Wiring.SkipWire(num17, num18);
						*Main.tile[num17, num18].frameX = (short)((int)(*Main.tile[num17, num18].frameX) + num16);
					}
				}
				NetMessage.SendTileSquare(-1, num14, num15, 2, 2, TileChangeType.None);
				return;
			}
			if (type == 356)
			{
				int num19 = (int)(*tile.frameX % 36 / 18);
				int num20 = (int)(*tile.frameY % 54 / 18);
				int num21 = i - num19;
				int num22 = j - num20;
				for (int num23 = num21; num23 < num21 + 2; num23++)
				{
					for (int num24 = num22; num24 < num22 + 3; num24++)
					{
						Wiring.SkipWire(num23, num24);
					}
				}
				if (!Main.fastForwardTimeToDawn && Main.sundialCooldown == 0)
				{
					Main.Sundialing();
				}
				NetMessage.SendTileSquare(-1, num21, num22, 2, 2, TileChangeType.None);
				return;
			}
			if (type == 663)
			{
				int num25 = (int)(*tile.frameX % 36 / 18);
				int num26 = (int)(*tile.frameY % 54 / 18);
				int num27 = i - num25;
				int num28 = j - num26;
				for (int num29 = num27; num29 < num27 + 2; num29++)
				{
					for (int num30 = num28; num30 < num28 + 3; num30++)
					{
						Wiring.SkipWire(num29, num30);
					}
				}
				if (!Main.fastForwardTimeToDusk && Main.moondialCooldown == 0)
				{
					Main.Moondialing();
				}
				NetMessage.SendTileSquare(-1, num27, num28, 2, 2, TileChangeType.None);
				return;
			}
			if (type == 425)
			{
				int num31 = (int)(*tile.frameX % 36 / 18);
				int num32 = (int)(*tile.frameY % 36 / 18);
				int num33 = i - num31;
				int num34 = j - num32;
				for (int num35 = num33; num35 < num33 + 2; num35++)
				{
					for (int num36 = num34; num36 < num34 + 2; num36++)
					{
						Wiring.SkipWire(num35, num36);
					}
				}
				if (Main.AnnouncementBoxDisabled)
				{
					return;
				}
				Color pink = Color.Pink;
				int num37 = Sign.ReadSign(num33, num34, false);
				if (num37 == -1 || Main.sign[num37] == null || string.IsNullOrWhiteSpace(Main.sign[num37].text))
				{
					return;
				}
				if (Main.AnnouncementBoxRange == -1)
				{
					if (Main.netMode == 0)
					{
						Main.NewTextMultiline(Main.sign[num37].text, false, pink, 460);
						return;
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(107, -1, -1, NetworkText.FromLiteral(Main.sign[num37].text), 255, (float)pink.R, (float)pink.G, (float)pink.B, 460, 0, 0);
						return;
					}
				}
				else if (Main.netMode == 0)
				{
					if (Main.player[Main.myPlayer].Distance(new Vector2((float)(num33 * 16 + 16), (float)(num34 * 16 + 16))) <= (float)Main.AnnouncementBoxRange)
					{
						Main.NewTextMultiline(Main.sign[num37].text, false, pink, 460);
						return;
					}
				}
				else
				{
					if (Main.netMode != 2)
					{
						return;
					}
					for (int num38 = 0; num38 < 255; num38++)
					{
						if (Main.player[num38].active && Main.player[num38].Distance(new Vector2((float)(num33 * 16 + 16), (float)(num34 * 16 + 16))) <= (float)Main.AnnouncementBoxRange)
						{
							NetMessage.SendData(107, num38, -1, NetworkText.FromLiteral(Main.sign[num37].text), 255, (float)pink.R, (float)pink.G, (float)pink.B, 460, 0, 0);
						}
					}
				}
				return;
			}
			else
			{
				if (type == 405)
				{
					Wiring.ToggleFirePlace(i, j, tile, forcedStateWhereTrueIsOn, doSkipWires);
					return;
				}
				if (type == 209)
				{
					int num39 = (int)(*tile.frameX % 72 / 18);
					int num40 = (int)(*tile.frameY % 54 / 18);
					int num41 = i - num39;
					int num42 = j - num40;
					int num43 = (int)(*tile.frameY / 54);
					int num44 = (int)(*tile.frameX / 72);
					int num45 = -1;
					if (num39 == 1 || num39 == 2)
					{
						num45 = num40;
					}
					int num46 = 0;
					if (num39 == 3)
					{
						num46 = -54;
					}
					if (num39 == 0)
					{
						num46 = 54;
					}
					if (num43 >= 8 && num46 > 0)
					{
						num46 = 0;
					}
					if (num43 == 0 && num46 < 0)
					{
						num46 = 0;
					}
					bool flag = false;
					if (num46 != 0)
					{
						for (int num47 = num41; num47 < num41 + 4; num47++)
						{
							for (int num48 = num42; num48 < num42 + 3; num48++)
							{
								Wiring.SkipWire(num47, num48);
								*Main.tile[num47, num48].frameY = (short)((int)(*Main.tile[num47, num48].frameY) + num46);
							}
						}
						flag = true;
					}
					if ((num44 == 3 || num44 == 4) && (num45 == 0 || num45 == 1))
					{
						num46 = ((num44 == 3) ? 72 : -72);
						for (int num49 = num41; num49 < num41 + 4; num49++)
						{
							for (int num50 = num42; num50 < num42 + 3; num50++)
							{
								Wiring.SkipWire(num49, num50);
								*Main.tile[num49, num50].frameX = (short)((int)(*Main.tile[num49, num50].frameX) + num46);
							}
						}
						flag = true;
					}
					if (flag)
					{
						NetMessage.SendTileSquare(-1, num41, num42, 4, 3, TileChangeType.None);
					}
					if (num45 != -1)
					{
						bool flag2 = true;
						if ((num44 == 3 || num44 == 4) && num45 < 2)
						{
							flag2 = false;
						}
						if (Wiring.CheckMech(num41, num42, 30) && flag2)
						{
							WorldGen.ShootFromCannon(num41, num42, num43, num44 + 1, 0, 0f, Wiring.CurrentUser, true);
						}
					}
					return;
				}
				if (type == 212)
				{
					int num51 = (int)(*tile.frameX % 54 / 18);
					int num52 = (int)(*tile.frameY % 54 / 18);
					int num53 = i - num51;
					int num54 = j - num52;
					short num154 = *tile.frameX / 54;
					int num55 = -1;
					if (num51 == 1)
					{
						num55 = num52;
					}
					int num56 = 0;
					if (num51 == 0)
					{
						num56 = -54;
					}
					if (num51 == 2)
					{
						num56 = 54;
					}
					if (num154 >= 1 && num56 > 0)
					{
						num56 = 0;
					}
					if (num154 == 0 && num56 < 0)
					{
						num56 = 0;
					}
					bool flag3 = false;
					if (num56 != 0)
					{
						for (int num57 = num53; num57 < num53 + 3; num57++)
						{
							for (int num58 = num54; num58 < num54 + 3; num58++)
							{
								Wiring.SkipWire(num57, num58);
								*Main.tile[num57, num58].frameX = (short)((int)(*Main.tile[num57, num58].frameX) + num56);
							}
						}
						flag3 = true;
					}
					if (flag3)
					{
						NetMessage.SendTileSquare(-1, num53, num54, 3, 3, TileChangeType.None);
					}
					if (num55 != -1 && Wiring.CheckMech(num53, num54, 10))
					{
						float num155 = 12f + (float)Main.rand.Next(450) * 0.01f;
						float num59 = (float)Main.rand.Next(85, 105);
						float num156 = (float)Main.rand.Next(-35, 11);
						int type2 = 166;
						int damage = 0;
						float knockBack = 0f;
						Vector2 vector;
						vector..ctor((float)((num53 + 2) * 16 - 8), (float)((num54 + 2) * 16 - 8));
						if (*tile.frameX / 54 == 0)
						{
							num59 *= -1f;
							vector.X -= 12f;
						}
						else
						{
							vector.X += 12f;
						}
						float num60 = num59;
						float num61 = num156;
						float num62 = (float)Math.Sqrt((double)(num60 * num60 + num61 * num61));
						num62 = num155 / num62;
						num60 *= num62;
						num61 *= num62;
						Projectile.NewProjectile(Wiring.GetProjectileSource(num53, num54), vector.X, vector.Y, num60, num61, type2, damage, knockBack, Wiring.CurrentUser, 0f, 0f, 0f);
					}
					return;
				}
				if (type == 215)
				{
					Wiring.ToggleCampFire(i, j, tile, forcedStateWhereTrueIsOn, doSkipWires);
					return;
				}
				if (type == 130)
				{
					if (Main.tile[i, j - 1] == null || !Main.tile[i, j - 1].active() || (!TileID.Sets.BasicChest[(int)(*Main.tile[i, j - 1].type)] && !TileID.Sets.BasicChestFake[(int)(*Main.tile[i, j - 1].type)] && *Main.tile[i, j - 1].type != 88))
					{
						*tile.type = 131;
						WorldGen.SquareTileFrame(i, j, true);
						NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
					}
					return;
				}
				if (type == 131)
				{
					*tile.type = 130;
					WorldGen.SquareTileFrame(i, j, true);
					NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
					return;
				}
				if (type == 387 || type == 386)
				{
					bool value = type == 387;
					int num63 = WorldGen.ShiftTrapdoor(i, j, true, -1).ToInt();
					if (num63 == 0)
					{
						num63 = -WorldGen.ShiftTrapdoor(i, j, false, -1).ToInt();
					}
					if (num63 != 0)
					{
						NetMessage.SendData(19, -1, -1, null, 3 - value.ToInt(), (float)i, (float)j, (float)num63, 0, 0, 0);
					}
					return;
				}
				if (type == 389 || type == 388)
				{
					bool flag4 = type == 389;
					WorldGen.ShiftTallGate(i, j, flag4, false);
					NetMessage.SendData(19, -1, -1, null, 4 + flag4.ToInt(), (float)i, (float)j, 0f, 0, 0, 0);
					return;
				}
				if (TileLoader.CloseDoorID(Main.tile[i, j]) >= 0)
				{
					if (WorldGen.CloseDoor(i, j, true))
					{
						NetMessage.SendData(19, -1, -1, null, 1, (float)i, (float)j, 0f, 0, 0, 0);
					}
					return;
				}
				if (TileLoader.OpenDoorID(Main.tile[i, j]) >= 0)
				{
					int num64 = 1;
					if (Main.rand.Next(2) == 0)
					{
						num64 = -1;
					}
					if (!WorldGen.OpenDoor(i, j, num64))
					{
						if (WorldGen.OpenDoor(i, j, -num64))
						{
							NetMessage.SendData(19, -1, -1, null, 0, (float)i, (float)j, (float)(-(float)num64), 0, 0, 0);
							return;
						}
					}
					else
					{
						NetMessage.SendData(19, -1, -1, null, 0, (float)i, (float)j, (float)num64, 0, 0, 0);
					}
					return;
				}
				if (type == 216)
				{
					WorldGen.LaunchRocket(i, j, true);
					Wiring.SkipWire(i, j);
					return;
				}
				if (type == 497 || (type == 15 && *tile.frameY / 40 == 1) || (type == 15 && *tile.frameY / 40 == 20))
				{
					int num65 = j - (int)(*tile.frameY % 40 / 18);
					Wiring.SkipWire(i, num65);
					Wiring.SkipWire(i, num65 + 1);
					if (Wiring.CheckMech(i, num65, 60))
					{
						Projectile.NewProjectile(Wiring.GetProjectileSource(i, num65), (float)(i * 16 + 8), (float)(num65 * 16 + 12), 0f, 0f, 733, 0, 0f, Main.myPlayer, 0f, 0f, 0f);
					}
					return;
				}
				int num157 = type;
				if (num157 != 235)
				{
					if (num157 != 335)
					{
						if (num157 != 338)
						{
							if (TileID.Sets.Torch[type])
							{
								Wiring.ToggleTorch(i, j, tile, forcedStateWhereTrueIsOn);
							}
							else
							{
								if (num157 <= 174)
								{
									if (num157 <= 100)
									{
										if (num157 <= 42)
										{
											switch (num157)
											{
											case 33:
												goto IL_17C5;
											case 34:
												Wiring.ToggleChandelier(i, j, tile, forcedStateWhereTrueIsOn, doSkipWires);
												goto IL_3686;
											case 35:
												goto IL_206A;
											default:
												if (num157 != 42)
												{
													goto IL_205F;
												}
												Wiring.ToggleHangingLantern(i, j, tile, forcedStateWhereTrueIsOn, doSkipWires);
												goto IL_3686;
											}
										}
										else
										{
											if (num157 == 49)
											{
												goto IL_17C5;
											}
											switch (num157)
											{
											case 92:
												Wiring.ToggleLampPost(i, j, tile, forcedStateWhereTrueIsOn, doSkipWires);
												goto IL_3686;
											case 93:
												Wiring.ToggleLamp(i, j, tile, forcedStateWhereTrueIsOn, doSkipWires);
												goto IL_3686;
											case 94:
												goto IL_205F;
											case 95:
												break;
											default:
												if (num157 != 100)
												{
													goto IL_205F;
												}
												break;
											}
										}
									}
									else if (num157 <= 139)
									{
										if (num157 != 126)
										{
											if (num157 != 137)
											{
												if (num157 != 139)
												{
													goto IL_205F;
												}
												goto IL_206A;
											}
											else
											{
												int num66 = (int)(*tile.frameY / 18);
												Vector2 vector2 = Vector2.Zero;
												float speedX = 0f;
												float speedY = 0f;
												int num67 = 0;
												int damage2 = 0;
												switch (num66)
												{
												case 0:
												case 1:
												case 2:
												case 5:
													if (Wiring.CheckMech(i, j, 200))
													{
														int num68 = (*tile.frameX == 0) ? -1 : ((*tile.frameX == 18) ? 1 : 0);
														int num69 = (*tile.frameX >= 36) ? ((*tile.frameX >= 72) ? 1 : -1) : 0;
														vector2..ctor((float)(i * 16 + 8 + 10 * num68), (float)(j * 16 + 8 + 10 * num69));
														float num70 = 3f;
														if (num66 == 0)
														{
															num67 = 98;
															damage2 = 20;
															num70 = 12f;
														}
														if (num66 == 1)
														{
															num67 = 184;
															damage2 = 40;
															num70 = 12f;
														}
														if (num66 == 2)
														{
															num67 = 187;
															damage2 = 40;
															num70 = 5f;
														}
														if (num66 == 5)
														{
															num67 = 980;
															damage2 = 30;
															num70 = 12f;
														}
														speedX = (float)num68 * num70;
														speedY = (float)num69 * num70;
													}
													break;
												case 3:
													if (Wiring.CheckMech(i, j, 300))
													{
														int num71 = 200;
														for (int num72 = 0; num72 < 1000; num72++)
														{
															if (Main.projectile[num72].active && Main.projectile[num72].type == num67)
															{
																float num73 = (new Vector2((float)(i * 16 + 8), (float)(j * 18 + 8)) - Main.projectile[num72].Center).Length();
																num71 = ((num73 >= 50f) ? ((num73 >= 100f) ? ((num73 >= 200f) ? ((num73 >= 300f) ? ((num73 >= 400f) ? ((num73 >= 500f) ? ((num73 >= 700f) ? ((num73 >= 900f) ? ((num73 >= 1200f) ? (num71 - 1) : (num71 - 2)) : (num71 - 3)) : (num71 - 4)) : (num71 - 5)) : (num71 - 6)) : (num71 - 8)) : (num71 - 10)) : (num71 - 15)) : (num71 - 50));
															}
														}
														if (num71 > 0)
														{
															num67 = 185;
															damage2 = 40;
															int num74 = 0;
															int num75 = 0;
															switch (*tile.frameX / 18)
															{
															case 0:
															case 1:
																num74 = 0;
																num75 = 1;
																break;
															case 2:
																num74 = 0;
																num75 = -1;
																break;
															case 3:
																num74 = -1;
																num75 = 0;
																break;
															case 4:
																num74 = 1;
																num75 = 0;
																break;
															}
															speedX = (float)(4 * num74) + (float)Main.rand.Next(-20 + ((num74 == 1) ? 20 : 0), 21 - ((num74 == -1) ? 20 : 0)) * 0.05f;
															speedY = (float)(4 * num75) + (float)Main.rand.Next(-20 + ((num75 == 1) ? 20 : 0), 21 - ((num75 == -1) ? 20 : 0)) * 0.05f;
															vector2..ctor((float)(i * 16 + 8 + 14 * num74), (float)(j * 16 + 8 + 14 * num75));
														}
													}
													break;
												case 4:
													if (Wiring.CheckMech(i, j, 90))
													{
														int num76 = 0;
														int num77 = 0;
														switch (*tile.frameX / 18)
														{
														case 0:
														case 1:
															num76 = 0;
															num77 = 1;
															break;
														case 2:
															num76 = 0;
															num77 = -1;
															break;
														case 3:
															num76 = -1;
															num77 = 0;
															break;
														case 4:
															num76 = 1;
															num77 = 0;
															break;
														}
														speedX = (float)(8 * num76);
														speedY = (float)(8 * num77);
														damage2 = 60;
														num67 = 186;
														vector2..ctor((float)(i * 16 + 8 + 18 * num76), (float)(j * 16 + 8 + 18 * num77));
													}
													break;
												}
												switch (num66)
												{
												case -10:
													if (Wiring.CheckMech(i, j, 200))
													{
														int num78 = -1;
														if (*tile.frameX != 0)
														{
															num78 = 1;
														}
														speedX = (float)(12 * num78);
														damage2 = 20;
														num67 = 98;
														vector2..ctor((float)(i * 16 + 8), (float)(j * 16 + 7));
														vector2.X += (float)(10 * num78);
														vector2.Y += 2f;
													}
													break;
												case -9:
													if (Wiring.CheckMech(i, j, 200))
													{
														int num79 = -1;
														if (*tile.frameX != 0)
														{
															num79 = 1;
														}
														speedX = (float)(12 * num79);
														damage2 = 40;
														num67 = 184;
														vector2..ctor((float)(i * 16 + 8), (float)(j * 16 + 7));
														vector2.X += (float)(10 * num79);
														vector2.Y += 2f;
													}
													break;
												case -8:
													if (Wiring.CheckMech(i, j, 200))
													{
														int num80 = -1;
														if (*tile.frameX != 0)
														{
															num80 = 1;
														}
														speedX = (float)(5 * num80);
														damage2 = 40;
														num67 = 187;
														vector2..ctor((float)(i * 16 + 8), (float)(j * 16 + 7));
														vector2.X += (float)(10 * num80);
														vector2.Y += 2f;
													}
													break;
												case -7:
													if (Wiring.CheckMech(i, j, 300))
													{
														num67 = 185;
														int num81 = 200;
														for (int num82 = 0; num82 < 1000; num82++)
														{
															if (Main.projectile[num82].active && Main.projectile[num82].type == num67)
															{
																float num83 = (new Vector2((float)(i * 16 + 8), (float)(j * 18 + 8)) - Main.projectile[num82].Center).Length();
																num81 = ((num83 >= 50f) ? ((num83 >= 100f) ? ((num83 >= 200f) ? ((num83 >= 300f) ? ((num83 >= 400f) ? ((num83 >= 500f) ? ((num83 >= 700f) ? ((num83 >= 900f) ? ((num83 >= 1200f) ? (num81 - 1) : (num81 - 2)) : (num81 - 3)) : (num81 - 4)) : (num81 - 5)) : (num81 - 6)) : (num81 - 8)) : (num81 - 10)) : (num81 - 15)) : (num81 - 50));
															}
														}
														if (num81 > 0)
														{
															speedX = (float)Main.rand.Next(-20, 21) * 0.05f;
															speedY = 4f + (float)Main.rand.Next(0, 21) * 0.05f;
															damage2 = 40;
															vector2..ctor((float)(i * 16 + 8), (float)(j * 16 + 16));
															vector2.Y += 6f;
															Projectile.NewProjectile(Wiring.GetProjectileSource(i, j), (float)((int)vector2.X), (float)((int)vector2.Y), speedX, speedY, num67, damage2, 2f, Main.myPlayer, 0f, 0f, 0f);
														}
													}
													break;
												case -6:
													if (Wiring.CheckMech(i, j, 90))
													{
														speedX = 0f;
														speedY = 8f;
														damage2 = 60;
														num67 = 186;
														vector2..ctor((float)(i * 16 + 8), (float)(j * 16 + 16));
														vector2.Y += 10f;
													}
													break;
												}
												if (num67 != 0)
												{
													Projectile.NewProjectile(Wiring.GetProjectileSource(i, j), (float)((int)vector2.X), (float)((int)vector2.Y), speedX, speedY, num67, damage2, 2f, Main.myPlayer, 0f, 0f, 0f);
													goto IL_3686;
												}
												goto IL_3686;
											}
										}
									}
									else
									{
										if (num157 == 149)
										{
											Wiring.ToggleHolidayLight(i, j, tile, forcedStateWhereTrueIsOn);
											goto IL_3686;
										}
										if (num157 != 173)
										{
											if (num157 != 174)
											{
												goto IL_205F;
											}
											goto IL_17C5;
										}
									}
								}
								else if (num157 <= 443)
								{
									if (num157 <= 314)
									{
										if (num157 == 244)
										{
											int num84;
											for (num84 = (int)(*tile.frameX / 18); num84 >= 3; num84 -= 3)
											{
											}
											int num85;
											for (num85 = (int)(*tile.frameY / 18); num85 >= 3; num85 -= 3)
											{
											}
											int num86 = i - num84;
											int num87 = j - num85;
											int num88 = 54;
											if (*Main.tile[num86, num87].frameX >= 54)
											{
												num88 = -54;
											}
											for (int num89 = num86; num89 < num86 + 3; num89++)
											{
												for (int num90 = num87; num90 < num87 + 2; num90++)
												{
													Wiring.SkipWire(num89, num90);
													*Main.tile[num89, num90].frameX = (short)((int)(*Main.tile[num89, num90].frameX) + num88);
												}
											}
											NetMessage.SendTileSquare(-1, num86, num87, 3, 2, TileChangeType.None);
											goto IL_3686;
										}
										if (num157 != 314)
										{
											goto IL_205F;
										}
										if (Wiring.CheckMech(i, j, 5))
										{
											Minecart.FlipSwitchTrack(i, j);
											goto IL_3686;
										}
										goto IL_3686;
									}
									else
									{
										if (num157 == 372)
										{
											goto IL_17C5;
										}
										if (num157 == 429)
										{
											short num158 = *Main.tile[i, j].frameX / 18;
											bool flag5 = num158 % 2 >= 1;
											bool flag6 = num158 % 4 >= 2;
											bool flag7 = num158 % 8 >= 4;
											bool flag8 = num158 % 16 >= 8;
											bool flag9 = false;
											short num91 = 0;
											switch (Wiring._currentWireColor)
											{
											case 1:
												num91 = 18;
												flag9 = !flag5;
												break;
											case 2:
												num91 = 72;
												flag9 = !flag7;
												break;
											case 3:
												num91 = 36;
												flag9 = !flag6;
												break;
											case 4:
												num91 = 144;
												flag9 = !flag8;
												break;
											}
											if (flag9)
											{
												ref short frameX = ref tile.frameX;
												frameX += num91;
											}
											else
											{
												ref short frameX2 = ref tile.frameX;
												frameX2 -= num91;
											}
											NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
											goto IL_3686;
										}
										if (num157 != 443)
										{
											goto IL_205F;
										}
										Wiring.GeyserTrap(i, j);
										goto IL_3686;
									}
								}
								else if (num157 <= 565)
								{
									if (num157 != 531)
									{
										if (num157 != 564)
										{
											if (num157 != 565)
											{
												goto IL_205F;
											}
											int num92;
											for (num92 = (int)(*tile.frameX / 18); num92 >= 2; num92 -= 2)
											{
											}
											int num93;
											for (num93 = (int)(*tile.frameY / 18); num93 >= 2; num93 -= 2)
											{
											}
											int num94 = i - num92;
											int num95 = j - num93;
											int num96 = 36;
											if (*Main.tile[num94, num95].frameX >= 36)
											{
												num96 = -36;
											}
											for (int num97 = num94; num97 < num94 + 2; num97++)
											{
												for (int num98 = num95; num98 < num95 + 2; num98++)
												{
													Wiring.SkipWire(num97, num98);
													*Main.tile[num97, num98].frameX = (short)((int)(*Main.tile[num97, num98].frameX) + num96);
												}
											}
											NetMessage.SendTileSquare(-1, num94, num95, 2, 2, TileChangeType.None);
											goto IL_3686;
										}
									}
									else
									{
										int num99 = (int)(*tile.frameX / 36);
										int num100 = (int)(*tile.frameY / 54);
										int num101 = i - ((int)(*tile.frameX) - num99 * 36) / 18;
										int num102 = j - ((int)(*tile.frameY) - num100 * 54) / 18;
										if (!Wiring.CheckMech(num101, num102, 900))
										{
											goto IL_3686;
										}
										Vector2 vector3 = new Vector2((float)(num101 + 1), (float)num102) * 16f;
										vector3.Y += 28f;
										int num103 = 99;
										int damage3 = 70;
										float knockBack2 = 10f;
										if (num103 != 0)
										{
											Projectile.NewProjectile(Wiring.GetProjectileSource(num101, num102), (float)((int)vector3.X), (float)((int)vector3.Y), 0f, 0f, num103, damage3, knockBack2, Main.myPlayer, 0f, 0f, 0f);
											goto IL_3686;
										}
										goto IL_3686;
									}
								}
								else
								{
									if (num157 == 593)
									{
										Wiring.SkipWire(i, j);
										short num104 = (*Main.tile[i, j].frameX != 0) ? -18 : 18;
										ref short frameX3 = ref Main.tile[i, j].frameX;
										frameX3 += num104;
										if (Main.netMode == 2)
										{
											NetMessage.SendTileSquare(-1, i, j, 1, 1, TileChangeType.None);
										}
										int num105 = (num104 > 0) ? 4 : 3;
										Animation.NewTemporaryAnimation(num105, 593, i, j);
										NetMessage.SendTemporaryAnimation(-1, num105, 593, i, j);
										goto IL_3686;
									}
									if (num157 == 594)
									{
										int num106;
										for (num106 = (int)(*tile.frameY / 18); num106 >= 2; num106 -= 2)
										{
										}
										num106 = j - num106;
										int num107 = (int)(*tile.frameX / 18);
										if (num107 > 1)
										{
											num107 -= 2;
										}
										num107 = i - num107;
										Wiring.SkipWire(num107, num106);
										Wiring.SkipWire(num107, num106 + 1);
										Wiring.SkipWire(num107 + 1, num106);
										Wiring.SkipWire(num107 + 1, num106 + 1);
										short num108 = (*Main.tile[num107, num106].frameX != 0) ? -36 : 36;
										for (int num109 = 0; num109 < 2; num109++)
										{
											for (int num110 = 0; num110 < 2; num110++)
											{
												ref short frameX4 = ref Main.tile[num107 + num109, num106 + num110].frameX;
												frameX4 += num108;
											}
										}
										if (Main.netMode == 2)
										{
											NetMessage.SendTileSquare(-1, num107, num106, 2, 2, TileChangeType.None);
										}
										int num111 = (num108 > 0) ? 4 : 3;
										Animation.NewTemporaryAnimation(num111, 594, num107, num106);
										NetMessage.SendTemporaryAnimation(-1, num111, 594, num107, num106);
										goto IL_3686;
									}
									if (num157 != 646)
									{
										goto IL_205F;
									}
									goto IL_17C5;
								}
								Wiring.Toggle2x2Light(i, j, tile, forcedStateWhereTrueIsOn, doSkipWires);
								goto IL_3686;
								IL_17C5:
								Wiring.ToggleCandle(i, j, tile, forcedStateWhereTrueIsOn);
								goto IL_3686;
								IL_205F:
								if (!TileLoader.IsModMusicBox(tile))
								{
									if (num157 <= 410)
									{
										if (num157 <= 143)
										{
											if (num157 != 105)
											{
												if (num157 == 141)
												{
													WorldGen.KillTile(i, j, false, false, true);
													NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
													Projectile.NewProjectile(Wiring.GetProjectileSource(i, j), (float)(i * 16 + 8), (float)(j * 16 + 8), 0f, 0f, 108, 500, 10f, Main.myPlayer, 0f, 0f, 0f);
													goto IL_3686;
												}
												if (num157 - 142 > 1)
												{
													goto IL_3686;
												}
												int num112 = j - (int)(*tile.frameY / 18);
												int num113 = (int)(*tile.frameX / 18);
												if (num113 > 1)
												{
													num113 -= 2;
												}
												num113 = i - num113;
												Wiring.SkipWire(num113, num112);
												Wiring.SkipWire(num113, num112 + 1);
												Wiring.SkipWire(num113 + 1, num112);
												Wiring.SkipWire(num113 + 1, num112 + 1);
												if (type == 142)
												{
													for (int num114 = 0; num114 < 4; num114++)
													{
														if (Wiring._numInPump >= 19)
														{
															break;
														}
														int num115;
														int num116;
														switch (num114)
														{
														case 0:
															num115 = num113;
															num116 = num112 + 1;
															break;
														case 1:
															num115 = num113 + 1;
															num116 = num112 + 1;
															break;
														case 2:
															num115 = num113;
															num116 = num112;
															break;
														default:
															num115 = num113 + 1;
															num116 = num112;
															break;
														}
														Wiring._inPumpX[Wiring._numInPump] = num115;
														Wiring._inPumpY[Wiring._numInPump] = num116;
														Wiring._numInPump++;
													}
													goto IL_3686;
												}
												for (int num117 = 0; num117 < 4; num117++)
												{
													if (Wiring._numOutPump >= 19)
													{
														break;
													}
													int num118;
													int num119;
													switch (num117)
													{
													case 0:
														num118 = num113;
														num119 = num112 + 1;
														break;
													case 1:
														num118 = num113 + 1;
														num119 = num112 + 1;
														break;
													case 2:
														num118 = num113;
														num119 = num112;
														break;
													default:
														num118 = num113 + 1;
														num119 = num112;
														break;
													}
													Wiring._outPumpX[Wiring._numOutPump] = num118;
													Wiring._outPumpY[Wiring._numOutPump] = num119;
													Wiring._numOutPump++;
												}
												goto IL_3686;
											}
											else
											{
												int num120 = j - (int)(*tile.frameY / 18);
												int num121 = (int)(*tile.frameX / 18);
												int num122 = 0;
												while (num121 >= 2)
												{
													num121 -= 2;
													num122++;
												}
												num121 = i - num121;
												num121 = i - (int)(*tile.frameX % 36 / 18);
												num120 = j - (int)(*tile.frameY % 54 / 18);
												int num123 = (int)(*tile.frameY / 54);
												num123 %= 3;
												num122 = (int)(*tile.frameX / 36) + num123 * 55;
												Wiring.SkipWire(num121, num120);
												Wiring.SkipWire(num121, num120 + 1);
												Wiring.SkipWire(num121, num120 + 2);
												Wiring.SkipWire(num121 + 1, num120);
												Wiring.SkipWire(num121 + 1, num120 + 1);
												Wiring.SkipWire(num121 + 1, num120 + 2);
												int num124 = num121 * 16 + 16;
												int num125 = (num120 + 3) * 16;
												int num126 = -1;
												int num127 = -1;
												bool flag10 = true;
												bool flag11 = false;
												if (num122 != 5)
												{
													if (num122 != 13)
													{
														switch (num122)
														{
														case 30:
															num127 = 6;
															break;
														case 35:
															num127 = 2;
															break;
														case 51:
															num127 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
															{
																299,
																538
															});
															break;
														case 52:
															num127 = 356;
															break;
														case 53:
															num127 = 357;
															break;
														case 54:
															num127 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
															{
																355,
																358
															});
															break;
														case 55:
															num127 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
															{
																367,
																366
															});
															break;
														case 56:
															num127 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
															{
																359,
																359,
																359,
																359,
																360
															});
															break;
														case 57:
															num127 = 377;
															break;
														case 58:
															num127 = 300;
															break;
														case 59:
															num127 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
															{
																364,
																362
															});
															break;
														case 60:
															num127 = 148;
															break;
														case 61:
															num127 = 361;
															break;
														case 62:
															num127 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
															{
																487,
																486,
																485
															});
															break;
														case 63:
															num127 = 164;
															flag10 &= NPC.MechSpawn((float)num124, (float)num125, 165);
															break;
														case 64:
															num127 = 86;
															flag11 = true;
															break;
														case 65:
															num127 = 490;
															break;
														case 66:
															num127 = 82;
															break;
														case 67:
															num127 = 449;
															break;
														case 68:
															num127 = 167;
															break;
														case 69:
															num127 = 480;
															break;
														case 70:
															num127 = 48;
															break;
														case 71:
															num127 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
															{
																170,
																180,
																171
															});
															flag11 = true;
															break;
														case 72:
															num127 = 481;
															break;
														case 73:
															num127 = 482;
															break;
														case 74:
															num127 = 430;
															break;
														case 75:
															num127 = 489;
															break;
														case 76:
															num127 = 611;
															break;
														case 77:
															num127 = 602;
															break;
														case 78:
															num127 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
															{
																595,
																596,
																599,
																597,
																600,
																598
															});
															break;
														case 79:
															num127 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
															{
																616,
																617
															});
															break;
														case 80:
															num127 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
															{
																671,
																672
															});
															break;
														case 81:
															num127 = 673;
															break;
														case 82:
															num127 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
															{
																674,
																675
															});
															break;
														}
													}
													else
													{
														num127 = 24;
													}
												}
												else
												{
													num127 = 73;
												}
												if (num127 != -1 && Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, num127) && flag10)
												{
													if (!flag11 || !Collision.SolidTiles(num121 - 2, num121 + 3, num120, num120 + 2))
													{
														num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124, num125, num127, 0, 0f, 0f, 0f, 0f, 255);
													}
													else
													{
														Vector2 position = new Vector2((float)(num124 - 4), (float)(num125 - 22)) - new Vector2(10f);
														Utils.PoofOfSmoke(position);
														NetMessage.SendData(106, -1, -1, null, (int)position.X, position.Y, 0f, 0f, 0, 0, 0);
													}
												}
												if (num126 <= -1)
												{
													if (num122 <= 27)
													{
														switch (num122)
														{
														case 2:
															if (Wiring.CheckMech(num121, num120, 600) && Item.MechSpawn((float)num124, (float)num125, 184) && Item.MechSpawn((float)num124, (float)num125, 1735) && Item.MechSpawn((float)num124, (float)num125, 1868))
															{
																Item.NewItem(Wiring.GetItemSource(num124, num125), num124, num125 - 16, 0, 0, 184, 1, false, 0, false, false);
															}
															break;
														case 3:
														case 5:
														case 6:
														case 11:
														case 12:
														case 13:
														case 14:
														case 15:
															break;
														case 4:
															if (Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, 1))
															{
																num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124, num125 - 12, 1, 0, 0f, 0f, 0f, 0f, 255);
															}
															break;
														case 7:
															if (Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, 49))
															{
																num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124 - 4, num125 - 6, 49, 0, 0f, 0f, 0f, 0f, 255);
															}
															break;
														case 8:
															if (Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, 55))
															{
																num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124, num125 - 12, 55, 0, 0f, 0f, 0f, 0f, 255);
															}
															break;
														case 9:
														{
															int type3 = 46;
															if (BirthdayParty.PartyIsUp)
															{
																type3 = 540;
															}
															if (Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, type3))
															{
																num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124, num125 - 12, type3, 0, 0f, 0f, 0f, 0f, 255);
															}
															break;
														}
														case 10:
															if (Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, 21))
															{
																num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124, num125, 21, 0, 0f, 0f, 0f, 0f, 255);
															}
															break;
														case 16:
															if (Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, 42))
															{
																if (!Collision.SolidTiles(num121 - 1, num121 + 1, num120, num120 + 1))
																{
																	num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124, num125 - 12, 42, 0, 0f, 0f, 0f, 0f, 255);
																}
																else
																{
																	Vector2 position2 = new Vector2((float)(num124 - 4), (float)(num125 - 22)) - new Vector2(10f);
																	Utils.PoofOfSmoke(position2);
																	NetMessage.SendData(106, -1, -1, null, (int)position2.X, position2.Y, 0f, 0f, 0, 0, 0);
																}
															}
															break;
														case 17:
															if (Wiring.CheckMech(num121, num120, 600) && Item.MechSpawn((float)num124, (float)num125, 166))
															{
																Item.NewItem(Wiring.GetItemSource(num124, num125), num124, num125 - 20, 0, 0, 166, 1, false, 0, false, false);
															}
															break;
														case 18:
															if (Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, 67))
															{
																num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124, num125 - 12, 67, 0, 0f, 0f, 0f, 0f, 255);
															}
															break;
														default:
															if (num122 != 23)
															{
																if (num122 == 27)
																{
																	if (Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, 85))
																	{
																		num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124 - 9, num125, 85, 0, 0f, 0f, 0f, 0f, 255);
																	}
																}
															}
															else if (Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, 63))
															{
																num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124, num125 - 12, 63, 0, 0f, 0f, 0f, 0f, 255);
															}
															break;
														}
													}
													else if (num122 != 28)
													{
														switch (num122)
														{
														case 34:
															for (int num128 = 0; num128 < 2; num128++)
															{
																for (int num129 = 0; num129 < 3; num129++)
																{
																	Tile tile2 = Main.tile[num121 + num128, num120 + num129];
																	*tile2.type = 349;
																	*tile2.frameX = (short)(num128 * 18 + 216);
																	*tile2.frameY = (short)(num129 * 18);
																}
															}
															Animation.NewTemporaryAnimation(0, 349, num121, num120);
															if (Main.netMode == 2)
															{
																NetMessage.SendTileSquare(-1, num121, num120, 2, 3, TileChangeType.None);
															}
															break;
														case 35:
														case 36:
														case 38:
														case 39:
															break;
														case 37:
															if (Wiring.CheckMech(num121, num120, 600) && Item.MechSpawn((float)num124, (float)num125, 58) && Item.MechSpawn((float)num124, (float)num125, 1734) && Item.MechSpawn((float)num124, (float)num125, 1867))
															{
																Item.NewItem(Wiring.GetItemSource(num124, num125), num124, num125 - 16, 0, 0, 58, 1, false, 0, false, false);
															}
															break;
														case 40:
															if (Wiring.CheckMech(num121, num120, 300))
															{
																List<int> array2 = new List<int>(50);
																int num130 = 0;
																for (int num131 = 0; num131 < 200; num131++)
																{
																	bool vanillaCanGo = false;
																	if (Main.npc[num131].active && (Main.npc[num131].type == 17 || Main.npc[num131].type == 19 || Main.npc[num131].type == 22 || Main.npc[num131].type == 38 || Main.npc[num131].type == 54 || Main.npc[num131].type == 107 || Main.npc[num131].type == 108 || Main.npc[num131].type == 142 || Main.npc[num131].type == 160 || Main.npc[num131].type == 207 || Main.npc[num131].type == 209 || Main.npc[num131].type == 227 || Main.npc[num131].type == 228 || Main.npc[num131].type == 229 || Main.npc[num131].type == 368 || Main.npc[num131].type == 369 || Main.npc[num131].type == 550 || Main.npc[num131].type == 441 || Main.npc[num131].type == 588))
																	{
																		vanillaCanGo = true;
																	}
																	if (Main.npc[num131].active && NPCLoader.CanGoToStatue(Main.npc[num131], true).GetValueOrDefault(vanillaCanGo))
																	{
																		array2.Add(num131);
																		num130++;
																	}
																}
																if (num130 > 0)
																{
																	int num132 = array2[Main.rand.Next(num130)];
																	Main.npc[num132].position.X = (float)(num124 - Main.npc[num132].width / 2);
																	Main.npc[num132].position.Y = (float)(num125 - Main.npc[num132].height - 1);
																	NetMessage.SendData(23, -1, -1, null, num132, 0f, 0f, 0f, 0, 0, 0);
																	NPCLoader.OnGoToStatue(Main.npc[num132], true);
																}
															}
															break;
														case 41:
															if (Wiring.CheckMech(num121, num120, 300))
															{
																List<int> array3 = new List<int>(50);
																int num133 = 0;
																for (int num134 = 0; num134 < 200; num134++)
																{
																	bool vanillaCanGo2 = false;
																	if (Main.npc[num134].active && (Main.npc[num134].type == 18 || Main.npc[num134].type == 20 || Main.npc[num134].type == 124 || Main.npc[num134].type == 178 || Main.npc[num134].type == 208 || Main.npc[num134].type == 353 || Main.npc[num134].type == 633 || Main.npc[num134].type == 663))
																	{
																		vanillaCanGo2 = true;
																	}
																	if (Main.npc[num134].active && NPCLoader.CanGoToStatue(Main.npc[num134], false).GetValueOrDefault(vanillaCanGo2))
																	{
																		array3.Add(num134);
																		num133++;
																	}
																}
																if (num133 > 0)
																{
																	int num135 = array3[Main.rand.Next(num133)];
																	Main.npc[num135].position.X = (float)(num124 - Main.npc[num135].width / 2);
																	Main.npc[num135].position.Y = (float)(num125 - Main.npc[num135].height - 1);
																	NetMessage.SendData(23, -1, -1, null, num135, 0f, 0f, 0f, 0, 0, 0);
																	NPCLoader.OnGoToStatue(Main.npc[num135], false);
																}
															}
															break;
														case 42:
															if (Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, 58))
															{
																num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124, num125 - 12, 58, 0, 0f, 0f, 0f, 0f, 255);
															}
															break;
														default:
															if (num122 == 50)
															{
																if (Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, 65))
																{
																	if (!Collision.SolidTiles(num121 - 2, num121 + 3, num120, num120 + 2))
																	{
																		num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124, num125 - 12, 65, 0, 0f, 0f, 0f, 0f, 255);
																	}
																	else
																	{
																		Vector2 position3 = new Vector2((float)(num124 - 4), (float)(num125 - 22)) - new Vector2(10f);
																		Utils.PoofOfSmoke(position3);
																		NetMessage.SendData(106, -1, -1, null, (int)position3.X, position3.Y, 0f, 0f, 0, 0, 0);
																	}
																}
															}
															break;
														}
													}
													else if (Wiring.CheckMech(num121, num120, 30) && NPC.MechSpawn((float)num124, (float)num125, 74))
													{
														num126 = NPC.NewNPC(Wiring.GetNPCSource(num121, num120), num124, num125 - 12, (int)Utils.SelectRandom<short>(Main.rand, new short[]
														{
															74,
															297,
															298
														}), 0, 0f, 0f, 0f, 0f, 255);
													}
												}
												if (num126 >= 0)
												{
													Main.npc[num126].value = 0f;
													Main.npc[num126].npcSlots = 0f;
													Main.npc[num126].SpawnedFromStatue = true;
													Main.npc[num126].CanBeReplacedByOtherNPCs = true;
													goto IL_3686;
												}
												goto IL_3686;
											}
										}
										else if (num157 <= 210)
										{
											if (num157 == 207)
											{
												WorldGen.SwitchFountain(i, j);
												goto IL_3686;
											}
											if (num157 != 210)
											{
												goto IL_3686;
											}
											WorldGen.ExplodeMine(i, j, true);
											goto IL_3686;
										}
										else
										{
											if (num157 == 349)
											{
												int num136 = (int)(*tile.frameY / 18);
												num136 %= 3;
												int num137 = j - num136;
												int num138;
												for (num138 = (int)(*tile.frameX / 18); num138 >= 2; num138 -= 2)
												{
												}
												num138 = i - num138;
												Wiring.SkipWire(num138, num137);
												Wiring.SkipWire(num138, num137 + 1);
												Wiring.SkipWire(num138, num137 + 2);
												Wiring.SkipWire(num138 + 1, num137);
												Wiring.SkipWire(num138 + 1, num137 + 1);
												Wiring.SkipWire(num138 + 1, num137 + 2);
												short num139 = (*Main.tile[num138, num137].frameX != 0) ? -216 : 216;
												for (int num140 = 0; num140 < 2; num140++)
												{
													for (int num141 = 0; num141 < 3; num141++)
													{
														ref short frameX5 = ref Main.tile[num138 + num140, num137 + num141].frameX;
														frameX5 += num139;
													}
												}
												if (Main.netMode == 2)
												{
													NetMessage.SendTileSquare(-1, num138, num137, 2, 3, TileChangeType.None);
												}
												Animation.NewTemporaryAnimation((num139 <= 0) ? 1 : 0, 349, num138, num137);
												goto IL_3686;
											}
											if (num157 != 410)
											{
												goto IL_3686;
											}
										}
									}
									else if (num157 <= 506)
									{
										if (num157 == 455)
										{
											BirthdayParty.ToggleManualParty();
											goto IL_3686;
										}
										if (num157 != 480)
										{
											if (num157 != 506)
											{
												goto IL_3686;
											}
											int num142 = (int)(*tile.frameY / 18);
											num142 %= 3;
											int num143 = j - num142;
											int num144;
											for (num144 = (int)(*tile.frameX / 18); num144 >= 2; num144 -= 2)
											{
											}
											num144 = i - num144;
											Wiring.SkipWire(num144, num143);
											Wiring.SkipWire(num144, num143 + 1);
											Wiring.SkipWire(num144, num143 + 2);
											Wiring.SkipWire(num144 + 1, num143);
											Wiring.SkipWire(num144 + 1, num143 + 1);
											Wiring.SkipWire(num144 + 1, num143 + 2);
											short num145 = (*Main.tile[num144, num143].frameX >= 72) ? -72 : 72;
											for (int num146 = 0; num146 < 2; num146++)
											{
												for (int num147 = 0; num147 < 3; num147++)
												{
													ref short frameX6 = ref Main.tile[num144 + num146, num143 + num147].frameX;
													frameX6 += num145;
												}
											}
											if (Main.netMode == 2)
											{
												NetMessage.SendTileSquare(-1, num144, num143, 2, 3, TileChangeType.None);
												goto IL_3686;
											}
											goto IL_3686;
										}
									}
									else if (num157 <= 546)
									{
										if (num157 != 509)
										{
											if (num157 != 546)
											{
												goto IL_3686;
											}
											*tile.type = 557;
											WorldGen.SquareTileFrame(i, j, true);
											NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
											goto IL_3686;
										}
									}
									else
									{
										if (num157 == 557)
										{
											*tile.type = 546;
											WorldGen.SquareTileFrame(i, j, true);
											NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
											goto IL_3686;
										}
										if (num157 - 657 > 1)
										{
											goto IL_3686;
										}
									}
									WorldGen.SwitchMonolith(i, j);
									goto IL_3686;
								}
								IL_206A:
								WorldGen.SwitchMB(i, j);
							}
						}
						else
						{
							int num148 = j - (int)(*tile.frameY / 18);
							int num149 = i - (int)(*tile.frameX / 18);
							Wiring.SkipWire(num149, num148);
							Wiring.SkipWire(num149, num148 + 1);
							if (Wiring.CheckMech(num149, num148, 30))
							{
								bool flag12 = false;
								for (int num150 = 0; num150 < 1000; num150++)
								{
									if (Main.projectile[num150].active && Main.projectile[num150].aiStyle == 73 && Main.projectile[num150].ai[0] == (float)num149 && Main.projectile[num150].ai[1] == (float)num148)
									{
										flag12 = true;
										break;
									}
								}
								if (!flag12)
								{
									int type4 = 419 + Main.rand.Next(4);
									Projectile.NewProjectile(Wiring.GetProjectileSource(num149, num148), (float)(num149 * 16 + 8), (float)(num148 * 16 + 2), 0f, 0f, type4, 0, 0f, Main.myPlayer, (float)num149, (float)num148, 0f);
								}
							}
						}
					}
					else
					{
						int num151 = j - (int)(*tile.frameY / 18);
						int num152 = i - (int)(*tile.frameX / 18);
						Wiring.SkipWire(num152, num151);
						Wiring.SkipWire(num152, num151 + 1);
						Wiring.SkipWire(num152 + 1, num151);
						Wiring.SkipWire(num152 + 1, num151 + 1);
						if (Wiring.CheckMech(num152, num151, 30))
						{
							WorldGen.LaunchRocketSmall(num152, num151, true);
						}
					}
				}
				else
				{
					int num153 = i - (int)(*tile.frameX / 18);
					if (*tile.wall != 87 || (double)j <= Main.worldSurface || NPC.downedPlantBoss)
					{
						if (Wiring._teleport[0].X == -1f)
						{
							Wiring._teleport[0].X = (float)num153;
							Wiring._teleport[0].Y = (float)j;
							if (tile.halfBrick())
							{
								Vector2[] teleport = Wiring._teleport;
								int num159 = 0;
								teleport[num159].Y = teleport[num159].Y + 0.5f;
							}
						}
						else if (Wiring._teleport[0].X != (float)num153 || Wiring._teleport[0].Y != (float)j)
						{
							Wiring._teleport[1].X = (float)num153;
							Wiring._teleport[1].Y = (float)j;
							if (tile.halfBrick())
							{
								Vector2[] teleport2 = Wiring._teleport;
								int num160 = 1;
								teleport2[num160].Y = teleport2[num160].Y + 0.5f;
							}
						}
					}
				}
				IL_3686:
				TileLoader.HitWire(i, j, type);
				return;
			}
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0040B348 File Offset: 0x00409548
		public unsafe static void ToggleHolidayLight(int i, int j, Tile tileCache, bool? forcedStateWhereTrueIsOn)
		{
			bool flag = *tileCache.frameX >= 54;
			if (forcedStateWhereTrueIsOn == null || !forcedStateWhereTrueIsOn.Value != flag)
			{
				if (*tileCache.frameX < 54)
				{
					ref short frameX = ref tileCache.frameX;
					frameX += 54;
				}
				else
				{
					ref short frameX2 = ref tileCache.frameX;
					frameX2 -= 54;
				}
				NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0040B3B0 File Offset: 0x004095B0
		public unsafe static void ToggleHangingLantern(int i, int j, Tile tileCache, bool? forcedStateWhereTrueIsOn, bool doSkipWires)
		{
			int num;
			for (num = (int)(*tileCache.frameY / 18); num >= 2; num -= 2)
			{
			}
			int num2 = j - num;
			short num3 = 18;
			if (*tileCache.frameX > 0)
			{
				num3 = -18;
			}
			bool flag = *tileCache.frameX > 0;
			if (forcedStateWhereTrueIsOn == null || !forcedStateWhereTrueIsOn.Value != flag)
			{
				ref short frameX = ref Main.tile[i, num2].frameX;
				frameX += num3;
				ref short frameX2 = ref Main.tile[i, num2 + 1].frameX;
				frameX2 += num3;
				if (doSkipWires)
				{
					Wiring.SkipWire(i, num2);
					Wiring.SkipWire(i, num2 + 1);
				}
				NetMessage.SendTileSquare(-1, i, j, 1, 2, TileChangeType.None);
			}
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0040B464 File Offset: 0x00409664
		public unsafe static void Toggle2x2Light(int i, int j, Tile tileCache, bool? forcedStateWhereTrueIsOn, bool doSkipWires)
		{
			int num;
			for (num = (int)(*tileCache.frameY / 18); num >= 2; num -= 2)
			{
			}
			num = j - num;
			int num2 = (int)(*tileCache.frameX / 18);
			if (num2 > 1)
			{
				num2 -= 2;
			}
			num2 = i - num2;
			short num3 = 36;
			if (*Main.tile[num2, num].frameX > 0)
			{
				num3 = -36;
			}
			bool flag = *Main.tile[num2, num].frameX > 0;
			if (forcedStateWhereTrueIsOn == null || !forcedStateWhereTrueIsOn.Value != flag)
			{
				ref short frameX = ref Main.tile[num2, num].frameX;
				frameX += num3;
				ref short frameX2 = ref Main.tile[num2, num + 1].frameX;
				frameX2 += num3;
				ref short frameX3 = ref Main.tile[num2 + 1, num].frameX;
				frameX3 += num3;
				ref short frameX4 = ref Main.tile[num2 + 1, num + 1].frameX;
				frameX4 += num3;
				if (doSkipWires)
				{
					Wiring.SkipWire(num2, num);
					Wiring.SkipWire(num2 + 1, num);
					Wiring.SkipWire(num2, num + 1);
					Wiring.SkipWire(num2 + 1, num + 1);
				}
				NetMessage.SendTileSquare(-1, num2, num, 2, 2, TileChangeType.None);
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0040B59C File Offset: 0x0040979C
		public unsafe static void ToggleLampPost(int i, int j, Tile tileCache, bool? forcedStateWhereTrueIsOn, bool doSkipWires)
		{
			int num = j - (int)(*tileCache.frameY / 18);
			short num2 = 18;
			if (*tileCache.frameX > 0)
			{
				num2 = -18;
			}
			bool flag = *tileCache.frameX > 0;
			if (forcedStateWhereTrueIsOn != null && !forcedStateWhereTrueIsOn.Value == flag)
			{
				return;
			}
			for (int k = num; k < num + 6; k++)
			{
				ref short frameX = ref Main.tile[i, k].frameX;
				frameX += num2;
				if (doSkipWires)
				{
					Wiring.SkipWire(i, k);
				}
			}
			NetMessage.SendTileSquare(-1, i, num, 1, 6, TileChangeType.None);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0040B62C File Offset: 0x0040982C
		public unsafe static void ToggleTorch(int i, int j, Tile tileCache, bool? forcedStateWhereTrueIsOn)
		{
			bool flag = *tileCache.frameX >= 66;
			if (forcedStateWhereTrueIsOn == null || !forcedStateWhereTrueIsOn.Value != flag)
			{
				if (*tileCache.frameX < 66)
				{
					ref short frameX = ref tileCache.frameX;
					frameX += 66;
				}
				else
				{
					ref short frameX2 = ref tileCache.frameX;
					frameX2 -= 66;
				}
				NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
			}
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0040B694 File Offset: 0x00409894
		public unsafe static void ToggleCandle(int i, int j, Tile tileCache, bool? forcedStateWhereTrueIsOn)
		{
			short num = 18;
			if (*tileCache.frameX > 0)
			{
				num = -18;
			}
			bool flag = *tileCache.frameX > 0;
			if (forcedStateWhereTrueIsOn == null || !forcedStateWhereTrueIsOn.Value != flag)
			{
				ref short frameX = ref tileCache.frameX;
				frameX += num;
				NetMessage.SendTileSquare(-1, i, j, 3, TileChangeType.None);
			}
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0040B6EC File Offset: 0x004098EC
		public unsafe static void ToggleLamp(int i, int j, Tile tileCache, bool? forcedStateWhereTrueIsOn, bool doSkipWires)
		{
			int num;
			for (num = (int)(*tileCache.frameY / 18); num >= 3; num -= 3)
			{
			}
			num = j - num;
			short num2 = 18;
			if (*tileCache.frameX > 0)
			{
				num2 = -18;
			}
			bool flag = *tileCache.frameX > 0;
			if (forcedStateWhereTrueIsOn == null || !forcedStateWhereTrueIsOn.Value != flag)
			{
				ref short frameX = ref Main.tile[i, num].frameX;
				frameX += num2;
				ref short frameX2 = ref Main.tile[i, num + 1].frameX;
				frameX2 += num2;
				ref short frameX3 = ref Main.tile[i, num + 2].frameX;
				frameX3 += num2;
				if (doSkipWires)
				{
					Wiring.SkipWire(i, num);
					Wiring.SkipWire(i, num + 1);
					Wiring.SkipWire(i, num + 2);
				}
				NetMessage.SendTileSquare(-1, i, num, 1, 3, TileChangeType.None);
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0040B7C0 File Offset: 0x004099C0
		public unsafe static void ToggleChandelier(int i, int j, Tile tileCache, bool? forcedStateWhereTrueIsOn, bool doSkipWires)
		{
			int num;
			for (num = (int)(*tileCache.frameY / 18); num >= 3; num -= 3)
			{
			}
			int num2 = j - num;
			int num3 = (int)(*tileCache.frameX % 108 / 18);
			if (num3 > 2)
			{
				num3 -= 3;
			}
			num3 = i - num3;
			short num4 = 54;
			if (*Main.tile[num3, num2].frameX % 108 > 0)
			{
				num4 = -54;
			}
			bool flag = *Main.tile[num3, num2].frameX % 108 > 0;
			if (forcedStateWhereTrueIsOn != null && !forcedStateWhereTrueIsOn.Value == flag)
			{
				return;
			}
			for (int k = num3; k < num3 + 3; k++)
			{
				for (int l = num2; l < num2 + 3; l++)
				{
					ref short frameX = ref Main.tile[k, l].frameX;
					frameX += num4;
					if (doSkipWires)
					{
						Wiring.SkipWire(k, l);
					}
				}
			}
			NetMessage.SendTileSquare(-1, num3 + 1, num2 + 1, 3, TileChangeType.None);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0040B8B8 File Offset: 0x00409AB8
		public unsafe static void ToggleCampFire(int i, int j, Tile tileCache, bool? forcedStateWhereTrueIsOn, bool doSkipWires)
		{
			int num = (int)(*tileCache.frameX % 54 / 18);
			int num2 = (int)(*tileCache.frameY % 36 / 18);
			int num3 = i - num;
			int num4 = j - num2;
			bool flag = *Main.tile[num3, num4].frameY >= 36;
			if (forcedStateWhereTrueIsOn != null && !forcedStateWhereTrueIsOn.Value == flag)
			{
				return;
			}
			int num5 = 36;
			if (*Main.tile[num3, num4].frameY >= 36)
			{
				num5 = -36;
			}
			for (int k = num3; k < num3 + 3; k++)
			{
				for (int l = num4; l < num4 + 2; l++)
				{
					if (doSkipWires)
					{
						Wiring.SkipWire(k, l);
					}
					*Main.tile[k, l].frameY = (short)((int)(*Main.tile[k, l].frameY) + num5);
				}
			}
			NetMessage.SendTileSquare(-1, num3, num4, 3, 2, TileChangeType.None);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0040B9B4 File Offset: 0x00409BB4
		public unsafe static void ToggleFirePlace(int i, int j, Tile theBlock, bool? forcedStateWhereTrueIsOn, bool doSkipWires)
		{
			int num = (int)(*theBlock.frameX % 54 / 18);
			int num2 = (int)(*theBlock.frameY % 36 / 18);
			int num3 = i - num;
			int num4 = j - num2;
			bool flag = *Main.tile[num3, num4].frameX >= 54;
			if (forcedStateWhereTrueIsOn != null && !forcedStateWhereTrueIsOn.Value == flag)
			{
				return;
			}
			int num5 = 54;
			if (*Main.tile[num3, num4].frameX >= 54)
			{
				num5 = -54;
			}
			for (int k = num3; k < num3 + 3; k++)
			{
				for (int l = num4; l < num4 + 2; l++)
				{
					if (doSkipWires)
					{
						Wiring.SkipWire(k, l);
					}
					*Main.tile[k, l].frameX = (short)((int)(*Main.tile[k, l].frameX) + num5);
				}
			}
			NetMessage.SendTileSquare(-1, num3, num4, 3, 2, TileChangeType.None);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0040BAB0 File Offset: 0x00409CB0
		private unsafe static void GeyserTrap(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			if (*tile.type != 443)
			{
				return;
			}
			int num = (int)(*tile.frameX / 36);
			int num2 = i - ((int)(*tile.frameX) - num * 36) / 18;
			if (Wiring.CheckMech(num2, j, 200))
			{
				Vector2 zero = Vector2.Zero;
				Vector2 zero2 = Vector2.Zero;
				int num3 = 654;
				int damage = 20;
				if (num < 2)
				{
					zero = new Vector2((float)(num2 + 1), (float)j) * 16f;
					zero2..ctor(0f, -8f);
				}
				else
				{
					zero = new Vector2((float)(num2 + 1), (float)(j + 1)) * 16f;
					zero2..ctor(0f, 8f);
				}
				if (num3 != 0)
				{
					Projectile.NewProjectile(Wiring.GetProjectileSource(num2, j), (float)((int)zero.X), (float)((int)zero.Y), zero2.X, zero2.Y, num3, damage, 2f, Main.myPlayer, 0f, 0f, 0f);
				}
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0040BBC4 File Offset: 0x00409DC4
		private static void Teleport()
		{
			if (Wiring._teleport[0].X < Wiring._teleport[1].X + 3f && Wiring._teleport[0].X > Wiring._teleport[1].X - 3f && Wiring._teleport[0].Y > Wiring._teleport[1].Y - 3f && Wiring._teleport[0].Y < Wiring._teleport[1].Y)
			{
				return;
			}
			Rectangle[] array = new Rectangle[2];
			array[0].X = (int)(Wiring._teleport[0].X * 16f);
			array[0].Width = 48;
			array[0].Height = 48;
			array[0].Y = (int)(Wiring._teleport[0].Y * 16f - (float)array[0].Height);
			array[1].X = (int)(Wiring._teleport[1].X * 16f);
			array[1].Width = 48;
			array[1].Height = 48;
			array[1].Y = (int)(Wiring._teleport[1].Y * 16f - (float)array[1].Height);
			for (int i = 0; i < 2; i++)
			{
				Vector2 vector;
				vector..ctor((float)(array[1].X - array[0].X), (float)(array[1].Y - array[0].Y));
				if (i == 1)
				{
					vector..ctor((float)(array[0].X - array[1].X), (float)(array[0].Y - array[1].Y));
				}
				if (!Wiring.blockPlayerTeleportationForOneIteration)
				{
					for (int j = 0; j < 255; j++)
					{
						if (Main.player[j].active && !Main.player[j].dead && !Main.player[j].teleporting && Wiring.TeleporterHitboxIntersects(array[i], Main.player[j].Hitbox))
						{
							Vector2 vector2 = Main.player[j].position + vector;
							Main.player[j].teleporting = true;
							if (Main.netMode == 2)
							{
								RemoteClient.CheckSection(j, vector2, 1);
							}
							Main.player[j].Teleport(vector2, 0, 0);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(65, -1, -1, null, 0, (float)j, vector2.X, vector2.Y, 0, 0, 0);
							}
						}
					}
				}
				for (int k = 0; k < 200; k++)
				{
					if (Main.npc[k].active && !Main.npc[k].teleporting && Main.npc[k].lifeMax > 5 && !Main.npc[k].boss && !Main.npc[k].noTileCollide)
					{
						int type = Main.npc[k].type;
						if (!NPCID.Sets.TeleportationImmune[type] && Wiring.TeleporterHitboxIntersects(array[i], Main.npc[k].Hitbox))
						{
							Main.npc[k].teleporting = true;
							Main.npc[k].Teleport(Main.npc[k].position + vector, 0, 0);
						}
					}
				}
			}
			for (int l = 0; l < 255; l++)
			{
				Main.player[l].teleporting = false;
			}
			for (int m = 0; m < 200; m++)
			{
				Main.npc[m].teleporting = false;
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0040BFCC File Offset: 0x0040A1CC
		private static bool TeleporterHitboxIntersects(Rectangle teleporter, Rectangle entity)
		{
			Rectangle rectangle = Rectangle.Union(teleporter, entity);
			return rectangle.Width <= teleporter.Width + entity.Width && rectangle.Height <= teleporter.Height + entity.Height;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0040C010 File Offset: 0x0040A210
		public unsafe static void DeActive(int i, int j)
		{
			if (!Main.tile[i, j].active() || (*Main.tile[i, j].type == 226 && (double)j > Main.worldSurface && !NPC.downedPlantBoss))
			{
				return;
			}
			bool flag = Main.tileSolid[(int)(*Main.tile[i, j].type)] && !TileID.Sets.NotReallySolid[(int)(*Main.tile[i, j].type)];
			ushort type = *Main.tile[i, j].type;
			if (type == 314 || type - 386 <= 3 || type == 476)
			{
				flag = false;
			}
			if (flag && (!Main.tile[i, j - 1].active() || (!TileID.Sets.BasicChest[(int)(*Main.tile[i, j - 1].type)] && *Main.tile[i, j - 1].type != 26 && *Main.tile[i, j - 1].type != 77 && *Main.tile[i, j - 1].type != 88 && *Main.tile[i, j - 1].type != 470 && *Main.tile[i, j - 1].type != 475 && *Main.tile[i, j - 1].type != 237 && *Main.tile[i, j - 1].type != 597 && WorldGen.CanKillTile(i, j))))
			{
				Main.tile[i, j].inActive(true);
				WorldGen.SquareTileFrame(i, j, false);
				if (Main.netMode != 1)
				{
					NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
				}
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0040C220 File Offset: 0x0040A420
		public static void ReActive(int i, int j)
		{
			Main.tile[i, j].inActive(false);
			WorldGen.SquareTileFrame(i, j, false);
			if (Main.netMode != 1)
			{
				NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0040C25C File Offset: 0x0040A45C
		private static void MassWireOperationInner(Player user, Point ps, Point pe, Vector2 dropPoint, bool dir, ref int wireCount, ref int actuatorCount)
		{
			Math.Abs(ps.X - pe.X);
			Math.Abs(ps.Y - pe.Y);
			int num = Math.Sign(pe.X - ps.X);
			int num2 = Math.Sign(pe.Y - ps.Y);
			WiresUI.Settings.MultiToolMode toolMode = WiresUI.Settings.ToolMode;
			Point pt = default(Point);
			bool flag = false;
			Item.StartCachingType(530);
			Item.StartCachingType(849);
			int num3;
			int num4;
			int num5;
			if (dir)
			{
				pt.X = ps.X;
				num3 = ps.Y;
				num4 = pe.Y;
				num5 = num2;
			}
			else
			{
				pt.Y = ps.Y;
				num3 = ps.X;
				num4 = pe.X;
				num5 = num;
			}
			int i = num3;
			while (i != num4 && !flag)
			{
				if (dir)
				{
					pt.Y = i;
				}
				else
				{
					pt.X = i;
				}
				bool? flag2 = Wiring.MassWireOperationStep(user, pt, toolMode, ref wireCount, ref actuatorCount);
				if (flag2 != null && !flag2.Value)
				{
					flag = true;
					break;
				}
				i += num5;
			}
			if (dir)
			{
				pt.Y = pe.Y;
				num3 = ps.X;
				num4 = pe.X;
				num5 = num;
			}
			else
			{
				pt.X = pe.X;
				num3 = ps.Y;
				num4 = pe.Y;
				num5 = num2;
			}
			int j = num3;
			while (j != num4 && !flag)
			{
				if (!dir)
				{
					pt.Y = j;
				}
				else
				{
					pt.X = j;
				}
				bool? flag3 = Wiring.MassWireOperationStep(user, pt, toolMode, ref wireCount, ref actuatorCount);
				if (flag3 != null && !flag3.Value)
				{
					flag = true;
					break;
				}
				j += num5;
			}
			if (!flag)
			{
				Wiring.MassWireOperationStep(user, pe, toolMode, ref wireCount, ref actuatorCount);
			}
			IEntitySource source_Misc = user.GetSource_Misc(ItemSourceID.ToContextString(5));
			Item.DropCache(source_Misc, dropPoint, Vector2.Zero, 530, true);
			Item.DropCache(source_Misc, dropPoint, Vector2.Zero, 849, true);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0040C454 File Offset: 0x0040A654
		private static bool? MassWireOperationStep(Player user, Point pt, WiresUI.Settings.MultiToolMode mode, ref int wiresLeftToConsume, ref int actuatorsLeftToConstume)
		{
			if (!WorldGen.InWorld(pt.X, pt.Y, 1))
			{
				return null;
			}
			Tile tile = Main.tile[pt.X, pt.Y];
			if (tile == null)
			{
				return null;
			}
			if (user != null && !user.CanDoWireStuffHere(pt.X, pt.Y))
			{
				return null;
			}
			if (!mode.HasFlag(WiresUI.Settings.MultiToolMode.Cutter))
			{
				if (mode.HasFlag(WiresUI.Settings.MultiToolMode.Red) && !tile.wire())
				{
					if (wiresLeftToConsume <= 0)
					{
						return new bool?(false);
					}
					wiresLeftToConsume--;
					WorldGen.PlaceWire(pt.X, pt.Y);
					NetMessage.SendData(17, -1, -1, null, 5, (float)pt.X, (float)pt.Y, 0f, 0, 0, 0);
				}
				if (mode.HasFlag(WiresUI.Settings.MultiToolMode.Green) && !tile.wire3())
				{
					if (wiresLeftToConsume <= 0)
					{
						return new bool?(false);
					}
					wiresLeftToConsume--;
					WorldGen.PlaceWire3(pt.X, pt.Y);
					NetMessage.SendData(17, -1, -1, null, 12, (float)pt.X, (float)pt.Y, 0f, 0, 0, 0);
				}
				if (mode.HasFlag(WiresUI.Settings.MultiToolMode.Blue) && !tile.wire2())
				{
					if (wiresLeftToConsume <= 0)
					{
						return new bool?(false);
					}
					wiresLeftToConsume--;
					WorldGen.PlaceWire2(pt.X, pt.Y);
					NetMessage.SendData(17, -1, -1, null, 10, (float)pt.X, (float)pt.Y, 0f, 0, 0, 0);
				}
				if (mode.HasFlag(WiresUI.Settings.MultiToolMode.Yellow) && !tile.wire4())
				{
					if (wiresLeftToConsume <= 0)
					{
						return new bool?(false);
					}
					wiresLeftToConsume--;
					WorldGen.PlaceWire4(pt.X, pt.Y);
					NetMessage.SendData(17, -1, -1, null, 16, (float)pt.X, (float)pt.Y, 0f, 0, 0, 0);
				}
				if (mode.HasFlag(WiresUI.Settings.MultiToolMode.Actuator) && !tile.actuator())
				{
					if (actuatorsLeftToConstume <= 0)
					{
						return new bool?(false);
					}
					actuatorsLeftToConstume--;
					WorldGen.PlaceActuator(pt.X, pt.Y);
					NetMessage.SendData(17, -1, -1, null, 8, (float)pt.X, (float)pt.Y, 0f, 0, 0, 0);
				}
			}
			if (mode.HasFlag(WiresUI.Settings.MultiToolMode.Cutter))
			{
				if (mode.HasFlag(WiresUI.Settings.MultiToolMode.Red) && tile.wire() && WorldGen.KillWire(pt.X, pt.Y))
				{
					NetMessage.SendData(17, -1, -1, null, 6, (float)pt.X, (float)pt.Y, 0f, 0, 0, 0);
				}
				if (mode.HasFlag(WiresUI.Settings.MultiToolMode.Green) && tile.wire3() && WorldGen.KillWire3(pt.X, pt.Y))
				{
					NetMessage.SendData(17, -1, -1, null, 13, (float)pt.X, (float)pt.Y, 0f, 0, 0, 0);
				}
				if (mode.HasFlag(WiresUI.Settings.MultiToolMode.Blue) && tile.wire2() && WorldGen.KillWire2(pt.X, pt.Y))
				{
					NetMessage.SendData(17, -1, -1, null, 11, (float)pt.X, (float)pt.Y, 0f, 0, 0, 0);
				}
				if (mode.HasFlag(WiresUI.Settings.MultiToolMode.Yellow) && tile.wire4() && WorldGen.KillWire4(pt.X, pt.Y))
				{
					NetMessage.SendData(17, -1, -1, null, 17, (float)pt.X, (float)pt.Y, 0f, 0, 0, 0);
				}
				if (mode.HasFlag(WiresUI.Settings.MultiToolMode.Actuator) && tile.actuator() && WorldGen.KillActuator(pt.X, pt.Y))
				{
					NetMessage.SendData(17, -1, -1, null, 9, (float)pt.X, (float)pt.Y, 0f, 0, 0, 0);
				}
			}
			return new bool?(true);
		}

		// Token: 0x04000F12 RID: 3858
		public static bool blockPlayerTeleportationForOneIteration;

		/// <summary>
		/// True while wiring pulse code is running, which happens in <see cref="M:Terraria.Wiring.TripWire(System.Int32,System.Int32,System.Int32,System.Int32)" />. Check this before calling <see cref="M:Terraria.Wiring.SkipWire(System.Int32,System.Int32)" /> in any code that is shared between wiring and other interactions to prevent buggy behavior.<br /><br />
		/// For example, the code in <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/ExampleCampfire.cs#L97">ExampleCampfire</see> needs to check <see cref="F:Terraria.Wiring.running" /> because the code is shared between wiring and right click interactions. 
		/// </summary>
		// Token: 0x04000F13 RID: 3859
		public static bool running;

		// Token: 0x04000F14 RID: 3860
		private static Dictionary<Point16, bool> _wireSkip;

		// Token: 0x04000F15 RID: 3861
		public static DoubleStack<Point16> _wireList;

		// Token: 0x04000F16 RID: 3862
		public static DoubleStack<byte> _wireDirectionList;

		// Token: 0x04000F17 RID: 3863
		public static Dictionary<Point16, byte> _toProcess;

		// Token: 0x04000F18 RID: 3864
		private static Queue<Point16> _GatesCurrent;

		// Token: 0x04000F19 RID: 3865
		public static Queue<Point16> _LampsToCheck;

		// Token: 0x04000F1A RID: 3866
		public static Queue<Point16> _GatesNext;

		// Token: 0x04000F1B RID: 3867
		private static Dictionary<Point16, bool> _GatesDone;

		// Token: 0x04000F1C RID: 3868
		private static Dictionary<Point16, byte> _PixelBoxTriggers;

		// Token: 0x04000F1D RID: 3869
		public static Vector2[] _teleport;

		// Token: 0x04000F1E RID: 3870
		private const int MaxPump = 20;

		// Token: 0x04000F1F RID: 3871
		public static int[] _inPumpX;

		// Token: 0x04000F20 RID: 3872
		public static int[] _inPumpY;

		// Token: 0x04000F21 RID: 3873
		public static int _numInPump;

		// Token: 0x04000F22 RID: 3874
		public static int[] _outPumpX;

		// Token: 0x04000F23 RID: 3875
		public static int[] _outPumpY;

		// Token: 0x04000F24 RID: 3876
		public static int _numOutPump;

		// Token: 0x04000F25 RID: 3877
		private const int MaxMech = 1000;

		// Token: 0x04000F26 RID: 3878
		private static int[] _mechX;

		// Token: 0x04000F27 RID: 3879
		private static int[] _mechY;

		// Token: 0x04000F28 RID: 3880
		private static int _numMechs;

		// Token: 0x04000F29 RID: 3881
		private static int[] _mechTime;

		// Token: 0x04000F2A RID: 3882
		public static int _currentWireColor;

		// Token: 0x04000F2B RID: 3883
		private static int CurrentUser = 255;
	}
}
