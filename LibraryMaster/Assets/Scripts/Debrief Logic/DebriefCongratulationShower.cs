using System;
using ATG.LevelControl;
using UnityEngine;
using VFXLogic;
using Zenject;

namespace Debrief_Logic
{
    public class DebriefCongratulationShower : MonoBehaviour, IInitializable
    {
        [SerializeField] private Vector3 _showerPosition;
        [SerializeField] private Vector3 _showerRotation;
        
        [Inject] private IVFXControllable _vfx;
        [Inject] private ILevelStatus _levelStatus;

        private Camera _camera;


        private void Awake()
        {
            _camera = Camera.main;
        }

        [Inject]
        public void Initialize()
        {
            _levelStatus.OnCompleteLevel += OnCompleteLevel;
        }

        private void OnCompleteLevel(object sender, EventArgs e)
        {
#if UNITY_ANDROID || UNITY_IOS
            Taptic.Vibrate();
#endif
            ParticleSystem ps = _vfx.PlayVFXLoop(VFXType.Confetti, Vector3.zero, Vector3.down);
            
            ps.transform.SetParent(_camera.transform);
            ps.transform.localPosition = _showerPosition;
            ps.transform.localRotation = Quaternion.Euler(_showerRotation);
        }
    }
}