using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Terraria.Social.Base
{
	// Token: 0x02000181 RID: 385
	public class ServerJoinRequestsManager
	{
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06001AE3 RID: 6883 RVA: 0x004E6678 File Offset: 0x004E4878
		// (remove) Token: 0x06001AE4 RID: 6884 RVA: 0x004E66B0 File Offset: 0x004E48B0
		public event ServerJoinRequestEvent OnRequestAdded;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06001AE5 RID: 6885 RVA: 0x004E66E8 File Offset: 0x004E48E8
		// (remove) Token: 0x06001AE6 RID: 6886 RVA: 0x004E6720 File Offset: 0x004E4920
		public event ServerJoinRequestEvent OnRequestRemoved;

		// Token: 0x06001AE7 RID: 6887 RVA: 0x004E6755 File Offset: 0x004E4955
		public ServerJoinRequestsManager()
		{
			this._requests = new List<UserJoinToServerRequest>();
			this.CurrentRequests = new ReadOnlyCollection<UserJoinToServerRequest>(this._requests);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x004E677C File Offset: 0x004E497C
		public void Update()
		{
			for (int i = this._requests.Count - 1; i >= 0; i--)
			{
				if (!this._requests[i].IsValid())
				{
					this.RemoveRequestAtIndex(i);
				}
			}
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x004E67BC File Offset: 0x004E49BC
		public void Add(UserJoinToServerRequest request)
		{
			for (int i = this._requests.Count - 1; i >= 0; i--)
			{
				if (this._requests[i].Equals(request))
				{
					this.RemoveRequestAtIndex(i);
				}
			}
			this._requests.Add(request);
			request.OnAccepted += delegate()
			{
				this.RemoveRequest(request);
			};
			request.OnRejected += delegate()
			{
				this.RemoveRequest(request);
			};
			if (this.OnRequestAdded != null)
			{
				this.OnRequestAdded(request);
			}
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x004E6870 File Offset: 0x004E4A70
		private void RemoveRequestAtIndex(int i)
		{
			UserJoinToServerRequest request = this._requests[i];
			this._requests.RemoveAt(i);
			if (this.OnRequestRemoved != null)
			{
				this.OnRequestRemoved(request);
			}
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x004E68AA File Offset: 0x004E4AAA
		private void RemoveRequest(UserJoinToServerRequest request)
		{
			if (this._requests.Remove(request) && this.OnRequestRemoved != null)
			{
				this.OnRequestRemoved(request);
			}
		}

		// Token: 0x040015DB RID: 5595
		private readonly List<UserJoinToServerRequest> _requests;

		// Token: 0x040015DC RID: 5596
		public readonly ReadOnlyCollection<UserJoinToServerRequest> CurrentRequests;
	}
}
