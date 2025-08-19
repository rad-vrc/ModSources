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
using Terraria.Net;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.Creative
{
	// Token: 0x020002B9 RID: 697
	public class CreativeUI
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x00542689 File Offset: 0x00540889
		// (set) Token: 0x06002247 RID: 8775 RVA: 0x00542691 File Offset: 0x00540891
		public bool Enabled { get; private set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x0054269A File Offset: 0x0054089A
		public bool Blocked
		{
			get
			{
				return Main.LocalPlayer.talkNPC != -1 || Main.LocalPlayer.chest != -1;
			}
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x005426BC File Offset: 0x005408BC
		public CreativeUI()
		{
			for (int i = 0; i < this._itemSlotsForUI.Length; i++)
			{
				this._itemSlotsForUI[i] = new Item();
			}
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00542718 File Offset: 0x00540918
		public void Initialize()
		{
			this._buttonTexture = Main.Assets.Request<Texture2D>("Images/UI/Creative/Journey_Toggle", 1);
			this._buttonBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Creative/Journey_Toggle_MouseOver", 1);
			this._itemIdsAvailableInfinitely.Clear();
			this._uiState = new UICreativePowersMenu();
			this._powersUI.SetState(this._uiState);
			this._initialized = true;
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x0054277F File Offset: 0x0054097F
		public void Update(GameTime gameTime)
		{
			if (!this.Enabled)
			{
				return;
			}
			if (!Main.playerInventory)
			{
				return;
			}
			this._powersUI.Update(gameTime);
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x005427A0 File Offset: 0x005409A0
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
			if (this.Blocked)
			{
				return;
			}
			Vector2 location = new Vector2(28f, 267f);
			Vector2 value = new Vector2(353f, 258f);
			new Vector2(40f, 267f);
			value + new Vector2(50f, 50f);
			if (Main.screenHeight < 650 && this.Enabled)
			{
				location.X += 52f * Main.inventoryScale;
			}
			this.DrawToggleButton(spriteBatch, location);
			if (!this.Enabled)
			{
				return;
			}
			this._powersUI.Draw(spriteBatch, Main.gameTimeCache);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0054286A File Offset: 0x00540A6A
		public UIElement ProvideItemSlotElement(int itemSlotContext)
		{
			if (itemSlotContext != 0)
			{
				return null;
			}
			return new UIItemSlot(this._itemSlotsForUI, itemSlotContext, 30);
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x0054287F File Offset: 0x00540A7F
		public Item GetItemByIndex(int itemSlotContext)
		{
			if (itemSlotContext != 0)
			{
				return null;
			}
			return this._itemSlotsForUI[itemSlotContext];
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x0054288E File Offset: 0x00540A8E
		public void SetItembyIndex(Item item, int itemSlotContext)
		{
			if (itemSlotContext == 0)
			{
				this._itemSlotsForUI[itemSlotContext] = item;
			}
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x0054289C File Offset: 0x00540A9C
		private void DrawToggleButton(SpriteBatch spritebatch, Vector2 location)
		{
			Vector2 vector = this._buttonTexture.Size();
			Rectangle rectangle = Utils.CenteredRectangle(location + vector / 2f, vector);
			UILinkPointNavigator.SetPosition(311, rectangle.Center.ToVector2());
			spritebatch.Draw(this._buttonTexture.Value, location, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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
				spritebatch.Draw(this._buttonBorderTexture.Value, location, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					this.ToggleMenu();
				}
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x005429D8 File Offset: 0x00540BD8
		public void SwapItem(ref Item item)
		{
			Utils.Swap<Item>(ref item, ref this._itemSlotsForUI[0]);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x005429EC File Offset: 0x00540BEC
		public void CloseMenu()
		{
			this.Enabled = false;
			if (this._itemSlotsForUI[0].stack > 0)
			{
				this._itemSlotsForUI[0] = Main.LocalPlayer.GetItem(Main.myPlayer, this._itemSlotsForUI[0], GetItemSettings.InventoryUIToInventorySettings);
			}
			this.StopPlayingSacrificeAnimations();
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x00542A3C File Offset: 0x00540C3C
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

		// Token: 0x06002254 RID: 8788 RVA: 0x00542ADB File Offset: 0x00540CDB
		public bool IsShowingResearchMenu()
		{
			return this.Enabled && this._uiState != null && this._uiState.IsShowingResearchMenu;
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x00542AFA File Offset: 0x00540CFA
		public void SacrificeItemInSacrificeSlot()
		{
			if (this._uiState == null)
			{
				return;
			}
			this._uiState.SacrificeWhatsInResearchMenu();
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x00542B10 File Offset: 0x00540D10
		public void StopPlayingSacrificeAnimations()
		{
			if (this._uiState == null)
			{
				return;
			}
			this._uiState.StopPlayingResearchAnimations();
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x00542B28 File Offset: 0x00540D28
		public bool ShouldDrawSacrificeArea()
		{
			if (!this._itemSlotsForUI[0].IsAir)
			{
				return true;
			}
			Item mouseItem = Main.mouseItem;
			int num;
			return !mouseItem.IsAir && CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(mouseItem.type, out num) && Main.LocalPlayerCreativeTracker.ItemSacrifices.GetSacrificeCount(mouseItem.type) < num;
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x00542B88 File Offset: 0x00540D88
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

		// Token: 0x06002259 RID: 8793 RVA: 0x00542BD4 File Offset: 0x00540DD4
		public CreativeUI.ItemSacrificeResult SacrificeItem(out int amountWeSacrificed)
		{
			int num = 0;
			int num2 = 0;
			amountWeSacrificed = 0;
			Item item = this._itemSlotsForUI[0];
			if (!Main.LocalPlayerCreativeTracker.ItemSacrifices.TryGetSacrificeNumbers(item.type, out num2, out num))
			{
				return CreativeUI.ItemSacrificeResult.CannotSacrifice;
			}
			int num3 = Utils.Clamp<int>(num - num2, 0, num);
			if (num3 == 0)
			{
				return CreativeUI.ItemSacrificeResult.CannotSacrifice;
			}
			int num4 = Math.Min(num3, item.stack);
			if (!Main.ServerSideCharacter)
			{
				Main.LocalPlayerCreativeTracker.ItemSacrifices.RegisterItemSacrifice(item.type, num4);
			}
			else
			{
				NetPacket packet = NetCreativeUnlocksPlayerReportModule.SerializeSacrificeRequest(item.type, num4);
				NetManager.Instance.SendToServerOrLoopback(packet);
			}
			bool flag = num4 == num3;
			item.stack -= num4;
			if (item.stack <= 0)
			{
				item.TurnToAir(false);
			}
			amountWeSacrificed = num4;
			this.RefreshAvailableInfiniteItemsList();
			if (item.stack > 0)
			{
				item.position.X = Main.player[Main.myPlayer].Center.X - (float)(item.width / 2);
				item.position.Y = Main.player[Main.myPlayer].Center.Y - (float)(item.height / 2);
				this._itemSlotsForUI[0] = Main.LocalPlayer.GetItem(Main.myPlayer, item, GetItemSettings.InventoryUIToInventorySettings);
			}
			if (!flag)
			{
				return CreativeUI.ItemSacrificeResult.SacrificedButNotDone;
			}
			return CreativeUI.ItemSacrificeResult.SacrificedAndDone;
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x00542D14 File Offset: 0x00540F14
		private void RefreshAvailableInfiniteItemsList()
		{
			this._itemIdsAvailableInfinitely.Clear();
			Main.LocalPlayerCreativeTracker.ItemSacrifices.FillListOfItemsThatCanBeObtainedInfinitely(this._itemIdsAvailableInfinitely);
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x00542D38 File Offset: 0x00540F38
		public void Reset()
		{
			for (int i = 0; i < this._itemSlotsForUI.Length; i++)
			{
				this._itemSlotsForUI[i].TurnToAir(false);
			}
			this._initialized = false;
			this.Enabled = false;
		}

		// Token: 0x040047BC RID: 18364
		public const int ItemSlotIndexes_SacrificeItem = 0;

		// Token: 0x040047BD RID: 18365
		public const int ItemSlotIndexes_Count = 1;

		// Token: 0x040047BF RID: 18367
		private bool _initialized;

		// Token: 0x040047C0 RID: 18368
		private Asset<Texture2D> _buttonTexture;

		// Token: 0x040047C1 RID: 18369
		private Asset<Texture2D> _buttonBorderTexture;

		// Token: 0x040047C2 RID: 18370
		private Item[] _itemSlotsForUI = new Item[1];

		// Token: 0x040047C3 RID: 18371
		private List<int> _itemIdsAvailableInfinitely = new List<int>();

		// Token: 0x040047C4 RID: 18372
		private UserInterface _powersUI = new UserInterface();

		// Token: 0x040047C5 RID: 18373
		public int GamepadPointIdForInfiniteItemSearchHack = -1;

		// Token: 0x040047C6 RID: 18374
		public bool GamepadMoveToSearchButtonHack;

		// Token: 0x040047C7 RID: 18375
		private UICreativePowersMenu _uiState;

		// Token: 0x02000699 RID: 1689
		public enum ItemSacrificeResult
		{
			// Token: 0x040061D4 RID: 25044
			CannotSacrifice,
			// Token: 0x040061D5 RID: 25045
			SacrificedButNotDone,
			// Token: 0x040061D6 RID: 25046
			SacrificedAndDone
		}
	}
}
