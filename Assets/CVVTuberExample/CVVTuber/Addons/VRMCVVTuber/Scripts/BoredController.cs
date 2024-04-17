using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AddonScripts
{
    public class BoredController : MonoBehaviour
    {

        public SkinnedMeshRenderer MouthSkin;
        public SkinnedMeshRenderer EyeSkin;
        public float MinEyeParam = 20;
        public float EyeLeapSmallSpeed = 0.1f;
        public float EyeLeapBigSpeed = 0.5f;
        public float EyeLeapBoredSmallSpeed = 0.1f;
        public float EyeLeapBoredBigSpeed = 0.5f;
        public float BoredStartTime = 5.0f;


        private float eyeParam;
        private float eyeParamBored;
        private float leapSum;
        private float leapSumBored;
        private float notTalkCount;
        private bool boredEyeFlag = false;

        void Update()
        {
            if (CheckMouthBlendShap())
            {
                notTalkCount = 0f;
                if (leapSum > 0f)
                {
                    //眼球を大きくしている
                    leapSum -= EyeLeapBigSpeed * Time.deltaTime;
                    eyeParam = Mathf.Lerp(0, MinEyeParam, leapSum);
                    EyeSkin.SetBlendShapeWeight(1, eyeParam);
                }
                //暇な目を戻している
                if (leapSumBored > 0f)
                {
                    leapSumBored -= EyeLeapBoredBigSpeed * Time.deltaTime;
                    eyeParamBored = Mathf.Lerp(0, 100, leapSumBored);
                    MouthSkin.SetBlendShapeWeight(33, eyeParamBored);
                }
            }
            else
            {
                //一定時間何も喋ってないと暇そうな目をしだす
                notTalkCount += Time.deltaTime;
                if (notTalkCount >= 5.0f)
                {
                    boredEyeFlag = true;
                }
                else
                {
                    boredEyeFlag = false;
                }

                //眼球を小さくしている
                if (leapSum < 1.0f)
                {
                    leapSum += EyeLeapSmallSpeed * Time.deltaTime;
                    eyeParam = Mathf.Lerp(0, MinEyeParam, leapSum);
                    EyeSkin.SetBlendShapeWeight(1, eyeParam);
                }
                if (boredEyeFlag)
                {
                    //暇な目にしてる
                    if (leapSumBored <= 1.0f)
                    {
                        leapSumBored += EyeLeapBoredSmallSpeed * Time.deltaTime;
                        eyeParamBored = Mathf.Lerp(0, 100, leapSumBored);
                        MouthSkin.SetBlendShapeWeight(33, eyeParamBored);
                    }
                }
            }
        }
        //母音BlendShapeが変化しているか調べ、喋ってる状態か判断する
        private bool CheckMouthBlendShap()
        {
            for (int i = 0; i < 5; i++)
            {
                if (MouthSkin.GetBlendShapeWeight(i) != 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

