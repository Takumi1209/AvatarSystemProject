using CVVTuber;
using CVVTuber.VRM;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CVVTuberExample
{
    public class VRMCVVTuberExample : MonoBehaviour
    {
        /// <summary>
        /// The webcam texture mat source getter.
        /// </summary>
        public WebCamTextureMatSourceGetter webCamTextureMatSourceGetter;

        /// <summary>
        /// The dlib face landmark getter.
        /// </summary>
        public DlibFaceLandmarkGetter dlibFaceLandmarkGetter;

        public FaceAnimationController faceAnimationController;

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

        public Toggle Toggle;
        public void OnChangeQuestionToggle()
        {
            
            if (Toggle.isOn)
            {
                faceAnimationController.enableNoseAndJaw = true;
            }
            else
            {
                faceAnimationController.enableNoseAndJaw = false;
            }
        }
    }
}