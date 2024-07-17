using CVVTuber.VRM;
using SFB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVRMAvatar : MonoBehaviour
{

    RuntimeVRMMetaLoader runtimeVRMLoader;

    ExtensionFilter[] extensions = new[] {
        new ExtensionFilter("VRM Files", "vrm", "VRM"),
        };
    public void LaodVRM()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Load VRM File", "", extensions, true);
        // Vrm10Instance vrm10Instance = await Vrm10.LoadPathAsync(paths[0]);
        string path = paths[0];

        // ���łɊJ����Ă���VRM�t�@�C��������ꍇ�͍폜����B
        GameObject currentVrm = GameObject.Find("VRM");

        if (currentVrm != null)
        {
            Destroy(currentVrm);
        }

       // runtimeVRMLoader.ImportVRMAsync(path);

    }
}
