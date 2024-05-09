using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VRM;

namespace AddonScripts{
    public class vrmAutoController : VRMLookAtHead
    {
        public VRMBlendShapeProxy blendShapeProxy;

        public float BlinkParam;
        public float MIniBlinkParam = 15f;
        [Range(0, 1)]
        public float eyeLeapT = 0.4f;

        public float timeOut = 1.0f;
        private float timeElapsed;

        public VRMLookAtHead VRMLookAtHead;
        public float BeforeYaw;
        public float BeforePitch;

      
        // Start is called before the first frame update
        void Start()
        {
            // Ensure VRMLookAtHead is not null
            if (VRMLookAtHead == null)
            {
                VRMLookAtHead = GetComponent<VRMLookAtHead>();
            }

        }
       


        // Update is called once per frame
        void Update()
        {

            timeElapsed += Time.deltaTime;
            float Yaw = VRMLookAtHead.Yaw;
            float Pitch = VRMLookAtHead.Pitch;
         

            // Debug.Log("Yaw: " + Math.Abs(Yaw - BeforeYaw) + " Pitch: " + Math.Abs(Pitch - BeforePitch));
            
            if (timeElapsed >= timeOut)
            {
                if (Math.Abs(Yaw - BeforeYaw) < 0.05f && Math.Abs(Pitch - BeforePitch) < 0.05f)
                {
                    blendShapeProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Neutral), 1.0f);
                    float randomin = UnityEngine.Random.Range(0.0f, 1.0f);
                    if(randomin < 0.6f)
                    {
                        //ƒ‰ƒ“ƒ_ƒ€‚É–Ú‚ð•Â‚¶‚é
                        BlinkParam = Mathf.Lerp(0.9f, 1.0f, 0.2f);
                        blendShapeProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), BlinkParam);
                        Debug.Log("Blink");

                    }
                    //yaw‚Æpitch‚ðˆê’è‚Ì”ÍˆÍ“à‚Åƒ‰ƒ“ƒ_ƒ€‚É“®‚©‚·
                    float randomYaw = 10 * UnityEngine.Random.Range(-1.0f, 1.0f);
                    float randomPitch = 10 * UnityEngine.Random.Range(0.8f, 1.2f);
                    Yaw = randomYaw;
                    Pitch = randomPitch;
                    VRMLookAtHead.RaiseYawPitchChanged(randomYaw, randomPitch);
                    

                }
                BeforeYaw = Yaw;
                BeforePitch = Pitch;
               
                timeElapsed = 0.0f;
            }


        }
    }

}
