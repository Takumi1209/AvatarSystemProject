using UnityEngine;
using System.Threading.Tasks;
using UniVRM10;
using SFB;
using CVVTuber.VRM;
using VRM;
using VRMShaders;
using UniGLTF;
using UniVRM10.Migration;


public class UniVrm10 : MonoBehaviour
{
    // �f�t�H���g�œǂݍ��݂������f��������ꍇ��path���Z�b�g����B
    string path = "";

    [SerializeField] private RuntimeAnimatorController vrmAnimatorController;

    private VRMLoader vrmLoader;

    // Open file with filter
    // �g���q��vrm�܂���VRM�̃t�@�C���̂ݑI��������B
    ExtensionFilter[] extensions = new[] {
        new ExtensionFilter("VRM Files", "vrm", "VRM"),
    };

    // Start is called before the first frame update
    async void Start()
    {
        if (path != "" && path != null)
        {
            // VRM�t�@�C���̃��[�h
            Vrm10Instance vrm10Instance = await Vrm10.LoadPathAsync(path);

            // �Z�b�g���ꂽVRM��Animator�R���|�[�l���g�Ɏ��O�ɏ�������RuntimeAnimationController���Z�b�g
            SetAnimatorController();
        }

    }

    // Update is called once per frame
    void Update() { }

    // Button�R���|�[�l���g��On Click()�ɃZ�b�g���ė��p���邱�Ƃ�z��
    public void LaodVRM()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Load VRM File", "", extensions, true);
        // Vrm10Instance vrm10Instance = await Vrm10.LoadPathAsync(paths[0]);
        string path = paths[0];

        // ���łɊJ����Ă���VRM�t�@�C��������ꍇ�͍폜����B
        GameObject currentVrm = GameObject.Find("VRMAvatar");
        if (currentVrm != null)
        {
            Destroy(currentVrm);
        }
       // OpenVRM(paths);
        // LoadAsync(path);

       // SetAnimatorController();
       // SetVRMMeta();
    }

    private async Task OpenVRM(string[] paths)
    {
        // VRM�t�@�C���̃��[�h
        Vrm10Instance vrm10Instance = await Vrm10.LoadPathAsync(paths[0]);

        vrm10Instance.name = "VRMAvatar";
        // �Z�b�g���ꂽVRM��Animator�R���|�[�l���g�Ɏ��O�ɏ�������RuntimeAnimationController���Z�b�g
        SetAnimatorController();

        // VRM�̃��^�����擾
        var meta = vrm10Instance.Vrm.Meta;
        // vrmLoader.LoadMeta(meta);
    }

    /*
    private async Task LoadAsync(string paths)
    {
       var gltfData = new AutoGltfFileParser(paths).Parse();
        var vrmData = new VRMData(gltfData);
        var context = new VRMImporterContext(vrmData);
        RuntimeGltfInstance instance = await context.LoadAsync(new ImmediateCaller());

        var root = instance.Root;
        root.name = "VRMAvatar";

        SetAnimatorController();

        instance.EnableUpdateWhenOffscreen();
        // �������ł�����\������(�f�t�H���g�ł͔�\��)
        instance.ShowMeshes();
    }
    */
    private void SetAnimatorController()
    {
        GameObject vrm1 = GameObject.Find("VRMAvatar");
        Animator vrm1Animator = vrm1.GetComponent<Animator>();
        vrm1Animator.runtimeAnimatorController = vrmAnimatorController;
    }

    private void SetVRMMeta()
    {

       GameObject vrm = GameObject.Find("VRMAvatar");
       VRMMeta vrmMeta = vrm.GetComponent<VRMMeta>();
       vrmLoader.LoadMeta(vrmMeta);
        
    }

   
}
