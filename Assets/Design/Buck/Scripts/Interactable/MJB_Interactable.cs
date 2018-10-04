using UnityEngine;

public class MJB_Interactable : MonoBehaviour
{
    public float radius = 3f;

    bool isFocus = false;
    bool hasInteracted = false;

    Transform player;
    public Transform interactionTransform;

    public virtual void Interact()
    {
        //This method is meant to be overridden 
    }

    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);

            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        hasInteracted = false;
        isFocus = true;
        player = playerTransform;
    }

    public void OnDeFocused()
    {
        hasInteracted = false;
        isFocus = false;
        player = null;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
