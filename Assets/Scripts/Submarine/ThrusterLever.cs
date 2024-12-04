using System;
using UnityEngine;

namespace Submarine
{
    public class ThrusterLever : MonoBehaviour
    {
        [SerializeField] private Transform _max;
        [SerializeField] private Transform _min;
        [SerializeField] private Transform _origin;
        
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public float GetThrust()
        {
            float distMinToOrigin = Vector3.Distance(_min.position, _origin.position);
            float distMinToPosition = Vector3.Distance(_min.position, _transform.position);
            
            float distMaxToOrigin = Vector3.Distance(_max.position, _origin.position);
            float distMaxToPosition = Vector3.Distance(_max.position, _transform.position);

            return distMinToOrigin < distMinToPosition ? Mathf.Lerp(1, 0, distMaxToPosition / distMaxToOrigin) : Mathf.Lerp(-0.25f, 0, distMinToPosition / distMinToOrigin);
        }
    }
}