using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Basic.Reference.Assemblies;
using log4net.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Terraria.Localization;
using Terraria.ModLoader.Engine;
using Terraria.ModLoader.Exceptions;
using Terraria.Social.Steam;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000362 RID: 866
	internal class ModCompile
	{
		// Token: 0x06003000 RID: 12288 RVA: 0x0053831C File Offset: 0x0053651C
		internal static string[] FindModSources()
		{
			List<string> modSources = new List<string>();
			if (Directory.Exists(ModCompile.ModSourcePath))
			{
				modSources.AddRange(Directory.GetDirectories(ModCompile.ModSourcePath, "*", SearchOption.TopDirectoryOnly).Where(delegate(string dir)
				{
					DirectoryInfo directory = new DirectoryInfo(dir);
					return directory.Name[0] != '.' && directory.Name != "ModAssemblies" && directory.Name != "Mod Libraries";
				}));
			}
			if (ModOrganizer.AllFoundMods == null)
			{
				ModOrganizer.FindAllMods();
			}
			modSources.AddRange(from m in ModOrganizer.AllFoundMods
			where m.location == ModLocation.Local
			select m.properties.modSource into s
			where !string.IsNullOrEmpty(s)
			select s);
			IEnumerable<string> source = modSources.Distinct<string>();
			Func<string, bool> predicate;
			if ((predicate = ModCompile.<>O.<0>__Exists) == null)
			{
				predicate = (ModCompile.<>O.<0>__Exists = new Func<string, bool>(Directory.Exists));
			}
			return source.Where(predicate).ToArray<string>();
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x00538424 File Offset: 0x00536624
		public static bool DeveloperMode
		{
			get
			{
				return Debugger.IsAttached || ModCompile.FindModSources().Length != 0;
			}
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x00538438 File Offset: 0x00536638
		internal static void UpdateReferencesFolder()
		{
			if (ModCompile.referencesUpdated)
			{
				return;
			}
			try
			{
				if (Directory.Exists(ModCompile.oldModReferencesPath))
				{
					Directory.Delete(ModCompile.oldModReferencesPath, true);
				}
			}
			catch (Exception e)
			{
				Logging.tML.Error("Failed to delete old /references dir", e);
			}
			ModCompile.UpdateFileContents(ModCompile.modTargetsPath, "<Project ToolsVersion=\"14.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\r\n  <Import Project=\"" + SecurityElement.Escape(ModCompile.tMLModTargetsPath) + "\" />\r\n</Project>");
			ModCompile.referencesUpdated = true;
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x005384B4 File Offset: 0x005366B4
		private static void UpdateFileContents(string path, string contents)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(path));
			byte[] bytes = Encoding.UTF8.GetBytes(contents);
			if (!File.Exists(path) || !bytes.SequenceEqual(File.ReadAllBytes(path)))
			{
				File.WriteAllBytes(path, bytes);
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x005384F8 File Offset: 0x005366F8
		public static Process StartOnHost(ProcessStartInfo info)
		{
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PRESSURE_VESSEL_RUNTIME")))
			{
				info.Arguments = "--alongside-steam --host -- " + info.FileName + " " + info.Arguments;
				info.FileName = "steam-runtime-launch-client";
			}
			return Process.Start(info);
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x00538548 File Offset: 0x00536748
		public ModCompile(ModCompile.IBuildStatus status)
		{
			this.status = status;
			ModCompile.activelyModding = true;
			Logging.ResetPastExceptions();
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x00538564 File Offset: 0x00536764
		internal void BuildAll()
		{
			ModCompile.<>c__DisplayClass20_0 CS$<>8__locals1 = new ModCompile.<>c__DisplayClass20_0();
			CS$<>8__locals1.modList = new List<LocalMod>();
			foreach (string modFolder in ModCompile.FindModSources())
			{
				CS$<>8__locals1.modList.Add(this.ReadBuildInfo(modFolder));
			}
			CS$<>8__locals1.installedMods = (from mod in ModOrganizer.FindMods(false)
			where !CS$<>8__locals1.modList.Exists((LocalMod m) => m.Name == mod.Name)
			select mod).ToList<LocalMod>();
			CS$<>8__locals1.requiredFromInstall = new HashSet<LocalMod>();
			foreach (LocalMod mod3 in CS$<>8__locals1.modList)
			{
				CS$<>8__locals1.<BuildAll>g__Require|1(mod3, true);
			}
			CS$<>8__locals1.modList.AddRange(CS$<>8__locals1.requiredFromInstall);
			List<ModCompile.BuildingMod> modsToBuild;
			try
			{
				ModOrganizer.EnsureDependenciesExist(CS$<>8__locals1.modList, true);
				ModOrganizer.EnsureTargetVersionsMet(CS$<>8__locals1.modList);
				ModOrganizer.EnsureHashesAreValid(CS$<>8__locals1.modList);
				modsToBuild = ModOrganizer.Sort(CS$<>8__locals1.modList).OfType<ModCompile.BuildingMod>().ToList<ModCompile.BuildingMod>();
			}
			catch (ModSortingException ex)
			{
				throw new BuildException(ex.Message, ErrorReporting.TMLErrorCode.TML002);
			}
			int num = 0;
			foreach (ModCompile.BuildingMod mod2 in modsToBuild)
			{
				this.status.SetProgress(num++, modsToBuild.Count);
				this.Build(mod2);
			}
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x005386E8 File Offset: 0x005368E8
		internal static void BuildModCommandLine(string modFolder)
		{
			ModCompile.UpdateReferencesFolder();
			LanguageManager.Instance.SetLanguage(GameCulture.DefaultCulture);
			Lang.InitializeLegacyLocalization();
			try
			{
				new ModCompile(new ModCompile.ConsoleBuildStatus()).Build(modFolder);
			}
			catch (BuildException e)
			{
				ErrorReporting.LogStandardDiagnosticError(e.Message, e.errorCode, true, "tModLoader", "Mod Build");
				if (e.InnerException != null)
				{
					Console.Error.WriteLine(e.InnerException);
				}
				Environment.Exit(1);
			}
			catch (Exception ex)
			{
				ErrorReporting.LogStandardDiagnosticError(ex.Message, ErrorReporting.TMLErrorCode.TML001, true, "tModLoader", "Mod Build");
				Environment.Exit(1);
			}
			WorkshopSocialModule.SteamCMDPublishPreparer(modFolder);
			Environment.Exit(0);
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x005387A4 File Offset: 0x005369A4
		internal void Build(string modFolder)
		{
			this.Build(this.ReadBuildInfo(modFolder));
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x005387B4 File Offset: 0x005369B4
		private ModCompile.BuildingMod ReadBuildInfo(string modFolder)
		{
			if (modFolder.EndsWith("\\") || modFolder.EndsWith("/"))
			{
				modFolder = modFolder.Substring(0, modFolder.Length - 1);
			}
			string modName = Path.GetFileName(modFolder);
			this.status.SetStatus(Language.GetTextValue("tModLoader.ReadingProperties", modName));
			BuildProperties properties;
			try
			{
				properties = BuildProperties.ReadBuildFile(modFolder);
			}
			catch (Exception e)
			{
				throw new BuildException(Language.GetTextValue("tModLoader.BuildErrorFailedLoadBuildTxt", Path.Combine(modFolder, "build.txt")), e, ErrorReporting.TMLErrorCode.TML002);
			}
			return new ModCompile.BuildingMod(new TmodFile(Path.Combine(ModLoader.ModPath, modName + ".tmod"), modName, properties.version), properties, modFolder);
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x00538868 File Offset: 0x00536A68
		private void Build(ModCompile.BuildingMod mod)
		{
			try
			{
				this.status.SetStatus(Language.GetTextValue("tModLoader.Building", mod.Name));
				byte[] code;
				byte[] pdb;
				this.BuildMod(mod, out code, out pdb);
				mod.modFile.AddFile(mod.Name + ".dll", code);
				if (pdb != null)
				{
					mod.modFile.AddFile(mod.Name + ".pdb", pdb);
				}
				this.PackageMod(mod);
				Mod loadedMod;
				if (ModLoader.TryGetMod(mod.Name, out loadedMod))
				{
					loadedMod.Close();
				}
				try
				{
					mod.modFile.Save();
				}
				catch (IOException e)
				{
					throw new BuildException("Please close tModLoader or disable the mod in-game to build mods directly.", e, ErrorReporting.TMLErrorCode.TML003);
				}
				ModLoader.EnableMod(mod.Name);
				LocalizationLoader.HandleModBuilt(mod.Name);
			}
			catch (Exception ex)
			{
				ex.Data["mod"] = mod.Name;
				throw;
			}
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x0053895C File Offset: 0x00536B5C
		private void PackageMod(ModCompile.BuildingMod mod)
		{
			this.status.SetStatus(Language.GetTextValue("tModLoader.Packaging", mod));
			this.status.SetProgress(0, 1);
			mod.modFile.AddFile("Info", mod.properties.ToBytes());
			List<string> resources = (from res in Directory.GetFiles(mod.path, "*", SearchOption.AllDirectories)
			where !this.IgnoreResource(mod, res)
			select res).ToList<string>();
			this.status.SetProgress(this.packedResourceCount = 0, resources.Count);
			Parallel.ForEach<string>(resources, delegate(string resource)
			{
				this.AddResource(mod, resource);
			});
			string libFolder = Path.Combine(mod.path, "lib");
			IEnumerable<string> dllReferences = mod.properties.dllReferences;
			Func<string, string> <>9__2;
			Func<string, string> selector;
			if ((selector = <>9__2) == null)
			{
				selector = (<>9__2 = ((string dllName) => this.DllRefPath(mod, dllName)));
			}
			foreach (string dllPath in dllReferences.Select(selector))
			{
				if (!dllPath.StartsWith(libFolder))
				{
					mod.modFile.AddFile("lib/" + Path.GetFileName(dllPath), File.ReadAllBytes(dllPath));
				}
			}
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x00538ADC File Offset: 0x00536CDC
		private bool IgnoreResource(ModCompile.BuildingMod mod, string resource)
		{
			string relPath = resource.Substring(mod.path.Length + 1);
			return this.IgnoreCompletely(mod, resource) || relPath == "build.txt" || (!mod.properties.includeSource && ModCompile.sourceExtensions.Contains(Path.GetExtension(resource))) || Path.GetFileName(resource) == "Thumbs.db";
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x00538B48 File Offset: 0x00536D48
		private bool IgnoreCompletely(ModCompile.BuildingMod mod, string resource)
		{
			string relPath = resource.Substring(mod.path.Length + 1);
			return mod.properties.ignoreFile(relPath) || relPath[0] == '.' || relPath.StartsWith("bin" + Path.DirectorySeparatorChar.ToString()) || relPath.StartsWith("obj" + Path.DirectorySeparatorChar.ToString());
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x00538BBC File Offset: 0x00536DBC
		private void AddResource(ModCompile.BuildingMod mod, string resource)
		{
			string relPath = resource.Substring(mod.path.Length + 1);
			using (FileStream src = File.OpenRead(resource))
			{
				using (MemoryStream dst = new MemoryStream())
				{
					if (!ContentConverters.Convert(ref relPath, src, dst))
					{
						src.CopyTo(dst);
					}
					mod.modFile.AddFile(relPath, dst.ToArray());
					Interlocked.Increment(ref this.packedResourceCount);
					this.status.SetProgress(this.packedResourceCount, -1);
				}
			}
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x00538C60 File Offset: 0x00536E60
		private List<LocalMod> FindReferencedMods(BuildProperties properties)
		{
			Dictionary<string, LocalMod> existingMods = ModOrganizer.FindMods(false).ToDictionary((LocalMod mod) => mod.modFile.Name, (LocalMod mod) => mod);
			Dictionary<string, LocalMod> mods = new Dictionary<string, LocalMod>();
			this.FindReferencedMods(properties, existingMods, mods, true);
			return mods.Values.ToList<LocalMod>();
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x00538CD4 File Offset: 0x00536ED4
		private void FindReferencedMods(BuildProperties properties, Dictionary<string, LocalMod> existingMods, Dictionary<string, LocalMod> mods, bool requireWeak)
		{
			using (IEnumerator<string> enumerator = properties.RefNames(true).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string refName = enumerator.Current;
					if (!mods.ContainsKey(refName))
					{
						bool isWeak = properties.weakReferences.Any((BuildProperties.ModReference r) => r.mod == refName);
						LocalMod mod;
						try
						{
							if (!existingMods.TryGetValue(refName, out mod))
							{
								throw new FileNotFoundException("Could not find \"" + refName + ".tmod\" in your subscribed Workshop mods nor the Mods folder");
							}
						}
						catch (FileNotFoundException obj) when (isWeak && !requireWeak)
						{
							continue;
						}
						catch (Exception ex)
						{
							throw new BuildException(Language.GetTextValue("tModLoader.BuildErrorModReference", refName), ex, ErrorReporting.TMLErrorCode.TML002);
						}
						mods[refName] = mod;
						this.FindReferencedMods(mod.properties, existingMods, mods, false);
					}
				}
			}
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x00538DF4 File Offset: 0x00536FF4
		private void BuildMod(ModCompile.BuildingMod mod, out byte[] code, out byte[] pdb)
		{
			string dllName = mod.Name + ".dll";
			ModCompile.<>c__DisplayClass32_0 CS$<>8__locals1;
			CS$<>8__locals1.dllPath = null;
			mod.properties.modSource = mod.path;
			string eacValue;
			if (mod.properties.noCompile)
			{
				CS$<>8__locals1.dllPath = Path.Combine(mod.path, dllName);
			}
			else if (Program.LaunchParameters.TryGetValue("-eac", out eacValue))
			{
				CS$<>8__locals1.dllPath = eacValue;
				mod.properties.eacPath = ModCompile.<BuildMod>g__pdbPath|32_0(ref CS$<>8__locals1);
				this.status.SetStatus(Language.GetTextValue("tModLoader.EnabledEAC", mod.properties.eacPath));
			}
			if (CS$<>8__locals1.dllPath == null)
			{
				this.CompileMod(mod, out code, out pdb);
				return;
			}
			if (!File.Exists(CS$<>8__locals1.dllPath))
			{
				throw new BuildException(Language.GetTextValue("tModLoader.BuildErrorLoadingPrecompiled", CS$<>8__locals1.dllPath), ErrorReporting.TMLErrorCode.TML002);
			}
			this.status.SetStatus(Language.GetTextValue("tModLoader.LoadingPrecompiled", dllName, Path.GetFileName(CS$<>8__locals1.dllPath)));
			code = File.ReadAllBytes(CS$<>8__locals1.dllPath);
			pdb = (File.Exists(ModCompile.<BuildMod>g__pdbPath|32_0(ref CS$<>8__locals1)) ? File.ReadAllBytes(ModCompile.<BuildMod>g__pdbPath|32_0(ref CS$<>8__locals1)) : null);
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x00538F20 File Offset: 0x00537120
		private void CompileMod(ModCompile.BuildingMod mod, out byte[] code, out byte[] pdb)
		{
			this.status.SetStatus(Language.GetTextValue("tModLoader.Compiling", mod.Name + ".dll"));
			string tempDir = Path.Combine(mod.path, "compile_temp");
			if (Directory.Exists(tempDir))
			{
				Directory.Delete(tempDir, true);
			}
			Directory.CreateDirectory(tempDir);
			List<string> refs = new List<string>();
			refs.AddRange(ModCompile.GetTerrariaReferences());
			refs.AddRange(from dllName in mod.properties.dllReferences
			select this.DllRefPath(mod, dllName));
			foreach (LocalMod refMod in this.FindReferencedMods(mod.properties))
			{
				using (refMod.modFile.Open())
				{
					string path2 = tempDir;
					LocalMod localMod = refMod;
					string path = Path.Combine(path2, ((localMod != null) ? localMod.ToString() : null) + ".dll");
					File.WriteAllBytes(path, refMod.modFile.GetModAssembly());
					refs.Add(path);
					foreach (string refDll in refMod.properties.dllReferences)
					{
						path = Path.Combine(tempDir, refDll + ".dll");
						File.WriteAllBytes(path, refMod.modFile.GetBytes("lib/" + refDll + ".dll"));
						refs.Add(path);
					}
				}
			}
			string[] files = (from file in Directory.GetFiles(mod.path, "*.cs", SearchOption.AllDirectories)
			where !this.IgnoreCompletely(mod, file)
			select file).ToArray<string>();
			string unsafeParam;
			bool _allowUnsafe;
			bool allowUnsafe = Program.LaunchParameters.TryGetValue("-unsafe", out unsafeParam) && bool.TryParse(unsafeParam, out _allowUnsafe) && _allowUnsafe;
			List<string> preprocessorSymbols = new List<string>
			{
				"FNA"
			};
			string defineParam;
			if (Program.LaunchParameters.TryGetValue("-define", out defineParam))
			{
				preprocessorSymbols.AddRange(defineParam.Split(new char[]
				{
					';',
					' '
				}));
			}
			if (BuildInfo.IsStable)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
				defaultInterpolatedStringHandler.AppendLiteral("TML_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(BuildInfo.tMLVersion.Major);
				defaultInterpolatedStringHandler.AppendLiteral("_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(BuildInfo.tMLVersion.Minor, "D2");
				string tmlVersionPreprocessorSymbol = defaultInterpolatedStringHandler.ToStringAndClear();
				preprocessorSymbols.Add(tmlVersionPreprocessorSymbol);
			}
			Diagnostic[] results = ModCompile.RoslynCompile(mod.Name, refs, files, preprocessorSymbols.ToArray(), allowUnsafe, out code, out pdb);
			int numWarnings = results.Count((Diagnostic e) => e.Severity == 2);
			int numErrors = results.Length - numWarnings;
			this.status.LogCompilerLine(Language.GetTextValue("tModLoader.CompilationResult", numErrors, numWarnings), Level.Info);
			foreach (Diagnostic line in results)
			{
				this.status.LogCompilerLine(line.ToString(), (line.Severity == 2) ? Level.Warn : Level.Error);
			}
			try
			{
				if (Directory.Exists(tempDir))
				{
					Directory.Delete(tempDir, true);
				}
			}
			catch (Exception)
			{
			}
			if (numErrors > 0)
			{
				Diagnostic firstError = results.First((Diagnostic e) => e.Severity == 3);
				string textValue = Language.GetTextValue("tModLoader.CompileError", mod.Name + ".dll", numErrors, numWarnings);
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
				defaultInterpolatedStringHandler.AppendLiteral("\nError: ");
				defaultInterpolatedStringHandler.AppendFormatted<Diagnostic>(firstError);
				BuildException buildException = new BuildException(textValue + defaultInterpolatedStringHandler.ToStringAndClear(), ErrorReporting.TMLErrorCode.TML002);
				if (firstError.ToString().Contains("'LocalizedText' does not contain a definition for 'SetDefault'"))
				{
					buildException.HelpLink = "https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-FAQ#localizedtext-does-not-contain-a-definition-for-setdefault";
					buildException.Data["showTModPorterHint"] = true;
				}
				throw buildException;
			}
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x00539388 File Offset: 0x00537588
		private string DllRefPath(ModCompile.BuildingMod mod, string dllName)
		{
			string path = Path.Combine(mod.path, "lib", dllName) + ".dll";
			if (File.Exists(path))
			{
				return path;
			}
			string eacPath;
			if (Program.LaunchParameters.TryGetValue("-eac", out eacPath))
			{
				string outputCopiedPath = Path.Combine(Path.GetDirectoryName(eacPath), dllName + ".dll");
				if (File.Exists(outputCopiedPath))
				{
					return outputCopiedPath;
				}
			}
			throw new BuildException("Missing dll reference: " + path, ErrorReporting.TMLErrorCode.TML002);
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x00539400 File Offset: 0x00537600
		private static IEnumerable<string> GetTerrariaReferences()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			yield return executingAssembly.Location;
			string libsDir = Path.Combine(Path.GetDirectoryName(executingAssembly.Location), "Libraries");
			foreach (string f in Directory.EnumerateFiles(libsDir, "*.dll", SearchOption.AllDirectories))
			{
				string path = f.Replace('\\', '/');
				if (!path.EndsWith(".resources.dll") && !path.Contains("/Native/") && !path.Contains("/runtime"))
				{
					yield return f;
				}
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>
		/// Compile a dll for the mod based on required includes.
		/// </summary>
		// Token: 0x06003015 RID: 12309 RVA: 0x0053940C File Offset: 0x0053760C
		private static Diagnostic[] RoslynCompile(string name, List<string> references, string[] files, string[] preprocessorSymbols, bool allowUnsafe, out byte[] code, out byte[] pdb)
		{
			OutputKind outputKind = 2;
			bool flag = false;
			string text = null;
			string text2 = null;
			string text3 = null;
			IEnumerable<string> enumerable = null;
			AssemblyIdentityComparer @default = DesktopAssemblyIdentityComparer.Default;
			CSharpCompilationOptions options = new CSharpCompilationOptions(outputKind, flag, text, text2, text3, enumerable, preprocessorSymbols.Contains("DEBUG") ? 0 : 1, false, allowUnsafe, null, null, default(ImmutableArray<byte>), null, 0, 0, 4, null, true, false, null, null, null, @default, null, false, 0, 0);
			CSharpParseOptions parseOptions = new CSharpParseOptions(2147483646, 1, 0, preprocessorSymbols);
			EmitOptions emitOptions = new EmitOptions(false, 2, null, null, 0, 0UL, false, default(SubsystemVersion), null, false, true, default(ImmutableArray<InstrumentationKind>), null, null, null);
			IEnumerable<PortableExecutableReference> refs = from s in references
			select MetadataReference.CreateFromFile(s, default(MetadataReferenceProperties), null);
			refs = refs.Concat(Net80.References.All);
			IEnumerable<SyntaxTree> src = from f in files
			select SyntaxFactory.ParseSyntaxTree(File.ReadAllText(f), parseOptions, f, Encoding.UTF8, default(CancellationToken));
			CSharpCompilation comp = CSharpCompilation.Create(name, src, refs, options);
			Diagnostic[] result;
			using (MemoryStream peStream = new MemoryStream())
			{
				using (MemoryStream pdbStream = new MemoryStream())
				{
					EmitResult emitResult = comp.Emit(peStream, pdbStream, null, null, null, emitOptions, null, null, null, null, default(CancellationToken));
					code = peStream.ToArray();
					pdb = pdbStream.ToArray();
					result = (from d in emitResult.Diagnostics
					where d.Severity >= 2
					select d).ToArray<Diagnostic>();
				}
			}
			return result;
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x005395A8 File Offset: 0x005377A8
		internal static void UpdateSubstitutedDescriptionValues(ref string description, string modVersion, string homepage)
		{
			description = Language.GetText(description).FormatWith(new
			{
				ModVersion = modVersion,
				ModHomepage = homepage,
				tMLVersion = BuildInfo.tMLVersion.MajorMinor().ToString(),
				tMLBuildPurpose = BuildInfo.Purpose.ToString()
			});
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x005396AB File Offset: 0x005378AB
		[CompilerGenerated]
		internal static string <BuildMod>g__pdbPath|32_0(ref ModCompile.<>c__DisplayClass32_0 A_0)
		{
			return Path.ChangeExtension(A_0.dllPath, "pdb");
		}

		// Token: 0x04001CE2 RID: 7394
		public static readonly string ModSourcePath = Path.Combine(Program.SavePathShared, "ModSources");

		// Token: 0x04001CE3 RID: 7395
		public static bool activelyModding;

		// Token: 0x04001CE4 RID: 7396
		internal static DateTime recentlyBuiltModCheckTimeCutoff = DateTime.Now - TimeSpan.FromSeconds(60.0);

		// Token: 0x04001CE5 RID: 7397
		private static readonly string tMLDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		// Token: 0x04001CE6 RID: 7398
		private static readonly string oldModReferencesPath = Path.Combine(Program.SavePath, "references");

		// Token: 0x04001CE7 RID: 7399
		private static readonly string modTargetsPath = Path.Combine(ModCompile.ModSourcePath, "tModLoader.targets");

		// Token: 0x04001CE8 RID: 7400
		private static readonly string tMLModTargetsPath = Path.Combine(ModCompile.tMLDir, "tMLMod.targets");

		// Token: 0x04001CE9 RID: 7401
		private static bool referencesUpdated = false;

		// Token: 0x04001CEA RID: 7402
		internal static IList<string> sourceExtensions = new List<string>
		{
			".csproj",
			".cs",
			".sln"
		};

		// Token: 0x04001CEB RID: 7403
		private ModCompile.IBuildStatus status;

		// Token: 0x04001CEC RID: 7404
		private int packedResourceCount;

		// Token: 0x02000ABA RID: 2746
		public interface IBuildStatus
		{
			// Token: 0x06005A02 RID: 23042
			void SetProgress(int i, int n = -1);

			// Token: 0x06005A03 RID: 23043
			void SetStatus(string msg);

			// Token: 0x06005A04 RID: 23044
			void LogCompilerLine(string msg, Level level);
		}

		// Token: 0x02000ABB RID: 2747
		private class ConsoleBuildStatus : ModCompile.IBuildStatus
		{
			// Token: 0x06005A05 RID: 23045 RVA: 0x006A2FA8 File Offset: 0x006A11A8
			public void SetProgress(int i, int n)
			{
			}

			// Token: 0x06005A06 RID: 23046 RVA: 0x006A2FAA File Offset: 0x006A11AA
			public void SetStatus(string msg)
			{
				Console.WriteLine(msg);
			}

			// Token: 0x06005A07 RID: 23047 RVA: 0x006A2FB2 File Offset: 0x006A11B2
			public void LogCompilerLine(string msg, Level level)
			{
				((level == Level.Error) ? Console.Error : Console.Out).WriteLine(msg);
			}
		}

		// Token: 0x02000ABC RID: 2748
		private class BuildingMod : LocalMod
		{
			// Token: 0x06005A09 RID: 23049 RVA: 0x006A2FDB File Offset: 0x006A11DB
			public BuildingMod(TmodFile modFile, BuildProperties properties, string path) : base(ModLocation.Local, modFile, properties)
			{
				this.path = path;
			}

			// Token: 0x04006DF6 RID: 28150
			public string path;
		}

		// Token: 0x02000ABD RID: 2749
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006DF7 RID: 28151
			public static Func<string, bool> <0>__Exists;
		}
	}
}
