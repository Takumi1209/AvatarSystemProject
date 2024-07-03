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
    public string currentSceneName = "VRMCVVTuberExample"; // アバターを置き換えるシーン名

    // ファイルダイアログを開いてVRMファイルをロードする関数
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

    // VRMをインポートする関数
    private async void ImportVRM(string path)
    {
        using (var data = new AutoGltfFileParser(path).Parse())
        {
            var vrm = new VRMData(data);

            using (var loader = new VRMImporterContext(vrm))
            {
                // メタデータを読み込む
                var meta = await loader.ReadMetaAsync(new ImmediateCaller(), false);
                Debug.Log("Meta data loaded");
                



                // VRMモデルを読み込む
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

    // 既存のVRMアバターを置き換える関数
    private void ReplaceAvatar(GameObject newAvatar)
    {
        // 他のシーンのターゲットアバターが "PlayerAvatar" タグ付けされていると仮定
        GameObject oldAvatar = GameObject.FindWithTag("PlayerAvatar");
        if (oldAvatar != null)
        {
            Destroy(oldAvatar);
        }

        newAvatar.tag = "PlayerAvatar";

        // プレイモードでのみ DontDestroyOnLoad を呼び出す
        if (Application.isPlaying)
        {
            DontDestroyOnLoad(newAvatar); // 新しいアバターがシーンをロードしても破壊されないようにする
        }
        else
        {
            Debug.LogWarning("DontDestroyOnLoad can only be used in play mode.");
        }

        // シーンがビルド設定に存在するかどうかを確認
        if (SceneExists(currentSceneName))
        {
            SceneManager.LoadScene(currentSceneName);
        }
        else
        {
            Debug.LogError($"Scene '{currentSceneName}' couldn't be loaded because it has not been added to the build settings or the AssetBundle has not been loaded.");
        }
    }

    // シーンがビルド設定に存在するかどうかを確認する関数
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
                // 新しいアバターを正しい位置/回転に設定
                newAvatar.transform.position = Vector3.zero; // 望む位置に設定
                newAvatar.transform.rotation = Quaternion.identity; // 望む回転に設定
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
