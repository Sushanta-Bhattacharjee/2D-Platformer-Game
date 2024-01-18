using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public BoxCollider2D boxCol;
    public Vector2 boxColInitSize;
    public Vector2 boxColInitOffset;

    private void Awake()
    {
        Debug.Log("Player controller awake");
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
        //Running
        float speed = Input.GetAxisRaw("Horizontal");
        Animator.SetFloat("Speed", Mathf.Abs(speed));
        //Flipping the player
        Vector3 scale = transform.localScale;
        if(speed < 0)
        {
            scale.x = -1f * Mathf.Abs( scale.x );
        }
        else if(speed > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        //Crouching
        float VerticalInput = Input.GetAxis("Vertical");
        PlayCrouchAnimation(VerticalInput);
        if(Input.GetKey(KeyCode.LeftControl))
        {
            Crouch(true);
        }
        else
        {
            Crouch(false);
        }
    }

        public void Crouch(bool crouch)
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

    public void PlayCrouchAnimation(float vertical)
    {
        if(vertical > 0)
        {
            Animator.SetTrigger("Crouch");
        }
    }

}
