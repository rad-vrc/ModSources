ExampleMod BossBars/BossBarStyles/DamageClasses 学習まとめ:

- ExampleBossBar.cs (ModBossBar)
  - 目的: シンプルなボスバーのカスタム表示(色/アイコン/揺れ)
  - API: GetIconTexture(ref Rectangle? iconFrame) -> TextureAssets.NpcHead[36]、PreDraw(SpriteBatch, NPC, ref BossBarDrawParams)
  - 表現: 体力割合に応じた揺れ(drawParams.BarCenter変動)、アイコン色Main.DiscoColor、PreDrawでtrue返し

- ExampleBossBarStyle.cs (ModBossBarStyle)
  - 目的: メニューから選択可能な独自ボスバー描画スタイル
  - API: PreventDraw=true, Draw(SpriteBatch, IBigProgressBar currentBar, BigProgressBarInfo info)
  - ロジック: CommonBossBigProgressBarなら life% を算出して BigProgressBarHelper.DrawBareBonesBar で描画、showTextならテキスト描画。特殊バーは currentBar.Draw を委譲

- ExampleDamageClass.cs (DamageClass)
  - 目的: ダメージクラスの継承/効果継承/デフォルト補正/ツールチップ制御
  - API: GetModifierInheritance(DamageClass)->StatInheritanceData、GetEffectInheritance(DamageClass)、SetDefaultStats(Player)、UseStandardCritCalcs、ShowStatTooltipLine(Player, lineName)
  - 仕様: GenericはFull継承、それ以外はNone。Melee/Magicの効果継承true。デフォルトでCrit+4/ArmorPen+10。Speed行非表示、標準クリ計算を使用
