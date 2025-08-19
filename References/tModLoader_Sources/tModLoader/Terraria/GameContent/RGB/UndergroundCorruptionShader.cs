using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020005A6 RID: 1446
	public class UndergroundCorruptionShader : ChromaShader
	{
		// Token: 0x06004276 RID: 17014 RVA: 0x005F74B8 File Offset: 0x005F56B8
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 value = Vector4.Lerp(this._flameColor, this._flameTipColor, 0.25f);
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time + fragment.GetCanvasPositionOfIndex(i).X)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._corruptionColor, value, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x005F7528 File Offset: 0x005F5728
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float dynamicNoise = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.05f), time * 0.1f);
				dynamicNoise = Math.Max(0f, 1f - dynamicNoise * dynamicNoise * 4f * (1.2f - canvasPositionOfIndex.Y)) * canvasPositionOfIndex.Y;
				dynamicNoise = MathHelper.Clamp(dynamicNoise, 0f, 1f);
				Vector4 value = Vector4.Lerp(this._flameColor, this._flameTipColor, dynamicNoise);
				Vector4 color = Vector4.Lerp(this._corruptionColor, value, dynamicNoise);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x040059AD RID: 22957
		private readonly Vector4 _corruptionColor = new Vector4(Color.Purple.ToVector3() * 0.2f, 1f);

		// Token: 0x040059AE RID: 22958
		private readonly Vector4 _flameColor = Color.Green.ToVector4();

		// Token: 0x040059AF RID: 22959
		private readonly Vector4 _flameTipColor = Color.Yellow.ToVector4();
	}
}
