using UnityEngine;

public class normalAttack : MonoBehaviour
{
    private bool attacking = false;
    public bool violence = false;
    public enemyHit enemyHit;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Attack") && enemyHit.canHit)
        {
            //Animation
            attacking = true;
            GetComponent<Animator>().SetBool("isAttacking", attacking);
            
            //Turning on damage variable
            violence = true;
        }
        if(Input.GetButtonUp("Attack"))
        {
            //Animation
            attacking = false;
            GetComponent<Animator>().SetBool("isAttacking", attacking);

            //Turning off damage variable
            violence = false;
        }
    }
}
