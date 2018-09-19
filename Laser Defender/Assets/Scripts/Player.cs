using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] float health = 200f;

    [Header("Laser")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFirePeriod = 0.1f;

    [Header("Sound")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] float laserSoundVolume = 0.7f;


    Text healthText;
    Coroutine firingCoroutine;
    float xMin, xMax, yMin, yMax;


	// Use this for initialization
	void Start () {
        SetUpMoveBoundaries();
	}

    void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding; ;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding; ;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding; ;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding; ;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        Fire();
	}

    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine =  StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserSound, transform.position, laserSoundVolume);
            yield return new WaitForSeconds(projectileFirePeriod);
        }
    }

    void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
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
            AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
            Destroy(gameObject);
            FindObjectOfType<Level>().LoadGameOver();
        }
    }
}

