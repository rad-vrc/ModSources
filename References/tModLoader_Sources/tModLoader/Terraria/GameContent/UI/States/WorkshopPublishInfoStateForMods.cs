using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.Social;
using Terraria.Social.Base;
using Terraria.Social.Steam;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004E9 RID: 1257
	public class WorkshopPublishInfoStateForMods : AWorkshopPublishInfoState<TmodFile>
	{
		// Token: 0x06003D09 RID: 15625 RVA: 0x005C74F8 File Offset: 0x005C56F8
		public WorkshopPublishInfoStateForMods(UIState stateToGoBackTo, TmodFile modFile, NameValueCollection buildData) : base(stateToGoBackTo, modFile)
		{
			this._instructionsTextKey = "Workshop.ModPublishDescription";
			this._publishedObjectNameDescriptorTexKey = "Workshop.ModName";
			this._buildData = buildData;
			this._previewImagePath = buildData["iconpath"];
			this.changeNotes = buildData["changelog"];
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x005C754C File Offset: 0x005C574C
		protected override string GetPublishedObjectDisplayName()
		{
			return this._dataObject.Name;
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x005C755C File Offset: 0x005C575C
		protected override void GoToPublishConfirmation()
		{
			bool resizedPreviewImage = false;
			Texture2D texture2D;
			int newWidth;
			int newHeight;
			if (this.CheckPreviewImageNeedsResizing(out texture2D, out newWidth, out newHeight))
			{
				string previewImagePath = this._previewImagePath;
				string dstPath = Path.Combine(Path.GetTempPath(), "icon_workshop.png." + DateTime.Now.Ticks.ToString());
				WorkshopPublishInfoStateForMods.UpscaleAndSaveImageAsPng(previewImagePath, dstPath, newWidth, newHeight);
				this._previewImagePath = dstPath;
				resizedPreviewImage = true;
			}
			SocialAPI.Workshop.PublishMod(this._dataObject, this._buildData, base.GetPublishSettings());
			UIState currentState = Main.MenuUI.CurrentState;
			if (((currentState != null) ? currentState.GetType() : null) != typeof(UIReportsPage))
			{
				string iconWorkshopPath = Path.Combine(this._buildData["sourcesfolder"], "icon_workshop.png");
				if (this._previewImagePath != iconWorkshopPath && !resizedPreviewImage && !File.Exists(iconWorkshopPath))
				{
					try
					{
						File.Copy(this._previewImagePath, iconWorkshopPath, true);
						this._previewImagePath = iconWorkshopPath;
					}
					catch
					{
					}
				}
				Main.menuMode = 888;
				Main.MenuUI.SetState(this._previousUIState);
			}
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x005C7680 File Offset: 0x005C5880
		protected override List<WorkshopTagOption> GetTagsToShow()
		{
			return SteamedWraps.ModTags;
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x005C7687 File Offset: 0x005C5887
		protected override bool TryFindingTags(out FoundWorkshopEntryInfo info)
		{
			return SocialAPI.Workshop.TryGetInfoForMod(this._dataObject, out info);
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x005C769C File Offset: 0x005C589C
		internal UIElement CreateTmlDisclaimer(string tagGroup)
		{
			float num = 60f;
			float num2 = 0f + num;
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 1f, 0.5f, 16f);
			groupOptionButton.HAlign = 0.5f;
			groupOptionButton.VAlign = 0f;
			groupOptionButton.Width = StyleDimension.FromPixelsAndPercent(0f, 1f);
			groupOptionButton.Left = StyleDimension.FromPixels(0f);
			groupOptionButton.Height = StyleDimension.FromPixelsAndPercent(num2 + 4f, 0f);
			groupOptionButton.Top = StyleDimension.FromPixels(0f);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetCurrentOption(false);
			groupOptionButton.Width.Set(0f, 1f);
			UIElement uIElement = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(num, 0f)
			};
			groupOptionButton.Append(uIElement);
			UIText uIText = new UIText(Language.GetText("tModLoader.WorkshopDisclaimer"), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-40f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				TextColor = Color.Cyan,
				IgnoresMouseInteraction = true
			};
			uIText.PaddingLeft = 20f;
			uIText.PaddingRight = 20f;
			uIText.PaddingTop = 4f;
			uIText.IsWrapped = true;
			this._tMLDisclaimerText = uIText;
			groupOptionButton.OnLeftClick += this.TmlDisclaimerText_OnClick;
			groupOptionButton.OnMouseOver += this.TmlDisclaimerText_OnMouseOver;
			groupOptionButton.OnMouseOut += this.TmlDisclaimerText_OnMouseOut;
			uIElement.Append(uIText);
			uIText.SetSnapPoint(tagGroup, 0, null, null);
			this._tMLDisclaimerButton = uIText;
			return groupOptionButton;
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x005C7899 File Offset: 0x005C5A99
		private void TmlDisclaimerText_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._tMLDisclaimerText.TextColor = Color.Cyan;
			base.ClearOptionDescription(evt, listeningElement);
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x005C78B3 File Offset: 0x005C5AB3
		private void TmlDisclaimerText_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._tMLDisclaimerText.TextColor = Color.LightCyan;
			base.ShowOptionDescription(evt, listeningElement);
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x005C78E2 File Offset: 0x005C5AE2
		private void TmlDisclaimerText_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Utils.OpenToURL("https://forums.terraria.org/index.php?threads/player-created-game-enhancements-rules-guidelines.286/");
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x005C78F0 File Offset: 0x005C5AF0
		public override void OnInitialize()
		{
			base.OnInitialize();
			Mod mod;
			if (ModLoader.TryGetMod(this._dataObject.Name, out mod))
			{
				Dictionary<GameCulture, int> localizationCounts = LocalizationLoader.GetLocalizationCounts(mod);
				int countMaxEntries = localizationCounts.DefaultIfEmpty<KeyValuePair<GameCulture, int>>().Max((KeyValuePair<GameCulture, int> x) => x.Value);
				Logging.tML.Info("Determining localization progress for " + mod.Name + ":");
				foreach (GroupOptionButton<WorkshopTagOption> tagOption in this._tagOptions)
				{
					if (tagOption.OptionValue.NameKey.StartsWith("tModLoader.TagsLanguage_"))
					{
						string text = tagOption.OptionValue.NameKey.Split('_', StringSplitOptions.None)[1];
						if (text != null)
						{
							GameCulture gameCulture;
							switch (text.Length)
							{
							case 6:
							{
								char c = text[0];
								if (c != 'F')
								{
									if (c != 'G')
									{
										if (c != 'P')
										{
											goto IL_28D;
										}
										if (!(text == "Polish"))
										{
											goto IL_28D;
										}
										gameCulture = GameCulture.FromName("pl-PL");
									}
									else
									{
										if (!(text == "German"))
										{
											goto IL_28D;
										}
										gameCulture = GameCulture.FromName("de-DE");
									}
								}
								else
								{
									if (!(text == "French"))
									{
										goto IL_28D;
									}
									gameCulture = GameCulture.FromName("fr-FR");
								}
								break;
							}
							case 7:
							{
								char c = text[0];
								if (c <= 'E')
								{
									if (c != 'C')
									{
										if (c != 'E')
										{
											goto IL_28D;
										}
										if (!(text == "English"))
										{
											goto IL_28D;
										}
										gameCulture = GameCulture.FromName("en-US");
									}
									else
									{
										if (!(text == "Chinese"))
										{
											goto IL_28D;
										}
										gameCulture = GameCulture.FromName("zh-Hans");
									}
								}
								else if (c != 'I')
								{
									if (c != 'R')
									{
										if (c != 'S')
										{
											goto IL_28D;
										}
										if (!(text == "Spanish"))
										{
											goto IL_28D;
										}
										gameCulture = GameCulture.FromName("es-ES");
									}
									else
									{
										if (!(text == "Russian"))
										{
											goto IL_28D;
										}
										gameCulture = GameCulture.FromName("ru-RU");
									}
								}
								else
								{
									if (!(text == "Italian"))
									{
										goto IL_28D;
									}
									gameCulture = GameCulture.FromName("it-IT");
								}
								break;
							}
							case 8:
							case 9:
								goto IL_28D;
							case 10:
								if (!(text == "Portuguese"))
								{
									goto IL_28D;
								}
								gameCulture = GameCulture.FromName("pt-BR");
								break;
							default:
								goto IL_28D;
							}
							GameCulture culture = gameCulture;
							int countOtherEntries;
							localizationCounts.TryGetValue(culture, out countOtherEntries);
							float localizationProgress = (float)countOtherEntries / (float)countMaxEntries;
							ILog tML = Logging.tML;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 5);
							defaultInterpolatedStringHandler.AppendFormatted(culture.Name);
							defaultInterpolatedStringHandler.AppendLiteral(", ");
							defaultInterpolatedStringHandler.AppendFormatted<int>(countOtherEntries);
							defaultInterpolatedStringHandler.AppendLiteral("/");
							defaultInterpolatedStringHandler.AppendFormatted<int>(countMaxEntries);
							defaultInterpolatedStringHandler.AppendLiteral(", ");
							defaultInterpolatedStringHandler.AppendFormatted<float>(localizationProgress, "P0");
							defaultInterpolatedStringHandler.AppendLiteral(", missing ");
							defaultInterpolatedStringHandler.AppendFormatted<int>(countMaxEntries - countOtherEntries);
							tML.Info(defaultInterpolatedStringHandler.ToStringAndClear());
							bool languageMostlyLocalized = localizationProgress > 0.75f;
							bool languagePreviouslyLocalizedAndStillEnough = tagOption.IsSelected && localizationProgress > 0.5f;
							tagOption.SetCurrentOption((languageMostlyLocalized || languagePreviouslyLocalizedAndStillEnough) ? tagOption.OptionValue : null);
							tagOption.SetColor(tagOption.IsSelected ? (languageMostlyLocalized ? new Color(192, 175, 235) : new Color(255, 175, 235)) : Colors.InventoryDefaultColor, 1f);
							continue;
						}
						IL_28D:
						throw new NotImplementedException();
					}
				}
			}
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x005C7CE8 File Offset: 0x005C5EE8
		internal void AddNonModOwnerPublishWarning(UIList uiList)
		{
			QueryParameters queryParameters = default(QueryParameters);
			queryParameters.searchModSlugs = new string[]
			{
				this._dataObject.Name
			};
			queryParameters.queryType = QueryType.SearchDirect;
			ModDownloadItem mod;
			if (!WorkshopHelper.TryGetModDownloadItem(this._dataObject.Name, out mod) || mod == null)
			{
				return;
			}
			ulong existingAuthorID = ulong.Parse(mod.OwnerId);
			if (existingAuthorID == 0UL || existingAuthorID == SteamUser.GetSteamID().m_SteamID)
			{
				return;
			}
			float num = 180f;
			float num2 = 0f + num;
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 1f, 0.5f, 16f)
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Left = StyleDimension.FromPixels(0f),
				Height = StyleDimension.FromPixelsAndPercent(num2 + 4f, 0f),
				Top = StyleDimension.FromPixels(0f),
				ShowHighlightWhenSelected = false
			};
			groupOptionButton.SetCurrentOption(false);
			groupOptionButton.Width.Set(0f, 1f);
			UIElement uIElement = new UIElement
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(num, 0f)
			};
			uIElement.OnLeftClick += delegate(UIMouseEvent sender, UIElement e)
			{
				Utils.OpenToURL("https://github.com/tModLoader/tModLoader/wiki/Workshop#renaming-a-mod");
			};
			groupOptionButton.Append(uIElement);
			UIText uIText = new UIText(Language.GetTextValue("tModLoader.NonModOwnerPublishWarning", this._dataObject.Name), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				TextColor = Color.Yellow,
				IgnoresMouseInteraction = true
			};
			uIText.PaddingLeft = 20f;
			uIText.PaddingRight = 20f;
			uIText.PaddingTop = 4f;
			uIText.IsWrapped = true;
			uIElement.Append(uIText);
			uIText.SetSnapPoint("warning", 0, null, null);
			uiList.Add(groupOptionButton);
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x005C7F50 File Offset: 0x005C6150
		protected override void UpdateImagePreview()
		{
			base.UpdateImagePreview();
			if (this.imageWarningText == null)
			{
				this.imageWarningText = new UIText(string.Empty, 1f, false)
				{
					TextOriginX = 0f,
					TextOriginY = 0.5f,
					Width = this._previewImagePathPlate.Width,
					Height = new StyleDimension(0f, 0f),
					Left = new StyleDimension(10f, 0f),
					Top = new StyleDimension(10f, 0f),
					TextColor = new Color(106, 190, 48)
				};
				this._previewImagePathPlate.Parent.Append(this.imageWarningText);
			}
			Texture2D tex;
			int newWidth;
			int newHeight;
			if (this.CheckPreviewImageNeedsResizing(out tex, out newWidth, out newHeight))
			{
				UIText uitext = this.imageWarningText;
				string key = "tModLoader.ModWorkshopIconResizeWarning";
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted<int>(tex.Width);
				defaultInterpolatedStringHandler.AppendLiteral("x");
				defaultInterpolatedStringHandler.AppendFormatted<int>(tex.Height);
				object arg = defaultInterpolatedStringHandler.ToStringAndClear();
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted<int>(newWidth);
				defaultInterpolatedStringHandler.AppendLiteral("x");
				defaultInterpolatedStringHandler.AppendFormatted<int>(newHeight);
				uitext.SetText(Language.GetTextValue(key, arg, defaultInterpolatedStringHandler.ToStringAndClear()));
				return;
			}
			this.imageWarningText.SetText(string.Empty);
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x005C80AC File Offset: 0x005C62AC
		private bool CheckPreviewImageNeedsResizing(out Texture2D texture, out int newWidth, out int newHeight)
		{
			if (this._previewImagePath != null)
			{
				Texture2D tex = this._previewImageTransientTexture;
				if (tex != null)
				{
					Texture2D texture2D = tex;
					texture = texture2D;
					newWidth = 480;
					newHeight = 480;
					return tex.Width < 480 || tex.Height < 480;
				}
			}
			texture = null;
			newWidth = 0;
			newHeight = 0;
			return false;
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x005C8108 File Offset: 0x005C6308
		internal static void UpscaleAndSaveImageAsPng(string srcImagePath, string dstImagePath, int dstWidth, int dstHeight)
		{
			using (FileStream srcStream = File.OpenRead(srcImagePath))
			{
				int srcWidth;
				int srcHeight;
				byte[] srcBytes;
				Texture2D.TextureDataFromStreamEXT(srcStream, ref srcWidth, ref srcHeight, ref srcBytes, -1, -1, false);
				using (FileStream dstStream = File.OpenWrite(dstImagePath))
				{
					PlatformUtilities.SavePng(dstStream, srcWidth, srcHeight, dstWidth, dstHeight, srcBytes);
				}
			}
		}

		// Token: 0x040055F8 RID: 22008
		public const string TmlRules = "https://forums.terraria.org/index.php?threads/player-created-game-enhancements-rules-guidelines.286/";

		// Token: 0x040055F9 RID: 22009
		private readonly NameValueCollection _buildData;

		// Token: 0x040055FA RID: 22010
		protected UIText imageWarningText;

		// Token: 0x040055FB RID: 22011
		internal string changeNotes;
	}
}
