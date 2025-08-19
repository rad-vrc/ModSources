using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using log4net;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000353 RID: 851
	internal class BuildProperties
	{
		// Token: 0x06002F7F RID: 12159 RVA: 0x00534F30 File Offset: 0x00533130
		public IEnumerable<BuildProperties.ModReference> Refs(bool includeWeak)
		{
			if (!includeWeak)
			{
				return this.modReferences;
			}
			return this.modReferences.Concat(this.weakReferences);
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x00534F5A File Offset: 0x0053315A
		public IEnumerable<string> RefNames(bool includeWeak)
		{
			return from dep in this.Refs(includeWeak)
			select dep.mod;
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x00534F88 File Offset: 0x00533188
		private static IEnumerable<string> ReadList(string value)
		{
			return from s in value.Split(',', StringSplitOptions.None)
			select s.Trim() into s
			where s.Length > 0
			select s;
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x00534FE8 File Offset: 0x005331E8
		private static IEnumerable<string> ReadList(BinaryReader reader)
		{
			List<string> list = new List<string>();
			string item = reader.ReadString();
			while (item.Length > 0)
			{
				list.Add(item);
				item = reader.ReadString();
			}
			return list;
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x0053501C File Offset: 0x0053321C
		private static void WriteList<T>(IEnumerable<T> list, BinaryWriter writer)
		{
			foreach (T item in list)
			{
				writer.Write(item.ToString());
			}
			writer.Write("");
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x0053507C File Offset: 0x0053327C
		internal static BuildProperties ReadBuildFile(string modDir)
		{
			string propertiesFile = modDir + Path.DirectorySeparatorChar.ToString() + "build.txt";
			string descriptionfile = modDir + Path.DirectorySeparatorChar.ToString() + "description.txt";
			BuildProperties properties = new BuildProperties();
			if (!File.Exists(propertiesFile))
			{
				return properties;
			}
			if (File.Exists(descriptionfile))
			{
				properties.description = File.ReadAllText(descriptionfile);
			}
			foreach (string line in File.ReadAllLines(propertiesFile))
			{
				if (!string.IsNullOrWhiteSpace(line))
				{
					int split = line.IndexOf('=');
					if (split >= 0)
					{
						string property = line.Substring(0, split).Trim();
						string value = line.Substring(split + 1).Trim();
						if (value.Length != 0 && property != null)
						{
							switch (property.Length)
							{
							case 4:
								if (property == "side")
								{
									if (!Enum.TryParse<ModSide>(value, true, out properties.side))
									{
										throw new Exception("side is not one of (Both, Client, Server, NoSync): " + value);
									}
								}
								break;
							case 6:
								if (property == "author")
								{
									properties.author = value;
								}
								break;
							case 7:
								if (property == "version")
								{
									Version result;
									if (Version.TryParse(value, out result))
									{
										properties.version = result;
									}
									else
									{
										ILog tML = Logging.tML;
										DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(206, 2);
										defaultInterpolatedStringHandler.AppendLiteral("The version found in ");
										defaultInterpolatedStringHandler.AppendFormatted(propertiesFile);
										defaultInterpolatedStringHandler.AppendLiteral(", \"");
										defaultInterpolatedStringHandler.AppendFormatted(value);
										defaultInterpolatedStringHandler.AppendLiteral("\", is not a valid version number. Read the \"version\" section of https://github.com/tModLoader/tModLoader/wiki/build.txt#available-properties for more info on correct version numbers.");
										tML.Error(defaultInterpolatedStringHandler.ToStringAndClear());
									}
								}
								break;
							case 8:
							{
								char c = property[1];
								if (c != 'i')
								{
									if (c == 'o')
									{
										if (property == "homepage")
										{
											properties.homepage = value;
										}
									}
								}
								else if (property == "hideCode")
								{
									properties.hideCode = string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
								}
								break;
							}
							case 9:
							{
								char c = property[0];
								if (c != 'n')
								{
									if (c == 's')
									{
										if (property == "sortAfter")
										{
											properties.sortAfter = BuildProperties.ReadList(value).ToArray<string>();
										}
									}
								}
								else if (property == "noCompile")
								{
									properties.noCompile = string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
								}
								break;
							}
							case 10:
								if (property == "sortBefore")
								{
									properties.sortBefore = BuildProperties.ReadList(value).ToArray<string>();
								}
								break;
							case 11:
							{
								char c = property[0];
								if (c != 'b')
								{
									if (c == 'd')
									{
										if (property == "displayName")
										{
											properties.displayName = value;
										}
									}
								}
								else if (property == "buildIgnore")
								{
									properties.buildIgnores = (from s in value.Split(',', StringSplitOptions.None)
									select s.Trim().Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar) into s
									where s.Length > 0
									select s).ToArray<string>();
								}
								break;
							}
							case 13:
							{
								char c = property[0];
								if (c <= 'h')
								{
									if (c != 'd')
									{
										if (c == 'h')
										{
											if (property == "hideResources")
											{
												properties.hideResources = string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
											}
										}
									}
									else if (property == "dllReferences")
									{
										properties.dllReferences = BuildProperties.ReadList(value).ToArray<string>();
									}
								}
								else if (c != 'i')
								{
									if (c == 'm')
									{
										if (property == "modReferences")
										{
											BuildProperties properties3 = properties;
											IEnumerable<string> source = BuildProperties.ReadList(value);
											Func<string, BuildProperties.ModReference> selector;
											if ((selector = BuildProperties.<>O.<0>__Parse) == null)
											{
												selector = (BuildProperties.<>O.<0>__Parse = new Func<string, BuildProperties.ModReference>(BuildProperties.ModReference.Parse));
											}
											properties3.modReferences = source.Select(selector).ToArray<BuildProperties.ModReference>();
										}
									}
								}
								else if (property == "includeSource")
								{
									properties.includeSource = string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
								}
								break;
							}
							case 14:
							{
								char c = property[0];
								if (c != 't')
								{
									if (c == 'w')
									{
										if (property == "weakReferences")
										{
											BuildProperties properties2 = properties;
											IEnumerable<string> source2 = BuildProperties.ReadList(value);
											Func<string, BuildProperties.ModReference> selector2;
											if ((selector2 = BuildProperties.<>O.<0>__Parse) == null)
											{
												selector2 = (BuildProperties.<>O.<0>__Parse = new Func<string, BuildProperties.ModReference>(BuildProperties.ModReference.Parse));
											}
											properties2.weakReferences = source2.Select(selector2).ToArray<BuildProperties.ModReference>();
										}
									}
								}
								else if (property == "translationMod")
								{
									properties.translationMod = string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
								}
								break;
							}
							case 17:
								if (property == "playableOnPreview")
								{
									properties.playableOnPreview = string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
								}
								break;
							}
						}
					}
				}
			}
			List<string> refs = properties.RefNames(true).ToList<string>();
			if (refs.Count != refs.Distinct<string>().Count<string>())
			{
				throw new Exception("Duplicate mod/weak reference");
			}
			if (properties.dllReferences.Intersect(from x in properties.modReferences
			select x.mod).Any<string>())
			{
				throw new Exception("dllReferences contains duplicate of modReferences");
			}
			properties.sortAfter = (from dep in properties.RefNames(true)
			where !properties.sortBefore.Contains(dep)
			select dep).Concat(properties.sortAfter).Distinct<string>().ToArray<string>();
			ModCompile.UpdateSubstitutedDescriptionValues(ref properties.description, properties.version.ToString(), properties.homepage);
			return properties;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x00535798 File Offset: 0x00533998
		internal byte[] ToBytes()
		{
			byte[] data;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(memoryStream))
				{
					if (this.dllReferences.Length != 0)
					{
						writer.Write("dllReferences");
						BuildProperties.WriteList<string>(this.dllReferences, writer);
					}
					if (this.modReferences.Length != 0)
					{
						writer.Write("modReferences");
						BuildProperties.WriteList<BuildProperties.ModReference>(this.modReferences, writer);
					}
					if (this.weakReferences.Length != 0)
					{
						writer.Write("weakReferences");
						BuildProperties.WriteList<BuildProperties.ModReference>(this.weakReferences, writer);
					}
					if (this.sortAfter.Length != 0)
					{
						writer.Write("sortAfter");
						BuildProperties.WriteList<string>(this.sortAfter, writer);
					}
					if (this.sortBefore.Length != 0)
					{
						writer.Write("sortBefore");
						BuildProperties.WriteList<string>(this.sortBefore, writer);
					}
					if (this.author.Length > 0)
					{
						writer.Write("author");
						writer.Write(this.author);
					}
					writer.Write("version");
					writer.Write(this.version.ToString());
					if (this.displayName.Length > 0)
					{
						writer.Write("displayName");
						writer.Write(this.displayName);
					}
					if (this.homepage.Length > 0)
					{
						writer.Write("homepage");
						writer.Write(this.homepage);
					}
					if (this.description.Length > 0)
					{
						writer.Write("description");
						writer.Write(this.description);
					}
					if (this.noCompile)
					{
						writer.Write("noCompile");
					}
					if (!this.playableOnPreview)
					{
						writer.Write("!playableOnPreview");
					}
					if (this.translationMod)
					{
						writer.Write("translationMod");
					}
					if (!this.hideCode)
					{
						writer.Write("!hideCode");
					}
					if (!this.hideResources)
					{
						writer.Write("!hideResources");
					}
					if (this.includeSource)
					{
						writer.Write("includeSource");
					}
					if (this.eacPath.Length > 0)
					{
						writer.Write("eacPath");
						writer.Write(this.eacPath);
					}
					if (this.side != ModSide.Both)
					{
						writer.Write("side");
						writer.Write((byte)this.side);
					}
					if (this.modSource.Length > 0)
					{
						writer.Write("modSource");
						writer.Write(this.modSource);
					}
					writer.Write("buildVersion");
					writer.Write(this.buildVersion.ToString());
					writer.Write("");
				}
				data = memoryStream.ToArray();
			}
			return data;
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x00535A5C File Offset: 0x00533C5C
		internal static BuildProperties ReadModFile(TmodFile modFile)
		{
			return BuildProperties.ReadFromStream(modFile.GetStream("Info", false));
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x00535A70 File Offset: 0x00533C70
		internal static BuildProperties ReadFromStream(Stream stream)
		{
			BuildProperties properties = new BuildProperties();
			properties.hideCode = true;
			properties.hideResources = true;
			using (BinaryReader reader = new BinaryReader(stream))
			{
				string tag = reader.ReadString();
				while (tag.Length > 0)
				{
					if (tag == "dllReferences")
					{
						properties.dllReferences = BuildProperties.ReadList(reader).ToArray<string>();
					}
					if (tag == "modReferences")
					{
						BuildProperties buildProperties = properties;
						IEnumerable<string> source = BuildProperties.ReadList(reader);
						Func<string, BuildProperties.ModReference> selector;
						if ((selector = BuildProperties.<>O.<0>__Parse) == null)
						{
							selector = (BuildProperties.<>O.<0>__Parse = new Func<string, BuildProperties.ModReference>(BuildProperties.ModReference.Parse));
						}
						buildProperties.modReferences = source.Select(selector).ToArray<BuildProperties.ModReference>();
					}
					if (tag == "weakReferences")
					{
						BuildProperties buildProperties2 = properties;
						IEnumerable<string> source2 = BuildProperties.ReadList(reader);
						Func<string, BuildProperties.ModReference> selector2;
						if ((selector2 = BuildProperties.<>O.<0>__Parse) == null)
						{
							selector2 = (BuildProperties.<>O.<0>__Parse = new Func<string, BuildProperties.ModReference>(BuildProperties.ModReference.Parse));
						}
						buildProperties2.weakReferences = source2.Select(selector2).ToArray<BuildProperties.ModReference>();
					}
					if (tag == "sortAfter")
					{
						properties.sortAfter = BuildProperties.ReadList(reader).ToArray<string>();
					}
					if (tag == "sortBefore")
					{
						properties.sortBefore = BuildProperties.ReadList(reader).ToArray<string>();
					}
					if (tag == "author")
					{
						properties.author = reader.ReadString();
					}
					if (tag == "version")
					{
						properties.version = new Version(reader.ReadString());
					}
					if (tag == "displayName")
					{
						properties.displayName = reader.ReadString();
					}
					if (tag == "homepage")
					{
						properties.homepage = reader.ReadString();
					}
					if (tag == "description")
					{
						properties.description = reader.ReadString();
					}
					if (tag == "noCompile")
					{
						properties.noCompile = true;
					}
					if (tag == "!playableOnPreview")
					{
						properties.playableOnPreview = false;
					}
					if (tag == "translationMod")
					{
						properties.translationMod = true;
					}
					if (tag == "!hideCode")
					{
						properties.hideCode = false;
					}
					if (tag == "!hideResources")
					{
						properties.hideResources = false;
					}
					if (tag == "includeSource")
					{
						properties.includeSource = true;
					}
					if (tag == "eacPath")
					{
						properties.eacPath = reader.ReadString();
					}
					if (tag == "side")
					{
						properties.side = (ModSide)reader.ReadByte();
					}
					if (tag == "buildVersion")
					{
						properties.buildVersion = new Version(reader.ReadString());
					}
					if (tag == "modSource")
					{
						properties.modSource = reader.ReadString();
					}
					tag = reader.ReadString();
				}
			}
			return properties;
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x00535D1C File Offset: 0x00533F1C
		internal static void InfoToBuildTxt(Stream src, Stream dst)
		{
			BuildProperties properties = BuildProperties.ReadFromStream(src);
			StringBuilder sb = new StringBuilder();
			StringBuilder stringBuilder;
			StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler;
			if (properties.displayName.Length > 0)
			{
				stringBuilder = sb;
				StringBuilder stringBuilder2 = stringBuilder;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(14, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("displayName = ");
				appendInterpolatedStringHandler.AppendFormatted(properties.displayName);
				stringBuilder2.AppendLine(ref appendInterpolatedStringHandler);
			}
			if (properties.author.Length > 0)
			{
				stringBuilder = sb;
				StringBuilder stringBuilder3 = stringBuilder;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(9, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("author = ");
				appendInterpolatedStringHandler.AppendFormatted(properties.author);
				stringBuilder3.AppendLine(ref appendInterpolatedStringHandler);
			}
			stringBuilder = sb;
			StringBuilder stringBuilder4 = stringBuilder;
			appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(10, 1, stringBuilder);
			appendInterpolatedStringHandler.AppendLiteral("version = ");
			appendInterpolatedStringHandler.AppendFormatted<Version>(properties.version);
			stringBuilder4.AppendLine(ref appendInterpolatedStringHandler);
			if (properties.homepage.Length > 0)
			{
				stringBuilder = sb;
				StringBuilder stringBuilder5 = stringBuilder;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(11, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("homepage = ");
				appendInterpolatedStringHandler.AppendFormatted(properties.homepage);
				stringBuilder5.AppendLine(ref appendInterpolatedStringHandler);
			}
			if (properties.dllReferences.Length != 0)
			{
				stringBuilder = sb;
				StringBuilder stringBuilder6 = stringBuilder;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(16, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("dllReferences = ");
				appendInterpolatedStringHandler.AppendFormatted(string.Join(", ", properties.dllReferences));
				stringBuilder6.AppendLine(ref appendInterpolatedStringHandler);
			}
			if (properties.modReferences.Length != 0)
			{
				stringBuilder = sb;
				StringBuilder stringBuilder7 = stringBuilder;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(16, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("modReferences = ");
				appendInterpolatedStringHandler.AppendFormatted(string.Join<BuildProperties.ModReference>(", ", properties.modReferences));
				stringBuilder7.AppendLine(ref appendInterpolatedStringHandler);
			}
			if (properties.weakReferences.Length != 0)
			{
				stringBuilder = sb;
				StringBuilder stringBuilder8 = stringBuilder;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(17, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("weakReferences = ");
				appendInterpolatedStringHandler.AppendFormatted(string.Join<BuildProperties.ModReference>(", ", properties.weakReferences));
				stringBuilder8.AppendLine(ref appendInterpolatedStringHandler);
			}
			if (properties.noCompile)
			{
				sb.AppendLine("noCompile = true");
			}
			if (properties.hideCode)
			{
				sb.AppendLine("hideCode = true");
			}
			if (properties.hideResources)
			{
				sb.AppendLine("hideResources = true");
			}
			if (properties.includeSource)
			{
				sb.AppendLine("includeSource = true");
			}
			if (!properties.playableOnPreview)
			{
				sb.AppendLine("playableOnPreview = false");
			}
			if (properties.translationMod)
			{
				sb.AppendLine("translationMod = true");
			}
			if (properties.side != ModSide.Both)
			{
				stringBuilder = sb;
				StringBuilder stringBuilder9 = stringBuilder;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(7, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("side = ");
				appendInterpolatedStringHandler.AppendFormatted<ModSide>(properties.side);
				stringBuilder9.AppendLine(ref appendInterpolatedStringHandler);
			}
			if (properties.sortAfter.Length != 0)
			{
				stringBuilder = sb;
				StringBuilder stringBuilder10 = stringBuilder;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(12, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("sortAfter = ");
				appendInterpolatedStringHandler.AppendFormatted(string.Join(", ", properties.sortAfter));
				stringBuilder10.AppendLine(ref appendInterpolatedStringHandler);
			}
			if (properties.sortBefore.Length != 0)
			{
				stringBuilder = sb;
				StringBuilder stringBuilder11 = stringBuilder;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(13, 1, stringBuilder);
				appendInterpolatedStringHandler.AppendLiteral("sortBefore = ");
				appendInterpolatedStringHandler.AppendFormatted(string.Join(", ", properties.sortBefore));
				stringBuilder11.AppendLine(ref appendInterpolatedStringHandler);
			}
			byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
			dst.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x00536030 File Offset: 0x00534230
		internal bool ignoreFile(string resource)
		{
			return this.buildIgnores.Any((string fileMask) => this.FitsMask(resource, fileMask));
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x00536068 File Offset: 0x00534268
		private bool FitsMask(string fileName, string fileMask)
		{
			return new Regex("^" + Regex.Escape(fileMask.Replace(".", "__DOT__").Replace("*", "__STAR__").Replace("?", "__QM__")).Replace("__DOT__", "[.]").Replace("__STAR__", ".*").Replace("__QM__", ".") + "$", RegexOptions.IgnoreCase).IsMatch(fileName);
		}

		// Token: 0x04001C9B RID: 7323
		internal string[] dllReferences = new string[0];

		// Token: 0x04001C9C RID: 7324
		internal BuildProperties.ModReference[] modReferences = new BuildProperties.ModReference[0];

		// Token: 0x04001C9D RID: 7325
		internal BuildProperties.ModReference[] weakReferences = new BuildProperties.ModReference[0];

		// Token: 0x04001C9E RID: 7326
		internal string[] sortAfter = new string[0];

		// Token: 0x04001C9F RID: 7327
		internal string[] sortBefore = new string[0];

		// Token: 0x04001CA0 RID: 7328
		internal string[] buildIgnores = new string[0];

		// Token: 0x04001CA1 RID: 7329
		internal string author = "";

		// Token: 0x04001CA2 RID: 7330
		internal Version version = new Version(1, 0);

		// Token: 0x04001CA3 RID: 7331
		internal string displayName = "";

		// Token: 0x04001CA4 RID: 7332
		internal bool noCompile;

		// Token: 0x04001CA5 RID: 7333
		internal bool hideCode;

		// Token: 0x04001CA6 RID: 7334
		internal bool hideResources;

		// Token: 0x04001CA7 RID: 7335
		internal bool includeSource;

		// Token: 0x04001CA8 RID: 7336
		internal string eacPath = "";

		// Token: 0x04001CA9 RID: 7337
		internal bool beta;

		// Token: 0x04001CAA RID: 7338
		internal Version buildVersion = BuildInfo.tMLVersion;

		// Token: 0x04001CAB RID: 7339
		internal string homepage = "";

		// Token: 0x04001CAC RID: 7340
		internal string description = "";

		// Token: 0x04001CAD RID: 7341
		internal ModSide side;

		// Token: 0x04001CAE RID: 7342
		internal bool playableOnPreview = true;

		// Token: 0x04001CAF RID: 7343
		internal bool translationMod;

		// Token: 0x04001CB0 RID: 7344
		internal string modSource = "";

		// Token: 0x02000AA1 RID: 2721
		internal struct ModReference
		{
			// Token: 0x060059A9 RID: 22953 RVA: 0x006A2667 File Offset: 0x006A0867
			public ModReference(string mod, Version target)
			{
				this.mod = mod;
				this.target = target;
			}

			// Token: 0x060059AA RID: 22954 RVA: 0x006A2677 File Offset: 0x006A0877
			public override string ToString()
			{
				if (!(this.target == null))
				{
					string str = this.mod;
					string str2 = "@";
					Version version = this.target;
					return str + str2 + ((version != null) ? version.ToString() : null);
				}
				return this.mod;
			}

			// Token: 0x060059AB RID: 22955 RVA: 0x006A26B0 File Offset: 0x006A08B0
			public static BuildProperties.ModReference Parse(string spec)
			{
				string[] split = spec.Split('@', StringSplitOptions.None);
				if (split.Length == 1)
				{
					return new BuildProperties.ModReference(split[0], null);
				}
				if (split.Length > 2)
				{
					throw new Exception("Invalid mod reference: " + spec);
				}
				BuildProperties.ModReference result;
				try
				{
					result = new BuildProperties.ModReference(split[0], new Version(split[1]));
				}
				catch
				{
					throw new Exception("Invalid mod reference: " + spec);
				}
				return result;
			}

			// Token: 0x04006DAE RID: 28078
			public string mod;

			// Token: 0x04006DAF RID: 28079
			public Version target;
		}

		// Token: 0x02000AA2 RID: 2722
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006DB0 RID: 28080
			public static Func<string, BuildProperties.ModReference> <0>__Parse;
		}
	}
}
