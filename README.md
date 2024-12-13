1. 动画相关操作
1.1. Rename 和 AnimationRename
功能：
重命名 FBX 文件的 .meta 文件内容中的某些动画名称（如 idle 变为 idle1 等）。
针对动画组件，批量修改动画剪辑的名称。
细节：
使用 Selection.objects 选择多个对象并遍历。
针对 .meta 文件直接读取和写入（未使用 ModelImporter 修改）。
AnimationRename 修改动画组件内的动画剪辑名称。
1.2. 动作文件规范化
功能：
提供针对带相机和不带相机的 FBX 动作文件规范化逻辑。
检查和修复 skin 文件是否符合规范，包括导入选项（如禁用灯光、材质等）。
动画名称验证和修改，确保 idle 动画名称为规范化的 idle1。
细节：
通过 Regex 匹配动画名称的格式。
根据文件名判断是技能动画还是其他类型动画。
设置动画的循环模式或一次性模式，并保存重新导入。
1.3. 添加动画到预制体
功能：
自动为预制体添加 Animation 或 Animator，并加载指定路径下的动画文件。
通过 AnimationClip 修改为 Legacy 模式或直接绑定到 AnimatorController。
细节：
提供不同类型预制体（如 idle、enter、skill）的专属逻辑。
支持自动创建或加载动画控制器（.controller 文件）。
1.4. 动画循环模式设置
功能：
为选中的对象设置所有动画剪辑为循环模式。
细节：
根据动画类型（Legacy 或 Generic）分别修改 WrapMode 或 loopTime。
2. 预制体操作
2.1. 摄像机相关
功能：
禁用 idle 预制体中的所有子摄像机。
细节：
遍历子对象中的 Camera 组件，将其设置为非激活状态。
2.2. 层级设置
功能：
设置预制体及其子对象的层级，例如 CommonFish、DeadFish 或 FishAboveUI。
细节：
使用递归函数 SetLayer，为所有子对象设置相同的层级。
3. Skin 文件规范化
功能：
规范化 skin 文件，主要修改导入选项，包括：
禁用灯光、材质导入和动画导入。
根据选项决定是否保留相机。
细节：
针对文件夹中的 skin.fbx 文件进行验证和修改
