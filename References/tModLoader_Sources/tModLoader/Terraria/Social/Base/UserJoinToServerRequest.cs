using System;

namespace Terraria.Social.Base
{
	// Token: 0x0200010A RID: 266
	public abstract class UserJoinToServerRequest
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x004BE8FA File Offset: 0x004BCAFA
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x004BE902 File Offset: 0x004BCB02
		internal string UserDisplayName { get; private set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x004BE90B File Offset: 0x004BCB0B
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x004BE913 File Offset: 0x004BCB13
		internal string UserFullIdentifier { get; private set; }

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600190F RID: 6415 RVA: 0x004BE91C File Offset: 0x004BCB1C
		// (remove) Token: 0x06001910 RID: 6416 RVA: 0x004BE954 File Offset: 0x004BCB54
		public event Action OnAccepted;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06001911 RID: 6417 RVA: 0x004BE98C File Offset: 0x004BCB8C
		// (remove) Token: 0x06001912 RID: 6418 RVA: 0x004BE9C4 File Offset: 0x004BCBC4
		public event Action OnRejected;

		// Token: 0x06001913 RID: 6419 RVA: 0x004BE9F9 File Offset: 0x004BCBF9
		public UserJoinToServerRequest(string userDisplayName, string fullIdentifier)
		{
			this.UserDisplayName = userDisplayName;
			this.UserFullIdentifier = fullIdentifier;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x004BEA0F File Offset: 0x004BCC0F
		public void Accept()
		{
			if (this.OnAccepted != null)
			{
				this.OnAccepted();
			}
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x004BEA24 File Offset: 0x004BCC24
		public void Reject()
		{
			if (this.OnRejected != null)
			{
				this.OnRejected();
			}
		}

		// Token: 0x06001916 RID: 6422
		public abstract bool IsValid();

		// Token: 0x06001917 RID: 6423
		public abstract string GetUserWrapperText();
	}
}
