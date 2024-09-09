using UnityEngine;
using UnityEngine.Events;

public class TriggerBehaviour : MonoBehaviour
{
    public UnityEvent onPlayerEnter;

    public UnityEvent onPlayerExit;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) onPlayerEnter.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) onPlayerExit.Invoke();
    }
}