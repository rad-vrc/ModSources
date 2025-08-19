using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002F5 RID: 757
	public interface IEntryIcon
	{
		// Token: 0x060023AE RID: 9134
		void Update(BestiaryUICollectionInfo providedInfo, Rectangle hitbox, EntryIconDrawSettings settings);

		// Token: 0x060023AF RID: 9135
		void Draw(BestiaryUICollectionInfo providedInfo, SpriteBatch spriteBatch, EntryIconDrawSettings settings);

		// Token: 0x060023B0 RID: 9136
		bool GetUnlockState(BestiaryUICollectionInfo providedInfo);

		// Token: 0x060023B1 RID: 9137
		string GetHoverText(BestiaryUICollectionInfo providedInfo);

		// Token: 0x060023B2 RID: 9138
		IEntryIcon CreateClone();
	}
}
