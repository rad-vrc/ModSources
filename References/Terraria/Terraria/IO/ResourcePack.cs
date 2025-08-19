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
	// Token: 0x020000D3 RID: 211
	public class ResourcePack
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x004ADC2C File Offset: 0x004ABE2C
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

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x004ADC48 File Offset: 0x004ABE48
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x004ADC50 File Offset: 0x004ABE50
		public string Name { get; private set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x004ADC59 File Offset: 0x004ABE59
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x004ADC61 File Offset: 0x004ABE61
		public string Description { get; private set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x004ADC6A File Offset: 0x004ABE6A
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x004ADC72 File Offset: 0x004ABE72
		public string Author { get; private set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x004ADC7B File Offset: 0x004ABE7B
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x004ADC83 File Offset: 0x004ABE83
		public ResourcePackVersion Version { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x004ADC8C File Offset: 0x004ABE8C
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x004ADC94 File Offset: 0x004ABE94
		public bool IsEnabled { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x004ADC9D File Offset: 0x004ABE9D
		// (set) Token: 0x060014A9 RID: 5289 RVA: 0x004ADCA5 File Offset: 0x004ABEA5
		public int SortingOrder { get; set; }

		// Token: 0x060014AA RID: 5290 RVA: 0x004ADCB0 File Offset: 0x004ABEB0
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

		// Token: 0x060014AB RID: 5291 RVA: 0x004ADD2F File Offset: 0x004ABF2F
		public void Refresh()
		{
			this._needsReload = true;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x004ADD38 File Offset: 0x004ABF38
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

		// Token: 0x060014AD RID: 5293 RVA: 0x004ADDA8 File Offset: 0x004ABFA8
		private Texture2D CreateIcon()
		{
			if (!this.HasFile("icon.png"))
			{
				return XnaExtensions.Get<IAssetRepository>(this._services).Request<Texture2D>("Images/UI/DefaultResourcePackIcon", 1).Value;
			}
			Texture2D result;
			using (Stream stream = this.OpenStream("icon.png"))
			{
				result = XnaExtensions.Get<AssetReaderCollection>(this._services).Read<Texture2D>(stream, ".png");
			}
			return result;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x004ADE20 File Offset: 0x004AC020
		private void LoadManifest()
		{
			if (!this.HasFile("pack.json"))
			{
				throw new FileNotFoundException(string.Format("Resource Pack at \"{0}\" must contain a {1} file.", this.FullPath, "pack.json"));
			}
			JObject jobject;
			using (Stream stream = this.OpenStream("pack.json"))
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					jobject = JObject.Parse(streamReader.ReadToEnd());
				}
			}
			this.Name = Extensions.Value<string>(jobject["Name"]);
			this.Description = Extensions.Value<string>(jobject["Description"]);
			this.Author = Extensions.Value<string>(jobject["Author"]);
			this.Version = jobject["Version"].ToObject<ResourcePackVersion>();
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x004ADF00 File Offset: 0x004AC100
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

		// Token: 0x060014B0 RID: 5296 RVA: 0x004ADF4F File Offset: 0x004AC14F
		private bool HasFile(string fileName)
		{
			if (!this.IsCompressed)
			{
				return File.Exists(Path.Combine(this.FullPath, fileName));
			}
			return this._zipFile.ContainsEntry(fileName);
		}

		// Token: 0x0400124D RID: 4685
		public readonly string FullPath;

		// Token: 0x0400124E RID: 4686
		public readonly string FileName;

		// Token: 0x04001255 RID: 4693
		private readonly IServiceProvider _services;

		// Token: 0x04001256 RID: 4694
		public readonly bool IsCompressed;

		// Token: 0x04001257 RID: 4695
		public readonly ResourcePack.BrandingType Branding;

		// Token: 0x04001258 RID: 4696
		private readonly ZipFile _zipFile;

		// Token: 0x04001259 RID: 4697
		private Texture2D _icon;

		// Token: 0x0400125A RID: 4698
		private IContentSource _contentSource;

		// Token: 0x0400125B RID: 4699
		private bool _needsReload = true;

		// Token: 0x0400125C RID: 4700
		private const string ICON_FILE_NAME = "icon.png";

		// Token: 0x0400125D RID: 4701
		private const string PACK_FILE_NAME = "pack.json";

		// Token: 0x02000563 RID: 1379
		public enum BrandingType
		{
			// Token: 0x040058F7 RID: 22775
			None,
			// Token: 0x040058F8 RID: 22776
			SteamWorkshop
		}
	}
}
