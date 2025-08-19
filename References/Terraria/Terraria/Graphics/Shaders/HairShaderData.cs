using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x02000101 RID: 257
	public class HairShaderData : ShaderData
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x004C844F File Offset: 0x004C664F
		public bool ShaderDisabled
		{
			get
			{
				return this._shaderDisabled;
			}
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x004C8458 File Offset: 0x004C6658
		public HairShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
		{
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x004C84A4 File Offset: 0x004C66A4
		public virtual void Apply(Player player, DrawData? drawData = null)
		{
			if (this._shaderDisabled)
			{
				return;
			}
			base.Shader.Parameters["uColor"].SetValue(this._uColor);
			base.Shader.Parameters["uSaturation"].SetValue(this._uSaturation);
			base.Shader.Parameters["uSecondaryColor"].SetValue(this._uSecondaryColor);
			base.Shader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly);
			base.Shader.Parameters["uOpacity"].SetValue(this._uOpacity);
			base.Shader.Parameters["uTargetPosition"].SetValue(this._uTargetPosition);
			if (drawData != null)
			{
				DrawData value = drawData.Value;
				Vector4 value2 = new Vector4((float)value.sourceRect.Value.X, (float)value.sourceRect.Value.Y, (float)value.sourceRect.Value.Width, (float)value.sourceRect.Value.Height);
				base.Shader.Parameters["uSourceRect"].SetValue(value2);
				base.Shader.Parameters["uWorldPosition"].SetValue(Main.screenPosition + value.position);
				base.Shader.Parameters["uImageSize0"].SetValue(new Vector2((float)value.texture.Width, (float)value.texture.Height));
			}
			else
			{
				base.Shader.Parameters["uSourceRect"].SetValue(new Vector4(0f, 0f, 4f, 4f));
			}
			if (this._uImage != null)
			{
				Main.graphics.GraphicsDevice.Textures[1] = this._uImage.Value;
				base.Shader.Parameters["uImageSize1"].SetValue(new Vector2((float)this._uImage.Width(), (float)this._uImage.Height()));
			}
			if (player != null)
			{
				base.Shader.Parameters["uDirection"].SetValue((float)player.direction);
			}
			this.Apply();
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x004C8716 File Offset: 0x004C6916
		public virtual Color GetColor(Player player, Color lightColor)
		{
			return new Color(lightColor.ToVector4() * player.hairColor.ToVector4());
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x004C8734 File Offset: 0x004C6934
		public HairShaderData UseColor(float r, float g, float b)
		{
			return this.UseColor(new Vector3(r, g, b));
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x004C8744 File Offset: 0x004C6944
		public HairShaderData UseColor(Color color)
		{
			return this.UseColor(color.ToVector3());
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x004C8753 File Offset: 0x004C6953
		public HairShaderData UseColor(Vector3 color)
		{
			this._uColor = color;
			return this;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x004C875D File Offset: 0x004C695D
		public HairShaderData UseImage(string path)
		{
			this._uImage = Main.Assets.Request<Texture2D>(path, 1);
			return this;
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x004C8772 File Offset: 0x004C6972
		public HairShaderData UseOpacity(float alpha)
		{
			this._uOpacity = alpha;
			return this;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x004C877C File Offset: 0x004C697C
		public HairShaderData UseSecondaryColor(float r, float g, float b)
		{
			return this.UseSecondaryColor(new Vector3(r, g, b));
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x004C878C File Offset: 0x004C698C
		public HairShaderData UseSecondaryColor(Color color)
		{
			return this.UseSecondaryColor(color.ToVector3());
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x004C879B File Offset: 0x004C699B
		public HairShaderData UseSecondaryColor(Vector3 color)
		{
			this._uSecondaryColor = color;
			return this;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x004C87A5 File Offset: 0x004C69A5
		public HairShaderData UseSaturation(float saturation)
		{
			this._uSaturation = saturation;
			return this;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x004C87AF File Offset: 0x004C69AF
		public HairShaderData UseTargetPosition(Vector2 position)
		{
			this._uTargetPosition = position;
			return this;
		}

		// Token: 0x0400134E RID: 4942
		protected Vector3 _uColor = Vector3.One;

		// Token: 0x0400134F RID: 4943
		protected Vector3 _uSecondaryColor = Vector3.One;

		// Token: 0x04001350 RID: 4944
		protected float _uSaturation = 1f;

		// Token: 0x04001351 RID: 4945
		protected float _uOpacity = 1f;

		// Token: 0x04001352 RID: 4946
		protected Asset<Texture2D> _uImage;

		// Token: 0x04001353 RID: 4947
		protected bool _shaderDisabled;

		// Token: 0x04001354 RID: 4948
		private Vector2 _uTargetPosition = Vector2.One;
	}
}
