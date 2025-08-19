using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x02000394 RID: 916
	internal class BooleanElement : ConfigElement<bool>
	{
		// Token: 0x0600316A RID: 12650 RVA: 0x0053F920 File Offset: 0x0053DB20
		public override void OnBind()
		{
			base.OnBind();
			this._toggleTexture = Main.Assets.Request<Texture2D>("Images/UI/Settings_Toggle");
			base.OnLeftClick += delegate(UIMouseEvent ev, UIElement v)
			{
				this.Value = !this.Value;
			};
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x0053F950 File Offset: 0x0053DB50
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, this.Value ? Lang.menu[126].Value : Lang.menu[124].Value, new Vector2(dimensions.X + dimensions.Width - 60f, dimensions.Y + 8f), Color.White, 0f, Vector2.Zero, new Vector2(0.8f), -1f, 2f);
			Rectangle sourceRectangle;
			sourceRectangle..ctor(this.Value ? ((this._toggleTexture.Width() - 2) / 2 + 2) : 0, 0, (this._toggleTexture.Width() - 2) / 2, this._toggleTexture.Height());
			Vector2 drawPosition;
			drawPosition..ctor(dimensions.X + dimensions.Width - (float)sourceRectangle.Width - 10f, dimensions.Y + 8f);
			spriteBatch.Draw(this._toggleTexture.Value, drawPosition, new Rectangle?(sourceRectangle), Color.White, 0f, Vector2.Zero, Vector2.One, 0, 0f);
		}

		// Token: 0x04001D44 RID: 7492
		private Asset<Texture2D> _toggleTexture;
	}
}
