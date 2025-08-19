using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

namespace QoLCompendium.Core.UI
{
	// Token: 0x02000269 RID: 617
	public class CustomUIImageButton : SafeUIElement
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x00073774 File Offset: 0x00071974
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x0007377C File Offset: 0x0007197C
		public float VisibilityActive { get; private set; } = 1f;

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00073785 File Offset: 0x00071985
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x0007378D File Offset: 0x0007198D
		public float VisibilityInactive { get; private set; } = 1f;

		// Token: 0x06000E56 RID: 3670 RVA: 0x00073798 File Offset: 0x00071998
		public CustomUIImageButton(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00073800 File Offset: 0x00071A00
		public void SetHoverImage(Asset<Texture2D> texture)
		{
			this._borderTexture = texture;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0007380C File Offset: 0x00071A0C
		public void SetImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00073858 File Offset: 0x00071A58
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this._texture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? this.VisibilityActive : this.VisibilityInactive));
			if (this._borderTexture == null || !base.IsMouseHovering)
			{
				return;
			}
			spriteBatch.Draw(this._borderTexture.Value, dimensions.Position(), Color.White);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x000738D4 File Offset: 0x00071AD4
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(SoundID.MenuTick, null, null);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x000738FD File Offset: 0x00071AFD
		public override void SafeUpdate(GameTime gameTime)
		{
			if (this.ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00073917 File Offset: 0x00071B17
		public void SetVisibility(float whenActive, float whenInactive)
		{
			this.VisibilityActive = MathHelper.Clamp(whenActive, 0f, 1f);
			this.VisibilityInactive = MathHelper.Clamp(whenInactive, 0f, 1f);
		}

		// Token: 0x040005C5 RID: 1477
		private Asset<Texture2D> _texture;

		// Token: 0x040005C6 RID: 1478
		private Asset<Texture2D> _borderTexture;
	}
}
