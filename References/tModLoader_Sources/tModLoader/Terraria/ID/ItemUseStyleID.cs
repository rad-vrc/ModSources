using System;

namespace Terraria.ID
{
	/// <summary>
	/// Assign <see cref="F:Terraria.Item.useStyle" /> to one of these to give your item an animation while in use. The <see href="https://terraria.wiki.gg/wiki/Use_Style_IDs">Use Style IDs wiki page</see> has examples and animations of each of these use styles.
	/// <br /> If none of these animations match what you want, consider making the animation a behavior of a held projectile or use the <see cref="M:Terraria.ModLoader.ModItem.UseStyle(Terraria.Player,Microsoft.Xna.Framework.Rectangle)" /> hook to implement a custom animation.
	/// </summary>
	// Token: 0x02000411 RID: 1041
	public class ItemUseStyleID
	{
		// Token: 0x04003D9E RID: 15774
		public const int None = 0;

		/// <summary>Item is swung in an overhead arch.<br />Used for many sword weapons, block placement, etc.</summary>
		// Token: 0x04003D9F RID: 15775
		public const int Swing = 1;

		/// <summary>
		/// Unused
		/// </summary>
		// Token: 0x04003DA0 RID: 15776
		public const int DrinkOld = 7;

		/// <summary>Item is thrust horizontally.<br />Use by <see cref="F:Terraria.ID.ItemID.Umbrella" /> and <see cref="F:Terraria.ID.ItemID.TragicUmbrella" /></summary>
		// Token: 0x04003DA1 RID: 15777
		public const int Thrust = 3;

		/// <summary>Item is held up.<br />Used for items such as mana/life crystals, life fruit, magic mirrors, and summoning items</summary>
		// Token: 0x04003DA2 RID: 15778
		public const int HoldUp = 4;

		/// <summary>
		/// Item is held in front of player pointing towards mouse. Item sprite should be oriented horizontally with handle at left and muzzle end at right. Set <c><see cref="F:Terraria.Item.staff" />[Type] = true;</c> in SetStaticDefaults if the weapon sprite is oriented like a staff (handle at bottom left, end at top right).
		/// <br /> Used by items such as guns, spell books, flails, yoyos, and spears
		/// </summary>
		// Token: 0x04003DA3 RID: 15779
		public const int Shoot = 5;

		/// <summary>
		/// Item is brought in front of player and rotated towards player.
		/// <br /> Used by Recall Potion and Potion of Return
		/// </summary>
		// Token: 0x04003DA4 RID: 15780
		public const int DrinkLong = 6;

		/// <summary>Eating or using<br />Used by food</summary>
		// Token: 0x04003DA5 RID: 15781
		public const int EatFood = 2;

		/// <summary>
		/// Item is swung like a golf club.
		/// <br /> Used by golf clubs.
		/// </summary>
		// Token: 0x04003DA6 RID: 15782
		public const int GolfPlay = 8;

		/// <summary>
		/// Item is brought in front of player and rotated towards player.
		/// <br /> Used by potions, drinks, flasks, and hair dyes.
		/// </summary>
		// Token: 0x04003DA7 RID: 15783
		public const int DrinkLiquid = 9;

		// Token: 0x04003DA8 RID: 15784
		public const int HiddenAnimation = 10;

		/// <summary>
		/// Used by Lawn Mower
		/// </summary>
		// Token: 0x04003DA9 RID: 15785
		public const int MowTheLawn = 11;

		/// <summary>
		/// Used by guitar-shaped instruments
		/// </summary>
		// Token: 0x04003DAA RID: 15786
		public const int Guitar = 12;

		/// <summary>
		/// Item is thrust in any direction towards the mouse.
		/// Used by Shortswords
		/// </summary>
		// Token: 0x04003DAB RID: 15787
		public const int Rapier = 13;

		/// <summary>
		/// Item is raised high by off hand.
		/// <br /> Used by Nightglow.
		/// </summary>
		// Token: 0x04003DAC RID: 15788
		public const int RaiseLamp = 14;
	}
}
