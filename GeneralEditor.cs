using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using FishingFramework;
using System.Reflection;
using UnityEditor.Animations;
using System.Text.RegularExpressions;

public class GeneralEditor 
{
    [UnityEditor.MenuItem("Tools/Rename &r")]
   public static void Rename()
    {
        //UnityEngine.Object[] objs = UnityEditor.Selection.GetFiltered(typeof(UnityEngine.Object), UnityEditor.SelectionMode.Assets);

        string path = "Assets/Resources/Model/Room1Assets/lx_boss_ey/ani/horizon_fg_special_eyu@idle1.fbx";

        //var assetPath = path;// path.Replace(Application.dataPath, "Assets");

        var fileName = Path.GetFileNameWithoutExtension(path);



        UnityEngine.Object[] objs = Selection.objects;
        foreach (var obj in objs)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            assetPath += ".meta";
            assetPath = assetPath.ReplaceFirst("Assets",Application.dataPath);
            Debug.Log(assetPath);

            if (File.Exists(assetPath))
            {
                string text = File.ReadAllText(assetPath);
                Debug.Log(text);
                text = text.Replace(" idle_2", " lxd2");
                text = text.Replace(" idle_3", " lxd3");
                text = text.Replace(" idle_4", " lxd4");
                text = text.Replace(" idle", " idle1");
                text = text.Replace(" lxd", " idle");
                File.WriteAllText(assetPath, text);
            }
            else
            {
                Debug.Log("文件不存在");
            }



            //ModelImporter importer = (ModelImporter)ModelImporter.GetAtPath(assetPath);
            //var clips = importer.clipAnimations;
            //for (int i = 0;i< clips.Length; i++)
            //{
            //    var clip = clips[i];
            //    if (clip.name == "idle1" || clip.name == "idle2" || clip.name == "idle3" || clip.name == "idle4"|| clip.name == "idle5")
            //    {
            //        continue;
            //    }
            //    if (clip.name == "idle")
            //    {
            //        clip.name = "idle1";
            //    }
            //    if (clip.name == "idle2")
            //    {
            //        clip.name = "idle2";
            //    }
            //}            

            //var oldClip = importer.clipAnimations[0];
            // ModelImporterClipAnimation newClip = new ModelImporterClipAnimation();
            // newClip.name = oldClip.name;
            // newClip.loopTime = false;
            // newClip.firstFrame = oldClip.firstFrame;
            // newClip.lastFrame = oldClip.lastFrame;
            // newClip.keepOriginalPositionXZ = true;
            // newClip.keepOriginalOrientation = true;
            // newClip.keepOriginalPositionY = true;
            // newClip.lockRootRotation = false;
            // newClip.lockRootHeightY = false;
            // newClip.lockRootPositionXZ = false;
            // newClip.heightOffset = 0;
            // newClip.rotationOffset = 0;
            //ModelImporterClipAnimation[] newClips = new ModelImporterClipAnimation[] { clips};



            //importer.clipAnimations = clips;

            // importer.SaveAndReimport();
        }

        AssetDatabase.Refresh();

        //ModelImporter modelImpoter = AssetImporter.GetAtPath(assetPath) as ModelImporter;


        //var clip = modelImpoter.defaultClipAnimations[0];
        //modelImpoter.animationType = ModelImporterAnimationType.Legacy;
        //var clip = modelImpoter.clipAnimations[0];                

        //ModelImporterClipAnimation newClip = new ModelImporterClipAnimation();
        //if (clip.name == "idle1" || clip.name == "idle2")
        //{
        //    return;
        //}
        //if (clip.name == "idle")
        //{
        //    clip.name = "idle1";
        //}
        //if(clip.name == "idle2")
        //{
        //    clip.name = "idle2";
        //}


        //newClip.name = "idle2";
        //newClip.loopTime = true;
        //newClip.firstFrame = clip.firstFrame;
        //newClip.lastFrame = clip.lastFrame;
        //newClip.keepOriginalOrientation = clip.keepOriginalOrientation;
        //newClip.keepOriginalPositionXZ = clip.keepOriginalPositionXZ;
        //newClip.keepOriginalPositionY = clip.keepOriginalPositionY;
        //newClip.lockRootRotation = clip.lockRootRotation;
        //newClip.lockRootHeightY = clip.lockRootHeightY;
        //newClip.lockRootPositionXZ = clip.lockRootPositionXZ;
        //newClip.heightOffset = clip.heightOffset;
        //newClip.rotationOffset = clip.rotationOffset;
        //newClip.wrapMode = clip.wrapMode;
        //ModelImporterClipAnimation[] clips = new ModelImporterClipAnimation[1] { clip };
        //modelImpoter.clipAnimations = clips;
        //modelImpoter.SaveAndReimport();

        //Debug.Log(clip.name);
        //clip.name = "idle";
        //modelImpoter.SaveAndReimport();
        ////AssetDatabase.SaveAssets();
        //AssetDatabase.Refresh();
        //Debug.Log(clip.name);


    }

    [UnityEditor.MenuItem("Tools/Rename A &d")]
    public static void AniamtionRename()
    {
        UnityEngine.Object[] objs = Selection.objects;
        foreach (var obj in objs)
        {
            var gameObject = obj as GameObject;
            var anim = gameObject.GetComponentInChildren<Animation>();
            var idle = anim.GetClip("idle");
            if(idle != null)
            {
                idle.name = "idle1";
            }
            var idle2 = anim.GetClip("idle2");
            if (idle2 != null)
            {
                idle2.name = "idle2";
            }
        }
    }

    [UnityEditor.MenuItem("Assets/动作文件●规范化/带相机", false, 1450)]
    public static void ChangeAnimationNameAndTypeWithCamera()
    {
        bool checkSkin = false;
        var objs = Selection.objects;
        bool skinImportCamera = false;
        string patern = @"idle\d";
        Match result = null;
        foreach (var obj in objs)
        {
            string path =  AssetDatabase.GetAssetPath(obj);
            ModelImporter mi = AssetImporter.GetAtPath(path) as ModelImporter;

            //判断skin文件有没有规范化
            if (!checkSkin)
            {   //获取到skin文件
                Debug.Log("path:" + path);                
                string skinPath = path.ReplaceFirst("Assets", Application.dataPath);
                Debug.LogError(skinPath);
                string skinDicPath = Path.GetDirectoryName(skinPath);
                Debug.LogError(skinDicPath);
                var f = Directory.GetFiles(skinDicPath+"\\","*skin.fbx");
                if (f == null || f.Length == 0)
                {
                    EditorUtility.DisplayDialog("提示", "请先导入skin文件，并保证skin文件命名规范正确", "确认");
                    Debug.LogError("请先导入skin文件，并保证skin文件命名规范正确");
                    return;
                }
                Debug.Log(skinDicPath + "   " + f[0]);
                skinPath = f[0].Replace('\\', '/');
                skinPath = skinPath.Replace(Application.dataPath, "Assets");
                ModelImporter skinMi = AssetImporter.GetAtPath(skinPath) as ModelImporter;
                if(mi == null)
                {
                    EditorUtility.DisplayDialog("提示", "没有找到skin文件，请找程序同学解决", "确认");
                    //EditorGUILayout.HelpBox("没有找到skin文件，请找程序同学解决", MessageType.Error);
                    Debug.Log("没有找到skin文件，请找程序同学解决");
                    return;
                }                
                if (skinMi.importLights || skinMi.materialImportMode!= ModelImporterMaterialImportMode.None || skinMi.importAnimation || skinMi.addCollider || skinMi.useFileScale)
                {
                    EditorUtility.DisplayDialog("提示", "请先规范化skin文件", "确认");
                    //EditorGUILayout.HelpBox("请先规范化skin文件", MessageType.Error);
                    Debug.Log("请先规范化skin文件");
                    return;
                }
                checkSkin = true;
                skinImportCamera = skinMi.importCameras;
            }
            if (!skinImportCamera)//skin没带相机
            {
                EditorUtility.DisplayDialog("提示", "Skin文件没有导出相机，请确认该动作是否需要导出相机，如果需要，请先将skin文件导出相机后再设置该动作文件", "确认");
                return;
            }
            string objName = obj.name;
            result = Regex.Match(objName, patern);
            if (obj.name.Contains("idle") && !result.Success)
            {
                EditorUtility.DisplayDialog("提示", "请确保idle动作名字是规范的，只有一个idle动作需要命名位idle1", "确认");
            }
            mi.importLights = false;
            mi.materialImportMode = ModelImporterMaterialImportMode.None;            
            mi.addCollider = false;
            mi.useFileScale = false;

            string name = "";
            if (obj.name.Contains("skill"))
            {
                var index = obj.name.IndexOf("skill");
                Debug.Log(index);
                name = obj.name.Substring(index);
            }
            else
            {
               var ss = obj.name.Split('_');
                name = ss[ss.Length - 1];
            }

            if(mi != null)
            {
                mi.importCameras = true;
                if (obj.name.Contains("skill"))
                {
                    mi.animationType = ModelImporterAnimationType.Generic;
                    ModelImporterClipAnimation[] clips = mi.clipAnimations;
                    if (clips.Length == 0)
                    {
                        clips = mi.defaultClipAnimations;
                    }
                    for (int i = 0; i < clips.Length; i++)
                    {
                        clips[i].loopTime = false;
                        clips[i].name = name;
                    }
                    mi.clipAnimations = clips;
                }
               else
               {
                    mi.animationType = ModelImporterAnimationType.Legacy;
                    ModelImporterClipAnimation[] clips = mi.clipAnimations;
                    if(clips.Length == 0)
                    {
                        clips = mi.defaultClipAnimations;
                    }                                        
                    for (int i = 0; i < clips.Length; i++)
                    {
                        Debug.Log(clips[i].name);
                        if(clips[i].wrapMode != WrapMode.Loop)
                        {
                            clips[i].wrapMode = WrapMode.Once;
                        }
                        clips[i].name = name;
                    }
                    mi.clipAnimations = clips;
               }
               mi.SaveAndReimport();                   
            }
        }
    }

    [UnityEditor.MenuItem("Assets/动作文件●规范化/不带相机", false, 1450)]
    public static void ChangeAnimationNameAndTypeNoCamera()
    {
        bool checkSkin = false;
        var objs = Selection.objects;
        bool skinImportCamera = false;
        string patern = @"idle\d";
        Match result = null;
        foreach (var obj in objs)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            ModelImporter mi = AssetImporter.GetAtPath(path) as ModelImporter;

            //判断skin文件有没有规范化
            if (!checkSkin)
            {   //获取到skin文件
                Debug.Log("path:" + path);
                string skinPath = path.ReplaceFirst("Assets", Application.dataPath);
                Debug.LogError(skinPath);
                string skinDicPath = Path.GetDirectoryName(skinPath);
                Debug.LogError(skinDicPath);
                var f = Directory.GetFiles(skinDicPath + "\\", "*skin.fbx");
                if (f == null || f.Length == 0)
                {
                    EditorUtility.DisplayDialog("提示", "请先导入skin文件，并保证skin文件命名规范正确", "确认");
                    Debug.LogError("请先导入skin文件，并保证skin文件命名规范正确");
                    return;
                }
                Debug.Log(skinDicPath + "   " + f[0]);
                skinPath = f[0].Replace('\\', '/');
                skinPath = skinPath.Replace(Application.dataPath, "Assets");
                ModelImporter skinMi = AssetImporter.GetAtPath(skinPath) as ModelImporter;
                if (mi == null)
                {
                    EditorUtility.DisplayDialog("提示", "没有找到skin文件，请找程序同学解决", "确认");
                    //EditorGUILayout.HelpBox("没有找到skin文件，请找程序同学解决", MessageType.Error);
                    Debug.Log("没有找到skin文件，请找程序同学解决");
                    return;
                }
                if (skinMi.importLights || skinMi.materialImportMode != ModelImporterMaterialImportMode.None || skinMi.importAnimation || skinMi.addCollider || skinMi.useFileScale)
                {
                    EditorUtility.DisplayDialog("提示", "请先规范化skin文件", "确认");
                    //EditorGUILayout.HelpBox("请先规范化skin文件", MessageType.Error);
                    Debug.Log("请先规范化skin文件");
                    return;
                }
                checkSkin = true;
                skinImportCamera = skinMi.importCameras;
            }
            string objName = obj.name;
            result = Regex.Match(objName, patern);            
            if (obj.name.Contains("idle") && !result.Success)
            {
                EditorUtility.DisplayDialog("提示", "请确保idle动作名字是规范的，只有一个idle动作需要命名位idle1", "确认");
            }
            mi.importLights = false;
            mi.materialImportMode = ModelImporterMaterialImportMode.None;
            mi.addCollider = false;
            mi.useFileScale = false;

            string name = "";
            if (obj.name.Contains("skill"))
            {
                var index = obj.name.IndexOf("skill");
                Debug.Log(index);
                name = obj.name.Substring(index);
            }
            else
            {
                var ss = obj.name.Split('_');
                name = ss[ss.Length - 1];
            }

            if (mi != null)
            {
                mi.importCameras = false;
                if (obj.name.Contains("skill"))
                {
                    mi.animationType = ModelImporterAnimationType.Generic;
                    ModelImporterClipAnimation[] clips = mi.clipAnimations;
                    if (clips.Length == 0)
                    {
                        clips = mi.defaultClipAnimations;
                    }
                    for (int i = 0; i < clips.Length; i++)
                    {
                        clips[i].loopTime = false;
                        clips[i].name = name;
                    }
                    mi.clipAnimations = clips;
                }
                else
                {
                    mi.animationType = ModelImporterAnimationType.Legacy;
                    ModelImporterClipAnimation[] clips = mi.clipAnimations;
                    if (clips.Length == 0)
                    {
                        clips = mi.defaultClipAnimations;
                    }
                    for (int i = 0; i < clips.Length; i++)
                    {
                        Debug.Log(clips[i].name);
                        if (clips[i].wrapMode != WrapMode.Loop)
                        {
                            clips[i].wrapMode = WrapMode.Once;
                        }
                        clips[i].name = name;
                    }
                    mi.clipAnimations = clips;
                }
                mi.SaveAndReimport();
            }
        }

    }

    [UnityEditor.MenuItem("Assets/Skin文件●规范化/带相机", false, 1450)]
    public static void ChangeSkinWithCamera()
    {
        var objs = Selection.objects;
        foreach (var obj in objs)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            ModelImporter mi = AssetImporter.GetAtPath(path) as ModelImporter;

            mi.importLights = false;
            mi.materialImportMode = ModelImporterMaterialImportMode.None;
            mi.importCameras = true;
            mi.importAnimation = false;
            mi.addCollider = false;
            mi.useFileScale = false;
            mi.SaveAndReimport();
        }
    }

    [UnityEditor.MenuItem("Assets/Skin文件●规范化/不带相机", false, 1450)]
    public static void ChangeSkinWithoutCamera()
    {
        var objs = Selection.objects;
        foreach (var obj in objs)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            ModelImporter mi = AssetImporter.GetAtPath(path) as ModelImporter;

            mi.importLights = false;
            mi.materialImportMode = ModelImporterMaterialImportMode.None;
            mi.importCameras = false;
            mi.importAnimation = false;
            mi.addCollider = false;
            mi.useFileScale = false;
            mi.SaveAndReimport();
        }
    }



    [UnityEditor.MenuItem("Assets/动作文件●循环模式", false, 1450)]
    public static void ChangeAnimationToLoop()
    {
        var objs = Selection.objects;
        foreach (var obj in objs)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            ModelImporter mi = AssetImporter.GetAtPath(path) as ModelImporter;
            if (mi != null)
            {
                //if (obj.name.Contains("idle"))
                {
                    ModelImporterClipAnimation[] clips = mi.clipAnimations;
                    if (mi.animationType == ModelImporterAnimationType.Legacy)
                    {
                        //ModelImporterClipAnimation[] clips = mi.clipAnimations;
                        for (int i = 0; i < clips.Length; i++)
                        {
                            clips[i].wrapMode = WrapMode.Loop;
                        }

                    }
                    else if (mi.animationType == ModelImporterAnimationType.Generic)
                    {
                        for (int i = 0; i < clips.Length; i++)
                        {
                            clips[i].loopTime = true;                            
                        }
                    }
                    mi.clipAnimations = clips;
                    //}
                    mi.SaveAndReimport();
                }
            }
        }
    }

    //[MenuItem("GameObject/给idle预制体添加动画", false,-100)]
    [MenuItem("Assets/idle预制体●添加动画/不包含dead", false, 1450)]
  
    public static void AddAnimationToPrefab()
    {
        var objs = Selection.objects;
        foreach (var obj in objs)
        {
            GameObject gameObject = obj as GameObject;
            string path = AssetDatabase.GetAssetPath(gameObject);
            path = Path.GetFullPath(path);
            path = path.Replace(Path.GetFileName(path), "");
            path = Path.Combine(path, gameObject.name.Replace("_idle", "") + "\\ani\\");
            Debug.Log(path);
            int childCount = gameObject.transform.childCount;
            Transform skin = null;
            Animation animation = null;
            var skinObj = FindSkinObj(gameObject);
            if (skinObj == null)
            {
                Debug.LogError("没有找到skin文件");
                return;
            }
            skin = skinObj.transform;
            Animator animator = skin.GetComponent<Animator>();
            if (animator != null)
            {
                GameObject.DestroyImmediate(animator, true);
            }
            animation = skin.GetComponent<Animation>();
            if (animation != null)
            {
                GameObject.DestroyImmediate(animation, true);
                AssetDatabase.Refresh();
            }
            animation = skin.gameObject.AddComponent<Animation>();
            var files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                string p = Path.GetFullPath(files[i]);
                p = p.Replace("\\", "/");
                p = p.Replace(Application.dataPath, "Assets/");
                Object[] anim = AssetDatabase.LoadAllAssetsAtPath(p);
                foreach (var item in anim)
                {
                    if (item.GetType() == typeof(AnimationClip) && (item.name.Contains("idle") && !item.name.Contains("skill")))
                    {
                        if (item.name == "idle1")
                        {
                           animation.clip = (AnimationClip)item;
                        }
                        AnimationClip c = item as AnimationClip;
                        c.legacy = true;
                        animation.AddClip(c, c.name);
                        AssetDatabase.SaveAssets();
                    }
                }
                //EditorUtility.SetDirty(animation);
                EditorUtility.SetDirty(skin);
                EditorUtility.SetDirty(gameObject);
                AssetDatabase.SaveAssets();

            }
            bool success = false;
            PrefabUtility.SavePrefabAsset(gameObject, out success);
            AssetDatabase.Refresh();
        }

        SetLayerCommonFish();
        IdlePrefabDisableCamera();
    }


    [MenuItem("Assets/idle预制体●添加动画/包含dead", false, 1450)]

    public static void AddAnimationToPrefabContainDead()
    {
        var objs = Selection.objects;
        foreach (var obj in objs)
        {
            GameObject gameObject = obj as GameObject;
            string path = AssetDatabase.GetAssetPath(gameObject);
            path = Path.GetFullPath(path);
            path = path.Replace(Path.GetFileName(path), "");
            path = Path.Combine(path, gameObject.name.Replace("_idle", "") + "\\ani\\");
            Debug.Log(path);
            int childCount = gameObject.transform.childCount;
            Transform skin = null;
            Animation animation = null;
            var skinObj = FindSkinObj(gameObject);
            if (skinObj == null)
            {
                Debug.LogError("没有找到skin文件");
                return;
            }
            skin = skinObj.transform;
            Animator animator = skin.GetComponent<Animator>();
            if (animator != null)
            {
                GameObject.DestroyImmediate(animator, true);
            }
            animation = skin.GetComponent<Animation>();
            if (animation != null)
            {
                GameObject.DestroyImmediate(animation, true);
                AssetDatabase.Refresh();
            }
            animation = skin.gameObject.AddComponent<Animation>();
            var files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                string p = Path.GetFullPath(files[i]);
                p = p.Replace("\\", "/");
                p = p.Replace(Application.dataPath, "Assets/");
                Object[] anim = AssetDatabase.LoadAllAssetsAtPath(p);
                foreach (var item in anim)
                {
                    if (item.GetType() == typeof(AnimationClip) && (((item.name.Contains("idle")|| item.name.Contains("dead"))) && !item.name.Contains("skill")))
                    {
                        if (item.name == "idle1")
                        {
                            animation.clip = (AnimationClip)item;
                        }
                        AnimationClip c = item as AnimationClip;
                        c.legacy = true;
                        animation.AddClip(c, c.name);
                        AssetDatabase.SaveAssets();
                    }
                }
                //EditorUtility.SetDirty(animation);
                EditorUtility.SetDirty(skin);
                EditorUtility.SetDirty(gameObject);
                AssetDatabase.SaveAssets();

            }

            bool success = false;
            PrefabUtility.SavePrefabAsset(gameObject, out success);
            AssetDatabase.Refresh();
        }
    }

    private static GameObject FindSkinObj(GameObject prefabObj)
    {
        if (prefabObj.name.Contains("skin"))
        {
            return prefabObj;
        }
        int childCount = prefabObj.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var res = FindSkinObj(prefabObj.transform.GetChild(i).gameObject);
            if (res)
            {
                return res;
            }           
        }
        return null;
    }


    [MenuItem("Assets/skill预制体●添加动画", false, 1450)]
    public static void AddAnimatorToPrefab()
    {
        //创建动画控制器
        var objs = Selection.objects;
        foreach (var obj in objs)
        {
            GameObject gameObject = obj as GameObject;
            string path = AssetDatabase.GetAssetPath(gameObject);
            path = Path.GetFullPath(path);
            path = path.Replace(Path.GetFileName(path), "");
            path = Path.Combine(path, gameObject.name.Replace("_skill", "") + "\\ani\\");
            string filePath = path + gameObject.name + ".controller";
            Debug.Log(filePath);
            AnimatorController controller;
            string loadPath = filePath.Replace("\\", "/");
            loadPath = loadPath.Replace(Application.dataPath, "Assets/");
            if(File.Exists(filePath))
            {
                controller = AssetDatabase.LoadAssetAtPath(loadPath,typeof(AnimatorController)) as AnimatorController;
            }
            else
            {
                controller = AnimatorController.CreateAnimatorControllerAtPath(loadPath);                
            }
           
            int childCount = gameObject.transform.childCount;
            Transform skin = null;
            Animator animator;

            for (int i = 0; i < childCount; i++)
            {
                skin = gameObject.transform.GetChild(i);
                if (skin.name.Contains("skin"))
                {
                    Animation animation = skin.GetComponent<Animation>();
                    if (animation != null)
                    {                        
                        GameObject.DestroyImmediate(animation,true);
                    }
                    animator = skin.GetComponent<Animator>();
                    if (animator == null)
                    {
                        animator = skin.gameObject.AddComponent<Animator>();
                    }
                    animator.runtimeAnimatorController = controller;
                    break;
                }                
            }
            if(controller.layers == null ||controller.layers.Length == 0)
            {
                AnimatorControllerLayer layer = new AnimatorControllerLayer();
                layer.name = "Base Layer";
                controller.AddLayer(layer);
            }
            AnimatorStateMachine sm = controller.layers[0].stateMachine;
            //todo 清空state
            var states = sm.states;
            for (int i = states.Length - 1; i >=0; i--)
            {                
                sm.RemoveState(states[i].state);
            }
            
       
            var files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                string p = Path.GetFullPath(files[i]);
                p = p.Replace("\\", "/");
                p = p.Replace(Application.dataPath, "Assets/");
                Object[] anim = AssetDatabase.LoadAllAssetsAtPath(p);
                foreach (var item in anim)
                {
                    if (item.GetType() == typeof(AnimationClip) && item.name.Contains("skill"))
                    {
                        AnimationClip c = item as AnimationClip;                        
                        AnimatorState state = sm.AddState(c.name);
                        state.motion = c;
                    }
                }
                AssetDatabase.SaveAssets();
            }

            bool success = false;
            PrefabUtility.SavePrefabAsset(gameObject, out success);
            AssetDatabase.Refresh();
        }
    }

    [MenuItem("Assets/enter预制体●添加动画", false, 1450)]
    public static void AddAnimationToEnterPrefab()
    {
          var objs = Selection.objects;
        foreach (var obj in objs)
        {
            GameObject gameObject = obj as GameObject;
            string path = AssetDatabase.GetAssetPath(gameObject);
            path = Path.GetFullPath(path);
            path = path.Replace(Path.GetFileName(path), "");
            path = Path.Combine(path,gameObject.name.Replace("_enter","")+"\\ani\\");
            int childCount = gameObject.transform.childCount;
            Transform skin = null;
            Animation animation = null;

            for (int i = 0; i < childCount; i++)
            {
                skin = gameObject.transform.GetChild(i);
                if (skin.name.Contains("skin"))
                {
                    Animator animator = skin.GetComponent<Animator>();
                    if (animator != null)
                    {
                        GameObject.DestroyImmediate(animator,true);
                    }
                    animation = skin.GetComponent<Animation>();
                    if (animation == null)
                    {
                        animation = skin.gameObject.AddComponent<Animation>();
                    }
                }                
            }

            if (animation != null)
            {
                for (int i = 0; i < 7; i++)
                {
                    var clip = animation.GetClip("enter");
                    if (clip != null)
                    {
                        animation.RemoveClip(clip);
                    }
                }
            }           
            var files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                string p = Path.GetFullPath(files[i]);
                p = p.Replace("\\", "/");
                p = p.Replace(Application.dataPath, "Assets/");
                Object[] anim = AssetDatabase.LoadAllAssetsAtPath(p);
                foreach (var item in anim)
                {
                    if(item.GetType() == typeof(AnimationClip)&&(item.name.Contains("enter")&& !item.name.Contains("skill")))
                    {
                        if(item.name == "enter")
                        {
                            animation.clip = (AnimationClip)item;
                        }
                        AnimationClip c = item as AnimationClip;
                        c.legacy = true;
                        animation.AddClip(c, c.name);
                        AssetDatabase.SaveAssets();
                    }
                }
                EditorUtility.SetDirty(animation);
                EditorUtility.SetDirty(skin);
                EditorUtility.SetDirty(gameObject); 
                AssetDatabase.SaveAssets();
               
            }

            bool success = false;
            PrefabUtility.SavePrefabAsset(gameObject, out success);
            AssetDatabase.Refresh();
        }
    }


    [MenuItem("Assets/idle预制体关闭摄像机", false, 1450)]
    public static void IdlePrefabDisableCamera()
    {
        var objs = Selection.objects;
        foreach (var obj in objs)
        {
            GameObject gameObject = obj as GameObject;
            if (gameObject != null)
            {
                var cams = gameObject.GetComponentsInChildren<Camera>();
                for (int i = 0; i < cams.Length; i++)
                {
                    cams[i].gameObject.SetActive(false);
                }
            }
            EditorUtility.SetDirty(gameObject);
            AssetDatabase.SaveAssets();
        }
    }


    [MenuItem("Assets/预制体层级设置/CommonFish", false, 1450)]
    public static void SetLayerCommonFish()
    {
        var objs = Selection.objects;
        foreach (var obj in objs)
        {
            GameObject gameObject = obj as GameObject;
            SetLayer(gameObject.transform, 10);
            EditorUtility.SetDirty(gameObject);
            AssetDatabase.SaveAssets();
        }
    }

    [MenuItem("Assets/预制体层级设置/DeadFish", false, 1450)]
    public static void SetLayerDeadFish()
    {
        var objs = Selection.objects;
        foreach (var obj in objs)
        {
            GameObject gameObject = obj as GameObject;
            SetLayer(gameObject.transform, 13);
            EditorUtility.SetDirty(gameObject);
            AssetDatabase.SaveAssets();
        }
    }

    [MenuItem("Assets/预制体层级设置/FishAboveUI", false, 1450)]
    public static void SetLayerFishAboveUI()
    {
        var objs = Selection.objects;
        foreach (var obj in objs)
        {
            GameObject gameObject = obj as GameObject;
            SetLayer(gameObject.transform, 19);
            EditorUtility.SetDirty(gameObject);
            AssetDatabase.SaveAssets();
        }
    }

    private static void SetLayer(Transform t,int layer)
    {
        t.gameObject.layer = layer;
        if(t.childCount == 0)
        {
            return;
        }
        for (int i = 0;i < t.childCount; i++)
        {
            SetLayer(t.GetChild(i), layer);
        }
    }
}