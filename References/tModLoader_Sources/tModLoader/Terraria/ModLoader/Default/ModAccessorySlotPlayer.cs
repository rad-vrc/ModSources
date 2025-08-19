using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002C5 RID: 709
	public sealed class ModAccessorySlotPlayer : ModPlayer
	{
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x0052E703 File Offset: 0x0052C903
		private static AccessorySlotLoader Loader
		{
			get
			{
				return LoaderManager.Get<AccessorySlotLoader>();
			}
		}

		/// <summary>
		/// Total modded slots to show, including UnloadedAccessorySlot at the end.
		/// On the local instance of a ModAccessorySlotPlayer, this might have extra entries not present on remote clients.
		/// </summary>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x0052E70A File Offset: 0x0052C90A
		public int SlotCount
		{
			get
			{
				return this.slots.Count;
			}
		}

		/// <summary>
		/// Total loaded modded slots.
		/// This does not include UnloadedAccessorySlot entries. This value is used for network syncing since this will be consistent between clients.
		/// </summary>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x0052E717 File Offset: 0x0052C917
		public int LoadedSlotCount
		{
			get
			{
				return ModAccessorySlotPlayer.Loader.TotalCount;
			}
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x0052E724 File Offset: 0x0052C924
		public ModAccessorySlotPlayer()
		{
			foreach (ModAccessorySlot slot in ModAccessorySlotPlayer.Loader.list)
			{
				this.slots.Add(slot.FullName, slot.Type);
			}
			this.ResetAndSizeAccessoryArrays();
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x0052E7B0 File Offset: 0x0052C9B0
		internal void ResetAndSizeAccessoryArrays()
		{
			int size = this.SlotCount;
			this.exAccessorySlot = new Item[2 * size];
			this.exDyesAccessory = new Item[size];
			this.exHideAccessory = new bool[size];
			this.sharedLoadoutSlotTypes = new bool[size];
			for (int i = 0; i < size; i++)
			{
				this.exDyesAccessory[i] = new Item();
				this.exHideAccessory[i] = false;
				this.exAccessorySlot[i * 2] = new Item();
				this.exAccessorySlot[i * 2 + 1] = new Item();
			}
			foreach (ModAccessorySlot slot in ModAccessorySlotPlayer.Loader.list)
			{
				if (!slot.HasEquipmentLoadoutSupport)
				{
					this.sharedLoadoutSlotTypes[slot.Type] = true;
				}
			}
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x0052E894 File Offset: 0x0052CA94
		public override void Initialize()
		{
			this.exLoadouts = (from loadoutIndex in Enumerable.Range(0, base.Player.Loadouts.Length)
			select new ModAccessorySlotPlayer.ExEquipmentLoadout(loadoutIndex, this.SlotCount, base.Player.Loadouts[loadoutIndex])).ToArray<ModAccessorySlotPlayer.ExEquipmentLoadout>();
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x0052E8C8 File Offset: 0x0052CAC8
		public override void SaveData(TagCompound tag)
		{
			tag["order"] = this.slots.Keys.ToList<string>();
			string key = "items";
			IEnumerable<Item> source = this.exAccessorySlot;
			Func<Item, TagCompound> selector;
			if ((selector = ModAccessorySlotPlayer.<>O.<0>__Save) == null)
			{
				selector = (ModAccessorySlotPlayer.<>O.<0>__Save = new Func<Item, TagCompound>(ItemIO.Save));
			}
			tag[key] = source.Select(selector).ToList<TagCompound>();
			string key2 = "dyes";
			IEnumerable<Item> source2 = this.exDyesAccessory;
			Func<Item, TagCompound> selector2;
			if ((selector2 = ModAccessorySlotPlayer.<>O.<0>__Save) == null)
			{
				selector2 = (ModAccessorySlotPlayer.<>O.<0>__Save = new Func<Item, TagCompound>(ItemIO.Save));
			}
			tag[key2] = source2.Select(selector2).ToList<TagCompound>();
			tag["visible"] = this.exHideAccessory.ToList<bool>();
			ModAccessorySlotPlayer.ExEquipmentLoadout[] array = this.exLoadouts;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SaveData(tag);
			}
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x0052E990 File Offset: 0x0052CB90
		public override void LoadData(TagCompound tag)
		{
			List<string> order = tag.GetList<string>("order").ToList<string>();
			foreach (string name in order)
			{
				if (!this.slots.ContainsKey(name))
				{
					this.slots.Add(name, this.slots.Count);
				}
			}
			this.ResetAndSizeAccessoryArrays();
			IEnumerable<TagCompound> list = tag.GetList<TagCompound>("items");
			Func<TagCompound, Item> selector;
			if ((selector = ModAccessorySlotPlayer.<>O.<1>__Load) == null)
			{
				selector = (ModAccessorySlotPlayer.<>O.<1>__Load = new Func<TagCompound, Item>(ItemIO.Load));
			}
			List<Item> items = list.Select(selector).ToList<Item>();
			IEnumerable<TagCompound> list2 = tag.GetList<TagCompound>("dyes");
			Func<TagCompound, Item> selector2;
			if ((selector2 = ModAccessorySlotPlayer.<>O.<1>__Load) == null)
			{
				selector2 = (ModAccessorySlotPlayer.<>O.<1>__Load = new Func<TagCompound, Item>(ItemIO.Load));
			}
			List<Item> dyes = list2.Select(selector2).ToList<Item>();
			List<bool> visible = tag.GetList<bool>("visible").ToList<bool>();
			ModAccessorySlotPlayer.ExEquipmentLoadout[] array = this.exLoadouts;
			for (int j = 0; j < array.Length; j++)
			{
				IReadOnlyList<Item> extraItemsFromLoadout = array[j].LoadData(tag, order, this.slots, this.sharedLoadoutSlotTypes);
				this.extraItems.AddRange(extraItemsFromLoadout);
			}
			for (int i = 0; i < order.Count; i++)
			{
				int type = this.slots[order[i]];
				this.exDyesAccessory[type] = dyes[i];
				this.exHideAccessory[type] = visible[i];
				this.exAccessorySlot[type] = items[i];
				this.exAccessorySlot[type + this.SlotCount] = items[i + order.Count];
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x0052EB48 File Offset: 0x0052CD48
		public override void OnEnterWorld()
		{
			this.DetectConflictsWithSharedSlots();
			if (this.extraItems.Count == 0)
			{
				return;
			}
			foreach (Item item in this.extraItems)
			{
				base.Player.QuickSpawnItem(null, item, 1);
			}
			Main.NewText(Language.GetTextValue("tModLoader.ModAccessorySlotNoLongerSharedItemsRemoved"), byte.MaxValue, byte.MaxValue, byte.MaxValue);
			this.extraItems.Clear();
		}

		/// <summary>
		/// Updates functional slot visibility information on the player for Mod Slots, in a similar fashion to Player.UpdateVisibleAccessories()
		/// </summary>
		// Token: 0x06002D8D RID: 11661 RVA: 0x0052EBE0 File Offset: 0x0052CDE0
		public override void UpdateVisibleAccessories()
		{
			AccessorySlotLoader loader = LoaderManager.Get<AccessorySlotLoader>();
			for (int i = 0; i < this.SlotCount; i++)
			{
				if (loader.ModdedIsSpecificItemSlotUnlockedAndUsable(i, base.Player, false))
				{
					base.Player.UpdateVisibleAccessories(this.exAccessorySlot[i], this.exHideAccessory[i], i, true);
				}
			}
		}

		/// <summary>
		/// Updates vanity slot information on the player for Mod Slots, in a similar fashion to Player.UpdateVisibleAccessories()
		/// </summary>
		// Token: 0x06002D8E RID: 11662 RVA: 0x0052EC34 File Offset: 0x0052CE34
		public override void UpdateVisibleVanityAccessories()
		{
			AccessorySlotLoader loader = LoaderManager.Get<AccessorySlotLoader>();
			for (int i = 0; i < this.SlotCount; i++)
			{
				if (loader.ModdedIsSpecificItemSlotUnlockedAndUsable(i, base.Player, true))
				{
					int vanitySlot = i + this.SlotCount;
					if (!base.Player.ItemIsVisuallyIncompatible(this.exAccessorySlot[vanitySlot]))
					{
						base.Player.UpdateVisibleAccessory(vanitySlot, this.exAccessorySlot[vanitySlot], true);
					}
				}
			}
		}

		/// <summary>
		/// Mirrors Player.UpdateDyes() for modded slots
		/// Runs On Player Select, so is Player instance sensitive!!!
		/// </summary>
		// Token: 0x06002D8F RID: 11663 RVA: 0x0052EC9C File Offset: 0x0052CE9C
		public void UpdateDyes(bool socialSlots)
		{
			AccessorySlotLoader loader = LoaderManager.Get<AccessorySlotLoader>();
			int num2 = socialSlots ? this.SlotCount : 0;
			int end = socialSlots ? (this.SlotCount * 2) : this.SlotCount;
			for (int i = num2; i < end; i++)
			{
				if (loader.ModdedIsSpecificItemSlotUnlockedAndUsable(i, base.Player, socialSlots))
				{
					int num = i % this.exDyesAccessory.Length;
					base.Player.UpdateItemDye(i < this.exDyesAccessory.Length, this.exHideAccessory[num], this.exAccessorySlot[i], this.exDyesAccessory[num]);
				}
			}
		}

		/// <summary>
		/// Runs a simplified version of Player.UpdateEquips for the Modded Accessory Slots
		/// </summary>
		// Token: 0x06002D90 RID: 11664 RVA: 0x0052ED24 File Offset: 0x0052CF24
		public override void UpdateEquips()
		{
			AccessorySlotLoader loader = LoaderManager.Get<AccessorySlotLoader>();
			for (int i = 0; i < this.SlotCount; i++)
			{
				if (loader.ModdedIsSpecificItemSlotUnlockedAndUsable(i, base.Player, false))
				{
					loader.CustomUpdateEquips(i, base.Player);
				}
			}
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x0052ED68 File Offset: 0x0052CF68
		public void DropItems(IEntitySource itemSource)
		{
			LoaderManager.Get<AccessorySlotLoader>();
			for (int i = 0; i < this.SlotCount; i++)
			{
				base.Player.TryDroppingSingleItem(itemSource, this.exAccessorySlot[i]);
				base.Player.TryDroppingSingleItem(itemSource, this.exAccessorySlot[i + this.SlotCount]);
				base.Player.TryDroppingSingleItem(itemSource, this.exDyesAccessory[i]);
				foreach (ModAccessorySlotPlayer.ExEquipmentLoadout equipmentLoadout in this.exLoadouts)
				{
					base.Player.TryDroppingSingleItem(itemSource, equipmentLoadout.ExAccessorySlot[i]);
					base.Player.TryDroppingSingleItem(itemSource, equipmentLoadout.ExAccessorySlot[i + this.SlotCount]);
					base.Player.TryDroppingSingleItem(itemSource, equipmentLoadout.ExDyesAccessory[i]);
				}
			}
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x0052EE34 File Offset: 0x0052D034
		public override void CopyClientState(ModPlayer targetCopy)
		{
			ModAccessorySlotPlayer defaultInv = (ModAccessorySlotPlayer)targetCopy;
			for (int i = 0; i < this.LoadedSlotCount; i++)
			{
				this.exAccessorySlot[i].CopyNetStateTo(defaultInv.exAccessorySlot[i]);
				this.exAccessorySlot[i + this.SlotCount].CopyNetStateTo(defaultInv.exAccessorySlot[i + this.LoadedSlotCount]);
				this.exDyesAccessory[i].CopyNetStateTo(defaultInv.exDyesAccessory[i]);
				defaultInv.exHideAccessory[i] = this.exHideAccessory[i];
			}
			for (int loadoutIndex = 0; loadoutIndex < this.exLoadouts.Length; loadoutIndex++)
			{
				this.<CopyClientState>g__CopyState|26_0(this.exLoadouts[loadoutIndex], defaultInv.exLoadouts[loadoutIndex]);
			}
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x0052EEE0 File Offset: 0x0052D0E0
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModAccessorySlotPlayer.<>c__DisplayClass27_0 CS$<>8__locals1;
			CS$<>8__locals1.toWho = toWho;
			CS$<>8__locals1.<>4__this = this;
			for (int i = 0; i < this.LoadedSlotCount; i++)
			{
				ModAccessorySlotPlayer.NetHandler.SendSlot(CS$<>8__locals1.toWho, base.Player.whoAmI, i, this.exAccessorySlot[i], -1);
				ModAccessorySlotPlayer.NetHandler.SendSlot(CS$<>8__locals1.toWho, base.Player.whoAmI, i + this.LoadedSlotCount, this.exAccessorySlot[i + this.SlotCount], -1);
				ModAccessorySlotPlayer.NetHandler.SendSlot(CS$<>8__locals1.toWho, base.Player.whoAmI, -i - 1, this.exDyesAccessory[i], -1);
				ModAccessorySlotPlayer.NetHandler.SendVisualState(CS$<>8__locals1.toWho, base.Player.whoAmI, i, this.exHideAccessory[i]);
			}
			foreach (ModAccessorySlotPlayer.ExEquipmentLoadout equipmentLoadout in this.exLoadouts)
			{
				this.<SyncPlayer>g__Sync|27_0(equipmentLoadout, ref CS$<>8__locals1);
			}
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x0052EFC8 File Offset: 0x0052D1C8
		public override void SendClientChanges(ModPlayer clientPlayer)
		{
			ModAccessorySlotPlayer clientInv = (ModAccessorySlotPlayer)clientPlayer;
			for (int i = 0; i < this.LoadedSlotCount; i++)
			{
				if (this.exAccessorySlot[i].IsNetStateDifferent(clientInv.exAccessorySlot[i]))
				{
					ModAccessorySlotPlayer.NetHandler.SendSlot(-1, base.Player.whoAmI, i, this.exAccessorySlot[i], -1);
				}
				if (this.exAccessorySlot[i + this.SlotCount].IsNetStateDifferent(clientInv.exAccessorySlot[i + this.LoadedSlotCount]))
				{
					ModAccessorySlotPlayer.NetHandler.SendSlot(-1, base.Player.whoAmI, i + this.LoadedSlotCount, this.exAccessorySlot[i + this.SlotCount], -1);
				}
				if (this.exDyesAccessory[i].IsNetStateDifferent(clientInv.exDyesAccessory[i]))
				{
					ModAccessorySlotPlayer.NetHandler.SendSlot(-1, base.Player.whoAmI, -i - 1, this.exDyesAccessory[i], -1);
				}
				if (this.exHideAccessory[i] != clientInv.exHideAccessory[i])
				{
					ModAccessorySlotPlayer.NetHandler.SendVisualState(-1, base.Player.whoAmI, i, this.exHideAccessory[i]);
				}
			}
			for (int loadoutIndex = 0; loadoutIndex < this.exLoadouts.Length; loadoutIndex++)
			{
				this.<SendClientChanges>g__SendClientChanges|28_0(this.exLoadouts[loadoutIndex], clientInv.exLoadouts[loadoutIndex]);
			}
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x0052F100 File Offset: 0x0052D300
		public override void OnEquipmentLoadoutSwitched(int oldLoadoutIndex, int loadoutIndex)
		{
			this.exLoadouts[oldLoadoutIndex].Swap(this);
			this.exLoadouts[loadoutIndex].Swap(this);
			if (base.Player.whoAmI == Main.myPlayer)
			{
				if (Main.netMode != 0)
				{
					this.CopyClientState(Main.clientPlayer.GetModPlayer<ModAccessorySlotPlayer>());
					for (int i = 0; i < this.LoadedSlotCount; i++)
					{
						ModAccessorySlotPlayer.NetHandler.SendVisualState(-1, base.Player.whoAmI, i, this.exHideAccessory[i]);
					}
				}
				this.DetectConflictsWithSharedSlots();
			}
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x0052F184 File Offset: 0x0052D384
		private void DetectConflictsWithSharedSlots()
		{
			bool anyConflict = false;
			for (int i = 0; i < this.exAccessorySlot.Length / 2; i++)
			{
				if (this.IsSharedSlot(i))
				{
					anyConflict |= ModAccessorySlotPlayer.Loader.IsAccessoryInConflict(base.Player, this.exAccessorySlot[i], i, -10);
					anyConflict |= ModAccessorySlotPlayer.Loader.IsAccessoryInConflict(base.Player, this.exAccessorySlot[i + this.SlotCount], i + this.SlotCount, -11);
				}
			}
			if (anyConflict)
			{
				Main.NewText(Language.GetTextValue("tModLoader.SharedAccessorySlotConflictMessage"), byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x0052F21C File Offset: 0x0052D41C
		internal bool IsSharedSlot(int slotType)
		{
			return this.sharedLoadoutSlotTypes[slotType % this.SlotCount];
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x0052F230 File Offset: 0x0052D430
		internal bool SharedSlotHasLoadoutConflict(int slotType, bool vanitySlot)
		{
			if (!this.IsSharedSlot(slotType))
			{
				return false;
			}
			int functional = slotType % this.SlotCount;
			int vanity = functional + this.SlotCount;
			if (vanitySlot)
			{
				return ModAccessorySlotPlayer.Loader.IsAccessoryInConflict(base.Player, this.exAccessorySlot[vanity], vanity, -11);
			}
			return ModAccessorySlotPlayer.Loader.IsAccessoryInConflict(base.Player, this.exAccessorySlot[functional], functional, -10);
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x0052F2B0 File Offset: 0x0052D4B0
		[CompilerGenerated]
		private void <CopyClientState>g__CopyState|26_0(ModAccessorySlotPlayer.ExEquipmentLoadout equipmentLoadout, ModAccessorySlotPlayer.ExEquipmentLoadout targetEquipmentLoadout)
		{
			for (int i = 0; i < this.LoadedSlotCount; i++)
			{
				equipmentLoadout.ExAccessorySlot[i].CopyNetStateTo(targetEquipmentLoadout.ExAccessorySlot[i]);
				equipmentLoadout.ExAccessorySlot[i + this.SlotCount].CopyNetStateTo(targetEquipmentLoadout.ExAccessorySlot[i + this.LoadedSlotCount]);
				equipmentLoadout.ExDyesAccessory[i].CopyNetStateTo(targetEquipmentLoadout.ExDyesAccessory[i]);
			}
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x0052F31C File Offset: 0x0052D51C
		[CompilerGenerated]
		private void <SyncPlayer>g__Sync|27_0(ModAccessorySlotPlayer.ExEquipmentLoadout loadout, ref ModAccessorySlotPlayer.<>c__DisplayClass27_0 A_2)
		{
			for (int slot = 0; slot < this.LoadedSlotCount; slot++)
			{
				ModAccessorySlotPlayer.NetHandler.SendSlot(A_2.toWho, base.Player.whoAmI, slot, loadout.ExAccessorySlot[slot], (sbyte)loadout.LoadoutIndex);
				ModAccessorySlotPlayer.NetHandler.SendSlot(A_2.toWho, base.Player.whoAmI, slot + this.LoadedSlotCount, loadout.ExAccessorySlot[slot + this.SlotCount], (sbyte)loadout.LoadoutIndex);
				ModAccessorySlotPlayer.NetHandler.SendSlot(A_2.toWho, base.Player.whoAmI, -slot - 1, loadout.ExDyesAccessory[slot], (sbyte)loadout.LoadoutIndex);
			}
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x0052F3C4 File Offset: 0x0052D5C4
		[CompilerGenerated]
		private void <SendClientChanges>g__SendClientChanges|28_0(ModAccessorySlotPlayer.ExEquipmentLoadout equipmentLoadout, ModAccessorySlotPlayer.ExEquipmentLoadout clientEquipmentLoadout)
		{
			for (int slot = 0; slot < this.LoadedSlotCount; slot++)
			{
				if (equipmentLoadout.ExAccessorySlot[slot].IsNetStateDifferent(clientEquipmentLoadout.ExAccessorySlot[slot]))
				{
					ModAccessorySlotPlayer.NetHandler.SendSlot(-1, base.Player.whoAmI, slot, equipmentLoadout.ExAccessorySlot[slot], (sbyte)equipmentLoadout.LoadoutIndex);
				}
				if (equipmentLoadout.ExAccessorySlot[slot + this.SlotCount].IsNetStateDifferent(clientEquipmentLoadout.ExAccessorySlot[slot + this.LoadedSlotCount]))
				{
					ModAccessorySlotPlayer.NetHandler.SendSlot(-1, base.Player.whoAmI, slot + this.LoadedSlotCount, equipmentLoadout.ExAccessorySlot[slot + this.SlotCount], (sbyte)equipmentLoadout.LoadoutIndex);
				}
				if (equipmentLoadout.ExDyesAccessory[slot].IsNetStateDifferent(clientEquipmentLoadout.ExDyesAccessory[slot]))
				{
					ModAccessorySlotPlayer.NetHandler.SendSlot(-1, base.Player.whoAmI, -slot - 1, equipmentLoadout.ExDyesAccessory[slot], (sbyte)equipmentLoadout.LoadoutIndex);
				}
			}
		}

		/// <summary> <see cref="T:Terraria.ModLoader.ModAccessorySlot" /> corollary to the accessory and vanity slots of <see cref="F:Terraria.Player.armor" />. </summary>
		// Token: 0x04001C53 RID: 7251
		internal Item[] exAccessorySlot;

		/// <summary> <see cref="T:Terraria.ModLoader.ModAccessorySlot" /> corollary to <see cref="F:Terraria.Player.dye" />. </summary>
		// Token: 0x04001C54 RID: 7252
		internal Item[] exDyesAccessory;

		/// <summary> <see cref="T:Terraria.ModLoader.ModAccessorySlot" /> corollary to <see cref="F:Terraria.Player.hideVisibleAccessory" />. </summary>
		// Token: 0x04001C55 RID: 7253
		internal bool[] exHideAccessory;

		/// <summary> All slots that can potentially show for this user. First the loaded ModAccessorySlots, followed by any unloaded slots. </summary>
		// Token: 0x04001C56 RID: 7254
		private readonly Dictionary<string, int> slots = new Dictionary<string, int>();

		/// <summary> Which slots are shared, indexed by ModAccessorySlot.Type </summary>
		// Token: 0x04001C57 RID: 7255
		private bool[] sharedLoadoutSlotTypes;

		/// <inheritdoc cref="T:Terraria.ModLoader.Default.ModAccessorySlotPlayer.ExEquipmentLoadout" />
		// Token: 0x04001C58 RID: 7256
		private ModAccessorySlotPlayer.ExEquipmentLoadout[] exLoadouts;

		/// <summary> Holds items from a saved <see cref="T:Terraria.ModLoader.ModAccessorySlot" /> that changed to not <see cref="P:Terraria.ModLoader.ModAccessorySlot.HasEquipmentLoadoutSupport" /> ("shared") and would otherwise be lost. Will be returned to the player when entering a world. </summary>
		// Token: 0x04001C59 RID: 7257
		private List<Item> extraItems = new List<Item>();

		// Token: 0x04001C5A RID: 7258
		internal bool scrollSlots;

		// Token: 0x04001C5B RID: 7259
		internal int scrollbarSlotPosition;

		/// <summary>
		/// <see cref="T:Terraria.ModLoader.Default.ModAccessorySlotPlayer.ExEquipmentLoadout" /> holds loadout items for loadouts not in use. <see cref="T:Terraria.ModLoader.ModAccessorySlot" /> corollary to <see cref="T:Terraria.EquipmentLoadout" />.
		/// Note that when a loadout is selected, the items in <see cref="T:Terraria.ModLoader.Default.ModAccessorySlotPlayer.ExEquipmentLoadout" /> are swapped into <see cref="F:Terraria.ModLoader.Default.ModAccessorySlotPlayer.exAccessorySlot" />, etc. and the loadout is left with empty Item instances. The active loadout items are stored on the player and the <see cref="T:Terraria.ModLoader.Default.ModAccessorySlotPlayer.ExEquipmentLoadout" /> is left empty, this is the same approach used for vanilla loadouts as well (<see cref="F:Terraria.Player.Loadouts" />).
		/// </summary>
		// Token: 0x02000A77 RID: 2679
		internal sealed class ExEquipmentLoadout
		{
			// Token: 0x0600590B RID: 22795 RVA: 0x006A0A30 File Offset: 0x0069EC30
			public ExEquipmentLoadout(int loadoutIndex, int slotCount, EquipmentLoadout loadoutReference)
			{
				this.LoadoutIndex = loadoutIndex;
				this.LoadoutReference = loadoutReference;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
				defaultInterpolatedStringHandler.AppendLiteral("loadout_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(loadoutIndex);
				this.identifier = defaultInterpolatedStringHandler.ToStringAndClear();
				this.ResetAndSizeAccessoryArrays(slotCount);
			}

			// Token: 0x17000910 RID: 2320
			// (get) Token: 0x0600590C RID: 22796 RVA: 0x006A0AA3 File Offset: 0x0069ECA3
			public int LoadoutIndex { get; }

			// Token: 0x17000911 RID: 2321
			// (get) Token: 0x0600590D RID: 22797 RVA: 0x006A0AAB File Offset: 0x0069ECAB
			public EquipmentLoadout LoadoutReference { get; }

			/// <inheritdoc cref="F:Terraria.ModLoader.Default.ModAccessorySlotPlayer.exAccessorySlot" />
			// Token: 0x17000912 RID: 2322
			// (get) Token: 0x0600590E RID: 22798 RVA: 0x006A0AB3 File Offset: 0x0069ECB3
			// (set) Token: 0x0600590F RID: 22799 RVA: 0x006A0ABB File Offset: 0x0069ECBB
			public Item[] ExAccessorySlot { get; private set; } = Array.Empty<Item>();

			/// <inheritdoc cref="F:Terraria.ModLoader.Default.ModAccessorySlotPlayer.exDyesAccessory" />
			// Token: 0x17000913 RID: 2323
			// (get) Token: 0x06005910 RID: 22800 RVA: 0x006A0AC4 File Offset: 0x0069ECC4
			// (set) Token: 0x06005911 RID: 22801 RVA: 0x006A0ACC File Offset: 0x0069ECCC
			public Item[] ExDyesAccessory { get; private set; } = Array.Empty<Item>();

			/// <inheritdoc cref="F:Terraria.ModLoader.Default.ModAccessorySlotPlayer.exHideAccessory" />
			// Token: 0x17000914 RID: 2324
			// (get) Token: 0x06005912 RID: 22802 RVA: 0x006A0AD5 File Offset: 0x0069ECD5
			// (set) Token: 0x06005913 RID: 22803 RVA: 0x006A0ADD File Offset: 0x0069ECDD
			public bool[] ExHideAccessory { get; private set; } = Array.Empty<bool>();

			// Token: 0x06005914 RID: 22804 RVA: 0x006A0AE8 File Offset: 0x0069ECE8
			public void SaveData(TagCompound tag)
			{
				string key = this.identifier;
				TagCompound tagCompound = new TagCompound();
				string key2 = "items";
				IEnumerable<Item> exAccessorySlot = this.ExAccessorySlot;
				Func<Item, TagCompound> selector;
				if ((selector = ModAccessorySlotPlayer.ExEquipmentLoadout.<>O.<0>__Save) == null)
				{
					selector = (ModAccessorySlotPlayer.ExEquipmentLoadout.<>O.<0>__Save = new Func<Item, TagCompound>(ItemIO.Save));
				}
				tagCompound[key2] = exAccessorySlot.Select(selector).ToList<TagCompound>();
				string key3 = "dyes";
				IEnumerable<Item> exDyesAccessory = this.ExDyesAccessory;
				Func<Item, TagCompound> selector2;
				if ((selector2 = ModAccessorySlotPlayer.ExEquipmentLoadout.<>O.<0>__Save) == null)
				{
					selector2 = (ModAccessorySlotPlayer.ExEquipmentLoadout.<>O.<0>__Save = new Func<Item, TagCompound>(ItemIO.Save));
				}
				tagCompound[key3] = exDyesAccessory.Select(selector2).ToList<TagCompound>();
				tagCompound["hidden"] = this.ExHideAccessory.ToList<bool>();
				tag[key] = tagCompound;
			}

			/// <summary>
			/// Loads data for this loadout and updates this instance accordingly.
			/// Returns a collection of <see cref="T:Terraria.Item" />s, which were not added to the loadout,
			/// because <see cref="P:Terraria.ModLoader.ModAccessorySlot.HasEquipmentLoadoutSupport" /> changed since the last save.
			/// </summary>
			/// <param name="tag">The <see cref="T:Terraria.ModLoader.IO.TagCompound" /> from which to load the data</param>
			/// <param name="order">Saved slot names in order.</param>
			/// <param name="slots">Slot name to slot info mapping.</param>
			/// <param name="sharedLoadoutSlotTypes">Slots that don't have loadout support</param>
			// Token: 0x06005915 RID: 22805 RVA: 0x006A0B88 File Offset: 0x0069ED88
			public IReadOnlyList<Item> LoadData(TagCompound tag, List<string> order, Dictionary<string, int> slots, bool[] sharedLoadoutSlotTypes)
			{
				List<Item> itemsToDrop = new List<Item>();
				this.ResetAndSizeAccessoryArrays(slots.Count);
				if (!tag.ContainsKey(this.identifier))
				{
					return itemsToDrop;
				}
				tag = tag.GetCompound(this.identifier);
				IEnumerable<TagCompound> list = tag.GetList<TagCompound>("items");
				Func<TagCompound, Item> selector;
				if ((selector = ModAccessorySlotPlayer.ExEquipmentLoadout.<>O.<1>__Load) == null)
				{
					selector = (ModAccessorySlotPlayer.ExEquipmentLoadout.<>O.<1>__Load = new Func<TagCompound, Item>(ItemIO.Load));
				}
				List<Item> items = list.Select(selector).ToList<Item>();
				IEnumerable<TagCompound> list2 = tag.GetList<TagCompound>("dyes");
				Func<TagCompound, Item> selector2;
				if ((selector2 = ModAccessorySlotPlayer.ExEquipmentLoadout.<>O.<1>__Load) == null)
				{
					selector2 = (ModAccessorySlotPlayer.ExEquipmentLoadout.<>O.<1>__Load = new Func<TagCompound, Item>(ItemIO.Load));
				}
				List<Item> dyes = list2.Select(selector2).ToList<Item>();
				List<bool> visible = tag.GetList<bool>("hidden").ToList<bool>();
				for (int i = 0; i < order.Count; i++)
				{
					int type = slots[order[i]];
					Item dye = dyes[i];
					Item accessory = items[i];
					Item vanityItem = items[i + order.Count];
					bool isHidden = visible[i];
					if (sharedLoadoutSlotTypes[type])
					{
						if (!accessory.IsAir)
						{
							itemsToDrop.Add(accessory);
						}
						if (!vanityItem.IsAir)
						{
							itemsToDrop.Add(vanityItem);
						}
						if (!dye.IsAir)
						{
							itemsToDrop.Add(dye);
						}
					}
					else
					{
						this.ExDyesAccessory[type] = dye;
						this.ExAccessorySlot[type + slots.Count] = vanityItem;
						this.ExAccessorySlot[type] = accessory;
						this.ExHideAccessory[type] = isHidden;
					}
				}
				return itemsToDrop;
			}

			// Token: 0x06005916 RID: 22806 RVA: 0x006A0D04 File Offset: 0x0069EF04
			private void ResetAndSizeAccessoryArrays(int size)
			{
				this.ExAccessorySlot = new Item[2 * size];
				this.ExDyesAccessory = new Item[size];
				this.ExHideAccessory = new bool[size];
				for (int i = 0; i < size; i++)
				{
					this.ExDyesAccessory[i] = new Item();
					this.ExHideAccessory[i] = false;
					this.ExAccessorySlot[i * 2] = new Item();
					this.ExAccessorySlot[i * 2 + 1] = new Item();
				}
			}

			// Token: 0x06005917 RID: 22807 RVA: 0x006A0D7C File Offset: 0x0069EF7C
			internal void Swap(ModAccessorySlotPlayer modAccessorySlotPlayer)
			{
				Item[] armor = modAccessorySlotPlayer.exAccessorySlot;
				for (int i = 0; i < armor.Length; i++)
				{
					if (!modAccessorySlotPlayer.IsSharedSlot(i))
					{
						Utils.Swap<Item>(ref armor[i], ref this.ExAccessorySlot[i]);
					}
				}
				Item[] dye = modAccessorySlotPlayer.exDyesAccessory;
				for (int j = 0; j < dye.Length; j++)
				{
					if (!modAccessorySlotPlayer.IsSharedSlot(j))
					{
						Utils.Swap<Item>(ref dye[j], ref this.ExDyesAccessory[j]);
					}
				}
				bool[] hideVisibleAccessory = modAccessorySlotPlayer.exHideAccessory;
				for (int k = 0; k < hideVisibleAccessory.Length; k++)
				{
					if (!modAccessorySlotPlayer.IsSharedSlot(k))
					{
						Utils.Swap<bool>(ref hideVisibleAccessory[k], ref this.ExHideAccessory[k]);
					}
				}
			}

			// Token: 0x04006D27 RID: 27943
			private readonly string identifier;

			// Token: 0x02000E28 RID: 3624
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04007C5B RID: 31835
				public static Func<Item, TagCompound> <0>__Save;

				// Token: 0x04007C5C RID: 31836
				public static Func<TagCompound, Item> <1>__Load;
			}
		}

		// Token: 0x02000A78 RID: 2680
		internal static class NetHandler
		{
			// Token: 0x06005918 RID: 22808 RVA: 0x006A0E3C File Offset: 0x0069F03C
			public static void SendSlot(int toWho, int plr, int slot, Item item, sbyte loadout = -1)
			{
				ModPacket p = ModLoaderMod.GetPacket(0);
				p.Write(1);
				if (Main.netMode == 2)
				{
					p.Write((byte)plr);
				}
				p.Write(loadout);
				p.Write((sbyte)slot);
				ItemIO.Send(item, p, true, false);
				p.Send(toWho, plr);
			}

			// Token: 0x06005919 RID: 22809 RVA: 0x006A0E88 File Offset: 0x0069F088
			private static void HandleSlot(BinaryReader r, int fromWho)
			{
				if (Main.netMode == 1)
				{
					fromWho = (int)r.ReadByte();
				}
				ModAccessorySlotPlayer dPlayer = Main.player[fromWho].GetModPlayer<ModAccessorySlotPlayer>();
				sbyte loadout = r.ReadSByte();
				sbyte slot = r.ReadSByte();
				Item item = ItemIO.Receive(r, true, false);
				ModAccessorySlotPlayer.NetHandler.SetSlot(loadout, slot, item, dPlayer);
				if (Main.netMode == 2)
				{
					ModAccessorySlotPlayer.NetHandler.SendSlot(-1, fromWho, (int)slot, item, loadout);
				}
			}

			// Token: 0x0600591A RID: 22810 RVA: 0x006A0EE4 File Offset: 0x0069F0E4
			public static void SendVisualState(int toWho, int plr, int slot, bool hideVisual)
			{
				ModPacket p = ModLoaderMod.GetPacket(0);
				p.Write(2);
				if (Main.netMode == 2)
				{
					p.Write((byte)plr);
				}
				p.Write((sbyte)slot);
				p.Write(hideVisual);
				p.Send(toWho, plr);
			}

			// Token: 0x0600591B RID: 22811 RVA: 0x006A0F28 File Offset: 0x0069F128
			private static void HandleVisualState(BinaryReader r, int fromWho)
			{
				if (Main.netMode == 1)
				{
					fromWho = (int)r.ReadByte();
				}
				ModAccessorySlotPlayer dPlayer = Main.player[fromWho].GetModPlayer<ModAccessorySlotPlayer>();
				sbyte slot = r.ReadSByte();
				dPlayer.exHideAccessory[(int)slot] = r.ReadBoolean();
				if (Main.netMode == 2)
				{
					ModAccessorySlotPlayer.NetHandler.SendVisualState(-1, fromWho, (int)slot, dPlayer.exHideAccessory[(int)slot]);
				}
			}

			// Token: 0x0600591C RID: 22812 RVA: 0x006A0F80 File Offset: 0x0069F180
			public static void HandlePacket(BinaryReader r, int fromWho)
			{
				byte b = r.ReadByte();
				if (b == 1)
				{
					ModAccessorySlotPlayer.NetHandler.HandleSlot(r, fromWho);
					return;
				}
				if (b != 2)
				{
					return;
				}
				ModAccessorySlotPlayer.NetHandler.HandleVisualState(r, fromWho);
			}

			// Token: 0x0600591D RID: 22813 RVA: 0x006A0FAC File Offset: 0x0069F1AC
			public static void SetSlot(sbyte loadout, sbyte slot, Item item, ModAccessorySlotPlayer dPlayer)
			{
				if (loadout == -1)
				{
					if (slot < 0)
					{
						dPlayer.exDyesAccessory[(int)(-(int)(slot + 1))] = item;
						return;
					}
					dPlayer.exAccessorySlot[(int)slot] = item;
					return;
				}
				else
				{
					ModAccessorySlotPlayer.ExEquipmentLoadout equipmentLoadout = dPlayer.exLoadouts[(int)loadout];
					if (slot < 0)
					{
						equipmentLoadout.ExDyesAccessory[(int)(-(int)(slot + 1))] = item;
						return;
					}
					equipmentLoadout.ExAccessorySlot[(int)slot] = item;
					return;
				}
			}

			// Token: 0x04006D2D RID: 27949
			public const byte InventorySlot = 1;

			// Token: 0x04006D2E RID: 27950
			public const byte VisualState = 2;

			// Token: 0x04006D2F RID: 27951
			public const byte Server = 2;

			// Token: 0x04006D30 RID: 27952
			public const byte Client = 1;

			// Token: 0x04006D31 RID: 27953
			public const byte SP = 0;
		}

		// Token: 0x02000A79 RID: 2681
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006D32 RID: 27954
			public static Func<Item, TagCompound> <0>__Save;

			// Token: 0x04006D33 RID: 27955
			public static Func<TagCompound, Item> <1>__Load;
		}
	}
}
