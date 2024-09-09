using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 4f;

    private CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        characterController.SimpleMove(transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"))) * speed);
    }
}