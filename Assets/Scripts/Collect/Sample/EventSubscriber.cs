using UnityEngine;
using System.Collections;

using Collect.Events;
using Collect.Slots;
using UnityEngine.EventSystems;

public class EventSubscriber : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //  Listening to the `ItemDidDrop` event
        ItemDropEventManager.OnItemDidDrop += ItemDidDrop;
	}

    public void ItemDidDrop(GameObject item, Slot slot, PointerEventData data) {
        Debug.Log(item.name);
        Debug.Log(slot.Item.name);
        Debug.Log(slot.name);
        Debug.Log(data.position);
    }
}
