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
    // デフォルトで読み込みたいモデルがある場合はpathをセットする。
    string path = "";

    [SerializeField] private RuntimeAnimatorController vrmAnimatorController;

    private VRMLoader vrmLoader;

    // Open file with filter
    // 拡張子がvrmまたはVRMのファイルのみ選択させる。
    ExtensionFilter[] extensions = new[] {
        new ExtensionFilter("VRM Files", "vrm", "VRM"),
    };

    // Start is called before the first frame update
    async void Start()
    {
        if (path != "" && path != null)
        {
            // VRMファイルのロード
            Vrm10Instance vrm10Instance = await Vrm10.LoadPathAsync(path);

            // セットされたVRMのAnimatorコンポーネントに事前に準備したRuntimeAnimationControllerをセット
            SetAnimatorController();
        }

    }

    // Update is called once per frame
    void Update() { }

    // ButtonコンポーネントとOn Click()にセットして利用することを想定
    public void LaodVRM()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Load VRM File", "", extensions, true);
        // Vrm10Instance vrm10Instance = await Vrm10.LoadPathAsync(paths[0]);
        string path = paths[0];

        // すでに開かれているVRMファイルがある場合は削除する。
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
        // VRMファイルのロード
        Vrm10Instance vrm10Instance = await Vrm10.LoadPathAsync(paths[0]);

        vrm10Instance.name = "VRMAvatar";
        // セットされたVRMのAnimatorコンポーネントに事前に準備したRuntimeAnimationControllerをセット
        SetAnimatorController();

        // VRMのメタ情報を取得
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
        // 準備ができたら表示する(デフォルトでは非表示)
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
