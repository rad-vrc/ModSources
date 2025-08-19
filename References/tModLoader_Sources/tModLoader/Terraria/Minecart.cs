using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria
{
	// Token: 0x0200003B RID: 59
	public static class Minecart
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x00149CC8 File Offset: 0x00147EC8
		public static void Initialize()
		{
			if (!Main.dedServ && (float)TextureAssets.MinecartMount.Width() != 50f)
			{
				throw new Exception("Be sure to update Minecart.textureWidth to match the actual texture size of " + 50f.ToString() + ".");
			}
			Minecart._rightSideConnection = new int[36];
			Minecart._leftSideConnection = new int[36];
			Minecart._trackType = new int[36];
			Minecart._boostLeft = new bool[36];
			Minecart._texturePosition = new Vector2[40];
			Minecart._tileHeight = new int[36][];
			for (int i = 0; i < 36; i++)
			{
				int[] array = new int[8];
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = 5;
				}
				Minecart._tileHeight[i] = array;
			}
			int num = 0;
			Minecart._leftSideConnection[num] = -1;
			Minecart._rightSideConnection[num] = -1;
			Minecart._tileHeight[num][0] = -4;
			Minecart._tileHeight[num][7] = -4;
			Minecart._texturePosition[num] = new Vector2(0f, 0f);
			num++;
			Minecart._leftSideConnection[num] = 1;
			Minecart._rightSideConnection[num] = 1;
			Minecart._texturePosition[num] = new Vector2(1f, 0f);
			num++;
			Minecart._leftSideConnection[num] = -1;
			Minecart._rightSideConnection[num] = 1;
			for (int k = 0; k < 4; k++)
			{
				Minecart._tileHeight[num][k] = -1;
			}
			Minecart._texturePosition[num] = new Vector2(2f, 1f);
			num++;
			Minecart._leftSideConnection[num] = 1;
			Minecart._rightSideConnection[num] = -1;
			for (int l = 4; l < 8; l++)
			{
				Minecart._tileHeight[num][l] = -1;
			}
			Minecart._texturePosition[num] = new Vector2(3f, 1f);
			num++;
			Minecart._leftSideConnection[num] = 2;
			Minecart._rightSideConnection[num] = 1;
			Minecart._tileHeight[num][0] = 1;
			Minecart._tileHeight[num][1] = 2;
			Minecart._tileHeight[num][2] = 3;
			Minecart._tileHeight[num][3] = 3;
			Minecart._tileHeight[num][4] = 4;
			Minecart._tileHeight[num][5] = 4;
			Minecart._texturePosition[num] = new Vector2(0f, 2f);
			num++;
			Minecart._leftSideConnection[num] = 1;
			Minecart._rightSideConnection[num] = 2;
			Minecart._tileHeight[num][2] = 4;
			Minecart._tileHeight[num][3] = 4;
			Minecart._tileHeight[num][4] = 3;
			Minecart._tileHeight[num][5] = 3;
			Minecart._tileHeight[num][6] = 2;
			Minecart._tileHeight[num][7] = 1;
			Minecart._texturePosition[num] = new Vector2(1f, 2f);
			num++;
			Minecart._leftSideConnection[num] = 1;
			Minecart._rightSideConnection[num] = 0;
			Minecart._tileHeight[num][4] = 6;
			Minecart._tileHeight[num][5] = 6;
			Minecart._tileHeight[num][6] = 7;
			Minecart._tileHeight[num][7] = 8;
			Minecart._texturePosition[num] = new Vector2(0f, 1f);
			num++;
			Minecart._leftSideConnection[num] = 0;
			Minecart._rightSideConnection[num] = 1;
			Minecart._tileHeight[num][0] = 8;
			Minecart._tileHeight[num][1] = 7;
			Minecart._tileHeight[num][2] = 6;
			Minecart._tileHeight[num][3] = 6;
			Minecart._texturePosition[num] = new Vector2(1f, 1f);
			num++;
			Minecart._leftSideConnection[num] = 0;
			Minecart._rightSideConnection[num] = 2;
			for (int m = 0; m < 8; m++)
			{
				Minecart._tileHeight[num][m] = 8 - m;
			}
			Minecart._texturePosition[num] = new Vector2(0f, 3f);
			num++;
			Minecart._leftSideConnection[num] = 2;
			Minecart._rightSideConnection[num] = 0;
			for (int n = 0; n < 8; n++)
			{
				Minecart._tileHeight[num][n] = n + 1;
			}
			Minecart._texturePosition[num] = new Vector2(1f, 3f);
			num++;
			Minecart._leftSideConnection[num] = 2;
			Minecart._rightSideConnection[num] = -1;
			Minecart._tileHeight[num][0] = 1;
			Minecart._tileHeight[num][1] = 2;
			for (int num2 = 2; num2 < 8; num2++)
			{
				Minecart._tileHeight[num][num2] = -1;
			}
			Minecart._texturePosition[num] = new Vector2(4f, 1f);
			num++;
			Minecart._leftSideConnection[num] = -1;
			Minecart._rightSideConnection[num] = 2;
			Minecart._tileHeight[num][6] = 2;
			Minecart._tileHeight[num][7] = 1;
			for (int num3 = 0; num3 < 6; num3++)
			{
				Minecart._tileHeight[num][num3] = -1;
			}
			Minecart._texturePosition[num] = new Vector2(5f, 1f);
			num++;
			Minecart._leftSideConnection[num] = 0;
			Minecart._rightSideConnection[num] = -1;
			Minecart._tileHeight[num][0] = 8;
			Minecart._tileHeight[num][1] = 7;
			Minecart._tileHeight[num][2] = 6;
			for (int num4 = 3; num4 < 8; num4++)
			{
				Minecart._tileHeight[num][num4] = -1;
			}
			Minecart._texturePosition[num] = new Vector2(6f, 1f);
			num++;
			Minecart._leftSideConnection[num] = -1;
			Minecart._rightSideConnection[num] = 0;
			Minecart._tileHeight[num][5] = 6;
			Minecart._tileHeight[num][6] = 7;
			Minecart._tileHeight[num][7] = 8;
			for (int num5 = 0; num5 < 5; num5++)
			{
				Minecart._tileHeight[num][num5] = -1;
			}
			Minecart._texturePosition[num] = new Vector2(7f, 1f);
			num++;
			Minecart._leftSideConnection[num] = -1;
			Minecart._rightSideConnection[num] = 1;
			Minecart._tileHeight[num][0] = -4;
			Minecart._texturePosition[num] = new Vector2(2f, 0f);
			num++;
			Minecart._leftSideConnection[num] = 1;
			Minecart._rightSideConnection[num] = -1;
			Minecart._tileHeight[num][7] = -4;
			Minecart._texturePosition[num] = new Vector2(3f, 0f);
			num++;
			Minecart._leftSideConnection[num] = 2;
			Minecart._rightSideConnection[num] = -1;
			for (int num6 = 0; num6 < 6; num6++)
			{
				Minecart._tileHeight[num][num6] = num6 + 1;
			}
			Minecart._tileHeight[num][6] = -3;
			Minecart._tileHeight[num][7] = -3;
			Minecart._texturePosition[num] = new Vector2(4f, 0f);
			num++;
			Minecart._leftSideConnection[num] = -1;
			Minecart._rightSideConnection[num] = 2;
			Minecart._tileHeight[num][0] = -3;
			Minecart._tileHeight[num][1] = -3;
			for (int num7 = 2; num7 < 8; num7++)
			{
				Minecart._tileHeight[num][num7] = 8 - num7;
			}
			Minecart._texturePosition[num] = new Vector2(5f, 0f);
			num++;
			Minecart._leftSideConnection[num] = 0;
			Minecart._rightSideConnection[num] = -1;
			for (int num8 = 0; num8 < 6; num8++)
			{
				Minecart._tileHeight[num][num8] = 8 - num8;
			}
			Minecart._tileHeight[num][6] = -3;
			Minecart._tileHeight[num][7] = -3;
			Minecart._texturePosition[num] = new Vector2(6f, 0f);
			num++;
			Minecart._leftSideConnection[num] = -1;
			Minecart._rightSideConnection[num] = 0;
			Minecart._tileHeight[num][0] = -3;
			Minecart._tileHeight[num][1] = -3;
			for (int num9 = 2; num9 < 8; num9++)
			{
				Minecart._tileHeight[num][num9] = num9 + 1;
			}
			Minecart._texturePosition[num] = new Vector2(7f, 0f);
			num++;
			Minecart._leftSideConnection[num] = -1;
			Minecart._rightSideConnection[num] = -1;
			Minecart._tileHeight[num][0] = -4;
			Minecart._tileHeight[num][7] = -4;
			Minecart._trackType[num] = 1;
			Minecart._texturePosition[num] = new Vector2(0f, 4f);
			num++;
			Minecart._leftSideConnection[num] = 1;
			Minecart._rightSideConnection[num] = 1;
			Minecart._trackType[num] = 1;
			Minecart._texturePosition[num] = new Vector2(1f, 4f);
			num++;
			Minecart._leftSideConnection[num] = -1;
			Minecart._rightSideConnection[num] = 1;
			Minecart._tileHeight[num][0] = -4;
			Minecart._trackType[num] = 1;
			Minecart._texturePosition[num] = new Vector2(0f, 5f);
			num++;
			Minecart._leftSideConnection[num] = 1;
			Minecart._rightSideConnection[num] = -1;
			Minecart._tileHeight[num][7] = -4;
			Minecart._trackType[num] = 1;
			Minecart._texturePosition[num] = new Vector2(1f, 5f);
			num++;
			Minecart._leftSideConnection[num] = -1;
			Minecart._rightSideConnection[num] = 1;
			for (int num10 = 0; num10 < 6; num10++)
			{
				Minecart._tileHeight[num][num10] = -2;
			}
			Minecart._texturePosition[num] = new Vector2(2f, 2f);
			num++;
			Minecart._leftSideConnection[num] = 1;
			Minecart._rightSideConnection[num] = -1;
			for (int num11 = 2; num11 < 8; num11++)
			{
				Minecart._tileHeight[num][num11] = -2;
			}
			Minecart._texturePosition[num] = new Vector2(3f, 2f);
			num++;
			Minecart._leftSideConnection[num] = 2;
			Minecart._rightSideConnection[num] = -1;
			Minecart._tileHeight[num][0] = 1;
			Minecart._tileHeight[num][1] = 2;
			for (int num12 = 2; num12 < 8; num12++)
			{
				Minecart._tileHeight[num][num12] = -2;
			}
			Minecart._texturePosition[num] = new Vector2(4f, 2f);
			num++;
			Minecart._leftSideConnection[num] = -1;
			Minecart._rightSideConnection[num] = 2;
			Minecart._tileHeight[num][6] = 2;
			Minecart._tileHeight[num][7] = 1;
			for (int num13 = 0; num13 < 6; num13++)
			{
				Minecart._tileHeight[num][num13] = -2;
			}
			Minecart._texturePosition[num] = new Vector2(5f, 2f);
			num++;
			Minecart._leftSideConnection[num] = 0;
			Minecart._rightSideConnection[num] = -1;
			Minecart._tileHeight[num][0] = 8;
			Minecart._tileHeight[num][1] = 7;
			Minecart._tileHeight[num][2] = 6;
			for (int num14 = 3; num14 < 8; num14++)
			{
				Minecart._tileHeight[num][num14] = -2;
			}
			Minecart._texturePosition[num] = new Vector2(6f, 2f);
			num++;
			Minecart._leftSideConnection[num] = -1;
			Minecart._rightSideConnection[num] = 0;
			Minecart._tileHeight[num][5] = 6;
			Minecart._tileHeight[num][6] = 7;
			Minecart._tileHeight[num][7] = 8;
			for (int num15 = 0; num15 < 5; num15++)
			{
				Minecart._tileHeight[num][num15] = -2;
			}
			Minecart._texturePosition[num] = new Vector2(7f, 2f);
			num++;
			Minecart._leftSideConnection[num] = 1;
			Minecart._rightSideConnection[num] = 1;
			Minecart._trackType[num] = 2;
			Minecart._boostLeft[num] = false;
			Minecart._texturePosition[num] = new Vector2(2f, 3f);
			num++;
			Minecart._leftSideConnection[num] = 1;
			Minecart._rightSideConnection[num] = 1;
			Minecart._trackType[num] = 2;
			Minecart._boostLeft[num] = true;
			Minecart._texturePosition[num] = new Vector2(3f, 3f);
			num++;
			Minecart._leftSideConnection[num] = 0;
			Minecart._rightSideConnection[num] = 2;
			for (int num16 = 0; num16 < 8; num16++)
			{
				Minecart._tileHeight[num][num16] = 8 - num16;
			}
			Minecart._trackType[num] = 2;
			Minecart._boostLeft[num] = false;
			Minecart._texturePosition[num] = new Vector2(4f, 3f);
			num++;
			Minecart._leftSideConnection[num] = 2;
			Minecart._rightSideConnection[num] = 0;
			for (int num17 = 0; num17 < 8; num17++)
			{
				Minecart._tileHeight[num][num17] = num17 + 1;
			}
			Minecart._trackType[num] = 2;
			Minecart._boostLeft[num] = true;
			Minecart._texturePosition[num] = new Vector2(5f, 3f);
			num++;
			Minecart._leftSideConnection[num] = 0;
			Minecart._rightSideConnection[num] = 2;
			for (int num18 = 0; num18 < 8; num18++)
			{
				Minecart._tileHeight[num][num18] = 8 - num18;
			}
			Minecart._trackType[num] = 2;
			Minecart._boostLeft[num] = true;
			Minecart._texturePosition[num] = new Vector2(6f, 3f);
			num++;
			Minecart._leftSideConnection[num] = 2;
			Minecart._rightSideConnection[num] = 0;
			for (int num19 = 0; num19 < 8; num19++)
			{
				Minecart._tileHeight[num][num19] = num19 + 1;
			}
			Minecart._trackType[num] = 2;
			Minecart._boostLeft[num] = false;
			Minecart._texturePosition[num] = new Vector2(7f, 3f);
			num++;
			Minecart._texturePosition[36] = new Vector2(0f, 6f);
			Minecart._texturePosition[37] = new Vector2(1f, 6f);
			Minecart._texturePosition[39] = new Vector2(0f, 7f);
			Minecart._texturePosition[38] = new Vector2(1f, 7f);
			for (int num20 = 0; num20 < Minecart._texturePosition.Length; num20++)
			{
				Minecart._texturePosition[num20] *= 18f;
			}
			for (int num21 = 0; num21 < Minecart._tileHeight.Length; num21++)
			{
				int[] array2 = Minecart._tileHeight[num21];
				for (int num22 = 0; num22 < array2.Length; num22++)
				{
					if (array2[num22] >= 0)
					{
						array2[num22] = (8 - array2[num22]) * 2;
					}
				}
			}
			int[] array3 = new int[36];
			Minecart._trackSwitchOptions = new int[64][];
			for (int num23 = 0; num23 < 64; num23++)
			{
				int num24 = 0;
				for (int num25 = 1; num25 < 256; num25 <<= 1)
				{
					if ((num23 & num25) == num25)
					{
						num24++;
					}
				}
				int num26 = 0;
				int num27 = 0;
				while (num27 < 36)
				{
					array3[num27] = -1;
					int num28 = 0;
					switch (Minecart._leftSideConnection[num27])
					{
					case 0:
						num28 |= 1;
						break;
					case 1:
						num28 |= 2;
						break;
					case 2:
						num28 |= 4;
						break;
					}
					switch (Minecart._rightSideConnection[num27])
					{
					case 0:
						num28 |= 8;
						break;
					case 1:
						num28 |= 16;
						break;
					case 2:
						num28 |= 32;
						break;
					}
					if (num24 < 2)
					{
						if (num23 == num28)
						{
							goto IL_E31;
						}
					}
					else if (num28 != 0 && (num23 & num28) == num28)
					{
						goto IL_E31;
					}
					IL_E3D:
					num27++;
					continue;
					IL_E31:
					array3[num27] = num27;
					num26++;
					goto IL_E3D;
				}
				if (num26 != 0)
				{
					int[] array4 = new int[num26];
					int num29 = 0;
					for (int num30 = 0; num30 < 36; num30++)
					{
						if (array3[num30] != -1)
						{
							array4[num29] = array3[num30];
							num29++;
						}
					}
					Minecart._trackSwitchOptions[num23] = array4;
				}
			}
			Minecart._firstPressureFrame = -1;
			Minecart._firstLeftBoostFrame = -1;
			Minecart._firstRightBoostFrame = -1;
			for (int num31 = 0; num31 < Minecart._trackType.Length; num31++)
			{
				int num32 = Minecart._trackType[num31];
				if (num32 != 1)
				{
					if (num32 == 2)
					{
						if (Minecart._boostLeft[num31])
						{
							if (Minecart._firstLeftBoostFrame == -1)
							{
								Minecart._firstLeftBoostFrame = (short)num31;
							}
						}
						else if (Minecart._firstRightBoostFrame == -1)
						{
							Minecart._firstRightBoostFrame = (short)num31;
						}
					}
				}
				else if (Minecart._firstPressureFrame == -1)
				{
					Minecart._firstPressureFrame = (short)num31;
				}
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0014ABED File Offset: 0x00148DED
		public unsafe static bool IsPressurePlate(Tile tile)
		{
			return !(tile == null) && (tile.active() && *tile.type == 314 && (*tile.frameX == 20 || *tile.frameX == 21));
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0014AC30 File Offset: 0x00148E30
		public unsafe static BitsByte TrackCollision(Player Player, ref Vector2 Position, ref Vector2 Velocity, ref Vector2 lastBoost, int Width, int Height, bool followDown, bool followUp, int fallStart, bool trackOnly, Mount.MountDelegatesData delegatesData)
		{
			if (followDown && followUp)
			{
				followDown = false;
				followUp = false;
			}
			Vector2 vector;
			vector..ctor((float)(Width / 2) - 25f, (float)(Height / 2));
			Vector2 vector2 = Position + new Vector2((float)(Width / 2) - 25f, (float)(Height / 2)) + Minecart._trackMagnetOffset;
			Vector2 vector3 = Velocity;
			float num = vector3.Length();
			vector3.Normalize();
			Vector2 vector4 = vector2;
			Tile tile = default(Tile);
			bool flag = false;
			bool flag2 = true;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			Minecart.TrackState trackState = Minecart.TrackState.NoTrack;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			Vector2 vector5 = Vector2.Zero;
			Vector2 vector6 = Vector2.Zero;
			BitsByte result = default(BitsByte);
			for (;;)
			{
				int num5 = (int)(vector4.X / 16f);
				int num6 = (int)(vector4.Y / 16f);
				int num7 = (int)vector4.X % 16 / 2;
				if (flag2)
				{
					num4 = num7;
				}
				bool flag7 = num7 != num4;
				if ((trackState == Minecart.TrackState.OnBack || trackState == Minecart.TrackState.OnTrack || trackState == Minecart.TrackState.OnFront) && num5 != num2)
				{
					int num8 = (int)((trackState != Minecart.TrackState.OnBack) ? tile.FrontTrack() : tile.BackTrack());
					int num27 = (vector3.X >= 0f) ? Minecart._rightSideConnection[num8] : Minecart._leftSideConnection[num8];
					if (num27 != 0)
					{
						if (num27 == 2)
						{
							num6++;
							vector4.Y += 2f;
						}
					}
					else
					{
						num6--;
						vector4.Y -= 2f;
					}
				}
				Minecart.TrackState trackState2 = Minecart.TrackState.NoTrack;
				bool flag8 = false;
				if (num5 != num2 || num6 != num3)
				{
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						flag8 = true;
					}
					tile = Main.tile[num5, num6];
					if (tile == null)
					{
						tile = default(Tile);
						Main.tile[num5, num6] = tile;
					}
					flag = (tile.nactive() && *tile.type == 314);
				}
				if (flag)
				{
					Minecart.TrackState trackState3 = Minecart.TrackState.NoTrack;
					int num9 = (int)tile.FrontTrack();
					int num10 = (int)tile.BackTrack();
					int num11 = Minecart._tileHeight[num9][num7];
					switch (num11)
					{
					case -4:
						if (trackState == Minecart.TrackState.OnFront)
						{
							if (trackOnly)
							{
								vector4 -= vector6;
								num = 0f;
								trackState2 = Minecart.TrackState.OnFront;
								flag6 = true;
							}
							else
							{
								trackState2 = Minecart.TrackState.NoTrack;
								flag5 = true;
							}
						}
						break;
					case -3:
						if (trackState == Minecart.TrackState.OnFront)
						{
							trackState = Minecart.TrackState.NoTrack;
							Matrix matrix = (Velocity.X > 0f) ? ((Minecart._leftSideConnection[num9] != 2) ? Matrix.CreateRotationZ(0.7853982f) : Matrix.CreateRotationZ(-0.7853982f)) : ((Minecart._rightSideConnection[num9] != 2) ? Matrix.CreateRotationZ(-0.7853982f) : Matrix.CreateRotationZ(0.7853982f));
							vector5 = Vector2.Transform(new Vector2(Velocity.X, 0f), matrix);
							vector5.X = Velocity.X;
							flag4 = true;
							num = 0f;
						}
						break;
					case -2:
						if (trackState == Minecart.TrackState.OnFront)
						{
							if (trackOnly)
							{
								vector4 -= vector6;
								num = 0f;
								trackState2 = Minecart.TrackState.OnFront;
								flag6 = true;
							}
							else
							{
								if (vector3.X < 0f)
								{
									float num12 = (float)(num5 * 16 + (num7 + 1) * 2) - vector4.X;
									vector4.X += num12;
									num += num12 / vector3.X;
								}
								vector3.X = 0f - vector3.X;
								result[1] = true;
								trackState2 = Minecart.TrackState.OnFront;
							}
						}
						break;
					case -1:
						if (trackState == Minecart.TrackState.OnFront)
						{
							vector4 -= vector6;
							num = 0f;
							trackState2 = Minecart.TrackState.OnFront;
							flag6 = true;
						}
						break;
					default:
					{
						float num13 = (float)(num6 * 16 + num11);
						if (num5 != num2 && trackState == Minecart.TrackState.NoTrack && vector4.Y > num13 && vector4.Y - num13 < 2f)
						{
							flag8 = false;
							trackState = Minecart.TrackState.AboveFront;
						}
						Minecart.TrackState trackState4 = (vector4.Y >= num13) ? ((vector4.Y <= num13) ? Minecart.TrackState.OnTrack : Minecart.TrackState.BelowTrack) : Minecart.TrackState.AboveTrack;
						if (num10 != -1)
						{
							float num14 = (float)(num6 * 16 + Minecart._tileHeight[num10][num7]);
							trackState3 = ((vector4.Y >= num14) ? ((vector4.Y <= num14) ? Minecart.TrackState.OnTrack : Minecart.TrackState.BelowTrack) : Minecart.TrackState.AboveTrack);
						}
						switch (trackState4)
						{
						case Minecart.TrackState.AboveTrack:
							switch (trackState3)
							{
							case Minecart.TrackState.AboveTrack:
								trackState2 = Minecart.TrackState.AboveTrack;
								break;
							case Minecart.TrackState.OnTrack:
								trackState2 = Minecart.TrackState.OnBack;
								break;
							case Minecart.TrackState.BelowTrack:
								trackState2 = Minecart.TrackState.AboveFront;
								break;
							default:
								trackState2 = Minecart.TrackState.AboveFront;
								break;
							}
							break;
						case Minecart.TrackState.OnTrack:
							trackState2 = ((trackState3 == Minecart.TrackState.OnTrack) ? Minecart.TrackState.OnTrack : Minecart.TrackState.OnFront);
							break;
						case Minecart.TrackState.BelowTrack:
							switch (trackState3)
							{
							case Minecart.TrackState.AboveTrack:
								trackState2 = Minecart.TrackState.AboveBack;
								break;
							case Minecart.TrackState.OnTrack:
								trackState2 = Minecart.TrackState.OnBack;
								break;
							case Minecart.TrackState.BelowTrack:
								trackState2 = Minecart.TrackState.BelowTrack;
								break;
							default:
								trackState2 = Minecart.TrackState.BelowTrack;
								break;
							}
							break;
						}
						break;
					}
					}
				}
				if (!flag8)
				{
					if (trackState != trackState2)
					{
						bool flag9 = false;
						if (flag7 || vector3.Y > 0f)
						{
							switch (trackState)
							{
							case Minecart.TrackState.AboveTrack:
								switch (trackState2)
								{
								case Minecart.TrackState.AboveTrack:
									trackState2 = Minecart.TrackState.OnTrack;
									break;
								case Minecart.TrackState.AboveFront:
									trackState2 = Minecart.TrackState.OnBack;
									break;
								case Minecart.TrackState.AboveBack:
									trackState2 = Minecart.TrackState.OnFront;
									break;
								}
								break;
							case Minecart.TrackState.OnTrack:
							{
								int num15 = Minecart._tileHeight[(int)tile.FrontTrack()][num7];
								int num16 = Minecart._tileHeight[(int)tile.BackTrack()][num7];
								trackState2 = (followDown ? ((num15 >= num16) ? Minecart.TrackState.OnFront : Minecart.TrackState.OnBack) : ((!followUp) ? Minecart.TrackState.OnFront : ((num15 >= num16) ? Minecart.TrackState.OnBack : Minecart.TrackState.OnFront)));
								flag9 = true;
								break;
							}
							case Minecart.TrackState.AboveFront:
								if (trackState2 == Minecart.TrackState.BelowTrack)
								{
									trackState2 = Minecart.TrackState.OnFront;
								}
								break;
							case Minecart.TrackState.AboveBack:
								if (trackState2 == Minecart.TrackState.BelowTrack)
								{
									trackState2 = Minecart.TrackState.OnBack;
								}
								break;
							case Minecart.TrackState.OnFront:
								trackState2 = Minecart.TrackState.OnFront;
								flag9 = true;
								break;
							case Minecart.TrackState.OnBack:
								trackState2 = Minecart.TrackState.OnBack;
								flag9 = true;
								break;
							}
							int num17 = -1;
							if (trackState2 != Minecart.TrackState.OnTrack && trackState2 != Minecart.TrackState.OnFront)
							{
								if (trackState2 == Minecart.TrackState.OnBack)
								{
									num17 = (int)tile.BackTrack();
								}
							}
							else
							{
								num17 = (int)tile.FrontTrack();
							}
							if (num17 != -1)
							{
								if (!flag9 && Velocity.Y > Player.defaultGravity)
								{
									int num18 = (int)(Position.Y / 16f);
									if (fallStart < num18 - 1)
									{
										delegatesData.MinecartLandingSound(Player, Position, Width, Height);
										Minecart.WheelSparks(delegatesData.MinecartDust, Position, Width, Height, 10);
									}
								}
								if (trackState == Minecart.TrackState.AboveFront && Minecart._trackType[num17] == 1)
								{
									flag3 = true;
								}
								vector3.Y = 0f;
								vector4.Y = (float)(num6 * 16 + Minecart._tileHeight[num17][num7]);
							}
						}
					}
				}
				else if (trackState2 == Minecart.TrackState.OnFront || trackState2 == Minecart.TrackState.OnBack || trackState2 == Minecart.TrackState.OnTrack)
				{
					if (flag && Minecart._trackType[(int)tile.FrontTrack()] == 1)
					{
						flag3 = true;
					}
					vector3.Y = 0f;
				}
				if (trackState2 == Minecart.TrackState.OnFront)
				{
					int num19 = (int)tile.FrontTrack();
					if (Minecart._trackType[num19] == 2 && lastBoost.X == 0f && lastBoost.Y == 0f)
					{
						lastBoost = new Vector2((float)num5, (float)num6);
						if (Minecart._boostLeft[num19])
						{
							result[4] = true;
						}
						else
						{
							result[5] = true;
						}
					}
				}
				num4 = num7;
				trackState = trackState2;
				num2 = num5;
				num3 = num6;
				if (num <= 0f)
				{
					break;
				}
				float num20 = vector4.X % 2f;
				float num21 = vector4.Y % 2f;
				float num22 = 3f;
				float num23 = 3f;
				if (vector3.X < 0f)
				{
					num22 = num20 + 0.125f;
				}
				else if (vector3.X > 0f)
				{
					num22 = 2f - num20;
				}
				if (vector3.Y < 0f)
				{
					num23 = num21 + 0.125f;
				}
				else if (vector3.Y > 0f)
				{
					num23 = 2f - num21;
				}
				if (num22 == 3f && num23 == 3f)
				{
					goto IL_813;
				}
				float num24 = Math.Abs(num22 / vector3.X);
				float num25 = Math.Abs(num23 / vector3.Y);
				float num26 = (num24 < num25) ? num24 : num25;
				if (num26 > num)
				{
					vector6 = vector3 * num;
					num = 0f;
				}
				else
				{
					vector6 = vector3 * num26;
					num -= num26;
				}
				vector4 += vector6;
			}
			if (lastBoost.X != (float)num2 || lastBoost.Y != (float)num3)
			{
				lastBoost = Vector2.Zero;
			}
			IL_813:
			if (flag3)
			{
				result[3] = true;
			}
			if (flag5)
			{
				Velocity.X = vector4.X - vector2.X;
				Velocity.Y = Player.defaultGravity;
			}
			else if (flag4)
			{
				result[2] = true;
				Velocity = vector5;
			}
			else if (result[1])
			{
				Velocity.X = 0f - Velocity.X;
				Position.X = vector4.X - Minecart._trackMagnetOffset.X - vector.X - Velocity.X;
				if (vector3.Y == 0f)
				{
					Velocity.Y = 0f;
				}
			}
			else
			{
				if (flag6)
				{
					Velocity.X = vector4.X - vector2.X;
				}
				if (vector3.Y == 0f)
				{
					Velocity.Y = 0f;
				}
			}
			Position.Y += vector4.Y - vector2.Y - Velocity.Y;
			Position.Y = (float)Math.Round((double)Position.Y, 2);
			if (trackState == Minecart.TrackState.OnTrack || trackState - Minecart.TrackState.OnFront <= 1)
			{
				result[0] = true;
			}
			return result;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0014B578 File Offset: 0x00149778
		public unsafe static bool FrameTrack(int i, int j, bool pound, bool mute = false)
		{
			if (Minecart._trackType == null)
			{
				return false;
			}
			Tile tile = Main.tile[i, j];
			if (tile == null)
			{
				tile = default(Tile);
				Main.tile[i, j] = tile;
			}
			if (mute && *tile.type != 314)
			{
				return false;
			}
			int nearbyTilesSetLookupIndex = Minecart.GetNearbyTilesSetLookupIndex(i, j);
			int num = (int)tile.FrontTrack();
			int num2 = (int)tile.BackTrack();
			int num3 = (num >= 0 && num < Minecart._trackType.Length) ? Minecart._trackType[num] : 0;
			int num4 = -1;
			int num5 = -1;
			int[] array = Minecart._trackSwitchOptions[nearbyTilesSetLookupIndex];
			if (array != null)
			{
				if (!pound)
				{
					int num6 = -1;
					int num7 = -1;
					bool flag = false;
					for (int k = 0; k < array.Length; k++)
					{
						int num8 = array[k];
						if (num2 == array[k])
						{
							num5 = k;
						}
						if (Minecart._trackType[num8] == num3)
						{
							if (Minecart._leftSideConnection[num8] == -1 || Minecart._rightSideConnection[num8] == -1)
							{
								if (num == array[k])
								{
									num4 = k;
									flag = true;
								}
								if (num6 == -1)
								{
									num6 = k;
								}
							}
							else
							{
								if (num == array[k])
								{
									num4 = k;
									flag = false;
								}
								if (num7 == -1)
								{
									num7 = k;
								}
							}
						}
					}
					if (num7 != -1)
					{
						if (num4 == -1 || flag)
						{
							num4 = num7;
						}
					}
					else
					{
						if (num4 == -1)
						{
							if (num3 == 1)
							{
								return false;
							}
							if (num3 == 2)
							{
								return false;
							}
							num4 = num6;
						}
						num5 = -1;
					}
				}
				else
				{
					for (int l = 0; l < array.Length; l++)
					{
						if (num == array[l])
						{
							num4 = l;
						}
						if (num2 == array[l])
						{
							num5 = l;
						}
					}
					int num9 = 0;
					int num10 = 0;
					for (int m = 0; m < array.Length; m++)
					{
						if (Minecart._trackType[array[m]] == num3)
						{
							if (Minecart._leftSideConnection[array[m]] == -1 || Minecart._rightSideConnection[array[m]] == -1)
							{
								num10++;
							}
							else
							{
								num9++;
							}
						}
					}
					if (num9 < 2 && num10 < 2)
					{
						return false;
					}
					bool flag2 = num9 == 0;
					bool flag3 = false;
					if (!flag2)
					{
						while (!flag3)
						{
							num5++;
							if (num5 >= array.Length)
							{
								num5 = -1;
								break;
							}
							if ((Minecart._leftSideConnection[array[num5]] != Minecart._leftSideConnection[array[num4]] || Minecart._rightSideConnection[array[num5]] != Minecart._rightSideConnection[array[num4]]) && Minecart._trackType[array[num5]] == num3 && Minecart._leftSideConnection[array[num5]] != -1 && Minecart._rightSideConnection[array[num5]] != -1)
							{
								flag3 = true;
							}
						}
					}
					if (!flag3)
					{
						for (;;)
						{
							num4++;
							if (num4 >= array.Length)
							{
								break;
							}
							if (Minecart._trackType[array[num4]] == num3 && (Minecart._leftSideConnection[array[num4]] == -1 || Minecart._rightSideConnection[array[num4]] == -1) == flag2)
							{
								goto IL_2F2;
							}
						}
						num4 = -1;
						do
						{
							num4++;
						}
						while (Minecart._trackType[array[num4]] != num3 || (Minecart._leftSideConnection[array[num4]] == -1 || Minecart._rightSideConnection[array[num4]] == -1) != flag2);
					}
				}
				IL_2F2:
				bool flag4 = false;
				if (num4 != -2)
				{
					if (num4 != -1)
					{
						if ((int)tile.FrontTrack() != array[num4])
						{
							flag4 = true;
						}
					}
					else if (tile.FrontTrack() != 0)
					{
						flag4 = true;
					}
				}
				else if (tile.FrontTrack() != Minecart._firstPressureFrame)
				{
					flag4 = true;
				}
				if (num5 == -1)
				{
					if (tile.BackTrack() != -1)
					{
						flag4 = true;
					}
				}
				else if ((int)tile.BackTrack() != array[num5])
				{
					flag4 = true;
				}
				if (num4 != -2)
				{
					if (num4 != -1)
					{
						tile.FrontTrack((short)array[num4]);
					}
					else
					{
						tile.FrontTrack(0);
					}
				}
				else
				{
					tile.FrontTrack(Minecart._firstPressureFrame);
				}
				if (num5 == -1)
				{
					tile.BackTrack(-1);
				}
				else
				{
					tile.BackTrack((short)array[num5]);
				}
				if (pound && flag4 && !mute)
				{
					WorldGen.KillTile(i, j, true, false, false);
				}
				return true;
			}
			if (pound)
			{
				return false;
			}
			tile.FrontTrack(0);
			tile.BackTrack(-1);
			return false;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0014B938 File Offset: 0x00149B38
		private unsafe static int GetNearbyTilesSetLookupIndex(int i, int j)
		{
			int num = 0;
			if (Main.tile[i - 1, j - 1] != null && *Main.tile[i - 1, j - 1].type == 314)
			{
				num++;
			}
			if (Main.tile[i - 1, j] != null && *Main.tile[i - 1, j].type == 314)
			{
				num += 2;
			}
			if (Main.tile[i - 1, j + 1] != null && *Main.tile[i - 1, j + 1].type == 314)
			{
				num += 4;
			}
			if (Main.tile[i + 1, j - 1] != null && *Main.tile[i + 1, j - 1].type == 314)
			{
				num += 8;
			}
			if (Main.tile[i + 1, j] != null && *Main.tile[i + 1, j].type == 314)
			{
				num += 16;
			}
			if (Main.tile[i + 1, j + 1] != null && *Main.tile[i + 1, j + 1].type == 314)
			{
				num += 32;
			}
			return num;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0014BAAC File Offset: 0x00149CAC
		public unsafe static bool GetOnTrack(int tileX, int tileY, ref Vector2 Position, int Width, int Height)
		{
			Tile tile = Main.tile[tileX, tileY];
			if (*tile.type != 314)
			{
				return false;
			}
			Vector2 vector;
			vector..ctor((float)(Width / 2) - 25f, (float)(Height / 2));
			Vector2 vector2 = Position + vector + Minecart._trackMagnetOffset;
			int num = (int)vector2.X % 16 / 2;
			int num2 = -1;
			int num3 = 0;
			for (int i = num; i < 8; i++)
			{
				num3 = Minecart._tileHeight[(int)(*tile.frameX)][i];
				if (num3 >= 0)
				{
					num2 = i;
					break;
				}
			}
			if (num2 == -1)
			{
				for (int num4 = num - 1; num4 >= 0; num4--)
				{
					num3 = Minecart._tileHeight[(int)(*tile.frameX)][num4];
					if (num3 >= 0)
					{
						num2 = num4;
						break;
					}
				}
			}
			if (num2 == -1)
			{
				return false;
			}
			vector2.X = (float)(tileX * 16 + num2 * 2);
			vector2.Y = (float)(tileY * 16 + num3);
			vector2 -= Minecart._trackMagnetOffset;
			vector2 -= vector;
			Position = vector2;
			return true;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0014BBC0 File Offset: 0x00149DC0
		public unsafe static bool OnTrack(Vector2 Position, int Width, int Height)
		{
			Vector2 vector = Position + new Vector2((float)(Width / 2) - 25f, (float)(Height / 2)) + Minecart._trackMagnetOffset;
			int num = (int)(vector.X / 16f);
			int num2 = (int)(vector.Y / 16f);
			return !(Main.tile[num, num2] == null) && *Main.tile[num, num2].type == 314;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0014BC40 File Offset: 0x00149E40
		public static float TrackRotation(Player player, ref float rotation, Vector2 Position, int Width, int Height, bool followDown, bool followUp, Mount.MountDelegatesData delegatesData)
		{
			Vector2 leftWheel;
			Vector2 rightWheel;
			Minecart.GetWheelsPositions(player, Position, Width, Height, followDown, followUp, delegatesData, out leftWheel, out rightWheel);
			float num = rightWheel.Y - leftWheel.Y;
			float num2 = rightWheel.X - leftWheel.X;
			float num3 = num / num2;
			float num5 = leftWheel.Y + (Position.X - leftWheel.X) * num3;
			float num4 = (Position.X - (float)((int)Position.X)) * num3;
			rotation = (float)Math.Atan2((double)num, (double)num2);
			return num5 - Position.Y + num4;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0014BCC4 File Offset: 0x00149EC4
		public static void GetWheelsPositions(Player player, Vector2 Position, int Width, int Height, bool followDown, bool followUp, Mount.MountDelegatesData delegatesData, out Vector2 leftWheel, out Vector2 rightWheel)
		{
			leftWheel = Position;
			rightWheel = Position;
			Vector2 lastBoost = Vector2.Zero;
			Vector2 Velocity;
			Velocity..ctor(-12f, 0f);
			Minecart.TrackCollision(player, ref leftWheel, ref Velocity, ref lastBoost, Width, Height, followDown, followUp, 0, true, delegatesData);
			leftWheel += Velocity;
			Velocity..ctor(12f, 0f);
			Minecart.TrackCollision(player, ref rightWheel, ref Velocity, ref lastBoost, Width, Height, followDown, followUp, 0, true, delegatesData);
			rightWheel += Velocity;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0014BD60 File Offset: 0x00149F60
		public static void HitTrackSwitch(Vector2 Position, int Width, int Height)
		{
			Vector2 magnetPosition = Minecart.GetMagnetPosition(Position, Width, Height);
			int num = (int)(magnetPosition.X / 16f);
			int num2 = (int)(magnetPosition.Y / 16f);
			Wiring.HitSwitch(num, num2);
			NetMessage.SendData(59, -1, -1, null, num, (float)num2, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0014BDB1 File Offset: 0x00149FB1
		public static Vector2 GetMagnetPosition(Vector2 Position, int Width, int Height)
		{
			new Vector2((float)(Width / 2) - 25f, (float)(Height / 2));
			return Position + new Vector2((float)(Width / 2) - 25f, (float)(Height / 2)) + Minecart._trackMagnetOffset;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0014BDEC File Offset: 0x00149FEC
		public static void FlipSwitchTrack(int i, int j)
		{
			Tile tileTrack = Main.tile[i, j];
			short num = tileTrack.FrontTrack();
			if (num == -1)
			{
				return;
			}
			int num2 = Minecart._trackType[(int)num];
			if (num2 != 0)
			{
				if (num2 != 2)
				{
					return;
				}
				Minecart.FrameTrack(i, j, true, true);
				NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
			}
			else if (tileTrack.BackTrack() != -1)
			{
				tileTrack.FrontTrack(tileTrack.BackTrack());
				tileTrack.BackTrack(num);
				NetMessage.SendTileSquare(-1, i, j, TileChangeType.None);
				return;
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0014BE5C File Offset: 0x0014A05C
		public unsafe static void TrackColors(int i, int j, Tile trackTile, out int frontColor, out int backColor)
		{
			if (*trackTile.type == 314)
			{
				frontColor = (int)trackTile.color();
				backColor = frontColor;
				if (*trackTile.frameY == -1)
				{
					return;
				}
				int num = Minecart._leftSideConnection[(int)(*trackTile.frameX)];
				int num2 = Minecart._rightSideConnection[(int)(*trackTile.frameX)];
				int num3 = Minecart._leftSideConnection[(int)(*trackTile.frameY)];
				int num4 = Minecart._rightSideConnection[(int)(*trackTile.frameY)];
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				int num8 = 0;
				for (int k = 0; k < 4; k++)
				{
					int num9;
					switch (k)
					{
					case 1:
						num9 = num2;
						break;
					case 2:
						num9 = num3;
						break;
					case 3:
						num9 = num4;
						break;
					default:
						num9 = num;
						break;
					}
					int num10;
					switch (num9)
					{
					case 0:
						num10 = -1;
						break;
					case 1:
						num10 = 0;
						break;
					case 2:
						num10 = 1;
						break;
					default:
						num10 = 0;
						break;
					}
					Tile tile = (k % 2 != 0) ? Main.tile[i + 1, j + num10] : Main.tile[i - 1, j + num10];
					int num11 = (int)((tile != null && tile.active() && *tile.type == 314) ? tile.color() : 0);
					switch (k)
					{
					case 1:
						num6 = num11;
						break;
					case 2:
						num7 = num11;
						break;
					case 3:
						num8 = num11;
						break;
					default:
						num5 = num11;
						break;
					}
				}
				if (num == num3)
				{
					if (num6 != 0)
					{
						frontColor = num6;
					}
					else if (num5 != 0)
					{
						frontColor = num5;
					}
					if (num8 != 0)
					{
						backColor = num8;
						return;
					}
					if (num7 != 0)
					{
						backColor = num7;
					}
					return;
				}
				else if (num2 == num4)
				{
					if (num5 != 0)
					{
						frontColor = num5;
					}
					else if (num6 != 0)
					{
						frontColor = num6;
					}
					if (num7 != 0)
					{
						backColor = num7;
						return;
					}
					if (num8 != 0)
					{
						backColor = num8;
					}
					return;
				}
				else
				{
					if (num6 == 0)
					{
						if (num5 != 0)
						{
							frontColor = num5;
						}
					}
					else if (num5 != 0)
					{
						frontColor = ((num2 <= num) ? num6 : num5);
					}
					if (num8 == 0)
					{
						if (num7 != 0)
						{
							backColor = num7;
							return;
						}
					}
					else if (num7 != 0)
					{
						backColor = ((num4 <= num3) ? num8 : num7);
						return;
					}
				}
			}
			else
			{
				frontColor = 0;
				backColor = 0;
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0014C05F File Offset: 0x0014A25F
		public static bool DrawLeftDecoration(int frameID)
		{
			return frameID >= 0 && frameID < 36 && Minecart._leftSideConnection[frameID] == 2;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0014C076 File Offset: 0x0014A276
		public static bool DrawRightDecoration(int frameID)
		{
			return frameID >= 0 && frameID < 36 && Minecart._rightSideConnection[frameID] == 2;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0014C08D File Offset: 0x0014A28D
		public static bool DrawBumper(int frameID)
		{
			return frameID >= 0 && frameID < 36 && (Minecart._tileHeight[frameID][0] == -1 || Minecart._tileHeight[frameID][7] == -1);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0014C0B4 File Offset: 0x0014A2B4
		public static bool DrawBouncyBumper(int frameID)
		{
			return frameID >= 0 && frameID < 36 && (Minecart._tileHeight[frameID][0] == -2 || Minecart._tileHeight[frameID][7] == -2);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0014C0E0 File Offset: 0x0014A2E0
		public unsafe static void PlaceTrack(Tile trackCache, int style)
		{
			trackCache.active(true);
			*trackCache.type = 314;
			*trackCache.frameY = -1;
			switch (style)
			{
			case 0:
				*trackCache.frameX = -1;
				return;
			case 1:
				*trackCache.frameX = Minecart._firstPressureFrame;
				return;
			case 2:
				*trackCache.frameX = Minecart._firstLeftBoostFrame;
				return;
			case 3:
				*trackCache.frameX = Minecart._firstRightBoostFrame;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0014C158 File Offset: 0x0014A358
		public unsafe static int GetTrackItem(Tile trackCache)
		{
			switch (Minecart._trackType[(int)(*trackCache.frameX)])
			{
			case 0:
				return 2340;
			case 1:
				return 2492;
			case 2:
				return 2739;
			default:
				return 0;
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0014C19C File Offset: 0x0014A39C
		public static Rectangle GetSourceRect(int frameID, int animationFrame = 0)
		{
			if (frameID < 0 || frameID >= 40)
			{
				return new Rectangle(0, 0, 0, 0);
			}
			Vector2 vector = Minecart._texturePosition[frameID];
			Rectangle result;
			result..ctor((int)vector.X, (int)vector.Y, 16, 16);
			if (frameID < 36 && Minecart._trackType[frameID] == 2)
			{
				result.Y += 18 * animationFrame;
			}
			return result;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0014C200 File Offset: 0x0014A400
		public static bool GetAreExpectationsForSidesMet(Point tileCoords, int? expectedYOffsetForLeft, int? expectedYOffsetForRight)
		{
			Tile tileTrack = Main.tile[tileCoords.X, tileCoords.Y];
			if (expectedYOffsetForLeft != null)
			{
				short num = tileTrack.FrontTrack();
				int num2 = Minecart.ConvertOffsetYToTrackConnectionValue(expectedYOffsetForLeft.Value);
				if (Minecart._leftSideConnection[(int)num] != num2)
				{
					return false;
				}
			}
			if (expectedYOffsetForRight != null)
			{
				short num3 = tileTrack.FrontTrack();
				int num4 = Minecart.ConvertOffsetYToTrackConnectionValue(expectedYOffsetForRight.Value);
				if (Minecart._rightSideConnection[(int)num3] != num4)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0014C27C File Offset: 0x0014A47C
		public static void TryFittingTileOrientation(Point tileCoords, int? expectedYOffsetForLeft, int? expectedYOffsetForRight)
		{
			int nearbyTilesSetLookupIndex = Minecart.GetNearbyTilesSetLookupIndex(tileCoords.X, tileCoords.Y);
			int[] array = Minecart._trackSwitchOptions[nearbyTilesSetLookupIndex];
			if (array == null)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(tileCoords);
			int num = Minecart._trackType[(int)tileSafely.FrontTrack()];
			int? num2 = null;
			int[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				int num3 = array2[i];
				int num6 = Minecart._leftSideConnection[num3];
				int num7 = Minecart._rightSideConnection[num3];
				int num8 = Minecart._trackType[num3];
				if (expectedYOffsetForLeft == null)
				{
					goto IL_8A;
				}
				int num4 = Minecart.ConvertOffsetYToTrackConnectionValue(expectedYOffsetForLeft.Value);
				if (Minecart._leftSideConnection[num3] == num4)
				{
					goto IL_8A;
				}
				IL_C3:
				i++;
				continue;
				IL_8A:
				if (expectedYOffsetForRight != null)
				{
					int num5 = Minecart.ConvertOffsetYToTrackConnectionValue(expectedYOffsetForRight.Value);
					if (Minecart._rightSideConnection[num3] != num5)
					{
						goto IL_C3;
					}
				}
				if (Minecart._trackType[num3] == num)
				{
					num2 = new int?(num3);
					break;
				}
				goto IL_C3;
			}
			if (num2 != null)
			{
				tileSafely.FrontTrack((short)num2.Value);
				NetMessage.SendTileSquare(-1, tileCoords.X, tileCoords.Y, TileChangeType.None);
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0014C387 File Offset: 0x0014A587
		private static int ConvertOffsetYToTrackConnectionValue(int offsetY)
		{
			if (offsetY == -1)
			{
				return 0;
			}
			if (offsetY != 1)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0014C398 File Offset: 0x0014A598
		private static int ConvertTrackConnectionValueToOffsetY(int trackConnectionValue)
		{
			if (trackConnectionValue == 0)
			{
				return -1;
			}
			if (trackConnectionValue != 2)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0014C3A8 File Offset: 0x0014A5A8
		public static void WheelSparks(Action<Vector2> DustAction, Vector2 Position, int Width, int Height, int sparkCount)
		{
			Vector2 vector;
			vector..ctor((float)(Width / 2) - 25f, (float)(Height / 2));
			Vector2 obj = Position + vector + Minecart._trackMagnetOffset;
			for (int i = 0; i < sparkCount; i++)
			{
				DustAction(obj);
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0014C3F0 File Offset: 0x0014A5F0
		private unsafe static short FrontTrack(this Tile tileTrack)
		{
			return *tileTrack.frameX;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0014C3FA File Offset: 0x0014A5FA
		private unsafe static void FrontTrack(this Tile tileTrack, short trackID)
		{
			*tileTrack.frameX = trackID;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0014C405 File Offset: 0x0014A605
		private unsafe static short BackTrack(this Tile tileTrack)
		{
			return *tileTrack.frameY;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0014C40F File Offset: 0x0014A60F
		private unsafe static void BackTrack(this Tile tileTrack, short trackID)
		{
			*tileTrack.frameY = trackID;
		}

		// Token: 0x04000791 RID: 1937
		private const int TotalFrames = 36;

		// Token: 0x04000792 RID: 1938
		public const int LeftDownDecoration = 36;

		// Token: 0x04000793 RID: 1939
		public const int RightDownDecoration = 37;

		// Token: 0x04000794 RID: 1940
		public const int BouncyBumperDecoration = 38;

		// Token: 0x04000795 RID: 1941
		public const int RegularBumperDecoration = 39;

		// Token: 0x04000796 RID: 1942
		public const int Flag_OnTrack = 0;

		// Token: 0x04000797 RID: 1943
		public const int Flag_BouncyBumper = 1;

		// Token: 0x04000798 RID: 1944
		public const int Flag_UsedRamp = 2;

		// Token: 0x04000799 RID: 1945
		public const int Flag_HitSwitch = 3;

		// Token: 0x0400079A RID: 1946
		public const int Flag_BoostLeft = 4;

		// Token: 0x0400079B RID: 1947
		public const int Flag_BoostRight = 5;

		// Token: 0x0400079C RID: 1948
		private const int NoConnection = -1;

		// Token: 0x0400079D RID: 1949
		private const int TopConnection = 0;

		// Token: 0x0400079E RID: 1950
		private const int MiddleConnection = 1;

		// Token: 0x0400079F RID: 1951
		private const int BottomConnection = 2;

		// Token: 0x040007A0 RID: 1952
		private const int BumperEnd = -1;

		// Token: 0x040007A1 RID: 1953
		private const int BouncyEnd = -2;

		// Token: 0x040007A2 RID: 1954
		private const int RampEnd = -3;

		// Token: 0x040007A3 RID: 1955
		private const int OpenEnd = -4;

		// Token: 0x040007A4 RID: 1956
		public const float BoosterSpeed = 4f;

		// Token: 0x040007A5 RID: 1957
		private const int Type_Normal = 0;

		// Token: 0x040007A6 RID: 1958
		private const int Type_Pressure = 1;

		// Token: 0x040007A7 RID: 1959
		private const int Type_Booster = 2;

		// Token: 0x040007A8 RID: 1960
		private static Vector2 _trackMagnetOffset = new Vector2(25f, 26f);

		// Token: 0x040007A9 RID: 1961
		private const float MinecartTextureWidth = 50f;

		// Token: 0x040007AA RID: 1962
		private static int[] _leftSideConnection;

		// Token: 0x040007AB RID: 1963
		private static int[] _rightSideConnection;

		// Token: 0x040007AC RID: 1964
		private static int[] _trackType;

		// Token: 0x040007AD RID: 1965
		private static bool[] _boostLeft;

		// Token: 0x040007AE RID: 1966
		private static Vector2[] _texturePosition;

		// Token: 0x040007AF RID: 1967
		private static short _firstPressureFrame;

		// Token: 0x040007B0 RID: 1968
		private static short _firstLeftBoostFrame;

		// Token: 0x040007B1 RID: 1969
		private static short _firstRightBoostFrame;

		// Token: 0x040007B2 RID: 1970
		private static int[][] _trackSwitchOptions;

		// Token: 0x040007B3 RID: 1971
		private static int[][] _tileHeight;

		// Token: 0x020007B3 RID: 1971
		private enum TrackState
		{
			// Token: 0x04006645 RID: 26181
			NoTrack = -1,
			// Token: 0x04006646 RID: 26182
			AboveTrack,
			// Token: 0x04006647 RID: 26183
			OnTrack,
			// Token: 0x04006648 RID: 26184
			BelowTrack,
			// Token: 0x04006649 RID: 26185
			AboveFront,
			// Token: 0x0400664A RID: 26186
			AboveBack,
			// Token: 0x0400664B RID: 26187
			OnFront,
			// Token: 0x0400664C RID: 26188
			OnBack
		}
	}
}
