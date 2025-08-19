using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000366 RID: 870
	public class UIBestiaryEntryIcon : UIElement
	{
		// Token: 0x060027F9 RID: 10233 RVA: 0x0058767C File Offset: 0x0058587C
		public UIBestiaryEntryIcon(BestiaryEntry entry, bool isPortrait)
		{
			this._entry = entry;
			this.IgnoresMouseInteraction = true;
			this.OverrideSamplerState = Main.DefaultSamplerState;
			this.UseImmediateMode = true;
			this.Width.Set(0f, 1f);
			this.Height.Set(0f, 1f);
			this._notUnlockedTexture = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Locked", 1);
			this._isPortrait = isPortrait;
			this._collectionInfo = this._entry.UIInfoProvider.GetEntryUICollectionInfo();
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x0058770C File Offset: 0x0058590C
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

		// Token: 0x060027FB RID: 10235 RVA: 0x00587798 File Offset: 0x00585998
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
			spriteBatch.Draw(value, dimensions.Center(), null, Color.White * 0.15f, 0f, value.Size() / 2f, 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x0058786D File Offset: 0x00585A6D
		public string GetHoverText()
		{
			return this._entry.Icon.GetHoverText(this._collectionInfo);
		}

		// Token: 0x04004B33 RID: 19251
		private BestiaryEntry _entry;

		// Token: 0x04004B34 RID: 19252
		private Asset<Texture2D> _notUnlockedTexture;

		// Token: 0x04004B35 RID: 19253
		private bool _isPortrait;

		// Token: 0x04004B36 RID: 19254
		public bool ForceHover;

		// Token: 0x04004B37 RID: 19255
		private BestiaryUICollectionInfo _collectionInfo;
	}
}
