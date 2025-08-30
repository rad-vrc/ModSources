ExampleMod/Content/Buffs フォルダ学習まとめ:

- AbsorbTeamDamageBuff.cs: ModBuff; 説明に装飾値埋込; Main.buffNoSave/NoTimeDisplay; ModPlayer(ExampleDamageModificationPlayer)にフラグ。
- AnimatedBuff.cs: ModBuff; アニメアイコン(PreDrawでTexture/Source差替); FrameCount/AnimationSpeed/Request<Texture2D>; Updateで全ダメ+10%（GenericDamageClass）。
- Blocky.cs: ModBuff; デバフ/Nurse不可; townNPCs>=1かつ装備フラグで強化と周期的アイテム付与、ジャンプ強化/落下耐性; 条件外なら自動解除。
- ExampleCrateBuff.cs: ModBuff; 釣り系フラグをModPlayerに付与。
- ExampleDefenseBuff.cs: ModBuff; 説明に+Def埋込; Updateで防御+10。
- ExampleDefenseDebuff.cs: ModBuff; PvP可; Ichor免疫継承; NPC/Player側に各フラグ、Playerは防御*0.75。
- ExampleDodgeBuff.cs: ModBuff; 保存しない; ModPlayerに回避フラグ。
- ExampleGravityDebuff.cs: ModBuff; NPC.GravityMultiplier をCos進行で変更。
- ExampleJavelinDebuff.cs: ModBuff; BoneJavelin免疫継承; DOT用GlobalNPCにフラグ。
- ExampleLifeRegenDebuff.cs (+ ModPlayer): デバフ/PvP/保存しない/Expert延長; ModPlayerでフラグ→UpdateBadLifeRegenで regen 0化/時間0化/毎秒8ダメ。
- ExampleMinecartBuff.cs: ModBuff; BuffID.Sets.BasicMountData で自動搭乗/NoTimeDisplay/NoSaveの面倒をTMLに任せる。表示名/説明はバニラ流用例。
- ExampleMountBuff.cs: ModBuff; NoTimeDisplay/NoSave; UpdateでSetMount+バフ時間リセット。
- ExampleWeaponImbue.cs: ModBuff; Flask系/近接専用/永続; ModPlayerに付与、MeleeEnchantActive=true。
- ExampleWhipDebuff.cs (+ ExampleWhipAdvancedDebuff/ExampleWhipDebuffNPC): TagBuff; 1つ目はFlat加算、2つ目は%加算し適用後に除去; SummonTagDamageMultiplierを考慮、プレイヤー由来攻撃のみ対象。
