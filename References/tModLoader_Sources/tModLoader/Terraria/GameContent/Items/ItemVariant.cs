using System;
using Terraria.Localization;

namespace Terraria.GameContent.Items
{
	/// <summary>
	/// Describes a variant of an <see cref="T:Terraria.Item" />.
	/// </summary>
	// Token: 0x020005EA RID: 1514
	public class ItemVariant
	{
		/// <summary>
		/// Creates a new <see cref="T:Terraria.GameContent.Items.ItemVariant" /> with the provided localized description.
		/// </summary>
		/// <param name="description">The localized description to use.</param>
		// Token: 0x06004368 RID: 17256 RVA: 0x005FF507 File Offset: 0x005FD707
		public ItemVariant(LocalizedText description)
		{
			this.Description = description;
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x005FF516 File Offset: 0x005FD716
		public override string ToString()
		{
			return this.Description.ToString();
		}

		/// <summary>
		/// The localized description of this <see cref="T:Terraria.GameContent.Items.ItemVariant" />.
		/// </summary>
		// Token: 0x04005A21 RID: 23073
		public readonly LocalizedText Description;
	}
}
