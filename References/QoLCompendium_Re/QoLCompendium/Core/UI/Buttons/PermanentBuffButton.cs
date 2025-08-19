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
	// Token: 0x02000298 RID: 664
	public class PermanentBuffButton : CustomUIImageButton
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x00086AEE File Offset: 0x00084CEE
		// (set) Token: 0x06001130 RID: 4400 RVA: 0x00086AF6 File Offset: 0x00084CF6
		public string Tooltip { get; set; }

		// Token: 0x06001131 RID: 4401 RVA: 0x00086AFF File Offset: 0x00084CFF
		public PermanentBuffButton(Asset<Texture2D> faceTexture) : base(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", 2))
		{
			this.faceTexture = faceTexture;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00086B19 File Offset: 0x00084D19
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			this.UpdateDraw(spriteBatch);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00086B29 File Offset: 0x00084D29
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.UpdateDraw(spriteBatch);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00086B3C File Offset: 0x00084D3C
		private void UpdateDraw(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			if (this.moddedBuff && this.drawTexture != null)
			{
				if (this.disabled)
				{
					spriteBatch.Draw(this.drawTexture.Value, dimensions.Position(), Color.Gray * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
				}
				else
				{
					spriteBatch.Draw(this.drawTexture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
				}
			}
			else if (this.disabled)
			{
				spriteBatch.Draw(this.faceTexture.Value, dimensions.Position(), Color.Gray * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
			}
			else
			{
				spriteBatch.Draw(this.faceTexture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? base.VisibilityActive : base.VisibilityInactive));
			}
			if (this.moddedBuff)
			{
				if (base.IsMouseHovering)
				{
					if (this.disabled)
					{
						Main.hoverItemName = this.ModTooltip.Value + " " + Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.Disabled");
						Main.mouseText = true;
						return;
					}
					Main.hoverItemName = this.ModTooltip.Value;
					Main.mouseText = true;
					return;
				}
			}
			else if (base.IsMouseHovering)
			{
				if (this.disabled)
				{
					Main.hoverItemName = this.Tooltip + " " + Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.Disabled");
					Main.mouseText = true;
					return;
				}
				Main.hoverItemName = this.Tooltip;
				Main.mouseText = true;
			}
		}

		// Token: 0x0400074E RID: 1870
		private readonly Asset<Texture2D> faceTexture;

		// Token: 0x0400074F RID: 1871
		public Asset<Texture2D> drawTexture;

		// Token: 0x04000751 RID: 1873
		public LocalizedText ModTooltip;

		// Token: 0x04000752 RID: 1874
		public bool disabled;

		// Token: 0x04000753 RID: 1875
		public bool moddedBuff;
	}
}
