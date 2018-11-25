using System.Collections;

using Kristal;

using UnityEngine;
using UnityEngine.Playables;

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

    protected override void Start( ) {
        base.Start();
        if ( tag != "PickUp" ){
            FillBullets(AttachedCharacter.gameObject);
        }
    }
    public void FillBullets(GameObject collider ) {
        bullets = new GameObject[30];
        for ( int i = 0 ; i < bullets.Length ; i++ ){
            bullets[ i ] = Instantiate( stats.Projectile );
            bullets[i].gameObject.SetActive(false);
            bullets[ i ].gameObject.layer = collider.gameObject.layer;
            bullets[i].transform.SetParent(GameObject.Find("Bullets").transform);
        }
    }
    public override object this[ string _property_name ] {
        get { return GetType( ).GetField( _property_name ).GetValue( this ); }
        set { GetType( ).GetField( _property_name ).SetValue( this , value ); }
    }
    
    public override void Attack(BaseCharacter enemy ) {
        Debug.Log( "Attacking" );

        if ( canFire && Ammo > 0 ){
            StartCoroutine( Fire(enemy ) );
        }
        else if ( !reloading && Ammo == 0 ){
            StartCoroutine( Reload( ) );
        }
    }

    private IEnumerator Fire(BaseCharacter enemy ) {

        canFire = false;
        var proj = GetPulledBullets( );
        proj.gameObject.transform.position = AttachedCharacter.transform.position + ( AttachedCharacter.transform.forward * 2 );
        proj.transform.rotation = transform.rotation;
        proj.gameObject.SetActive(true);
        StartCoroutine( destroyBullet( proj ) );

        Ammo -= 1;

        yield return new WaitForSeconds( 1 / FireRate );

        canFire = true;
    }

    IEnumerator destroyBullet(GameObject projectile ) {
        yield return new WaitForSeconds( 0.5f );
        projectile.gameObject.SetActive(false);
    }
    public override void PickUp( ) {
        base.PickUp();
        FillBullets(StaticManager.Character.gameObject);
    }
    protected override void OnTriggerEnter(Collider collider ) {
        base.OnTriggerEnter(collider);
       // projectile.layer = collider.gameObject.layer;
    }

    private IEnumerator Reload( ) {
        reloading = true;

        yield return new WaitForSeconds( ReloadTime );
        Ammo      = Capacity;
        reloading = false;
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