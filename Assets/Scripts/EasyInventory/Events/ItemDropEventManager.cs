using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;
using EasyInventory.Handlers;
using EasyInventory.Slots;

namespace EasyInventory.Events {

    public class ItemDropEventManager {

        public delegate void ItemDidDrop(GameObject item, Slot slot, PointerEventData data);
        public static event ItemDidDrop OnItemDidDrop;

        public static void TriggerItemDidDrop(GameObject item, Slot slot, PointerEventData data) {
            if (OnItemDidDrop != null) {
                OnItemDidDrop(item, slot, data);
            }
        }
    }
}
