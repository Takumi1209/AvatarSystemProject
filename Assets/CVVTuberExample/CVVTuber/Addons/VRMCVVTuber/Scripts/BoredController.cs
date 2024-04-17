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
                    //ä·ãÖÇëÂÇ´Ç≠ÇµÇƒÇ¢ÇÈ
                    leapSum -= EyeLeapBigSpeed * Time.deltaTime;
                    eyeParam = Mathf.Lerp(0, MinEyeParam, leapSum);
                    EyeSkin.SetBlendShapeWeight(1, eyeParam);
                }
                //â…Ç»ñ⁄ÇñﬂÇµÇƒÇ¢ÇÈ
                if (leapSumBored > 0f)
                {
                    leapSumBored -= EyeLeapBoredBigSpeed * Time.deltaTime;
                    eyeParamBored = Mathf.Lerp(0, 100, leapSumBored);
                    MouthSkin.SetBlendShapeWeight(33, eyeParamBored);
                }
            }
            else
            {
                //àÍíËéûä‘âΩÇ‡íùÇ¡ÇƒÇ»Ç¢Ç∆â…ÇªÇ§Ç»ñ⁄ÇÇµÇæÇ∑
                notTalkCount += Time.deltaTime;
                if (notTalkCount >= 5.0f)
                {
                    boredEyeFlag = true;
                }
                else
                {
                    boredEyeFlag = false;
                }

                //ä·ãÖÇè¨Ç≥Ç≠ÇµÇƒÇ¢ÇÈ
                if (leapSum < 1.0f)
                {
                    leapSum += EyeLeapSmallSpeed * Time.deltaTime;
                    eyeParam = Mathf.Lerp(0, MinEyeParam, leapSum);
                    EyeSkin.SetBlendShapeWeight(1, eyeParam);
                }
                if (boredEyeFlag)
                {
                    //â…Ç»ñ⁄Ç…ÇµÇƒÇÈ
                    if (leapSumBored <= 1.0f)
                    {
                        leapSumBored += EyeLeapBoredSmallSpeed * Time.deltaTime;
                        eyeParamBored = Mathf.Lerp(0, 100, leapSumBored);
                        MouthSkin.SetBlendShapeWeight(33, eyeParamBored);
                    }
                }
            }
        }
        //ïÍâπBlendShapeÇ™ïœâªÇµÇƒÇ¢ÇÈÇ©í≤Ç◊ÅAíùÇ¡ÇƒÇÈèÛë‘Ç©îªífÇ∑ÇÈ
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

