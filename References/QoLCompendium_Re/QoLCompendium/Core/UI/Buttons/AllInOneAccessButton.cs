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
	// Token: 0x02000291 RID: 657
	public class AllInOneAccessButton : CustomUIImageButton
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x000864CE File Offset: 0x000846CE
		// (set) Token: 0x0600110D RID: 4365 RVA: 0x000864D6 File Offset: 0x000846D6
		public LocalizedText Tooltip { get; set; }

		// Token: 0x0600110E RID: 4366 RVA: 0x000864E0 File Offset: 0x000846E0
		public AllInOneAccessButton(Asset<Texture2D> faceTexture) : base(ModContent.Request<Texture2D>("QoLCompendium/Assets/Backgrounds/Background_" + AllInOneAccessButton.backgroundTexture.ToString(), 2))
		{
			this.faceTexture = faceTexture;
			base.SetHoverImage(ModContent.Request<Texture2D>("QoLCompendium/Assets/Storages/Storage_Hover_" + AllInOneAccessButton.storageTexture.ToString(), 2));
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00086534 File Offset: 0x00084734
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this.faceTexture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00086587 File Offset: 0x00084787
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			if (base.IsMouseHovering)
			{
				Main.hoverItemName = this.Tooltip.Value;
				Main.mouseText = true;
			}
		}

		// Token: 0x04000732 RID: 1842
		private readonly Asset<Texture2D> faceTexture;

		// Token: 0x04000734 RID: 1844
		public static int storageTexture;

		// Token: 0x04000735 RID: 1845
		public static int backgroundTexture;
	}
}
