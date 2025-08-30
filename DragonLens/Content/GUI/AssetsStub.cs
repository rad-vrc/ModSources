using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace DragonLens
{
	// AssGen が未生成のときにのみ使われる最小限スタブ
	internal static class Assets
	{
		internal static class GUI
		{
			public static Asset<Texture2D> Refresh => TextureAssets.MagicPixel; // 代替
			public static Asset<Texture2D> TabWide => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Gradient => TextureAssets.MagicPixel;
			public static Asset<Texture2D> NoBox => TextureAssets.MagicPixel;
			public static Asset<Texture2D> AdminIcon => TextureAssets.MagicPixel;
			public static Asset<Texture2D> KickIcon => TextureAssets.MagicPixel;
			public static Asset<Texture2D> InventoryIcon => TextureAssets.MagicPixel;
			public static Asset<Texture2D> StalkIcon => TextureAssets.MagicPixel;
			public static Asset<Texture2D> ColorScale => TextureAssets.MagicPixel;
			public static Asset<Texture2D> CloudScale => TextureAssets.MagicPixel;
			public static Asset<Texture2D> WindScale => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Rain => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Sandstorm => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Play => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Pause => TextureAssets.MagicPixel;
			public static Asset<Texture2D> TimeScale => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Remove => TextureAssets.MagicPixel;
			public static Asset<Texture2D> StructureHelper => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Picker => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Help => TextureAssets.MagicPixel;
			public static Asset<Texture2D> NewBar => TextureAssets.MagicPixel;
			public static Asset<Texture2D> SaveLayout => TextureAssets.MagicPixel;
			public static Asset<Texture2D> LoadLayout => TextureAssets.MagicPixel;
			public static Asset<Texture2D> StyleButton => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Star => TextureAssets.MagicPixel;
			public static Asset<Texture2D> SpecialTabs => TextureAssets.MagicPixel;
			public static Asset<Texture2D> AlphaScale => TextureAssets.MagicPixel;
			public static Asset<Texture2D> RedScale => TextureAssets.MagicPixel;
			public static Asset<Texture2D> GreenScale => TextureAssets.MagicPixel;
			public static Asset<Texture2D> BlueScale => TextureAssets.MagicPixel;
		}

		internal static class Filters
		{
			public static Asset<Texture2D> Magic => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Vanilla => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Friendly => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Hostile => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Critter => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Boss => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Pets => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Ammo => TextureAssets.MagicPixel;
			public static Asset<Texture2D> tModLoader => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Unknown => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Melee => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Ranged => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Summon => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Throwing => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Defense => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Accessory => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Wings => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Hooks => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Mounts => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Vanity => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Pickaxe => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Axe => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Hammer => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Placeable => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Consumables => TextureAssets.MagicPixel;
			public static Asset<Texture2D> MakeNPC => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Expert => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Master => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Material => TextureAssets.MagicPixel;
		}

		internal static class Misc
		{
			public static Asset<Texture2D> GlowAlpha => TextureAssets.MagicPixel;
			public static Asset<Texture2D> Sound => TextureAssets.MagicPixel;
			public static Asset<Texture2D> PreviewSimple => TextureAssets.MagicPixel;
			public static Asset<Texture2D> PreviewAdv => TextureAssets.MagicPixel;
			public static Asset<Texture2D> PreviewHeros => TextureAssets.MagicPixel;
			public static Asset<Texture2D> PreviewCheatsheet => TextureAssets.MagicPixel;
		}

		internal static class Themes
		{
			internal static class BoxProviders
			{
				internal static class MinimalBoxes
				{
					public static Asset<Texture2D> Box => TextureAssets.MagicPixel;
				}
				internal static class SimpleBoxes
				{
					public static Asset<Texture2D> Box => TextureAssets.MagicPixel;
				}
				internal static class VanillaBoxes
				{
					public static Asset<Texture2D> Box => TextureAssets.MagicPixel;
				}
			}
		}
	}
}

