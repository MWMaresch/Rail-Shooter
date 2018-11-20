using System;
using UnityEngine;

public enum LaserType { Single, Twin, somethingelse };

public class Player : MonoBehaviour {

    public float maxSpeed;
    public float controlSpeed;
    public float zSpeed;
    public Transform crosshair;
    public Camera cam;
    public GameObject laser;
    public GameObject smallImpact;
    public GameObject muzzleFlash;
    public GameObject deathExplosion;
    public GameObject leftCannon;
    public GameObject rightCannon;
    public GameObject gameOverObject;
    public GameObject shieldHUD;
    public GameObject hp2HUD;
    public GameObject hp1HUD;
    public float Health { get { return health; } }
    public Vector3 velocity;

    private float health;
    private Vector3 prevPosition;
    private bool mouseEnabled = true;
    private bool shooting = false;
    private bool autoShooting = false;
    private float lastShootTime;
    private float damageTimer;
    private float shieldTimer;
    private Renderer muzzleFlashRend;
    private Vector3 cameraOrigPos;
    private Vector3 targetDirection;
    private LaserType laserType;
    private bool isAlive;
    private bool exploded;

    private float screenSnapFraction;

    // Use this for initialization
    void Start ()
    {
        shieldHUD.SetActive(true);
        hp2HUD.SetActive(true);
        hp1HUD.SetActive(false);
        velocity = new Vector3(0,0,0);
        exploded = false;
        isAlive = true;
        health = 2;
        laserType = LaserType.Single;
        cameraOrigPos = Camera.main.transform.position;
        lastShootTime = 999f;
        muzzleFlashRend = muzzleFlash.GetComponent<Renderer>();
        muzzleFlashRend.enabled = false;

        //this will eventually be moved to the main menu or something else that starts much earlier
        Application.targetFrameRate = 120;
        screenSnapFraction = Screen.width / 480f;
        //Debug.Log("target fps is: " + Application.targetFrameRate);
        //Screen.SetResolution(1440, 810, false, 180);
        //480, 960, 1440
        //270, 540, 810
    }
    //Update is called once per frame
    private void Update()
    {
        if (mouseEnabled)
        {
            Vector3 mousePos = new Vector3 (Mathf.RoundToInt(Input.mousePosition.x / screenSnapFraction) * screenSnapFraction, Mathf.RoundToInt(Input.mousePosition.y / screenSnapFraction) * screenSnapFraction, 5f);
            
            //this value must be the distance between the camera and the crosshair
            //mousePos.z = 5f;
            mousePos = cam.ScreenToWorldPoint(mousePos);
            if (Time.timeScale > 0)
                crosshair.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
        }
        //for now, because menus haven't been made yet, to switch to mouse controls, we press a button to toggle it
        if (Input.GetButtonDown("ToggleMouse")) 
        {
            if (mouseEnabled)
                mouseEnabled = false;
            else
                mouseEnabled = true;
        }
        if (Input.GetButtonDown("Fire1"))
            shooting = true;
        if (Input.GetButton("Fire1"))
            autoShooting = true;
        else
            autoShooting = false;

        //button presses are detected in update instead of FixedUpdate for two reasons:
        //1. less input delay at higher refresh rates
        //2. for some reason, they sometimes trigger twice when put in Update instead
    }
    // FixedUpdate is called once every 16ms
    void FixedUpdate()
    {
        //timer for muzzle flash
        if (muzzleFlashRend.enabled && lastShootTime > 0.06f)
            muzzleFlashRend.enabled = false;
        else
            lastShootTime += Time.fixedDeltaTime;

        if (!shieldHUD.activeSelf && shieldTimer <= 0)
            shieldHUD.SetActive(true);
        else
            shieldTimer -= Time.fixedDeltaTime;

        if (isAlive)
        {


            //timer for damage flickering
            if (damageTimer > 0)
            {
                Camera.main.transform.position += 0.2f * new Vector3(UnityEngine.Random.Range(-damageTimer, damageTimer), UnityEngine.Random.Range(-damageTimer, damageTimer), 0f);

                if (GetComponent<Renderer>().enabled)
                    GetComponent<Renderer>().enabled = false;
                else
                    GetComponent<Renderer>().enabled = true;
                damageTimer -= Time.fixedDeltaTime;
            }
            else if (!GetComponent<Renderer>().enabled)
            {
                GetComponent<Renderer>().enabled = true;
                Camera.main.transform.position = cameraOrigPos;
            }

            //move the crosshair
            if (!mouseEnabled)
            {
                if (!GlobalOptions.InvertYAxis)
                {
                    if (Input.GetButton("Sprint"))
                        crosshair.transform.position += new Vector3(Input.GetAxis("Horizontal") * 1.75f * controlSpeed, Input.GetAxis("Vertical") * 1.75f * controlSpeed);
                    else
                        crosshair.transform.position += new Vector3(Input.GetAxis("Horizontal") * 0.85f * controlSpeed, Input.GetAxis("Vertical") * 0.85f * controlSpeed);
                }
                else
                {
                    if (Input.GetButton("Sprint"))
                        crosshair.transform.position += new Vector3(Input.GetAxis("Horizontal") * 1.75f * controlSpeed, Input.GetAxis("Vertical") * -1.75f * controlSpeed);
                    else
                        crosshair.transform.position += new Vector3(Input.GetAxis("Horizontal") * 0.85f * controlSpeed, Input.GetAxis("Vertical") * -0.85f * controlSpeed);
                }
            }
            crosshair.LookAt(cam.transform);

            //here we check to make sure the ship and crosshair don't go out of bounds, and if they do, we stop it
            Vector3 screenCH = cam.WorldToScreenPoint(crosshair.transform.position);
            if (screenCH.x < 0)
                crosshair.transform.position = new Vector3(cam.ScreenToWorldPoint(new Vector3(0f, screenCH.y, screenCH.z)).x, crosshair.transform.position.y, crosshair.transform.position.z);
            else if (screenCH.x > Screen.width)
                crosshair.transform.position = new Vector3(cam.ScreenToWorldPoint(new Vector3(Screen.width, screenCH.y, screenCH.z)).x, crosshair.transform.position.y, crosshair.transform.position.z);

            if (screenCH.y < 0)
                crosshair.transform.position = new Vector3(crosshair.transform.position.x, cam.ScreenToWorldPoint(new Vector3(screenCH.x, 0f, screenCH.z)).y, crosshair.transform.position.z);
            else if (screenCH.y > Screen.height)
                crosshair.transform.position = new Vector3(crosshair.transform.position.x, cam.ScreenToWorldPoint(new Vector3(screenCH.x, Screen.height, screenCH.z)).y, crosshair.transform.position.z);


            //rotate the crosshair with the camera to prevent aliasing
            crosshair.rotation = cam.transform.rotation;

            //shooting is faster when mashing than when holding down the button
            if ((shooting && lastShootTime > 0.06f) || (autoShooting && lastShootTime > 0.15f))
            {
                muzzleFlash.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, UnityEngine.Random.Range(-1f, 1f), transform.rotation.w);
                lastShootTime = 0f;
                shooting = false;
                if (laserType == LaserType.Single)
                {
                    Instantiate(laser, transform.position, transform.rotation);
                }
                else if (laserType == LaserType.Twin)
                {
                    Instantiate(laser, rightCannon.transform.position, transform.rotation);
                    Instantiate(laser, leftCannon.transform.position, transform.rotation);
                }
                GetComponent<AudioSource>().Play();
                muzzleFlashRend.enabled = true;
            }


            if (transform.position.y < -2.5f)
                transform.position = new Vector3(transform.position.x, -2.5f, transform.position.z);

            //move the ship, and rotate it depending on how much it moved
            prevPosition = transform.position;
            targetDirection = new Vector3(crosshair.transform.position.x - transform.position.x, crosshair.transform.position.y - 0.4f - transform.position.y);

            if (targetDirection.magnitude * 0.2f >= maxSpeed)
                transform.position += maxSpeed * targetDirection.normalized;
            else
                transform.position += 0.2f * targetDirection;

            velocity.x = transform.position.x - prevPosition.x;
            velocity.y = transform.position.y - prevPosition.y;

            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.2f * damageTimer * (float)Math.Sin(damageTimer * 30f) - velocity.x * 0.75f, transform.rotation.w);
        }
        else if (!exploded)
        {
            transform.Rotate(new Vector3(0, 0, 1f), -50f * velocity.x);
            transform.position += new Vector3(velocity.x * 0.5f + UnityEngine.Random.Range(-0.2f, 0.2f), -0.1f + UnityEngine.Random.Range(-0.1f, 0.1f), 0);
            damageTimer += 1;
            if (damageTimer % 10 == 0)
            {
                Instantiate(smallImpact, transform.position, transform.rotation);
            }
        }
        else
        {
        }
    }

    public void TakeDamage(float damage)
    {
        shieldTimer = 7f;
        //indicator that we're hit
        if (!shieldHUD.activeSelf)
        {
            health -= damage;
            if (health == 1)
            {
                hp1HUD.SetActive(true);
                hp2HUD.SetActive(false);
            }
        }
        else
            shieldHUD.SetActive(false);

        if (health <= 0)
        {
            hp1HUD.SetActive(false);
            GetComponent<SpriteRenderer>().color = Color.red;
            isAlive = false;
            damageTimer = 0f;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
            damageTimer = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (health <= 0f && !exploded && damageTimer > 2f)
        {
            GetComponent<Renderer>().enabled = false;
            Instantiate(deathExplosion, transform.position, transform.rotation);
            exploded = true;
            Instantiate(gameOverObject, Camera.main.transform.position + new Vector3(0, 0, 1), new Quaternion(0, 0, 0, 0));
        }
        else if (damageTimer <= 0f)
        {
            if (other.tag == "EnemyWeapon")
            {
                Instantiate(smallImpact, transform.position, transform.rotation);
                TakeDamage(1);
                Destroy(other.gameObject);
            }
            else if (other.gameObject.tag == "Enemy")
            {
                other.GetComponent<Enemy>().Explode();
                TakeDamage(1);
            }
            else if (other.gameObject.tag == "TwinPowerup")
            {
                GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().AddScore(100);
                other.GetComponent<Renderer>().enabled = false;
                other.GetComponent<AudioSource>().Play();
                //Destroy(other.gameObject);
                laserType = LaserType.Twin;
            }
        }
    }
}
