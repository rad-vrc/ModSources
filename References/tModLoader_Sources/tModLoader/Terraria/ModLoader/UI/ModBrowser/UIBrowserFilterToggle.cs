using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.ModLoader.UI.ModBrowser
{
	// Token: 0x0200026D RID: 621
	public class UIBrowserFilterToggle<T> : UICycleImage where T : struct, Enum
	{
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x0051EFE1 File Offset: 0x0051D1E1
		private static Asset<Texture2D> Texture
		{
			get
			{
				return UICommon.ModBrowserIconsTexture;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06002ADF RID: 10975 RVA: 0x0051EFE8 File Offset: 0x0051D1E8
		// (set) Token: 0x06002AE0 RID: 10976 RVA: 0x0051EFF0 File Offset: 0x0051D1F0
		public T State { get; private set; }

		// Token: 0x06002AE1 RID: 10977 RVA: 0x0051EFFC File Offset: 0x0051D1FC
		public UIBrowserFilterToggle(int textureOffsetX, int textureOffsetY, int padding = 2) : base(UIBrowserFilterToggle<T>.Texture, Enum.GetValues(typeof(T)).Length, 32, 32, textureOffsetX, textureOffsetY, padding)
		{
			base.OnLeftClick += this.UpdateToNext;
			base.OnRightClick += this.UpdateToPrevious;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x0051F053 File Offset: 0x0051D253
		public void SetCurrentState(T @enum)
		{
			this.State = @enum;
			base.SetCurrentState((int)((object)this.State));
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0051F072 File Offset: 0x0051D272
		private void UpdateToNext(UIMouseEvent @event, UIElement element)
		{
			this.SetCurrentState(this.State.NextEnum<T>());
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x0051F085 File Offset: 0x0051D285
		private void UpdateToPrevious(UIMouseEvent @event, UIElement element)
		{
			this.SetCurrentState(this.State.PreviousEnum<T>());
		}
	}
}
