using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020005A9 RID: 1449
	public class UnderworldShader : ChromaShader
	{
		// Token: 0x0600427F RID: 17023 RVA: 0x005F79F4 File Offset: 0x005F5BF4
		public UnderworldShader(Color backColor, Color frontColor, float speed)
		{
			this._backColor = backColor.ToVector4();
			this._frontColor = frontColor.ToVector4();
			this._speed = speed;
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x005F7A20 File Offset: 0x005F5C20
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 color = Vector4.Lerp(this._backColor, this._frontColor, (float)Math.Sin((double)(time * this._speed + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x005F7A84 File Offset: 0x005F5C84
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float dynamicNoise = NoiseHelper.GetDynamicNoise(fragment.GetCanvasPositionOfIndex(i) * 0.5f, time * this._speed / 3f);
				Vector4 color = Vector4.Lerp(this._backColor, this._frontColor, dynamicNoise);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x040059B6 RID: 22966
		private readonly Vector4 _backColor;

		// Token: 0x040059B7 RID: 22967
		private readonly Vector4 _frontColor;

		// Token: 0x040059B8 RID: 22968
		private readonly float _speed;
	}
}
