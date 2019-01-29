using System.Collections;

using UnityEngine;

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
        base.Start( );

        if ( tag != "PickUp" ){
            FillBullets( AttachedCharacter.gameObject );
        }

        type = SubClasses.Types.RANGE;

        if ( AttachedCharacter ){
            AttachedCharacter.AnimationClass.SwitchWeapon(this);
        }
    }

    public void FillBullets( GameObject collider ) {
        bullets = new GameObject[10];

        for ( var i = 0 ; i < bullets.Length ; i++ ){
            bullets[ i ] = Instantiate( projectile.gameObject );
            bullets[ i ].gameObject.SetActive( false );
            bullets[ i ].gameObject.layer = collider.gameObject.layer;
            bullets[ i ].transform.SetParent( GameObject.Find( "BulletContainer" ).transform );
            bullets[ i ].GetComponent < Projectile >( ).weapon = this;
        }
    }

    public override object this[ string propertyName ] {
        get {
            if ( GetType( ).GetField( propertyName ) != null ){
                return GetType( ).GetField( propertyName ).GetValue( this );
            }

            if ( base[ propertyName ] != null ){
                return base[ propertyName ];
            }

            return null;
        }
        set { GetType( ).GetField( propertyName ).SetValue( this , value ); }
    }

    public override void Use( ) {
        base.Use( );
        Debug.Log( "Attacking" );

        if ( canFire && Ammo > 0 ){
            AttachedCharacter.AnimationClass.Play( AnimationClass.states.Attack );
        }
        else if ( !reloading && Ammo == 0 ){
            StartCoroutine( Reload( ) );
        }

        int ind = Random.Range( 0 , clips.Length - 1 );
        audio.clip = clips[ind];

    }

    public override void Activate( ) {
        StartCoroutine( Fire( ) );
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }

    private IEnumerator Fire( ) {
        if ( KnockBack ){
            AttachedCharacter.KnockBack( KnockBackAmount );
        }

        var proj = GetPulledBullets( );

        proj.gameObject.transform.position = AttachedCharacter.bulletPosition.position;
        proj.transform.rotation            = AttachedCharacter.bulletPosition.rotation;

        proj.gameObject.SetActive( true );
        proj.GetComponent < Rigidbody >( ).AddForce( AttachedCharacter.transform.forward * proj.GetComponent < Projectile >( ).GetSpeed( ) , ForceMode.Impulse );
        StartCoroutine( stopBullet( 2 , proj ) );
        Ammo -= 1;

        yield return new WaitForSeconds( FireRate );

        canFire = true;
    }

    public IEnumerator stopBullet( int i , GameObject proj ) {
        yield return new WaitForSeconds( i );

        proj.SetActive( false );
    }

    public override void AssignDamage( ) {
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
        type = SubClasses.Types.RANGE;
        base.Attach( );

        if ( bullets == null ){
            FillBullets( AttachedCharacter.gameObject );
        }
    }

    public override void IncreaseSubClass( float amount ) {
        base.IncreaseSubClass( amount );
        var character = AttachedCharacter as Companion;
        var level     = ( int )character.range.CurrentLevel;
        character.range.IncreaseLevel( amount );
        var currLevel = ( int )character.range.CurrentLevel;

        if ( currLevel - level == 1 ){
            InstatiateFloatingText.InstantiateFloatingText( "RANGE++" , character , Color.green , new Vector3( 1 , 1 , 1 ) , 0.2f );
        }
    }

    [ HideInInspector ] protected int _lastBullet;

    public GameObject GetPulledBullets( ) {
        GameObject currentBullet;

        if ( _lastBullet == bullets.Length - 1 ){
            _lastBullet   = 0;
            currentBullet = bullets[ 0 ];
        }
        else{
            currentBullet = bullets[ _lastBullet ];
        }

        _lastBullet += 1;

        return currentBullet;
    }

}