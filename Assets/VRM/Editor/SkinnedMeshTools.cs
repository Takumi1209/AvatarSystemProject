using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

static class SkinnedMeshTools
{
    [MenuItem("CONTEXT/SkinnedMeshRenderer/Update Bone Hierarchy")]
    static void UpdateBoneHierarchy(MenuCommand cmd)
    {
        var targetSkin = cmd.context as SkinnedMeshRenderer;

        if (targetSkin.sharedMesh == null)
        {
            return;
        }

        var path = AssetDatabase.GetAssetPath(targetSkin.sharedMesh);

        foreach (var obj in AssetDatabase.LoadAllAssetsAtPath(path))
        {
            if (obj is GameObject go)
            {
                SkinnedMeshRenderer sourceSkin = null;

                foreach (var skin in go.GetComponentsInChildren<SkinnedMeshRenderer>(true))
                {
                    if (skin.sharedMesh == targetSkin.sharedMesh)
                    {
                        sourceSkin = skin;
                        break;
                    }
                }

                if (sourceSkin != null && sourceSkin.rootBone != null && 0 < sourceSkin.bones.Length)
                {
                    ReplaceBones(targetSkin, sourceSkin);
                    break;
                }
            }
        }

        EditorUtility.SetDirty(targetSkin);
    }

    static void ReplaceBones(SkinnedMeshRenderer target, SkinnedMeshRenderer source)
    {
        var targetRoot = target.rootBone;
        var sourceRoot = source.rootBone;
        var sourceBones = source.bones;

        List<Transform> newBones = new List<Transform>();

        foreach (var bone in sourceBones)
        {
            var path = GetRelativePath(sourceRoot, bone);
            var targetBone = GetOrCreateTransform(targetRoot, path);
            targetBone.localPosition = bone.localPosition;
            targetBone.localRotation = bone.localRotation;
            targetBone.localScale = bone.localScale;
            newBones.Add(targetBone);
        }

        target.bones = newBones.ToArray();
    }

    static Transform GetOrCreateTransform(Transform root, string path)
    {
        if (!string.IsNullOrEmpty(path))
        {
            foreach (var name in path.Split('/'))
            {
                var child = root.Find(name);

                if (child == null)
                {
                    var go = new GameObject(name);
                    child = go.transform;
                    child.SetParent(root, false);
                }

                root = child;
            }
        }
        return root;
    }

    static string GetRelativePath(Transform root, Transform target)
    {
        if (target != root)
        {
            string result = "";

            while (target != null && target != root)
            {
                if (0 < result.Length)
                {
                    result = "/" + result;
                }

                result = target.name + result;
                target = target.parent;

                if (target == root)
                {
                    return result;
                }
            }
        }

        return null;
    }
}
