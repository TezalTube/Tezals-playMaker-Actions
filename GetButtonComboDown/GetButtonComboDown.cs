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
    [Tooltip("Sends an Event when multiple Buttons are pressed.")]
    public class GetButtonComboDown : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The names of the buttons to check. Define them in the Unity Input Manager.")]
        public FsmString[] buttonNames;  // Array to hold up to 4 buttons

        [Tooltip("Event to send if all the buttons are pressed.")]
        public FsmEvent sendEvent;

        [Tooltip("Set to True if all the buttons are pressed, otherwise False.")]
        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;

        public override void Reset()
        {
            buttonNames = new FsmString[4];  // Up to 4 buttons
            sendEvent = null;
            storeResult = null;
        }

        public override void OnUpdate()
        {
#if !NEW_INPUT_SYSTEM_ONLY
            // Check if all buttons are pressed
            bool allButtonsDown = buttonNames.All(button => Input.GetButtonDown(button.Value));

            storeResult.Value = allButtonsDown;

            if (allButtonsDown)
            {
                Fsm.Event(sendEvent);
            }
#endif
        }

#if UNITY_EDITOR
        public override string AutoName()
        {
            return ActionHelpers.AutoName(this, string.Join(", ", buttonNames.Select(b => b.Value))) + " " + (sendEvent != null ? sendEvent.Name : "");
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
