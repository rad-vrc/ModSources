using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x020001F0 RID: 496
	public class Profiles
	{
		// Token: 0x0200060A RID: 1546
		public class StackedNPCProfile : ITownNPCProfile
		{
			// Token: 0x06003342 RID: 13122 RVA: 0x00605820 File Offset: 0x00603A20
			public StackedNPCProfile(params ITownNPCProfile[] profilesInOrderOfVariants)
			{
				this._profiles = profilesInOrderOfVariants;
			}

			// Token: 0x06003343 RID: 13123 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
			public int RollVariation()
			{
				return 0;
			}

			// Token: 0x06003344 RID: 13124 RVA: 0x00605830 File Offset: 0x00603A30
			public string GetNameForVariant(NPC npc)
			{
				int num = 0;
				if (this._profiles.IndexInRange(npc.townNpcVariationIndex))
				{
					num = npc.townNpcVariationIndex;
				}
				return this._profiles[num].GetNameForVariant(npc);
			}

			// Token: 0x06003345 RID: 13125 RVA: 0x00605868 File Offset: 0x00603A68
			public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
			{
				int num = 0;
				if (this._profiles.IndexInRange(npc.townNpcVariationIndex))
				{
					num = npc.townNpcVariationIndex;
				}
				return this._profiles[num].GetTextureNPCShouldUse(npc);
			}

			// Token: 0x06003346 RID: 13126 RVA: 0x006058A0 File Offset: 0x00603AA0
			public int GetHeadTextureIndex(NPC npc)
			{
				int num = 0;
				if (this._profiles.IndexInRange(npc.townNpcVariationIndex))
				{
					num = npc.townNpcVariationIndex;
				}
				return this._profiles[num].GetHeadTextureIndex(npc);
			}

			// Token: 0x0400604C RID: 24652
			internal ITownNPCProfile[] _profiles;
		}

		// Token: 0x0200060B RID: 1547
		public class LegacyNPCProfile : ITownNPCProfile
		{
			// Token: 0x06003347 RID: 13127 RVA: 0x006058D8 File Offset: 0x00603AD8
			public LegacyNPCProfile(string npcFileTitleFilePath, int defaultHeadIndex, bool includeDefault = true, bool uniquePartyTexture = true)
			{
				this._rootFilePath = npcFileTitleFilePath;
				this._defaultVariationHeadIndex = defaultHeadIndex;
				if (Main.dedServ)
				{
					return;
				}
				this._defaultNoAlt = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + (includeDefault ? "_Default" : ""), 0);
				if (uniquePartyTexture)
				{
					this._defaultParty = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + (includeDefault ? "_Default_Party" : "_Party"), 0);
					return;
				}
				this._defaultParty = this._defaultNoAlt;
			}

			// Token: 0x06003348 RID: 13128 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
			public int RollVariation()
			{
				return 0;
			}

			// Token: 0x06003349 RID: 13129 RVA: 0x0060595E File Offset: 0x00603B5E
			public string GetNameForVariant(NPC npc)
			{
				return NPC.getNewNPCName(npc.type);
			}

			// Token: 0x0600334A RID: 13130 RVA: 0x0060596B File Offset: 0x00603B6B
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

			// Token: 0x0600334B RID: 13131 RVA: 0x0060599A File Offset: 0x00603B9A
			public int GetHeadTextureIndex(NPC npc)
			{
				return this._defaultVariationHeadIndex;
			}

			// Token: 0x0400604D RID: 24653
			private string _rootFilePath;

			// Token: 0x0400604E RID: 24654
			private int _defaultVariationHeadIndex;

			// Token: 0x0400604F RID: 24655
			internal Asset<Texture2D> _defaultNoAlt;

			// Token: 0x04006050 RID: 24656
			internal Asset<Texture2D> _defaultParty;
		}

		// Token: 0x0200060C RID: 1548
		public class TransformableNPCProfile : ITownNPCProfile
		{
			// Token: 0x0600334C RID: 13132 RVA: 0x006059A4 File Offset: 0x00603BA4
			public TransformableNPCProfile(string npcFileTitleFilePath, int defaultHeadIndex, bool includeCredits = true)
			{
				this._rootFilePath = npcFileTitleFilePath;
				this._defaultVariationHeadIndex = defaultHeadIndex;
				if (Main.dedServ)
				{
					return;
				}
				this._defaultNoAlt = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default", 0);
				this._defaultTransformed = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default_Transformed", 0);
				if (includeCredits)
				{
					this._defaultCredits = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default_Credits", 0);
				}
			}

			// Token: 0x0600334D RID: 13133 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
			public int RollVariation()
			{
				return 0;
			}

			// Token: 0x0600334E RID: 13134 RVA: 0x0060595E File Offset: 0x00603B5E
			public string GetNameForVariant(NPC npc)
			{
				return NPC.getNewNPCName(npc.type);
			}

			// Token: 0x0600334F RID: 13135 RVA: 0x00605A24 File Offset: 0x00603C24
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

			// Token: 0x06003350 RID: 13136 RVA: 0x00605A63 File Offset: 0x00603C63
			public int GetHeadTextureIndex(NPC npc)
			{
				return this._defaultVariationHeadIndex;
			}

			// Token: 0x04006051 RID: 24657
			private string _rootFilePath;

			// Token: 0x04006052 RID: 24658
			private int _defaultVariationHeadIndex;

			// Token: 0x04006053 RID: 24659
			internal Asset<Texture2D> _defaultNoAlt;

			// Token: 0x04006054 RID: 24660
			internal Asset<Texture2D> _defaultTransformed;

			// Token: 0x04006055 RID: 24661
			internal Asset<Texture2D> _defaultCredits;
		}

		// Token: 0x0200060D RID: 1549
		public class VariantNPCProfile : ITownNPCProfile
		{
			// Token: 0x06003351 RID: 13137 RVA: 0x00605A6C File Offset: 0x00603C6C
			public VariantNPCProfile(string npcFileTitleFilePath, string npcBaseName, int[] variantHeadIds, params string[] variantTextureNames)
			{
				this._rootFilePath = npcFileTitleFilePath;
				this._npcBaseName = npcBaseName;
				this._variantHeadIDs = variantHeadIds;
				this._variants = variantTextureNames;
				foreach (string str in this._variants)
				{
					string text = this._rootFilePath + "_" + str;
					if (!Main.dedServ)
					{
						this._variantTextures[text] = Main.Assets.Request<Texture2D>(text, 0);
					}
				}
			}

			// Token: 0x06003352 RID: 13138 RVA: 0x00605AF4 File Offset: 0x00603CF4
			public Profiles.VariantNPCProfile SetPartyTextures(params string[] variantTextureNames)
			{
				foreach (string str in variantTextureNames)
				{
					string text = this._rootFilePath + "_" + str + "_Party";
					if (!Main.dedServ)
					{
						this._variantTextures[text] = Main.Assets.Request<Texture2D>(text, 0);
					}
				}
				return this;
			}

			// Token: 0x06003353 RID: 13139 RVA: 0x00605B4C File Offset: 0x00603D4C
			public int RollVariation()
			{
				return Main.rand.Next(this._variants.Length);
			}

			// Token: 0x06003354 RID: 13140 RVA: 0x00605B60 File Offset: 0x00603D60
			public string GetNameForVariant(NPC npc)
			{
				return Language.RandomFromCategory(this._npcBaseName + "Names_" + this._variants[npc.townNpcVariationIndex], WorldGen.genRand).Value;
			}

			// Token: 0x06003355 RID: 13141 RVA: 0x00605B90 File Offset: 0x00603D90
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

			// Token: 0x06003356 RID: 13142 RVA: 0x00605C14 File Offset: 0x00603E14
			public int GetHeadTextureIndex(NPC npc)
			{
				return this._variantHeadIDs[npc.townNpcVariationIndex];
			}

			// Token: 0x04006056 RID: 24662
			private string _rootFilePath;

			// Token: 0x04006057 RID: 24663
			private string _npcBaseName;

			// Token: 0x04006058 RID: 24664
			private int[] _variantHeadIDs;

			// Token: 0x04006059 RID: 24665
			private string[] _variants;

			// Token: 0x0400605A RID: 24666
			internal Dictionary<string, Asset<Texture2D>> _variantTextures = new Dictionary<string, Asset<Texture2D>>();
		}
	}
}
