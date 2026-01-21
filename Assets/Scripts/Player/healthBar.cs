using UnityEngine;

public class healthBar : MonoBehaviour
{
    public GameObject Hopps;

    // Update is called once per frame
    void Update()
    {
        changeHealthBar(Hopps.GetComponent<Health>().health);
    }

    public void changeHealthBar(int healthAmount)
    {
        float scaleChange = healthAmount / 10f;
        transform.localScale = new Vector3(1f, scaleChange, 1f);
    }
}
