using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200067F RID: 1663
	public class CustomEntryIcon : IEntryIcon
	{
		// Token: 0x060047CE RID: 18382 RVA: 0x00646723 File Offset: 0x00644923
		public CustomEntryIcon(string nameLanguageKey, string texturePath, Func<bool> unlockCondition)
		{
			this._text = Language.GetText(nameLanguageKey);
			this._textureAsset = Main.Assets.Request<Texture2D>(texturePath);
			this._unlockCondition = unlockCondition;
			this.UpdateUnlockState(false);
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x00646756 File Offset: 0x00644956
		public IEntryIcon CreateClone()
		{
			return new CustomEntryIcon(this._text.Key, this._textureAsset.Name, this._unlockCondition);
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x00646779 File Offset: 0x00644979
		public void Update(BestiaryUICollectionInfo providedInfo, Rectangle hitbox, EntryIconDrawSettings settings)
		{
			this.UpdateUnlockState(this.GetUnlockState(providedInfo));
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x00646788 File Offset: 0x00644988
		public void Draw(BestiaryUICollectionInfo providedInfo, SpriteBatch spriteBatch, EntryIconDrawSettings settings)
		{
			Rectangle iconbox = settings.iconbox;
			spriteBatch.Draw(this._textureAsset.Value, iconbox.Center.ToVector2() + Vector2.One, new Rectangle?(this._sourceRectangle), Color.White, 0f, this._sourceRectangle.Size() / 2f, 1f, 0, 0f);
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x006467F8 File Offset: 0x006449F8
		public string GetHoverText(BestiaryUICollectionInfo providedInfo)
		{
			if (this.GetUnlockState(providedInfo))
			{
				return this._text.Value;
			}
			return "???";
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x00646814 File Offset: 0x00644A14
		private void UpdateUnlockState(bool state)
		{
			this._sourceRectangle = this._textureAsset.Frame(2, 1, state.ToInt(), 0, 0, 0);
			this._sourceRectangle.Inflate(-2, -2);
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x00646841 File Offset: 0x00644A41
		public bool GetUnlockState(BestiaryUICollectionInfo providedInfo)
		{
			return providedInfo.UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0;
		}

		// Token: 0x04005BFB RID: 23547
		private LocalizedText _text;

		// Token: 0x04005BFC RID: 23548
		private Asset<Texture2D> _textureAsset;

		// Token: 0x04005BFD RID: 23549
		private Rectangle _sourceRectangle;

		// Token: 0x04005BFE RID: 23550
		private Func<bool> _unlockCondition;
	}
}
