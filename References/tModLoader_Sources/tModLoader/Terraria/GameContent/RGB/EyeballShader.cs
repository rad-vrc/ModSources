using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using Terraria.Utilities;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000588 RID: 1416
	public class EyeballShader : ChromaShader
	{
		// Token: 0x06004213 RID: 16915 RVA: 0x005F3DAC File Offset: 0x005F1FAC
		public EyeballShader(bool isSpawning)
		{
			this._isSpawning = isSpawning;
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x005F3E00 File Offset: 0x005F2000
		public override void Update(float elapsedTime)
		{
			this.UpdateEyelid(elapsedTime);
			bool flag = this._timeUntilPupilMove <= 0f;
			this._pupilOffset = (this._targetOffset + this._pupilOffset) * 0.5f;
			this._timeUntilPupilMove -= elapsedTime;
			if (flag)
			{
				float num2 = (float)this._random.NextDouble() * 6.2831855f;
				float num3;
				if (this._isSpawning)
				{
					this._timeUntilPupilMove = (float)this._random.NextDouble() * 0.4f + 0.3f;
					num3 = (float)this._random.NextDouble() * 0.7f;
				}
				else
				{
					this._timeUntilPupilMove = (float)this._random.NextDouble() * 0.4f + 0.6f;
					num3 = (float)this._random.NextDouble() * 0.3f;
				}
				this._targetOffset = new Vector2((float)Math.Cos((double)num2), (float)Math.Sin((double)num2)) * num3;
			}
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x005F3EF8 File Offset: 0x005F20F8
		private void UpdateEyelid(float elapsedTime)
		{
			float num = 0.5f;
			float num2 = 6f;
			if (this._isSpawning)
			{
				if (NPC.MoonLordCountdown >= NPC.MaxMoonLordCountdown - 10)
				{
					this._eyelidStateTime = 0f;
					this._eyelidState = EyeballShader.EyelidState.Closed;
				}
				num = (float)NPC.MoonLordCountdown / (float)NPC.MaxMoonLordCountdown * 10f + 0.5f;
				num2 = 2f;
			}
			this._eyelidStateTime += elapsedTime;
			switch (this._eyelidState)
			{
			case EyeballShader.EyelidState.Closed:
				this._eyelidProgress = 0f;
				if (this._eyelidStateTime > num)
				{
					this._eyelidStateTime = 0f;
					this._eyelidState = EyeballShader.EyelidState.Opening;
					return;
				}
				break;
			case EyeballShader.EyelidState.Opening:
				this._eyelidProgress = this._eyelidStateTime / 0.4f;
				if (this._eyelidStateTime > 0.4f)
				{
					this._eyelidStateTime = 0f;
					this._eyelidState = EyeballShader.EyelidState.Open;
					return;
				}
				break;
			case EyeballShader.EyelidState.Open:
				this._eyelidProgress = 1f;
				if (this._eyelidStateTime > num2)
				{
					this._eyelidStateTime = 0f;
					this._eyelidState = EyeballShader.EyelidState.Closing;
					return;
				}
				break;
			case EyeballShader.EyelidState.Closing:
				this._eyelidProgress = 1f - this._eyelidStateTime / 0.4f;
				if (this._eyelidStateTime > 0.4f)
				{
					this._eyelidStateTime = 0f;
					this._eyelidState = EyeballShader.EyelidState.Closed;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x005F4040 File Offset: 0x005F2240
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector2 vector;
			vector..ctor(1.5f, 0.5f);
			Vector2 vector2 = vector + this._pupilOffset;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector2 vector3 = canvasPositionOfIndex - vector;
				Vector4 vector4 = Vector4.One;
				float num = (vector2 - canvasPositionOfIndex).Length();
				for (int j = 1; j < EyeballShader.Rings.Length; j++)
				{
					EyeballShader.Ring ring = EyeballShader.Rings[j];
					EyeballShader.Ring ring2 = EyeballShader.Rings[j - 1];
					if (num < ring.Distance)
					{
						vector4 = Vector4.Lerp(ring2.Color, ring.Color, (num - ring2.Distance) / (ring.Distance - ring2.Distance));
						break;
					}
				}
				float num2 = (float)Math.Sqrt((double)(1f - 0.4f * vector3.Y * vector3.Y)) * 5f;
				float num3 = Math.Abs(vector3.X) - num2 * (1.1f * this._eyelidProgress - 0.1f);
				if (num3 > 0f)
				{
					vector4 = Vector4.Lerp(vector4, this._eyelidColor, Math.Min(1f, num3 * 10f));
				}
				fragment.SetColor(i, vector4);
			}
		}

		// Token: 0x04005948 RID: 22856
		private static readonly EyeballShader.Ring[] Rings = new EyeballShader.Ring[]
		{
			new EyeballShader.Ring(Color.Black.ToVector4(), 0f),
			new EyeballShader.Ring(Color.Black.ToVector4(), 0.4f),
			new EyeballShader.Ring(new Color(17, 220, 237).ToVector4(), 0.5f),
			new EyeballShader.Ring(new Color(17, 120, 237).ToVector4(), 0.6f),
			new EyeballShader.Ring(Vector4.One, 0.65f)
		};

		// Token: 0x04005949 RID: 22857
		private readonly Vector4 _eyelidColor = new Color(108, 110, 75).ToVector4();

		// Token: 0x0400594A RID: 22858
		private float _eyelidProgress;

		// Token: 0x0400594B RID: 22859
		private Vector2 _pupilOffset = Vector2.Zero;

		// Token: 0x0400594C RID: 22860
		private Vector2 _targetOffset = Vector2.Zero;

		// Token: 0x0400594D RID: 22861
		private readonly UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x0400594E RID: 22862
		private float _timeUntilPupilMove;

		// Token: 0x0400594F RID: 22863
		private float _eyelidStateTime;

		// Token: 0x04005950 RID: 22864
		private readonly bool _isSpawning;

		// Token: 0x04005951 RID: 22865
		private EyeballShader.EyelidState _eyelidState;

		// Token: 0x02000C5F RID: 3167
		private struct Ring
		{
			// Token: 0x06005FDE RID: 24542 RVA: 0x006D1826 File Offset: 0x006CFA26
			public Ring(Vector4 color, float distance)
			{
				this.Color = color;
				this.Distance = distance;
			}

			// Token: 0x0400796C RID: 31084
			public readonly Vector4 Color;

			// Token: 0x0400796D RID: 31085
			public readonly float Distance;
		}

		// Token: 0x02000C60 RID: 3168
		private enum EyelidState
		{
			// Token: 0x0400796F RID: 31087
			Closed,
			// Token: 0x04007970 RID: 31088
			Opening,
			// Token: 0x04007971 RID: 31089
			Open,
			// Token: 0x04007972 RID: 31090
			Closing
		}
	}
}
