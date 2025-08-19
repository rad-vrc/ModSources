using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using NATUPNPLib;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.Net;
using Terraria.Net.Sockets;
using Terraria.Social;
using Terraria.Social.Base;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x0200003F RID: 63
	public class Netplay
	{
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000715 RID: 1813 RVA: 0x0015DDD0 File Offset: 0x0015BFD0
		// (remove) Token: 0x06000716 RID: 1814 RVA: 0x0015DE04 File Offset: 0x0015C004
		public static event Action OnDisconnect;

		// Token: 0x06000717 RID: 1815 RVA: 0x0015DE38 File Offset: 0x0015C038
		private static void UpdateServerInMainThread()
		{
			for (int i = 0; i < 256; i++)
			{
				if (NetMessage.buffer[i].checkBytes)
				{
					NetMessage.CheckBytes(i);
				}
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0015DE6C File Offset: 0x0015C06C
		private static string GetLocalIPAddress()
		{
			string result = "";
			foreach (IPAddress iPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
			{
				if (Netplay.AcceptedFamilyType(iPAddress.AddressFamily))
				{
					result = iPAddress.ToString();
					break;
				}
			}
			return result;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0015DEB8 File Offset: 0x0015C0B8
		private static void ResetNetDiag()
		{
			Main.ActiveNetDiagnosticsUI.Reset();
		}

		/// <summary>
		/// Sets all world sections as unloaded for all clients. This results in all clients being sent the world sections again.
		/// <br /><br /> This is used at the end of Hardmode world generation to force all clients to receive the tiles from the server again.
		/// <br /><br /> If you are doing massive world changes in-game outside of the Hardmode transformation, this is a simple way to ensure that all clients see those tile changes without sending countless <see cref="M:Terraria.NetMessage.SendTileSquare(System.Int32,System.Int32,System.Int32,Terraria.ID.TileChangeType)" /> messages.
		/// <br /><br /> <b>Call on Server only!</b>
		/// </summary>
		// Token: 0x0600071A RID: 1818 RVA: 0x0015DEC4 File Offset: 0x0015C0C4
		public static void ResetSections()
		{
			for (int i = 0; i < 256; i++)
			{
				for (int j = 0; j < Main.maxSectionsX; j++)
				{
					for (int k = 0; k < Main.maxSectionsY; k++)
					{
						Netplay.Clients[i].TileSections[j, k] = false;
					}
				}
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0015DF18 File Offset: 0x0015C118
		public static void AddBan(int plr)
		{
			RemoteAddress remoteAddress = Netplay.Clients[plr].Socket.GetRemoteAddress();
			using (StreamWriter streamWriter = new StreamWriter(Netplay.BanFilePath, true))
			{
				streamWriter.WriteLine("//" + Main.player[plr].name);
				streamWriter.WriteLine(remoteAddress.GetIdentifier());
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0015DF88 File Offset: 0x0015C188
		public static bool IsBanned(RemoteAddress address)
		{
			try
			{
				string identifier = address.GetIdentifier();
				if (File.Exists(Netplay.BanFilePath))
				{
					using (StreamReader streamReader = new StreamReader(Netplay.BanFilePath))
					{
						string text;
						while ((text = streamReader.ReadLine()) != null)
						{
							if (text == identifier)
							{
								return true;
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0015DFFC File Offset: 0x0015C1FC
		private static void OpenPort(int port)
		{
			if (!Main.dedServ)
			{
				return;
			}
			string localIPAddress = Netplay.GetLocalIPAddress();
			if (Netplay._upnpnat == null)
			{
				Netplay._upnpnat = new UPnPNAT();
				Netplay._mappings = Netplay._upnpnat.StaticPortMappingCollection;
			}
			if (Netplay._mappings == null)
			{
				return;
			}
			bool flag = false;
			foreach (object obj in Netplay._mappings)
			{
				IStaticPortMapping mapping = (IStaticPortMapping)obj;
				if (mapping.InternalPort == port && mapping.InternalClient == localIPAddress && mapping.Protocol == "TCP")
				{
					flag = true;
				}
			}
			if (!flag)
			{
				Netplay._mappings.Add(port, "TCP", port, localIPAddress, true, "Terraria Server");
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0015E0D0 File Offset: 0x0015C2D0
		private static void ClosePort(int port)
		{
			if (!Main.dedServ)
			{
				return;
			}
			string localIPAddress = Netplay.GetLocalIPAddress();
			bool flag = false;
			if (Netplay._mappings == null)
			{
				return;
			}
			foreach (object obj in Netplay._mappings)
			{
				IStaticPortMapping mapping = (IStaticPortMapping)obj;
				if (mapping.InternalPort == port && mapping.InternalClient == localIPAddress && mapping.Protocol == "TCP")
				{
					flag = true;
				}
			}
			if (!flag)
			{
				Netplay._mappings.Remove(port, "TCP");
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0015E17C File Offset: 0x0015C37C
		private static void ServerFullWriteCallBack(object state)
		{
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0015E180 File Offset: 0x0015C380
		private static void OnConnectionAccepted(ISocket client)
		{
			Logging.ServerConsoleLine(Language.GetTextValue("Net.ClientConnecting", client.GetRemoteAddress().GetFriendlyName()));
			int num = Netplay.FindNextOpenClientSlot();
			if (num != -1)
			{
				Netplay.Clients[num].Reset();
				Netplay.Clients[num].Socket = client;
			}
			else
			{
				MessageBuffer obj = Netplay.fullBuffer;
				lock (obj)
				{
					Netplay.KickClient(client, NetworkText.FromKey("CLI.ServerIsFull", Array.Empty<object>()));
				}
			}
			if (Netplay.FindNextOpenClientSlot() == -1)
			{
				Netplay.StopListening();
				Netplay.IsListening = false;
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0015E220 File Offset: 0x0015C420
		private static void KickClient(ISocket client, NetworkText kickMessage)
		{
			BinaryWriter writer = Netplay.fullBuffer.writer;
			if (writer == null)
			{
				Netplay.fullBuffer.ResetWriter();
				writer = Netplay.fullBuffer.writer;
			}
			writer.BaseStream.Position = 0L;
			long position = writer.BaseStream.Position;
			writer.BaseStream.Position += 2L;
			writer.Write(2);
			kickMessage.Serialize(writer);
			if (Main.dedServ)
			{
				Console.WriteLine(Language.GetTextValue("CLI.ClientWasBooted", client.GetRemoteAddress().ToString(), kickMessage));
			}
			int num = (int)writer.BaseStream.Position;
			writer.BaseStream.Position = position;
			writer.Write((short)num);
			writer.BaseStream.Position = (long)num;
			byte[] writeBuffer = Netplay.fullBuffer.writeBuffer;
			int offset = 0;
			int size = num;
			SocketSendCallback callback;
			if ((callback = Netplay.<>O.<0>__ServerFullWriteCallBack) == null)
			{
				callback = (Netplay.<>O.<0>__ServerFullWriteCallBack = new SocketSendCallback(Netplay.ServerFullWriteCallBack));
			}
			client.AsyncSend(writeBuffer, offset, size, callback, client);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0015E309 File Offset: 0x0015C509
		public static void OnConnectedToSocialServer(ISocket client)
		{
			Netplay.StartSocialClient(client);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0015E314 File Offset: 0x0015C514
		private static bool StartListening()
		{
			if (SocialAPI.Network != null)
			{
				NetSocialModule network = SocialAPI.Network;
				SocketConnectionAccepted callback;
				if ((callback = Netplay.<>O.<1>__OnConnectionAccepted) == null)
				{
					callback = (Netplay.<>O.<1>__OnConnectionAccepted = new SocketConnectionAccepted(Netplay.OnConnectionAccepted));
				}
				network.StartListening(callback);
			}
			ISocket tcpListener = Netplay.TcpListener;
			SocketConnectionAccepted callback2;
			if ((callback2 = Netplay.<>O.<1>__OnConnectionAccepted) == null)
			{
				callback2 = (Netplay.<>O.<1>__OnConnectionAccepted = new SocketConnectionAccepted(Netplay.OnConnectionAccepted));
			}
			return tcpListener.StartListening(callback2);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0015E373 File Offset: 0x0015C573
		private static void StopListening()
		{
			if (SocialAPI.Network != null)
			{
				SocialAPI.Network.StopListening();
			}
			Netplay.TcpListener.StopListening();
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0015E390 File Offset: 0x0015C590
		public static void StartServer()
		{
			Netplay.InitializeServer();
			ThreadStart start;
			if ((start = Netplay.<>O.<2>__ServerLoop) == null)
			{
				start = (Netplay.<>O.<2>__ServerLoop = new ThreadStart(Netplay.ServerLoop));
			}
			Netplay._serverThread = new Thread(start)
			{
				IsBackground = true,
				Name = "Server Loop Thread"
			};
			Netplay._serverThread.Start();
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0015E3E4 File Offset: 0x0015C5E4
		private static void InitializeServer()
		{
			Netplay.Connection.ResetSpecialFlags();
			Netplay.ResetNetDiag();
			if (Main.rand == null)
			{
				Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
			}
			Main.myPlayer = 255;
			Netplay.ServerIP = IPAddress.Any;
			Main.menuMode = 14;
			Main.statusText = Lang.menu[8].Value;
			Main.netMode = 2;
			Netplay.Disconnect = false;
			for (int i = 0; i < 256; i++)
			{
				Netplay.Clients[i] = new RemoteClient();
				Netplay.Clients[i].Reset();
				Netplay.Clients[i].Id = i;
				Netplay.Clients[i].ReadBuffer = new byte[1024];
			}
			Netplay.TcpListener = new TcpSocket();
			if (!Netplay.Disconnect)
			{
				if (!Netplay.StartListening())
				{
					Main.statusText = Language.GetTextValue("Error.TriedToRunServerTwice");
					Netplay.SaveOnServerExit = false;
					Netplay.Disconnect = true;
				}
				Main.statusText = Language.GetTextValue("CLI.ServerStarted");
			}
			if (!Netplay.UseUPNP)
			{
				return;
			}
			try
			{
				Netplay.OpenPort(Netplay.ListenPort);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0015E50C File Offset: 0x0015C70C
		private static void ServerLoop()
		{
			int num = 0;
			Netplay.StartBroadCasting();
			while (!Netplay.Disconnect)
			{
				Netplay.StartListeningIfNeeded();
				Netplay.UpdateConnectedClients();
				num = (num + 1) % 10;
				Thread.Sleep((num == 0) ? 1 : 0);
			}
			Netplay.StopBroadCasting();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0015E548 File Offset: 0x0015C748
		private static void UpdateConnectedClients()
		{
			int num = 0;
			for (int i = 0; i < 256; i++)
			{
				if (Netplay.Clients[i].PendingTermination)
				{
					if (Netplay.Clients[i].PendingTerminationApproved)
					{
						Netplay.Clients[i].State = 0;
						NetMessage.SyncDisconnectedPlayer(i);
						Netplay.Clients[i].Reset();
					}
				}
				else if (Netplay.Clients[i].IsConnected())
				{
					Netplay.Clients[i].Update();
					num++;
				}
				else if (Netplay.Clients[i].IsActive)
				{
					Netplay.Clients[i].SetPendingTermination("Connection lost");
					Netplay.Clients[i].PendingTerminationApproved = true;
				}
				else
				{
					Netplay.Clients[i].StatusText2 = "";
					if (i < 255)
					{
						bool active = Main.player[i].active;
						Main.player[i].active = false;
						if (active)
						{
							Player.Hooks.PlayerDisconnect(i);
						}
					}
				}
			}
			Netplay.HasClients = (num != 0);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0015E644 File Offset: 0x0015C844
		private static void StartListeningIfNeeded()
		{
			if (!Netplay.IsListening)
			{
				if (Netplay.Clients.Any((RemoteClient client) => !client.IsConnected()))
				{
					try
					{
						Netplay.StartListening();
						Netplay.IsListening = true;
					}
					catch
					{
						if (!Main.ignoreErrors)
						{
							throw;
						}
					}
					return;
				}
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0015E6B0 File Offset: 0x0015C8B0
		private static void UpdateClientInMainThread()
		{
			if (Main.netMode == 1 && Netplay.Connection.Socket.IsConnected() && !Netplay.Connection.ServerWantsToRunCheckBytesInClientLoopThread && NetMessage.buffer[256].checkBytes)
			{
				NetMessage.CheckBytes(256);
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0015E700 File Offset: 0x0015C900
		public static void AddCurrentServerToRecentList()
		{
			if (Netplay.Connection.Socket.GetRemoteAddress().Type != AddressType.Tcp)
			{
				return;
			}
			for (int i = 0; i < Main.maxMP; i++)
			{
				if (Main.recentIP[i].ToLower() == Netplay.ServerIPText.ToLower() && Main.recentPort[i] == Netplay.ListenPort)
				{
					for (int j = i; j < Main.maxMP - 1; j++)
					{
						Main.recentIP[j] = Main.recentIP[j + 1];
						Main.recentPort[j] = Main.recentPort[j + 1];
						Main.recentWorld[j] = Main.recentWorld[j + 1];
					}
				}
			}
			for (int num = Main.maxMP - 1; num > 0; num--)
			{
				Main.recentIP[num] = Main.recentIP[num - 1];
				Main.recentPort[num] = Main.recentPort[num - 1];
				Main.recentWorld[num] = Main.recentWorld[num - 1];
			}
			Main.recentIP[0] = Netplay.ServerIPText;
			Main.recentPort[0] = Netplay.ListenPort;
			Main.recentWorld[0] = Main.worldName;
			Main.SaveRecent();
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0015E80C File Offset: 0x0015CA0C
		public static void SocialClientLoop(object threadContext)
		{
			ISocket socket = (ISocket)threadContext;
			Netplay.ClientLoopSetup(socket.GetRemoteAddress());
			Netplay.Connection.Socket = socket;
			Netplay.InnerClientLoop();
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0015E83C File Offset: 0x0015CA3C
		public static void TcpClientLoop()
		{
			Netplay.ClientLoopSetup(new TcpAddress(Netplay.ServerIP, Netplay.ListenPort));
			Main.menuMode = 14;
			bool flag = true;
			while (flag)
			{
				flag = false;
				try
				{
					Netplay.Connection.Socket.Connect(new TcpAddress(Netplay.ServerIP, Netplay.ListenPort));
					flag = false;
				}
				catch
				{
					if (Platform.IsOSX)
					{
						Thread.Sleep(200);
						Netplay.Connection.Socket.Close();
						Netplay.Connection.Socket = new TcpSocket();
					}
					if (!Netplay.Disconnect && Main.gameMenu)
					{
						flag = true;
					}
				}
			}
			Netplay.InnerClientLoop();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0015E8E8 File Offset: 0x0015CAE8
		private static void ClientLoopSetup(RemoteAddress address)
		{
			string friendlyName = address.GetFriendlyName();
			if (string.IsNullOrEmpty(friendlyName))
			{
				Utils.ShowFancyErrorMessage("Unable to find Steam Hosted Multiplayer server.", 0, null);
			}
			Logging.Terraria.InfoFormat("Connecting to {0}", friendlyName);
			Netplay.Connection.ResetSpecialFlags();
			Netplay.ResetNetDiag();
			Main.ServerSideCharacter = false;
			if (Main.rand == null)
			{
				Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
			}
			Main.player[Main.myPlayer].hostile = false;
			Main.clientPlayer = Main.player[Main.myPlayer].clientClone();
			for (int i = 0; i < 255; i++)
			{
				if (i != Main.myPlayer)
				{
					Main.player[i] = new Player();
				}
			}
			Main.netMode = 1;
			Main.menuMode = 14;
			if (!Main.autoPass)
			{
				Main.statusText = Language.GetTextValue("Net.ConnectingTo", friendlyName);
			}
			Netplay.Disconnect = false;
			Netplay.Connection = new RemoteServer();
			Netplay.Connection.ReadBuffer = new byte[65535];
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0015E9E8 File Offset: 0x0015CBE8
		private static void InnerClientLoop()
		{
			try
			{
				NetMessage.buffer[256].Reset();
				int num = -1;
				while (!Netplay.Disconnect)
				{
					if (Netplay.Connection.Socket.IsConnected())
					{
						if (Netplay.Connection.ServerWantsToRunCheckBytesInClientLoopThread && NetMessage.buffer[256].checkBytes)
						{
							NetMessage.CheckBytes(256);
						}
						Netplay.Connection.IsActive = true;
						if (Netplay.Connection.State == 0)
						{
							Main.statusText = Language.GetTextValue("Net.FoundServer");
							Netplay.Connection.State = 1;
							NetMessage.SendData(1, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						}
						if (Netplay.Connection.State == 1 && ModNet.NetReloadActive && Main.netMode == 0)
						{
							Main.netMode = 1;
							Netplay.<InnerClientLoop>g__SendKeepAliveDuringModReloadMessage|51_0();
							Main.netMode = 0;
						}
						if (Netplay.Connection.State == 1 && Main.menuMode == 888 && Main.MenuUI.CurrentState == Interface.serverModsDifferMessage && Main.netMode == 1)
						{
							Netplay.<InnerClientLoop>g__SendKeepAliveDuringModReloadMessage|51_0();
						}
						if (Netplay.Connection.State == 2 && num != Netplay.Connection.State)
						{
							Main.statusText = Language.GetTextValue("Net.SendingPlayerData");
						}
						if (Netplay.Connection.State == 3 && num != Netplay.Connection.State)
						{
							Main.statusText = Language.GetTextValue("Net.RequestingWorldInformation");
						}
						if (Netplay.Connection.State == 4)
						{
							WorldGen.worldCleared = false;
							Netplay.Connection.State = 5;
							if (Main.cloudBGActive >= 1f)
							{
								Main.cloudBGAlpha = 1f;
							}
							else
							{
								Main.cloudBGAlpha = 0f;
							}
							Main.windSpeedCurrent = Main.windSpeedTarget;
							Cloud.resetClouds();
							Main.cloudAlpha = Main.maxRaining;
							Main.ToggleGameplayUpdates(false);
							WorldGen.clearWorld();
							SystemLoader.OnWorldLoad();
							if (Main.mapEnabled)
							{
								Main.Map.Load();
							}
						}
						if (Netplay.Connection.State == 5 && Main.loadMapLock)
						{
							float num2 = (float)Main.loadMapLastX / (float)Main.maxTilesX;
							Main.statusText = Lang.gen[68].Value + " " + ((int)(num2 * 100f + 1f)).ToString() + "%";
						}
						else if (Netplay.Connection.State == 5 && WorldGen.worldCleared)
						{
							Netplay.Connection.State = 6;
							Main.player[Main.myPlayer].FindSpawn();
							NetMessage.SendData(8, -1, -1, null, Main.player[Main.myPlayer].SpawnX, (float)Main.player[Main.myPlayer].SpawnY, 0f, 0f, 0, 0, 0);
						}
						if (Netplay.Connection.State == 6 && num != Netplay.Connection.State)
						{
							Main.statusText = Language.GetTextValue("Net.RequestingTileData");
						}
						if (!Netplay.Connection.IsReading && !Netplay.Disconnect && Netplay.Connection.Socket.IsDataAvailable())
						{
							Netplay.Connection.IsReading = true;
							Netplay.Connection.Socket.AsyncReceive(Netplay.Connection.ReadBuffer, 0, Netplay.Connection.ReadBuffer.Length, new SocketReceiveCallback(Netplay.Connection.ClientReadCallBack), null);
						}
						if (Netplay.Connection.StatusMax > 0 && Netplay.Connection.StatusText != "")
						{
							if (Netplay.Connection.StatusCount >= Netplay.Connection.StatusMax)
							{
								Main.statusText = Language.GetTextValue("Net.StatusComplete", Netplay.Connection.StatusText);
								Netplay.Connection.StatusText = "";
								Netplay.Connection.StatusMax = 0;
								Netplay.Connection.StatusCount = 0;
							}
							else
							{
								Main.statusText = Netplay.Connection.StatusText + ": " + ((int)((float)Netplay.Connection.StatusCount / (float)Netplay.Connection.StatusMax * 100f)).ToString() + "%";
							}
						}
						Thread.Sleep(1);
					}
					else if (Netplay.Connection.IsActive)
					{
						Main.statusText = Language.GetTextValue("Net.LostConnection");
						Netplay.Disconnect = true;
					}
					num = Netplay.Connection.State;
				}
				try
				{
					Netplay.Connection.Socket.Close();
				}
				catch
				{
				}
				if (!Main.gameMenu)
				{
					SystemLoader.OnWorldUnload();
					Main.gameMenu = true;
					Main.SwitchNetMode(0);
					MapHelper.noStatusText = true;
					Player.SavePlayer(Main.ActivePlayerFileData, false);
					Player.ClearPlayerTempInfo();
					Main.ActivePlayerFileData.StopPlayTimer();
					SoundEngine.StopTrackedSounds();
					MapHelper.noStatusText = false;
					Main.menuMode = 14;
				}
				NetMessage.buffer[256].Reset();
				if (Main.menuMode == 15 && Main.statusText == Language.GetTextValue("Net.LostConnection"))
				{
					Main.menuMode = 14;
				}
				if (Netplay.Connection.StatusText != "" && Netplay.Connection.StatusText != null)
				{
					Main.statusText = Language.GetTextValue("Net.LostConnection");
				}
				Netplay.Connection.StatusCount = 0;
				Netplay.Connection.StatusMax = 0;
				Netplay.Connection.StatusText = "";
				Main.SwitchNetMode(0);
			}
			catch (Exception value)
			{
				try
				{
					using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
					{
						streamWriter.WriteLine(DateTime.Now);
						streamWriter.WriteLine(value);
						streamWriter.WriteLine("");
					}
				}
				catch
				{
				}
				Netplay.Disconnect = true;
			}
			if (Netplay.OnDisconnect != null)
			{
				Netplay.OnDisconnect();
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0015EFCC File Offset: 0x0015D1CC
		private static int FindNextOpenClientSlot()
		{
			for (int i = 0; i < Main.maxNetPlayers; i++)
			{
				if (!Netplay.Clients[i].IsConnected())
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0015EFFA File Offset: 0x0015D1FA
		public static void StartSocialClient(ISocket socket)
		{
			ParameterizedThreadStart start;
			if ((start = Netplay.<>O.<3>__SocialClientLoop) == null)
			{
				start = (Netplay.<>O.<3>__SocialClientLoop = new ParameterizedThreadStart(Netplay.SocialClientLoop));
			}
			new Thread(start)
			{
				Name = "Social Client Thread",
				IsBackground = true
			}.Start(socket);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0015F034 File Offset: 0x0015D234
		public static void StartTcpClient()
		{
			ThreadStart start;
			if ((start = Netplay.<>O.<4>__TcpClientLoop) == null)
			{
				start = (Netplay.<>O.<4>__TcpClientLoop = new ThreadStart(Netplay.TcpClientLoop));
			}
			new Thread(start)
			{
				Name = "TCP Client Thread",
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0015F070 File Offset: 0x0015D270
		public static bool SetRemoteIP(string remoteAddress)
		{
			if (remoteAddress.Contains(':'))
			{
				int colonInd = remoteAddress.LastIndexOf(':');
				int port;
				if (int.TryParse(remoteAddress.AsSpan(colonInd + 1), out port))
				{
					remoteAddress = remoteAddress.Substring(0, colonInd);
					bool flag = Netplay.SetRemoteIPOld(remoteAddress);
					if (flag)
					{
						Netplay.ListenPort = port;
					}
					return flag;
				}
			}
			return Netplay.SetRemoteIPOld(remoteAddress);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0015F0C4 File Offset: 0x0015D2C4
		public static bool SetRemoteIPOld(string remoteAddress)
		{
			try
			{
				IPAddress address;
				if (IPAddress.TryParse(remoteAddress, out address))
				{
					Netplay.ServerIP = address;
					Netplay.ServerIPText = address.ToString();
					return true;
				}
				IPAddress[] addressList = Dns.GetHostEntry(remoteAddress).AddressList;
				for (int i = 0; i < addressList.Length; i++)
				{
					if (Netplay.AcceptedFamilyType(addressList[i].AddressFamily))
					{
						Netplay.ServerIP = addressList[i];
						Netplay.ServerIPText = remoteAddress;
						return true;
					}
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0015F144 File Offset: 0x0015D344
		public static void SetRemoteIPAsync(string remoteAddress, Action successCallBack)
		{
			try
			{
				IPAddress address;
				if (IPAddress.TryParse(remoteAddress, out address))
				{
					Netplay.ServerIP = address;
					Netplay.ServerIPText = address.ToString();
					successCallBack();
				}
				else
				{
					Netplay.InvalidateAllOngoingIPSetAttempts();
					AsyncCallback requestCallback;
					if ((requestCallback = Netplay.<>O.<5>__SetRemoteIPAsyncCallback) == null)
					{
						requestCallback = (Netplay.<>O.<5>__SetRemoteIPAsyncCallback = new AsyncCallback(Netplay.SetRemoteIPAsyncCallback));
					}
					Dns.BeginGetHostAddresses(remoteAddress, requestCallback, new Netplay.SetRemoteIPRequestInfo
					{
						RequestId = Netplay._currentRequestId,
						SuccessCallback = successCallBack,
						RemoteAddress = remoteAddress
					});
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0015F1D0 File Offset: 0x0015D3D0
		public static void InvalidateAllOngoingIPSetAttempts()
		{
			Netplay._currentRequestId++;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0015F1DE File Offset: 0x0015D3DE
		private static bool AcceptedFamilyType(AddressFamily family)
		{
			return family == AddressFamily.InterNetwork;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0015F1E8 File Offset: 0x0015D3E8
		private static void SetRemoteIPAsyncCallback(IAsyncResult ar)
		{
			Netplay.SetRemoteIPRequestInfo setRemoteIPRequestInfo = (Netplay.SetRemoteIPRequestInfo)ar.AsyncState;
			if (setRemoteIPRequestInfo.RequestId != Netplay._currentRequestId)
			{
				return;
			}
			try
			{
				bool flag = false;
				IPAddress[] array = Dns.EndGetHostAddresses(ar);
				for (int i = 0; i < array.Length; i++)
				{
					if (Netplay.AcceptedFamilyType(array[i].AddressFamily))
					{
						Netplay.ServerIP = array[i];
						Netplay.ServerIPText = setRemoteIPRequestInfo.RemoteAddress;
						flag = true;
						break;
					}
				}
				if (flag)
				{
					setRemoteIPRequestInfo.SuccessCallback();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0015F270 File Offset: 0x0015D470
		public static void Initialize()
		{
			if (Main.dedServ)
			{
				for (int i = 0; i < 257; i++)
				{
					if (i < 256)
					{
						Netplay.Clients[i] = new RemoteClient();
					}
					NetMessage.buffer[i] = new MessageBuffer();
					NetMessage.buffer[i].whoAmI = i;
				}
			}
			NetMessage.buffer[256] = new MessageBuffer();
			NetMessage.buffer[256].whoAmI = 256;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0015F2E6 File Offset: 0x0015D4E6
		public static void UpdateInMainThread()
		{
			if (Main.dedServ)
			{
				Netplay.UpdateServerInMainThread();
				return;
			}
			Netplay.UpdateClientInMainThread();
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0015F2FA File Offset: 0x0015D4FA
		public static int GetSectionX(int x)
		{
			return x / 200;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0015F303 File Offset: 0x0015D503
		public static int GetSectionY(int y)
		{
			return y / 150;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0015F30C File Offset: 0x0015D50C
		private static void BroadcastThread()
		{
			Netplay.BroadcastClient = new UdpClient();
			new IPEndPoint(IPAddress.Any, 0);
			Netplay.BroadcastClient.EnableBroadcast = true;
			new DateTime(0L);
			long num = 0L;
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					int value = 1010;
					binaryWriter.Write(value);
					binaryWriter.Write(Netplay.ListenPort);
					binaryWriter.Write(Main.worldName);
					string text = Dns.GetHostName();
					if (text == "localhost")
					{
						text = Environment.MachineName;
					}
					binaryWriter.Write(text);
					binaryWriter.Write((ushort)Main.maxTilesX);
					binaryWriter.Write(Main.ActiveWorldFileData.HasCrimson);
					binaryWriter.Write(Main.ActiveWorldFileData.GameMode);
					binaryWriter.Write((byte)Main.maxNetPlayers);
					num = memoryStream.Position;
					binaryWriter.Write(0);
					binaryWriter.Write(Main.ActiveWorldFileData.IsHardMode);
					binaryWriter.Flush();
					array = memoryStream.ToArray();
					goto IL_15B;
				}
			}
			IL_FB:
			int num2 = 0;
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					num2++;
				}
			}
			array[(int)num] = (byte)num2;
			try
			{
				Netplay.BroadcastClient.Send(array, array.Length, new IPEndPoint(IPAddress.Broadcast, 8888));
			}
			catch
			{
			}
			Thread.Sleep(1000);
			IL_15B:
			if (Netplay.abortBroadcastThread)
			{
				return;
			}
			goto IL_FB;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0015F4A4 File Offset: 0x0015D6A4
		public static void StartBroadCasting()
		{
			if (Netplay.broadcastThread != null)
			{
				Netplay.StopBroadCasting();
			}
			Netplay.abortBroadcastThread = false;
			ThreadStart start;
			if ((start = Netplay.<>O.<6>__BroadcastThread) == null)
			{
				start = (Netplay.<>O.<6>__BroadcastThread = new ThreadStart(Netplay.BroadcastThread));
			}
			Netplay.broadcastThread = new Thread(start);
			Netplay.broadcastThread.Start();
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0015F4F2 File Offset: 0x0015D6F2
		public static void StopBroadCasting()
		{
			if (Netplay.broadcastThread != null)
			{
				Netplay.abortBroadcastThread = true;
				while (Netplay.broadcastThread.IsAlive)
				{
					Thread.Sleep(1);
				}
				Netplay.broadcastThread = null;
			}
			if (Netplay.BroadcastClient != null)
			{
				Netplay.BroadcastClient.Close();
				Netplay.BroadcastClient = null;
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0015F5BE File Offset: 0x0015D7BE
		[CompilerGenerated]
		internal static void <InnerClientLoop>g__SendKeepAliveDuringModReloadMessage|51_0()
		{
			if (ModNet.NetReloadKeepAliveTimer.ElapsedMilliseconds > 30000L)
			{
				ModNet.NetReloadKeepAliveTimer.Restart();
				new ModPacket(253, 256).Send(-1, -1);
			}
		}

		// Token: 0x040007F7 RID: 2039
		public const int MaxConnections = 256;

		// Token: 0x040007F8 RID: 2040
		public const int NetBufferSize = 1024;

		// Token: 0x040007F9 RID: 2041
		public static string BanFilePath = "banlist.txt";

		// Token: 0x040007FA RID: 2042
		public static string ServerPassword = "";

		// Token: 0x040007FB RID: 2043
		public static RemoteClient[] Clients = new RemoteClient[256];

		// Token: 0x040007FC RID: 2044
		public static RemoteServer Connection = new RemoteServer();

		// Token: 0x040007FD RID: 2045
		public static IPAddress ServerIP;

		// Token: 0x040007FE RID: 2046
		public static string ServerIPText = "";

		// Token: 0x040007FF RID: 2047
		public static ISocket TcpListener;

		// Token: 0x04000800 RID: 2048
		public static int ListenPort = 7777;

		// Token: 0x04000801 RID: 2049
		public static bool IsListening = true;

		// Token: 0x04000802 RID: 2050
		public static bool UseUPNP = true;

		// Token: 0x04000803 RID: 2051
		public static bool SaveOnServerExit = true;

		// Token: 0x04000804 RID: 2052
		public static bool Disconnect;

		// Token: 0x04000805 RID: 2053
		public static bool SpamCheck = false;

		// Token: 0x04000806 RID: 2054
		public static bool HasClients;

		// Token: 0x04000807 RID: 2055
		private static Thread _serverThread;

		// Token: 0x04000808 RID: 2056
		public static UPnPNAT _upnpnat;

		// Token: 0x04000809 RID: 2057
		public static IStaticPortMappingCollection _mappings;

		// Token: 0x0400080A RID: 2058
		public static MessageBuffer fullBuffer = new MessageBuffer();

		// Token: 0x0400080B RID: 2059
		private static int _currentRequestId;

		// Token: 0x0400080C RID: 2060
		private static UdpClient BroadcastClient = null;

		// Token: 0x0400080D RID: 2061
		private static Thread broadcastThread = null;

		// Token: 0x0400080F RID: 2063
		private static bool abortBroadcastThread = false;

		// Token: 0x020007C1 RID: 1985
		private class SetRemoteIPRequestInfo
		{
			// Token: 0x040066C2 RID: 26306
			public int RequestId;

			// Token: 0x040066C3 RID: 26307
			public Action SuccessCallback;

			// Token: 0x040066C4 RID: 26308
			public string RemoteAddress;
		}

		// Token: 0x020007C2 RID: 1986
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040066C5 RID: 26309
			public static SocketSendCallback <0>__ServerFullWriteCallBack;

			// Token: 0x040066C6 RID: 26310
			public static SocketConnectionAccepted <1>__OnConnectionAccepted;

			// Token: 0x040066C7 RID: 26311
			public static ThreadStart <2>__ServerLoop;

			// Token: 0x040066C8 RID: 26312
			public static ParameterizedThreadStart <3>__SocialClientLoop;

			// Token: 0x040066C9 RID: 26313
			public static ThreadStart <4>__TcpClientLoop;

			// Token: 0x040066CA RID: 26314
			public static AsyncCallback <5>__SetRemoteIPAsyncCallback;

			// Token: 0x040066CB RID: 26315
			public static ThreadStart <6>__BroadcastThread;
		}
	}
}
