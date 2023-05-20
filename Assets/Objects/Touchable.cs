using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : MonoBehaviour
{

    public bool IsSpeedBoost;
    public float SpeedBoost;
    public float Time;

    public float Cooldown;

    public void Used()
    {
        MainMenu.Instance.StartCoroutine(Reactivate());
        gameObject.SetActive(false);
    }

    private IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(Cooldown);
        gameObject.SetActive(true);
    }

}
