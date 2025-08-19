using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000289 RID: 649
	public class TagPrinter
	{
		// Token: 0x06002C42 RID: 11330 RVA: 0x00526EDF File Offset: 0x005250DF
		public override string ToString()
		{
			return this.sb.ToString();
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x00526EEC File Offset: 0x005250EC
		private string TypeString(Type type)
		{
			if (type == typeof(byte))
			{
				return "byte";
			}
			if (type == typeof(short))
			{
				return "short";
			}
			if (type == typeof(int))
			{
				return "int";
			}
			if (type == typeof(long))
			{
				return "long";
			}
			if (type == typeof(float))
			{
				return "float";
			}
			if (type == typeof(double))
			{
				return "double";
			}
			if (type == typeof(string))
			{
				return "string";
			}
			if (type == typeof(byte[]))
			{
				return "byte[]";
			}
			if (type == typeof(int[]))
			{
				return "int[]";
			}
			if (type == typeof(TagCompound))
			{
				return "object";
			}
			if (type == typeof(IList))
			{
				return "list";
			}
			throw new ArgumentException("Unknown Type: " + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x00527020 File Offset: 0x00525220
		private void WriteList<T>(char start, char end, bool multiline, IEnumerable<T> list, Action<T> write)
		{
			this.sb.Append(start);
			this.indent += "  ";
			bool first = true;
			foreach (T entry in list)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					this.sb.Append(multiline ? "," : ", ");
				}
				if (multiline)
				{
					this.sb.AppendLine().Append(this.indent);
				}
				write(entry);
			}
			this.indent = this.indent.Substring(2);
			if (multiline && !first)
			{
				this.sb.AppendLine().Append(this.indent);
			}
			this.sb.Append(end);
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x00527108 File Offset: 0x00525308
		private void WriteEntry(KeyValuePair<string, object> entry)
		{
			if (entry.Value == null)
			{
				this.sb.Append('"').Append(entry.Key).Append("\" = null");
				return;
			}
			Type type = entry.Value.GetType();
			bool isList = entry.Value is IList && !(entry.Value is Array);
			this.sb.Append(this.TypeString(isList ? type.GetGenericArguments()[0] : type));
			this.sb.Append(" \"").Append(entry.Key).Append("\" ");
			if (type != typeof(TagCompound) && !isList)
			{
				this.sb.Append("= ");
			}
			this.WriteValue(entry.Value);
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x005271F0 File Offset: 0x005253F0
		private void WriteValue(object elem)
		{
			if (elem is byte)
			{
				this.sb.Append((byte)elem);
				return;
			}
			if (elem is short)
			{
				this.sb.Append((short)elem);
				return;
			}
			if (elem is int)
			{
				this.sb.Append((int)elem);
				return;
			}
			if (elem is long)
			{
				this.sb.Append((long)elem);
				return;
			}
			if (elem is float)
			{
				this.sb.Append((float)elem);
				return;
			}
			if (elem is double)
			{
				this.sb.Append((double)elem);
				return;
			}
			if (elem is string)
			{
				this.sb.Append('"').Append((string)elem).Append('"');
				return;
			}
			if (elem is byte[])
			{
				this.sb.Append('[').Append(string.Join<byte>(", ", (byte[])elem)).Append(']');
				return;
			}
			if (elem is int[])
			{
				this.sb.Append('[').Append(string.Join<int>(", ", (int[])elem)).Append(']');
				return;
			}
			if (elem is TagCompound)
			{
				TagCompound tag = (TagCompound)elem;
				this.WriteList<KeyValuePair<string, object>>('{', '}', true, tag, new Action<KeyValuePair<string, object>>(this.WriteEntry));
				return;
			}
			if (elem is IList)
			{
				Type type = elem.GetType().GetGenericArguments()[0];
				this.WriteList<object>('[', ']', type == typeof(string) || type == typeof(TagCompound) || typeof(IList).IsAssignableFrom(type), ((IList)elem).Cast<object>(), delegate(object o)
				{
					if (type == typeof(IList))
					{
						this.sb.Append(this.TypeString(o.GetType().GetGenericArguments()[0])).Append(' ');
					}
					this.WriteValue(o);
				});
			}
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x005273E5 File Offset: 0x005255E5
		public static string Print(TagCompound tag)
		{
			TagPrinter tagPrinter = new TagPrinter();
			tagPrinter.WriteValue(tag);
			return tagPrinter.ToString();
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x005273F8 File Offset: 0x005255F8
		public static string Print(KeyValuePair<string, object> entry)
		{
			TagPrinter tagPrinter = new TagPrinter();
			tagPrinter.WriteEntry(entry);
			return tagPrinter.ToString();
		}

		// Token: 0x04001C00 RID: 7168
		private string indent = "";

		// Token: 0x04001C01 RID: 7169
		private StringBuilder sb = new StringBuilder();
	}
}
