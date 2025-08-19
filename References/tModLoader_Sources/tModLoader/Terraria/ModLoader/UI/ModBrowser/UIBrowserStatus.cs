using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.ModLoader.UI.ModBrowser
{
	// Token: 0x0200026E RID: 622
	public class UIBrowserStatus : UIAnimatedImage
	{
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x0051F098 File Offset: 0x0051D298
		private static Asset<Texture2D> Texture
		{
			get
			{
				return UICommon.ModBrowserIconsTexture;
			}
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x0051F0A0 File Offset: 0x0051D2A0
		public UIBrowserStatus() : base(UIBrowserStatus.Texture, 32, 32, 204, 0, 1, 6, 2)
		{
			this.SetCurrentState(AsyncProviderState.Completed);
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x0051F0CC File Offset: 0x0051D2CC
		public void SetCurrentState(AsyncProviderState state)
		{
			switch (state)
			{
			case AsyncProviderState.Loading:
				base.FrameStart = 0;
				base.FrameCount = 4;
				return;
			case AsyncProviderState.Completed:
				base.FrameStart = 5;
				base.FrameCount = 1;
				return;
			case AsyncProviderState.Canceled:
			case AsyncProviderState.Aborted:
				base.FrameStart = 4;
				base.FrameCount = 1;
				return;
			default:
				return;
			}
		}
	}
}
