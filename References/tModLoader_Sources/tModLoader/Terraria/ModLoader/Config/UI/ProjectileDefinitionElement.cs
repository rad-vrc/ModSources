using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003B5 RID: 949
	internal class ProjectileDefinitionElement : DefinitionElement<ProjectileDefinition>
	{
		// Token: 0x06003291 RID: 12945 RVA: 0x005449A6 File Offset: 0x00542BA6
		protected override DefinitionOptionElement<ProjectileDefinition> CreateDefinitionOptionElement()
		{
			return new ProjectileDefinitionOptionElement(this.Value, 0.5f);
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x005449B8 File Offset: 0x00542BB8
		protected override List<DefinitionOptionElement<ProjectileDefinition>> CreateDefinitionOptionElementList()
		{
			List<DefinitionOptionElement<ProjectileDefinition>> options = new List<DefinitionOptionElement<ProjectileDefinition>>();
			for (int i = 0; i < ProjectileLoader.ProjectileCount; i++)
			{
				ProjectileDefinitionOptionElement optionElement = new ProjectileDefinitionOptionElement(new ProjectileDefinition(i), base.OptionScale);
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

		// Token: 0x06003293 RID: 12947 RVA: 0x00544A24 File Offset: 0x00542C24
		protected override List<DefinitionOptionElement<ProjectileDefinition>> GetPassedOptionElements()
		{
			List<DefinitionOptionElement<ProjectileDefinition>> passed = new List<DefinitionOptionElement<ProjectileDefinition>>();
			foreach (DefinitionOptionElement<ProjectileDefinition> option in base.Options)
			{
				if (Lang.GetProjectileName(option.Type).Value.IndexOf(base.ChooserFilter.CurrentString, StringComparison.OrdinalIgnoreCase) != -1)
				{
					string modname = option.Definition.Mod;
					if (option.Type >= (int)ProjectileID.Count)
					{
						modname = ProjectileLoader.GetProjectile(option.Type).Mod.DisplayNameClean;
					}
					if (modname.Contains(base.ChooserFilterMod.CurrentString, StringComparison.OrdinalIgnoreCase))
					{
						passed.Add(option);
					}
				}
			}
			return passed;
		}
	}
}
