using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000721 RID: 1825
	public struct NPCStrengthHelper
	{
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060049FB RID: 18939 RVA: 0x0064EB2E File Offset: 0x0064CD2E
		public bool IsExpertMode
		{
			get
			{
				return this._strengthOverride >= 2f || this._gameModeDifficulty >= 2f;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060049FC RID: 18940 RVA: 0x0064EB4F File Offset: 0x0064CD4F
		public bool IsMasterMode
		{
			get
			{
				return this._strengthOverride >= 3f || this._gameModeDifficulty >= 3f;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060049FD RID: 18941 RVA: 0x0064EB70 File Offset: 0x0064CD70
		public bool ExtraDamageForGetGoodWorld
		{
			get
			{
				return this._strengthOverride >= 4f || this._gameModeDifficulty >= 4f;
			}
		}

		// Token: 0x060049FE RID: 18942 RVA: 0x0064EB94 File Offset: 0x0064CD94
		public NPCStrengthHelper(GameModeData data, float strength, bool isGetGoodWorld)
		{
			this._strengthOverride = strength;
			this._gameModeData = data;
			this._gameModeDifficulty = 1f;
			if (this._gameModeData.IsExpertMode)
			{
				this._gameModeDifficulty += 1f;
			}
			if (this._gameModeData.IsMasterMode)
			{
				this._gameModeDifficulty += 1f;
			}
			if (isGetGoodWorld)
			{
				this._gameModeDifficulty += 1f;
			}
		}

		// Token: 0x04005F20 RID: 24352
		private float _strengthOverride;

		// Token: 0x04005F21 RID: 24353
		private float _gameModeDifficulty;

		// Token: 0x04005F22 RID: 24354
		private GameModeData _gameModeData;
	}
}
