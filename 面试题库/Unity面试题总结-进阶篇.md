# Unity 面试题总结 - 进阶篇

> **整理时间**：2025年  
> **用途**：面试准备 + Unity进阶知识复习  
> **重点**：物理系统、UI系统、动画系统、协程、资源管理等进阶知识点

---

## 目录

### 💛物理系统
1. [CharacterController和Rigidbody的区别](#1-charactercontroller和rigidbody的区别)
2. [射线检测碰撞物的原理](#2-射线检测碰撞物的原理是)
3. [链条关节](#3-什么叫做链条关节)
4. [物体发生碰撞的必要条件](#4-物体发生碰撞的必要条件)
5. [碰撞过程的三个阶段](#5-在物体发生碰撞的整个过程中有几个阶段分别列出对应的函数-三个阶段)
6. [碰撞器和触发器的区别](#6-unity3d中的碰撞器和触发器的区别)
7. [施加力的方式](#7-unity3d的物理引擎中有几种施加力的方式分别描述出来)
8. [高速碰撞穿透问题](#8-当一个细小的高速物体撞向另一个较大的物体时会出现什么情况如何避免)
9. [物理更新函数](#9-物理更新一般放在哪个系统函数里)

### 💚UI & 2D 部分
1. [UGUI合批问题](#1-ugui-合批的一些问题)
2. [Image和RawImage的区别](#2-image和rawimage的区别)
3. [实现2D游戏的方式](#3-使用unity3d实现2d游戏有几种方式)
4. [Texture和Sprite的区别](#4-将图片的texturetype选项分别选为texture和sprite有什么区别)
5. [UI多分辨率适配](#5-请简述如何在不同分辨率下保持ui的一致性)
6. [Canvas的三种模式](#6-画布的三种模式缩放模式)
7. [Text和TMPText的区别](#7-text-和-tmptext的区别-优缺点)

### 💙动画系统
1. [游戏动画类型](#1-请描述游戏动画有哪几种以及其原理)
2. [Avatar的作用](#2-avator的作用)
3. [反向旋转动画](#3-反向旋转动画的方法是什么)
4. [Animation.CrossFade](#4-animationcrossfade-是什么)
5. [Animation的五个方法](#5-写出-animation-的五个方法)
6. [SkinnedMesh实现原理](#6-简述-skinnedmesh-的实现原理)
7. [动画层的作用](#7-动画层animation-layers的作用是什么)
8. [Animation和Animator的区别](#8-animation和animator的区别)

### 💜协程
1. [进程、线程、协程的概念](#1-简述进程线程协程的概念)
2. [协程的作用](#2-简述协程的作用)
3. [协程的底层原理](#3-简述协程的底层原理)
4. [线程与协程的区别](#4-线程与协程的区别)
5. [协同程序的执行代码](#5-协同程序的执行代码是什么有何用处有何缺点)

### 🤎数据持久化 & 资源管理
1. [Unity常用资源路径](#1-unity常用资源路径有哪些)
2. [资源迁移方法](#2-如何安全的在不同工程间安全地迁移asset数据三种方法)
3. [PlayerPrefs](#3-unity-提供了一个用于保存读取数据的类playerprefs请列出保存读取整形数据的函数)
4. [动态加载资源的方式](#4-动态加载资源的方式)
5. [AssetBundle打包](#5-assetsbundle-打包)
6. [AssetBundle加载](#6-assetbundle加载)
7. [AssetBundle卸载流程](#7-assetbundle卸载流程)
8. [ScriptableObject](#8-scriptableobejct)

---

## 💛物理系统

### 1. CharacterController和Rigidbody的区别

Rigidbody具有完全真实物理的特性，而CharacterController可以说是受限的Rigidbody，具有一定的物理效果但不是完全真实的。

---

### 2. 射线检测碰撞物的原理是？

**答**：射线是3D世界中一个点向一个方向发射的一条无终点的线，在发射轨迹中与其他物体发生碰撞时，它将停止发射。

Unity中的射线检测是基于三维空间中的线性投射原理来识别碰撞物的。

在Unity中，射线检测是一种利用线性几何学原理来确定一个物体是否与另一个物体发生碰撞的方法。具体来说，它涉及以下几个关键步骤：

1. **定义射线起点和方向**：首先需要确定射线的起点位置和发射方向。起点通常是在场景中的一个特定位置，而方向则定义了射线的路径。

2. **射线投射**：从起点沿指定方向发射一条无限长的线，这条线即为射线。

3. **检测碰撞**：射线在其路径上移动时，Unity会检测它是否与场景中带有碰撞器（Collider）的任何物体相交。碰撞器是一个附加在游戏对象上的组件，用于定义对象的物理形状，以便进行碰撞检测。

4. **获取碰撞信息**：一旦射线与某个物体的碰撞器发生交集，射线检测将停止，并且可以返回碰撞点的位置、碰撞物体的信息，以及碰撞发生的距离等信息。

5. **应用碰撞结果**：根据碰撞信息，开发者可以实现各种功能，例如判断子弹是否击中目标、触发特定事件或UI交互等。

**总结**：射线检测的核心在于通过线性投射的方式，快速确定某个方向上是否存在障碍物，并据此进行相应的逻辑处理。这种技术在游戏开发中非常常用，尤其是在处理用户输入和游戏物体之间的交互时。

---

### 3. 什么叫做链条关节?

**Hinge Joint**（铰链关节），可以模拟两个物体间用一根链条连接在一起的情况，能保持两个物体在一个固定距离内部相互移动而不产生作用力，但是达到固定距离后就会产生拉力。

**详细说明**：

在Unity的物理引擎中，关节（Joint）用于连接两个游戏对象，并限制或定义它们之间的运动方式。铰链关节是五大关节类型之一，它允许物体绕一个固定点进行旋转，类似于现实世界中的门铰链。以下是其主要特点：

- **锚点定位**：锚点定义了物体将围绕哪个点旋转。这个点是相对于附加了铰链关节的物体的局部坐标系而言的。
- **旋转轴向**：可以设定物体围绕哪个轴向进行旋转，如X、Y或Z轴。
- **弹性摆动**：如果启用了使用弹性的选项，物体会像摆动的钟摆一样自行摆动到目标位置。
- **断裂力**：可以设置一个力量值，当施加在物体上的力超过这个值时，铰链关节将会断裂，即物体会脱离铰链关节的限制。

总的来说，通过使用铰链关节，开发者可以在游戏中实现多种动态效果，比如门的开关、桥的升降等。这种关节类型非常适合于需要固定点旋转的场合，使得游戏内的物体运动看起来更加真实和自然。

---

### 4. 物体发生碰撞的必要条件？

两个物体都必须带有碰撞器Collider，其中一个物体还必须带有Rigidbody刚体。

---

### 5. 在物体发生碰撞的整个过程中，有几个阶段，分别列出对应的函数 三个阶段

- `OnCollisionEnter`
- `OnCollisionStay`
- `OnCollisionExit`

---

### 6. Unity3d中的碰撞器和触发器的区别?

碰撞器是触发器的载体，而触发器只是碰撞器身上的一个属性。

- **当Is Trigger=false时**：碰撞器根据物理引擎引发碰撞，产生碰撞的效果，可以调用`OnCollisionEnter/Stay/Exit`函数；

- **当Is Trigger=true时**：碰撞器被物理引擎所忽略，没有碰撞效果，可以调用`OnTriggerEnter/Stay/Exit`函数。

如果既要检测到物体的接触又不想让碰撞检测影响物体移动或要检测一个物件是否经过空间中的某个区域这时就可以用到触发器。

---

### 7. Unity3d的物理引擎中，有几种施加力的方式，分别描述出来

- `rigidbody.AddForce`
- `rigidbody.AddForceAtPosition`

都在 rigidbody系列函数中。

---

### 8. 当一个细小的高速物体撞向另一个较大的物体时，会出现什么情况?如何避免?

**穿透（碰撞检测失败）**

**解决方法**：例如CS射击游戏，可以使用开枪时发射射线，射线碰撞到则掉血击中。

---

### 9. 物理更新一般放在哪个系统函数里？

Unity中的物理更新一般放在**FixedUpdate**系统函数里。在Unity游戏开发中，物理引擎的更新是一个核心环节，它决定了游戏中刚体运动的仿真效果和准确性。FixedUpdate是Unity提供的一个专门用于物理计算的函数，它与普通渲染帧率解耦，保证了物理模拟的稳定性和一致性。

#### FixedUpdate的特点

- **固定时间调用**：不同于Update函数每一帧调用，FixedUpdate以固定的时间间隔被调用。这为物理计算提供了一个稳定的频率，避免了因帧率波动而影响物理仿真的效果。
- **适用于物理计算**：由于FixedUpdate的调用频率独立于渲染帧率，因此特别适合用于处理物理引擎的计算。这使得物理仿真更加平滑且可预测，尤其在性能较低的设备上依然能保持稳定。

#### FixedUpdate与Update的区别

- **调用频率不同**：Update每帧渲染时调用一次，而FixedUpdate则按照设定的固定时间间隔调用。这意味着在帧率不稳定的情况下，FixedUpdate能够提供更一致的物理计算频率。
- **适用场景差异**：Update更适合处理与渲染相关的逻辑，如UI更新、非物理驱动的游戏逻辑等；而FixedUpdate则更合适处理物理相关的操作，如刚体移动、力的作用等。

#### FixedUpdate的应用

- **刚体移动**：通过使用Rigidbody组件的MovePosition方法，可以在FixedUpdate中实现刚体的平滑移动，确保移动过程中正确处理碰撞。
- **力的作用**：在FixedUpdate中应用力到刚体上，可以保证力的施加在每个物理更新步骤中都得到考虑，从而产生连贯的运动效果。

#### FixedUpdate的设置

- **调整Fixed Timestep**：通过调整Project Settings中的Fixed Timestep值，可以改变FixedUpdate的调用间隔，从而优化物理计算的性能和精度。
- **Maximum Allowed Timestep**：设置最大允许的时间步长，以避免在性能低下时物理模拟过慢，平衡游戏体验和CPU开销。

#### Unity物理系统的高级特性

- **Physics类**：Unity的Physics类提供了一套丰富的API，用于控制物理系统的行为，包括力的施加、碰撞检测和触发器事件的处理等。
- **物理组件更新**：除了FixedUpdate外，Unity还提供了其他组件和接口，如Collider、Rigidbody等，它们在物理框架内各司其职，共同构成了完整的物理模拟体系。

**注意事项**：

- 在使用FixedUpdate进行物理计算时，应注意避免在其中执行与时间无关的操作，因为这可能导致不可预期的行为。
- 虽然FixedUpdate适用于物理计算，但如果物理计算需求不高，过度依赖FixedUpdate可能会无意中增加CPU负担。因此，合理评估物理计算的需求，并据此选择适当的更新策略是至关重要的。
- 对于复杂的物理仿真，考虑使用协程或其他技术来辅助实现更为复杂的物理模拟逻辑可能是一个不错的选择。

**总结**：将物理更新放入FixedUpdate中是Unity中处理物理计算的一种常见且推荐的做法。它利用了Unity提供的固定时间步长调用机制，保证了物理模拟的稳定性和准确性，特别是在需要精确物理仿真的游戏开发中显示出其重要性。通过合理配置FixedUpdate的参数，开发者可以优化游戏的性能，同时确保良好的用户体验。

---

## 💚UI & 2D 部分

### 1. UGUI 合批的一些问题

简单来说在一个Canvas下，需要相同的material，相同的纹理以及相同的Z值。

**常见问题**：
- UI上的字体Texture使用的是字体的图集，往往和我们自己的UI图集不一样，因此无法合批。
- UI的动态更新会影响网格的重绘，因此需要动静分离。

---

### 2. Image和RawImage的区别

| 特性 | Image | RawImage |
|------|-------|----------|
| 性能 | Image比RawImage更消耗性能 | 性能会比Image好很多 |
| 图片类型 | Image只能使用Sprite属性的图片 | RawImage什么样的都可以使用 |
| 适用场景 | Image适合放一些有操作的图片，裁剪平铺旋转什么的，针对Image Type属性 | RawImage就放单独展示的图片就可以 |

---

### 3. 使用Unity3d实现2d游戏，有几种方式？

1. **使用本身UGUI**：UGUI是Unity官方推出的最新UI系统，UI就是UserInterface。
2. **把摄像机的投影改为正交投影**：不考虑Z轴。
3. **使用Unity自身的2D模式**：在2d模式中，层级视图中只有一个正交摄像机，场景视图选择的是2D模式。
4. **使用2D Toolkit插件**：2D Toolkit是一组与Unity环境无缝集成的工具，提供高效的2D精灵和文本系统。

---

### 4. 将图片的TextureType选项分别选为Texture和Sprite有什么区别

- **Sprite**：作为UI精灵使用
- **Texture**：作用模型贴图使用

---

### 5. 请简述如何在不同分辨率下保持UI的一致性

多屏幕分辨率下的UI布局一般考虑两个问题：

1. **布局元素的位置**：即屏幕分辨率变化的情况下，布局元素的位置可能固定不动，导致布局元素可能超出边界；
2. **布局元素的尺寸**：即在屏幕分辨率变化的情况下，布局元素的大小尺寸可能会固定不变，导致布局元素之间出现重叠等功能。

为了解决这两个问题，在Unity UGUI体系中有两个组件可以来解决问题，分别是布局元素的Rect Transform和Canvas的Canvas Scaler组件。

**CanvasScaler中UI Scale Mode有三种模式**：
- **Constant Pixel Size**：使UI保持自己的尺寸，与屏幕尺寸无关
- **Scale With Screen Size**：根据屏幕分辨率来进行缩放适配（推荐）
- **Constant Physical Size**：使UI元素保持相同的物理大小，与屏幕尺寸无关

在这个模式下，有两个参数，一个是我们在开发过程中的标准分辨率，一个是屏幕的匹配模式，通过这里面的设置，就可以完成多分辨率下的适配问题。

---

### 6. 画布的三种模式.缩放模式

#### 三种模式

1. **屏幕空间-覆盖模式(Screen Space-Overlay)**：
   - Canvas创建出来后，默认就是该模式，该模式和摄像机无关，即使场景内没有摄像机，UI游戏物体照样渲染
   - 屏幕空间：电脑或者手机显示屏的2D空间，只有x轴和y轴
   - 覆盖模式：UI元素永远在3D元素的前面

2. **屏幕空间-摄像机模式(Screen Space-Camera)**：
   - 设置成该模式后需要指定一个摄像机游戏物体，指定后UGUI就会自动出现在该摄像机的"投射范围"内，和NGUI的默认UI Root效果一致，如果隐藏掉摄像机，UGUI当然就无法渲染

3. **世界空间模式(WorldSpace)**：
   - 设置成该模式后UGUI就相当于是场景内的一个普通的"Cube 游戏模型"，可以在场景内任意的移动UGUI元素的位置，通常用于怪物血条显示和VR开发

#### 缩放模式

| Property | Function |
|----------|----------|
| UI Scale Mode | Canvas中UI元素的缩放模式 |
| Constant Pixel Size | 使UI保持自己的尺寸，与屏幕尺寸无关 |
| Scale With Screen Size | 屏幕尺寸越大，UI越大 |
| Constant Physical Size | 使UI元素保持相同的物理大小，与屏幕尺寸无关 |

**注意**：Constant Pixel Size、Constant Physical Size实际上他们本质是一样的，只不过 Constant Pixel Size 通过逻辑像素大小调节来维持缩放，而 Constant Physical Size 通过物理大小调节来维持缩放。

---

### 7. Text 和 TMPText的区别 优缺点

| 特性 | Text | TMPText |
|------|------|---------|
| 渲染方式 | 像素渲染，放大之后就会模糊 | 网格渲染，不会模糊 |
| 父物体影响 | 使用Text父物体的放大缩小会影响子物体Text的清晰度 | TMPText不会 |
| 实现原理 | - | TMPText会把字体生成一个类似于贴图的东西然后读取贴图的坐标来获取对应的文字 |
| 更换文字消耗 | - | 更换文字的消耗会比Text大 |
| 适用场景 | 需要经常变动的文字用Text好点 | TMPText更适用于不会变动的文字，特别是在量大的情况下，性能比Text高一些 |
| 字体库 | - | TMPText在字体库很大的情况下查找更换会比较慢 |

---

## 💙动画系统

### 1. 请描述游戏动画有哪几种，以及其原理?

主要有关节动画、骨骼动画、单一网格模型动画(关键帧动画)。

1. **关节动画**：把角色分成若干独立部分，一个部分对应一个网格模型，部分的动画连接成一个整体的动画，角色比较灵活，Quake2中使用这种动画；

2. **骨骼动画**：广泛应用的动画方式，集成了以上两个方式的优点，骨骼按角色特点组成一定的层次结构，有关节相连，可做相对运动，皮肤作为单一网格蒙在骨骼之外，决定角色的外观；

3. **单一网格模型动画**：由一个完整的网格模型构成，在动画序列的关键帧里记录各个顶点的原位置及其改变量，然后插值运算实现动画效果，角色动画较真实。

---

### 2. Avator的作用

用户提供的模型骨架和Unity的骨架结构进行适配，是一种骨架映射关系。

**作用**：方便动画的重定向

**AnimationType有三种类型**：
- **Humanoid人型**：可以动画重定向，游戏对象挂载animator，子类原始模型+重定向模型，设置原始模型和使用模型的AnimationType为Humanoid类型
- **Generic非人型**
- **Legacy旧版**

**相关概念**：
- **Avatar Mask身体遮罩**：身体某一部分是否受到动画影响
- **反向动力学 IK**：通过手或脚来控制身体其他部分

---

### 3. 反向旋转动画的方法是什么？

将动画速度调成-1

```csharp
animation.speed = -1;
```

---

### 4. Animation.CrossFade 是什么？

动画淡入淡出

---

### 5. 写出 Animation 的五个方法

1. **AddClip**：将 clip 添加到名称为 newName 的动画中。
2. **Blend**：在后续 time 秒中将名称为 animation 的动画向 targetWeight 混合。
3. **CrossFade**：在后续 time 秒的时间段内，使名称为 animation 的动画淡入，使其他动画淡出。
4. **CrossFadeQueued**：使动画在上一个动画播放完成后交叉淡入淡出。
5. **IsPlaying**：名称为 name 的动画是否正在播放？
6. **PlayQueued**：在先前的动画播放完毕后再播放动画。
7. **RemoveClip**：从动画列表中移除剪辑。
8. **Sample**：对当前状态的动画进行采样。
9. **Stop**：停止所有使用该动画启动的正在播放的动画。

---

### 6. 简述 SkinnedMesh 的实现原理

**SkinnedMesh蒙皮网格动画**

分为骨骼和蒙皮两部分：
- **骨骼**：是一个层次结构，存储了骨骼的Transform数据
- **蒙皮**：是mesh顶点附着在骨骼之上，顶点可以被多个骨骼影响，决定了其权重等
- 还有将顶点从Mesh空间变换到骨骼空间

---

### 7. 动画层(Animation Layers)的作用是什么？

**动画分层**

身体部位动画分层，比如我只想动动头，身体其他部分不发生动画，可以方便处理动画区分。

---

### 8. Animation和Animator的区别

Animation和Animator 虽然都是控制动画的播放，但是它们的用法和相关语法都是大有不同的。

- **Animation**：控制一个动画的播放
- **Animator**：多个动画之间相互切换，并且Animator有一个动画控制器，俗称动画状态机

Animator利用它做动画的切换是很方便的，但是它有一个缺点就是占用内存比Animation大。

---

## 💜协程

### 1. 简述进程、线程、协程的概念

#### 进程

保存在硬盘上的程序运行以后，会在内存空间里形成一个独立的内存体，这个内存体有自己独立的地址空间，有自己的堆，不同进程间可以进行进程间通信，上级挂靠单位是操作系统。一个应用程序相当于一个进程，操作系统会以进程为单位，分配系统资源（CPU 时间片、内存等资源），进程是资源分配的最小单位。

#### 线程

线程从属于进程，也被称为轻量级进程，是程序的实际执行者。线程是操作系统能够进行运算调度的最小单位。它被包含在进程之中，是进程中的实际运作单位。一条线程指的是进程中一个单一顺序的控制流，一个进程中可以并发多个线程，每条线程并行执行不同的任务。一个线程只有一个进程。

每个独立的线程有一个程序运行的入口、顺序执行序列和程序的出口，但是线程不能够独立执行，必须依存在应用程序中，由应用程序提供多个线程执行控制。

线程拥有自己独立的栈和共享的堆，共享堆，不共享栈，线程亦由操作系统调度(标准线程是的)。

#### 协程

协程是伴随着主线程一起运行的一段程序。

协程与协程之间是并行执行，与主线程也是并行执行，同一时间只能执行一个协程提起协程，自然是要想到线程，因为协程的定义就是伴随主线程来运行的。

一个线程可以拥有多个协程，协程不是被操作系统内核所管理，而完全是由程序所控制。

协程和线程一样共享堆，不共享栈，协程由程序员在协程的代码里显示调度。

协成是单线程下由应用程序级别实现的并发。

---

### 2. 简述协程的作用

在Unity中只有主线程才能访问Unity3D的对象、方法、组件。当主线程在执行一个对资源消耗很大的操作时，在这一帧我们的程序就会出现帧率下降，画面卡顿的现象！

那这个时候我们就可以利用协程来做这件事，因为协程是伴随着主线程运行的，主线程依旧可以丝滑轻松的工作，把脏活累活交给协程处理就好了！简单来说：协程是辅助主线程的操作，避免游戏卡顿。

---

### 3. 简述协程的底层原理

协程是通过迭代器来实现功能的，通过关键字`IEnumerator`来定义一个迭代方法。

- `StartCoroutine` 接受到的是一个 `IEnumerator`，这是个接口，并且是枚举器或迭代器的意思。
- `yield` 是 C#的一个关键字，也是一个语法糖，背后的原理会生成一个类，并且也是一个枚举器，而且不同于 return，yield 可以出现多次。
- `yield` 实际上就是返回一次结果，因为我们要一次一次枚举一个值出来，所以多个 yield 其实是个状态模式，第一个 yield 是状态 1，第二个 yield 是状态 2，每次访问时会基于状态知道当前应该执行哪一个 yield，取得哪一个值。

从程序的角度讲，协程的核心就是迭代器。

想要定义一个协程方法有两个因素：
1. 方法的返回值为 `IEnumerator`
2. 方法中有 `yield`关键字

当代码满足以上两个条件时，此方法的执行就具有了迭代器的特质，其核心就是 `MoveNext`方法。

方法内的内容将会被分成两部分：yield 之前的代码和 yield 之后的代码。yield之前的代码会在第一次执行MoveNext时执行，yield之后的代码会在第二次执行MoveNext方法时执行。

而在Unity中，MoveNext的执行时机是以帧为单位的，无论你是设置了延迟时间，还是通过按钮调用MoveNext，亦或是根本没有设置执行条件，Unity都会在每一帧的生命周期中判断当前帧是否满足当前协程所定义的条件，一旦满足，当前帧就会抽出CPU时间执行你所定义的协程迭代器的MoveNext。

**注意**：只要方法中有yield语句，那么方法的返回值就必须是 `IEnumerator`，不然无法通过编译。

---

### 4. 线程与协程的区别

| 特性 | 协程 | 线程 |
|------|------|------|
| 执行方式 | 协作式程序，一系列互相依赖的协程间依次使用CPU，每次只有一个协程工作，而其他协程处于休眠状态 | 多线程是阻塞式的，每个IO都必须开启一个新的线程 |
| Unity对象访问 | 协程实际上是在一个线程中，只不过每个协程对CPU进行分时，协程可以访问和使用unity的所有方法和component | 但是对于多CPU的系统应该使用thread，尤其是有大量数据运算的时刻，但是IO密集型就不适合；而且thread中不能操作unity的很多方法和component |
| 执行数量 | 同一时间只能执行某个协程 | 同一时间可以同时执行多个线程 |
| 开销 | 开辟多个协程开销不大 | 开辟多条线程开销很大 |
| 适用场景 | 协程适合对某任务进行分时处理 | 线程适合多任务同时处理 |
| 本质 | 协同程序是通过协作来完成，在任一指定时刻只有一个协同程序在运行，并且这个正在运行的协同程序只在必要时才会被挂起 | 在多处理器情况下，从概念上来讲多线程程序同时运行多个线程 |

---

### 5. 协同程序的执行代码是什么？有何用处，有何缺点?

#### 启动协程

- `StartCoroutine(string methodName)`：通过协程的方法名(字符串形式)启动。
- `StartCoroutine(string methodName，object values)`：带参数的通过方法名(字符串形式)进行调用。
- `StartCoroutine(IEnumerator routine)`：通过调用方法的形式启动。

#### 停止协程

- `StopCoroutine(string methodName)`：通过方法名（字符串）来关闭协程。
- `StopCoroutine(IEnumerator routine)`：通过调用方法的形式来关闭协程。
- `StopCoroutine(Coroutine routine)`：通过指定的协程来关闭。
- `StopAllCoroutine()`：作用是停止所有该脚本中启动的协程。

#### 作用

一个协同程序在执行过程中，可以在任意位置使用yield语句。yield的返回值控制何时恢复协同程序向下执行。协同程序在对象自有帧执行过程中堪称优秀。协同程序在性能上没有更多的开销。

#### 缺点

协同程序并非真线程，可能会发生堵塞。

**示例代码**：
```csharp
function Start() {
    // - After 0 seconds, prints "Starting 0.0"
    // - After 0 seconds, prints "Before WaitAndPrint Finishes 0.0"
    // - After 2 seconds, prints "WaitAndPrint 2.0"
    // 先打印"Starting 0.0"和"Before WaitAndPrint Finishes 0.0"两句,2秒后打印"WaitAndPrint 2.0"
    print ("Starting " + Time.time );
    
    // Start function WaitAndPrint as a coroutine. And continue execution while it is running
    // 协同程序WaitAndPrint在Start函数内执行,可以视同于它与Start函数同步执行.
    StartCoroutine(WaitAndPrint(2.0));
    print ("Before WaitAndPrint Finishes " + Time.time );
}

function WaitAndPrint (waitTime : float) {
    // suspend execution for waitTime seconds
    // 暂停执行waitTime秒
    yield WaitForSeconds (waitTime);
    print ("WaitAndPrint "+ Time.time );
}
```

---

## 🤎数据持久化 & 资源管理

### 1. unity常用资源路径有哪些

```csharp
//获取的目录路径最后不包含  /
//获得的文件路径开头包含 /
Application.dataPath; //Asset文件夹的绝对路径
//只读
Application.streamingAssetsPath;  //StreamingAssets文件夹的绝对路径（要先判断是否存在这个文件夹路径）
Application.persistentDataPath; //可读写

//资源数据库 (AssetDatabase) 是允许您访问工程中的资源的 API
AssetDatabase.GetAllAssetPaths(); //获取所有的资源文件（不包含meta文件）
AssetDatabase.GetAssetPath(object); //获取object对象的相对路径
AssetDatabase.Refresh(); //刷新
AssetDatabase.GetDependencies(string); //获取依赖项文件

Directory.Delete(p, true); //删除P路径目录
Directory.Exists(p);  //是否存在P路径目录
Directory.CreateDirectory(p); //创建P路径目录

// AssetDatabase //类库，对Asset文件夹下的文件进行操作，获取相对路径，获取所有文件，获取相对依赖项
// Directory //类库，相关文件夹路径目录进行操作，是否存在，创建目录，删除等操作
```

---

### 2. 如何安全的在不同工程间安全地迁移asset数据?三种方法

1. 将Assets目录和Library目录一起迁移
2. 导出包（package）
3. 用unity自带的assets Server功能

---

### 3. unity 提供了一个用于保存读取数据的类，（playerPrefs），请列出保存读取整形数据的函数

PlayerPrefs类是一个本地持久化保存与读取数据的类

PlayerPrefs类支持3中数据类型的保存和读取，浮点型，整形，和字符串型。

分别对应的函数为：

- `SetInt()`：保存整型数据；`GetInt()`：读取整形数据；
- `SetFloat()`：保存浮点型数据； `GetFloat()`：读取浮点型数据；
- `SetString()`：保存字符串型数据； `GetString()`：读取字符串型数据；

---

### 4. 动态加载资源的方式?

1. **Instantiate**：最简单的一种方式，以实例化的方式动态生成一个物体。
2. **AssetBundle**：即将资源打成 asset bundle 放在服务器或本地磁盘，然后使用WWW模块get下来，然后从这个bundle中load某个object，unity官方推荐也是绝大多数商业化项目使用的一种方式。
3. **Resource.Load**：可以直接load并返回某个类型的Object，前提是要把这个资源放在Resource命名的文件夹下，Unity不管有没有场景引用，都会将其全部打入到安装包中
4. **AssetDatabase.loadasset**：这种方式只在editor范围内有效，游戏运行时没有这个函数，它通常是在开发中调试用的。

---

### 5. AssetsBundle 打包

```csharp
using UnityEditor;
using System.IO;

public class CreateAssetBundles //进行AssetBundle打包
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string dir = "AssetBundles";
        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);
        }
        
        BuildPipeline.BuildAssetBundles(dir, //路径必须创建
            BuildAssetBundleOptions.ChunkBasedCompression, //压缩类型***
            BuildTarget.StandaloneWindows64);//平台***
    }
}
```

**压缩类型说明**：

| 选项 | 说明 |
|------|------|
| None | Build assetBundle without any special option.（LZMA压缩，压缩率高，解压久） |
| UncompressedAssetBundle | Don't compress the data when creating the asset bundle.（不压缩，解压快） |
| ChunkBasedCompression | Use chunk-based LZ4 compression when creating the AssetBundle.（压缩率比LZMA低，解压速度接近无压缩） |

---

### 6. AssetBundle加载

#### 第一种：LoadFromMemory(LoadFromMemoryAsync)

```csharp
IEnumerator Start()
{
    string path = "AssetBundles/wall.unity3d";
    
    AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));
    
    yield return request;
    
    AssetBundle ab = request.assetBundle;
    
    GameObject wallPrefab = ab.LoadAsset<GameObject>("Cube");
    
    Instantiate(wallPrefab);
}
```

#### 第二种：LoadFromFile（LoadFromFileAsync）

```csharp
IEnumerator Start()
{
    string path = "AssetBundles/wall.unity3d";
    
    AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(path);
    
    yield return request;
    
    AssetBundle ab = request.assetBundle;
    
    GameObject wallPrefab = ab.LoadAsset<GameObject>("Cube");
    
    Instantiate(wallPrefab);
}
```

#### 第三种：UnityWebRequest

```csharp
IEnumerator Start()
{
    string uri = @"http://localhost/AssetBundles/cubewall.unity3d";
    
    UnityWebRequest request = UnityWebRequest.GetAssetBundle(uri);
    
    yield return request.Send();
    
    AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
    
    GameObject wallPrefab = ab.LoadAsset<GameObject>("Cube");
    
    Instantiate(wallPrefab);
}
```

#### 第四种：WWW（无依赖）

```csharp
private IEnumerator LoadNoDepandenceAsset()
{
    string path = "";
    
    if (loadLocal)
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        path += "File:///";
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        path += "File://";
#endif
        path += assetBundlePath + "/" + assetBundleName;
        
        //www对象
        WWW www = new WWW(path);
        
        //等待下载【到内存】
        yield return www;
        
        //获取到AssetBundle
        AssetBundle bundle = www.assetBundle;
        
        //加载资源
        GameObject prefab = bundle.LoadAsset<GameObject>(assetRealName);
        
        //Test:实例化
        Instantiate(prefab);
    }
}
```

#### 第四种：WWW（有依赖）

```csharp
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAssetsDemo : MonoBehaviour
{
    [Header("版本号")]
    public int version = 1;
    
    [Header("加载本地资源")]
    public bool loadLocal = true;
    [Header("资源的bundle名称")]
    public string assetBundleName;
    [Header("资源的真正的文件名称")]
    public string assetRealName;
    
    //bundle所在的路径
    private string assetBundlePath;
    //bundle所在的文件夹名称
    private string assetBundleRootName;
    
    private void Awake()
    {
        assetBundlePath = Application.dataPath + "/OutputAssetBundle";
        assetBundleRootName = assetBundlePath.Substring(assetBundlePath.LastIndexOf("/") + 1);
        
        Debug.Log(assetBundleRootName);
    }
    
    IEnumerator LoadAssetsByWWW()
    {
        string path = "";
        //判断是不是本地加载
        if (loadLocal) // loadLocal=true为本地资源
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            path += "File:///";
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            path += "File://";
#endif
        }
        
        //获取要加载的资源路径【bundle的总说明文件】
        path += assetBundlePath + "/" + assetBundleRootName;
        
        //加载
        WWW www = WWW.LoadFromCacheOrDownload(path, version);
        yield return www;
        
        //拿到其中的bundle
        AssetBundle manifestBundle = www.assetBundle;
        
        //获取到说明文件
        AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        
        //获取资源的所有依赖
        string[] dependencies = manifest.GetAllDependencies(assetBundleName);
        
        //卸载Bundle和解压出来的manifest对象
        manifestBundle.Unload(true);
        
        //获取到相对路径
        path = path.Remove(path.LastIndexOf("/") + 1);
        
        //声明依赖的Bundle数组
        AssetBundle[] depAssetBundles = new AssetBundle[dependencies.Length];
        
        //遍历加载所有的依赖
        for (int i = 0; i < dependencies.Length; i++)
        {
            //获取到依赖Bundle的路径
            string depPath = path + dependencies[i];
            
            //获取新的路径进行加载
            www = WWW.LoadFromCacheOrDownload(depPath, version);
            yield return www;
            
            //将依赖临时保存
            depAssetBundles[i] = www.assetBundle;
        }
        
        //获取路径
        path += assetBundleName;
        
        //加载最终资源
        www = WWW.LoadFromCacheOrDownload(path, version);
        
        //等待下载
        yield return www;
        
        //获取到真正的AssetBundle
        AssetBundle realAssetBundle = www.assetBundle;
        
        //加载真正的资源
        GameObject prefab = realAssetBundle.LoadAsset<GameObject>(assetRealName);
        
        //生成
        Instantiate(prefab);
        
        //卸载依赖
        for (int i = 0; i < depAssetBundles.Length; i++)
        {
            depAssetBundles[i].Unload(true);
        }
        
        realAssetBundle.Unload(true);
    }
}
```

---

### 7. AssetBundle卸载流程

`AssetBundle.Unload(bool)`

- **true**：卸载所有资源
- **false**：只卸载没使用的资源，而正在使用的资源与AssetBundle依赖关系会丢失，调用`Resources.UnloadUnusedAssets`可以卸载。

或者等场景切换的时候自动调用`Resources.UnloadUnusedAssets`。

---

### 8. ScriptableObejct

ScriptableObject是一个数据容器，它可以用来保存大量数据。

**主要的用处**：就是在项目中通过将数据存储在ScriptableObject对象，避免值拷贝来减少游戏运行中的内存占用。

当你有一个预制体，上面挂了一个存有不变数据的MonoBehaviour 脚本时，每次我们实例化预制体时都将产生一次数据拷贝，这时我们可以使用ScriptableObject对象来存储数据，然后通过引用来访问预制体中的数据。这样可以避免在内存中产生一份拷贝数据。

与MonoBehaviour 一样，ScriptableObject也继承自Unity基类object，但是与MonoBehaviour不同的是，ScriptableObject不能和GameObject对相关联，相反，通常我们会将它保存为Assets资源。

在编辑器模式下，我们可以在编辑和运行时将数据保存到ScriptableObject，因为保存ScriptableObject需要用到编辑器空间个脚本，但是在开发模式下不能使用ScriptableObject来保存数据，但是你可以使用ScriptableObject资源中已保存的数据。

---

## 总结

这份Unity进阶篇面试题总结涵盖了以下核心知识点：

1. **物理系统**：CharacterController、Rigidbody、射线检测、碰撞检测、物理更新
2. **UI & 2D**：UGUI合批、Image与RawImage、Canvas模式、UI适配、Text与TMPText
3. **动画系统**：动画类型、Avatar、Animation与Animator、SkinnedMesh
4. **协程**：协程原理、线程与协程的区别、协程的使用
5. **资源管理**：资源路径、AssetBundle打包与加载、ScriptableObject、数据持久化

这些知识点是Unity开发中的进阶内容，掌握这些内容对于面试和实际开发都非常重要。

---

**参考来源**：CSDN博客 - 呆呆敲代码的小Y

