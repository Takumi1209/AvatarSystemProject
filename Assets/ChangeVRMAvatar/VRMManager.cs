using UnityEngine;
using VRM;

public class VRMManager : MonoBehaviour
{
    public static VRMManager Instance { get; private set; }
    public GameObject LoadedAvatar { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SetLoadedAvatar(GameObject avatar)
    {
        LoadedAvatar = avatar;
    }
}
