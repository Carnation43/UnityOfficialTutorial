using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public static HealthManager instance;
    public int defaultLives = 3;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int value)
    {
        defaultLives -= value;
        if(defaultLives > 0)
        {
            healthText.text = "Lives  :  " + defaultLives.ToString();
        }
        else
        {
            defaultLives = 0;
            healthText.text = "Lives  :  " + defaultLives.ToString();
            GameManager.Instance.GameOver();
            print("Game Over...");
        }
        
    }
}
