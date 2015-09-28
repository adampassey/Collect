using UnityEngine;
using System.Collections;
using Collect.Slots;
using Collect.Items;
using Collect.Exceptions;

namespace Collect.Containers {

    [AddComponentMenu("Collect/Containers/Standard Container")]
    public class StandardContainer : MonoBehaviour, Container, SlotDelegate {

        //  the event broadcast when an item is added to this container
        public event ItemAdded ItemWasAdded;

        //  the event broadcast when an item is removed from this container
        public event ItemRemoved ItemWasRemoved;

        //  subscribing to slot events
        public event ItemAddedToSlot ItemWasAddedToSlot;
        public event ItemRemovedFromSlot ItemWasRemovedFromSlot;

        private ArrayList slots;

        public ArrayList Slots {
            get { return slots; }
        }

        void Start() {
            slots = retrieveSlots();
        }

        /**
         *  Open this container window
         **/
        public void Open() {
            gameObject.SetActive(true);
        }

        /**
         *  Close this container window
         **/
        public void Close() {
            gameObject.SetActive(false);
        }

        /**
         *  Toggle the container window
         **/
        public void Toggle() {
            if (gameObject.activeSelf) {
                gameObject.SetActive(false);
            } else {
                gameObject.SetActive(true);
            }
        }

        /**
         *  Add a GameObject to the the first
         *  open slot. Expects GameObject to have
         *  a `Draggable` component attached.
         *
         **/
        public void Add(GameObject item) {
            Draggable dragHandler = item.GetComponent<Draggable>();
            if (dragHandler == null) {
                throw new MissingComponentException("Adding to Container requires DragHandler component");
            }

            Stackable stackHandler = item.GetComponent<Stackable>();
            Slot emptySlot = null, stackableSlot = null;

            foreach(Slot slot in Slots) {

                //  retrieve the first empty slot and retain
                if (emptySlot == null && slot.Item == null) {
                    emptySlot = slot;

                //  if there's an item in this slot
                //  but the item being added is stackable
                //  check if it can stack
                } else if (slot.Item && stackHandler != null) {
                    Stackable itemStack = slot.Item.GetComponent<Stackable>();

                    if (itemStack != null && itemStack.CanStack(stackHandler)) {
                        stackableSlot = slot;
                    }
                }
            }

            //  add the item to the slot
            //  will prioritize stackable slot
            if (stackableSlot != null) {
                stackableSlot.AddItem(dragHandler);
            } else if (emptySlot != null) {
                emptySlot.AddItem(dragHandler);
            }

            throw new NotStackableException("Unable to add item (" + dragHandler.name + ") to container ( " + name + ")");
        }

        /**
         *  Remove an item from this slot. 
         *
         **/
        public GameObject Remove(Slot slot) {
            if (slot.Item == null) {
                return null;
            }

            return slot.RemoveItem().gameObject;
        }

        /**
         *  Remove this item from the container. Will
         *  iterate through Slots until it finds the
         *  item. Will return null if not found.
         *
         *  Expects GameObject to have `Draggable` component
         *  attached.
         *
         **/
        public GameObject Remove(GameObject item) {
            Draggable dragHandler = item.GetComponent<Draggable>();
            if (dragHandler == null) {
                throw new MissingComponentException("Removing from Container requires DragHandler component");
            }

            foreach(Slot slot in Slots) {
                if (slot.Item == dragHandler) {
                    return slot.Item.gameObject;
                }
            }

            return null;
        }

        /**
         *  Called when the item is added to this container
         **/
        public void ItemAddedToSlotDelegate(GameObject item) {
            if (ItemWasAdded != null) {
                ItemWasAdded(item);
            }
        }

        /**
         *  Called when this item is removed from this container
         **/
        public void ItemRemovedFromSlotDelegate(GameObject item) {
            if (ItemWasRemoved != null) {
                ItemWasRemoved(item);
            }
        }

        /**
         *  Retrieve all the slots that are inside this container.
         *
         **/
        private ArrayList retrieveSlots() {
            ArrayList slots = new ArrayList();
            Slot[] childSlots = transform.GetComponentsInChildren<Slot>();
            foreach (Slot slot in childSlots) {
                slot.ItemAddedDelegate = ItemAddedToSlotDelegate;
                slot.ItemRemovedDelegate = ItemRemovedFromSlotDelegate;
                slots.Add(slot);
            }
            return slots;
        }
    }
}
