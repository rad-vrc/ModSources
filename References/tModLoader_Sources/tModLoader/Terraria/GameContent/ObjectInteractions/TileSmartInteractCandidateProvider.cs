using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020005D1 RID: 1489
	public class TileSmartInteractCandidateProvider : ISmartInteractCandidateProvider
	{
		// Token: 0x060042E7 RID: 17127 RVA: 0x005FAE30 File Offset: 0x005F9030
		public void ClearSelfAndPrepareForCheck()
		{
			Main.SmartInteractX = -1;
			Main.SmartInteractY = -1;
			Main.TileInteractionLX = -1;
			Main.TileInteractionHX = -1;
			Main.TileInteractionLY = -1;
			Main.TileInteractionHY = -1;
			Main.SmartInteractTileCoords.Clear();
			Main.SmartInteractTileCoordsSelected.Clear();
			this.targets.Clear();
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x005FAE80 File Offset: 0x005F9080
		public unsafe bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate)
		{
			candidate = null;
			Point point = settings.mousevec.ToTileCoordinates();
			this.FillPotentialTargetTiles(settings);
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			if (this.targets.Count > 0)
			{
				float num5 = -1f;
				Tuple<int, int> tuple = this.targets[0];
				for (int i = 0; i < this.targets.Count; i++)
				{
					float num6 = Vector2.Distance(new Vector2((float)this.targets[i].Item1, (float)this.targets[i].Item2) * 16f + Vector2.One * 8f, settings.mousevec);
					if (num5 == -1f || num6 <= num5)
					{
						num5 = num6;
						tuple = this.targets[i];
					}
				}
				if (Collision.InTileBounds(tuple.Item1, tuple.Item2, settings.LX, settings.LY, settings.HX, settings.HY))
				{
					num = tuple.Item1;
					num2 = tuple.Item2;
				}
			}
			bool flag = false;
			int j = 0;
			while (j < this.targets.Count)
			{
				int item = this.targets[j].Item1;
				int item2 = this.targets[j].Item2;
				Tile tile = Main.tile[item, item2];
				int num7 = 0;
				int num8 = 0;
				int num9 = 18;
				int num10 = 18;
				int num11 = 2;
				ushort num14 = *tile.type;
				if (num14 <= 338)
				{
					if (num14 <= 104)
					{
						if (num14 <= 29)
						{
							if (num14 <= 11)
							{
								if (num14 != 10)
								{
									if (num14 == 11)
									{
										goto IL_4F6;
									}
								}
								else
								{
									num7 = 1;
									num8 = 3;
									num11 = 0;
								}
							}
							else
							{
								if (num14 == 15)
								{
									goto IL_4B4;
								}
								if (num14 == 21)
								{
									goto IL_4E3;
								}
								if (num14 == 29)
								{
									goto IL_4DB;
								}
							}
						}
						else if (num14 <= 89)
						{
							if (num14 == 55)
							{
								goto IL_4E3;
							}
							if (num14 == 79)
							{
								goto IL_4EB;
							}
							switch (num14)
							{
							case 85:
								goto IL_4E3;
							case 88:
								num7 = 3;
								num8 = 1;
								num11 = 0;
								break;
							case 89:
								goto IL_51F;
							}
						}
						else
						{
							if (num14 == 97)
							{
								goto IL_4E3;
							}
							if (num14 == 102)
							{
								goto IL_550;
							}
							if (num14 == 104)
							{
								num7 = 2;
								num8 = 5;
							}
						}
					}
					else if (num14 <= 207)
					{
						if (num14 <= 136)
						{
							if (num14 == 125 || num14 == 132)
							{
								goto IL_4E3;
							}
							if (num14 == 136)
							{
								goto IL_49B;
							}
						}
						else
						{
							if (num14 == 139)
							{
								goto IL_4EB;
							}
							if (num14 == 144)
							{
								goto IL_49B;
							}
							if (num14 == 207)
							{
								num7 = 2;
								num8 = 4;
								num11 = 0;
							}
						}
					}
					else
					{
						if (num14 <= 237)
						{
							if (num14 == 209)
							{
								num7 = 4;
								num8 = 3;
								num11 = 0;
								goto IL_55E;
							}
							switch (num14)
							{
							case 212:
								num7 = 4;
								num8 = 3;
								goto IL_55E;
							case 213:
							case 214:
								goto IL_55E;
							case 215:
								goto IL_51F;
							case 216:
								break;
							default:
								if (num14 != 237)
								{
									goto IL_55E;
								}
								goto IL_51F;
							}
						}
						else
						{
							if (num14 == 287 || num14 == 335)
							{
								goto IL_4E3;
							}
							if (num14 != 338)
							{
								goto IL_55E;
							}
						}
						num7 = 1;
						num8 = 2;
					}
				}
				else
				{
					if (num14 <= 475)
					{
						if (num14 <= 410)
						{
							if (num14 <= 356)
							{
								if (num14 != 354)
								{
									if (num14 != 356)
									{
										goto IL_55E;
									}
									goto IL_4F6;
								}
							}
							else
							{
								if (num14 == 377)
								{
									goto IL_51F;
								}
								switch (num14)
								{
								case 386:
									goto IL_4E3;
								case 387:
									goto IL_4DB;
								case 388:
								case 389:
									num7 = 1;
									num8 = 5;
									goto IL_55E;
								default:
									if (num14 != 410)
									{
										goto IL_55E;
									}
									goto IL_4F6;
								}
							}
						}
						else if (num14 <= 441)
						{
							if (num14 != 411 && num14 != 425 && num14 != 441)
							{
								goto IL_55E;
							}
							goto IL_4E3;
						}
						else if (num14 != 455)
						{
							switch (num14)
							{
							case 463:
								goto IL_550;
							case 464:
								num7 = 5;
								num8 = 4;
								goto IL_55E;
							case 465:
							case 466:
							case 469:
								goto IL_55E;
							case 467:
							case 468:
								goto IL_4E3;
							case 470:
								goto IL_4F6;
							default:
								if (num14 != 475)
								{
									goto IL_55E;
								}
								goto IL_550;
							}
						}
					}
					else if (num14 <= 509)
					{
						if (num14 <= 491)
						{
							if (num14 == 480)
							{
								goto IL_4F6;
							}
							if (num14 == 487)
							{
								num7 = 4;
								num8 = 2;
								num11 = 0;
								goto IL_55E;
							}
							if (num14 != 491)
							{
								goto IL_55E;
							}
						}
						else
						{
							if (num14 == 494)
							{
								goto IL_49B;
							}
							if (num14 == 497)
							{
								goto IL_4B4;
							}
							if (num14 != 509)
							{
								goto IL_55E;
							}
							goto IL_4F6;
						}
					}
					else if (num14 <= 597)
					{
						if (num14 - 510 <= 1)
						{
							goto IL_4EB;
						}
						if (num14 == 573)
						{
							goto IL_4E3;
						}
						if (num14 != 597)
						{
							goto IL_55E;
						}
						goto IL_550;
					}
					else
					{
						if (num14 == 621)
						{
							goto IL_4E3;
						}
						if (num14 - 657 > 1 && num14 != 663)
						{
							goto IL_55E;
						}
						goto IL_4F6;
					}
					num7 = 3;
					num8 = 3;
					num11 = 0;
				}
				IL_55E:
				TileLoader.ModifySmartInteractCoords((int)(*tile.type), ref num7, ref num8, ref num9, ref num10, ref num11);
				if (num7 != 0 && num8 != 0)
				{
					int num12 = item - (int)(*tile.frameX) % (num9 * num7) / num9;
					int num13 = item2 - (int)(*tile.frameY) % (num10 * num8 + num11) / num10;
					bool flag2 = Collision.InTileBounds(num, num2, num12, num13, num12 + num7 - 1, num13 + num8 - 1);
					bool flag3 = Collision.InTileBounds(point.X, point.Y, num12, num13, num12 + num7 - 1, num13 + num8 - 1);
					if (flag3)
					{
						num3 = point.X;
						num4 = point.Y;
					}
					if (!settings.FullInteraction)
					{
						flag2 = (flag2 && flag3);
					}
					if (flag)
					{
						flag2 = false;
					}
					for (int k = num12; k < num12 + num7; k++)
					{
						for (int l = num13; l < num13 + num8; l++)
						{
							Point item3;
							item3..ctor(k, l);
							if (!Main.SmartInteractTileCoords.Contains(item3))
							{
								if (flag2)
								{
									Main.SmartInteractTileCoordsSelected.Add(item3);
								}
								if (flag2 || settings.FullInteraction)
								{
									Main.SmartInteractTileCoords.Add(item3);
								}
							}
						}
					}
					if (!flag && flag2)
					{
						flag = true;
					}
				}
				j++;
				continue;
				IL_49B:
				num7 = 1;
				num8 = 1;
				num11 = 0;
				goto IL_55E;
				IL_4B4:
				num7 = 1;
				num8 = 2;
				num11 = 4;
				goto IL_55E;
				IL_4DB:
				num7 = 2;
				num8 = 1;
				goto IL_55E;
				IL_4E3:
				num7 = 2;
				num8 = 2;
				goto IL_55E;
				IL_4EB:
				num7 = 2;
				num8 = 2;
				num11 = 0;
				goto IL_55E;
				IL_4F6:
				num7 = 2;
				num8 = 3;
				num11 = 0;
				goto IL_55E;
				IL_51F:
				num7 = 3;
				num8 = 2;
				goto IL_55E;
				IL_550:
				num7 = 3;
				num8 = 4;
				goto IL_55E;
			}
			if (settings.DemandOnlyZeroDistanceTargets)
			{
				if (num3 != -1 && num4 != -1)
				{
					this._candidate.Reuse(true, 0f, num3, num4, settings.LX - 10, settings.LY - 10, settings.HX + 10, settings.HY + 10);
					candidate = this._candidate;
					return true;
				}
				return false;
			}
			else
			{
				if (num != -1 && num2 != -1)
				{
					float distanceFromCursor = new Rectangle(num * 16, num2 * 16, 16, 16).ClosestPointInRect(settings.mousevec).Distance(settings.mousevec);
					this._candidate.Reuse(false, distanceFromCursor, num, num2, settings.LX - 10, settings.LY - 10, settings.HX + 10, settings.HY + 10);
					candidate = this._candidate;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x005FB600 File Offset: 0x005F9800
		private unsafe void FillPotentialTargetTiles(SmartInteractScanSettings settings)
		{
			for (int i = settings.LX; i <= settings.HX; i++)
			{
				for (int j = settings.LY; j <= settings.HY; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!(tile == null) && tile.active())
					{
						ushort num = *tile.type;
						if (num <= 287)
						{
							if (num <= 104)
							{
								if (num <= 55)
								{
									if (num <= 15)
									{
										if (num - 10 <= 1)
										{
											goto IL_2E1;
										}
										if (num == 15)
										{
											goto IL_2F8;
										}
									}
									else if (num == 21 || num == 29 || num == 55)
									{
										goto IL_2E1;
									}
								}
								else if (num <= 89)
								{
									if (num == 79 || num == 85 || num - 88 <= 1)
									{
										goto IL_2E1;
									}
								}
								else if (num == 97 || num == 102 || num == 104)
								{
									goto IL_2E1;
								}
							}
							else if (num <= 144)
							{
								if (num <= 132)
								{
									if (num == 125 || num == 132)
									{
										goto IL_2E1;
									}
								}
								else if (num == 136 || num == 139 || num == 144)
								{
									goto IL_2E1;
								}
							}
							else if (num <= 212)
							{
								if (num == 207 || num == 209)
								{
									goto IL_2E1;
								}
								if (num == 212)
								{
									if (settings.player.HasItem(949))
									{
										this.targets.Add(new Tuple<int, int>(i, j));
									}
								}
							}
							else
							{
								if (num - 215 <= 1)
								{
									goto IL_2E1;
								}
								if (num != 237)
								{
									if (num == 287)
									{
										goto IL_2E1;
									}
								}
								else if (settings.player.HasItem(1293))
								{
									this.targets.Add(new Tuple<int, int>(i, j));
								}
							}
						}
						else if (num <= 470)
						{
							if (num <= 377)
							{
								if (num <= 338)
								{
									if (num == 335 || num == 338)
									{
										goto IL_2E1;
									}
								}
								else
								{
									if (num == 354)
									{
										goto IL_2E1;
									}
									if (num != 356)
									{
										if (num == 377)
										{
											goto IL_2E1;
										}
									}
									else if (!Main.fastForwardTimeToDawn && (Main.netMode == 1 || Main.sundialCooldown == 0))
									{
										this.targets.Add(new Tuple<int, int>(i, j));
									}
								}
							}
							else if (num <= 425)
							{
								if (num - 386 <= 3 || num - 410 <= 1 || num == 425)
								{
									goto IL_2E1;
								}
							}
							else
							{
								if (num == 441 || num == 455)
								{
									goto IL_2E1;
								}
								switch (num)
								{
								case 463:
								case 464:
								case 467:
								case 468:
								case 470:
									goto IL_2E1;
								}
							}
						}
						else if (num <= 497)
						{
							if (num <= 487)
							{
								if (num == 475 || num == 480 || num == 487)
								{
									goto IL_2E1;
								}
							}
							else
							{
								if (num == 491 || num == 494)
								{
									goto IL_2E1;
								}
								if (num == 497)
								{
									goto IL_2F8;
								}
							}
						}
						else if (num <= 597)
						{
							if (num - 509 <= 2 || num == 573 || num == 597)
							{
								goto IL_2E1;
							}
						}
						else
						{
							if (num == 621 || num - 657 <= 1)
							{
								goto IL_2E1;
							}
							if (num == 663)
							{
								if (!Main.fastForwardTimeToDusk && (Main.netMode == 1 || Main.moondialCooldown == 0))
								{
									this.targets.Add(new Tuple<int, int>(i, j));
								}
							}
						}
						IL_3C4:
						if (TileLoader.HasSmartInteract(i, j, (int)(*tile.type), settings))
						{
							this.targets.Add(new Tuple<int, int>(i, j));
							goto IL_3E8;
						}
						goto IL_3E8;
						IL_2E1:
						this.targets.Add(new Tuple<int, int>(i, j));
						goto IL_3C4;
						IL_2F8:
						if (settings.player.IsWithinSnappngRangeToTile(i, j, 40))
						{
							this.targets.Add(new Tuple<int, int>(i, j));
							goto IL_3C4;
						}
						goto IL_3C4;
					}
					IL_3E8:;
				}
			}
		}

		// Token: 0x040059DF RID: 23007
		private List<Tuple<int, int>> targets = new List<Tuple<int, int>>();

		// Token: 0x040059E0 RID: 23008
		private TileSmartInteractCandidateProvider.ReusableCandidate _candidate = new TileSmartInteractCandidateProvider.ReusableCandidate();

		// Token: 0x02000C6A RID: 3178
		private class ReusableCandidate : ISmartInteractCandidate
		{
			// Token: 0x17000969 RID: 2409
			// (get) Token: 0x06005FF9 RID: 24569 RVA: 0x006D1AF1 File Offset: 0x006CFCF1
			// (set) Token: 0x06005FFA RID: 24570 RVA: 0x006D1AF9 File Offset: 0x006CFCF9
			public float DistanceFromCursor { get; private set; }

			// Token: 0x06005FFB RID: 24571 RVA: 0x006D1B02 File Offset: 0x006CFD02
			public void Reuse(bool strictSettings, float distanceFromCursor, int AimedX, int AimedY, int LX, int LY, int HX, int HY)
			{
				this.DistanceFromCursor = distanceFromCursor;
				this._strictSettings = strictSettings;
				this._aimedX = AimedX;
				this._aimedY = AimedY;
				this._lx = LX;
				this._ly = LY;
				this._hx = HX;
				this._hy = HY;
			}

			// Token: 0x06005FFC RID: 24572 RVA: 0x006D1B44 File Offset: 0x006CFD44
			public void WinCandidacy()
			{
				Main.SmartInteractX = this._aimedX;
				Main.SmartInteractY = this._aimedY;
				if (this._strictSettings)
				{
					Main.SmartInteractShowingFake = (Main.SmartInteractTileCoords.Count > 0);
				}
				else
				{
					Main.SmartInteractShowingGenuine = true;
				}
				Main.TileInteractionLX = this._lx - 10;
				Main.TileInteractionLY = this._ly - 10;
				Main.TileInteractionHX = this._hx + 10;
				Main.TileInteractionHY = this._hy + 10;
			}

			// Token: 0x0400798D RID: 31117
			private bool _strictSettings;

			// Token: 0x0400798E RID: 31118
			private int _aimedX;

			// Token: 0x0400798F RID: 31119
			private int _aimedY;

			// Token: 0x04007990 RID: 31120
			private int _hx;

			// Token: 0x04007991 RID: 31121
			private int _hy;

			// Token: 0x04007992 RID: 31122
			private int _lx;

			// Token: 0x04007993 RID: 31123
			private int _ly;
		}
	}
}
