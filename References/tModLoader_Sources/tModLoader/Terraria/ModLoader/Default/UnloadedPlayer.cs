using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002D5 RID: 725
	public class UnloadedPlayer : ModPlayer
	{
		// Token: 0x06002DFA RID: 11770 RVA: 0x0053098E File Offset: 0x0052EB8E
		public override void Initialize()
		{
			this.data = new List<TagCompound>();
			this.unloadedResearch = new List<TagCompound>();
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x005309A6 File Offset: 0x0052EBA6
		public override void SaveData(TagCompound tag)
		{
			tag["list"] = this.data;
			tag["unloadedResearch"] = this.unloadedResearch;
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x005309CA File Offset: 0x0052EBCA
		public override void LoadData(TagCompound tag)
		{
			PlayerIO.LoadModData(base.Player, tag.GetList<TagCompound>("list"));
			PlayerIO.LoadResearch(base.Player, tag.GetList<TagCompound>("unloadedResearch"));
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x005309F8 File Offset: 0x0052EBF8
		public override void OnEnterWorld()
		{
			if (Main.netMode != 1 && Main.ActiveWorldFileData.ModSaveErrors.Any<KeyValuePair<string, string>>())
			{
				Main.NewText(Utils.CreateSaveErrorMessage("tModLoader.WorldCustomDataSaveFail", Main.ActiveWorldFileData.ModSaveErrors, false).ToString(), new Color?(Color.OrangeRed));
			}
			if (base.Player.ModSaveErrors.Any<KeyValuePair<string, string>>())
			{
				NetworkText message = Utils.CreateSaveErrorMessage("tModLoader.PlayerCustomDataSaveFail", base.Player.ModSaveErrors, false);
				ChatHelper.DisplayMessageOnClient(message, Color.OrangeRed, Main.myPlayer);
				Logging.tML.Warn(message);
			}
		}

		// Token: 0x04001C6D RID: 7277
		internal IList<TagCompound> data;

		// Token: 0x04001C6E RID: 7278
		internal IList<TagCompound> unloadedResearch;
	}
}
