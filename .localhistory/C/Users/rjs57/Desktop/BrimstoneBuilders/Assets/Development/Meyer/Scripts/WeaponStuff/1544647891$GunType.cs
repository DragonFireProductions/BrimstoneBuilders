using System.Collections;

using Kristal;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GunType : WeaponObject {

    [ SerializeField ] public int Ammo;

    private bool canFire = true;

    [ SerializeField ] public int Capacity;

    [ SerializeField ] public float FireRate;

    [ SerializeField ] public float Range;

    private bool reloading;

    public Projectile projectile;

    private GameObject[] bullets;

    [ SerializeField ] public float ReloadTime;

    public Image reloading_prog;

    protected override void Start( ) {
        base.Start();
        if ( tag != "PickUp" ){
            FillBullets(AttachedCharacter.gameObject);
        }


        reloading_prog = GetComponent<Image>();
        type = SubClasses.Types.RANGE;
        reloading_prog.fillAmount = 0.0f;
    }

    //void Awake()
    //{
    //    reloading_prog.fillAmount = 0.0f;
    //}
    public void FillBullets(GameObject collider ) {
        bullets = new GameObject[10];
        for ( int i = 0 ; i < bullets.Length ; i++ ){
            bullets[ i ] = Instantiate( projectile.gameObject );
            bullets[i].gameObject.SetActive(false);
            bullets[ i ].gameObject.layer = collider.gameObject.layer;
            bullets[i].transform.SetParent(GameObject.Find("BulletContainer").transform);
            bullets[ i ].GetComponent < Projectile >( ).weapon = this;
        }
    }
    public override object this[string propertyName]
    {
        get
        {
            if (this.GetType().GetField(propertyName) != null)
            {
                return this.GetType().GetField(propertyName).GetValue(this);
            }
            else if (base[propertyName] != null)
            {
                return base[propertyName];
            }
            else
            {
                return null;
            }
        }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
    }
    public override void Use( ) {
        base.Use();
        Debug.Log( "Attacking" );

        if ( canFire && Ammo > 0 ){
            StartCoroutine( Fire( ) );
        }
        else if ( !reloading && Ammo == 0 ){
            StartCoroutine( Reload( ) );
        }
    }

    private IEnumerator Fire( ) {

        canFire = false;
        var proj = GetPulledBullets( );

        proj.gameObject.transform.position = AttachedCharacter.transform.position + ( AttachedCharacter.transform.forward * 2 );
        proj.transform.rotation = transform.rotation;

        proj.gameObject.SetActive(true);
        StartCoroutine( stopBullet( 2 , proj ) );
        Ammo -= 1;
        reloading_prog.fillAmount += 1.0f;
        yield return new WaitForSeconds( FireRate );
        reloading_prog.fillAmount = 0.0f;
        canFire = true;
    }


    public IEnumerator stopBullet(int i, GameObject proj ) {
        yield return new WaitForSeconds(i);
        proj.SetActive(false);
    }

    public override void AssignDamage()
    {
        var a = AttachedCharacter as Companion;
        Damage = a.range.CurrentLevel;
    }
    private IEnumerator Reload( ) {
        reloading = true;

        yield return new WaitForSeconds( ReloadTime );
        Ammo      = Capacity;
        reloading = false;

    }

    public override void Attach( ) {
        base.Attach();

        if ( bullets == null ){
        FillBullets(AttachedCharacter.gameObject);
        }
    }


    public override void IncreaseSubClass(float amount ) {
        base.IncreaseSubClass(amount);
        var character = AttachedCharacter as Companion;
        int level = (int)character.range.CurrentLevel;
        character.range.IncreaseLevel(amount);
        int currLevel = ( int )character.range.CurrentLevel;
        if ( currLevel - level == 1 )
            InstatiateFloatingText.InstantiateFloatingText("RANGE++",character,  Color.magenta, new Vector3(1,1,1));
    }
    [HideInInspector] protected int _lastBullet;
    public GameObject GetPulledBullets( ) {
            GameObject currentBullet;
            if (_lastBullet == bullets.Length - 1)
            {
                _lastBullet   = 0;
                currentBullet = bullets[0];
            }
            else
            {
                currentBullet = bullets[_lastBullet];
            }

            _lastBullet += 1;
            return currentBullet;
    }
}