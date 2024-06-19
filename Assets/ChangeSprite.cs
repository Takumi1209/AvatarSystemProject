using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; //Input Field�p�Ɏg��
using System.Windows.Forms; //OpenFileDialog�p�Ɏg��


public class ChangeSprite : MonoBehaviour
{

    public InputField input_field_path_;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenExistFile()
    {

        OpenFileDialog open_file_dialog = new OpenFileDialog();

        //InputField�̏����l�������Ă���(��������ƃ_�C�A���O�����̏ꏊ����J��)
        open_file_dialog.FileName = input_field_path_.text;

        //csv�t�@�C�����J�����Ƃ��w�肷��
        open_file_dialog.Filter = "vrm�t�@�C��|*.vrm";

        //�t�@�C�������݂��Ȃ��ꍇ�͌x�����o��(true)�A�x�����o���Ȃ�(false)
        open_file_dialog.CheckFileExists = false;

        //�_�C�A���O���J��
        open_file_dialog.ShowDialog();

        //�擾�����t�@�C������InputField�ɑ������
        input_field_path_.text = open_file_dialog.FileName;

    }

}
