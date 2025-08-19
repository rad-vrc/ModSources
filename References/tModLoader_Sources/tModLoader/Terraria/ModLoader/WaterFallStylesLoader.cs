using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.ModLoader
{
	// Token: 0x02000234 RID: 564
	[Autoload(true, Side = ModSide.Client)]
	public class WaterFallStylesLoader : SceneEffectLoader<ModWaterfallStyle>
	{
		// Token: 0x060028AB RID: 10411 RVA: 0x0050C928 File Offset: 0x0050AB28
		public WaterFallStylesLoader()
		{
			base.Initialize(26);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x0050C938 File Offset: 0x0050AB38
		internal override void ResizeArrays()
		{
			Array.Resize<Asset<Texture2D>>(ref Main.instance.waterfallManager.waterfallTexture, base.TotalCount);
		}
	}
}
