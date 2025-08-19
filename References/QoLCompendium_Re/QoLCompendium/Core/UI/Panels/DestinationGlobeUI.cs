using System;
using QoLCompendium.Core.UI.Buttons;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace QoLCompendium.Core.UI.Panels
{
	// Token: 0x02000270 RID: 624
	public class DestinationGlobeUI : UIState
	{
		// Token: 0x06000F4C RID: 3916 RVA: 0x00076884 File Offset: 0x00074A84
		public override void OnInitialize()
		{
			this.GlobePanel = new UIPanel();
			this.GlobePanel.Top.Set((float)(Main.screenHeight / 2), 0f);
			this.GlobePanel.Left.Set((float)(Main.screenWidth / 2 - 32), 0f);
			this.GlobePanel.Width.Set(400f, 0f);
			this.GlobePanel.Height.Set(400f, 0f);
			this.GlobePanel.BackgroundColor *= 0f;
			this.GlobePanel.BorderColor *= 0f;
			base.Append(this.GlobePanel);
			BiomeButton.backgroundTexture = 0;
			BiomeButton desertSelect = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 0));
			desertSelect.Left.Set(0f, 0f);
			desertSelect.Top.Set(0f, 0f);
			desertSelect.Width.Set(40f, 0f);
			desertSelect.Height.Set(40f, 0f);
			desertSelect.OnLeftClick += this.DesertClicked;
			desertSelect.Tooltip = UISystem.DesertText;
			this.GlobePanel.Append(desertSelect);
			BiomeButton.backgroundTexture = 1;
			BiomeButton snowSelect = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 1));
			snowSelect.Left.Set(40f, 0f);
			snowSelect.Top.Set(0f, 0f);
			snowSelect.Width.Set(40f, 0f);
			snowSelect.Height.Set(40f, 0f);
			snowSelect.OnLeftClick += this.SnowClicked;
			snowSelect.Tooltip = UISystem.SnowText;
			this.GlobePanel.Append(snowSelect);
			BiomeButton.backgroundTexture = 1;
			BiomeButton jungleSelect = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 2));
			jungleSelect.Left.Set(80f, 0f);
			jungleSelect.Top.Set(0f, 0f);
			jungleSelect.Width.Set(40f, 0f);
			jungleSelect.Height.Set(40f, 0f);
			jungleSelect.OnLeftClick += this.JungleClicked;
			jungleSelect.Tooltip = UISystem.JungleText;
			this.GlobePanel.Append(jungleSelect);
			BiomeButton.backgroundTexture = 1;
			BiomeButton mushroomSelect = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 3));
			mushroomSelect.Left.Set(120f, 0f);
			mushroomSelect.Top.Set(0f, 0f);
			mushroomSelect.Width.Set(40f, 0f);
			mushroomSelect.Height.Set(40f, 0f);
			mushroomSelect.OnLeftClick += this.MushroomClicked;
			mushroomSelect.Tooltip = UISystem.GlowingMushroomText;
			this.GlobePanel.Append(mushroomSelect);
			BiomeButton.backgroundTexture = 1;
			BiomeButton corruptionSelect = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 4));
			corruptionSelect.Left.Set(160f, 0f);
			corruptionSelect.Top.Set(0f, 0f);
			corruptionSelect.Width.Set(40f, 0f);
			corruptionSelect.Height.Set(40f, 0f);
			corruptionSelect.OnLeftClick += this.CorruptionClicked;
			corruptionSelect.Tooltip = UISystem.CorruptionText;
			this.GlobePanel.Append(corruptionSelect);
			BiomeButton.backgroundTexture = 1;
			BiomeButton crimsonSelect = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 5));
			crimsonSelect.Left.Set(200f, 0f);
			crimsonSelect.Top.Set(0f, 0f);
			crimsonSelect.Width.Set(40f, 0f);
			crimsonSelect.Height.Set(40f, 0f);
			crimsonSelect.OnLeftClick += this.CrimsonClicked;
			crimsonSelect.Tooltip = UISystem.CrimsonText;
			this.GlobePanel.Append(crimsonSelect);
			BiomeButton.backgroundTexture = 1;
			BiomeButton hallowSelect = new BiomeButton(Common.GetAsset("Biomes", "Biome_", BiomeButton.biomeTexture = 6));
			hallowSelect.Left.Set(240f, 0f);
			hallowSelect.Top.Set(0f, 0f);
			hallowSelect.Width.Set(40f, 0f);
			hallowSelect.Height.Set(40f, 0f);
			hallowSelect.OnLeftClick += this.HallowClicked;
			hallowSelect.Tooltip = UISystem.HallowText;
			this.GlobePanel.Append(hallowSelect);
			GenericUIButton.backgroundTexture = 1;
			GenericUIButton resetBiome = new GenericUIButton(Common.GetAsset("Buttons", "Button_Small_", GenericUIButton.buttonTexture = 2));
			resetBiome.Left.Set(280f, 0f);
			resetBiome.Top.Set(0f, 0f);
			resetBiome.Width.Set(40f, 0f);
			resetBiome.Height.Set(40f, 0f);
			resetBiome.OnLeftClick += this.ResetBiomeClicked;
			resetBiome.Tooltip = UISystem.ResetText;
			this.GlobePanel.Append(resetBiome);
			GenericUIButton.backgroundTexture = 2;
			GenericUIButton closeButton = new GenericUIButton(Common.GetAsset("Buttons", "Button_Small_", GenericUIButton.buttonTexture = 3));
			closeButton.Left.Set(320f, 0f);
			closeButton.Top.Set(0f, 0f);
			closeButton.Width.Set(40f, 0f);
			closeButton.Height.Set(40f, 0f);
			closeButton.OnLeftClick += this.CloseButtonClicked;
			closeButton.Tooltip = UISystem.CloseText;
			this.GlobePanel.Append(closeButton);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00076F0E File Offset: 0x0007510E
		private void ResetBiomeClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			DestinationGlobeUI.BiomeClick(0);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00076F16 File Offset: 0x00075116
		private void DesertClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			DestinationGlobeUI.BiomeClick(1);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00076F1E File Offset: 0x0007511E
		private void SnowClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			DestinationGlobeUI.BiomeClick(2);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00076F26 File Offset: 0x00075126
		private void JungleClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			DestinationGlobeUI.BiomeClick(3);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00076F2E File Offset: 0x0007512E
		private void MushroomClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			DestinationGlobeUI.BiomeClick(4);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00076F36 File Offset: 0x00075136
		private void CorruptionClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			DestinationGlobeUI.BiomeClick(5);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00076F3E File Offset: 0x0007513E
		private void CrimsonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			DestinationGlobeUI.BiomeClick(6);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00076F46 File Offset: 0x00075146
		private void HallowClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			DestinationGlobeUI.BiomeClick(7);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00076F4E File Offset: 0x0007514E
		private void ForestClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			DestinationGlobeUI.BiomeClick(8);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00076F58 File Offset: 0x00075158
		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.GameUpdateCount - DestinationGlobeUI.timeStart >= 10U)
			{
				SoundEngine.PlaySound(SoundID.MenuClose, null, null);
				DestinationGlobeUI.visible = false;
			}
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00076F90 File Offset: 0x00075190
		public static void BiomeClick(int biome)
		{
			if (Main.GameUpdateCount - DestinationGlobeUI.timeStart >= 10U)
			{
				Main.LocalPlayer.GetModPlayer<QoLCPlayer>().selectedBiome = biome;
				SoundEngine.PlaySound(SoundID.MenuTick, null, null);
			}
		}

		// Token: 0x04000634 RID: 1588
		public UIPanel GlobePanel;

		// Token: 0x04000635 RID: 1589
		public static bool visible;

		// Token: 0x04000636 RID: 1590
		public static uint timeStart;
	}
}
