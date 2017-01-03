using UnityEngine;
using System.Collections;

namespace AISandbox
{
    public class PredictionController : MonoBehaviour
    {
        private PursuitAndEvasion pursuitAndEvasionScript;
        private SpriteRenderer spriteRendererScript;
        private SimpleActor simpleActorScript;
        private void Start()
        {
            pursuitAndEvasionScript = GameObject.Find("Pursuit and Evasion").GetComponent<PursuitAndEvasion>();
            spriteRendererScript = GetComponent<SpriteRenderer>();
            simpleActorScript = pursuitAndEvasionScript._target_actor.GetComponent<SimpleActor>();
        }
        private void Update()
        {
            if (spriteRendererScript.enabled && (Mathf.Abs(Vector3.Distance(pursuitAndEvasionScript._pursuing_actor.transform.position, transform.position)) < 2.5f))
            {
                spriteRendererScript.enabled = false;
                Invoke("ResetLevel", 2.0f);
            }

            Vector3 targetActorVelocity = new Vector3(pursuitAndEvasionScript._target_actor.Velocity.x, pursuitAndEvasionScript._target_actor.Velocity.y);

            float futureTime = Vector3.Distance(pursuitAndEvasionScript._target_actor.transform.position, pursuitAndEvasionScript._pursuing_actor.transform.position) / simpleActorScript.MAX_SPEED;

            transform.position = pursuitAndEvasionScript._target_actor.transform.position + targetActorVelocity * futureTime;            
        }
        private void ResetLevel()
        {
            pursuitAndEvasionScript.ResetLevel();
        }
    }
}
