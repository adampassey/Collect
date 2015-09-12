using UnityEngine;
using System.Collections;

namespace Collect.Exceptions {

    public class MissingPrefabException : UnityException {

        public MissingPrefabException() {
        }

        public MissingPrefabException(string text) : base(text) {
        }
    }
}
