using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200024E RID: 590
	public class RainShader : ChromaShader
	{
		// Token: 0x06001F4D RID: 8013 RVA: 0x005123E4 File Offset: 0x005105E4
		public override void Update(float elapsedTime)
		{
			this._inBloodMoon = Main.bloodMoon;
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x005123F4 File Offset: 0x005105F4
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		}, IsTransparent = true)]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 value;
			if (this._inBloodMoon)
			{
				value = new Vector4(1f, 0f, 0f, 1f);
			}
			else
			{
				value = new Vector4(0f, 0f, 1f, 1f);
			}
			Vector4 vector = new Vector4(0f, 0f, 0f, 0.75f);
			for (int i = 0; i < fragment.Count; i++)
			{
				ref Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 10f + time) % 10f - canvasPositionOfIndex.Y;
				Vector4 vector2 = vector;
				if (num > 0f)
				{
					float amount = Math.Max(0f, 1.2f - num);
					if (num < 0.2f)
					{
						amount = num * 5f;
					}
					vector2 = Vector4.Lerp(vector2, value, amount);
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x0400465B RID: 18011
		private bool _inBloodMoon;
	}
}
