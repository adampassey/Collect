using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;
using EasyInventory.Handler;

namespace EasyInventory.Events {

    public class ItemDropEventManager {

        public delegate void ItemDidDrop(GameObject item, PointerEventData data);
        public static event ItemDidDrop OnItemDidDrop;

        public static void TriggerItemDidDrop(GameObject item, PointerEventData data) {
            if (OnItemDidDrop != null) {
                OnItemDidDrop(item, data);
            }
        }
    }
}
