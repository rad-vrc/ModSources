using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003AC RID: 940
	public class NeverValidProgressBar : IBigProgressBar
	{
		// Token: 0x060029E5 RID: 10725 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			return false;
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
		}
	}
}
