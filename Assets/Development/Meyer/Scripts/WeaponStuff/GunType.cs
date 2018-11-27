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
            bullets[ i ] = Instantiate( projectile.gameObject );
            bullets[i].gameObject.SetActive(false);
            bullets[ i ].gameObject.layer = collider.gameObject.layer;
            bullets[i].transform.SetParent(GameObject.Find("BulletContainer").transform);
            bullets[ i ].GetComponent < Projectile >( ).weapon = this;
        }
    }
    public override object this[ string _property_name ] {
        get { return GetType( ).GetField( _property_name ).GetValue( this ); }
        set { GetType( ).GetField( _property_name ).SetValue( this , value ); }
    }
    
    public override void Use(BaseCharacter enemy ) {
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
       
        Ammo -= 1;

        yield return new WaitForSeconds( 1 / FireRate );

        canFire = true;
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