using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using ReLogic.Threading;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Terraria.Graphics.Light
{
	// Token: 0x02000460 RID: 1120
	public class LegacyLighting : ILightingEngine
	{
		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060036C6 RID: 14022 RVA: 0x0057EA92 File Offset: 0x0057CC92
		// (set) Token: 0x060036C7 RID: 14023 RVA: 0x0057EA9A File Offset: 0x0057CC9A
		public int Mode { get; set; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060036C8 RID: 14024 RVA: 0x0057EAA3 File Offset: 0x0057CCA3
		public bool IsColorOrWhiteMode
		{
			get
			{
				return this.Mode < 2;
			}
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x0057EAB0 File Offset: 0x0057CCB0
		public LegacyLighting(Camera camera)
		{
			this._camera = camera;
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x0057EB5C File Offset: 0x0057CD5C
		public Vector3 GetColor(int x, int y)
		{
			int num = x - this._requestedRectLeft + Lighting.OffScreenTiles;
			int num2 = y - this._requestedRectTop + Lighting.OffScreenTiles;
			Vector2 unscaledSize = this._camera.UnscaledSize;
			if (num < 0 || num2 < 0 || (float)num >= unscaledSize.X / 16f + (float)(Lighting.OffScreenTiles * 2) + 10f || (float)num2 >= unscaledSize.Y / 16f + (float)(Lighting.OffScreenTiles * 2))
			{
				return Vector3.Zero;
			}
			LegacyLighting.LightingState lightingState = this._states[num][num2];
			return new Vector3(lightingState.R, lightingState.G, lightingState.B);
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x0057EBFC File Offset: 0x0057CDFC
		public void Rebuild()
		{
			this._tempLights = new Dictionary<Point16, LegacyLighting.ColorTriplet>();
			this._swipe = new LegacyLighting.LightingSwipeData();
			this._threadSwipes = new LegacyLighting.LightingSwipeData[Environment.ProcessorCount];
			for (int i = 0; i < this._threadSwipes.Length; i++)
			{
				this._threadSwipes[i] = new LegacyLighting.LightingSwipeData();
			}
			int num = (int)this._camera.UnscaledSize.X / 16 + 90 + 10;
			int num2 = (int)this._camera.UnscaledSize.Y / 16 + 90 + 10;
			this._lightMap.SetSize(num, num2);
			if (this._states != null && this._states.Length >= num && this._states[0].Length >= num2)
			{
				return;
			}
			this._states = new LegacyLighting.LightingState[num][];
			this._axisFlipStates = new LegacyLighting.LightingState[num2][];
			for (int j = 0; j < num2; j++)
			{
				this._axisFlipStates[j] = new LegacyLighting.LightingState[num];
			}
			for (int k = 0; k < num; k++)
			{
				LegacyLighting.LightingState[] array = new LegacyLighting.LightingState[num2];
				for (int l = 0; l < num2; l++)
				{
					LegacyLighting.LightingState lightingState = array[l] = new LegacyLighting.LightingState();
					this._axisFlipStates[l][k] = lightingState;
				}
				this._states[k] = array;
			}
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x0057ED38 File Offset: 0x0057CF38
		public void AddLight(int x, int y, Vector3 color)
		{
			float x2 = color.X;
			float y2 = color.Y;
			float z = color.Z;
			if (x - this._requestedRectLeft + Lighting.OffScreenTiles < 0 || (float)(x - this._requestedRectLeft + Lighting.OffScreenTiles) >= this._camera.UnscaledSize.X / 16f + (float)(Lighting.OffScreenTiles * 2) + 10f || y - this._requestedRectTop + Lighting.OffScreenTiles < 0 || (float)(y - this._requestedRectTop + Lighting.OffScreenTiles) >= this._camera.UnscaledSize.Y / 16f + (float)(Lighting.OffScreenTiles * 2) + 10f || this._tempLights.Count == 2000)
			{
				return;
			}
			Point16 key = new Point16(x, y);
			LegacyLighting.ColorTriplet value;
			if (this._tempLights.TryGetValue(key, out value))
			{
				if (this._rgb)
				{
					if (value.R < x2)
					{
						value.R = x2;
					}
					if (value.G < y2)
					{
						value.G = y2;
					}
					if (value.B < z)
					{
						value.B = z;
					}
					this._tempLights[key] = value;
					return;
				}
				float num = (x2 + y2 + z) / 3f;
				if (value.R < num)
				{
					this._tempLights[key] = new LegacyLighting.ColorTriplet(num);
					return;
				}
			}
			else
			{
				value = ((!this._rgb) ? new LegacyLighting.ColorTriplet((x2 + y2 + z) / 3f) : new LegacyLighting.ColorTriplet(x2, y2, z));
				this._tempLights.Add(key, value);
			}
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x0057EEC4 File Offset: 0x0057D0C4
		public void ProcessArea(Rectangle area)
		{
			this._oldSkyColor = this._skyColor;
			float num = (float)Main.tileColor.R / 255f;
			float num2 = (float)Main.tileColor.G / 255f;
			float num3 = (float)Main.tileColor.B / 255f;
			this._skyColor = (num + num2 + num3) / 3f;
			if (this.IsColorOrWhiteMode)
			{
				this._offScreenTiles2 = 34;
				Lighting.OffScreenTiles = 40;
			}
			else
			{
				this._offScreenTiles2 = 18;
				Lighting.OffScreenTiles = 23;
			}
			this._requestedRectLeft = area.Left;
			this._requestedRectRight = area.Right;
			this._requestedRectTop = area.Top;
			this._requestedRectBottom = area.Bottom;
			this._expandedRectLeft = this._requestedRectLeft - Lighting.OffScreenTiles;
			this._expandedRectTop = this._requestedRectTop - Lighting.OffScreenTiles;
			this._expandedRectRight = this._requestedRectRight + Lighting.OffScreenTiles;
			this._expandedRectBottom = this._requestedRectBottom + Lighting.OffScreenTiles;
			Main.renderCount++;
			int maxLightArrayX = (int)this._camera.UnscaledSize.X / 16 + Lighting.OffScreenTiles * 2;
			int maxLightArrayY = (int)this._camera.UnscaledSize.Y / 16 + Lighting.OffScreenTiles * 2;
			if (Main.renderCount < 3)
			{
				this.DoColors();
			}
			if (Main.renderCount == 2)
			{
				this.CopyFullyProcessedDataOver(maxLightArrayX, maxLightArrayY);
			}
			else if (!Main.renderNow)
			{
				this.ShiftUnProcessedDataOver(maxLightArrayX, maxLightArrayY);
				if (Netplay.Connection.StatusMax > 0)
				{
					Main.mapTime = 1;
				}
				if (Main.mapDelay > 0)
				{
					Main.mapDelay--;
				}
				else if (Main.mapTime == 0 && Main.mapEnabled && Main.renderCount == 3)
				{
					try
					{
						this.TryUpdatingMapWithLight();
					}
					catch
					{
					}
				}
				if (this._oldSkyColor != this._skyColor)
				{
					this.UpdateLightToSkyColor(num, num2, num3);
				}
			}
			if (Main.renderCount > LegacyLighting.RenderPhases)
			{
				this.PreRenderPhase();
			}
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x0057F0C4 File Offset: 0x0057D2C4
		private unsafe void TryUpdatingMapWithLight()
		{
			Main.mapTime = Main.mapTimeMax;
			Main.updateMap = true;
			int num = 40;
			Vector2 unscaledPosition = this._camera.UnscaledPosition;
			float x = this._camera.UnscaledSize.X;
			float y = this._camera.UnscaledSize.Y;
			x = (float)((int)(x / Main.GameViewMatrix.Zoom.X));
			y = (float)((int)(y / Main.GameViewMatrix.Zoom.Y));
			Vector2 vector = unscaledPosition + Main.GameViewMatrix.Translation;
			int value = (int)Math.Floor((double)(vector.X / 16f));
			int value2 = (int)Math.Floor((double)((vector.X + x) / 16f)) + 1;
			int value3 = (int)Math.Floor((double)(vector.Y / 16f));
			int value4 = (int)Math.Floor((double)((vector.Y + y) / 16f)) + 1;
			value = Utils.Clamp<int>(value, Lighting.OffScreenTiles, Main.maxTilesX - Lighting.OffScreenTiles);
			value2 = Utils.Clamp<int>(value2, Lighting.OffScreenTiles, Main.maxTilesX - Lighting.OffScreenTiles);
			value3 = Utils.Clamp<int>(value3, Lighting.OffScreenTiles, Main.maxTilesY - Lighting.OffScreenTiles);
			value4 = Utils.Clamp<int>(value4, Lighting.OffScreenTiles, Main.maxTilesY - Lighting.OffScreenTiles);
			Main.mapMinX = Utils.Clamp<int>(this._requestedRectLeft, num, Main.maxTilesX - num);
			Main.mapMaxX = Utils.Clamp<int>(this._requestedRectRight, num, Main.maxTilesX - num);
			Main.mapMinY = Utils.Clamp<int>(this._requestedRectTop, num, Main.maxTilesY - num);
			Main.mapMaxY = Utils.Clamp<int>(this._requestedRectBottom, num, Main.maxTilesY - num);
			Main.mapMinX = Utils.Clamp<int>(Main.mapMinX, value, value2);
			Main.mapMaxX = Utils.Clamp<int>(Main.mapMaxX, value, value2);
			Main.mapMinY = Utils.Clamp<int>(Main.mapMinY, value3, value4);
			Main.mapMaxY = Utils.Clamp<int>(Main.mapMaxY, value3, value4);
			int offScreenTiles = Lighting.OffScreenTiles;
			for (int i = Main.mapMinX; i < Main.mapMaxX; i++)
			{
				LegacyLighting.LightingState[] array = this._states[i - this._requestedRectLeft + offScreenTiles];
				for (int j = Main.mapMinY; j < Main.mapMaxY; j++)
				{
					LegacyLighting.LightingState lightingState = array[j - this._requestedRectTop + offScreenTiles];
					Tile tile = Main.tile[i, j];
					float num2 = 0f;
					if (lightingState.R > num2)
					{
						num2 = lightingState.R;
					}
					if (lightingState.G > num2)
					{
						num2 = lightingState.G;
					}
					if (lightingState.B > num2)
					{
						num2 = lightingState.B;
					}
					if (this.IsColorOrWhiteMode)
					{
						num2 *= 1.5f;
					}
					byte b = (byte)Math.Min(255f, num2 * 255f);
					if ((double)j < Main.worldSurface && !tile.active() && *tile.wall == 0 && *tile.liquid == 0)
					{
						b = 22;
					}
					if (b > 18 || Main.Map[i, j].Light > 0)
					{
						if (b < 22)
						{
							b = 22;
						}
						Main.Map.UpdateLighting(i, j, b);
					}
				}
			}
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x0057F3F4 File Offset: 0x0057D5F4
		private unsafe void UpdateLightToSkyColor(float tileR, float tileG, float tileB)
		{
			int num = Utils.Clamp<int>(this._expandedRectLeft, 0, Main.maxTilesX - 1);
			int num2 = Utils.Clamp<int>(this._expandedRectRight, 0, Main.maxTilesX - 1);
			int num3 = Utils.Clamp<int>(this._expandedRectTop, 0, Main.maxTilesY - 1);
			int num4 = Utils.Clamp<int>(this._expandedRectBottom, 0, (int)Main.worldSurface - 1);
			if ((double)num3 >= Main.worldSurface)
			{
				return;
			}
			for (int i = num; i < num2; i++)
			{
				LegacyLighting.LightingState[] array = this._states[i - this._expandedRectLeft];
				for (int j = num3; j < num4; j++)
				{
					LegacyLighting.LightingState lightingState = array[j - this._expandedRectTop];
					Tile tile = Main.tile[i, j];
					if (tile == null)
					{
						tile = default(Tile);
						Main.tile[i, j] = tile;
					}
					if ((!tile.active() || !Main.tileNoSunLight[(int)(*tile.type)]) && lightingState.R < this._skyColor && *tile.liquid < 200 && (Main.wallLight[(int)(*tile.wall)] || *tile.wall == 73))
					{
						lightingState.R = tileR;
						if (lightingState.G < this._skyColor)
						{
							lightingState.G = tileG;
						}
						if (lightingState.B < this._skyColor)
						{
							lightingState.B = tileB;
						}
					}
				}
			}
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x0057F564 File Offset: 0x0057D764
		private void ShiftUnProcessedDataOver(int maxLightArrayX, int maxLightArrayY)
		{
			Vector2 screenLastPosition = Main.screenLastPosition;
			Vector2 unscaledPosition = this._camera.UnscaledPosition;
			int num = (int)Math.Floor((double)(unscaledPosition.X / 16f)) - (int)Math.Floor((double)(screenLastPosition.X / 16f));
			if (num > 5 || num < -5)
			{
				num = 0;
			}
			int num2;
			int num3;
			int num4;
			if (num < 0)
			{
				num2 = -1;
				num *= -1;
				num3 = maxLightArrayX;
				num4 = num;
			}
			else
			{
				num2 = 1;
				num3 = 0;
				num4 = maxLightArrayX - num;
			}
			int num5 = (int)Math.Floor((double)(unscaledPosition.Y / 16f)) - (int)Math.Floor((double)(screenLastPosition.Y / 16f));
			if (num5 > 5 || num5 < -5)
			{
				num5 = 0;
			}
			int num6;
			int num7;
			int num8;
			if (num5 < 0)
			{
				num6 = -1;
				num5 *= -1;
				num7 = maxLightArrayY;
				num8 = num5;
			}
			else
			{
				num6 = 1;
				num7 = 0;
				num8 = maxLightArrayY - num5;
			}
			if (num == 0 && num5 == 0)
			{
				return;
			}
			for (int i = num3; i != num4; i += num2)
			{
				LegacyLighting.LightingState[] array = this._states[i];
				LegacyLighting.LightingState[] array2 = this._states[i + num * num2];
				for (int j = num7; j != num8; j += num6)
				{
					LegacyLighting.LightingState lightingState2 = array[j];
					LegacyLighting.LightingState lightingState = array2[j + num5 * num6];
					lightingState2.R = lightingState.R;
					lightingState2.G = lightingState.G;
					lightingState2.B = lightingState.B;
				}
			}
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x0057F6A8 File Offset: 0x0057D8A8
		private void CopyFullyProcessedDataOver(int maxLightArrayX, int maxLightArrayY)
		{
			Vector2 unscaledPosition = this._camera.UnscaledPosition;
			int num = (int)Math.Floor((double)(unscaledPosition.X / 16f)) - this._scrX;
			int num2 = (int)Math.Floor((double)(unscaledPosition.Y / 16f)) - this._scrY;
			if (num > 16)
			{
				num = 0;
			}
			if (num2 > 16)
			{
				num2 = 0;
			}
			int num3 = 0;
			int num4 = maxLightArrayX;
			int num5 = 0;
			int num6 = maxLightArrayY;
			if (num < 0)
			{
				num3 -= num;
			}
			else
			{
				num4 -= num;
			}
			if (num2 < 0)
			{
				num5 -= num2;
			}
			else
			{
				num6 -= num2;
			}
			if (this._rgb)
			{
				int num7 = maxLightArrayX;
				if (this._states.Length <= num7 + num)
				{
					num7 = this._states.Length - num - 1;
				}
				for (int i = num3; i < num7; i++)
				{
					LegacyLighting.LightingState[] array = this._states[i];
					LegacyLighting.LightingState[] array2 = this._states[i + num];
					int num8 = num6;
					if (array2.Length <= num8 + num)
					{
						num8 = array2.Length - num2 - 1;
					}
					for (int j = num5; j < num8; j++)
					{
						LegacyLighting.LightingState lightingState3 = array[j];
						LegacyLighting.LightingState lightingState = array2[j + num2];
						lightingState3.R = lightingState.R2;
						lightingState3.G = lightingState.G2;
						lightingState3.B = lightingState.B2;
					}
				}
				return;
			}
			int num9 = num4;
			if (this._states.Length <= num9 + num)
			{
				num9 = this._states.Length - num - 1;
			}
			for (int k = num3; k < num9; k++)
			{
				LegacyLighting.LightingState[] array3 = this._states[k];
				LegacyLighting.LightingState[] array4 = this._states[k + num];
				int num10 = num6;
				if (array4.Length <= num10 + num)
				{
					num10 = array4.Length - num2 - 1;
				}
				for (int l = num5; l < num10; l++)
				{
					LegacyLighting.LightingState lightingState4 = array3[l];
					LegacyLighting.LightingState lightingState2 = array4[l + num2];
					lightingState4.R = lightingState2.R2;
					lightingState4.G = lightingState2.R2;
					lightingState4.B = lightingState2.R2;
				}
			}
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x0057F88C File Offset: 0x0057DA8C
		public void Clear()
		{
			int num = (int)this._camera.UnscaledSize.X / 16 + Lighting.OffScreenTiles * 2;
			int num2 = (int)this._camera.UnscaledSize.Y / 16 + Lighting.OffScreenTiles * 2;
			for (int i = 0; i < num; i++)
			{
				if (i < this._states.Length)
				{
					LegacyLighting.LightingState[] array = this._states[i];
					for (int j = 0; j < num2; j++)
					{
						if (j < array.Length)
						{
							LegacyLighting.LightingState lightingState = array[j];
							lightingState.R = 0f;
							lightingState.G = 0f;
							lightingState.B = 0f;
						}
					}
				}
			}
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x0057F930 File Offset: 0x0057DB30
		private void PreRenderPhase()
		{
			float num16 = (float)Main.tileColor.R / 255f;
			float num17 = (float)Main.tileColor.G / 255f;
			float num18 = (float)Main.tileColor.B / 255f;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num = 0;
			int num2 = (int)this._camera.UnscaledSize.X / 16 + Lighting.OffScreenTiles * 2 + 10;
			int num3 = 0;
			int num4 = (int)this._camera.UnscaledSize.Y / 16 + Lighting.OffScreenTiles * 2 + 10;
			this._minX = num2;
			this._maxX = num;
			this._minY = num4;
			this._maxY = num3;
			this._rgb = (this.Mode == 0 || this.Mode == 3);
			for (int i = num; i < num2; i++)
			{
				LegacyLighting.LightingState[] array = this._states[i];
				for (int j = num3; j < num4; j++)
				{
					LegacyLighting.LightingState lightingState4 = array[j];
					lightingState4.R2 = 0f;
					lightingState4.G2 = 0f;
					lightingState4.B2 = 0f;
					lightingState4.StopLight = false;
					lightingState4.WetLight = false;
					lightingState4.HoneyLight = false;
				}
			}
			if (Main.wofNPCIndex >= 0 && Main.player[Main.myPlayer].gross)
			{
				try
				{
					int num5 = (int)this._camera.UnscaledPosition.Y / 16 - 10;
					int num6 = (int)(this._camera.UnscaledPosition.Y + this._camera.UnscaledSize.Y) / 16 + 10;
					int num7 = (int)Main.npc[Main.wofNPCIndex].position.X / 16;
					num7 = ((Main.npc[Main.wofNPCIndex].direction <= 0) ? (num7 + 2) : (num7 - 3));
					int num8 = num7 + 8;
					float num9 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
					float num10 = 0.3f;
					float num11 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
					num9 *= 0.2f;
					num10 *= 0.1f;
					num11 *= 0.3f;
					for (int k = num7; k <= num8; k++)
					{
						LegacyLighting.LightingState[] array2 = this._states[k - num7];
						for (int l = num5; l <= num6; l++)
						{
							LegacyLighting.LightingState lightingState = array2[l - this._expandedRectTop];
							if (lightingState.R2 < num9)
							{
								lightingState.R2 = num9;
							}
							if (lightingState.G2 < num10)
							{
								lightingState.G2 = num10;
							}
							if (lightingState.B2 < num11)
							{
								lightingState.B2 = num11;
							}
						}
					}
				}
				catch
				{
				}
			}
			num = Utils.Clamp<int>(this._expandedRectLeft, 5, Main.maxTilesX - 1);
			num2 = Utils.Clamp<int>(this._expandedRectRight, 5, Main.maxTilesX - 1);
			num3 = Utils.Clamp<int>(this._expandedRectTop, 5, Main.maxTilesY - 1);
			num4 = Utils.Clamp<int>(this._expandedRectBottom, 5, Main.maxTilesY - 1);
			Main.SceneMetrics.ScanAndExportToMain(new SceneMetricsScanSettings
			{
				VisualScanArea = new Rectangle?(new Rectangle(num, num3, num2 - num, num4 - num3)),
				BiomeScanCenterPositionInWorld = new Vector2?(Main.LocalPlayer.Center),
				ScanOreFinderData = Main.LocalPlayer.accOreFinder
			});
			this._tileScanner.Update();
			this._tileScanner.ExportTo(new Rectangle(num, num3, num2 - num, num4 - num3), this._lightMap, new TileLightScannerOptions
			{
				DrawInvisibleWalls = Main.ShouldShowInvisibleWalls()
			});
			for (int m = num; m < num2; m++)
			{
				LegacyLighting.LightingState[] array3 = this._states[m - this._expandedRectLeft];
				for (int n = num3; n < num4; n++)
				{
					LegacyLighting.LightingState lightingState2 = array3[n - this._expandedRectTop];
					Tile tile = Main.tile[m, n];
					if (tile == null)
					{
						Main.tile[m, n] = default(Tile);
					}
					Vector3 color;
					this._lightMap.GetLight(m - num, n - num3, out color);
					if (this._rgb)
					{
						lightingState2.R2 = color.X;
						lightingState2.G2 = color.Y;
						lightingState2.B2 = color.Z;
					}
					else
					{
						lightingState2.B2 = (lightingState2.G2 = (lightingState2.R2 = (color.X + color.Y + color.Z) / 3f));
					}
					switch (this._lightMap.GetMask(m - num, n - num3))
					{
					case LightMaskMode.Solid:
						lightingState2.StopLight = true;
						break;
					case LightMaskMode.Water:
						lightingState2.WetLight = true;
						break;
					case LightMaskMode.Honey:
						lightingState2.WetLight = true;
						lightingState2.HoneyLight = true;
						break;
					}
					if (lightingState2.R2 > 0f || (this._rgb && (lightingState2.G2 > 0f || lightingState2.B2 > 0f)))
					{
						int num12 = m - this._expandedRectLeft;
						int num13 = n - this._expandedRectTop;
						if (this._minX > num12)
						{
							this._minX = num12;
						}
						if (this._maxX < num12 + 1)
						{
							this._maxX = num12 + 1;
						}
						if (this._minY > num13)
						{
							this._minY = num13;
						}
						if (this._maxY < num13 + 1)
						{
							this._maxY = num13 + 1;
						}
					}
				}
			}
			foreach (KeyValuePair<Point16, LegacyLighting.ColorTriplet> tempLight in this._tempLights)
			{
				int num14 = (int)tempLight.Key.X - this._requestedRectLeft + Lighting.OffScreenTiles;
				int num15 = (int)tempLight.Key.Y - this._requestedRectTop + Lighting.OffScreenTiles;
				if (num14 >= 0 && (float)num14 < this._camera.UnscaledSize.X / 16f + (float)(Lighting.OffScreenTiles * 2) + 10f && num15 >= 0 && (float)num15 < this._camera.UnscaledSize.Y / 16f + (float)(Lighting.OffScreenTiles * 2) + 10f)
				{
					LegacyLighting.LightingState lightingState3 = this._states[num14][num15];
					if (lightingState3.R2 < tempLight.Value.R)
					{
						lightingState3.R2 = tempLight.Value.R;
					}
					if (lightingState3.G2 < tempLight.Value.G)
					{
						lightingState3.G2 = tempLight.Value.G;
					}
					if (lightingState3.B2 < tempLight.Value.B)
					{
						lightingState3.B2 = tempLight.Value.B;
					}
					if (this._minX > num14)
					{
						this._minX = num14;
					}
					if (this._maxX < num14 + 1)
					{
						this._maxX = num14 + 1;
					}
					if (this._minY > num15)
					{
						this._minY = num15;
					}
					if (this._maxY < num15 + 1)
					{
						this._maxY = num15 + 1;
					}
				}
			}
			if (!Main.gamePaused)
			{
				this._tempLights.Clear();
			}
			this._minX += this._expandedRectLeft;
			this._maxX += this._expandedRectLeft;
			this._minY += this._expandedRectTop;
			this._maxY += this._expandedRectTop;
			this._minBoundArea.Set(this._minX, this._maxX, this._minY, this._maxY);
			this._requestedArea.Set(this._requestedRectLeft, this._requestedRectRight, this._requestedRectTop, this._requestedRectBottom);
			this._expandedArea.Set(this._expandedRectLeft, this._expandedRectRight, this._expandedRectTop, this._expandedRectBottom);
			this._offScreenTiles2ExpandedArea.Set(this._requestedRectLeft - this._offScreenTiles2, this._requestedRectRight + this._offScreenTiles2, this._requestedRectTop - this._offScreenTiles2, this._requestedRectBottom + this._offScreenTiles2);
			this._scrX = (int)Math.Floor((double)(this._camera.UnscaledPosition.X / 16f));
			this._scrY = (int)Math.Floor((double)(this._camera.UnscaledPosition.Y / 16f));
			Main.renderCount = 0;
			TimeLogger.LightingTime(0, stopwatch.Elapsed.TotalMilliseconds);
			this.DoColors();
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x0058020C File Offset: 0x0057E40C
		private void DoColors()
		{
			if (this.IsColorOrWhiteMode)
			{
				this._blueWave += (float)this._blueDir * 0.0001f;
				if (this._blueWave > 1f)
				{
					this._blueWave = 1f;
					this._blueDir = -1;
				}
				else if (this._blueWave < 0.97f)
				{
					this._blueWave = 0.97f;
					this._blueDir = 1;
				}
				if (this._rgb)
				{
					this._negLight = 0.91f;
					this._negLight2 = 0.56f;
					this._honeyLightG = 0.7f * this._negLight * this._blueWave;
					this._honeyLightR = 0.75f * this._negLight * this._blueWave;
					this._honeyLightB = 0.6f * this._negLight * this._blueWave;
					switch (Main.waterStyle)
					{
					case 0:
					case 1:
					case 7:
					case 8:
						this._wetLightG = 0.96f * this._negLight * this._blueWave;
						this._wetLightR = 0.88f * this._negLight * this._blueWave;
						this._wetLightB = 1.015f * this._negLight * this._blueWave;
						goto IL_403;
					case 2:
						this._wetLightG = 0.85f * this._negLight * this._blueWave;
						this._wetLightR = 0.94f * this._negLight * this._blueWave;
						this._wetLightB = 1.01f * this._negLight * this._blueWave;
						goto IL_403;
					case 3:
						this._wetLightG = 0.95f * this._negLight * this._blueWave;
						this._wetLightR = 0.84f * this._negLight * this._blueWave;
						this._wetLightB = 1.015f * this._negLight * this._blueWave;
						goto IL_403;
					case 4:
						this._wetLightG = 0.86f * this._negLight * this._blueWave;
						this._wetLightR = 0.9f * this._negLight * this._blueWave;
						this._wetLightB = 1.01f * this._negLight * this._blueWave;
						goto IL_403;
					case 5:
						this._wetLightG = 0.99f * this._negLight * this._blueWave;
						this._wetLightR = 0.84f * this._negLight * this._blueWave;
						this._wetLightB = 1.01f * this._negLight * this._blueWave;
						goto IL_403;
					case 6:
						this._wetLightR = 0.83f * this._negLight * this._blueWave;
						this._wetLightG = 0.93f * this._negLight * this._blueWave;
						this._wetLightB = 0.98f * this._negLight * this._blueWave;
						goto IL_403;
					case 9:
						this._wetLightG = 0.88f * this._negLight * this._blueWave;
						this._wetLightR = 1f * this._negLight * this._blueWave;
						this._wetLightB = 0.84f * this._negLight * this._blueWave;
						goto IL_403;
					case 10:
						this._wetLightG = 1f * this._negLight * this._blueWave;
						this._wetLightR = 0.83f * this._negLight * this._blueWave;
						this._wetLightB = 1f * this._negLight * this._blueWave;
						goto IL_403;
					case 12:
						this._wetLightG = 0.98f * this._negLight * this._blueWave;
						this._wetLightR = 0.95f * this._negLight * this._blueWave;
						this._wetLightB = 0.85f * this._negLight * this._blueWave;
						goto IL_403;
					}
					this._wetLightG = 0f;
					this._wetLightR = 0f;
					this._wetLightB = 0f;
					IL_403:
					LoaderManager.Get<WaterStylesLoader>().LightColorMultiplier(Main.waterStyle, this._negLight * this._blueWave, ref this._wetLightR, ref this._wetLightG, ref this._wetLightB);
				}
				else
				{
					this._negLight = 0.9f;
					this._negLight2 = 0.54f;
					this._wetLightR = 0.95f * this._negLight * this._blueWave;
				}
				if (Main.player[Main.myPlayer].nightVision)
				{
					this._negLight *= 1.03f;
					this._negLight2 *= 1.03f;
				}
				if (Main.player[Main.myPlayer].blind)
				{
					this._negLight *= 0.95f;
					this._negLight2 *= 0.95f;
				}
				if (Main.player[Main.myPlayer].blackout)
				{
					this._negLight *= 0.85f;
					this._negLight2 *= 0.85f;
				}
				if (Main.player[Main.myPlayer].headcovered)
				{
					this._negLight *= 0.85f;
					this._negLight2 *= 0.85f;
				}
				SystemLoader.ModifyLightingBrightness(ref this._negLight, ref this._negLight2);
			}
			else
			{
				this._negLight = 0.04f;
				this._negLight2 = 0.16f;
				if (Main.player[Main.myPlayer].nightVision)
				{
					this._negLight -= 0.013f;
					this._negLight2 -= 0.04f;
				}
				if (Main.player[Main.myPlayer].blind)
				{
					this._negLight += 0.03f;
					this._negLight2 += 0.06f;
				}
				if (Main.player[Main.myPlayer].blackout)
				{
					this._negLight += 0.09f;
					this._negLight2 += 0.18f;
				}
				if (Main.player[Main.myPlayer].headcovered)
				{
					this._negLight += 0.09f;
					this._negLight2 += 0.18f;
				}
				SystemLoader.ModifyLightingBrightness(ref this._negLight, ref this._negLight2);
				this._wetLightR = this._negLight * 1.2f;
				this._wetLightG = this._negLight * 1.1f;
			}
			int num;
			int num2;
			switch (Main.renderCount)
			{
			case 0:
				num = 0;
				num2 = 1;
				break;
			case 1:
				num = 1;
				num2 = 3;
				break;
			case 2:
				num = 3;
				num2 = 4;
				break;
			default:
				num = 0;
				num2 = 0;
				break;
			}
			Stopwatch stopwatch = new Stopwatch();
			int left = this._expandedArea.Left;
			int top = this._expandedArea.Top;
			for (int i = num; i < num2; i++)
			{
				stopwatch.Restart();
				int num3 = 0;
				int num4 = 0;
				switch (i)
				{
				case 0:
					this._swipe.InnerLoop1Start = this._minBoundArea.Top - top;
					this._swipe.InnerLoop2Start = this._minBoundArea.Bottom - top;
					this._swipe.InnerLoop1End = this._requestedArea.Bottom + LegacyLighting.RenderPhases - top;
					this._swipe.InnerLoop2End = this._requestedArea.Top - LegacyLighting.RenderPhases - top;
					num3 = this._minBoundArea.Left - left;
					num4 = this._minBoundArea.Right - left;
					this._swipe.JaggedArray = this._states;
					break;
				case 1:
					this._swipe.InnerLoop1Start = this._expandedArea.Left - left;
					this._swipe.InnerLoop2Start = this._expandedArea.Right - left;
					this._swipe.InnerLoop1End = this._requestedArea.Right + LegacyLighting.RenderPhases - left;
					this._swipe.InnerLoop2End = this._requestedArea.Left - LegacyLighting.RenderPhases - left;
					num3 = this._expandedArea.Top - top;
					num4 = this._expandedArea.Bottom - top;
					this._swipe.JaggedArray = this._axisFlipStates;
					break;
				case 2:
					this._swipe.InnerLoop1Start = this._offScreenTiles2ExpandedArea.Top - top;
					this._swipe.InnerLoop2Start = this._offScreenTiles2ExpandedArea.Bottom - top;
					this._swipe.InnerLoop1End = this._requestedArea.Bottom + LegacyLighting.RenderPhases - top;
					this._swipe.InnerLoop2End = this._requestedArea.Top - LegacyLighting.RenderPhases - top;
					num3 = this._offScreenTiles2ExpandedArea.Left - left;
					num4 = this._offScreenTiles2ExpandedArea.Right - left;
					this._swipe.JaggedArray = this._states;
					break;
				case 3:
					this._swipe.InnerLoop1Start = this._offScreenTiles2ExpandedArea.Left - left;
					this._swipe.InnerLoop2Start = this._offScreenTiles2ExpandedArea.Right - left;
					this._swipe.InnerLoop1End = this._requestedArea.Right + LegacyLighting.RenderPhases - left;
					this._swipe.InnerLoop2End = this._requestedArea.Left - LegacyLighting.RenderPhases - left;
					num3 = this._offScreenTiles2ExpandedArea.Top - top;
					num4 = this._offScreenTiles2ExpandedArea.Bottom - top;
					this._swipe.JaggedArray = this._axisFlipStates;
					break;
				}
				if (this._swipe.InnerLoop1Start > this._swipe.InnerLoop1End)
				{
					this._swipe.InnerLoop1Start = this._swipe.InnerLoop1End;
				}
				if (this._swipe.InnerLoop2Start < this._swipe.InnerLoop2End)
				{
					this._swipe.InnerLoop2Start = this._swipe.InnerLoop2End;
				}
				if (num3 > num4)
				{
					num3 = num4;
				}
				ParallelForAction callback;
				switch (this.Mode)
				{
				case 0:
					callback = new ParallelForAction(this.doColors_Mode0_Swipe);
					break;
				case 1:
					callback = new ParallelForAction(this.doColors_Mode1_Swipe);
					break;
				case 2:
					callback = new ParallelForAction(this.doColors_Mode2_Swipe);
					break;
				case 3:
					callback = new ParallelForAction(this.doColors_Mode3_Swipe);
					break;
				default:
					callback = new ParallelForAction(this.doColors_Mode0_Swipe);
					break;
				}
				FastParallel.For(num3, num4, callback, this._swipe);
				LegacyLighting._swipeRandom.NextSeed();
				TimeLogger.LightingTime(i + 1, stopwatch.Elapsed.TotalMilliseconds);
			}
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x00580CAC File Offset: 0x0057EEAC
		private void doColors_Mode0_Swipe(int outerLoopStart, int outerLoopEnd, object context)
		{
			LegacyLighting.LightingSwipeData lightingSwipeData = context as LegacyLighting.LightingSwipeData;
			FastRandom fastRandom = default(FastRandom);
			try
			{
				bool flag = true;
				for (;;)
				{
					int num;
					int val;
					int val2;
					if (flag)
					{
						num = 1;
						val = lightingSwipeData.InnerLoop1Start;
						val2 = lightingSwipeData.InnerLoop1End;
					}
					else
					{
						num = -1;
						val = lightingSwipeData.InnerLoop2Start;
						val2 = lightingSwipeData.InnerLoop2End;
					}
					for (int i = outerLoopStart; i < outerLoopEnd; i++)
					{
						LegacyLighting.LightingState[] array = lightingSwipeData.JaggedArray[i];
						float num2 = 0f;
						float num3 = 0f;
						float num4 = 0f;
						int num5 = Math.Min(array.Length - 1, Math.Max(0, val));
						int num6 = Math.Min(array.Length - 1, Math.Max(0, val2));
						int j = num5;
						while (j != num6)
						{
							LegacyLighting.LightingState lightingState = array[j];
							LegacyLighting.LightingState lightingState2 = array[j + num];
							bool flag3;
							bool flag2 = flag3 = false;
							if (lightingState.R2 > num2)
							{
								num2 = lightingState.R2;
							}
							else if ((double)num2 <= 0.0185)
							{
								flag3 = true;
							}
							else if (lightingState.R2 < num2)
							{
								lightingState.R2 = num2;
							}
							if (lightingState.WetLight)
							{
								fastRandom = LegacyLighting._swipeRandom.WithModifier((ulong)((long)(i * 1000 + j)));
							}
							if (!flag3 && lightingState2.R2 <= num2)
							{
								num2 = (lightingState.StopLight ? (num2 * this._negLight2) : ((!lightingState.WetLight) ? (num2 * this._negLight) : ((!lightingState.HoneyLight) ? (num2 * (this._wetLightR * (float)fastRandom.Next(98, 100) * 0.01f)) : (num2 * (this._honeyLightR * (float)fastRandom.Next(98, 100) * 0.01f)))));
							}
							if (lightingState.G2 > num3)
							{
								num3 = lightingState.G2;
							}
							else if ((double)num3 <= 0.0185)
							{
								flag2 = true;
							}
							else
							{
								lightingState.G2 = num3;
							}
							if (!flag2 && lightingState2.G2 <= num3)
							{
								num3 = (lightingState.StopLight ? (num3 * this._negLight2) : ((!lightingState.WetLight) ? (num3 * this._negLight) : ((!lightingState.HoneyLight) ? (num3 * (this._wetLightG * (float)fastRandom.Next(97, 100) * 0.01f)) : (num3 * (this._honeyLightG * (float)fastRandom.Next(97, 100) * 0.01f)))));
							}
							if (lightingState.B2 > num4)
							{
								num4 = lightingState.B2;
								goto IL_262;
							}
							if ((double)num4 > 0.0185)
							{
								lightingState.B2 = num4;
								goto IL_262;
							}
							IL_2DA:
							j += num;
							continue;
							IL_262:
							if (lightingState2.B2 < num4)
							{
								num4 = ((!lightingState.StopLight) ? ((!lightingState.WetLight) ? (num4 * this._negLight) : ((!lightingState.HoneyLight) ? (num4 * (this._wetLightB * (float)fastRandom.Next(97, 100) * 0.01f)) : (num4 * (this._honeyLightB * (float)fastRandom.Next(97, 100) * 0.01f)))) : (num4 * this._negLight2));
								goto IL_2DA;
							}
							goto IL_2DA;
						}
					}
					if (!flag)
					{
						break;
					}
					flag = false;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x00580FDC File Offset: 0x0057F1DC
		private void doColors_Mode1_Swipe(int outerLoopStart, int outerLoopEnd, object context)
		{
			LegacyLighting.LightingSwipeData lightingSwipeData = context as LegacyLighting.LightingSwipeData;
			FastRandom fastRandom = default(FastRandom);
			try
			{
				bool flag = true;
				for (;;)
				{
					int num;
					int num2;
					int num3;
					if (flag)
					{
						num = 1;
						num2 = lightingSwipeData.InnerLoop1Start;
						num3 = lightingSwipeData.InnerLoop1End;
					}
					else
					{
						num = -1;
						num2 = lightingSwipeData.InnerLoop2Start;
						num3 = lightingSwipeData.InnerLoop2End;
					}
					for (int i = outerLoopStart; i < outerLoopEnd; i++)
					{
						LegacyLighting.LightingState[] array = lightingSwipeData.JaggedArray[i];
						float num4 = 0f;
						int j = num2;
						while (j != num3)
						{
							LegacyLighting.LightingState lightingState = array[j];
							if (lightingState.R2 > num4)
							{
								num4 = lightingState.R2;
								goto IL_9F;
							}
							if ((double)num4 > 0.0185)
							{
								if (lightingState.R2 < num4)
								{
									lightingState.R2 = num4;
									goto IL_9F;
								}
								goto IL_9F;
							}
							IL_13A:
							j += num;
							continue;
							IL_9F:
							if (array[j + num].R2 > num4)
							{
								goto IL_13A;
							}
							if (lightingState.StopLight)
							{
								num4 *= this._negLight2;
								goto IL_13A;
							}
							if (lightingState.WetLight)
							{
								fastRandom = LegacyLighting._swipeRandom.WithModifier((ulong)((long)(i * 1000 + j)));
								num4 = ((!lightingState.HoneyLight) ? (num4 * (this._wetLightR * (float)fastRandom.Next(98, 100) * 0.01f)) : (num4 * (this._honeyLightR * (float)fastRandom.Next(98, 100) * 0.01f)));
								goto IL_13A;
							}
							num4 *= this._negLight;
							goto IL_13A;
						}
					}
					if (!flag)
					{
						break;
					}
					flag = false;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x0058116C File Offset: 0x0057F36C
		private void doColors_Mode2_Swipe(int outerLoopStart, int outerLoopEnd, object context)
		{
			LegacyLighting.LightingSwipeData lightingSwipeData = context as LegacyLighting.LightingSwipeData;
			try
			{
				bool flag = true;
				for (;;)
				{
					int num;
					int num2;
					int num3;
					if (flag)
					{
						num = 1;
						num2 = lightingSwipeData.InnerLoop1Start;
						num3 = lightingSwipeData.InnerLoop1End;
					}
					else
					{
						num = -1;
						num2 = lightingSwipeData.InnerLoop2Start;
						num3 = lightingSwipeData.InnerLoop2End;
					}
					for (int i = outerLoopStart; i < outerLoopEnd; i++)
					{
						LegacyLighting.LightingState[] array = lightingSwipeData.JaggedArray[i];
						float num4 = 0f;
						int j = num2;
						while (j != num3)
						{
							LegacyLighting.LightingState lightingState = array[j];
							if (lightingState.R2 > num4)
							{
								num4 = lightingState.R2;
								goto IL_7E;
							}
							if (num4 > 0f)
							{
								lightingState.R2 = num4;
								goto IL_7E;
							}
							IL_B1:
							j += num;
							continue;
							IL_7E:
							num4 = ((!lightingState.StopLight) ? ((!lightingState.WetLight) ? (num4 - this._negLight) : (num4 - this._wetLightR)) : (num4 - this._negLight2));
							goto IL_B1;
						}
					}
					if (!flag)
					{
						break;
					}
					flag = false;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x00581264 File Offset: 0x0057F464
		private void doColors_Mode3_Swipe(int outerLoopStart, int outerLoopEnd, object context)
		{
			LegacyLighting.LightingSwipeData lightingSwipeData = context as LegacyLighting.LightingSwipeData;
			try
			{
				bool flag = true;
				for (;;)
				{
					int num;
					int num2;
					int num3;
					if (flag)
					{
						num = 1;
						num2 = lightingSwipeData.InnerLoop1Start;
						num3 = lightingSwipeData.InnerLoop1End;
					}
					else
					{
						num = -1;
						num2 = lightingSwipeData.InnerLoop2Start;
						num3 = lightingSwipeData.InnerLoop2End;
					}
					for (int i = outerLoopStart; i < outerLoopEnd; i++)
					{
						LegacyLighting.LightingState[] array = lightingSwipeData.JaggedArray[i];
						float num4 = 0f;
						float num5 = 0f;
						float num6 = 0f;
						int j = num2;
						while (j != num3)
						{
							LegacyLighting.LightingState lightingState = array[j];
							bool flag3;
							bool flag2 = flag3 = false;
							if (lightingState.R2 > num4)
							{
								num4 = lightingState.R2;
							}
							else if (num4 <= 0f)
							{
								flag3 = true;
							}
							else
							{
								lightingState.R2 = num4;
							}
							if (!flag3)
							{
								num4 = (lightingState.StopLight ? (num4 - this._negLight2) : ((!lightingState.WetLight) ? (num4 - this._negLight) : (num4 - this._wetLightR)));
							}
							if (lightingState.G2 > num5)
							{
								num5 = lightingState.G2;
							}
							else if (num5 <= 0f)
							{
								flag2 = true;
							}
							else
							{
								lightingState.G2 = num5;
							}
							if (!flag2)
							{
								num5 = (lightingState.StopLight ? (num5 - this._negLight2) : ((!lightingState.WetLight) ? (num5 - this._negLight) : (num5 - this._wetLightG)));
							}
							if (lightingState.B2 > num6)
							{
								num6 = lightingState.B2;
								goto IL_15D;
							}
							if (num6 > 0f)
							{
								lightingState.B2 = num6;
								goto IL_15D;
							}
							IL_17C:
							j += num;
							continue;
							IL_15D:
							num6 = ((!lightingState.StopLight) ? (num6 - this._negLight) : (num6 - this._negLight2));
							goto IL_17C;
						}
					}
					if (!flag)
					{
						break;
					}
					flag = false;
				}
			}
			catch
			{
			}
		}

		// Token: 0x0400508F RID: 20623
		public static int RenderPhases = 4;

		// Token: 0x04005090 RID: 20624
		private bool _rgb = true;

		// Token: 0x04005091 RID: 20625
		private int _offScreenTiles2 = 35;

		// Token: 0x04005092 RID: 20626
		private float _oldSkyColor;

		// Token: 0x04005093 RID: 20627
		private float _skyColor;

		// Token: 0x04005094 RID: 20628
		private int _requestedRectLeft;

		// Token: 0x04005095 RID: 20629
		private int _requestedRectRight;

		// Token: 0x04005096 RID: 20630
		private int _requestedRectTop;

		// Token: 0x04005097 RID: 20631
		private int _requestedRectBottom;

		// Token: 0x04005098 RID: 20632
		private LegacyLighting.LightingState[][] _states;

		// Token: 0x04005099 RID: 20633
		private LegacyLighting.LightingState[][] _axisFlipStates;

		// Token: 0x0400509A RID: 20634
		private LegacyLighting.LightingSwipeData _swipe;

		// Token: 0x0400509B RID: 20635
		private LegacyLighting.LightingSwipeData[] _threadSwipes;

		// Token: 0x0400509C RID: 20636
		private int _scrX;

		// Token: 0x0400509D RID: 20637
		private int _scrY;

		// Token: 0x0400509E RID: 20638
		private int _minX;

		// Token: 0x0400509F RID: 20639
		private int _maxX;

		// Token: 0x040050A0 RID: 20640
		private int _minY;

		// Token: 0x040050A1 RID: 20641
		private int _maxY;

		// Token: 0x040050A2 RID: 20642
		private const int MAX_TEMP_LIGHTS = 2000;

		// Token: 0x040050A3 RID: 20643
		private Dictionary<Point16, LegacyLighting.ColorTriplet> _tempLights;

		// Token: 0x040050A4 RID: 20644
		private int _expandedRectLeft;

		// Token: 0x040050A5 RID: 20645
		private int _expandedRectTop;

		// Token: 0x040050A6 RID: 20646
		private int _expandedRectRight;

		// Token: 0x040050A7 RID: 20647
		private int _expandedRectBottom;

		// Token: 0x040050A8 RID: 20648
		private float _negLight = 0.04f;

		// Token: 0x040050A9 RID: 20649
		private float _negLight2 = 0.16f;

		// Token: 0x040050AA RID: 20650
		private float _wetLightR = 0.16f;

		// Token: 0x040050AB RID: 20651
		private float _wetLightG = 0.16f;

		// Token: 0x040050AC RID: 20652
		private float _wetLightB = 0.16f;

		// Token: 0x040050AD RID: 20653
		private float _honeyLightR = 0.16f;

		// Token: 0x040050AE RID: 20654
		private float _honeyLightG = 0.16f;

		// Token: 0x040050AF RID: 20655
		private float _honeyLightB = 0.16f;

		// Token: 0x040050B0 RID: 20656
		private float _blueWave = 1f;

		// Token: 0x040050B1 RID: 20657
		private int _blueDir = 1;

		// Token: 0x040050B2 RID: 20658
		private LegacyLighting.RectArea _minBoundArea;

		// Token: 0x040050B3 RID: 20659
		private LegacyLighting.RectArea _requestedArea;

		// Token: 0x040050B4 RID: 20660
		private LegacyLighting.RectArea _expandedArea;

		// Token: 0x040050B5 RID: 20661
		private LegacyLighting.RectArea _offScreenTiles2ExpandedArea;

		// Token: 0x040050B6 RID: 20662
		private TileLightScanner _tileScanner = new TileLightScanner();

		// Token: 0x040050B7 RID: 20663
		private readonly Camera _camera;

		// Token: 0x040050B8 RID: 20664
		private static FastRandom _swipeRandom = FastRandom.CreateWithRandomSeed();

		// Token: 0x040050B9 RID: 20665
		private LightMap _lightMap = new LightMap();

		// Token: 0x02000B7A RID: 2938
		public struct RectArea
		{
			// Token: 0x06005CF2 RID: 23794 RVA: 0x006C5532 File Offset: 0x006C3732
			public void Set(int left, int right, int top, int bottom)
			{
				this.Left = left;
				this.Right = right;
				this.Top = top;
				this.Bottom = bottom;
			}

			// Token: 0x040075FF RID: 30207
			public int Left;

			// Token: 0x04007600 RID: 30208
			public int Right;

			// Token: 0x04007601 RID: 30209
			public int Top;

			// Token: 0x04007602 RID: 30210
			public int Bottom;
		}

		// Token: 0x02000B7B RID: 2939
		private class LightingSwipeData
		{
			// Token: 0x06005CF3 RID: 23795 RVA: 0x006C5551 File Offset: 0x006C3751
			public LightingSwipeData()
			{
				this.InnerLoop1Start = 0;
				this.InnerLoop1End = 0;
				this.InnerLoop2Start = 0;
				this.InnerLoop2End = 0;
			}

			// Token: 0x06005CF4 RID: 23796 RVA: 0x006C5575 File Offset: 0x006C3775
			public void CopyFrom(LegacyLighting.LightingSwipeData from)
			{
				this.InnerLoop1Start = from.InnerLoop1Start;
				this.InnerLoop1End = from.InnerLoop1End;
				this.InnerLoop2Start = from.InnerLoop2Start;
				this.InnerLoop2End = from.InnerLoop2End;
				this.JaggedArray = from.JaggedArray;
			}

			// Token: 0x04007603 RID: 30211
			public int InnerLoop1Start;

			// Token: 0x04007604 RID: 30212
			public int InnerLoop1End;

			// Token: 0x04007605 RID: 30213
			public int InnerLoop2Start;

			// Token: 0x04007606 RID: 30214
			public int InnerLoop2End;

			// Token: 0x04007607 RID: 30215
			public LegacyLighting.LightingState[][] JaggedArray;
		}

		// Token: 0x02000B7C RID: 2940
		private class LightingState
		{
			// Token: 0x06005CF5 RID: 23797 RVA: 0x006C55B3 File Offset: 0x006C37B3
			public Vector3 ToVector3()
			{
				return new Vector3(this.R, this.G, this.B);
			}

			// Token: 0x04007608 RID: 30216
			public float R;

			// Token: 0x04007609 RID: 30217
			public float R2;

			// Token: 0x0400760A RID: 30218
			public float G;

			// Token: 0x0400760B RID: 30219
			public float G2;

			// Token: 0x0400760C RID: 30220
			public float B;

			// Token: 0x0400760D RID: 30221
			public float B2;

			// Token: 0x0400760E RID: 30222
			public bool StopLight;

			// Token: 0x0400760F RID: 30223
			public bool WetLight;

			// Token: 0x04007610 RID: 30224
			public bool HoneyLight;
		}

		// Token: 0x02000B7D RID: 2941
		private struct ColorTriplet
		{
			// Token: 0x06005CF7 RID: 23799 RVA: 0x006C55D4 File Offset: 0x006C37D4
			public ColorTriplet(float R, float G, float B)
			{
				this.R = R;
				this.G = G;
				this.B = B;
			}

			// Token: 0x06005CF8 RID: 23800 RVA: 0x006C55EC File Offset: 0x006C37EC
			public ColorTriplet(float averageColor)
			{
				this.B = averageColor;
				this.G = averageColor;
				this.R = averageColor;
			}

			// Token: 0x04007611 RID: 30225
			public float R;

			// Token: 0x04007612 RID: 30226
			public float G;

			// Token: 0x04007613 RID: 30227
			public float B;
		}
	}
}
