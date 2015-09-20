using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Collect.Slots;

namespace Collect.Items {

    [AddComponentMenu("Collect/Items/Stackable Item Splitter")]
    public class StackableSplitter : MonoBehaviour {

        [Tooltip("Stack that items will be removed from")]
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
            //  if they lose focus on the splitter,
            //  destroy it
            if (!inputField.isFocused) {
                Destroy(gameObject);
            }

            //  also destroy it if they hit escape
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Destroy(gameObject);
            }

            //  `Enter` or `Return` has been pressed,
            //  get the value of what was entered and
            //  retrieve that number of items from the
            //  current stack
            if (Input.GetKeyDown(KeyCode.Return) || 
                Input.GetKeyDown(KeyCode.KeypadEnter)) {

                int numberToRemove = int.Parse(inputField.text);
                if (numberToRemove == 0) {
                    return;
                }

                Stackable newStack = Stack.Remove(numberToRemove);
                Slot parentSlot = Stack.GetParentSlot();

                Draggable newDraggableStack = newStack.GetComponent<Draggable>();
                newDraggableStack.OnBeginDrag(null);

                //  if the item that was asked to be split is not
                //  being dragged, put it back in it's parent
                //  slot. Otherwise, it will also start being
                //  dragged
                Draggable thisDraggable = Stack.GetComponent<Draggable>();
                if (!thisDraggable.IsBeingDragged()) {
                    parentSlot.Item = thisDraggable;
                }
            }
        }
    }
}
