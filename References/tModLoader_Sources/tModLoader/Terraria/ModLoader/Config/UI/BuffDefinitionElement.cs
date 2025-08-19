using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x02000395 RID: 917
	internal class BuffDefinitionElement : DefinitionElement<BuffDefinition>
	{
		// Token: 0x0600316E RID: 12654 RVA: 0x0053FA9E File Offset: 0x0053DC9E
		protected override DefinitionOptionElement<BuffDefinition> CreateDefinitionOptionElement()
		{
			return new BuffDefinitionOptionElement(this.Value, 0.5f);
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x0053FAB0 File Offset: 0x0053DCB0
		protected override List<DefinitionOptionElement<BuffDefinition>> CreateDefinitionOptionElementList()
		{
			List<DefinitionOptionElement<BuffDefinition>> options = new List<DefinitionOptionElement<BuffDefinition>>();
			for (int i = 0; i < BuffLoader.BuffCount; i++)
			{
				BuffDefinition buffDefinition = (i == 0) ? new BuffDefinition() : new BuffDefinition(i);
				BuffDefinitionOptionElement optionElement = new BuffDefinitionOptionElement(buffDefinition, base.OptionScale);
				optionElement.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					this.Value = optionElement.Definition;
					this.UpdateNeeded = true;
					this.SelectionExpanded = false;
				};
				options.Add(optionElement);
			}
			return options;
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x0053FB28 File Offset: 0x0053DD28
		protected override List<DefinitionOptionElement<BuffDefinition>> GetPassedOptionElements()
		{
			List<DefinitionOptionElement<BuffDefinition>> passed = new List<DefinitionOptionElement<BuffDefinition>>();
			foreach (DefinitionOptionElement<BuffDefinition> option in base.Options)
			{
				if (Lang.GetBuffName(option.Type).Contains(base.ChooserFilter.CurrentString, StringComparison.OrdinalIgnoreCase))
				{
					string modname = "Terraria";
					if (option.Type >= BuffID.Count)
					{
						modname = BuffLoader.GetBuff(option.Type).Mod.DisplayNameClean;
					}
					if (modname.IndexOf(base.ChooserFilterMod.CurrentString, StringComparison.OrdinalIgnoreCase) != -1)
					{
						passed.Add(option);
					}
				}
			}
			return passed;
		}
	}
}
