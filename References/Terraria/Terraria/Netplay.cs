using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.Map;
using Terraria.Net;
using Terraria.Net.Sockets;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x0200002F RID: 47
	public class Netplay
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000286 RID: 646 RVA: 0x0003C1C0 File Offset: 0x0003A3C0
		// (remove) Token: 0x06000287 RID: 647 RVA: 0x0003C1F4 File Offset: 0x0003A3F4
		public static event Action OnDisconnect;

		// Token: 0x06000288 RID: 648 RVA: 0x0003C228 File Offset: 0x0003A428
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

		// Token: 0x06000289 RID: 649 RVA: 0x0003C25C File Offset: 0x0003A45C
		private static string GetLocalIPAddress()
		{
			string result = "";
			foreach (IPAddress ipaddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
			{
				if (Netplay.AcceptedFamilyType(ipaddress.AddressFamily))
				{
					result = ipaddress.ToString();
					break;
				}
			}
			return result;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0003C2A8 File Offset: 0x0003A4A8
		private static void ResetNetDiag()
		{
			Main.ActiveNetDiagnosticsUI.Reset();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0003C2B4 File Offset: 0x0003A4B4
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

		// Token: 0x0600028C RID: 652 RVA: 0x0003C308 File Offset: 0x0003A508
		public static void AddBan(int plr)
		{
			RemoteAddress remoteAddress = Netplay.Clients[plr].Socket.GetRemoteAddress();
			using (StreamWriter streamWriter = new StreamWriter(Netplay.BanFilePath, true))
			{
				streamWriter.WriteLine("//" + Main.player[plr].name);
				streamWriter.WriteLine(remoteAddress.GetIdentifier());
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0003C378 File Offset: 0x0003A578
		public static bool IsBanned(RemoteAddress address)
		{
			try
			{
				string identifier = address.GetIdentifier();
				if (File.Exists(Netplay.BanFilePath))
				{
					using (StreamReader streamReader = new StreamReader(Netplay.BanFilePath))
					{
						string a;
						while ((a = streamReader.ReadLine()) != null)
						{
							if (a == identifier)
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

		// Token: 0x0600028E RID: 654 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private static void OpenPort(int port)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private static void ClosePort(int port)
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private static void ServerFullWriteCallBack(object state)
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0003C3F0 File Offset: 0x0003A5F0
		private static void OnConnectionAccepted(ISocket client)
		{
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
					Netplay.KickClient(client, NetworkText.FromKey("CLI.ServerIsFull", new object[0]));
				}
			}
			if (Netplay.FindNextOpenClientSlot() == -1)
			{
				Netplay.StopListening();
				Netplay.IsListening = false;
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0003C478 File Offset: 0x0003A678
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
			client.AsyncSend(Netplay.fullBuffer.writeBuffer, 0, num, new SocketSendCallback(Netplay.ServerFullWriteCallBack), client);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0003C552 File Offset: 0x0003A752
		public static void OnConnectedToSocialServer(ISocket client)
		{
			Netplay.StartSocialClient(client);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0003C55A File Offset: 0x0003A75A
		private static bool StartListening()
		{
			if (SocialAPI.Network != null)
			{
				SocialAPI.Network.StartListening(new SocketConnectionAccepted(Netplay.OnConnectionAccepted));
			}
			return Netplay.TcpListener.StartListening(new SocketConnectionAccepted(Netplay.OnConnectionAccepted));
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0003C590 File Offset: 0x0003A790
		private static void StopListening()
		{
			if (SocialAPI.Network != null)
			{
				SocialAPI.Network.StopListening();
			}
			Netplay.TcpListener.StopListening();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0003C5AD File Offset: 0x0003A7AD
		public static void StartServer()
		{
			Netplay.InitializeServer();
			Netplay._serverThread = new Thread(new ThreadStart(Netplay.ServerLoop))
			{
				IsBackground = true,
				Name = "Server Loop Thread"
			};
			Netplay._serverThread.Start();
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0003C5E8 File Offset: 0x0003A7E8
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

		// Token: 0x06000298 RID: 664 RVA: 0x0003C710 File Offset: 0x0003A910
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

		// Token: 0x06000299 RID: 665 RVA: 0x0003C750 File Offset: 0x0003A950
		private static void UpdateConnectedClients()
		{
			int num = 0;
			for (int i = 0; i < 256; i++)
			{
				if (Netplay.Clients[i].PendingTermination)
				{
					if (Netplay.Clients[i].PendingTerminationApproved)
					{
						Netplay.Clients[i].Reset();
						NetMessage.SyncDisconnectedPlayer(i);
					}
				}
				else if (Netplay.Clients[i].IsConnected())
				{
					Netplay.Clients[i].Update();
					num++;
				}
				else if (Netplay.Clients[i].IsActive)
				{
					Netplay.Clients[i].PendingTermination = true;
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

		// Token: 0x0600029A RID: 666 RVA: 0x0003C838 File Offset: 0x0003AA38
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
				}
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0003C8A4 File Offset: 0x0003AAA4
		private static void UpdateClientInMainThread()
		{
			if (Main.netMode != 1)
			{
				return;
			}
			if (!Netplay.Connection.Socket.IsConnected())
			{
				return;
			}
			if (!Netplay.Connection.ServerWantsToRunCheckBytesInClientLoopThread && NetMessage.buffer[256].checkBytes)
			{
				NetMessage.CheckBytes(256);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0003C8F4 File Offset: 0x0003AAF4
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
			for (int k = Main.maxMP - 1; k > 0; k--)
			{
				Main.recentIP[k] = Main.recentIP[k - 1];
				Main.recentPort[k] = Main.recentPort[k - 1];
				Main.recentWorld[k] = Main.recentWorld[k - 1];
			}
			Main.recentIP[0] = Netplay.ServerIPText;
			Main.recentPort[0] = Netplay.ListenPort;
			Main.recentWorld[0] = Main.worldName;
			Main.SaveRecent();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0003CA00 File Offset: 0x0003AC00
		public static void SocialClientLoop(object threadContext)
		{
			ISocket socket = (ISocket)threadContext;
			Netplay.ClientLoopSetup(socket.GetRemoteAddress());
			Netplay.Connection.Socket = socket;
			Netplay.InnerClientLoop();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0003CA30 File Offset: 0x0003AC30
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

		// Token: 0x0600029F RID: 671 RVA: 0x0003CADC File Offset: 0x0003ACDC
		private static void ClientLoopSetup(RemoteAddress address)
		{
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
				Main.statusText = Language.GetTextValue("Net.ConnectingTo", address.GetFriendlyName());
			}
			Netplay.Disconnect = false;
			Netplay.Connection = new RemoteServer();
			Netplay.Connection.ReadBuffer = new byte[1024];
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0003CBB4 File Offset: 0x0003ADB4
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
							if (Main.mapEnabled)
							{
								Main.Map.Load();
							}
						}
						if (Netplay.Connection.State == 5 && Main.loadMapLock)
						{
							float num2 = (float)Main.loadMapLastX / (float)Main.maxTilesX;
							Main.statusText = string.Concat(new object[]
							{
								Lang.gen[68].Value,
								" ",
								(int)(num2 * 100f + 1f),
								"%"
							});
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
								Main.statusText = string.Concat(new object[]
								{
									Netplay.Connection.StatusText,
									": ",
									(int)((float)Netplay.Connection.StatusCount / (float)Netplay.Connection.StatusMax * 100f),
									"%"
								});
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

		// Token: 0x060002A1 RID: 673 RVA: 0x0003D144 File Offset: 0x0003B344
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

		// Token: 0x060002A2 RID: 674 RVA: 0x0003D172 File Offset: 0x0003B372
		public static void StartSocialClient(ISocket socket)
		{
			new Thread(new ParameterizedThreadStart(Netplay.SocialClientLoop))
			{
				Name = "Social Client Thread",
				IsBackground = true
			}.Start(socket);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0003D19D File Offset: 0x0003B39D
		public static void StartTcpClient()
		{
			new Thread(new ThreadStart(Netplay.TcpClientLoop))
			{
				Name = "TCP Client Thread",
				IsBackground = true
			}.Start();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0003D1C7 File Offset: 0x0003B3C7
		public static bool SetRemoteIP(string remoteAddress)
		{
			return Netplay.SetRemoteIPOld(remoteAddress);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0003D1D0 File Offset: 0x0003B3D0
		public static bool SetRemoteIPOld(string remoteAddress)
		{
			try
			{
				IPAddress ipaddress;
				if (IPAddress.TryParse(remoteAddress, out ipaddress))
				{
					Netplay.ServerIP = ipaddress;
					Netplay.ServerIPText = ipaddress.ToString();
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

		// Token: 0x060002A6 RID: 678 RVA: 0x0003D250 File Offset: 0x0003B450
		public static void SetRemoteIPAsync(string remoteAddress, Action successCallBack)
		{
			try
			{
				IPAddress ipaddress;
				if (IPAddress.TryParse(remoteAddress, out ipaddress))
				{
					Netplay.ServerIP = ipaddress;
					Netplay.ServerIPText = ipaddress.ToString();
					successCallBack();
				}
				else
				{
					Netplay.InvalidateAllOngoingIPSetAttempts();
					Dns.BeginGetHostAddresses(remoteAddress, new AsyncCallback(Netplay.SetRemoteIPAsyncCallback), new Netplay.SetRemoteIPRequestInfo
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

		// Token: 0x060002A7 RID: 679 RVA: 0x0003D2CC File Offset: 0x0003B4CC
		public static void InvalidateAllOngoingIPSetAttempts()
		{
			Netplay._currentRequestId++;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0003D2DA File Offset: 0x0003B4DA
		private static bool AcceptedFamilyType(AddressFamily family)
		{
			return family == AddressFamily.InterNetwork;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0003D2E4 File Offset: 0x0003B4E4
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

		// Token: 0x060002AA RID: 682 RVA: 0x0003D36C File Offset: 0x0003B56C
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

		// Token: 0x060002AB RID: 683 RVA: 0x0003D3E2 File Offset: 0x0003B5E2
		public static void UpdateInMainThread()
		{
			if (Main.dedServ)
			{
				Netplay.UpdateServerInMainThread();
				return;
			}
			Netplay.UpdateClientInMainThread();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0003D3F6 File Offset: 0x0003B5F6
		public static int GetSectionX(int x)
		{
			return x / 200;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0003D3FF File Offset: 0x0003B5FF
		public static int GetSectionY(int y)
		{
			return y / 150;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0003D408 File Offset: 0x0003B608
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
				}
			}
			for (;;)
			{
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
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0003D59C File Offset: 0x0003B79C
		public static void StartBroadCasting()
		{
			if (Netplay.broadcastThread != null)
			{
				Netplay.StopBroadCasting();
			}
			Netplay.broadcastThread = new Thread(new ThreadStart(Netplay.BroadcastThread));
			Netplay.broadcastThread.Start();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0003D5CA File Offset: 0x0003B7CA
		public static void StopBroadCasting()
		{
			if (Netplay.broadcastThread != null)
			{
				Netplay.broadcastThread.Abort();
				Netplay.broadcastThread = null;
			}
			if (Netplay.BroadcastClient != null)
			{
				Netplay.BroadcastClient.Close();
				Netplay.BroadcastClient = null;
			}
		}

		// Token: 0x040001ED RID: 493
		public const int MaxConnections = 256;

		// Token: 0x040001EE RID: 494
		public const int NetBufferSize = 1024;

		// Token: 0x040001F0 RID: 496
		public static string BanFilePath = "banlist.txt";

		// Token: 0x040001F1 RID: 497
		public static string ServerPassword = "";

		// Token: 0x040001F2 RID: 498
		public static RemoteClient[] Clients = new RemoteClient[256];

		// Token: 0x040001F3 RID: 499
		public static RemoteServer Connection = new RemoteServer();

		// Token: 0x040001F4 RID: 500
		public static IPAddress ServerIP;

		// Token: 0x040001F5 RID: 501
		public static string ServerIPText = "";

		// Token: 0x040001F6 RID: 502
		public static ISocket TcpListener;

		// Token: 0x040001F7 RID: 503
		public static int ListenPort = 7777;

		// Token: 0x040001F8 RID: 504
		public static bool IsListening = true;

		// Token: 0x040001F9 RID: 505
		public static bool UseUPNP = true;

		// Token: 0x040001FA RID: 506
		public static bool SaveOnServerExit = true;

		// Token: 0x040001FB RID: 507
		public static bool Disconnect;

		// Token: 0x040001FC RID: 508
		public static bool SpamCheck = false;

		// Token: 0x040001FD RID: 509
		public static bool HasClients;

		// Token: 0x040001FE RID: 510
		private static Thread _serverThread;

		// Token: 0x040001FF RID: 511
		public static MessageBuffer fullBuffer = new MessageBuffer();

		// Token: 0x04000200 RID: 512
		private static int _currentRequestId;

		// Token: 0x04000201 RID: 513
		private static UdpClient BroadcastClient = null;

		// Token: 0x04000202 RID: 514
		private static Thread broadcastThread = null;

		// Token: 0x020004AF RID: 1199
		private class SetRemoteIPRequestInfo
		{
			// Token: 0x0400563E RID: 22078
			public int RequestId;

			// Token: 0x0400563F RID: 22079
			public Action SuccessCallback;

			// Token: 0x04005640 RID: 22080
			public string RemoteAddress;
		}
	}
}
