using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AnimationCurveSO")]
public class AnimationCurveSO : ScriptableObject
{
    [SerializeField]
    private AnimationCurve curve; 

    public AnimationCurve Curve { get { return curve; } }
}
