﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.OS;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.Utilities;

namespace Terraria.Graphics.Capture
{
	// Token: 0x020000FB RID: 251
	internal class CaptureCamera : IDisposable
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x004C6DFE File Offset: 0x004C4FFE
		public bool IsCapturing
		{
			get
			{
				Monitor.Enter(this._captureLock);
				bool result = this._activeSettings != null;
				Monitor.Exit(this._captureLock);
				return result;
			}
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x004C6E20 File Offset: 0x004C5020
		public CaptureCamera(GraphicsDevice graphics)
		{
			CaptureCamera.CameraExists = true;
			this._graphics = graphics;
			this._spriteBatch = new SpriteBatch(graphics);
			try
			{
				this._frameBuffer = new RenderTarget2D(graphics, 2048, 2048, false, graphics.PresentationParameters.BackBufferFormat, DepthFormat.None);
				this._filterFrameBuffer1 = new RenderTarget2D(graphics, 2048, 2048, false, graphics.PresentationParameters.BackBufferFormat, DepthFormat.None);
				this._filterFrameBuffer2 = new RenderTarget2D(graphics, 2048, 2048, false, graphics.PresentationParameters.BackBufferFormat, DepthFormat.None);
			}
			catch
			{
				Main.CaptureModeDisabled = true;
				return;
			}
			this._downscaleSampleState = SamplerState.AnisotropicClamp;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x004C6EF4 File Offset: 0x004C50F4
		public void Capture(CaptureSettings settings)
		{
			Main.GlobalTimerPaused = true;
			Monitor.Enter(this._captureLock);
			if (this._activeSettings != null)
			{
				throw new InvalidOperationException("Capture called while another capture was already active.");
			}
			this._activeSettings = settings;
			Microsoft.Xna.Framework.Rectangle area = settings.Area;
			float num = 1f;
			if (settings.UseScaling)
			{
				if (area.Width * 16 > 4096)
				{
					num = 4096f / (float)(area.Width * 16);
				}
				if (area.Height * 16 > 4096)
				{
					num = Math.Min(num, 4096f / (float)(area.Height * 16));
				}
				num = Math.Min(1f, num);
				this._outputImageSize = new Size((int)MathHelper.Clamp((float)((int)(num * (float)(area.Width * 16))), 1f, 4096f), (int)MathHelper.Clamp((float)((int)(num * (float)(area.Height * 16))), 1f, 4096f));
				this._outputData = new byte[4 * this._outputImageSize.Width * this._outputImageSize.Height];
				int num2 = (int)Math.Floor((double)(num * 2048f));
				this._scaledFrameData = new byte[4 * num2 * num2];
				this._scaledFrameBuffer = new RenderTarget2D(this._graphics, num2, num2, false, this._graphics.PresentationParameters.BackBufferFormat, DepthFormat.None);
			}
			else
			{
				this._outputData = new byte[16777216];
			}
			this._tilesProcessed = 0f;
			this._totalTiles = (float)(area.Width * area.Height);
			for (int i = area.X; i < area.X + area.Width; i += 126)
			{
				for (int j = area.Y; j < area.Y + area.Height; j += 126)
				{
					int num3 = Math.Min(128, area.X + area.Width - i);
					int num4 = Math.Min(128, area.Y + area.Height - j);
					int width = (int)Math.Floor((double)(num * (float)(num3 * 16)));
					int height = (int)Math.Floor((double)(num * (float)(num4 * 16)));
					int x = (int)Math.Floor((double)(num * (float)((i - area.X) * 16)));
					int y = (int)Math.Floor((double)(num * (float)((j - area.Y) * 16)));
					this._renderQueue.Enqueue(new CaptureCamera.CaptureChunk(new Microsoft.Xna.Framework.Rectangle(i, j, num3, num4), new Microsoft.Xna.Framework.Rectangle(x, y, width, height)));
				}
			}
			Monitor.Exit(this._captureLock);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x004C7180 File Offset: 0x004C5380
		public void DrawTick()
		{
			Monitor.Enter(this._captureLock);
			if (this._activeSettings == null)
			{
				return;
			}
			bool notRetro = Lighting.NotRetro;
			if (this._renderQueue.Count > 0)
			{
				CaptureCamera.CaptureChunk captureChunk = this._renderQueue.Dequeue();
				this._graphics.SetRenderTarget(null);
				this._graphics.Clear(Microsoft.Xna.Framework.Color.Transparent);
				Main.instance.TilesRenderer.PrepareForAreaDrawing(captureChunk.Area.Left, captureChunk.Area.Right, captureChunk.Area.Top, captureChunk.Area.Bottom, false);
				Main.instance.TilePaintSystem.PrepareAllRequests();
				this._graphics.SetRenderTarget(this._frameBuffer);
				this._graphics.Clear(Microsoft.Xna.Framework.Color.Transparent);
				if (notRetro)
				{
					Microsoft.Xna.Framework.Color clearColor = this._activeSettings.CaptureBackground ? Microsoft.Xna.Framework.Color.Black : Microsoft.Xna.Framework.Color.Transparent;
					Filters.Scene.BeginCapture(this._filterFrameBuffer1, clearColor);
					Main.instance.DrawCapture(captureChunk.Area, this._activeSettings);
					Filters.Scene.EndCapture(this._frameBuffer, this._filterFrameBuffer1, this._filterFrameBuffer2, clearColor);
				}
				else
				{
					Main.instance.DrawCapture(captureChunk.Area, this._activeSettings);
				}
				if (this._activeSettings.UseScaling)
				{
					this._graphics.SetRenderTarget(this._scaledFrameBuffer);
					this._graphics.Clear(Microsoft.Xna.Framework.Color.Transparent);
					this._spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, this._downscaleSampleState, DepthStencilState.Default, RasterizerState.CullNone);
					this._spriteBatch.Draw(this._frameBuffer, new Microsoft.Xna.Framework.Rectangle(0, 0, this._scaledFrameBuffer.Width, this._scaledFrameBuffer.Height), Microsoft.Xna.Framework.Color.White);
					this._spriteBatch.End();
					this._graphics.SetRenderTarget(null);
					this._scaledFrameBuffer.GetData<byte>(this._scaledFrameData, 0, this._scaledFrameBuffer.Width * this._scaledFrameBuffer.Height * 4);
					this.DrawBytesToBuffer(this._scaledFrameData, this._outputData, this._scaledFrameBuffer.Width, this._outputImageSize.Width, captureChunk.ScaledArea);
				}
				else
				{
					this._graphics.SetRenderTarget(null);
					this.SaveImage(this._frameBuffer, captureChunk.ScaledArea.Width, captureChunk.ScaledArea.Height, ImageFormat.Png, this._activeSettings.OutputName, string.Concat(new object[]
					{
						captureChunk.Area.X,
						"-",
						captureChunk.Area.Y,
						".png"
					}));
				}
				this._tilesProcessed += (float)(captureChunk.Area.Width * captureChunk.Area.Height);
			}
			if (this._renderQueue.Count == 0)
			{
				this.FinishCapture();
			}
			Monitor.Exit(this._captureLock);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x004C748C File Offset: 0x004C568C
		private unsafe void DrawBytesToBuffer(byte[] sourceBuffer, byte[] destinationBuffer, int sourceBufferWidth, int destinationBufferWidth, Microsoft.Xna.Framework.Rectangle area)
		{
			fixed (byte* ptr = &destinationBuffer[0])
			{
				byte* ptr2 = ptr;
				fixed (byte* ptr3 = &sourceBuffer[0])
				{
					byte* ptr4 = ptr3;
					ptr2 += destinationBufferWidth * area.Y + area.X << 2;
					for (int i = 0; i < area.Height; i++)
					{
						for (int j = 0; j < area.Width; j++)
						{
							if (Program.IsXna)
							{
								ptr2[2] = *ptr4;
								ptr2[1] = ptr4[1];
								*ptr2 = ptr4[2];
								ptr2[3] = ptr4[3];
							}
							else
							{
								*ptr2 = *ptr4;
								ptr2[1] = ptr4[1];
								ptr2[2] = ptr4[2];
								ptr2[3] = ptr4[3];
							}
							ptr4 += 4;
							ptr2 += 4;
						}
						ptr4 += sourceBufferWidth - area.Width << 2;
						ptr2 += destinationBufferWidth - area.Width << 2;
					}
				}
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x004C7564 File Offset: 0x004C5764
		public float GetProgress()
		{
			return this._tilesProcessed / this._totalTiles;
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x004C7574 File Offset: 0x004C5774
		private bool SaveImage(int width, int height, ImageFormat imageFormat, string filename)
		{
			if (!Utils.TryCreatingDirectory(Main.SavePath + Path.DirectorySeparatorChar.ToString() + "Captures" + Path.DirectorySeparatorChar.ToString()))
			{
				return false;
			}
			bool result;
			try
			{
				if (!Platform.IsWindows)
				{
					using (FileStream fileStream = File.Create(filename))
					{
						PlatformUtilities.SavePng(fileStream, width, height, width, height, this._outputData);
						goto IL_BF;
					}
				}
				using (Bitmap bitmap = new Bitmap(width, height))
				{
					System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, width, height);
					BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
					IntPtr scan = bitmapData.Scan0;
					Marshal.Copy(this._outputData, 0, scan, width * height * 4);
					bitmap.UnlockBits(bitmapData);
					bitmap.Save(filename, imageFormat);
					bitmap.Dispose();
				}
				IL_BF:
				result = true;
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				result = false;
			}
			return result;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x004C767C File Offset: 0x004C587C
		private void SaveImage(Texture2D texture, int width, int height, ImageFormat imageFormat, string foldername, string filename)
		{
			string text = string.Concat(new string[]
			{
				Main.SavePath,
				Path.DirectorySeparatorChar.ToString(),
				"Captures",
				Path.DirectorySeparatorChar.ToString(),
				foldername
			});
			string text2 = Path.Combine(text, filename);
			if (!Utils.TryCreatingDirectory(text))
			{
				return;
			}
			if (!Platform.IsWindows)
			{
				int elementCount = texture.Width * texture.Height * 4;
				texture.GetData<byte>(this._outputData, 0, elementCount);
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < height; i++)
				{
					for (int j = 0; j < width; j++)
					{
						this._outputData[num2] = this._outputData[num];
						this._outputData[num2 + 1] = this._outputData[num + 1];
						this._outputData[num2 + 2] = this._outputData[num + 2];
						this._outputData[num2 + 3] = this._outputData[num + 3];
						num += 4;
						num2 += 4;
					}
					num += texture.Width - width << 2;
				}
				using (FileStream fileStream = File.Create(text2))
				{
					PlatformUtilities.SavePng(fileStream, width, height, width, height, this._outputData);
					return;
				}
			}
			using (Bitmap bitmap = new Bitmap(width, height))
			{
				System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, width, height);
				int elementCount2 = texture.Width * texture.Height * 4;
				texture.GetData<byte>(this._outputData, 0, elementCount2);
				int num3 = 0;
				int num4 = 0;
				for (int k = 0; k < height; k++)
				{
					for (int l = 0; l < width; l++)
					{
						byte b = this._outputData[num3 + 2];
						this._outputData[num4 + 2] = this._outputData[num3];
						this._outputData[num4] = b;
						this._outputData[num4 + 1] = this._outputData[num3 + 1];
						this._outputData[num4 + 3] = this._outputData[num3 + 3];
						num3 += 4;
						num4 += 4;
					}
					num3 += texture.Width - width << 2;
				}
				BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
				IntPtr scan = bitmapData.Scan0;
				Marshal.Copy(this._outputData, 0, scan, width * height * 4);
				bitmap.UnlockBits(bitmapData);
				bitmap.Save(text2, imageFormat);
			}
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x004C7910 File Offset: 0x004C5B10
		private void FinishCapture()
		{
			if (this._activeSettings.UseScaling)
			{
				int num = 0;
				while (!this.SaveImage(this._outputImageSize.Width, this._outputImageSize.Height, ImageFormat.Png, string.Concat(new string[]
				{
					Main.SavePath,
					Path.DirectorySeparatorChar.ToString(),
					"Captures",
					Path.DirectorySeparatorChar.ToString(),
					this._activeSettings.OutputName,
					".png"
				})))
				{
					GC.Collect();
					Thread.Sleep(5);
					num++;
					Console.WriteLine(Language.GetTextValue("Error.CaptureError"));
					if (num > 5)
					{
						Console.WriteLine(Language.GetTextValue("Error.UnableToCapture"));
						break;
					}
				}
			}
			this._outputData = null;
			this._scaledFrameData = null;
			Main.GlobalTimerPaused = false;
			CaptureInterface.EndCamera();
			if (this._scaledFrameBuffer != null)
			{
				this._scaledFrameBuffer.Dispose();
				this._scaledFrameBuffer = null;
			}
			this._activeSettings = null;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x004C7A18 File Offset: 0x004C5C18
		public void Dispose()
		{
			if (!Main.dedServ)
			{
				Monitor.Enter(this._captureLock);
				if (this._isDisposed)
				{
					Monitor.Exit(this._captureLock);
					return;
				}
				this._frameBuffer.Dispose();
				this._filterFrameBuffer1.Dispose();
				this._filterFrameBuffer2.Dispose();
				if (this._scaledFrameBuffer != null)
				{
					this._scaledFrameBuffer.Dispose();
					this._scaledFrameBuffer = null;
				}
				CaptureCamera.CameraExists = false;
				this._isDisposed = true;
				Monitor.Exit(this._captureLock);
			}
		}

		// Token: 0x0400131E RID: 4894
		private static bool CameraExists;

		// Token: 0x0400131F RID: 4895
		public const int CHUNK_SIZE = 128;

		// Token: 0x04001320 RID: 4896
		public const int FRAMEBUFFER_PIXEL_SIZE = 2048;

		// Token: 0x04001321 RID: 4897
		public const int INNER_CHUNK_SIZE = 126;

		// Token: 0x04001322 RID: 4898
		public const int MAX_IMAGE_SIZE = 4096;

		// Token: 0x04001323 RID: 4899
		public const string CAPTURE_DIRECTORY = "Captures";

		// Token: 0x04001324 RID: 4900
		private RenderTarget2D _frameBuffer;

		// Token: 0x04001325 RID: 4901
		private RenderTarget2D _scaledFrameBuffer;

		// Token: 0x04001326 RID: 4902
		private RenderTarget2D _filterFrameBuffer1;

		// Token: 0x04001327 RID: 4903
		private RenderTarget2D _filterFrameBuffer2;

		// Token: 0x04001328 RID: 4904
		private GraphicsDevice _graphics;

		// Token: 0x04001329 RID: 4905
		private readonly object _captureLock = new object();

		// Token: 0x0400132A RID: 4906
		private bool _isDisposed;

		// Token: 0x0400132B RID: 4907
		private CaptureSettings _activeSettings;

		// Token: 0x0400132C RID: 4908
		private Queue<CaptureCamera.CaptureChunk> _renderQueue = new Queue<CaptureCamera.CaptureChunk>();

		// Token: 0x0400132D RID: 4909
		private SpriteBatch _spriteBatch;

		// Token: 0x0400132E RID: 4910
		private byte[] _scaledFrameData;

		// Token: 0x0400132F RID: 4911
		private byte[] _outputData;

		// Token: 0x04001330 RID: 4912
		private Size _outputImageSize;

		// Token: 0x04001331 RID: 4913
		private SamplerState _downscaleSampleState;

		// Token: 0x04001332 RID: 4914
		private float _tilesProcessed;

		// Token: 0x04001333 RID: 4915
		private float _totalTiles;

		// Token: 0x02000597 RID: 1431
		private class CaptureChunk
		{
			// Token: 0x0600323E RID: 12862 RVA: 0x005EB928 File Offset: 0x005E9B28
			public CaptureChunk(Microsoft.Xna.Framework.Rectangle area, Microsoft.Xna.Framework.Rectangle scaledArea)
			{
				this.Area = area;
				this.ScaledArea = scaledArea;
			}

			// Token: 0x04005A0F RID: 23055
			public readonly Microsoft.Xna.Framework.Rectangle Area;

			// Token: 0x04005A10 RID: 23056
			public readonly Microsoft.Xna.Framework.Rectangle ScaledArea;
		}
	}
}
