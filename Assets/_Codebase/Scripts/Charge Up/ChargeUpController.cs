using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeUpController : MonoBehaviour
{

    // X-axis is charge time, y-axis is outputted value, should be 0-1; 
    [SerializeField]
    private AnimationCurveSO _chargeCurve;


    // Current value
    private float _currentValue;
    public float CurrentValue { get { return _currentValue; } }

    // Time of the current charge
    [SerializeField]
    private float _maxTime = 2.0f;

    private float _timeCharged = 0.0f; 
    public float TimeCharged { get { return _timeCharged; } }


    // Whether or not it is charging
    private bool _charging = false; 
    public bool Charging { get { return _charging; } }

    // should the charge up stay at the max value, or reverse once its reached the max
    public bool reverseWhenAtMaxValue = true;
    private float _valueIncreaseMultiplier = 1.0f;
    public bool reverse = false;

    [SerializeField] private AudioSource chargeAudio;
    [SerializeField] private AudioSource contactAudio;


    public void ResetCharge()
    {
        _charging = false;
        _timeCharged = 0.0f;
        _currentValue = 0.0f; 
    }

    /// <summary>
    /// Begins the charge up
    /// </summary>
    public void StartCharge()
    {
        _charging = true;

        if (reverse)
        {
            _timeCharged = _maxTime;
            _valueIncreaseMultiplier = -Mathf.Abs(_valueIncreaseMultiplier);
        }
        else
        {
            _timeCharged = 0.0f;
            _valueIncreaseMultiplier = Mathf.Abs(_valueIncreaseMultiplier);
        }
        _currentValue = 0.0f; 
    }

    /// <summary>
    /// Ends the charge up 
    /// </summary>
    /// <returns>The charge value when it ended</returns>
    public float EndCharge()
    {
        if (chargeAudio != null)
        {
            if (chargeAudio.isPlaying)
            {
                chargeAudio.Pause();
            }
        }
        if (contactAudio != null)
        {
            SoundManager.Instance.PlaySFX(contactAudio, 1.3f);
        }
        _charging = false;
        return _currentValue;
    }

    // Update is called once per frame
    void Update()
    {

        if (Charging)
        {
            _timeCharged += Time.deltaTime * _valueIncreaseMultiplier;

            if (chargeAudio != null)
            {
                if (!chargeAudio.isPlaying)
                {
                    SoundManager.Instance.PlaySFX(chargeAudio, 0);
                }
            }
           

            CheckForReverse();
        }

        if (_chargeCurve != null)
        {
            _currentValue = _chargeCurve.Curve.Evaluate(_timeCharged / _maxTime);
        } 
        else
        {
            _currentValue = _timeCharged / _maxTime;
        }
    }

    private void CheckForReverse()
    {
        // Go back and forth
        if (reverseWhenAtMaxValue)
        {
            if (_timeCharged > _maxTime)
            {
                _valueIncreaseMultiplier = -1.0f;
            }
            else if (_timeCharged < 0.0f)
            {
                _valueIncreaseMultiplier = 1.0f;
            }
        } 
        // Stop at max value
        else
        {
            if (_timeCharged > _maxTime || _timeCharged < 0.0f)
            {
                _timeCharged = Mathf.Clamp(_timeCharged, 0, _maxTime);
                _valueIncreaseMultiplier = 0.0f; 
            }
        }
    }
 
}
