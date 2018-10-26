public class CompanionNav : BaseNav {

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