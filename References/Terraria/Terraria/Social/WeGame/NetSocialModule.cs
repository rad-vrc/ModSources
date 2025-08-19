using System;
using System.Collections.Concurrent;
using rail;
using Terraria.Net;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200015B RID: 347
	public abstract class NetSocialModule : NetSocialModule
	{
		// Token: 0x060019C7 RID: 6599 RVA: 0x004E27F4 File Offset: 0x004E09F4
		protected NetSocialModule()
		{
			this._reader = new WeGameP2PReader();
			this._writer = new WeGameP2PWriter();
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x004E2828 File Offset: 0x004E0A28
		public override void Initialize()
		{
			CoreSocialModule.OnTick += this._reader.ReadTick;
			CoreSocialModule.OnTick += this._writer.SendAll;
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x004E2856 File Offset: 0x004E0A56
		public override void Shutdown()
		{
			this._lobby.Leave();
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x004E2864 File Offset: 0x004E0A64
		public override bool IsConnected(RemoteAddress address)
		{
			if (address == null)
			{
				return false;
			}
			RailID key = this.RemoteAddressToRailId(address);
			return this._connectionStateMap.ContainsKey(key) && this._connectionStateMap[key] == NetSocialModule.ConnectionState.Connected;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x004E289E File Offset: 0x004E0A9E
		protected RailID GetLocalPeer()
		{
			return rail_api.RailFactory().RailPlayer().GetRailID();
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x004E28B0 File Offset: 0x004E0AB0
		protected bool GetSessionState(RailID userId, RailNetworkSessionState state)
		{
			IRailNetwork railNetwork = rail_api.RailFactory().RailNetworkHelper();
			if (railNetwork.GetSessionState(userId, state) != null)
			{
				WeGameHelper.WriteDebugString("GetSessionState Failed user:{0}", new object[]
				{
					userId.id_
				});
				return false;
			}
			return true;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x004E28F3 File Offset: 0x004E0AF3
		protected RailID RemoteAddressToRailId(RemoteAddress address)
		{
			return ((WeGameAddress)address).rail_id;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x004E2900 File Offset: 0x004E0B00
		public override bool Send(RemoteAddress address, byte[] data, int length)
		{
			RailID user = this.RemoteAddressToRailId(address);
			this._writer.QueueSend(user, data, length);
			return true;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x004E2924 File Offset: 0x004E0B24
		public override int Receive(RemoteAddress address, byte[] data, int offset, int length)
		{
			if (address == null)
			{
				return 0;
			}
			RailID user = this.RemoteAddressToRailId(address);
			return this._reader.Receive(user, data, offset, length);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x004E2950 File Offset: 0x004E0B50
		public override bool IsDataAvailable(RemoteAddress address)
		{
			RailID id = this.RemoteAddressToRailId(address);
			return this._reader.IsDataAvailable(id);
		}

		// Token: 0x0400155A RID: 5466
		protected const int LobbyMessageJoin = 1;

		// Token: 0x0400155B RID: 5467
		protected Lobby _lobby = new Lobby();

		// Token: 0x0400155C RID: 5468
		protected WeGameP2PReader _reader;

		// Token: 0x0400155D RID: 5469
		protected WeGameP2PWriter _writer;

		// Token: 0x0400155E RID: 5470
		protected ConcurrentDictionary<RailID, NetSocialModule.ConnectionState> _connectionStateMap = new ConcurrentDictionary<RailID, NetSocialModule.ConnectionState>();

		// Token: 0x0400155F RID: 5471
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

		// Token: 0x020005B3 RID: 1459
		public enum ConnectionState
		{
			// Token: 0x04005A77 RID: 23159
			Inactive,
			// Token: 0x04005A78 RID: 23160
			Authenticating,
			// Token: 0x04005A79 RID: 23161
			Connected
		}
	}
}
