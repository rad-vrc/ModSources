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
	// Token: 0x020000D4 RID: 212
	public class ResourcePackList
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x004ADF78 File Offset: 0x004AC178
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

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x004AE040 File Offset: 0x004AC240
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

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x004AE0E4 File Offset: 0x004AC2E4
		public IEnumerable<ResourcePack> AllPacks
		{
			get
			{
				return from pack in this._resourcePacks
				orderby pack.Name, pack.Version, pack.FileName
				select pack;
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x004AE163 File Offset: 0x004AC363
		public ResourcePackList()
		{
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x004AE176 File Offset: 0x004AC376
		public ResourcePackList(IEnumerable<ResourcePack> resourcePacks)
		{
			this._resourcePacks.AddRange(resourcePacks);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x004AE198 File Offset: 0x004AC398
		public JArray ToJson()
		{
			List<ResourcePackList.ResourcePackEntry> list = new List<ResourcePackList.ResourcePackEntry>(this._resourcePacks.Count);
			list.AddRange(from pack in this._resourcePacks
			select new ResourcePackList.ResourcePackEntry(pack.FileName, pack.IsEnabled, pack.SortingOrder));
			return JArray.FromObject(list);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x004AE1EC File Offset: 0x004AC3EC
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

		// Token: 0x060014B8 RID: 5304 RVA: 0x004AE234 File Offset: 0x004AC434
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

		// Token: 0x060014B9 RID: 5305 RVA: 0x004AE26C File Offset: 0x004AC46C
		private static void CreatePacksFromSavedJson(JArray serializedState, IServiceProvider services, string searchPath, List<ResourcePack> resourcePacks)
		{
			foreach (ResourcePackList.ResourcePackEntry resourcePackEntry in ResourcePackList.CreatePackEntryListFromJson(serializedState))
			{
				if (resourcePackEntry.FileName != null)
				{
					string text = Path.Combine(searchPath, resourcePackEntry.FileName);
					try
					{
						bool flag = File.Exists(text) || Directory.Exists(text);
						ResourcePack.BrandingType branding = ResourcePack.BrandingType.None;
						string text2;
						if (!flag && SocialAPI.Workshop != null && SocialAPI.Workshop.TryGetPath(resourcePackEntry.FileName, out text2))
						{
							text = text2;
							flag = true;
							branding = SocialAPI.Workshop.Branding.ResourcePackBrand;
						}
						if (flag)
						{
							ResourcePack item = new ResourcePack(services, text, branding)
							{
								IsEnabled = resourcePackEntry.Enabled,
								SortingOrder = resourcePackEntry.SortingOrder
							};
							resourcePacks.Add(item);
						}
					}
					catch (Exception arg)
					{
						Console.WriteLine("Failed to read resource pack {0}: {1}", text, arg);
					}
				}
			}
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x004AE368 File Offset: 0x004AC568
		private static void CreatePacksFromDirectories(IServiceProvider services, string searchPath, List<ResourcePack> resourcePacks)
		{
			foreach (string text in Directory.GetDirectories(searchPath))
			{
				try
				{
					string folderName = Path.GetFileName(text);
					if (resourcePacks.All((ResourcePack pack) => pack.FileName != folderName))
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

		// Token: 0x060014BB RID: 5307 RVA: 0x004AE3E8 File Offset: 0x004AC5E8
		private static void CreatePacksFromZips(IServiceProvider services, string searchPath, List<ResourcePack> resourcePacks)
		{
			foreach (string text in Directory.GetFiles(searchPath, "*.zip"))
			{
				try
				{
					string fileName = Path.GetFileName(text);
					if (resourcePacks.All((ResourcePack pack) => pack.FileName != fileName))
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

		// Token: 0x060014BC RID: 5308 RVA: 0x004AE46C File Offset: 0x004AC66C
		private static void CreatePacksFromWorkshopFolders(IServiceProvider services, List<ResourcePack> resourcePacks)
		{
			WorkshopSocialModule workshop = SocialAPI.Workshop;
			if (workshop == null)
			{
				return;
			}
			List<string> listOfSubscribedResourcePackPaths = workshop.GetListOfSubscribedResourcePackPaths();
			ResourcePack.BrandingType resourcePackBrand = workshop.Branding.ResourcePackBrand;
			foreach (string text in listOfSubscribedResourcePackPaths)
			{
				try
				{
					string folderName = Path.GetFileName(text);
					if (resourcePacks.All((ResourcePack pack) => pack.FileName != folderName))
					{
						resourcePacks.Add(new ResourcePack(services, text, resourcePackBrand));
					}
				}
				catch (Exception arg)
				{
					Console.WriteLine("Failed to read resource pack {0}: {1}", text, arg);
				}
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x004AE52C File Offset: 0x004AC72C
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

		// Token: 0x0400125E RID: 4702
		private readonly List<ResourcePack> _resourcePacks = new List<ResourcePack>();

		// Token: 0x02000564 RID: 1380
		private struct ResourcePackEntry
		{
			// Token: 0x06003134 RID: 12596 RVA: 0x005E4E66 File Offset: 0x005E3066
			public ResourcePackEntry(string name, bool enabled, int sortingOrder)
			{
				this.FileName = name;
				this.Enabled = enabled;
				this.SortingOrder = sortingOrder;
			}

			// Token: 0x040058F9 RID: 22777
			public string FileName;

			// Token: 0x040058FA RID: 22778
			public bool Enabled;

			// Token: 0x040058FB RID: 22779
			public int SortingOrder;
		}
	}
}
