using System.Threading.Tasks;
using UnityEngine;

namespace Main.Scripts.Wizard_s_Scripts.State
{
    public class WizardRunState : BaseWizardState
    {
        private static readonly int Run = Animator.StringToHash("Run");
        private readonly Transform _transform;
        private readonly float _cachedSpeed;
        private readonly float _cachedRotate;

        private float _speed;
        private float _rotate;
        private bool _isRunning;

        public WizardRunState(Animator animation, IWizardStateSwitch wizardStateSwitch, Transform transform,
            float speed, float rotate) : base(animation,
            wizardStateSwitch)
        {
            _transform = transform;
            _speed = speed;
            _rotate = rotate;

            _cachedSpeed = _speed;
            _cachedRotate = _rotate;
        }

        public override void Start()
        {
            Debug.Log("Run State");

            _speed = _cachedSpeed;
            _rotate = _cachedRotate;


            Animation.SetFloat(Run, 0.5f);
        }

        public override void Stop()
        {
            Debug.Log("Run State Stopped");
            Animation.SetFloat(Run, 0);
        }

        public override async void Action()
        {
            if (Input.GetKey(KeyCode.W))
            {
                var direction = Vector3.forward * _speed * Time.deltaTime;
                _transform.Translate(direction);
                _isRunning = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                var direction = -Vector3.forward * _speed * Time.deltaTime;
                _transform.Translate(direction);
                _isRunning = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                var direction = -Vector3.right * _speed * Time.deltaTime;
                _transform.Rotate(0, _rotate * -1, 0);
                _isRunning = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                var direction = Vector3.right * _speed * Time.deltaTime;
                _transform.Rotate(0, _rotate, 0);
                _isRunning = true;
            }

            if (_isRunning)
            {
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
                {
                    WizardStateSwitch.SwitchState<WizardIdleState>();
                    return;
                }

                if (Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.W) ||
                    Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.W))
                {
                    WizardStateSwitch.SwitchState<WizardIdleState>();
                    return;
                }
                
                if (Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.S) ||
                    Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.S))
                {
                    WizardStateSwitch.SwitchState<WizardIdleState>();
                }
            }
        }
    }
}