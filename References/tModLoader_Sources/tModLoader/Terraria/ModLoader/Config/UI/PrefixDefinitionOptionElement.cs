using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003B4 RID: 948
	internal class PrefixDefinitionOptionElement : DefinitionOptionElement<PrefixDefinition>
	{
		// Token: 0x0600328D RID: 12941 RVA: 0x005448B0 File Offset: 0x00542AB0
		public PrefixDefinitionOptionElement(PrefixDefinition definition, float scale = 0.75f) : base(definition, scale)
		{
			this.Width.Set(150f * scale, 0f);
			this.Height.Set(40f * scale, 0f);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = new UIAutoScaleTextTextPanel<string>(base.Definition.DisplayName, 1f, false);
			uiautoScaleTextTextPanel.Width.Percent = 1f;
			uiautoScaleTextTextPanel.Height.Percent = 1f;
			this.text = uiautoScaleTextTextPanel;
			base.Append(this.text);
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x0054493B File Offset: 0x00542B3B
		public override void SetItem(PrefixDefinition item)
		{
			base.SetItem(item);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = this.text;
			if (uiautoScaleTextTextPanel == null)
			{
				return;
			}
			uiautoScaleTextTextPanel.SetText(item.DisplayName);
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x0054495A File Offset: 0x00542B5A
		public override void SetScale(float scale)
		{
			base.SetScale(scale);
			this.Width.Set(150f * scale, 0f);
			this.Height.Set(40f * scale, 0f);
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x00544991 File Offset: 0x00542B91
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (base.IsMouseHovering)
			{
				UIModConfig.Tooltip = base.Tooltip;
			}
		}

		// Token: 0x04001DAF RID: 7599
		private readonly UIAutoScaleTextTextPanel<string> text;
	}
}
