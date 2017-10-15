using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody rb;
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpForce;
    public float turnSmoothing = 15f;
    public Animator anim;

    public float knockBackForce;
    public float knockBackTime;

    private float knockBackCounter;
    private Vector3 movement;
    private float distanceToGround;
    private Collider col;
    private bool isDead;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        distanceToGround = col.bounds.extents.y;
        isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDead)
        {
            if (knockBackCounter <= 0)
            {
                float lh = Input.GetAxis("Horizontal");
                float lv = Input.GetAxis("Vertical");

                movement = Camera.main.transform.TransformDirection(new Vector3(lh, 0f, lv));

                movement.y = 0f;

                movement = movement.normalized * moveSpeed * Time.deltaTime;

                rb.MovePosition(transform.position + movement);

                bool isGrounded = IsGrounded();

                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }

                if (lh != 0f || lv != 0f)
                {
                    Rotate(lh, lv);
                }

                anim.SetBool("IsGrounded", isGrounded);
                anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"))));
            }
            else
            {
                knockBackCounter -= Time.deltaTime;
            }
        }
	}

    void Rotate(float lh, float lv) {

        Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);

        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);

        rb.MoveRotation(newRotation);
    }

    bool IsGrounded() {   
      return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 1f);
    }

    public void KnockBack(Vector3 direction) {
        knockBackCounter = knockBackTime;

        Vector3 knockBackDirection = direction * knockBackForce;

        rb.AddForce(knockBackDirection, ForceMode.Impulse);
    }

    public void Die() {
        isDead = true;
        anim.SetBool("IsDead", isDead);
    }
}
