using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using EasyInventory.Handler;

namespace EasyInventory.Slots {

    [AddComponentMenu("Easy Inventory/Slots/Slot")]
    public class Slot : MonoBehaviour, IDropHandler {

        public DragHandler item;

        /**
         *  This event is fired when an item is dropped
         *  onto this slot. Will accept the item
         *  into the slot OR swap the item.
         *
         **/
        public void OnDrop(PointerEventData eventData) {
            if (item == null) {
                item = DragHandler.draggedItem;
                item.transform.SetParent(transform);
                item.Slot = this;
            } else {
                throw new UnityException("Error: Easy Inventory does not support item swapping. Yet.");
            }
        }
    }
}
