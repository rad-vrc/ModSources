using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003BF RID: 959
	internal class TileDefinitionElement : DefinitionElement<TileDefinition>
	{
		// Token: 0x060032E1 RID: 13025 RVA: 0x00545CED File Offset: 0x00543EED
		protected override DefinitionOptionElement<TileDefinition> CreateDefinitionOptionElement()
		{
			return new TileDefinitionOptionElement(this.Value, 0.5f);
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x00545D00 File Offset: 0x00543F00
		protected override List<DefinitionOptionElement<TileDefinition>> CreateDefinitionOptionElementList()
		{
			List<DefinitionOptionElement<TileDefinition>> options = new List<DefinitionOptionElement<TileDefinition>>();
			for (int i = -1; i < TileLoader.TileCount; i++)
			{
				TileDefinitionOptionElement optionElement = new TileDefinitionOptionElement((i == -1) ? new TileDefinition() : new TileDefinition(i), base.OptionScale);
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

		// Token: 0x060032E3 RID: 13027 RVA: 0x00545D78 File Offset: 0x00543F78
		protected override List<DefinitionOptionElement<TileDefinition>> GetPassedOptionElements()
		{
			List<DefinitionOptionElement<TileDefinition>> passed = new List<DefinitionOptionElement<TileDefinition>>();
			foreach (DefinitionOptionElement<TileDefinition> option in base.Options)
			{
				TileDefinition definition = option.Definition;
				if ((((definition != null) ? definition.DisplayName : null) ?? "").Contains(base.ChooserFilter.CurrentString, StringComparison.OrdinalIgnoreCase))
				{
					string modname = "Terraria";
					if (option.Type >= (int)TileID.Count)
					{
						modname = TileLoader.GetTile(option.Type).Mod.DisplayNameClean;
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
