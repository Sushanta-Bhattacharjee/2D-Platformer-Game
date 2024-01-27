using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public BoxCollider2D boxCol;
    public Vector2 boxColInitSize;
    public Vector2 boxColInitOffset;
    public float speed;
    public float jump;
    private Rigidbody2D rb;
    private bool isJumping;

    private void Awake()
    {
        Debug.Log("Player controller awake");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log("Collision: " + collision.gameObject.name);
    // }

    // Start is called before the first frame update
    void Start()
    {
         //Fetching initial collider properties
        boxColInitSize = boxCol.size;
        boxColInitOffset = boxCol.offset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");//run&movement animation
        float vertical = Input.GetAxisRaw("Jump");//jump animation
        float VerticalInput = Input.GetAxisRaw("Vertical");//crouch animation

        MoveCharacter(horizontal , vertical);
        PlayMovementAnimation(horizontal , vertical);
        PlayCrouchAnimation(VerticalInput);
    }

    private void PlayMovementAnimation(float horizontal , float vertical)
    {
        //Running        
        Animator.SetFloat("Speed", Mathf.Abs(horizontal));
        //Flipping the player
        Vector3 scale = transform.localScale;
        if(horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs( scale.x );
        }
        else if(horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        //Crouching
        if(Input.GetKey(KeyCode.LeftControl))
        {
            Crouch(true);
        }
        else
        {
            Crouch(false);
        }

        MoveCharacter(horizontal,vertical);

        //Jump
        if(vertical>0)
        {
            Animator.SetBool("Jump", true);
        }
        else{
            Animator.SetBool("Jump", false);
        }
    }

    private void Crouch(bool crouch)
    {
         if(crouch == true)
        {
            float offX = -0.1122566f;     //Offset X
            float offY = 0.5896489f;      //Offset Y

            float sizeX = 0.9406279f;     //Size X
            float sizeY = 1.337797f;     //Size Y

            boxCol.size = new Vector2(sizeX, sizeY);   //Setting the size of collider
            boxCol.offset = new Vector2(offX, offY);   //Setting the offset of collider
        }
        else
        {
            //Reset collider to initial values
            boxCol.size = boxColInitSize;
            boxCol.offset = boxColInitOffset;
        }
        Animator.SetBool("Crouch", crouch);
    }

    private void PlayCrouchAnimation(float vertical)
    {
        if(vertical > 0)
        {
            Animator.SetTrigger("Crouch");
        }
    }

    private void MoveCharacter(float horizontal , float vertical)
    {
        //moving character horizontally
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;

        //moving character vertically
        if(vertical>0 && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jump), ForceMode2D.Force);
            isJumping = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D jumping)
    {
        if(jumping.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

}
