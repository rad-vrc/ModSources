using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as the central class from which SceneEffect functions are supported and carried out.
	/// </summary>
	// Token: 0x020001F6 RID: 502
	public abstract class SceneEffectLoader<T> : Loader<T> where T : ModType
	{
		// Token: 0x06002713 RID: 10003 RVA: 0x00501EE0 File Offset: 0x005000E0
		public virtual void ChooseStyle(out int style, out SceneEffectPriority priority)
		{
			style = -1;
			priority = SceneEffectPriority.None;
		}
	}
}
