using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200024B RID: 587
	public class LowLifeShader : ChromaShader
	{
		// Token: 0x06001F41 RID: 8001 RVA: 0x00511F04 File Offset: 0x00510104
		[RgbProcessor(new EffectDetailLevel[]
		{
			0,
			1
		}, IsTransparent = true)]
		private void ProcessAnyDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			float scaleFactor = (float)Math.Cos((double)(time * 3.1415927f)) * 0.3f + 0.7f;
			Vector4 vector = LowLifeShader._baseColor * scaleFactor;
			vector.W = LowLifeShader._baseColor.W;
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004652 RID: 18002
		private static Vector4 _baseColor = new Color(40, 0, 8, 255).ToVector4();
	}
}
