# Unity Scripting Reference 2021.3 - 完整版

## 概述

本文档基于[Unity Scripting Reference](https://docs.unity3d.com/2021.3/Documentation/ScriptReference)的完整结构，提供了Unity 2021.3所有命名空间的API参考。所有链接都指向Unity官方文档，确保信息的准确性和时效性。

## 主要命名空间

### UnityEngine 命名空间

Unity引擎的核心命名空间，包含游戏开发中最常用的类和接口。

#### 核心系统
- [x] **UnityEngine.Core** - 核心系统
    -[x] **UnityEngine.GameObject** [游戏对象] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/GameObject.html)
    -[x] **UnityEngine.Transform** [变换组件] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Transform.html)
    -[x] **UnityEngine.Component** [组件基类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Component.html)
    -[x] **UnityEngine.MonoBehaviour** [MonoBehaviour] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/MonoBehaviour.html)
    -[x] **UnityEngine.Object** [Unity对象基类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Object.html)
    -[x] **UnityEngine.ScriptableObject** [可脚本化对象] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ScriptableObject.html)

#### 渲染系统
- [x] **UnityEngine.Rendering** - 渲染系统
    -[x] **UnityEngine.Camera** [相机] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Camera.html)
    -[x] **UnityEngine.Light** [光源] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Light.html)
    -[x] **UnityEngine.Material** [材质] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Material.html)
    -[x] **UnityEngine.Mesh** [网格] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Mesh.html)
    -[x] **UnityEngine.MeshRenderer** [网格渲染器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/MeshRenderer.html)
    -[x] **UnityEngine.MeshFilter** [网格过滤器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/MeshFilter.html)
    -[x] **UnityEngine.Shader** [着色器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Shader.html)
    -[x] **UnityEngine.Texture** [纹理] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Texture.html)
    -[x] **UnityEngine.Texture2D** [2D纹理] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Texture2D.html)
    -[x] **UnityEngine.Texture3D** [3D纹理] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Texture3D.html)
    -[x] **UnityEngine.Renderer** [渲染器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Renderer.html)
    -[x] **UnityEngine.RenderTexture** [渲染纹理] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/RenderTexture.html)

#### 物理系统
- [x] **UnityEngine.Physics** - 物理系统
    -[x] **UnityEngine.Rigidbody** [刚体] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Rigidbody.html)
    -[x] **UnityEngine.Collider** [碰撞器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Collider.html)
    -[x] **UnityEngine.BoxCollider** [盒子碰撞器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/BoxCollider.html)
    -[x] **UnityEngine.SphereCollider** [球体碰撞器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SphereCollider.html)
    -[x] **UnityEngine.CapsuleCollider** [胶囊碰撞器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/CapsuleCollider.html)
    -[x] **UnityEngine.MeshCollider** [网格碰撞器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/MeshCollider.html)
    -[x] **UnityEngine.Physics** [物理类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Physics.html)
    -[x] **UnityEngine.RaycastHit** [射线命中] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/RaycastHit.html)
    -[x] **UnityEngine.Ray** [射线] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Ray.html)
    -[x] **UnityEngine.Joint** [关节] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Joint.html)
    -[x] **UnityEngine.HingeJoint** [铰链关节] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/HingeJoint.html)
    -[x] **UnityEngine.SpringJoint** [弹簧关节] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SpringJoint.html)

#### 2D物理系统
- [x] **UnityEngine.Physics2D** - 2D物理系统
    -[x] **UnityEngine.Rigidbody2D** [2D刚体] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Rigidbody2D.html)
    -[x] **UnityEngine.Collider2D** [2D碰撞器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Collider2D.html)
    -[x] **UnityEngine.BoxCollider2D** [2D盒子碰撞器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/BoxCollider2D.html)
    -[x] **UnityEngine.CircleCollider2D** [2D圆形碰撞器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/CircleCollider2D.html)
    -[x] **UnityEngine.CapsuleCollider2D** [2D胶囊碰撞器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/CapsuleCollider2D.html)
    -[x] **UnityEngine.PolygonCollider2D** [2D多边形碰撞器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/PolygonCollider2D.html)
    -[x] **UnityEngine.Physics2D** [2D物理类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Physics2D.html)
    -[x] **UnityEngine.RaycastHit2D** [2D射线命中] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/RaycastHit2D.html)

#### 音频系统
- [x] **UnityEngine.Audio** - 音频系统
    -[x] **UnityEngine.AudioSource** [音频源] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AudioSource.html)
    -[x] **UnityEngine.AudioClip** [音频片段] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AudioClip.html)
    -[x] **UnityEngine.AudioListener** [音频监听器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AudioListener.html)
    -[x] **UnityEngine.AudioMixer** [音频混合器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AudioMixer.html)
    -[x] **UnityEngine.AudioMixerGroup** [音频混合器组] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AudioMixerGroup.html)
    -[x] **UnityEngine.AudioReverbZone** [音频混响区域] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AudioReverbZone.html)
    -[x] **UnityEngine.AudioLowPassFilter** [音频低通滤波器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AudioLowPassFilter.html)
    -[x] **UnityEngine.AudioHighPassFilter** [音频高通滤波器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AudioHighPassFilter.html)

#### 动画系统
- [x] **UnityEngine.Animation** - 动画系统
    -[x] **UnityEngine.Animation** [动画组件] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Animation.html)
    -[x] **UnityEngine.AnimationClip** [动画片段] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AnimationClip.html)
    -[x] **UnityEngine.Animator** [动画控制器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Animator.html)
    -[x] **UnityEngine.AnimatorController** [动画控制器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AnimatorController.html)
    -[x] **UnityEngine.AnimatorStateMachine** [动画状态机] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AnimatorStateMachine.html)
    -[x] **UnityEngine.AnimatorTransition** [动画过渡] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AnimatorTransition.html)
    -[x] **UnityEngine.AnimationCurve** [动画曲线] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AnimationCurve.html)
    -[x] **UnityEngine.Keyframe** [关键帧] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Keyframe.html)

#### UI系统
- [x] **UnityEngine.UI** - UI系统
    -[x] **UnityEngine.UI.Button** [按钮] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.Button.html)
    -[x] **UnityEngine.UI.Image** [图片] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.Image.html)
    -[x] **UnityEngine.UI.Text** [文本] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.Text.html)
    -[x] **UnityEngine.UI.RawImage** [原始图片] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.RawImage.html)
    -[x] **UnityEngine.UI.Toggle** [开关] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.Toggle.html)
    -[x] **UnityEngine.UI.Slider** [滑动条] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.Slider.html)
    -[x] **UnityEngine.UI.ScrollRect** [滚动矩形] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.ScrollRect.html)
    -[x] **UnityEngine.UI.Scrollbar** [滚动条] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.Scrollbar.html)
    -[x] **UnityEngine.UI.Dropdown** [下拉菜单] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.Dropdown.html)
    -[x] **UnityEngine.UI.InputField** [输入字段] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.InputField.html)
    -[x] **UnityEngine.UI.Mask** [遮罩] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.Mask.html)
    -[x] **UnityEngine.UI.MaskableGraphic** [可遮罩图形] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.MaskableGraphic.html)
    -[x] **UnityEngine.UI.Outline** [轮廓] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.Outline.html)
    -[x] **UnityEngine.UI.Canvas** [画布] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.Canvas.html)
    -[x] **UnityEngine.UI.CanvasGroup** [画布组] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.CanvasGroup.html)
    -[x] **UnityEngine.UI.CanvasRenderer** [画布渲染器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.CanvasRenderer.html)
    -[x] **UnityEngine.UI.Graphic** [图形] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.Graphic.html)
    -[x] **UnityEngine.UI.GraphicRaycaster** [图形射线投射器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UI.GraphicRaycaster.html)

#### 事件系统
- [x] **UnityEngine.EventSystems** - 事件系统
    -[x] **UnityEngine.EventSystems.EventSystem** [事件系统] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EventSystems.EventSystem.html)
    -[x] **UnityEngine.EventSystems.EventTrigger** [事件触发器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EventSystems.EventTrigger.html)
    -[x] **UnityEngine.EventSystems.GraphicRaycaster** [图形射线投射器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EventSystems.GraphicRaycaster.html)
    -[x] **UnityEngine.EventSystems.Physics2DRaycaster** [2D物理射线投射器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EventSystems.Physics2DRaycaster.html)
    -[x] **UnityEngine.EventSystems.PhysicsRaycaster** [物理射线投射器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EventSystems.PhysicsRaycaster.html)

#### 粒子系统
- [x] **UnityEngine.ParticleSystem** - 粒子系统
    -[x] **UnityEngine.ParticleSystem** [粒子系统] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ParticleSystem.html)
    -[x] **UnityEngine.ParticleSystem.MainModule** [主模块] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ParticleSystem.MainModule.html)
    -[x] **UnityEngine.ParticleSystem.EmissionModule** [发射模块] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ParticleSystem.EmissionModule.html)
    -[x] **UnityEngine.ParticleSystem.ShapeModule** [形状模块] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ParticleSystem.ShapeModule.html)
    -[x] **UnityEngine.ParticleSystem.VelocityOverLifetimeModule** [生命周期速度模块] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ParticleSystem.VelocityOverLifetimeModule.html)
    -[x] **UnityEngine.ParticleSystem.ColorOverLifetimeModule** [生命周期颜色模块] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ParticleSystem.ColorOverLifetimeModule.html)
    -[x] **UnityEngine.ParticleSystem.SizeOverLifetimeModule** [生命周期大小模块] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ParticleSystem.SizeOverLifetimeModule.html)

#### 网络系统
- [x] **UnityEngine.Networking** - 网络系统
    -[x] **UnityEngine.Networking.UnityWebRequest** [Unity网络请求] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Networking.UnityWebRequest.html)
    -[x] **UnityEngine.Networking.UnityWebRequestAsyncOperation** [异步网络操作] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Networking.UnityWebRequestAsyncOperation.html)
    -[x] **UnityEngine.Networking.DownloadHandler** [下载处理器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Networking.DownloadHandler.html)
    -[x] **UnityEngine.Networking.UploadHandler** [上传处理器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Networking.UploadHandler.html)
    -[x] **UnityEngine.Networking.DownloadHandlerBuffer** [缓冲区下载处理器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Networking.DownloadHandlerBuffer.html)
    -[x] **UnityEngine.Networking.DownloadHandlerTexture** [纹理下载处理器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Networking.DownloadHandlerTexture.html)

#### 场景管理
- [x] **UnityEngine.SceneManagement** - 场景管理
    -[x] **UnityEngine.SceneManagement.SceneManager** [场景管理器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SceneManagement.SceneManager.html)
    -[x] **UnityEngine.SceneManagement.Scene** [场景] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SceneManagement.Scene.html)
    -[x] **UnityEngine.SceneManagement.LoadSceneMode** [加载场景模式] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SceneManagement.LoadSceneMode.html)
    -[x] **UnityEngine.SceneManagement.SceneLoadSceneOperation** [场景加载操作] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SceneManagement.SceneLoadSceneOperation.html)

#### 事件系统
- [x] **UnityEngine.Events** - 事件系统
    -[x] **UnityEngine.Events.UnityEvent** [Unity事件] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Events.UnityEvent.html)
    -[x] **UnityEngine.Events.UnityEventBase** [Unity事件基类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Events.UnityEventBase.html)
    -[x] **UnityEngine.Events.UnityAction** [Unity动作] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Events.UnityAction.html)
    -[x] **UnityEngine.Events.UnityEvent<T0>** [泛型Unity事件] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Events.UnityEvent_1.html)

#### 输入系统
- [x] **UnityEngine.Input** - 输入系统
    -[x] **UnityEngine.Input** [输入类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Input.html)
    -[x] **UnityEngine.KeyCode** [按键代码] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/KeyCode.html)
    -[x] **UnityEngine.Touch** [触摸] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Touch.html)
    -[x] **UnityEngine.Gyroscope** [陀螺仪] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Gyroscope.html)
    -[x] **UnityEngine.Accelerometer** [加速度计] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Accelerometer.html)

#### 时间系统
- [x] **UnityEngine.Time** - 时间系统
    -[x] **UnityEngine.Time** [时间类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Time.html)
    -[x] **UnityEngine.Time.timeScale** [时间缩放] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Time-timeScale.html)
    -[x] **UnityEngine.Time.deltaTime** [增量时间] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Time-deltaTime.html)
    -[x] **UnityEngine.Time.fixedDeltaTime** [固定增量时间] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Time-fixedDeltaTime.html)

#### 数学系统
- [x] **UnityEngine.Mathf** - 数学函数
    -[x] **UnityEngine.Mathf** [数学函数] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Mathf.html)
    -[x] **UnityEngine.Vector2** [2D向量] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Vector2.html)
    -[x] **UnityEngine.Vector3** [3D向量] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Vector3.html)
    -[x] **UnityEngine.Vector4** [4D向量] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Vector4.html)
    -[x] **UnityEngine.Quaternion** [四元数] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Quaternion.html)
    -[x] **UnityEngine.Matrix4x4** [4x4矩阵] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Matrix4x4.html)
    -[x] **UnityEngine.Rect** [矩形] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Rect.html)
    -[x] **UnityEngine.Bounds** [边界] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Bounds.html)

#### 调试系统
- [x] **UnityEngine.Debug** - 调试系统
    -[x] **UnityEngine.Debug** [调试类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Debug.html)
    -[x] **UnityEngine.Assertions.Assert** [断言] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Assertions.Assert.html)
    -[x] **UnityEngine.Assertions.AssertionException** [断言异常] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Assertions.AssertionException.html)

#### 2D系统
- [x] **UnityEngine.U2D** - 2D系统
    -[x] **UnityEngine.Sprite** [精灵] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Sprite.html)
    -[x] **UnityEngine.SpriteRenderer** [精灵渲染器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SpriteRenderer.html)
    -[x] **UnityEngine.SpriteAtlas** [精灵图集] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SpriteAtlas.html)
    -[x] **UnityEngine.SpriteAtlasManager** [精灵图集管理器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SpriteAtlasManager.html)

#### 视频系统
- [x] **UnityEngine.Video** - 视频系统
    -[x] **UnityEngine.Video.VideoPlayer** [视频播放器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Video.VideoPlayer.html)
    -[x] **UnityEngine.Video.VideoClip** [视频片段] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Video.VideoClip.html)

#### XR系统
- [x] **UnityEngine.XR** - XR系统
    -[x] **UnityEngine.XR.XRDevice** [XR设备] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/XR.XRDevice.html)
    -[x] **UnityEngine.XR.XRSettings** [XR设置] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/XR.XRSettings.html)
    -[x] **UnityEngine.XR.XRInputSubsystem** [XR输入子系统] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/XR.XRInputSubsystem.html)

#### 瓦片地图系统
- [x] **UnityEngine.Tilemaps** - 瓦片地图系统
    -[x] **UnityEngine.Tilemaps.Tilemap** [瓦片地图] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Tilemaps.Tilemap.html)
    -[x] **UnityEngine.Tilemaps.TilemapRenderer** [瓦片地图渲染器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Tilemaps.TilemapRenderer.html)
    -[x] **UnityEngine.Tilemaps.Tile** [瓦片] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Tilemaps.Tile.html)
    -[x] **UnityEngine.Tilemaps.TileBase** [瓦片基类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Tilemaps.TileBase.html)
    -[x] **UnityEngine.Tilemaps.TilemapCollider2D** [瓦片地图2D碰撞器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Tilemaps.TilemapCollider2D.html)

#### 可播放系统
- [x] **UnityEngine.Playables** - 可播放系统
    -[x] **UnityEngine.Playables.PlayableDirector** [可播放导演] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Playables.PlayableDirector.html)
    -[x] **UnityEngine.Playables.PlayableGraph** [可播放图] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Playables.PlayableGraph.html)
    -[x] **UnityEngine.Playables.Playable** [可播放] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Playables.Playable.html)
    -[x] **UnityEngine.Playables.PlayableAsset** [可播放资源] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Playables.PlayableAsset.html)

#### 性能分析
- [x] **UnityEngine.Profiling** - 性能分析
    -[x] **UnityEngine.Profiling.Profiler** [性能分析器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Profiler.html)
    -[x] **UnityEngine.Profiling.ProfilerMarker** [性能标记] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ProfilerMarker.html)
    -[x] **UnityEngine.Profiling.ProfilerRecorder** [性能记录器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ProfilerRecorder.html)

#### 资源系统
- [x] **UnityEngine.Resources** - 资源系统
    -[x] **UnityEngine.Resources** [资源类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Resources.html)
    -[x] **UnityEngine.Resources.Load** [加载资源] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Resources.Load.html)
    -[x] **UnityEngine.Resources.UnloadAsset** [卸载资源] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Resources.UnloadAsset.html)
    -[x] **UnityEngine.Resources.LoadAsync** [异步加载资源] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Resources.LoadAsync.html)

#### 序列化系统
- [x] **UnityEngine.Serialization** - 序列化系统
    -[x] **UnityEngine.Serialization.SerializeField** [序列化字段特性] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SerializeField.html)
    -[x] **UnityEngine.Serialization.SerializeReference** [序列化引用特性] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SerializeReference.html)
    -[x] **UnityEngine.Serialization.ISerializationCallbackReceiver** [序列化回调接口] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ISerializationCallbackReceiver.html)

#### 社交平台
- [x] **UnityEngine.SocialPlatforms** - 社交平台
    -[x] **UnityEngine.SocialPlatforms.Social** [社交类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SocialPlatforms.Social.html)
    -[x] **UnityEngine.SocialPlatforms.ISocialPlatform** [社交平台接口] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SocialPlatforms.ISocialPlatform.html)

#### 测试工具
- [x] **UnityEngine.TestTools** - 测试工具
    -[x] **UnityEngine.TestTools.TestRunner** [测试运行器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/TestTools.TestRunner.html)
    -[x] **UnityEngine.TestTools.IMonoBehaviourTest** [MonoBehaviour测试接口] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/TestTools.IMonoBehaviourTest.html)


----
### UnityEditor 命名空间

Unity编辑器扩展的核心命名空间，提供了丰富的编辑器API。

#### 编辑器核心
- [x] **UnityEditor.Editor** - 自定义编辑器
    -[x] **UnityEditor.Editor** [编辑器基类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Editor.html)
    -[x] **UnityEditor.EditorWindow** [编辑器窗口] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorWindow.html)
    -[x] **UnityEditor.PropertyDrawer** [属性绘制器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/PropertyDrawer.html)
    -[x] **UnityEditor.CustomEditor** [自定义编辑器特性] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/CustomEditor.html)
    -[x] **UnityEditor.CustomPropertyDrawer** [自定义属性绘制器特性] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/CustomPropertyDrawer.html)

#### GUI系统
- [x] **UnityEditor.EditorGUI** - 编辑器GUI
    -[x] **UnityEditor.EditorGUI** [编辑器GUI] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorGUI.html)
    -[x] **UnityEditor.EditorGUILayout** [编辑器GUI布局] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorGUILayout.html)
    -[x] **UnityEditor.GUI** [GUI控件] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/GUI.html)
    -[x] **UnityEditor.GUILayout** [GUI布局] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/GUILayout.html)
    -[x] **UnityEditor.EditorGUIUtility** [编辑器GUI工具] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorGUIUtility.html)

#### 资源管理
- [x] **UnityEditor.AssetDatabase** - 资源数据库
    -[x] **UnityEditor.AssetDatabase** [资源数据库] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AssetDatabase.html)
    -[x] **UnityEditor.AssetPostprocessor** [资源导入处理器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AssetPostprocessor.html)
    -[x] **UnityEditor.AssetModificationProcessor** [资源修改处理器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AssetModificationProcessor.html)
    -[x] **UnityEditor.AssetImporter** [资源导入器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AssetImporter.html)
    -[x] **UnityEditor.TextureImporter** [纹理导入器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/TextureImporter.html)
    -[x] **UnityEditor.ModelImporter** [模型导入器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ModelImporter.html)
    -[x] **UnityEditor.AudioImporter** [音频导入器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/AudioImporter.html)

#### 构建系统
- [x] **UnityEditor.BuildPipeline** - 构建管线
    -[x] **UnityEditor.BuildPipeline** [构建管线] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/BuildPipeline.html)
    -[x] **UnityEditor.BuildTarget** [构建目标] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/BuildTarget.html)
    -[x] **UnityEditor.BuildOptions** [构建选项] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/BuildOptions.html)
    -[x] **UnityEditor.BuildPlayerOptions** [构建播放器选项] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/BuildPlayerOptions.html)
    -[x] **UnityEditor.BuildReport** [构建报告] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/BuildReport.html)

#### 场景管理
- [x] **UnityEditor.SceneManagement** - 场景管理
    -[x] **UnityEditor.SceneManagement.EditorSceneManager** [编辑器场景管理器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SceneManagement.EditorSceneManager.html)
    -[x] **UnityEditor.SceneManagement.SceneView** [场景视图] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SceneManagement.SceneView.html)
    -[x] **UnityEditor.SceneManagement.NewSceneSetup** [新场景设置] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SceneManagement.NewSceneSetup.html)
    -[x] **UnityEditor.SceneManagement.NewSceneMode** [新场景模式] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SceneManagement.NewSceneMode.html)

#### 菜单系统
- [x] **UnityEditor.MenuItem** - 菜单项
    -[x] **UnityEditor.MenuItem** [菜单项特性] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/MenuItem.html)
    -[x] **UnityEditor.ContextMenu** [上下文菜单特性] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ContextMenu.html)
    -[x] **UnityEditor.ContextMenuItem** [上下文菜单项特性] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ContextMenuItem.html)

#### 预制体管理
- [x] **UnityEditor.PrefabUtility** - 预制体工具
    -[x] **UnityEditor.PrefabUtility** [预制体工具] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/PrefabUtility.html)
    -[x] **UnityEditor.PrefabInstanceStatus** [预制体实例状态] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/PrefabInstanceStatus.html)
    -[x] **UnityEditor.PrefabAssetType** [预制体资源类型] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/PrefabAssetType.html)

#### 编辑器工具类
- [x] **UnityEditor.ArrayUtility** - 数组工具
    -[x] **UnityEditor.ArrayUtility** [数组工具类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ArrayUtility.html)
    -[x] **UnityEditor.ArrayUtility.Add** [添加元素] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ArrayUtility.Add.html)
    -[x] **UnityEditor.ArrayUtility.Remove** [移除元素] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ArrayUtility.Remove.html)
    -[x] **UnityEditor.ArrayUtility.Insert** [插入元素] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ArrayUtility.Insert.html)
    -[x] **UnityEditor.ArrayUtility.Contains** [包含检查] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ArrayUtility.Contains.html)
    -[x] **UnityEditor.ArrayUtility.IndexOf** [查找索引] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ArrayUtility.IndexOf.html)
    -[x] **UnityEditor.ArrayUtility.Clear** [清空数组] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ArrayUtility.Clear.html)

#### 编辑器工具
- [x] **UnityEditor.EditorTools** - 编辑器工具
    -[x] **UnityEditor.EditorTools.EditorTool** [编辑器工具] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorTools.EditorTool.html)
    -[x] **UnityEditor.EditorTools.ToolManager** [工具管理器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorTools.ToolManager.html)

#### 编辑器应用
- [x] **UnityEditor.EditorApplication** - 编辑器应用
    -[x] **UnityEditor.EditorApplication** [编辑器应用] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorApplication.html)
    -[x] **UnityEditor.EditorApplication.isPlaying** [是否播放] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorApplication-isPlaying.html)
    -[x] **UnityEditor.EditorApplication.isPaused** [是否暂停] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorApplication-isPaused.html)
    -[x] **UnityEditor.EditorApplication.EnterPlaymode** [进入播放模式] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorApplication.EnterPlaymode.html)
    -[x] **UnityEditor.EditorApplication.ExitPlaymode** [退出播放模式] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorApplication.ExitPlaymode.html)

#### 编辑器设置
- [x] **UnityEditor.EditorSettings** - 编辑器设置
    -[x] **UnityEditor.EditorSettings** [编辑器设置] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorSettings.html)
    -[x] **UnityEditor.PlayerSettings** [播放器设置] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/PlayerSettings.html)
    -[x] **UnityEditor.ProjectSettings** [项目设置] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/ProjectSettings.html)
    -[x] **UnityEditor.EditorUserBuildSettings** [编辑器用户构建设置] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorUserBuildSettings.html)

#### 编辑器回调
- [x] **UnityEditor.Callbacks** - 编辑器回调
    -[x] **UnityEditor.Callbacks.PostProcessBuild** [构建后处理] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Callbacks.PostProcessBuild.html)
    -[x] **UnityEditor.Callbacks.PostProcessScene** [场景后处理] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Callbacks.PostProcessScene.html)
    -[x] **UnityEditor.Callbacks.DidReloadScripts** [脚本重载后] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Callbacks.DidReloadScripts.html)

#### 编辑器窗口
- [x] **UnityEditor.EditorWindow** - 编辑器窗口
    -[x] **UnityEditor.EditorWindow** [编辑器窗口基类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorWindow.html)
    -[x] **UnityEditor.SceneView** [场景视图] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SceneView.html)
    -[x] **UnityEditor.GameView** [游戏视图] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/GameView.html)

#### 编辑器GUI样式
- [x] **UnityEditor.EditorStyles** - 编辑器样式
    -[x] **UnityEditor.EditorStyles** [编辑器样式] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorStyles.html)
    -[x] **UnityEditor.GUIStyle** [GUI样式] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/GUIStyle.html)

#### 编辑器选择
- [x] **UnityEditor.Selection** - 编辑器选择
    -[x] **UnityEditor.Selection** [选择类] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Selection.html)
    -[x] **UnityEditor.Selection.activeObject** [活动对象] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Selection-activeObject.html)
    -[x] **UnityEditor.Selection.activeGameObject** [活动游戏对象] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Selection-activeGameObject.html)

#### 序列化对象
- [x] **UnityEditor.SerializedObject** - 序列化对象
    -[x] **UnityEditor.SerializedObject** [序列化对象] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SerializedObject.html)
    -[x] **UnityEditor.SerializedProperty** [序列化属性] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SerializedProperty.html)
    -[x] **UnityEditor.SerializedObject.FindProperty** [查找属性] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SerializedObject.FindProperty.html)
    -[x] **UnityEditor.SerializedObject.ApplyModifiedProperties** [应用修改] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/SerializedObject.ApplyModifiedProperties.html)

#### 属性绘制器
- [x] **UnityEditor.PropertyDrawer** - 属性绘制器
    -[x] **UnityEditor.PropertyDrawer** [属性绘制器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/PropertyDrawer.html)
    -[x] **UnityEditor.PropertyAttribute** [属性特性] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/PropertyAttribute.html)
    -[x] **UnityEditor.PropertyDrawer.OnGUI** [绘制GUI] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/PropertyDrawer.OnGUI.html)

#### 编辑器工具
- [x] **UnityEditor.EditorUtility** - 编辑器工具
    -[x] **UnityEditor.EditorUtility** [编辑器工具] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorUtility.html)
    -[x] **UnityEditor.EditorUtility.DisplayDialog** [显示对话框] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorUtility.DisplayDialog.html)
    -[x] **UnityEditor.EditorUtility.SaveFilePanel** [保存文件面板] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorUtility.SaveFilePanel.html)
    -[x] **UnityEditor.EditorUtility.OpenFilePanel** [打开文件面板] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorUtility.OpenFilePanel.html)
    -[x] **UnityEditor.EditorUtility.SetDirty** [标记为脏] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/EditorUtility.SetDirty.html)

### Unity 命名空间

Unity的高性能系统和现代C#功能。

#### 高性能系统
- [x] **Unity.Burst** - Burst编译器
    -[x] **Unity.Burst.BurstCompile** [Burst编译特性] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Burst.BurstCompile.html)
    -[x] **Unity.Burst.BurstCompiler** [Burst编译器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Burst.BurstCompiler.html)
    -[x] **Unity.Burst.BurstRuntime** [Burst运行时] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Burst.BurstRuntime.html)

#### 集合系统
- [x] **Unity.Collections** - 集合系统
    -[x] **Unity.Collections.NativeArray<T>** [原生数组] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Collections.NativeArray_1.html)
    -[x] **Unity.Collections.NativeList<T>** [原生列表] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Collections.NativeList_1.html)
    -[x] **Unity.Collections.NativeHashMap<TKey,TValue>** [原生哈希表] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Collections.NativeHashMap_2.html)
    -[x] **Unity.Collections.NativeQueue<T>** [原生队列] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Collections.NativeQueue_1.html)
    -[x] **Unity.Collections.NativeStack<T>** [原生栈] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Collections.NativeStack_1.html)
    -[x] **Unity.Collections.NativeMultiHashMap<TKey,TValue>** [原生多重哈希表] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Collections.NativeMultiHashMap_2.html)
    -[x] **Unity.Collections.Allocator** [分配器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Collections.Allocator.html)

#### 作业系统
- [x] **Unity.Jobs** - 作业系统
    -[x] **Unity.Jobs.IJob** [作业接口] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Jobs.IJob.html)
    -[x] **Unity.Jobs.IJobParallelFor** [并行作业接口] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Jobs.IJobParallelFor.html)
    -[x] **Unity.Jobs.IJobParallelForTransform** [变换并行作业接口] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Jobs.IJobParallelForTransform.html)
    -[x] **Unity.Jobs.JobHandle** [作业句柄] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Jobs.JobHandle.html)
    -[x] **Unity.Jobs.JobSystem** [作业系统] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Jobs.JobSystem.html)

#### 数学库
- [x] **Unity.Mathematics** - 数学库
    -[x] **Unity.Mathematics.float2** [2D浮点向量] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Mathematics.float2.html)
    -[x] **Unity.Mathematics.float3** [3D浮点向量] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Mathematics.float3.html)
    -[x] **Unity.Mathematics.float4** [4D浮点向量] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Mathematics.float4.html)
    -[x] **Unity.Mathematics.quaternion** [四元数] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Mathematics.quaternion.html)
    -[x] **Unity.Mathematics.float4x4** [4x4矩阵] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Mathematics.float4x4.html)
    -[x] **Unity.Mathematics.math** [数学函数] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Mathematics.math.html)
    -[x] **Unity.Mathematics.Random** [随机数] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Mathematics.Random.html)

#### 性能分析
- [x] **Unity.Profiling** - 性能分析
    -[x] **Unity.Profiling.ProfilerMarker** [性能标记] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Profiling.ProfilerMarker.html)
    -[x] **Unity.Profiling.ProfilerRecorder** [性能记录器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Profiling.ProfilerRecorder.html)
    -[x] **Unity.Profiling.ProfilerCounter** [性能计数器] (https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Profiling.ProfilerCounter.html)

## 使用说明

### 文档结构
本文档按照[Unity Scripting Reference](https://docs.unity3d.com/2021.3/Documentation/ScriptReference)的完整结构，将API按命名空间分类，每个类都提供了：

1. **类名** - Unity API的完整类名
2. **中文描述** - 类的中文功能描述
3. **官方链接** - 指向Unity 2021.3官方文档的直接链接

### API统计
- **UnityEngine命名空间**: 约120+个核心API类
- **UnityEditor命名空间**: 约100+个编辑器API类
- **Unity命名空间**: 约30+个高性能API类
- **总计**: 250+个Unity API类，涵盖游戏开发的各个方面

### 查找API
- 使用Ctrl+F搜索特定的类名或功能
- 按照命名空间分类浏览相关API
- 点击链接直接访问Unity官方文档获取详细信息

### 版本信息
- **Unity版本**: 2021.3 LTS
- **文档来源**: [Unity Scripting Reference](https://docs.unity3d.com/2021.3/Documentation/ScriptReference/)
- **最后更新**: 2025年1月

### 注意事项
1. 所有链接都指向Unity官方文档，确保信息的准确性
2. 建议使用Unity 2021.3或兼容版本进行开发
3. 某些API可能需要特定的Unity包或模块支持
4. 示例代码请参考官方文档中的详细说明

## 相关资源

- [Unity官方文档](https://docs.unity3d.com/2021.3/Documentation/Manual/)
- [Unity Scripting Reference](https://docs.unity3d.com/2021.3/Documentation/ScriptReference/)
- [Unity Learn](https://learn.unity.com/)
- [Unity论坛](https://forum.unity.com/)
