using System;

namespace Terraria.Physics
{
	// Token: 0x020000B6 RID: 182
	public struct BallStepResult
	{
		// Token: 0x060013F6 RID: 5110 RVA: 0x004A1E5A File Offset: 0x004A005A
		private BallStepResult(BallState state)
		{
			this.State = state;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x004A1E63 File Offset: 0x004A0063
		public static BallStepResult OutOfBounds()
		{
			return new BallStepResult(BallState.OutOfBounds);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x004A1E6B File Offset: 0x004A006B
		public static BallStepResult Moving()
		{
			return new BallStepResult(BallState.Moving);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x004A1E73 File Offset: 0x004A0073
		public static BallStepResult Resting()
		{
			return new BallStepResult(BallState.Resting);
		}

		// Token: 0x040011D8 RID: 4568
		public readonly BallState State;
	}
}
