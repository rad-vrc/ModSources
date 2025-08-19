using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using Terraria.Utilities;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000246 RID: 582
	public class EyeballShader : ChromaShader
	{
		// Token: 0x06001F30 RID: 7984 RVA: 0x00511484 File Offset: 0x0050F684
		public EyeballShader(bool isSpawning)
		{
			this._isSpawning = isSpawning;
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x005114D8 File Offset: 0x0050F6D8
		public override void Update(float elapsedTime)
		{
			this.UpdateEyelid(elapsedTime);
			bool flag = this._timeUntilPupilMove <= 0f;
			this._pupilOffset = (this._targetOffset + this._pupilOffset) * 0.5f;
			this._timeUntilPupilMove -= elapsedTime;
			if (flag)
			{
				float num = (float)this._random.NextDouble() * 6.2831855f;
				float scaleFactor;
				if (this._isSpawning)
				{
					this._timeUntilPupilMove = (float)this._random.NextDouble() * 0.4f + 0.3f;
					scaleFactor = (float)this._random.NextDouble() * 0.7f;
				}
				else
				{
					this._timeUntilPupilMove = (float)this._random.NextDouble() * 0.4f + 0.6f;
					scaleFactor = (float)this._random.NextDouble() * 0.3f;
				}
				this._targetOffset = new Vector2((float)Math.Cos((double)num), (float)Math.Sin((double)num)) * scaleFactor;
			}
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x005115D0 File Offset: 0x0050F7D0
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

		// Token: 0x06001F33 RID: 7987 RVA: 0x00511718 File Offset: 0x0050F918
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector2 vector = new Vector2(1.5f, 0.5f);
			Vector2 value = vector + this._pupilOffset;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector2 vector2 = canvasPositionOfIndex - vector;
				Vector4 vector3 = Vector4.One;
				float num = (value - canvasPositionOfIndex).Length();
				for (int j = 1; j < EyeballShader.Rings.Length; j++)
				{
					EyeballShader.Ring ring = EyeballShader.Rings[j];
					EyeballShader.Ring ring2 = EyeballShader.Rings[j - 1];
					if (num < ring.Distance)
					{
						vector3 = Vector4.Lerp(ring2.Color, ring.Color, (num - ring2.Distance) / (ring.Distance - ring2.Distance));
						break;
					}
				}
				float num2 = (float)Math.Sqrt((double)(1f - 0.4f * vector2.Y * vector2.Y)) * 5f;
				float num3 = Math.Abs(vector2.X) - num2 * (1.1f * this._eyelidProgress - 0.1f);
				if (num3 > 0f)
				{
					vector3 = Vector4.Lerp(vector3, this._eyelidColor, Math.Min(1f, num3 * 10f));
				}
				fragment.SetColor(i, vector3);
			}
		}

		// Token: 0x0400463C RID: 17980
		private static readonly EyeballShader.Ring[] Rings = new EyeballShader.Ring[]
		{
			new EyeballShader.Ring(Color.Black.ToVector4(), 0f),
			new EyeballShader.Ring(Color.Black.ToVector4(), 0.4f),
			new EyeballShader.Ring(new Color(17, 220, 237).ToVector4(), 0.5f),
			new EyeballShader.Ring(new Color(17, 120, 237).ToVector4(), 0.6f),
			new EyeballShader.Ring(Vector4.One, 0.65f)
		};

		// Token: 0x0400463D RID: 17981
		private readonly Vector4 _eyelidColor = new Color(108, 110, 75).ToVector4();

		// Token: 0x0400463E RID: 17982
		private float _eyelidProgress;

		// Token: 0x0400463F RID: 17983
		private Vector2 _pupilOffset = Vector2.Zero;

		// Token: 0x04004640 RID: 17984
		private Vector2 _targetOffset = Vector2.Zero;

		// Token: 0x04004641 RID: 17985
		private readonly UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x04004642 RID: 17986
		private float _timeUntilPupilMove;

		// Token: 0x04004643 RID: 17987
		private float _eyelidStateTime;

		// Token: 0x04004644 RID: 17988
		private readonly bool _isSpawning;

		// Token: 0x04004645 RID: 17989
		private EyeballShader.EyelidState _eyelidState;

		// Token: 0x0200062F RID: 1583
		private struct Ring
		{
			// Token: 0x060033B7 RID: 13239 RVA: 0x00606A8D File Offset: 0x00604C8D
			public Ring(Vector4 color, float distance)
			{
				this.Color = color;
				this.Distance = distance;
			}

			// Token: 0x040060EB RID: 24811
			public readonly Vector4 Color;

			// Token: 0x040060EC RID: 24812
			public readonly float Distance;
		}

		// Token: 0x02000630 RID: 1584
		private enum EyelidState
		{
			// Token: 0x040060EE RID: 24814
			Closed,
			// Token: 0x040060EF RID: 24815
			Opening,
			// Token: 0x040060F0 RID: 24816
			Open,
			// Token: 0x040060F1 RID: 24817
			Closing
		}
	}
}
