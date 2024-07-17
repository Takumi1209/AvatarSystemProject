using SFB;
using System;
using System.Collections;
using System.Windows.Forms;
using UnityEngine;
using VRM;
using VRMShaders;
using UnityEngine.UI;
using AddonScripts;
using Mebiustos.BreathController;
using UniGLTF.Extensions.VRMC_vrm;
using UniHumanoid;


namespace CVVTuber.VRM
{
    public class RuntimeVRMMetaLoader : MonoBehaviour
    {
        [Tooltip("A filename for runtime loading from StreamingAssets folder. (e.g. \"VRM/Zundamon_human_vrm.vrm\")")]
        public string vrmFilePath = "VRM/Zundamon_human_vrm.vrm";

        public UnityEngine.UI.Button LoadVRMButton;

        [Space(5)]

        GameObject m_model;

        public virtual bool isDone { get; protected set; }

        public virtual bool isError { get; protected set; }

        public virtual void Dispose()
        {
            isDone = isError = false;

            GameObject.Destroy(m_model);
        }

        public virtual VRMMeta GetMeta()
        {
            if (isDone == false || isError == true)
                return null;

            return m_model.GetComponent<VRMMeta>();
        }

        public virtual IEnumerator LoadVRMMetaAsync()
        {
            yield return OpenCVForUnity.UnityUtils.Utils.getFilePathAsync(vrmFilePath, (result) =>
            {
                ImportVRMAsync(result);
            });

            while (!isDone)
            {
                yield return null;
            }
        }

        protected async void ImportVRMAsync(string path)
        {
            var loaded = await VrmUtility.LoadAsync(path);
            loaded.ShowMeshes();
            loaded.EnableUpdateWhenOffscreen();

            if (m_model != null)
            {
                GameObject.Destroy(m_model.gameObject);
            }

            m_model = loaded.gameObject;
            
            var meta = loaded.gameObject.GetComponent<VRMMeta>();
            if (meta == null)
            {
                isDone = isError = true;
            }
            else
            {
                var metaObject = meta.Meta;
               // Debug.LogFormat("meta: title:{0}", metaObject.Title);
               // Debug.LogFormat("meta: version:{0}", metaObject.Version);
               // Debug.LogFormat("meta: author:{0}", metaObject.Author);
               // Debug.LogFormat("meta: exporterVersion:{0}", metaObject.ExporterVersion);

                isDone = true;
            }

            var animator = m_model.GetComponent<Animator>();
            var leftEye = animator.GetBoneTransform(HumanBodyBones.LeftEye);
            var rightEye = animator.GetBoneTransform(HumanBodyBones.RightEye);

            VRMBlendShapeProxy vrmBlendShapePloxy = m_model.GetComponent<VRMBlendShapeProxy>();
            VRMLookAtHead vrmLookAtHead = m_model.GetComponent<VRMLookAtHead>();

            //AutoMoving script Attach
            EyeJitter eyeJitter = m_model.AddComponent<EyeJitter>();
            eyeJitter.rightEye = rightEye;
            eyeJitter.leftEye = leftEye;

            vrmAutoController vrmautoController = m_model.AddComponent<vrmAutoController>();
            vrmautoController.blendShapeProxy = vrmBlendShapePloxy;
            vrmautoController.VRMLookAtHead = vrmLookAtHead;

            m_model.AddComponent<BreathController>();
        }

        void Update()
        {
          
        }

        ///////////
        // Interrupt process to set Meta of VRM loaded at runtime to VRMLoader before Start of VRMCVVTuberControllManager is done.
        IEnumerator Start()
        {
            var runtimeVrmMetaLoader = GetComponent<RuntimeVRMMetaLoader>();
            if (runtimeVrmMetaLoader != null)
            {
                yield return runtimeVrmMetaLoader.LoadVRMMetaAsync();

                var vrmLoader = GetComponent<VRMLoader>();
                if (vrmLoader != null)
                {
                    vrmLoader.meta = runtimeVrmMetaLoader.GetMeta();
                }
            }

            if (LoadVRMButton != null)
            {
                LoadVRMButton.onClick.AddListener(LaodVRM);
            }
        }
       

       

        ExtensionFilter[] extensions = new[] {
        new ExtensionFilter("VRM Files", "vrm", "VRM"),
        };

        public void LaodVRM()
        {
            string[] paths = StandaloneFileBrowser.OpenFilePanel("Load VRM File", "", extensions, true);
            // Vrm10Instance vrm10Instance = await Vrm10.LoadPathAsync(paths[0]);
            string path = paths[0];

            // すでに開かれているVRMファイルがある場合は削除する。
            GameObject currentVrm = GameObject.Find("VRM");

            if (currentVrm != null)
            {
                Destroy(currentVrm);
            }

            ImportVRMAsync(path);

        }



    }
}
