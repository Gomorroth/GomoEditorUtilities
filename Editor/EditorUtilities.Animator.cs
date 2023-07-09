using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

namespace gomoru.su
{
    partial class EditorUtilities
    {
        public static AnimatorController AddParameter<T>(this AnimatorController controller, string name, T defaultValue)
        {
            var param = new AnimatorControllerParameter()
            {
                name = name,

            };
            if (typeof(T) == typeof(float))
            {
                param.type = AnimatorControllerParameterType.Float;
                param.defaultFloat = (float)(object)defaultValue;
            }
            else if (typeof(T) == typeof(int))
            {
                param.type = AnimatorControllerParameterType.Int;
                param.defaultInt = (int)(object)defaultValue;
            }
            else if (typeof(T) == typeof(bool))
            {
                param.type = AnimatorControllerParameterType.Bool;
                param.defaultBool = (bool)(object)defaultValue;
            }
            else throw new ArgumentException(nameof(defaultValue));
            controller.AddParameter(param);
            return controller;
        }

        private static AnimatorCondition[] _conditions1 = new AnimatorCondition[1];
        private static AnimatorCondition[] _conditions2 = new AnimatorCondition[2];

        public static AnimatorStateTransition AddTransition(this AnimatorState state, AnimatorState destination, AnimatorCondition condition)
        {
            _conditions1[0] = condition;
            return state.AddTransition(destination, _conditions1);
        }

        public static AnimatorStateTransition AddTransition(this AnimatorState state, AnimatorState destination, AnimatorCondition condition1, AnimatorCondition condition2)
        {
            _conditions2[0] = condition1;
            _conditions2[1] = condition2;
            return state.AddTransition(destination, _conditions2);
        }

        public static AnimatorStateTransition AddTransition(this AnimatorState state, AnimatorState destination, params AnimatorCondition[] conditions)
        {
            var transition = new AnimatorStateTransition()
            {
                destinationState = destination,
                hasExitTime = false,
                duration = 0,
                hasFixedDuration = true,
                canTransitionToSelf = false,
                conditions = conditions,
            }.HideInHierarchy().AddTo(state);
            state.AddTransition(transition);
            return transition;
        }

        public static void ClearLayers(this AnimatorController controller) => controller.layers = Array.Empty<AnimatorControllerLayer>();

    }
}
