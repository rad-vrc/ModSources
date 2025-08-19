using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000591 RID: 1425
	internal class KeybindsMenuShader : ChromaShader
	{
		// Token: 0x0600422E RID: 16942 RVA: 0x005F4FC8 File Offset: 0x005F31C8
		[RgbProcessor(new EffectDetailLevel[]
		{
			0,
			1
		}, IsTransparent = true)]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			float num = (float)Math.Cos((double)(time * 1.5707964f)) * 0.2f + 0.8f;
			Vector4 color = KeybindsMenuShader._baseColor * num;
			color.W = KeybindsMenuShader._baseColor.W;
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0400596D RID: 22893
		private static Vector4 _baseColor = new Color(20, 20, 20, 245).ToVector4();
	}
}
