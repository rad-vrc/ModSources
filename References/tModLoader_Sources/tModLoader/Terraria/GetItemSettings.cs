using System;

namespace Terraria
{
	/// <summary>
	/// Contains settings for the popup text shown when using <see cref="M:Terraria.Player.GetItem(System.Int32,Terraria.Item,Terraria.GetItemSettings)" />. The contained static fields can be used to match vanilla behavior for various actions.
	/// <para /> <b>LongText:</b> <inheritdoc cref="M:Terraria.PopupText.NewText(Terraria.PopupTextContext,Terraria.Item,System.Int32,System.Boolean,System.Boolean)" path="/param[@name='longText']" />
	/// <br /> <b>NoText:</b> If <see langword="true" />, no <see cref="T:Terraria.PopupText" /> will spawn.
	/// <br /> <b>CanGoIntoVoidVault:</b> If <see langword="true" />, the item can be placed into the Void Vault.
	/// </summary>
	// Token: 0x0200002F RID: 47
	public struct GetItemSettings
	{
		/// <inheritdoc cref="T:Terraria.GetItemSettings" />
		// Token: 0x06000214 RID: 532 RVA: 0x00022BD1 File Offset: 0x00020DD1
		public GetItemSettings(bool LongText = false, bool NoText = false, bool CanGoIntoVoidVault = false, Action<Item> StepAfterHandlingSlotNormally = null)
		{
			this.LongText = LongText;
			this.NoText = NoText;
			this.CanGoIntoVoidVault = CanGoIntoVoidVault;
			this.StepAfterHandlingSlotNormally = StepAfterHandlingSlotNormally;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00022BF0 File Offset: 0x00020DF0
		public void HandlePostAction(Item item)
		{
			if (this.StepAfterHandlingSlotNormally != null)
			{
				this.StepAfterHandlingSlotNormally(item);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00022C06 File Offset: 0x00020E06
		private static void MakeNewAndShiny(Item item)
		{
			item.newAndShiny = true;
		}

		// Token: 0x040001DA RID: 474
		public static GetItemSettings InventoryEntityToPlayerInventorySettings = new GetItemSettings(false, true, false, null);

		// Token: 0x040001DB RID: 475
		public static GetItemSettings NPCEntityToPlayerInventorySettings = new GetItemSettings(true, false, false, null);

		// Token: 0x040001DC RID: 476
		public static GetItemSettings LootAllSettings = default(GetItemSettings);

		// Token: 0x040001DD RID: 477
		public static GetItemSettings LootAllSettingsRegularChest = new GetItemSettings(false, false, true, null);

		// Token: 0x040001DE RID: 478
		public static GetItemSettings PickupItemFromWorld = new GetItemSettings(false, false, true, null);

		// Token: 0x040001DF RID: 479
		public static GetItemSettings GetItemInDropItemCheck = new GetItemSettings(false, true, false, null);

		// Token: 0x040001E0 RID: 480
		public static GetItemSettings InventoryUIToInventorySettings = default(GetItemSettings);

		// Token: 0x040001E1 RID: 481
		public static GetItemSettings InventoryUIToInventorySettingsShowAsNew = new GetItemSettings(false, true, false, new Action<Item>(GetItemSettings.MakeNewAndShiny));

		// Token: 0x040001E2 RID: 482
		public static GetItemSettings ItemCreatedFromItemUsage = default(GetItemSettings);

		// Token: 0x040001E3 RID: 483
		public readonly bool LongText;

		// Token: 0x040001E4 RID: 484
		public readonly bool NoText;

		// Token: 0x040001E5 RID: 485
		public readonly bool CanGoIntoVoidVault;

		// Token: 0x040001E6 RID: 486
		public readonly Action<Item> StepAfterHandlingSlotNormally;
	}
}
