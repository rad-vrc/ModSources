using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003A9 RID: 937
	internal class ItemDefinitionElement : DefinitionElement<ItemDefinition>
	{
		// Token: 0x06003257 RID: 12887 RVA: 0x00543166 File Offset: 0x00541366
		protected override DefinitionOptionElement<ItemDefinition> CreateDefinitionOptionElement()
		{
			return new ItemDefinitionOptionElement(this.Value, 0.5f);
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x00543178 File Offset: 0x00541378
		protected override List<DefinitionOptionElement<ItemDefinition>> CreateDefinitionOptionElementList()
		{
			List<DefinitionOptionElement<ItemDefinition>> options = new List<DefinitionOptionElement<ItemDefinition>>();
			for (int i = 0; i < ItemLoader.ItemCount; i++)
			{
				ItemDefinitionOptionElement optionElement = new ItemDefinitionOptionElement(new ItemDefinition(i), base.OptionScale);
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

		// Token: 0x06003259 RID: 12889 RVA: 0x005431E4 File Offset: 0x005413E4
		protected override List<DefinitionOptionElement<ItemDefinition>> GetPassedOptionElements()
		{
			List<DefinitionOptionElement<ItemDefinition>> passed = new List<DefinitionOptionElement<ItemDefinition>>();
			foreach (DefinitionOptionElement<ItemDefinition> option in base.Options)
			{
				if (!ItemID.Sets.Deprecated[option.Type] && Lang.GetItemNameValue(option.Type).Contains(base.ChooserFilter.CurrentString, StringComparison.OrdinalIgnoreCase))
				{
					string modname = "Terraria";
					if (option.Type >= (int)ItemID.Count)
					{
						modname = ItemLoader.GetItem(option.Type).Mod.DisplayNameClean;
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
