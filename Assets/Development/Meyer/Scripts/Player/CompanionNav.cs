public class CompanionNav : BaseNav {

    public void Start( ) {
        base.Start( );
        SetState = state.FOLLOW;
    }
    private void Update( ) {
        switch ( State ){
            case state.FOLLOW: {
                Agent.destination = Assets.Meyer.TestScripts.Player.Character.Player.transform.position;
            }

                break;
            default:

                break;
        }
    }

}