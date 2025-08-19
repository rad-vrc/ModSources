using System;
using System.Collections.Generic;
using System.Linq;
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
	// Token: 0x0200034D RID: 845
	public class UIResourcePackSelectionMenu : UIState, IHaveBackButtonCommand
	{
		// Token: 0x060026C8 RID: 9928 RVA: 0x0057A2A6 File Offset: 0x005784A6
		public UIResourcePackSelectionMenu(UIState uiStateToGoBackTo, AssetSourceController sourceController, ResourcePackList currentResourcePackList)
		{
			this._sourceController = sourceController;
			this._uiStateToGoBackTo = uiStateToGoBackTo;
			this.BuildPage();
			this._packsList = currentResourcePackList;
			this.PopulatePackList();
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x0057A2D0 File Offset: 0x005784D0
		private void PopulatePackList()
		{
			this._availablePacksList.Clear();
			this._enabledPacksList.Clear();
			this.CleanUpResourcePackPriority();
			IEnumerable<ResourcePack> enabledPacks = this._packsList.EnabledPacks;
			IEnumerable<ResourcePack> disabledPacks = this._packsList.DisabledPacks;
			int num = 0;
			foreach (ResourcePack resourcePack in disabledPacks)
			{
				UIResourcePack uiresourcePack = new UIResourcePack(resourcePack, num)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f)
				};
				UIElement uielement = this.CreatePackToggleButton(resourcePack);
				uielement.OnUpdate += this.EnablePackUpdate;
				uielement.SetSnapPoint("ToggleToOn", num, null, null);
				uiresourcePack.ContentPanel.Append(uielement);
				uielement = this.CreatePackInfoButton(resourcePack);
				uielement.OnUpdate += this.SeeInfoUpdate;
				uielement.SetSnapPoint("InfoOff", num, null, null);
				uiresourcePack.ContentPanel.Append(uielement);
				this._availablePacksList.Add(uiresourcePack);
				num++;
			}
			num = 0;
			foreach (ResourcePack resourcePack2 in enabledPacks)
			{
				UIResourcePack uiresourcePack2 = new UIResourcePack(resourcePack2, num)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f)
				};
				if (resourcePack2.IsEnabled)
				{
					UIElement uielement2 = this.CreatePackToggleButton(resourcePack2);
					uielement2.Left = new StyleDimension(0f, 0f);
					uielement2.Width = new StyleDimension(0f, 0.5f);
					uielement2.OnUpdate += this.DisablePackUpdate;
					uielement2.SetSnapPoint("ToggleToOff", num, null, null);
					uiresourcePack2.ContentPanel.Append(uielement2);
					uielement2 = this.CreatePackInfoButton(resourcePack2);
					uielement2.OnUpdate += this.SeeInfoUpdate;
					uielement2.Left = new StyleDimension(0f, 0.5f);
					uielement2.Width = new StyleDimension(0f, 0.16666667f);
					uielement2.SetSnapPoint("InfoOn", num, null, null);
					uiresourcePack2.ContentPanel.Append(uielement2);
					uielement2 = this.CreateOffsetButton(resourcePack2, -1);
					uielement2.Left = new StyleDimension(0f, 0.6666667f);
					uielement2.Width = new StyleDimension(0f, 0.16666667f);
					uielement2.SetSnapPoint("OrderUp", num, null, null);
					uiresourcePack2.ContentPanel.Append(uielement2);
					uielement2 = this.CreateOffsetButton(resourcePack2, 1);
					uielement2.Left = new StyleDimension(0f, 0.8333334f);
					uielement2.Width = new StyleDimension(0f, 0.16666667f);
					uielement2.SetSnapPoint("OrderDown", num, null, null);
					uiresourcePack2.ContentPanel.Append(uielement2);
				}
				this._enabledPacksList.Add(uiresourcePack2);
				num++;
			}
			this.UpdateTitles();
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x0057A660 File Offset: 0x00578860
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
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/TexturePackButtons", 1);
			UIImageFramed element = new UIImageFramed(asset, asset.Frame(2, 2, (offset == 1) ? 1 : 0, 0, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			groupOptionButton.Append(element);
			groupOptionButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			};
			int offsetLocalForLambda = offset;
			if (flag)
			{
				groupOptionButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				};
			}
			else
			{
				groupOptionButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
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

		// Token: 0x060026CB RID: 9931 RVA: 0x0057A814 File Offset: 0x00578A14
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
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/TexturePackButtons", 1);
			UIImageFramed element = new UIImageFramed(asset, asset.Frame(2, 2, resourcePack.IsEnabled ? 0 : 1, 1, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			groupOptionButton.Append(element);
			groupOptionButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			};
			groupOptionButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				resourcePack.IsEnabled = !resourcePack.IsEnabled;
				this.SetResourcePackAsTopPriority(resourcePack);
				this.PopulatePackList();
				Main.instance.ResetAllContentBasedRenderTargets();
			};
			return groupOptionButton;
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x0057A96C File Offset: 0x00578B6C
		private void SetResourcePackAsTopPriority(ResourcePack resourcePack)
		{
			if (!resourcePack.IsEnabled)
			{
				return;
			}
			int num = -1;
			foreach (ResourcePack resourcePack2 in this._packsList.EnabledPacks)
			{
				if (num < resourcePack2.SortingOrder && resourcePack2 != resourcePack)
				{
					num = resourcePack2.SortingOrder;
				}
			}
			resourcePack.SortingOrder = num + 1;
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x0057A9E0 File Offset: 0x00578BE0
		private void OffsetResourcePackPriority(ResourcePack resourcePack, int offset)
		{
			if (!resourcePack.IsEnabled)
			{
				return;
			}
			List<ResourcePack> list = this._packsList.EnabledPacks.ToList<ResourcePack>();
			int num = list.IndexOf(resourcePack);
			int num2 = Utils.Clamp<int>(num + offset, 0, list.Count - 1);
			if (num2 == num)
			{
				return;
			}
			int sortingOrder = list[num].SortingOrder;
			list[num].SortingOrder = list[num2].SortingOrder;
			list[num2].SortingOrder = sortingOrder;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x0057AA58 File Offset: 0x00578C58
		private UIElement CreatePackInfoButton(ResourcePack resourcePack)
		{
			UIResourcePackInfoButton<string> uiresourcePackInfoButton = new UIResourcePackInfoButton<string>("", 0.8f, false);
			uiresourcePackInfoButton.Width = StyleDimension.FromPixelsAndPercent(0f, 0.5f);
			uiresourcePackInfoButton.Height = StyleDimension.Fill;
			uiresourcePackInfoButton.ResourcePack = resourcePack;
			uiresourcePackInfoButton.SetPadding(0f);
			UIImage element = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CharInfo", 1))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			uiresourcePackInfoButton.Append(element);
			uiresourcePackInfoButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			};
			uiresourcePackInfoButton.OnLeftClick += this.Click_Info;
			return uiresourcePackInfoButton;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x0057AB18 File Offset: 0x00578D18
		private void Click_Info(UIMouseEvent evt, UIElement listeningElement)
		{
			UIResourcePackInfoButton<string> uiresourcePackInfoButton = listeningElement as UIResourcePackInfoButton<string>;
			if (uiresourcePackInfoButton != null)
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.MenuUI.SetState(new UIResourcePackInfoMenu(this, uiresourcePackInfoButton.ResourcePack));
			}
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x0057AB5A File Offset: 0x00578D5A
		private void ApplyListChanges()
		{
			this._sourceController.UseResourcePacks(new ResourcePackList(from uiPack in this._enabledPacksList
			select ((UIResourcePack)uiPack).ResourcePack));
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x0057AB98 File Offset: 0x00578D98
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

		// Token: 0x060026D2 RID: 9938 RVA: 0x0057AC14 File Offset: 0x00578E14
		private void BuildPage()
		{
			base.RemoveAllChildren();
			UIElement uielement = new UIElement();
			uielement.Width.Set(0f, 0.8f);
			uielement.MaxWidth.Set(800f, 0f);
			uielement.MinWidth.Set(600f, 0f);
			uielement.Top.Set(240f, 0f);
			uielement.Height.Set(-240f, 1f);
			uielement.HAlign = 0.5f;
			base.Append(uielement);
			UIPanel uipanel = new UIPanel
			{
				Width = StyleDimension.Fill,
				Height = new StyleDimension(-110f, 1f),
				BackgroundColor = new Color(33, 43, 79) * 0.8f,
				PaddingRight = 0f,
				PaddingLeft = 0f
			};
			uielement.Append(uipanel);
			int num = 35;
			int num2 = num;
			int num3 = 30;
			UIElement uielement2 = new UIElement
			{
				Width = StyleDimension.Fill,
				Height = StyleDimension.FromPixelsAndPercent((float)(-(float)(num3 + 4 + 5)), 1f),
				VAlign = 1f
			};
			uielement2.SetPadding(0f);
			uipanel.Append(uielement2);
			UIElement uielement3 = new UIElement
			{
				Width = new StyleDimension(-20f, 0.5f),
				Height = new StyleDimension(0f, 1f),
				Left = new StyleDimension(10f, 0f)
			};
			uielement3.SetPadding(0f);
			uielement2.Append(uielement3);
			UIElement uielement4 = new UIElement
			{
				Width = new StyleDimension(-20f, 0.5f),
				Height = new StyleDimension(0f, 1f),
				Left = new StyleDimension(-10f, 0f),
				HAlign = 1f
			};
			uielement4.SetPadding(0f);
			uielement2.Append(uielement4);
			UIList uilist = new UIList
			{
				Width = new StyleDimension(-25f, 1f),
				Height = new StyleDimension(0f, 1f),
				ListPadding = 5f,
				HAlign = 1f
			};
			uielement3.Append(uilist);
			this._availablePacksList = uilist;
			UIList uilist2 = new UIList
			{
				Width = new StyleDimension(-25f, 1f),
				Height = new StyleDimension(0f, 1f),
				ListPadding = 5f,
				HAlign = 0f,
				Left = new StyleDimension(0f, 0f)
			};
			uielement4.Append(uilist2);
			this._enabledPacksList = uilist2;
			UIText uitext = new UIText(Language.GetText("UI.AvailableResourcePacksTitle"), 1f, false)
			{
				HAlign = 0f,
				Left = new StyleDimension(25f, 0f),
				Width = new StyleDimension(-25f, 0.5f),
				VAlign = 0f,
				Top = new StyleDimension(10f, 0f)
			};
			this._titleAvailable = uitext;
			uipanel.Append(uitext);
			UIText uitext2 = new UIText(Language.GetText("UI.EnabledResourcePacksTitle"), 1f, false)
			{
				HAlign = 1f,
				Left = new StyleDimension(-25f, 0f),
				Width = new StyleDimension(-25f, 0.5f),
				VAlign = 0f,
				Top = new StyleDimension(10f, 0f)
			};
			this._titleEnabled = uitext2;
			uipanel.Append(uitext2);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.ResourcePacks"), 1f, true)
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Top = new StyleDimension(-44f, 0f),
				BackgroundColor = new Color(73, 94, 171)
			};
			uitextPanel.SetPadding(13f);
			uielement.Append(uitextPanel);
			UIScrollbar uiscrollbar = new UIScrollbar
			{
				Height = new StyleDimension(0f, 1f),
				HAlign = 0f,
				Left = new StyleDimension(0f, 0f)
			};
			uielement3.Append(uiscrollbar);
			this._availablePacksList.SetScrollbar(uiscrollbar);
			UIVerticalSeparator element = new UIVerticalSeparator
			{
				Height = new StyleDimension(-12f, 1f),
				HAlign = 0.5f,
				VAlign = 1f,
				Color = new Color(89, 116, 213, 255) * 0.9f
			};
			uipanel.Append(element);
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
			UIScrollbar uiscrollbar2 = new UIScrollbar
			{
				Height = new StyleDimension(0f, 1f),
				HAlign = 1f
			};
			uielement4.Append(uiscrollbar2);
			this._enabledPacksList.SetScrollbar(uiscrollbar2);
			this.AddBackAndFolderButtons(uielement);
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0057B228 File Offset: 0x00579428
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

		// Token: 0x060026D4 RID: 9940 RVA: 0x0057B28C File Offset: 0x0057948C
		private void AddBackAndFolderButtons(UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true)
			{
				Width = new StyleDimension(-8f, 0.5f),
				Height = new StyleDimension(50f, 0f),
				VAlign = 1f,
				HAlign = 0f,
				Top = new StyleDimension(-45f, 0f)
			};
			uitextPanel.OnMouseOver += UIResourcePackSelectionMenu.FadedMouseOver;
			uitextPanel.OnMouseOut += UIResourcePackSelectionMenu.FadedMouseOut;
			uitextPanel.OnLeftClick += this.GoBackClick;
			uitextPanel.SetSnapPoint("GoBack", 0, null, null);
			outerContainer.Append(uitextPanel);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("GameUI.OpenFileFolder"), 0.7f, true)
			{
				Width = new StyleDimension(-8f, 0.5f),
				Height = new StyleDimension(50f, 0f),
				VAlign = 1f,
				HAlign = 1f,
				Top = new StyleDimension(-45f, 0f)
			};
			uitextPanel2.OnMouseOver += UIResourcePackSelectionMenu.FadedMouseOver;
			uitextPanel2.OnMouseOut += UIResourcePackSelectionMenu.FadedMouseOut;
			uitextPanel2.OnLeftClick += this.OpenFoldersClick;
			uitextPanel2.SetSnapPoint("OpenFolder", 0, null, null);
			outerContainer.Append(uitextPanel2);
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0057B428 File Offset: 0x00579628
		private void OpenFoldersClick(UIMouseEvent evt, UIElement listeningElement)
		{
			JArray jarray;
			string folderPath;
			AssetInitializer.GetResourcePacksFolderPathAndConfirmItExists(out jarray, out folderPath);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			Utils.OpenFolder(folderPath);
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x0057B459 File Offset: 0x00579659
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x0057B464 File Offset: 0x00579664
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

		// Token: 0x060026D8 RID: 9944 RVA: 0x0057B4B8 File Offset: 0x005796B8
		private static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x0056D365 File Offset: 0x0056B565
		private static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x0057B50D File Offset: 0x0057970D
		private void OffsetBackwardUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.OffsetTexturePackPriorityDown");
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x0057B51A File Offset: 0x0057971A
		private void OffsetFrontwardUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.OffsetTexturePackPriorityUp");
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x0057B527 File Offset: 0x00579727
		private void EnablePackUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.EnableTexturePack");
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x0057B534 File Offset: 0x00579734
		private void DisablePackUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.DisableTexturePack");
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x0057B541 File Offset: 0x00579741
		private void SeeInfoUpdate(UIElement affectedElement)
		{
			UIResourcePackSelectionMenu.DisplayMouseTextIfHovered(affectedElement, "UI.SeeTexturePackInfo");
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x0057B550 File Offset: 0x00579750
		private static void DisplayMouseTextIfHovered(UIElement affectedElement, string textKey)
		{
			if (affectedElement.IsMouseHovering)
			{
				string textValue = Language.GetTextValue(textKey);
				Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x0057B57E File Offset: 0x0057977E
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x0057B590 File Offset: 0x00579790
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			int idRangeEndExclusive = num;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			List<SnapPoint> snapPoints2 = this._availablePacksList.GetSnapPoints();
			this._helper.CullPointsOutOfElementArea(spriteBatch, snapPoints2, this._availablePacksList);
			List<SnapPoint> snapPoints3 = this._enabledPacksList.GetSnapPoints();
			this._helper.CullPointsOutOfElementArea(spriteBatch, snapPoints3, this._enabledPacksList);
			UILinkPoint[] verticalStripFromCategoryName = this._helper.GetVerticalStripFromCategoryName(ref idRangeEndExclusive, snapPoints2, "ToggleToOn");
			UILinkPoint[] verticalStripFromCategoryName2 = this._helper.GetVerticalStripFromCategoryName(ref idRangeEndExclusive, snapPoints2, "InfoOff");
			UILinkPoint[] verticalStripFromCategoryName3 = this._helper.GetVerticalStripFromCategoryName(ref idRangeEndExclusive, snapPoints3, "ToggleToOff");
			UILinkPoint[] verticalStripFromCategoryName4 = this._helper.GetVerticalStripFromCategoryName(ref idRangeEndExclusive, snapPoints3, "InfoOn");
			UILinkPoint[] verticalStripFromCategoryName5 = this._helper.GetVerticalStripFromCategoryName(ref idRangeEndExclusive, snapPoints3, "OrderUp");
			UILinkPoint[] verticalStripFromCategoryName6 = this._helper.GetVerticalStripFromCategoryName(ref idRangeEndExclusive, snapPoints3, "OrderDown");
			UILinkPoint uilinkPoint = null;
			UILinkPoint uilinkPoint2 = null;
			for (int i = 0; i < snapPoints.Count; i++)
			{
				SnapPoint snapPoint = snapPoints[i];
				string name = snapPoint.Name;
				if (!(name == "GoBack"))
				{
					if (name == "OpenFolder")
					{
						uilinkPoint2 = this._helper.MakeLinkPointFromSnapPoint(idRangeEndExclusive++, snapPoint);
					}
				}
				else
				{
					uilinkPoint = this._helper.MakeLinkPointFromSnapPoint(idRangeEndExclusive++, snapPoint);
				}
			}
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName2, verticalStripFromCategoryName, 0);
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName, verticalStripFromCategoryName3, 0);
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName3, verticalStripFromCategoryName4, 0);
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName4, verticalStripFromCategoryName5, 0);
			this._helper.LinkVerticalStrips(verticalStripFromCategoryName5, verticalStripFromCategoryName6, 0);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName, uilinkPoint);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName2, uilinkPoint);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName5, uilinkPoint2);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName6, uilinkPoint2);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName3, uilinkPoint2);
			this._helper.LinkVerticalStripBottomSideToSingle(verticalStripFromCategoryName4, uilinkPoint2);
			this._helper.PairLeftRight(uilinkPoint, uilinkPoint2);
			this._helper.MoveToVisuallyClosestPoint(num, idRangeEndExclusive);
		}

		// Token: 0x04004A61 RID: 19041
		private readonly AssetSourceController _sourceController;

		// Token: 0x04004A62 RID: 19042
		private UIList _availablePacksList;

		// Token: 0x04004A63 RID: 19043
		private UIList _enabledPacksList;

		// Token: 0x04004A64 RID: 19044
		private ResourcePackList _packsList;

		// Token: 0x04004A65 RID: 19045
		private UIText _titleAvailable;

		// Token: 0x04004A66 RID: 19046
		private UIText _titleEnabled;

		// Token: 0x04004A67 RID: 19047
		private UIState _uiStateToGoBackTo;

		// Token: 0x04004A68 RID: 19048
		private const string _snapCategory_ToggleFromOffToOn = "ToggleToOn";

		// Token: 0x04004A69 RID: 19049
		private const string _snapCategory_ToggleFromOnToOff = "ToggleToOff";

		// Token: 0x04004A6A RID: 19050
		private const string _snapCategory_InfoWhenOff = "InfoOff";

		// Token: 0x04004A6B RID: 19051
		private const string _snapCategory_InfoWhenOn = "InfoOn";

		// Token: 0x04004A6C RID: 19052
		private const string _snapCategory_OffsetOrderUp = "OrderUp";

		// Token: 0x04004A6D RID: 19053
		private const string _snapCategory_OffsetOrderDown = "OrderDown";

		// Token: 0x04004A6E RID: 19054
		private const string _snapPointName_goBack = "GoBack";

		// Token: 0x04004A6F RID: 19055
		private const string _snapPointName_openFolder = "OpenFolder";

		// Token: 0x04004A70 RID: 19056
		private UIGamepadHelper _helper;
	}
}
