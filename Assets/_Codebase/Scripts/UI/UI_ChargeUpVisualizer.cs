using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ChargeUpVisualizer : MonoBehaviour
{
    public ChargeUpController chargeUp;
    public Vector2 barChargeDirection = new Vector2(0, 1);

    public RectTransform indicator;
    public RectTransform bar;

    public bool hideWhenNotCharging = true; 


    // Update is called once per frame
    void Update()
    {

        if (!hideWhenNotCharging || chargeUp.Charging)
        {

            Vector2 indicatorRelativePos = (bar.rect.size * barChargeDirection * chargeUp.CurrentValue) - (bar.rect.size / 2 * barChargeDirection);
            indicator.position = bar.position + new Vector3(indicatorRelativePos.x, indicatorRelativePos.y, indicator.position.z); 
        }
    }
}
