using FMODUnity;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayRandomClip : MonoBehaviour
    {
        [SerializeField] private string _testClipEventPath;
        [SerializeField] private bool _testPlayOneShot;
        [SerializeField] private bool _testPlayIn3D;
        [SerializeField] private Vector3 _testPlayPosition;
        [SerializeField] private Transform _testPlayTransform;
        [SerializeField] private bool _testPlayAttached;
        [SerializeField] private Transform _testAttachTarget;

        private void Update()
        {
            if(_testClipEventPath == "")
            {
                return;
            }
            if (_testPlayOneShot)
            {
                _testPlayOneShot = false;
                if (_testPlayIn3D)
                {
                    if (_testPlayTransform != null)
                    {
                        RuntimeManager.PlayOneShot(_testClipEventPath, _testPlayTransform.position);
                    }
                    else
                    {
                        RuntimeManager.PlayOneShot(_testClipEventPath, _testPlayPosition);
                    }
                }
                else
                {
                    RuntimeManager.PlayOneShot(_testClipEventPath);
                }
            }
            if (_testPlayAttached && _testAttachTarget != null)
            {
                _testPlayAttached = false;
                RuntimeManager.PlayOneShotAttached(_testClipEventPath, _testAttachTarget.gameObject);
            }
        }
    }
}