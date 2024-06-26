using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniGLTF;
using VRM;
using VRMShaders;

public class LoadModel : MonoBehaviour
{
    RuntimeGltfInstance instance;
    string path = "C:/Users/aise-member/Desktop/VRMAvatars/a.vrm";

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update() { }

    async void Load()
    {
        Debug.Log(path);
        this.instance = await VrmUtility.LoadAsync(path, new RuntimeOnlyAwaitCaller());
        // this.instance.EnableUpdateWhenOffscreen();
        this.instance.ShowMeshes();
    }
}
