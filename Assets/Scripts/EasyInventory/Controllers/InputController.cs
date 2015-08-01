using UnityEngine;
using System.Collections;

using EasyInventory.Static.Inputs;
using EasyInventory.Containers;

namespace EasyInventory.Controllers {

    [AddComponentMenu("Easy Inventory/Controllers/Input Controller")]
    public class InputController : MonoBehaviour {

        [Tooltip("The Container to to send inputs to")]
        public GameObject Container;

        private Container container;

        // Use this for initialization
        void Start() {
            if (Container == null) {
                throw new UnityException("No container assigned to this controller.");
            }

            container = Container.GetComponent<Container>();
            if (container == null) {
                throw new UnityException("No container component attached to this Container object.");
            }
        }

        // Update is called once per frame
        void Update() {

           if (Input.GetButtonDown(InputName.Inventory.TOGGLE)) {
                container.Toggle();
            }

        }
    }
}
