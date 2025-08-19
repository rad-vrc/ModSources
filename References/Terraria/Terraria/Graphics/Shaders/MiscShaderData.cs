using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x02000100 RID: 256
	public class MiscShaderData : ShaderData
	{
		// Token: 0x0600164E RID: 5710 RVA: 0x004C7F84 File Offset: 0x004C6184
		public MiscShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x004C7FD0 File Offset: 0x004C61D0
		public virtual void Apply(DrawData? drawData = null)
		{
			base.Shader.Parameters["uColor"].SetValue(this._uColor);
			base.Shader.Parameters["uSaturation"].SetValue(this._uSaturation);
			base.Shader.Parameters["uSecondaryColor"].SetValue(this._uSecondaryColor);
			base.Shader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly);
			base.Shader.Parameters["uOpacity"].SetValue(this._uOpacity);
			base.Shader.Parameters["uShaderSpecificData"].SetValue(this._shaderSpecificData);
			if (drawData != null)
			{
				DrawData value = drawData.Value;
				Vector4 zero = Vector4.Zero;
				if (drawData.Value.sourceRect != null)
				{
					zero = new Vector4((float)value.sourceRect.Value.X, (float)value.sourceRect.Value.Y, (float)value.sourceRect.Value.Width, (float)value.sourceRect.Value.Height);
				}
				base.Shader.Parameters["uSourceRect"].SetValue(zero);
				base.Shader.Parameters["uWorldPosition"].SetValue(Main.screenPosition + value.position);
				base.Shader.Parameters["uImageSize0"].SetValue(new Vector2((float)value.texture.Width, (float)value.texture.Height));
			}
			else
			{
				base.Shader.Parameters["uSourceRect"].SetValue(new Vector4(0f, 0f, 4f, 4f));
			}
			SamplerState value2 = SamplerState.LinearWrap;
			if (this._customSamplerState != null)
			{
				value2 = this._customSamplerState;
			}
			if (this._uImage0 != null)
			{
				Main.graphics.GraphicsDevice.Textures[0] = this._uImage0.Value;
				Main.graphics.GraphicsDevice.SamplerStates[0] = value2;
				base.Shader.Parameters["uImageSize0"].SetValue(new Vector2((float)this._uImage0.Width(), (float)this._uImage0.Height()));
			}
			if (this._uImage1 != null)
			{
				Main.graphics.GraphicsDevice.Textures[1] = this._uImage1.Value;
				Main.graphics.GraphicsDevice.SamplerStates[1] = value2;
				base.Shader.Parameters["uImageSize1"].SetValue(new Vector2((float)this._uImage1.Width(), (float)this._uImage1.Height()));
			}
			if (this._uImage2 != null)
			{
				Main.graphics.GraphicsDevice.Textures[2] = this._uImage2.Value;
				Main.graphics.GraphicsDevice.SamplerStates[2] = value2;
				base.Shader.Parameters["uImageSize2"].SetValue(new Vector2((float)this._uImage2.Width(), (float)this._uImage2.Height()));
			}
			bool useProjectionMatrix = this._useProjectionMatrix;
			base.Apply();
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x004C834D File Offset: 0x004C654D
		public MiscShaderData UseColor(float r, float g, float b)
		{
			return this.UseColor(new Vector3(r, g, b));
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x004C835D File Offset: 0x004C655D
		public MiscShaderData UseColor(Color color)
		{
			return this.UseColor(color.ToVector3());
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x004C836C File Offset: 0x004C656C
		public MiscShaderData UseColor(Vector3 color)
		{
			this._uColor = color;
			return this;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x004C8376 File Offset: 0x004C6576
		public MiscShaderData UseSamplerState(SamplerState state)
		{
			this._customSamplerState = state;
			return this;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x004C8380 File Offset: 0x004C6580
		public MiscShaderData UseImage0(string path)
		{
			this._uImage0 = Main.Assets.Request<Texture2D>(path, 1);
			return this;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x004C8395 File Offset: 0x004C6595
		public MiscShaderData UseImage1(string path)
		{
			this._uImage1 = Main.Assets.Request<Texture2D>(path, 1);
			return this;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x004C83AA File Offset: 0x004C65AA
		public MiscShaderData UseImage2(string path)
		{
			this._uImage2 = Main.Assets.Request<Texture2D>(path, 1);
			return this;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x004C83BF File Offset: 0x004C65BF
		private static bool IsPowerOfTwo(int n)
		{
			return (int)Math.Ceiling(Math.Log((double)n) / Math.Log(2.0)) == (int)Math.Floor(Math.Log((double)n) / Math.Log(2.0));
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x004C83FB File Offset: 0x004C65FB
		public MiscShaderData UseOpacity(float alpha)
		{
			this._uOpacity = alpha;
			return this;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x004C8405 File Offset: 0x004C6605
		public MiscShaderData UseSecondaryColor(float r, float g, float b)
		{
			return this.UseSecondaryColor(new Vector3(r, g, b));
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x004C8415 File Offset: 0x004C6615
		public MiscShaderData UseSecondaryColor(Color color)
		{
			return this.UseSecondaryColor(color.ToVector3());
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x004C8424 File Offset: 0x004C6624
		public MiscShaderData UseSecondaryColor(Vector3 color)
		{
			this._uSecondaryColor = color;
			return this;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x004C842E File Offset: 0x004C662E
		public MiscShaderData UseProjectionMatrix(bool doUse)
		{
			this._useProjectionMatrix = doUse;
			return this;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x004C8438 File Offset: 0x004C6638
		public MiscShaderData UseSaturation(float saturation)
		{
			this._uSaturation = saturation;
			return this;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x004C8442 File Offset: 0x004C6642
		public virtual MiscShaderData GetSecondaryShader(Entity entity)
		{
			return this;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x004C8445 File Offset: 0x004C6645
		public MiscShaderData UseShaderSpecificData(Vector4 specificData)
		{
			this._shaderSpecificData = specificData;
			return this;
		}

		// Token: 0x04001344 RID: 4932
		private Vector3 _uColor = Vector3.One;

		// Token: 0x04001345 RID: 4933
		private Vector3 _uSecondaryColor = Vector3.One;

		// Token: 0x04001346 RID: 4934
		private float _uSaturation = 1f;

		// Token: 0x04001347 RID: 4935
		private float _uOpacity = 1f;

		// Token: 0x04001348 RID: 4936
		private Asset<Texture2D> _uImage0;

		// Token: 0x04001349 RID: 4937
		private Asset<Texture2D> _uImage1;

		// Token: 0x0400134A RID: 4938
		private Asset<Texture2D> _uImage2;

		// Token: 0x0400134B RID: 4939
		private bool _useProjectionMatrix;

		// Token: 0x0400134C RID: 4940
		private Vector4 _shaderSpecificData = Vector4.Zero;

		// Token: 0x0400134D RID: 4941
		private SamplerState _customSamplerState;
	}
}
