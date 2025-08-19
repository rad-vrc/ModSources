using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x02000447 RID: 1095
	public class HairShaderData : ShaderData
	{
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x0057A6BB File Offset: 0x005788BB
		public bool ShaderDisabled
		{
			get
			{
				return this._shaderDisabled;
			}
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x0057A6C4 File Offset: 0x005788C4
		[Obsolete("Removed in 1.4.5. Use Asset<Effect> version instead. Asset version works with AsyncLoad")]
		public HairShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x0057A710 File Offset: 0x00578910
		public HairShaderData(Asset<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x0057A75C File Offset: 0x0057895C
		public virtual void Apply(Player player, DrawData? drawData = null)
		{
			if (!this._shaderDisabled)
			{
				EffectParameter effectParameter = base.Shader.Parameters["uColor"];
				if (effectParameter != null)
				{
					effectParameter.SetValue(this._uColor);
				}
				EffectParameter effectParameter2 = base.Shader.Parameters["uSaturation"];
				if (effectParameter2 != null)
				{
					effectParameter2.SetValue(this._uSaturation);
				}
				EffectParameter effectParameter3 = base.Shader.Parameters["uSecondaryColor"];
				if (effectParameter3 != null)
				{
					effectParameter3.SetValue(this._uSecondaryColor);
				}
				EffectParameter effectParameter4 = base.Shader.Parameters["uTime"];
				if (effectParameter4 != null)
				{
					effectParameter4.SetValue(Main.GlobalTimeWrappedHourly);
				}
				EffectParameter effectParameter5 = base.Shader.Parameters["uOpacity"];
				if (effectParameter5 != null)
				{
					effectParameter5.SetValue(this._uOpacity);
				}
				EffectParameter effectParameter6 = base.Shader.Parameters["uTargetPosition"];
				if (effectParameter6 != null)
				{
					effectParameter6.SetValue(this._uTargetPosition);
				}
				if (drawData != null)
				{
					DrawData value = drawData.Value;
					Vector4 value2;
					value2..ctor((float)value.sourceRect.Value.X, (float)value.sourceRect.Value.Y, (float)value.sourceRect.Value.Width, (float)value.sourceRect.Value.Height);
					EffectParameter effectParameter7 = base.Shader.Parameters["uSourceRect"];
					if (effectParameter7 != null)
					{
						effectParameter7.SetValue(value2);
					}
					EffectParameter effectParameter8 = base.Shader.Parameters["uWorldPosition"];
					if (effectParameter8 != null)
					{
						effectParameter8.SetValue(Main.screenPosition + value.position);
					}
					EffectParameter effectParameter9 = base.Shader.Parameters["uImageSize0"];
					if (effectParameter9 != null)
					{
						effectParameter9.SetValue(new Vector2((float)value.texture.Width, (float)value.texture.Height));
					}
				}
				else
				{
					EffectParameter effectParameter10 = base.Shader.Parameters["uSourceRect"];
					if (effectParameter10 != null)
					{
						effectParameter10.SetValue(new Vector4(0f, 0f, 4f, 4f));
					}
				}
				if (this._uImage != null)
				{
					Main.graphics.GraphicsDevice.Textures[1] = this._uImage.Value;
					EffectParameter effectParameter11 = base.Shader.Parameters["uImageSize1"];
					if (effectParameter11 != null)
					{
						effectParameter11.SetValue(new Vector2((float)this._uImage.Width(), (float)this._uImage.Height()));
					}
				}
				if (player != null)
				{
					EffectParameter effectParameter12 = base.Shader.Parameters["uDirection"];
					if (effectParameter12 != null)
					{
						effectParameter12.SetValue((float)player.direction);
					}
				}
				this.Apply();
			}
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x0057AA18 File Offset: 0x00578C18
		public virtual Color GetColor(Player player, Color lightColor)
		{
			return new Color(lightColor.ToVector4() * player.hairColor.ToVector4());
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x0057AA36 File Offset: 0x00578C36
		public HairShaderData UseColor(float r, float g, float b)
		{
			return this.UseColor(new Vector3(r, g, b));
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x0057AA46 File Offset: 0x00578C46
		public HairShaderData UseColor(Color color)
		{
			return this.UseColor(color.ToVector3());
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x0057AA55 File Offset: 0x00578C55
		public HairShaderData UseColor(Vector3 color)
		{
			this._uColor = color;
			return this;
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x0057AA5F File Offset: 0x00578C5F
		public HairShaderData UseImage(string path)
		{
			this._uImage = Main.Assets.Request<Texture2D>(path);
			return this;
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x0057AA73 File Offset: 0x00578C73
		public HairShaderData UseImage(Asset<Texture2D> asset)
		{
			this._uImage = asset;
			return this;
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x0057AA7D File Offset: 0x00578C7D
		public HairShaderData UseOpacity(float alpha)
		{
			this._uOpacity = alpha;
			return this;
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x0057AA87 File Offset: 0x00578C87
		public HairShaderData UseSecondaryColor(float r, float g, float b)
		{
			return this.UseSecondaryColor(new Vector3(r, g, b));
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x0057AA97 File Offset: 0x00578C97
		public HairShaderData UseSecondaryColor(Color color)
		{
			return this.UseSecondaryColor(color.ToVector3());
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x0057AAA6 File Offset: 0x00578CA6
		public HairShaderData UseSecondaryColor(Vector3 color)
		{
			this._uSecondaryColor = color;
			return this;
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x0057AAB0 File Offset: 0x00578CB0
		public HairShaderData UseSaturation(float saturation)
		{
			this._uSaturation = saturation;
			return this;
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x0057AABA File Offset: 0x00578CBA
		public HairShaderData UseTargetPosition(Vector2 position)
		{
			this._uTargetPosition = position;
			return this;
		}

		// Token: 0x04005005 RID: 20485
		protected Vector3 _uColor = Vector3.One;

		// Token: 0x04005006 RID: 20486
		protected Vector3 _uSecondaryColor = Vector3.One;

		// Token: 0x04005007 RID: 20487
		protected float _uSaturation = 1f;

		// Token: 0x04005008 RID: 20488
		protected float _uOpacity = 1f;

		// Token: 0x04005009 RID: 20489
		protected Asset<Texture2D> _uImage;

		// Token: 0x0400500A RID: 20490
		protected bool _shaderDisabled;

		// Token: 0x0400500B RID: 20491
		private Vector2 _uTargetPosition = Vector2.One;
	}
}
