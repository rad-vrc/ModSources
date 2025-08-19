using System;
using Microsoft.Xna.Framework;
using QoLCompendium.Core.UI.Buttons;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace QoLCompendium.Core.UI.Panels
{
	// Token: 0x0200026E RID: 622
	public class AllInOneAccessUI : UIState
	{
		// Token: 0x06000F2F RID: 3887 RVA: 0x00074E5C File Offset: 0x0007305C
		public override void OnInitialize()
		{
			this.StoragePanel = new UIPanel();
			this.StoragePanel.Top.Set((float)(Main.screenHeight / 2), 0f);
			this.StoragePanel.Left.Set((float)(Main.screenWidth / 2 + 47), 0f);
			this.StoragePanel.Width.Set(200f, 0f);
			this.StoragePanel.Height.Set(200f, 0f);
			this.StoragePanel.BackgroundColor *= 0f;
			this.StoragePanel.BorderColor *= 0f;
			base.Append(this.StoragePanel);
			AllInOneAccessButton.backgroundTexture = 0;
			AllInOneAccessButton piggyBank = new AllInOneAccessButton(Common.GetAsset("Storages", "Storage_", AllInOneAccessButton.storageTexture = 0));
			piggyBank.Left.Set(0f, 0f);
			piggyBank.Top.Set(0f, 0f);
			piggyBank.Width.Set(40f, 0f);
			piggyBank.Height.Set(40f, 0f);
			piggyBank.OnLeftClick += this.PiggyBankClicked;
			piggyBank.Tooltip = UISystem.PiggyBankText;
			this.StoragePanel.Append(piggyBank);
			AllInOneAccessButton.backgroundTexture = 1;
			AllInOneAccessButton safe = new AllInOneAccessButton(Common.GetAsset("Storages", "Storage_", AllInOneAccessButton.storageTexture = 1));
			safe.Left.Set(40f, 0f);
			safe.Top.Set(0f, 0f);
			safe.Width.Set(40f, 0f);
			safe.Height.Set(40f, 0f);
			safe.OnLeftClick += this.SafeClicked;
			safe.Tooltip = UISystem.SafeText;
			this.StoragePanel.Append(safe);
			AllInOneAccessButton.backgroundTexture = 1;
			AllInOneAccessButton defendersForge = new AllInOneAccessButton(Common.GetAsset("Storages", "Storage_", AllInOneAccessButton.storageTexture = 2));
			defendersForge.Left.Set(80f, 0f);
			defendersForge.Top.Set(0f, 0f);
			defendersForge.Width.Set(40f, 0f);
			defendersForge.Height.Set(40f, 0f);
			defendersForge.OnLeftClick += this.DefendersForgeClicked;
			defendersForge.Tooltip = UISystem.DefendersForgeText;
			this.StoragePanel.Append(defendersForge);
			AllInOneAccessButton.backgroundTexture = 1;
			AllInOneAccessButton voidVault = new AllInOneAccessButton(Common.GetAsset("Storages", "Storage_", AllInOneAccessButton.storageTexture = 3));
			voidVault.Left.Set(120f, 0f);
			voidVault.Top.Set(0f, 0f);
			voidVault.Width.Set(40f, 0f);
			voidVault.Height.Set(40f, 0f);
			voidVault.OnLeftClick += this.VoidVaultClicked;
			voidVault.Tooltip = UISystem.VoidVaultText;
			this.StoragePanel.Append(voidVault);
			GenericUIButton.backgroundTexture = 2;
			GenericUIButton closeButton = new GenericUIButton(Common.GetAsset("Buttons", "Button_Small_", GenericUIButton.buttonTexture = 3));
			closeButton.Left.Set(160f, 0f);
			closeButton.Top.Set(0f, 0f);
			closeButton.Width.Set(40f, 0f);
			closeButton.Height.Set(40f, 0f);
			closeButton.OnLeftClick += this.CloseButtonClicked;
			closeButton.Tooltip = UISystem.CloseText;
			this.StoragePanel.Append(closeButton);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0007524A File Offset: 0x0007344A
		private void PiggyBankClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			AllInOneAccessUI.StorageClick(-2, SoundID.Item59);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00075258 File Offset: 0x00073458
		private void SafeClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			AllInOneAccessUI.StorageClick(-3, SoundID.MenuOpen);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00075266 File Offset: 0x00073466
		private void DefendersForgeClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			AllInOneAccessUI.StorageClick(-4, SoundID.MenuOpen);
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00075274 File Offset: 0x00073474
		private void VoidVaultClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			AllInOneAccessUI.StorageClick(-5, SoundID.Item130);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00075284 File Offset: 0x00073484
		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - AllInOneAccessUI.timeStart >= 10U)
			{
				SoundEngine.PlaySound(SoundID.MenuClose, null, null);
				AllInOneAccessUI.visible = false;
			}
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x000752BC File Offset: 0x000734BC
		public static void StorageClick(int chest, SoundStyle sound)
		{
			if (Main.GameUpdateCount - AllInOneAccessUI.timeStart >= 10U)
			{
				Main.playerInventory = true;
				Main.LocalPlayer.chest = chest;
				QoLCompendium.LastOpenedBank = new int?(chest);
				Point pos = Main.LocalPlayer.Center.ToTileCoordinates();
				Main.LocalPlayer.chestX = pos.X;
				Main.LocalPlayer.chestY = pos.Y;
				Main.oldNPCShop = 0;
				Main.LocalPlayer.SetTalkNPC(-1, false);
				Main.SetNPCShopIndex(0);
				SoundEngine.PlaySound(sound, new Vector2?(Main.LocalPlayer.position), null);
				Recipe.FindRecipes(false);
			}
		}

		// Token: 0x0400062C RID: 1580
		public UIPanel StoragePanel;

		// Token: 0x0400062D RID: 1581
		public static bool visible;

		// Token: 0x0400062E RID: 1582
		public static uint timeStart;
	}
}
