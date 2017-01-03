using UnityEngine;
using System.Collections;

namespace AISandbox{
    public class Buttons : MonoBehaviour{
        private PursuitAndEvasion pursuitAndEvasionScript;
        void Start(){
            pursuitAndEvasionScript = GameObject.Find("Pursuit and Evasion").GetComponent<PursuitAndEvasion>();
        }
        public void ResetLevel(){
            pursuitAndEvasionScript.ResetLevel();
        }
        public void QuitGame(){
            pursuitAndEvasionScript.QuitGame();
        }
    }
}
