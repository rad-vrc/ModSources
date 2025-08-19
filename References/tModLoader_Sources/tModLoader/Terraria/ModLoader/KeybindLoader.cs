using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Terraria.ModLoader
{
	// Token: 0x0200018B RID: 395
	public sealed class KeybindLoader : Loader
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x004DD1FA File Offset: 0x004DB3FA
		internal static IEnumerable<ModKeybind> Keybinds
		{
			get
			{
				return KeybindLoader.modKeybinds.Values;
			}
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x004DD206 File Offset: 0x004DB406
		internal override void Unload()
		{
			KeybindLoader.modKeybinds.Clear();
		}

		/// <summary>
		/// Registers a keybind with a <paramref name="name" /> and <paramref name="defaultBinding" />. Use the returned <see cref="T:Terraria.ModLoader.ModKeybind" /> to detect when buttons are pressed.
		/// </summary>
		/// <param name="mod"> The mod that this keybind will belong to. Usually, this would be your mod instance. </param>
		/// <param name="name"> The internal name of the keybind. The localization key "Mods.{ModName}.Keybinds.{KeybindName}.DisplayName" will be used for the display name. <br />It is recommended that this not have any spaces. </param>
		/// <param name="defaultBinding"> The default binding. </param>
		// Token: 0x06001EDB RID: 7899 RVA: 0x004DD212 File Offset: 0x004DB412
		public static ModKeybind RegisterKeybind(Mod mod, string name, Keys defaultBinding)
		{
			return KeybindLoader.RegisterKeybind(mod, name, defaultBinding.ToString());
		}

		/// <summary>
		/// Registers a keybind with a <paramref name="name" /> and <paramref name="defaultBinding" />. Use the returned <see cref="T:Terraria.ModLoader.ModKeybind" /> to detect when buttons are pressed.
		/// </summary>
		/// <param name="mod"> The mod that this keybind will belong to. Usually, this would be your mod instance. </param>
		/// <param name="name"> The internal name of the keybind. The localization key "Mods.{ModName}.Keybinds.{KeybindName}.DisplayName" will be used for the display name. <br />It is recommended that this not have any spaces. </param>
		/// <param name="defaultBinding"> The default binding. </param>
		// Token: 0x06001EDC RID: 7900 RVA: 0x004DD228 File Offset: 0x004DB428
		public static ModKeybind RegisterKeybind(Mod mod, string name, string defaultBinding)
		{
			if (mod == null)
			{
				throw new ArgumentNullException("mod");
			}
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException("name");
			}
			if (string.IsNullOrWhiteSpace(defaultBinding))
			{
				throw new ArgumentNullException("defaultBinding");
			}
			return KeybindLoader.RegisterKeybind(new ModKeybind(mod, name, defaultBinding));
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x004DD276 File Offset: 0x004DB476
		private static ModKeybind RegisterKeybind(ModKeybind keybind)
		{
			KeybindLoader.modKeybinds[keybind.FullName] = keybind;
			return keybind;
		}

		// Token: 0x04001649 RID: 5705
		internal static readonly IDictionary<string, ModKeybind> modKeybinds = new Dictionary<string, ModKeybind>();
	}
}
