using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader.Core;
using Terraria.Social.Base;

namespace Terraria.ModLoader.UI.ModBrowser
{
	// Token: 0x02000266 RID: 614
	public class ModDownloadItem
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x0051EBC0 File Offset: 0x0051CDC0
		// (set) Token: 0x06002ACE RID: 10958 RVA: 0x0051EBC8 File Offset: 0x0051CDC8
		public bool NeedUpdate { get; private set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06002ACF RID: 10959 RVA: 0x0051EBD1 File Offset: 0x0051CDD1
		// (set) Token: 0x06002AD0 RID: 10960 RVA: 0x0051EBD9 File Offset: 0x0051CDD9
		public bool AppNeedRestartToReinstall { get; private set; }

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x0051EBE2 File Offset: 0x0051CDE2
		public bool IsInstalled
		{
			get
			{
				return this.Installed != null;
			}
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0051EBF0 File Offset: 0x0051CDF0
		public ModDownloadItem(string displayName, string name, Version version, string author, string modReferences, ModSide modSide, string modIconUrl, string publishId, int downloads, int hot, DateTime timeStamp, Version modloaderversion, string homepage, string ownerId, string[] referencesById)
		{
			this.ModName = name;
			this.DisplayName = displayName;
			this.DisplayNameClean = Utils.CleanChatTags(displayName);
			this.PublishId = new ModPubId_t
			{
				m_ModPubId = publishId
			};
			this.OwnerId = ownerId;
			this.Author = author;
			this.ModReferencesBySlug = modReferences;
			this.ModReferenceByModId = Array.ConvertAll<string, ModPubId_t>(referencesById, (string x) => new ModPubId_t
			{
				m_ModPubId = x
			});
			this.ModSide = modSide;
			this.ModIconUrl = modIconUrl;
			this.Downloads = downloads;
			this.Hot = hot;
			this.Homepage = homepage;
			this.TimeStamp = timeStamp;
			this.Version = version;
			this.ModloaderVersion = modloaderversion;
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x0051ECB8 File Offset: 0x0051CEB8
		internal void UpdateInstallState()
		{
			this.Installed = Interface.modBrowser.SocialBackend.IsItemInstalled(this.ModName);
			this.NeedUpdate = (this.Installed != null && Interface.modBrowser.SocialBackend.DoesItemNeedUpdate(this.PublishId, this.Installed, this.Version));
			this.AppNeedRestartToReinstall = (this.Installed == null && Interface.modBrowser.SocialBackend.DoesAppNeedRestartToReinstallItem(this.PublishId));
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x0051ED38 File Offset: 0x0051CF38
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ModDownloadItem);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0051ED46 File Offset: 0x0051CF46
		private ValueTuple<string, string, Version> GetComparable()
		{
			return new ValueTuple<string, string, Version>(this.ModName, this.PublishId.m_ModPubId, this.Version);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x0051ED64 File Offset: 0x0051CF64
		public bool Equals(ModDownloadItem item)
		{
			if (item == null)
			{
				return false;
			}
			ValueTuple<string, string, Version> comparable = this.GetComparable();
			ValueTuple<string, string, Version> comparable2 = item.GetComparable();
			return comparable.Item1 == comparable2.Item1 && comparable.Item2 == comparable2.Item2 && comparable.Item3 == comparable2.Item3;
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x0051EDC0 File Offset: 0x0051CFC0
		public override int GetHashCode()
		{
			return this.GetComparable().GetHashCode();
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x0051EDE1 File Offset: 0x0051CFE1
		public static IEnumerable<ModDownloadItem> NeedsInstallOrUpdate(IEnumerable<ModDownloadItem> downloads)
		{
			return downloads.Where(delegate(ModDownloadItem item)
			{
				if (item == null)
				{
					return false;
				}
				item.UpdateInstallState();
				return !item.IsInstalled || item.NeedUpdate;
			});
		}

		// Token: 0x04001B61 RID: 7009
		public readonly string ModName;

		// Token: 0x04001B62 RID: 7010
		public readonly string DisplayName;

		// Token: 0x04001B63 RID: 7011
		public readonly string DisplayNameClean;

		// Token: 0x04001B64 RID: 7012
		public readonly ModPubId_t PublishId;

		// Token: 0x04001B65 RID: 7013
		public readonly string OwnerId;

		// Token: 0x04001B66 RID: 7014
		public readonly Version Version;

		// Token: 0x04001B67 RID: 7015
		public readonly string Author;

		// Token: 0x04001B68 RID: 7016
		public readonly string ModIconUrl;

		// Token: 0x04001B69 RID: 7017
		public readonly DateTime TimeStamp;

		// Token: 0x04001B6A RID: 7018
		public readonly string ModReferencesBySlug;

		// Token: 0x04001B6B RID: 7019
		public readonly ModPubId_t[] ModReferenceByModId;

		// Token: 0x04001B6C RID: 7020
		public readonly ModSide ModSide;

		// Token: 0x04001B6D RID: 7021
		public readonly int Downloads;

		// Token: 0x04001B6E RID: 7022
		public readonly int Hot;

		// Token: 0x04001B6F RID: 7023
		public readonly string Homepage;

		// Token: 0x04001B70 RID: 7024
		public readonly Version ModloaderVersion;

		// Token: 0x04001B71 RID: 7025
		internal LocalMod Installed;
	}
}
