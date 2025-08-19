using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Net;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.Creative
{
	// Token: 0x02000645 RID: 1605
	public class CreativeUI
	{
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x0600466E RID: 18030 RVA: 0x00631B1D File Offset: 0x0062FD1D
		// (set) Token: 0x0600466F RID: 18031 RVA: 0x00631B25 File Offset: 0x0062FD25
		public bool Enabled { get; private set; }

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06004670 RID: 18032 RVA: 0x00631B2E File Offset: 0x0062FD2E
		public bool Blocked
		{
			get
			{
				return Main.LocalPlayer.talkNPC != -1 || Main.LocalPlayer.chest != -1;
			}
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x00631B50 File Offset: 0x0062FD50
		public CreativeUI()
		{
			for (int i = 0; i < this._itemSlotsForUI.Length; i++)
			{
				this._itemSlotsForUI[i] = new Item();
			}
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x00631BAC File Offset: 0x0062FDAC
		public void Initialize()
		{
			this._buttonTexture = Main.Assets.Request<Texture2D>("Images/UI/Creative/Journey_Toggle");
			this._buttonBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Creative/Journey_Toggle_MouseOver");
			this._itemIdsAvailableInfinitely.Clear();
			this._uiState = new UICreativePowersMenu();
			this._powersUI.SetState(this._uiState);
			this._initialized = true;
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x00631C11 File Offset: 0x0062FE11
		public void Update(GameTime gameTime)
		{
			if (this.Enabled && Main.playerInventory)
			{
				this._powersUI.Update(gameTime);
			}
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x00631C30 File Offset: 0x0062FE30
		public void Draw(SpriteBatch spriteBatch)
		{
			if (!this._initialized)
			{
				this.Initialize();
			}
			if (Main.LocalPlayer.difficulty != 3)
			{
				this.Enabled = false;
				return;
			}
			if (!this.Blocked)
			{
				Vector2 location;
				location..ctor(28f, 267f);
				Vector2 vector = new Vector2(353f, 258f);
				new Vector2(40f, 267f);
				vector + new Vector2(50f, 50f);
				if (Main.screenHeight < 650 && this.Enabled)
				{
					location.X += 52f * Main.inventoryScale;
				}
				this.DrawToggleButton(spriteBatch, location);
				if (this.Enabled)
				{
					this._powersUI.Draw(spriteBatch, Main.gameTimeCache);
				}
			}
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x00631CFB File Offset: 0x0062FEFB
		public UIElement ProvideItemSlotElement(int itemSlotContext)
		{
			if (itemSlotContext != 0)
			{
				return null;
			}
			return new UIItemSlot(this._itemSlotsForUI, itemSlotContext, 30);
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x00631D10 File Offset: 0x0062FF10
		public Item GetItemByIndex(int itemSlotContext)
		{
			if (itemSlotContext != 0)
			{
				return null;
			}
			return this._itemSlotsForUI[itemSlotContext];
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x00631D1F File Offset: 0x0062FF1F
		public void SetItembyIndex(Item item, int itemSlotContext)
		{
			if (itemSlotContext == 0)
			{
				this._itemSlotsForUI[itemSlotContext] = item;
			}
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x00631D30 File Offset: 0x0062FF30
		private void DrawToggleButton(SpriteBatch spritebatch, Vector2 location)
		{
			Vector2 vector = this._buttonTexture.Size();
			Rectangle rectangle = Utils.CenteredRectangle(location + vector / 2f, vector);
			UILinkPointNavigator.SetPosition(311, rectangle.Center.ToVector2());
			spritebatch.Draw(this._buttonTexture.Value, location, null, Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
			Main.LocalPlayer.creativeInterface = false;
			if (rectangle.Contains(Main.MouseScreen.ToPoint()))
			{
				Main.LocalPlayer.creativeInterface = true;
				Main.LocalPlayer.mouseInterface = true;
				if (this.Enabled)
				{
					Main.instance.MouseText(Language.GetTextValue("CreativePowers.PowersMenuOpen"), 0, 0, -1, -1, -1, -1, 0);
				}
				else
				{
					Main.instance.MouseText(Language.GetTextValue("CreativePowers.PowersMenuClosed"), 0, 0, -1, -1, -1, -1, 0);
				}
				spritebatch.Draw(this._buttonBorderTexture.Value, location, null, Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					this.ToggleMenu();
				}
			}
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x00631E6C File Offset: 0x0063006C
		public void SwapItem(ref Item item)
		{
			Utils.Swap<Item>(ref item, ref this._itemSlotsForUI[0]);
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x00631E80 File Offset: 0x00630080
		public void CloseMenu()
		{
			this.Enabled = false;
			if (this._itemSlotsForUI[0].stack > 0)
			{
				this._itemSlotsForUI[0] = Main.LocalPlayer.GetItem(Main.myPlayer, this._itemSlotsForUI[0], GetItemSettings.InventoryUIToInventorySettings);
			}
			this.StopPlayingSacrificeAnimations();
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x00631ED0 File Offset: 0x006300D0
		public void ToggleMenu()
		{
			this.Enabled = !this.Enabled;
			this._powersUI.EscapeElements();
			UISliderBase.EscapeElements();
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			if (this.Enabled)
			{
				Recipe.FindRecipes(false);
				Main.LocalPlayer.tileEntityAnchor.Clear();
				this.RefreshAvailableInfiniteItemsList();
				return;
			}
			if (this._itemSlotsForUI[0].stack > 0)
			{
				this._itemSlotsForUI[0] = Main.LocalPlayer.GetItem(Main.myPlayer, this._itemSlotsForUI[0], GetItemSettings.InventoryUIToInventorySettings);
				this.StopPlayingSacrificeAnimations();
			}
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x00631F6F File Offset: 0x0063016F
		public bool IsShowingResearchMenu()
		{
			return this.Enabled && this._uiState != null && this._uiState.IsShowingResearchMenu;
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x00631F8E File Offset: 0x0063018E
		public void SacrificeItemInSacrificeSlot()
		{
			if (this._uiState != null)
			{
				this._uiState.SacrificeWhatsInResearchMenu();
			}
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x00631FA3 File Offset: 0x006301A3
		public void StopPlayingSacrificeAnimations()
		{
			if (this._uiState != null)
			{
				this._uiState.StopPlayingResearchAnimations();
			}
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x00631FB8 File Offset: 0x006301B8
		public bool ShouldDrawSacrificeArea()
		{
			if (!this._itemSlotsForUI[0].IsAir)
			{
				return true;
			}
			Item mouseItem = Main.mouseItem;
			int amountNeeded;
			return !mouseItem.IsAir && CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(mouseItem.type, out amountNeeded) && Main.LocalPlayerCreativeTracker.ItemSacrifices.GetSacrificeCount(mouseItem.type) < amountNeeded;
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x00632018 File Offset: 0x00630218
		public bool GetSacrificeNumbers(out int itemIdChecked, out int amountWeHave, out int amountNeededTotal)
		{
			amountWeHave = 0;
			amountNeededTotal = 0;
			itemIdChecked = 0;
			Item item = this._itemSlotsForUI[0];
			if (!item.IsAir)
			{
				itemIdChecked = item.type;
			}
			return Main.LocalPlayerCreativeTracker.ItemSacrifices.TryGetSacrificeNumbers(item.type, out amountWeHave, out amountNeededTotal);
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x00632063 File Offset: 0x00630263
		public CreativeUI.ItemSacrificeResult SacrificeItem(out int amountWeSacrificed)
		{
			return CreativeUI.SacrificeItem(ref this._itemSlotsForUI[0], out amountWeSacrificed, false);
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x00632078 File Offset: 0x00630278
		public static CreativeUI.ItemSacrificeResult SacrificeItem(Item toSacrifice, out int amountWeSacrificed)
		{
			return CreativeUI.SacrificeItem(ref toSacrifice, out amountWeSacrificed, false);
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x00632084 File Offset: 0x00630284
		public static CreativeUI.ItemSacrificeResult SacrificeItem(ref Item item, out int amountWeSacrificed, bool returnRemainderToPlayer = false)
		{
			int amountNeededTotal = 0;
			int amountWeHave = 0;
			amountWeSacrificed = 0;
			if (!ItemLoader.CanResearch(item))
			{
				return CreativeUI.ItemSacrificeResult.CannotSacrifice;
			}
			if (!Main.LocalPlayerCreativeTracker.ItemSacrifices.TryGetSacrificeNumbers(item.type, out amountWeHave, out amountNeededTotal))
			{
				return CreativeUI.ItemSacrificeResult.CannotSacrifice;
			}
			int num = Utils.Clamp<int>(amountNeededTotal - amountWeHave, 0, amountNeededTotal);
			if (num == 0)
			{
				return CreativeUI.ItemSacrificeResult.CannotSacrifice;
			}
			int num2 = Math.Min(num, item.stack);
			if (!Main.ServerSideCharacter)
			{
				Main.LocalPlayerCreativeTracker.ItemSacrifices.RegisterItemSacrifice(item.type, num2);
			}
			else
			{
				NetPacket packet = NetCreativeUnlocksPlayerReportModule.SerializeSacrificeRequest(item.type, num2);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}
			bool num3 = num2 == num;
			ItemLoader.OnResearched(item, num3);
			item.stack -= num2;
			if (item.stack <= 0)
			{
				item.TurnToAir(false);
			}
			amountWeSacrificed = num2;
			Main.CreativeMenu.RefreshAvailableInfiniteItemsList();
			if (item.stack > 0 && returnRemainderToPlayer)
			{
				item.position.X = Main.player[Main.myPlayer].Center.X - (float)(item.width / 2);
				item.position.Y = Main.player[Main.myPlayer].Center.Y - (float)(item.height / 2);
				item = Main.LocalPlayer.GetItem(Main.myPlayer, item, GetItemSettings.InventoryUIToInventorySettings);
			}
			if (!num3)
			{
				return CreativeUI.ItemSacrificeResult.SacrificedButNotDone;
			}
			return CreativeUI.ItemSacrificeResult.SacrificedAndDone;
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x006321DC File Offset: 0x006303DC
		private void RefreshAvailableInfiniteItemsList()
		{
			this._itemIdsAvailableInfinitely.Clear();
			Main.LocalPlayerCreativeTracker.ItemSacrifices.FillListOfItemsThatCanBeObtainedInfinitely(this._itemIdsAvailableInfinitely);
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x00632200 File Offset: 0x00630400
		public void Reset()
		{
			for (int i = 0; i < this._itemSlotsForUI.Length; i++)
			{
				this._itemSlotsForUI[i].TurnToAir(false);
			}
			this._initialized = false;
			this.Enabled = false;
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x0063223C File Offset: 0x0063043C
		public static CreativeUI.ItemSacrificeResult ResearchItem(int type)
		{
			int amountNeeded;
			if (!CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(type, out amountNeeded))
			{
				return CreativeUI.ItemSacrificeResult.CannotSacrifice;
			}
			int num;
			return CreativeUI.SacrificeItem(new Item(type, amountNeeded, 0), out num);
		}

		/// <summary>
		/// Method that allows you to easily get how many items of a type you have researched so far
		/// </summary>
		/// <param name="type">The item type to check.</param>
		/// <param name="fullyResearched">True if it is fully researched.</param>
		/// <returns></returns>
		// Token: 0x06004687 RID: 18055 RVA: 0x0063226C File Offset: 0x0063046C
		public static int GetSacrificeCount(int type, out bool fullyResearched)
		{
			fullyResearched = false;
			int amountNeeded;
			if (!CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(type, out amountNeeded))
			{
				return 0;
			}
			int amountSacrificed;
			Main.LocalPlayerCreativeTracker.ItemSacrifices._sacrificesCountByItemIdCache.TryGetValue(type, out amountSacrificed);
			fullyResearched = (amountSacrificed >= amountNeeded);
			return amountSacrificed;
		}

		/// <summary>
		/// Method that allows you to easily get how many items of a type you need to fully research that item
		/// </summary>
		/// <param name="type">The item type to check.</param>
		/// <returns>The number of sacrifices remaining , or null if the item can never be unlocked.</returns>
		// Token: 0x06004688 RID: 18056 RVA: 0x006322B0 File Offset: 0x006304B0
		public static int? GetSacrificesRemaining(int type)
		{
			int amountNeeded;
			if (!CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(type, out amountNeeded))
			{
				return null;
			}
			int amountSacrificed;
			Main.LocalPlayerCreativeTracker.ItemSacrifices._sacrificesCountByItemIdCache.TryGetValue(type, out amountSacrificed);
			return new int?(Utils.Clamp<int>(amountNeeded - amountSacrificed, 0, amountNeeded));
		}

		// Token: 0x04005B77 RID: 23415
		public const int ItemSlotIndexes_SacrificeItem = 0;

		// Token: 0x04005B78 RID: 23416
		public const int ItemSlotIndexes_Count = 1;

		// Token: 0x04005B79 RID: 23417
		private bool _initialized;

		// Token: 0x04005B7A RID: 23418
		private Asset<Texture2D> _buttonTexture;

		// Token: 0x04005B7B RID: 23419
		private Asset<Texture2D> _buttonBorderTexture;

		// Token: 0x04005B7C RID: 23420
		private Item[] _itemSlotsForUI = new Item[1];

		// Token: 0x04005B7D RID: 23421
		private List<int> _itemIdsAvailableInfinitely = new List<int>();

		// Token: 0x04005B7E RID: 23422
		private UserInterface _powersUI = new UserInterface();

		// Token: 0x04005B7F RID: 23423
		public int GamepadPointIdForInfiniteItemSearchHack = -1;

		// Token: 0x04005B80 RID: 23424
		public bool GamepadMoveToSearchButtonHack;

		// Token: 0x04005B81 RID: 23425
		private UICreativePowersMenu _uiState;

		// Token: 0x02000CF2 RID: 3314
		public enum ItemSacrificeResult
		{
			// Token: 0x04007A91 RID: 31377
			CannotSacrifice,
			// Token: 0x04007A92 RID: 31378
			SacrificedButNotDone,
			// Token: 0x04007A93 RID: 31379
			SacrificedAndDone
		}
	}
}
