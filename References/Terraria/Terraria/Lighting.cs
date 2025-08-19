using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000043 RID: 67
	public class Lighting
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0033BF1B File Offset: 0x0033A11B
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x0033BF22 File Offset: 0x0033A122
		public static float GlobalBrightness { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0033BF2A File Offset: 0x0033A12A
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x0033BF34 File Offset: 0x0033A134
		public static LightMode Mode
		{
			get
			{
				return Lighting._mode;
			}
			set
			{
				Lighting._mode = value;
				switch (Lighting._mode)
				{
				case LightMode.White:
					Lighting._activeEngine = Lighting.LegacyEngine;
					Lighting.LegacyEngine.Mode = 1;
					break;
				case LightMode.Retro:
					Lighting._activeEngine = Lighting.LegacyEngine;
					Lighting.LegacyEngine.Mode = 2;
					break;
				case LightMode.Trippy:
					Lighting._activeEngine = Lighting.LegacyEngine;
					Lighting.LegacyEngine.Mode = 3;
					break;
				case LightMode.Color:
					Lighting._activeEngine = Lighting.NewEngine;
					Lighting.LegacyEngine.Mode = 0;
					Lighting.OffScreenTiles = 35;
					break;
				}
				Main.renderCount = 0;
				Main.renderNow = false;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0033BFD2 File Offset: 0x0033A1D2
		public static bool NotRetro
		{
			get
			{
				return Lighting.Mode != LightMode.Retro && Lighting.Mode != LightMode.Trippy;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0033BFE9 File Offset: 0x0033A1E9
		public static bool UsingNewLighting
		{
			get
			{
				return Lighting.Mode == LightMode.Color;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0033BFF3 File Offset: 0x0033A1F3
		public static bool UpdateEveryFrame
		{
			get
			{
				return Main.LightingEveryFrame && !Main.RenderTargetsRequired && !Lighting.NotRetro;
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0033C00D File Offset: 0x0033A20D
		public static void Initialize()
		{
			Lighting.GlobalBrightness = 1.2f;
			Lighting.NewEngine.Rebuild();
			Lighting.LegacyEngine.Rebuild();
			if (Lighting._activeEngine == null)
			{
				Lighting.Mode = LightMode.Color;
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0033C03A File Offset: 0x0033A23A
		public static void LightTiles(int firstX, int lastX, int firstY, int lastY)
		{
			Main.render = true;
			Lighting.UpdateGlobalBrightness();
			Lighting._activeEngine.ProcessArea(new Rectangle(firstX, firstY, lastX - firstX, lastY - firstY));
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0033C05E File Offset: 0x0033A25E
		private static void UpdateGlobalBrightness()
		{
			Lighting.GlobalBrightness = 1.2f;
			if (Main.player[Main.myPlayer].blind)
			{
				Lighting.GlobalBrightness = 1f;
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0033C088 File Offset: 0x0033A288
		public static float Brightness(int x, int y)
		{
			Vector3 color = Lighting._activeEngine.GetColor(x, y);
			return Lighting.GlobalBrightness * (color.X + color.Y + color.Z) / 3f;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0033C0C4 File Offset: 0x0033A2C4
		public static Vector3 GetSubLight(Vector2 position)
		{
			Vector2 vector = position / 16f - new Vector2(0.5f, 0.5f);
			Vector2 vector2 = new Vector2(vector.X % 1f, vector.Y % 1f);
			int num = (int)vector.X;
			int num2 = (int)vector.Y;
			Vector3 color = Lighting._activeEngine.GetColor(num, num2);
			Vector3 color2 = Lighting._activeEngine.GetColor(num + 1, num2);
			Vector3 color3 = Lighting._activeEngine.GetColor(num, num2 + 1);
			Vector3 color4 = Lighting._activeEngine.GetColor(num + 1, num2 + 1);
			Vector3 value = Vector3.Lerp(color, color2, vector2.X);
			Vector3 value2 = Vector3.Lerp(color3, color4, vector2.X);
			return Vector3.Lerp(value, value2, vector2.Y);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0033C189 File Offset: 0x0033A389
		public static void AddLight(Vector2 position, Vector3 rgb)
		{
			Lighting.AddLight((int)(position.X / 16f), (int)(position.Y / 16f), rgb.X, rgb.Y, rgb.Z);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0033C1BC File Offset: 0x0033A3BC
		public static void AddLight(Vector2 position, float r, float g, float b)
		{
			Lighting.AddLight((int)(position.X / 16f), (int)(position.Y / 16f), r, g, b);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0033C1E0 File Offset: 0x0033A3E0
		public static void AddLight(int i, int j, int torchID, float lightAmount)
		{
			float num;
			float num2;
			float num3;
			TorchID.TorchColor(torchID, out num, out num2, out num3);
			Lighting._activeEngine.AddLight(i, j, new Vector3(num * lightAmount, num2 * lightAmount, num3 * lightAmount));
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0033C214 File Offset: 0x0033A414
		public static void AddLight(Vector2 position, int torchID)
		{
			float r;
			float g;
			float b;
			TorchID.TorchColor(torchID, out r, out g, out b);
			Lighting.AddLight((int)position.X / 16, (int)position.Y / 16, r, g, b);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0033C249 File Offset: 0x0033A449
		public static void AddLight(int i, int j, float r, float g, float b)
		{
			if (Main.gamePaused)
			{
				return;
			}
			if (Main.netMode == 2)
			{
				return;
			}
			Lighting._activeEngine.AddLight(i, j, new Vector3(r, g, b));
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0033C271 File Offset: 0x0033A471
		public static void NextLightMode()
		{
			Lighting.Mode++;
			if (!Enum.IsDefined(typeof(LightMode), Lighting.Mode))
			{
				Lighting.Mode = LightMode.White;
			}
			Lighting.Clear();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0033C2A5 File Offset: 0x0033A4A5
		public static void Clear()
		{
			Lighting._activeEngine.Clear();
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0033C2B1 File Offset: 0x0033A4B1
		public static Color GetColor(Point tileCoords)
		{
			if (Main.gameMenu)
			{
				return Color.White;
			}
			return new Color(Lighting._activeEngine.GetColor(tileCoords.X, tileCoords.Y) * Lighting.GlobalBrightness);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0033C2E5 File Offset: 0x0033A4E5
		public static Color GetColor(Point tileCoords, Color originalColor)
		{
			if (Main.gameMenu)
			{
				return originalColor;
			}
			return new Color(Lighting._activeEngine.GetColor(tileCoords.X, tileCoords.Y) * originalColor.ToVector3());
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0033C317 File Offset: 0x0033A517
		public static Color GetColor(int x, int y, Color oldColor)
		{
			if (Main.gameMenu)
			{
				return oldColor;
			}
			return new Color(Lighting._activeEngine.GetColor(x, y) * oldColor.ToVector3());
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0033C340 File Offset: 0x0033A540
		public static Color GetColorClamped(int x, int y, Color oldColor)
		{
			if (Main.gameMenu)
			{
				return oldColor;
			}
			Vector3 vector = Lighting._activeEngine.GetColor(x, y);
			vector = Vector3.Min(Vector3.One, vector);
			return new Color(vector * oldColor.ToVector3());
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0033C384 File Offset: 0x0033A584
		public static Color GetColor(int x, int y)
		{
			if (Main.gameMenu)
			{
				return Color.White;
			}
			Color result = default(Color);
			Vector3 color = Lighting._activeEngine.GetColor(x, y);
			float num = Lighting.GlobalBrightness * 255f;
			int num2 = (int)(color.X * num);
			int num3 = (int)(color.Y * num);
			int num4 = (int)(color.Z * num);
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			num4 <<= 16;
			num3 <<= 8;
			result.PackedValue = (uint)(num2 | num3 | num4 | -16777216);
			return result;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0033C428 File Offset: 0x0033A628
		public static void GetColor9Slice(int centerX, int centerY, ref Color[] slices)
		{
			int num = 0;
			for (int i = centerX - 1; i <= centerX + 1; i++)
			{
				for (int j = centerY - 1; j <= centerY + 1; j++)
				{
					Vector3 color = Lighting._activeEngine.GetColor(i, j);
					int num2 = (int)(255f * color.X * Lighting.GlobalBrightness);
					int num3 = (int)(255f * color.Y * Lighting.GlobalBrightness);
					int num4 = (int)(255f * color.Z * Lighting.GlobalBrightness);
					if (num2 > 255)
					{
						num2 = 255;
					}
					if (num3 > 255)
					{
						num3 = 255;
					}
					if (num4 > 255)
					{
						num4 = 255;
					}
					num4 <<= 16;
					num3 <<= 8;
					slices[num].PackedValue = (uint)(num2 | num3 | num4 | -16777216);
					num += 3;
				}
				num -= 8;
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0033C510 File Offset: 0x0033A710
		public static void GetColor9Slice(int x, int y, ref Vector3[] slices)
		{
			slices[0] = Lighting._activeEngine.GetColor(x - 1, y - 1) * Lighting.GlobalBrightness;
			slices[3] = Lighting._activeEngine.GetColor(x - 1, y) * Lighting.GlobalBrightness;
			slices[6] = Lighting._activeEngine.GetColor(x - 1, y + 1) * Lighting.GlobalBrightness;
			slices[1] = Lighting._activeEngine.GetColor(x, y - 1) * Lighting.GlobalBrightness;
			slices[4] = Lighting._activeEngine.GetColor(x, y) * Lighting.GlobalBrightness;
			slices[7] = Lighting._activeEngine.GetColor(x, y + 1) * Lighting.GlobalBrightness;
			slices[2] = Lighting._activeEngine.GetColor(x + 1, y - 1) * Lighting.GlobalBrightness;
			slices[5] = Lighting._activeEngine.GetColor(x + 1, y) * Lighting.GlobalBrightness;
			slices[8] = Lighting._activeEngine.GetColor(x + 1, y + 1) * Lighting.GlobalBrightness;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0033C644 File Offset: 0x0033A844
		public static void GetCornerColors(int centerX, int centerY, out VertexColors vertices, float scale = 1f)
		{
			vertices = default(VertexColors);
			Vector3 color = Lighting._activeEngine.GetColor(centerX, centerY);
			Vector3 color2 = Lighting._activeEngine.GetColor(centerX, centerY - 1);
			Vector3 color3 = Lighting._activeEngine.GetColor(centerX, centerY + 1);
			Vector3 color4 = Lighting._activeEngine.GetColor(centerX - 1, centerY);
			Vector3 color5 = Lighting._activeEngine.GetColor(centerX + 1, centerY);
			Vector3 color6 = Lighting._activeEngine.GetColor(centerX - 1, centerY - 1);
			Vector3 color7 = Lighting._activeEngine.GetColor(centerX + 1, centerY - 1);
			Vector3 color8 = Lighting._activeEngine.GetColor(centerX - 1, centerY + 1);
			Vector3 color9 = Lighting._activeEngine.GetColor(centerX + 1, centerY + 1);
			float num = Lighting.GlobalBrightness * scale * 63.75f;
			int num2 = (int)((color2.X + color6.X + color4.X + color.X) * num);
			int num3 = (int)((color2.Y + color6.Y + color4.Y + color.Y) * num);
			int num4 = (int)((color2.Z + color6.Z + color4.Z + color.Z) * num);
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			num3 <<= 8;
			num4 <<= 16;
			vertices.TopLeftColor.PackedValue = (uint)(num2 | num3 | num4 | -16777216);
			num2 = (int)((color2.X + color7.X + color5.X + color.X) * num);
			num3 = (int)((color2.Y + color7.Y + color5.Y + color.Y) * num);
			num4 = (int)((color2.Z + color7.Z + color5.Z + color.Z) * num);
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			num3 <<= 8;
			num4 <<= 16;
			vertices.TopRightColor.PackedValue = (uint)(num2 | num3 | num4 | -16777216);
			num2 = (int)((color3.X + color8.X + color4.X + color.X) * num);
			num3 = (int)((color3.Y + color8.Y + color4.Y + color.Y) * num);
			num4 = (int)((color3.Z + color8.Z + color4.Z + color.Z) * num);
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			num3 <<= 8;
			num4 <<= 16;
			vertices.BottomLeftColor.PackedValue = (uint)(num2 | num3 | num4 | -16777216);
			num2 = (int)((color3.X + color9.X + color5.X + color.X) * num);
			num3 = (int)((color3.Y + color9.Y + color5.Y + color.Y) * num);
			num4 = (int)((color3.Z + color9.Z + color5.Z + color.Z) * num);
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			num3 <<= 8;
			num4 <<= 16;
			vertices.BottomRightColor.PackedValue = (uint)(num2 | num3 | num4 | -16777216);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0033C9F8 File Offset: 0x0033ABF8
		public static void GetColor4Slice(int centerX, int centerY, ref Color[] slices)
		{
			Vector3 color = Lighting._activeEngine.GetColor(centerX, centerY - 1);
			Vector3 color2 = Lighting._activeEngine.GetColor(centerX, centerY + 1);
			Vector3 color3 = Lighting._activeEngine.GetColor(centerX - 1, centerY);
			Vector3 color4 = Lighting._activeEngine.GetColor(centerX + 1, centerY);
			float num = color.X + color.Y + color.Z;
			float num2 = color2.X + color2.Y + color2.Z;
			float num3 = color4.X + color4.Y + color4.Z;
			float num4 = color3.X + color3.Y + color3.Z;
			if (num >= num4)
			{
				int num5 = (int)(255f * color3.X * Lighting.GlobalBrightness);
				int num6 = (int)(255f * color3.Y * Lighting.GlobalBrightness);
				int num7 = (int)(255f * color3.Z * Lighting.GlobalBrightness);
				if (num5 > 255)
				{
					num5 = 255;
				}
				if (num6 > 255)
				{
					num6 = 255;
				}
				if (num7 > 255)
				{
					num7 = 255;
				}
				slices[0] = new Color((int)((byte)num5), (int)((byte)num6), (int)((byte)num7), 255);
			}
			else
			{
				int num8 = (int)(255f * color.X * Lighting.GlobalBrightness);
				int num9 = (int)(255f * color.Y * Lighting.GlobalBrightness);
				int num10 = (int)(255f * color.Z * Lighting.GlobalBrightness);
				if (num8 > 255)
				{
					num8 = 255;
				}
				if (num9 > 255)
				{
					num9 = 255;
				}
				if (num10 > 255)
				{
					num10 = 255;
				}
				slices[0] = new Color((int)((byte)num8), (int)((byte)num9), (int)((byte)num10), 255);
			}
			if (num >= num3)
			{
				int num11 = (int)(255f * color4.X * Lighting.GlobalBrightness);
				int num12 = (int)(255f * color4.Y * Lighting.GlobalBrightness);
				int num13 = (int)(255f * color4.Z * Lighting.GlobalBrightness);
				if (num11 > 255)
				{
					num11 = 255;
				}
				if (num12 > 255)
				{
					num12 = 255;
				}
				if (num13 > 255)
				{
					num13 = 255;
				}
				slices[1] = new Color((int)((byte)num11), (int)((byte)num12), (int)((byte)num13), 255);
			}
			else
			{
				int num14 = (int)(255f * color.X * Lighting.GlobalBrightness);
				int num15 = (int)(255f * color.Y * Lighting.GlobalBrightness);
				int num16 = (int)(255f * color.Z * Lighting.GlobalBrightness);
				if (num14 > 255)
				{
					num14 = 255;
				}
				if (num15 > 255)
				{
					num15 = 255;
				}
				if (num16 > 255)
				{
					num16 = 255;
				}
				slices[1] = new Color((int)((byte)num14), (int)((byte)num15), (int)((byte)num16), 255);
			}
			if (num2 >= num4)
			{
				int num17 = (int)(255f * color3.X * Lighting.GlobalBrightness);
				int num18 = (int)(255f * color3.Y * Lighting.GlobalBrightness);
				int num19 = (int)(255f * color3.Z * Lighting.GlobalBrightness);
				if (num17 > 255)
				{
					num17 = 255;
				}
				if (num18 > 255)
				{
					num18 = 255;
				}
				if (num19 > 255)
				{
					num19 = 255;
				}
				slices[2] = new Color((int)((byte)num17), (int)((byte)num18), (int)((byte)num19), 255);
			}
			else
			{
				int num20 = (int)(255f * color2.X * Lighting.GlobalBrightness);
				int num21 = (int)(255f * color2.Y * Lighting.GlobalBrightness);
				int num22 = (int)(255f * color2.Z * Lighting.GlobalBrightness);
				if (num20 > 255)
				{
					num20 = 255;
				}
				if (num21 > 255)
				{
					num21 = 255;
				}
				if (num22 > 255)
				{
					num22 = 255;
				}
				slices[2] = new Color((int)((byte)num20), (int)((byte)num21), (int)((byte)num22), 255);
			}
			if (num2 >= num3)
			{
				int num23 = (int)(255f * color4.X * Lighting.GlobalBrightness);
				int num24 = (int)(255f * color4.Y * Lighting.GlobalBrightness);
				int num25 = (int)(255f * color4.Z * Lighting.GlobalBrightness);
				if (num23 > 255)
				{
					num23 = 255;
				}
				if (num24 > 255)
				{
					num24 = 255;
				}
				if (num25 > 255)
				{
					num25 = 255;
				}
				slices[3] = new Color((int)((byte)num23), (int)((byte)num24), (int)((byte)num25), 255);
				return;
			}
			int num26 = (int)(255f * color2.X * Lighting.GlobalBrightness);
			int num27 = (int)(255f * color2.Y * Lighting.GlobalBrightness);
			int num28 = (int)(255f * color2.Z * Lighting.GlobalBrightness);
			if (num26 > 255)
			{
				num26 = 255;
			}
			if (num27 > 255)
			{
				num27 = 255;
			}
			if (num28 > 255)
			{
				num28 = 255;
			}
			slices[3] = new Color((int)((byte)num26), (int)((byte)num27), (int)((byte)num28), 255);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0033CF30 File Offset: 0x0033B130
		public static void GetColor4Slice(int x, int y, ref Vector3[] slices)
		{
			Vector3 color = Lighting._activeEngine.GetColor(x, y - 1);
			Vector3 color2 = Lighting._activeEngine.GetColor(x, y + 1);
			Vector3 color3 = Lighting._activeEngine.GetColor(x - 1, y);
			Vector3 color4 = Lighting._activeEngine.GetColor(x + 1, y);
			float num = color.X + color.Y + color.Z;
			float num2 = color2.X + color2.Y + color2.Z;
			float num3 = color4.X + color4.Y + color4.Z;
			float num4 = color3.X + color3.Y + color3.Z;
			if (num >= num4)
			{
				slices[0] = color3 * Lighting.GlobalBrightness;
			}
			else
			{
				slices[0] = color * Lighting.GlobalBrightness;
			}
			if (num >= num3)
			{
				slices[1] = color4 * Lighting.GlobalBrightness;
			}
			else
			{
				slices[1] = color * Lighting.GlobalBrightness;
			}
			if (num2 >= num4)
			{
				slices[2] = color3 * Lighting.GlobalBrightness;
			}
			else
			{
				slices[2] = color2 * Lighting.GlobalBrightness;
			}
			if (num2 >= num3)
			{
				slices[3] = color4 * Lighting.GlobalBrightness;
				return;
			}
			slices[3] = color2 * Lighting.GlobalBrightness;
		}

		// Token: 0x040008F5 RID: 2293
		private const float DEFAULT_GLOBAL_BRIGHTNESS = 1.2f;

		// Token: 0x040008F6 RID: 2294
		private const float BLIND_GLOBAL_BRIGHTNESS = 1f;

		// Token: 0x040008F8 RID: 2296
		[Old]
		public static int OffScreenTiles = 45;

		// Token: 0x040008F9 RID: 2297
		private static LightMode _mode = LightMode.Color;

		// Token: 0x040008FA RID: 2298
		private static readonly LightingEngine NewEngine = new LightingEngine();

		// Token: 0x040008FB RID: 2299
		private static readonly LegacyLighting LegacyEngine = new LegacyLighting(Main.Camera);

		// Token: 0x040008FC RID: 2300
		private static ILightingEngine _activeEngine;
	}
}
