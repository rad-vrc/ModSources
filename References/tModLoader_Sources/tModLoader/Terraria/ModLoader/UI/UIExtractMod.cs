using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Terraria.Localization;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000245 RID: 581
	internal class UIExtractMod : UIProgress
	{
		// Token: 0x06002988 RID: 10632 RVA: 0x00512CD4 File Offset: 0x00510ED4
		public override void OnActivate()
		{
			base.OnActivate();
			this._cts = new CancellationTokenSource();
			base.OnCancel += delegate()
			{
				this._cts.Cancel();
			};
			Task.Run(new Func<Task>(this.Extract), this._cts.Token);
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x00512D21 File Offset: 0x00510F21
		internal void Show(LocalMod mod, int gotoMenu)
		{
			this.mod = mod;
			this.gotoMenu = gotoMenu;
			Main.menuMode = 10019;
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x00512D3C File Offset: 0x00510F3C
		private Task Extract()
		{
			StreamWriter log = null;
			IDisposable modHandle = null;
			try
			{
				string modReferencesPath = Path.Combine(ModCompile.ModSourcePath, "ModAssemblies");
				string oldModReferencesPath = Path.Combine(ModCompile.ModSourcePath, "Mod Libraries");
				if (Directory.Exists(oldModReferencesPath) && !Directory.Exists(modReferencesPath))
				{
					ILog tML = Logging.tML;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Migrating from \"");
					defaultInterpolatedStringHandler.AppendFormatted(oldModReferencesPath);
					defaultInterpolatedStringHandler.AppendLiteral("\" to \"");
					defaultInterpolatedStringHandler.AppendFormatted(modReferencesPath);
					defaultInterpolatedStringHandler.AppendLiteral("\"");
					tML.Info(defaultInterpolatedStringHandler.ToStringAndClear());
					Directory.Move(oldModReferencesPath, modReferencesPath);
					Logging.tML.Info("Moving old ModAssemblies folder to new location migration success");
				}
				string dir = Path.Combine(Main.SavePath, "ModReader", this.mod.Name);
				if (Directory.Exists(dir))
				{
					Directory.Delete(dir, true);
				}
				Directory.CreateDirectory(dir);
				log = new StreamWriter(Path.Combine(dir, "extract.log"))
				{
					AutoFlush = true
				};
				if (this.mod.properties.hideCode)
				{
					log.WriteLine(Language.GetTextValue("tModLoader.ExtractHideCodeMessage"));
				}
				else if (!this.mod.properties.includeSource)
				{
					log.WriteLine(Language.GetTextValue("tModLoader.ExtractNoSourceCodeMessage"));
				}
				if (this.mod.properties.hideResources)
				{
					log.WriteLine(Language.GetTextValue("tModLoader.ExtractHideResourcesMessage"));
				}
				log.WriteLine(Language.GetTextValue("tModLoader.ExtractFileListing"));
				int i = 0;
				modHandle = this.mod.modFile.Open();
				foreach (TmodFile.FileEntry entry in this.mod.modFile)
				{
					this._cts.Token.ThrowIfCancellationRequested();
					string name = entry.Name;
					Action<Stream, Stream> converter;
					ContentConverters.Reverse(ref name, out converter);
					this.DisplayText = name;
					base.Progress = (float)i++ / (float)this.mod.modFile.Count;
					if (!(name == "extract.log"))
					{
						if (UIExtractMod.codeExtensions.Contains(Path.GetExtension(name)) ? this.mod.properties.hideCode : this.mod.properties.hideResources)
						{
							log.Write("[hidden] " + name);
						}
						else
						{
							log.WriteLine(name);
							string path = Path.Combine(dir, name);
							Directory.CreateDirectory(Path.GetDirectoryName(path));
							using (FileStream dst = File.OpenWrite(path))
							{
								using (Stream src = this.mod.modFile.GetStream(entry, false))
								{
									if (converter != null)
									{
										converter(src, dst);
									}
									else
									{
										src.CopyTo(dst);
									}
								}
							}
							if (name == this.mod.Name + ".dll")
							{
								Directory.CreateDirectory(modReferencesPath);
								string sourceFileName = path;
								string path2 = modReferencesPath;
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 2);
								defaultInterpolatedStringHandler.AppendFormatted(this.mod.Name);
								defaultInterpolatedStringHandler.AppendLiteral("_v");
								defaultInterpolatedStringHandler.AppendFormatted<Version>(this.mod.modFile.Version);
								defaultInterpolatedStringHandler.AppendLiteral(".dll");
								File.Copy(sourceFileName, Path.Combine(path2, defaultInterpolatedStringHandler.ToStringAndClear()), true);
								if (log != null)
								{
									log.WriteLine("You can find this mod's .dll files under " + Path.GetFullPath(modReferencesPath) + " for easy mod collaboration!");
								}
							}
							if (name == this.mod.Name + ".xml" && !this.mod.properties.hideCode)
							{
								Directory.CreateDirectory(modReferencesPath);
								string sourceFileName2 = path;
								string path3 = modReferencesPath;
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 2);
								defaultInterpolatedStringHandler.AppendFormatted(this.mod.Name);
								defaultInterpolatedStringHandler.AppendLiteral("_v");
								defaultInterpolatedStringHandler.AppendFormatted<Version>(this.mod.modFile.Version);
								defaultInterpolatedStringHandler.AppendLiteral(".xml");
								File.Copy(sourceFileName2, Path.Combine(path3, defaultInterpolatedStringHandler.ToStringAndClear()), true);
								if (log != null)
								{
									log.WriteLine("You can find this mod's documentation .xml file under " + Path.GetFullPath(modReferencesPath) + " for easy mod collaboration!");
								}
							}
						}
					}
				}
				Utils.OpenFolder(dir);
			}
			catch (OperationCanceledException)
			{
				if (log != null)
				{
					log.WriteLine("Extraction was cancelled.");
				}
				return Task.FromResult<bool>(false);
			}
			catch (Exception e)
			{
				if (log != null)
				{
					log.WriteLine(e);
				}
				Logging.tML.Error(Language.GetTextValue("tModLoader.ExtractErrorWhileExtractingMod", this.mod.Name), e);
				Main.menuMode = this.gotoMenu;
				return Task.FromResult<bool>(false);
			}
			finally
			{
				if (log != null)
				{
					log.Close();
				}
				if (modHandle != null)
				{
					modHandle.Dispose();
				}
			}
			Main.menuMode = this.gotoMenu;
			return Task.FromResult<bool>(true);
		}

		// Token: 0x04001A5C RID: 6748
		private const string LOG_NAME = "extract.log";

		// Token: 0x04001A5D RID: 6749
		private LocalMod mod;

		// Token: 0x04001A5E RID: 6750
		private static readonly IList<string> codeExtensions = new List<string>(ModCompile.sourceExtensions)
		{
			".dll",
			".pdb"
		};

		// Token: 0x04001A5F RID: 6751
		private CancellationTokenSource _cts;
	}
}
