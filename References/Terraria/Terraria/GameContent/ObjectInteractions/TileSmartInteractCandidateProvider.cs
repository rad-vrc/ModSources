using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x02000264 RID: 612
	public class TileSmartInteractCandidateProvider : ISmartInteractCandidateProvider
	{
		// Token: 0x06001F8F RID: 8079 RVA: 0x00515474 File Offset: 0x00513674
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

		// Token: 0x06001F90 RID: 8080 RVA: 0x005154C4 File Offset: 0x005136C4
		public bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate)
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
				ushort type = tile.type;
				if (type <= 338)
				{
					if (type <= 104)
					{
						if (type <= 29)
						{
							if (type <= 11)
							{
								if (type != 10)
								{
									if (type == 11)
									{
										goto IL_4F5;
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
								if (type == 15)
								{
									goto IL_4B3;
								}
								if (type == 21)
								{
									goto IL_4E2;
								}
								if (type == 29)
								{
									goto IL_4DA;
								}
							}
						}
						else if (type <= 89)
						{
							if (type == 55)
							{
								goto IL_4E2;
							}
							if (type == 79)
							{
								goto IL_4EA;
							}
							switch (type)
							{
							case 85:
								goto IL_4E2;
							case 88:
								num7 = 3;
								num8 = 1;
								num11 = 0;
								break;
							case 89:
								goto IL_51E;
							}
						}
						else
						{
							if (type == 97)
							{
								goto IL_4E2;
							}
							if (type == 102)
							{
								goto IL_54F;
							}
							if (type == 104)
							{
								num7 = 2;
								num8 = 5;
							}
						}
					}
					else if (type <= 207)
					{
						if (type <= 136)
						{
							if (type == 125 || type == 132)
							{
								goto IL_4E2;
							}
							if (type == 136)
							{
								goto IL_49A;
							}
						}
						else
						{
							if (type == 139)
							{
								goto IL_4EA;
							}
							if (type == 144)
							{
								goto IL_49A;
							}
							if (type == 207)
							{
								num7 = 2;
								num8 = 4;
								num11 = 0;
							}
						}
					}
					else
					{
						if (type <= 237)
						{
							if (type == 209)
							{
								num7 = 4;
								num8 = 3;
								num11 = 0;
								goto IL_55D;
							}
							switch (type)
							{
							case 212:
								num7 = 4;
								num8 = 3;
								goto IL_55D;
							case 213:
							case 214:
								goto IL_55D;
							case 215:
								goto IL_51E;
							case 216:
								break;
							default:
								if (type != 237)
								{
									goto IL_55D;
								}
								goto IL_51E;
							}
						}
						else
						{
							if (type == 287 || type == 335)
							{
								goto IL_4E2;
							}
							if (type != 338)
							{
								goto IL_55D;
							}
						}
						num7 = 1;
						num8 = 2;
					}
				}
				else
				{
					if (type <= 475)
					{
						if (type <= 410)
						{
							if (type <= 356)
							{
								if (type != 354)
								{
									if (type != 356)
									{
										goto IL_55D;
									}
									goto IL_4F5;
								}
							}
							else
							{
								if (type == 377)
								{
									goto IL_51E;
								}
								switch (type)
								{
								case 386:
									goto IL_4E2;
								case 387:
									goto IL_4DA;
								case 388:
								case 389:
									num7 = 1;
									num8 = 5;
									goto IL_55D;
								default:
									if (type != 410)
									{
										goto IL_55D;
									}
									goto IL_4F5;
								}
							}
						}
						else if (type <= 441)
						{
							if (type != 411 && type != 425 && type != 441)
							{
								goto IL_55D;
							}
							goto IL_4E2;
						}
						else if (type != 455)
						{
							switch (type)
							{
							case 463:
								goto IL_54F;
							case 464:
								num7 = 5;
								num8 = 4;
								goto IL_55D;
							case 465:
							case 466:
							case 469:
								goto IL_55D;
							case 467:
							case 468:
								goto IL_4E2;
							case 470:
								goto IL_4F5;
							default:
								if (type != 475)
								{
									goto IL_55D;
								}
								goto IL_54F;
							}
						}
					}
					else if (type <= 509)
					{
						if (type <= 491)
						{
							if (type == 480)
							{
								goto IL_4F5;
							}
							if (type == 487)
							{
								num7 = 4;
								num8 = 2;
								num11 = 0;
								goto IL_55D;
							}
							if (type != 491)
							{
								goto IL_55D;
							}
						}
						else
						{
							if (type == 494)
							{
								goto IL_49A;
							}
							if (type == 497)
							{
								goto IL_4B3;
							}
							if (type != 509)
							{
								goto IL_55D;
							}
							goto IL_4F5;
						}
					}
					else if (type <= 597)
					{
						if (type - 510 <= 1)
						{
							goto IL_4EA;
						}
						if (type == 573)
						{
							goto IL_4E2;
						}
						if (type != 597)
						{
							goto IL_55D;
						}
						goto IL_54F;
					}
					else
					{
						if (type == 621)
						{
							goto IL_4E2;
						}
						if (type - 657 > 1 && type != 663)
						{
							goto IL_55D;
						}
						goto IL_4F5;
					}
					num7 = 3;
					num8 = 3;
					num11 = 0;
				}
				IL_55D:
				if (num7 != 0 && num8 != 0)
				{
					int num12 = item - (int)tile.frameX % (num9 * num7) / num9;
					int num13 = item2 - (int)tile.frameY % (num10 * num8 + num11) / num10;
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
							Point item3 = new Point(k, l);
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
				IL_49A:
				num7 = 1;
				num8 = 1;
				num11 = 0;
				goto IL_55D;
				IL_4B3:
				num7 = 1;
				num8 = 2;
				num11 = 4;
				goto IL_55D;
				IL_4DA:
				num7 = 2;
				num8 = 1;
				goto IL_55D;
				IL_4E2:
				num7 = 2;
				num8 = 2;
				goto IL_55D;
				IL_4EA:
				num7 = 2;
				num8 = 2;
				num11 = 0;
				goto IL_55D;
				IL_4F5:
				num7 = 2;
				num8 = 3;
				num11 = 0;
				goto IL_55D;
				IL_51E:
				num7 = 3;
				num8 = 2;
				goto IL_55D;
				IL_54F:
				num7 = 3;
				num8 = 4;
				goto IL_55D;
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

		// Token: 0x06001F91 RID: 8081 RVA: 0x00515C28 File Offset: 0x00513E28
		private void FillPotentialTargetTiles(SmartInteractScanSettings settings)
		{
			for (int i = settings.LX; i <= settings.HX; i++)
			{
				for (int j = settings.LY; j <= settings.HY; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.active())
					{
						ushort type = tile.type;
						if (type <= 287)
						{
							if (type <= 104)
							{
								if (type <= 55)
								{
									if (type <= 15)
									{
										if (type - 10 > 1)
										{
											if (type != 15)
											{
												goto IL_3BB;
											}
											goto IL_2EF;
										}
									}
									else if (type != 21 && type != 29 && type != 55)
									{
										goto IL_3BB;
									}
								}
								else if (type <= 89)
								{
									if (type != 79 && type != 85 && type - 88 > 1)
									{
										goto IL_3BB;
									}
								}
								else if (type != 97 && type != 102 && type != 104)
								{
									goto IL_3BB;
								}
							}
							else if (type <= 144)
							{
								if (type <= 132)
								{
									if (type != 125 && type != 132)
									{
										goto IL_3BB;
									}
								}
								else if (type != 136 && type != 139 && type != 144)
								{
									goto IL_3BB;
								}
							}
							else if (type <= 212)
							{
								if (type != 207 && type != 209)
								{
									if (type != 212)
									{
										goto IL_3BB;
									}
									if (settings.player.HasItem(949))
									{
										this.targets.Add(new Tuple<int, int>(i, j));
										goto IL_3BB;
									}
									goto IL_3BB;
								}
							}
							else if (type - 215 > 1)
							{
								if (type != 237)
								{
									if (type != 287)
									{
										goto IL_3BB;
									}
								}
								else
								{
									if (settings.player.HasItem(1293))
									{
										this.targets.Add(new Tuple<int, int>(i, j));
										goto IL_3BB;
									}
									goto IL_3BB;
								}
							}
						}
						else if (type <= 470)
						{
							if (type <= 377)
							{
								if (type <= 338)
								{
									if (type != 335 && type != 338)
									{
										goto IL_3BB;
									}
								}
								else if (type != 354)
								{
									if (type != 356)
									{
										if (type != 377)
										{
											goto IL_3BB;
										}
									}
									else
									{
										if (!Main.fastForwardTimeToDawn && (Main.netMode == 1 || Main.sundialCooldown == 0))
										{
											this.targets.Add(new Tuple<int, int>(i, j));
											goto IL_3BB;
										}
										goto IL_3BB;
									}
								}
							}
							else if (type <= 425)
							{
								if (type - 386 > 3 && type - 410 > 1 && type != 425)
								{
									goto IL_3BB;
								}
							}
							else if (type != 441 && type != 455)
							{
								switch (type)
								{
								case 463:
								case 464:
								case 467:
								case 468:
								case 470:
									break;
								case 465:
								case 466:
								case 469:
									goto IL_3BB;
								default:
									goto IL_3BB;
								}
							}
						}
						else if (type <= 497)
						{
							if (type <= 487)
							{
								if (type != 475 && type != 480 && type != 487)
								{
									goto IL_3BB;
								}
							}
							else if (type != 491 && type != 494)
							{
								if (type != 497)
								{
									goto IL_3BB;
								}
								goto IL_2EF;
							}
						}
						else if (type <= 597)
						{
							if (type - 509 > 2 && type != 573 && type != 597)
							{
								goto IL_3BB;
							}
						}
						else if (type != 621 && type - 657 > 1)
						{
							if (type != 663)
							{
								goto IL_3BB;
							}
							if (!Main.fastForwardTimeToDusk && (Main.netMode == 1 || Main.moondialCooldown == 0))
							{
								this.targets.Add(new Tuple<int, int>(i, j));
								goto IL_3BB;
							}
							goto IL_3BB;
						}
						this.targets.Add(new Tuple<int, int>(i, j));
						goto IL_3BB;
						IL_2EF:
						if (settings.player.IsWithinSnappngRangeToTile(i, j, 40))
						{
							this.targets.Add(new Tuple<int, int>(i, j));
						}
					}
					IL_3BB:;
				}
			}
		}

		// Token: 0x0400468D RID: 18061
		private List<Tuple<int, int>> targets = new List<Tuple<int, int>>();

		// Token: 0x0400468E RID: 18062
		private TileSmartInteractCandidateProvider.ReusableCandidate _candidate = new TileSmartInteractCandidateProvider.ReusableCandidate();

		// Token: 0x0200063E RID: 1598
		private class ReusableCandidate : ISmartInteractCandidate
		{
			// Token: 0x060033CD RID: 13261 RVA: 0x00607482 File Offset: 0x00605682
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

			// Token: 0x170003CD RID: 973
			// (get) Token: 0x060033CE RID: 13262 RVA: 0x006074C1 File Offset: 0x006056C1
			// (set) Token: 0x060033CF RID: 13263 RVA: 0x006074C9 File Offset: 0x006056C9
			public float DistanceFromCursor { get; private set; }

			// Token: 0x060033D0 RID: 13264 RVA: 0x006074D4 File Offset: 0x006056D4
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

			// Token: 0x04006146 RID: 24902
			private bool _strictSettings;

			// Token: 0x04006147 RID: 24903
			private int _aimedX;

			// Token: 0x04006148 RID: 24904
			private int _aimedY;

			// Token: 0x04006149 RID: 24905
			private int _hx;

			// Token: 0x0400614A RID: 24906
			private int _hy;

			// Token: 0x0400614B RID: 24907
			private int _lx;

			// Token: 0x0400614C RID: 24908
			private int _ly;
		}
	}
}
