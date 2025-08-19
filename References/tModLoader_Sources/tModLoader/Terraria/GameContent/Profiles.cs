using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x020004B1 RID: 1201
	public class Profiles
	{
		// Token: 0x02000BB4 RID: 2996
		public class StackedNPCProfile : ITownNPCProfile
		{
			// Token: 0x06005D8B RID: 23947 RVA: 0x006C8CFB File Offset: 0x006C6EFB
			public StackedNPCProfile(params ITownNPCProfile[] profilesInOrderOfVariants)
			{
				this._profiles = profilesInOrderOfVariants;
			}

			// Token: 0x06005D8C RID: 23948 RVA: 0x006C8D0A File Offset: 0x006C6F0A
			public int RollVariation()
			{
				return 0;
			}

			// Token: 0x06005D8D RID: 23949 RVA: 0x006C8D10 File Offset: 0x006C6F10
			public string GetNameForVariant(NPC npc)
			{
				int num = 0;
				if (this._profiles.IndexInRange(npc.townNpcVariationIndex))
				{
					num = npc.townNpcVariationIndex;
				}
				return this._profiles[num].GetNameForVariant(npc);
			}

			// Token: 0x06005D8E RID: 23950 RVA: 0x006C8D48 File Offset: 0x006C6F48
			public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
			{
				int num = 0;
				if (this._profiles.IndexInRange(npc.townNpcVariationIndex))
				{
					num = npc.townNpcVariationIndex;
				}
				return this._profiles[num].GetTextureNPCShouldUse(npc);
			}

			// Token: 0x06005D8F RID: 23951 RVA: 0x006C8D80 File Offset: 0x006C6F80
			public int GetHeadTextureIndex(NPC npc)
			{
				int num = 0;
				if (this._profiles.IndexInRange(npc.townNpcVariationIndex))
				{
					num = npc.townNpcVariationIndex;
				}
				return this._profiles[num].GetHeadTextureIndex(npc);
			}

			// Token: 0x040076E0 RID: 30432
			internal ITownNPCProfile[] _profiles;
		}

		// Token: 0x02000BB5 RID: 2997
		public class LegacyNPCProfile : ITownNPCProfile
		{
			// Token: 0x06005D90 RID: 23952 RVA: 0x006C8DB8 File Offset: 0x006C6FB8
			public LegacyNPCProfile(string npcFileTitleFilePath, int defaultHeadIndex, bool includeDefault = true, bool uniquePartyTexture = true)
			{
				this._rootFilePath = npcFileTitleFilePath;
				this._defaultVariationHeadIndex = defaultHeadIndex;
				if (!Main.dedServ)
				{
					this._defaultNoAlt = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + (includeDefault ? "_Default" : ""), 0);
					if (uniquePartyTexture)
					{
						this._defaultParty = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + (includeDefault ? "_Default_Party" : "_Party"), 0);
						return;
					}
					this._defaultParty = this._defaultNoAlt;
				}
			}

			// Token: 0x06005D91 RID: 23953 RVA: 0x006C8E3D File Offset: 0x006C703D
			public int RollVariation()
			{
				return 0;
			}

			// Token: 0x06005D92 RID: 23954 RVA: 0x006C8E40 File Offset: 0x006C7040
			public string GetNameForVariant(NPC npc)
			{
				return npc.getNewNPCName();
			}

			// Token: 0x06005D93 RID: 23955 RVA: 0x006C8E48 File Offset: 0x006C7048
			public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
			{
				if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
				{
					return this._defaultNoAlt;
				}
				if (npc.altTexture == 1)
				{
					return this._defaultParty;
				}
				return this._defaultNoAlt;
			}

			// Token: 0x06005D94 RID: 23956 RVA: 0x006C8E77 File Offset: 0x006C7077
			public int GetHeadTextureIndex(NPC npc)
			{
				return this._defaultVariationHeadIndex;
			}

			// Token: 0x040076E1 RID: 30433
			private string _rootFilePath;

			// Token: 0x040076E2 RID: 30434
			private int _defaultVariationHeadIndex;

			// Token: 0x040076E3 RID: 30435
			internal Asset<Texture2D> _defaultNoAlt;

			// Token: 0x040076E4 RID: 30436
			internal Asset<Texture2D> _defaultParty;
		}

		// Token: 0x02000BB6 RID: 2998
		public class TransformableNPCProfile : ITownNPCProfile
		{
			// Token: 0x06005D95 RID: 23957 RVA: 0x006C8E80 File Offset: 0x006C7080
			public TransformableNPCProfile(string npcFileTitleFilePath, int defaultHeadIndex, bool includeCredits = true)
			{
				this._rootFilePath = npcFileTitleFilePath;
				this._defaultVariationHeadIndex = defaultHeadIndex;
				if (!Main.dedServ)
				{
					this._defaultNoAlt = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default", 0);
					this._defaultTransformed = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default_Transformed", 0);
					if (includeCredits)
					{
						this._defaultCredits = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default_Credits", 0);
					}
				}
			}

			// Token: 0x06005D96 RID: 23958 RVA: 0x006C8EFF File Offset: 0x006C70FF
			public int RollVariation()
			{
				return 0;
			}

			// Token: 0x06005D97 RID: 23959 RVA: 0x006C8F02 File Offset: 0x006C7102
			public string GetNameForVariant(NPC npc)
			{
				return npc.getNewNPCName();
			}

			// Token: 0x06005D98 RID: 23960 RVA: 0x006C8F0A File Offset: 0x006C710A
			public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
			{
				if (npc.altTexture == 3 && this._defaultCredits != null)
				{
					return this._defaultCredits;
				}
				if (npc.IsABestiaryIconDummy)
				{
					return this._defaultNoAlt;
				}
				if (npc.altTexture == 2)
				{
					return this._defaultTransformed;
				}
				return this._defaultNoAlt;
			}

			// Token: 0x06005D99 RID: 23961 RVA: 0x006C8F49 File Offset: 0x006C7149
			public int GetHeadTextureIndex(NPC npc)
			{
				return this._defaultVariationHeadIndex;
			}

			// Token: 0x040076E5 RID: 30437
			private string _rootFilePath;

			// Token: 0x040076E6 RID: 30438
			private int _defaultVariationHeadIndex;

			// Token: 0x040076E7 RID: 30439
			internal Asset<Texture2D> _defaultNoAlt;

			// Token: 0x040076E8 RID: 30440
			internal Asset<Texture2D> _defaultTransformed;

			// Token: 0x040076E9 RID: 30441
			internal Asset<Texture2D> _defaultCredits;
		}

		// Token: 0x02000BB7 RID: 2999
		public class VariantNPCProfile : ITownNPCProfile
		{
			// Token: 0x06005D9A RID: 23962 RVA: 0x006C8F54 File Offset: 0x006C7154
			public VariantNPCProfile(string npcFileTitleFilePath, string npcBaseName, int[] variantHeadIds, params string[] variantTextureNames)
			{
				this._rootFilePath = npcFileTitleFilePath;
				this._npcBaseName = npcBaseName;
				this._variantHeadIDs = variantHeadIds;
				this._variants = variantTextureNames;
				foreach (string text in this._variants)
				{
					string text2 = this._rootFilePath + "_" + text;
					if (!Main.dedServ)
					{
						this._variantTextures[text2] = Main.Assets.Request<Texture2D>(text2, 0);
					}
				}
			}

			// Token: 0x06005D9B RID: 23963 RVA: 0x006C8FDC File Offset: 0x006C71DC
			public Profiles.VariantNPCProfile SetPartyTextures(params string[] variantTextureNames)
			{
				foreach (string text in variantTextureNames)
				{
					string text2 = this._rootFilePath + "_" + text + "_Party";
					if (!Main.dedServ)
					{
						this._variantTextures[text2] = Main.Assets.Request<Texture2D>(text2, 0);
					}
				}
				return this;
			}

			// Token: 0x06005D9C RID: 23964 RVA: 0x006C9034 File Offset: 0x006C7234
			public int RollVariation()
			{
				return Main.rand.Next(this._variants.Length);
			}

			// Token: 0x06005D9D RID: 23965 RVA: 0x006C9048 File Offset: 0x006C7248
			public string GetNameForVariant(NPC npc)
			{
				return Language.RandomFromCategory(this._npcBaseName + "Names_" + this._variants[npc.townNpcVariationIndex], WorldGen.genRand).Value;
			}

			// Token: 0x06005D9E RID: 23966 RVA: 0x006C9078 File Offset: 0x006C7278
			public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
			{
				string text = this._rootFilePath + "_" + this._variants[npc.townNpcVariationIndex];
				if (npc.IsABestiaryIconDummy)
				{
					return this._variantTextures[text];
				}
				if (npc.altTexture == 1 && this._variantTextures.ContainsKey(text + "_Party"))
				{
					return this._variantTextures[text + "_Party"];
				}
				return this._variantTextures[text];
			}

			// Token: 0x06005D9F RID: 23967 RVA: 0x006C90FC File Offset: 0x006C72FC
			public int GetHeadTextureIndex(NPC npc)
			{
				return this._variantHeadIDs[npc.townNpcVariationIndex];
			}

			// Token: 0x040076EA RID: 30442
			private string _rootFilePath;

			// Token: 0x040076EB RID: 30443
			private string _npcBaseName;

			// Token: 0x040076EC RID: 30444
			private int[] _variantHeadIDs;

			// Token: 0x040076ED RID: 30445
			private string[] _variants;

			// Token: 0x040076EE RID: 30446
			internal Dictionary<string, Asset<Texture2D>> _variantTextures = new Dictionary<string, Asset<Texture2D>>();
		}

		/// <summary>
		/// Class that is some-what identical to <seealso cref="T:Terraria.GameContent.Profiles.LegacyNPCProfile" /> that allows for
		/// modded texture usage. Also allows for any potential children classes to mess with the fields.
		/// </summary>
		// Token: 0x02000BB8 RID: 3000
		public class DefaultNPCProfile : ITownNPCProfile
		{
			// Token: 0x06005DA0 RID: 23968 RVA: 0x006C910C File Offset: 0x006C730C
			public DefaultNPCProfile(string texturePath, int headSlot, string partyTexturePath = null)
			{
				this.currentHeadSlot = headSlot;
				if (Main.dedServ)
				{
					return;
				}
				this.defaultTexture = ModContent.Request<Texture2D>(texturePath, 2);
				this.partyTexture = ((!string.IsNullOrEmpty(partyTexturePath)) ? ModContent.Request<Texture2D>(partyTexturePath, 2) : this.defaultTexture);
			}

			// Token: 0x06005DA1 RID: 23969 RVA: 0x006C9158 File Offset: 0x006C7358
			public int RollVariation()
			{
				return 0;
			}

			// Token: 0x06005DA2 RID: 23970 RVA: 0x006C915B File Offset: 0x006C735B
			public string GetNameForVariant(NPC npc)
			{
				return npc.getNewNPCName();
			}

			// Token: 0x06005DA3 RID: 23971 RVA: 0x006C9163 File Offset: 0x006C7363
			public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
			{
				if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
				{
					return this.defaultTexture;
				}
				if (npc.altTexture != 1)
				{
					return this.defaultTexture;
				}
				return this.partyTexture;
			}

			// Token: 0x06005DA4 RID: 23972 RVA: 0x006C9192 File Offset: 0x006C7392
			public int GetHeadTextureIndex(NPC npc)
			{
				return this.currentHeadSlot;
			}

			// Token: 0x040076EF RID: 30447
			protected int currentHeadSlot;

			// Token: 0x040076F0 RID: 30448
			protected Asset<Texture2D> defaultTexture;

			// Token: 0x040076F1 RID: 30449
			protected Asset<Texture2D> partyTexture;
		}
	}
}
