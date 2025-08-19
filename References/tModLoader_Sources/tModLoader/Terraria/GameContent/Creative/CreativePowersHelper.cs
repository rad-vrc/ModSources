using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
	// Token: 0x02000643 RID: 1603
	public class CreativePowersHelper
	{
		// Token: 0x0600465F RID: 18015 RVA: 0x00631686 File Offset: 0x0062F886
		private static Asset<Texture2D> GetPowerIconAsset(string path)
		{
			return Main.Assets.Request<Texture2D>(path);
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x00631694 File Offset: 0x0062F894
		public static UIImageFramed GetIconImage(Point iconLocation)
		{
			Asset<Texture2D> powerIconAsset = CreativePowersHelper.GetPowerIconAsset("Images/UI/Creative/Infinite_Powers");
			return new UIImageFramed(powerIconAsset, powerIconAsset.Frame(21, 1, iconLocation.X, iconLocation.Y, 0, 0))
			{
				MarginLeft = 4f,
				MarginTop = 4f,
				VAlign = 0.5f,
				HAlign = 1f,
				IgnoresMouseInteraction = true
			};
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x006316FC File Offset: 0x0062F8FC
		public static GroupOptionButton<bool> CreateToggleButton(CreativePowerUIElementRequestInfo info)
		{
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 0.8f, 0.5f, 10f);
			groupOptionButton.Width = new StyleDimension((float)info.PreferredButtonWidth, 0f);
			groupOptionButton.Height = new StyleDimension((float)info.PreferredButtonHeight, 0f);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetCurrentOption(false);
			groupOptionButton.SetColorsBasedOnSelectionState(new Color(152, 175, 235), Colors.InventoryDefaultColor, 1f, 0.7f);
			groupOptionButton.SetColorsBasedOnSelectionState(Main.OurFavoriteColor, Colors.InventoryDefaultColor, 1f, 0.7f);
			return groupOptionButton;
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x006317A8 File Offset: 0x0062F9A8
		public static GroupOptionButton<bool> CreateSimpleButton(CreativePowerUIElementRequestInfo info)
		{
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 0.8f, 0.5f, 10f);
			groupOptionButton.Width = new StyleDimension((float)info.PreferredButtonWidth, 0f);
			groupOptionButton.Height = new StyleDimension((float)info.PreferredButtonHeight, 0f);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetCurrentOption(false);
			groupOptionButton.SetColorsBasedOnSelectionState(new Color(152, 175, 235), Colors.InventoryDefaultColor, 1f, 0.7f);
			return groupOptionButton;
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x00631838 File Offset: 0x0062FA38
		public static GroupOptionButton<T> CreateCategoryButton<T>(CreativePowerUIElementRequestInfo info, T option, T currentOption) where T : IConvertible, IEquatable<T>
		{
			GroupOptionButton<T> groupOptionButton = new GroupOptionButton<T>(option, null, null, Color.White, null, 0.8f, 0.5f, 10f);
			groupOptionButton.Width = new StyleDimension((float)info.PreferredButtonWidth, 0f);
			groupOptionButton.Height = new StyleDimension((float)info.PreferredButtonHeight, 0f);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetCurrentOption(currentOption);
			groupOptionButton.SetColorsBasedOnSelectionState(new Color(152, 175, 235), Colors.InventoryDefaultColor, 1f, 0.7f);
			return groupOptionButton;
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x006318C8 File Offset: 0x0062FAC8
		public static void AddPermissionTextIfNeeded(ICreativePower power, ref string originalText)
		{
			if (!CreativePowersHelper.IsAvailableForPlayer(power, Main.myPlayer))
			{
				string textValue = Language.GetTextValue("CreativePowers.CantUsePowerBecauseOfNoPermissionFromServer");
				originalText = originalText + "\n" + textValue;
			}
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x006318FC File Offset: 0x0062FAFC
		public static void AddDescriptionIfNeeded(ref string originalText, string descriptionKey)
		{
			if (CreativePowerSettings.ShouldPowersBeElaborated)
			{
				string textValue = Language.GetTextValue(descriptionKey);
				originalText = originalText + "\n" + textValue;
			}
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x00631928 File Offset: 0x0062FB28
		public static void AddUnlockTextIfNeeded(ref string originalText, bool needed, string descriptionKey)
		{
			if (!needed)
			{
				string textValue = Language.GetTextValue(descriptionKey);
				originalText = originalText + "\n" + textValue;
			}
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x00631950 File Offset: 0x0062FB50
		public static UIVerticalSlider CreateSlider(Func<float> GetSliderValueMethod, Action<float> SetValueKeyboardMethod, Action SetValueGamepadMethod)
		{
			return new UIVerticalSlider(GetSliderValueMethod, SetValueKeyboardMethod, SetValueGamepadMethod, Color.Red)
			{
				Width = new StyleDimension(12f, 0f),
				Height = new StyleDimension(-10f, 1f),
				Left = new StyleDimension(6f, 0f),
				HAlign = 0f,
				VAlign = 0.5f,
				EmptyColor = Color.OrangeRed,
				FilledColor = Color.CornflowerBlue
			};
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x006319D5 File Offset: 0x0062FBD5
		public static void UpdateUseMouseInterface(UIElement affectedElement)
		{
			if (affectedElement.IsMouseHovering)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x006319EC File Offset: 0x0062FBEC
		public static void UpdateUnlockStateByPower(ICreativePower power, UIElement button, Color colorWhenSelected)
		{
			IGroupOptionButton asButton = button as IGroupOptionButton;
			if (asButton != null)
			{
				button.OnUpdate += delegate(UIElement <p0>)
				{
					CreativePowersHelper.UpdateUnlockStateByPowerInternal(power, colorWhenSelected, asButton);
				};
			}
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x00631A34 File Offset: 0x0062FC34
		public static bool IsAvailableForPlayer(ICreativePower power, int playerIndex)
		{
			PowerPermissionLevel currentPermissionLevel = power.CurrentPermissionLevel;
			if (currentPermissionLevel != PowerPermissionLevel.CanBeChangedByHostAlone)
			{
				return currentPermissionLevel == PowerPermissionLevel.CanBeChangedByEveryone;
			}
			return Main.netMode == 0 || Main.countsAsHostForGameplay[playerIndex];
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x00631A64 File Offset: 0x0062FC64
		private static void UpdateUnlockStateByPowerInternal(ICreativePower power, Color colorWhenSelected, IGroupOptionButton asButton)
		{
			bool isUnlocked = power.GetIsUnlocked();
			bool flag = !CreativePowersHelper.IsAvailableForPlayer(power, Main.myPlayer);
			asButton.SetBorderColor(flag ? Color.DimGray : Color.White);
			if (flag)
			{
				asButton.SetColorsBasedOnSelectionState(new Color(60, 60, 60), new Color(60, 60, 60), 0.7f, 0.7f);
				return;
			}
			if (isUnlocked)
			{
				asButton.SetColorsBasedOnSelectionState(colorWhenSelected, Colors.InventoryDefaultColor, 1f, 0.7f);
				return;
			}
			asButton.SetColorsBasedOnSelectionState(Color.Crimson, Color.Red, 0.7f, 0.7f);
		}

		// Token: 0x04005B72 RID: 23410
		public const int TextureIconColumns = 21;

		// Token: 0x04005B73 RID: 23411
		public const int TextureIconRows = 1;

		// Token: 0x04005B74 RID: 23412
		public static Color CommonSelectedColor = new Color(152, 175, 235);

		// Token: 0x02000CF0 RID: 3312
		public class CreativePowerIconLocations
		{
			// Token: 0x04007A76 RID: 31350
			public static readonly Point Unassigned = new Point(0, 0);

			// Token: 0x04007A77 RID: 31351
			public static readonly Point Deprecated = new Point(0, 0);

			// Token: 0x04007A78 RID: 31352
			public static readonly Point ItemDuplication = new Point(0, 0);

			// Token: 0x04007A79 RID: 31353
			public static readonly Point ItemResearch = new Point(1, 0);

			// Token: 0x04007A7A RID: 31354
			public static readonly Point TimeCategory = new Point(2, 0);

			// Token: 0x04007A7B RID: 31355
			public static readonly Point WeatherCategory = new Point(3, 0);

			// Token: 0x04007A7C RID: 31356
			public static readonly Point EnemyStrengthSlider = new Point(4, 0);

			// Token: 0x04007A7D RID: 31357
			public static readonly Point GameEvents = new Point(5, 0);

			// Token: 0x04007A7E RID: 31358
			public static readonly Point Godmode = new Point(6, 0);

			// Token: 0x04007A7F RID: 31359
			public static readonly Point BlockPlacementRange = new Point(7, 0);

			// Token: 0x04007A80 RID: 31360
			public static readonly Point StopBiomeSpread = new Point(8, 0);

			// Token: 0x04007A81 RID: 31361
			public static readonly Point EnemySpawnRate = new Point(9, 0);

			// Token: 0x04007A82 RID: 31362
			public static readonly Point FreezeTime = new Point(10, 0);

			// Token: 0x04007A83 RID: 31363
			public static readonly Point TimeDawn = new Point(11, 0);

			// Token: 0x04007A84 RID: 31364
			public static readonly Point TimeNoon = new Point(12, 0);

			// Token: 0x04007A85 RID: 31365
			public static readonly Point TimeDusk = new Point(13, 0);

			// Token: 0x04007A86 RID: 31366
			public static readonly Point TimeMidnight = new Point(14, 0);

			// Token: 0x04007A87 RID: 31367
			public static readonly Point WindDirection = new Point(15, 0);

			// Token: 0x04007A88 RID: 31368
			public static readonly Point WindFreeze = new Point(16, 0);

			// Token: 0x04007A89 RID: 31369
			public static readonly Point RainStrength = new Point(17, 0);

			// Token: 0x04007A8A RID: 31370
			public static readonly Point RainFreeze = new Point(18, 0);

			// Token: 0x04007A8B RID: 31371
			public static readonly Point ModifyTime = new Point(19, 0);

			// Token: 0x04007A8C RID: 31372
			public static readonly Point PersonalCategory = new Point(20, 0);
		}
	}
}
