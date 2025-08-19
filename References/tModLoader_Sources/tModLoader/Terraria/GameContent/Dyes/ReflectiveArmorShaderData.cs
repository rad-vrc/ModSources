using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x02000635 RID: 1589
	public class ReflectiveArmorShaderData : ArmorShaderData
	{
		// Token: 0x0600459D RID: 17821 RVA: 0x00614547 File Offset: 0x00612747
		public ReflectiveArmorShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x00614554 File Offset: 0x00612754
		public override void Apply(Entity entity, DrawData? drawData)
		{
			if (entity == null)
			{
				EffectParameter effectParameter = base.Shader.Parameters["uLightSource"];
				if (effectParameter != null)
				{
					effectParameter.SetValue(Vector3.Zero);
				}
			}
			else
			{
				float num = 0f;
				if (drawData != null)
				{
					num = drawData.Value.rotation;
				}
				Vector2 position = entity.position;
				float num2 = (float)entity.width;
				float num3 = (float)entity.height;
				Vector2 vector = position + new Vector2(num2, num3) * 0.1f;
				num2 *= 0.8f;
				num3 *= 0.8f;
				Vector3 subLight = Lighting.GetSubLight(vector + new Vector2(num2 * 0.5f, 0f));
				Vector3 subLight2 = Lighting.GetSubLight(vector + new Vector2(0f, num3 * 0.5f));
				Vector3 subLight3 = Lighting.GetSubLight(vector + new Vector2(num2, num3 * 0.5f));
				Vector3 subLight4 = Lighting.GetSubLight(vector + new Vector2(num2 * 0.5f, num3));
				float num4 = subLight.X + subLight.Y + subLight.Z;
				float num5 = subLight2.X + subLight2.Y + subLight2.Z;
				float num6 = subLight3.X + subLight3.Y + subLight3.Z;
				float num7 = subLight4.X + subLight4.Y + subLight4.Z;
				Vector2 spinningpoint;
				spinningpoint..ctor(num6 - num5, num7 - num4);
				float num8 = spinningpoint.Length();
				if (num8 > 1f)
				{
					num8 = 1f;
					spinningpoint /= num8;
				}
				if (entity.direction == -1)
				{
					spinningpoint.X *= -1f;
				}
				spinningpoint = spinningpoint.RotatedBy((double)(0f - num), default(Vector2));
				Vector3 value;
				value..ctor(spinningpoint, 1f - (spinningpoint.X * spinningpoint.X + spinningpoint.Y * spinningpoint.Y));
				value.X *= 2f;
				value.Y -= 0.15f;
				value.Y *= 2f;
				value.Normalize();
				value.Z *= 0.6f;
				EffectParameter effectParameter2 = base.Shader.Parameters["uLightSource"];
				if (effectParameter2 != null)
				{
					effectParameter2.SetValue(value);
				}
			}
			base.Apply(entity, drawData);
		}
	}
}
