using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Health")]
    [SerializeField] float health=100;

    [Header("Laser")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFirePeriod = 0.1f;

    [Header("Death Stuff")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float deathVFXdestroy=1f;

    [Header("Sound")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] float laserSoundVolume = 0.7f;

    GameSession session;

    // Use this for initialization
    void Start () {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        session = FindObjectOfType<GameSession>();
    }
	
	// Update is called once per frame
	void Update () {
        CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(laserSound, transform.position, laserSoundVolume);
        /* yield return new WaitForSeconds(projectileFirePeriod); */
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            
            Destroy(gameObject);
            GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explosion, deathVFXdestroy);
            AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
            session.AddToScore(100);
        }
    }
}
