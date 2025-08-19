using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	// Token: 0x02000176 RID: 374
	public static class GoreLoader
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x004D4A9D File Offset: 0x004D2C9D
		// (set) Token: 0x06001DE1 RID: 7649 RVA: 0x004D4AA4 File Offset: 0x004D2CA4
		public static int GoreCount { get; private set; } = (int)GoreID.Count;

		/// <summary> Registers a new gore with the provided texture. Typically used with <see cref="T:Terraria.ModLoader.SimpleModGore" /> as TGore for gore with no additional logic.
		/// <para /> Use this for any gore textures not autoloaded by the <see cref="P:Terraria.ModLoader.Mod.GoreAutoloadingEnabled" /> logic.
		/// </summary>
		// Token: 0x06001DE2 RID: 7650 RVA: 0x004D4AAC File Offset: 0x004D2CAC
		public static bool AddGoreFromTexture<TGore>(Mod mod, string texture) where TGore : ModGore, new()
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
			TGore tgore = Activator.CreateInstance<TGore>();
			tgore.nameOverride = Path.GetFileNameWithoutExtension(texture);
			tgore.textureOverride = texture;
			return mod.AddContent(tgore);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x004D4B1C File Offset: 0x004D2D1C
		internal static void RegisterModGore(ModGore modGore)
		{
			int id = GoreLoader.GoreCount++;
			modGore.Type = id;
			GoreLoader.gores[id] = modGore;
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x004D4B4C File Offset: 0x004D2D4C
		internal static void AutoloadGores(Mod mod)
		{
			foreach (string path in from t in mod.RootContentSource.EnumerateAssets()
			where t.Contains("Gores/")
			select t)
			{
				string texturePath = Path.ChangeExtension(path, null);
				ModGore modGore;
				if (!mod.TryFind<ModGore>(Path.GetFileName(texturePath), out modGore))
				{
					string textureKey = mod.Name + "/" + texturePath;
					GoreLoader.AddGoreFromTexture<SimpleModGore>(mod, textureKey);
				}
			}
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x004D4BEC File Offset: 0x004D2DEC
		internal static void ResizeAndFillArrays()
		{
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.Gore, GoreLoader.GoreCount);
			LoaderUtils.ResetStaticMembers(typeof(GoreID), true);
			Array.Resize<bool>(ref ChildSafety.SafeGore, GoreLoader.GoreCount);
			for (int i = (int)GoreID.Count; i < GoreLoader.GoreCount; i++)
			{
				GoreID.Sets.DisappearSpeed[i] = 1;
				GoreID.Sets.DisappearSpeedAlpha[i] = 1;
			}
			foreach (KeyValuePair<int, ModGore> pair in GoreLoader.gores)
			{
				TextureAssets.Gore[pair.Key] = ModContent.Request<Texture2D>(pair.Value.Texture, 2);
			}
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x004D4CA4 File Offset: 0x004D2EA4
		internal static void Unload()
		{
			GoreLoader.gores.Clear();
			GoreLoader.GoreCount = (int)GoreID.Count;
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x004D4CBC File Offset: 0x004D2EBC
		internal static ModGore GetModGore(int type)
		{
			ModGore modGore;
			GoreLoader.gores.TryGetValue(type, out modGore);
			return modGore;
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x004D4CD8 File Offset: 0x004D2ED8
		internal static void SetupUpdateType(Gore gore)
		{
			if (gore.ModGore != null && gore.ModGore.UpdateType > 0)
			{
				gore.realType = gore.type;
				gore.type = gore.ModGore.UpdateType;
			}
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x004D4D0D File Offset: 0x004D2F0D
		internal static void TakeDownUpdateType(Gore gore)
		{
			if (gore.realType > 0)
			{
				gore.type = gore.realType;
				gore.realType = 0;
			}
		}

		// Token: 0x040015BB RID: 5563
		internal static readonly IDictionary<int, ModGore> gores = new Dictionary<int, ModGore>();
	}
}
