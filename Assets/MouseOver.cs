using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour
{
    private string name;

    private bool selected;

    private List<UIItemsWithLabels.Label> labels;

    public void Start()
    {

        var l_trigger = GetComponent<EventTrigger>();
        var l_entry1 = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        l_entry1.callback.AddListener(_data => { OnPointerEnter((PointerEventData)_data); });
        l_trigger.triggers.Add(l_entry1);
        var l_entry2 = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        l_entry2.callback.AddListener(_data => { OnPointerExit((PointerEventData)_data); });
        l_trigger.triggers.Add(l_entry2);
    }

    

    public virtual void OnPointerEnter(PointerEventData data)
    {
        StaticManager.UiInventory.OnMouseOver();
    }

    public virtual void OnPointerExit(PointerEventData data)
    {
        StaticManager.UiInventory.OnMouseExit();
    }
    
}
