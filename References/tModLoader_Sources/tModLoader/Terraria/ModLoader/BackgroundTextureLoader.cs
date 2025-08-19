using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This is the class that keeps track of all modded background textures and their slots/IDs.
	/// <para /> Remember that unless you manually register backgrounds via <see cref="M:Terraria.ModLoader.BackgroundTextureLoader.AddBackgroundTexture(Terraria.ModLoader.Mod,System.String)" />, only files found in a folder or subfolder of a folder named "Backgrounds" will be autoloaded as background textures.
	/// </summary>
	// Token: 0x02000143 RID: 323
	[Autoload(true, Side = ModSide.Client)]
	public sealed class BackgroundTextureLoader : Loader
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x004CD1A9 File Offset: 0x004CB3A9
		private static BackgroundTextureLoader Instance
		{
			get
			{
				return LoaderManager.Get<BackgroundTextureLoader>();
			}
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x004CD1B0 File Offset: 0x004CB3B0
		public BackgroundTextureLoader()
		{
			base.Initialize(Main.maxBackgrounds);
		}

		/// <summary> Returns the slot/ID of the background texture with the given full path. The path must be prefixed with a mod name. Throws exceptions on failure. </summary>
		// Token: 0x06001AC0 RID: 6848 RVA: 0x004CD1C3 File Offset: 0x004CB3C3
		public static int GetBackgroundSlot(string texture)
		{
			return BackgroundTextureLoader.backgrounds[texture];
		}

		/// <summary> Returns the slot/ID of the background texture with the given mod and path. Throws exceptions on failure. </summary>
		// Token: 0x06001AC1 RID: 6849 RVA: 0x004CD1D0 File Offset: 0x004CB3D0
		public static int GetBackgroundSlot(Mod mod, string texture)
		{
			return BackgroundTextureLoader.GetBackgroundSlot(mod.Name + "/" + texture);
		}

		/// <summary> Safely attempts to output the slot/ID of the background texture with the given full path. The path must be prefixed with a mod name. </summary>
		// Token: 0x06001AC2 RID: 6850 RVA: 0x004CD1E8 File Offset: 0x004CB3E8
		public static bool TryGetBackgroundSlot(string texture, out int slot)
		{
			return BackgroundTextureLoader.backgrounds.TryGetValue(texture, out slot);
		}

		/// <summary> Safely attempts to output the slot/ID of the background texture with the given mod and path. </summary>
		// Token: 0x06001AC3 RID: 6851 RVA: 0x004CD1F6 File Offset: 0x004CB3F6
		public static bool TryGetBackgroundSlot(Mod mod, string texture, out int slot)
		{
			return BackgroundTextureLoader.TryGetBackgroundSlot(mod.Name + "/" + texture, out slot);
		}

		/// <summary>
		/// Adds a texture to the list of background textures and assigns it a background texture slot.
		/// <para /> Use this for any background textures not autoloaded by the <see cref="P:Terraria.ModLoader.Mod.BackgroundAutoloadingEnabled" /> logic.
		/// </summary>
		/// <param name="mod">The mod that owns this background.</param>
		/// <param name="texture">The texture.</param>
		// Token: 0x06001AC4 RID: 6852 RVA: 0x004CD210 File Offset: 0x004CB410
		public static void AddBackgroundTexture(Mod mod, string texture)
		{
			if (mod == null)
			{
				throw new ArgumentNullException("mod");
			}
			if (texture == null)
			{
				throw new ArgumentNullException("texture");
			}
			if (!mod.loading)
			{
				throw new Exception(Language.GetTextValue("tModLoader.LoadErrorNotLoading"));
			}
			ModContent.Request<Texture2D>(texture, 2);
			BackgroundTextureLoader.backgrounds[texture] = BackgroundTextureLoader.Instance.Reserve();
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x004CD270 File Offset: 0x004CB470
		internal override void ResizeArrays()
		{
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.Background, base.TotalCount);
			Array.Resize<int>(ref Main.backgroundHeight, base.TotalCount);
			Array.Resize<int>(ref Main.backgroundWidth, base.TotalCount);
			foreach (string texture in BackgroundTextureLoader.backgrounds.Keys)
			{
				int slot = BackgroundTextureLoader.backgrounds[texture];
				Asset<Texture2D> tex = ModContent.Request<Texture2D>(texture, 2);
				TextureAssets.Background[slot] = tex;
				Main.backgroundWidth[slot] = tex.Width();
				Main.backgroundHeight[slot] = tex.Height();
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x004CD320 File Offset: 0x004CB520
		internal override void Unload()
		{
			base.Unload();
			BackgroundTextureLoader.backgrounds.Clear();
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x004CD334 File Offset: 0x004CB534
		internal static void AutoloadBackgrounds(Mod mod)
		{
			foreach (string path in from t in mod.RootContentSource.EnumerateAssets()
			where t.Contains("Backgrounds/")
			select t)
			{
				string texturePath = Path.ChangeExtension(path, null);
				string textureKey = mod.Name + "/" + texturePath;
				BackgroundTextureLoader.AddBackgroundTexture(mod, textureKey);
			}
		}

		// Token: 0x04001479 RID: 5241
		internal static IDictionary<string, int> backgrounds = new Dictionary<string, int>();
	}
}
