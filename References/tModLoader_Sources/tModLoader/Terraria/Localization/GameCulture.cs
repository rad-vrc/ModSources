using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Terraria.Localization
{
	// Token: 0x020003D4 RID: 980
	[DebuggerDisplay("{Name}")]
	public class GameCulture
	{
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x005543A9 File Offset: 0x005525A9
		public static IEnumerable<GameCulture> KnownCultures
		{
			get
			{
				return GameCulture._legacyCultures.Values;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x005543B5 File Offset: 0x005525B5
		// (set) Token: 0x06003377 RID: 13175 RVA: 0x005543BC File Offset: 0x005525BC
		public static GameCulture DefaultCulture { get; set; } = GameCulture._NamedCultures[GameCulture.CultureName.English];

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06003378 RID: 13176 RVA: 0x005543C4 File Offset: 0x005525C4
		public bool IsActive
		{
			get
			{
				return Language.ActiveCulture == this;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x005543CE File Offset: 0x005525CE
		public string Name
		{
			get
			{
				return this.CultureInfo.Name;
			}
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x005543DB File Offset: 0x005525DB
		public static GameCulture FromCultureName(GameCulture.CultureName name)
		{
			if (!GameCulture._NamedCultures.ContainsKey(name))
			{
				return GameCulture.DefaultCulture;
			}
			return GameCulture._NamedCultures[name];
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x005543FB File Offset: 0x005525FB
		public static GameCulture FromLegacyId(int id)
		{
			if (id < 1)
			{
				id = 1;
			}
			if (!GameCulture._legacyCultures.ContainsKey(id))
			{
				return GameCulture.DefaultCulture;
			}
			return GameCulture._legacyCultures[id];
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x00554424 File Offset: 0x00552624
		public static GameCulture FromName(string name)
		{
			return GameCulture._legacyCultures.Values.SingleOrDefault((GameCulture culture) => culture.Name == name) ?? GameCulture.DefaultCulture;
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x0055452F File Offset: 0x0055272F
		public GameCulture(string name, int legacyId)
		{
			this.CultureInfo = new CultureInfo(name);
			this.LegacyId = legacyId;
			GameCulture.RegisterLegacyCulture(this, legacyId);
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x00554551 File Offset: 0x00552751
		private static void RegisterLegacyCulture(GameCulture culture, int legacyId)
		{
			if (GameCulture._legacyCultures == null)
			{
				GameCulture._legacyCultures = new Dictionary<int, GameCulture>();
			}
			GameCulture._legacyCultures.Add(legacyId, culture);
		}

		// Token: 0x04001E43 RID: 7747
		private static Dictionary<GameCulture.CultureName, GameCulture> _NamedCultures = new Dictionary<GameCulture.CultureName, GameCulture>
		{
			{
				GameCulture.CultureName.English,
				new GameCulture("en-US", 1)
			},
			{
				GameCulture.CultureName.German,
				new GameCulture("de-DE", 2)
			},
			{
				GameCulture.CultureName.Italian,
				new GameCulture("it-IT", 3)
			},
			{
				GameCulture.CultureName.French,
				new GameCulture("fr-FR", 4)
			},
			{
				GameCulture.CultureName.Spanish,
				new GameCulture("es-ES", 5)
			},
			{
				GameCulture.CultureName.Russian,
				new GameCulture("ru-RU", 6)
			},
			{
				GameCulture.CultureName.Chinese,
				new GameCulture("zh-Hans", 7)
			},
			{
				GameCulture.CultureName.Portuguese,
				new GameCulture("pt-BR", 8)
			},
			{
				GameCulture.CultureName.Polish,
				new GameCulture("pl-PL", 9)
			}
		};

		// Token: 0x04001E44 RID: 7748
		private static Dictionary<int, GameCulture> _legacyCultures;

		// Token: 0x04001E45 RID: 7749
		public readonly CultureInfo CultureInfo;

		// Token: 0x04001E46 RID: 7750
		public readonly int LegacyId;

		// Token: 0x02000B18 RID: 2840
		public enum CultureName
		{
			// Token: 0x04006EFD RID: 28413
			English = 1,
			// Token: 0x04006EFE RID: 28414
			German,
			// Token: 0x04006EFF RID: 28415
			Italian,
			// Token: 0x04006F00 RID: 28416
			French,
			// Token: 0x04006F01 RID: 28417
			Spanish,
			// Token: 0x04006F02 RID: 28418
			Russian,
			// Token: 0x04006F03 RID: 28419
			Chinese,
			// Token: 0x04006F04 RID: 28420
			Portuguese,
			// Token: 0x04006F05 RID: 28421
			Polish,
			// Token: 0x04006F06 RID: 28422
			Unknown = 9999
		}
	}
}
