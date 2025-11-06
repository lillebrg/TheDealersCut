using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //message to display when player looks at the object
    public string promtMessage;

    public void BaseInteract() {
        Interact();
    }
    protected virtual void Interact()
    {
        //this is basically an interface, so we use this method in the child classes
    }
}
