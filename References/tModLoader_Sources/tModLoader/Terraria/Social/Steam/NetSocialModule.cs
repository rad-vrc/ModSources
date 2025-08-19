using System;
using System.Collections.Concurrent;
using System.IO;
using Steamworks;
using Terraria.Net;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x020000EB RID: 235
	public abstract class NetSocialModule : NetSocialModule
	{
		// Token: 0x06001818 RID: 6168 RVA: 0x004BA780 File Offset: 0x004B8980
		protected NetSocialModule(int readChannel, int writeChannel)
		{
			this._reader = new SteamP2PReader(readChannel);
			this._writer = new SteamP2PWriter(writeChannel);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x004BA7CC File Offset: 0x004B89CC
		public override void Initialize()
		{
			CoreSocialModule.OnTick += this._reader.ReadTick;
			CoreSocialModule.OnTick += this._writer.SendAll;
			this._lobbyChatMessage = Callback<LobbyChatMsg_t>.Create(new Callback<LobbyChatMsg_t>.DispatchDelegate(this.OnLobbyChatMessage));
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x004BA81D File Offset: 0x004B8A1D
		public override void Shutdown()
		{
			this._lobby.Leave();
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x004BA82C File Offset: 0x004B8A2C
		public override bool IsConnected(RemoteAddress address)
		{
			if (address == null)
			{
				return false;
			}
			CSteamID cSteamID = this.RemoteAddressToSteamId(address);
			if (!this._connectionStateMap.ContainsKey(cSteamID) || this._connectionStateMap[cSteamID] != NetSocialModule.ConnectionState.Connected)
			{
				return false;
			}
			if (this.GetSessionState(cSteamID).m_bConnectionActive != 1)
			{
				this.Close(address);
				return false;
			}
			return true;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x004BA880 File Offset: 0x004B8A80
		protected virtual void OnLobbyChatMessage(LobbyChatMsg_t result)
		{
			if (result.m_ulSteamIDLobby != this._lobby.Id.m_SteamID || result.m_eChatEntryType != 1 || result.m_ulSteamIDUser != this._lobby.Owner.m_SteamID)
			{
				return;
			}
			byte[] message = this._lobby.GetMessage((int)result.m_iChatID);
			if (message.Length == 0)
			{
				return;
			}
			using (MemoryStream memoryStream = new MemoryStream(message))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					if (binaryReader.ReadByte() == 1)
					{
						while ((long)message.Length - memoryStream.Position >= 8L)
						{
							CSteamID cSteamID;
							cSteamID..ctor(binaryReader.ReadUInt64());
							if (cSteamID != SteamUser.GetSteamID())
							{
								this._lobby.SetPlayedWith(cSteamID);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x004BA960 File Offset: 0x004B8B60
		protected P2PSessionState_t GetSessionState(CSteamID userId)
		{
			P2PSessionState_t pConnectionState;
			SteamNetworking.GetP2PSessionState(userId, ref pConnectionState);
			return pConnectionState;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x004BA977 File Offset: 0x004B8B77
		protected CSteamID RemoteAddressToSteamId(RemoteAddress address)
		{
			return ((SteamAddress)address).SteamId;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x004BA984 File Offset: 0x004B8B84
		public override bool Send(RemoteAddress address, byte[] data, int length)
		{
			CSteamID user = this.RemoteAddressToSteamId(address);
			this._writer.QueueSend(user, data, length);
			return true;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x004BA9A8 File Offset: 0x004B8BA8
		public override int Receive(RemoteAddress address, byte[] data, int offset, int length)
		{
			if (address == null)
			{
				return 0;
			}
			CSteamID user = this.RemoteAddressToSteamId(address);
			return this._reader.Receive(user, data, offset, length);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x004BA9D4 File Offset: 0x004B8BD4
		public override bool IsDataAvailable(RemoteAddress address)
		{
			CSteamID id = this.RemoteAddressToSteamId(address);
			return this._reader.IsDataAvailable(id);
		}

		// Token: 0x04001349 RID: 4937
		protected const int ServerReadChannel = 1;

		// Token: 0x0400134A RID: 4938
		protected const int ClientReadChannel = 2;

		// Token: 0x0400134B RID: 4939
		protected const int LobbyMessageJoin = 1;

		// Token: 0x0400134C RID: 4940
		protected const ushort GamePort = 27005;

		// Token: 0x0400134D RID: 4941
		protected const ushort SteamPort = 27006;

		// Token: 0x0400134E RID: 4942
		protected const ushort QueryPort = 27007;

		// Token: 0x0400134F RID: 4943
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

		// Token: 0x04001350 RID: 4944
		protected SteamP2PReader _reader;

		// Token: 0x04001351 RID: 4945
		protected SteamP2PWriter _writer;

		// Token: 0x04001352 RID: 4946
		protected Lobby _lobby = new Lobby();

		// Token: 0x04001353 RID: 4947
		protected ConcurrentDictionary<CSteamID, NetSocialModule.ConnectionState> _connectionStateMap = new ConcurrentDictionary<CSteamID, NetSocialModule.ConnectionState>();

		// Token: 0x04001354 RID: 4948
		protected object _steamLock = new object();

		// Token: 0x04001355 RID: 4949
		private Callback<LobbyChatMsg_t> _lobbyChatMessage;

		// Token: 0x02000886 RID: 2182
		public enum ConnectionState
		{
			// Token: 0x040069E6 RID: 27110
			Inactive,
			// Token: 0x040069E7 RID: 27111
			Authenticating,
			// Token: 0x040069E8 RID: 27112
			Connected
		}

		// Token: 0x02000887 RID: 2183
		// (Invoke) Token: 0x060051B8 RID: 20920
		protected delegate void AsyncHandshake(CSteamID client);
	}
}
