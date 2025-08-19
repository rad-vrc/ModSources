using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class represents a chat or console command. Use the CommandType to specify the scope of the command.
	/// </summary>
	// Token: 0x020001A8 RID: 424
	public abstract class ModCommand : ModType
	{
		/// <summary>The desired text to trigger this command.</summary>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06002056 RID: 8278
		public abstract string Command { get; }

		/// <summary>A flag enum representing context where this command operates.</summary>
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06002057 RID: 8279
		public abstract CommandType Type { get; }

		/// <summary>A short usage explanation for this command.</summary>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x004E2E03 File Offset: 0x004E1003
		public virtual string Usage
		{
			get
			{
				return "/" + this.Command;
			}
		}

		/// <summary>A short description of this command.</summary>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x004E2E15 File Offset: 0x004E1015
		public virtual string Description
		{
			get
			{
				return "";
			}
		}

		/// <summary>If false (default), the arguments to <see cref="M:Terraria.ModLoader.ModCommand.Action(Terraria.ModLoader.CommandCaller,System.String,System.String[])" /> will be provided as lowercase.</summary>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x004E2E1C File Offset: 0x004E101C
		public virtual bool IsCaseSensitive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x004E2E1F File Offset: 0x004E101F
		protected sealed override void Register()
		{
			CommandLoader.Add(this);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x004E2E27 File Offset: 0x004E1027
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>The code that is executed when the command is triggered.</summary>
		// Token: 0x0600205D RID: 8285
		public abstract void Action(CommandCaller caller, string input, string[] args);
	}
}
