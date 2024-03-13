using System.Collections.Generic;
using UnityEngine;

public class AreaOfInteraction : MonoBehaviour
{
    private Collider2D interactionCollider;
    private List<Transform> transformsInContact = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        interactionCollider = transform.GetComponent<Collider2D>();
    }

    public List<Transform> GetContacts()
    {
        return transformsInContact;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Transform inContact = collider.transform;
        if (!transformsInContact.Contains(inContact))
        {
            transformsInContact.Add(inContact);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Transform inContact = collider.transform;
        if (transformsInContact.Contains(inContact))
        {
            transformsInContact.Remove(inContact);
        }
    }
}
