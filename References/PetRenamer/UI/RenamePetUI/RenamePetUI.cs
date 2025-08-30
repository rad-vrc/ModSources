using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace PetRenamer.UI.RenamePetUI
{
	// Token: 0x02000009 RID: 9
	internal class RenamePetUI : UIPanel
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002E9F File Offset: 0x0000109F
		internal int RelativeLeft
		{
			get
			{
				return Main.screenWidth / 2 - 240;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002EAE File Offset: 0x000010AE
		internal int RelativeTop
		{
			get
			{
				return Main.screenHeight / 2 + 42;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002EBA File Offset: 0x000010BA
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002EC1 File Offset: 0x000010C1
		internal static LocalizedText TitleText { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002EC9 File Offset: 0x000010C9
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002ED0 File Offset: 0x000010D0
		internal static LocalizedText SlotMouseoverText { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002ED8 File Offset: 0x000010D8
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002EDF File Offset: 0x000010DF
		internal static LocalizedText SlotMouseoverSlowText { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002EE7 File Offset: 0x000010E7
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002EEE File Offset: 0x000010EE
		internal static LocalizedText SlotMouseoverVerySlowEasterEggText { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002EF6 File Offset: 0x000010F6
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002EFD File Offset: 0x000010FD
		internal static LocalizedText InputText { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002F05 File Offset: 0x00001105
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002F0C File Offset: 0x0000110C
		internal static LocalizedText ApplyButtonText { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002F14 File Offset: 0x00001114
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002F1B File Offset: 0x0000111B
		internal static LocalizedText RandomButtonText { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002F23 File Offset: 0x00001123
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002F2A File Offset: 0x0000112A
		internal static LocalizedText ClearButtonText { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002F32 File Offset: 0x00001132
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002F39 File Offset: 0x00001139
		internal static LocalizedText CloseButtonText { get; private set; }

		// Token: 0x06000064 RID: 100 RVA: 0x00002F44 File Offset: 0x00001144
		internal static void LoadLocalization(Mod mod)
		{
			if (RenamePetUI.TitleText == null)
			{
				RenamePetUI.TitleText = RenamePetUI.GetLocalization(mod, "Title");
			}
			if (RenamePetUI.SlotMouseoverText == null)
			{
				RenamePetUI.SlotMouseoverText = RenamePetUI.GetLocalization(mod, "SlotMouseover");
			}
			if (RenamePetUI.SlotMouseoverSlowText == null)
			{
				RenamePetUI.SlotMouseoverSlowText = RenamePetUI.GetLocalization(mod, "SlotMouseoverSlow");
			}
			if (RenamePetUI.SlotMouseoverVerySlowEasterEggText == null)
			{
				RenamePetUI.SlotMouseoverVerySlowEasterEggText = RenamePetUI.GetLocalization(mod, "SlotMouseoverVerySlowEasterEgg");
			}
			if (RenamePetUI.InputText == null)
			{
				RenamePetUI.InputText = RenamePetUI.GetLocalization(mod, "Input");
			}
			if (RenamePetUI.ApplyButtonText == null)
			{
				RenamePetUI.ApplyButtonText = RenamePetUI.GetLocalization(mod, "ApplyButton");
			}
			if (RenamePetUI.RandomButtonText == null)
			{
				RenamePetUI.RandomButtonText = RenamePetUI.GetLocalization(mod, "RandomButton");
			}
			if (RenamePetUI.ClearButtonText == null)
			{
				RenamePetUI.ClearButtonText = RenamePetUI.GetLocalization(mod, "ClearButton");
			}
			if (RenamePetUI.CloseButtonText == null)
			{
				RenamePetUI.CloseButtonText = RenamePetUI.GetLocalization(mod, "CloseButton");
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003034 File Offset: 0x00001234
		private static LocalizedText GetLocalization(Mod mod, string name)
		{
			string category = "UI.RenamePetUI.";
			return Language.GetOrRegister(mod.GetLocalizationKey(category + name), null);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000305C File Offset: 0x0000125C
		public override void OnInitialize()
		{
			this.Width.Pixels = 480f;
			this.Height.Pixels = 155f;
			this.Top.Pixels = 1.0737418E+09f;
			this.Left.Pixels = 1.0737418E+09f;
			this.panels = new List<UIPanel>();
			float nextElementY = -this.PaddingTop / 2f;
			UIText uitext = new UIText(RenamePetUI.TitleText.ToString(), 1f, false);
			uitext.Top.Pixels = nextElementY;
			uitext.HAlign = 0.5f;
			this.titleText = uitext;
			base.Append(this.titleText);
			nextElementY += 20f;
			string name = string.Empty;
			VanillaItemSlotWrapper vanillaItemSlotWrapper = new VanillaItemSlotWrapper(4, 1f);
			vanillaItemSlotWrapper.Left.Pixels = 0f;
			vanillaItemSlotWrapper.Top.Pixels = nextElementY;
			vanillaItemSlotWrapper.HAlign = 0.5f;
			vanillaItemSlotWrapper.ValidItemFunc = ((Item item) => item.IsAir || PetRenamer.IsPetItem(item));
			this.itemSlot = vanillaItemSlotWrapper;
			this.itemSlot.OnEmptyMouseover += delegate(int timer)
			{
				Main.hoverItemName = RenamePetUI.SlotMouseoverText.ToString();
				if (timer > 60)
				{
					Main.hoverItemName = RenamePetUI.SlotMouseoverSlowText.ToString();
					return;
				}
				if (timer > 3600)
				{
					Main.hoverItemName = RenamePetUI.SlotMouseoverVerySlowEasterEggText.ToString();
				}
			};
			base.Append(this.itemSlot);
			nextElementY += 55f;
			Item uiItem = Main.mouseItem;
			bool skipCheck = false;
			Item renamePetUIItem = Main.LocalPlayer.GetModPlayer<PRPlayer>().renamePetUIItem;
			if (!renamePetUIItem.IsAir)
			{
				skipCheck = true;
				uiItem = renamePetUIItem;
			}
			if (skipCheck || (!uiItem.IsAir && this.itemSlot.Valid(uiItem)))
			{
				PRItem petItem;
				if (uiItem.TryGetGlobalItem<PRItem>(out petItem))
				{
					name = petItem.petName;
				}
				this.itemSlot.Item = uiItem.Clone();
				uiItem.TurnToAir(false);
			}
			UIBetterTextBox uibetterTextBox = new UIBetterTextBox(RenamePetUI.InputText.ToString(), name);
			uibetterTextBox.BackgroundColor = Color.White;
			uibetterTextBox.Top.Pixels = nextElementY;
			uibetterTextBox.Width.Precent = 1f;
			uibetterTextBox.Height.Pixels = 30f;
			this.commandInput = uibetterTextBox;
			if (!this.itemSlot.Item.IsAir)
			{
				this.commandInput.Focus();
			}
			base.Append(this.commandInput);
			nextElementY += 36f;
			DynamicSpriteFont value = FontAssets.MouseText.Value;
			string rawText = RenamePetUI.RandomButtonText.ToString();
			Vector2 textSize = value.MeasureString(rawText);
			float panelToTextPadding = 18f;
			UIPanel uipanel = new UIPanel();
			uipanel.Top.Pixels = nextElementY;
			uipanel.Width.Pixels = textSize.X + panelToTextPadding;
			uipanel.Height.Pixels = 30f;
			uipanel.HAlign = 0.5f;
			uipanel.BackgroundColor = RenamePetUI.bgColor;
			this.randomizeButton = uipanel;
			this.randomizeButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement element)
			{
				this.RandomizeText();
			};
			UIText uitext2 = new UIText(rawText, 1f, false);
			uitext2.Top.Pixels = -4f;
			uitext2.Left.Pixels = -2f;
			UIText randomizeButtonText = uitext2;
			this.randomizeButton.Append(randomizeButtonText);
			base.Append(this.randomizeButton);
			this.panels.Add(this.randomizeButton);
			float minPadBetweenButtons = 30f;
			float spaceOccupiedByMiddleButtonOnSide = this.randomizeButton.Width.Pixels / 2f + minPadBetweenButtons;
			rawText = RenamePetUI.ApplyButtonText.ToString();
			float spaceOccupied = value.MeasureString(rawText).X + panelToTextPadding;
			float left = this.Width.Pixels / 2f - spaceOccupiedByMiddleButtonOnSide - spaceOccupied;
			UIPanel uipanel2 = new UIPanel();
			uipanel2.Top.Pixels = nextElementY;
			uipanel2.Left.Pixels = left;
			uipanel2.Width.Pixels = spaceOccupied;
			uipanel2.Height.Pixels = 30f;
			uipanel2.BackgroundColor = RenamePetUI.bgColor;
			this.applyButton = uipanel2;
			this.applyButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement element)
			{
				this.ApplyNameToItem();
			};
			UIText uitext3 = new UIText(rawText, 1f, false);
			uitext3.Top.Pixels = -4f;
			uitext3.Left.Pixels = -2f;
			UIText applyButtonText = uitext3;
			this.applyButton.Append(applyButtonText);
			base.Append(this.applyButton);
			this.panels.Add(this.applyButton);
			rawText = RenamePetUI.ClearButtonText.ToString();
			spaceOccupied = value.MeasureString(rawText).X + panelToTextPadding;
			left = this.Width.Pixels / 2f + spaceOccupiedByMiddleButtonOnSide - 24f;
			UIPanel uipanel3 = new UIPanel();
			uipanel3.Top.Pixels = nextElementY;
			uipanel3.Left.Pixels = left;
			uipanel3.Width.Pixels = spaceOccupied;
			uipanel3.Height.Pixels = 30f;
			uipanel3.BackgroundColor = RenamePetUI.bgColor;
			this.clearButton = uipanel3;
			this.clearButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement element)
			{
				this.ClearTextField();
			};
			UIText uitext4 = new UIText(rawText, 1f, false);
			uitext4.Top.Pixels = -4f;
			uitext4.Left.Pixels = -2f;
			UIText clearButtonText = uitext4;
			this.clearButton.Append(clearButtonText);
			base.Append(this.clearButton);
			this.panels.Add(this.clearButton);
			UIQuitButton quitButton = new UIQuitButton(RenamePetUI.CloseButtonText.ToString());
			quitButton.Top.Pixels = -this.PaddingTop / 2f;
			quitButton.Left.Pixels = 480f - this.PaddingRight - quitButton.Width.Pixels - 8f;
			quitButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement element)
			{
				PRUISystem.CloseRenamePetUI();
			};
			base.Append(quitButton);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003608 File Offset: 0x00001808
		public override void OnActivate()
		{
			base.OnActivate();
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003630 File Offset: 0x00001830
		public override void OnDeactivate()
		{
			base.OnDeactivate();
			if (!RenamePetUI.saveItemInUI && !Main.gameMenu)
			{
				SoundEngine.PlaySound(SoundID.MenuClose, null, null);
			}
			Player player = Main.LocalPlayer;
			Item item = this.itemSlot.Item;
			if (RenamePetUI.saveItemInUI)
			{
				RenamePetUI.saveItemInUI = false;
				if (!item.IsAir)
				{
					player.GetModPlayer<PRPlayer>().renamePetUIItem = item.Clone();
					return;
				}
			}
			else if (!item.IsAir)
			{
				IEntitySource source = player.GetSource_Misc("PlayerDropItemCheck");
				player.QuickSpawnItem(source, item, item.stack);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000036C4 File Offset: 0x000018C4
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			this.Left.Pixels = (float)this.RelativeLeft;
			this.Top.Pixels = (float)this.RelativeTop;
			foreach (UIPanel uipanel in this.panels)
			{
				uipanel.BackgroundColor = (uipanel.IsMouseHovering ? RenamePetUI.hoverColor : RenamePetUI.bgColor);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003754 File Offset: 0x00001954
		private void ApplyNameToItem()
		{
			Item item = this.itemSlot.Item;
			PRItem petItem;
			if (!item.IsAir && this.itemSlot.Valid(item) && item.TryGetGlobalItem<PRItem>(out petItem))
			{
				string petName = petItem.petName;
				petItem.petName = this.commandInput.currentString;
				petItem.petOwner = Main.LocalPlayer.name;
				if (petName != petItem.petName)
				{
					SoundEngine.PlaySound(SoundID.ResearchComplete, null, null);
					return;
				}
				SoundEngine.PlaySound(SoundID.MenuTick, null, null);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000037EC File Offset: 0x000019EC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this.firstDraw)
			{
				this.firstDraw = false;
				return;
			}
			if (this.ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
				Main.LocalPlayer.cursorItemIconEnabled = false;
				Main.ItemIconCacheUpdate(0);
			}
			base.DrawSelf(spriteBatch);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000383C File Offset: 0x00001A3C
		private void ClearTextField()
		{
			if (this.commandInput.currentString.Length > 0)
			{
				SoundEngine.PlaySound(SoundID.MenuTick, null, null);
			}
			this.commandInput.SetText(string.Empty);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003884 File Offset: 0x00001A84
		private void RandomizeText()
		{
			if (PetRenamer.randomNames != null)
			{
				string name = Main.rand.Next(PetRenamer.randomNames);
				string fullText = name;
				if (PetRenamer.randomAdjectives != null && !Main.rand.NextBool(10))
				{
					string adj = Main.rand.Next(PetRenamer.randomAdjectives);
					if (Main.rand.NextBool())
					{
						fullText = name + " the " + adj;
					}
					else
					{
						string[] adjSameLetter = (from s in PetRenamer.randomAdjectives
						where s.StartsWith(name[0].ToString())
						select s).ToArray<string>();
						if (adjSameLetter.Length != 0)
						{
							adj = Main.rand.Next(adjSameLetter);
							fullText = adj + " " + name;
						}
					}
				}
				SoundEngine.PlaySound(SoundID.MenuTick, null, null);
				this.commandInput.SetText(fullText);
			}
		}

		// Token: 0x04000025 RID: 37
		private UIText titleText;

		// Token: 0x04000026 RID: 38
		private UIBetterTextBox commandInput;

		// Token: 0x04000027 RID: 39
		private UIPanel applyButton;

		// Token: 0x04000028 RID: 40
		private UIPanel randomizeButton;

		// Token: 0x04000029 RID: 41
		private UIPanel clearButton;

		// Token: 0x0400002A RID: 42
		private List<UIPanel> panels;

		// Token: 0x0400002B RID: 43
		private VanillaItemSlotWrapper itemSlot;

		// Token: 0x0400002C RID: 44
		private static readonly Color bgColor = new Color(73, 94, 171);

		// Token: 0x0400002D RID: 45
		private static readonly Color hoverColor = new Color(100, 118, 184);

		// Token: 0x0400002E RID: 46
		internal const int width = 480;

		// Token: 0x0400002F RID: 47
		internal const int height = 155;

		// Token: 0x04000030 RID: 48
		internal bool firstDraw = true;

		// Token: 0x0400003A RID: 58
		internal static bool saveItemInUI = false;
	}
}
