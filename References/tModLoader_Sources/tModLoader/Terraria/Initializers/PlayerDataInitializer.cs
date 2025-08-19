using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.Initializers
{
	// Token: 0x020003F0 RID: 1008
	public static class PlayerDataInitializer
	{
		// Token: 0x060034E2 RID: 13538 RVA: 0x00566B04 File Offset: 0x00564D04
		public static void Load()
		{
			TextureAssets.Players = new Asset<Texture2D>[PlayerVariantID.Count, PlayerTextureID.Count];
			PlayerDataInitializer.LoadStarterMale();
			PlayerDataInitializer.LoadStarterFemale();
			PlayerDataInitializer.LoadStickerMale();
			PlayerDataInitializer.LoadStickerFemale();
			PlayerDataInitializer.LoadGangsterMale();
			PlayerDataInitializer.LoadGangsterFemale();
			PlayerDataInitializer.LoadCoatMale();
			PlayerDataInitializer.LoadDressFemale();
			PlayerDataInitializer.LoadDressMale();
			PlayerDataInitializer.LoadCoatFemale();
			PlayerDataInitializer.LoadDisplayDollMale();
			PlayerDataInitializer.LoadDisplayDollFemale();
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x00566B64 File Offset: 0x00564D64
		private static void LoadVariant(int ID, int[] pieceIDs)
		{
			for (int i = 0; i < pieceIDs.Length; i++)
			{
				TextureAssets.Players[ID, pieceIDs[i]] = Main.Assets.Request<Texture2D>("Images/Player_" + ID.ToString() + "_" + pieceIDs[i].ToString(), 2);
			}
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x00566BBC File Offset: 0x00564DBC
		private static void CopyVariant(int to, int from)
		{
			for (int i = 0; i < PlayerTextureID.Count; i++)
			{
				TextureAssets.Players[to, i] = TextureAssets.Players[from, i];
			}
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x00566BF1 File Offset: 0x00564DF1
		private static void LoadStarterMale()
		{
			PlayerDataInitializer.LoadVariant(0, new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13,
				15
			});
			TextureAssets.Players[0, 14] = Asset<Texture2D>.Empty;
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x00566C1D File Offset: 0x00564E1D
		private static void LoadStickerMale()
		{
			PlayerDataInitializer.CopyVariant(1, 0);
			PlayerDataInitializer.LoadVariant(1, new int[]
			{
				4,
				6,
				8,
				11,
				12,
				13
			});
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x00566C3D File Offset: 0x00564E3D
		private static void LoadGangsterMale()
		{
			PlayerDataInitializer.CopyVariant(2, 0);
			PlayerDataInitializer.LoadVariant(2, new int[]
			{
				4,
				6,
				8,
				11,
				12,
				13
			});
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x00566C5D File Offset: 0x00564E5D
		private static void LoadCoatMale()
		{
			PlayerDataInitializer.CopyVariant(3, 0);
			PlayerDataInitializer.LoadVariant(3, new int[]
			{
				4,
				6,
				8,
				11,
				12,
				13,
				14
			});
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x00566C7D File Offset: 0x00564E7D
		private static void LoadDressMale()
		{
			PlayerDataInitializer.CopyVariant(8, 0);
			PlayerDataInitializer.LoadVariant(8, new int[]
			{
				4,
				6,
				8,
				11,
				12,
				13,
				14
			});
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x00566C9D File Offset: 0x00564E9D
		private static void LoadStarterFemale()
		{
			PlayerDataInitializer.CopyVariant(4, 0);
			PlayerDataInitializer.LoadVariant(4, new int[]
			{
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13
			});
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x00566CBE File Offset: 0x00564EBE
		private static void LoadStickerFemale()
		{
			PlayerDataInitializer.CopyVariant(5, 4);
			PlayerDataInitializer.LoadVariant(5, new int[]
			{
				4,
				6,
				8,
				11,
				12,
				13
			});
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x00566CDE File Offset: 0x00564EDE
		private static void LoadGangsterFemale()
		{
			PlayerDataInitializer.CopyVariant(6, 4);
			PlayerDataInitializer.LoadVariant(6, new int[]
			{
				4,
				6,
				8,
				11,
				12,
				13
			});
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x00566CFE File Offset: 0x00564EFE
		private static void LoadCoatFemale()
		{
			PlayerDataInitializer.CopyVariant(7, 4);
			PlayerDataInitializer.LoadVariant(7, new int[]
			{
				4,
				6,
				8,
				11,
				12,
				13,
				14
			});
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x00566D1E File Offset: 0x00564F1E
		private static void LoadDressFemale()
		{
			PlayerDataInitializer.CopyVariant(9, 4);
			PlayerDataInitializer.LoadVariant(9, new int[]
			{
				4,
				6,
				8,
				11,
				12,
				13
			});
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x00566D40 File Offset: 0x00564F40
		private static void LoadDisplayDollMale()
		{
			PlayerDataInitializer.CopyVariant(10, 0);
			PlayerDataInitializer.LoadVariant(10, new int[]
			{
				0,
				2,
				3,
				5,
				7,
				9,
				10
			});
			Asset<Texture2D> asset = TextureAssets.Players[10, 2];
			TextureAssets.Players[10, 2] = asset;
			TextureAssets.Players[10, 1] = asset;
			TextureAssets.Players[10, 4] = asset;
			TextureAssets.Players[10, 6] = asset;
			TextureAssets.Players[10, 11] = asset;
			TextureAssets.Players[10, 12] = asset;
			TextureAssets.Players[10, 13] = asset;
			TextureAssets.Players[10, 8] = asset;
			TextureAssets.Players[10, 15] = asset;
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x00566E00 File Offset: 0x00565000
		private static void LoadDisplayDollFemale()
		{
			PlayerDataInitializer.CopyVariant(11, 10);
			PlayerDataInitializer.LoadVariant(11, new int[]
			{
				3,
				5,
				7,
				9,
				10
			});
			Asset<Texture2D> asset = TextureAssets.Players[10, 2];
			TextureAssets.Players[11, 2] = asset;
			TextureAssets.Players[11, 1] = asset;
			TextureAssets.Players[11, 4] = asset;
			TextureAssets.Players[11, 6] = asset;
			TextureAssets.Players[11, 11] = asset;
			TextureAssets.Players[11, 12] = asset;
			TextureAssets.Players[11, 13] = asset;
			TextureAssets.Players[11, 8] = asset;
			TextureAssets.Players[11, 15] = asset;
		}
	}
}
