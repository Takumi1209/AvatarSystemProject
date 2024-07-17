using System;
using System.Collections;
using UnityEngine;
using VRM;
using UniVRM10;
using UniGLTF.Extensions.VRMC_vrm;
using System.IO;
using VRMShaders;



namespace CVVTuber.VRM
{
    public class VRMLoader : MonoBehaviour
    {
        public VRMMeta meta;

      
        [Space(10)]

        public LookTarget faceCamera;

        public VRMFirstPerson firstPerson;

        public VRMBlendShapeProxy blendShape;

        public VRMLookAtHead lookAtHead;

        public Animator animator;

        public virtual bool isDone { get; protected set; }

        public virtual bool isError { get; protected set; }

        void Start()
        {
            
        }

        public virtual void LoadMeta(VRMMeta meta)
        {
            SetupTarget(meta);
        }

        public virtual void Dispose()
        {
            isDone = isError = false;
        }

        protected virtual void SetupTarget(VRMMeta meta)
        {
          
            blendShape = meta.GetComponent<VRMBlendShapeProxy>();
           
            firstPerson = meta.GetComponent<VRMFirstPerson>();
            
            animator = meta.GetComponent<Animator>();

            if (animator != null)
            {
                firstPerson.Setup();

                if (faceCamera != null)
                {
                    faceCamera.Target = animator.GetBoneTransform(HumanBodyBones.Head);
                }
            }

            lookAtHead = meta.GetComponent<VRMLookAtHead>();
        }
    }
}
