using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.Graphics;

namespace Terraria.GameContent.Golf
{
	// Token: 0x0200061A RID: 1562
	public class FancyGolfPredictionLine
	{
		// Token: 0x060044B1 RID: 17585 RVA: 0x0060B174 File Offset: 0x00609374
		public FancyGolfPredictionLine(int iterations)
		{
			this._positions = new List<Vector2>(iterations * 2 + 1);
			this._iterations = iterations;
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x0060B1E4 File Offset: 0x006093E4
		public void Update(Entity golfBall, Vector2 impactVelocity, float roughLandResistance)
		{
			bool flag = Main.tileSolid[379];
			Main.tileSolid[379] = false;
			this._positions.Clear();
			this._time += 0.016666668f;
			this._entity.position = golfBall.position;
			this._entity.width = golfBall.width;
			this._entity.height = golfBall.height;
			GolfHelper.HitGolfBall(this._entity, impactVelocity, roughLandResistance);
			this._positions.Add(this._entity.position);
			float angularVelocity = 0f;
			for (int i = 0; i < this._iterations; i++)
			{
				GolfHelper.StepGolfBall(this._entity, ref angularVelocity);
				this._positions.Add(this._entity.position);
			}
			Main.tileSolid[379] = flag;
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x0060B2C4 File Offset: 0x006094C4
		public void Draw(Camera camera, SpriteBatch spriteBatch, float chargeProgress)
		{
			this._drawer.Begin(camera.GameViewMatrix.TransformationMatrix);
			int count = this._positions.Count;
			Texture2D value = TextureAssets.Extra[33].Value;
			Vector2 vector3 = new Vector2(3.5f, 3.5f);
			Vector2 origin = value.Size() / 2f;
			Vector2 vector2 = vector3 - camera.UnscaledPosition;
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < this._positions.Count - 1; i++)
			{
				float length;
				float num4;
				this.GetSectionLength(i, out length, out num4);
				if (length != 0f)
				{
					while (num < num2 + length)
					{
						float num3 = (num - num2) / length + (float)i;
						Vector2 position = this.GetPosition((num - num2) / length + (float)i);
						Color color = this.GetColor2(num3);
						color *= MathHelper.Clamp(2f - 2f * num3 / (float)(this._positions.Count - 1), 0f, 1f);
						spriteBatch.Draw(value, position + vector2, null, color, 0f, origin, this.GetScale(num), 0, 0f);
						num += 4f;
					}
					num2 += length;
				}
			}
			this._drawer.End();
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x0060B42C File Offset: 0x0060962C
		private Color GetColor(float travelledLength)
		{
			float num = travelledLength % 200f / 200f;
			num *= (float)this._colors.Length;
			num -= this._time * 3.1415927f * 1.5f;
			num %= (float)this._colors.Length;
			if (num < 0f)
			{
				num += (float)this._colors.Length;
			}
			int num2 = (int)Math.Floor((double)num);
			int num3 = num2 + 1;
			num2 = Utils.Clamp<int>(num2 % this._colors.Length, 0, this._colors.Length - 1);
			num3 = Utils.Clamp<int>(num3 % this._colors.Length, 0, this._colors.Length - 1);
			float amount = num - (float)num2;
			Color color = Color.Lerp(this._colors[num2], this._colors[num3], amount);
			color.A = 64;
			return color * 0.6f;
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x0060B508 File Offset: 0x00609708
		private Color GetColor2(float index)
		{
			float num4 = index * 0.5f - this._time * 3.1415927f * 1.5f;
			int num2 = (int)Math.Floor((double)num4) % this._colors.Length;
			if (num2 < 0)
			{
				num2 += this._colors.Length;
			}
			int num3 = (num2 + 1) % this._colors.Length;
			float amount = num4 - (float)Math.Floor((double)num4);
			Color color = Color.Lerp(this._colors[num2], this._colors[num3], amount);
			color.A = 64;
			return color * 0.6f;
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x0060B59C File Offset: 0x0060979C
		private float GetScale(float travelledLength)
		{
			return 0.2f + Utils.GetLerpValue(0.8f, 1f, (float)Math.Cos((double)(travelledLength / 50f + this._time * -3.1415927f)) * 0.5f + 0.5f, true) * 0.15f;
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x0060B5EC File Offset: 0x006097EC
		private void GetSectionLength(int startIndex, out float length, out float rotation)
		{
			int num = startIndex + 1;
			if (num >= this._positions.Count)
			{
				num = this._positions.Count - 1;
			}
			length = Vector2.Distance(this._positions[startIndex], this._positions[num]);
			rotation = (this._positions[num] - this._positions[startIndex]).ToRotation();
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x0060B65C File Offset: 0x0060985C
		private Vector2 GetPosition(float indexProgress)
		{
			int num = (int)Math.Floor((double)indexProgress);
			int num2 = num + 1;
			if (num2 >= this._positions.Count)
			{
				num2 = this._positions.Count - 1;
			}
			float amount = indexProgress - (float)num;
			return Vector2.Lerp(this._positions[num], this._positions[num2], amount);
		}

		// Token: 0x04005A97 RID: 23191
		private readonly List<Vector2> _positions;

		// Token: 0x04005A98 RID: 23192
		private readonly Entity _entity = new FancyGolfPredictionLine.PredictionEntity();

		// Token: 0x04005A99 RID: 23193
		private readonly int _iterations;

		// Token: 0x04005A9A RID: 23194
		private readonly Color[] _colors = new Color[]
		{
			Color.White,
			Color.Gray
		};

		// Token: 0x04005A9B RID: 23195
		private readonly BasicDebugDrawer _drawer = new BasicDebugDrawer(Main.instance.GraphicsDevice);

		// Token: 0x04005A9C RID: 23196
		private float _time;

		// Token: 0x02000CC9 RID: 3273
		private class PredictionEntity : Entity
		{
		}
	}
}
