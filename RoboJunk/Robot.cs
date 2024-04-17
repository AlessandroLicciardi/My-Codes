using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Robot : MonoBehaviour
{
    //Le parti commentate erano in uso con il vecchio sistema di movimento per evitare di chiamare value o metodi erroneamente le ho disattivate (Amos Illi Grignani)

    [SerializeField] public LayerMask platformsLayer;
    public float Distance { get { return distance; } }
    public float MoveSpeed = 5.0f;
    public float JumpForce = 10.0f;
    public float DropSpeed = 10.0f;
    public enum eActorState
    {
        Grounded,
        Jumping,
        Falling,
        Dead,
        Digging
    }

    public eActorState state;
    private eActorState PrevState;
    //private bool GroundedRay;
    private bool GroundedBox;
    public float CheckRadius;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    public BoxCollider2D ForwardCollider; 
    Animator animator;
    public bool shooting;

    public GameObject Player;
    private float distance;
    public float extraheight = 0.5f;                        //distance for raycast ground trigger

    public float JumpTimeCounter;
    public float JumpTime;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;
    public GameObject ground;
    public UiController uiController;

    [SerializeField] [Range(0f, 1f)] public float digSpd;
    [SerializeField] [Range(0f, 1f)] public float UnDigSpd;

    public GameObject Shooting_Projectile;
    public float Shooting_duration, Shooting_delay, Shooting_projectileSpeed, Shooting_ProjectileDespawnOffset;
    public Vector3 Shooting_ProjectileSpawnOffset;
    // public float ShootingDurationLeft, ShootingCurrentTrash = 0;
    // public int Shooting_TrashForActivation;

    [Header ("Animations")]
    public GameObject RobotDrill;
    public GameObject RobotNormal;

    [Header ("Particles")]
    public GameObject groundParticles;

    [Header ("Sounds")]
    public AudioClip JumpSound;
    public AudioController audioController;
    //private float CurrentLoad = 5;



    //public float CurrentYVelocity;
    //private Spawner spawner;
    //private float downLimit, groundLimit;
    //private Vector2 direction, position;
    //int sideSpeed;                                  //speed the player moves up- or down-wards

    private int currentTutorial;

    void Awake()
    {
        ForwardCollider = GetComponentInChildren<BoxCollider2D>();
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        state = eActorState.Falling;
        animator = GetComponent<Animator>();
        shooting = false;
        RobotNormal.SetActive(true);
        RobotDrill.SetActive(false);
        //spawner = FindObjectOfType<Spawner>();
        //downLimit = spawner.ScreenEdgeUnderground;
        //groundLimit = spawner.groundLimitY;
    }


    private void Start()
    {
        distance = 0;
        state = eActorState.Falling;
        //sideSpeed = 2;
    }

    void Update()
    {
        CheckAnimation();
        gravityModifier();
        if (state != eActorState.Digging)
        {
            StartCoroutine(CheckIfGrounded());
        }
        // if (ShootingCurrentTrash >= Shooting_TrashForActivation)
        // {
        //     StartCoroutine(Shoot());
        // }


        if (SceneManager.GetActiveScene().buildIndex == 2 && currentTutorial != 6) return;
        
        if (Input.GetKeyDown(KeyCode.Space) && state == eActorState.Grounded && state != eActorState.Dead && SceneManager.GetActiveScene().buildIndex != 0)
        {
            audioController.PlayClip(JumpSound);
            GameObject particles = Instantiate(groundParticles, new Vector3 (transform.position.x, transform.position.y - 0.7f, transform.position.z), Quaternion.identity);
            particles.transform.SetParent(FindObjectOfType<GameController>().GetComponent<PathController>().CurrentTracks[0].transform);
            state = eActorState.Jumping;
            rb.velocity = Vector3.up * JumpForce;
            JumpTimeCounter = JumpTime;
        }

        if (Input.GetKey(KeyCode.Space) && state == eActorState.Jumping)
        {
            if (JumpTimeCounter > 0)
            {
                rb.velocity = Vector3.up * JumpForce;
                JumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                state = eActorState.Falling;
            }          
        }

        if (Input.GetKeyUp(KeyCode.Space) && state != eActorState.Digging)
        {
            state = eActorState.Falling;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) /*&& state != eActorState.Grounded*/ && state != eActorState.Digging && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (state == eActorState.Grounded && transform.position.y < 1)
            {
                StartCoroutine(Digging());
            }
            else
            {
                StartCoroutine(FastDrop());
            }
        }

        //if(Input.GetKey(KeyCode.DownArrow) && state == eActorState.Grounded && transform.position.y < 1)
        //{
        //    StartCoroutine(Digging());
        //}

        //if(Input.GetKeyUp(KeyCode.DownArrow) && state == eActorState.Digging && transform.position.y < (ground.transform.position.y - boxCollider.bounds.extents.y /2))
        //{
        //    StartCoroutine(UnDigging());
        //}
    }



    IEnumerator CheckIfGrounded()
    {
        //GroundedRay = Physics.Raycast(boxCollider.bounds.center, Vector3.down, boxCollider.bounds.extents.y + extraheight, platformsLayer);
        //GroundedCircle = Physics.BoxCast(new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.extents.y, boxCollider.bounds.center.z), boxCollider.bounds.extents, Vector3.down);
        GroundedBox = Physics.BoxCast(boxCollider.bounds.center, boxCollider.bounds.extents / 2, Vector3.down, Quaternion.identity,boxCollider.bounds.extents.y + extraheight, platformsLayer);
        float cooldown = 0f;
        if (rb.velocity.y < 0 && !GroundedBox && state != eActorState.Dead && state != eActorState.Digging && transform.position.y > (ground.transform.position.y + boxCollider.bounds.extents.y / 2))
        {
            state = eActorState.Falling;
            cooldown = 0f;
        }
        else if (GroundedBox && state != eActorState.Jumping && state != eActorState.Dead)
        {
            state = eActorState.Grounded;
            cooldown = 0.2f;
        }
        yield return new WaitForSeconds(cooldown);
    }

    IEnumerator Digging()
    {
        if (state == eActorState.Falling)
        {
            yield return null;
        }
        if(SceneManager.GetActiveScene().buildIndex != 2)
        {
            GameObject particles = Instantiate(groundParticles, new Vector3 (transform.position.x, transform.position.y - 0.7f, transform.position.z), Quaternion.identity);
            particles.transform.SetParent(FindObjectOfType<GameController>().GetComponent<PathController>().CurrentTracks[0].transform);
        }
        state = eActorState.Digging;
        rb.useGravity = false;
        ground.GetComponent<MeshCollider>().enabled = false;
        rb.velocity = new Vector3(0, rb.velocity.y + (-11 * digSpd), rb.velocity.z);
        yield return new WaitForSeconds(0.1f);
        RobotNormal.SetActive(false);
        RobotDrill.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        while (Input.GetKey(KeyCode.DownArrow) && state == eActorState.Digging)
        {
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(UnDigging());
        //rb.velocity = new Vector3(0, 0, rb.velocity.z);
    }

    IEnumerator UnDigging()
    {
        rb.velocity = new Vector3(0, 15 * UnDigSpd, rb.velocity.z);
        yield return new WaitForSeconds(0.1f);
        RobotNormal.SetActive(true);
        RobotNormal.GetComponent<AudioSource>().enabled = true;
        RobotDrill.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        ground.GetComponent<MeshCollider>().enabled = true;
        rb.useGravity = true;
        RobotDrill.transform.rotation = Quaternion.Euler(32f, 270f, 0f);
        //rb.velocity = new Vector3(0, 0, rb.velocity.z);
        state = eActorState.Falling;
        yield return new WaitForSeconds(0.5f);
        RobotNormal.GetComponent<AudioSource>().enabled = false;
    }

    private void gravityModifier()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }


    private void CheckAnimation()
    {
        if (state != PrevState)
        {
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", false);
            animator.SetBool("Running", false);
            animator.SetBool("Dying", false);
            animator.SetBool("Digging", false);
            switch (state)
            {

                case eActorState.Grounded:
                    animator.SetBool("Running", true);
                    break;
                case eActorState.Jumping:
                    animator.SetBool("Jumping", true);
                    break;
                case eActorState.Falling:
                    animator.SetBool("Falling", true);
                    break;
                case eActorState.Dead:
                    animator.SetBool("Dying", true);
                    break;
                case eActorState.Digging:
                    animator.SetBool("Digging", true);
                    break;
            }
            //Debug.Log(state + "");
        }
        PrevState = state;

    }


    public eActorState ShowState()
    {
        return state;
    }

    public IEnumerator Ledgegrab()
    {
       state = eActorState.Jumping;
       rb.AddForce(new Vector3(0, JumpForce/4, 0), ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator FastDrop()
    {
        eActorState TempState = state;
        rb.AddForce(-(new Vector3(0, DropSpeed, 0)), ForceMode.VelocityChange);
        while (state == TempState)
        {
            yield return new WaitForEndOfFrame();
        }
        if (Input.GetKey(KeyCode.DownArrow) && state == eActorState.Grounded && transform.position.y < 1)
        {
            StartCoroutine(Digging());
        }
        yield return new WaitForSeconds(0.05f);
        if(SceneManager.GetActiveScene().buildIndex != 2)
        {
            GameObject particles = Instantiate(groundParticles, new Vector3 (transform.position.x, transform.position.y - 0.7f, transform.position.z), Quaternion.identity);
            particles.transform.SetParent(FindObjectOfType<GameController>().GetComponent<PathController>().CurrentTracks[0].transform);
        }
        yield return null;
    }

    IEnumerator FastDropWithoutDig()
    {
        eActorState TempState = state;
        rb.AddForce(-(new Vector3(0, DropSpeed, 0)), ForceMode.VelocityChange);
        while (state == TempState)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    // public void Shooting_AddTrashCounter()
    // {
    //     ShootingCurrentTrash++;
    // }

    public void TutorialDig()
    {
        StartCoroutine(Digging());
    }
    public void TutorialFastDrop()
    {
        StartCoroutine(FastDrop());
    }
    public void TutorialFastDropWithoutDig()
    {
        StartCoroutine(FastDropWithoutDig());
    }

    public void setTutorialIndex(int index)
    {
        currentTutorial = index;
    }

    // public void RetryReset()
    // {
    //     ShootingCurrentTrash = 0;
    //     ShootingDurationLeft = 0;
    // }

    // IEnumerator Shoot()
    // {
    //     shooting = true;
    //     ShootingCurrentTrash = 0;
    //     uiController.PowerUpSlider.maxValue = Shooting_duration;
    //     uiController.PowerUpSlider.value = CurrentLoad;
    //     StartCoroutine(PowerUpBar());
    //     GameObject _proj;
    //     ShootingDurationLeft = Shooting_duration;
    //     while (ShootingDurationLeft > 0)
    //     {
    //         _proj = Instantiate(Shooting_Projectile, transform.position + Shooting_ProjectileSpawnOffset, Quaternion.identity);
    //         _proj.GetComponent<Projectile>().MoveSpeed = Shooting_projectileSpeed;
    //         _proj.GetComponent<Projectile>().DespawnOffset = Shooting_ProjectileDespawnOffset;
    //         yield return new WaitForSeconds(Shooting_delay);
    //         ShootingDurationLeft -= Shooting_delay;
    //         ShootingCurrentTrash--;
    //     }
    //     ShootingCurrentTrash = 0;
    //     CurrentLoad = 5;
    //     uiController.PowerUpSlider.maxValue = Shooting_TrashForActivation;
    //     shooting = false;
    // }

    // IEnumerator PowerUpBar()
    // {
    //     if(shooting)
    //     {
    //         CurrentLoad -= load;
    //         uiController.PowerUpSlider.value = CurrentLoad;
    //         yield return new WaitForSeconds(ReloadBarTime);
    //         StartCoroutine(PowerUpBar());
    //     }
    //     else
    //     {
    //         StopCoroutine(PowerUpBar());
    //     }
    // }


    //private void CheckIfGrounded()
    //{

    //    if (rb.velocity.y < -0.01 && Mathf.Abs(transform.position.y - Ground.transform.position.y) > extraheight)
    //    {
    //        state = eActorState.Falling;
    //    }
    //    else if (rb.velocity.y == 0 && Mathf.Abs(transform.position.y - Ground.transform.position.y) <= extraheight)
    //    {
    //        state = eActorState.Grounded;
    //    }

    //}


    //private void Move()
    //{


    //    if (state == eActorState.Grounded)
    //    {
    //        direction += Vector2.zero;
    //        position.x = transform.position.x;
    //        position.y = (direction + sideSpeed * Time.deltaTime * Vector2.right).y;
    //        if (transform.position.y > groundLimit + extraheight)
    //        {
    //            position.y = groundLimit; //if not jumping cannot go upwards indefnitely
    //            direction = Vector2.zero; //stop moving
    //        }
    //        if (transform.position.y < downLimit + extraheight)
    //        {
    //            position.y = downLimit;    //you reched the bottom
    //            direction = Vector2.zero; //stop moving
    //        }
    //        distance += MoveSpeed * Time.deltaTime;
    //        rb.AddForce(new Vector3(0, position.y, 0), ForceMode.Force);
    //    }
    //}

    //private void TakeInput()
    //{
    //    direction = Vector2.zero;

    //    if (Input.GetKey(KeyCode.DownArrow) && Player.transform.position.y > downLimit + extraheight)
    //    {
    //        direction = sideSpeed * Vector2.down;
    //    }

    //    if (Input.GetKey(KeyCode.UpArrow) && Player.transform.position.y < groundLimit + extraheight)
    //    {
    //        direction = sideSpeed * Vector2.up;
    //    }
    //}

}
