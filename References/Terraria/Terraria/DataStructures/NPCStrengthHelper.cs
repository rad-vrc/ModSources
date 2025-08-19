using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000406 RID: 1030
	public struct NPCStrengthHelper
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x0059D9C1 File Offset: 0x0059BBC1
		public bool IsExpertMode
		{
			get
			{
				return this._strengthOverride >= 2f || this._gameModeDifficulty >= 2f;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x0059D9E2 File Offset: 0x0059BBE2
		public bool IsMasterMode
		{
			get
			{
				return this._strengthOverride >= 3f || this._gameModeDifficulty >= 3f;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x0059DA03 File Offset: 0x0059BC03
		public bool ExtraDamageForGetGoodWorld
		{
			get
			{
				return this._strengthOverride >= 4f || this._gameModeDifficulty >= 4f;
			}
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x0059DA24 File Offset: 0x0059BC24
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

		// Token: 0x04004F57 RID: 20311
		private float _strengthOverride;

		// Token: 0x04004F58 RID: 20312
		private float _gameModeDifficulty;

		// Token: 0x04004F59 RID: 20313
		private GameModeData _gameModeData;
	}
}
