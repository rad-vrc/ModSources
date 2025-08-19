using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.Net.Sockets;

namespace Terraria
{
	// Token: 0x0200002D RID: 45
	public class RemoteClient
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0003B8BD File Offset: 0x00039ABD
		public bool IsConnected()
		{
			return this.Socket != null && this.Socket.IsConnected();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0003B8D4 File Offset: 0x00039AD4
		public void SpamUpdate()
		{
			if (!Netplay.SpamCheck)
			{
				this.SpamProjectile = 0f;
				this.SpamDeleteBlock = 0f;
				this.SpamAddBlock = 0f;
				this.SpamWater = 0f;
				return;
			}
			if (this.SpamProjectile > this.SpamProjectileMax)
			{
				NetMessage.BootPlayer(this.Id, NetworkText.FromKey("Net.CheatingProjectileSpam", new object[0]));
			}
			if (this.SpamAddBlock > this.SpamAddBlockMax)
			{
				NetMessage.BootPlayer(this.Id, NetworkText.FromKey("Net.CheatingTileSpam", new object[0]));
			}
			if (this.SpamDeleteBlock > this.SpamDeleteBlockMax)
			{
				NetMessage.BootPlayer(this.Id, NetworkText.FromKey("Net.CheatingTileRemovalSpam", new object[0]));
			}
			if (this.SpamWater > this.SpamWaterMax)
			{
				NetMessage.BootPlayer(this.Id, NetworkText.FromKey("Net.CheatingLiquidSpam", new object[0]));
			}
			this.SpamProjectile -= 0.4f;
			if (this.SpamProjectile < 0f)
			{
				this.SpamProjectile = 0f;
			}
			this.SpamAddBlock -= 0.3f;
			if (this.SpamAddBlock < 0f)
			{
				this.SpamAddBlock = 0f;
			}
			this.SpamDeleteBlock -= 5f;
			if (this.SpamDeleteBlock < 0f)
			{
				this.SpamDeleteBlock = 0f;
			}
			this.SpamWater -= 0.2f;
			if (this.SpamWater < 0f)
			{
				this.SpamWater = 0f;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0003BA61 File Offset: 0x00039C61
		public void SpamClear()
		{
			this.SpamProjectile = 0f;
			this.SpamAddBlock = 0f;
			this.SpamDeleteBlock = 0f;
			this.SpamWater = 0f;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0003BA90 File Offset: 0x00039C90
		public static void CheckSection(int playerIndex, Vector2 position, int fluff = 1)
		{
			int sectionX = Netplay.GetSectionX((int)(position.X / 16f));
			int sectionY = Netplay.GetSectionY((int)(position.Y / 16f));
			int num = 0;
			for (int i = sectionX - fluff; i < sectionX + fluff + 1; i++)
			{
				for (int j = sectionY - fluff; j < sectionY + fluff + 1; j++)
				{
					if (i >= 0 && i < Main.maxSectionsX && j >= 0 && j < Main.maxSectionsY && !Netplay.Clients[playerIndex].TileSections[i, j])
					{
						num++;
					}
				}
			}
			if (num > 0)
			{
				int num2 = num;
				NetMessage.SendData(9, playerIndex, -1, Lang.inter[44].ToNetworkText(), num2, 0f, 0f, 0f, 0, 0, 0);
				Netplay.Clients[playerIndex].StatusText2 = Language.GetTextValue("Net.IsReceivingTileData");
				Netplay.Clients[playerIndex].StatusMax += num2;
				for (int k = sectionX - fluff; k < sectionX + fluff + 1; k++)
				{
					for (int l = sectionY - fluff; l < sectionY + fluff + 1; l++)
					{
						NetMessage.SendSection(playerIndex, k, l);
					}
				}
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0003BBC0 File Offset: 0x00039DC0
		public bool SectionRange(int size, int firstX, int firstY)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = firstX;
				int num2 = firstY;
				if (i == 1)
				{
					num += size;
				}
				if (i == 2)
				{
					num2 += size;
				}
				if (i == 3)
				{
					num += size;
					num2 += size;
				}
				int sectionX = Netplay.GetSectionX(num);
				int sectionY = Netplay.GetSectionY(num2);
				if (this.TileSections[sectionX, sectionY])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0003BC1C File Offset: 0x00039E1C
		public void ResetSections()
		{
			for (int i = 0; i < Main.maxSectionsX; i++)
			{
				for (int j = 0; j < Main.maxSectionsY; j++)
				{
					this.TileSections[i, j] = false;
				}
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0003BC58 File Offset: 0x00039E58
		public void Reset()
		{
			this.ResetSections();
			if (this.Id < 255)
			{
				Main.player[this.Id] = new Player();
			}
			this.TimeOutTimer = 0;
			this.StatusCount = 0;
			this.StatusMax = 0;
			this.StatusText2 = "";
			this.StatusText = "";
			this.State = 0;
			this._isReading = false;
			this.PendingTermination = false;
			this.PendingTerminationApproved = false;
			this.SpamClear();
			this.IsActive = false;
			NetMessage.buffer[this.Id].Reset();
			if (this.Socket != null)
			{
				this.Socket.Close();
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0003BD03 File Offset: 0x00039F03
		public void ServerWriteCallBack(object state)
		{
			NetMessage.buffer[this.Id].spamCount--;
			if (this.StatusMax > 0)
			{
				this.StatusCount++;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0003BD35 File Offset: 0x00039F35
		public void Update()
		{
			if (!this.IsActive)
			{
				this.State = 0;
				this.IsActive = true;
			}
			this.TryRead();
			this.UpdateStatusText();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0003BD5C File Offset: 0x00039F5C
		private void TryRead()
		{
			if (this._isReading)
			{
				return;
			}
			try
			{
				if (this.Socket.IsDataAvailable())
				{
					this._isReading = true;
					this.Socket.AsyncReceive(this.ReadBuffer, 0, this.ReadBuffer.Length, new SocketReceiveCallback(this.ServerReadCallBack), null);
				}
			}
			catch
			{
				this.PendingTermination = true;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0003BDD0 File Offset: 0x00039FD0
		private void ServerReadCallBack(object state, int length)
		{
			if (!Netplay.Disconnect)
			{
				if (length == 0)
				{
					this.PendingTermination = true;
				}
				else
				{
					try
					{
						NetMessage.ReceiveBytes(this.ReadBuffer, length, this.Id);
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
			this._isReading = false;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0003BE2C File Offset: 0x0003A02C
		private void UpdateStatusText()
		{
			if (this.StatusMax > 0 && this.StatusText2 != "")
			{
				if (this.StatusCount >= this.StatusMax)
				{
					this.StatusText = Language.GetTextValue("Net.ClientStatusComplete", this.Socket.GetRemoteAddress(), this.Name, this.StatusText2);
					this.StatusText2 = "";
					this.StatusMax = 0;
					this.StatusCount = 0;
					return;
				}
				this.StatusText = string.Concat(new object[]
				{
					"(",
					this.Socket.GetRemoteAddress(),
					") ",
					this.Name,
					" ",
					this.StatusText2,
					": ",
					(int)((float)this.StatusCount / (float)this.StatusMax * 100f),
					"%"
				});
				return;
			}
			else
			{
				if (this.State == 0)
				{
					this.StatusText = Language.GetTextValue("Net.ClientConnecting", string.Format("({0}) {1}", this.Socket.GetRemoteAddress(), this.Name));
					return;
				}
				if (this.State == 1)
				{
					this.StatusText = Language.GetTextValue("Net.ClientSendingData", this.Socket.GetRemoteAddress(), this.Name);
					return;
				}
				if (this.State == 2)
				{
					this.StatusText = Language.GetTextValue("Net.ClientRequestedWorldInfo", this.Socket.GetRemoteAddress(), this.Name);
					return;
				}
				if (this.State != 3 && this.State == 10)
				{
					try
					{
						this.StatusText = Language.GetTextValue("Net.ClientPlaying", this.Socket.GetRemoteAddress(), this.Name);
					}
					catch (Exception)
					{
						this.PendingTermination = true;
					}
				}
				return;
			}
		}

		// Token: 0x040001CB RID: 459
		public ISocket Socket;

		// Token: 0x040001CC RID: 460
		public int Id;

		// Token: 0x040001CD RID: 461
		public string Name = "Anonymous";

		// Token: 0x040001CE RID: 462
		public bool IsActive;

		// Token: 0x040001CF RID: 463
		public bool PendingTermination;

		// Token: 0x040001D0 RID: 464
		public bool PendingTerminationApproved;

		// Token: 0x040001D1 RID: 465
		public bool IsAnnouncementCompleted;

		// Token: 0x040001D2 RID: 466
		public int State;

		// Token: 0x040001D3 RID: 467
		public int TimeOutTimer;

		// Token: 0x040001D4 RID: 468
		public string StatusText = "";

		// Token: 0x040001D5 RID: 469
		public string StatusText2;

		// Token: 0x040001D6 RID: 470
		public int StatusCount;

		// Token: 0x040001D7 RID: 471
		public int StatusMax;

		// Token: 0x040001D8 RID: 472
		public bool[,] TileSections = new bool[Main.maxTilesX / 200 + 1, Main.maxTilesY / 150 + 1];

		// Token: 0x040001D9 RID: 473
		public byte[] ReadBuffer;

		// Token: 0x040001DA RID: 474
		public float SpamProjectile;

		// Token: 0x040001DB RID: 475
		public float SpamAddBlock;

		// Token: 0x040001DC RID: 476
		public float SpamDeleteBlock;

		// Token: 0x040001DD RID: 477
		public float SpamWater;

		// Token: 0x040001DE RID: 478
		public float SpamProjectileMax = 100f;

		// Token: 0x040001DF RID: 479
		public float SpamAddBlockMax = 100f;

		// Token: 0x040001E0 RID: 480
		public float SpamDeleteBlockMax = 500f;

		// Token: 0x040001E1 RID: 481
		public float SpamWaterMax = 50f;

		// Token: 0x040001E2 RID: 482
		private volatile bool _isReading;
	}
}
