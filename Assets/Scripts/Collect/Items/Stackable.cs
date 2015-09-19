using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Collect.Exceptions;

namespace Collect.Items {

    [AddComponentMenu("Collect/Items/Stackable Item")]
    [RequireComponent(typeof (Draggable))]
    public class Stackable : MonoBehaviour, IPointerClickHandler {

        [Tooltip("How many items this stack can hold")]
        public int max = 5;

        [Tooltip("Key modifier used to split stack")]
        public KeyCode keyModifier = KeyCode.LeftShift;

        private Draggable draggable;

        // Use this for initialization
        void Start() {
            draggable = GetComponent<Draggable>();
        }

        public void Add(Stackable stackable) {
            Stackable[] stackables = GetComponentsInChildren<Stackable>();
            if (stackables.Length >= max || stackable.Size() + stackables.Length > max) {

                //  TODO: create new stackable and begin
                //  dragging it with the rest in this stack
                throw new NotStackableException();
            }

            stackable.transform.SetParent(transform);
            stackable.gameObject.SetActive(false);
        }

        public Stackable Remove(int count) {

            Stackable[] stackables = GetComponentsInChildren<Stackable>();
            if (count >= stackables.Length) {
                draggable.BeginDrag();
                return this;
            } else {
                Stackable baseStackable = null;
                foreach(Stackable s in stackables) {
                    if (s.transform == transform) {
                        continue;
                    }

                    if (baseStackable == null) {
                        baseStackable = s;
                        continue;
                    }

                    if (baseStackable.Size() >= count) {
                        break;
                    } else {
                        baseStackable.Add(s);
                    }
                }
                return baseStackable;
            }
            return null;
        }

        public Stackable Remove() {
            return Remove(1);
        }

        public int Size() {
            return GetComponentsInChildren<Stackable>().Length;
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (Input.GetKey(keyModifier)) {
                //  display UI w/ text field
                StackableSplitterFactory.Create(this);
                eventData.Use();
            }
        }
    }
}
