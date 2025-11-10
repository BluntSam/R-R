using UnityEngine;

public class normalAttack : MonoBehaviour
{
    private bool attacking = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Attack"))
        {
            //Animation
            attacking = true;
            GetComponent<Animator>().SetBool("isAttacking", attacking);
        }
        if(Input.GetButtonUp("Attack"))
        {
            //Animation
            attacking = false;
            GetComponent<Animator>().SetBool("isAttacking", attacking);
        }
    }
}
