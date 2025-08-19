using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace QoLCompendium.Core.UI.Buttons
{
	// Token: 0x02000299 RID: 665
	public class PermanentUpgradedBuffButton : CustomUIImageButton
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00086D00 File Offset: 0x00084F00
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x00086D08 File Offset: 0x00084F08
		public LocalizedText Tooltip { get; set; }

		// Token: 0x06001137 RID: 4407 RVA: 0x00086D11 File Offset: 0x00084F11
		public PermanentUpgradedBuffButton(Asset<Texture2D> faceTexture) : base(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentUpgradedBuff", 2))
		{
			this.faceTexture = faceTexture;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00086D2B File Offset: 0x00084F2B
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			this.UpdateDraw(spriteBatch);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00086D3B File Offset: 0x00084F3B
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.UpdateDraw(spriteBatch);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00086D4C File Offset: 0x00084F4C
		private void UpdateDraw(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this.faceTexture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
			if (base.IsMouseHovering)
			{
				Main.hoverItemName = this.Tooltip.Value;
				Main.mouseText = true;
			}
		}

		// Token: 0x04000754 RID: 1876
		private readonly Asset<Texture2D> faceTexture;
	}
}
