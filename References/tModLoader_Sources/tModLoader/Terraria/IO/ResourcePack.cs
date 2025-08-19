using System;
using System.IO;
using Ionic.Zip;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using ReLogic.Content;
using ReLogic.Content.Sources;
using ReLogic.Utilities;
using Terraria.GameContent;

namespace Terraria.IO
{
	// Token: 0x020003E2 RID: 994
	public class ResourcePack
	{
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x00556F07 File Offset: 0x00555107
		public Texture2D Icon
		{
			get
			{
				if (this._icon == null)
				{
					this._icon = this.CreateIcon();
				}
				return this._icon;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x00556F23 File Offset: 0x00555123
		// (set) Token: 0x06003424 RID: 13348 RVA: 0x00556F2B File Offset: 0x0055512B
		public string Name { get; private set; }

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x00556F34 File Offset: 0x00555134
		// (set) Token: 0x06003426 RID: 13350 RVA: 0x00556F3C File Offset: 0x0055513C
		public string Description { get; private set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x00556F45 File Offset: 0x00555145
		// (set) Token: 0x06003428 RID: 13352 RVA: 0x00556F4D File Offset: 0x0055514D
		public string Author { get; private set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x00556F56 File Offset: 0x00555156
		// (set) Token: 0x0600342A RID: 13354 RVA: 0x00556F5E File Offset: 0x0055515E
		public ResourcePackVersion Version { get; private set; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x00556F67 File Offset: 0x00555167
		// (set) Token: 0x0600342C RID: 13356 RVA: 0x00556F6F File Offset: 0x0055516F
		public bool IsEnabled { get; set; }

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x00556F78 File Offset: 0x00555178
		// (set) Token: 0x0600342E RID: 13358 RVA: 0x00556F80 File Offset: 0x00555180
		public int SortingOrder { get; set; }

		// Token: 0x0600342F RID: 13359 RVA: 0x00556F8C File Offset: 0x0055518C
		public ResourcePack(IServiceProvider services, string path, ResourcePack.BrandingType branding = ResourcePack.BrandingType.None)
		{
			if (File.Exists(path))
			{
				this.IsCompressed = true;
			}
			else if (!Directory.Exists(path))
			{
				throw new FileNotFoundException("Unable to find file or folder for resource pack at: " + path);
			}
			this.FileName = Path.GetFileName(path);
			this._services = services;
			this.FullPath = path;
			this.Branding = branding;
			if (this.IsCompressed)
			{
				this._zipFile = ZipFile.Read(path);
			}
			this.LoadManifest();
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x0055700B File Offset: 0x0055520B
		public void Refresh()
		{
			this._needsReload = true;
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x00557014 File Offset: 0x00555214
		public IContentSource GetContentSource()
		{
			if (this._needsReload)
			{
				if (this.IsCompressed)
				{
					this._contentSource = new ZipContentSource(this.FullPath, "Content");
				}
				else
				{
					this._contentSource = new FileSystemContentSource(Path.Combine(this.FullPath, "Content"));
				}
				this._contentSource.ContentValidator = VanillaContentValidator.Instance;
				this._needsReload = false;
			}
			return this._contentSource;
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x00557084 File Offset: 0x00555284
		private Texture2D CreateIcon()
		{
			if (!this.HasFile("icon.png"))
			{
				return XnaExtensions.Get<IAssetRepository>(this._services).Request<Texture2D>("Images/UI/DefaultResourcePackIcon").Value;
			}
			Texture2D value;
			using (Stream stream = this.OpenStream("icon.png"))
			{
				value = XnaExtensions.Get<IAssetRepository>(this._services).CreateUntracked<Texture2D>(stream, ".png", 1).Value;
			}
			return value;
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x00557100 File Offset: 0x00555300
		private void LoadManifest()
		{
			if (!this.HasFile("pack.json"))
			{
				throw new FileNotFoundException(string.Format("Resource Pack at \"{0}\" must contain a {1} file.", this.FullPath, "pack.json"));
			}
			JObject jObject;
			using (Stream stream = this.OpenStream("pack.json"))
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					jObject = JObject.Parse(streamReader.ReadToEnd());
				}
			}
			this.Name = Extensions.Value<string>(jObject["Name"]);
			this.Description = Extensions.Value<string>(jObject["Description"]);
			this.Author = Extensions.Value<string>(jObject["Author"]);
			this.Version = jObject["Version"].ToObject<ResourcePackVersion>();
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x005571E0 File Offset: 0x005553E0
		private Stream OpenStream(string fileName)
		{
			if (!this.IsCompressed)
			{
				return File.OpenRead(Path.Combine(this.FullPath, fileName));
			}
			ZipEntry zipEntry = this._zipFile[fileName];
			MemoryStream memoryStream = new MemoryStream((int)zipEntry.UncompressedSize);
			zipEntry.Extract(memoryStream);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x0055722F File Offset: 0x0055542F
		private bool HasFile(string fileName)
		{
			if (!this.IsCompressed)
			{
				return File.Exists(Path.Combine(this.FullPath, fileName));
			}
			return this._zipFile.ContainsEntry(fileName);
		}

		// Token: 0x04001E82 RID: 7810
		public readonly string FullPath;

		// Token: 0x04001E83 RID: 7811
		public readonly string FileName;

		// Token: 0x04001E84 RID: 7812
		private readonly IServiceProvider _services;

		// Token: 0x04001E85 RID: 7813
		public readonly bool IsCompressed;

		// Token: 0x04001E86 RID: 7814
		public readonly ResourcePack.BrandingType Branding;

		// Token: 0x04001E87 RID: 7815
		private readonly ZipFile _zipFile;

		// Token: 0x04001E88 RID: 7816
		private Texture2D _icon;

		// Token: 0x04001E89 RID: 7817
		private IContentSource _contentSource;

		// Token: 0x04001E8A RID: 7818
		private bool _needsReload = true;

		// Token: 0x04001E8B RID: 7819
		private const string ICON_FILE_NAME = "icon.png";

		// Token: 0x04001E8C RID: 7820
		private const string PACK_FILE_NAME = "pack.json";

		// Token: 0x02000B25 RID: 2853
		public enum BrandingType
		{
			// Token: 0x04006F1E RID: 28446
			None,
			// Token: 0x04006F1F RID: 28447
			SteamWorkshop
		}
	}
}
