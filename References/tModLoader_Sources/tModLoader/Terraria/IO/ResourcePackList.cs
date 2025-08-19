using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Social;
using Terraria.Social.Base;

namespace Terraria.IO
{
	// Token: 0x020003E4 RID: 996
	public class ResourcePackList
	{
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x005572BC File Offset: 0x005554BC
		public IEnumerable<ResourcePack> EnabledPacks
		{
			get
			{
				return from pack in this._resourcePacks
				where pack.IsEnabled
				orderby pack.SortingOrder, pack.Name, pack.Version, pack.FileName
				select pack;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x00557384 File Offset: 0x00555584
		public IEnumerable<ResourcePack> DisabledPacks
		{
			get
			{
				return from pack in this._resourcePacks
				where !pack.IsEnabled
				orderby pack.Name, pack.Version, pack.FileName
				select pack;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x00557428 File Offset: 0x00555628
		public IEnumerable<ResourcePack> AllPacks
		{
			get
			{
				return from pack in this._resourcePacks
				orderby pack.Name, pack.Version, pack.FileName
				select pack;
			}
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x005574A7 File Offset: 0x005556A7
		public ResourcePackList()
		{
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x005574BA File Offset: 0x005556BA
		public ResourcePackList(IEnumerable<ResourcePack> resourcePacks)
		{
			this._resourcePacks.AddRange(resourcePacks);
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x005574DC File Offset: 0x005556DC
		public JArray ToJson()
		{
			List<ResourcePackList.ResourcePackEntry> list = new List<ResourcePackList.ResourcePackEntry>(this._resourcePacks.Count);
			list.AddRange(from pack in this._resourcePacks
			select new ResourcePackList.ResourcePackEntry(pack.FileName, pack.IsEnabled, pack.SortingOrder));
			return JArray.FromObject(list);
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x00557530 File Offset: 0x00555730
		public static ResourcePackList FromJson(JArray serializedState, IServiceProvider services, string searchPath)
		{
			if (!Directory.Exists(searchPath))
			{
				return new ResourcePackList();
			}
			List<ResourcePack> resourcePacks = new List<ResourcePack>();
			ResourcePackList.CreatePacksFromSavedJson(serializedState, services, searchPath, resourcePacks);
			ResourcePackList.CreatePacksFromZips(services, searchPath, resourcePacks);
			ResourcePackList.CreatePacksFromDirectories(services, searchPath, resourcePacks);
			ResourcePackList.CreatePacksFromWorkshopFolders(services, resourcePacks);
			return new ResourcePackList(resourcePacks);
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x00557578 File Offset: 0x00555778
		public static ResourcePackList Publishable(JArray serializedState, IServiceProvider services, string searchPath)
		{
			if (!Directory.Exists(searchPath))
			{
				return new ResourcePackList();
			}
			List<ResourcePack> resourcePacks = new List<ResourcePack>();
			ResourcePackList.CreatePacksFromZips(services, searchPath, resourcePacks);
			ResourcePackList.CreatePacksFromDirectories(services, searchPath, resourcePacks);
			return new ResourcePackList(resourcePacks);
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x005575B0 File Offset: 0x005557B0
		private static void CreatePacksFromSavedJson(JArray serializedState, IServiceProvider services, string searchPath, List<ResourcePack> resourcePacks)
		{
			foreach (ResourcePackList.ResourcePackEntry item2 in ResourcePackList.CreatePackEntryListFromJson(serializedState))
			{
				if (item2.FileName != null)
				{
					string text = Path.Combine(searchPath, item2.FileName);
					try
					{
						bool flag = File.Exists(text) || Directory.Exists(text);
						ResourcePack.BrandingType branding = ResourcePack.BrandingType.None;
						string fullPathFound;
						if (!flag && SocialAPI.Workshop != null && SocialAPI.Workshop.TryGetPath(item2.FileName, out fullPathFound))
						{
							text = fullPathFound;
							flag = true;
							branding = SocialAPI.Workshop.Branding.ResourcePackBrand;
						}
						if (flag)
						{
							ResourcePack item3 = new ResourcePack(services, text, branding)
							{
								IsEnabled = item2.Enabled,
								SortingOrder = item2.SortingOrder
							};
							resourcePacks.Add(item3);
						}
					}
					catch (Exception arg)
					{
						Console.WriteLine("Failed to read resource pack {0}: {1}", text, arg);
					}
				}
			}
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x005576AC File Offset: 0x005558AC
		private static void CreatePacksFromDirectories(IServiceProvider services, string searchPath, List<ResourcePack> resourcePacks)
		{
			string folderName;
			Func<ResourcePack, bool> <>9__0;
			foreach (string text in Directory.GetDirectories(searchPath))
			{
				try
				{
					folderName = Path.GetFileName(text);
					Func<ResourcePack, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((ResourcePack pack) => pack.FileName != folderName));
					}
					if (resourcePacks.All(predicate))
					{
						resourcePacks.Add(new ResourcePack(services, text, ResourcePack.BrandingType.None));
					}
				}
				catch (Exception arg)
				{
					Console.WriteLine("Failed to read resource pack {0}: {1}", text, arg);
				}
			}
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x00557740 File Offset: 0x00555940
		private static void CreatePacksFromZips(IServiceProvider services, string searchPath, List<ResourcePack> resourcePacks)
		{
			string fileName;
			Func<ResourcePack, bool> <>9__0;
			foreach (string text in Directory.GetFiles(searchPath, "*.zip"))
			{
				try
				{
					fileName = Path.GetFileName(text);
					Func<ResourcePack, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((ResourcePack pack) => pack.FileName != fileName));
					}
					if (resourcePacks.All(predicate))
					{
						resourcePacks.Add(new ResourcePack(services, text, ResourcePack.BrandingType.None));
					}
				}
				catch (Exception arg)
				{
					Console.WriteLine("Failed to read resource pack {0}: {1}", text, arg);
				}
			}
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x005577D8 File Offset: 0x005559D8
		private static void CreatePacksFromWorkshopFolders(IServiceProvider services, List<ResourcePack> resourcePacks)
		{
			WorkshopSocialModule workshop = SocialAPI.Workshop;
			if (workshop == null)
			{
				return;
			}
			List<string> listOfSubscribedResourcePackPaths = workshop.GetListOfSubscribedResourcePackPaths();
			ResourcePack.BrandingType resourcePackBrand = workshop.Branding.ResourcePackBrand;
			string folderName;
			Func<ResourcePack, bool> <>9__0;
			foreach (string item in listOfSubscribedResourcePackPaths)
			{
				try
				{
					folderName = Path.GetFileName(item);
					Func<ResourcePack, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((ResourcePack pack) => pack.FileName != folderName));
					}
					if (resourcePacks.All(predicate))
					{
						resourcePacks.Add(new ResourcePack(services, item, resourcePackBrand));
					}
				}
				catch (Exception arg)
				{
					Console.WriteLine("Failed to read resource pack {0}: {1}", item, arg);
				}
			}
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x005578AC File Offset: 0x00555AAC
		private static IEnumerable<ResourcePackList.ResourcePackEntry> CreatePackEntryListFromJson(JArray serializedState)
		{
			try
			{
				if (serializedState != null && serializedState.Count != 0)
				{
					return serializedState.ToObject<List<ResourcePackList.ResourcePackEntry>>();
				}
			}
			catch (JsonReaderException arg)
			{
				Console.WriteLine("Failed to parse configuration entry for resource pack list. {0}", arg);
			}
			return new List<ResourcePackList.ResourcePackEntry>();
		}

		// Token: 0x04001E93 RID: 7827
		private readonly List<ResourcePack> _resourcePacks = new List<ResourcePack>();

		// Token: 0x02000B26 RID: 2854
		private struct ResourcePackEntry
		{
			// Token: 0x06005B7C RID: 23420 RVA: 0x006A5EF1 File Offset: 0x006A40F1
			public ResourcePackEntry(string name, bool enabled, int sortingOrder)
			{
				this.FileName = name;
				this.Enabled = enabled;
				this.SortingOrder = sortingOrder;
			}

			// Token: 0x04006F20 RID: 28448
			public string FileName;

			// Token: 0x04006F21 RID: 28449
			public bool Enabled;

			// Token: 0x04006F22 RID: 28450
			public int SortingOrder;
		}
	}
}
