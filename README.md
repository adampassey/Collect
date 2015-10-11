# Collect
A drag-and-drop grid-based inventory system for [Unity 4.6+](https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/the-new-ui). View a [working example in your web browser](http://adampassey.github.io/collect/Build/Build.html), _Chrome not supported_. **[Collect](https://github.com/adampassey/collect)** makes it easy to a build grid-based inventory and equipment system in the Unity Editor, complete with:

* Tooltips
* Stackable items
* Slots with specific item types
* Sound effects
* Events

![alt tag](http://i.imgur.com/CN5GZf1.png)

## Installation
Download [collect.unitypackage](https://github.com/adampassey/collect/raw/master/collect.unitypackage) and import it with Unity by going to `Assets > Import Package > Custom Package`. Choose [collect.unitypackage](https://github.com/adampassey/collect/raw/master/collect.unitypackage) and import all assets.

## Setup

[Collect](https://github.com/adampassey/collect) assumes you are [familiar with the tools used to create UI's in Unity](https://unity3d.com/learn/tutorials/topics/user-interface-ui).

### Containers

In order to create an inventory system with [Collect](https://github.com/adampassey/collect), you'll first need to create containers. This is primarily done through the Unity editor. Create a new container by creating a [Game Object](http://docs.unity3d.com/ScriptReference/GameObject.html) and attaching a `Container` component to it. Do this by selecting the object in the editor and clicking `Add Component`, then attach the `Collect/Containers/Standard Container` component to your object.

#### Slots

Create `Slot`'s in a container by creating a new [Game Object](http://docs.unity3d.com/ScriptReference/GameObject.html) within your `Container` object and attaching a `Collect/Slots/slot` component. This will allow `Draggable` objects to be dropped onto this slot.

##### Grid-based alignment

Generally `Slot`'s within a container are aligned as a grid. In order to align `Slot`'s within a `Container` in a grid automatically, attach a [Grid Layout Group](http://docs.unity3d.com/Manual/script-GridLayoutGroup.html) to your `Container`. 

##### Slots with specific type

If a `Slot` will only allow `Draggable` items of a certain type to be dropped into it, attach a `Collect/Slots/Slot With Type` component to the `Slot` instead of a `Collect/Slots/Slot` component. 

_The `Slot With Type` component is great for creating equipment systems._

See the [Sample Scene](https://github.com/adampassey/collect/blob/master/Assets/Scenes/Collect/sample-scene.unity) included in this project for more details on how to properly create a grid-based inventory with built-in Unity components and [Collect](https://github.com/adampassey/collect). 

### Items

To create a `Draggable` item, create a new [Game Object](http://docs.unity3d.com/ScriptReference/GameObject.html) and attach a `Collect/Items/Draggable Item` component to it. 

#### Stackable Items

[Collect](https://github.com/adampassey/collect) supports `Stackable` items as well. Simply attach a `Collect/Items/Stackable Items` component to an object with a `Draggable` component attached.

_NOTE: The `Stackable` component needs priority over the `Draggable` component to work appropriately. This can be controlled adjusting the [Unity Script Execution Order Settings](http://docs.unity3d.com/Manual/class-ScriptExecution.html), or the `Stackable` component can be placed *above* the `Draggable` component in the `Inspector`._

#### Sounds

In order to play sounds when an item is picked up or dropped, simply attach a `/Collect/Items/Draggable Item Sound` component to your object. Then drag an [Audio Clip](http://docs.unity3d.com/Manual/class-AudioClip.html) into the `Pick Up Sound` and `Put Down Sound` fields.

## Events

[Collect](https://github.com/adampassey/collect) exposes several events for `Draggable` items and `Container`'s. 

### Containers

`Container`'s expose events when a [Game Object](http://docs.unity3d.com/ScriptReference/GameObject.html) is added or removed from it. See [Container Delegate](https://github.com/adampassey/collect/blob/master/Assets/Scripts/Collect/Containers/ContainerDelegate.cs) for required methods. Other components can subscribe to these events easily:

```c#
//  This custom component will listen to the events of the
//  `Container` component attached to this object

public class ContainerListener : MonoBehaviour {

  //  the `Container` component
  private Container container;

  public void Start() {
    container = GetComponent<Container>();
    
    //  subscribe to both item added and 
    //  removed events
    container.ItemWasAdded += ItemWasAdded;
    container.ItemWasRemvoed += ItemWasRemoved;
  }
  
  public void OnDestroy() {
    container.ItemWasAdded -= ItemWasAdded;
    container.ItemWasRemoved -= ItemWasRemoved;
  }
  
  public void ItemWasAdded(GameObject item) {
    Debug.Log("An item was added to this container: " + item.name);
  }
  
  public void ItemWasRemoved(GameObject item) {
    Debug.Log("An item was removed from this container: " + item.name);
  }
}

```

See the [Sample](https://github.com/adampassey/collect/tree/master/Assets/Scripts/Collect/Sample) directory for more samples of subscribing to these events.

### Items

`Draggable` also exposes several events that can be subscribed to.

```c#
//  This custom component will listen to the events of the
//  `Draggable` component attached to this object

public class DraggableListener : MonoBehaviour {

  //  the `Draggable` component
  private Draggable draggable;

  public void Start() {
    draggable = GetComponent<Draggable>();
    
    //  subscribe to drag begin and end events
    draggable.OnDraggableBeginDrag += DraggableBeginDrag;
    draggable.OnDraggableEndDrag += DraggableEndDrag;
  }
  
  public void OnDestroy() {
    draggable.OnDraggableBeginDrag -= DraggableBeginDrag;
    draggable.OnDraggableEndDrag -= DraggableEndDrag;
  }
  
  public void DraggableBeginDrag(PointerEventData eventData) {
    Debug.Log("Beginning drag");
  }
  
  public void DraggableEndDrag(PointerEventData eventData) {
    Debug.Log("Ending drag");
  }
}
```

Sometimes, `Draggable` items are dropped in _invalid_ locations. When this happens, [Collect](https://github.com/adampassey/collect) places the object back in it's previous `Slot` and fires an event. It is up to the application to determine what to do with this object. Here is an example of subscribing to this event:

```c#
public class EventSubscriber : MonoBehaviour {

  public void Start() {
    ItemDropEventManager.OnItemDidDrop += ItemDidDrop;
  }
  
  public void OnDestroy() {
    ItemDropEventManager.OnItemDidDrop -= ItemDidDrop;
  }
  
  public void ItemDidDrop(GameObject item, Slot slot, PointerEventData data) {
    //  handle item drop or delete
  }
}
```
