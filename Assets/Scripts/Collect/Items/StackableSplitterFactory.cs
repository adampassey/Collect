using UnityEngine;
using Collect.Utils;

namespace Collect.Items {

    public class StackableSplitterFactory {

        private static string stackableSplitterPath = "Prefabs/Collect/Stack Splitter";

        public static void Create(Stackable stack) {

            GameObject stackableSplitterResource = Resources.Load(stackableSplitterPath) as GameObject;

            GameObject stackableSplitterObject = GameObject.Instantiate(stackableSplitterResource, stack.transform.position, Quaternion.identity) as GameObject;

            stackableSplitterObject.transform.SetParent(CanvasHelper.GetCanvas().transform);

            StackableSplitter stackableSplitter = stackableSplitterObject.GetComponent<StackableSplitter>();

            stackableSplitter.Stack = stack;
        }
    }
}
