using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PetRenamer
{
	// Token: 0x02000003 RID: 3
	internal class PetRenamer : Mod
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002062 File Offset: 0x00000262
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002069 File Offset: 0x00000269
		public static LocalizedText UnboundText { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002071 File Offset: 0x00000271
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002078 File Offset: 0x00000278
		public static LocalizedText PetNameInSelectScreenText { get; private set; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002080 File Offset: 0x00000280
		internal static bool IsPetItem(Item item)
		{
			return item.type > 0 && item.shoot > 0 && item.buffType > 0 && item.buffType < Main.vanityPet.Length && (Main.vanityPet[item.buffType] || Main.lightPet[item.buffType]);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020D8 File Offset: 0x000002D8
		private string[] GetArrayFromJson(string name)
		{
			string[] ret = null;
			try
			{
				JObject jobject = JsonConvert.DeserializeObject<JObject>(Encoding.UTF8.GetString(base.GetFileBytes(name + ".json")));
				JToken data = (jobject != null) ? jobject[name] : null;
				if (data != null)
				{
					ret = data.ToObject<string[]>();
				}
			}
			catch (Exception e)
			{
				base.Logger.Warn(e);
			}
			return ret;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002144 File Offset: 0x00000344
		public override void Load()
		{
			PetRenamer.RenamePetUIHotkey = KeybindLoader.RegisterKeybind(this, "RenamePet", "P");
			PetRenamer.UnboundText = Language.GetOrRegister(base.GetLocalizationKey("Keybinds.Unbound"), null);
			if (!Main.dedServ)
			{
				PetRenamer.randomNames = this.GetArrayFromJson("names");
				PetRenamer.randomAdjectives = this.GetArrayFromJson("adjectives");
			}
			string category = "PlayerSelectScreenUI.";
			if (PetRenamer.PetNameInSelectScreenText == null)
			{
				PetRenamer.PetNameInSelectScreenText = Language.GetOrRegister(base.GetLocalizationKey(category + "PetNameInSelectScreen"), null);
			}
			if (Config.Instance.ShowPetNameInSelectScreen)
			{
				On_UICharacterListItem.hook_DrawSelf hook_DrawSelf;
				if ((hook_DrawSelf = PetRenamer.<>O.<0>__On_UICharacterListItem_DrawSelf) == null)
				{
					hook_DrawSelf = (PetRenamer.<>O.<0>__On_UICharacterListItem_DrawSelf = new On_UICharacterListItem.hook_DrawSelf(PetRenamer.On_UICharacterListItem_DrawSelf));
				}
				On_UICharacterListItem.DrawSelf += hook_DrawSelf;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FC File Offset: 0x000003FC
		private static void On_UICharacterListItem_DrawSelf(On_UICharacterListItem.orig_DrawSelf orig, UICharacterListItem self, SpriteBatch spriteBatch)
		{
			PlayerFileData data = (PlayerFileData)typeof(UICharacterListItem).GetField("_data", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
			Player player = data.Player;
			bool addedPetName = false;
			string origName = data.Name;
			PRItem petItem;
			if (!player.hideMisc[0] && player.miscEquips[0].TryGetGlobalItem<PRItem>(out petItem) && petItem.petName != string.Empty)
			{
				addedPetName = true;
				data.Name = PetRenamer.PetNameInSelectScreenText.Format(new object[]
				{
					data.Name,
					petItem.petName
				});
			}
			orig.Invoke(self, spriteBatch);
			if (addedPetName)
			{
				data.Name = origName;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022AA File Offset: 0x000004AA
		public override void Unload()
		{
			PetRenamer.RenamePetUIHotkey = null;
			PetRenamer.ACTPetsWithSmallVerticalHitbox = null;
			if (!Main.dedServ)
			{
				PetRenamer.randomNames = null;
				PetRenamer.randomAdjectives = null;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022CC File Offset: 0x000004CC
		public override void PostSetupContent()
		{
			List<int> tempList = new List<int>();
			for (int i = (int)ProjectileID.Count; i < ProjectileLoader.ProjectileCount; i++)
			{
				ModProjectile mProj = ProjectileLoader.GetProjectile(i);
				if (mProj != null && mProj.Mod.Name == "AssortedCrazyThings" && mProj.GetType().Name.StartsWith("CuteSlime"))
				{
					tempList.Add(mProj.Projectile.type);
				}
			}
			PetRenamer.ACTPetsWithSmallVerticalHitbox = tempList.ToArray();
			Array.Sort<int>(PetRenamer.ACTPetsWithSmallVerticalHitbox);
		}

		// Token: 0x04000003 RID: 3
		internal const int VANITY_PET = 0;

		// Token: 0x04000004 RID: 4
		internal const int LIGHT_PET = 1;

		// Token: 0x04000005 RID: 5
		internal static ModKeybind RenamePetUIHotkey;

		// Token: 0x04000007 RID: 7
		internal static int[] ACTPetsWithSmallVerticalHitbox;

		// Token: 0x04000008 RID: 8
		internal static string[] randomNames;

		// Token: 0x04000009 RID: 9
		internal static string[] randomAdjectives;

		// Token: 0x0200000E RID: 14
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000052 RID: 82
			public static On_UICharacterListItem.hook_DrawSelf <0>__On_UICharacterListItem_DrawSelf;
		}
	}
}
