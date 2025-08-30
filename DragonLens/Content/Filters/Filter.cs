using DragonLens.Content.GUI;
using DragonLens.Helpers;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonLens.Content.Filters
{
	internal delegate bool FilterDelegate(BrowserButton button);

	/// <summary>
	/// The logical backend for FilterButton elements. Defines the data for the element as well as the filtering logic.
	/// </summary>
	internal class Filter
	{
		/// <summary>
		/// Whether this filter is a mod filter. You can only activate one mod filter an once.
		/// </summary>
		public bool isModFilter;

		/// <summary>
		/// The texture used to draw this filters icon if no custom drawing is defined.
		/// </summary>
		public Asset<Texture2D> texture;

		/// <summary>
		/// The localization key of this filter, used to get localized text for name and description
		/// </summary>
		public string localizationKey;

		/// <summary>
		/// The name of this filter, shown on the tooltip to the user when hovered over.
		/// </summary>
		public virtual string Name => LocalizationHelper.GetText($"{localizationKey}.Name");

		/// <summary>
		/// The description of this filter, shown on the tooltip to the user when hovered over.
		/// </summary>
		public virtual string Description => LocalizationHelper.GetText($"{localizationKey}.Description");

		/// <summary>
		/// Defines which elements should be filtered by this filter. Return true to filter out, false to leave in.
		/// </summary>
		public FilterDelegate shouldFilter;

		public Filter(Asset<Texture2D> texture, string localizationKey, FilterDelegate shouldFilter)
		{
			this.texture = texture;
			this.localizationKey = localizationKey;
			this.shouldFilter = shouldFilter;
		}

		private static readonly Dictionary<string, string> IconAlias = new()
		{
			{"AnyDamage", "Unknown"},
			{"Armor", "Defense"},
			{"Deprecated", "Unknown"},
			{"Buff", "Friendly"},
			{"Debuff", "Hostile"}
		};

		private static readonly Dictionary<string, Asset<Texture2D>> IconCache = new();

		private Asset<Texture2D> EnsureIcon()
		{
			try
			{
				var magic = Terraria.GameContent.TextureAssets.MagicPixel;
				if (texture != null && texture.Value != magic.Value)
					return texture;

				string key = localizationKey ?? string.Empty;
				string last = key.Contains('.') ? key[(key.LastIndexOf('.') + 1)..] : key;
				if (string.IsNullOrWhiteSpace(last)) last = "Unknown";
				if (IconAlias.TryGetValue(last, out var alias))
					last = alias;

				string path = $"DragonLens/Assets/Filters/{last}";
				if (IconCache.TryGetValue(path, out var cached))
				{
					texture = cached;
					return texture;
				}

				var req = ModContent.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad);
				texture = req;
				IconCache[path] = req;
			}
			catch
			{
				texture = Terraria.GameContent.TextureAssets.MagicPixel;
			}

			return texture;
		}

		/// <summary>
		/// Allows you to change how the filter's icon should draw
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="target">The bounding box of the filter button</param>
		public virtual void Draw(SpriteBatch spriteBatch, Rectangle target)
		{
			Texture2D tex = EnsureIcon().Value;
			int widest = tex.Width > tex.Height ? tex.Width : tex.Height;
			float scale = widest > 0 ? target.Width / (float)widest : 1f;

			Vector2 center = target.Center.ToVector2();
			Vector2 origin = tex.Size() / 2f;

			// ドロップシャドウ（細線アイコンの視認性改善）
			spriteBatch.Draw(tex, center + new Vector2(1, 1), null, Color.Black * 0.35f, 0f, origin, scale, SpriteEffects.None, 0f);

			// 本体
			spriteBatch.Draw(tex, center, null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
		}
	}
}
