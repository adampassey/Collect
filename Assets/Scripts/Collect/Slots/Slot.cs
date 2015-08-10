using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using Collect.Items;

namespace Collect.Slots {

    [AddComponentMenu("Collect/Slots/Slot")]
    public class Slot : MonoBehaviour, IDropHandler {

        //  the item in this slot
        public Draggable Item;

        //  delegate called when an item is added to this slot
        public ItemAddedToSlot ItemAddedDelegate;

        //  delegate called when an item is removed from this slot
        public ItemRemovedFromSlot ItemRemovedDelegate;

        public void Start() {
            Item = GetComponentInChildren<Draggable>();
        }

        /**
         *  This event is fired when an item is dropped
         *  onto this slot. Will accept the item
         *  into the slot OR swap the item.
         *
         **/
        public virtual void OnDrop(PointerEventData eventData) {

            if (eventData.used) {
                return;
            }

            if (Item == null) {
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
            if (Item == null) {
                return null;
            }

            Draggable oldItem = Item;
            Item = null;

            if (ItemRemovedDelegate != null) {
                ItemRemovedDelegate(oldItem.gameObject);
            }

            return oldItem;
        }

        /**
         *  Add the `DragHandler` to this slot.
         *
         **/
         public void AddItem(Draggable item) {
            this.Item = item;
            item.transform.SetParent(transform);

            if (ItemAddedDelegate != null) {
                ItemAddedDelegate(item.gameObject);
            }
        }
    }
}
