using UnityEngine;
using System.Collections;

using UnityEngine.UI;

namespace EasyInventory {

    [AddComponentMenu("Easy Inventory/Slot")]
    public class Slot : MonoBehaviour {

        private Image itemImage;
        private GameObject item;

        /**
         *  A Slot contains a UI/Image that is responsible
         *  for rendering the items image within a container.
         *  If one is not set, will throw UnityException
         *
         **/
        public void Start() {
            itemImage = GetComponentInChildren<Image>();
            if (itemImage == null) {
                throw new UnityException("Cannot retrieve image component inside of slot.");
            }
        }

        /**
         *  Add a GameObject to this slot. Expects the
         *  GameObject to have an attached SpriteRenderer
         *  for use in rendering the item
         *  
         **/
        public void Add(GameObject item) {
            SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) {
                throw new UnityException("Item needs a Sprite Renderer component to be added to this slot.");
            }

            itemImage.sprite = spriteRenderer.sprite;
            item.transform.parent = transform;
            item.SetActive(false);
        }

        /**
         *  Remove the item in this slot.
         *
         **/
        public GameObject Remove() {
            itemImage.gameObject.SetActive(false);

            GameObject oldItem = item;
            oldItem.transform.parent = null;
            oldItem.SetActive(true);
            item = null;

            return oldItem;
        }
    }
}
