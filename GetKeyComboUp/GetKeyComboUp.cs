// (c) Copyright HutongGames, LLC 2010-2021. All rights reserved.

// The new Input System optionally supports the legacy input manager 
// Written by Chad Leuci (TezalTube) DO NOT CREDIT ME, THIS IS YOURS TO USE!!!   (:
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
#define NEW_INPUT_SYSTEM_ONLY
#endif

using System;
using System.Linq;

#if !NEW_INPUT_SYSTEM_ONLY
using UnityEngine;
#endif

namespace HutongGames.PlayMaker.Actions
{
#if NEW_INPUT_SYSTEM_ONLY
    [Obsolete("This action is not supported in the new Input System. " +
              "Use PlayerInputGetButtonValues or GamepadGetButtonValues instead.")]
#endif
    [ActionCategory(ActionCategory.Input)]
    [Tooltip("Sends an Event when multiple Keys are released.")]
    public class GetKeyComboUp : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The names of the keys to check. Define them as KeyCode values.")]
        public FsmString[] keyNames;  // Array to hold up to 4 keys

        [Tooltip("Event to send if all the keys are released.")]
        public FsmEvent sendEvent;

        [Tooltip("Set to True if all the keys are released, otherwise False.")]
        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;

        public override void Reset()
        {
            keyNames = new FsmString[4];  // Up to 4 keys
            sendEvent = null;
            storeResult = null;
        }

        public override void OnUpdate()
        {
#if !NEW_INPUT_SYSTEM_ONLY
            // Check if all keys are released
            bool allKeysUp = keyNames.All(key => Input.GetKeyUp((KeyCode)Enum.Parse(typeof(KeyCode), key.Value)));

            storeResult.Value = allKeysUp;

            if (allKeysUp)
            {
                Fsm.Event(sendEvent);
            }
#endif
        }

#if UNITY_EDITOR
        public override string AutoName()
        {
            return ActionHelpers.AutoName(this, string.Join(", ", keyNames.Select(k => k.Value))) + " " + (sendEvent != null ? sendEvent.Name : "");
        }

#if NEW_INPUT_SYSTEM_ONLY
        public override string ErrorCheck()
        {
            return "This action is not supported in the new Input System. " +
                   "Use PlayerInputGetButtonValues or GamepadGetButtonValues instead.";
        }
#endif

#endif
    }
}


