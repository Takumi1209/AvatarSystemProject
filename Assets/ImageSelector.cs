using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;
using System.IO;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class ImageSelector : MonoBehaviour
{
    public Button selectImageButton;
    // Start is called before the first frame update
    void Start()
    {
        selectImageButton.onClick.AddListener(OpenFileDialog);
    }

    void OpenFileDialog()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            InitialDirectory = "c:\\",
            Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
            FilterIndex = 1,
            RestoreDirectory = true
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string imagePath = openFileDialog.FileName;
            PlayerPrefs.SetString("SelectedImagePath", imagePath);
            PlayerPrefs.Save();
            SceneManager.LoadScene("VRMCVVTuber");
        }
    }
}
