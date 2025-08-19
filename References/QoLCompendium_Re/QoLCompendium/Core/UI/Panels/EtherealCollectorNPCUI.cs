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
	// Token: 0x02000272 RID: 626
	public class EtherealCollectorNPCUI : UIState
	{
		// Token: 0x06000F63 RID: 3939 RVA: 0x000775BC File Offset: 0x000757BC
		public override void OnInitialize()
		{
			this.ShopPanel = new UIPanel();
			this.ShopPanel.SetPadding(0f);
			this.ShopPanel.Left.Set(575f, 0f);
			this.ShopPanel.Top.Set(300f, 0f);
			this.ShopPanel.Width.Set(385f, 0f);
			this.ShopPanel.Height.Set(340f, 0f);
			this.ShopPanel.BackgroundColor = new Color(73, 94, 171);
			this.ShopPanel.OnLeftMouseDown += this.DragStart;
			this.ShopPanel.OnLeftMouseUp += this.DragEnd;
			UIText potionText = new UIText(UISystem.ECPotionText, 1f, false);
			potionText.Left.Set(35f, 0f);
			potionText.Top.Set(10f, 0f);
			potionText.Width.Set(60f, 0f);
			potionText.Height.Set(22f, 0f);
			this.ShopPanel.Append(potionText);
			UIText stationText = new UIText(UISystem.ECStationText, 1f, false);
			stationText.Left.Set(35f, 0f);
			stationText.Top.Set(40f, 0f);
			stationText.Width.Set(60f, 0f);
			stationText.Height.Set(22f, 0f);
			this.ShopPanel.Append(stationText);
			UIText materialText = new UIText(UISystem.ECMaterialText, 1f, false);
			materialText.Left.Set(35f, 0f);
			materialText.Top.Set(70f, 0f);
			materialText.Width.Set(60f, 0f);
			materialText.Height.Set(22f, 0f);
			this.ShopPanel.Append(materialText);
			UIText material2Text = new UIText(UISystem.ECMaterial2Text, 1f, false);
			material2Text.Left.Set(35f, 0f);
			material2Text.Top.Set(100f, 0f);
			material2Text.Width.Set(60f, 0f);
			material2Text.Height.Set(22f, 0f);
			this.ShopPanel.Append(material2Text);
			UIText bagText = new UIText(UISystem.ECBagText, 1f, false);
			bagText.Left.Set(35f, 0f);
			bagText.Top.Set(130f, 0f);
			bagText.Width.Set(60f, 0f);
			bagText.Height.Set(22f, 0f);
			this.ShopPanel.Append(bagText);
			UIText crateText = new UIText(UISystem.ECCrateText, 1f, false);
			crateText.Left.Set(35f, 0f);
			crateText.Top.Set(160f, 0f);
			crateText.Width.Set(60f, 0f);
			crateText.Height.Set(22f, 0f);
			this.ShopPanel.Append(crateText);
			UIText oreText = new UIText(UISystem.ECOreText, 1f, false);
			oreText.Left.Set(35f, 0f);
			oreText.Top.Set(190f, 0f);
			oreText.Width.Set(60f, 0f);
			oreText.Height.Set(22f, 0f);
			this.ShopPanel.Append(oreText);
			UIText naturalText = new UIText(UISystem.ECNaturalBlockText, 1f, false);
			naturalText.Left.Set(35f, 0f);
			naturalText.Top.Set(220f, 0f);
			naturalText.Width.Set(60f, 0f);
			naturalText.Height.Set(22f, 0f);
			this.ShopPanel.Append(naturalText);
			UIText buildingText = new UIText(UISystem.ECBuildingBlockText, 1f, false);
			buildingText.Left.Set(35f, 0f);
			buildingText.Top.Set(250f, 0f);
			buildingText.Width.Set(60f, 0f);
			buildingText.Height.Set(22f, 0f);
			this.ShopPanel.Append(buildingText);
			UIText herbText = new UIText(UISystem.ECHerbText, 1f, false);
			herbText.Left.Set(35f, 0f);
			herbText.Top.Set(280f, 0f);
			herbText.Width.Set(60f, 0f);
			herbText.Height.Set(22f, 0f);
			this.ShopPanel.Append(herbText);
			UIText fishText = new UIText(UISystem.ECFishText, 1f, false);
			fishText.Left.Set(35f, 0f);
			fishText.Top.Set(310f, 0f);
			fishText.Width.Set(60f, 0f);
			fishText.Height.Set(22f, 0f);
			this.ShopPanel.Append(fishText);
			Asset<Texture2D> texture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonPlay", 2);
			Asset<Texture2D> buttonDeleteTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete", 2);
			UIImageButton potionButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.ECPotionShop)
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
			if (!QoLCompendium.shopConfig.ECStationShop)
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
			if (!QoLCompendium.shopConfig.ECMaterialShop)
			{
				materialButton = new UIImageButton(buttonDeleteTexture);
			}
			materialButton.Left.Set(10f, 0f);
			materialButton.Top.Set(70f, 0f);
			materialButton.Width.Set(22f, 0f);
			materialButton.Height.Set(22f, 0f);
			materialButton.OnLeftClick += this.MaterialShopClicked;
			this.ShopPanel.Append(materialButton);
			UIImageButton material2Button = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.ECMaterialShop)
			{
				material2Button = new UIImageButton(buttonDeleteTexture);
			}
			material2Button.Left.Set(10f, 0f);
			material2Button.Top.Set(100f, 0f);
			material2Button.Width.Set(22f, 0f);
			material2Button.Height.Set(22f, 0f);
			material2Button.OnLeftClick += this.Material2ShopClicked;
			this.ShopPanel.Append(material2Button);
			UIImageButton bagButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.ECBagShop)
			{
				bagButton = new UIImageButton(buttonDeleteTexture);
			}
			bagButton.Left.Set(10f, 0f);
			bagButton.Top.Set(130f, 0f);
			bagButton.Width.Set(22f, 0f);
			bagButton.Height.Set(22f, 0f);
			bagButton.OnLeftClick += this.BagShopClicked;
			this.ShopPanel.Append(bagButton);
			UIImageButton crateButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.ECCrateShop)
			{
				crateButton = new UIImageButton(buttonDeleteTexture);
			}
			crateButton.Left.Set(10f, 0f);
			crateButton.Top.Set(160f, 0f);
			crateButton.Width.Set(22f, 0f);
			crateButton.Height.Set(22f, 0f);
			crateButton.OnLeftClick += this.CrateShopClicked;
			this.ShopPanel.Append(crateButton);
			UIImageButton oreButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.ECOreShop)
			{
				oreButton = new UIImageButton(buttonDeleteTexture);
			}
			oreButton.Left.Set(10f, 0f);
			oreButton.Top.Set(190f, 0f);
			oreButton.Width.Set(22f, 0f);
			oreButton.Height.Set(22f, 0f);
			oreButton.OnLeftClick += this.OreShopClicked;
			this.ShopPanel.Append(oreButton);
			UIImageButton naturalButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.ECNaturalBlocksShop)
			{
				naturalButton = new UIImageButton(buttonDeleteTexture);
			}
			naturalButton.Left.Set(10f, 0f);
			naturalButton.Top.Set(220f, 0f);
			naturalButton.Width.Set(22f, 0f);
			naturalButton.Height.Set(22f, 0f);
			naturalButton.OnLeftClick += this.NaturalShopClicked;
			this.ShopPanel.Append(naturalButton);
			UIImageButton buildingButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.ECBuildingBlocksShop)
			{
				buildingButton = new UIImageButton(buttonDeleteTexture);
			}
			buildingButton.Left.Set(10f, 0f);
			buildingButton.Top.Set(250f, 0f);
			buildingButton.Width.Set(22f, 0f);
			buildingButton.Height.Set(22f, 0f);
			buildingButton.OnLeftClick += this.BuildingShopClicked;
			this.ShopPanel.Append(buildingButton);
			UIImageButton herbButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.ECHerbShop)
			{
				herbButton = new UIImageButton(buttonDeleteTexture);
			}
			herbButton.Left.Set(10f, 0f);
			herbButton.Top.Set(280f, 0f);
			herbButton.Width.Set(22f, 0f);
			herbButton.Height.Set(22f, 0f);
			herbButton.OnLeftClick += this.HerbShopClicked;
			this.ShopPanel.Append(herbButton);
			UIImageButton fishButton = new UIImageButton(texture);
			if (!QoLCompendium.shopConfig.ECFishShop)
			{
				fishButton = new UIImageButton(buttonDeleteTexture);
			}
			fishButton.Left.Set(10f, 0f);
			fishButton.Top.Set(310f, 0f);
			fishButton.Width.Set(22f, 0f);
			fishButton.Height.Set(22f, 0f);
			fishButton.OnLeftClick += this.FishShopClicked;
			this.ShopPanel.Append(fishButton);
			UIImageButton closeButton = new UIImageButton(buttonDeleteTexture);
			closeButton.Left.Set(350f, 0f);
			closeButton.Top.Set(10f, 0f);
			closeButton.Width.Set(22f, 0f);
			closeButton.Height.Set(22f, 0f);
			closeButton.OnLeftClick += this.CloseButtonClicked;
			this.ShopPanel.Append(closeButton);
			base.Append(this.ShopPanel);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x000782A3 File Offset: 0x000764A3
		private void PotionShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EtherealCollectorNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.ECPotionShop)
			{
				EtherealCollectorNPC.shopNum = 0;
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x000782CC File Offset: 0x000764CC
		private void StationShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EtherealCollectorNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.ECStationShop)
			{
				EtherealCollectorNPC.shopNum = 1;
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x000782F5 File Offset: 0x000764F5
		private void MaterialShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EtherealCollectorNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.ECMaterialShop)
			{
				EtherealCollectorNPC.shopNum = 2;
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0007831E File Offset: 0x0007651E
		private void Material2ShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EtherealCollectorNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.ECMaterialShop)
			{
				EtherealCollectorNPC.shopNum = 3;
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00078347 File Offset: 0x00076547
		private void BagShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EtherealCollectorNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.ECBagShop)
			{
				EtherealCollectorNPC.shopNum = 4;
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00078370 File Offset: 0x00076570
		private void CrateShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EtherealCollectorNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.ECCrateShop)
			{
				EtherealCollectorNPC.shopNum = 5;
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00078399 File Offset: 0x00076599
		private void OreShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EtherealCollectorNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.ECOreShop)
			{
				EtherealCollectorNPC.shopNum = 6;
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x000783C2 File Offset: 0x000765C2
		private void NaturalShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EtherealCollectorNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.ECNaturalBlocksShop)
			{
				EtherealCollectorNPC.shopNum = 7;
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x000783EB File Offset: 0x000765EB
		private void BuildingShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EtherealCollectorNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.ECBuildingBlocksShop)
			{
				EtherealCollectorNPC.shopNum = 8;
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00078414 File Offset: 0x00076614
		private void HerbShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EtherealCollectorNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.ECHerbShop)
			{
				EtherealCollectorNPC.shopNum = 9;
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0007843E File Offset: 0x0007663E
		private void FishShopClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EtherealCollectorNPCUI.timeStart >= 10U && QoLCompendium.shopConfig.ECFishShop)
			{
				EtherealCollectorNPC.shopNum = 10;
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00078468 File Offset: 0x00076668
		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - EtherealCollectorNPCUI.timeStart >= 10U)
			{
				SoundEngine.PlaySound(SoundID.MenuClose, null, null);
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x000784A0 File Offset: 0x000766A0
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			this.offset = new Vector2(evt.MousePosition.X - this.ShopPanel.Left.Pixels, evt.MousePosition.Y - this.ShopPanel.Top.Pixels);
			this.dragging = true;
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x000784F8 File Offset: 0x000766F8
		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			Vector2 end = evt.MousePosition;
			this.dragging = false;
			this.ShopPanel.Left.Set(end.X - this.offset.X, 0f);
			this.ShopPanel.Top.Set(end.Y - this.offset.Y, 0f);
			this.Recalculate();
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00078568 File Offset: 0x00076768
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

		// Token: 0x0400063A RID: 1594
		public UIPanel ShopPanel;

		// Token: 0x0400063B RID: 1595
		public static bool visible;

		// Token: 0x0400063C RID: 1596
		public static uint timeStart;

		// Token: 0x0400063D RID: 1597
		private Vector2 offset;

		// Token: 0x0400063E RID: 1598
		public bool dragging;
	}
}
