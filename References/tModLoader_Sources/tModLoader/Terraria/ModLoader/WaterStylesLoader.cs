using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.GameContent.Liquid;

namespace Terraria.ModLoader
{
	// Token: 0x02000235 RID: 565
	[Autoload(true, Side = ModSide.Client)]
	public class WaterStylesLoader : SceneEffectLoader<ModWaterStyle>
	{
		// Token: 0x060028AD RID: 10413 RVA: 0x0050C954 File Offset: 0x0050AB54
		public WaterStylesLoader()
		{
			base.Initialize(15);
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x0050C964 File Offset: 0x0050AB64
		internal override void ResizeArrays()
		{
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.Liquid, base.TotalCount);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.LiquidSlope, base.TotalCount);
			Array.Resize<Asset<Texture2D>>(ref LiquidRenderer.Instance._liquidTextures, base.TotalCount);
			Array.Resize<float>(ref Main.liquidAlpha, base.TotalCount);
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x0050C9B8 File Offset: 0x0050ABB8
		public override void ChooseStyle(out int style, out SceneEffectPriority priority)
		{
			int tst = Main.LocalPlayer.CurrentSceneEffect.waterStyle.value;
			style = -1;
			priority = SceneEffectPriority.None;
			if (tst >= base.VanillaCount)
			{
				style = tst;
				priority = Main.LocalPlayer.CurrentSceneEffect.waterStyle.priority;
			}
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x0050CA04 File Offset: 0x0050AC04
		public void UpdateLiquidAlphas()
		{
			if (Main.waterStyle >= base.VanillaCount)
			{
				for (int i = 0; i < base.VanillaCount; i++)
				{
					if (i != 1 && i != 11)
					{
						Main.liquidAlpha[i] -= 0.2f;
						if (Main.liquidAlpha[i] < 0f)
						{
							Main.liquidAlpha[i] = 0f;
						}
					}
				}
			}
			foreach (ModWaterStyle modWaterStyle in this.list)
			{
				int type = modWaterStyle.Slot;
				if (Main.waterStyle == type)
				{
					Main.liquidAlpha[type] += 0.2f;
					if (Main.liquidAlpha[type] > 1f)
					{
						Main.liquidAlpha[type] = 1f;
					}
				}
				else
				{
					Main.liquidAlpha[type] -= 0.2f;
					if (Main.liquidAlpha[type] < 0f)
					{
						Main.liquidAlpha[type] = 0f;
					}
				}
			}
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x0050CB10 File Offset: 0x0050AD10
		public void DrawWaterfall(WaterfallManager waterfallManager)
		{
			foreach (ModWaterStyle waterStyle in this.list)
			{
				if (Main.liquidAlpha[waterStyle.Slot] > 0f)
				{
					waterfallManager.DrawWaterfall(waterStyle.ChooseWaterfallStyle(), Main.liquidAlpha[waterStyle.Slot]);
				}
			}
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x0050CB88 File Offset: 0x0050AD88
		public void LightColorMultiplier(int style, float factor, ref float r, ref float g, ref float b)
		{
			ModWaterStyle waterStyle = base.Get(style);
			if (waterStyle != null)
			{
				waterStyle.LightColorMultiplier(ref r, ref g, ref b);
				r *= factor;
				g *= factor;
				b *= factor;
			}
		}
	}
}
