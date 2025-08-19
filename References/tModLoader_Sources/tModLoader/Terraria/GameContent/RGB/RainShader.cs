using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200059E RID: 1438
	public class RainShader : ChromaShader
	{
		// Token: 0x0600425E RID: 16990 RVA: 0x005F65EA File Offset: 0x005F47EA
		public override void Update(float elapsedTime)
		{
			this._inBloodMoon = Main.bloodMoon;
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x005F65F8 File Offset: 0x005F47F8
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		}, IsTransparent = true)]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 value = (!this._inBloodMoon) ? new Vector4(0f, 0f, 1f, 1f) : new Vector4(1f, 0f, 0f, 1f);
			Vector4 vector;
			vector..ctor(0f, 0f, 0f, 0.75f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
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

		// Token: 0x04005992 RID: 22930
		private bool _inBloodMoon;
	}
}
