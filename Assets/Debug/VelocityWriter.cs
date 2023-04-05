using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VelocityWriter : MonoBehaviour
{

    public TextMeshProUGUI velocityText;
    public Rigidbody observingRB;

    private void Update()
    {
        velocityText.SetText($"Velocity: {observingRB.velocity}");
    }

}
