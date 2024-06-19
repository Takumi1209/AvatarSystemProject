using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;

public class ImageApplier : MonoBehaviour
{
    public GameObject targetGameObject;

    void Start()
    {
        string imagePath = PlayerPrefs.GetString("SelectedImagePath");
        if (!string.IsNullOrEmpty(imagePath))
        {
            StartCoroutine(LoadImage(imagePath));
        }
    }

    private IEnumerator LoadImage(string path)
    {
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(fileData);

        Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        targetGameObject.GetComponent<SpriteRenderer>().sprite = newSprite;

        yield return null;
    }
}
