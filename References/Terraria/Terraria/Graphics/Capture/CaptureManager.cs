using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Capture
{
	// Token: 0x020000FC RID: 252
	public class CaptureManager : IDisposable
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x004C7A9E File Offset: 0x004C5C9E
		public bool IsCapturing
		{
			get
			{
				return !Main.dedServ && this._camera.IsCapturing;
			}
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x004C7AB4 File Offset: 0x004C5CB4
		public CaptureManager()
		{
			this._interface = new CaptureInterface();
			if (!Main.dedServ)
			{
				this._camera = new CaptureCamera(Main.instance.GraphicsDevice);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x004C7AE3 File Offset: 0x004C5CE3
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x004C7AF0 File Offset: 0x004C5CF0
		public bool Active
		{
			get
			{
				return this._interface.Active;
			}
			set
			{
				if (Main.CaptureModeDisabled)
				{
					return;
				}
				if (this._interface.Active != value)
				{
					this._interface.ToggleCamera(value);
				}
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x004C7B14 File Offset: 0x004C5D14
		public bool UsingMap
		{
			get
			{
				return this.Active && this._interface.UsingMap();
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x004C7B2B File Offset: 0x004C5D2B
		public void Scrolling()
		{
			this._interface.Scrolling();
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x004C7B38 File Offset: 0x004C5D38
		public void Update()
		{
			this._interface.Update();
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x004C7B45 File Offset: 0x004C5D45
		public void Draw(SpriteBatch sb)
		{
			this._interface.Draw(sb);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x004C7B53 File Offset: 0x004C5D53
		public float GetProgress()
		{
			return this._camera.GetProgress();
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x004C7B60 File Offset: 0x004C5D60
		public void Capture()
		{
			CaptureSettings settings = new CaptureSettings
			{
				Area = new Rectangle(2660, 100, 1000, 1000),
				UseScaling = false
			};
			this.Capture(settings);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x004C7B9D File Offset: 0x004C5D9D
		public void Capture(CaptureSettings settings)
		{
			this._camera.Capture(settings);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x004C7BAB File Offset: 0x004C5DAB
		public void DrawTick()
		{
			this._camera.DrawTick();
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x004C7BB8 File Offset: 0x004C5DB8
		public void Dispose()
		{
			this._camera.Dispose();
		}

		// Token: 0x04001334 RID: 4916
		public static CaptureManager Instance = new CaptureManager();

		// Token: 0x04001335 RID: 4917
		private CaptureInterface _interface;

		// Token: 0x04001336 RID: 4918
		private CaptureCamera _camera;
	}
}
