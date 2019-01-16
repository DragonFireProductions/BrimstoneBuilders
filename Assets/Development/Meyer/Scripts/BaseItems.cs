using UnityEngine;
using UnityEngine.UI;

public class BaseItems : MonoBehaviour {



    public virtual object this[ string propertyName ] {
        get {
            if ( GetType( ).GetField( propertyName ) != null ){
                return GetType( ).GetField( propertyName ).GetValue( this );
            }
            else if ( stats[ propertyName ] != null ){
                return stats[ propertyName ];
            }
            return null;
        }
        set { GetType( ).GetField( propertyName ).SetValue( this , value ); }
    }

    public GameObject item;

    public ItemStats stats;

    public BaseCharacter AttachedCharacter;

    public string objectName;

    public RawImage icon;

    public GameObject light;

    protected virtual void Start( ) { }

    public virtual void Use( ) { }

    public virtual void Attach( ) { }

    public virtual void AssignDamage( ) { }

    public virtual void IncreaseSubClass( float amount ) { }

}