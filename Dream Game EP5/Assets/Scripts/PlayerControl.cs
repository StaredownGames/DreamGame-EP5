using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int damage;
    Camera cam;
    Animator anim;
    public float speed = 1.25f;
    bool sneakin = false;
    public int weaponType = 0;
    public GameObject pistol;
    public GameObject activeEnemy;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindObjectOfType<Camera>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT
        float axisH = Input.GetAxis("Horizontal");
        float axisV = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(axisV,-axisH,0) * speed * Time.deltaTime);

        //FACE THE MOUSE POINTER

        //convert the mouse position to world coordinates.
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        //what direction we want to look at.
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        //get the distance from player to mousePosition.
        float playerToMouseDistance = Vector2.Distance(mousePosition,(Vector2)transform.position);

        if (playerToMouseDistance >= .25f)
        {
            transform.right = direction;

        }

        //ANIMATION
        if (Input.GetKeyDown(KeyCode.C))
        {
            weaponType++;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            weaponType--;
        }

        if (Input.GetMouseButton(1))
        {
            switch (weaponType)
            {
                case 0://No weapon
                    if (Input.GetMouseButtonDown(0))
                    {
                        anim.SetTrigger("punch");
                    }
                    if (anim.GetBool("isSneaking") == false)
                    {
                        anim.SetBool("isSneaking", true);
                    }
                    break;
                case 1: //Melee weapon
                    break;
                case 2: //Pistol
                    damage = 100;
                    pistol.SetActive(true);
                    if (anim.GetBool("isHolding") == false)
                    {
                        anim.SetBool("isHolding", true);
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        //Fire a ray from the player outward 10 "units"
                        RaycastHit2D hit = Physics2D.Raycast(pistol.transform.position,transform.right, 10f);
                        if (hit.collider != null)
                        {
                            GameObject go = hit.collider.gameObject;
                            switch (go.tag)
                            {
                                case "Enemy":
                                    activeEnemy = go;
                                    DealDamage();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case 3: //Rifle
                    break;
                case 4: //Heavy
                    break;
                default:
                    break;
            }
            
        }
        else
        {
            pistol.SetActive(false);
            anim.SetBool("isSneaking", false);
            anim.SetBool("isHolding",false);
        } 

        switch (axisV)
        {
            case 1:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetBool("isWalking", true);
                    speed = 1.5f;
                    break;
                }
                anim.SetBool("isRunning", true);
                speed = 2.5f;
                break;
            case -1:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetBool("isWalkingBackward", true);
                    speed = 1.5f;
                    break;
                }
                anim.SetBool("isRunningBackward", true);
                speed = 2.5f;
                break;
            default:
                anim.SetBool("isRunning", false);
                anim.SetBool("isRunningBackward", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isWalkingBackward", false);
                break;
        }

        switch (axisH)
        {
            case 1:
                anim.SetBool("strafeRight", true);
                break;
            case -1:
                anim.SetBool("strafeLeft", true);
                break;
            default:
                anim.SetBool("strafeRight", false);
                anim.SetBool("strafeLeft", false);
                break;
        }
    }

    void DealDamage()
    {
        AiControl enemyScript = activeEnemy.GetComponent<AiControl>();
        enemyScript.health -= damage;
    }
 }