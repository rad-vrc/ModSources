using System;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x0200020F RID: 527
	[Autoload(false)]
	public abstract class VanillaDamageClass : DamageClass
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x0050985B File Offset: 0x00507A5B
		public override LocalizedText DisplayName
		{
			get
			{
				return Language.GetText(this.LangKey);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06002803 RID: 10243
		protected abstract string LangKey { get; }
	}
}
