using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000030 RID: 48
	public class Gore
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00022CA0 File Offset: 0x00020EA0
		public float Width
		{
			get
			{
				if (TextureAssets.Gore[this.type].IsLoaded)
				{
					return this.scale * (float)this.Frame.GetSourceRectangle(TextureAssets.Gore[this.type].Value).Width;
				}
				return 1f;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00022CF0 File Offset: 0x00020EF0
		public float Height
		{
			get
			{
				if (TextureAssets.Gore[this.type].IsLoaded)
				{
					return this.scale * (float)this.Frame.GetSourceRectangle(TextureAssets.Gore[this.type].Value).Height;
				}
				return 1f;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00022D40 File Offset: 0x00020F40
		public Rectangle AABBRectangle
		{
			get
			{
				if (TextureAssets.Gore[this.type].IsLoaded)
				{
					Rectangle sourceRectangle = this.Frame.GetSourceRectangle(TextureAssets.Gore[this.type].Value);
					return new Rectangle((int)this.position.X, (int)this.position.Y, (int)((float)sourceRectangle.Width * this.scale), (int)((float)sourceRectangle.Height * this.scale));
				}
				return new Rectangle(0, 0, 1, 1);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00022DC2 File Offset: 0x00020FC2
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00022DCF File Offset: 0x00020FCF
		[Old("Please use Frame instead.")]
		public byte frame
		{
			get
			{
				return this.Frame.CurrentRow;
			}
			set
			{
				this.Frame.CurrentRow = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00022DDD File Offset: 0x00020FDD
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00022DEC File Offset: 0x00020FEC
		[Old("Please use Frame instead.")]
		public byte numFrames
		{
			get
			{
				return this.Frame.RowCount;
			}
			set
			{
				SpriteFrame spriteFrame2 = new SpriteFrame(this.Frame.ColumnCount, value)
				{
					CurrentColumn = this.Frame.CurrentColumn,
					CurrentRow = this.Frame.CurrentRow
				};
				this.Frame = spriteFrame2;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00022E3C File Offset: 0x0002103C
		private void UpdateAmbientFloorCloud()
		{
			this.timeLeft -= GoreID.Sets.DisappearSpeed[this.type];
			if (this.timeLeft <= 0)
			{
				this.active = false;
				return;
			}
			bool flag = false;
			Point point = (this.position + new Vector2(15f, 0f)).ToTileCoordinates();
			Tile tile = Main.tile[point.X, point.Y];
			Tile tile2 = Main.tile[point.X, point.Y + 1];
			Tile tile3 = Main.tile[point.X, point.Y + 2];
			if (tile == null || tile2 == null || tile3 == null)
			{
				this.active = false;
				return;
			}
			if (WorldGen.SolidTile(tile) || (!WorldGen.SolidTile(tile2) && !WorldGen.SolidTile(tile3)))
			{
				flag = true;
			}
			if (this.timeLeft <= 30)
			{
				flag = true;
			}
			this.velocity.X = 0.4f * Main.WindForVisuals;
			if (!flag)
			{
				if (this.alpha > 220)
				{
					this.alpha--;
				}
			}
			else
			{
				this.alpha++;
				if (this.alpha >= 255)
				{
					this.active = false;
					return;
				}
			}
			this.position += this.velocity;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00022F9C File Offset: 0x0002119C
		private void UpdateAmbientAirborneCloud()
		{
			this.timeLeft -= GoreID.Sets.DisappearSpeed[this.type];
			if (this.timeLeft <= 0)
			{
				this.active = false;
				return;
			}
			bool flag = false;
			Point point = (this.position + new Vector2(15f, 0f)).ToTileCoordinates();
			this.rotation = this.velocity.ToRotation();
			Tile tile = Main.tile[point.X, point.Y];
			if (tile == null)
			{
				this.active = false;
				return;
			}
			if (WorldGen.SolidTile(tile))
			{
				flag = true;
			}
			if (this.timeLeft <= 60)
			{
				flag = true;
			}
			if (!flag)
			{
				if (this.alpha > 240 && Main.rand.Next(5) == 0)
				{
					this.alpha--;
				}
			}
			else
			{
				if (Main.rand.Next(5) == 0)
				{
					this.alpha++;
				}
				if (this.alpha >= 255)
				{
					this.active = false;
					return;
				}
			}
			this.position += this.velocity;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000230B8 File Offset: 0x000212B8
		private void UpdateFogMachineCloud()
		{
			this.timeLeft -= GoreID.Sets.DisappearSpeed[this.type];
			if (this.timeLeft <= 0)
			{
				this.active = false;
				return;
			}
			bool flag = false;
			Point point = (this.position + new Vector2(15f, 0f)).ToTileCoordinates();
			if (WorldGen.SolidTile(Main.tile[point.X, point.Y]))
			{
				flag = true;
			}
			if (this.timeLeft <= 240)
			{
				flag = true;
			}
			if (!flag)
			{
				if (this.alpha > 225 && Main.rand.Next(2) == 0)
				{
					this.alpha--;
				}
			}
			else
			{
				if (Main.rand.Next(2) == 0)
				{
					this.alpha++;
				}
				if (this.alpha >= 255)
				{
					this.active = false;
					return;
				}
			}
			this.position += this.velocity;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000231B4 File Offset: 0x000213B4
		private void UpdateLightningBunnySparks()
		{
			if (this.frameCounter == 0)
			{
				this.frameCounter = 1;
				this.Frame.CurrentRow = (byte)Main.rand.Next(3);
			}
			this.timeLeft -= GoreID.Sets.DisappearSpeed[this.type];
			if (this.timeLeft <= 0)
			{
				this.active = false;
				return;
			}
			this.alpha = (int)MathHelper.Lerp(255f, 0f, (float)this.timeLeft / 15f);
			float num = (255f - (float)this.alpha) / 255f;
			num *= this.scale;
			Lighting.AddLight(this.position + new Vector2(this.Width / 2f, this.Height / 2f), num * 0.4f, num, num);
			this.position += this.velocity;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000232A0 File Offset: 0x000214A0
		private unsafe float ChumFloatingChunk_GetWaterLine(int X, int Y)
		{
			float result = this.position.Y + this.Height;
			if (Main.tile[X, Y - 1] == null)
			{
				Main.tile[X, Y - 1] = default(Tile);
			}
			if (Main.tile[X, Y] == null)
			{
				Main.tile[X, Y] = default(Tile);
			}
			if (Main.tile[X, Y + 1] == null)
			{
				Main.tile[X, Y + 1] = default(Tile);
			}
			if (*Main.tile[X, Y - 1].liquid > 0)
			{
				result = (float)(Y * 16);
				result -= (float)(*Main.tile[X, Y - 1].liquid / 16);
			}
			else if (*Main.tile[X, Y].liquid > 0)
			{
				result = (float)((Y + 1) * 16);
				result -= (float)(*Main.tile[X, Y].liquid / 16);
			}
			else if (*Main.tile[X, Y + 1].liquid > 0)
			{
				result = (float)((Y + 2) * 16);
				result -= (float)(*Main.tile[X, Y + 1].liquid / 16);
			}
			return result;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00023404 File Offset: 0x00021604
		private bool DeactivateIfOutsideOfWorld()
		{
			Point point = this.position.ToTileCoordinates();
			if (!WorldGen.InWorld(point.X, point.Y, 0))
			{
				this.active = false;
				return true;
			}
			if (Main.tile[point.X, point.Y] == null)
			{
				this.active = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00023464 File Offset: 0x00021664
		public unsafe void Update()
		{
			if (Main.netMode == 2 || !this.active)
			{
				return;
			}
			if (this.ModGore != null && !this.ModGore.Update(this))
			{
				return;
			}
			GoreLoader.SetupUpdateType(this);
			if (this.sticky)
			{
				if (this.DeactivateIfOutsideOfWorld())
				{
					return;
				}
				float num = this.velocity.Length();
				if (num > 32f)
				{
					this.velocity *= 32f / num;
				}
			}
			switch (GoreID.Sets.SpecialAI[this.type])
			{
			case 4:
				this.UpdateAmbientFloorCloud();
				GoreLoader.TakeDownUpdateType(this);
				return;
			case 5:
				this.UpdateAmbientAirborneCloud();
				GoreLoader.TakeDownUpdateType(this);
				return;
			case 6:
				this.UpdateFogMachineCloud();
				GoreLoader.TakeDownUpdateType(this);
				return;
			case 7:
				this.UpdateLightningBunnySparks();
				GoreLoader.TakeDownUpdateType(this);
				return;
			default:
			{
				if ((this.type == 1217 || this.type == 1218) && this.frameCounter == 0)
				{
					this.frameCounter = 1;
					this.Frame.CurrentRow = (byte)Main.rand.Next(3);
				}
				bool flag = this.type >= 1024 && this.type <= 1026;
				if (this.type >= 276 && this.type <= 282)
				{
					this.velocity.X = this.velocity.X * 0.98f;
					this.velocity.Y = this.velocity.Y * 0.98f;
					if (this.velocity.Y < this.scale)
					{
						this.velocity.Y = this.velocity.Y + 0.05f;
					}
					if ((double)this.velocity.Y > 0.1)
					{
						if (this.velocity.X > 0f)
						{
							this.rotation += 0.01f;
						}
						else
						{
							this.rotation -= 0.01f;
						}
					}
				}
				if (this.type >= 570 && this.type <= 572)
				{
					this.scale -= 0.001f;
					if ((double)this.scale <= 0.01)
					{
						this.scale = 0.01f;
						this.timeLeft = 0;
					}
					this.sticky = false;
					this.rotation = this.velocity.X * 0.1f;
				}
				else if (GoreID.Sets.LiquidDroplet[this.type])
				{
					if (this.type == 943 || (this.type >= 1160 && this.type <= 1162))
					{
						this.alpha = 0;
					}
					else if ((double)this.position.Y < Main.worldSurface * 16.0 + 8.0)
					{
						this.alpha = 0;
					}
					else
					{
						this.alpha = 100;
					}
					int num2 = 4;
					this.frameCounter += 1;
					if (this.frame <= 4)
					{
						int num3 = (int)(this.position.X / 16f);
						int num4 = (int)(this.position.Y / 16f) - 1;
						if (WorldGen.InWorld(num3, num4, 0) && !Main.tile[num3, num4].active())
						{
							this.active = false;
						}
						if (this.frame == 0)
						{
							num2 = 24 + Main.rand.Next(256);
						}
						if (this.frame == 1)
						{
							num2 = 24 + Main.rand.Next(256);
						}
						if (this.frame == 2)
						{
							num2 = 24 + Main.rand.Next(256);
						}
						if (this.frame == 3)
						{
							num2 = 24 + Main.rand.Next(96);
						}
						if (this.frame == 5)
						{
							num2 = 16 + Main.rand.Next(64);
						}
						if (this.type == 716)
						{
							num2 *= 2;
						}
						if (this.type == 717)
						{
							num2 *= 4;
						}
						if ((this.type == 943 || (this.type >= 1160 && this.type <= 1162)) && this.frame < 6)
						{
							num2 = 4;
						}
						if ((int)this.frameCounter >= num2)
						{
							this.frameCounter = 0;
							byte b = this.frame;
							this.frame = b + 1;
							if (this.frame == 5)
							{
								int num5 = Gore.NewGore(this.position, this.velocity, (this.realType != 0) ? this.realType : this.type, 1f);
								Main.gore[num5].frame = 9;
								Main.gore[num5].velocity *= 0f;
							}
						}
					}
					else if (this.frame <= 6)
					{
						num2 = 8;
						if (this.type == 716)
						{
							num2 *= 2;
						}
						if (this.type == 717)
						{
							num2 *= 3;
						}
						if ((int)this.frameCounter >= num2)
						{
							this.frameCounter = 0;
							byte b = this.frame;
							this.frame = b + 1;
							if (this.frame == 7)
							{
								this.active = false;
							}
						}
					}
					else if (this.frame <= 9)
					{
						num2 = 6;
						if (this.type == 716)
						{
							num2 = (int)((double)num2 * 1.5);
							this.velocity.Y = this.velocity.Y + 0.175f;
						}
						else if (this.type == 717)
						{
							num2 *= 2;
							this.velocity.Y = this.velocity.Y + 0.15f;
						}
						else if (this.type == 943)
						{
							num2 = (int)((double)num2 * 1.5);
							this.velocity.Y = this.velocity.Y + 0.2f;
						}
						else
						{
							this.velocity.Y = this.velocity.Y + 0.2f;
						}
						if ((double)this.velocity.Y < 0.5)
						{
							this.velocity.Y = 0.5f;
						}
						if (this.velocity.Y > 12f)
						{
							this.velocity.Y = 12f;
						}
						if ((int)this.frameCounter >= num2)
						{
							this.frameCounter = 0;
							byte b = this.frame;
							this.frame = b + 1;
						}
						if (this.frame > 9)
						{
							this.frame = 7;
						}
					}
					else
					{
						if (this.type == 716)
						{
							num2 *= 2;
						}
						else if (this.type == 717)
						{
							num2 *= 6;
						}
						this.velocity.Y = this.velocity.Y + 0.1f;
						if ((int)this.frameCounter >= num2)
						{
							this.frameCounter = 0;
							byte b = this.frame;
							this.frame = b + 1;
						}
						this.velocity *= 0f;
						if (this.frame > 14)
						{
							this.active = false;
						}
					}
				}
				else if (this.type == 11 || this.type == 12 || this.type == 13 || this.type == 61 || this.type == 62 || this.type == 63 || this.type == 99 || this.type == 220 || this.type == 221 || this.type == 222 || (this.type >= 375 && this.type <= 377) || (this.type >= 435 && this.type <= 437) || (this.type >= 861 && this.type <= 862))
				{
					this.velocity.Y = this.velocity.Y * 0.98f;
					this.velocity.X = this.velocity.X * 0.98f;
					this.scale -= 0.007f;
					if ((double)this.scale < 0.1)
					{
						this.scale = 0.1f;
						this.alpha = 255;
					}
				}
				else if (this.type == 16 || this.type == 17)
				{
					this.velocity.Y = this.velocity.Y * 0.98f;
					this.velocity.X = this.velocity.X * 0.98f;
					this.scale -= 0.01f;
					if ((double)this.scale < 0.1)
					{
						this.scale = 0.1f;
						this.alpha = 255;
					}
				}
				else if (this.type == 1201)
				{
					if (this.frameCounter == 0)
					{
						this.frameCounter = 1;
						this.Frame.CurrentRow = (byte)Main.rand.Next(4);
					}
					this.scale -= 0.002f;
					if ((double)this.scale < 0.1)
					{
						this.scale = 0.1f;
						this.alpha = 255;
					}
					this.rotation += this.velocity.X * 0.1f;
					int num6 = (int)(this.position.X + 6f) / 16;
					int num7 = (int)(this.position.Y - 6f) / 16;
					if (Main.tile[num6, num7] == null || *Main.tile[num6, num7].liquid <= 0)
					{
						this.velocity.Y = this.velocity.Y + 0.2f;
						if (this.velocity.Y < 0f)
						{
							this.velocity *= 0.92f;
						}
					}
					else
					{
						this.velocity.Y = this.velocity.Y + 0.005f;
						float num8 = this.velocity.Length();
						if (num8 > 1f)
						{
							this.velocity *= 0.1f;
						}
						else if (num8 > 0.1f)
						{
							this.velocity *= 0.98f;
						}
					}
				}
				else if (this.type == 1208)
				{
					if (this.frameCounter == 0)
					{
						this.frameCounter = 1;
						this.Frame.CurrentRow = (byte)Main.rand.Next(4);
					}
					Vector2 vector = this.position + new Vector2(this.Width, this.Height) / 2f;
					int num9 = (int)vector.X / 16;
					int num10 = (int)vector.Y / 16;
					bool flag2 = Main.tile[num9, num10] != null && *Main.tile[num9, num10].liquid > 0;
					this.scale -= 0.0005f;
					if ((double)this.scale < 0.1)
					{
						this.scale = 0.1f;
						this.alpha = 255;
					}
					this.rotation += this.velocity.X * 0.1f;
					if (flag2)
					{
						this.velocity.X = this.velocity.X * 0.9f;
						int num11 = (int)vector.X / 16;
						int num12 = (int)(vector.Y / 16f);
						float num41 = this.position.Y / 16f;
						int num13 = (int)((this.position.Y + this.Height) / 16f);
						if (Main.tile[num11, num12] == null)
						{
							Main.tile[num11, num12] = default(Tile);
						}
						if (Main.tile[num11, num13] == null)
						{
							Main.tile[num11, num13] = default(Tile);
						}
						if (this.velocity.Y > 0f)
						{
							this.velocity.Y = this.velocity.Y * 0.5f;
						}
						num11 = (int)(vector.X / 16f);
						num12 = (int)(vector.Y / 16f);
						float num14 = this.ChumFloatingChunk_GetWaterLine(num11, num12);
						if (vector.Y > num14)
						{
							this.velocity.Y = this.velocity.Y - 0.1f;
							if (this.velocity.Y < -8f)
							{
								this.velocity.Y = -8f;
							}
							if (vector.Y + this.velocity.Y < num14)
							{
								this.velocity.Y = num14 - vector.Y;
							}
						}
						else
						{
							this.velocity.Y = num14 - vector.Y;
						}
						bool flag3 = !flag2 && this.velocity.Length() < 0.8f;
						int maxValue = flag2 ? 270 : 15;
						if (Main.rand.Next(maxValue) == 0 && !flag3)
						{
							Gore gore = Gore.NewGoreDirect(this.position + Vector2.UnitY * 6f, Vector2.Zero, 1201, this.scale * 0.7f);
							if (flag2)
							{
								gore.velocity = Vector2.UnitX * Main.rand.NextFloatDirection() * 0.5f + Vector2.UnitY * Main.rand.NextFloat();
							}
							else if (gore.velocity.Y < 0f)
							{
								gore.velocity.Y = 0f - gore.velocity.Y;
							}
						}
					}
					else
					{
						if (this.velocity.Y == 0f)
						{
							this.velocity.X = this.velocity.X * 0.95f;
						}
						this.velocity.X = this.velocity.X * 0.98f;
						this.velocity.Y = this.velocity.Y + 0.3f;
						if (this.velocity.Y > 15.9f)
						{
							this.velocity.Y = 15.9f;
						}
					}
				}
				else if (this.type == 331)
				{
					this.alpha += 5;
					this.velocity.Y = this.velocity.Y * 0.95f;
					this.velocity.X = this.velocity.X * 0.95f;
					this.rotation = this.velocity.X * 0.1f;
				}
				else if (GoreID.Sets.SpecialAI[this.type] == 3)
				{
					byte b = this.frameCounter + 1;
					this.frameCounter = b;
					if (b >= 8 && this.velocity.Y > 0.2f)
					{
						this.frameCounter = 0;
						int num15 = (int)(this.Frame.CurrentRow / 4);
						b = this.Frame.CurrentRow + 1;
						this.Frame.CurrentRow = b;
						if ((int)b >= 4 + num15 * 4)
						{
							this.Frame.CurrentRow = (byte)(num15 * 4);
						}
					}
				}
				else if (GoreID.Sets.SpecialAI[this.type] != 1 && GoreID.Sets.SpecialAI[this.type] != 2)
				{
					if (this.type >= 907 && this.type <= 909)
					{
						this.rotation = 0f;
						this.velocity.X = this.velocity.X * 0.98f;
						if (this.velocity.Y > 0f && this.velocity.Y < 0.001f)
						{
							this.velocity.Y = -0.5f + Main.rand.NextFloat() * -3f;
						}
						if (this.velocity.Y > -1f)
						{
							this.velocity.Y = this.velocity.Y - 0.1f;
						}
						if (this.scale < 1f)
						{
							this.scale += 0.1f;
						}
						byte b = this.frameCounter + 1;
						this.frameCounter = b;
						if (b >= 8)
						{
							this.frameCounter = 0;
							b = this.frame + 1;
							this.frame = b;
							if (b >= 3)
							{
								this.frame = 0;
							}
						}
					}
					else if (this.type == 1218)
					{
						if (this.timeLeft > 8)
						{
							this.timeLeft = 8;
						}
						this.velocity.X = this.velocity.X * 0.95f;
						if (Math.Abs(this.velocity.X) <= 0.1f)
						{
							this.velocity.X = 0f;
						}
						if (this.alpha < 100 && this.velocity.Length() > 0f && Main.rand.Next(5) == 0)
						{
							int num16 = 246;
							switch (this.Frame.CurrentRow)
							{
							case 0:
								num16 = 246;
								break;
							case 1:
								num16 = 245;
								break;
							case 2:
								num16 = 244;
								break;
							}
							int num17 = Dust.NewDust(this.position + new Vector2(6f, 4f), 4, 4, num16, 0f, 0f, 0, default(Color), 1f);
							Main.dust[num17].alpha = 255;
							Main.dust[num17].scale = 0.8f;
							Main.dust[num17].velocity = Vector2.Zero;
						}
						this.velocity.Y = this.velocity.Y + 0.2f;
						this.rotation = 0f;
					}
					else if (this.type < 411 || this.type > 430)
					{
						this.velocity.Y = this.velocity.Y + 0.2f;
						this.rotation += this.velocity.X * 0.05f;
					}
					else if (GoreID.Sets.SpecialAI[this.type] != 3)
					{
						this.rotation += this.velocity.X * 0.1f;
					}
				}
				if (this.type >= 580 && this.type <= 582)
				{
					this.rotation = 0f;
					this.velocity.X = this.velocity.X * 0.95f;
				}
				if (GoreID.Sets.SpecialAI[this.type] == 2)
				{
					if (this.timeLeft < 60)
					{
						this.alpha += Main.rand.Next(1, 7);
					}
					else if (this.alpha > 100)
					{
						this.alpha -= Main.rand.Next(1, 4);
					}
					if (this.alpha < 0)
					{
						this.alpha = 0;
					}
					if (this.alpha > 255)
					{
						this.timeLeft = 0;
					}
					this.velocity.X = (this.velocity.X * 50f + Main.WindForVisuals * 2f + (float)Main.rand.Next(-10, 11) * 0.1f) / 51f;
					float num18 = 0f;
					if (this.velocity.X < 0f)
					{
						num18 = this.velocity.X * 0.2f;
					}
					this.velocity.Y = (this.velocity.Y * 50f + -0.35f + num18 + (float)Main.rand.Next(-10, 11) * 0.2f) / 51f;
					this.rotation = this.velocity.X * 0.6f;
					float num19 = -1f;
					if (TextureAssets.Gore[this.type].IsLoaded)
					{
						Rectangle rectangle;
						rectangle..ctor((int)this.position.X, (int)this.position.Y, (int)((float)TextureAssets.Gore[this.type].Width() * this.scale), (int)((float)TextureAssets.Gore[this.type].Height() * this.scale));
						for (int i = 0; i < 255; i++)
						{
							if (Main.player[i].active && !Main.player[i].dead)
							{
								Rectangle value;
								value..ctor((int)Main.player[i].position.X, (int)Main.player[i].position.Y, Main.player[i].width, Main.player[i].height);
								if (rectangle.Intersects(value))
								{
									this.timeLeft = 0;
									num19 = Main.player[i].velocity.Length();
									break;
								}
							}
						}
					}
					if (this.timeLeft > 0)
					{
						if (Main.rand.Next(2) == 0)
						{
							this.timeLeft--;
						}
						if (Main.rand.Next(50) == 0)
						{
							this.timeLeft -= 5;
						}
						if (Main.rand.Next(100) == 0)
						{
							this.timeLeft -= 10;
						}
					}
					else
					{
						this.alpha = 255;
						if (TextureAssets.Gore[this.type].IsLoaded && num19 != -1f)
						{
							float num20 = (float)TextureAssets.Gore[this.type].Width() * this.scale * 0.8f;
							float x = this.position.X;
							float y = this.position.Y;
							float num21 = (float)TextureAssets.Gore[this.type].Width() * this.scale;
							float num22 = (float)TextureAssets.Gore[this.type].Height() * this.scale;
							int num23 = 31;
							int j = 0;
							while ((float)j < num20)
							{
								int num24 = Dust.NewDust(new Vector2(x, y), (int)num21, (int)num22, num23, 0f, 0f, 0, default(Color), 1f);
								Main.dust[num24].velocity *= (1f + num19) / 3f;
								Main.dust[num24].noGravity = true;
								Main.dust[num24].alpha = 100;
								Main.dust[num24].scale = this.scale;
								j++;
							}
						}
					}
				}
				if (this.type >= 411 && this.type <= 430)
				{
					this.alpha = 50;
					this.velocity.X = (this.velocity.X * 50f + Main.WindForVisuals * 2f + (float)Main.rand.Next(-10, 11) * 0.1f) / 51f;
					this.velocity.Y = (this.velocity.Y * 50f + -0.25f + (float)Main.rand.Next(-10, 11) * 0.2f) / 51f;
					this.rotation = this.velocity.X * 0.3f;
					if (TextureAssets.Gore[this.type].IsLoaded)
					{
						Rectangle rectangle2;
						rectangle2..ctor((int)this.position.X, (int)this.position.Y, (int)((float)TextureAssets.Gore[this.type].Width() * this.scale), (int)((float)TextureAssets.Gore[this.type].Height() * this.scale));
						for (int k = 0; k < 255; k++)
						{
							if (Main.player[k].active && !Main.player[k].dead)
							{
								Rectangle value2;
								value2..ctor((int)Main.player[k].position.X, (int)Main.player[k].position.Y, Main.player[k].width, Main.player[k].height);
								if (rectangle2.Intersects(value2))
								{
									this.timeLeft = 0;
								}
							}
						}
						if (Collision.SolidCollision(this.position, (int)((float)TextureAssets.Gore[this.type].Width() * this.scale), (int)((float)TextureAssets.Gore[this.type].Height() * this.scale)))
						{
							this.timeLeft = 0;
						}
					}
					if (this.timeLeft > 0)
					{
						if (Main.rand.Next(2) == 0)
						{
							this.timeLeft--;
						}
						if (Main.rand.Next(50) == 0)
						{
							this.timeLeft -= 5;
						}
						if (Main.rand.Next(100) == 0)
						{
							this.timeLeft -= 10;
						}
					}
					else
					{
						this.alpha = 255;
						if (TextureAssets.Gore[this.type].IsLoaded)
						{
							float num25 = (float)TextureAssets.Gore[this.type].Width() * this.scale * 0.8f;
							float x2 = this.position.X;
							float y2 = this.position.Y;
							float num26 = (float)TextureAssets.Gore[this.type].Width() * this.scale;
							float num27 = (float)TextureAssets.Gore[this.type].Height() * this.scale;
							int num28 = 176;
							if (this.type >= 416 && this.type <= 420)
							{
								num28 = 177;
							}
							if (this.type >= 421 && this.type <= 425)
							{
								num28 = 178;
							}
							if (this.type >= 426 && this.type <= 430)
							{
								num28 = 179;
							}
							int l = 0;
							while ((float)l < num25)
							{
								int num29 = Dust.NewDust(new Vector2(x2, y2), (int)num26, (int)num27, num28, 0f, 0f, 0, default(Color), 1f);
								Main.dust[num29].noGravity = true;
								Main.dust[num29].alpha = 100;
								Main.dust[num29].scale = this.scale;
								l++;
							}
						}
					}
				}
				else if (GoreID.Sets.SpecialAI[this.type] != 3 && GoreID.Sets.SpecialAI[this.type] != 1)
				{
					if (GoreID.Sets.LiquidDroplet[this.type])
					{
						if (this.type == 716)
						{
							float num30 = 0.6f;
							num30 = ((this.frame == 0) ? (num30 * 0.1f) : ((this.frame == 1) ? (num30 * 0.2f) : ((this.frame == 2) ? (num30 * 0.3f) : ((this.frame == 3) ? (num30 * 0.4f) : ((this.frame == 4) ? (num30 * 0.5f) : ((this.frame == 5) ? (num30 * 0.4f) : ((this.frame == 6) ? (num30 * 0.2f) : ((this.frame <= 9) ? (num30 * 0.5f) : ((this.frame == 10) ? (num30 * 0.5f) : ((this.frame == 11) ? (num30 * 0.4f) : ((this.frame == 12) ? (num30 * 0.3f) : ((this.frame == 13) ? (num30 * 0.2f) : ((this.frame != 14) ? 0f : (num30 * 0.1f))))))))))))));
							float num31 = 1f * num30;
							float num32 = 0.5f * num30;
							float num33 = 0.1f * num30;
							Lighting.AddLight(this.position + new Vector2(8f, 8f), num31, num32, num33);
						}
						Vector2 vector2 = this.velocity;
						this.velocity = Collision.TileCollision(this.position, this.velocity, 16, 14, false, false, 1);
						if (this.velocity != vector2)
						{
							if (this.frame < 10)
							{
								this.frame = 10;
								this.frameCounter = 0;
								if (this.type != 716 && this.type != 717 && this.type != 943 && (this.type < 1160 || this.type > 1162))
								{
									SoundEngine.PlaySound(39, (int)this.position.X + 8, (int)this.position.Y + 8, Main.rand.Next(2), 1f, 0f);
								}
							}
						}
						else if (Collision.WetCollision(this.position + this.velocity, 16, 14))
						{
							if (this.frame < 10)
							{
								this.frame = 10;
								this.frameCounter = 0;
								if (this.type != 716 && this.type != 717 && this.type != 943 && (this.type < 1160 || this.type > 1162))
								{
									SoundEngine.PlaySound(39, (int)this.position.X + 8, (int)this.position.Y + 8, 2, 1f, 0f);
								}
								((WaterShaderData)Filters.Scene["WaterDistortion"].GetShader()).QueueRipple(this.position + new Vector2(8f, 8f), 1f, RippleShape.Square, 0f);
							}
							int num34 = (int)(this.position.X + 8f) / 16;
							int num35 = (int)(this.position.Y + 14f) / 16;
							if (Main.tile[num34, num35] != null && *Main.tile[num34, num35].liquid > 0)
							{
								this.velocity *= 0f;
								this.position.Y = (float)(num35 * 16 - (int)(*Main.tile[num34, num35].liquid / 16));
							}
						}
					}
					else if (this.sticky)
					{
						int num36 = 32;
						if (TextureAssets.Gore[this.type].IsLoaded)
						{
							num36 = TextureAssets.Gore[this.type].Width();
							if (TextureAssets.Gore[this.type].Height() < num36)
							{
								num36 = TextureAssets.Gore[this.type].Height();
							}
						}
						if (flag)
						{
							num36 = 4;
						}
						num36 = (int)((float)num36 * 0.9f);
						Vector2 vector4 = this.velocity;
						this.velocity = Collision.TileCollision(this.position, this.velocity, (int)((float)num36 * this.scale), (int)((float)num36 * this.scale), false, false, 1);
						if (this.velocity.Y == 0f)
						{
							if (flag)
							{
								this.velocity.X = this.velocity.X * 0.94f;
							}
							else
							{
								this.velocity.X = this.velocity.X * 0.97f;
							}
							if ((double)this.velocity.X > -0.01 && (double)this.velocity.X < 0.01)
							{
								this.velocity.X = 0f;
							}
						}
						if (this.timeLeft > 0)
						{
							this.timeLeft -= GoreID.Sets.DisappearSpeed[this.type];
						}
						else
						{
							this.alpha += GoreID.Sets.DisappearSpeedAlpha[this.type];
						}
					}
					else
					{
						this.alpha += 2 * GoreID.Sets.DisappearSpeedAlpha[this.type];
					}
				}
				if (this.type >= 907 && this.type <= 909)
				{
					int num37 = 32;
					if (TextureAssets.Gore[this.type].IsLoaded)
					{
						num37 = TextureAssets.Gore[this.type].Width();
						if (TextureAssets.Gore[this.type].Height() < num37)
						{
							num37 = TextureAssets.Gore[this.type].Height();
						}
					}
					num37 = (int)((float)num37 * 0.9f);
					Vector4 vector3 = Collision.SlopeCollision(this.position, this.velocity, num37, num37, 0f, true);
					this.position.X = vector3.X;
					this.position.Y = vector3.Y;
					this.velocity.X = vector3.Z;
					this.velocity.Y = vector3.W;
				}
				if (GoreID.Sets.SpecialAI[this.type] == 1)
				{
					this.Gore_UpdateSail();
				}
				else if (GoreID.Sets.SpecialAI[this.type] == 3)
				{
					this.Gore_UpdateLeaf();
				}
				else
				{
					this.position += this.velocity;
				}
				if (this.alpha >= 255)
				{
					this.active = false;
				}
				if (this.light > 0f)
				{
					float num38 = this.light * this.scale;
					float num39 = this.light * this.scale;
					float num40 = this.light * this.scale;
					if (this.type == 16)
					{
						num40 *= 0.3f;
						num39 *= 0.8f;
					}
					else if (this.type == 17)
					{
						num39 *= 0.6f;
						num38 *= 0.3f;
					}
					if (TextureAssets.Gore[this.type].IsLoaded)
					{
						Lighting.AddLight((int)((this.position.X + (float)TextureAssets.Gore[this.type].Width() * this.scale / 2f) / 16f), (int)((this.position.Y + (float)TextureAssets.Gore[this.type].Height() * this.scale / 2f) / 16f), num38, num39, num40);
					}
					else
					{
						Lighting.AddLight((int)((this.position.X + 32f * this.scale / 2f) / 16f), (int)((this.position.Y + 32f * this.scale / 2f) / 16f), num38, num39, num40);
					}
				}
				GoreLoader.TakeDownUpdateType(this);
				return;
			}
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00025708 File Offset: 0x00023908
		private unsafe void Gore_UpdateLeaf()
		{
			Vector2 vector = this.position + new Vector2(12f) / 2f - new Vector2(4f) / 2f;
			vector.Y -= 4f;
			Vector2 vector2 = this.position - vector;
			if (this.velocity.Y < 0f)
			{
				Vector2 vector3;
				vector3..ctor(this.velocity.X, -0.2f);
				int num = 4;
				num = (int)((float)num * 0.9f);
				Point point = (new Vector2((float)num, (float)num) / 2f + vector).ToTileCoordinates();
				if (!WorldGen.InWorld(point.X, point.Y, 0))
				{
					this.active = false;
					return;
				}
				Tile tile = Main.tile[point.X, point.Y];
				if (tile == null)
				{
					this.active = false;
					return;
				}
				int num2 = 6;
				Rectangle rectangle;
				rectangle..ctor(point.X * 16, point.Y * 16 + (int)(*tile.liquid / 16), 16, (int)(16 - *tile.liquid / 16));
				Rectangle value;
				value..ctor((int)vector.X, (int)vector.Y + num2, num, num);
				bool flag = tile != null && *tile.liquid > 0 && rectangle.Intersects(value);
				if (flag)
				{
					if (tile.honey())
					{
						vector3.X = 0f;
					}
					else if (tile.lava())
					{
						this.active = false;
						for (int i = 0; i < 5; i++)
						{
							Dust.NewDust(this.position, num, num, 31, 0f, -0.2f, 0, default(Color), 1f);
						}
					}
					else
					{
						vector3.X = Main.WindForVisuals;
					}
					if ((double)this.position.Y > Main.worldSurface * 16.0)
					{
						vector3.X = 0f;
					}
				}
				if (!WorldGen.SolidTile(point.X, point.Y + 1, false) && !flag)
				{
					this.velocity.Y = 0.1f;
					this.timeLeft = 0;
					this.alpha += 20;
				}
				vector3 = Collision.TileCollision(vector, vector3, num, num, false, false, 1);
				if (flag)
				{
					this.rotation = vector3.ToRotation() + 1.5707964f;
				}
				vector3.X *= 0.94f;
				if (!flag || ((double)vector3.X > -0.01 && (double)vector3.X < 0.01))
				{
					vector3.X = 0f;
				}
				if (this.timeLeft > 0)
				{
					this.timeLeft -= GoreID.Sets.DisappearSpeed[this.type];
				}
				else
				{
					this.alpha += GoreID.Sets.DisappearSpeedAlpha[this.type];
				}
				this.velocity.X = vector3.X;
				this.position.X = this.position.X + this.velocity.X;
				return;
			}
			else
			{
				this.velocity.Y = this.velocity.Y + 0.017453292f;
				Vector2 vector4;
				vector4..ctor(Vector2.UnitY.RotatedBy((double)this.velocity.Y, default(Vector2)).X * 1f, Math.Abs(Vector2.UnitY.RotatedBy((double)this.velocity.Y, default(Vector2)).Y) * 1f);
				int num3 = 4;
				if ((double)this.position.Y < Main.worldSurface * 16.0)
				{
					vector4.X += Main.WindForVisuals * 4f;
				}
				Vector2 vector5 = vector4;
				vector4 = Collision.TileCollision(vector, vector4, num3, num3, false, false, 1);
				Vector4 vector6 = Collision.SlopeCollision(vector, vector4, num3, num3, 1f, false);
				this.position.X = vector6.X;
				this.position.Y = vector6.Y;
				vector4.X = vector6.Z;
				vector4.Y = vector6.W;
				this.position += vector2;
				if (vector4 != vector5)
				{
					this.velocity.Y = -1f;
				}
				Point point2 = (new Vector2(this.Width, this.Height) * 0.5f + this.position).ToTileCoordinates();
				if (!WorldGen.InWorld(point2.X, point2.Y, 0))
				{
					this.active = false;
					return;
				}
				Tile tile2 = Main.tile[point2.X, point2.Y];
				if (tile2 == null)
				{
					this.active = false;
					return;
				}
				int num4 = 6;
				Rectangle rectangle2;
				rectangle2..ctor(point2.X * 16, point2.Y * 16 + (int)(*tile2.liquid / 16), 16, (int)(16 - *tile2.liquid / 16));
				Rectangle value2;
				value2..ctor((int)vector.X, (int)vector.Y + num4, num3, num3);
				if (tile2 != null && *tile2.liquid > 0 && rectangle2.Intersects(value2))
				{
					this.velocity.Y = -1f;
				}
				this.position += vector4;
				this.rotation = vector4.ToRotation() + 1.5707964f;
				if (this.timeLeft > 0)
				{
					this.timeLeft -= GoreID.Sets.DisappearSpeed[this.type];
					return;
				}
				this.alpha += GoreID.Sets.DisappearSpeedAlpha[this.type];
				return;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00025CDC File Offset: 0x00023EDC
		private void Gore_UpdateSail()
		{
			if (this.velocity.Y < 0f)
			{
				Vector2 vector;
				vector..ctor(this.velocity.X, 0.6f);
				int num = 32;
				if (TextureAssets.Gore[this.type].IsLoaded)
				{
					num = TextureAssets.Gore[this.type].Width();
					if (TextureAssets.Gore[this.type].Height() < num)
					{
						num = TextureAssets.Gore[this.type].Height();
					}
				}
				num = (int)((float)num * 0.9f);
				vector = Collision.TileCollision(this.position, vector, (int)((float)num * this.scale), (int)((float)num * this.scale), false, false, 1);
				vector.X *= 0.97f;
				if ((double)vector.X > -0.01 && (double)vector.X < 0.01)
				{
					vector.X = 0f;
				}
				if (this.timeLeft > 0)
				{
					this.timeLeft--;
				}
				else
				{
					this.alpha++;
				}
				this.velocity.X = vector.X;
				return;
			}
			this.velocity.Y = this.velocity.Y + 0.05235988f;
			Vector2 vector2;
			vector2..ctor(Vector2.UnitY.RotatedBy((double)this.velocity.Y, default(Vector2)).X * 2f, Math.Abs(Vector2.UnitY.RotatedBy((double)this.velocity.Y, default(Vector2)).Y) * 3f);
			vector2 *= 2f;
			int num2 = 32;
			if (TextureAssets.Gore[this.type].IsLoaded)
			{
				num2 = TextureAssets.Gore[this.type].Width();
				if (TextureAssets.Gore[this.type].Height() < num2)
				{
					num2 = TextureAssets.Gore[this.type].Height();
				}
			}
			Vector2 vector3 = vector2;
			vector2 = Collision.TileCollision(this.position, vector2, (int)((float)num2 * this.scale), (int)((float)num2 * this.scale), false, false, 1);
			if (vector2 != vector3)
			{
				this.velocity.Y = -1f;
			}
			this.position += vector2;
			this.rotation = vector2.ToRotation() + 3.1415927f;
			if (this.timeLeft > 0)
			{
				this.timeLeft--;
				return;
			}
			this.alpha++;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00025F6B File Offset: 0x0002416B
		internal static Gore NewGorePerfect(Vector2 Position, Vector2 Velocity, int Type, float Scale = 1f)
		{
			return Gore.NewGorePerfect(IEntitySource.GetGoreFallback(), Position, Velocity, Type, Scale);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00025F7B File Offset: 0x0002417B
		internal static Gore NewGoreDirect(Vector2 Position, Vector2 Velocity, int Type, float Scale = 1f)
		{
			return Gore.NewGoreDirect(IEntitySource.GetGoreFallback(), Position, Velocity, Type, Scale);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00025F8B File Offset: 0x0002418B
		internal static int NewGore(Vector2 Position, Vector2 Velocity, int Type, float Scale = 1f)
		{
			return Gore.NewGore(IEntitySource.GetGoreFallback(), Position, Velocity, Type, Scale);
		}

		/// <summary>
		/// Spawns a gore with an exact position and velocity, no randomization
		/// </summary>
		/// <param name="source">Recommend using <see cref="M:Terraria.Entity.GetSource_Death(System.String)" /> or <see cref="M:Terraria.Entity.GetSource_FromThis(System.String)" />" as the spawn source</param>
		/// <param name="Position"></param>
		/// <param name="Velocity"></param>
		/// <param name="Type"></param>
		/// <param name="Scale"></param>
		/// <returns></returns>
		// Token: 0x0600022B RID: 555 RVA: 0x00025F9B File Offset: 0x0002419B
		public static Gore NewGorePerfect(IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale = 1f)
		{
			Gore gore = Gore.NewGoreDirect(source, Position, Velocity, Type, Scale);
			gore.position = Position;
			gore.velocity = Velocity;
			return gore;
		}

		/// <summary>
		/// Spawns a gore with given properties
		/// </summary>
		/// <param name="source">Recommend using <see cref="M:Terraria.Entity.GetSource_Death(System.String)" /> or <see cref="M:Terraria.Entity.GetSource_FromThis(System.String)" />" as the spawn source</param>
		/// <param name="Position"></param>
		/// <param name="Velocity"></param>
		/// <param name="Type"></param>
		/// <param name="Scale"></param>
		/// <returns>A reference to the gore</returns>
		// Token: 0x0600022C RID: 556 RVA: 0x00025FB6 File Offset: 0x000241B6
		public static Gore NewGoreDirect(IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale = 1f)
		{
			return Main.gore[Gore.NewGore(source, Position, Velocity, Type, Scale)];
		}

		/// <summary>
		/// Spawns a gore with given properties
		/// </summary>
		/// <param name="source">Recommend using <see cref="M:Terraria.Entity.GetSource_Death(System.String)" /> or <see cref="M:Terraria.Entity.GetSource_FromThis(System.String)" />" as the spawn source</param>
		/// <param name="Position"></param>
		/// <param name="Velocity"></param>
		/// <param name="Type"></param>
		/// <param name="Scale"></param>
		/// <returns>The index of the gore in the <see cref="F:Terraria.Main.gore" /> array</returns>
		// Token: 0x0600022D RID: 557 RVA: 0x00025FCC File Offset: 0x000241CC
		public static int NewGore(IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale = 1f)
		{
			if (Main.netMode == 2)
			{
				return 600;
			}
			if (Main.gamePaused)
			{
				return 600;
			}
			if (WorldGen.gen)
			{
				return 600;
			}
			if (Main.rand == null)
			{
				Main.rand = new UnifiedRandom();
			}
			if (Type <= 0)
			{
				return 600;
			}
			int num = 600;
			for (int i = 0; i < 600; i++)
			{
				if (!Main.gore[i].active)
				{
					num = i;
					break;
				}
			}
			if (num == 600)
			{
				return num;
			}
			Main.gore[num].Frame = new SpriteFrame(1, 1);
			Main.gore[num].frameCounter = 0;
			Main.gore[num].behindTiles = false;
			Main.gore[num].light = 0f;
			Main.gore[num].position = Position;
			Main.gore[num].velocity = Velocity;
			Gore gore2 = Main.gore[num];
			gore2.velocity.Y = gore2.velocity.Y - (float)Main.rand.Next(10, 31) * 0.1f;
			Gore gore3 = Main.gore[num];
			gore3.velocity.X = gore3.velocity.X + (float)Main.rand.Next(-20, 21) * 0.1f;
			Main.gore[num].type = Type;
			Main.gore[num].active = true;
			Main.gore[num].alpha = 0;
			Main.gore[num].rotation = 0f;
			Main.gore[num].scale = Scale;
			if (!ChildSafety.Disabled && ChildSafety.DangerousGore(Type))
			{
				Main.gore[num].type = Main.rand.Next(11, 14);
				Main.gore[num].scale = Main.rand.NextFloat() * 0.5f + 0.5f;
				Main.gore[num].velocity /= 2f;
			}
			if (Gore.goreTime == 0 || Type == 11 || Type == 12 || Type == 13 || Type == 16 || Type == 17 || Type == 61 || Type == 62 || Type == 63 || Type == 99 || Type == 220 || Type == 221 || Type == 222 || Type == 435 || Type == 436 || Type == 437 || (Type >= 861 && Type <= 862))
			{
				Main.gore[num].sticky = false;
			}
			else if (Type >= 375 && Type <= 377)
			{
				Main.gore[num].sticky = false;
				Main.gore[num].alpha = 100;
			}
			else
			{
				Main.gore[num].sticky = true;
				Main.gore[num].timeLeft = Gore.goreTime;
			}
			if (GoreID.Sets.LiquidDroplet[Type])
			{
				Main.gore[num].numFrames = 15;
				Main.gore[num].behindTiles = true;
				Main.gore[num].timeLeft = Gore.goreTime * 3;
			}
			if (Type == 16 || Type == 17)
			{
				Main.gore[num].alpha = 100;
				Main.gore[num].scale = 0.7f;
				Main.gore[num].light = 1f;
			}
			if (Type >= 570 && Type <= 572)
			{
				Main.gore[num].velocity = Velocity;
			}
			if (Type == 1201 || Type == 1208)
			{
				Main.gore[num].Frame = new SpriteFrame(1, 4);
			}
			if (Type == 1217 || Type == 1218)
			{
				Main.gore[num].Frame = new SpriteFrame(1, 3);
			}
			if (Type == 1225)
			{
				Main.gore[num].Frame = new SpriteFrame(1, 3);
				Main.gore[num].timeLeft = 10 + Main.rand.Next(6);
				Main.gore[num].sticky = false;
				if (TextureAssets.Gore[Type].IsLoaded)
				{
					Main.gore[num].position.X = Position.X - (float)(TextureAssets.Gore[Type].Width() / 2) * Scale;
					Main.gore[num].position.Y = Position.Y - (float)TextureAssets.Gore[Type].Height() * Scale / 2f;
				}
			}
			int num3 = GoreID.Sets.SpecialAI[Type];
			if (num3 == 3)
			{
				Main.gore[num].velocity = new Vector2((Main.rand.NextFloat() - 0.5f) * 1f, Main.rand.NextFloat() * 6.2831855f);
				bool flag = GoreID.Sets.PaintedFallingLeaf[Type];
				Gore gore4 = Main.gore[num];
				SpriteFrame spriteFrame = new SpriteFrame((!flag) ? 1 : 32, 8)
				{
					CurrentRow = (byte)Main.rand.Next(8)
				};
				gore4.Frame = spriteFrame;
				Main.gore[num].frameCounter = (byte)Main.rand.Next(8);
			}
			if (num3 == 1)
			{
				Main.gore[num].velocity = new Vector2((Main.rand.NextFloat() - 0.5f) * 3f, Main.rand.NextFloat() * 6.2831855f);
			}
			if (Type >= 411 && Type <= 430 && TextureAssets.Gore[Type].IsLoaded)
			{
				Main.gore[num].position.X = Position.X - (float)(TextureAssets.Gore[Type].Width() / 2) * Scale;
				Main.gore[num].position.Y = Position.Y - (float)TextureAssets.Gore[Type].Height() * Scale;
				Gore gore5 = Main.gore[num];
				gore5.velocity.Y = gore5.velocity.Y * ((float)Main.rand.Next(90, 150) * 0.01f);
				Gore gore6 = Main.gore[num];
				gore6.velocity.X = gore6.velocity.X * ((float)Main.rand.Next(40, 90) * 0.01f);
				int num2 = Main.rand.Next(4) * 5;
				Main.gore[num].type += num2;
				Main.gore[num].timeLeft = Main.rand.Next(Gore.goreTime / 2, Gore.goreTime * 2);
				Main.gore[num].sticky = true;
				if (Gore.goreTime == 0)
				{
					Main.gore[num].timeLeft = Main.rand.Next(150, 600);
				}
			}
			if (Type >= 907 && Type <= 909)
			{
				Main.gore[num].sticky = true;
				Main.gore[num].numFrames = 3;
				Main.gore[num].frame = (byte)Main.rand.Next(3);
				Main.gore[num].frameCounter = (byte)Main.rand.Next(5);
				Main.gore[num].rotation = 0f;
			}
			if (num3 == 2)
			{
				Main.gore[num].sticky = false;
				if (TextureAssets.Gore[Type].IsLoaded)
				{
					Main.gore[num].alpha = 150;
					Main.gore[num].velocity = Velocity;
					Main.gore[num].position.X = Position.X - (float)(TextureAssets.Gore[Type].Width() / 2) * Scale;
					Main.gore[num].position.Y = Position.Y - (float)TextureAssets.Gore[Type].Height() * Scale / 2f;
					Main.gore[num].timeLeft = Main.rand.Next(Gore.goreTime / 2, Gore.goreTime + 1);
				}
			}
			if (num3 == 4)
			{
				Main.gore[num].alpha = 254;
				Main.gore[num].timeLeft = 300;
			}
			if (num3 == 5)
			{
				Main.gore[num].alpha = 254;
				Main.gore[num].timeLeft = 240;
			}
			if (num3 == 6)
			{
				Main.gore[num].alpha = 254;
				Main.gore[num].timeLeft = 480;
			}
			Gore gore = Main.gore[num];
			gore.ResetNewFields();
			gore.ModGore = GoreLoader.GetModGore(gore.type);
			ModGore modGore = gore.ModGore;
			if (modGore != null)
			{
				modGore.OnSpawn(gore, source ?? IEntitySource.GetGoreFallback());
			}
			if (Main.gore[num].DeactivateIfOutsideOfWorld())
			{
				return 600;
			}
			return num;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00026804 File Offset: 0x00024A04
		public Color GetAlpha(Color newColor)
		{
			ModGore modGore = this.ModGore;
			Color? color = (modGore != null) ? modGore.GetAlpha(this, newColor) : null;
			if (color != null)
			{
				return color.GetValueOrDefault();
			}
			float num = (float)(255 - this.alpha) / 255f;
			int r;
			int g;
			int b;
			if (this.type == 16 || this.type == 17)
			{
				r = (int)newColor.R;
				g = (int)newColor.G;
				b = (int)newColor.B;
			}
			else
			{
				if (this.type == 716)
				{
					return new Color(255, 255, 255, 200);
				}
				if (this.type >= 570 && this.type <= 572)
				{
					byte b2 = (byte)(255 - this.alpha);
					return new Color((int)b2, (int)b2, (int)b2, (int)(b2 / 2));
				}
				if (this.type == 331)
				{
					return new Color(255, 255, 255, 50);
				}
				if (this.type == 1225)
				{
					return new Color(num, num, num, num);
				}
				r = (int)((float)newColor.R * num);
				g = (int)((float)newColor.G * num);
				b = (int)((float)newColor.B * num);
			}
			int num2 = (int)newColor.A - this.alpha;
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (this.type >= 1202 && this.type <= 1204)
			{
				return new Color(r, g, b, (num2 < 20) ? num2 : 20);
			}
			return new Color(r, g, b, num2);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000269AC File Offset: 0x00024BAC
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000269B4 File Offset: 0x00024BB4
		public ModGore ModGore { get; private set; }

		// Token: 0x06000231 RID: 561 RVA: 0x000269BD File Offset: 0x00024BBD
		private void ResetNewFields()
		{
			this.drawOffset = Vector2.Zero;
			this.realType = 0;
		}

		// Token: 0x040001E7 RID: 487
		public static int goreTime = 600;

		// Token: 0x040001E8 RID: 488
		public Vector2 position;

		// Token: 0x040001E9 RID: 489
		public Vector2 velocity;

		// Token: 0x040001EA RID: 490
		public float rotation;

		// Token: 0x040001EB RID: 491
		public float scale;

		// Token: 0x040001EC RID: 492
		public int alpha;

		// Token: 0x040001ED RID: 493
		public int type;

		// Token: 0x040001EE RID: 494
		public float light;

		// Token: 0x040001EF RID: 495
		public bool active;

		// Token: 0x040001F0 RID: 496
		public bool sticky = true;

		// Token: 0x040001F1 RID: 497
		public int timeLeft = Gore.goreTime;

		// Token: 0x040001F2 RID: 498
		public bool behindTiles;

		// Token: 0x040001F3 RID: 499
		public byte frameCounter;

		// Token: 0x040001F4 RID: 500
		public SpriteFrame Frame = new SpriteFrame(1, 1);

		// Token: 0x040001F5 RID: 501
		public Vector2 drawOffset;

		// Token: 0x040001F6 RID: 502
		internal int realType;
	}
}
