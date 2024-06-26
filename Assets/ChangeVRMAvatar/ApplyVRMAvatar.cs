using UnityEngine;

public class ApplyVRMAvatar : MonoBehaviour
{
    public Vector3 spawnPoint = new Vector3(2, 1, 0);  // The point where the avatar will be instantiated

    private void Start()
    {
        if (VRMManager.Instance != null && VRMManager.Instance.LoadedAvatar != null)
        {
            GameObject avatar = Instantiate(VRMManager.Instance.LoadedAvatar, spawnPoint, Quaternion.identity);
            VRMManager.Instance.SetLoadedAvatar(avatar);  // Update the reference to the instantiated avatar
        }
        else
        {
           // Debug.LogError("No VRM avatar found to apply.");
        }
    }
}

