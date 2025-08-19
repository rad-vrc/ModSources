using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Allows for implementing types to be loaded and unloaded.
	/// </summary>
	// Token: 0x0200017B RID: 379
	public interface ILoadable
	{
		/// <summary>
		/// Called when loading the type.
		/// </summary>
		/// <param name="mod">The mod instance associated with this type.</param>
		// Token: 0x06001DF6 RID: 7670
		void Load(Mod mod);

		/// <summary>
		/// Whether or not this type should be loaded when it's told to. Returning false disables <see cref="M:Terraria.ModLoader.Mod.AddContent(Terraria.ModLoader.ILoadable)" /> from actually loading this type.
		/// </summary>
		/// <param name="mod">The mod instance trying to add this content</param>
		// Token: 0x06001DF7 RID: 7671 RVA: 0x004D4E64 File Offset: 0x004D3064
		bool IsLoadingEnabled(Mod mod)
		{
			return true;
		}

		/// <summary>
		/// Called during unloading when needed.
		/// </summary>
		// Token: 0x06001DF8 RID: 7672
		void Unload();
	}
}
