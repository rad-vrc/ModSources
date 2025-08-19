using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200058A RID: 1418
	public class FrostLegionShader : ChromaShader
	{
		// Token: 0x0600421B RID: 16923 RVA: 0x005F4431 File Offset: 0x005F2631
		public FrostLegionShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x005F4454 File Offset: 0x005F2654
		[RgbProcessor(new EffectDetailLevel[]
		{
			1,
			0
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float staticNoise = NoiseHelper.GetStaticNoise(fragment.GetGridPositionOfIndex(i).X / 2);
				float num = (canvasPositionOfIndex.Y + canvasPositionOfIndex.X / 2f - staticNoise + time) % 2f;
				if (num < 0f)
				{
					num += 2f;
				}
				if (num < 0.2f)
				{
					num = 1f - num / 0.2f;
				}
				float amount = num / 2f;
				Vector4 color = Vector4.Lerp(this._primaryColor, this._secondaryColor, amount);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x04005955 RID: 22869
		private readonly Vector4 _primaryColor;

		// Token: 0x04005956 RID: 22870
		private readonly Vector4 _secondaryColor;
	}
}
