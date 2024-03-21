using UnityEngine;


public interface Interactable
{
    //Called when the user begins interacting with this object. 
    void OnBeginInteraction(Transform hand) { }

    //Called  when the user stops interacting with this object. 
    void OnEndInteraction() { }

    //Called by HandController when an unattached controller overlaps this object's collider.
    void OnHoverEnter(Transform hand) { }

    //Called by HandController when an unattached controller overlaps this object's collider.
    void OnHoverExit(Transform hand) { }

    //Called by HandController on each frame an unattached controller overlaps this object's collider.
    void OnHoverStay(Transform hand) { }
}