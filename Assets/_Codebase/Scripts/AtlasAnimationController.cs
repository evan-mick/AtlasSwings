using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtlasAnimationController : MonoBehaviour
{

    [SerializeField]
    private Animator _animator;
    private string _currentState = "NONE";

    static string HARD_HIT = "HARD";
    static string MEDIUM_HIT = "MEDIUM";
    static string LIGHT_HIT = "LIGHT";
    static string DANCE = "DANCE";
    static string NONE = "NONE";

    private void Start()
    {
        //_animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (_currentState == newState) { return; }
        _animator.Play(newState);
        _currentState = newState;

    }


}
