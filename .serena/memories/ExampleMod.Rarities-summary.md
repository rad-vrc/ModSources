ExampleMod/Content/Rarities 学習まとめ:

- ExampleModRarity.cs（ModRarity）
  - 目的: Mod専用レアリティ（淡色系）と前後段階の接頭辞補正対応。
  - API: RarityColor、GetPrefixedRarity(int offset, float valueMult)。offset>0 なら上位レア（ExampleHigherTierModRarity）へ、その他は自身(Type)。

- ExampleHigherTierModRarity.cs（ModRarity）
  - 目的: 上位段階のModレアリティ（Discoカラーの強弱変形）。
  - API: RarityColor（Main.DiscoR/G/B を除算調整）、GetPrefixedRarity。offset<0 なら下位へ（ExampleModRarity）、その他は自身(Type)。
