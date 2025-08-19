using System;

namespace Terraria.ID
{
	// Token: 0x020003FF RID: 1023
	public class CursorOverrideID
	{
		/// <summary>
		/// Default cursor
		/// </summary>
		// Token: 0x040020DE RID: 8414
		public const short DefaultCursor = 0;

		/// <summary>
		/// Smart cursor enabled
		/// </summary>
		// Token: 0x040020DF RID: 8415
		public const short SmartCursor = 1;

		/// <summary>
		/// Try to display items in chat messages
		/// </summary>
		// Token: 0x040020E0 RID: 8416
		public const short Magnifiers = 2;

		/// <summary>
		/// Try to mark as favorite
		/// </summary>
		// Token: 0x040020E1 RID: 8417
		public const short FavoriteStar = 3;

		/// <summary>
		/// The first in-game camera point
		/// <br>If <see cref="F:Terraria.Main.cursorOverride" /> is set to this, the color will be the same as the cursor color</br>
		/// </summary>
		// Token: 0x040020E2 RID: 8418
		public const short CameraLight = 4;

		/// <summary>
		/// The second in-game camera point
		/// <br>If <see cref="F:Terraria.Main.cursorOverride" /> is set to this, the color will be the same as the cursor color</br>
		/// </summary>
		// Token: 0x040020E3 RID: 8419
		public const short CameraDark = 5;

		/// <summary>
		/// Quick trash
		/// </summary>
		// Token: 0x040020E4 RID: 8420
		public const short TrashCan = 6;

		/// <summary>
		/// From guide slot, research slot, reforge slot, etc. to inventory
		/// </summary>
		// Token: 0x040020E5 RID: 8421
		public const short BackInventory = 7;

		/// <summary>
		/// From chest to inventory
		/// </summary>
		// Token: 0x040020E6 RID: 8422
		public const short ChestToInventory = 8;

		/// <summary>
		/// From inventory to chest
		/// </summary>
		// Token: 0x040020E7 RID: 8423
		public const short InventoryToChest = 9;

		/// <summary>
		/// Quick sell items to NPC
		/// </summary>
		// Token: 0x040020E8 RID: 8424
		public const short QuickSell = 10;

		/// <summary>
		/// Default cursor outline
		/// </summary>
		// Token: 0x040020E9 RID: 8425
		public const short DefaultCursorOutline = 11;

		/// <summary>
		/// Smart cursor outline
		/// </summary>
		// Token: 0x040020EA RID: 8426
		public const short SmartCursorOutline = 12;

		/// <summary>
		/// Smart cursor enabled if <see cref="P:Terraria.GameInput.PlayerInput.SettingsForUI.ShowGamepadCursor" /> is true
		/// <br>When using the gamepad in smart cursor mode, the cursor that is relatively close to the player</br>
		/// </summary>
		// Token: 0x040020EB RID: 8427
		public const short GamepadSmartCursor = 13;

		/// <summary>
		/// Smart cursor outline if <see cref="P:Terraria.GameInput.PlayerInput.SettingsForUI.ShowGamepadCursor" /> is true
		/// </summary>
		// Token: 0x040020EC RID: 8428
		public const short GamepadSmartCursorOutline = 14;

		/// <summary>
		/// Default cursor if <see cref="P:Terraria.GameInput.PlayerInput.SettingsForUI.ShowGamepadCursor" /> is true
		/// </summary>
		// Token: 0x040020ED RID: 8429
		public const short GamepadDefaultCursor = 15;

		/// <summary>
		/// Default cursor outline if <see cref="P:Terraria.GameInput.PlayerInput.SettingsForUI.ShowGamepadCursor" /> is true
		/// </summary>
		// Token: 0x040020EE RID: 8430
		public const short GamepadDefaultCursorOutline = 16;

		/// <summary>
		/// Actual cursor position indicator if smart cursor enabled and <see cref="P:Terraria.GameInput.PlayerInput.SettingsForUI.ShowGamepadCursor" /> is true
		/// <br>When using the gamepad in smart cursor mode, the cursor that is relatively far away from the player</br>
		/// </summary>
		// Token: 0x040020EF RID: 8431
		public const short GamepadSmartCursorAlt = 17;
	}
}
