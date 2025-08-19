using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	/// <summary>
	/// Provides a bestiary UI element to show to the user in the bestiary.
	/// </summary>
	// Token: 0x0200068A RID: 1674
	public interface IBestiaryInfoElement
	{
		/// <summary>
		/// Use to create the UIElement for this bestiary entry. Called when the bestiary entry is clicked in-game. Use <see cref="F:Terraria.GameContent.Bestiary.BestiaryUICollectionInfo.UnlockState" /> to dynamically populate the UI according to how fully unlocked this particular bestiary entry is. Return null if no UIElement should be provided, which is usually the case for NPC never encountered.
		/// <code>
		/// if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
		/// 	return null;
		///
		/// // other code creating and returning a UIElement showing info
		/// </code>
		/// </summary>
		// Token: 0x060047F2 RID: 18418
		UIElement ProvideUIElement(BestiaryUICollectionInfo info);
	}
}
