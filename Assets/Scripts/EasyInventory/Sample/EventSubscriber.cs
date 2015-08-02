using UnityEngine;
using System.Collections;

using EasyInventory.Events;
using EasyInventory.Slots;
using UnityEngine.EventSystems;

public class EventSubscriber : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //  Listening to the `ItemDidDrop` event
        ItemDropEventManager.OnItemDidDrop += ItemDidDrop;
	}

    public void ItemDidDrop(GameObject item, Slot slot, PointerEventData data) {
        Debug.Log(item.name);
        Debug.Log(slot.item.name);
        Debug.Log(slot.name);
        Debug.Log(data.position);
    }
}
