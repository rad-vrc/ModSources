using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004DF RID: 1247
	public class UIResourcePackSelectionMenu : UIState, IHaveBackButtonCommand
	{
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x005BE037 File Offset: 0x005BC237
		// (set) Token: 0x06003C1F RID: 15391 RVA: 0x005BE03F File Offset: 0x005BC23F
		public UIState PreviousUIState
		{
			get
			{
				return this._uiStateToGoBackTo;
			}
			set
			{
				this._uiStateToGoBackTo = value;
			}
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x005BE048 File Offset: 0x005BC248
		public UIResourcePackSelectionMenu(UIState uiStateToGoBackTo, AssetSourceController sourceController, ResourcePackList currentResourcePackList)
		{
			this._sourceController = sourceController;
			this._uiStateToGoBackTo = uiStateToGoBackTo;
			this.BuildPage();
			this._packsList = currentResourcePackList;
			this.PopulatePackList();
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x005BE074 File Offset: 0x005BC274
		private void PopulatePackList()
		{
			this._availablePacksList.Clear();
			this._enabledPacksList.Clear();
			this.CleanUpResourcePackPriority();
			IEnumerable<ResourcePack> enabledPacks = this._packsList.EnabledPacks;
			IEnumerable<ResourcePack> disabledPacks = this._packsList.DisabledPacks;
			int num = 0;
			foreach (ResourcePack item in disabledPacks)
			{
				UIResourcePack uIResourcePack = new UIResourcePack(item, num)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f)
				};
				UIElement uIElement = this.CreatePackToggleButton(item);
				uIElement.OnUpdate += this.EnablePackUpdate;
				uIElement.SetSnapPoint("ToggleToOn", num, null, null);
				uIResourcePack.ContentPanel.Append(uIElement);
				uIElement = this.CreatePackInfoButton(item);
				uIElement.OnUpdate += this.SeeInfoUpdate;
				uIElement.SetSnapPoint("InfoOff", num, null, null);
				uIResourcePack.ContentPanel.Append(uIElement);
				this._availablePacksList.Add(uIResourcePack);
				num++;
			}
			num = 0;
			foreach (ResourcePack item2 in enabledPacks)
			{
				UIResourcePack uIResourcePack2 = new UIResourcePack(item2, num)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f)
				};
				if (item2.IsEnabled)
				{
					UIElement uIElement2 = this.CreatePackToggleButton(item2);
					uIElement2.Left = new StyleDimension(0f, 0f);
					uIElement2.Width = new StyleDimension(0f, 0.5f);
					uIElement2.OnUpdate += this.DisablePackUpdate;
					uIElement2.SetSnapPoint("ToggleToOff", num, null, null);
					uIResourcePack2.ContentPanel.Append(uIElement2);
					uIElement2 = this.CreatePackInfoButton(item2);
					uIElement2.OnUpdate += this.SeeInfoUpdate;
					uIElement2.Left = new StyleDimension(0f, 0.5f);
					uIElement2.Width = new StyleDimension(0f, 0.16666667f);
					uIElement2.SetSnapPoint("InfoOn", num, null, null);
					uIResourcePack2.ContentPanel.Append(uIElement2);
					uIElement2 = this.CreateOffsetButton(item2, -1);
					uIElement2.Left = new StyleDimension(0f, 0.6666667f);
					uIElement2.Width = new StyleDimension(0f, 0.16666667f);
					uIElement2.SetSnapPoint("OrderUp", num, null, null);
					uIResourcePack2.ContentPanel.Append(uIElement2);
					uIElement2 = this.CreateOffsetButton(item2, 1);
					uIElement2.Left = new StyleDimension(0f, 0.8333334f);
					uIElement2.Width = new StyleDimension(0f, 0.16666667f);
					uIElement2.SetSnapPoint("OrderDown", num, null, null);
					uIResourcePack2.ContentPanel.Append(uIElement2);
				}
				this._enabledPacksList.Add(uIResourcePack2);
				num++;
			}
			this.UpdateTitles();
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x005BE404 File Offset: 0x005BC604
		private UIElement CreateOffsetButton(ResourcePack resourcePack, int offset)
		{
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 0.8f, 0.5f, 10f)
			{
				Left = StyleDimension.FromPercent(0.5f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 0.5f),
				Height = StyleDimension.Fill
			};
			bool flag = (offset == -1 && resourcePack.SortingOrder == 0) | (offset == 1 && resourcePack.SortingOrder == this._packsList.EnabledPacks.Count<ResourcePack>() - 1);
			Color lightCyan = Color.LightCyan;
			groupOptionButton.SetColorsBasedOnSelectionState(lightCyan, lightCyan, 0.7f, 0.7f);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetPadding(0f);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/TexturePackButtons");
			UIImageFramed element = new UIImageFramed(asset, asset.Frame(2, 2, (offset == 1) ? 1 : 0, 0, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			groupOptionButton.Append(element);
			groupOptionButton.OnMouseOver += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			};
			int offsetLocalForLambda = offset;
			if (flag)
			{
				groupOptionButton.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				};
			}
			else
			{
				groupOptionButton.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					this.OffsetResourcePackPriority(resourcePack, offsetLocalForLambda);
					this.PopulatePackList();
					Main.instance.ResetAllContentBasedRenderTargets();
				};
			}
			if (offset == 1)
			{
				groupOptionButton.OnUpdate += this.OffsetFrontwardUpdate;
			}
			else
			{
				groupOptionButton.OnUpdate += this.OffsetBackwardUpdate;
			}
			return groupOptionButton;
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x005BE5B4 File Offset: 0x005BC7B4
		private UIElement CreatePackToggleButton(ResourcePack resourcePack)
		{
			Language.GetText(resourcePack.IsEnabled ? "GameUI.Enabled" : "GameUI.Disabled");
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 0.8f, 0.5f, 10f);
			groupOptionButton.Left = StyleDimension.FromPercent(0.5f);
			groupOptionButton.Width = StyleDimension.FromPixelsAndPercent(0f, 0.5f);
			groupOptionButton.Height = StyleDimension.Fill;
			groupOptionButton.SetColorsBasedOnSelectionState(Color.LightGreen, Color.PaleVioletRed, 0.7f, 0.7f);
			groupOptionButton.SetCurrentOption(resourcePack.IsEnabled);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetPadding(0f);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/TexturePackButtons");
			UIImageFramed element = new UIImageFramed(asset, asset.Frame(2, 2, (!resourcePack.IsEnabled) ? 1 : 0, 1, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			groupOptionButton.Append(element);
			groupOptionButton.OnMouseOver += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			};
			groupOptionButton.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				resourcePack.IsEnabled = !resourcePack.IsEnabled;
				this.SetResourcePackAsTopPriority(resourcePack);
				this.PopulatePackList();
				Main.instance.ResetAllContentBasedRenderTargets();
			};
			return groupOptionButton;
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x005BE708 File Offset: 0x005BC908
		private void SetResourcePackAsTopPriority(ResourcePack resourcePack)
		{
			if (!resourcePack.IsEnabled)
			{
				return;
			}
			int num = -1;
			foreach (ResourcePack enabledPack in this._packsList.EnabledPacks)
			{
				if (num < enabledPack.SortingOrder && enabledPack != resourcePack)
				{
					num = enabledPack.SortingOrder;
				}
			}
			resourcePack.SortingOrder = num + 1;
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x005BE77C File Offset: 0x005BC97C
		private void OffsetResourcePackPriority(ResourcePack resourcePack, int offset)
		{
			if (resourcePack.IsEnabled)
			{
				List<ResourcePack> list = this._packsList.EnabledPacks.ToList<ResourcePack>();
				int num = list.IndexOf(resourcePack);
				int num2 = Utils.Clamp<int>(num + offset, 0, list.Count - 1);
				if (num2 != num)
				{
					int sortingOrder = list[num].SortingOrder;
					list[num].SortingOrder = list[num2].SortingOrder;
					list[num2].SortingOrder = sortingOrder;
				}
			}
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x005BE7F4 File Offset: 0x005BC9F4
		private UIElement CreatePackInfoButton(ResourcePack resourcePack)
		{
			UIResourcePackInfoButton<string> uiresourcePackInfoButton = new UIResourcePackInfoButton<string>("", 0.8f, false);
			uiresourcePackInfoButton.Width = StyleDimension.FromPixelsAndPercent(0f, 0.5f);
			uiresourcePackInfoButton.Height = StyleDimension.Fill;
			uiresourcePackInfoButton.ResourcePack = resourcePack;
			uiresourcePackInfoButton.SetPadding(0f);
			UIImage element = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CharInfo"))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			uiresourcePackInfoButton.Append(element);
			uiresourcePackInfoButton.OnMouseOver += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			};
			uiresourcePackInfoButton.OnLeftClick += this.Click_Info;
			return uiresourcePackInfoButton;
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x005BE8B4 File Offset: 0x005BCAB4
		private void Click_Info(UIMouseEvent evt, UIElement listeningElement)
		{
			UIResourcePackInfoButton<string> uIResourcePackInfoButton = listeningElement as UIResourcePackInfoButton<string>;
			if (uIResourcePackInfoButton != null)
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.MenuUI.SetState(new UIResourcePackInfoMenu(this, uIResourcePackInfoButton.ResourcePack));
			}
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x005BE8F6 File Offset: 0x005BCAF6
		private void ApplyListChanges()
		{
			this._sourceController.UseResourcePacks(new ResourcePackList(from uiPack in this._enabledPacksList
			select ((UIResourcePack)uiPack).ResourcePack));
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x005BE934 File Offset: 0x005BCB34
		private void CleanUpResourcePackPriority()
		{
			IEnumerable<ResourcePack> enumerable = from pack in this._packsList.EnabledPacks
			orderby pack.SortingOrder
			select pack;
			int num = 0;
			foreach (ResourcePack resourcePack in enumerable)
			{
				resourcePack.SortingOrder = num++;
			}
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x005BE9B0 File Offset: 0x005BCBB0
		private void BuildPage()
		{
			base.RemoveAllChildren();
			UIElement uIElement = new UIElement();
			uIElement.Width.Set(0f, 0.8f);
			uIElement.MaxWidth.Set(800f, 0f);
			uIElement.MinWidth.Set(600f, 0f);
			uIElement.Top.Set(240f, 0f);
			uIElement.Height.Set(-240f, 1f);
			uIElement.HAlign = 0.5f;
			base.Append(uIElement);
			UIPanel uIPanel = new UIPanel
			{
				Width = StyleDimension.Fill,
				Height = new StyleDimension(-110f, 1f),
				BackgroundColor = new Color(33, 43, 79) * 0.8f,
				PaddingRight = 0f,
				PaddingLeft = 0f
			};
			uIElement.Append(uIPanel);
			int num = 35;
			int num2 = num;
			int num3 = 30;
			UIElement uIElement2 = new UIElement
			{
				Width = StyleDimension.Fill,
				Height = StyleDimension.FromPixelsAndPercent((float)(-(float)(num3 + 4 + 5)), 1f),
				VAlign = 1f
			};
			uIElement2.SetPadding(0f);
			uIPanel.Append(uIElement2);
			UIElement uIElement3 = new UIElement
			{
				Width = new StyleDimension(-20f, 0.5f),
				Height = new StyleDimension(0f, 1f),
				Left = new StyleDimension(10f, 0f)
			};
			uIElement3.SetPadding(0f);
			uIElement2.Append(uIElement3);
			UIElement uIElement4 = new UIElement
			{
				Width = new StyleDimension(-20f, 0.5f),
				Height = new StyleDimension(0f, 1f),
				Left = new StyleDimension(-10f, 0f),
				HAlign = 1f
			};
			uIElement4.SetPadding(0f);
			uIElement2.Append(uIElement4);
			UIList uIList = new UIList
			{
				Width = new StyleDimension(-25f, 1f),
				Height = new StyleDimension(0f, 1f),
				ListPadding = 5f,
				HAlign = 1f
			};
			uIElement3.Append(uIList);
			this._availablePacksList = uIList;
			UIList uIList2 = new UIList
			{
				Width = new StyleDimension(-25f, 1f),
				Height = new StyleDimension(0f, 1f),
				ListPadding = 5f,
				HAlign = 0f,
				Left = new StyleDimension(0f, 0f)
			};
			uIElement4.Append(uIList2);
			this._enabledPacksList = uIList2;
			UIElement uielement = uIPanel;
			UIText uitext = new UIText(Language.GetText("UI.AvailableResourcePacksTitle"), 1f, false);
			uitext.HAlign = 0f;
			uitext.Left = new StyleDimension(25f, 0f);
			uitext.Width = new StyleDimension(-25f, 0.5f);
			uitext.VAlign = 0f;
			uitext.Top = new StyleDimension(10f, 0f);
			UIText element2 = uitext;
			this._titleAvailable = uitext;
			uielement.Append(element2);
			UIElement uielement2 = uIPanel;
			UIText uitext2 = new UIText(Language.GetText("UI.EnabledResourcePacksTitle"), 1f, false);
			uitext2.HAlign = 1f;
			uitext2.Left = new StyleDimension(-25f, 0f);
			uitext2.Width = new StyleDimension(-25f, 0.5f);
			uitext2.VAlign = 0f;
			uitext2.Top = new StyleDimension(10f, 0f);
			element2 = uitext2;
			this._titleEnabled = uitext2;
			uielement2.Append(element2);
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.ResourcePacks"), 1f, true)
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Top = new StyleDimension(-44f, 0f),
				BackgroundColor = new Color(73, 94, 171)
			};
			uITextPanel.SetPadding(13f);
			uIElement.Append(uITextPanel);
			UIScrollbar uIScrollbar = new UIScrollbar
			{
				Height = new StyleDimension(0f, 1f),
				HAlign = 0f,
				Left = new StyleDimension(0f, 0f)
			};
			uIElement3.Append(uIScrollbar);
			this._availablePacksList.SetScrollbar(uIScrollbar);
			UIVerticalSeparator element = new UIVerticalSeparator
			{
				Height = new StyleDimension(-12f, 1f),
				HAlign = 0.5f,
				VAlign = 1f,
				Color = new Color(89, 116, 213, 255) * 0.9f
			};
			uIPanel.Append(element);
			UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true);
			uihorizontalSeparator.Width = new StyleDimension((float)(-(float)num2), 0.5f);
			uihorizontalSeparator.VAlign = 0f;
			uihorizontalSeparator.HAlign = 0f;
			uihorizontalSeparator.Color = new Color(89, 116, 213, 255) * 0.9f;
			uihorizontalSeparator.Top = new StyleDimension((float)num3, 0f);
			uihorizontalSeparator.Left = new StyleDimension((float)num, 0f);
			UIHorizontalSeparator uihorizontalSeparator2 = new UIHorizontalSeparator(2, true);
			uihorizontalSeparator2.Width = new StyleDimension((float)(-(float)num2), 0.5f);
			uihorizontalSeparator2.VAlign = 0f;
			uihorizontalSeparator2.HAlign = 1f;
			uihorizontalSeparator2.Color = new Color(89, 116, 213, 255) * 0.9f;
			uihorizontalSeparator2.Top = new StyleDimension((float)num3, 0f);
			uihorizontalSeparator2.Left = new StyleDimension((float)(-(float)num), 0f);
			UIScrollbar uIScrollbar2 = new UIScrollbar
			{
				Height = new StyleDimension(0f, 1f),
				HAlign = 1f
			};
			uIElement4.Append(uIScrollbar2);
			this._enabledPacksList.SetScrollbar(uIScrollbar2);
			this.AddBackAndFolderButtons(uIElement);
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x005BEFBC File Offset: 0x005BD1BC
		private void UpdateTitles()
		{
			this._titleAvailable.SetText(Language.GetText("UI.AvailableResourcePacksTitle").FormatWith(new
			{
				Amount = this._availablePacksList.Count
			}));
			this._titleEnabled.SetText(Language.GetText("UI.EnabledResourcePacksTitle").FormatWith(new
			{
				Amount = this._enabledPacksList.Count
			}));
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x005BF020 File Offset: 0x005BD220
		private void AddBackAndFolderButtons(UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true)
			{
				Width = new StyleDimension(-8f, 0.5f),
				Height = new StyleDimension(50f, 0f),
				VAlign = 1f,
				HAlign = 0f,
				Top = new StyleDimension(-45f, 0f)
			};
			UIElement uielement = uITextPanel;
			UIElement.MouseEvent value;
			if ((value = UIResourcePackSelectionMenu.<>O.<0>__FadedMouseOver) == null)
			{
				value = (UIResourcePackSelectionMenu.<>O.<0>__FadedMouseOver = new UIElement.MouseEvent(UIResourcePackSelectionMenu.FadedMouseOver));
			}
			uielement.OnMouseOver += value;
			UIElement uielement2 = uITextPanel;
			UIElement.MouseEvent value2;
			if ((value2 = UIResourcePackSelectionMenu.<>O.<1>__FadedMouseOut) == null)
			{
				value2 = (UIResourcePackSelectionMenu.<>O.<1>__FadedMouseOut = new UIElement.MouseEvent(UIResourcePackSelectionMenu.FadedMouseOut));
			}
			uielement2.OnMouseOut += value2;
			uITextPanel.OnLeftClick += this.GoBackClick;
			uITextPanel.SetSnapPoint("GoBack", 0, null, null);
			outerContainer.Append(uITextPanel);
			UITextPanel<LocalizedText> uITextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("GameUI.OpenFileFolder"), 0.7f, true)
			{
				Width = new StyleDimension(-8f, 0.5f),
				Height = new StyleDimension(50f, 0f),
				VAlign = 1f,
				HAlign = 1f,
				Top = new StyleDimension(-45f, 0f)
			};
			UIElement uielement3 = uITextPanel2;
			UIElement.MouseEvent value3;
			if ((value3 = UIResourcePackSelectionMenu.<>O.<0>__FadedMouseOver) == null)
			{
				value3 = (UIResourcePackSelectionMenu.<>O.<0>__FadedMouseOver = new UIElement.MouseEvent(UIResourcePackSelectionMenu.FadedMouseOver));
			}
			uielement3.OnMouseOver += value3;
			UIElement uielement4 = uITextPanel2;
			UIElement.MouseEvent value4;
			if ((value4 = UIResourcePackSelectionMenu.<>O.<1>__FadedMouseOut) == null)
			{
				value4 = (UIResourcePackSelectionMenu.<>O.<1>__FadedMouseOut = new UIElement.MouseEvent(UIResourcePackSelectionMenu.FadedMouseOut));
			}
			uielement4.OnMouseOut += value4;
			uITextPanel2.OnLeftClick += this.OpenFoldersClick;
			uITextPanel2.SetSnapPoint("OpenFolder", 0, null, null);
			outerContainer.Append(uITextPanel2);
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x005BF1F8 File Offset: 0x005BD3F8
		private void OpenFoldersClick(UIMouseEvent evt, UIElement listeningElement)
		{
			JArray jarray;
			string resourcePackFolder;
			AssetInitializer.GetResourcePacksFolderPathAndConfirmItExists(out jarray, out resourcePackFolder);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Utils.OpenFolder(resourcePackFolder);
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x005BF229 File Offset: 0x005BD429
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x005BF234 File Offset: 0x005BD434
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			this.ApplyListChanges();
			Main.SaveSettings();
			if (this._uiStateToGoBackTo != null)
			{
				Main.MenuUI.SetState(this._uiStateToGoBackTo);
				return;
			}
			Main.menuMode = 0;
			IngameFancyUI.Close();
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x005BF288 File Offset: 0x005BD488
		private static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x005BF2DD File Offset: 0x005BD4DD
		private static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x005BF31C File Offset: 0x005BD51C
		private void OffsetBackwardUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.OffsetTexturePackPriorityDown");
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x005BF329 File Offset: 0x005BD529
		private void OffsetFrontwardUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.OffsetTexturePackPriorityUp");
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x005BF336 File Offset: 0x005BD536
		private void EnablePackUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.EnableTexturePack");
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x005BF343 File Offset: 0x005BD543
		private void DisablePackUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.DisableTexturePack");
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x005BF350 File Offset: 0x005BD550
		private void SeeInfoUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.SeeTexturePackInfo");
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x005BF360 File Offset: 0x005BD560
		private static void DisplayMouseTextIfHovered(UIElement affectedElement, string textKey)
		{
			if (affectedElement.IsMouseHovering)
			{
				string textValue = Language.GetTextValue(textKey);
				Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x005BF38E File Offset: 0x005BD58E
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x005BF3A0 File Offset: 0x005BD5A0
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			int currentID = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			List<SnapPoint> snapPoints2 = this._availablePacksList.GetSnapPoints();
			this._helper.CullPointsOutOfElementArea(spriteBatch, snapPoints2, this._availablePacksList);
			List<SnapPoint> snapPoints3 = this._enabledPacksList.GetSnapPoints();
			this._helper.CullPointsOutOfElementArea(spriteBatch, snapPoints3, this._enabledPacksList);
			UILinkPoint[] verticalStripFromCategoryName = this._helper.GetVerticalStripFromCategoryName(ref currentID, snapPoints2, "ToggleToOn");
			UILinkPoint[] verticalStripFromCategoryName2 = this._helper.GetVerticalStripFromCategoryName(ref currentID, snapPoints2, "InfoOff");
			UILinkPoint[] verticalStripFromCategoryName3 = this._helper.GetVerticalStripFromCategoryName(ref currentID, snapPoints3, "ToggleToOff");
			UILinkPoint[] verticalStripFromCategoryName4 = this._helper.GetVerticalStripFromCategoryName(ref currentID, snapPoints3, "InfoOn");
			UILinkPoint[] verticalStripFromCategoryName5 = this._helper.GetVerticalStripFromCategoryName(ref currentID, snapPoints3, "OrderUp");
			UILinkPoint[] verticalStripFromCategoryName6 = this._helper.GetVerticalStripFromCategoryName(ref currentID, snapPoints3, "OrderDown");
			UILinkPoint uILinkPoint = null;
			UILinkPoint uILinkPoint2 = null;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint = snapPoints[i];
				string name = snapPoint.Name;
				if (!(name == "GoBack"))
				{
					if (name == "OpenFolder")
					{
						uILinkPoint2 = this._helper.MakeLinkPointFromSnapPoint(currentID++, snapPoint);
					}
				}
				else
				{
					uILinkPoint = this._helper.MakeLinkPointFromSnapPoint(currentID++, snapPoint);
				}
			}
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName2, verticalStripFromCategoryName, 0);
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName, verticalStripFromCategoryName3, 0);
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName3, verticalStripFromCategoryName4, 0);
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName4, verticalStripFromCategoryName5, 0);
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName5, verticalStripFromCategoryName6, 0);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName, uILinkPoint);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName2, uILinkPoint);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName5, uILinkPoint2);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName6, uILinkPoint2);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName3, uILinkPoint2);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName4, uILinkPoint2);
			this._helper.PairLeftRight(uILinkPoint, uILinkPoint2);
			this._helper.MoveToVisuallyClosestPoint(num, currentID);
		}

		// Token: 0x04005576 RID: 21878
		private readonly AssetSourceController _sourceController;

		// Token: 0x04005577 RID: 21879
		private UIList _availablePacksList;

		// Token: 0x04005578 RID: 21880
		private UIList _enabledPacksList;

		// Token: 0x04005579 RID: 21881
		private ResourcePackList _packsList;

		// Token: 0x0400557A RID: 21882
		private UIText _titleAvailable;

		// Token: 0x0400557B RID: 21883
		private UIText _titleEnabled;

		// Token: 0x0400557C RID: 21884
		private UIState _uiStateToGoBackTo;

		// Token: 0x0400557D RID: 21885
		private const string _snapCategory_ToggleFromOffToOn = "ToggleToOn";

		// Token: 0x0400557E RID: 21886
		private const string _snapCategory_ToggleFromOnToOff = "ToggleToOff";

		// Token: 0x0400557F RID: 21887
		private const string _snapCategory_InfoWhenOff = "InfoOff";

		// Token: 0x04005580 RID: 21888
		private const string _snapCategory_InfoWhenOn = "InfoOn";

		// Token: 0x04005581 RID: 21889
		private const string _snapCategory_OffsetOrderUp = "OrderUp";

		// Token: 0x04005582 RID: 21890
		private const string _snapCategory_OffsetOrderDown = "OrderDown";

		// Token: 0x04005583 RID: 21891
		private const string _snapPointName_goBack = "GoBack";

		// Token: 0x04005584 RID: 21892
		private const string _snapPointName_openFolder = "OpenFolder";

		// Token: 0x04005585 RID: 21893
		private UIGamepadHelper _helper;

		// Token: 0x02000BF2 RID: 3058
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040077F2 RID: 30706
			public static UIElement.MouseEvent <0>__FadedMouseOver;

			// Token: 0x040077F3 RID: 30707
			public static UIElement.MouseEvent <1>__FadedMouseOut;
		}
	}
}
