using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prototyper : MonoBehaviour
{
    public enum Orientation
    {
        XYZ,
        XZY
    }
    private float horizontal;
    private float vertical;
    [SerializeField] private Vector3 movement;
    private Animator animator;
    public float speed = 10;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontal, 0, vertical) * Time.deltaTime * speed;
        if (movement.x != 0 || movement.z != 0)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);
        transform.Translate(movement);
    }


}
