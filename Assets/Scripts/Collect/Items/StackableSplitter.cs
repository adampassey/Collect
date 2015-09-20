using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Collect.Slots;

namespace Collect.Items {

    [AddComponentMenu("Collect/Items/Stackable Item Splitter")]
    public class StackableSplitter : MonoBehaviour {

        public Stackable Stack;

        private InputField inputField;

        public void Start() {
            inputField = GetComponentInChildren<InputField>();

            if (inputField == null) {
                throw new MissingComponentException();
            }

            inputField.ActivateInputField();
        }

        //  TODO: Can any of this be done event-driven?
        public void Update() {
            if (!inputField.isFocused) {
                Destroy(gameObject);
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                Destroy(gameObject);
            }

            if (Input.GetKeyDown(KeyCode.Return) || 
                Input.GetKeyDown(KeyCode.KeypadEnter)) {
                
                //  TODO: Handle `0` case
                Stackable newStack = Stack.Remove(int.Parse(inputField.text));
                Slot parentSlot = Stack.GetParentSlot();

                Draggable newDraggableStack = newStack.GetComponent<Draggable>();
                newDraggableStack.OnBeginDrag(null);

                //  put the stack back in the slot
                //  as it will be picked up (which we don't want)
                if (Stack.Size() < newStack.Size()) {
                    parentSlot.Item = Stack.GetComponent<Draggable>();
                }
            }
        }
    }
}
