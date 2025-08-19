using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002F8 RID: 760
	public class CustomEntryIcon : IEntryIcon
	{
		// Token: 0x060023BC RID: 9148 RVA: 0x00557FC2 File Offset: 0x005561C2
		public CustomEntryIcon(string nameLanguageKey, string texturePath, Func<bool> unlockCondition)
		{
			this._text = Language.GetText(nameLanguageKey);
			this._textureAsset = Main.Assets.Request<Texture2D>(texturePath, 1);
			this._unlockCondition = unlockCondition;
			this.UpdateUnlockState(false);
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x00557FF6 File Offset: 0x005561F6
		public IEntryIcon CreateClone()
		{
			return new CustomEntryIcon(this._text.Key, this._textureAsset.Name, this._unlockCondition);
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x00558019 File Offset: 0x00556219
		public void Update(BestiaryUICollectionInfo providedInfo, Rectangle hitbox, EntryIconDrawSettings settings)
		{
			this.UpdateUnlockState(this.GetUnlockState(providedInfo));
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x00558028 File Offset: 0x00556228
		public void Draw(BestiaryUICollectionInfo providedInfo, SpriteBatch spriteBatch, EntryIconDrawSettings settings)
		{
			Rectangle iconbox = settings.iconbox;
			spriteBatch.Draw(this._textureAsset.Value, iconbox.Center.ToVector2() + Vector2.One, new Rectangle?(this._sourceRectangle), Color.White, 0f, this._sourceRectangle.Size() / 2f, 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x00558098 File Offset: 0x00556298
		public string GetHoverText(BestiaryUICollectionInfo providedInfo)
		{
			if (this.GetUnlockState(providedInfo))
			{
				return this._text.Value;
			}
			return "???";
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x005580B4 File Offset: 0x005562B4
		private void UpdateUnlockState(bool state)
		{
			this._sourceRectangle = this._textureAsset.Frame(2, 1, state.ToInt(), 0, 0, 0);
			this._sourceRectangle.Inflate(-2, -2);
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x00557FB7 File Offset: 0x005561B7
		public bool GetUnlockState(BestiaryUICollectionInfo providedInfo)
		{
			return providedInfo.UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0;
		}

		// Token: 0x04004843 RID: 18499
		private LocalizedText _text;

		// Token: 0x04004844 RID: 18500
		private Asset<Texture2D> _textureAsset;

		// Token: 0x04004845 RID: 18501
		private Rectangle _sourceRectangle;

		// Token: 0x04004846 RID: 18502
		private Func<bool> _unlockCondition;
	}
}
