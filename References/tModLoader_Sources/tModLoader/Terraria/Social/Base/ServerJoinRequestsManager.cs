using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Terraria.Social.Base
{
	// Token: 0x02000103 RID: 259
	public class ServerJoinRequestsManager
	{
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060018EF RID: 6383 RVA: 0x004BE464 File Offset: 0x004BC664
		// (remove) Token: 0x060018F0 RID: 6384 RVA: 0x004BE49C File Offset: 0x004BC69C
		public event ServerJoinRequestEvent OnRequestAdded;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060018F1 RID: 6385 RVA: 0x004BE4D4 File Offset: 0x004BC6D4
		// (remove) Token: 0x060018F2 RID: 6386 RVA: 0x004BE50C File Offset: 0x004BC70C
		public event ServerJoinRequestEvent OnRequestRemoved;

		// Token: 0x060018F3 RID: 6387 RVA: 0x004BE541 File Offset: 0x004BC741
		public ServerJoinRequestsManager()
		{
			this._requests = new List<UserJoinToServerRequest>();
			this.CurrentRequests = new ReadOnlyCollection<UserJoinToServerRequest>(this._requests);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x004BE568 File Offset: 0x004BC768
		public void Update()
		{
			for (int num = this._requests.Count - 1; num >= 0; num--)
			{
				if (!this._requests[num].IsValid())
				{
					this.RemoveRequestAtIndex(num);
				}
			}
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x004BE5A8 File Offset: 0x004BC7A8
		public void Add(UserJoinToServerRequest request)
		{
			for (int num = this._requests.Count - 1; num >= 0; num--)
			{
				if (this._requests[num].Equals(request))
				{
					this.RemoveRequestAtIndex(num);
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

		// Token: 0x060018F6 RID: 6390 RVA: 0x004BE65C File Offset: 0x004BC85C
		private void RemoveRequestAtIndex(int i)
		{
			UserJoinToServerRequest request = this._requests[i];
			this._requests.RemoveAt(i);
			if (this.OnRequestRemoved != null)
			{
				this.OnRequestRemoved(request);
			}
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x004BE696 File Offset: 0x004BC896
		private void RemoveRequest(UserJoinToServerRequest request)
		{
			if (this._requests.Remove(request) && this.OnRequestRemoved != null)
			{
				this.OnRequestRemoved(request);
			}
		}

		// Token: 0x0400138D RID: 5005
		private readonly List<UserJoinToServerRequest> _requests;

		// Token: 0x0400138E RID: 5006
		public readonly ReadOnlyCollection<UserJoinToServerRequest> CurrentRequests;
	}
}
