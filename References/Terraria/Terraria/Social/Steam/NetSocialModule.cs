using System;
using System.Collections.Concurrent;
using System.IO;
using Steamworks;
using Terraria.Net;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x0200017B RID: 379
	public abstract class NetSocialModule : NetSocialModule
	{
		// Token: 0x06001AC3 RID: 6851 RVA: 0x004E5CC4 File Offset: 0x004E3EC4
		protected NetSocialModule(int readChannel, int writeChannel)
		{
			this._reader = new SteamP2PReader(readChannel);
			this._writer = new SteamP2PWriter(writeChannel);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x004E5D10 File Offset: 0x004E3F10
		public override void Initialize()
		{
			CoreSocialModule.OnTick += this._reader.ReadTick;
			CoreSocialModule.OnTick += this._writer.SendAll;
			this._lobbyChatMessage = Callback<LobbyChatMsg_t>.Create(new Callback<LobbyChatMsg_t>.DispatchDelegate(this.OnLobbyChatMessage));
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x004E5D61 File Offset: 0x004E3F61
		public override void Shutdown()
		{
			this._lobby.Leave();
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x004E5D70 File Offset: 0x004E3F70
		public override bool IsConnected(RemoteAddress address)
		{
			if (address == null)
			{
				return false;
			}
			CSteamID csteamID = this.RemoteAddressToSteamId(address);
			if (!this._connectionStateMap.ContainsKey(csteamID) || this._connectionStateMap[csteamID] != NetSocialModule.ConnectionState.Connected)
			{
				return false;
			}
			if (this.GetSessionState(csteamID).m_bConnectionActive != 1)
			{
				this.Close(address);
				return false;
			}
			return true;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x004E5DC4 File Offset: 0x004E3FC4
		protected virtual void OnLobbyChatMessage(LobbyChatMsg_t result)
		{
			if (result.m_ulSteamIDLobby != this._lobby.Id.m_SteamID)
			{
				return;
			}
			if (result.m_eChatEntryType != 1)
			{
				return;
			}
			if (result.m_ulSteamIDUser != this._lobby.Owner.m_SteamID)
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
					byte b = binaryReader.ReadByte();
					if (b == 1)
					{
						while ((long)message.Length - memoryStream.Position >= 8L)
						{
							CSteamID csteamID;
							csteamID..ctor(binaryReader.ReadUInt64());
							if (csteamID != SteamUser.GetSteamID())
							{
								this._lobby.SetPlayedWith(csteamID);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x004E5EA8 File Offset: 0x004E40A8
		protected P2PSessionState_t GetSessionState(CSteamID userId)
		{
			P2PSessionState_t result;
			SteamNetworking.GetP2PSessionState(userId, ref result);
			return result;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x004E5EBF File Offset: 0x004E40BF
		protected CSteamID RemoteAddressToSteamId(RemoteAddress address)
		{
			return ((SteamAddress)address).SteamId;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x004E5ECC File Offset: 0x004E40CC
		public override bool Send(RemoteAddress address, byte[] data, int length)
		{
			CSteamID user = this.RemoteAddressToSteamId(address);
			this._writer.QueueSend(user, data, length);
			return true;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x004E5EF0 File Offset: 0x004E40F0
		public override int Receive(RemoteAddress address, byte[] data, int offset, int length)
		{
			if (address == null)
			{
				return 0;
			}
			CSteamID user = this.RemoteAddressToSteamId(address);
			return this._reader.Receive(user, data, offset, length);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x004E5F1C File Offset: 0x004E411C
		public override bool IsDataAvailable(RemoteAddress address)
		{
			CSteamID id = this.RemoteAddressToSteamId(address);
			return this._reader.IsDataAvailable(id);
		}

		// Token: 0x040015C0 RID: 5568
		protected const int ServerReadChannel = 1;

		// Token: 0x040015C1 RID: 5569
		protected const int ClientReadChannel = 2;

		// Token: 0x040015C2 RID: 5570
		protected const int LobbyMessageJoin = 1;

		// Token: 0x040015C3 RID: 5571
		protected const ushort GamePort = 27005;

		// Token: 0x040015C4 RID: 5572
		protected const ushort SteamPort = 27006;

		// Token: 0x040015C5 RID: 5573
		protected const ushort QueryPort = 27007;

		// Token: 0x040015C6 RID: 5574
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

		// Token: 0x040015C7 RID: 5575
		protected SteamP2PReader _reader;

		// Token: 0x040015C8 RID: 5576
		protected SteamP2PWriter _writer;

		// Token: 0x040015C9 RID: 5577
		protected Lobby _lobby = new Lobby();

		// Token: 0x040015CA RID: 5578
		protected ConcurrentDictionary<CSteamID, NetSocialModule.ConnectionState> _connectionStateMap = new ConcurrentDictionary<CSteamID, NetSocialModule.ConnectionState>();

		// Token: 0x040015CB RID: 5579
		protected object _steamLock = new object();

		// Token: 0x040015CC RID: 5580
		private Callback<LobbyChatMsg_t> _lobbyChatMessage;

		// Token: 0x020005BD RID: 1469
		public enum ConnectionState
		{
			// Token: 0x04005A8D RID: 23181
			Inactive,
			// Token: 0x04005A8E RID: 23182
			Authenticating,
			// Token: 0x04005A8F RID: 23183
			Connected
		}

		// Token: 0x020005BE RID: 1470
		// (Invoke) Token: 0x060032A0 RID: 12960
		protected delegate void AsyncHandshake(CSteamID client);
	}
}
