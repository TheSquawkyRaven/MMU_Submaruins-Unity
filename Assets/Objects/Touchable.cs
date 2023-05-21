using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : MonoBehaviour
{

    public GameObject ModelObject;
    public Collider Collider;
    public AudioSource OnUsedAudio;

    public bool IsSpeedBoost;
    public float SpeedBoost;
    public float Time;

    public float Cooldown;

    public void Used()
    {
        if (OnUsedAudio != null)
        {
            OnUsedAudio.Play();
        }
        StartCoroutine(Reactivate());
        ModelObject.SetActive(false);
        Collider.enabled = false;
    }

    private IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(Cooldown);
        ModelObject.SetActive(true);
        Collider.enabled = true;
    }

}
