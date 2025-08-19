using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x020002B1 RID: 689
	public struct ParticleOrchestraSettings
	{
		// Token: 0x060021B1 RID: 8625 RVA: 0x0052B40F File Offset: 0x0052960F
		public void Serialize(BinaryWriter writer)
		{
			writer.WriteVector2(this.PositionInWorld);
			writer.WriteVector2(this.MovementVector);
			writer.Write(this.UniqueInfoPiece);
			writer.Write(this.IndexOfPlayerWhoInvokedThis);
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x0052B441 File Offset: 0x00529641
		public void DeserializeFrom(BinaryReader reader)
		{
			this.PositionInWorld = reader.ReadVector2();
			this.MovementVector = reader.ReadVector2();
			this.UniqueInfoPiece = reader.ReadInt32();
			this.IndexOfPlayerWhoInvokedThis = reader.ReadByte();
		}

		// Token: 0x04004777 RID: 18295
		public Vector2 PositionInWorld;

		// Token: 0x04004778 RID: 18296
		public Vector2 MovementVector;

		// Token: 0x04004779 RID: 18297
		public int UniqueInfoPiece;

		// Token: 0x0400477A RID: 18298
		public byte IndexOfPlayerWhoInvokedThis;

		// Token: 0x0400477B RID: 18299
		public const int SerializationSize = 21;
	}
}
