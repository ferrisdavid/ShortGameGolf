using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubManager : MonoBehaviour
{
    [SerializeField]
    private ClubGrab putter;

    [SerializeField]
    private ClubGrab chipper;

    [SerializeField]
    private Transform putterBagSnapPoint;
    [SerializeField]
    private Transform chipperBagSnapPoint;

    [SerializeField]
    private GameObject highlightable;


    private MeshRenderer highlightableMesh;

    // Start is called before the first frame update
    void Start()
    {
        highlightableMesh = highlightable.GetComponent<MeshRenderer>();
        highlightableMesh.materials[1].SetColor("g_vOutlineColor", Color.clear);
    }

    private void OnTriggerEnter(Collider other) {
        // Check for Club Layer
        if (other.gameObject.layer == 6) {
            highlightableMesh.materials[1].SetColor("g_vOutlineColor", new Color( 0.85f, 0.8f, 0.45f, 1 ));
        }

        // Check for interactable layer return to bag immediately
        if (other.gameObject.layer == 9) {
            Transform releasedSnapPoint = other.gameObject.name == "Putter" ? putterBagSnapPoint : chipperBagSnapPoint;
            other.transform.position = releasedSnapPoint.position;
            other.transform.localRotation = Quaternion.LookRotation(Vector3.forward, -Vector3.up);
            other.GetComponent<ClubGrab>().inBag = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == 6) {
            highlightableMesh.materials[1].SetColor("g_vOutlineColor", Color.clear);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!putter.GetAttachedHand() && !chipper.GetAttachedHand()) {
            highlightableMesh.materials[1].SetColor("g_vOutlineColor", Color.clear);
        }

        // Manage the Chipper and Putter Position when not held. (Return the Clubs to the bag after release)
        if (!putter.inBag && putter.GetAttachedHand() == null) {
            StartCoroutine(ReturnToBag(putterBagSnapPoint, putter));
        }
        else if (putter.inBag) {
            putter.transform.position = putterBagSnapPoint.position;
        }

        if (!chipper.inBag && chipper.GetAttachedHand() == null) {
            StartCoroutine(ReturnToBag(chipperBagSnapPoint, chipper));
        }
        else if (chipper.inBag) {
            chipper.transform.position = chipperBagSnapPoint.position;
        }
    }

    
    IEnumerator ReturnToBag(Transform releasedSnapPoint, ClubGrab club) {
        yield return new WaitForSeconds(1.1f);
        club.transform.position = releasedSnapPoint.position;
        club.transform.localRotation = Quaternion.LookRotation(Vector3.forward, -Vector3.up);
        club.GetComponent<ClubGrab>().inBag = true;
    }
}
