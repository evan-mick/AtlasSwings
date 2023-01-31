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
    [SerializeField]
    private ChargeUpController chargeUp; 

    [Header("Map Mode")]
    [SerializeField]
    private float _moveSpeed = 30.0f;
    [SerializeField]
    private float _resizeRate = 5000.0f;

    [SerializeField] private AudioSource shuffleAudio;
    private bool isShifting;

    [SerializeField] private AudioSource mapAudio;
    [SerializeField] private AudioSource playMusic;
    [SerializeField] private AudioSource winMusic;
    [SerializeField] private AudioSource loseMusic;
    [SerializeField] private AudioSource flightAudio;

    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _loseMenu;


    [SerializeField] private GameObject playerLocation;

    // Projectile mode
    private DestructiveProjectile _currentProjectile;

    enum GameState
    {
        LAUNCH = 0,
        PROJECTILE = 1,
        MAP = 2, 
    }

    private GameState _curGameState = GameState.LAUNCH;

    

    // Update is called once per frame
    void Update()
    {
        if (playMusic != null)
        {
            if (!playMusic.isPlaying && !_loseMenu.activeInHierarchy && !_winMenu.activeInHierarchy) 
            {
                SoundManager.Instance.PlayMusic(playMusic);
            } else if (!loseMusic.isPlaying &&  _loseMenu.activeInHierarchy) {

                SoundManager.Instance.PlayMusic(loseMusic);
            }
            else if (!winMusic.isPlaying && _winMenu.activeInHierarchy)
            {

                SoundManager.Instance.PlayMusic(winMusic);
            }

        }
        
        // Finite State Machine for control 

        // LAUNCH STATE
        if (_curGameState.Equals(GameState.LAUNCH))
        {
            
            // Launch ball when ready
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                chargeUp.StartCharge();
            }

            // Rotate Launch Controller
            float xMove = Input.GetAxis("Horizontal");
            if (xMove == 0)
            {
                isShifting = false;
            }
            else if (!isShifting)
            {
                if (shuffleAudio != null)
                {
                    SoundManager.Instance.PlaySFX(shuffleAudio, 0);
                }
                isShifting = true;
            }

            _launchController.RotateLauncher(xMove * Time.deltaTime * _rotateSpeed);

            // Set Range by charge value
            float setRange = chargeUp.CurrentValue; 
                //Input.GetAxis("Mouse ScrollWheel") * _scrollAddAmount * Time.deltaTime;
            _launchController.SetRangeByPercent(setRange);

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                LaunchBall();
                chargeUp.EndCharge();
            }

            // Go to Map State
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (mapAudio != null)
                {
                    SoundManager.Instance.PlaySFX(mapAudio, 0);
                }
                _camManager.SetMapCamera(_launchController.transform.position);
                _curGameState = GameState.MAP;
                playerLocation.SetActive(true);
            }

            
        }
        // PROJECTILE STATE
        else if (_curGameState.Equals(GameState.PROJECTILE))
        {

            bool destructiveProjectileSlowed = _currentProjectile == null;

            // Go back to launch State, placeholder until we add in bounce stuff
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && destructiveProjectileSlowed)
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
                if (mapAudio != null)
                {
                    SoundManager.Instance.PlaySFX(mapAudio, 0);
                }
                GotoLaunchState();


                playerLocation.SetActive(false);
                Vector3 position = playerLocation.transform.position;
                position.y = 100;
                playerLocation.transform.position = position;

            }
        }



    }

    private void LaunchBall()
    {
        // Launch new projectile, and set proper game state
        Rigidbody follow = _launchController.LaunchProjectile();
        _camManager.SetProjectileCamera(follow);
        _curGameState = GameState.PROJECTILE;

        // Stop visualization
        _launchController.SetTrajectoryVisible(false);

        // Try and set up event for when the projectile stops
        follow.TryGetComponent<DestructiveProjectile>(out DestructiveProjectile projectile);
        if (projectile)
        {
            projectile.E_OnStop.AddListener(OnProjectileStopped);
            _currentProjectile = projectile;
        }
    }

    private void GotoLaunchState()
    {
        _launchController.SetTrajectoryVisible(true);
        _launchController.ShowVisualizer();

        chargeUp.ResetCharge();

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
