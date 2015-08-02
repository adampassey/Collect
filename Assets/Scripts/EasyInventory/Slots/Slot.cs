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
                AddItem(DragHandler.draggedItem);
            } else {

                //  swap the item with the item
                //  that was dropped
                DragHandler currentItem = RemoveItem();
                DragHandler newItem = DragHandler.draggedItem;
                Slot otherSlot = newItem.Slot;

                newItem.Slot.AddItem(currentItem);
                AddItem(newItem);

                //  this is set on `OnDragEnd` in `DragHandler`
                //  but we have to set it manually here because
                //  the `OnEndDrag` will compare against the
                //  original slot
                currentItem.Slot = otherSlot;
            }
        }

        /**
         *  Remove the `DragHandler` from this slot.
         *  Will return null if no item is present.
         *
         **/
        public DragHandler RemoveItem() {
            if (item == null) {
                return null;
            }

            DragHandler oldItem = item;
            item = null;
            return oldItem;
        }

        /**
         *  Add the `DragHandler` to this slot.
         *
         **/
         public void AddItem(DragHandler item) {
            this.item = item;
            item.transform.SetParent(transform);
        }
    }
}
