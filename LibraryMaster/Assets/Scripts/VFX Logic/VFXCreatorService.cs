using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VFXLogic;
using Zenject;

namespace VFXLogic
{
    public class VFXCreatorService : MonoBehaviour, IVFXControllable
    {
        [Inject] private VFXData _prefabData;

        private Dictionary<VFXType, List<ParticleSystem>> _activeData = new Dictionary<VFXType, List<ParticleSystem>>();


        public void PlayVFX(VFXType type, Vector3 position, Vector3 lookTO)
        {
            if (!_activeData.ContainsKey(type))
            {
                ParticleSystem newPs = Instantiate(_prefabData.GetParticlePrefabByType(type));
                List<ParticleSystem> newType = new List<ParticleSystem>(){newPs};

                _activeData.Add(type,newType);
            }
            
            var waitingVFX = _activeData[type].FirstOrDefault(p => !p.isPlaying);
            if (waitingVFX == null)
            {
                waitingVFX = Instantiate(_prefabData.GetParticlePrefabByType(type));
                _activeData[type].Add(waitingVFX);
            }

            waitingVFX.transform.position = position;
            waitingVFX.transform.LookAt(lookTO);

            waitingVFX.Play();
        }
        public void PlayVFX(VFXType type, Vector3 position, Transform lookTo)
        {
            if (!_activeData.ContainsKey(type))
            {
                ParticleSystem newPs = Instantiate(_prefabData.GetParticlePrefabByType(type));
                List<ParticleSystem> newType = new List<ParticleSystem>(){newPs};

                _activeData.Add(type,newType);
            }
            
            var waitingVFX = _activeData[type].FirstOrDefault(p => !p.isPlaying);
            if (waitingVFX == null)
            {
                waitingVFX = Instantiate(_prefabData.GetParticlePrefabByType(type));
                _activeData[type].Add(waitingVFX);
            }

            waitingVFX.transform.position = position;
            waitingVFX.transform.LookAt(lookTo);

            waitingVFX.Play();
        }
        public void PlayVFXLoop(VFXType type, Vector3 position, Vector3 lookTo)
        {
            ParticleSystem ps;
            
            if (!_activeData.ContainsKey(type))
            {
                ParticleSystem newPs = Instantiate(_prefabData.GetParticlePrefabByType(type));
                List<ParticleSystem> newType = new List<ParticleSystem>(){newPs};

                _activeData.Add(type,newType);

                ps = newPs;
            }
            else
            {
                ps = _activeData[type][0];
            }
            
            ps.transform.position = position;
            ps.transform.LookAt(lookTo);

            ps.Play();
        }
        public void PlayVFXLoop(VFXType type, Vector3 position, Transform lookTo)
        {
            ParticleSystem ps;
            
            if (!_activeData.ContainsKey(type))
            {
                ParticleSystem newPs = Instantiate(_prefabData.GetParticlePrefabByType(type));
                List<ParticleSystem> newType = new List<ParticleSystem>(){newPs};

                _activeData.Add(type,newType);

                ps = newPs;
            }
            else
            {
                ps = _activeData[type][0];
            }
            
            ps.transform.position = position;
            ps.transform.LookAt(lookTo);

            ps.Play();
        }
    }
}
    
