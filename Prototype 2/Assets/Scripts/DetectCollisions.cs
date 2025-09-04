using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Instead of destroying the projectile when it collides with an animal
        //Destroy(other.gameObject); 

        // Just deactivate the food and destroy the animal
        if (other.CompareTag("Bullet"))
        {
            DifferentScores differentScores = GetComponent<DifferentScores>();

            if (differentScores != null)
                ScoreManager.instance.AddScore(differentScores.scoreValue);

            GetComponent<AnimalBar>().FeedAnimal(1);

            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Player"))
        {
            DifferentDamage differentDamage = GetComponent<DifferentDamage>();
            HealthManager.instance.Damage(differentDamage.damageValue);
        }
    }
}
