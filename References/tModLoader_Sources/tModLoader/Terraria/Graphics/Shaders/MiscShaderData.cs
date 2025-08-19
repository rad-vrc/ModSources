using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x02000449 RID: 1097
	public class MiscShaderData : ShaderData
	{
		// Token: 0x06003621 RID: 13857 RVA: 0x0057AC08 File Offset: 0x00578E08
		[Obsolete("Removed in 1.4.5. Use Asset<Effect> version instead. Asset version works with AsyncLoad")]
		public MiscShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x0057AC54 File Offset: 0x00578E54
		public MiscShaderData(Asset<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x0057ACA0 File Offset: 0x00578EA0
		public virtual void Apply(DrawData? drawData = null)
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
			EffectParameter effectParameter6 = base.Shader.Parameters["uShaderSpecificData"];
			if (effectParameter6 != null)
			{
				effectParameter6.SetValue(this._shaderSpecificData);
			}
			if (drawData != null)
			{
				DrawData value = drawData.Value;
				Vector4 value2 = Vector4.Zero;
				if (drawData.Value.sourceRect != null)
				{
					value2..ctor((float)value.sourceRect.Value.X, (float)value.sourceRect.Value.Y, (float)value.sourceRect.Value.Width, (float)value.sourceRect.Value.Height);
				}
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
			SamplerState value3 = SamplerState.LinearWrap;
			if (this._customSamplerState != null)
			{
				value3 = this._customSamplerState;
			}
			if (this._uImage0 != null)
			{
				Main.graphics.GraphicsDevice.Textures[0] = this._uImage0.Value;
				Main.graphics.GraphicsDevice.SamplerStates[0] = value3;
				EffectParameter effectParameter11 = base.Shader.Parameters["uImageSize0"];
				if (effectParameter11 != null)
				{
					effectParameter11.SetValue(new Vector2((float)this._uImage0.Width(), (float)this._uImage0.Height()));
				}
			}
			if (this._uImage1 != null)
			{
				Main.graphics.GraphicsDevice.Textures[1] = this._uImage1.Value;
				Main.graphics.GraphicsDevice.SamplerStates[1] = value3;
				EffectParameter effectParameter12 = base.Shader.Parameters["uImageSize1"];
				if (effectParameter12 != null)
				{
					effectParameter12.SetValue(new Vector2((float)this._uImage1.Width(), (float)this._uImage1.Height()));
				}
			}
			if (this._uImage2 != null)
			{
				Main.graphics.GraphicsDevice.Textures[2] = this._uImage2.Value;
				Main.graphics.GraphicsDevice.SamplerStates[2] = value3;
				EffectParameter effectParameter13 = base.Shader.Parameters["uImageSize2"];
				if (effectParameter13 != null)
				{
					effectParameter13.SetValue(new Vector2((float)this._uImage2.Width(), (float)this._uImage2.Height()));
				}
			}
			bool useProjectionMatrix = this._useProjectionMatrix;
			base.Apply();
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x0057B06B File Offset: 0x0057926B
		public MiscShaderData UseColor(float r, float g, float b)
		{
			return this.UseColor(new Vector3(r, g, b));
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x0057B07B File Offset: 0x0057927B
		public MiscShaderData UseColor(Color color)
		{
			return this.UseColor(color.ToVector3());
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x0057B08A File Offset: 0x0057928A
		public MiscShaderData UseColor(Vector3 color)
		{
			this._uColor = color;
			return this;
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x0057B094 File Offset: 0x00579294
		public MiscShaderData UseSamplerState(SamplerState state)
		{
			this._customSamplerState = state;
			return this;
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x0057B09E File Offset: 0x0057929E
		public MiscShaderData UseImage0(string path)
		{
			this._uImage0 = Main.Assets.Request<Texture2D>(path);
			return this;
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x0057B0B2 File Offset: 0x005792B2
		public MiscShaderData UseImage1(string path)
		{
			this._uImage1 = Main.Assets.Request<Texture2D>(path);
			return this;
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x0057B0C6 File Offset: 0x005792C6
		public MiscShaderData UseImage2(string path)
		{
			this._uImage2 = Main.Assets.Request<Texture2D>(path);
			return this;
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x0057B0DA File Offset: 0x005792DA
		public MiscShaderData UseImage0(Asset<Texture2D> asset)
		{
			this._uImage0 = asset;
			return this;
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x0057B0E4 File Offset: 0x005792E4
		public MiscShaderData UseImage1(Asset<Texture2D> asset)
		{
			this._uImage1 = asset;
			return this;
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x0057B0EE File Offset: 0x005792EE
		public MiscShaderData UseImage2(Asset<Texture2D> asset)
		{
			this._uImage2 = asset;
			return this;
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x0057B0F8 File Offset: 0x005792F8
		private static bool IsPowerOfTwo(int n)
		{
			return (int)Math.Ceiling(Math.Log((double)n) / Math.Log(2.0)) == (int)Math.Floor(Math.Log((double)n) / Math.Log(2.0));
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x0057B134 File Offset: 0x00579334
		public MiscShaderData UseOpacity(float alpha)
		{
			this._uOpacity = alpha;
			return this;
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x0057B13E File Offset: 0x0057933E
		public MiscShaderData UseSecondaryColor(float r, float g, float b)
		{
			return this.UseSecondaryColor(new Vector3(r, g, b));
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x0057B14E File Offset: 0x0057934E
		public MiscShaderData UseSecondaryColor(Color color)
		{
			return this.UseSecondaryColor(color.ToVector3());
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x0057B15D File Offset: 0x0057935D
		public MiscShaderData UseSecondaryColor(Vector3 color)
		{
			this._uSecondaryColor = color;
			return this;
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x0057B167 File Offset: 0x00579367
		public MiscShaderData UseProjectionMatrix(bool doUse)
		{
			this._useProjectionMatrix = doUse;
			return this;
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x0057B171 File Offset: 0x00579371
		public MiscShaderData UseSaturation(float saturation)
		{
			this._uSaturation = saturation;
			return this;
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x0057B17B File Offset: 0x0057937B
		public virtual MiscShaderData GetSecondaryShader(Entity entity)
		{
			return this;
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x0057B17E File Offset: 0x0057937E
		public MiscShaderData UseShaderSpecificData(Vector4 specificData)
		{
			this._shaderSpecificData = specificData;
			return this;
		}

		// Token: 0x04005010 RID: 20496
		private Vector3 _uColor = Vector3.One;

		// Token: 0x04005011 RID: 20497
		private Vector3 _uSecondaryColor = Vector3.One;

		// Token: 0x04005012 RID: 20498
		private float _uSaturation = 1f;

		// Token: 0x04005013 RID: 20499
		private float _uOpacity = 1f;

		// Token: 0x04005014 RID: 20500
		private Asset<Texture2D> _uImage0;

		// Token: 0x04005015 RID: 20501
		private Asset<Texture2D> _uImage1;

		// Token: 0x04005016 RID: 20502
		private Asset<Texture2D> _uImage2;

		// Token: 0x04005017 RID: 20503
		private bool _useProjectionMatrix;

		// Token: 0x04005018 RID: 20504
		private Vector4 _shaderSpecificData = Vector4.Zero;

		// Token: 0x04005019 RID: 20505
		private SamplerState _customSamplerState;
	}
}
