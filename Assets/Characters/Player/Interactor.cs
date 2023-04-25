using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{

    public Transform RaycastOrigin;
    public Transform RaycastDirection;
    public float Range;

    public Interactable Interactable;

    public TextMeshProUGUI InteractableDisplayText;

    private void Start()
    {
        InteractableDisplayText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Physics.Raycast(RaycastOrigin.position, RaycastDirection.position - RaycastOrigin.position, out RaycastHit hit, Range, 1 << 7))
        {
            if (hit.collider.TryGetComponent(out Interactable interactable))
            {
                InteractableChanged(interactable);
                return;
            }
        }
        InteractableChanged(null);
    }

    private void InteractableChanged(Interactable interactable)
    {
        if (Interactable == interactable)
        {
            return;
        }
        Interactable = interactable;
        if (interactable == null)
        {
            InteractableDisplayText.gameObject.SetActive(false);
            return;
        }
        InteractableDisplayText.gameObject.SetActive(true);
        InteractableDisplayText.SetText($"Press 'E' to interact with [{interactable.Name}]");
    }

}
