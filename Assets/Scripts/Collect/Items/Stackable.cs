using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Collect.Exceptions;
using Collect.Slots;

namespace Collect.Items {

    [AddComponentMenu("Collect/Items/Stackable Item")]
    [RequireComponent(typeof (Draggable))]
    public class Stackable : MonoBehaviour, IPointerClickHandler {

        [Tooltip("How many items this stack can hold")]
        public int max = 5;

        [Tooltip("Key modifier used to split stack")]
        public KeyCode keyModifier = KeyCode.LeftShift;

        [Tooltip("Stack of items")]
        public List<Stackable> Stack = new List<Stackable>();

        private Draggable draggable;
        private Text countLabel;
        private const string countLabelName = "Count Label";

        // Use this for initialization
        void Start() {
            draggable = GetComponent<Draggable>();

            countLabel = GetComponentInChildren<Text>();

            if (countLabel == null) {
                throw new MissingComponentException("Cannot display stack count without child `Text` component on item");
            }

            UpdateCountLabel();
        }

        /**
         *  Add an item to this stack. If a `Stackable` item
         *  is added that has several children, will iterate
         *  through the children first and add them. Then,
         *  it will add the final object. 
         *
         *  If the size of both stacks is larger than the max
         *  allowed, will throw a `NotStackableException`
         *
         *  @param Stackable stackable - The item to be stacked
         **/
        public void Add(Stackable stackable) {
            if (stackable.GetType() != GetType()) {
                throw new NotStackableException("Unable to stack, these items are not of the same type");
            }

            if (Size() >= max - 1 || Size() + stackable.Size() >= max - 1) {
                throw new NotStackableException("Unable to stack: " + this + " with " + stackable);
            }

            foreach(Stackable s in stackable.Stack) {
                //  TODO: handle where you can go over max?
                Add(s);
            }

            stackable.transform.SetParent(transform);
            stackable.gameObject.SetActive(false);
            stackable.Stack.Clear();
            Stack.Add(stackable);
            UpdateCountLabel();
        }

        /**
         *  Remove the specified number of items
         *  from this stack. Will return `this` if
         *  count requested is larger (or equal to)
         *  current size.
         *
         *  If not, will pop off the top of the stack
         *  and create a new `Stackable` that will then
         *  be returned
         **/
        public Stackable Remove(int requestedCount) {
            if (requestedCount - 1 >= Size()) {
                return this;
            }

            Stackable baseStackable = Get(0);

            if (requestedCount > 1) {
                List<Stackable> otherStackables = Stack.GetRange(0, requestedCount - 1);
                Stack.RemoveRange(0, requestedCount - 1);

                foreach (Stackable s in otherStackables) {
                    baseStackable.Add(s);
                }
            }

            UpdateCountLabel();
            baseStackable.UpdateCountLabel();
            return baseStackable;
        }

        /**
         *  Convenience method to remove a 
         *  single item from the stack
         **/
        public Stackable Remove() {
            return Remove(1);
        }

        /**
         *  Remove an item from the stack and
         *  prepare it for the scene- this will
         *  activate the object and clear it from
         *  the stack.
         **/
        private Stackable Get(int index) {
            Stackable stack = Stack[index];
            stack.gameObject.SetActive(true);
            Stack.RemoveAt(index);
            return stack;
        }

        /**
         *  The current size of this stack
         **/
        public int Size() {
            return Stack.Count;
        }

        /**
         *  When this stack is clicked, check if the
         *  user is holding down the key modifier. If
         *  so, display the `StackableSplitter` which
         *  allows the user to enter a number to 
         *  remove from this stack
         **/
        public void OnPointerClick(PointerEventData eventData) {
            if (Input.GetKey(keyModifier)) {
                StackableSplitterFactory.Create(this);
                eventData.Use();
            }
        }

        /**
         *  Get the parent slot of this stack. Useful
         *  when stacking is not possible, and `Draggable`
         *  does not retain this reference
         **/
        public Slot GetParentSlot() {
            return GetComponentInParent<Slot>();
        }

        /**
         *  Update the count label assocaited with this
         *  stack.
         **/
        public void UpdateCountLabel() {
            if (Size() >= 1) {
                countLabel.text = Size() + 1 + "/" + max;
            } else {
                countLabel.text = "";
            }
        }
    }
}
