using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using ReLogic.Threading;

namespace Terraria.Graphics.Light
{
	// Token: 0x0200011B RID: 283
	public class LightingEngine : ILightingEngine
	{
		// Token: 0x0600170D RID: 5901 RVA: 0x004D0F02 File Offset: 0x004CF102
		public void AddLight(int x, int y, Vector3 color)
		{
			this._perFrameLights.Add(new LightingEngine.PerFrameLight(new Point(x, y), color));
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x004D0F1C File Offset: 0x004CF11C
		public void Clear()
		{
			this._activeLightMap.Clear();
			this._workingLightMap.Clear();
			this._perFrameLights.Clear();
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x004D0F40 File Offset: 0x004CF140
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

		// Token: 0x06001710 RID: 5904 RVA: 0x004D0F90 File Offset: 0x004CF190
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

		// Token: 0x06001711 RID: 5905 RVA: 0x004D10E6 File Offset: 0x004CF2E6
		private void IncrementState()
		{
			this._state = (this._state + 1) % LightingEngine.EngineState.Max;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x004D10F8 File Offset: 0x004CF2F8
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

		// Token: 0x06001713 RID: 5907 RVA: 0x004D116D File Offset: 0x004CF36D
		private void ProcessBlur()
		{
			this.UpdateLightDecay();
			this.ApplyPerFrameLights();
			this._workingLightMap.Blur();
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x004D1186 File Offset: 0x004CF386
		private void Present()
		{
			Utils.Swap<LightMap>(ref this._activeLightMap, ref this._workingLightMap);
			Utils.Swap<Rectangle>(ref this._activeProcessedArea, ref this._workingProcessedArea);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x004D11AC File Offset: 0x004CF3AC
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
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x004D14CC File Offset: 0x004CF6CC
		private void ApplyPerFrameLights()
		{
			for (int i = 0; i < this._perFrameLights.Count; i++)
			{
				Point position = this._perFrameLights[i].Position;
				if (this._workingProcessedArea.Contains(position))
				{
					Vector3 color = this._perFrameLights[i].Color;
					Vector3 vector = this._workingLightMap[position.X - this._workingProcessedArea.X, position.Y - this._workingProcessedArea.Y];
					Vector3.Max(ref vector, ref color, out color);
					this._workingLightMap[position.X - this._workingProcessedArea.X, position.Y - this._workingProcessedArea.Y] = color;
				}
			}
			this._perFrameLights.Clear();
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x004D159D File Offset: 0x004CF79D
		public void Rebuild()
		{
			this._activeProcessedArea = Rectangle.Empty;
			this._workingProcessedArea = Rectangle.Empty;
			this._state = LightingEngine.EngineState.MinimapUpdate;
			this._activeLightMap = new LightMap();
			this._workingLightMap = new LightMap();
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x004D15D4 File Offset: 0x004CF7D4
		private void ExportToMiniMap()
		{
			if (!Main.mapEnabled)
			{
				return;
			}
			if (this._activeProcessedArea.Width <= 0 || this._activeProcessedArea.Height <= 0)
			{
				return;
			}
			Rectangle area = new Rectangle(this._activeProcessedArea.X + 28, this._activeProcessedArea.Y + 28, this._activeProcessedArea.Width - 56, this._activeProcessedArea.Height - 56);
			Rectangle value = new Rectangle(0, 0, Main.maxTilesX, Main.maxTilesY);
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

		// Token: 0x040013DA RID: 5082
		public const int AREA_PADDING = 28;

		// Token: 0x040013DB RID: 5083
		private const int NON_VISIBLE_PADDING = 18;

		// Token: 0x040013DC RID: 5084
		private readonly List<LightingEngine.PerFrameLight> _perFrameLights = new List<LightingEngine.PerFrameLight>();

		// Token: 0x040013DD RID: 5085
		private TileLightScanner _tileScanner = new TileLightScanner();

		// Token: 0x040013DE RID: 5086
		private LightMap _activeLightMap = new LightMap();

		// Token: 0x040013DF RID: 5087
		private Rectangle _activeProcessedArea;

		// Token: 0x040013E0 RID: 5088
		private LightMap _workingLightMap = new LightMap();

		// Token: 0x040013E1 RID: 5089
		private Rectangle _workingProcessedArea;

		// Token: 0x040013E2 RID: 5090
		private readonly Stopwatch _timer = new Stopwatch();

		// Token: 0x040013E3 RID: 5091
		private LightingEngine.EngineState _state;

		// Token: 0x0200059D RID: 1437
		private enum EngineState
		{
			// Token: 0x04005A2A RID: 23082
			MinimapUpdate,
			// Token: 0x04005A2B RID: 23083
			ExportMetrics,
			// Token: 0x04005A2C RID: 23084
			Scan,
			// Token: 0x04005A2D RID: 23085
			Blur,
			// Token: 0x04005A2E RID: 23086
			Max
		}

		// Token: 0x0200059E RID: 1438
		private struct PerFrameLight
		{
			// Token: 0x06003248 RID: 12872 RVA: 0x005EBB32 File Offset: 0x005E9D32
			public PerFrameLight(Point position, Vector3 color)
			{
				this.Position = position;
				this.Color = color;
			}

			// Token: 0x04005A2F RID: 23087
			public readonly Point Position;

			// Token: 0x04005A30 RID: 23088
			public readonly Vector3 Color;
		}
	}
}
