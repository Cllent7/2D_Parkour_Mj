Unity 复刻类天天酷跑 2D 横版跑酷游戏
一、核心玩法与角色控制系统
采用单例模式实现角色全局唯一控制（PlayerController.Instance），操作逻辑简洁直观：
跳跃机制：通过InputManager监听右侧屏幕点击，触发Jump()方法，支持二段跳（jumpCount限制次数），两次跳跃高度一致（共用speedJump参数），跳跃状态通过animator.SetBool("Jump", true)实时切换动画
下滑机制：点击左侧屏幕触发EnterSlides()，角色碰撞体高度减半（boxCollider2D.size动态调整），实现低矮障碍穿越；松开时调用ExitSlides()恢复碰撞体积，伴随 "Slide" 动画状态切换
状态管理：通过isGround（地面检测）、isSlides（滑行状态）、isJump（跳跃状态）等布尔变量精准控制角色行为，确保动作响应无延迟
二、无限地图生成系统
基于对象池模式实现高性能地图管理，核心逻辑由InfiniteMapGenerator与MapPoolManager协同完成：
地图片段管理：
每个地图片段（MapSegment）包含终点位置（EndPoint）和长度（Length）属性
初始化时生成initilSegments个片段，通过MapPoolManager.Instance.GetRandomSegment()从对象池获取预制体
动态生成规则：
当玩家距离当前最后一个片段终点小于 30f 时，触发新片段生成（SpawnNewSegment()）
新片段位置基于上一片段终点计算（spawnX = lastSegmentEndX），确保无缝衔接
资源回收：当活跃片段数量超过initilSegments + 1时，回收最左侧旧片段（RemoveOldSegment()），通过SetActive(false)实现对象复用
三、关卡与障碍设计
3.1 关卡差异化设计
第一关：目标累计 50 颗星星（Star）过关，通过Level_1_UIManager监听OnScoreUpdated事件，达到分数后显示通关面板（SuccessPanle）
第二关：无限循环模式，以奔跑距离为核心记录（DistanceManager），通过RankManager保存最近 3 次距离与最远记录
3.2 障碍系统实现
基础障碍：Obstacle类通过碰撞检测（OnTriggerEnter2D）触发死亡逻辑（GameEvents.TriggerPlayerDied()）
导弹障碍（MissileObstacle）：
玩家进入triggerDistance范围后激活飞行状态（isFlying = true）
沿固定方向（moveDirection = Vector3.left）以moveSpeed飞行，伴随导弹音效（MissileSfx()）
对象池管理：所有障碍与地图片段通过对象池复用，避免频繁实例化销毁导致的性能损耗
四、数据与 UI 系统
4.1 数据管理架构
核心数据载体：MyData（ScriptableObject）存储当前星星数（currentStar）、总星星数（TotalStars）、距离记录列表（RecentDistances）等关键数据
数据持久化：通过RankManager结合PlayerPrefs实现数据本地存储，包含：
最近 3 次奔跑距离（KEY_RECET_DISTANCES）
最远奔跑距离（KEY_FARTHEST_DISTANCE）
累计星星总数（TotalStars）
4.2 事件与 UI 交互
观察者模式应用：
GameEvents提供全局事件总线（OnScoreUpdated、OnPlayerDied等）
UI 面板通过订阅事件实现数据实时更新（如分数变化、死亡状态）
UI 模块组成：
公共模块（UICommonModule）：封装暂停 / 继续、分数显示等通用功能
关卡专属 UI：Level_1_UIManager与Level_2_UIManager分别处理对应关卡的胜利 / 失败逻辑
排行榜 UI（RankUI）：展示最近 3 次记录、最远记录及总星星数，通过UpdateRankText()刷新
五、音频与反馈系统
AudioManager单例统一管理所有音效：
动作音效：跳跃（JumpSfx）、滑行（可扩展）
物品音效：普通星星（StarSfx）、大星星（BigStarSfx）
障碍音效：碰撞（obstacleSfx）、导弹发射（MissileSfx）
UI 音效：按钮点击（ButtonSfx）
六、代码架构特点
单例模式：核心管理器（GameManager、AudioManager、InputManager等）均采用单例，确保全局唯一访问点
对象池模式：地图片段与障碍通过对象池复用，优化性能
事件驱动：基于GameEvents的观察者模式，降低模块间耦合
数据与逻辑分离：通过ScriptableObject（MyData）存储数据，逻辑由各管理器负责
可扩展性：关卡 UI 继承通用模块，障碍系统支持新增类型（仅需实现碰撞逻辑）
