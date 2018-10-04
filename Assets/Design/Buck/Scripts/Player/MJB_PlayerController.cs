using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MJB_PlayerMotor))]
public class MJB_PlayerController : MonoBehaviour
{
    Camera cam;

    public LayerMask movementMask, interactableMask;

    MJB_PlayerMotor motor;
    public MJB_Interactable focus;

	// Use this for initialization
	void Start ()
    {
        cam = Camera.main;
        motor = GetComponent<MJB_PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);

                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                MJB_Interactable interactable = hit.collider.GetComponent<MJB_Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(MJB_Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if(focus != null)
                focus.OnDeFocused();
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDeFocused();

        focus = null;
        motor.StopFollowingTarget();
    }
}
