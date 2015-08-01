using UnityEngine;
using System.Collections;

namespace EasyInventory.Containers {

    [AddComponentMenu("Easy Inventory/Containers/Standard Container")]
    public class StandardContainer : MonoBehaviour, Container {

        private ArrayList items { get; set; }

        // Use this for initialization
        void Start() {
            items = new ArrayList();
        }

        // Update is called once per frame
        void Update() {

        }

        public void Open() {
            gameObject.SetActive(true);
        }

        public void Close() {
            gameObject.SetActive(false);
        }

        public void Toggle() {
            if (gameObject.activeSelf) {
                gameObject.SetActive(false);
            } else {
                gameObject.SetActive(true);
            }
        }

        public void Add(GameObject obj) {

        }
    }
}
