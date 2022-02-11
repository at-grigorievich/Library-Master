using UnityEngine;

namespace VFXLogic
{
    public interface IVFXControllable
    {
        public void PlayVFX(VFXType type, Vector3 position, Vector3 lookTo);
        public void PlayVFX(VFXType type, Vector3 position, Transform lookTo);
        
        public void PlayVFXLoop(VFXType type, Vector3 position, Vector3 lookTo);
        public void PlayVFXLoop(VFXType type, Vector3 position, Transform lookTo);
    }
}