using System;
using System.Collections.Concurrent;
using rail;
using Terraria.Net;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000DC RID: 220
	public abstract class NetSocialModule : NetSocialModule
	{
		// Token: 0x06001790 RID: 6032 RVA: 0x004B8838 File Offset: 0x004B6A38
		protected NetSocialModule()
		{
			this._reader = new WeGameP2PReader();
			this._writer = new WeGameP2PWriter();
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x004B886C File Offset: 0x004B6A6C
		public override void Initialize()
		{
			CoreSocialModule.OnTick += this._reader.ReadTick;
			CoreSocialModule.OnTick += this._writer.SendAll;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x004B889A File Offset: 0x004B6A9A
		public override void Shutdown()
		{
			this._lobby.Leave();
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x004B88A8 File Offset: 0x004B6AA8
		public override bool IsConnected(RemoteAddress address)
		{
			if (address == null)
			{
				return false;
			}
			RailID key = this.RemoteAddressToRailId(address);
			return this._connectionStateMap.ContainsKey(key) && this._connectionStateMap[key] == NetSocialModule.ConnectionState.Connected;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x004B88E2 File Offset: 0x004B6AE2
		protected RailID GetLocalPeer()
		{
			return rail_api.RailFactory().RailPlayer().GetRailID();
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x004B88F3 File Offset: 0x004B6AF3
		protected bool GetSessionState(RailID userId, RailNetworkSessionState state)
		{
			if (rail_api.RailFactory().RailNetworkHelper().GetSessionState(userId, state) != null)
			{
				WeGameHelper.WriteDebugString("GetSessionState Failed user:{0}", new object[]
				{
					userId.id_
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x004B8929 File Offset: 0x004B6B29
		protected RailID RemoteAddressToRailId(RemoteAddress address)
		{
			return ((WeGameAddress)address).rail_id;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x004B8938 File Offset: 0x004B6B38
		public override bool Send(RemoteAddress address, byte[] data, int length)
		{
			RailID user = this.RemoteAddressToRailId(address);
			this._writer.QueueSend(user, data, length);
			return true;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x004B895C File Offset: 0x004B6B5C
		public override int Receive(RemoteAddress address, byte[] data, int offset, int length)
		{
			if (address == null)
			{
				return 0;
			}
			RailID user = this.RemoteAddressToRailId(address);
			return this._reader.Receive(user, data, offset, length);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x004B8988 File Offset: 0x004B6B88
		public override bool IsDataAvailable(RemoteAddress address)
		{
			RailID id = this.RemoteAddressToRailId(address);
			return this._reader.IsDataAvailable(id);
		}

		// Token: 0x0400130B RID: 4875
		protected const int LobbyMessageJoin = 1;

		// Token: 0x0400130C RID: 4876
		protected Lobby _lobby = new Lobby();

		// Token: 0x0400130D RID: 4877
		protected WeGameP2PReader _reader;

		// Token: 0x0400130E RID: 4878
		protected WeGameP2PWriter _writer;

		// Token: 0x0400130F RID: 4879
		protected ConcurrentDictionary<RailID, NetSocialModule.ConnectionState> _connectionStateMap = new ConcurrentDictionary<RailID, NetSocialModule.ConnectionState>();

		// Token: 0x04001310 RID: 4880
		protected static readonly byte[] _handshake = new byte[]
		{
			10,
			0,
			93,
			114,
			101,
			108,
			111,
			103,
			105,
			99
		};

		// Token: 0x02000880 RID: 2176
		public enum ConnectionState
		{
			// Token: 0x040069D7 RID: 27095
			Inactive,
			// Token: 0x040069D8 RID: 27096
			Authenticating,
			// Token: 0x040069D9 RID: 27097
			Connected
		}
	}
}
