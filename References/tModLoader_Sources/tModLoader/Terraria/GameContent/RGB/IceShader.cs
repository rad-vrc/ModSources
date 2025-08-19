using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200058F RID: 1423
	public class IceShader : ChromaShader
	{
		// Token: 0x06004229 RID: 16937 RVA: 0x005F4C8C File Offset: 0x005F2E8C
		public IceShader(Color baseColor, Color iceColor)
		{
			this._baseColor = baseColor.ToVector4();
			this._iceColor = iceColor.ToVector4();
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x005F4CD8 File Offset: 0x005F2ED8
		[RgbProcessor(new EffectDetailLevel[]
		{
			0,
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float dynamicNoise = NoiseHelper.GetDynamicNoise(new Vector2((canvasPositionOfIndex.X - canvasPositionOfIndex.Y) * 0.2f, 0f), time / 5f);
				dynamicNoise = Math.Max(0f, 1f - dynamicNoise * 1.5f);
				float dynamicNoise2 = NoiseHelper.GetDynamicNoise(new Vector2((canvasPositionOfIndex.X - canvasPositionOfIndex.Y) * 0.3f, 0.3f), time / 20f);
				dynamicNoise2 = Math.Max(0f, 1f - dynamicNoise2 * 5f);
				Vector4 value = Vector4.Lerp(this._baseColor, this._iceColor, dynamicNoise);
				value = Vector4.Lerp(value, this._shineColor, dynamicNoise2);
				fragment.SetColor(i, value);
			}
		}

		// Token: 0x04005967 RID: 22887
		private readonly Vector4 _baseColor;

		// Token: 0x04005968 RID: 22888
		private readonly Vector4 _iceColor;

		// Token: 0x04005969 RID: 22889
		private readonly Vector4 _shineColor = new Vector4(1f, 1f, 0.7f, 1f);
	}
}
