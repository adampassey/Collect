using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
                Stack.Remove(int.Parse(inputField.text));
            }
        }
    }
}
