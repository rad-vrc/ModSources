using System;
using ReLogic.Content;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x020001F4 RID: 500
	public class ContentRejectionFromSize : IRejectionReason
	{
		// Token: 0x06001CF2 RID: 7410 RVA: 0x004FE8A5 File Offset: 0x004FCAA5
		public ContentRejectionFromSize(int neededWidth, int neededHeight, int actualWidth, int actualHeight)
		{
			this._neededWidth = neededWidth;
			this._neededHeight = neededHeight;
			this._actualWidth = actualWidth;
			this._actualHeight = actualHeight;
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x004FE8CA File Offset: 0x004FCACA
		public string GetReason()
		{
			return Language.GetTextValueWith("AssetRejections.BadSize", new
			{
				NeededWidth = this._neededWidth,
				NeededHeight = this._neededHeight,
				ActualWidth = this._actualWidth,
				ActualHeight = this._actualHeight
			});
		}

		// Token: 0x040043E2 RID: 17378
		private int _neededWidth;

		// Token: 0x040043E3 RID: 17379
		private int _neededHeight;

		// Token: 0x040043E4 RID: 17380
		private int _actualWidth;

		// Token: 0x040043E5 RID: 17381
		private int _actualHeight;
	}
}
