using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.UI
{
	// Token: 0x02000084 RID: 132
	public class ItemTooltip
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0049088C File Offset: 0x0048EA8C
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

		// Token: 0x060011EE RID: 4590 RVA: 0x0000B904 File Offset: 0x00009B04
		private ItemTooltip()
		{
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x004908A6 File Offset: 0x0048EAA6
		private ItemTooltip(string key)
		{
			this._text = Language.GetText(key);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x004908BA File Offset: 0x0048EABA
		public static ItemTooltip FromLanguageKey(string key)
		{
			if (!Language.Exists(key))
			{
				return ItemTooltip.None;
			}
			return new ItemTooltip(key);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x004908D0 File Offset: 0x0048EAD0
		public string GetLine(int line)
		{
			this.ValidateTooltip();
			return this._tooltipLines[line];
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x004908E0 File Offset: 0x0048EAE0
		private void ValidateTooltip()
		{
			if (this._validatorKey != ItemTooltip._globalValidatorKey)
			{
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
				this._tooltipLines = text.Split(new char[]
				{
					'\n'
				});
				this._processedText = text;
			}
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00490994 File Offset: 0x0048EB94
		public static void AddGlobalProcessor(TooltipProcessor processor)
		{
			ItemTooltip._globalProcessors.Add(processor);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x004909A1 File Offset: 0x0048EBA1
		public static void RemoveGlobalProcessor(TooltipProcessor processor)
		{
			ItemTooltip._globalProcessors.Remove(processor);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x004909AF File Offset: 0x0048EBAF
		public static void ClearGlobalProcessors()
		{
			ItemTooltip._globalProcessors.Clear();
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x004909BB File Offset: 0x0048EBBB
		public static void InvalidateTooltips()
		{
			ItemTooltip._globalValidatorKey += 1UL;
			if (ItemTooltip._globalValidatorKey == 18446744073709551615UL)
			{
				ItemTooltip._globalValidatorKey = 0UL;
			}
		}

		// Token: 0x04000FED RID: 4077
		public static readonly ItemTooltip None = new ItemTooltip();

		// Token: 0x04000FEE RID: 4078
		private static readonly List<TooltipProcessor> _globalProcessors = new List<TooltipProcessor>();

		// Token: 0x04000FEF RID: 4079
		private static ulong _globalValidatorKey = 1UL;

		// Token: 0x04000FF0 RID: 4080
		private string[] _tooltipLines;

		// Token: 0x04000FF1 RID: 4081
		private ulong _validatorKey;

		// Token: 0x04000FF2 RID: 4082
		private readonly LocalizedText _text;

		// Token: 0x04000FF3 RID: 4083
		private string _processedText;
	}
}
