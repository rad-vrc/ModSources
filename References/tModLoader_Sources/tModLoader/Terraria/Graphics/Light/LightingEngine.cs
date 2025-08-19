using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using ReLogic.Threading;
using Terraria.ModLoader;

namespace Terraria.Graphics.Light
{
	// Token: 0x02000461 RID: 1121
	public class LightingEngine : ILightingEngine
	{
		// Token: 0x060036DA RID: 14042 RVA: 0x0058144A File Offset: 0x0057F64A
		public void AddLight(int x, int y, Vector3 color)
		{
			this._perFrameLights.Add(new LightingEngine.PerFrameLight(new Point(x, y), color));
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x00581464 File Offset: 0x0057F664
		public void Clear()
		{
			this._activeLightMap.Clear();
			this._workingLightMap.Clear();
			this._perFrameLights.Clear();
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x00581488 File Offset: 0x0057F688
		public Vector3 GetColor(int x, int y)
		{
			if (!this._activeProcessedArea.Contains(x, y))
			{
				return Vector3.Zero;
			}
			x -= this._activeProcessedArea.X;
			y -= this._activeProcessedArea.Y;
			return this._activeLightMap[x, y];
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x005814D8 File Offset: 0x0057F6D8
		public void ProcessArea(Rectangle area)
		{
			Main.renderCount = (Main.renderCount + 1) % 4;
			this._timer.Start();
			TimeLogger.LightingTime(0, 0.0);
			switch (this._state)
			{
			case LightingEngine.EngineState.MinimapUpdate:
				if (Main.mapDelay > 0)
				{
					Main.mapDelay--;
				}
				else
				{
					this.ExportToMiniMap();
				}
				TimeLogger.LightingTime(1, this._timer.Elapsed.TotalMilliseconds);
				break;
			case LightingEngine.EngineState.ExportMetrics:
				area.Inflate(28, 28);
				Main.SceneMetrics.ScanAndExportToMain(new SceneMetricsScanSettings
				{
					VisualScanArea = new Rectangle?(area),
					BiomeScanCenterPositionInWorld = new Vector2?(Main.LocalPlayer.Center),
					ScanOreFinderData = Main.LocalPlayer.accOreFinder
				});
				TimeLogger.LightingTime(2, this._timer.Elapsed.TotalMilliseconds);
				break;
			case LightingEngine.EngineState.Scan:
				this.ProcessScan(area);
				TimeLogger.LightingTime(3, this._timer.Elapsed.TotalMilliseconds);
				break;
			case LightingEngine.EngineState.Blur:
				this.ProcessBlur();
				this.Present();
				TimeLogger.LightingTime(4, this._timer.Elapsed.TotalMilliseconds);
				break;
			}
			this.IncrementState();
			this._timer.Reset();
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x0058162E File Offset: 0x0057F82E
		private void IncrementState()
		{
			this._state = (this._state + 1) % LightingEngine.EngineState.Max;
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x00581640 File Offset: 0x0057F840
		private void ProcessScan(Rectangle area)
		{
			area.Inflate(28, 28);
			this._workingProcessedArea = area;
			this._workingLightMap.SetSize(area.Width, area.Height);
			this._workingLightMap.NonVisiblePadding = 18;
			this._tileScanner.Update();
			this._tileScanner.ExportTo(area, this._workingLightMap, new TileLightScannerOptions
			{
				DrawInvisibleWalls = Main.ShouldShowInvisibleWalls()
			});
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x005816B5 File Offset: 0x0057F8B5
		private void ProcessBlur()
		{
			this.UpdateLightDecay();
			this.ApplyPerFrameLights();
			this._workingLightMap.Blur();
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x005816CE File Offset: 0x0057F8CE
		private void Present()
		{
			Utils.Swap<LightMap>(ref this._activeLightMap, ref this._workingLightMap);
			Utils.Swap<Rectangle>(ref this._activeProcessedArea, ref this._workingProcessedArea);
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x005816F4 File Offset: 0x0057F8F4
		private void UpdateLightDecay()
		{
			LightMap workingLightMap = this._workingLightMap;
			workingLightMap.LightDecayThroughAir = 0.91f;
			workingLightMap.LightDecayThroughSolid = 0.56f;
			workingLightMap.LightDecayThroughHoney = new Vector3(0.75f, 0.7f, 0.6f) * 0.91f;
			switch (Main.waterStyle)
			{
			case 0:
			case 1:
			case 7:
			case 8:
				workingLightMap.LightDecayThroughWater = new Vector3(0.88f, 0.96f, 1.015f) * 0.91f;
				break;
			case 2:
				workingLightMap.LightDecayThroughWater = new Vector3(0.94f, 0.85f, 1.01f) * 0.91f;
				break;
			case 3:
				workingLightMap.LightDecayThroughWater = new Vector3(0.84f, 0.95f, 1.015f) * 0.91f;
				break;
			case 4:
				workingLightMap.LightDecayThroughWater = new Vector3(0.9f, 0.86f, 1.01f) * 0.91f;
				break;
			case 5:
				workingLightMap.LightDecayThroughWater = new Vector3(0.84f, 0.99f, 1.01f) * 0.91f;
				break;
			case 6:
				workingLightMap.LightDecayThroughWater = new Vector3(0.83f, 0.93f, 0.98f) * 0.91f;
				break;
			case 9:
				workingLightMap.LightDecayThroughWater = new Vector3(1f, 0.88f, 0.84f) * 0.91f;
				break;
			case 10:
				workingLightMap.LightDecayThroughWater = new Vector3(0.83f, 1f, 1f) * 0.91f;
				break;
			case 12:
				workingLightMap.LightDecayThroughWater = new Vector3(0.95f, 0.98f, 0.85f) * 0.91f;
				break;
			case 13:
				workingLightMap.LightDecayThroughWater = new Vector3(0.9f, 1f, 1.02f) * 0.91f;
				break;
			}
			float factor = 0.91f;
			float throughWaterR = workingLightMap.LightDecayThroughWater.X;
			float throughWaterG = workingLightMap.LightDecayThroughWater.Y;
			float throughWaterB = workingLightMap.LightDecayThroughWater.Z;
			LoaderManager.Get<WaterStylesLoader>().LightColorMultiplier(Main.waterStyle, factor, ref throughWaterR, ref throughWaterG, ref throughWaterB);
			workingLightMap.LightDecayThroughWater = new Vector3(throughWaterR, throughWaterG, throughWaterB);
			if (Main.player[Main.myPlayer].nightVision)
			{
				workingLightMap.LightDecayThroughAir *= 1.03f;
				workingLightMap.LightDecayThroughSolid *= 1.03f;
			}
			if (Main.player[Main.myPlayer].blind)
			{
				workingLightMap.LightDecayThroughAir *= 0.95f;
				workingLightMap.LightDecayThroughSolid *= 0.95f;
			}
			if (Main.player[Main.myPlayer].blackout)
			{
				workingLightMap.LightDecayThroughAir *= 0.85f;
				workingLightMap.LightDecayThroughSolid *= 0.85f;
			}
			if (Main.player[Main.myPlayer].headcovered)
			{
				workingLightMap.LightDecayThroughAir *= 0.85f;
				workingLightMap.LightDecayThroughSolid *= 0.85f;
			}
			workingLightMap.LightDecayThroughAir *= Player.airLightDecay;
			workingLightMap.LightDecayThroughSolid *= Player.solidLightDecay;
			float throughAir = 1f;
			float throughSolid = 1f;
			SystemLoader.ModifyLightingBrightness(ref throughAir, ref throughSolid);
			workingLightMap.LightDecayThroughAir *= throughAir;
			workingLightMap.LightDecayThroughSolid *= throughSolid;
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x00581A9C File Offset: 0x0057FC9C
		private void ApplyPerFrameLights()
		{
			for (int i = 0; i < this._perFrameLights.Count; i++)
			{
				Point position = this._perFrameLights[i].Position;
				if (this._workingProcessedArea.Contains(position))
				{
					Vector3 value = this._perFrameLights[i].Color;
					Vector3 value2 = this._workingLightMap[position.X - this._workingProcessedArea.X, position.Y - this._workingProcessedArea.Y];
					Vector3.Max(ref value2, ref value, ref value);
					this._workingLightMap[position.X - this._workingProcessedArea.X, position.Y - this._workingProcessedArea.Y] = value;
				}
			}
			this._perFrameLights.Clear();
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x00581B6D File Offset: 0x0057FD6D
		public void Rebuild()
		{
			this._activeProcessedArea = Rectangle.Empty;
			this._workingProcessedArea = Rectangle.Empty;
			this._state = LightingEngine.EngineState.MinimapUpdate;
			this._activeLightMap = new LightMap();
			this._workingLightMap = new LightMap();
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x00581BA4 File Offset: 0x0057FDA4
		private void ExportToMiniMap()
		{
			if (!Main.mapEnabled || this._activeProcessedArea.Width <= 0 || this._activeProcessedArea.Height <= 0)
			{
				return;
			}
			Rectangle area = new Rectangle(this._activeProcessedArea.X + 28, this._activeProcessedArea.Y + 28, this._activeProcessedArea.Width - 56, this._activeProcessedArea.Height - 56);
			Rectangle value;
			value..ctor(0, 0, Main.maxTilesX, Main.maxTilesY);
			value.Inflate(-40, -40);
			area = Rectangle.Intersect(area, value);
			Main.mapMinX = area.Left;
			Main.mapMinY = area.Top;
			Main.mapMaxX = area.Right;
			Main.mapMaxY = area.Bottom;
			FastParallel.For(area.Left, area.Right, delegate(int start, int end, object context)
			{
				for (int i = start; i < end; i++)
				{
					for (int j = area.Top; j < area.Bottom; j++)
					{
						Vector3 vector = this._activeLightMap[i - this._activeProcessedArea.X, j - this._activeProcessedArea.Y];
						float num = Math.Max(Math.Max(vector.X, vector.Y), vector.Z);
						byte light = (byte)Math.Min(255, (int)(num * 255f));
						Main.Map.UpdateLighting(i, j, light);
					}
				}
			}, null);
			Main.updateMap = true;
		}

		// Token: 0x040050BB RID: 20667
		public const int AREA_PADDING = 28;

		// Token: 0x040050BC RID: 20668
		private const int NON_VISIBLE_PADDING = 18;

		// Token: 0x040050BD RID: 20669
		private readonly List<LightingEngine.PerFrameLight> _perFrameLights = new List<LightingEngine.PerFrameLight>();

		// Token: 0x040050BE RID: 20670
		private TileLightScanner _tileScanner = new TileLightScanner();

		// Token: 0x040050BF RID: 20671
		private LightMap _activeLightMap = new LightMap();

		// Token: 0x040050C0 RID: 20672
		private Rectangle _activeProcessedArea;

		// Token: 0x040050C1 RID: 20673
		private LightMap _workingLightMap = new LightMap();

		// Token: 0x040050C2 RID: 20674
		private Rectangle _workingProcessedArea;

		// Token: 0x040050C3 RID: 20675
		private readonly Stopwatch _timer = new Stopwatch();

		// Token: 0x040050C4 RID: 20676
		private LightingEngine.EngineState _state;

		// Token: 0x02000B7E RID: 2942
		private enum EngineState
		{
			// Token: 0x04007615 RID: 30229
			MinimapUpdate,
			// Token: 0x04007616 RID: 30230
			ExportMetrics,
			// Token: 0x04007617 RID: 30231
			Scan,
			// Token: 0x04007618 RID: 30232
			Blur,
			// Token: 0x04007619 RID: 30233
			Max
		}

		// Token: 0x02000B7F RID: 2943
		private struct PerFrameLight
		{
			// Token: 0x06005CF9 RID: 23801 RVA: 0x006C5612 File Offset: 0x006C3812
			public PerFrameLight(Point position, Vector3 color)
			{
				this.Position = position;
				this.Color = color;
			}

			// Token: 0x0400761A RID: 30234
			public readonly Point Position;

			// Token: 0x0400761B RID: 30235
			public readonly Vector3 Color;
		}
	}
}
