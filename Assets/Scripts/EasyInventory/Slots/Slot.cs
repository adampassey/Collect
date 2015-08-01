using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using EasyInventory.Handler;

namespace EasyInventory.Slots {

    [AddComponentMenu("Easy Inventory/Slots/Slot")]
    public class Slot : MonoBehaviour, IDropHandler {

        public void OnDrop(PointerEventData eventData) {
            if (transform.childCount == 0) {
                GameObject item = DragHandler.item;
                item.transform.SetParent(transform);
            }
        }
    }
}
