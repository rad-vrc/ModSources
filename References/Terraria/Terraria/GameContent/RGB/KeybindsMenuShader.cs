using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200024A RID: 586
	internal class KeybindsMenuShader : ChromaShader
	{
		// Token: 0x06001F3E RID: 7998 RVA: 0x00511E78 File Offset: 0x00510078
		[RgbProcessor(new EffectDetailLevel[]
		{
			0,
			1
		}, IsTransparent = true)]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			float scaleFactor = (float)Math.Cos((double)(time * 1.5707964f)) * 0.2f + 0.8f;
			Vector4 vector = KeybindsMenuShader._baseColor * scaleFactor;
			vector.W = KeybindsMenuShader._baseColor.W;
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004651 RID: 18001
		private static Vector4 _baseColor = new Color(20, 20, 20, 245).ToVector4();
	}
}
