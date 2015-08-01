using UnityEngine;
using System.Collections;

namespace EasyInventory.Slots {

    [AddComponentMenu("Easy Inventory/Slots/Draggable")]
    public class Draggable : MonoBehaviour {

        public void OnDrag() {
            transform.position = Input.mousePosition;
        }
    }
}
