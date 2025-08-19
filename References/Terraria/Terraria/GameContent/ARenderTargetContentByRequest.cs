using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x020001D4 RID: 468
	public abstract class ARenderTargetContentByRequest : INeedRenderTargetContent
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x004F1429 File Offset: 0x004EF629
		public bool IsReady
		{
			get
			{
				return this._wasPrepared;
			}
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x004F1431 File Offset: 0x004EF631
		public void Request()
		{
			this._wasRequested = true;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x004F143A File Offset: 0x004EF63A
		public RenderTarget2D GetTarget()
		{
			return this._target;
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x004F1442 File Offset: 0x004EF642
		public void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			this._wasPrepared = false;
			if (!this._wasRequested)
			{
				return;
			}
			this._wasRequested = false;
			this.HandleUseReqest(device, spriteBatch);
		}

		// Token: 0x06001C11 RID: 7185
		protected abstract void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch);

		// Token: 0x06001C12 RID: 7186 RVA: 0x004F1464 File Offset: 0x004EF664
		protected void PrepareARenderTarget_AndListenToEvents(ref RenderTarget2D target, GraphicsDevice device, int neededWidth, int neededHeight, RenderTargetUsage usage)
		{
			if (target == null || target.IsDisposed || target.Width != neededWidth || target.Height != neededHeight)
			{
				if (target != null)
				{
					target.ContentLost -= this.target_ContentLost;
					target.Disposing -= this.target_Disposing;
				}
				target = new RenderTarget2D(device, neededWidth, neededHeight, false, device.PresentationParameters.BackBufferFormat, DepthFormat.None, 0, usage);
				target.ContentLost += this.target_ContentLost;
				target.Disposing += this.target_Disposing;
			}
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x004F14FE File Offset: 0x004EF6FE
		private void target_Disposing(object sender, EventArgs e)
		{
			this._wasPrepared = false;
			this._target = null;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x004F150E File Offset: 0x004EF70E
		private void target_ContentLost(object sender, EventArgs e)
		{
			this._wasPrepared = false;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x004F1517 File Offset: 0x004EF717
		public void Reset()
		{
			this._wasPrepared = false;
			this._wasRequested = false;
			this._target = null;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x004F1530 File Offset: 0x004EF730
		protected void PrepareARenderTarget_WithoutListeningToEvents(ref RenderTarget2D target, GraphicsDevice device, int neededWidth, int neededHeight, RenderTargetUsage usage)
		{
			if (target == null || target.IsDisposed || target.Width != neededWidth || target.Height != neededHeight)
			{
				target = new RenderTarget2D(device, neededWidth, neededHeight, false, device.PresentationParameters.BackBufferFormat, DepthFormat.None, 0, usage);
			}
		}

		// Token: 0x04004364 RID: 17252
		protected RenderTarget2D _target;

		// Token: 0x04004365 RID: 17253
		protected bool _wasPrepared;

		// Token: 0x04004366 RID: 17254
		private bool _wasRequested;
	}
}
