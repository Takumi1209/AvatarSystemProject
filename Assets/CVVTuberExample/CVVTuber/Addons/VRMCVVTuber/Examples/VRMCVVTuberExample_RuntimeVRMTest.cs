using CVVTuber;
using CVVTuber.VRM;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using SFB;

namespace CVVTuberExample
{
    public class VRMCVVTuberExample_RuntimeVRMTest : MonoBehaviour
    {
        ///////
        public RuntimeVRMMetaLoader runtimeVRMMetaLoader;

        public VRMCVVTuberControllManager vRMControllManager;
        ///////

        /// <summary>
        /// The webcam texture mat source getter.
        /// </summary>
        public WebCamTextureMatSourceGetter webCamTextureMatSourceGetter;

        /// <summary>
        /// The dlib face landmark getter.
        /// </summary>
        public DlibFaceLandmarkGetter dlibFaceLandmarkGetter;

        // Use this for initialization
        void Start()
        {
            // Load global settings.
            dlibFaceLandmarkGetter.dlibShapePredictorFileName = CVVTuberExample.dlibShapePredictorFileName;
            dlibFaceLandmarkGetter.dlibShapePredictorMobileFileName = CVVTuberExample.dlibShapePredictorFileName;
        }

        /// <summary>
        /// Raises the back button click event.
        /// </summary>
        public void OnBackButtonClick()
        {
            SceneManager.LoadScene("CVVTuberExample");
        }

        /// <summary>
        /// Raises the change camera button click event.
        /// </summary>
        public void OnChangeCameraButtonClick()
        {
            webCamTextureMatSourceGetter.ChangeCamera();
        }

        /////
        public void OnLoadVRMButtonClick()
        {
            StartCoroutine(LoadVRM());
        }

        private IEnumerator LoadVRM()
        {
            ExtensionFilter[] extensions = new[] {
               new ExtensionFilter("VRM Files", "vrm", "VRM"),
            };

            if (runtimeVRMMetaLoader != null)
            {
                string[] paths = StandaloneFileBrowser.OpenFilePanel("Load VRM File", "", extensions, true);
                string path = paths[0];
                //var path = OpenCVForUnity.UnityUtils.Utils.getFilePath("VRM/AliciaSolid_vrm-0.51.vrm");
                if (string.IsNullOrEmpty(path))
                {
                    yield break;
                }

                Debug.Log("path: " + path);

                runtimeVRMMetaLoader.vrmFilePath = path;


                yield return runtimeVRMMetaLoader.LoadVRMMetaAsync();

                if (vRMControllManager != null)
                {
                    vRMControllManager.changeVRM(runtimeVRMMetaLoader.GetMeta());
                }
            }
        }
        /////
    }
}