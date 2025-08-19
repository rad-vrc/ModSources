using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x0200054F RID: 1359
	public interface IBigProgressBar
	{
		// Token: 0x0600403C RID: 16444
		bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info);

		// Token: 0x0600403D RID: 16445
		void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch);
	}
}
