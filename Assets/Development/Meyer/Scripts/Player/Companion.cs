public class Companion : BaseCharacter {
    

    // Use this for initialization
    private void Start( ) {
        Awake( );
        material.color = BaseColor;
        Nav            = gameObject.GetComponent < CompanionNav >( );
    }

    public override void Damage( BaseCharacter _attacker ) { }

    public void Remove( BaseCharacter _chara ) { }

}