using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000025 RID: 37
	public class Cloud
	{
		// Token: 0x0600012F RID: 303 RVA: 0x0000D204 File Offset: 0x0000B404
		public static void resetClouds()
		{
			if (!Main.dedServ)
			{
				Main.windSpeedCurrent = Main.windSpeedTarget;
				for (int i = 0; i < 200; i++)
				{
					Main.cloud[i].active = false;
				}
				for (int j = 0; j < Main.numClouds; j++)
				{
					Cloud.addCloud();
					Main.cloud[j].Alpha = 1f;
				}
				for (int k = 0; k < 200; k++)
				{
					Main.cloud[k].Alpha = 1f;
				}
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000D288 File Offset: 0x0000B488
		public static void addCloud()
		{
			if (Main.netMode == 2)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < 200; i++)
			{
				if (!Main.cloud[i].active)
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				return;
			}
			Main.cloud[num].kill = false;
			Main.cloud[num].rSpeed = 0f;
			Main.cloud[num].sSpeed = 0f;
			Main.cloud[num].scale = (float)Cloud.rand.Next(70, 131) * 0.01f;
			Main.cloud[num].rotation = (float)Cloud.rand.Next(-10, 11) * 0.01f;
			Main.cloud[num].width = (int)((float)TextureAssets.Cloud[Main.cloud[num].type].Width() * Main.cloud[num].scale);
			Main.cloud[num].height = (int)((float)TextureAssets.Cloud[Main.cloud[num].type].Height() * Main.cloud[num].scale);
			Main.cloud[num].Alpha = 0f;
			Main.cloud[num].spriteDir = 0;
			if (Cloud.rand.Next(2) == 0)
			{
				Main.cloud[num].spriteDir = 1;
			}
			float num2 = Main.windSpeedCurrent;
			if (!Main.gameMenu)
			{
				num2 = Main.windSpeedCurrent - Main.player[Main.myPlayer].velocity.X * 0.1f;
			}
			int num3 = 0;
			int num4 = 0;
			if (num2 > 0f)
			{
				num3 -= 200;
			}
			if (num2 < 0f)
			{
				num4 += 200;
			}
			int num5 = 300;
			float x = (float)Cloud.rand.Next(num3 - num5, Main.screenWidth + num4 + num5);
			Main.cloud[num].Alpha = 0f;
			Main.cloud[num].position.Y = (float)Cloud.rand.Next((int)((float)(-(float)Main.screenHeight) * 0.25f), (int)((float)Main.screenHeight * 0.15f));
			if (Cloud.rand.Next(3) == 0)
			{
				Cloud cloud = Main.cloud[num];
				cloud.position.Y = cloud.position.Y - (float)Cloud.rand.Next((int)((float)Main.screenHeight * 0.1f));
			}
			int? cloudChoice = CloudLoader.ChooseCloud(22f, false);
			if (cloudChoice != null && cloudChoice.Value != 0)
			{
				Main.cloud[num].type = cloudChoice.Value;
			}
			else
			{
				Main.cloud[num].type = Cloud.rand.Next(4);
				if ((Main.cloudAlpha > 0f && Cloud.rand.Next(4) != 0) || (Main.cloudBGActive >= 1f && Cloud.rand.Next(2) == 0))
				{
					Main.cloud[num].type = Cloud.rand.Next(18, 22);
					if ((double)Main.cloud[num].scale >= 1.15)
					{
						Cloud cloud2 = Main.cloud[num];
						cloud2.position.Y = cloud2.position.Y - 150f;
					}
					if (Main.cloud[num].scale >= 1f)
					{
						Cloud cloud3 = Main.cloud[num];
						cloud3.position.Y = cloud3.position.Y - 150f;
					}
				}
				else if (Main.cloudBGActive <= 0f && Main.cloudAlpha == 0f && Main.cloud[num].scale < 1f && Main.cloud[num].position.Y < (float)(-(float)Main.screenHeight) * 0.15f && (double)Main.numClouds <= 80.0)
				{
					Main.cloud[num].type = Cloud.rand.Next(9, 14);
				}
				else if ((((double)Main.cloud[num].scale < 1.15 && Main.cloud[num].position.Y < (float)(-(float)Main.screenHeight) * 0.3f) || ((double)Main.cloud[num].scale < 0.85 && Main.cloud[num].position.Y < (float)Main.screenHeight * 0.15f)) && ((double)Main.numClouds > 70.0 || Main.cloudBGActive >= 1f))
				{
					Main.cloud[num].type = Cloud.rand.Next(4, 9);
				}
				else if (Main.cloud[num].position.Y > (float)(-(float)Main.screenHeight) * 0.15f && Cloud.rand.Next(2) == 0 && (double)Main.numClouds > 20.0)
				{
					Main.cloud[num].type = Cloud.rand.Next(14, 18);
				}
			}
			if (Cloud.rand.Next((Main.dontStarveWorld || Main.tenthAnniversaryWorld) ? 25 : 150) == 0)
			{
				Main.cloud[num].type = Cloud.RollRareCloud();
			}
			else if (Main.tenthAnniversaryWorld && Cloud.rand.Next(3) == 0)
			{
				Main.cloud[num].type = Cloud.RollRareCloud();
			}
			Main.cloud[num].ModCloud = CloudLoader.GetModCloud(Main.cloud[num].type);
			if ((double)Main.cloud[num].scale > 1.2)
			{
				Cloud cloud4 = Main.cloud[num];
				cloud4.position.Y = cloud4.position.Y + 100f;
			}
			if ((double)Main.cloud[num].scale > 1.3)
			{
				Main.cloud[num].scale = 1.3f;
			}
			if ((double)Main.cloud[num].scale < 0.7)
			{
				Main.cloud[num].scale = 0.7f;
			}
			Main.cloud[num].active = true;
			Main.cloud[num].position.X = x;
			if (Main.cloud[num].position.X > (float)(Main.screenWidth + 400))
			{
				Main.cloud[num].Alpha = 1f;
			}
			if (Main.cloud[num].position.X + (float)TextureAssets.Cloud[Main.cloud[num].type].Width() * Main.cloud[num].scale < -400f)
			{
				Main.cloud[num].Alpha = 1f;
			}
			ModCloud modCloud = Main.cloud[num].ModCloud;
			if (modCloud != null)
			{
				modCloud.OnSpawn(Main.cloud[num]);
			}
			Rectangle rectangle;
			rectangle..ctor((int)Main.cloud[num].position.X, (int)Main.cloud[num].position.Y, Main.cloud[num].width, Main.cloud[num].height);
			for (int j = 0; j < 200; j++)
			{
				if (num != j && Main.cloud[j].active)
				{
					Rectangle value;
					value..ctor((int)Main.cloud[j].position.X, (int)Main.cloud[j].position.Y, Main.cloud[j].width, Main.cloud[j].height);
					if (rectangle.Intersects(value))
					{
						Main.cloud[num].active = false;
					}
				}
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000D9DC File Offset: 0x0000BBDC
		private static int RollRareCloud()
		{
			int num = -1;
			bool flag = false;
			while (!flag)
			{
				int? cloudChoice = CloudLoader.ChooseCloud((float)((!Main.tenthAnniversaryWorld) ? 18 : 15), true);
				if (cloudChoice != null && cloudChoice.Value != 0)
				{
					return cloudChoice.Value;
				}
				num = ((!Main.tenthAnniversaryWorld) ? Cloud.rand.Next(22, 41) : Cloud.rand.Next(22, 37));
				switch (num)
				{
				case 25:
				case 26:
					flag = NPC.downedBoss1;
					break;
				default:
					flag = true;
					break;
				case 28:
					if (Cloud.rand.Next(10) == 0)
					{
						flag = true;
					}
					break;
				case 30:
				case 35:
					flag = Main.hardMode;
					break;
				case 31:
					flag = NPC.downedBoss3;
					break;
				case 36:
					flag = (NPC.downedBoss2 && WorldGen.crimson);
					break;
				case 37:
				case 38:
				case 39:
				case 40:
					if (Main.dontStarveWorld || Cloud.rand.Next(10) == 0)
					{
						flag = true;
					}
					break;
				}
			}
			return num;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000DAF0 File Offset: 0x0000BCF0
		public Color cloudColor(Color bgColor)
		{
			float num = this.scale * this.Alpha;
			if (num > 1f)
			{
				num = 1f;
			}
			byte b = (byte)((float)((int)((float)bgColor.R * num)));
			float num2 = (float)((int)((float)bgColor.G * num));
			float num3 = (float)((int)((float)bgColor.B * num));
			float num4 = (float)((int)((float)bgColor.A * num));
			return new Color((int)b, (int)((byte)num2), (int)((byte)num3), (int)((byte)num4));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000DB58 File Offset: 0x0000BD58
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000DB60 File Offset: 0x0000BD60
		public static void UpdateClouds()
		{
			if (Main.netMode == 2)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < 200; i++)
			{
				if (Main.cloud[i].active)
				{
					Main.cloud[i].Update();
					if (!Main.cloud[i].kill)
					{
						num++;
					}
				}
			}
			for (int j = 0; j < 200; j++)
			{
				if (Main.cloud[j].active)
				{
					if (j > 1 && (!Main.cloud[j - 1].active || (double)Main.cloud[j - 1].scale > (double)Main.cloud[j].scale + 0.02))
					{
						Cloud cloud = (Cloud)Main.cloud[j - 1].Clone();
						Main.cloud[j - 1] = (Cloud)Main.cloud[j].Clone();
						Main.cloud[j] = cloud;
					}
					if (j < 199 && (!Main.cloud[j].active || (double)Main.cloud[j + 1].scale < (double)Main.cloud[j].scale - 0.02))
					{
						Cloud cloud2 = (Cloud)Main.cloud[j + 1].Clone();
						Main.cloud[j + 1] = (Cloud)Main.cloud[j].Clone();
						Main.cloud[j] = cloud2;
					}
				}
			}
			if (num < Main.numClouds)
			{
				Cloud.addCloud();
				return;
			}
			if (num > Main.numClouds)
			{
				int num2 = Cloud.rand.Next(num);
				int num3 = 0;
				while (Main.cloud[num2].kill && num3 < 100)
				{
					num3++;
					num2 = Cloud.rand.Next(num);
				}
				Main.cloud[num2].kill = true;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000DD24 File Offset: 0x0000BF24
		public void Update()
		{
			if (WorldGen.drunkWorldGenText && Main.gameMenu)
			{
				this.type = 28;
			}
			if (this.scale == 1f)
			{
				this.scale -= 0.0001f;
			}
			if ((double)this.scale == 1.15)
			{
				this.scale -= 0.0001f;
			}
			float num;
			if (this.scale < 1f)
			{
				num = 0.07f;
				float num2 = this.scale + 0.15f;
				num2 = (num2 + 1f) / 2f;
				num2 *= num2;
				num *= num2;
			}
			else if ((double)this.scale <= 1.15)
			{
				num = 0.19f;
				float num3 = this.scale - 0.075f;
				num3 *= num3;
				num *= num3;
			}
			else
			{
				num = 0.23f;
				float num4 = this.scale - 0.15f - 0.075f;
				num4 *= num4;
				num *= num4;
			}
			this.position.X = this.position.X + Main.windSpeedCurrent * 9f * num * (float)Main.dayRate;
			float num5 = Main.screenPosition.X - Main.screenLastPosition.X;
			this.position.X = this.position.X - num5 * num;
			float num6 = 600f;
			if (Main.bgAlphaFrontLayer[4] == 1f && this.position.Y > 200f)
			{
				this.kill = true;
				this.Alpha -= 0.005f * (float)Main.dayRate;
			}
			if (!this.kill)
			{
				if (this.Alpha < 1f)
				{
					this.Alpha += 0.001f * (float)Main.dayRate;
					if (this.Alpha > 1f)
					{
						this.Alpha = 1f;
					}
				}
			}
			else
			{
				this.Alpha -= 0.001f * (float)Main.dayRate;
				if (this.Alpha <= 0f)
				{
					this.active = false;
				}
			}
			if (this.position.X + (float)TextureAssets.Cloud[this.type].Width() * this.scale < 0f - num6 || this.position.X > (float)Main.screenWidth + num6)
			{
				this.active = false;
			}
			this.rSpeed += (float)Cloud.rand.Next(-10, 11) * 2E-05f;
			if ((double)this.rSpeed > 0.0002)
			{
				this.rSpeed = 0.0002f;
			}
			if ((double)this.rSpeed < -0.0002)
			{
				this.rSpeed = -0.0002f;
			}
			if ((double)this.rotation > 0.02)
			{
				this.rotation = 0.02f;
			}
			if ((double)this.rotation < -0.02)
			{
				this.rotation = -0.02f;
			}
			this.rotation += this.rSpeed;
			this.width = (int)((float)TextureAssets.Cloud[this.type].Width() * this.scale);
			this.height = (int)((float)TextureAssets.Cloud[this.type].Height() * this.scale);
			if (this.type >= 9 && this.type <= 13 && (Main.cloudAlpha > 0f || Main.cloudBGActive >= 1f))
			{
				this.kill = true;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000E08E File Offset: 0x0000C28E
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000E096 File Offset: 0x0000C296
		public ModCloud ModCloud { get; private set; }

		// Token: 0x06000138 RID: 312 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		internal static void SwapOutModdedClouds()
		{
			if (!Main.dedServ)
			{
				for (int i = 0; i < 200; i++)
				{
					Main.cloud[i].ModCloud = null;
					if (Main.cloud[i].type >= CloudID.Count)
					{
						Main.cloud[i].type = Main.rand.Next(0, 22);
					}
				}
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000E100 File Offset: 0x0000C300
		public override string ToString()
		{
			if (!this.active)
			{
				return "Inactive";
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 3);
			defaultInterpolatedStringHandler.AppendLiteral("type: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.type);
			defaultInterpolatedStringHandler.AppendLiteral(", scale: ");
			defaultInterpolatedStringHandler.AppendFormatted<float>(this.scale);
			defaultInterpolatedStringHandler.AppendLiteral(", ModCloud: ");
			ModCloud modCloud = this.ModCloud;
			defaultInterpolatedStringHandler.AppendFormatted(((modCloud != null) ? modCloud.FullName : null) ?? "null");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x040000AE RID: 174
		public Vector2 position;

		// Token: 0x040000AF RID: 175
		public float scale;

		// Token: 0x040000B0 RID: 176
		public float rotation;

		// Token: 0x040000B1 RID: 177
		public float rSpeed;

		// Token: 0x040000B2 RID: 178
		public float sSpeed;

		// Token: 0x040000B3 RID: 179
		public bool active;

		// Token: 0x040000B4 RID: 180
		public SpriteEffects spriteDir;

		// Token: 0x040000B5 RID: 181
		public int type;

		// Token: 0x040000B6 RID: 182
		public int width;

		// Token: 0x040000B7 RID: 183
		public int height;

		// Token: 0x040000B8 RID: 184
		public float Alpha;

		// Token: 0x040000B9 RID: 185
		public bool kill;

		// Token: 0x040000BA RID: 186
		private static UnifiedRandom rand = new UnifiedRandom();
	}
}
