using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria
{
	/// <summary>
	/// This class manages lighting in the game world. Lighting is calculated and tracked at tile coordinates within the users game view. The most common use of this class is to use GetColor methods to retrieve the light values at a location for drawing a specific entity. Another common usage is adding light to the game world using the AddLight methods.
	/// </summary>
	// Token: 0x02000036 RID: 54
	public class Lighting
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0008D21A File Offset: 0x0008B41A
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0008D221 File Offset: 0x0008B421
		public static float GlobalBrightness { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0008D229 File Offset: 0x0008B429
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0008D230 File Offset: 0x0008B430
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

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0008D2CE File Offset: 0x0008B4CE
		public static bool NotRetro
		{
			get
			{
				return Lighting.Mode != LightMode.Retro && Lighting.Mode != LightMode.Trippy;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0008D2E5 File Offset: 0x0008B4E5
		public static bool UsingNewLighting
		{
			get
			{
				return Lighting.Mode == LightMode.Color;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0008D2EF File Offset: 0x0008B4EF
		public static bool UpdateEveryFrame
		{
			get
			{
				return Main.LightingEveryFrame && !Main.RenderTargetsRequired && !Lighting.NotRetro;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0008D309 File Offset: 0x0008B509
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

		// Token: 0x06000338 RID: 824 RVA: 0x0008D336 File Offset: 0x0008B536
		public static void LightTiles(int firstX, int lastX, int firstY, int lastY)
		{
			Main.render = true;
			Lighting.UpdateGlobalBrightness();
			Lighting._activeEngine.ProcessArea(new Rectangle(firstX, firstY, lastX - firstX, lastY - firstY));
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0008D35A File Offset: 0x0008B55A
		private static void UpdateGlobalBrightness()
		{
			Lighting.GlobalBrightness = 1.2f;
			if (Main.player[Main.myPlayer].blind)
			{
				Lighting.GlobalBrightness = 1f;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0008D384 File Offset: 0x0008B584
		public static float Brightness(int x, int y)
		{
			Vector3 color = Lighting._activeEngine.GetColor(x, y);
			return Lighting.GlobalBrightness * (color.X + color.Y + color.Z) / 3f;
		}

		/// <summary>
		/// Gets the lighting color at the specified World coordinate location. The value is calculated through bilinear interpolation of the nearby tile coordinates.
		/// <LightingGetColor>
		/// 		<para /> Lighting is tracked at Tile Coordinate granularity (<see href="https://github.com/tModLoader/tModLoader/wiki/Coordinates">Coordinates wiki page</see>). Typically the center of an entity, converted to tile coordinates, is used to query the lighting color at the entity's location. That color is then used to draw the entity.
		/// 		<para /> Lighting values in-between tile coordinates can be interpolated using <see cref="M:Terraria.Lighting.GetSubLight(Microsoft.Xna.Framework.Vector2)" />, but usually the lighting values at the tile coordinates closest to the center of an entity are sufficient. There are many more GetColorX methods for more advanced situations, if needed. If drawing a really large sprite, one might consider splitting up the drawing to allow sections of the sprite to be drawn at different light values.
		/// 	</LightingGetColor>
		/// </summary>
		// Token: 0x0600033B RID: 827 RVA: 0x0008D3C0 File Offset: 0x0008B5C0
		public static Vector3 GetSubLight(Vector2 position)
		{
			Vector2 vector = position / 16f - new Vector2(0.5f, 0.5f);
			Vector2 vector2;
			vector2..ctor(vector.X % 1f, vector.Y % 1f);
			int num = (int)vector.X;
			int num2 = (int)vector.Y;
			Vector3 color5 = Lighting._activeEngine.GetColor(num, num2);
			Vector3 color2 = Lighting._activeEngine.GetColor(num + 1, num2);
			Vector3 color3 = Lighting._activeEngine.GetColor(num, num2 + 1);
			Vector3 color4 = Lighting._activeEngine.GetColor(num + 1, num2 + 1);
			Vector3 vector3 = Vector3.Lerp(color5, color2, vector2.X);
			Vector3 value2 = Vector3.Lerp(color3, color4, vector2.X);
			return Vector3.Lerp(vector3, value2, vector2.Y);
		}

		/// <summary>
		/// Adds light to the world at the specified coordinates.<para />
		/// This overload takes in world coordinates and a Vector3 containing float values typically ranging from 0 to 1. Values greater than 1 will cause the light to propagate farther. A <see cref="T:Microsoft.Xna.Framework.Vector3" /> is used for this method instead of <see cref="T:Microsoft.Xna.Framework.Color" /> to allow these overflow values.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="rgb"></param>
		// Token: 0x0600033C RID: 828 RVA: 0x0008D485 File Offset: 0x0008B685
		public static void AddLight(Vector2 position, Vector3 rgb)
		{
			Lighting.AddLight((int)(position.X / 16f), (int)(position.Y / 16f), rgb.X, rgb.Y, rgb.Z);
		}

		/// <summary>
		/// <summary>
		/// Adds light to the world at the specified coordinates.<para />
		/// This overload takes in world coordinates and float values typically ranging from 0 to 1. Values greater than 1 will cause the light to propagate farther.
		/// </summary>
		/// </summary>
		/// <param name="position"></param>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		// Token: 0x0600033D RID: 829 RVA: 0x0008D4B8 File Offset: 0x0008B6B8
		public static void AddLight(Vector2 position, float r, float g, float b)
		{
			Lighting.AddLight((int)(position.X / 16f), (int)(position.Y / 16f), r, g, b);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0008D4DC File Offset: 0x0008B6DC
		public static void AddLight(int i, int j, int torchID, float lightAmount)
		{
			float R;
			float G;
			float B;
			TorchID.TorchColor(torchID, out R, out G, out B);
			Lighting._activeEngine.AddLight(i, j, new Vector3(R * lightAmount, G * lightAmount, B * lightAmount));
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0008D510 File Offset: 0x0008B710
		public static void AddLight(Vector2 position, int torchID)
		{
			float R;
			float G;
			float B;
			TorchID.TorchColor(torchID, out R, out G, out B);
			Lighting.AddLight((int)position.X / 16, (int)position.Y / 16, R, G, B);
		}

		/// <summary>
		/// Adds light to the world at the specified coordinates.<para />
		/// This overload takes in tile coordinates and float values typically ranging from 0 to 1. Values greater than 1 will cause the light to propagate farther.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		// Token: 0x06000340 RID: 832 RVA: 0x0008D545 File Offset: 0x0008B745
		public static void AddLight(int i, int j, float r, float g, float b)
		{
			if (!Main.gamePaused && Main.netMode != 2)
			{
				Lighting._activeEngine.AddLight(i, j, new Vector3(r, g, b));
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0008D56B File Offset: 0x0008B76B
		public static void NextLightMode()
		{
			Lighting.Mode++;
			if (!Enum.IsDefined(typeof(LightMode), Lighting.Mode))
			{
				Lighting.Mode = LightMode.White;
			}
			Lighting.Clear();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0008D59F File Offset: 0x0008B79F
		public static void Clear()
		{
			Lighting._activeEngine.Clear();
		}

		/// <summary> <inheritdoc cref="M:Terraria.Lighting.GetColor(System.Int32,System.Int32)" /> </summary>
		// Token: 0x06000343 RID: 835 RVA: 0x0008D5AB File Offset: 0x0008B7AB
		public static Color GetColor(Point tileCoords)
		{
			if (Main.gameMenu)
			{
				return Color.White;
			}
			return new Color(Lighting._activeEngine.GetColor(tileCoords.X, tileCoords.Y) * Lighting.GlobalBrightness);
		}

		/// <summary>
		/// Gets the lighting color at the specified Tile coordinate location, then multiplies it by the provided Color.
		/// <LightingGetColor>
		/// 		<para /> Lighting is tracked at Tile Coordinate granularity (<see href="https://github.com/tModLoader/tModLoader/wiki/Coordinates">Coordinates wiki page</see>). Typically the center of an entity, converted to tile coordinates, is used to query the lighting color at the entity's location. That color is then used to draw the entity.
		/// 		<para /> Lighting values in-between tile coordinates can be interpolated using <see cref="M:Terraria.Lighting.GetSubLight(Microsoft.Xna.Framework.Vector2)" />, but usually the lighting values at the tile coordinates closest to the center of an entity are sufficient. There are many more GetColorX methods for more advanced situations, if needed. If drawing a really large sprite, one might consider splitting up the drawing to allow sections of the sprite to be drawn at different light values.
		/// 	</LightingGetColor>
		/// </summary>
		// Token: 0x06000344 RID: 836 RVA: 0x0008D5DF File Offset: 0x0008B7DF
		public static Color GetColor(Point tileCoords, Color originalColor)
		{
			if (Main.gameMenu)
			{
				return originalColor;
			}
			return new Color(Lighting._activeEngine.GetColor(tileCoords.X, tileCoords.Y) * originalColor.ToVector3());
		}

		/// <summary> <inheritdoc cref="M:Terraria.Lighting.GetColor(Microsoft.Xna.Framework.Point,Microsoft.Xna.Framework.Color)" /> </summary>
		// Token: 0x06000345 RID: 837 RVA: 0x0008D611 File Offset: 0x0008B811
		public static Color GetColor(int x, int y, Color oldColor)
		{
			if (Main.gameMenu)
			{
				return oldColor;
			}
			return new Color(Lighting._activeEngine.GetColor(x, y) * oldColor.ToVector3());
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0008D63C File Offset: 0x0008B83C
		public static Color GetColorClamped(int x, int y, Color oldColor)
		{
			if (Main.gameMenu)
			{
				return oldColor;
			}
			Vector3 color = Lighting._activeEngine.GetColor(x, y);
			color = Vector3.Min(Vector3.One, color);
			return new Color(color * oldColor.ToVector3());
		}

		/// <summary>
		/// Gets the lighting color at the specified Tile coordinate location.
		/// <LightingGetColor>
		/// 		<para /> Lighting is tracked at Tile Coordinate granularity (<see href="https://github.com/tModLoader/tModLoader/wiki/Coordinates">Coordinates wiki page</see>). Typically the center of an entity, converted to tile coordinates, is used to query the lighting color at the entity's location. That color is then used to draw the entity.
		/// 		<para /> Lighting values in-between tile coordinates can be interpolated using <see cref="M:Terraria.Lighting.GetSubLight(Microsoft.Xna.Framework.Vector2)" />, but usually the lighting values at the tile coordinates closest to the center of an entity are sufficient. There are many more GetColorX methods for more advanced situations, if needed. If drawing a really large sprite, one might consider splitting up the drawing to allow sections of the sprite to be drawn at different light values.
		/// 	</LightingGetColor>
		/// </summary>
		// Token: 0x06000347 RID: 839 RVA: 0x0008D680 File Offset: 0x0008B880
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

		// Token: 0x06000348 RID: 840 RVA: 0x0008D724 File Offset: 0x0008B924
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

		// Token: 0x06000349 RID: 841 RVA: 0x0008D80C File Offset: 0x0008BA0C
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

		// Token: 0x0600034A RID: 842 RVA: 0x0008D940 File Offset: 0x0008BB40
		public static void GetCornerColors(int centerX, int centerY, out VertexColors vertices, float scale = 1f)
		{
			vertices = default(VertexColors);
			Vector3 color = Lighting._activeEngine.GetColor(centerX, centerY);
			Vector3 color9 = Lighting._activeEngine.GetColor(centerX, centerY - 1);
			Vector3 color2 = Lighting._activeEngine.GetColor(centerX, centerY + 1);
			Vector3 color3 = Lighting._activeEngine.GetColor(centerX - 1, centerY);
			Vector3 color4 = Lighting._activeEngine.GetColor(centerX + 1, centerY);
			Vector3 color5 = Lighting._activeEngine.GetColor(centerX - 1, centerY - 1);
			Vector3 color6 = Lighting._activeEngine.GetColor(centerX + 1, centerY - 1);
			Vector3 color7 = Lighting._activeEngine.GetColor(centerX - 1, centerY + 1);
			Vector3 color8 = Lighting._activeEngine.GetColor(centerX + 1, centerY + 1);
			float num = Lighting.GlobalBrightness * scale * 63.75f;
			int num2 = (int)((color9.X + color5.X + color3.X + color.X) * num);
			int num3 = (int)((color9.Y + color5.Y + color3.Y + color.Y) * num);
			int num4 = (int)((color9.Z + color5.Z + color3.Z + color.Z) * num);
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
			num2 = (int)((color9.X + color6.X + color4.X + color.X) * num);
			num3 = (int)((color9.Y + color6.Y + color4.Y + color.Y) * num);
			num4 = (int)((color9.Z + color6.Z + color4.Z + color.Z) * num);
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
			num2 = (int)((color2.X + color7.X + color3.X + color.X) * num);
			num3 = (int)((color2.Y + color7.Y + color3.Y + color.Y) * num);
			num4 = (int)((color2.Z + color7.Z + color3.Z + color.Z) * num);
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
			num2 = (int)((color2.X + color8.X + color4.X + color.X) * num);
			num3 = (int)((color2.Y + color8.Y + color4.Y + color.Y) * num);
			num4 = (int)((color2.Z + color8.Z + color4.Z + color.Z) * num);
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

		// Token: 0x0600034B RID: 843 RVA: 0x0008DCE4 File Offset: 0x0008BEE4
		public static void GetColor4Slice(int centerX, int centerY, ref Color[] slices)
		{
			Vector3 color = Lighting._activeEngine.GetColor(centerX, centerY - 1);
			Vector3 color2 = Lighting._activeEngine.GetColor(centerX, centerY + 1);
			Vector3 color3 = Lighting._activeEngine.GetColor(centerX - 1, centerY);
			Vector3 color4 = Lighting._activeEngine.GetColor(centerX + 1, centerY);
			float num29 = color.X + color.Y + color.Z;
			float num2 = color2.X + color2.Y + color2.Z;
			float num3 = color4.X + color4.Y + color4.Z;
			float num4 = color3.X + color3.Y + color3.Z;
			if (num29 >= num4)
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
			if (num29 >= num3)
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

		// Token: 0x0600034C RID: 844 RVA: 0x0008E204 File Offset: 0x0008C404
		public static void GetColor4Slice(int x, int y, ref Vector3[] slices)
		{
			Vector3 color = Lighting._activeEngine.GetColor(x, y - 1);
			Vector3 color2 = Lighting._activeEngine.GetColor(x, y + 1);
			Vector3 color3 = Lighting._activeEngine.GetColor(x - 1, y);
			Vector3 color4 = Lighting._activeEngine.GetColor(x + 1, y);
			float num5 = color.X + color.Y + color.Z;
			float num2 = color2.X + color2.Y + color2.Z;
			float num3 = color4.X + color4.Y + color4.Z;
			float num4 = color3.X + color3.Y + color3.Z;
			if (num5 >= num4)
			{
				slices[0] = color3 * Lighting.GlobalBrightness;
			}
			else
			{
				slices[0] = color * Lighting.GlobalBrightness;
			}
			if (num5 >= num3)
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

		// Token: 0x040002D4 RID: 724
		private const float DEFAULT_GLOBAL_BRIGHTNESS = 1.2f;

		// Token: 0x040002D5 RID: 725
		private const float BLIND_GLOBAL_BRIGHTNESS = 1f;

		// Token: 0x040002D6 RID: 726
		[Old]
		public static int OffScreenTiles = 45;

		// Token: 0x040002D7 RID: 727
		private static LightMode _mode = LightMode.Color;

		// Token: 0x040002D8 RID: 728
		internal static readonly LightingEngine NewEngine = new LightingEngine();

		// Token: 0x040002D9 RID: 729
		public static readonly LegacyLighting LegacyEngine = new LegacyLighting(Main.Camera);

		// Token: 0x040002DA RID: 730
		private static ILightingEngine _activeEngine;
	}
}
