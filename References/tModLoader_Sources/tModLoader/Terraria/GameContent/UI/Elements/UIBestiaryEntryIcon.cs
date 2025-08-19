using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000504 RID: 1284
	public class UIBestiaryEntryIcon : UIElement
	{
		// Token: 0x06003DDE RID: 15838 RVA: 0x005CD9F0 File Offset: 0x005CBBF0
		public UIBestiaryEntryIcon(BestiaryEntry entry, bool isPortrait)
		{
			this._entry = entry;
			this.IgnoresMouseInteraction = true;
			this.OverrideSamplerState = Main.DefaultSamplerState;
			this.UseImmediateMode = true;
			this.Width.Set(0f, 1f);
			this.Height.Set(0f, 1f);
			this._notUnlockedTexture = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Locked");
			this._isPortrait = isPortrait;
			this._collectionInfo = this._entry.UIInfoProvider.GetEntryUICollectionInfo();
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x005CDA80 File Offset: 0x005CBC80
		public override void Update(GameTime gameTime)
		{
			this._collectionInfo = this._entry.UIInfoProvider.GetEntryUICollectionInfo();
			CalculatedStyle dimensions = base.GetDimensions();
			bool isHovered = base.IsMouseHovering || this.ForceHover;
			this._entry.Icon.Update(this._collectionInfo, dimensions.ToRectangle(), new EntryIconDrawSettings
			{
				iconbox = dimensions.ToRectangle(),
				IsPortrait = this._isPortrait,
				IsHovered = isHovered
			});
			base.Update(gameTime);
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x005CDB0C File Offset: 0x005CBD0C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			bool unlockState = this._entry.Icon.GetUnlockState(this._collectionInfo);
			bool isHovered = base.IsMouseHovering || this.ForceHover;
			if (unlockState)
			{
				this._entry.Icon.Draw(this._collectionInfo, spriteBatch, new EntryIconDrawSettings
				{
					iconbox = dimensions.ToRectangle(),
					IsPortrait = this._isPortrait,
					IsHovered = isHovered
				});
				return;
			}
			Texture2D value = this._notUnlockedTexture.Value;
			spriteBatch.Draw(value, dimensions.Center(), null, Color.White * 0.15f, 0f, value.Size() / 2f, 1f, 0, 0f);
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x005CDBE1 File Offset: 0x005CBDE1
		public string GetHoverText()
		{
			return this._entry.Icon.GetHoverText(this._collectionInfo);
		}

		// Token: 0x0400569F RID: 22175
		private BestiaryEntry _entry;

		// Token: 0x040056A0 RID: 22176
		private Asset<Texture2D> _notUnlockedTexture;

		// Token: 0x040056A1 RID: 22177
		private bool _isPortrait;

		// Token: 0x040056A2 RID: 22178
		public bool ForceHover;

		// Token: 0x040056A3 RID: 22179
		private BestiaryUICollectionInfo _collectionInfo;
	}
}
