using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarManager : MonoBehaviour
{
    public static AvatarManager Instance { get; private set; }
    public GameObject CurrentAvatar { get; private set; }
    public string avatarNameInScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetAvatar(GameObject newAvatar)
    {
        if (CurrentAvatar != null)
        {
            Destroy(CurrentAvatar);
        }
        CurrentAvatar = newAvatar;
    }

    public void LoadSceneAndFindAvatar(string sceneName, string avatarName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
        {
            if (scene.name == sceneName)
            {
                var avatar = GameObject.Find(avatarName);
                if (avatar != null)
                {
                    CurrentAvatar = avatar;
                }
            }
        };
    }
}
