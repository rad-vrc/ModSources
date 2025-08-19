using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x0200044A RID: 1098
	public class ScreenShaderData : ShaderData
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x0057B188 File Offset: 0x00579388
		public float Intensity
		{
			get
			{
				return this._uIntensity;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06003638 RID: 13880 RVA: 0x0057B190 File Offset: 0x00579390
		public float CombinedOpacity
		{
			get
			{
				return this._uOpacity * this._globalOpacity;
			}
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x0057B19F File Offset: 0x0057939F
		public ScreenShaderData(string passName) : this(Main.ScreenShaderRef, passName)
		{
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x0057B1B0 File Offset: 0x005793B0
		[Obsolete("Removed in 1.4.5. Use Asset<Effect> version instead. Asset version works with AsyncLoad")]
		public ScreenShaderData(Ref<Effect> shader, string passName)
		{
			this._uColor = Vector3.One;
			this._uSecondaryColor = Vector3.One;
			this._uOpacity = 1f;
			this._globalOpacity = 1f;
			this._uIntensity = 1f;
			this._uTargetPosition = Vector2.One;
			this._uDirection = new Vector2(0f, 1f);
			this._uImageOffset = Vector2.Zero;
			this._uAssetImages = new Asset<Texture2D>[3];
			this._uCustomImages = new Texture2D[3];
			this._samplerStates = new SamplerState[3];
			this._imageScales = new Vector2[]
			{
				Vector2.One,
				Vector2.One,
				Vector2.One
			};
			base..ctor(shader, passName);
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x0057B27C File Offset: 0x0057947C
		public ScreenShaderData(Asset<Effect> shader, string passName)
		{
			this._uColor = Vector3.One;
			this._uSecondaryColor = Vector3.One;
			this._uOpacity = 1f;
			this._globalOpacity = 1f;
			this._uIntensity = 1f;
			this._uTargetPosition = Vector2.One;
			this._uDirection = new Vector2(0f, 1f);
			this._uImageOffset = Vector2.Zero;
			this._uAssetImages = new Asset<Texture2D>[3];
			this._uCustomImages = new Texture2D[3];
			this._samplerStates = new SamplerState[3];
			this._imageScales = new Vector2[]
			{
				Vector2.One,
				Vector2.One,
				Vector2.One
			};
			base..ctor(shader, passName);
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x0057B347 File Offset: 0x00579547
		public virtual void Update(GameTime gameTime)
		{
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x0057B34C File Offset: 0x0057954C
		public override void Apply()
		{
			Vector2 vector;
			vector..ctor((float)Main.offScreenRange, (float)Main.offScreenRange);
			Vector2 value = new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / Main.GameViewMatrix.Zoom;
			Vector2 vector2 = new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f;
			Vector2 vector3 = Main.screenPosition + vector2 * (Vector2.One - Vector2.One / Main.GameViewMatrix.Zoom);
			EffectParameter effectParameter = base.Shader.Parameters["uColor"];
			if (effectParameter != null)
			{
				effectParameter.SetValue(this._uColor);
			}
			EffectParameter effectParameter2 = base.Shader.Parameters["uOpacity"];
			if (effectParameter2 != null)
			{
				effectParameter2.SetValue(this.CombinedOpacity);
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
			EffectParameter effectParameter5 = base.Shader.Parameters["uScreenResolution"];
			if (effectParameter5 != null)
			{
				effectParameter5.SetValue(value);
			}
			EffectParameter effectParameter6 = base.Shader.Parameters["uScreenPosition"];
			if (effectParameter6 != null)
			{
				effectParameter6.SetValue(vector3 - vector);
			}
			EffectParameter effectParameter7 = base.Shader.Parameters["uTargetPosition"];
			if (effectParameter7 != null)
			{
				effectParameter7.SetValue(this._uTargetPosition - vector);
			}
			EffectParameter effectParameter8 = base.Shader.Parameters["uImageOffset"];
			if (effectParameter8 != null)
			{
				effectParameter8.SetValue(this._uImageOffset);
			}
			EffectParameter effectParameter9 = base.Shader.Parameters["uIntensity"];
			if (effectParameter9 != null)
			{
				effectParameter9.SetValue(this._uIntensity);
			}
			EffectParameter effectParameter10 = base.Shader.Parameters["uProgress"];
			if (effectParameter10 != null)
			{
				effectParameter10.SetValue(this._uProgress);
			}
			EffectParameter effectParameter11 = base.Shader.Parameters["uDirection"];
			if (effectParameter11 != null)
			{
				effectParameter11.SetValue(this._uDirection);
			}
			EffectParameter effectParameter12 = base.Shader.Parameters["uZoom"];
			if (effectParameter12 != null)
			{
				effectParameter12.SetValue(Main.GameViewMatrix.Zoom);
			}
			for (int i = 0; i < this._uAssetImages.Length; i++)
			{
				Texture2D texture2D = this._uCustomImages[i];
				if (this._uAssetImages[i] != null && this._uAssetImages[i].IsLoaded)
				{
					texture2D = this._uAssetImages[i].Value;
				}
				if (texture2D != null)
				{
					Main.graphics.GraphicsDevice.Textures[i + 1] = texture2D;
					int width = texture2D.Width;
					int height = texture2D.Height;
					if (this._samplerStates[i] != null)
					{
						Main.graphics.GraphicsDevice.SamplerStates[i + 1] = this._samplerStates[i];
					}
					else if (Utils.IsPowerOfTwo(width) && Utils.IsPowerOfTwo(height))
					{
						Main.graphics.GraphicsDevice.SamplerStates[i + 1] = SamplerState.LinearWrap;
					}
					else
					{
						Main.graphics.GraphicsDevice.SamplerStates[i + 1] = SamplerState.AnisotropicClamp;
					}
					EffectParameter effectParameter13 = base.Shader.Parameters["uImageSize" + (i + 1).ToString()];
					if (effectParameter13 != null)
					{
						effectParameter13.SetValue(new Vector2((float)width, (float)height) * this._imageScales[i]);
					}
				}
			}
			base.Apply();
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x0057B6FC File Offset: 0x005798FC
		public ScreenShaderData UseImageOffset(Vector2 offset)
		{
			this._uImageOffset = offset;
			return this;
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x0057B706 File Offset: 0x00579906
		public ScreenShaderData UseIntensity(float intensity)
		{
			this._uIntensity = intensity;
			return this;
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x0057B710 File Offset: 0x00579910
		public ScreenShaderData UseColor(float r, float g, float b)
		{
			return this.UseColor(new Vector3(r, g, b));
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x0057B720 File Offset: 0x00579920
		public ScreenShaderData UseProgress(float progress)
		{
			this._uProgress = progress;
			return this;
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x0057B72A File Offset: 0x0057992A
		public ScreenShaderData UseImage(Asset<Texture2D> image, int index = 0, SamplerState samplerState = null)
		{
			this._samplerStates[index] = samplerState;
			this._uAssetImages[index] = image;
			this._uCustomImages[index] = null;
			return this;
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x0057B748 File Offset: 0x00579948
		public ScreenShaderData UseImage(Texture2D image, int index = 0, SamplerState samplerState = null)
		{
			this._samplerStates[index] = samplerState;
			this._uAssetImages[index] = null;
			this._uCustomImages[index] = image;
			return this;
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x0057B766 File Offset: 0x00579966
		public ScreenShaderData UseImage(string path, int index = 0, SamplerState samplerState = null)
		{
			this._uAssetImages[index] = Main.Assets.Request<Texture2D>(path);
			this._uCustomImages[index] = null;
			this._samplerStates[index] = samplerState;
			return this;
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x0057B78E File Offset: 0x0057998E
		public ScreenShaderData UseColor(Color color)
		{
			return this.UseColor(color.ToVector3());
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x0057B79D File Offset: 0x0057999D
		public ScreenShaderData UseColor(Vector3 color)
		{
			this._uColor = color;
			return this;
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x0057B7A7 File Offset: 0x005799A7
		public ScreenShaderData UseDirection(Vector2 direction)
		{
			this._uDirection = direction;
			return this;
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x0057B7B1 File Offset: 0x005799B1
		public ScreenShaderData UseGlobalOpacity(float opacity)
		{
			this._globalOpacity = opacity;
			return this;
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x0057B7BB File Offset: 0x005799BB
		public ScreenShaderData UseTargetPosition(Vector2 position)
		{
			this._uTargetPosition = position;
			return this;
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x0057B7C5 File Offset: 0x005799C5
		public ScreenShaderData UseSecondaryColor(float r, float g, float b)
		{
			return this.UseSecondaryColor(new Vector3(r, g, b));
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x0057B7D5 File Offset: 0x005799D5
		public ScreenShaderData UseSecondaryColor(Color color)
		{
			return this.UseSecondaryColor(color.ToVector3());
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x0057B7E4 File Offset: 0x005799E4
		public ScreenShaderData UseSecondaryColor(Vector3 color)
		{
			this._uSecondaryColor = color;
			return this;
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x0057B7EE File Offset: 0x005799EE
		public ScreenShaderData UseOpacity(float opacity)
		{
			this._uOpacity = opacity;
			return this;
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x0057B7F8 File Offset: 0x005799F8
		public ScreenShaderData UseImageScale(Vector2 scale, int index = 0)
		{
			this._imageScales[index] = scale;
			return this;
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x0057B808 File Offset: 0x00579A08
		public virtual ScreenShaderData GetSecondaryShader(Player player)
		{
			return this;
		}

		// Token: 0x0400501A RID: 20506
		private Vector3 _uColor;

		// Token: 0x0400501B RID: 20507
		private Vector3 _uSecondaryColor;

		// Token: 0x0400501C RID: 20508
		private float _uOpacity;

		// Token: 0x0400501D RID: 20509
		private float _globalOpacity;

		// Token: 0x0400501E RID: 20510
		private float _uIntensity;

		// Token: 0x0400501F RID: 20511
		private Vector2 _uTargetPosition;

		// Token: 0x04005020 RID: 20512
		private Vector2 _uDirection;

		// Token: 0x04005021 RID: 20513
		private float _uProgress;

		// Token: 0x04005022 RID: 20514
		private Vector2 _uImageOffset;

		// Token: 0x04005023 RID: 20515
		private Asset<Texture2D>[] _uAssetImages;

		// Token: 0x04005024 RID: 20516
		private Texture2D[] _uCustomImages;

		// Token: 0x04005025 RID: 20517
		private SamplerState[] _samplerStates;

		// Token: 0x04005026 RID: 20518
		private Vector2[] _imageScales;
	}
}
