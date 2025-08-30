using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI.Chat;

namespace PetRenamer
{
	// Token: 0x02000007 RID: 7
	internal class PRPlayer : ModPlayer
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002A99 File Offset: 0x00000C99
		private bool OpenedChatWithMouseItem
		{
			get
			{
				return !Main.chatRelease && PetRenamer.IsPetItem(Main.mouseItem);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002AAE File Offset: 0x00000CAE
		private bool MouseItemChangedToPetItem
		{
			get
			{
				return this.prevItemType != Main.mouseItem.type && PetRenamer.IsPetItem(Main.mouseItem);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002ACE File Offset: 0x00000CCE
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (PetRenamer.RenamePetUIHotkey.JustPressed)
			{
				PRUISystem.ToggleRenamePetUI();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AE1 File Offset: 0x00000CE1
		public override void Initialize()
		{
			this.petTypeVanity = 0;
			this.petNameVanity = string.Empty;
			this.petTypeLight = 0;
			this.petNameLight = string.Empty;
			this.renamePetUIItem = new Item();
			this.prevItemType = 0;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B19 File Offset: 0x00000D19
		public override void SaveData(TagCompound tag)
		{
			if (!this.renamePetUIItem.IsAir)
			{
				tag.Add("renamePetUIItem", this.renamePetUIItem);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B39 File Offset: 0x00000D39
		public override void LoadData(TagCompound tag)
		{
			this.renamePetUIItem = tag.Get<Item>("renamePetUIItem");
			if (this.renamePetUIItem == null)
			{
				this.renamePetUIItem = new Item();
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B5F File Offset: 0x00000D5F
		public override void PostUpdateEquips()
		{
			this.UpdatePets();
			if (Main.netMode != 2 && Main.myPlayer == base.Player.whoAmI)
			{
				this.Autocomplete();
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B87 File Offset: 0x00000D87
		private void UpdatePets()
		{
			this.SetTypeAndNameOfCurrentEquippedPetInSlot(0, ref this.petTypeVanity, ref this.petNameVanity);
			this.SetTypeAndNameOfCurrentEquippedPetInSlot(1, ref this.petTypeLight, ref this.petNameLight);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BB0 File Offset: 0x00000DB0
		private void SetTypeAndNameOfCurrentEquippedPetInSlot(int slot, ref int type, ref string name)
		{
			Item item = base.Player.miscEquips[slot];
			PRItem petItem;
			if (!base.Player.hideMisc[slot] && item.TryGetGlobalItem<PRItem>(out petItem))
			{
				type = item.shoot;
				name = petItem.petName;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BF8 File Offset: 0x00000DF8
		private void Autocomplete()
		{
			if (!Config.Instance.EnableChatAutofill)
			{
				return;
			}
			if (Main.drawingPlayerChat && (this.OpenedChatWithMouseItem || this.MouseItemChangedToPetItem) && !Main.chatText.StartsWith(PRCommand.CommandStart) && Main.chatText.Length == 0)
			{
				ChatManager.AddChatText(FontAssets.MouseText.Value, PRCommand.CommandStart, Vector2.One);
			}
			Item mouseItem = Main.mouseItem;
			this.prevItemType = ((mouseItem != null) ? mouseItem.type : 0);
		}

		// Token: 0x0400001B RID: 27
		internal int petTypeVanity;

		// Token: 0x0400001C RID: 28
		internal string petNameVanity;

		// Token: 0x0400001D RID: 29
		internal int petTypeLight;

		// Token: 0x0400001E RID: 30
		internal string petNameLight;

		// Token: 0x0400001F RID: 31
		internal Item renamePetUIItem;

		// Token: 0x04000020 RID: 32
		private int prevItemType;
	}
}
