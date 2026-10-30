# Unity 卡牌战斗项目

一个基于 Unity 的卡牌肉鸽战斗原型（类《杀戮尖塔》），采用**事件驱动 + ScriptableObject 数据配置 + 对象池**架构，实现玩家/敌人回合制卡牌战斗、伤害结算、关卡推进与卡牌奖励选择。

## 环境要求

- Unity（建议 2021.3 LTS 及以上）
- **DOTween**（补间动画插件，需通过 `Tools > Demigiant > DOTween Utility Panel > Setup DOTween` 完成初始化）
- TextMeshPro / UGUI

## 目录结构
- Assets/Scripts/
- ├── BattleManager.cs # 战斗执行入口：根据卡牌ID调用对应功能
- ├── Button.cs # UI 按钮事件（开/关面板、回合结束、下一关、重开、退出）
- ├── DamageTextUI.cs # 伤害数字飘字（DOTween 位移+缩放）
- ├── Energy.cs # 能量系统（单例，消耗/初始化/不足提示）
- ├── EventHandler.cs # 全局静态事件总线
- ├── Health.cs # 血量、血条、受伤闪烁、死亡处理
- ├── TextUI.cs # 卡牌描述文本显示/隐藏
- ├── TurnManager.cs # 回合控制器（玩家回合/敌人回合流转）
- ├── VfxPlayer.cs # 技能特效播放（AnimatorOverrideController 切换）
- ├── Audio/
- │ ├── AudioController.cs # 音频控制器（BGM/SFX，跨场景常驻）
- │ ├── AudioData.cs # 音量持久化数据（ScriptableObject）
- │ └── AudioUI.cs # 音量滑条 UI
- ├── Card/
- │ ├── Card.cs # 单张卡牌：拖拽、悬停放大、打出
- │ ├── CardDate.cs # CardData 数据结构（图片/ID/数值/特效/功能）
- │ ├── CardManager.cs # 牌库/手牌/弃牌堆、对象池、扇形布局、发牌
- │ └── Card_SO.cs # 卡牌总表（ScriptableObject）
- ├── Enemy/
- │ ├── Enemy.cs # 敌人基类（动画、攻击协程）
- │ ├── EnemyAttack.cs # 随机技能攻击逻辑
- │ ├── EnemyAttack1.cs # 顺序循环技能攻击逻辑
- │ ├── EnemyDate.cs # 敌人数据（血量/动画/技能/类型）
- │ ├── EnemyIntent.cs # 意图图标显示
- │ ├── EnemyLogicBase.cs # 攻击逻辑基类 + IEnemyLogic 接口
- │ ├── EnemyManager.cs # 敌人对象池、群体攻击、死亡回收、胜利检测
- │ └── Enemy_SO.cs # 敌人总表（ScriptableObject）
- ├── Function/
- │ ├── Function.cs # 卡牌功能基类（ScriptableObject）
- │ ├── IFunction.cs # 功能接口
- │ ├── Attack.cs # 造成伤害
- │ ├── Heal.cs # 回复血量
- │ └── Lifesteal.cs # 吸血（伤害+回复一半）
- ├── Level/
- │ ├── LevelManager.cs # 关卡生成（小怪/精英/Boss）、难度递增
- │ └── LevelUI.cs # 关卡进度 UI
- ├── Result/
- │ ├── CardResult.cs # 奖励卡牌点击选择
- │ ├── ResultPanel.cs # 胜负结算面板
- │ └── VictoryResult.cs # 胜利后随机三选一卡牌
- └── SceneSwitch/
- ├── SceneSwitch.cs # 场景切换触发
- └── SceneSwitchSystem.cs # 场景切换执行（跨场景常驻）

## 核心架构

### 1. 事件总线（EventHandler）
所有模块通过静态事件解耦通信，避免直接引用。主要事件：

| 事件 | 触发者 | 订阅者 |
|---|---|---|
| `Execute` | Card 拖到敌人 / 敌人攻击 | BattleManager |
| `Deal` | TurnManager / LevelManager | CardManager |
| `RecycleCard` / `RecycleAllCard` | Card / TurnManager | CardManager |
| `EnemyAttack` | TurnManager | EnemyManager |
| `EnemyDeath` | Health | EnemyManager |
| `EnemySpaw` | LevelManager | EnemyManager |
| `DamageText` | Function 子类 | DamageTextUI |
| `Win` / `Lose` | EnemyManager / Health | ResultPanel |
| `NextLevel` | Button | LevelManager / ResultPanel |
| `CardID` | CardResult | CardManager |
| `Scene` | SceneSwitch | SceneSwitchSystem |

> 新增模块时优先通过 EventHandler 通信，并在 `OnEnable` 订阅、`OnDisable` 注销。

### 2. 数据配置（ScriptableObject）
- `Card_SO`：所有卡牌数据（`List<CardData>`）
- `Enemy_SO`：所有敌人数据（`List<EnemyDate>`）
- `AudioData`：音量持久化
- `Function` 子类（Attack/Heal/Lifesteal）：卡牌效果逻辑，可在 Inspector 中配置
- `EnemyLogicBase` 子类（EnemyAttack/EnemyAttack1）：敌人攻击行为

### 3. 对象池
- `CardManager`：卡牌对象池（`IObjectPool<GameObject>`）
- `EnemyManager`：敌人对象池

## 战斗流程
- 玩家回合
- ├─ TurnManager.OnPlayerTrun()
- │ ├─ CallDeal() → CardManager 发牌
- │ ├─ 开启"回合结束"按钮
- │ └─ Energy.InitEnergy()
- ├─ 玩家拖拽卡牌到敌人
- │ ├─ Energy.ConsumEnergy()
- │ ├─ CallExecute(player, enemy, ID) → BattleManager.Battle()
- │ │ ├─ 查 Card_SO 取 CardData
- │ │ ├─ 设置 VfxPlayer 动画
- │ │ └─ Function.Functions(Caster, Target)
- │ └─ CallRecycleCard() → 进弃牌堆
- └─ 点击"回合结束" → CallEnemyTurn()

- 敌人回合
- ├─ TurnManager.OnEnemyTurn()
- │ ├─ 关闭回合结束按钮
- │ ├─ CallRecycleAllCard()
- | └─ CallEnemyAttack() → EnemyManager.AllEnemyAttack()
- │ └─ 逐个敌人播放攻击动画 → EnemyLogic → CallExecute()
- └─ 敌人全部攻击完 → 回到玩家回合

- 敌人全部死亡
- └─ EnemyManager.CheckWin() → CallWin() → ResultPanel 显示胜利 + 三选一卡牌
- └─ 点击卡牌 → CallCardID() → CardManager 加入牌库
- └─ 点击下一关 → CallNextLevel() → LevelManager.LevelSpawn()
├── SceneSwitch.cs # 场景切换触发
└── SceneSwitchSystem.cs # 场景切换执行（跨场景常驻）
