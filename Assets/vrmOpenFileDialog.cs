using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Forms;

public class vrmOpenFileDialog : MonoBehaviour
{
    public string ItemLoad()
    {
        OpenFileDialog ofd = new OpenFileDialog();
        //�_�C�A���O���J�����Ƃ��ɊJ���f�B���N�g���̃p�X
        ofd.InitialDirectory = "E:/Desktop/VRMAvatars";
        //�I���ł���t�@�C���`���̃t�B���^
        ofd.Filter = "VRM files(*.vrm)|*.vrm|All files(*.*)|*.*";
        //�_�C�A���O�㕔�ɕ\������^�C�g��
        ofd.Title = "VRM�t�@�C����I�����Ă�";
        //�_�C�A���O�̕\��
        ofd.ShowDialog();
        //�I�������t�@�C���̃p�X
        string filePath = ofd.FileName;
        return filePath;
    }
}