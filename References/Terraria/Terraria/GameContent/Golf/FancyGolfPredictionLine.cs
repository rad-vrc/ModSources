using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.Graphics;

namespace Terraria.GameContent.Golf
{
	// Token: 0x020002A1 RID: 673
	public class FancyGolfPredictionLine
	{
		// Token: 0x060020C6 RID: 8390 RVA: 0x0051EE10 File Offset: 0x0051D010
		public FancyGolfPredictionLine(int iterations)
		{
			this._positions = new List<Vector2>(iterations * 2 + 1);
			this._iterations = iterations;
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x0051EE80 File Offset: 0x0051D080
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
			float num = 0f;
			for (int i = 0; i < this._iterations; i++)
			{
				GolfHelper.StepGolfBall(this._entity, ref num);
				this._positions.Add(this._entity.position);
			}
			Main.tileSolid[379] = flag;
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x0051EF60 File Offset: 0x0051D160
		public void Draw(Camera camera, SpriteBatch spriteBatch, float chargeProgress)
		{
			this._drawer.Begin(camera.GameViewMatrix.TransformationMatrix);
			int count = this._positions.Count;
			Texture2D value = TextureAssets.Extra[33].Value;
			Vector2 value2 = new Vector2(3.5f, 3.5f);
			Vector2 origin = value.Size() / 2f;
			Vector2 value3 = value2 - camera.UnscaledPosition;
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < this._positions.Count - 1; i++)
			{
				float num3;
				float num4;
				this.GetSectionLength(i, out num3, out num4);
				if (num3 != 0f)
				{
					while (num < num2 + num3)
					{
						float num5 = (num - num2) / num3 + (float)i;
						Vector2 position = this.GetPosition((num - num2) / num3 + (float)i);
						Color color = this.GetColor2(num5);
						color *= MathHelper.Clamp(2f - 2f * num5 / (float)(this._positions.Count - 1), 0f, 1f);
						spriteBatch.Draw(value, position + value3, null, color, 0f, origin, this.GetScale(num), SpriteEffects.None, 0f);
						num += 4f;
					}
					num2 += num3;
				}
			}
			this._drawer.End();
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x0051F0C8 File Offset: 0x0051D2C8
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
			Color value = Color.Lerp(this._colors[num2], this._colors[num3], amount);
			value.A = 64;
			return value * 0.6f;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0051F1A8 File Offset: 0x0051D3A8
		private Color GetColor2(float index)
		{
			float num = index * 0.5f - this._time * 3.1415927f * 1.5f;
			int num2 = (int)Math.Floor((double)num) % this._colors.Length;
			if (num2 < 0)
			{
				num2 += this._colors.Length;
			}
			int num3 = (num2 + 1) % this._colors.Length;
			float amount = num - (float)Math.Floor((double)num);
			Color value = Color.Lerp(this._colors[num2], this._colors[num3], amount);
			value.A = 64;
			return value * 0.6f;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x0051F23C File Offset: 0x0051D43C
		private float GetScale(float travelledLength)
		{
			return 0.2f + Utils.GetLerpValue(0.8f, 1f, (float)Math.Cos((double)(travelledLength / 50f + this._time * -3.1415927f)) * 0.5f + 0.5f, true) * 0.15f;
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0051F28C File Offset: 0x0051D48C
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

		// Token: 0x060020CD RID: 8397 RVA: 0x0051F2FC File Offset: 0x0051D4FC
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

		// Token: 0x040046FB RID: 18171
		private readonly List<Vector2> _positions;

		// Token: 0x040046FC RID: 18172
		private readonly Entity _entity = new FancyGolfPredictionLine.PredictionEntity();

		// Token: 0x040046FD RID: 18173
		private readonly int _iterations;

		// Token: 0x040046FE RID: 18174
		private readonly Color[] _colors = new Color[]
		{
			Color.White,
			Color.Gray
		};

		// Token: 0x040046FF RID: 18175
		private readonly BasicDebugDrawer _drawer = new BasicDebugDrawer(Main.instance.GraphicsDevice);

		// Token: 0x04004700 RID: 18176
		private float _time;

		// Token: 0x0200068B RID: 1675
		private class PredictionEntity : Entity
		{
		}
	}
}
