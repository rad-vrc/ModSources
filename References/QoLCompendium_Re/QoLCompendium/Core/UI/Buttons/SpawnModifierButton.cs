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
	// Token: 0x0200029B RID: 667
	public class SpawnModifierButton : CustomUIImageButton
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00086F9E File Offset: 0x0008519E
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x00086FA6 File Offset: 0x000851A6
		public LocalizedText Tooltip { get; set; }

		// Token: 0x0600113F RID: 4415 RVA: 0x00086FB0 File Offset: 0x000851B0
		public SpawnModifierButton(Asset<Texture2D> faceTexture) : base(ModContent.Request<Texture2D>("QoLCompendium/Assets/Backgrounds/Background_" + SpawnModifierButton.backgroundTexture.ToString(), 2))
		{
			this.faceTexture = faceTexture;
			base.SetHoverImage(ModContent.Request<Texture2D>("QoLCompendium/Assets/SpawnModifiers/Modifier_Hover_" + SpawnModifierButton.modifierTexture.ToString(), 2));
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00087004 File Offset: 0x00085204
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this.faceTexture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00087057 File Offset: 0x00085257
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			if (base.IsMouseHovering)
			{
				Main.hoverItemName = this.Tooltip.Value;
				Main.mouseText = true;
			}
		}

		// Token: 0x04000756 RID: 1878
		private readonly Asset<Texture2D> faceTexture;

		// Token: 0x04000758 RID: 1880
		public static int modifierTexture;

		// Token: 0x04000759 RID: 1881
		public static int backgroundTexture;
	}
}
