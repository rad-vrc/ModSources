using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Classes annotated with this attribute will have their static constructor called again during the ResizeArrays stage of mod loading.
	/// <para /> This will happen before <see cref="M:Terraria.ModLoader.ModSystem.ResizeArrays" /> is called for any mod.
	/// <para /> This is intended for classes containing ID sets created through <see cref="T:Terraria.ID.SetFactory" />, similar to the design of vanilla classes such as <see cref="T:Terraria.ID.ItemID.Sets" />. This attribute removes the need to manually initialize these ID sets in <see cref="M:Terraria.ModLoader.ModSystem.ResizeArrays" /> and helps avoid mod ordering issues that would complicate the implementation logic.
	/// <para /> This will not work on generic classes.
	/// </summary>
	// Token: 0x020001F2 RID: 498
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ReinitializeDuringResizeArraysAttribute : Attribute
	{
	}
}
