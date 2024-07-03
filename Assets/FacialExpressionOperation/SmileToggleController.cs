using CVVTuber;
using CVVTuber.VRM;
using UnityEngine;
using UnityEngine.UI;

public class SmileToggleController : MonoBehaviour
{
    public Toggle SmileToggle; // UIのToggleコンポーネントへの参照
    public FaceAnimationController faceAnimationController; // 元のスクリプトへの参照

    void Start()
    {
        // トグルの初期状態を設定
        SmileToggle.isOn = faceAnimationController.enableNoseAndJaw;

        // トグルの値が変更されたときに呼び出されるリスナーを追加
        SmileToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // トグルの値が変更されたときに呼び出されるメソッド
    void OnToggleValueChanged(bool isOn)
    {
        faceAnimationController.enableSmile = isOn;
    }
}
