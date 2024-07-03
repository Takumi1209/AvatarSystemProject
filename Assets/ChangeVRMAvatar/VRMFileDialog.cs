using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniGLTF;
using VRM;
using VRMShaders;
using UnityEngine.UI;
using SFB;
//using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class VRMFileDialog : MonoBehaviour
{
    public RawImage displayImage;
    RuntimeGltfInstance _instance;
    string defaultDirectory = "C:/Users/aise-member/Desktop/VRMAvatars";

    public void OpenFileDialog()
    {
        var extensions = new[] {
            new ExtensionFilter("VRM Files", "vrm"),
            new ExtensionFilter("All Files", "*" ),
        };

        // Open the file dialog and get the selected file path(s)
        var paths = StandaloneFileBrowser.OpenFilePanel("Open VRM File", defaultDirectory, extensions, false);

        if (paths.Length > 0)
        {
            string path = paths[0];
            OpenFile(path);
        }
    }

    public async Task<GameObject> OpenFile(string path)
    {
        
        _instance = await VrmUtility.LoadAsync(defaultDirectory, new RuntimeOnlyAwaitCaller());
        // this.instance.EnableUpdateWhenOffscreen();
        _instance.ShowMeshes();

        Texture2D thumbnail = null;
        if (thumbnail != null)
        {
            displayImage.texture = thumbnail;
            AdjustRawImageSize(displayImage, thumbnail);
        }

        // Optionally, load the next scene
        SceneManager.LoadScene("VRMCVVTuberExample");

        return _instance.gameObject;
    }

    private void AdjustRawImageSize(RawImage rawImage, Texture2D texture)
    {
        RectTransform rt = rawImage.GetComponent<RectTransform>();
        float aspectRatio = (float)texture.width / texture.height;

        // Assume a maximum width and height for the RawImage
        float maxWidth = 600f;
        float maxHeight = 400f;

        if (aspectRatio > 1) // Wider than tall
        {
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxWidth / aspectRatio);
        }
        else // Taller than wide
        {
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxHeight * aspectRatio);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxHeight);
        }
    }
}