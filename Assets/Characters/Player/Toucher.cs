using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toucher : MonoBehaviour
{

    public PlayerMovement PlayerMovement;
    public Player Player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Touchable touchable))
        {
            if (touchable.IsSpeedBoost)
            {
                PlayerMovement.SetSpeedBoost(touchable.SpeedBoost, touchable.Time);
            }
            if (touchable.IsHealthBoost)
            {
                Player.IncreaseHealth(touchable.HealthBoost);
            }
            touchable.Used();
        }
    }


}
