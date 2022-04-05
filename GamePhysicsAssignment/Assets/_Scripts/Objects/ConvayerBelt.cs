using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects {
    public class ConvayerBelt : MonoBehaviour {

        private Animator _animator;
        private SurfaceEffector2D _sf;
        
        private const string ConvayerBeltSlow = "convayorBelt_slow";
        private const string ConvayerBeltFast = "convayorBelt_fast";
        private const string ConvayerBeltSuperFast = "convayorBelt_superFast";

        private float _sfSlowSpeed = 5f;
        private float _sfFastSpeed = 8f;
        private float _sfSuperFastSpeed = 12f;
        
        [SerializeField] private bool isSlow = false;
        [SerializeField] private bool isFast = false;
        [SerializeField] private bool isSuperFast = false;

        private void Start() {
            _animator = GetComponent<Animator>();
            _sf = GetComponent<SurfaceEffector2D>();
        }

        private void Update() {
            if (isSlow) {
                _sf.speed = _sfSlowSpeed;
                _animator.Play(ConvayerBeltSlow);
            }
            else if (isFast) {
                _sf.speed = _sfFastSpeed;
                _animator.Play(ConvayerBeltFast);
            }
            else if (isSuperFast) {
                _sf.speed = _sfSuperFastSpeed;
                _animator.Play(ConvayerBeltSuperFast);
            }
        }
    }
}