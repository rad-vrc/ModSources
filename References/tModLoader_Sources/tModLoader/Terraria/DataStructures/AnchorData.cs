using System;
using Terraria.Enums;

namespace Terraria.DataStructures
{
	// Token: 0x020006CF RID: 1743
	public struct AnchorData
	{
		/// <summary>
		/// Defines an anchor, meaning the surrounding tiles required for a multitile to be placeable at a location. <paramref name="type" /> dictates which types of tiles are eligible for the anchor while <paramref name="start" /> and <paramref name="count" /> dictate how much of the multitile edge need to have that specific anchor.
		/// For example, most furniture tiles will simply set a bottom anchor spanning the full width of the tile requiring solid tiles:
		/// <br /><c>TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);</c>
		/// <para /> <paramref name="type" /> can be defined using multiple options with the logical OR operator (<c>|</c>) to combine <see cref="T:Terraria.Enums.AnchorType" /> values, allowing tiles satisfying any of the AnchorTypes to work:
		/// <br /><c>AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide</c>
		/// <para /> The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#anchorbottomanchorleftanchorrightanchortop">Anchor section of the Basic Tile wiki guide</see> has more information and visualizations.
		/// </summary>
		// Token: 0x060048F8 RID: 18680 RVA: 0x0064C208 File Offset: 0x0064A408
		public AnchorData(AnchorType type, int count, int start)
		{
			this.type = type;
			this.tileCount = count;
			this.checkStart = start;
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x0064C21F File Offset: 0x0064A41F
		public static bool operator ==(AnchorData data1, AnchorData data2)
		{
			return data1.type == data2.type && data1.tileCount == data2.tileCount && data1.checkStart == data2.checkStart;
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x0064C24D File Offset: 0x0064A44D
		public static bool operator !=(AnchorData data1, AnchorData data2)
		{
			return data1.type != data2.type || data1.tileCount != data2.tileCount || data1.checkStart != data2.checkStart;
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x0064C280 File Offset: 0x0064A480
		public override bool Equals(object obj)
		{
			return obj is AnchorData && (this.type == ((AnchorData)obj).type && this.tileCount == ((AnchorData)obj).tileCount) && this.checkStart == ((AnchorData)obj).checkStart;
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x0064C2D4 File Offset: 0x0064A4D4
		public override int GetHashCode()
		{
			byte b = (byte)this.checkStart;
			byte b2 = (byte)this.tileCount;
			return (int)((ushort)this.type) << 16 | (int)b2 << 8 | (int)b;
		}

		// Token: 0x04005E4E RID: 24142
		public AnchorType type;

		// Token: 0x04005E4F RID: 24143
		public int tileCount;

		// Token: 0x04005E50 RID: 24144
		public int checkStart;

		// Token: 0x04005E51 RID: 24145
		public static AnchorData Empty;
	}
}
