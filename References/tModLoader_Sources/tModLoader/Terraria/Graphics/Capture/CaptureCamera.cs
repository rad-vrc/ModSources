using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.Utilities;

namespace Terraria.Graphics.Capture
{
	// Token: 0x02000477 RID: 1143
	internal class CaptureCamera : IDisposable
	{
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06003767 RID: 14183 RVA: 0x00587AAB File Offset: 0x00585CAB
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

		// Token: 0x06003768 RID: 14184 RVA: 0x00587ACC File Offset: 0x00585CCC
		public CaptureCamera(GraphicsDevice graphics)
		{
			CaptureCamera.CameraExists = true;
			this._graphics = graphics;
			this._spriteBatch = new SpriteBatch(graphics);
			try
			{
				this._frameBuffer = new RenderTarget2D(graphics, 2048, 2048, false, graphics.PresentationParameters.BackBufferFormat, 0);
				this._filterFrameBuffer1 = new RenderTarget2D(graphics, 2048, 2048, false, graphics.PresentationParameters.BackBufferFormat, 0);
				this._filterFrameBuffer2 = new RenderTarget2D(graphics, 2048, 2048, false, graphics.PresentationParameters.BackBufferFormat, 0);
			}
			catch
			{
				Main.CaptureModeDisabled = true;
				return;
			}
			this._downscaleSampleState = SamplerState.AnisotropicClamp;
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x00587BA0 File Offset: 0x00585DA0
		public void Capture(CaptureSettings settings)
		{
			Main.GlobalTimerPaused = true;
			Monitor.Enter(this._captureLock);
			if (this._activeSettings != null)
			{
				throw new InvalidOperationException("Capture called while another capture was already active.");
			}
			this._activeSettings = settings;
			Rectangle area = settings.Area;
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
				this._scaledFrameBuffer = new RenderTarget2D(this._graphics, num2, num2, false, this._graphics.PresentationParameters.BackBufferFormat, 0);
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
					this._renderQueue.Enqueue(new CaptureCamera.CaptureChunk(new Rectangle(i, j, num3, num4), new Rectangle(x, y, width, height)));
				}
			}
			Monitor.Exit(this._captureLock);
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x00587E2C File Offset: 0x0058602C
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
				this._graphics.Clear(Color.Transparent);
				Main.instance.TilesRenderer.PrepareForAreaDrawing(captureChunk.Area.Left, captureChunk.Area.Right, captureChunk.Area.Top, captureChunk.Area.Bottom, false);
				Main.instance.TilePaintSystem.PrepareAllRequests();
				this._graphics.SetRenderTarget(this._frameBuffer);
				this._graphics.Clear(Color.Transparent);
				if (notRetro)
				{
					Color clearColor = this._activeSettings.CaptureBackground ? Color.Black : Color.Transparent;
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
					this._graphics.Clear(Color.Transparent);
					this._spriteBatch.Begin(0, BlendState.AlphaBlend, this._downscaleSampleState, DepthStencilState.Default, RasterizerState.CullNone);
					this._spriteBatch.Draw(this._frameBuffer, new Rectangle(0, 0, this._scaledFrameBuffer.Width, this._scaledFrameBuffer.Height), Color.White);
					this._spriteBatch.End();
					this._graphics.SetRenderTarget(null);
					this._scaledFrameBuffer.GetData<byte>(this._scaledFrameData, 0, this._scaledFrameBuffer.Width * this._scaledFrameBuffer.Height * 4);
					this.DrawBytesToBuffer(this._scaledFrameData, this._outputData, this._scaledFrameBuffer.Width, this._outputImageSize.Width, captureChunk.ScaledArea);
				}
				else
				{
					this._graphics.SetRenderTarget(null);
					this.SaveImage(this._frameBuffer, captureChunk.ScaledArea.Width, captureChunk.ScaledArea.Height, CaptureCamera.ImageFormat.Png, this._activeSettings.OutputName, captureChunk.Area.X.ToString() + "-" + captureChunk.Area.Y.ToString() + ".png");
				}
				this._tilesProcessed += (float)(captureChunk.Area.Width * captureChunk.Area.Height);
			}
			if (this._renderQueue.Count == 0)
			{
				this.FinishCapture();
			}
			Monitor.Exit(this._captureLock);
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x00588120 File Offset: 0x00586320
		private unsafe void DrawBytesToBuffer(byte[] sourceBuffer, byte[] destinationBuffer, int sourceBufferWidth, int destinationBufferWidth, Rectangle area)
		{
			fixed (byte* ptr4 = &destinationBuffer[0])
			{
				byte* ptr5 = ptr4;
				fixed (byte* ptr6 = &sourceBuffer[0])
				{
					byte* ptr2 = ptr6;
					byte* ptr3 = ptr5 + (destinationBufferWidth * area.Y + area.X << 2);
					for (int i = 0; i < area.Height; i++)
					{
						for (int j = 0; j < area.Width; j++)
						{
							if (Program.IsXna)
							{
								ptr3[2] = *ptr2;
								ptr3[1] = ptr2[1];
								*ptr3 = ptr2[2];
								ptr3[3] = ptr2[3];
							}
							else
							{
								*ptr3 = *ptr2;
								ptr3[1] = ptr2[1];
								ptr3[2] = ptr2[2];
								ptr3[3] = ptr2[3];
							}
							ptr2 += 4;
							ptr3 += 4;
						}
						ptr2 += sourceBufferWidth - area.Width << 2;
						ptr3 += destinationBufferWidth - area.Width << 2;
					}
				}
			}
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x005881F6 File Offset: 0x005863F6
		public float GetProgress()
		{
			return this._tilesProcessed / this._totalTiles;
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x00588208 File Offset: 0x00586408
		private bool SaveImage(int width, int height, CaptureCamera.ImageFormat imageFormat, string filename)
		{
			if (!Utils.TryCreatingDirectory(Main.SavePath + Path.DirectorySeparatorChar.ToString() + "Captures" + Path.DirectorySeparatorChar.ToString()))
			{
				return false;
			}
			bool result;
			try
			{
				using (FileStream stream = File.Create(filename))
				{
					PlatformUtilities.SavePng(stream, width, height, width, height, this._outputData);
					result = true;
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				result = false;
			}
			return result;
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x00588290 File Offset: 0x00586490
		private void SaveImage(Texture2D texture, int width, int height, CaptureCamera.ImageFormat imageFormat, string foldername, string filename)
		{
			string text3 = string.Concat(new string[]
			{
				Main.SavePath,
				Path.DirectorySeparatorChar.ToString(),
				"Captures",
				Path.DirectorySeparatorChar.ToString(),
				foldername
			});
			string text2 = Path.Combine(text3, filename);
			if (!Utils.TryCreatingDirectory(text3))
			{
				return;
			}
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
			using (FileStream stream = File.Create(text2))
			{
				PlatformUtilities.SavePng(stream, width, height, width, height, this._outputData);
			}
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x005883C4 File Offset: 0x005865C4
		private void FinishCapture()
		{
			if (this._activeSettings.UseScaling)
			{
				int num = 0;
				while (!this.SaveImage(this._outputImageSize.Width, this._outputImageSize.Height, CaptureCamera.ImageFormat.Png, string.Concat(new string[]
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

		// Token: 0x06003770 RID: 14192 RVA: 0x005884C0 File Offset: 0x005866C0
		public void Dispose()
		{
			if (Main.dedServ)
			{
				return;
			}
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

		// Token: 0x04005116 RID: 20758
		private static bool CameraExists;

		// Token: 0x04005117 RID: 20759
		public const int CHUNK_SIZE = 128;

		// Token: 0x04005118 RID: 20760
		public const int FRAMEBUFFER_PIXEL_SIZE = 2048;

		// Token: 0x04005119 RID: 20761
		public const int INNER_CHUNK_SIZE = 126;

		// Token: 0x0400511A RID: 20762
		public const int MAX_IMAGE_SIZE = 4096;

		// Token: 0x0400511B RID: 20763
		public const string CAPTURE_DIRECTORY = "Captures";

		// Token: 0x0400511C RID: 20764
		private RenderTarget2D _frameBuffer;

		// Token: 0x0400511D RID: 20765
		private RenderTarget2D _scaledFrameBuffer;

		// Token: 0x0400511E RID: 20766
		private RenderTarget2D _filterFrameBuffer1;

		// Token: 0x0400511F RID: 20767
		private RenderTarget2D _filterFrameBuffer2;

		// Token: 0x04005120 RID: 20768
		private GraphicsDevice _graphics;

		// Token: 0x04005121 RID: 20769
		private readonly object _captureLock = new object();

		// Token: 0x04005122 RID: 20770
		private bool _isDisposed;

		// Token: 0x04005123 RID: 20771
		private CaptureSettings _activeSettings;

		// Token: 0x04005124 RID: 20772
		private Queue<CaptureCamera.CaptureChunk> _renderQueue = new Queue<CaptureCamera.CaptureChunk>();

		// Token: 0x04005125 RID: 20773
		private SpriteBatch _spriteBatch;

		// Token: 0x04005126 RID: 20774
		private byte[] _scaledFrameData;

		// Token: 0x04005127 RID: 20775
		private byte[] _outputData;

		// Token: 0x04005128 RID: 20776
		private Size _outputImageSize;

		// Token: 0x04005129 RID: 20777
		private SamplerState _downscaleSampleState;

		// Token: 0x0400512A RID: 20778
		private float _tilesProcessed;

		// Token: 0x0400512B RID: 20779
		private float _totalTiles;

		// Token: 0x02000B86 RID: 2950
		private enum ImageFormat
		{
			// Token: 0x04007646 RID: 30278
			Png
		}

		// Token: 0x02000B87 RID: 2951
		private class CaptureChunk
		{
			// Token: 0x06005D01 RID: 23809 RVA: 0x006C58FD File Offset: 0x006C3AFD
			public CaptureChunk(Rectangle area, Rectangle scaledArea)
			{
				this.Area = area;
				this.ScaledArea = scaledArea;
			}

			// Token: 0x04007647 RID: 30279
			public readonly Rectangle Area;

			// Token: 0x04007648 RID: 30280
			public readonly Rectangle ScaledArea;
		}
	}
}
