using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Forms;

public class vrmOpenFileDialog : MonoBehaviour
{
    public string ItemLoad()
    {
        OpenFileDialog ofd = new OpenFileDialog();
        //ダイアログが開いたときに開くディレクトリのパス
        ofd.InitialDirectory = "E:/Desktop/VRMAvatars";
        //選択できるファイル形式のフィルタ
        ofd.Filter = "VRM files(*.vrm)|*.vrm|All files(*.*)|*.*";
        //ダイアログ上部に表示するタイトル
        ofd.Title = "VRMファイルを選択してね";
        //ダイアログの表示
        ofd.ShowDialog();
        //選択したファイルのパス
        string filePath = ofd.FileName;
        return filePath;
    }
}