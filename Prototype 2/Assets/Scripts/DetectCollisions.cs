using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            DifferentScores differentScores = GetComponent<DifferentScores>();

            if (differentScores != null)
                ScoreManager.instance.AddScore(differentScores.scoreValue);

            GetComponent<AnimalBar>().FeedAnimal(1);
        
            Destroy(other.gameObject); // Ïú»Ù×Óµ¯
         
        }

        if (other.CompareTag("Player"))
        {

            DifferentDamage differentDamage = GetComponent<DifferentDamage>();
            HealthManager.instance.Damage(differentDamage.damageValue);
        }
    }
}
