using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnim : MonoBehaviour
{

    public void AnimEnd()
    {
        ToggleMenu.Instance.StartGame();
    }

}
