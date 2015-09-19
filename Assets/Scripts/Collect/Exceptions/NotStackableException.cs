using UnityEngine;
using System.Collections;

namespace Collect.Exceptions {

    public class NotStackableException : UnityException {

        public NotStackableException() {
        }

        public NotStackableException(string text) : base(text) {
        }
    }
}
