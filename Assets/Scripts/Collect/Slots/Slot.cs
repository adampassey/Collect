using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using Collect.Items;

namespace Collect.Slots {

    [AddComponentMenu("Collect/Slots/Slot")]
    public class Slot : MonoBehaviour, IDropHandler {

        public Draggable item;

        //  delegate called when an item is added to this slot
        public delegate void ItemAdded(GameObject item);
        public ItemAdded itemAddedDelegate;

        //  delegate called when an item is removed from this slot
        public delegate void ItemRemoved(GameObject item);
        public ItemRemoved itemRemovedDelegate;

        public void Start() {
            item = GetComponentInChildren<Draggable>();
        }

        /**
         *  This event is fired when an item is dropped
         *  onto this slot. Will accept the item
         *  into the slot OR swap the item.
         *
         **/
        public void OnDrop(PointerEventData eventData) {
            if (item == null) {
                AddItem(Draggable.DraggedItem);
            } else {

                //  swap the item with the item
                //  that was dropped
                Draggable otherItem = Draggable.DraggedItem;
                Slot otherSlot = otherItem.OldSlot;

                otherSlot.AddItem(RemoveItem());
                AddItem(otherItem);
            }
        }

        /**
         *  Remove the `DragHandler` from this slot.
         *  Will return null if no item is present.
         *
         **/
        public Draggable RemoveItem() {
            if (item == null) {
                return null;
            }

            Draggable oldItem = item;
            item = null;

            if (itemRemovedDelegate != null) {
                itemRemovedDelegate(oldItem.gameObject);
            }

            return oldItem;
        }

        /**
         *  Add the `DragHandler` to this slot.
         *
         **/
         public void AddItem(Draggable item) {
            this.item = item;
            item.transform.SetParent(transform);

            if (itemAddedDelegate != null) {
                itemAddedDelegate(item.gameObject);
            }
        }
    }
}
