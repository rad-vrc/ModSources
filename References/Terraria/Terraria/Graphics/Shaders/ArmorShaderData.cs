using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x02000103 RID: 259
	public class ArmorShaderData : ShaderData
	{
		// Token: 0x06001685 RID: 5765 RVA: 0x004C8DBC File Offset: 0x004C6FBC
		public ArmorShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x004C8E08 File Offset: 0x004C7008
		public virtual void Apply(Entity entity, DrawData? drawData = null)
		{
			base.Shader.Parameters["uColor"].SetValue(this._uColor);
			base.Shader.Parameters["uSaturation"].SetValue(this._uSaturation);
			base.Shader.Parameters["uSecondaryColor"].SetValue(this._uSecondaryColor);
			base.Shader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly);
			base.Shader.Parameters["uOpacity"].SetValue(this._uOpacity);
			base.Shader.Parameters["uTargetPosition"].SetValue(this._uTargetPosition);
			if (drawData != null)
			{
				DrawData value = drawData.Value;
				Vector4 value2;
				if (value.sourceRect != null)
				{
					value2 = new Vector4((float)value.sourceRect.Value.X, (float)value.sourceRect.Value.Y, (float)value.sourceRect.Value.Width, (float)value.sourceRect.Value.Height);
				}
				else
				{
					value2 = new Vector4(0f, 0f, (float)value.texture.Width, (float)value.texture.Height);
				}
				base.Shader.Parameters["uSourceRect"].SetValue(value2);
				base.Shader.Parameters["uLegacyArmorSourceRect"].SetValue(value2);
				base.Shader.Parameters["uWorldPosition"].SetValue(Main.screenPosition + value.position);
				base.Shader.Parameters["uImageSize0"].SetValue(new Vector2((float)value.texture.Width, (float)value.texture.Height));
				base.Shader.Parameters["uLegacyArmorSheetSize"].SetValue(new Vector2((float)value.texture.Width, (float)value.texture.Height));
				base.Shader.Parameters["uRotation"].SetValue(value.rotation * (value.effect.HasFlag(SpriteEffects.FlipHorizontally) ? -1f : 1f));
				base.Shader.Parameters["uDirection"].SetValue(value.effect.HasFlag(SpriteEffects.FlipHorizontally) ? -1 : 1);
			}
			else
			{
				Vector4 value3 = new Vector4(0f, 0f, 4f, 4f);
				base.Shader.Parameters["uSourceRect"].SetValue(value3);
				base.Shader.Parameters["uLegacyArmorSourceRect"].SetValue(value3);
				base.Shader.Parameters["uRotation"].SetValue(0f);
			}
			if (this._uImage != null)
			{
				Main.graphics.GraphicsDevice.Textures[1] = this._uImage.Value;
				base.Shader.Parameters["uImageSize1"].SetValue(new Vector2((float)this._uImage.Width(), (float)this._uImage.Height()));
			}
			if (entity != null)
			{
				base.Shader.Parameters["uDirection"].SetValue((float)entity.direction);
			}
			Player player = entity as Player;
			if (player != null)
			{
				Rectangle bodyFrame = player.bodyFrame;
				base.Shader.Parameters["uLegacyArmorSourceRect"].SetValue(new Vector4((float)bodyFrame.X, (float)bodyFrame.Y, (float)bodyFrame.Width, (float)bodyFrame.Height));
				base.Shader.Parameters["uLegacyArmorSheetSize"].SetValue(new Vector2(40f, 1120f));
			}
			this.Apply();
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x004C922E File Offset: 0x004C742E
		public ArmorShaderData UseColor(float r, float g, float b)
		{
			return this.UseColor(new Vector3(r, g, b));
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x004C923E File Offset: 0x004C743E
		public ArmorShaderData UseColor(Color color)
		{
			return this.UseColor(color.ToVector3());
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x004C924D File Offset: 0x004C744D
		public ArmorShaderData UseColor(Vector3 color)
		{
			this._uColor = color;
			return this;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x004C9257 File Offset: 0x004C7457
		public ArmorShaderData UseImage(string path)
		{
			this._uImage = Main.Assets.Request<Texture2D>(path, 1);
			return this;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x004C926C File Offset: 0x004C746C
		public ArmorShaderData UseOpacity(float alpha)
		{
			this._uOpacity = alpha;
			return this;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x004C9276 File Offset: 0x004C7476
		public ArmorShaderData UseTargetPosition(Vector2 position)
		{
			this._uTargetPosition = position;
			return this;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x004C9280 File Offset: 0x004C7480
		public ArmorShaderData UseSecondaryColor(float r, float g, float b)
		{
			return this.UseSecondaryColor(new Vector3(r, g, b));
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x004C9290 File Offset: 0x004C7490
		public ArmorShaderData UseSecondaryColor(Color color)
		{
			return this.UseSecondaryColor(color.ToVector3());
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x004C929F File Offset: 0x004C749F
		public ArmorShaderData UseSecondaryColor(Vector3 color)
		{
			this._uSecondaryColor = color;
			return this;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x004C92A9 File Offset: 0x004C74A9
		public ArmorShaderData UseSaturation(float saturation)
		{
			this._uSaturation = saturation;
			return this;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x004C8442 File Offset: 0x004C6642
		public virtual ArmorShaderData GetSecondaryShader(Entity entity)
		{
			return this;
		}

		// Token: 0x04001362 RID: 4962
		private Vector3 _uColor = Vector3.One;

		// Token: 0x04001363 RID: 4963
		private Vector3 _uSecondaryColor = Vector3.One;

		// Token: 0x04001364 RID: 4964
		private float _uSaturation = 1f;

		// Token: 0x04001365 RID: 4965
		private float _uOpacity = 1f;

		// Token: 0x04001366 RID: 4966
		private Asset<Texture2D> _uImage;

		// Token: 0x04001367 RID: 4967
		private Vector2 _uTargetPosition = Vector2.One;
	}
}
