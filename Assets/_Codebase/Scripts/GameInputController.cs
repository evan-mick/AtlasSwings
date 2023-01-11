using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Responsible for managing input and the current control state
/// 
/// THIS SHOULD PROBABLY BE REFACTORED BECAUSE ITS DOING A LOT OF STUFF
/// </summary>
public class GameInputController : MonoBehaviour
{
    [SerializeField]
    private CameraManager _camManager;
    [SerializeField]
    private LauncherController _launchController;


    [Header("Launcher Mode")]
    [SerializeField]
    private float _rotateSpeed = 180.0f;
    [SerializeField]
    private float _scrollAddAmount = 20.0f; 

    [Header("Map Mode")]
    private float _moveSpeed = 10.0f;
    private float _resizeRate = 5000.0f;

    // Projectile mode
    private DestructiveProjectile _currentProjectile;

    enum GameState
    {
        LAUNCH = 0,
        PROJECTILE = 1,
        MAP = 2, 
    }

    private GameState _curGameState = GameState.LAUNCH;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Finite State Machine for control 

        // LAUNCH STATE
        if (_curGameState.Equals(GameState.LAUNCH))
        {
            // Rotate Launch Controller
            float xMove = Input.GetAxis("Mouse X");
            _launchController.RotateLauncher(xMove * Time.deltaTime * _rotateSpeed);

            // Add Range, TODO, MAKE THE CHARGE METER FOR THIS
            float addRange = Input.GetAxis("Mouse ScrollWheel") * _scrollAddAmount * Time.deltaTime;
            _launchController.AddRange(addRange);


            // Launch ball when ready
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Rigidbody follow = _launchController.LaunchProjectile();
                _camManager.SetProjectileCamera(follow);
                _curGameState = GameState.PROJECTILE;

                _launchController.SetTrajectoryVisible(false);

                follow.TryGetComponent<DestructiveProjectile>(out DestructiveProjectile projectile);

                if (projectile)
                {
                    projectile.E_OnStop.AddListener(OnProjectileStopped);
                    _currentProjectile = projectile; 
                }
                

            }

            // Go to Map State
            if (Input.GetKeyDown(KeyCode.M))
            {
                _camManager.SetMapCamera();
                _curGameState = GameState.MAP;
            }
        }
        // PROJECTILE STATE
        else if (_curGameState.Equals(GameState.PROJECTILE))
        {

            bool destructiveProjectileSlowed = _currentProjectile == null;

            // Go back to launch State, placeholder until we add in bounce stuff
            if (Input.GetKeyDown(KeyCode.Space) && destructiveProjectileSlowed)
            {
                GotoLaunchState();
            }
        }
        // MAP STATE
        else if (_curGameState.Equals(GameState.MAP))
        {

            _camManager.MoveMapCamera(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * _moveSpeed);

            float resize = -Input.GetAxis("Mouse ScrollWheel") * _resizeRate * Time.deltaTime;
            _camManager.ResizeMapCamera(resize);

            // Go back to launch State
            if (Input.GetKeyDown(KeyCode.M))
            {
                GotoLaunchState();
            }
        }



    }

    private void GotoLaunchState()
    {
        _launchController.SetTrajectoryVisible(true);

        // This should probably be pooled
        if (_currentProjectile)
            Destroy(_currentProjectile.gameObject);

        _camManager.SetLaunchCamera();
        _curGameState = GameState.LAUNCH;
    }

    private void OnProjectileStopped()
    {
        _launchController.TeleportToPoint(_currentProjectile.transform.position);
        GotoLaunchState();
    }
}
