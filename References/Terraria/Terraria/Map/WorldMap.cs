using System;
using System.IO;
using Terraria.IO;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria.Map
{
	// Token: 0x020000D1 RID: 209
	public class WorldMap
	{
		// Token: 0x170001CA RID: 458
		public MapTile this[int x, int y]
		{
			get
			{
				return this._tiles[x, y];
			}
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x004AD76F File Offset: 0x004AB96F
		public WorldMap(int maxWidth, int maxHeight)
		{
			this.MaxWidth = maxWidth;
			this.MaxHeight = maxHeight;
			this._tiles = new MapTile[this.MaxWidth, this.MaxHeight];
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x004AD79C File Offset: 0x004AB99C
		public void ConsumeUpdate(int x, int y)
		{
			this._tiles[x, y].IsChanged = false;
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x004AD7B1 File Offset: 0x004AB9B1
		public void Update(int x, int y, byte light)
		{
			this._tiles[x, y] = MapHelper.CreateMapTile(x, y, light);
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x004AD7C8 File Offset: 0x004AB9C8
		public void SetTile(int x, int y, ref MapTile tile)
		{
			this._tiles[x, y] = tile;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x004AD7DD File Offset: 0x004AB9DD
		public bool IsRevealed(int x, int y)
		{
			return this._tiles[x, y].Light > 0;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x004AD7F4 File Offset: 0x004AB9F4
		public bool UpdateLighting(int x, int y, byte light)
		{
			MapTile mapTile = this._tiles[x, y];
			if (light == 0 && mapTile.Light == 0)
			{
				return false;
			}
			MapTile mapTile2 = MapHelper.CreateMapTile(x, y, Math.Max(mapTile.Light, light));
			if (mapTile2.Equals(ref mapTile))
			{
				return false;
			}
			this._tiles[x, y] = mapTile2;
			return true;
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x004AD84C File Offset: 0x004ABA4C
		public bool UpdateType(int x, int y)
		{
			MapTile mapTile = MapHelper.CreateMapTile(x, y, this._tiles[x, y].Light);
			if (mapTile.Equals(ref this._tiles[x, y]))
			{
				return false;
			}
			this._tiles[x, y] = mapTile;
			return true;
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void UnlockMapSection(int sectionX, int sectionY)
		{
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x004AD89C File Offset: 0x004ABA9C
		public void Load()
		{
			Lighting.Clear();
			bool isCloudSave = Main.ActivePlayerFileData.IsCloudSave;
			if (isCloudSave && SocialAPI.Cloud == null)
			{
				return;
			}
			if (!Main.mapEnabled)
			{
				return;
			}
			string text = Main.playerPathName.Substring(0, Main.playerPathName.Length - 4) + Path.DirectorySeparatorChar.ToString();
			if (Main.ActiveWorldFileData.UseGuidAsMapName)
			{
				string arg = text;
				text = text + Main.ActiveWorldFileData.UniqueId + ".map";
				if (!FileUtilities.Exists(text, isCloudSave))
				{
					text = arg + Main.worldID + ".map";
				}
			}
			else
			{
				text = text + Main.worldID + ".map";
			}
			if (!FileUtilities.Exists(text, isCloudSave))
			{
				Main.MapFileMetadata = FileMetadata.FromCurrentSettings(FileType.Map);
				return;
			}
			using (MemoryStream memoryStream = new MemoryStream(FileUtilities.ReadAllBytes(text, isCloudSave)))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					try
					{
						int num = binaryReader.ReadInt32();
						if (num <= 279)
						{
							if (num <= 91)
							{
								MapHelper.LoadMapVersion1(binaryReader, num);
							}
							else
							{
								MapHelper.LoadMapVersion2(binaryReader, num);
							}
							this.ClearEdges();
							Main.clearMap = true;
							Main.loadMap = true;
							Main.loadMapLock = true;
							Main.refreshMap = false;
						}
					}
					catch (Exception value)
					{
						using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
						{
							streamWriter.WriteLine(DateTime.Now);
							streamWriter.WriteLine(value);
							streamWriter.WriteLine("");
						}
						if (!isCloudSave)
						{
							File.Copy(text, text + ".bad", true);
						}
						this.Clear();
					}
				}
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x004ADA80 File Offset: 0x004ABC80
		public void Save()
		{
			MapHelper.SaveMap();
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x004ADA88 File Offset: 0x004ABC88
		public void Clear()
		{
			for (int i = 0; i < this.MaxWidth; i++)
			{
				for (int j = 0; j < this.MaxHeight; j++)
				{
					this._tiles[i, j].Clear();
				}
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x004ADACC File Offset: 0x004ABCCC
		public void ClearEdges()
		{
			for (int i = 0; i < this.MaxWidth; i++)
			{
				for (int j = 0; j < 40; j++)
				{
					this._tiles[i, j].Clear();
				}
			}
			for (int k = 0; k < this.MaxWidth; k++)
			{
				for (int l = this.MaxHeight - 40; l < this.MaxHeight; l++)
				{
					this._tiles[k, l].Clear();
				}
			}
			for (int m = 0; m < 40; m++)
			{
				for (int n = 40; n < this.MaxHeight - 40; n++)
				{
					this._tiles[m, n].Clear();
				}
			}
			for (int num = this.MaxWidth - 40; num < this.MaxWidth; num++)
			{
				for (int num2 = 40; num2 < this.MaxHeight - 40; num2++)
				{
					this._tiles[num, num2].Clear();
				}
			}
		}

		// Token: 0x04001249 RID: 4681
		public readonly int MaxWidth;

		// Token: 0x0400124A RID: 4682
		public readonly int MaxHeight;

		// Token: 0x0400124B RID: 4683
		public const int BlackEdgeWidth = 40;

		// Token: 0x0400124C RID: 4684
		private MapTile[,] _tiles;
	}
}
