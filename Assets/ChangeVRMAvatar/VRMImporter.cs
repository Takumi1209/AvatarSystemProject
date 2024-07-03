using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VRM;
using VRMShaders;
using SFB; // StandaloneFileBrowserの名前空間
using UniGLTF;

public class VRMImporter : MonoBehaviour
{
    public Button importButton;

    void Start()
    {
        importButton.onClick.AddListener(OnImportButtonClick);
    }

    async void OnImportButtonClick()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open VRM Avatar", "", "vrm", false);
        if (paths.Length > 0)
        {
            string path = paths[0];
            await LoadVRM(path);
        }
    }

    async Task LoadVRM(string path)
    {
        // VRMの解析
        using (var data = new AutoGltfFileParser(path).Parse())
        {
            var vrm = new VRMData(data);

            using (var context = new VRMImporterContext(vrm))
            {
                // メタ情報を読み取る
                var meta = await context.ReadMetaAsync(new ImmediateCaller(), false);

                // VRMをロード
                var instance = await context.LoadAsync(new RuntimeOnlyAwaitCaller());

                // 新しいアバターを設定
                GameObject newAvatar = instance.gameObject;
                newAvatar.transform.SetParent(transform, false);
                newAvatar.transform.localPosition = Vector3.zero;
                newAvatar.transform.localRotation = Quaternion.identity;

                // アバターマネージャーに新しいアバターを設定
                AvatarManager.Instance.SetAvatar(newAvatar);

                Debug.Log("VRM Loaded: " + meta.Title);
            }
        }
    }
}
