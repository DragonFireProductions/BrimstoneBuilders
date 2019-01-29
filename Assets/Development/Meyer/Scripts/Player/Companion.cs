using Assets.Meyer.TestScripts.Player;

using UnityEngine;

public class Companion : BaseCharacter {

    // Use this for initialization

    public PlayerInventory inventory;

    public CharacterInventoryUI inventoryUI;

    [ HideInInspector ] public int cost;

    [ HideInInspector ] public Magic magic = new Magic( );

    [ HideInInspector ] public Range range = new Range( );

    [ HideInInspector ] public Mele mele = new Mele( );

    [ HideInInspector ] public SubClasses CurrentSubClass;

    public Armor armor;

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

    protected void Awake( ) {
        base.Awake( );
    }

    protected virtual void Start( ) {
        magic.character = this;
        range.character = this;
        mele.character  = this;

        switch (attachedWeapon.type)
        {
            case SubClasses.Types.MELEE:
                CurrentSubClass = mele;
                break;
            case SubClasses.Types.RANGE:
                CurrentSubClass = range;

                break;
            case SubClasses.Types.MAGIC:
                CurrentSubClass = magic;
                break;
        }

        if ( tag == "Player" ){
            inventoryUI.Init( this );
        }
        else{
            inventoryUI.CharacterInventory.SetActive( false );
        }

        StaticManager.RealTime.Companions.Add( this );

        if ( this as Character ){
            StaticManager.map.Add(Map.Type.player, icon);
        }
        else{
            StaticManager.map.Add(Map.Type.companion, icon);
        }
    }

    public void OnTriggerEnter( Collider collider ) { }

    public override void Damage( int _damage , BaseItems item ) {
        if ( stats.Health > 0 ){
            base.Damage( _damage , item );
        }

        if ( stats.Health <= 0 ){
            if ( this == StaticManager.Character ){
                StaticManager.uiManager.GameOverWindow.SetActive( true );
                Time.timeScale = 0;
            }
            else{
            StaticManager.currencyManager.companions--;
                var a = Nav as CompanionNav;
            Destroy( a.behaviors.gameObject );
            StaticManager.RealTime.Companions.Remove( this );
                Destroy(inventoryUI.sendToButton.gameObject);
                Destroy(inventoryUI.tab.gameObject);
                Destroy(inventoryUI.CompanionSell.gameObject);
                Destroy(gameObject);
            }
           
        }
    }

    public void Remove( BaseCharacter _chara ) { }

}