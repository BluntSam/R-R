using UnityEngine;

public class Disabler : MonoBehaviour
{
    public GameObject screen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        screen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
