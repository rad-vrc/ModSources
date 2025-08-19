using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Terraria.GameInput;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Represents a loaded input binding. It is suggested to access the keybind status only in ModPlayer.ProcessTriggers.
	/// </summary>
	// Token: 0x020001B3 RID: 435
	public class ModKeybind
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x0600216F RID: 8559 RVA: 0x004E4710 File Offset: 0x004E2910
		// (set) Token: 0x06002170 RID: 8560 RVA: 0x004E4718 File Offset: 0x004E2918
		internal Mod Mod { get; set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x004E4721 File Offset: 0x004E2921
		// (set) Token: 0x06002172 RID: 8562 RVA: 0x004E4729 File Offset: 0x004E2929
		internal string Name { get; set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06002173 RID: 8563 RVA: 0x004E4732 File Offset: 0x004E2932
		internal string FullName
		{
			get
			{
				return this.Mod.Name + "/" + this.Name;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06002174 RID: 8564 RVA: 0x004E474F File Offset: 0x004E294F
		// (set) Token: 0x06002175 RID: 8565 RVA: 0x004E4757 File Offset: 0x004E2957
		internal string DefaultBinding { get; set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x004E4760 File Offset: 0x004E2960
		public LocalizedText DisplayName { get; }

		// Token: 0x06002177 RID: 8567 RVA: 0x004E4768 File Offset: 0x004E2968
		internal ModKeybind(Mod mod, string name, string defaultBinding)
		{
			this.Mod = mod;
			this.Name = name;
			this.DefaultBinding = defaultBinding;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
			defaultInterpolatedStringHandler.AppendLiteral("Mods.");
			defaultInterpolatedStringHandler.AppendFormatted(this.Mod.Name);
			defaultInterpolatedStringHandler.AppendLiteral(".Keybinds.");
			defaultInterpolatedStringHandler.AppendFormatted(this.Name);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted("DisplayName");
			this.DisplayName = Language.GetOrRegister(defaultInterpolatedStringHandler.ToStringAndClear(), () => Regex.Replace(this.Name, "([A-Z])", " $1").Trim());
		}

		/// <summary>
		/// Gets the currently assigned keybindings. Useful for prompts, tooltips, informing users.
		/// </summary>
		/// <param name="mode"> The InputMode. Choose between InputMode.Keyboard and InputMode.XBoxGamepad </param>
		// Token: 0x06002178 RID: 8568 RVA: 0x004E4807 File Offset: 0x004E2A07
		public List<string> GetAssignedKeys(InputMode mode = InputMode.Keyboard)
		{
			return PlayerInput.CurrentProfile.InputModes[mode].KeyStatus[this.FullName];
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x004E4829 File Offset: 0x004E2A29
		public bool RetroCurrent
		{
			get
			{
				return !Main.drawingPlayerChat && Main.player[Main.myPlayer].talkNPC == -1 && Main.player[Main.myPlayer].sign == -1 && this.Current;
			}
		}

		/// <summary>
		/// Returns true if this keybind is pressed currently. Useful for creating a behavior that relies on the keybind being held down.
		/// </summary>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x004E4860 File Offset: 0x004E2A60
		public bool Current
		{
			get
			{
				return PlayerInput.Triggers.Current.KeyStatus[this.FullName];
			}
		}

		/// <summary>
		/// Returns true if this keybind has just been pressed this update. This is a fire-once-per-press behavior.
		/// </summary>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x004E487C File Offset: 0x004E2A7C
		public bool JustPressed
		{
			get
			{
				return PlayerInput.Triggers.JustPressed.KeyStatus[this.FullName];
			}
		}

		/// <summary>
		/// Returns true if this keybind has just been released this update.
		/// </summary>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x004E4898 File Offset: 0x004E2A98
		public bool JustReleased
		{
			get
			{
				return PlayerInput.Triggers.JustReleased.KeyStatus[this.FullName];
			}
		}

		/// <summary>
		/// Returns true if this keybind has been pressed during the previous update.
		/// </summary>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x004E48B4 File Offset: 0x004E2AB4
		public bool Old
		{
			get
			{
				return PlayerInput.Triggers.Old.KeyStatus[this.FullName];
			}
		}
	}
}
