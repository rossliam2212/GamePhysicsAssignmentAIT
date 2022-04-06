using UnityEngine;

namespace Objects {
    public class ConvayerBeltLeft : MonoBehaviour {
        
        // Components
        private Animator _animator;
        private SurfaceEffector2D _sf;
        
        // Animation States
        private const string ConvayerBeltLeftSlow = "convayerBelt_left_slow";
        private const string ConvayerBeltLeftFast = "convayerBelt_left_fast";
        private const string ConvayerBeltLeftSuperFast = "convayerBelt_left_superFast";

        // Variables
        private float _sfSlowSpeed = -5f;
        private float _sfFastSpeed = -8f;
        private float _sfSuperFastSpeed = -12f;
        
        [SerializeField] private bool isSlow = false;
        [SerializeField] private bool isFast = false;
        [SerializeField] private bool isSuperFast = false;

        // Initialization
        private void Start() {
            _animator = GetComponent<Animator>();
            _sf = GetComponent<SurfaceEffector2D>();
        }

        private void Update() {
            // Slow Speed
            if (isSlow) {
                _sf.speed = _sfSlowSpeed;
                _animator.Play(ConvayerBeltLeftSlow);
            }
            // Fast Speed
            else if (isFast) {
                _sf.speed = _sfFastSpeed;
                _animator.Play(ConvayerBeltLeftFast);
            }
            // Super Fast Speed
            else if (isSuperFast) {
                _sf.speed = _sfSuperFastSpeed;
                _animator.Play(ConvayerBeltLeftSuperFast);
            }
        }
    }
}