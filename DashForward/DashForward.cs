using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Applies a force to the player to cause a dash effect in a set direction with a set amount of force.")]
    public class DashForward : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The Rigidbody of the player. The dash force will be applied to this.")]
        public FsmOwnerDefault player;

        [Tooltip("The amount of force to apply for the dash.")]
        public FsmFloat dashForce;

        [Tooltip("The direction of the dash.")]
        public FsmVector3 dashDirection;

        [Tooltip("Set to True if you want to apply the dash only once. Set to False if the dash should keep applying during the state.")]
        public bool applyOnce;

        private Rigidbody rb;

        public override void Reset()
        {
            player = null;
            dashForce = 10f;
            dashDirection = new FsmVector3 { Value = new Vector3(1, 0, 0) }; // Default direction: forward on the X-axis
            applyOnce = true;
        }

        public override void OnEnter()
        {
            // Get the player's Rigidbody
            rb = Fsm.GetOwnerDefaultTarget(player).GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Apply the dash force in the specified direction
                Dash();

                if (applyOnce)
                {
                    Finish(); // End the action immediately after one dash
                }
            }
            else
            {
                Debug.LogWarning("Rigidbody not found on the player.");
                Finish();
            }
        }

        public override void OnUpdate()
        {
            // If dash should be applied every frame, we could check the condition here and apply force each frame.
            if (!applyOnce && rb != null)
            {
                Dash();
            }
        }

        private void Dash()
        {
            // Apply the force to the Rigidbody in the specified direction, with the set dash force
            rb.AddForce(dashDirection.Value.normalized * dashForce.Value, ForceMode.Impulse);
        }
    }
}
