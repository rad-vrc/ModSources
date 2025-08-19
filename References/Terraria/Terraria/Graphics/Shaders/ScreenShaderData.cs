using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x02000102 RID: 258
	public class ScreenShaderData : ShaderData
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x004C87B9 File Offset: 0x004C69B9
		public float Intensity
		{
			get
			{
				return this._uIntensity;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x004C87C1 File Offset: 0x004C69C1
		public float CombinedOpacity
		{
			get
			{
				return this._uOpacity * this._globalOpacity;
			}
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x004C87D0 File Offset: 0x004C69D0
		public ScreenShaderData(string passName) : base(Main.ScreenShaderRef, passName)
		{
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x004C88A0 File Offset: 0x004C6AA0
		public ScreenShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void Update(GameTime gameTime)
		{
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x004C896C File Offset: 0x004C6B6C
		public override void Apply()
		{
			Vector2 value = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			Vector2 value2 = new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / Main.GameViewMatrix.Zoom;
			Vector2 value3 = new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f;
			Vector2 value4 = Main.screenPosition + value3 * (Vector2.One - Vector2.One / Main.GameViewMatrix.Zoom);
			base.Shader.Parameters["uColor"].SetValue(this._uColor);
			base.Shader.Parameters["uOpacity"].SetValue(this.CombinedOpacity);
			base.Shader.Parameters["uSecondaryColor"].SetValue(this._uSecondaryColor);
			base.Shader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly);
			base.Shader.Parameters["uScreenResolution"].SetValue(value2);
			base.Shader.Parameters["uScreenPosition"].SetValue(value4 - value);
			base.Shader.Parameters["uTargetPosition"].SetValue(this._uTargetPosition - value);
			base.Shader.Parameters["uImageOffset"].SetValue(this._uImageOffset);
			base.Shader.Parameters["uIntensity"].SetValue(this._uIntensity);
			base.Shader.Parameters["uProgress"].SetValue(this._uProgress);
			base.Shader.Parameters["uDirection"].SetValue(this._uDirection);
			base.Shader.Parameters["uZoom"].SetValue(Main.GameViewMatrix.Zoom);
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
					base.Shader.Parameters["uImageSize" + (i + 1)].SetValue(new Vector2((float)width, (float)height) * this._imageScales[i]);
				}
			}
			base.Apply();
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x004C8CCA File Offset: 0x004C6ECA
		public ScreenShaderData UseImageOffset(Vector2 offset)
		{
			this._uImageOffset = offset;
			return this;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x004C8CD4 File Offset: 0x004C6ED4
		public ScreenShaderData UseIntensity(float intensity)
		{
			this._uIntensity = intensity;
			return this;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x004C8CDE File Offset: 0x004C6EDE
		public ScreenShaderData UseColor(float r, float g, float b)
		{
			return this.UseColor(new Vector3(r, g, b));
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x004C8CEE File Offset: 0x004C6EEE
		public ScreenShaderData UseProgress(float progress)
		{
			this._uProgress = progress;
			return this;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x004C8CF8 File Offset: 0x004C6EF8
		public ScreenShaderData UseImage(Texture2D image, int index = 0, SamplerState samplerState = null)
		{
			this._samplerStates[index] = samplerState;
			this._uAssetImages[index] = null;
			this._uCustomImages[index] = image;
			return this;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x004C8D16 File Offset: 0x004C6F16
		public ScreenShaderData UseImage(string path, int index = 0, SamplerState samplerState = null)
		{
			this._uAssetImages[index] = Main.Assets.Request<Texture2D>(path, 1);
			this._uCustomImages[index] = null;
			this._samplerStates[index] = samplerState;
			return this;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x004C8D3F File Offset: 0x004C6F3F
		public ScreenShaderData UseColor(Color color)
		{
			return this.UseColor(color.ToVector3());
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x004C8D4E File Offset: 0x004C6F4E
		public ScreenShaderData UseColor(Vector3 color)
		{
			this._uColor = color;
			return this;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x004C8D58 File Offset: 0x004C6F58
		public ScreenShaderData UseDirection(Vector2 direction)
		{
			this._uDirection = direction;
			return this;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x004C8D62 File Offset: 0x004C6F62
		public ScreenShaderData UseGlobalOpacity(float opacity)
		{
			this._globalOpacity = opacity;
			return this;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x004C8D6C File Offset: 0x004C6F6C
		public ScreenShaderData UseTargetPosition(Vector2 position)
		{
			this._uTargetPosition = position;
			return this;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x004C8D76 File Offset: 0x004C6F76
		public ScreenShaderData UseSecondaryColor(float r, float g, float b)
		{
			return this.UseSecondaryColor(new Vector3(r, g, b));
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x004C8D86 File Offset: 0x004C6F86
		public ScreenShaderData UseSecondaryColor(Color color)
		{
			return this.UseSecondaryColor(color.ToVector3());
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x004C8D95 File Offset: 0x004C6F95
		public ScreenShaderData UseSecondaryColor(Vector3 color)
		{
			this._uSecondaryColor = color;
			return this;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x004C8D9F File Offset: 0x004C6F9F
		public ScreenShaderData UseOpacity(float opacity)
		{
			this._uOpacity = opacity;
			return this;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x004C8DA9 File Offset: 0x004C6FA9
		public ScreenShaderData UseImageScale(Vector2 scale, int index = 0)
		{
			this._imageScales[index] = scale;
			return this;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x004C8442 File Offset: 0x004C6642
		public virtual ScreenShaderData GetSecondaryShader(Player player)
		{
			return this;
		}

		// Token: 0x04001355 RID: 4949
		private Vector3 _uColor = Vector3.One;

		// Token: 0x04001356 RID: 4950
		private Vector3 _uSecondaryColor = Vector3.One;

		// Token: 0x04001357 RID: 4951
		private float _uOpacity = 1f;

		// Token: 0x04001358 RID: 4952
		private float _globalOpacity = 1f;

		// Token: 0x04001359 RID: 4953
		private float _uIntensity = 1f;

		// Token: 0x0400135A RID: 4954
		private Vector2 _uTargetPosition = Vector2.One;

		// Token: 0x0400135B RID: 4955
		private Vector2 _uDirection = new Vector2(0f, 1f);

		// Token: 0x0400135C RID: 4956
		private float _uProgress;

		// Token: 0x0400135D RID: 4957
		private Vector2 _uImageOffset = Vector2.Zero;

		// Token: 0x0400135E RID: 4958
		private Asset<Texture2D>[] _uAssetImages = new Asset<Texture2D>[3];

		// Token: 0x0400135F RID: 4959
		private Texture2D[] _uCustomImages = new Texture2D[3];

		// Token: 0x04001360 RID: 4960
		private SamplerState[] _samplerStates = new SamplerState[3];

		// Token: 0x04001361 RID: 4961
		private Vector2[] _imageScales = new Vector2[]
		{
			Vector2.One,
			Vector2.One,
			Vector2.One
		};
	}
}
