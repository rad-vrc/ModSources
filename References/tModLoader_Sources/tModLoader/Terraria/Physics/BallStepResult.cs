using System;

namespace Terraria.Physics
{
	// Token: 0x02000118 RID: 280
	public struct BallStepResult
	{
		// Token: 0x06001981 RID: 6529 RVA: 0x004BFC48 File Offset: 0x004BDE48
		private BallStepResult(BallState state)
		{
			this.State = state;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x004BFC51 File Offset: 0x004BDE51
		public static BallStepResult OutOfBounds()
		{
			return new BallStepResult(BallState.OutOfBounds);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x004BFC59 File Offset: 0x004BDE59
		public static BallStepResult Moving()
		{
			return new BallStepResult(BallState.Moving);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x004BFC61 File Offset: 0x004BDE61
		public static BallStepResult Resting()
		{
			return new BallStepResult(BallState.Resting);
		}

		// Token: 0x040013CF RID: 5071
		public readonly BallState State;
	}
}
