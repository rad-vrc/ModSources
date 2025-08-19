using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Stubble.Core;

namespace Terraria.ModLoader.Core
{
	/// <summary>
	/// Everything related to creating and maintaining mod source-code directories.
	/// </summary>
	// Token: 0x02000365 RID: 869
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SourceManagement
	{
		/// <summary> Writes mod template files to the provided source-code directory. </summary>
		// Token: 0x0600304D RID: 12365 RVA: 0x0053B7CC File Offset: 0x005399CC
		public static void WriteModTemplate(string modSrcDirectory, SourceManagement.TemplateParameters templateParameters)
		{
			Assembly assembly = typeof(ModLoader).Assembly;
			Directory.CreateDirectory(modSrcDirectory);
			foreach (string resourceKey in assembly.GetManifestResourceNames())
			{
				if (resourceKey.StartsWith("Terraria/ModLoader/Templates/"))
				{
					SourceManagement.TryWriteModTemplateFile(modSrcDirectory, resourceKey.Substring("Terraria/ModLoader/Templates/".Length), templateParameters);
				}
			}
		}

		/// <summary> Writes a single mod template file to the provided source-code directory. </summary>
		// Token: 0x0600304E RID: 12366 RVA: 0x0053B82C File Offset: 0x00539A2C
		public static bool TryWriteModTemplateFile(string modSrcDirectory, string partialResourceKey, SourceManagement.TemplateParameters templateParameters)
		{
			Assembly assembly = typeof(ModLoader).Assembly;
			string extension = Path.GetExtension(partialResourceKey);
			string relativePath = StaticStubbleRenderer.Render(partialResourceKey, templateParameters);
			string relativeDirectory = Path.GetDirectoryName(relativePath);
			if (string.IsNullOrWhiteSpace(Path.GetFileNameWithoutExtension(relativePath)))
			{
				return false;
			}
			string resourceKey = "Terraria/ModLoader/Templates/" + partialResourceKey;
			bool result;
			using (Stream resourceStream = assembly.GetManifestResourceStream(resourceKey))
			{
				byte[] data;
				if (SourceManagement.textExtensions.Contains(extension))
				{
					using (StreamReader streamReader = new StreamReader(resourceStream, null, true, -1, false))
					{
						string contents = StaticStubbleRenderer.Render(streamReader.ReadToEnd(), templateParameters);
						if (string.IsNullOrWhiteSpace(contents))
						{
							return false;
						}
						data = Encoding.UTF8.GetBytes(contents);
						goto IL_B5;
					}
				}
				data = new BinaryReader(resourceStream).ReadBytes((int)resourceStream.Length);
				IL_B5:
				Directory.CreateDirectory(Path.Combine(modSrcDirectory, relativeDirectory));
				File.WriteAllBytes(Path.Combine(modSrcDirectory, relativePath), data);
				result = true;
			}
			return result;
		}

		/// <summary> Returns whether the provided mod source-code directory requires an upgrade. </summary>
		// Token: 0x0600304F RID: 12367 RVA: 0x0053B938 File Offset: 0x00539B38
		public static bool SourceUpgradeNeeded(string modSrcDirectory)
		{
			return SourceManagement.CollectSourceUpgradeActions(modSrcDirectory).Count != 0;
		}

		/// <summary> Runs upgrades on the provided mod source-code directory. </summary>
		// Token: 0x06003050 RID: 12368 RVA: 0x0053B948 File Offset: 0x00539B48
		public static bool UpgradeSource(string modSrcDirectory)
		{
			List<Action> list = SourceManagement.CollectSourceUpgradeActions(modSrcDirectory);
			list.ForEach(delegate(Action a)
			{
				a();
			});
			return list.Count != 0;
		}

		/// <summary> Runs upgrades on the provided mod source-code directory. </summary>
		// Token: 0x06003051 RID: 12369 RVA: 0x0053B980 File Offset: 0x00539B80
		private static List<Action> CollectSourceUpgradeActions(string modSrcDirectory)
		{
			List<Action> modifications = new List<Action>();
			SourceManagement.TemplateParameters templateParameters = SourceManagement.TemplateParameters.FromSourceFolder(modSrcDirectory);
			Action csprojUpgradeAction;
			if (SourceManagement.TryGetCsprojUpgradeAction(modSrcDirectory, out csprojUpgradeAction, templateParameters))
			{
				modifications.Add(csprojUpgradeAction);
			}
			if (!File.Exists(Path.Combine(modSrcDirectory, "Properties", "launchSettings.json")))
			{
				modifications.Add(delegate
				{
					SourceManagement.TryWriteModTemplateFile(modSrcDirectory, "Properties/launchSettings.json", templateParameters);
				});
			}
			if (modifications.Count != 0)
			{
				modifications.Add(delegate
				{
					SourceManagement.DeleteIfExists(new DirectoryInfo(Path.Combine(modSrcDirectory, "obj")));
					SourceManagement.DeleteIfExists(new DirectoryInfo(Path.Combine(modSrcDirectory, "bin")));
					SourceManagement.DeleteIfExists(new FileInfo(Path.Combine(modSrcDirectory, "Properties", "AssemblyInfo.cs")));
				});
			}
			return modifications;
		}

		/// <summary> Checks a mod source-code directory for available upgrades, optionally applying them. </summary>
		// Token: 0x06003052 RID: 12370 RVA: 0x0053BA18 File Offset: 0x00539C18
		[NullableContext(2)]
		private static bool TryGetCsprojUpgradeAction([Nullable(1)] string modSrcDirectory, [NotNullWhen(true)] out Action result, SourceManagement.TemplateParameters templateParameters = null)
		{
			SourceManagement.<>c__DisplayClass9_0 CS$<>8__locals1 = new SourceManagement.<>c__DisplayClass9_0();
			CS$<>8__locals1.templateParameters = templateParameters;
			CS$<>8__locals1.csprojPath = Path.Combine(modSrcDirectory, Path.GetFileName(modSrcDirectory) + ".csproj");
			if (!File.Exists(CS$<>8__locals1.csprojPath) || !SourceManagement.TryLoadXmlDocument(CS$<>8__locals1.csprojPath, LoadOptions.PreserveWhitespace, out CS$<>8__locals1.document) || !SourceManagement.IsXmlAValidCsprojFile(CS$<>8__locals1.document))
			{
				result = delegate()
				{
					SourceManagement.ResetCsprojFile(CS$<>8__locals1.csprojPath, CS$<>8__locals1.templateParameters);
				};
				return true;
			}
			List<Action> modifications = SourceManagement.CollectCsprojModifications(CS$<>8__locals1.document);
			if (!modifications.Any<Action>())
			{
				result = null;
				return false;
			}
			result = delegate()
			{
				foreach (Action action in modifications)
				{
					action();
				}
				SourceManagement.WriteXmlDocumentToFile(CS$<>8__locals1.csprojPath, CS$<>8__locals1.document);
			};
			return true;
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x0053BAD0 File Offset: 0x00539CD0
		[NullableContext(2)]
		private static bool IsXmlAValidCsprojFile(XDocument document)
		{
			XElement xelement = (document != null) ? document.Root : null;
			if (xelement != null)
			{
				XName name = xelement.Name;
				if (name != null && name.LocalName == "Project")
				{
					XAttribute firstAttribute = xelement.FirstAttribute;
					if (firstAttribute != null)
					{
						XName name2 = firstAttribute.Name;
						if (name2 != null && name2.LocalName == "Sdk")
						{
							return firstAttribute.Value == "Microsoft.NET.Sdk";
						}
					}
				}
			}
			return false;
		}

		/// <summary> Returns an enumerable of delegates, executing which will upgrade the provided document. </summary>
		// Token: 0x06003054 RID: 12372 RVA: 0x0053BB44 File Offset: 0x00539D44
		private static List<Action> CollectCsprojModifications(XDocument document)
		{
			SourceManagement.<>c__DisplayClass11_0 CS$<>8__locals1 = new SourceManagement.<>c__DisplayClass11_0();
			CS$<>8__locals1.modifications = new List<Action>();
			CS$<>8__locals1.root = document.Root;
			IEnumerable<XElement> itemGroups = from e in CS$<>8__locals1.root.Elements("ItemGroup")
			where e.Attribute("Condition") == null
			select e;
			IEnumerable<XElement> propertyGroups = from e in CS$<>8__locals1.root.Elements("PropertyGroup")
			where e.Attribute("Condition") == null
			select e;
			if (!CS$<>8__locals1.root.Elements("Import").Any(delegate(XElement e)
			{
				if (e != null)
				{
					XAttribute firstAttribute = e.FirstAttribute;
					if (firstAttribute != null)
					{
						XName name = firstAttribute.Name;
						if (name != null && name.LocalName == "Project")
						{
							return firstAttribute.Value == "..\\tModLoader.targets";
						}
					}
				}
				return false;
			}))
			{
				CS$<>8__locals1.modifications.Add(delegate
				{
					XElement import = new XElement("Import");
					import.SetAttributeValue("Project", "..\\tModLoader.targets");
					CS$<>8__locals1.root.AddFirst(new object[]
					{
						new XText("\n\n\t"),
						new XComment(" Import tModLoader mod properties "),
						new XText("\n\t"),
						import
					});
				});
			}
			CS$<>8__locals1.<CollectCsprojModifications>g__RemoveNodes|0(propertyGroups.Elements("TargetFramework").Concat(propertyGroups.Elements("PlatformTarget")));
			CS$<>8__locals1.<CollectCsprojModifications>g__RemoveNodes|0(itemGroups.Elements("PackageReference").Where(delegate(XElement e)
			{
				XAttribute xattribute = e.Attribute("Include");
				return ((xattribute != null) ? xattribute.Value : null) == "tModLoader.CodeAssist";
			}));
			CS$<>8__locals1.<CollectCsprojModifications>g__RemoveNodes|0(propertyGroups.Elements("LangVersion").Where(delegate(XElement e)
			{
				Version v;
				return Version.TryParse(e.Value, out v) && v.MajorMinor() <= SourceManagement.maxLanguageVersionToRemove;
			}));
			return CS$<>8__locals1.modifications;
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x0053BCDC File Offset: 0x00539EDC
		private static bool TryLoadXmlDocument(string filePath, LoadOptions loadOptions, [Nullable(2)] [NotNullWhen(true)] out XDocument document)
		{
			bool result;
			using (new Logging.QuietExceptionHandle())
			{
				try
				{
					document = XDocument.Parse(File.ReadAllText(filePath), loadOptions);
					result = true;
				}
				catch
				{
					document = null;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x0053BD30 File Offset: 0x00539F30
		private static void WriteXmlDocumentToFile(string filePath, XDocument document)
		{
			StringBuilder sb = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(sb, new XmlWriterSettings
			{
				Indent = true,
				IndentChars = "\t",
				OmitXmlDeclaration = true
			}))
			{
				document.WriteTo(xmlWriter);
				xmlWriter.Flush();
				File.WriteAllText(filePath, sb.ToString());
			}
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x0053BDA0 File Offset: 0x00539FA0
		private static void ResetCsprojFile(string csprojPath, [Nullable(2)] SourceManagement.TemplateParameters templateParameters = null)
		{
			string modSrcDirectory = Path.GetDirectoryName(csprojPath);
			if (File.Exists(csprojPath))
			{
				File.Move(csprojPath, csprojPath + ".bak", true);
			}
			SourceManagement.TryWriteModTemplateFile(modSrcDirectory, "{{ModName}}.csproj", templateParameters ?? SourceManagement.TemplateParameters.FromSourceFolder(modSrcDirectory));
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x0053BDE8 File Offset: 0x00539FE8
		private static void DeleteIfExists(FileSystemInfo entry)
		{
			try
			{
				if (entry != null)
				{
					if (entry.Exists)
					{
						FileInfo file = entry as FileInfo;
						if (file == null)
						{
							DirectoryInfo directory = entry as DirectoryInfo;
							if (directory != null)
							{
								directory.Delete(true);
							}
						}
						else
						{
							file.Delete();
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04001CFE RID: 7422
		private const string TemplateResourcePrefix = "Terraria/ModLoader/Templates/";

		// Token: 0x04001CFF RID: 7423
		private static readonly Version maxLanguageVersionToRemove = new Version(12, 0);

		// Token: 0x04001D00 RID: 7424
		private static readonly HashSet<string> textExtensions = new HashSet<string>
		{
			".txt",
			".json",
			".hjson",
			".toml",
			".cs",
			".csproj",
			".sln"
		};

		// Token: 0x02000ADA RID: 2778
		[Nullable(0)]
		[RequiredMember]
		public class TemplateParameters : IEquatable<SourceManagement.TemplateParameters>
		{
			// Token: 0x17000926 RID: 2342
			// (get) Token: 0x06005A80 RID: 23168 RVA: 0x006A3CB7 File Offset: 0x006A1EB7
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[CompilerGenerated]
				get
				{
					return typeof(SourceManagement.TemplateParameters);
				}
			}

			// Token: 0x17000927 RID: 2343
			// (get) Token: 0x06005A81 RID: 23169 RVA: 0x006A3CC3 File Offset: 0x006A1EC3
			// (set) Token: 0x06005A82 RID: 23170 RVA: 0x006A3CCB File Offset: 0x006A1ECB
			[RequiredMember]
			public string ModName { get; set; }

			// Token: 0x17000928 RID: 2344
			// (get) Token: 0x06005A83 RID: 23171 RVA: 0x006A3CD4 File Offset: 0x006A1ED4
			// (set) Token: 0x06005A84 RID: 23172 RVA: 0x006A3CDC File Offset: 0x006A1EDC
			[RequiredMember]
			public string ModDisplayName { get; set; }

			// Token: 0x17000929 RID: 2345
			// (get) Token: 0x06005A85 RID: 23173 RVA: 0x006A3CE5 File Offset: 0x006A1EE5
			// (set) Token: 0x06005A86 RID: 23174 RVA: 0x006A3CED File Offset: 0x006A1EED
			[RequiredMember]
			public string ModAuthor { get; set; }

			// Token: 0x1700092A RID: 2346
			// (get) Token: 0x06005A87 RID: 23175 RVA: 0x006A3CF6 File Offset: 0x006A1EF6
			// (set) Token: 0x06005A88 RID: 23176 RVA: 0x006A3CFE File Offset: 0x006A1EFE
			[RequiredMember]
			public string ModVersion { get; set; }

			// Token: 0x1700092B RID: 2347
			// (get) Token: 0x06005A89 RID: 23177 RVA: 0x006A3D07 File Offset: 0x006A1F07
			// (set) Token: 0x06005A8A RID: 23178 RVA: 0x006A3D0F File Offset: 0x006A1F0F
			public string ItemName { get; set; }

			// Token: 0x1700092C RID: 2348
			// (get) Token: 0x06005A8B RID: 23179 RVA: 0x006A3D18 File Offset: 0x006A1F18
			// (set) Token: 0x06005A8C RID: 23180 RVA: 0x006A3D20 File Offset: 0x006A1F20
			public string ItemDisplayName { get; set; }

			// Token: 0x1700092D RID: 2349
			// (get) Token: 0x06005A8D RID: 23181 RVA: 0x006A3D29 File Offset: 0x006A1F29
			public bool IncludeItem
			{
				get
				{
					return this.ItemName != string.Empty;
				}
			}

			// Token: 0x06005A8E RID: 23182 RVA: 0x006A3D3C File Offset: 0x006A1F3C
			public static SourceManagement.TemplateParameters FromSourceFolder(string modSrcDirectory)
			{
				BuildProperties properties = BuildProperties.ReadBuildFile(modSrcDirectory);
				return new SourceManagement.TemplateParameters
				{
					ModName = Path.GetFileName(modSrcDirectory),
					ModDisplayName = properties.displayName,
					ModAuthor = properties.author,
					ModVersion = properties.version.ToString(),
					ItemName = string.Empty,
					ItemDisplayName = string.Empty
				};
			}

			// Token: 0x06005A8F RID: 23183 RVA: 0x006A3DA0 File Offset: 0x006A1FA0
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("TemplateParameters");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06005A90 RID: 23184 RVA: 0x006A3DEC File Offset: 0x006A1FEC
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("ModName = ");
				builder.Append(this.ModName);
				builder.Append(", ModDisplayName = ");
				builder.Append(this.ModDisplayName);
				builder.Append(", ModAuthor = ");
				builder.Append(this.ModAuthor);
				builder.Append(", ModVersion = ");
				builder.Append(this.ModVersion);
				builder.Append(", ItemName = ");
				builder.Append(this.ItemName);
				builder.Append(", ItemDisplayName = ");
				builder.Append(this.ItemDisplayName);
				builder.Append(", IncludeItem = ");
				builder.Append(this.IncludeItem.ToString());
				return true;
			}

			// Token: 0x06005A91 RID: 23185 RVA: 0x006A3EBC File Offset: 0x006A20BC
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(SourceManagement.TemplateParameters left, SourceManagement.TemplateParameters right)
			{
				return !(left == right);
			}

			// Token: 0x06005A92 RID: 23186 RVA: 0x006A3EC8 File Offset: 0x006A20C8
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(SourceManagement.TemplateParameters left, SourceManagement.TemplateParameters right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x06005A93 RID: 23187 RVA: 0x006A3EDC File Offset: 0x006A20DC
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ModName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ModDisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ModAuthor>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ModVersion>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ItemName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ItemDisplayName>k__BackingField);
			}

			// Token: 0x06005A94 RID: 23188 RVA: 0x006A3F83 File Offset: 0x006A2183
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as SourceManagement.TemplateParameters);
			}

			// Token: 0x06005A95 RID: 23189 RVA: 0x006A3F94 File Offset: 0x006A2194
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(SourceManagement.TemplateParameters other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ModName>k__BackingField, other.<ModName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ModDisplayName>k__BackingField, other.<ModDisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ModAuthor>k__BackingField, other.<ModAuthor>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ModVersion>k__BackingField, other.<ModVersion>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ItemName>k__BackingField, other.<ItemName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ItemDisplayName>k__BackingField, other.<ItemDisplayName>k__BackingField));
			}

			// Token: 0x06005A97 RID: 23191 RVA: 0x006A4060 File Offset: 0x006A2260
			[CompilerGenerated]
			[SetsRequiredMembers]
			protected TemplateParameters(SourceManagement.TemplateParameters original)
			{
				this.ModName = original.<ModName>k__BackingField;
				this.ModDisplayName = original.<ModDisplayName>k__BackingField;
				this.ModAuthor = original.<ModAuthor>k__BackingField;
				this.ModVersion = original.<ModVersion>k__BackingField;
				this.ItemName = original.<ItemName>k__BackingField;
				this.ItemDisplayName = original.<ItemDisplayName>k__BackingField;
			}

			// Token: 0x06005A98 RID: 23192 RVA: 0x006A40BB File Offset: 0x006A22BB
			[Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
			[CompilerFeatureRequired("RequiredMembers")]
			public TemplateParameters()
			{
				this.ItemName = string.Empty;
				this.ItemDisplayName = string.Empty;
				base..ctor();
			}
		}
	}
}
