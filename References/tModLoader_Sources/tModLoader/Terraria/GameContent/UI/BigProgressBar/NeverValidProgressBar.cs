using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000554 RID: 1364
	public class NeverValidProgressBar : IBigProgressBar
	{
		// Token: 0x06004051 RID: 16465 RVA: 0x005DFCE7 File Offset: 0x005DDEE7
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			return false;
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x005DFCEA File Offset: 0x005DDEEA
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
		}
	}
}
