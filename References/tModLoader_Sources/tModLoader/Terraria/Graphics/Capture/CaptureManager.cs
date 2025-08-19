using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Capture
{
	// Token: 0x02000479 RID: 1145
	public class CaptureManager : IDisposable
	{
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06003785 RID: 14213 RVA: 0x00589967 File Offset: 0x00587B67
		public bool IsCapturing
		{
			get
			{
				return !Main.dedServ && this._camera.IsCapturing;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06003786 RID: 14214 RVA: 0x0058997D File Offset: 0x00587B7D
		// (set) Token: 0x06003787 RID: 14215 RVA: 0x0058998A File Offset: 0x00587B8A
		public bool Active
		{
			get
			{
				return this._interface.Active;
			}
			set
			{
				if (!Main.CaptureModeDisabled && this._interface.Active != value)
				{
					this._interface.ToggleCamera(value);
				}
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06003788 RID: 14216 RVA: 0x005899AD File Offset: 0x00587BAD
		public bool UsingMap
		{
			get
			{
				return this.Active && this._interface.UsingMap();
			}
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x005899C4 File Offset: 0x00587BC4
		public CaptureManager()
		{
			this._interface = new CaptureInterface();
			if (!Main.dedServ)
			{
				this._camera = new CaptureCamera(Main.instance.GraphicsDevice);
			}
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x005899F3 File Offset: 0x00587BF3
		public void Scrolling()
		{
			this._interface.Scrolling();
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x00589A00 File Offset: 0x00587C00
		public void Update()
		{
			this._interface.Update();
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x00589A0D File Offset: 0x00587C0D
		public void Draw(SpriteBatch sb)
		{
			this._interface.Draw(sb);
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x00589A1B File Offset: 0x00587C1B
		public float GetProgress()
		{
			return this._camera.GetProgress();
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x00589A28 File Offset: 0x00587C28
		public void Capture()
		{
			CaptureSettings settings = new CaptureSettings
			{
				Area = new Rectangle(2660, 100, 1000, 1000),
				UseScaling = false
			};
			this.Capture(settings);
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x00589A65 File Offset: 0x00587C65
		public void Capture(CaptureSettings settings)
		{
			this._camera.Capture(settings);
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x00589A73 File Offset: 0x00587C73
		public void DrawTick()
		{
			this._camera.DrawTick();
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x00589A80 File Offset: 0x00587C80
		public void Dispose()
		{
			this._camera.Dispose();
		}

		// Token: 0x0400513C RID: 20796
		public static CaptureManager Instance = Main.dedServ ? new CaptureManager() : null;

		// Token: 0x0400513D RID: 20797
		private CaptureInterface _interface;

		// Token: 0x0400513E RID: 20798
		private CaptureCamera _camera;
	}
}
