using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

    public enum Animation {
        Jump = 0,
        Run,
        Slash,
        Run_2,
    }

    private Animator player_anim;

    void Awake() {
        player_anim = GetComponent<Animator>();
    }

    public void Play( Animation anim ) {
        player_anim.SetInteger( "State", ( int ) anim );
    }
}
