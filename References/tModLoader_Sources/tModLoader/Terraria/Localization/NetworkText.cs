using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Terraria.Localization
{
	/// <summary>
	/// Represents text that will be sent over the network in multiplayer and displayed to the receiving user in their selected language. <para />
	/// Use <see cref="M:Terraria.Localization.NetworkText.FromKey(System.String,System.Object[])" /> to send a localization key and optional substitutions. <see cref="M:Terraria.Localization.LocalizedText.ToNetworkText" /> can be used directly as well for the same effect.<para />
	/// Use <see cref="M:Terraria.Localization.NetworkText.FromFormattable(System.String,System.Object[])" /> to send a string with string formatting substitutions and associated substitutions. This is typically used with language-agnostic strings that don't need a localization entry, such as "{0} - {1}".<para />
	/// Use <see cref="M:Terraria.Localization.NetworkText.FromLiteral(System.String)" /> to send a string directly. This should be used to send text that can't be localized.
	/// </summary>
	// Token: 0x020003DA RID: 986
	public class NetworkText
	{
		// Token: 0x060033D6 RID: 13270 RVA: 0x00555B6A File Offset: 0x00553D6A
		private NetworkText(string text, NetworkText.Mode mode)
		{
			this._text = text;
			this._mode = mode;
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x00555B80 File Offset: 0x00553D80
		private static NetworkText[] ConvertSubstitutionsToNetworkText(object[] substitutions)
		{
			NetworkText[] array = new NetworkText[substitutions.Length];
			for (int i = 0; i < substitutions.Length; i++)
			{
				array[i] = NetworkText.From(substitutions[i]);
			}
			return array;
		}

		/// <summary>
		/// Creates a NetworkText object from a string with string formatting substitutions and associated substitutions. This is typically used with language-agnostic strings that don't need a localization entry, such as "{0} - {1}".
		/// </summary>
		/// <param name="text"></param>
		/// <param name="substitutions"></param>
		/// <returns></returns>
		// Token: 0x060033D8 RID: 13272 RVA: 0x00555BB0 File Offset: 0x00553DB0
		public static NetworkText FromFormattable(string text, params object[] substitutions)
		{
			return new NetworkText(text, NetworkText.Mode.Formattable)
			{
				_substitutions = NetworkText.ConvertSubstitutionsToNetworkText(substitutions)
			};
		}

		/// <summary>
		/// Creates a NetworkText object from a string. Use this for text that can't be localized.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		// Token: 0x060033D9 RID: 13273 RVA: 0x00555BC5 File Offset: 0x00553DC5
		public static NetworkText FromLiteral(string text)
		{
			return new NetworkText(text, NetworkText.Mode.Literal);
		}

		/// <summary>
		/// Creates a NetworkText object from a localization key and optional substitutions. The receiving client will see the resulting text in their selected language.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="substitutions"></param>
		/// <returns></returns>
		// Token: 0x060033DA RID: 13274 RVA: 0x00555BCE File Offset: 0x00553DCE
		public static NetworkText FromKey(string key, params object[] substitutions)
		{
			return new NetworkText(key, NetworkText.Mode.LocalizationKey)
			{
				_substitutions = NetworkText.ConvertSubstitutionsToNetworkText(substitutions)
			};
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x00555BE4 File Offset: 0x00553DE4
		public static NetworkText From(object o)
		{
			NetworkText networkText = o as NetworkText;
			NetworkText result;
			if (networkText == null)
			{
				LocalizedText localizedText = o as LocalizedText;
				if (localizedText == null)
				{
					result = NetworkText.FromLiteral(o.ToString());
				}
				else
				{
					result = localizedText.ToNetworkText();
				}
			}
			else
			{
				result = networkText;
			}
			return result;
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x00555C24 File Offset: 0x00553E24
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

		// Token: 0x060033DD RID: 13277 RVA: 0x00555C7C File Offset: 0x00553E7C
		public void Serialize(BinaryWriter writer)
		{
			writer.Write((byte)this._mode);
			writer.Write(this._text);
			this.SerializeSubstitutionList(writer);
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x00555CA0 File Offset: 0x00553EA0
		private void SerializeSubstitutionList(BinaryWriter writer)
		{
			if (this._mode != NetworkText.Mode.Literal)
			{
				writer.Write((byte)this._substitutions.Length);
				for (int i = 0; i < (this._substitutions.Length & 255); i++)
				{
					this._substitutions[i].Serialize(writer);
				}
			}
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x00555CEC File Offset: 0x00553EEC
		public static NetworkText Deserialize(BinaryReader reader)
		{
			NetworkText.Mode mode = (NetworkText.Mode)reader.ReadByte();
			NetworkText networkText = new NetworkText(reader.ReadString(), mode);
			networkText.DeserializeSubstitutionList(reader);
			return networkText;
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x00555D14 File Offset: 0x00553F14
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

		// Token: 0x060033E1 RID: 13281 RVA: 0x00555D48 File Offset: 0x00553F48
		private void DeserializeSubstitutionList(BinaryReader reader)
		{
			if (this._mode != NetworkText.Mode.Literal)
			{
				this._substitutions = new NetworkText[(int)reader.ReadByte()];
				for (int i = 0; i < this._substitutions.Length; i++)
				{
					this._substitutions[i] = NetworkText.Deserialize(reader);
				}
			}
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x00555D8F File Offset: 0x00553F8F
		private void SetToEmptyLiteral()
		{
			this._mode = NetworkText.Mode.Literal;
			this._text = string.Empty;
			this._substitutions = null;
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x00555DAC File Offset: 0x00553FAC
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
					object[] substitutions3 = this._substitutions;
					object[] substitutions = substitutions3;
					return string.Format(text, substitutions);
				}
				case NetworkText.Mode.LocalizationKey:
				{
					string text2 = this._text;
					object[] substitutions3 = this._substitutions;
					object[] substitutions2 = substitutions3;
					return Language.GetTextValue(text2, substitutions2);
				}
				default:
					return this._text;
				}
			}
			catch (Exception ex)
			{
				"NetworkText.ToString() threw an exception.\n" + this.ToDebugInfoString("") + "\n" + "Exception: " + ex;
				this.SetToEmptyLiteral();
			}
			return this._text;
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x00555E68 File Offset: 0x00554068
		private string ToDebugInfoString(string linePrefix = "")
		{
			string text = string.Format("{0}Mode: {1}\n{0}Text: {2}\n", linePrefix, this._mode, this._text);
			if (this._mode == NetworkText.Mode.LocalizationKey)
			{
				text = string.Concat(new string[]
				{
					text,
					linePrefix,
					"Localized Text: ",
					Language.GetTextValue(this._text),
					"\n"
				});
			}
			if (this._mode != NetworkText.Mode.Literal)
			{
				for (int i = 0; i < this._substitutions.Length; i++)
				{
					string str = text;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
					defaultInterpolatedStringHandler.AppendFormatted(linePrefix);
					defaultInterpolatedStringHandler.AppendLiteral("Substitution ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(i);
					defaultInterpolatedStringHandler.AppendLiteral(":\n");
					text = str + defaultInterpolatedStringHandler.ToStringAndClear();
					text += this._substitutions[i].ToDebugInfoString(linePrefix + "\t");
				}
			}
			return text;
		}

		// Token: 0x04001E5A RID: 7770
		public static readonly NetworkText Empty = NetworkText.FromLiteral("");

		// Token: 0x04001E5B RID: 7771
		private NetworkText[] _substitutions;

		// Token: 0x04001E5C RID: 7772
		private string _text;

		// Token: 0x04001E5D RID: 7773
		private NetworkText.Mode _mode;

		// Token: 0x02000B22 RID: 2850
		private enum Mode : byte
		{
			// Token: 0x04006F17 RID: 28439
			Literal,
			// Token: 0x04006F18 RID: 28440
			Formattable,
			// Token: 0x04006F19 RID: 28441
			LocalizationKey
		}
	}
}
