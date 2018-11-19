using System.Collections;

using Kristal;

using UnityEngine;
using UnityEngine.Playables;

public class GunType : WeaponObject {

    [ SerializeField ] public int Ammo;

    private bool canFire = true;

    [ SerializeField ] public int Capacity;

    [ SerializeField ] public float FireRate;

    [ SerializeField ] private GameObject projectile;

    [ SerializeField ] public float Range;

    private bool reloading;

    [ SerializeField ] public float ReloadTime;

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

        var l_projectile = Instantiate( this.projectile , AttacheBaseCharacter.transform.position + (AttacheBaseCharacter.transform.forward * 2) , transform.rotation ).GetComponent < Projectile >( );
        Destroy( l_projectile.gameObject , Range / l_projectile.GetSpeed( ) );

        Ammo -= 1;

        yield return new WaitForSeconds( 1 / FireRate );

        canFire = true;
    }

    protected override void OnTriggerEnter(Collider collider ) {
        base.OnTriggerEnter(collider);
        projectile.layer = AttacheBaseCharacter.gameObject.layer;
    }

    private IEnumerator Reload( ) {
        reloading = true;

        yield return new WaitForSeconds( ReloadTime );

        Ammo      = Capacity;
        reloading = false;
    }

}