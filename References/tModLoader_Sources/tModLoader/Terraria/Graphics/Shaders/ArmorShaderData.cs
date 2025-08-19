using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x02000444 RID: 1092
	public class ArmorShaderData : ShaderData
	{
		// Token: 0x060035F3 RID: 13811 RVA: 0x00579F60 File Offset: 0x00578160
		[Obsolete("Removed in 1.4.5. Use Asset<Effect> version instead. Asset version works with AsyncLoad")]
		public ArmorShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x00579FAC File Offset: 0x005781AC
		public ArmorShaderData(Asset<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x00579FF8 File Offset: 0x005781F8
		public virtual void Apply(Entity entity, DrawData? drawData = null)
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
				Vector4 value2 = (value.sourceRect == null) ? new Vector4(0f, 0f, (float)value.texture.Width, (float)value.texture.Height) : new Vector4((float)value.sourceRect.Value.X, (float)value.sourceRect.Value.Y, (float)value.sourceRect.Value.Width, (float)value.sourceRect.Value.Height);
				EffectParameter effectParameter7 = base.Shader.Parameters["uSourceRect"];
				if (effectParameter7 != null)
				{
					effectParameter7.SetValue(value2);
				}
				EffectParameter effectParameter8 = base.Shader.Parameters["uLegacyArmorSourceRect"];
				if (effectParameter8 != null)
				{
					effectParameter8.SetValue(value2);
				}
				EffectParameter effectParameter9 = base.Shader.Parameters["uWorldPosition"];
				if (effectParameter9 != null)
				{
					effectParameter9.SetValue(Main.screenPosition + value.position);
				}
				EffectParameter effectParameter10 = base.Shader.Parameters["uImageSize0"];
				if (effectParameter10 != null)
				{
					effectParameter10.SetValue(new Vector2((float)value.texture.Width, (float)value.texture.Height));
				}
				EffectParameter effectParameter11 = base.Shader.Parameters["uLegacyArmorSheetSize"];
				if (effectParameter11 != null)
				{
					effectParameter11.SetValue(new Vector2((float)value.texture.Width, (float)value.texture.Height));
				}
				EffectParameter effectParameter12 = base.Shader.Parameters["uRotation"];
				if (effectParameter12 != null)
				{
					effectParameter12.SetValue(value.rotation * (value.effect.HasFlag(1) ? -1f : 1f));
				}
				EffectParameter effectParameter13 = base.Shader.Parameters["uDirection"];
				if (effectParameter13 != null)
				{
					effectParameter13.SetValue((!value.effect.HasFlag(1)) ? 1 : -1);
				}
			}
			else
			{
				Vector4 value3;
				value3..ctor(0f, 0f, 4f, 4f);
				EffectParameter effectParameter14 = base.Shader.Parameters["uSourceRect"];
				if (effectParameter14 != null)
				{
					effectParameter14.SetValue(value3);
				}
				EffectParameter effectParameter15 = base.Shader.Parameters["uLegacyArmorSourceRect"];
				if (effectParameter15 != null)
				{
					effectParameter15.SetValue(value3);
				}
				EffectParameter effectParameter16 = base.Shader.Parameters["uRotation"];
				if (effectParameter16 != null)
				{
					effectParameter16.SetValue(0f);
				}
			}
			if (this._uImage != null)
			{
				Main.graphics.GraphicsDevice.Textures[1] = this._uImage.Value;
				EffectParameter effectParameter17 = base.Shader.Parameters["uImageSize1"];
				if (effectParameter17 != null)
				{
					effectParameter17.SetValue(new Vector2((float)this._uImage.Width(), (float)this._uImage.Height()));
				}
			}
			if (entity != null)
			{
				EffectParameter effectParameter18 = base.Shader.Parameters["uDirection"];
				if (effectParameter18 != null)
				{
					effectParameter18.SetValue((float)entity.direction);
				}
			}
			Player player = entity as Player;
			if (player != null)
			{
				Rectangle bodyFrame = player.bodyFrame;
				EffectParameter effectParameter19 = base.Shader.Parameters["uLegacyArmorSourceRect"];
				if (effectParameter19 != null)
				{
					effectParameter19.SetValue(new Vector4((float)bodyFrame.X, (float)bodyFrame.Y, (float)bodyFrame.Width, (float)bodyFrame.Height));
				}
				EffectParameter effectParameter20 = base.Shader.Parameters["uLegacyArmorSheetSize"];
				if (effectParameter20 != null)
				{
					effectParameter20.SetValue(new Vector2(40f, 1120f));
				}
			}
			this.Apply();
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x0057A499 File Offset: 0x00578699
		public ArmorShaderData UseColor(float r, float g, float b)
		{
			return this.UseColor(new Vector3(r, g, b));
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x0057A4A9 File Offset: 0x005786A9
		public ArmorShaderData UseColor(Color color)
		{
			return this.UseColor(color.ToVector3());
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x0057A4B8 File Offset: 0x005786B8
		public ArmorShaderData UseColor(Vector3 color)
		{
			this._uColor = color;
			return this;
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x0057A4C2 File Offset: 0x005786C2
		public ArmorShaderData UseImage(string path)
		{
			this._uImage = Main.Assets.Request<Texture2D>(path);
			return this;
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x0057A4D6 File Offset: 0x005786D6
		public ArmorShaderData UseImage(Asset<Texture2D> asset)
		{
			this._uImage = asset;
			return this;
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x0057A4E0 File Offset: 0x005786E0
		public ArmorShaderData UseOpacity(float alpha)
		{
			this._uOpacity = alpha;
			return this;
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x0057A4EA File Offset: 0x005786EA
		public ArmorShaderData UseTargetPosition(Vector2 position)
		{
			this._uTargetPosition = position;
			return this;
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x0057A4F4 File Offset: 0x005786F4
		public ArmorShaderData UseSecondaryColor(float r, float g, float b)
		{
			return this.UseSecondaryColor(new Vector3(r, g, b));
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x0057A504 File Offset: 0x00578704
		public ArmorShaderData UseSecondaryColor(Color color)
		{
			return this.UseSecondaryColor(color.ToVector3());
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x0057A513 File Offset: 0x00578713
		public ArmorShaderData UseSecondaryColor(Vector3 color)
		{
			this._uSecondaryColor = color;
			return this;
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x0057A51D File Offset: 0x0057871D
		public ArmorShaderData UseSaturation(float saturation)
		{
			this._uSaturation = saturation;
			return this;
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x0057A527 File Offset: 0x00578727
		public virtual ArmorShaderData GetSecondaryShader(Entity entity)
		{
			return this;
		}

		// Token: 0x04004FF9 RID: 20473
		private Vector3 _uColor = Vector3.One;

		// Token: 0x04004FFA RID: 20474
		private Vector3 _uSecondaryColor = Vector3.One;

		// Token: 0x04004FFB RID: 20475
		private float _uSaturation = 1f;

		// Token: 0x04004FFC RID: 20476
		private float _uOpacity = 1f;

		// Token: 0x04004FFD RID: 20477
		private Asset<Texture2D> _uImage;

		// Token: 0x04004FFE RID: 20478
		private Vector2 _uTargetPosition = Vector2.One;
	}
}
