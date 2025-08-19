using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003A4 RID: 932
	internal interface IBigProgressBar
	{
		// Token: 0x060029C8 RID: 10696
		bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info);

		// Token: 0x060029C9 RID: 10697
		void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch);
	}
}
