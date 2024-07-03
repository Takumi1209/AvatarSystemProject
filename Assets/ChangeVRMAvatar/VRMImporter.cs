using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VRM;
using VRMShaders;
using SFB; // StandaloneFileBrowser�̖��O���
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
        // VRM�̉��
        using (var data = new AutoGltfFileParser(path).Parse())
        {
            var vrm = new VRMData(data);

            using (var context = new VRMImporterContext(vrm))
            {
                // ���^����ǂݎ��
                var meta = await context.ReadMetaAsync(new ImmediateCaller(), false);

                // VRM�����[�h
                var instance = await context.LoadAsync(new RuntimeOnlyAwaitCaller());

                // �V�����A�o�^�[��ݒ�
                GameObject newAvatar = instance.gameObject;
                newAvatar.transform.SetParent(transform, false);
                newAvatar.transform.localPosition = Vector3.zero;
                newAvatar.transform.localRotation = Quaternion.identity;

                // �A�o�^�[�}�l�[�W���[�ɐV�����A�o�^�[��ݒ�
                AvatarManager.Instance.SetAvatar(newAvatar);

                Debug.Log("VRM Loaded: " + meta.Title);
            }
        }
    }
}
