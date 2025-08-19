using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200068F RID: 1679
	public interface IEntryIcon
	{
		// Token: 0x060047F7 RID: 18423
		void Update(BestiaryUICollectionInfo providedInfo, Rectangle hitbox, EntryIconDrawSettings settings);

		// Token: 0x060047F8 RID: 18424
		void Draw(BestiaryUICollectionInfo providedInfo, SpriteBatch spriteBatch, EntryIconDrawSettings settings);

		// Token: 0x060047F9 RID: 18425
		bool GetUnlockState(BestiaryUICollectionInfo providedInfo);

		// Token: 0x060047FA RID: 18426
		string GetHoverText(BestiaryUICollectionInfo providedInfo);

		// Token: 0x060047FB RID: 18427
		IEntryIcon CreateClone();
	}
}
