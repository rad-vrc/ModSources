using System;
using ReLogic.Content;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x02000493 RID: 1171
	public class ContentRejectionFromSize : IRejectionReason
	{
		// Token: 0x06003905 RID: 14597 RVA: 0x0059613F File Offset: 0x0059433F
		public ContentRejectionFromSize(int neededWidth, int neededHeight, int actualWidth, int actualHeight)
		{
			this._neededWidth = neededWidth;
			this._neededHeight = neededHeight;
			this._actualWidth = actualWidth;
			this._actualHeight = actualHeight;
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x00596164 File Offset: 0x00594364
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

		// Token: 0x04005232 RID: 21042
		private int _neededWidth;

		// Token: 0x04005233 RID: 21043
		private int _neededHeight;

		// Token: 0x04005234 RID: 21044
		private int _actualWidth;

		// Token: 0x04005235 RID: 21045
		private int _actualHeight;
	}
}
