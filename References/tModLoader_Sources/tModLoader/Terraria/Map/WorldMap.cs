using System;
using System.IO;
using System.Runtime.CompilerServices;
using Terraria.IO;
using Terraria.ModLoader.IO;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria.Map
{
	// Token: 0x020003D3 RID: 979
	public class WorldMap
	{
		// Token: 0x17000634 RID: 1588
		public MapTile this[int x, int y]
		{
			get
			{
				return this._tiles[x, y];
			}
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x00553EC8 File Offset: 0x005520C8
		public WorldMap(int maxWidth, int maxHeight)
		{
			this.MaxWidth = maxWidth;
			this.MaxHeight = maxHeight;
			this._tiles = new MapTile[this.MaxWidth, this.MaxHeight];
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x00553EF5 File Offset: 0x005520F5
		public void ConsumeUpdate(int x, int y)
		{
			this._tiles[x, y].IsChanged = false;
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x00553F0A File Offset: 0x0055210A
		public void Update(int x, int y, byte light)
		{
			this._tiles[x, y] = MapHelper.CreateMapTile(x, y, light);
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x00553F21 File Offset: 0x00552121
		public void SetTile(int x, int y, ref MapTile tile)
		{
			this._tiles[x, y] = tile;
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x00553F36 File Offset: 0x00552136
		public bool IsRevealed(int x, int y)
		{
			return this._tiles[x, y].Light > 0;
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x00553F50 File Offset: 0x00552150
		public bool UpdateLighting(int x, int y, byte light)
		{
			MapTile other = this._tiles[x, y];
			if (light == 0 && other.Light == 0)
			{
				return false;
			}
			MapTile mapTile = MapHelper.CreateMapTile(x, y, Math.Max(other.Light, light));
			if (mapTile.Equals(ref other))
			{
				return false;
			}
			this._tiles[x, y] = mapTile;
			return true;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x00553FA8 File Offset: 0x005521A8
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

		// Token: 0x06003370 RID: 13168 RVA: 0x00553FF6 File Offset: 0x005521F6
		public void UnlockMapSection(int sectionX, int sectionY)
		{
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x00553FF8 File Offset: 0x005521F8
		public void Load()
		{
			Lighting.Clear();
			bool isCloudSave = Main.ActivePlayerFileData.IsCloudSave;
			if ((isCloudSave && SocialAPI.Cloud == null) || !Main.mapEnabled)
			{
				return;
			}
			string text = Main.playerPathName.Substring(0, Main.playerPathName.Length - 4) + Path.DirectorySeparatorChar.ToString();
			if (Main.ActiveWorldFileData.UseGuidAsMapName)
			{
				string text2 = text;
				text = text + Main.ActiveWorldFileData.UniqueId + ".map";
				if (!FileUtilities.Exists(text, isCloudSave))
				{
					text = text2 + Main.worldID.ToString() + ".map";
				}
			}
			else
			{
				text = text + Main.worldID.ToString() + ".map";
			}
			if (!FileUtilities.Exists(text, isCloudSave))
			{
				Main.MapFileMetadata = FileMetadata.FromCurrentSettings(FileType.Map);
				return;
			}
			using (MemoryStream input = FileUtilities.ReadAllBytes(text, isCloudSave).ToMemoryStream(false))
			{
				using (BinaryReader binaryReader = new BinaryReader(input))
				{
					try
					{
						int num = binaryReader.ReadInt32();
						if (num > 279)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(106, 2);
							defaultInterpolatedStringHandler.AppendLiteral("Map release version too high (");
							defaultInterpolatedStringHandler.AppendFormatted<int>(num);
							defaultInterpolatedStringHandler.AppendLiteral("), the map file '");
							defaultInterpolatedStringHandler.AppendFormatted(text);
							defaultInterpolatedStringHandler.AppendLiteral("' is either corrupted or from a future version of Terraria.");
							throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
						}
						if (num <= 91)
						{
							MapHelper.LoadMapVersion1(binaryReader, num);
						}
						else
						{
							MapHelper.LoadMapVersion2(binaryReader, num);
						}
						MapIO.ReadModFile(text, isCloudSave);
						this.ClearEdges();
						Main.clearMap = true;
						Main.loadMap = true;
						Main.loadMapLock = true;
						Main.refreshMap = false;
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
						Main.MapFileMetadata = FileMetadata.FromCurrentSettings(FileType.Map);
					}
				}
			}
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x0055425C File Offset: 0x0055245C
		public void Save()
		{
			MapHelper.SaveMap();
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x00554264 File Offset: 0x00552464
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

		// Token: 0x06003374 RID: 13172 RVA: 0x005542A8 File Offset: 0x005524A8
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

		// Token: 0x04001E3F RID: 7743
		public readonly int MaxWidth;

		// Token: 0x04001E40 RID: 7744
		public readonly int MaxHeight;

		// Token: 0x04001E41 RID: 7745
		public const int BlackEdgeWidth = 40;

		// Token: 0x04001E42 RID: 7746
		private MapTile[,] _tiles;
	}
}
