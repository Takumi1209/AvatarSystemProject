using CVVTuber.VRM;
using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UniGLTF;
using Unity.VisualScripting;
using UnityEngine;
using UniVRM10;
using VRM;
using VRMShaders;

public class VRMImporter : MonoBehaviour
{
    // string initialPath =�@"Assets/Zundamon/Zundamon_human_vrm.vrm" ;

    [SerializeField] private RuntimeAnimatorController vrmAnimatorController;


    public GameObject Player;

    // Open file with filter
    // �g���q��vrm�܂���VRM�̃t�@�C���̂ݑI��������B
    ExtensionFilter[] extensions = new[] {
        new ExtensionFilter("VRM Files", "vrm", "VRM"),
    };

    // Button�R���|�[�l���g��On Click()�ɃZ�b�g���ė��p���邱�Ƃ�z��
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
      
       // LoadAsync(path);
    
    }

   
    private async Task<GameObject> LoadAsync(string path)
    {
        var instance = await VrmUtility.LoadAsync(path, new RuntimeOnlyAwaitCaller());

        SetAnimatorController();
        // SetVRMMeta();
        var meta = instance.Root.GetComponent<VRMMeta>();

        // SkinnedMeshRenderer �ɑ΂���w��
        instance.EnableUpdateWhenOffscreen();
        // �������ł�����\������(�f�t�H���g�ł͔�\��)
        instance.ShowMeshes();

       // vrmLoader = GetComponent<VRMLoader>();
       // vrmLoader.LoadMeta(meta);
      
        return instance.Root;
    }

    private void SetAnimatorController()
    {
        GameObject vrm1 = GameObject.Find("VRM");
        Animator vrm1Animator = vrm1.GetComponent<Animator>();
        vrm1Animator.runtimeAnimatorController = vrmAnimatorController;
    }

    private void SetVRMMeta()
    {

        GameObject vrm = GameObject.Find("VRM");
        VRMMeta vrmMeta = vrm.GetComponent<VRMMeta>();
        //vrmLoader.LoadMeta(vrmMeta);

    }
}
