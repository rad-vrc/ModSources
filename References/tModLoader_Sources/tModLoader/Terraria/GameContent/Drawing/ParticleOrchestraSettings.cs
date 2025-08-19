using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x02000639 RID: 1593
	public struct ParticleOrchestraSettings
	{
		// Token: 0x060045A6 RID: 17830 RVA: 0x0061497F File Offset: 0x00612B7F
		public void Serialize(BinaryWriter writer)
		{
			writer.WriteVector2(this.PositionInWorld);
			writer.WriteVector2(this.MovementVector);
			writer.Write(this.UniqueInfoPiece);
			writer.Write(this.IndexOfPlayerWhoInvokedThis);
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x006149B1 File Offset: 0x00612BB1
		public void DeserializeFrom(BinaryReader reader)
		{
			this.PositionInWorld = reader.ReadVector2();
			this.MovementVector = reader.ReadVector2();
			this.UniqueInfoPiece = reader.ReadInt32();
			this.IndexOfPlayerWhoInvokedThis = reader.ReadByte();
		}

		// Token: 0x04005B07 RID: 23303
		public Vector2 PositionInWorld;

		// Token: 0x04005B08 RID: 23304
		public Vector2 MovementVector;

		// Token: 0x04005B09 RID: 23305
		public int UniqueInfoPiece;

		// Token: 0x04005B0A RID: 23306
		public byte IndexOfPlayerWhoInvokedThis;

		// Token: 0x04005B0B RID: 23307
		public const int SerializationSize = 21;
	}
}
