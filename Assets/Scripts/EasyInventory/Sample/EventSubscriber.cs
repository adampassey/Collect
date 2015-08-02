using UnityEngine;
using System.Collections;

using EasyInventory.Events;
using UnityEngine.EventSystems;

public class EventSubscriber : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ItemDropEventManager.OnItemDidDrop += ItemDidDrop;
	}

    public void ItemDidDrop(GameObject item, PointerEventData data) {
        Debug.Log(item.name);
        Debug.Log(data.position);
    }
}
