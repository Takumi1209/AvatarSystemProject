using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRM;
using VRMShaders;
using SFB;
using UniGLTF;
using UnityEditor;
using System.Collections;

public class VRMAvatarLoader : MonoBehaviour
{
    public string currentSceneName = "VRMCVVTuberExample"; // �A�o�^�[��u��������V�[����

    // �t�@�C���_�C�A���O���J����VRM�t�@�C�������[�h����֐�
    public void LoadVRM()
    {
        var extensions = new[] {
            new ExtensionFilter("VRM Files", "vrm"),
        };
        var paths = StandaloneFileBrowser.OpenFilePanel("Open VRM Avatar", "", extensions, false);

        GameObject oldAvatar = GameObject.FindWithTag("PlayerAvatar");
        if (oldAvatar != null)
        {
            Destroy(oldAvatar);
        }
        if (paths.Length > 0)
        {
            string path = paths[0];
            ImportVRM(path);
        }
    }

    // VRM���C���|�[�g����֐�
    private async void ImportVRM(string path)
    {
        using (var data = new AutoGltfFileParser(path).Parse())
        {
            var vrm = new VRMData(data);

            using (var loader = new VRMImporterContext(vrm))
            {
                // ���^�f�[�^��ǂݍ���
                var meta = await loader.ReadMetaAsync(new ImmediateCaller(), false);
                Debug.Log("Meta data loaded");
                



                // VRM���f����ǂݍ���
                try
                {
                    var instance = await loader.LoadAsync(new RuntimeOnlyAwaitCaller());
                    Debug.Log("VRM model loaded");

                    if (instance != null && instance.Root != null)
                    {
                        ReplaceAvatar(instance.Root);
                    }
                    else
                    {
                        Debug.LogError("Failed to load VRM model.");
                    }
                }
                catch (MissingReferenceException ex)
                {
                    Debug.LogError($"Mesh reference was destroyed: {ex.Message}");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"An error occurred while loading the VRM model: {ex.Message}");
                }
            }
        }
    }

    // ������VRM�A�o�^�[��u��������֐�
    private void ReplaceAvatar(GameObject newAvatar)
    {
        // ���̃V�[���̃^�[�Q�b�g�A�o�^�[�� "PlayerAvatar" �^�O�t������Ă���Ɖ���
        GameObject oldAvatar = GameObject.FindWithTag("PlayerAvatar");
        if (oldAvatar != null)
        {
            Destroy(oldAvatar);
        }

        newAvatar.tag = "PlayerAvatar";

        // �v���C���[�h�ł̂� DontDestroyOnLoad ���Ăяo��
        if (Application.isPlaying)
        {
            DontDestroyOnLoad(newAvatar); // �V�����A�o�^�[���V�[�������[�h���Ă��j�󂳂�Ȃ��悤�ɂ���
        }
        else
        {
            Debug.LogWarning("DontDestroyOnLoad can only be used in play mode.");
        }

        // �V�[�����r���h�ݒ�ɑ��݂��邩�ǂ������m�F
        if (SceneExists(currentSceneName))
        {
            SceneManager.LoadScene(currentSceneName);
        }
        else
        {
            Debug.LogError($"Scene '{currentSceneName}' couldn't be loaded because it has not been added to the build settings or the AssetBundle has not been loaded.");
        }
    }

    // �V�[�����r���h�ݒ�ɑ��݂��邩�ǂ������m�F����֐�
    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == currentSceneName)
        {
            GameObject newAvatar = GameObject.FindWithTag("PlayerAvatar");
            if (newAvatar != null)
            {
                // �V�����A�o�^�[�𐳂����ʒu/��]�ɐݒ�
                newAvatar.transform.position = Vector3.zero; // �]�ވʒu�ɐݒ�
                newAvatar.transform.rotation = Quaternion.identity; // �]�މ�]�ɐݒ�
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
