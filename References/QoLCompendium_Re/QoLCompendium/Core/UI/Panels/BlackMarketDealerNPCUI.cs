using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Content.NPCs;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace QoLCompendium.Core.UI.Panels
{
	// Token: 0x0200026F RID: 623
	public class BlackMarketDealerNPCUI : UIState
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x00075368 File Offset: 0x00073568
		public override void OnInitialize()
		{
			this.ShopPanel = new UIPanel();
			this.ShopPanel.SetPadding(0f);
			this.ShopPanel.Left.Set(575f, 0f);
			this.ShopPanel.Top.Set(275f, 0f);
			this.ShopPanel.Width.Set(385f, 0f);
			this.ShopPanel.Height.Set(460f, 0f);
			this.ShopPanel.BackgroundColor = new Color(73, 94, 171);
			this.ShopPanel.OnLeftMouseDown += this.DragStart;
			this.ShopPanel.OnLeftMouseUp += this.DragEnd;
			UIText potionText = new UIText(UISystem.BMPotionText, 1f, false);
			potionText.Left.Set(35f, 0f);
			potionText.Top.Set(10f, 0f);
			potionText.Width.Set(60f, 0f);
			potionText.Height.Set(22f, 0f);
			this.ShopPanel.Append(potionText);
			UIText stationText = new UIText(UISystem.BMStationText, 1f, false);
			stationText.Left.Set(35f, 0f);
			stationText.Top.Set(40f, 0f);
			stationText.Width.Set(60f, 0f);
			stationText.Height.Set(22f, 0f);
			this.ShopPanel.Append(stationText);
			UIText materialText = new UIText(UISystem.BMMaterialText, 1f, false);
			materialText.Left.Set(35f, 0f);
			materialText.Top.Set(70f, 0f);
			materialText.Width.Set(60f, 0f);
			materialText.Height.Set(22f, 0f);
			this.ShopPanel.Append(materialText);
			UIText movementText = new UIText(UISystem.BMMovementAccessoryText, 1f, false);
			movementText.Left.Set(35f, 0f);
			movementText.Top.Set(100f, 0f);
			movementText.Width.Set(60f, 0f);
			movementText.Height.Set(22f, 0f);
			this.ShopPanel.Append(movementText);
			UIText combatText = new UIText(UISystem.BMCombatAccessoryText, 1f, false);
			combatText.Left.Set(35f, 0f);
			combatText.Top.Set(130f, 0f);
			combatText.Width.Set(60f, 0f);
			combatText.Height.Set(22f, 0f);
			this.ShopPanel.Append(combatText);
			UIText infoText = new UIText(UISystem.BMInformativeText, 1f, false);
			infoText.Left.Set(35f, 0f);
			infoText.Top.Set(160f, 0f);
			infoText.Width.Set(60f, 0f);
			infoText.Height.Set(22f, 0f);
			this.ShopPanel.Append(infoText);
			UIText bagText = new UIText(UISystem.BMBagText, 1f, false);
			bagText.Left.Set(35f, 0f);
			bagText.Top.Set(190f, 0f);
			bagText.Width.Set(60f, 0f);
			bagText.Height.Set(22f, 0f);
			this.ShopPanel.Append(bagText);
			UIText crateText = new UIText(UISystem.BMCrateText, 1f, false);
			crateText.Left.Set(35f, 0f);
			crateText.Top.Set(220f, 0f);
			crateText.Width.Set(60f, 0f);
			crateText.Height.Set(22f, 0f);
			this.ShopPanel.Append(crateText);
			UIText oreText = new UIText(UISystem.BMOreText, 1f, false);
			oreText.Left.Set(35f, 0f);
			oreText.Top.Set(250f, 0f);
			oreText.Width.Set(60f, 0f);
			oreText.Height.Set(22f, 0f);
			this.ShopPanel.Append(oreText);
			UIText naturalText = new UIText(UISystem.BMNaturalBlockText, 1f, false);
			naturalText.Left.Set(35f, 0f);
			naturalText.Top.Set(280f, 0f);
			naturalText.Width.Set(60f, 0f);
			naturalText.Height.Set(22f, 0f);
			this.ShopPanel.Append(naturalText);
			UIText buildingText = new UIText(UISystem.BMBuildingBlockText, 1f, false);
			buildingText.Left.Set(35f, 0f);
			buildingText.Top.Set(310f, 0f);
			buildingText.Width.Set(60f, 0f);
			buildingText.Height.Set(22f, 0f);
			this.ShopPanel.Append(buildingText);
			UIText herbText = new UIText(UISystem.BMHerbText, 1f, false);
			herbText.Left.Set(35f, 0f);
			herbText.Top.Set(340f, 0f);
			herbText.Width.Set(60f, 0f);
			herbText.Height.Set(22f, 0f);
			this.ShopPanel.Append(herbText);
			UIText fishText = new UIText(UISystem.BMFishText, 1f, false);
			fishText.Left.Set(35f, 0f);
			fishText.Top.Set(370f, 0f);
			fishText.Width.Set(60f, 0f);
			fishText.Height.Set(22f, 0f);
			this.ShopPanel.Append(fishText);
			UIText mountText = new UIText(UISystem.BMMountText, 1f, false);
			mountText.Left.Set(35f, 0f);
			mountText.Top.Set(400f, 0f);
			mountText.Width.Set(60f, 0f);
			mountText.Height.Set(22f, 0f);
			this.ShopPanel.Append(mountText);
			UIText ammoText = new UIText(UISystem.BMAmmoText, 1f, false);
			ammoText.Left.Set(35f, 0f);
			ammoText.Top.Set(430f, 0f);
			ammoText.Width.Set(30f, 0f);
			ammoText.Height.Set(22f, 0f);
			this.ShopPanel.Append(ammoText);
			Asset<Texture2D> texture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonPlay", 2);
			Asset<Texture2D> buttonDeleteTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete", 2);
			UIImageButton potionButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMPotionShop)
			{
				potionButton = new UIImageButton(buttonDeleteTexture);
			}
			potionButton.Left.Set(10f, 0f);
			potionButton.Top.Set(10f, 0f);
			potionButton.Width.Set(22f, 0f);
			potionButton.Height.Set(22f, 0f);
			potionButton.OnLeftClick += this.PotionShopClicked;
			this.ShopPanel.Append(potionButton);
			UIImageButton stationButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMMaterialShop)
			{
				stationButton = new UIImageButton(buttonDeleteTexture);
			}
			stationButton.Left.Set(10f, 0f);
			stationButton.Top.Set(40f, 0f);
			stationButton.Width.Set(22f, 0f);
			stationButton.Height.Set(22f, 0f);
			stationButton.OnLeftClick += this.StationShopClicked;
			this.ShopPanel.Append(stationButton);
			UIImageButton materialButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMMaterialShop)
			{
				materialButton = new UIImageButton(buttonDeleteTexture);
			}
			materialButton.Left.Set(10f, 0f);
			materialButton.Top.Set(70f, 0f);
			materialButton.Width.Set(22f, 0f);
			materialButton.Height.Set(22f, 0f);
			materialButton.OnLeftClick += this.MaterialShopClicked;
			this.ShopPanel.Append(materialButton);
			UIImageButton movementButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMMovementAccessoryShop)
			{
				movementButton = new UIImageButton(buttonDeleteTexture);
			}
			movementButton.Left.Set(10f, 0f);
			movementButton.Top.Set(100f, 0f);
			movementButton.Width.Set(22f, 0f);
			movementButton.Height.Set(22f, 0f);
			movementButton.OnLeftClick += this.MovementShopClicked;
			this.ShopPanel.Append(movementButton);
			UIImageButton combatButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMCombatAccessoryShop)
			{
				combatButton = new UIImageButton(buttonDeleteTexture);
			}
			combatButton.Left.Set(10f, 0f);
			combatButton.Top.Set(130f, 0f);
			combatButton.Width.Set(22f, 0f);
			combatButton.Height.Set(22f, 0f);
			combatButton.OnLeftClick += this.CombatShopClicked;
			this.ShopPanel.Append(combatButton);
			UIImageButton infoButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMInformationShop)
			{
				infoButton = new UIImageButton(buttonDeleteTexture);
			}
			infoButton.Left.Set(10f, 0f);
			infoButton.Top.Set(160f, 0f);
			infoButton.Width.Set(22f, 0f);
			infoButton.Height.Set(22f, 0f);
			infoButton.OnLeftClick += this.InfoShopClicked;
			this.ShopPanel.Append(infoButton);
			UIImageButton bagButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMBagShop)
			{
				bagButton = new UIImageButton(buttonDeleteTexture);
			}
			bagButton.Left.Set(10f, 0f);
			bagButton.Top.Set(190f, 0f);
			bagButton.Width.Set(22f, 0f);
			bagButton.Height.Set(22f, 0f);
			bagButton.OnLeftClick += this.BagShopClicked;
			this.ShopPanel.Append(bagButton);
			UIImageButton crateButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMCrateShop)
			{
				crateButton = new UIImageButton(buttonDeleteTexture);
			}
			crateButton.Left.Set(10f, 0f);
			crateButton.Top.Set(220f, 0f);
			crateButton.Width.Set(22f, 0f);
			crateButton.Height.Set(22f, 0f);
			crateButton.OnLeftClick += this.CrateShopClicked;
			this.ShopPanel.Append(crateButton);
			UIImageButton oreButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMOreShop)
			{
				oreButton = new UIImageButton(buttonDeleteTexture);
			}
			oreButton.Left.Set(10f, 0f);
			oreButton.Top.Set(250f, 0f);
			oreButton.Width.Set(22f, 0f);
			oreButton.Height.Set(22f, 0f);
			oreButton.OnLeftClick += this.OreShopClicked;
			this.ShopPanel.Append(oreButton);
			UIImageButton naturalButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMNaturalBlockShop)
			{
				naturalButton = new UIImageButton(buttonDeleteTexture);
			}
			naturalButton.Left.Set(10f, 0f);
			naturalButton.Top.Set(280f, 0f);
			naturalButton.Width.Set(22f, 0f);
			naturalButton.Height.Set(22f, 0f);
			naturalButton.OnLeftClick += this.NaturalShopClicked;
			this.ShopPanel.Append(naturalButton);
			UIImageButton buildingButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMBuildingBlockShop)
			{
				buildingButton = new UIImageButton(buttonDeleteTexture);
			}
			buildingButton.Left.Set(10f, 0f);
			buildingButton.Top.Set(310f, 0f);
			buildingButton.Width.Set(22f, 0f);
			buildingButton.Height.Set(22f, 0f);
			buildingButton.OnLeftClick += this.BuildingShopClicked;
			this.ShopPanel.Append(buildingButton);
			UIImageButton herbButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMHerbShop)
			{
				herbButton = new UIImageButton(buttonDeleteTexture);
			}
			herbButton.Left.Set(10f, 0f);
			herbButton.Top.Set(340f, 0f);
			herbButton.Width.Set(22f, 0f);
			herbButton.Height.Set(22f, 0f);
			herbButton.OnLeftClick += this.HerbShopClicked;
			this.ShopPanel.Append(herbButton);
			UIImageButton fishButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMFishShop)
			{
				fishButton = new UIImageButton(buttonDeleteTexture);
			}
			fishButton.Left.Set(10f, 0f);
			fishButton.Top.Set(370f, 0f);
			fishButton.Width.Set(22f, 0f);
			fishButton.Height.Set(22f, 0f);
			fishButton.OnLeftClick += this.FishShopClicked;
			this.ShopPanel.Append(fishButton);
			UIImageButton mountButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMMountShop)
			{
				mountButton = new UIImageButton(buttonDeleteTexture);
			}
			mountButton.Left.Set(10f, 0f);
			mountButton.Top.Set(400f, 0f);
			mountButton.Width.Set(22f, 0f);
			mountButton.Height.Set(22f, 0f);
			mountButton.OnLeftClick += this.MountShopClicked;
			this.ShopPanel.Append(mountButton);
			UIImageButton ammoButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.BMAmmoShop)
			{
				ammoButton = new UIImageButton(buttonDeleteTexture);
			}
			ammoButton.Left.Set(10f, 0f);
			ammoButton.Top.Set(430f, 0f);
			ammoButton.Width.Set(22f, 0f);
			ammoButton.Height.Set(22f, 0f);
			ammoButton.OnLeftClick += this.AmmoShopClicked;
			this.ShopPanel.Append(ammoButton);
			UIImageButton closeButton = new UIImageButton(buttonDeleteTexture);
			closeButton.Left.Set(350f, 0f);
			closeButton.Top.Set(10f, 0f);
			closeButton.Width.Set(22f, 0f);
			closeButton.Height.Set(22f, 0f);
			closeButton.OnLeftClick += this.CloseButtonClicked;
			this.ShopPanel.Append(closeButton);
			base.Append(this.ShopPanel);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0007647F File Offset: 0x0007467F
		private void PotionShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMPotionShop)
			{
				BMDealerNPC.shopNum = 0;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x000764A8 File Offset: 0x000746A8
		private void StationShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMStationShop)
			{
				BMDealerNPC.shopNum = 1;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x000764D1 File Offset: 0x000746D1
		private void MaterialShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMMaterialShop)
			{
				BMDealerNPC.shopNum = 2;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x000764FA File Offset: 0x000746FA
		private void MovementShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMMovementAccessoryShop)
			{
				BMDealerNPC.shopNum = 3;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00076523 File Offset: 0x00074723
		private void CombatShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMCombatAccessoryShop)
			{
				BMDealerNPC.shopNum = 4;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0007654C File Offset: 0x0007474C
		private void InfoShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMInformationShop)
			{
				BMDealerNPC.shopNum = 5;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00076575 File Offset: 0x00074775
		private void BagShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMBagShop)
			{
				BMDealerNPC.shopNum = 6;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0007659E File Offset: 0x0007479E
		private void CrateShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMCrateShop)
			{
				BMDealerNPC.shopNum = 7;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000765C7 File Offset: 0x000747C7
		private void OreShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMOreShop)
			{
				BMDealerNPC.shopNum = 8;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000765F0 File Offset: 0x000747F0
		private void NaturalShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMNaturalBlockShop)
			{
				BMDealerNPC.shopNum = 9;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0007661A File Offset: 0x0007481A
		private void BuildingShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMBuildingBlockShop)
			{
				BMDealerNPC.shopNum = 10;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00076644 File Offset: 0x00074844
		private void HerbShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMHerbShop)
			{
				BMDealerNPC.shopNum = 11;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0007666E File Offset: 0x0007486E
		private void FishShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMFishShop)
			{
				BMDealerNPC.shopNum = 12;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00076698 File Offset: 0x00074898
		private void MountShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMMountShop)
			{
				BMDealerNPC.shopNum = 13;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x000766C2 File Offset: 0x000748C2
		private void AmmoShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.BMAmmoShop)
			{
				BMDealerNPC.shopNum = 14;
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000766EC File Offset: 0x000748EC
		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - BlackMarketDealerNPCUI.timeStart >= 10U)
			{
				SoundEngine.PlaySound(SoundID.MenuClose, null, null);
				BlackMarketDealerNPCUI.visible = false;
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00076724 File Offset: 0x00074924
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			this.offset = new Vector2(evt.MousePosition.X - this.ShopPanel.Left.Pixels, evt.MousePosition.Y - this.ShopPanel.Top.Pixels);
			this.dragging = true;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0007677C File Offset: 0x0007497C
		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			this.dragging = false;
			this.ShopPanel.Left.Set(end.X - this.offset.X, 0f);
			this.ShopPanel.Top.Set(end.Y - this.offset.Y, 0f);
			this.Recalculate();
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x000767EC File Offset: 0x000749EC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Vector2 MousePosition;
			MousePosition..ctor((float)Main.mouseX, (float)Main.mouseY);
			if (this.ShopPanel.ContainsPoint(MousePosition))
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			if (this.dragging)
			{
				this.ShopPanel.Left.Set(MousePosition.X - this.offset.X, 0f);
				this.ShopPanel.Top.Set(MousePosition.Y - this.offset.Y, 0f);
				this.Recalculate();
			}
		}

		// Token: 0x0400062F RID: 1583
		public UIPanel ShopPanel;

		// Token: 0x04000630 RID: 1584
		public static bool visible;

		// Token: 0x04000631 RID: 1585
		public static uint timeStart;

		// Token: 0x04000632 RID: 1586
		private Vector2 offset;

		// Token: 0x04000633 RID: 1587
		public bool dragging;
	}
}
