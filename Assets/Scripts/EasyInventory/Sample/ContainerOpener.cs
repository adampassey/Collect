using UnityEngine;
using System.Collections;
using EasyInventory.Containers;

public class ContainerOpener : MonoBehaviour {

    private ContainerParent containerParent;

	// Use this for initialization
	void Start () {
        containerParent = GetComponent<ContainerParent>();
	}
	
	public void OnMouseDown() {
        containerParent.Container.Toggle();
    }
}
