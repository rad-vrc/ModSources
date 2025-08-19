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
	// Token: 0x020002B8 RID: 696
	public class CreativePowersHelper
	{
		// Token: 0x06002237 RID: 8759 RVA: 0x005421EE File Offset: 0x005403EE
		private static Asset<Texture2D> GetPowerIconAsset(string path)
		{
			return Main.Assets.Request<Texture2D>(path, 1);
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x005421FC File Offset: 0x005403FC
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

		// Token: 0x06002239 RID: 8761 RVA: 0x00542264 File Offset: 0x00540464
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

		// Token: 0x0600223A RID: 8762 RVA: 0x00542310 File Offset: 0x00540510
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

		// Token: 0x0600223B RID: 8763 RVA: 0x005423A0 File Offset: 0x005405A0
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

		// Token: 0x0600223C RID: 8764 RVA: 0x00542430 File Offset: 0x00540630
		public static void AddPermissionTextIfNeeded(ICreativePower power, ref string originalText)
		{
			if (!CreativePowersHelper.IsAvailableForPlayer(power, Main.myPlayer))
			{
				string textValue = Language.GetTextValue("CreativePowers.CantUsePowerBecauseOfNoPermissionFromServer");
				originalText = originalText + "\n" + textValue;
			}
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x00542464 File Offset: 0x00540664
		public static void AddDescriptionIfNeeded(ref string originalText, string descriptionKey)
		{
			if (!CreativePowerSettings.ShouldPowersBeElaborated)
			{
				return;
			}
			string textValue = Language.GetTextValue(descriptionKey);
			originalText = originalText + "\n" + textValue;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x00542490 File Offset: 0x00540690
		public static void AddUnlockTextIfNeeded(ref string originalText, bool needed, string descriptionKey)
		{
			if (needed)
			{
				return;
			}
			string textValue = Language.GetTextValue(descriptionKey);
			originalText = originalText + "\n" + textValue;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x005424B8 File Offset: 0x005406B8
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

		// Token: 0x06002240 RID: 8768 RVA: 0x0054253D File Offset: 0x0054073D
		public static void UpdateUseMouseInterface(UIElement affectedElement)
		{
			if (affectedElement.IsMouseHovering)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x00542554 File Offset: 0x00540754
		public static void UpdateUnlockStateByPower(ICreativePower power, UIElement button, Color colorWhenSelected)
		{
			IGroupOptionButton asButton = button as IGroupOptionButton;
			if (asButton == null)
			{
				return;
			}
			button.OnUpdate += delegate(UIElement element)
			{
				CreativePowersHelper.UpdateUnlockStateByPowerInternal(power, colorWhenSelected, asButton);
			};
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0054259C File Offset: 0x0054079C
		public static bool IsAvailableForPlayer(ICreativePower power, int playerIndex)
		{
			switch (power.CurrentPermissionLevel)
			{
			default:
				return false;
			case PowerPermissionLevel.CanBeChangedByHostAlone:
				return Main.netMode == 0 || Main.countsAsHostForGameplay[playerIndex];
			case PowerPermissionLevel.CanBeChangedByEveryone:
				return true;
			}
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x005425D8 File Offset: 0x005407D8
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

		// Token: 0x040047B9 RID: 18361
		public const int TextureIconColumns = 21;

		// Token: 0x040047BA RID: 18362
		public const int TextureIconRows = 1;

		// Token: 0x040047BB RID: 18363
		public static Color CommonSelectedColor = new Color(152, 175, 235);

		// Token: 0x02000697 RID: 1687
		public class CreativePowerIconLocations
		{
			// Token: 0x040061B9 RID: 25017
			public static readonly Point Unassigned = new Point(0, 0);

			// Token: 0x040061BA RID: 25018
			public static readonly Point Deprecated = new Point(0, 0);

			// Token: 0x040061BB RID: 25019
			public static readonly Point ItemDuplication = new Point(0, 0);

			// Token: 0x040061BC RID: 25020
			public static readonly Point ItemResearch = new Point(1, 0);

			// Token: 0x040061BD RID: 25021
			public static readonly Point TimeCategory = new Point(2, 0);

			// Token: 0x040061BE RID: 25022
			public static readonly Point WeatherCategory = new Point(3, 0);

			// Token: 0x040061BF RID: 25023
			public static readonly Point EnemyStrengthSlider = new Point(4, 0);

			// Token: 0x040061C0 RID: 25024
			public static readonly Point GameEvents = new Point(5, 0);

			// Token: 0x040061C1 RID: 25025
			public static readonly Point Godmode = new Point(6, 0);

			// Token: 0x040061C2 RID: 25026
			public static readonly Point BlockPlacementRange = new Point(7, 0);

			// Token: 0x040061C3 RID: 25027
			public static readonly Point StopBiomeSpread = new Point(8, 0);

			// Token: 0x040061C4 RID: 25028
			public static readonly Point EnemySpawnRate = new Point(9, 0);

			// Token: 0x040061C5 RID: 25029
			public static readonly Point FreezeTime = new Point(10, 0);

			// Token: 0x040061C6 RID: 25030
			public static readonly Point TimeDawn = new Point(11, 0);

			// Token: 0x040061C7 RID: 25031
			public static readonly Point TimeNoon = new Point(12, 0);

			// Token: 0x040061C8 RID: 25032
			public static readonly Point TimeDusk = new Point(13, 0);

			// Token: 0x040061C9 RID: 25033
			public static readonly Point TimeMidnight = new Point(14, 0);

			// Token: 0x040061CA RID: 25034
			public static readonly Point WindDirection = new Point(15, 0);

			// Token: 0x040061CB RID: 25035
			public static readonly Point WindFreeze = new Point(16, 0);

			// Token: 0x040061CC RID: 25036
			public static readonly Point RainStrength = new Point(17, 0);

			// Token: 0x040061CD RID: 25037
			public static readonly Point RainFreeze = new Point(18, 0);

			// Token: 0x040061CE RID: 25038
			public static readonly Point ModifyTime = new Point(19, 0);

			// Token: 0x040061CF RID: 25039
			public static readonly Point PersonalCategory = new Point(20, 0);
		}
	}
}
