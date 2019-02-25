using System.Collections;

using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeaderNav : BaseNav
{

    private Collider[] colliders;

    private float count;


    [HideInInspector] public ParticleSystem enemySystem;

    private RaycastHit hit;

    private Ray l_ray;

    private LayerMask mask;

    [SerializeField] private TextMeshProUGUI message;

    private bool timerEnabled = false;

    private ShopContainer prev_container;

    protected override void Start()
    {
        base.Start();
        hit = new RaycastHit();
        mask = LayerMask.GetMask("Enemy");
    }

    private void FixedUpdate()
    {
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
        lastPosition = transform.position;
    }

    public Vector3 clickpos;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        character.AnimationClass.animation.SetFloat("Walk", Agent.velocity.magnitude / Agent.speed);

        if (Input.GetMouseButton(0) && !StaticManager.UiInventory.ItemsInstance.windowIsOpen && !EventSystem.current.IsPointerOverGameObject())
        {
            l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int mask = 1 << 11;
            mask = ~mask;
            if (Physics.Raycast(l_ray, out hit, 1000, mask))
            {
                if (hit.collider.gameObject.layer == 0)
                {
                    SetState = state.MOVE;
                    if (character.enemy)
                    {
                        character.enemy.projector.gameObject.SetActive(false);
                    }

                    StaticManager.particleManager.Play(ParticleManager.states.Click, hit.point);

                    if (enemySystem)
                    {
                        Destroy(enemySystem.gameObject);
                    }
                }

                if (hit.collider.tag == "Enemy")
                {
                    if (character.enemy)
                    {
                        character.enemy.projector.gameObject.SetActive(false);
                    }
                    character.enemy = hit.collider.GetComponent<Enemy>();
                    if (character.attachedWeapon is GunType)
                    {
                        SetState = state.FREEZE;
                        rotate1();

                    }
                    else
                    {
                        SetState = state.ENEMY_CLICKED;
                    }
                    Destroy(enemySystem);

                    character.enemy.projector.gameObject.SetActive(true);
                }
            }


        }

        if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftShift))
        {
            Agent.isStopped = true;
            //SetState = state.FREEZE;
            l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int _mask = 1 << 11;
            _mask = ~_mask;
            if (character.attachedWeapon is GunType && Physics.Raycast(l_ray, out hit, 1000, _mask))
            {
                character.transform.LookAt(hit.point);
                character.attachedWeapon.Use();
            }
            else if(character.attachedWeapon is SwordType)
            {
                character.attachedWeapon.Use();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(l_ray, out hit))
            {
                if (hit.collider.tag == "ShopKeeper" && !StaticManager.UiInventory.ItemsInstance.windowIsOpen) //Left Click
                {
                    StaticManager.UiInventory.ShowWindow(StaticManager.UiInventory.ItemsInstance.ShopUI.obj);

                    if (prev_container)
                    {
                        prev_container.gameObject.SetActive(false);
                    }

                    var a = hit.collider.gameObject.GetComponent<Shop>();

                    a.shopContainer.gameObject.SetActive(true);

                    prev_container = a.shopContainer;


                    hit.collider.GetComponent<Shop>().shopContainer.gameObject.SetActive(true);
                    StaticManager.currencyManager._shop = hit.collider.GetComponent<Shop>();

                    StaticManager.currencyManager.SwitchToBuy();
                    StaticManager.Instance.Freeze();
                }
            }
        }

        if (!StaticManager.RealTime.Attacking)
        {
            colliders = Physics.OverlapSphere(transform.position, 10, mask);

            if (colliders.Length > 0)
            {
                StaticManager.RealTime.Attacking = true;
                StartCoroutine(yield());
            }
        }

        if (StaticManager.RealTime.Enemies.Count == 0)
        {
            StaticManager.RealTime.Attacking = false;
        }

        switch (State)
        {
            case state.ATTACKING:



                break;

                break;
            case state.MOVE:
                    Agent.SetDestination(hit.point);

                break;
            case state.ENEMY_CLICKED:
                rotate1();

                if (distance < 3)
                {
                    character.attachedWeapon.Use();
                }
                else
                {
                    SetState = state.MOVE;
                }

                break;
            case state.IDLE:

                break;
            case state.FOLLOW:

                break;
            case state.FREEZE:

                break;
            default:

                break;
        }
    }
    public IEnumerator rotate(Vector3 pos)
    {

        Vector3 _pos = new Vector3(pos.x, transform.position.y, pos.z);
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - _pos);

        while (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public void rotate1()
    {
        Vector3 enemy = new Vector3(character.enemy.transform.position.x, transform.position.y, character.enemy.transform.position.z);
        character.transform.LookAt(enemy);
        character.attachedWeapon.Use();
    }
    private IEnumerator show()
    {
        message.enabled = true;

        yield return new WaitForSeconds(2);

        message.enabled = false;
    }

    private IEnumerator yield()
    {
        yield return new WaitForSeconds(0.5f);

        colliders = Physics.OverlapSphere(transform.position, 20, mask);

        foreach (var l_collider in colliders)
        {
            StaticManager.RealTime.Attacking = true;
            StaticManager.RealTime.Enemies.Add(l_collider.gameObject.GetComponent<Enemy>());
            StaticManager.RealTime.Attacking = true;
            StaticManager.RealTime.SetAttackEnemies();
            SetState = state.ATTACKING;
        }
    }

}