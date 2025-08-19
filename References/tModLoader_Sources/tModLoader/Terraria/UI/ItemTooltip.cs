using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.UI
{
	// Token: 0x020000AC RID: 172
	public class ItemTooltip
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x004AF984 File Offset: 0x004ADB84
		public int Lines
		{
			get
			{
				this.ValidateTooltip();
				if (this._tooltipLines == null)
				{
					return 0;
				}
				return this._tooltipLines.Length;
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x004AF99E File Offset: 0x004ADB9E
		private ItemTooltip()
		{
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x004AF9A6 File Offset: 0x004ADBA6
		private ItemTooltip(string key)
		{
			this._text = Language.GetText(key);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x004AF9BA File Offset: 0x004ADBBA
		private ItemTooltip(LocalizedText text)
		{
			this._text = text;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x004AF9C9 File Offset: 0x004ADBC9
		public static ItemTooltip FromLanguageKey(string key)
		{
			if (!Language.Exists(key))
			{
				return ItemTooltip.None;
			}
			return new ItemTooltip(key);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x004AF9DF File Offset: 0x004ADBDF
		public static ItemTooltip FromLocalization(LocalizedText text)
		{
			return new ItemTooltip(text);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x004AF9E7 File Offset: 0x004ADBE7
		public string GetLine(int line)
		{
			this.ValidateTooltip();
			return this._tooltipLines[line];
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x004AF9F8 File Offset: 0x004ADBF8
		private void ValidateTooltip()
		{
			if (this._validatorKey == ItemTooltip._globalValidatorKey)
			{
				return;
			}
			this._validatorKey = ItemTooltip._globalValidatorKey;
			if (this._text == null)
			{
				this._tooltipLines = null;
				this._processedText = string.Empty;
				return;
			}
			string text = this._text.Value;
			foreach (TooltipProcessor tooltipProcessor in ItemTooltip._globalProcessors)
			{
				text = tooltipProcessor(text);
			}
			this._tooltipLines = text.Split('\n', StringSplitOptions.None);
			this._processedText = text;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x004AFAA0 File Offset: 0x004ADCA0
		public static void AddGlobalProcessor(TooltipProcessor processor)
		{
			ItemTooltip._globalProcessors.Add(processor);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x004AFAAD File Offset: 0x004ADCAD
		public static void RemoveGlobalProcessor(TooltipProcessor processor)
		{
			ItemTooltip._globalProcessors.Remove(processor);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x004AFABB File Offset: 0x004ADCBB
		public static void ClearGlobalProcessors()
		{
			ItemTooltip._globalProcessors.Clear();
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x004AFAC7 File Offset: 0x004ADCC7
		public static void InvalidateTooltips()
		{
			ItemTooltip._globalValidatorKey += 1UL;
			if (ItemTooltip._globalValidatorKey == 18446744073709551615UL)
			{
				ItemTooltip._globalValidatorKey = 0UL;
			}
		}

		// Token: 0x040010FC RID: 4348
		public static readonly ItemTooltip None = new ItemTooltip();

		// Token: 0x040010FD RID: 4349
		private static readonly List<TooltipProcessor> _globalProcessors = new List<TooltipProcessor>();

		// Token: 0x040010FE RID: 4350
		private static ulong _globalValidatorKey = 1UL;

		// Token: 0x040010FF RID: 4351
		private string[] _tooltipLines;

		// Token: 0x04001100 RID: 4352
		private ulong _validatorKey;

		// Token: 0x04001101 RID: 4353
		private readonly LocalizedText _text;

		// Token: 0x04001102 RID: 4354
		private string _processedText;
	}
}
