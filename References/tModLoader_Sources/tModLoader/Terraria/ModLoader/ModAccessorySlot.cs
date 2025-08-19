using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Default;

namespace Terraria.ModLoader
{
	/// <summary>
	/// A ModAccessorySlot instance represents a net new accessory slot instance. You can store fields in the ModAccessorySlot class.
	/// </summary>
	// Token: 0x02000199 RID: 409
	public abstract class ModAccessorySlot : ModType
	{
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x004E2481 File Offset: 0x004E0681
		// (set) Token: 0x06001FAB RID: 8107 RVA: 0x004E2489 File Offset: 0x004E0689
		public int Type { get; internal set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001FAC RID: 8108 RVA: 0x004E2492 File Offset: 0x004E0692
		public static Player Player
		{
			get
			{
				return Main.CurrentPlayer;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x004E2499 File Offset: 0x004E0699
		public ModAccessorySlotPlayer ModSlotPlayer
		{
			get
			{
				return AccessorySlotLoader.ModSlotPlayer(ModAccessorySlot.Player);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x004E24A8 File Offset: 0x004E06A8
		public virtual Vector2? CustomLocation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x004E24BE File Offset: 0x004E06BE
		public virtual string DyeBackgroundTexture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x004E24C1 File Offset: 0x004E06C1
		public virtual string VanityBackgroundTexture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x004E24C4 File Offset: 0x004E06C4
		public virtual string FunctionalBackgroundTexture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x004E24C7 File Offset: 0x004E06C7
		public virtual string DyeTexture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x004E24CA File Offset: 0x004E06CA
		public virtual string VanityTexture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x004E24CD File Offset: 0x004E06CD
		public virtual string FunctionalTexture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x004E24D0 File Offset: 0x004E06D0
		public virtual bool DrawFunctionalSlot
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x004E24D3 File Offset: 0x004E06D3
		public virtual bool DrawVanitySlot
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x004E24D6 File Offset: 0x004E06D6
		public virtual bool DrawDyeSlot
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this slot supports equipment loadouts. If <see langword="false" />,
		/// the slot's item is shared between all loadouts.
		/// <br /><br /> Defaults to <see langword="true" />.
		/// <br /><br /> Changing this value requires a reload. This value is not allowed to be different between multiplayer clients or issues will occur.
		/// <br /><br /> Changing the value from <see langword="true" /> to <see langword="false" /> will cause the extra items to be spawned on the player when they enter the world.
		/// <br /> Changing the value from <see langword="false" /> to <see langword="true" /> will result in the currently selected loadout holding the items.
		/// <br /><br /> Slots that don't support loadouts will appear with the default green background texture, as if they were an accessory in loadout #1 or from before loadout support was added to the game.
		/// </summary>
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x004E24D9 File Offset: 0x004E06D9
		public virtual bool HasEquipmentLoadoutSupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x004E24DC File Offset: 0x004E06DC
		// (set) Token: 0x06001FBA RID: 8122 RVA: 0x004E24F0 File Offset: 0x004E06F0
		public Item FunctionalItem
		{
			get
			{
				return this.ModSlotPlayer.exAccessorySlot[this.Type];
			}
			set
			{
				this.ModSlotPlayer.exAccessorySlot[this.Type] = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x004E2505 File Offset: 0x004E0705
		// (set) Token: 0x06001FBC RID: 8124 RVA: 0x004E2525 File Offset: 0x004E0725
		public Item VanityItem
		{
			get
			{
				return this.ModSlotPlayer.exAccessorySlot[this.Type + this.ModSlotPlayer.SlotCount];
			}
			set
			{
				this.ModSlotPlayer.exAccessorySlot[this.Type + this.ModSlotPlayer.SlotCount] = value;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x004E2546 File Offset: 0x004E0746
		// (set) Token: 0x06001FBE RID: 8126 RVA: 0x004E255A File Offset: 0x004E075A
		public Item DyeItem
		{
			get
			{
				return this.ModSlotPlayer.exDyesAccessory[this.Type];
			}
			set
			{
				this.ModSlotPlayer.exDyesAccessory[this.Type] = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x004E256F File Offset: 0x004E076F
		// (set) Token: 0x06001FC0 RID: 8128 RVA: 0x004E2583 File Offset: 0x004E0783
		public bool HideVisuals
		{
			get
			{
				return this.ModSlotPlayer.exHideAccessory[this.Type];
			}
			set
			{
				this.ModSlotPlayer.exHideAccessory[this.Type] = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x004E2598 File Offset: 0x004E0798
		public bool IsEmpty
		{
			get
			{
				return this.FunctionalItem.IsAir && this.VanityItem.IsAir && this.DyeItem.IsAir;
			}
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x004E25C1 File Offset: 0x004E07C1
		protected sealed override void Register()
		{
			this.Type = LoaderManager.Get<AccessorySlotLoader>().Register(this);
		}

		/// <summary>
		/// Allows drawing prior to vanilla ItemSlot.Draw code. Return false to NOT call ItemSlot.Draw
		/// </summary>
		// Token: 0x06001FC3 RID: 8131 RVA: 0x004E25D4 File Offset: 0x004E07D4
		public virtual bool PreDraw(AccessorySlotType context, Item item, Vector2 position, bool isHovered)
		{
			return true;
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x004E25D7 File Offset: 0x004E07D7
		public virtual void PostDraw(AccessorySlotType context, Item item, Vector2 position, bool isHovered)
		{
		}

		/// <summary>
		/// Override to replace the vanilla effect behavior of the slot with your own.
		/// By default calls:
		/// Player.VanillaUpdateEquips(FunctionalItem), Player.ApplyEquipFunctional(FunctionalItem, ShowVisuals), Player.ApplyEquipVanity(VanityItem)
		/// </summary>
		// Token: 0x06001FC5 RID: 8133 RVA: 0x004E25DC File Offset: 0x004E07DC
		public virtual void ApplyEquipEffects()
		{
			if (this.FunctionalItem.accessory)
			{
				ModAccessorySlot.Player.GrantPrefixBenefits(this.FunctionalItem);
			}
			ModAccessorySlot.Player.GrantArmorBenefits(this.FunctionalItem);
			ModAccessorySlot.Player.ApplyEquipFunctional(this.FunctionalItem, this.HideVisuals);
			ModAccessorySlot.Player.ApplyEquipVanity(this.VanityItem);
		}

		/// <summary>
		/// Override to set conditions on what can be placed in the slot. Default is to return false only when item property FitsAccessoryVanity says can't go in to a vanity slot.
		/// Return false to prevent the item going in slot. Return true for dyes, if you want dyes. Example: only wings can go in slot.
		/// Receives data:
		/// <para><paramref name="checkItem" /> :: the item that is attempting to enter the slot </para>
		/// </summary>
		// Token: 0x06001FC6 RID: 8134 RVA: 0x004E263C File Offset: 0x004E083C
		public virtual bool CanAcceptItem(Item checkItem, AccessorySlotType context)
		{
			return context != AccessorySlotType.VanitySlot || checkItem.FitsAccessoryVanitySlot;
		}

		/// <summary>
		/// After checking for empty slots in ItemSlot.AccessorySwap, this allows for changing what the default target slot (accSlotToSwapTo) will be.
		/// DOES NOT affect vanilla behavior of swapping items like for like where existing in a slot
		/// Return true to set this slot as the default targeted slot.
		/// </summary>
		// Token: 0x06001FC7 RID: 8135 RVA: 0x004E264B File Offset: 0x004E084B
		public virtual bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
		{
			return false;
		}

		/// <summary>
		/// Override to control whether or not drawing will be skipped during the given frame.
		/// NOTE: Nothing will be drawn, nor will subsequent drawing hooks be called on this slot for the frame while true
		/// </summary>
		// Token: 0x06001FC8 RID: 8136 RVA: 0x004E264E File Offset: 0x004E084E
		public virtual bool IsHidden()
		{
			return false;
		}

		/// <summary>
		/// Override to set conditions on when the slot is valid for stat/vanity calculations and player usage.
		/// Example: the demonHeart is consumed and in Expert mode in Vanilla.
		/// </summary>
		// Token: 0x06001FC9 RID: 8137 RVA: 0x004E2651 File Offset: 0x004E0851
		public virtual bool IsEnabled()
		{
			return true;
		}

		/// <summary>
		/// Override to change the condition on when the slot is visible, but otherwise non-functional for stat/vanity calculations.
		/// Defaults to check 'property' IsEmpty
		/// </summary>
		/// <returns></returns>
		// Token: 0x06001FCA RID: 8138 RVA: 0x004E2654 File Offset: 0x004E0854
		public virtual bool IsVisibleWhenNotEnabled()
		{
			return !this.IsEmpty;
		}

		/// <summary>
		/// Allows you to do stuff while the player is hovering over this slot.
		/// </summary>
		// Token: 0x06001FCB RID: 8139 RVA: 0x004E265F File Offset: 0x004E085F
		public virtual void OnMouseHover(AccessorySlotType context)
		{
		}

		/// <summary>
		/// Allows customizing the background texture draw color.
		/// <br /><br /> For <see cref="P:Terraria.ModLoader.ModAccessorySlot.HasEquipmentLoadoutSupport" /> slots without a custom texture, the color will already have been adjusted for the current loadout (ItemSlot.GetColorByLoadout), otherwise the color will be <see cref="F:Terraria.Main.inventoryBack" />, which will oscillate all values between 180 and 240.
		/// </summary>
		// Token: 0x06001FCC RID: 8140 RVA: 0x004E2661 File Offset: 0x004E0861
		public virtual void BackgroundDrawColor(AccessorySlotType context, ref Color color)
		{
		}
	}
}
