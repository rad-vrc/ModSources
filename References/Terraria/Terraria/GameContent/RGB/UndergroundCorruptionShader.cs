﻿using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000257 RID: 599
	public class UndergroundCorruptionShader : ChromaShader
	{
		// Token: 0x06001F6C RID: 8044 RVA: 0x005130B8 File Offset: 0x005112B8
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 value = Vector4.Lerp(this._flameColor, this._flameTipColor, 0.25f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._corruptionColor, value, (float)Math.Sin((double)(time + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x00513128 File Offset: 0x00511328
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
				float num = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.05f), time * 0.1f);
				num = Math.Max(0f, 1f - num * num * 4f * (1.2f - canvasPositionOfIndex.Y)) * canvasPositionOfIndex.Y;
				num = MathHelper.Clamp(num, 0f, 1f);
				Vector4 value = Vector4.Lerp(this._flameColor, this._flameTipColor, num);
				Vector4 vector = Vector4.Lerp(this._corruptionColor, value, num);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004673 RID: 18035
		private readonly Vector4 _corruptionColor = new Vector4(Color.Purple.ToVector3() * 0.2f, 1f);

		// Token: 0x04004674 RID: 18036
		private readonly Vector4 _flameColor = Color.Green.ToVector4();

		// Token: 0x04004675 RID: 18037
		private readonly Vector4 _flameTipColor = Color.Yellow.ToVector4();
	}
}
