using System.Collections;

using UnityEngine;

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
    
    public void Attack( ) {
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

        var l_projectile = Instantiate( this.projectile , transform.position + transform.forward , transform.rotation ).GetComponent < Projectile >( );
        Destroy( l_projectile.gameObject , Range / l_projectile.GetSpeed( ) );

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

}