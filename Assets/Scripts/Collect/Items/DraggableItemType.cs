using UnityEngine;
using System.Collections;

namespace Collect.Items {

    public enum ItemType {
        Head,
        Face,
        Neck,
        Shoulder,
        Chest,
        Back,
        Hands,
        Belt,
        Legs,
        Feet,
        Ring,
        RightHand,
        LeftHand,
        Relic
    };

    [AddComponentMenu("Collect/Items/Draggable Item Type")]
    public class DraggableItemType : MonoBehaviour {

        [Tooltip("Type of this item")]
        public ItemType ItemType;
        
    }
}
