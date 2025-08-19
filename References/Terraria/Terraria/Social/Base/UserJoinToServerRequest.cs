using System;

namespace Terraria.Social.Base
{
	// Token: 0x02000182 RID: 386
	public abstract class UserJoinToServerRequest
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x004E68CE File Offset: 0x004E4ACE
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x004E68D6 File Offset: 0x004E4AD6
		internal string UserDisplayName { get; private set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x004E68DF File Offset: 0x004E4ADF
		// (set) Token: 0x06001AEF RID: 6895 RVA: 0x004E68E7 File Offset: 0x004E4AE7
		internal string UserFullIdentifier { get; private set; }

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06001AF0 RID: 6896 RVA: 0x004E68F0 File Offset: 0x004E4AF0
		// (remove) Token: 0x06001AF1 RID: 6897 RVA: 0x004E6928 File Offset: 0x004E4B28
		public event Action OnAccepted;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06001AF2 RID: 6898 RVA: 0x004E6960 File Offset: 0x004E4B60
		// (remove) Token: 0x06001AF3 RID: 6899 RVA: 0x004E6998 File Offset: 0x004E4B98
		public event Action OnRejected;

		// Token: 0x06001AF4 RID: 6900 RVA: 0x004E69CD File Offset: 0x004E4BCD
		public UserJoinToServerRequest(string userDisplayName, string fullIdentifier)
		{
			this.UserDisplayName = userDisplayName;
			this.UserFullIdentifier = fullIdentifier;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x004E69E3 File Offset: 0x004E4BE3
		public void Accept()
		{
			if (this.OnAccepted != null)
			{
				this.OnAccepted();
			}
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x004E69F8 File Offset: 0x004E4BF8
		public void Reject()
		{
			if (this.OnRejected != null)
			{
				this.OnRejected();
			}
		}

		// Token: 0x06001AF7 RID: 6903
		public abstract bool IsValid();

		// Token: 0x06001AF8 RID: 6904
		public abstract string GetUserWrapperText();
	}
}
