using UnityEngine;
using System.Collections;
using Collect.Slots;
using Collect.Handlers;
using System;

namespace Collect.Containers {

    [AddComponentMenu("Easy Inventory/Containers/Standard Container")]
    public class StandardContainer : MonoBehaviour, Container {

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
         *  a `DragHandler` component attached.
         *
         **/
        public void Add(GameObject item) {
            Draggable dragHandler = item.GetComponent<Draggable>();
            if (dragHandler == null) {
                throw new MissingComponentException("Adding to Container requires DragHandler component");
            }

            foreach(Slot slot in Slots) {
                if (slot.item == null) {
                    slot.AddItem(dragHandler);
                }
            }
        }

        /**
         *  Remove an item from this slot. 
         *  Not sure when/how this would be used.
         *  TODO: Remove?
         *
         **/
        public GameObject Remove(Slot slot) {
            if (slot.item == null) {
                return null;
            }

            return slot.RemoveItem().gameObject;
        }

        /**
         *  Remove this item from the container. Will
         *  iterate through Slots until it finds the
         *  item. Will return null if not found.
         *
         *  Expects GameObject to have `DragHandler` component
         *  attached.
         *
         **/
        public GameObject Remove(GameObject item) {
            Draggable dragHandler = item.GetComponent<Draggable>();
            if (dragHandler == null) {
                throw new MissingComponentException("Removing from Container requires DragHandler component");
            }

            foreach(Slot slot in Slots) {
                if (slot.item == dragHandler) {
                    return slot.item.gameObject;
                }
            }

            return null;
        }

        /**
         *  Retrieve all the slots that are inside this container.
         *
         **/
        private ArrayList retrieveSlots() {
            ArrayList slots = new ArrayList();
            Slot[] childSlots = transform.GetComponentsInChildren<Slot>();
            foreach (Slot slot in childSlots) {
                slots.Add(slot);
            }
            return slots;
        }
    }
}
