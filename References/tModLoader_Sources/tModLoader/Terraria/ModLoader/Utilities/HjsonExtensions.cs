using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Hjson;

namespace Terraria.ModLoader.Utilities
{
	// Token: 0x02000236 RID: 566
	internal static class HjsonExtensions
	{
		// Token: 0x060028B3 RID: 10419 RVA: 0x0050CBC4 File Offset: 0x0050ADC4
		public static string ToFancyHjsonString(this JsonValue value, HjsonExtensions.HjsonStyle? style = null)
		{
			StringWriter stringWriter = new StringWriter();
			HjsonExtensions.HjsonStyle usedStyle = style ?? HjsonExtensions.DefaultHjsonStyle;
			HjsonExtensions.WriteFancyHjsonValue(stringWriter, value, 0, usedStyle, false, true, true);
			return stringWriter.ToString();
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x0050CC04 File Offset: 0x0050AE04
		private static void WriteFancyHjsonValue(TextWriter tw, JsonValue value, int level, in HjsonExtensions.HjsonStyle style, bool hasComments = false, bool noIndentation = false, bool isRootObject = false)
		{
			switch (value.JsonType)
			{
			case 0:
				HjsonExtensions.WriteString(tw, value.GetRawString(), level, hasComments, style.Separator);
				return;
			case 2:
			{
				JsonObject jObject = JsonUtil.Qo(value);
				WscJsonObject commentedObject = style.WriteComments ? (jObject as WscJsonObject) : null;
				bool showBraces = !isRootObject || ((commentedObject != null) ? commentedObject.RootBraces : style.EmitRootBraces);
				if (!noIndentation || showBraces)
				{
					tw.Write(style.Separator);
				}
				if (showBraces)
				{
					tw.Write("{");
				}
				if (commentedObject != null)
				{
					bool skipFirst = !showBraces;
					string kwl = HjsonExtensions.GetComments(commentedObject.Comments, "");
					JsonType? lastJsonType = null;
					foreach (string key in commentedObject.Order.Concat(commentedObject.Keys).Distinct<string>())
					{
						if (jObject.ContainsKey(key))
						{
							JsonValue val = jObject[key];
							JsonType jsonType = val.JsonType;
							JsonType? jsonType2 = lastJsonType;
							if (!(jsonType == jsonType2.GetValueOrDefault() & jsonType2 != null) && lastJsonType != null)
							{
								goto IL_128;
							}
							jsonType2 = lastJsonType;
							JsonType jsonType3 = 2;
							if (jsonType2.GetValueOrDefault() == jsonType3 & jsonType2 != null)
							{
								goto IL_128;
							}
							IL_12F:
							if (!skipFirst)
							{
								HjsonExtensions.NewLine(tw, level + ((showBraces > false) ? 1 : 0));
							}
							else
							{
								skipFirst = false;
							}
							if (!string.IsNullOrWhiteSpace(kwl))
							{
								string indentation = new string('\t', level + ((showBraces > false) ? 1 : 0));
								string[] lines = (from s in kwl.TrimStart().Split(new string[]
								{
									"\r\n",
									"\r",
									"\n"
								}, StringSplitOptions.TrimEntries)
								select s.Trim()).ToArray<string>();
								kwl = string.Join("\n" + indentation, lines);
								tw.Write(kwl);
								HjsonExtensions.NewLine(tw, level + ((showBraces > false) ? 1 : 0));
							}
							lastJsonType = new JsonType?(val.JsonType);
							kwl = HjsonExtensions.GetComments(commentedObject.Comments, key);
							LocalizationLoader.CommentedWscJsonObject commented = jObject as LocalizationLoader.CommentedWscJsonObject;
							object obj = commented != null && commented.CommentedOut.Contains(key);
							bool commentIsMultiline = false;
							object obj2 = obj;
							if (obj2 != null)
							{
								commentIsMultiline = (val.GetRawString().IndexOf('\n') != -1);
								if (commentIsMultiline)
								{
									tw.Write("/* ");
								}
								else
								{
									tw.Write("// ");
								}
							}
							tw.Write(HjsonExtensions.escapeName(key));
							tw.Write(':');
							HjsonExtensions.WriteFancyHjsonValue(tw, val, level + ((showBraces > false) ? 1 : 0), style, HjsonExtensions.TestCommentString(kwl), false, false);
							if ((obj2 & commentIsMultiline) != null)
							{
								tw.Write(" */");
								continue;
							}
							continue;
							IL_128:
							HjsonExtensions.NewLine(tw, 0);
							goto IL_12F;
						}
					}
					tw.Write(kwl);
					if (showBraces)
					{
						HjsonExtensions.NewLine(tw, level);
					}
				}
				else
				{
					bool skipFirst2 = !showBraces;
					JsonType? lastJsonType2 = null;
					foreach (KeyValuePair<string, JsonValue> pair in jObject)
					{
						JsonType jsonType4 = pair.Value.JsonType;
						JsonType? jsonType2 = lastJsonType2;
						if (!(jsonType4 == jsonType2.GetValueOrDefault() & jsonType2 != null) && lastJsonType2 != null)
						{
							goto IL_332;
						}
						jsonType2 = lastJsonType2;
						JsonType jsonType3 = 2;
						if (jsonType2.GetValueOrDefault() == jsonType3 & jsonType2 != null)
						{
							goto IL_332;
						}
						IL_339:
						lastJsonType2 = new JsonType?(pair.Value.JsonType);
						if (!skipFirst2)
						{
							HjsonExtensions.NewLine(tw, level + 1);
						}
						else
						{
							skipFirst2 = false;
						}
						tw.Write(HjsonExtensions.escapeName(pair.Key));
						tw.Write(':');
						HjsonExtensions.WriteFancyHjsonValue(tw, pair.Value, level + ((showBraces > false) ? 1 : 0), style, false, true, false);
						continue;
						IL_332:
						HjsonExtensions.NewLine(tw, 0);
						goto IL_339;
					}
					if (showBraces && jObject.Count > 0)
					{
						HjsonExtensions.NewLine(tw, level);
					}
				}
				if (showBraces)
				{
					tw.Write('}');
					return;
				}
				return;
			}
			case 3:
			{
				int i = 0;
				int j = value.Count;
				if (!style.NoIndentaion)
				{
					if (j > 0)
					{
						HjsonExtensions.NewLine(tw, level);
					}
					else
					{
						tw.Write(style.Separator);
					}
				}
				tw.Write('[');
				WscJsonArray whiteL = null;
				string wsl = null;
				if (style.WriteComments)
				{
					whiteL = (value as WscJsonArray);
					if (whiteL != null)
					{
						wsl = HjsonExtensions.GetComments(whiteL.Comments, 0);
					}
				}
				while (i < j)
				{
					JsonValue v = value[i];
					if (whiteL != null)
					{
						tw.Write(wsl);
						wsl = HjsonExtensions.GetComments(whiteL.Comments, i + 1);
					}
					HjsonExtensions.NewLine(tw, level + 1);
					HjsonExtensions.WriteFancyHjsonValue(tw, v, level + 1, style, wsl != null && HjsonExtensions.TestCommentString(wsl), false, false);
					i++;
				}
				if (whiteL != null)
				{
					tw.Write(wsl);
				}
				if (j > 0)
				{
					HjsonExtensions.NewLine(tw, level);
				}
				tw.Write(']');
				return;
			}
			case 4:
				tw.Write(style.Separator);
				tw.Write(value ? "true" : "false");
				return;
			}
			tw.Write(style.Separator);
			tw.Write(value.GetRawString());
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x0050D140 File Offset: 0x0050B340
		public static string GetRawString(this JsonValue value)
		{
			JsonType jsonType = value.JsonType;
			string result;
			if (jsonType != null)
			{
				if (jsonType != 1)
				{
					result = value.JsonType.ToString();
				}
				else
				{
					result = ((IFormattable)value).ToString("G", NumberFormatInfo.InvariantInfo).ToLowerInvariant();
				}
			}
			else
			{
				result = (value ?? "");
			}
			return result;
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x0050D1A2 File Offset: 0x0050B3A2
		private static void NewLine(TextWriter tw, int level)
		{
			tw.Write("\r\n");
			tw.Write(new string('\t', level));
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x0050D1C0 File Offset: 0x0050B3C0
		private static void WriteString(TextWriter tw, string value, int level, bool hasComment, string separator)
		{
			if (value == "")
			{
				tw.Write(separator + "\"\"");
				return;
			}
			char left = value[0];
			char right = value[value.Length - 1];
			char left2 = (value.Length > 1) ? value[1] : '\0';
			if (value.Length > 2)
			{
				char c2 = value[2];
			}
			bool flag;
			if (!hasComment)
			{
				flag = value.Any((char c) => HjsonExtensions.needsQuotes(c));
			}
			else
			{
				flag = true;
			}
			JsonValue dummy;
			if (!flag && !char.IsWhiteSpace(left) && !char.IsWhiteSpace(right) && left != '"' && left != '\'' && left != '#' && (left != '/' || (left2 != '*' && left2 != '/')) && !HjsonExtensions.isPunctuatorChar(left) && !HjsonExtensions.tryParseNumericLiteral(value, true, out dummy) && !HjsonExtensions.startsWithKeyword(value))
			{
				tw.Write(separator + value);
				return;
			}
			if (!value.Any((char c) => HjsonExtensions.needsEscape(c)))
			{
				tw.Write(separator + "\"" + value + "\"");
				return;
			}
			if (!value.Any((char c) => HjsonExtensions.needsEscapeML(c)) && !value.Contains("'''"))
			{
				if (!value.All((char c) => char.IsWhiteSpace(c)))
				{
					HjsonExtensions.WriteMultiLineString(value, tw, level, separator);
					return;
				}
			}
			tw.Write(separator + "\"" + HjsonExtensions.escapeString(value) + "\"");
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x0050D388 File Offset: 0x0050B588
		private static void WriteMultiLineString(string value, TextWriter tw, int level, string separator)
		{
			string[] lines = value.Replace("\r", "").Split('\n', StringSplitOptions.None);
			if (lines.Length == 1)
			{
				tw.Write(separator + "'''");
				tw.Write(lines[0]);
				tw.Write("'''");
				return;
			}
			level++;
			HjsonExtensions.NewLine(tw, level);
			tw.Write("'''");
			foreach (string line in lines)
			{
				HjsonExtensions.NewLine(tw, (!string.IsNullOrEmpty(line)) ? level : 0);
				tw.Write(line);
			}
			HjsonExtensions.NewLine(tw, level);
			tw.Write("'''");
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x0050D430 File Offset: 0x0050B630
		private static string GetComments(Dictionary<string, string> comments, string key)
		{
			if (!comments.ContainsKey(key))
			{
				return "";
			}
			return HjsonExtensions.GetComments(comments[key]);
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x0050D44D File Offset: 0x0050B64D
		private static string GetComments(List<string> comments, int index)
		{
			if (comments.Count <= index)
			{
				return "";
			}
			return HjsonExtensions.GetComments(comments[index]);
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x0050D46C File Offset: 0x0050B66C
		private static string GetComments(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (c == '\n' || c == '#' || (c == '/' && i + 1 < text.Length && (text[i + 1] == '/' || text[i + 1] == '*')))
				{
					break;
				}
				if (c > ' ')
				{
					return " # " + text;
				}
			}
			return text;
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x0050D4E6 File Offset: 0x0050B6E6
		private static bool TestCommentString(string text)
		{
			return text.Length > 0 && text[(text[0] == '\r' && text.Length > 1) ? 1 : 0] != '\n';
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0050D518 File Offset: 0x0050B718
		private static T GetDelegateOfMethod<T>(string type, string methodName) where T : Delegate
		{
			return typeof(HjsonValue).Assembly.GetType(type).GetMethod(methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).CreateDelegate<T>();
		}

		// Token: 0x0400194A RID: 6474
		private const string JsonWriter = "Hjson.JsonWriter";

		// Token: 0x0400194B RID: 6475
		private const string HjsonReader = "Hjson.HjsonReader";

		// Token: 0x0400194C RID: 6476
		private const string HjsonWriter = "Hjson.HjsonWriter";

		// Token: 0x0400194D RID: 6477
		private const string HjsonValue = "Hjson.HjsonValue";

		// Token: 0x0400194E RID: 6478
		public static readonly HjsonExtensions.HjsonStyle DefaultHjsonStyle = new HjsonExtensions.HjsonStyle
		{
			WriteComments = true,
			EmitRootBraces = false,
			NoIndentaion = false,
			Separator = " "
		};

		// Token: 0x0400194F RID: 6479
		private static readonly HjsonExtensions.TryParseNumericLiteralDelegate tryParseNumericLiteral = HjsonExtensions.GetDelegateOfMethod<HjsonExtensions.TryParseNumericLiteralDelegate>("Hjson.HjsonReader", "TryParseNumericLiteral");

		// Token: 0x04001950 RID: 6480
		private static readonly Func<string, string> escapeString = HjsonExtensions.GetDelegateOfMethod<Func<string, string>>("Hjson.JsonWriter", "EscapeString");

		// Token: 0x04001951 RID: 6481
		private static readonly Func<string, string> escapeName = HjsonExtensions.GetDelegateOfMethod<Func<string, string>>("Hjson.HjsonWriter", "escapeName");

		// Token: 0x04001952 RID: 6482
		private static readonly Func<string, bool> startsWithKeyword = HjsonExtensions.GetDelegateOfMethod<Func<string, bool>>("Hjson.HjsonWriter", "startsWithKeyword");

		// Token: 0x04001953 RID: 6483
		private static readonly Func<char, bool> needsEscapeML = HjsonExtensions.GetDelegateOfMethod<Func<char, bool>>("Hjson.HjsonWriter", "needsEscapeML");

		// Token: 0x04001954 RID: 6484
		private static readonly Func<char, bool> needsEscape = HjsonExtensions.GetDelegateOfMethod<Func<char, bool>>("Hjson.HjsonWriter", "needsEscape");

		// Token: 0x04001955 RID: 6485
		private static readonly Func<char, bool> needsQuotes = HjsonExtensions.GetDelegateOfMethod<Func<char, bool>>("Hjson.HjsonWriter", "needsQuotes");

		// Token: 0x04001956 RID: 6486
		private static readonly Func<char, bool> isPunctuatorChar = HjsonExtensions.GetDelegateOfMethod<Func<char, bool>>("Hjson.HjsonValue", "IsPunctuatorChar");

		// Token: 0x020009E8 RID: 2536
		public struct HjsonStyle
		{
			// Token: 0x04006BCF RID: 27599
			public bool WriteComments;

			// Token: 0x04006BD0 RID: 27600
			public bool EmitRootBraces;

			// Token: 0x04006BD1 RID: 27601
			public bool NoIndentaion;

			// Token: 0x04006BD2 RID: 27602
			public string Separator;
		}

		// Token: 0x020009E9 RID: 2537
		// (Invoke) Token: 0x060056AE RID: 22190
		private delegate bool TryParseNumericLiteralDelegate(string text, bool stopAtNext, out JsonValue value);
	}
}
