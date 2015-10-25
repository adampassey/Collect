using UnityEngine;
using System.Collections;

namespace Collect.Exceptions {

    public class CannotAddItemException : UnityException {

        public CannotAddItemException() {
        }

        public CannotAddItemException(string text) : base(text) {
        }
    }
}
