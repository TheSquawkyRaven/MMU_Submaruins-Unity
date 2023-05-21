using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{

    public Transform RaycastOrigin;
    public Transform RaycastDirection;
    public float Range;

    private Interactable Interactable;

    public TextMeshProUGUI InteractableDisplayText;

    public AudioSource PickupAudio;

    private void Start()
    {
        InteractableDisplayText.gameObject.SetActive(false);
    }

    private void Update()
    {
        Raycast();
        if (Interactable != null && Input.GetKeyDown(KeyCode.E))
        {
            bool pickedUp = Interactable.Interact(PlayerInventory.Instance);
            if (pickedUp)
            {
                PickupAudio.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlayerInventory.Instance.ToggleInventory();
        }
    }

    private void Raycast()
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
        if (Interactable != null)
        {
            Interactable.OnDestroy -= InteractableDestroyed;
        }
        Interactable = interactable;
        if (Interactable == null)
        {
            DisableInteractable();
            return;
        }
        Interactable.OnDestroy += InteractableDestroyed;
        InteractableDisplayText.gameObject.SetActive(true);
        InteractableDisplayText.SetText($"[E] - {Interactable.Name}");
    }

    private void InteractableDestroyed(Interactable interactable)
    {
        DisableInteractable();
    }

    private void DisableInteractable()
    {
        InteractableDisplayText.gameObject.SetActive(false);
    }

}
