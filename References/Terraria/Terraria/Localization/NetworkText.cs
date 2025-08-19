using System;
using System.IO;
using System.Text;

namespace Terraria.Localization
{
	// Token: 0x020000AF RID: 175
	public class NetworkText
	{
		// Token: 0x060013D6 RID: 5078 RVA: 0x004A0EB2 File Offset: 0x0049F0B2
		private NetworkText(string text, NetworkText.Mode mode)
		{
			this._text = text;
			this._mode = mode;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x004A0EC8 File Offset: 0x0049F0C8
		private static NetworkText[] ConvertSubstitutionsToNetworkText(object[] substitutions)
		{
			NetworkText[] array = new NetworkText[substitutions.Length];
			for (int i = 0; i < substitutions.Length; i++)
			{
				NetworkText networkText = substitutions[i] as NetworkText;
				if (networkText == null)
				{
					networkText = NetworkText.FromLiteral(substitutions[i].ToString());
				}
				array[i] = networkText;
			}
			return array;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x004A0F0B File Offset: 0x0049F10B
		public static NetworkText FromFormattable(string text, params object[] substitutions)
		{
			return new NetworkText(text, NetworkText.Mode.Formattable)
			{
				_substitutions = NetworkText.ConvertSubstitutionsToNetworkText(substitutions)
			};
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x004A0F20 File Offset: 0x0049F120
		public static NetworkText FromLiteral(string text)
		{
			return new NetworkText(text, NetworkText.Mode.Literal);
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x004A0F29 File Offset: 0x0049F129
		public static NetworkText FromKey(string key, params object[] substitutions)
		{
			return new NetworkText(key, NetworkText.Mode.LocalizationKey)
			{
				_substitutions = NetworkText.ConvertSubstitutionsToNetworkText(substitutions)
			};
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x004A0F40 File Offset: 0x0049F140
		public int GetMaxSerializedSize()
		{
			int num = 0;
			num++;
			num += 4 + Encoding.UTF8.GetByteCount(this._text);
			if (this._mode != NetworkText.Mode.Literal)
			{
				num++;
				for (int i = 0; i < this._substitutions.Length; i++)
				{
					num += this._substitutions[i].GetMaxSerializedSize();
				}
			}
			return num;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x004A0F98 File Offset: 0x0049F198
		public void Serialize(BinaryWriter writer)
		{
			writer.Write((byte)this._mode);
			writer.Write(this._text);
			this.SerializeSubstitutionList(writer);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x004A0FBC File Offset: 0x0049F1BC
		private void SerializeSubstitutionList(BinaryWriter writer)
		{
			if (this._mode == NetworkText.Mode.Literal)
			{
				return;
			}
			writer.Write((byte)this._substitutions.Length);
			for (int i = 0; i < (this._substitutions.Length & 255); i++)
			{
				this._substitutions[i].Serialize(writer);
			}
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x004A1008 File Offset: 0x0049F208
		public static NetworkText Deserialize(BinaryReader reader)
		{
			NetworkText.Mode mode = (NetworkText.Mode)reader.ReadByte();
			NetworkText networkText = new NetworkText(reader.ReadString(), mode);
			networkText.DeserializeSubstitutionList(reader);
			return networkText;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x004A1030 File Offset: 0x0049F230
		public static NetworkText DeserializeLiteral(BinaryReader reader)
		{
			NetworkText.Mode mode = (NetworkText.Mode)reader.ReadByte();
			NetworkText networkText = new NetworkText(reader.ReadString(), mode);
			networkText.DeserializeSubstitutionList(reader);
			if (mode != NetworkText.Mode.Literal)
			{
				networkText.SetToEmptyLiteral();
			}
			return networkText;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x004A1064 File Offset: 0x0049F264
		private void DeserializeSubstitutionList(BinaryReader reader)
		{
			if (this._mode == NetworkText.Mode.Literal)
			{
				return;
			}
			this._substitutions = new NetworkText[(int)reader.ReadByte()];
			for (int i = 0; i < this._substitutions.Length; i++)
			{
				this._substitutions[i] = NetworkText.Deserialize(reader);
			}
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x004A10AC File Offset: 0x0049F2AC
		private void SetToEmptyLiteral()
		{
			this._mode = NetworkText.Mode.Literal;
			this._text = string.Empty;
			this._substitutions = null;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x004A10C8 File Offset: 0x0049F2C8
		public override string ToString()
		{
			try
			{
				switch (this._mode)
				{
				case NetworkText.Mode.Literal:
					return this._text;
				case NetworkText.Mode.Formattable:
				{
					string text = this._text;
					object[] substitutions = this._substitutions;
					return string.Format(text, substitutions);
				}
				case NetworkText.Mode.LocalizationKey:
				{
					string text2 = this._text;
					object[] substitutions = this._substitutions;
					return Language.GetTextValue(text2, substitutions);
				}
				default:
					return this._text;
				}
			}
			catch (Exception arg)
			{
				"NetworkText.ToString() threw an exception.\n" + this.ToDebugInfoString("") + "\n" + "Exception: " + arg;
				this.SetToEmptyLiteral();
			}
			return this._text;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x004A117C File Offset: 0x0049F37C
		private string ToDebugInfoString(string linePrefix = "")
		{
			string text = string.Format("{0}Mode: {1}\n{0}Text: {2}\n", linePrefix, this._mode, this._text);
			if (this._mode == NetworkText.Mode.LocalizationKey)
			{
				text += string.Format("{0}Localized Text: {1}\n", linePrefix, Language.GetTextValue(this._text));
			}
			if (this._mode != NetworkText.Mode.Literal)
			{
				for (int i = 0; i < this._substitutions.Length; i++)
				{
					text += string.Format("{0}Substitution {1}:\n", linePrefix, i);
					text += this._substitutions[i].ToDebugInfoString(linePrefix + "\t");
				}
			}
			return text;
		}

		// Token: 0x040011C1 RID: 4545
		public static readonly NetworkText Empty = NetworkText.FromLiteral("");

		// Token: 0x040011C2 RID: 4546
		private NetworkText[] _substitutions;

		// Token: 0x040011C3 RID: 4547
		private string _text;

		// Token: 0x040011C4 RID: 4548
		private NetworkText.Mode _mode;

		// Token: 0x02000557 RID: 1367
		private enum Mode : byte
		{
			// Token: 0x040058DE RID: 22750
			Literal,
			// Token: 0x040058DF RID: 22751
			Formattable,
			// Token: 0x040058E0 RID: 22752
			LocalizationKey
		}
	}
}
