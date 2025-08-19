using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000594 RID: 1428
	public class LowLifeShader : ChromaShader
	{
		// Token: 0x06004237 RID: 16951 RVA: 0x005F536C File Offset: 0x005F356C
		[RgbProcessor(new EffectDetailLevel[]
		{
			0,
			1
		}, IsTransparent = true)]
		private void ProcessAnyDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			float num = (float)Math.Cos((double)(time * 3.1415927f)) * 0.3f + 0.7f;
			Vector4 color = LowLifeShader._baseColor * num;
			color.W = LowLifeShader._baseColor.W;
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x04005973 RID: 22899
		private static Vector4 _baseColor = new Color(40, 0, 8, 255).ToVector4();
	}
}
