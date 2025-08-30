# Items 学習 (9-of-10) Mounts Items

対象: ExampleMod/Content/Items/Mounts/*
tModLoader: 1.4.4

要点
- ExampleMountItem
  - 基本: useTime/Animation=20, useStyle=Swing, noMelee=true, UseSound=Item79
  - Item.mountType = ModContent.MountType<ExampleMount>()
  - 価格/レア: sellPrice(gold:3), Rarity=Green
  - レシピ: ExampleItem + ExampleWorkbench

- ExampleMinecart
  - Item.mountType = ModContent.MountType<ExampleMinecartMount>()
  - サイズ: 34x22、価格:1g、Rarity=Blue
  - レシピ: Minecart + ExampleItem×15 + Anvils

パターン/注意
- マウント召喚アイテムは Item.mountType に MountType<T> を設定
- Minecart は既存 Minecart から強化するクラフト例（バニラ車両 + Mod 素材）
- 見た目/挙動は `Content/Mounts/` 側の `ModMount` 実装に依存（速度、ジャンプ、体勢、座標補正など）
