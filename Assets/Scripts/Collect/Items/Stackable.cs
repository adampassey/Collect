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

        public void Add(Stackable stackable) {
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

        public Stackable Remove(int count) {
            if (Size() <= count - 1) {
                return this;
            }

            Stackable baseStackable = Get(0);

            List<Stackable> otherStackables = Stack.GetRange(0, count - 1);
            Stack.RemoveRange(0, count - 1);

            foreach (Stackable s in otherStackables) {
                baseStackable.Add(s);
            }

            UpdateCountLabel();
            return baseStackable;
        }

        public Stackable Remove() {
            return Remove(1);
        }

        private Stackable Get(int index) {
            Stackable stack = Stack[index];
            stack.gameObject.SetActive(true);
            Stack.RemoveAt(index);
            return stack;
        }

        public int Size() {
            return Stack.Count;
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (Input.GetKey(keyModifier)) {
                StackableSplitterFactory.Create(this);
                eventData.Use();
            }
        }

        public Slot GetParentSlot() {
            return GetComponentInParent<Slot>();
        }

        private void UpdateCountLabel() {
            if (Size() >= 1) {
                countLabel.text = Size() + 1 + "/" + max;
            } else {
                countLabel.text = "";
            }
        }
    }
}
