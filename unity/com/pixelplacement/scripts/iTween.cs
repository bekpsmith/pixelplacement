//VERSION: 1.0.20

/*
 Copyright (c) 2010 Bob Berkebile(http://www.pixelplacement.com), C# port by Patrick Corkum(http://www.insquare.com)

 Permission is hereby granted, free of charge, to any person  obtaining a copy of this software and associated documentation  files (the "Software"), to deal in the Software without  restriction, including without limitation the rights to use,  copy, modify, merge, publish, distribute, sublicense, and/or sell  copies of the Software, and to permit persons to whom the  Software is furnished to do so, subject to the following conditions:
 The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
/* 
 
/*
TERMS OF USE - EASING EQUATIONS

Open source under the BSD License.

Copyright � 2001 Robert Penner
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
    * Neither the name of the author nor the names of contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


    /// <summary>
    /// iTween is intended for creating simple animations on gameObjects within Unity.
    /// </summary>
    public class iTween : MonoBehaviour
    {
        #region structural stuff
        #region enums

        /// <summary>
        /// EasingTypes are the different methods in which the position of the game object is calculated 
        /// during each frame.
        /// </summary>
        public enum EasingType
        {
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInQuad,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeOutQuad,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInOutQuad,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInCubic,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeOutCubic,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInOutCubic,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInQuart,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeOutQuart,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInOutQuart,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInQuint,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeOutQuint,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInOutQuint,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInSine,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeOutSine,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInOutSine,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInExpo,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeOutExpo,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInOutExpo,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInCirc,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeOutCirc,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInOutCirc,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            linear,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            spring,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            bounce,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInBack,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeOutBack,
            /// <summary>
            /// Description not yet made.
            /// </summary>
            easeInOutBack
        }

        /// <summary>
        /// The LoopType is used to cause certain types of tweening to loop without need for a callback to do it.
        /// </summary>
        public enum LoopType
        {
            /// <summary>
            /// Will not loop - default behavior
            /// </summary>
            none,
            /// <summary>
            /// Will animate from start -> end, start -> end, start -> end
            /// </summary>
            loop,
            /// <summary>
            /// Will animate from start -> end -> start -> end, etc...
            /// </summary>
            pingPong
        }

        /// <summary>
        /// The FunctionType is the type of tween that can occur
        /// </summary>
        public enum FunctionType
        {
            /// <summary>
            /// fade is any fade operation, (i.e. fadeFrom and fadeTo)
            /// </summary>
            fade,
            /// <summary>
            /// move is any move operation, (i.e. moveFrom, moveTo, moveBy and moveBezier)
            /// </summary>
            move,
            /// <summary>
            /// scale is any scale operation, (i.e. scaleFrom, scaleTo, and scaleBy)
            /// </summary>
            /// 
            scale,
            /// <summary>
            /// rotate is any rotate operation, (i.e. rotateFrom, rotateTo, and rotateBy)
            /// </summary>
            rotate,
            /// <summary>
            /// color is any operation that is changing the rgb color, (i.e. colorFrom and colorTo)
            /// </summary>
            color,
            /// <summary>
            /// audio is any operation that changes the audio's pitch and/or volume, (i.e. audioTo and audioFrom)
            /// </summary>
            audio,
            /// <summary>
            /// punchPosition is the punch Operation that punches the object via moving the coordinates of the object
            /// </summary>
            punchPosition,
            /// <summary>
            /// punchRotation is the punch Operation that punches the object via rotating the object.
            /// </summary>
            punchRotation,
            /// <summary>
            /// shake is the operation that will shake the object
            /// </summary>
            shake,
            /// <summary>
            /// stab is the operation that will play an audio clip.
            /// </summary>
            stab
        }
        #endregion

        //helper class
        private struct Arguments
        {
            public float time;
            public float delay;
            public EasingType transition;

            public bool isXRSet;
            public bool isYGSet;
            public bool isZBSet;
            public float xr;
            public float yg;
            public float zb;

            public float alpha;

            public float volume;
            public float pitch;
            public AudioClip clip;
            public AudioSource audioSource;

            public string onComplete;
            public object onComplete_params;
            public GameObject onComplete_target;

            public FunctionType type;
            public bool isReverse;
            public bool isBy;
            public bool isWorld;
            public bool isBezier;
            public bool isMultiply;
            public List<Vector3> beziers;
            public LoopType loopType;
            public Vector3? lookAt;
            public bool orientToPath;
            public bool includeChildren;

            private Arguments(float? i_time, float i_defaultTime, float? i_delay, float i_defaultDelay, EasingType? i_transition, EasingType i_defaultTransition, string i_onComplete, object i_onComplete_params, FunctionType i_type, bool i_reverse, GameObject i_onCompleteTarget)
            {
                time = i_time.HasValue ? i_time.Value : i_defaultTime;
                delay = i_delay.HasValue ? i_delay.Value : i_defaultDelay;
                transition = i_transition.HasValue ? i_transition.Value : i_defaultTransition;
                onComplete = i_onComplete;
                onComplete_params = i_onComplete_params;
                isReverse = i_reverse;
                type = i_type;
                clip = null;
                audioSource = null;
                pitch = 0;
                volume = 0;
                alpha = 0;
                xr = 0;
                yg = 0;
                zb = 0;
                isXRSet = false;
                isYGSet = false;
                isZBSet = false;
                isBy = false;
                isWorld = false;
                isBezier = false;
                isMultiply = false;
                beziers = null;
                onComplete_target = i_onCompleteTarget;
                loopType = LoopType.none;
                orientToPath = true;
                lookAt = null;
                includeChildren = true;
            }
            public Arguments(float? i_time, float i_defaultTime, float? i_delay, float i_defaultDelay, EasingType? i_transition, EasingType i_defaultTransition, string i_onComplete, object i_onComplete_params, FunctionType i_type, bool i_reverse,
                            float? i_xr, float? i_yg, float? i_zb, bool i_isBy, bool i_isWorld, bool i_isMultiply, bool i_includeChildren, GameObject i_onCompleteTarget, LoopType i_loopType)
                : this(i_time, i_defaultTime, i_delay, i_defaultDelay, i_transition, i_defaultTransition, i_onComplete, i_onComplete_params, i_type, i_reverse, i_onCompleteTarget)
            {
                isXRSet = i_xr.HasValue;
                isYGSet = i_yg.HasValue;
                isZBSet = i_zb.HasValue;

                xr = isXRSet ? i_xr.Value : 0;
                yg = isYGSet ? i_yg.Value : 0;
                zb = isZBSet ? i_zb.Value : 0;
                isBy = i_isBy;
                isWorld = i_isWorld;
                isMultiply = i_isMultiply;
                loopType = i_loopType;
                includeChildren = i_includeChildren;
            }
            public Arguments(float? i_time, float i_defaultTime, float? i_delay, float i_defaultDelay, EasingType? i_transition, EasingType i_defaultTransition, string i_onComplete, object i_onComplete_params, FunctionType i_type, bool i_reverse,
                           List<Vector3> i_beziers, bool i_isWorld, GameObject i_onCompleteTarget, LoopType i_loopType, bool i_orientToPath, Vector3? i_lookAt)
                : this(i_time, i_defaultTime, i_delay, i_defaultDelay, i_transition, i_defaultTransition, i_onComplete, i_onComplete_params, i_type, i_reverse, i_onCompleteTarget)
            {
                isWorld = i_isWorld;
                isBezier = true;
                beziers = i_beziers;
                orientToPath = i_orientToPath;
                lookAt = i_lookAt;
                loopType = i_loopType;
            }
            public Arguments(float? i_time, float i_defaultTime, float? i_delay, float i_defaultDelay, EasingType? i_transition, EasingType i_defaultTransition, string i_onComplete, object i_onComplete_params, FunctionType i_type, bool i_reverse,
                           float? i_alpha, float i_defaultAlpha, bool i_includeChildren, GameObject i_onCompleteTarget, LoopType i_loopType)
                : this(i_time, i_defaultTime, i_delay, i_defaultDelay, i_transition, i_defaultTransition, i_onComplete, i_onComplete_params, i_type, i_reverse, i_onCompleteTarget)
            {
                alpha = i_alpha.HasValue ? i_alpha.Value : i_defaultAlpha;
                loopType = i_loopType;
                includeChildren = i_includeChildren;
            }

            public Arguments(float? i_time, float i_defaultTime, float? i_delay, float i_defaultDelay, EasingType? i_transition, EasingType i_defaultTransition, string i_onComplete, object i_onComplete_params, FunctionType i_type, bool i_reverse,
                           float? i_volume, float i_defaultVolume, float? i_pitch, float i_defaultPitch, AudioClip i_clip, AudioSource i_audioSource, GameObject i_onCompleteTarget)
                : this(i_time, i_defaultTime, i_delay, i_defaultDelay, i_transition, i_defaultTransition, i_onComplete, i_onComplete_params, i_type, i_reverse, i_onCompleteTarget)
            {
                volume = i_volume.HasValue ? i_volume.Value : i_defaultVolume;
                pitch = i_pitch.HasValue ? i_pitch.Value : i_defaultPitch;
                clip = i_clip;
                audioSource = i_audioSource;
            }
        }

        //help class for bezierCurves
        private struct BezierPointInfo
        {
            public Vector3 starting;
            public Vector3 intermediate;
            public Vector3 end;
        }

        //This allows us to shortcut looking up which easing function to use for each tween on each frame
        private delegate float easingFunction(float start, float end, float value);

        #endregion

        #region default values

        //Stab Defaults
        /// <summary>
        /// The Default Delay time for stab operations. This can be modified in code.
        /// </summary>
        public static float stabDefaultDelay = 0;
        /// <summary>
        /// The Default volume that stab operations will play at. This can be modified in code.
        /// </summary>
        public static float stabDefaultVolume = 1;
        /// <summary>
        /// The Default pitch that stab operations will play at. This can be modified in code.
        /// </summary>
        public static float stabDefaultPitch = 1;


        //Audio Defaults
        /// <summary>
        /// The Default time that it will take to transition the audio. This can be modified in code.
        /// </summary>
        public static float audioDefaultTime = 1;
        /// <summary>
        /// The Defaulttdelay time to transition the audio. This can be modified in code.
        /// </summary>
        public static float audioDefaultDelay = 0;
        /// <summary>
        /// The Default transition type for audio "animation". This can be modified in code.
        /// </summary>
        public static EasingType audioDefaultTransition = EasingType.linear;
        /// <summary>
        /// The Default volume that the audio will transition to for audio operations. This can be modified in code.
        /// </summary>
        public static float audioDefaultVolume = 1;
        /// <summary>
        /// The Default pitch that will be used to transition the audio to for audio operations. This can be modified in code.
        /// </summary>
        public static float audioDefaultPitch = 1;



        //Shake Defaults
        /// <summary>
        /// The Default time to shake objects for shake operations. This can be modified in code.
        /// </summary>
        public static float shakeDefaultTime = 1;
        /// <summary>
        /// The Default delay time to wait to perform shake operations. This can be modified in code.
        /// </summary>
        public static float shakeDefaultDelay = 0;



        //Punch Rotation Defaults
        /// <summary>
        /// The Default time that the punch rotation will take. This can be modified in code.
        /// </summary>
        public static float punchRotationDefaultTime = 1;
        /// <summary>
        /// The Default delay time before punch rotations will occur. This can be modified in code.
        /// </summary>
        public static float punchRotationDefaultDelay = 0;


        //Punch Position Defaults
        /// <summary>
        /// The Default time that the punch position will take. This can be modified in code.
        /// </summary>
        public static float punchPositionDefaultTime = 1;
        /// <summary>
        /// The Default delay time before punch positions will occur. This can be modified in code.
        /// </summary>
        public static float punchPositionDefaultDelay = 0;



        //Fade Defaults
        /// <summary>
        /// The Default time that the fade operations will take to fade the object. This can be modified in code.
        /// </summary>
        public static float fadeDefaultTime = 1;
        /// <summary>
        /// The Default delay time before fade operations will occur. This can be modified in code.
        /// </summary>
        public static float fadeDefaultDelay = 0;
        /// <summary>
        /// The Default transition type for fade "animation". This can be modified in code.
        /// </summary>
        public static EasingType fadeDefaultTransition = EasingType.linear;



        //Move Defaults
        /// <summary>
        /// The Default time that the move operations will take to move the object. This can be modified in code.
        /// </summary>
        public static float moveDefaultTime = 1;
        /// <summary>
        /// The Default delay time before move operations will occur. This can be modified in code.
        /// </summary>
        public static float moveDefaultDelay = 0;
        /// <summary>
        /// The Default transition type for move animation. This can be modified in code.
        /// </summary>
        public static EasingType moveDefaultTransition = EasingType.easeInOutCubic;




        //Move Defaults
        /// <summary>
        /// The Default time that the move operations using Bezier Curves will take to move the object. This can be modified in code.
        /// </summary>
        public static float moveBezierDefaultTime = 1;
        /// <summary>
        /// The Default delay time before move operations using Bezier Curves will occur. This can be modified in code.
        /// </summary>
        public static float moveBezierDefaultDelay = 0;
        /// <summary>
        /// The Default transition type for move animation using Bezier Curves. This can be modified in code.
        /// </summary>
        public static EasingType moveBezierDefaultTransition = EasingType.easeInOutCubic;

        /// <summary>
        /// The Default transition type for move animation using Bezier Curves. This can be modified in code.
        /// </summary>
        public static float moveBezierDefaultLookAtSpeed = 8;


        //Scale Defaults
        /// <summary>
        /// The Default time that the scale operations will take to scale the object. This can be modified in code.
        /// </summary>
        public static float scaleDefaultTime = 1;
        /// <summary>
        /// The Default delay time before scale operations will occur. This can be modified in code.
        /// </summary>
        public static float scaleDefaultDelay = 0;
        /// <summary>
        /// The Default transition type for scale animation. This can be modified in code.
        /// </summary>
        public static EasingType scaleDefaultTransition = EasingType.easeInOutCubic;


        //Rotate To Defaults
        /// <summary>
        /// The Default time that the rotating operations will take to rotate the object. (This is not used by rotateBy) This can be modified in code.
        /// </summary>
        public static float rotateDefaultTime = 1;
        /// <summary>
        /// The Default delay time rotating operations will rotate the object. (This is not used by rotateBy) This can be modified in code.
        /// </summary>
        public static float rotateDefaultDelay = 0;
        /// <summary>
        /// The Default transition type for rotate animation. (This is not used by rotateBy) This can be modified in code.
        /// </summary>
        public static EasingType rotateDefaultTransition = EasingType.easeInOutCubic;



        //Rotate By Defaults
        /// <summary>
        /// The Default time that the color operations will take to change the color of the object. This can be modified in code.
        /// </summary>
        public static float rotateByDefaultTime = 1;
        /// <summary>
        /// The Default delay time rotatingBy operations will rotate the object. (This is only used by rotateBy) This can be modified in code.
        /// </summary>
        public static float rotateByDefaultDelay = 0;
        /// <summary>
        /// The Default transition type for rotateBy animation. (This is only used by rotateBy) This can be modified in code.
        /// </summary>
        public static EasingType rotateByDefaultTransition = EasingType.easeInOutCubic;

        //Rotate Add Defaults
        /// <summary>
        /// The Default time that the color operations will take to change the color of the object. This can be modified in code.
        /// </summary>
        public static float rotateAddDefaultTime = 1;
        /// <summary>
        /// The Default delay time rotatingAdd operations will rotate the object. (This is only used by rotateBy) This can be modified in code.
        /// </summary>
        public static float rotateAddDefaultDelay = 0;
        /// <summary>
        /// The Default transition type for rotateAdd animation. (This is only used by rotateBy) This can be modified in code.
        /// </summary>
        public static EasingType rotateAddDefaultTransition = EasingType.easeInOutCubic;

        //Color To Defaults
        /// <summary>
        /// The Default time that the rotating operations will take to rotate the object. (This is not used by rotateBy) This can be modified in code.
        /// </summary>
        public static float colorDefaultTime = 1;
        /// <summary>
        /// The Default delay time before color operations will occur. This can be modified in code.
        /// </summary>
        public static float colorDefaultDelay = 0;
        /// <summary>
        /// The Default transition type for color "animation". This can be modified in code.
        /// </summary>
        public static EasingType colorDefaultTransition = EasingType.linear;
        #endregion

        /// <summary>
        /// This property will inform you if the tween is currently animating. This will be true after the delay and false before. 
        /// </summary>
        public bool inProgress;

        //this is our initialization variable
        private Arguments args;

        //shortcuts
        private Transform _transform;

        private bool _stopTween = false;
                

        private void StopTween()
        {
            _stopTween = true;
        }
        /// <summary>
        /// This is called by Unity and should be ignored by anyone using the library
        /// </summary>
        /// <returns>must be an IEnumerator in order to yield, which it must to delay execution.</returns>
        public IEnumerator Start()
        {
            _transform = transform;

            if (!inProgress && args.includeChildren)
            {
                foreach (Transform child in _transform)
                {
                    //Note, if Arguments was a class, this would not work. Since it is a struct, I get a copy.
                    Arguments newArgs = args;
                    newArgs.onComplete = null;
                    init(child.gameObject, newArgs);
                }
            }
            
            //delay:
            if (args.delay > 0)
            {
                yield return new WaitForSeconds(args.delay);
            }
            
            //don't destroy conflicts if we are not running yet.
            checkForConflicts();
            inProgress = true;

            
            switch (args.type)
            {
                case FunctionType.fade:
                    StartCoroutine(tweenFade());
                    break;

                case FunctionType.move:
                    //bezier is different enough to warrant it's own function here.
                    if (args.isBezier)
                    {
                        StartCoroutine(tweenMoveBezier());
                    }
                    else
                    {
                        StartCoroutine(tweenMove());
                    }
                    break;

                case FunctionType.scale:
                    StartCoroutine(tweenScale());
                    break;

                case FunctionType.rotate:
                    StartCoroutine(tweenRotate());
                    break;

                case FunctionType.color:
                    StartCoroutine(tweenColor());
                    break;

                case FunctionType.punchPosition:
                    StartCoroutine(tweenPunchPosition());
                    break;

                case FunctionType.punchRotation:
                    StartCoroutine(tweenPunchRotation());
                    break;

                case FunctionType.shake:
                    StartCoroutine(tweenShake());
                    break;

                case FunctionType.audio:
                    StartCoroutine(tweenAudio());
                    break;

                case FunctionType.stab:
                    StartCoroutine(tweenStab());
                    break;
            }

            yield return -1;
        }

        #region private non-static helper function

        private void DoCallback()
        {
            //callbacks?
            if (args.onComplete != null)
            {
                if (args.onComplete_target == null)
                {
                    SendMessage(args.onComplete, args.onComplete_params, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    args.onComplete_target.SendMessage(args.onComplete, args.onComplete_params, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        private void printVector(string caption, Vector3 v)
        {
            Debug.Log(caption + " - {" + v.x.ToString("f4") + ", " + v.y.ToString("f4") + ", " + v.z.ToString("f4") + "}");
        }

        //Check for and remove running tweens of same type:
        private void checkForConflicts()
        {
            Component[] scripts = GetComponents(typeof(iTween));
            if (scripts.Length > 1)
            {
                //this is okay to cast as isharpTween because we are only getting that type
                foreach (iTween tween in scripts)
                {
                    if ((tween != this) && tween.inProgress && IsTweenSameType(tween.args.type, args.type))
                    {
                        Destroy(tween);
                    }
                }
            }
        }

        //I know that this function does not need to be a seperate function, but it is possible
        //that in the future we might want to have 2 different enums be considered equivalent
        private static bool IsTweenSameType(FunctionType functionType, FunctionType type)
        {
            return (functionType == type);
        }

        private easingFunction getEasingFunction(EasingType easing)
        {

            switch (easing)
            {
                case EasingType.easeInQuad:
                    return new easingFunction(easeInQuad);
                case EasingType.easeOutQuad:
                    return new easingFunction(easeOutQuad);
                case EasingType.easeInOutQuad:
                    return new easingFunction(easeInOutQuad);
                case EasingType.easeInCubic:
                    return new easingFunction(easeInCubic);
                case EasingType.easeOutCubic:
                    return new easingFunction(easeOutCubic);
                case EasingType.easeInOutCubic:
                    return new easingFunction(easeInOutCubic);
                case EasingType.easeInQuart:
                    return new easingFunction(easeInQuart);
                case EasingType.easeOutQuart:
                    return new easingFunction(easeOutQuart);
                case EasingType.easeInOutQuart:
                    return new easingFunction(easeInOutQuart);
                case EasingType.easeInQuint:
                    return new easingFunction(easeInQuint);
                case EasingType.easeOutQuint:
                    return new easingFunction(easeOutQuint);
                case EasingType.easeInOutQuint:
                    return new easingFunction(easeInOutQuint);
                case EasingType.easeInSine:
                    return new easingFunction(easeInSine);
                case EasingType.easeOutSine:
                    return new easingFunction(easeOutSine);
                case EasingType.easeInOutSine:
                    return new easingFunction(easeInOutSine);
                case EasingType.easeInExpo:
                    return new easingFunction(easeInExpo);
                case EasingType.easeOutExpo:
                    return new easingFunction(easeOutExpo);
                case EasingType.easeInOutExpo:
                    return new easingFunction(easeInOutExpo);
                case EasingType.easeInCirc:
                    return new easingFunction(easeInCirc);
                case EasingType.easeOutCirc:
                    return new easingFunction(easeOutCirc);
                case EasingType.easeInOutCirc:
                    return new easingFunction(easeInOutCirc);
                case EasingType.linear:
                    return new easingFunction(linear);
                case EasingType.spring:
                    return new easingFunction(spring);
                case EasingType.bounce:
                    return new easingFunction(bounce);
                case EasingType.easeInBack:
                    return new easingFunction(easeInBack);
                case EasingType.easeOutBack:
                    return new easingFunction(easeOutBack);
                case EasingType.easeInOutBack:
                    return new easingFunction(easeInOutBack);
                default:
                    //todo: Perhaps we should throw an error
                    return null;
            }
        }

        private Vector3 ReverseDirection(Vector3 currentVector)
        {
            Vector3 newVector = new Vector3(args.xr, args.yg, args.zb);
            args.xr = currentVector.x;
            args.yg = currentVector.y;
            args.zb = currentVector.z;

            return newVector;
        }

        private void DefaultArgsToStartingPosition(Vector3 defaultVector)
        {
            if (!args.isXRSet)
            {
                args.xr = defaultVector.x;
            }
            if (!args.isYGSet)
            {
                args.yg = defaultVector.y;
            }
            if (!args.isZBSet)
            {
                args.zb = defaultVector.z;
            }
        }


        //Helper method for translating control points into bezier information.
        private List<BezierPointInfo> ParseBeziers(List<Vector3> points)
        {
            List<BezierPointInfo> returnPoints = new List<BezierPointInfo>();
            
            if (points.Count > 2)
            {

                //the first item is the starting position of the current point for that axis. So, we are storing off the following values:
                //The starting position for the current point for the axis
                //A smoothing point for the curve
                //The next major point			
                for (int iCurPoint = 0; iCurPoint < points.Count - 1; iCurPoint++)
                {
                    Vector3 curPoint = points[iCurPoint];

                    //I know I am going to store exactly 3, the starting, intermediate and end.
                    BezierPointInfo curSetofPoints;

                    curSetofPoints.starting = curPoint;
                    if (iCurPoint == 0)
                    {
                        curSetofPoints.intermediate = points[1] - ((points[2] - curPoint) / 4);
                    }
                    else
                    {
                        //double the current point minus the prior point's intermediate position
                        curSetofPoints.intermediate = 2 * curPoint - returnPoints[iCurPoint - 1].intermediate;
                    }

                    //This is fine because we end at the next to last item.
                    curSetofPoints.end = points[iCurPoint + 1];

                    returnPoints.Add(curSetofPoints);
                }
            }
            else
            {
                BezierPointInfo curSetofPoints;
                curSetofPoints.starting = points[0];
                curSetofPoints.intermediate = ((points[0] + points[1]) / 2);
                curSetofPoints.end = points[1];
                returnPoints.Add(curSetofPoints);
            }

            return returnPoints;
        }


        #region easing Functions
        //Easing curves - credits: Robert Penner and a few other that I can't recall right now with a few additions and tweaks:
        private float linear(float start, float end, float value)
        {
            return Mathf.Lerp(start, end, value);
        }

        private float clerp(float start, float end, float value)
        {
            float min = 0.0f;
            float max = 360.0f;
            float half = Mathf.Abs((max - min) / 2.0f);
            float retval = 0.0f;
            float diff = 0.0f;

            if ((end - start) < -half)
            {
                diff = ((max - start) + end) * value;
                retval = start + diff;
            }
            else if ((end - start) > half)
            {
                diff = -((max - end) + start) * value;
                retval = start + diff;
            }
            else retval = start + (end - start) * value;
            return retval;
        }

        private float spring(float start, float end, float value)
        {
            value = Mathf.Clamp01(value);
            value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
            return start + (end - start) * value;
        }

        private float easeInQuad(float start, float end, float value)
        {
            end -= start;
            return end * value * value + start;
        }

        private float easeOutQuad(float start, float end, float value)
        {
            end -= start;
            return -end * value * (value - 2) + start;
        }

        private float easeInOutQuad(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return end / 2 * value * value + start;
            value--;
            return -end / 2 * (value * (value - 2) - 1) + start;
        }

        private float easeInCubic(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value + start;
        }

        private float easeOutCubic(float start, float end, float value)
        {
            value--;
            end -= start;
            return end * (value * value * value + 1) + start;
        }

        private float easeInOutCubic(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return end / 2 * value * value * value + start;
            value -= 2;
            return end / 2 * (value * value * value + 2) + start;
        }

        private float easeInQuart(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value * value + start;
        }

        private float easeOutQuart(float start, float end, float value)
        {
            value--;
            end -= start;
            return -end * (value * value * value * value - 1) + start;
        }

        private float easeInOutQuart(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return end / 2 * value * value * value * value + start;
            value -= 2;
            return -end / 2 * (value * value * value * value - 2) + start;
        }

        private float easeInQuint(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value * value * value + start;
        }

        private float easeOutQuint(float start, float end, float value)
        {
            value--;
            end -= start;
            return end * (value * value * value * value * value + 1) + start;
        }

        private float easeInOutQuint(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return end / 2 * value * value * value * value * value + start;
            value -= 2;
            return end / 2 * (value * value * value * value * value + 2) + start;
        }

        private float easeInSine(float start, float end, float value)
        {
            end -= start;
            return -end * Mathf.Cos(value / 1 * (Mathf.PI / 2)) + end + start;
        }

        private float easeOutSine(float start, float end, float value)
        {
            end -= start;
            return end * Mathf.Sin(value / 1 * (Mathf.PI / 2)) + start;
        }

        private float easeInOutSine(float start, float end, float value)
        {
            end -= start;
            return -end / 2 * (Mathf.Cos(Mathf.PI * value / 1) - 1) + start;
        }

        private float easeInExpo(float start, float end, float value)
        {
            end -= start;
            return end * Mathf.Pow(2, 10 * (value / 1 - 1)) + start;
        }

        private float easeOutExpo(float start, float end, float value)
        {
            end -= start;
            return end * (-Mathf.Pow(2, -10 * value / 1) + 1) + start;
        }

        private float easeInOutExpo(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return end / 2 * Mathf.Pow(2, 10 * (value - 1)) + start;
            value--;
            return end / 2 * (-Mathf.Pow(2, -10 * value) + 2) + start;
        }

        private float easeInCirc(float start, float end, float value)
        {
            end -= start;
            return -end * (Mathf.Sqrt(1 - value * value) - 1) + start;
        }

        private float easeOutCirc(float start, float end, float value)
        {
            value--;
            end -= start;
            return end * Mathf.Sqrt(1 - value * value) + start;
        }

        private float easeInOutCirc(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return -end / 2 * (Mathf.Sqrt(1 - value * value) - 1) + start;
            value -= 2;
            return end / 2 * (Mathf.Sqrt(1 - value * value) + 1) + start;
        }

        private float bounce(float start, float end, float value)
        {
            value /= 1f;
            end -= start;
            if (value < (1 / 2.75f))
            {
                return end * (7.5625f * value * value) + start;
            }
            else if (value < (2 / 2.75f))
            {
                value -= (1.5f / 2.75f);
                return end * (7.5625f * (value) * value + .75f) + start;
            }
            else if (value < (2.5 / 2.75))
            {
                value -= (2.25f / 2.75f);
                return end * (7.5625f * (value) * value + .9375f) + start;
            }
            else
            {
                value -= (2.625f / 2.75f);
                return end * (7.5625f * (value) * value + .984375f) + start;
            }
        }

        private float easeInBack(float start, float end, float value)
        {
            end -= start;
            value /= 1;
            float s = 1.70158f;
            return end * (value) * value * ((s + 1) * value - s) + start;
        }

        private float easeOutBack(float start, float end, float value)
        {
            float s = 1.70158f;
            end -= start;
            value = (value / 1) - 1;
            return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
        }

        private float easeInOutBack(float start, float end, float value)
        {
            float s = 1.70158f;
            end -= start;
            value /= .5f;
            if ((value) < 1)
            {
                s *= (1.525f);
                return end / 2 * (value * value * (((s) + 1) * value - s)) + start;
            }
            value -= 2;
            s *= (1.525f);
            return end / 2 * ((value) * value * (((s) + 1) * value + s) + 2) + start;
        }

        private float punch(float amplitude, float value)
        {
            float s = 9;

            if (value == 0)
            {
                return 0;
            }

            if (value == 1)
            {
                return 0;
            }

            float period = 1 * 0.3f;

            s = period / (2 * Mathf.PI) * Mathf.Asin(0);
            return (amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 1 - s) * (2 * Mathf.PI) / period));

        }

        #endregion
        #endregion


        #region actual tween coroutines

        private void setFadeFrom()
        {
            if (guiTexture)
            {
                Color currentColor = guiTexture.color;

                guiTexture.color = new Color(currentColor.r, currentColor.g, currentColor.b, args.alpha);
                args.alpha = currentColor.a;
            }
            else
            {
                Color currentColor = renderer.material.color;

                renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, args.alpha);
                args.alpha = currentColor.a;
            }
        }

        ////Fade to application:
        private IEnumerator tweenFade()
        {
            if (args.isReverse)
            {
                setFadeFrom();
            }


            //define targets:
            float endA = args.alpha;

            //run tween:
            //don't force a divide by zero, just set it to the end value
            float startingAlpha = 0;
            if (args.time > 0)
            {
                
                if (_transform.guiTexture)
                {
                    startingAlpha = _transform.guiTexture.color.a;
                }
                else
                {
                    startingAlpha = _transform.renderer.material.color.a;
                }

                easingFunction easing = getEasingFunction(args.transition);

                float curTime = 0.0f;
                while ((curTime < 1) && (!_stopTween))
                {
                    float newAlpha = easing(startingAlpha, endA, curTime);

                    //move
                    if (_transform.guiTexture)
                    {
                        Color newColor = _transform.guiTexture.color;
                        newColor.a = newAlpha;
                        _transform.guiTexture.color = newColor;
                    }
                    else
                    {
                        Color newColor = _transform.renderer.material.color;
                        newColor.a = newAlpha;
                        _transform.renderer.material.color = newColor;
                    }

                    yield return 0;
                    curTime += Time.deltaTime * (1 / args.time);
                }
            }

            if (!_stopTween)
            {
                //make sure we end up where we are supposed to
                if (_transform.guiTexture)
                {
                    Color endColor = _transform.guiTexture.color;
                    endColor.a = endA;
                    _transform.guiTexture.color = endColor;
                }
                else
                {
                    Color endColor = _transform.renderer.material.color;
                    endColor.a = endA;
                    _transform.renderer.material.color = endColor;
                }

                DoCallback();

                //Loop?
                if (args.time > 0)
                {
                    switch (args.loopType)
                    {
                        case LoopType.none:
                            Destroy(this);
                            break;
                        case LoopType.loop:
                            //go back to the beginning
                            if (_transform.guiTexture)
                            {
                                Color newColor = _transform.guiTexture.color;
                                newColor.a = startingAlpha;
                                _transform.guiTexture.color = newColor;
                            }
                            else
                            {
                                Color newColor = _transform.renderer.material.color;
                                newColor.a = startingAlpha;
                                _transform.renderer.material.color = newColor;
                            }
                            StartCoroutine(Start());
                            break;
                        case LoopType.pingPong:
                            args.alpha = startingAlpha;
                            StartCoroutine(Start());
                            break;
                        default:
                            Destroy(this);
                            break;
                    }
                }
                else
                {
                    Destroy(this);
                }
            }
            else
            {
                Destroy(this);
            }
        }

        private void setMoveFrom()
        {
            //move it
            if (args.isWorld)
            {
                _transform.position = ReverseDirection(_transform.position);
            }
            else
            {
                _transform.localPosition = ReverseDirection(_transform.localPosition);
            }
        }

        //Move to application:
        private IEnumerator tweenMove()
        {
            Vector3 end;

            //define targets:
            if (args.isBy)
            {
                if (args.isWorld)
                {
                    end = new Vector3(_transform.position.x + args.xr, _transform.position.y + args.yg, _transform.position.z + args.zb);
                }
                else
                {
                    end = new Vector3(_transform.localPosition.x + args.xr, _transform.localPosition.y + args.yg, _transform.localPosition.z + args.zb);
                }
            }
            else
            {
                //can not do this is we are movingBy, because I need the non-moving axis to be 0.
                if (args.isWorld)
                {
                    DefaultArgsToStartingPosition(_transform.position);
                }
                else
                {
                    DefaultArgsToStartingPosition(_transform.localPosition);
                }

                if (args.isReverse)
                {
                    setMoveFrom();
                }

                end = new Vector3(args.xr, args.yg, args.zb);
            }

            //run tween
            //don't force a divide by zero, just set it to the end value
            Vector3 startingPosition = args.isWorld ? _transform.position : _transform.localPosition;
            if (args.time > 0)
            {
                easingFunction easing = getEasingFunction(args.transition);
                
                float curTime = 0.0f;
                while ((curTime < 1) && (!_stopTween))
                {
                    Vector3 newVector;
                    newVector.x = easing(startingPosition.x, end.x, curTime);
                    newVector.y = easing(startingPosition.y, end.y, curTime);
                    newVector.z = easing(startingPosition.z, end.z, curTime);

                    //move
                    if (args.isWorld)
                    {
                        _transform.position = newVector;
                    }
                    else
                    {
                        _transform.localPosition = newVector;
                    }

                    yield return 0;
                    curTime += Time.deltaTime * (1 / args.time);
                }
            }

            if (!_stopTween)
            {
                //make sure we end up where we're supposed to.
                if (args.isWorld)
                {
                    _transform.position = end;
                }
                else
                {
                    _transform.localPosition = end;
                }

                DoCallback();

                //Loop?
                if (args.time > 0)
                {
                    switch (args.loopType)
                    {
                        case LoopType.none:
                            Destroy(this);
                            break;
                        case LoopType.loop:
                            //go back to the beginning
                            if (args.isWorld)
                            {
                                _transform.position = startingPosition;
                            }
                            else
                            {
                                _transform.localPosition = startingPosition;
                            }
                            StartCoroutine(Start());
                            break;
                        case LoopType.pingPong:
                            args.xr = startingPosition.x;
                            args.yg = startingPosition.y;
                            args.zb = startingPosition.z;
                            StartCoroutine(Start());
                            break;
                        default:
                            Destroy(this);
                            break;
                    }
                }
                else
                {
                    Destroy(this);
                }
            }
            else
            {
                //cleanup
                Destroy(this);
            }
        }

        //Bezier move to application - Thank you David Bardos
        private IEnumerator tweenMoveBezier()
        {

            //Add the starting position to the curve info.
            if (args.isWorld)
            {
                args.beziers.Insert(0, _transform.position);
            }
            else
            {
                args.beziers.Insert(0, _transform.localPosition);
            }

            //help to get midpoints and other helpful info
            List<BezierPointInfo> _beziers = ParseBeziers(args.beziers);
            int iNumPoints = _beziers.Count;

            //get the easing function
            easingFunction easing = getEasingFunction(args.transition);

            //now tween
            float curTime = 0.0f;
            while ((curTime < 1) && (!_stopTween))
            {
                //get the easing as a percentage... not * 100, of course
                float virtTimePart = easing(0, 1, curTime);

                //get the array position of the intermediate point that we want
                int iCurAxisPoint;
                if (virtTimePart <= 0)
                {
                    //first array position
                    iCurAxisPoint = 0;
                }
                else if (virtTimePart >= 1)
                {
                    //last array position
                    iCurAxisPoint = iNumPoints - 1;
                }
                else
                {
                    //the transition is > 0 and less than 1. get the position we're looking for.
                    iCurAxisPoint = (int)Mathf.Floor(iNumPoints * virtTimePart);
                }

                //we are getting how far past the current point we are.
                float timeFract = iNumPoints * virtTimePart - iCurAxisPoint;

                //get the point info that we are interested in dealing with.
                BezierPointInfo bpi = _beziers[iCurAxisPoint];

                //get the new vector... I love vector math!
                Vector3 newVector = bpi.starting + timeFract * (2 * (1 - timeFract) * (bpi.intermediate - bpi.starting) + timeFract * (bpi.end - bpi.starting));

                //orientToPath - cutting off outer ends of curve percentage to avoid lookAt jitters:
                if (!args.lookAt.HasValue && args.orientToPath && curTime < .98 && curTime > .02)
                {
                    _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(newVector - _transform.position, Vector3.up), Time.deltaTime * moveBezierDefaultLookAtSpeed);
                }

                //look at target
                if (args.lookAt.HasValue)
                {
                    _transform.LookAt(args.lookAt.Value);
                }

                //move object
                if (args.isWorld)
                {
                    _transform.position = newVector;
                }
                else
                {
                    _transform.localPosition = newVector;
                }

                //wait until next frame
                yield return 0;
                curTime += Time.deltaTime * (1 / args.time);
            }

            if (!_stopTween)
            {
                //get the object to it's final resting position
                if (args.isWorld)
                {
                    _transform.position = _beziers[_beziers.Count - 1].end;
                }
                else
                {
                    _transform.localPosition = _beziers[_beziers.Count - 1].end;
                }


                DoCallback();


                //Loop?
                if (args.time > 0)
                {
                    switch (args.loopType)
                    {
                        case LoopType.none:
                            Destroy(this);
                            break;
                        case LoopType.loop:
                            //go back to the beginning
                            if (args.isWorld)
                            {
                                _transform.position = args.beziers[0];
                            }
                            else
                            {
                                _transform.localPosition = args.beziers[0];
                            }
                            //get rid of the one we added at the beginning
                            args.beziers.RemoveAt(0);
                            StartCoroutine(Start());
                            break;
                        case LoopType.pingPong:
                            args.beziers.Reverse();

                            //get rid of the one we are now at
                            args.beziers.RemoveAt(0);
                            StartCoroutine(Start());
                            break;
                        default:
                            Destroy(this);
                            break;
                    }
                }
                else
                {
                    Destroy(this);
                }
            }
            else
            {
                Destroy(this);
            }

        }


   
        private void setScaleFrom()
        {
            _transform.localScale = ReverseDirection(_transform.localScale);
        }

        //Scale to application:
        private IEnumerator tweenScale()
        {
            Vector3 end;

            if (args.isBy)
            {
                if (args.isMultiply)
                {
                    float x = args.isXRSet ? args.xr : 1;
                    float y = args.isYGSet ? args.yg : 1;
                    float z = args.isZBSet ? args.zb : 1;
                    
                    end = new Vector3(_transform.localScale.x * x, _transform.localScale.y * y, _transform.localScale.z * z);
                }
                else
                {
                    end = new Vector3(_transform.localScale.x + args.xr, _transform.localScale.y + args.yg, _transform.localScale.z + args.zb);
                }
            }
            else
            {
                DefaultArgsToStartingPosition(_transform.localScale);

                if (args.isReverse)
                {
                    setScaleFrom();
                }

                //define targets:
                end = new Vector3(args.xr, args.yg, args.zb);
            }

            //run tween:
            //don't force a divide by zero, just set it to the end value
            Vector3 start = _transform.localScale;
            if (args.time > 0)
            {
                //define object:                
                easingFunction easing = getEasingFunction(args.transition);

                float curTime = 0.0f;
                while ((curTime < 1) && (!_stopTween))
                {
                    Vector3 newVector;
                    newVector.x = easing(start.x, end.x, curTime);
                    newVector.y = easing(start.y, end.y, curTime);
                    newVector.z = easing(start.z, end.z, curTime);

                    //move
                    _transform.localScale = newVector;

                    yield return 0;
                    curTime += Time.deltaTime * (1 / args.time);
                }
            }

            if (!_stopTween)
            {
                //make sure we end up where we're supposed to.
                _transform.localScale = end;

                DoCallback();

                //Loop?
                if (args.time > 0)
                {
                    switch (args.loopType)
                    {
                        case LoopType.none:
                            Destroy(this);
                            break;
                        case LoopType.loop:
                            //go back to the beginning
                            if (args.isWorld)
                            {
                                _transform.localScale = start;
                            }
                            else
                            {
                                _transform.localScale = start;
                            }
                            StartCoroutine(Start());
                            break;
                        case LoopType.pingPong:
                            args.xr = start.x;
                            args.yg = start.y;
                            args.zb = start.z;
                            StartCoroutine(Start());
                            break;
                        default:
                            Destroy(this);
                            break;
                    }
                }
                else
                {
                    Destroy(this);
                }
            }
            else
            {
                Destroy(this);
            }
        }


        private void setRotateFrom()
        {
            _transform.localRotation = Quaternion.Euler(ReverseDirection(_transform.localEulerAngles));
        }

        //Rotate application:
        private IEnumerator tweenRotate()
        {
            Vector3 end;
            Vector3 e = _transform.localEulerAngles;

            if (args.isBy)
            {
                if (args.isMultiply)
                {
                    end = new Vector3(360 * args.xr + e.x, 360 * args.yg + e.y, 360 * args.zb + e.z);
                }
                else
                {
                    end = new Vector3(args.xr + e.x, args.yg + e.y, args.zb + e.z);
                }
            }
            else
            {
                DefaultArgsToStartingPosition(_transform.localEulerAngles);

                if (args.isReverse)
                {
                    setRotateFrom();
                }

                end = new Vector3(clerp(e.x, args.xr, 1), clerp(e.y, args.yg, 1), clerp(e.z, args.zb, 1));
            }            

            //run tween:            
            Vector3 start = new Vector3(e.x, e.y, e.z);

            //don't force a divide by zero, just set it to the end value
            if (args.time > 0)
            {
                //define object:                
                easingFunction easing = getEasingFunction(args.transition);
                float curTime = 0;
                while ((curTime < 1) && (!_stopTween))
                {
                    float x = easing(start.x, end.x, curTime);
                    float y = easing(start.y, end.y, curTime);
                    float z = easing(start.z, end.z, curTime);

                    //move
                    _transform.localRotation = Quaternion.Euler(x, y, z);

                    yield return 0;
                    curTime += Time.deltaTime * (1 / args.time);
                }
            }

            if (!_stopTween)
            {
                //make sure we end up where we're supposed to.
                _transform.localRotation = Quaternion.Euler(end);

                DoCallback();

                //Loop?
                if (args.time > 0)
                {
                    switch (args.loopType)
                    {
                        case LoopType.none:
                            Destroy(this);
                            break;
                        case LoopType.loop:
                            //go back to the beginning                       
                            _transform.localRotation = Quaternion.Euler(start);

                            StartCoroutine(Start());
                            break;
                        case LoopType.pingPong:
                            args.xr = start.x;
                            args.yg = start.y;
                            args.zb = start.z;
                            StartCoroutine(Start());
                            break;
                        default:
                            Destroy(this);
                            break;
                    }
                }
                else
                {
                    Destroy(this);
                }
            }
            else
            {
                Destroy(this);
            }
        }



        private void setColorFrom()
        {
            //we need to swap 
            Color oldColor = guiTexture ? guiTexture.color : renderer.material.color;

            Color newColor = new Color(args.xr, args.yg, args.zb, oldColor.a);

            //now update the target's color
            if (guiTexture)
            {
                guiTexture.color = newColor;
            }
            else
            {
                renderer.material.color = newColor;
            }

            args.xr = oldColor.r;
            args.yg = oldColor.g;
            args.zb = oldColor.b;
        }

        //Color to application:
        private IEnumerator tweenColor()
        {
            if (args.isReverse)
            {
                setColorFrom();
            }

            //define targets:
            Color end = new Color(args.xr, args.yg, args.zb);

            Color start = _transform.guiTexture ?_transform.guiTexture.color :_transform.renderer.material.color;

            //run tween:
            //don't force a divide by zero, just set it to the end value
            if (args.time > 0)
            {
                easingFunction easing = getEasingFunction(args.transition);

                float curTime = 0.0f;
                while ((curTime < 1) && (!_stopTween))
                {

                    Color newColor = new Color(start.r, start.g, start.b, start.a);
                    newColor.r = easing(start.r, end.r, curTime);
                    newColor.g = easing(start.g, end.g, curTime);
                    newColor.b = easing(start.b, end.b, curTime);

                    //move, make sure the alpha is done seperately from the color.
                    if (_transform.guiTexture)
                    {
                        newColor.a = _transform.guiTexture.color.a;
                        _transform.guiTexture.color = newColor;
                    }
                    else
                    {
                        newColor.a = _transform.renderer.material.color.a;
                        _transform.renderer.material.color = newColor;
                    }

                    yield return 0;
                    curTime += Time.deltaTime * (1 / args.time);
                }
            }

            //make sure we end up where we're supposed to.
            if (_transform.guiTexture)
            {
                end.a = _transform.guiTexture.color.a;
                _transform.guiTexture.color = end;
                start.a = end.a;
            }
            else
            {
                end.a = _transform.renderer.material.color.a;
                _transform.renderer.material.color = end;
                start.a = end.a;
            }

            if (!_stopTween)
            {
                DoCallback();

                //Loop?
                if (args.time > 0)
                {
                    switch (args.loopType)
                    {
                        case LoopType.none:
                            Destroy(this);
                            break;
                        case LoopType.loop:
                            //go back to the beginning                       

                            if (_transform.guiTexture)
                            {
                                _transform.guiTexture.color = start;
                            }
                            else
                            {
                                _transform.renderer.material.color = start;
                            }

                            StartCoroutine(Start());
                            break;
                        case LoopType.pingPong:
                            args.xr = start.r;
                            args.yg = start.g;
                            args.zb = start.b;
                            StartCoroutine(Start());
                            break;
                        default:
                            Destroy(this);
                            break;
                    }
                }
                else
                {
                    Destroy(this);
                }
            }
            else
            {
                //cleanup
                Destroy(this);
            }
        }

        private void setAudioFrom()
        {
            AudioSource sound = args.audioSource;

            float destinationVolume = sound.volume;
            float destinationPitch = sound.pitch;

            sound.volume = args.volume;
            sound.pitch = args.pitch;

            args.volume = destinationVolume;
            args.pitch = destinationPitch;
        }

        //Audio to application:
        private IEnumerator tweenAudio()
        {
            //construct args:        
            if (args.audioSource == null)
            {
                args.audioSource = audio;
            }

            if (args.isReverse)
            {
                setAudioFrom();
            }

            AudioSource sound = args.audioSource;

            //define targets:
            float endV = args.volume;
            float endP = args.pitch;

            //run tween:
            //don't force a divide by zero, just set it to the end value
            if (args.time > 0)
            {
                //define start:            
                float startV = sound.volume;
                float startP = sound.pitch;

                easingFunction easing = getEasingFunction(args.transition);

                float curTime = 0.0f;
                while ((curTime < 1) && (!_stopTween))
                {
                    //move
                    sound.volume = easing(startV, endV, curTime);
                    sound.pitch = easing(startP, endP, curTime);

                    yield return 0;
                    curTime += Time.deltaTime * (1 / args.time);
                }
            }

            if (!_stopTween)
            {
                //make sure we get all the way there
                sound.volume = endV;
                sound.pitch = endP;

                DoCallback();
            }
            //cleanup
            Destroy(this);
        }



        //Punch Position application:
        private IEnumerator tweenPunchPosition()
        {
            //run tween:
            //don't force a divide by zero, just set it to the end value
            if (args.time > 0)
            {
                Vector3 startPosition = _transform.localPosition;
                float curTime = 0.0f;
                while ((curTime < 1) && (!_stopTween))
                {
                    Vector3 newVector;

                    newVector.x = punch(args.xr, curTime) + startPosition.x;
                    newVector.y = punch(args.yg, curTime) + startPosition.y;
                    newVector.z = punch(args.zb, curTime) + startPosition.z;

                    //move
                    _transform.localPosition = newVector;

                    yield return 0;
                    curTime += Time.deltaTime * (1 / args.time);
                }


                //put us where we are supposed to be
                //NOTE: Since in the case where there is no time alotment, there can be no movement because the start is the end
                _transform.localPosition = startPosition;
            }

            if (!_stopTween)
            {
                DoCallback();
            }

            //cleanup
            Destroy(this);
        }

        //Punch rotation application:
        private IEnumerator tweenPunchRotation()
        {
            //run tween:
            //don't force a divide by zero, just set it to the end value
            if (args.time > 0)
            {
                //The args were number of rotations
                args.xr = args.xr * 360;
                args.yg = args.yg * 360;
                args.zb = args.zb * 360;

                //define object:
                Vector3 pos = _transform.localEulerAngles;

                float curTime = 0.0f;
                while ((curTime < 1) && (!_stopTween))
                {
                    Vector3 posAug;
                    posAug.x = punch(args.xr, curTime) + pos.x;
                    posAug.y = punch(args.yg, curTime) + pos.y;
                    posAug.z = punch(args.zb, curTime) + pos.z;

                    //move
                    _transform.localRotation = Quaternion.Euler(posAug.x, posAug.y, posAug.z);

                    yield return 0;
                    curTime += Time.deltaTime * (1 / args.time);
                }


                //Make sure we end up where we are supposed to
                //NOTE: Since in the case where there is no time alotment, there can be no movement because the start is the end
                _transform.localRotation = Quaternion.Euler(pos.x, pos.y, pos.z);
            }

            if (!_stopTween)
            {
                DoCallback();
            }

            //cleanup
            Destroy(this);
        }


        //Shake application:
        private IEnumerator tweenShake()
        {
            //run tween:
            //don't force a divide by zero, just set it to the end value
            if (args.time > 0)
            {
                //define targets:
                Vector3 shakeMagnitude = new Vector3(args.xr, args.yg, args.zb);
                Vector3 startingPosition = _transform.localPosition;

                //run tween:
                float curTime = 0.0f;
                while ((curTime < 1) && (!_stopTween))
                {
                    Vector3 newVector;
                    //if it is the first iteration, we make the impact, otherwize, we make it random
                    if (curTime > 0)
                    {
                        newVector.x = startingPosition.x + Random.Range(-shakeMagnitude.x, shakeMagnitude.x);
                        newVector.y = startingPosition.y + Random.Range(-shakeMagnitude.y, shakeMagnitude.y);
                        newVector.z = startingPosition.z + Random.Range(-shakeMagnitude.z, shakeMagnitude.z);
                    }
                    else
                    {
                        newVector.x = startingPosition.x + shakeMagnitude.x;
                        newVector.y = startingPosition.y + shakeMagnitude.y;
                        newVector.z = startingPosition.z + shakeMagnitude.z;
                    }

                    //move
                    _transform.localPosition = newVector;

                    yield return 0;

                    curTime += Time.deltaTime * (1 / args.time);

                    //setup for next iteration, reduce the maximum displacement linearly by time.
                    shakeMagnitude.x = args.xr - (curTime * args.xr);
                    shakeMagnitude.y = args.yg - (curTime * args.yg);
                    shakeMagnitude.z = args.zb - (curTime * args.zb);
                }

                //Make sure we end up where we are supposed to
                //NOTE: Since in the case where there is no time alotment, there can be no movement because the start is the end
                _transform.localPosition = startingPosition;
            }

            if (!_stopTween)
            {
                DoCallback();
            }
            //cleanup
            Destroy(this);
        }

        //Stab application:
        private IEnumerator tweenStab()
        {
            //make sure we have audio to play!
            if (audio == null)
            {
                gameObject.AddComponent("AudioSource");
                audio.playOnAwake = false;
            }

            //construct args:       
            if (args.clip == null)
            {
                args.clip = gameObject.audio.clip;
            }

            //target:
            AudioSource obj = gameObject.audio;
            obj.clip = args.clip;
            obj.volume = args.volume;
            obj.pitch = args.pitch;

            //move
            obj.PlayOneShot(obj.clip);

            //wait until the clip is played
            yield return new WaitForSeconds(obj.clip.length / args.pitch);

            if (!_stopTween)
            {
                DoCallback();
            }

            //cleanup
            Destroy(this);
        }

        #endregion


        #region private static helper functions

        //Registers and preps for static to component swap:
        private static void init(GameObject target, Arguments args)
        {
            iTween obj = (iTween)target.AddComponent(typeof(iTween));
            obj.args = args;
        }

        #endregion




        #region static interface functions
        /// <summary>
        /// Stops (and removes) tweening on an object:
        /// </summary>
        /// <param name="obj">The GameObject that you want to stop all tweening on</param>
        public static void stop(GameObject obj)
        {
            Component[] scripts = obj.GetComponents(typeof(iTween));
            foreach (iTween tween in scripts)
            {
                tween.StopTween();
            }
        }

        /// <summary>
        /// Stops (and removes) tweening of a certain type on an object derived from the root of the type (i.e. "moveTo" = "move").
        /// Any move, rotate, fade, color or scale will match any of the same type.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        public static void stopType(GameObject obj, FunctionType type)
        {
            Component[] scripts = obj.GetComponents(typeof(iTween));

            foreach (iTween tween in scripts)
            {
                if (IsTweenSameType(type, tween.args.type))
                {
                    tween.StopTween();
                }
            }
        }


        /// <summary>
        /// Finds the number of iTween scripts attached to the gameobject
        /// </summary>
        /// <param name="obj">The GameObject that you wish to know the count of iTween scripts</param>
        /// <returns>The number of iTween scripts attached to the given GameObject</returns>
        public static int tweenCount(GameObject obj)
        {
            return obj.GetComponents(typeof(iTween)).Length;
        }


        #region audioFrom
        /// <summary>
        /// Will change the audio of the object to the volume, pitch and audio source specified
        /// The audio will be transitioned with the current value of audioDefaultTransition
        /// The audiosource of the game object (target) will be used.
        /// </summary>
        /// <param name="target">The GameObject that will have the audio changed</param>
        /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
        /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
        /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
        public static void audioFrom(GameObject target, float? time, float? delay, float? volume, float? pitch)
        {
            audioFrom(target, time, delay, volume, pitch, null, null, null, null);
        }

        /// <summary>
        /// Will change the audio of the object to the volume, pitch and audio source specified
        /// The audio will be transitioned with the current value of audioDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will have the audio changed</param>
        /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
        /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
        /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
        /// <param name="audioSource">The audio source object that will be used for playing the audio. If null, the audio propery of the target will be used.</param>
        public static void audioFrom(GameObject target, float? time, float? delay, float? volume, float? pitch, AudioSource audioSource)
        {
            audioFrom(target, time, delay, volume, pitch, audioSource, null, null, null);
        }

        /// <summary>
        /// Will change the audio of the object to the volume, pitch and audio source specified
        /// </summary>
        /// <param name="target">The GameObject that will have the audio changed</param>
        /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
        /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
        /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
        /// <param name="audioSource">The audio source object that will be used for playing the audio. If null, the audio propery of the target will be used.</param>
        /// <param name="transition">The method in which you wish it to transition the audio. If null, it will use audioDefaultTransition</param>
        public static void audioFrom(GameObject target, float? time, float? delay, float? volume, float? pitch, AudioSource audioSource, EasingType? transition)
        {
            audioFrom(target, time, delay, volume, pitch, audioSource, transition, null, null);
        }

        /// <summary>
        /// Will change the audio of the object to the volume, pitch and audio source specified
        /// </summary>
        /// <param name="target">The GameObject that will have the audio changed</param>
        /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
        /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
        /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
        /// <param name="audioSource">The audio source object that will be used for playing the audio. If null, the audio propery of the target will be used.</param>
        /// <param name="transition">The method in which you wish it to transition the audio. If null, it will use audioDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void audioFrom(GameObject target, float? time, float? delay, float? volume, float? pitch, AudioSource audioSource, EasingType? transition, string onComplete, object oncomplete_params)
        {
            audioFrom(target, time, delay, volume, pitch, audioSource, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will change the audio of the object to the volume, pitch and audio source specified
        /// </summary>
        /// <param name="target">The GameObject that will have the audio changed</param>
        /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
        /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
        /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
        /// <param name="audioSource">The audio source object that will be used for playing the audio. If null, the audio propery of the target will be used.</param>
        /// <param name="transition">The method in which you wish it to transition the audio. If null, it will use audioDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void audioFrom(GameObject target, float? time, float? delay, float? volume, float? pitch, AudioSource audioSource, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            init(target, new Arguments(time, audioDefaultTime, delay, audioDefaultDelay, transition, audioDefaultTransition, onComplete, oncomplete_params,
                     FunctionType.audio, true, volume, audioDefaultVolume, pitch, audioDefaultPitch, null, audioSource, oncomplete_target));
        }

        /// <summary>
        /// Will change the audio of the object to the volume, pitch and audio source specified
        /// The audiosource of the game object (target) will be used.
        /// </summary>
        /// <param name="target">The GameObject that will have the audio changed</param>
        /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
        /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
        /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
        /// <param name="transition">The method in which you wish it to transition the audio. If null, it will use audioDefaultTransition</param>
        public static void audioFrom(GameObject target, float? time, float? delay, float? volume, float? pitch, EasingType? transition)
        {
            audioFrom(target, time, delay, volume, pitch, null, transition, null, null);
        }

        #endregion

        #region audioTo
        /// <summary>
        /// Will change the audio of the object to the volume, pitch and audio source specified
        /// The audio will be transitioned with the current value of audioDefaultTransition
        /// The audiosource of the game object (target) will be used.
        /// </summary>
        /// <param name="target">The GameObject that will have the audio changed</param>
        /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
        /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
        /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
        public static void audioTo(GameObject target, float? time, float? delay, float? volume, float? pitch)
        {
            audioTo(target, time, delay, volume, pitch, null, null, null, null);
        }

        /// <summary>
        /// Will change the audio of the object to the volume, pitch and audio source specified
        /// The audio will be transitioned with the current value of audioDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will have the audio changed</param>
        /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
        /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
        /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
        /// <param name="audioSource">The audio source object that will be used for playing the audio. If null, the audio propery of the target will be used.</param>
        public static void audioTo(GameObject target, float? time, float? delay, float? volume, float? pitch, AudioSource audioSource)
        {
            audioTo(target, time, delay, volume, pitch, audioSource, null, null, null);
        }

        /// <summary>
        /// Will change the audio of the object to the volume, pitch and audio source specified
        /// </summary>
        /// <param name="target">The GameObject that will have the audio changed</param>
        /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
        /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
        /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
        /// <param name="audioSource">The audio source object that will be used for playing the audio. If null, the audio propery of the target will be used.</param>
        /// <param name="transition">The method in which you wish it to transition the audio. If null, it will use audioDefaultTransition</param>
        public static void audioTo(GameObject target, float? time, float? delay, float? volume, float? pitch, AudioSource audioSource, EasingType? transition)
        {
            audioTo(target, time, delay, volume, pitch, audioSource, transition, null, null);
        }

        /// <summary>
        /// Will change the audio of the object to the volume, pitch and audio source specified
        /// </summary>
        /// <param name="target">The GameObject that will have the audio changed</param>
        /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
        /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
        /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
        /// <param name="audioSource">The audio source object that will be used for playing the audio. If null, the audio propery of the target will be used.</param>
        /// <param name="transition">The method in which you wish it to transition the audio. If null, it will use audioDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void audioTo(GameObject target, float? time, float? delay, float? volume, float? pitch, AudioSource audioSource, EasingType? transition, string onComplete, object oncomplete_params)
        {
            audioTo(target, time, delay, volume, pitch, audioSource, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will change the audio of the object to the volume, pitch and audio source specified
        /// </summary>
        /// <param name="target">The GameObject that will have the audio changed</param>
        /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
        /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
        /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
        /// <param name="audioSource">The audio source object that will be used for playing the audio. If null, the audio propery of the target will be used.</param>
        /// <param name="transition">The method in which you wish it to transition the audio. If null, it will use audioDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void audioTo(GameObject target, float? time, float? delay, float? volume, float? pitch, AudioSource audioSource, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            init(target, new Arguments(time, audioDefaultTime, delay, audioDefaultDelay, transition, audioDefaultTransition, onComplete, oncomplete_params,
                FunctionType.audio, false, volume, audioDefaultVolume, pitch, audioDefaultPitch, null, audioSource, oncomplete_target));
        }

        /// <summary>
        /// Will change the audio of the object to the volume, pitch and audio source specified
        /// The audiosource of the game object (target) will be used.
        /// </summary>
        /// <param name="target">The GameObject that will have the audio changed</param>
        /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
        /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
        /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
        /// <param name="transition">The method in which you wish it to transition the audio. If null, it will use audioDefaultTransition</param>
        public static void audioTo(GameObject target, float? time, float? delay, float? volume, float? pitch, EasingType? transition)
        {
            audioTo(target, time, delay, volume, pitch, null, transition, null, null);
        }
        #endregion

        #region colorFrom
        /// <summary>
        /// Will change the color of the object to the color specified and transition to the starting color at tween time
        /// The color will be transitioned with the current value of colorDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="color">The color that the object will transition from back to the starting color.</param>
        public static void colorFrom(GameObject target, float? time, float? delay, Color color)
        {
            colorFrom(target, time, delay, color.r, color.g, color.b, null, null, null);
        }

        /// <summary>
        /// Will change the color of the object to the color specified and transition to the starting color at tween time
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="color">The color that the object will transition from back to the starting color.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        public static void colorFrom(GameObject target, float? time, float? delay, Color color, EasingType? transition)
        {
            colorFrom(target, time, delay, color.r, color.g, color.b, transition, null, null);
        }

        /// <summary>
        /// Will change the color of the object to the color specified and transition to the starting color at tween time
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="color">The color that the object will transition from back to the starting color.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void colorFrom(GameObject target, float? time, float? delay, Color color, EasingType? transition, string onComplete, object oncomplete_params)
        {
            colorFrom(target, time, delay, color.r, color.g, color.b, transition, onComplete, oncomplete_params);
        }

        /// <summary>
        /// Will change the color of the object to the color specified and transition to the starting color at tween time
        /// The coloring will be transitioned with the current value of colorDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="g">The green value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="b">The blue value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        public static void colorFrom(GameObject target, float? time, float? delay, float? r, float? g, float? b)
        {
            colorFrom(target, time, delay, r, g, b, null, null, null);
        }

        /// <summary>
        /// Will change the color of the object to the color specified and transition to the starting color at tween time
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="g">The green value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="b">The blue value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        public static void colorFrom(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition)
        {
            colorFrom(target, time, delay, r, g, b, transition, null, null);
        }

        /// <summary>
        /// Will change the color of the object to the color specified and transition to the starting color at tween time
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="g">The green value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="b">The blue value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void colorFrom(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition, string onComplete, object oncomplete_params)
        {
            colorFrom(target, time, delay, r, g, b, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will change the color of the object to the color specified and transition to the starting color at tween time
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="g">The green value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="b">The blue value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void colorFrom(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            colorFrom(target, time, delay, r, g, b, transition, onComplete, oncomplete_params, oncomplete_target, true);
        }

        /// <summary>
        /// Will change the color of the object to the color specified and transition to the starting color at tween time
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="g">The green value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="b">The blue value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// /// <param name="includeChildren">If true (default), all children will be colored in the same fashion. Note that the callback will only be done for the top level tween.</param>
        public static void colorFrom(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, bool includeChildren)
        {
            init(target, new Arguments(time, colorDefaultTime, delay, colorDefaultDelay, transition, colorDefaultTransition, onComplete, oncomplete_params,
               FunctionType.color, true, r, g, b, false, false, false, includeChildren, oncomplete_target, LoopType.none));
        }

        #endregion

        #region colorTo
        /// <summary>
        /// Will change the color of the object to the color specified by the color parameter
        /// The color will be transitioned with the current value of colorDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="color">The color that the object will transition to.</param>
        public static void colorTo(GameObject target, float? time, float? delay, Color color)
        {
            colorTo(target, time, delay, color.r, color.g, color.b, null, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the color parameter
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="color">The color that the object will transition to.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        public static void colorTo(GameObject target, float? time, float? delay, Color color, EasingType? transition)
        {
            colorTo(target, time, delay, color.r, color.g, color.b, transition, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the color parameter
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="color">The color that the object will transition to.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void colorTo(GameObject target, float? time, float? delay, Color color, EasingType? transition, string onComplete, object oncomplete_params)
        {
            colorTo(target, time, delay, color.r, color.g, color.b, transition, onComplete, oncomplete_params, null, LoopType.none);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the color parameter
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="color">The color that the object will transition to.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void colorTo(GameObject target, float? time, float? delay, Color color, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            colorTo(target, time, delay, color.r, color.g, color.b, transition, onComplete, oncomplete_params, oncomplete_target, LoopType.none);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the r,g and b parameters
        /// The coloring will be transitioned with the current value of colorDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color to. If null, 0 will be used for the value</param>
        /// <param name="g">The green value of the rgb color to change the color to. If null, 0 will be used for the value</param>
        /// <param name="b">The blue value of the rgb color to change the color to. If null, 0 will be used for the value</param>
        public static void colorTo(GameObject target, float? time, float? delay, float? r, float? g, float? b)
        {
            colorTo(target, time, delay, r, g, b, null, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the r,g and b parameters
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color to. If null, 0 will be used for the value</param>
        /// <param name="g">The green value of the rgb color to change the color to. If null, 0 will be used for the value</param>
        /// <param name="b">The blue value of the rgb color to change the color to. If null, 0 will be used for the value</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        public static void colorTo(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition)
        {
            colorTo(target, time, delay, r, g, b, transition, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the r,g and b parameters
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="g">The green value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="b">The blue value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void colorTo(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition, string onComplete, object oncomplete_params)
        {
            colorTo(target, time, delay, r, g, b, transition, onComplete, oncomplete_params, null, LoopType.none);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the r,g and b parameters
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="g">The green value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="b">The blue value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void colorTo(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            colorTo(target, time, delay, r, g, b, transition, onComplete, oncomplete_params, oncomplete_target, LoopType.none);            
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the color parameter
        /// The color will be transitioned with the current value of colorDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="color">The color that the object will transition to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void colorTo(GameObject target, float? time, float? delay, Color color, LoopType loopType)
        {
            colorTo(target, time, delay, color.r, color.g, color.b, null, null, null, null, loopType);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the color parameter
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="color">The color that the object will transition to.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void colorTo(GameObject target, float? time, float? delay, Color color, EasingType? transition, LoopType loopType)
        {
            colorTo(target, time, delay, color.r, color.g, color.b, transition, null, null, null, loopType);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the color parameter
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="color">The color that the object will transition to.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void colorTo(GameObject target, float? time, float? delay, Color color, EasingType? transition, string onComplete, object oncomplete_params, LoopType loopType)
        {
            colorTo(target, time, delay, color.r, color.g, color.b, transition, onComplete, oncomplete_params, null, loopType);
        }


        /// <summary>
        /// Will change the color of the object to the color specified by the color parameter
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="color">The color that the object will transition to.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void colorTo(GameObject target, float? time, float? delay, Color color, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType)
        {
            colorTo(target, time, delay, color.r, color.g, color.b, transition, onComplete, oncomplete_params, oncomplete_target, loopType, true);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the color parameter
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="color">The color that the object will transition to.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="includeChildren">If true (default), all children will be colored in the same fashion. Note that the callback will only be done for the top level tween.</param>
        public static void colorTo(GameObject target, float? time, float? delay, Color color, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, bool includeChildren)
        {
            colorTo(target, time, delay, color.r, color.g, color.b, transition, onComplete, oncomplete_params, oncomplete_target, loopType, includeChildren);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the r,g and b parameters
        /// The coloring will be transitioned with the current value of colorDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color to. If null, 0 will be used for the value</param>
        /// <param name="g">The green value of the rgb color to change the color to. If null, 0 will be used for the value</param>
        /// <param name="b">The blue value of the rgb color to change the color to. If null, 0 will be used for the value</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void colorTo(GameObject target, float? time, float? delay, float? r, float? g, float? b, LoopType loopType)
        {
            colorTo(target, time, delay, r, g, b, null, null, null, loopType);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the r,g and b parameters
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color to. If null, 0 will be used for the value</param>
        /// <param name="g">The green value of the rgb color to change the color to. If null, 0 will be used for the value</param>
        /// <param name="b">The blue value of the rgb color to change the color to. If null, 0 will be used for the value</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void colorTo(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition, LoopType loopType)
        {
            colorTo(target, time, delay, r, g, b, transition, null, null, null, loopType);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the r,g and b parameters
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="g">The green value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="b">The blue value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void colorTo(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition, string onComplete, object oncomplete_params, LoopType loopType)
        {
            colorTo(target, time, delay, r, g, b, transition, onComplete, oncomplete_params, null, loopType);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the r,g and b parameters
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="g">The green value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="b">The blue value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void colorTo(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType)
        {
            colorTo(target, time, delay, r, g, b, transition, onComplete, oncomplete_params, oncomplete_target, loopType, true);
        }

        /// <summary>
        /// Will change the color of the object to the color specified by the r,g and b parameters
        /// </summary>
        /// <param name="target">The GameObject that will be colored</param>
        /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
        /// <param name="r">The red value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="g">The green value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="b">The blue value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
        /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// /// <param name="includeChildren">If true (default), all children will be colored in the same fashion. Note that the callback will only be done for the top level tween.</param>
        public static void colorTo(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, bool includeChildren)
        {
            init(target, new Arguments(time, colorDefaultTime, delay, colorDefaultDelay, transition, colorDefaultTransition, onComplete, oncomplete_params,
               FunctionType.color, false, r, g, b, false, false, false, includeChildren, oncomplete_target, loopType));
        }
        #endregion

        #region fadeFrom
        /// <summary>
        /// Will fade from that alpha to the current alpha of the given object. 
        /// It will transition using the current value of fadeDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade from. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade from to the current color. If null, it will fade from 0.</param>
        public static void fadeFrom(GameObject target, float? time, float? delay, float? alpha)
        {
            fadeFrom(target, time, delay, alpha, null, null, null);
        }

        /// <summary>
        /// Will fade from that alpha to the current alpha of the given object
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade from. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade from to the current color. If null, it will fade from 0.</param>
        /// <param name="transition">The method in which you wish it to fade. If null, it will use fadeDefaultTransition</param>
        public static void fadeFrom(GameObject target, float? time, float? delay, float? alpha, EasingType? transition)
        {
            fadeFrom(target, time, delay, alpha, transition, null, null);
        }

        /// <summary>
        /// Will fade from the given alpha to the current alpha of the given object
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade from. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade from to the current color. If null, it will fade from 0.</param>
        /// <param name="transition">The method in which you wish it to fade. If null, it will use fadeDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void fadeFrom(GameObject target, float? time, float? delay, float? alpha, EasingType? transition, string onComplete, object oncomplete_params)
        {
            fadeFrom(target, time, delay, alpha, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will fade from the given alpha to the current alpha of the given object
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade from. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade from to the current color. If null, it will fade from 0.</param>
        /// <param name="transition">The method in which you wish it to fade. If null, it will use fadeDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void fadeFrom(GameObject target, float? time, float? delay, float? alpha, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            fadeFrom(target, time, delay, alpha, transition, onComplete, oncomplete_params, oncomplete_target, true);
        }

        /// <summary>
        /// Will fade from the given alpha to the current alpha of the given object
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade from. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade from to the current color. If null, it will fade from 0.</param>
        /// <param name="transition">The method in which you wish it to fade. If null, it will use fadeDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="includeChildren">If true (default), all children will be faded in the same fashion. Note that the callback will only be done for the top level tween.</param>
        public static void fadeFrom(GameObject target, float? time, float? delay, float? alpha, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, bool includeChildren)
        {
            init(target, new Arguments(time, fadeDefaultTime, delay, fadeDefaultDelay, transition, fadeDefaultTransition, onComplete, oncomplete_params,
                FunctionType.fade, true, alpha, 0, includeChildren, oncomplete_target, LoopType.none));
        }

        #endregion

        #region fadeTo
        /// <summary>
        /// Will take the current color and fade it to the given alpha channel.
        /// The transition will be defaulted to the current value of fadeDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade to. If null, it will fade to 0.</param>
        public static void fadeTo(GameObject target, float? time, float? delay, float? alpha)
        {
            fadeTo(target, time, delay, alpha, null, null, null);
        }

        /// <summary>
        /// Will take the current color and fade it to the given alpha channel 
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade to. If null, it will fade to 0.</param>
        /// <param name="transition">The method in which you wish it to fade. If null, it will se fadeDefaultTransition</param>    
        public static void fadeTo(GameObject target, float? time, float? delay, float? alpha, EasingType? transition)
        {
            fadeTo(target, time, delay, alpha, transition, null, null);
        }
        /// <summary>
        /// Will take the current color and fade it to the given alpha channel 
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade to. If null, it will fade to 0.</param>
        /// <param name="transition">The method in which you wish it to fade. If null, it will use fadeDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void fadeTo(GameObject target, float? time, float? delay, float? alpha, EasingType? transition, string onComplete, object oncomplete_params)
        {
            fadeTo(target, time, delay, alpha, transition, onComplete, oncomplete_params, null, LoopType.none);
        }

        /// <summary>
        /// Will take the current color and fade it to the given alpha channel 
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade to. If null, it will fade to 0.</param>
        /// <param name="transition">The method in which you wish it to fade. If null, it will use fadeDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void fadeTo(GameObject target, float? time, float? delay, float? alpha, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            fadeTo(target, time, delay, alpha, transition, onComplete, oncomplete_params, oncomplete_target, LoopType.none);         
        }

        /// <summary>
        /// Will take the current color and fade it to the given alpha channel 
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade to. If null, it will fade to 0.</param>
        /// <param name="transition">The method in which you wish it to fade. If null, it will se fadeDefaultTransition</param>    
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void fadeTo(GameObject target, float? time, float? delay, float? alpha, EasingType? transition, LoopType loopType)
        {
            fadeTo(target, time, delay, alpha, transition, null, null, loopType);
        }

        /// <summary>
        /// Will take the current color and fade it to the given alpha channel 
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade to. If null, it will fade to 0.</param>
        /// <param name="transition">The method in which you wish it to fade. If null, it will use fadeDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void fadeTo(GameObject target, float? time, float? delay, float? alpha, EasingType? transition, string onComplete, object oncomplete_params, LoopType loopType )
        {
            fadeTo(target, time, delay, alpha, transition, onComplete, oncomplete_params, null, loopType);
        }

        /// <summary>
        /// Will take the current color and fade it to the given alpha channel 
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade to. If null, it will fade to 0.</param>
        /// <param name="transition">The method in which you wish it to fade. If null, it will use fadeDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void fadeTo(GameObject target, float? time, float? delay, float? alpha, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType)
        {
            fadeTo(target, time, delay, alpha, transition, onComplete, oncomplete_params, oncomplete_target, loopType, true);
        }

        /// <summary>
        /// Will take the current color and fade it to the given alpha channel 
        /// </summary>
        /// <param name="target">The GameObject that will be faded</param>
        /// <param name="time">The length of time it will take to do the fade. If null, it will use the fadeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
        /// <param name="alpha">The alpha that you wish to fade to. If null, it will fade to 0.</param>
        /// <param name="transition">The method in which you wish it to fade. If null, it will use fadeDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="includeChildren">If true (default), all children will be faded in the same fashion. Note that the callback will only be done for the top level tween.</param>
        public static void fadeTo(GameObject target, float? time, float? delay, float? alpha, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, bool includeChildren)
        {
            init(target, new Arguments(time, fadeDefaultTime, delay, fadeDefaultDelay, transition, fadeDefaultTransition, onComplete, oncomplete_params,
                FunctionType.fade, false, alpha, 0, includeChildren, oncomplete_target, loopType));
        }

        #endregion

        #region moveBy
        /// <summary>
        /// Will move the object on each axis the distance specified
        /// If any of x, y, or z are null then the value would be 0.
        /// The transition will be done with the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
        public static void moveBy(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            moveBy(target, time, delay, x, y, z, null, null, null);
        }

        /// <summary>
        /// Will move the object on each axis the distance specified
        /// If any of x, y, or z are null then the value would be 0.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        public static void moveBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            moveBy(target, time, delay, x, y, z, transition, null, null);
        }

        /// <summary>
        /// Will move the object on each axis the distance specified
        /// If any of x, y, or z are null then the value would be 0.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void moveBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            moveBy(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will move the object on each axis the distance specified
        /// If any of x, y, or z are null then the value would be 0.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void moveBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
               FunctionType.move, false, x, y, z, true, false, false, false, oncomplete_target, LoopType.none));
        }

        /// <summary>
        /// Will move by the given vector values. 
        /// It will transition using the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The distance to move. Don't make this null.</param>
        public static void moveBy(GameObject target, float? time, float? delay, Vector3 vector)
        {
            moveBy(target, time, delay, vector.x, vector.y, vector.z, null, null, null);
        }

        /// <summary>
        /// Will move by the given vector values. 
        /// It will transition using the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The distance to move. Don't make this null.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        public static void moveBy(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition)
        {
            moveBy(target, time, delay, vector.x, vector.y, vector.z, transition, null, null);
        }

        /// <summary>
        /// Will move by the given vector values. 
        /// It will transition using the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The distance to move. Don't make this null.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void moveBy(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params)
        {
            moveBy(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params, null);
        }

        #endregion 

  
        #region moveFrom
        /// <summary>
        /// Will move from the given coordinates to the current coordinates. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The transition will be done with the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move from on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move from on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move from on the z axis. If null, it will not move on the z axis.</param>
        public static void moveFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            moveFrom(target, time, delay, x, y, z, null, null, null);
        }

        /// <summary>
        /// Will move from the given coordinates to the current coordinates. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move from on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move from on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move from on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        public static void moveFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            moveFrom(target, time, delay, x, y, z, transition, null, null);
        }

        /// <summary>
        /// Will move from the given coordinates to the current coordinates. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move from on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move from on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move from on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void moveFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            moveFrom(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will move from the given coordinates to the current coordinates. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move from on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move from on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move from on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void moveFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
              FunctionType.move, true, x, y, z, false, false, false, false, oncomplete_target, LoopType.none));
        }

        /// <summary>
        /// Will  move from the given vector position to the current position. 
        /// It will transition using the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The position to move to. Don't make this null.</param>
        public static void moveFrom(GameObject target, float? time, float? delay, Vector3 vector)
        {
            moveFrom(target, time, delay, vector.x, vector.y, vector.z, null, null, null);
        }

        /// <summary>
        /// Will move from the given vector position to the current position.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The position to move to. Don't make this null.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        public static void moveFrom(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition)
        {
            moveFrom(target, time, delay, vector.x, vector.y, vector.z, transition, null, null);
        }

        /// <summary>
        /// Will move from the given vector position to the current position.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The position to move to. Don't make this null.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void moveFrom(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params)
        {
            moveFrom(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params);
        }

        #endregion

        #region moveFromWorld
        /// <summary>
        /// Will move from the given coordinates to the current coordinates using the gameobjects position property. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The transition will be done with the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move from on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move from on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move from on the z axis. If null, it will not move on the z axis.</param>
        public static void moveFromWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            moveFromWorld(target, time, delay, x, y, z, null, null, null);
        }

        /// <summary>
        /// Will move from the given coordinates to the current coordinates  using the gameobjects position property. . 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move from on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move from on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move from on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        public static void moveFromWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            moveFrom(target, time, delay, x, y, z, transition, null, null);
        }

        /// <summary>
        /// Will move from the given coordinates to the current coordinates using the game object's position property. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move from on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move from on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move from on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void moveFromWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            moveFromWorld(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will move from the given coordinates to the current coordinates using the game object's position property. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move from on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move from on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move from on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void moveFromWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
              FunctionType.move, true, x, y, z, false, true, false, false, oncomplete_target, LoopType.none));
        }

        /// <summary>
        /// Will move from the given vector position to the current position using the gameobjects position property. 
        /// It will transition using the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The position to move to. Don't make this null.</param>
        public static void moveFromWorld(GameObject target, float? time, float? delay, Vector3 vector)
        {
            moveFromWorld(target, time, delay, vector.x, vector.y, vector.z, null, null, null);
        }

        /// <summary>
        /// Will move from the given vector position to the current position using the gameobjects position property.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The position to move to. Don't make this null.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void moveFromWorld(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params)
        {
            moveFromWorld(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params);
        }

        #endregion

        #region moveTo
        /// <summary>
        /// Will move to the given coordinates. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The transition will be done with the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        public static void moveTo(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            moveTo(target, time, delay, x, y, z, null, null, null);
        }

        /// <summary>
        /// Will move to the given coordinates. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        public static void moveTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            moveTo(target, time, delay, x, y, z, transition, null, null);
        }

        /// <summary>
        /// Will move to the given coordinates. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void moveTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            moveTo(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will move to the given coordinates. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void moveTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            moveTo(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, oncomplete_target, LoopType.none);
        }

        /// <summary>
        /// Will  move to the given vector position. 
        /// It will transition using the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The position to move to. Don't make this null.</param>
        public static void moveTo(GameObject target, float? time, float? delay, Vector3 vector)
        {
            moveTo(target, time, delay, vector.x, vector.y, vector.z, null, null, null);
        }

        /// <summary>
        /// Will move to the given vector position.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The position to move to. Don't make this null.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        public static void moveTo(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition)
        {
            moveTo(target, time, delay, vector.x, vector.y, vector.z, transition, null, null);
        }

        /// <summary>
        /// Will move to the given vector position.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The position to move to. Don't make this null.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void moveTo(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params)
        {
            moveTo(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params);
        }

        /// <summary>
        /// Will move to the given coordinates. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The transition will be done with the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, LoopType loopType)
        {
            moveTo(target, time, delay, x, y, z, null, null, null, loopType);
        }

        /// <summary>
        /// Will move to the given coordinates. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, LoopType loopType)
        {
            moveTo(target, time, delay, x, y, z, transition, null, null, loopType);
        }

        /// <summary>
        /// Will move to the given coordinates. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, LoopType loopType)
        {
            moveTo(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null, loopType);
        }

        /// <summary>
        /// Will move to the given coordinates. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType)
        {
            init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
               FunctionType.move, false, x, y, z, false, false, false, false, oncomplete_target, loopType));
        }

        #endregion

        #region moveToBezier
        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, EasingType? transition, params Vector3[] points)
        {
            moveToBezier(target, time, delay, new List<Vector3>(points), transition, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, params Vector3[] points)
        {
            moveToBezier(target, time, delay, new List<Vector3>(points), transition, onComplete, oncomplete_params, oncomplete_target, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, EasingType? transition, string onComplete, object oncomplete_params, params Vector3[] points)
        {
            moveToBezier(target, time, delay, new List<Vector3>(points), transition, onComplete, oncomplete_params, null, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The transition will be done with the current value of moveBezierDefaultTransition.
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, List<Vector3> points)
        {
            moveToBezier(target, time, delay, points, null, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition)
        {
            moveToBezier(target, time, delay, points, transition, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params)
        {
            moveToBezier(target, time, delay, points, transition, onComplete, oncomplete_params, null, LoopType.none);            
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            moveToBezier(target, time, delay, points, transition, onComplete, oncomplete_params, oncomplete_target, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The transition will be done with the current value of moveBezierDefaultTransition.
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, params Vector3[] points)
        {
            moveToBezier(target, time, delay, new List<Vector3>(points), null, null, null, null, LoopType.none);            
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, EasingType? transition, LoopType loopType, params Vector3[] points)
        {
            moveToBezier(target, time, delay, new List<Vector3>(points), transition, null, null, null, loopType);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, params Vector3[] points)
        {
            moveToBezier(target, time, delay, new List<Vector3>(points), transition, onComplete, oncomplete_params, oncomplete_target, loopType);
        }


        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="orientToPath">If true, the object will be oriented to the path during the animation. If false, the object's orientation will remain the same through the animation</param>
        /// <param name="lookAt">If this value is set, orientToPath will be ignored and the object will constantly be oriented toward this vector during the animation.</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, bool orientToPath, Vector3? lookAt, params Vector3[] points)
        {
            moveToBezier(target, time, delay, new List<Vector3>(points), transition, onComplete, oncomplete_params, oncomplete_target, loopType, orientToPath, lookAt);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>        
        public static void moveToBezier(GameObject target, float? time, float? delay, EasingType? transition, string onComplete, object oncomplete_params, LoopType loopType, params Vector3[] points)
        {
            moveToBezier(target, time, delay, new List<Vector3>(points), transition, onComplete, oncomplete_params, null, loopType);            
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The transition will be done with the current value of moveBezierDefaultTransition.
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, LoopType loopType, List<Vector3> points)
        {
            moveToBezier(target, time, delay, points, null, null, null, null, loopType);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>   
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, LoopType loopType, List<Vector3> points, EasingType? transition)
        {
            moveToBezier(target, time, delay, points, transition, null, null, null, loopType);            
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params, LoopType loopType)
        {
            moveToBezier(target, time, delay, points, transition, onComplete, oncomplete_params, null, loopType);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType)
        {
            moveToBezier(target, time, delay, points, transition, onComplete, oncomplete_params, oncomplete_target, loopType, true, null);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="orientToPath">If true, the object will be oriented to the path during the animation. If false, the object's orientation will remain the same through the animation</param>
        /// <param name="lookAt">If this value is set, orientToPath will be ignored and the object will constantly be oriented toward this vector during the animation.</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, bool orientToPath, Vector3? lookAt)
        {
            init(target, new Arguments(time, moveBezierDefaultTime, delay, moveBezierDefaultDelay, transition, moveBezierDefaultTransition, onComplete, oncomplete_params,
               FunctionType.move, false, points, false, oncomplete_target, loopType, orientToPath, lookAt));
        }

        
        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
        /// The transition will be done with the current value of moveBezierDefaultTransition.
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        public static void moveToBezier(GameObject target, float? time, float? delay, LoopType loopType, params Vector3[] points)
        {
            moveToBezier(target, time, delay, new List<Vector3>(points), null, null, null);
        }
        #endregion

        #region moveToBezierWorld
        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, EasingType? transition, params Vector3[] points)
        {
            moveToBezierWorld(target, time, delay, new List<Vector3>(points), transition, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, params Vector3[] points)
        {
            moveToBezierWorld(target, time, delay, new List<Vector3>(points), transition, onComplete, oncomplete_params, oncomplete_target, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="orientToPath">If true, the object will be oriented to the path during the animation. If false, the object's orientation will remain the same through the animation</param>
        /// <param name="lookAt">If this value is set, orientToPath will be ignored and the object will constantly be oriented toward this vector during the animation.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, bool orientToPath, Vector3? lookAt, params Vector3[] points)
        {
            moveToBezierWorld(target, time, delay, new List<Vector3>(points), transition, onComplete, oncomplete_params, oncomplete_target, loopType, orientToPath, lookAt);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, EasingType? transition, string onComplete, object oncomplete_params, params Vector3[] points)
        {
            moveToBezierWorld(target, time, delay, new List<Vector3>(points), transition, onComplete, oncomplete_params, null, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The transition will be done with the current value of moveBezierDefaultTransition.
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, List<Vector3> points)
        {
            moveToBezierWorld(target, time, delay, points, null, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition)
        {
            moveToBezierWorld(target, time, delay, points, transition, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params)
        {
            moveToBezierWorld(target, time, delay, points, transition, onComplete, oncomplete_params, null, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            moveToBezierWorld(target, time, delay, points, transition, onComplete, oncomplete_params, oncomplete_target, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The transition will be done with the current value of moveBezierDefaultTransition.
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, params Vector3[] points)
        {
            moveToBezierWorld(target, time, delay, new List<Vector3>(points), null, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, EasingType? transition, LoopType loopType, params Vector3[] points)
        {
            moveToBezierWorld(target, time, delay, new List<Vector3>(points), transition, null, null, null, loopType);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, params Vector3[] points)
        {
            moveToBezierWorld(target, time, delay, new List<Vector3>(points), transition, onComplete, oncomplete_params, oncomplete_target, loopType);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, EasingType? transition, string onComplete, object oncomplete_params, LoopType loopType, params Vector3[] points)
        {
            moveToBezierWorld(target, time, delay, new List<Vector3>(points), transition, onComplete, oncomplete_params, null, loopType);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The transition will be done with the current value of moveBezierDefaultTransition.
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, LoopType loopType, List<Vector3> points)
        {
            moveToBezierWorld(target, time, delay, points, null, null, null, null, loopType);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, LoopType loopType, List<Vector3> points, EasingType? transition)
        {
            moveToBezierWorld(target, time, delay, points, transition, null, null, null, loopType);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params, LoopType loopType)
        {
            moveToBezierWorld(target, time, delay, points, transition, onComplete, oncomplete_params, null, loopType);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType)
        {
            moveToBezierWorld(target, time, delay, points, transition, onComplete, oncomplete_params, oncomplete_target, loopType, true, null);
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property.  
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
        /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="orientToPath">If true, the object will be oriented to the path during the animation. If false, the object's orientation will remain the same through the animation</param>
        /// <param name="lookAt">If this value is set, orientToPath will be ignored and the object will constantly be oriented toward this vector during the animation.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, bool orientToPath, Vector3? lookAt)
        {
            init(target, new Arguments(time, moveBezierDefaultTime, delay, moveBezierDefaultDelay, transition, moveBezierDefaultTransition, onComplete, oncomplete_params,
               FunctionType.move, false, points, false, oncomplete_target, loopType, orientToPath, lookAt));
        }

        /// <summary>
        /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property. 
        /// The transition will be done with the current value of moveBezierDefaultTransition.
        /// The object's orientation will change to align with the path.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        /// <param name="points">the vectors defining the points for the bezier curve. List one after another.</param>
        public static void moveToBezierWorld(GameObject target, float? time, float? delay, LoopType loopType, params Vector3[] points)
        {
            moveToBezierWorld(target, time, delay, new List<Vector3>(points), null, null, null);
        }

        #endregion

        #region moveToWorld
        /// <summary>
        /// Will move to the given coordinates using the gameObject's transform's position property. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The transition will be done with the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        public static void moveToWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            moveToWorld(target, time, delay, x, y, z, null, null, null);
        }

        /// <summary>
        /// Will move to the given coordinates using the gameObject's transform's position property. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        public static void moveToWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            moveToWorld(target, time, delay, x, y, z, transition, null, null);
        }

        /// <summary>
        /// Will move to the given coordinates using the gameObject's transform's position property. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void moveToWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            moveToWorld(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will move to the given coordinates using the gameObject's transform's position property. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void moveToWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            moveToWorld(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, oncomplete_target, LoopType.none);
        }

        /// <summary>
        /// Will  move to the given vector position. 
        /// It will transition using the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The position to move to. Don't make this null.</param>
        public static void moveToWorld(GameObject target, float? time, float? delay, Vector3 vector)
        {
            moveToWorld(target, time, delay, vector.x, vector.y, vector.z, null, null, null);
        }

        /// <summary>
        /// Will move to the given vector position.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The position to move to. Don't make this null.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        public static void moveToWorld(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition)
        {
            moveToWorld(target, time, delay, vector.x, vector.y, vector.z, transition, null, null);
        }

        /// <summary>
        /// Will move to the given vector position.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="vector">The position to move to. Don't make this null.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void moveToWorld(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params)
        {
            moveToWorld(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params);
        }

        /// <summary>
        /// Will move to the given coordinates using the gameObject's transform's position property. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The transition will be done with the current value of moveDefaultTransition.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveToWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, LoopType loopType)
        {
            moveToWorld(target, time, delay, x, y, z, null, null, null, loopType);
        }

        /// <summary>
        /// Will move to the given coordinates using the gameObject's transform's position property. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move to. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveToWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, LoopType loopType)
        {
            moveToWorld(target, time, delay, x, y, z, transition, null, null, loopType);
        }

        /// <summary>
        /// Will move to the given coordinates using the gameObject's transform's position property. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveToWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, LoopType loopType)
        {
            moveToWorld(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null, loopType);
        }

        /// <summary>
        /// Will move to the given coordinates using the gameObject's transform's position property. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be moved</param>
        /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
        /// <param name="x">The location to move to on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The location to move to on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The location to move to on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void moveToWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType)
        {
            init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
               FunctionType.move, false, x, y, z, false, true, false, false, oncomplete_target, loopType));
        }

        #endregion

        #region punchPosition
        /// <summary>
        /// Will punch the object's position with an amplitude per axis as specified by the x, y, and z parameters
        /// the object willuse a sine curve to determine the speed given the amplitude.
        /// The object will move and then return to it's starting position
        /// If any of x, y, or z are null then it will not move on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be punched</param>
        /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchPositionDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchPositionDefaultDelay</param>
        /// <param name="x">The amplitude to punch on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The amplitude to punch on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The amplitude to punch on the z axis. If null, it will not move on the z axis.</param>    
        public static void punchPosition(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            punchPosition(target, time, delay, x, y, z, null, null);
        }

        /// <summary>
        /// Will punch the object's position with an amplitude per axis as specified by the x, y, and z parameters
        /// the object will use a sine curve to determine the speed given the amplitude.
        /// The object will move and then return to it's starting position
        /// If any of x, y, or z are null then it will not move on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be punched</param>
        /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchPositionDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchPositionDefaultDelay</param>
        /// <param name="x">The amplitude to punch on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The amplitude to punch on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The amplitude to punch on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void punchPosition(GameObject target, float? time, float? delay, float? x, float? y, float? z, string onComplete, object oncomplete_params)
        {
            punchPosition(target, time, delay, x, y, z, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will punch the object's position with an amplitude per axis as specified by the x, y, and z parameters
        /// the object will use a sine curve to determine the speed given the amplitude.
        /// The object will move and then return to it's starting position
        /// If any of x, y, or z are null then it will not move on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be punched</param>
        /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchPositionDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchPositionDefaultDelay</param>
        /// <param name="x">The amplitude to punch on the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The amplitude to punch on the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The amplitude to punch on the z axis. If null, it will not move on the z axis.</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void punchPosition(GameObject target, float? time, float? delay, float? x, float? y, float? z, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            //NOTE: the easing type is set to any random value because it is not used
            init(target, new Arguments(time, punchPositionDefaultTime, delay, punchPositionDefaultDelay, EasingType.easeInExpo, EasingType.easeInExpo, onComplete, oncomplete_params,
              FunctionType.punchPosition, false, x, y, z, false, false, false, false, oncomplete_target, LoopType.none));
        }

        /// <summary>
        /// Will punch the object's position with an amplitude per axis as specified by the x, y, and z parameters
        /// the object willuse a sine curve to determine the speed given the amplitude.
        /// The object will move and then return to it's starting position
        /// If any of x, y, or z are null then it will not move on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be punched</param>
        /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchPositionDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchPositionDefaultDelay</param>
        /// <param name="vector">The vector specifying the amplitude of the punch along each axis. Don't make this null.</param>    
        public static void punchPosition(GameObject target, float? time, float? delay, Vector3 vector)
        {
            punchPosition(target, time, delay, vector.x, vector.y, vector.z, null, null);
        }

        /// <summary>
        /// Will punch the object's position with an amplitude per axis as specified by the x, y, and z parameters
        /// the object willuse a sine curve to determine the speed given the amplitude.
        /// The object will move and then return to it's starting position
        /// If any of x, y, or z are null then it will not move on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be punched</param>
        /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchPositionDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchPositionDefaultDelay</param>
        /// <param name="vector">The vector specifying the amplitude of the punch along each axis. Don't make this null.</param>    
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void punchPosition(GameObject target, float? time, float? delay, Vector3 vector, string onComplete, object oncomplete_params)
        {
            punchPosition(target, time, delay, vector.x, vector.y, vector.z, onComplete, oncomplete_params);
        }

        /// <summary>
        /// Will punch the object's position with an amplitude per axis as specified by the x, y, and z parameters
        /// the object willuse a sine curve to determine the speed given the amplitude.
        /// The object will move and then return to it's starting position
        /// If any of x, y, or z are null then it will not move on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be punched</param>
        /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchPositionDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchPositionDefaultDelay</param>
        /// <param name="vector">The vector specifying the amplitude of the punch along each axis. Don't make this null.</param>    
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void punchPosition(GameObject target, float? time, float? delay, Vector3 vector, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            punchPosition(target, time, delay, vector.x, vector.y, vector.z, onComplete, oncomplete_params, oncomplete_target);
        }
        #endregion

        #region punchRotation
        /// <summary>
        /// Will punch the object's rotation with an amplitude per axis as specified by the x, y, and z parameters
        /// the object will use a sine curve to determine the speed given the amplitude.
        /// The object will rotate and then return to it's starting position
        /// If any of x, y, or z are null then it will not rotate on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be punched</param>
        /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchRotationDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchRotationDefaultDelay</param>
        /// <param name="x">The amplitude to punch on the x axis (The number of rotations). If null, it will not rotate on the x axis.</param>
        /// <param name="y">The amplitude to punch on the y axis (The number of rotations). If null, it will not rotate on the y axis.</param>
        /// <param name="z">The amplitude to punch on the z axis (The number of rotations). If null, it will not rotate on the z axis.</param>
        public static void punchRotation(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            punchRotation(target, time, delay, x, y, z, null, null);
        }

        /// <summary>
        /// Will punch the object's rotation with an amplitude per axis as specified by the x, y, and z parameters
        /// the object will use a sine curve to determine the speed given the amplitude.
        /// The object will rotate and then return to it's starting position
        /// If any of x, y, or z are null then it will not rotate on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be punched</param>
        /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchRotationDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchRotationDefaultDelay</param>
        /// <param name="x">The amplitude to punch on the x axis (The number of rotations). If null, it will not rotate on the x axis.</param>
        /// <param name="y">The amplitude to punch on the y axis (The number of rotations). If null, it will not rotate on the y axis.</param>
        /// <param name="z">The amplitude to punch on the z axis (The number of rotations). If null, it will not rotate on the z axis.</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void punchRotation(GameObject target, float? time, float? delay, float? x, float? y, float? z, string onComplete, object oncomplete_params)
        {
            punchRotation(target, time, delay, x, y, z, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will punch the object's rotation with an amplitude per axis as specified by the x, y, and z parameters
        /// the object will use a sine curve to determine the speed given the amplitude.
        /// The object will rotate and then return to it's starting position
        /// If any of x, y, or z are null then it will not rotate on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be punched</param>
        /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchRotationDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchRotationDefaultDelay</param>
        /// <param name="x">The amplitude to punch on the x axis (The number of rotations). If null, it will not rotate on the x axis.</param>
        /// <param name="y">The amplitude to punch on the y axis (The number of rotations). If null, it will not rotate on the y axis.</param>
        /// <param name="z">The amplitude to punch on the z axis (The number of rotations). If null, it will not rotate on the z axis.</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void punchRotation(GameObject target, float? time, float? delay, float? x, float? y, float? z, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            //NOTE: the easing type is set to any random value because it is not used
            init(target, new Arguments(time, punchRotationDefaultTime, delay, punchRotationDefaultDelay, EasingType.easeInExpo, EasingType.easeInExpo, onComplete, oncomplete_params,
              FunctionType.punchRotation, false, x, y, z, false, false, false, false, oncomplete_target, LoopType.none));
        }

        /// <summary>
        /// Will punch the object's rotation with an amplitude per axis as specified by the x, y, and z parameters
        /// the object will use a sine curve to determine the speed given the amplitude.
        /// The object will rotate and then return to it's starting position
        /// If any of x, y, or z are null then it will not rotate on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be punched</param>
        /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchRotationDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchRotationDefaultDelay</param>
        /// <param name="vector">The vector specifying the amplitude of the punch rotating each axis (The number of rotations). Don't make this null.</param>    
        public static void punchRotation(GameObject target, float? time, float? delay, Vector3 vector)
        {
            punchRotation(target, time, delay, vector.x, vector.y, vector.z, null, null);
        }

        /// <summary>
        /// Will punch the object's rotation with an amplitude per axis as specified by the x, y, and z parameters
        /// the object will use a sine curve to determine the speed given the amplitude.
        /// The object will rotate and then return to it's starting position
        /// If any of x, y, or z are null then it will not rotate on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be punched</param>
        /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchRotationDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchRotationDefaultDelay</param>
        /// <param name="vector">The vector specifying the amplitude of the punch rotating each axis (The number of rotations). Don't make this null.</param>    
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void punchRotation(GameObject target, float? time, float? delay, Vector3 vector, string onComplete, object oncomplete_params)
        {
            punchRotation(target, time, delay, vector.x, vector.y, vector.z, onComplete, oncomplete_params);
        }

        /// <summary>
        /// Will punch the object's rotation with an amplitude per axis as specified by the x, y, and z parameters
        /// the object will use a sine curve to determine the speed given the amplitude.
        /// The object will rotate and then return to it's starting position
        /// If any of x, y, or z are null then it will not rotate on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be punched</param>
        /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchRotationDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchRotationDefaultDelay</param>
        /// <param name="vector">The vector specifying the amplitude of the punch rotating each axis (The number of rotations). Don't make this null.</param>    
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void punchRotation(GameObject target, float? time, float? delay, Vector3 vector, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            punchRotation(target, time, delay, vector.x, vector.y, vector.z, onComplete, oncomplete_params, oncomplete_target);
        }
        #endregion

        #region rotateAdd
        /// <summary>
        /// Will rotate the object the number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// The rotating will be transitioned with the current value of rotateAddDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateAddDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateAddDefaultDelay</param>
        /// <param name="x">The number of times to rotate on the x axis. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The number of times to rotate on the y axis. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The number of times to rotate on the z axis. If null, it will not rotate on the z axis.</param>   
        public static void rotateAdd(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            rotateAdd(target, time, delay, x, y, z, null, null, null);
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateAddDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateAddDefaultDelay</param>
        /// <param name="x">The number of times to rotate on the x axis. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The number of times to rotate on the y axis. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The number of times to rotate on the z axis. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateAddDefaultTransition</param>   
        public static void rotateAdd(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            rotateAdd(target, time, delay, x, y, z, transition, null, null);
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateAddDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateAddDefaultDelay</param>
        /// <param name="x">The number of times to rotate on the x axis. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The number of times to rotate on the y axis. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The number of times to rotate on the z axis. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateAddDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void rotateAdd(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            rotateAdd(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateAddDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateAddDefaultDelay</param>
        /// <param name="x">The number of times to rotate on the x axis. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The number of times to rotate on the y axis. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The number of times to rotate on the z axis. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateAddDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void rotateAdd(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            if (target.guiTexture || target.guiText)
            {
                //TODO: Why not throw a real error?
                Debug.LogError("ERROR: GUITextures cannot be rotated!");
                return;
            }

            init(target, new Arguments(time, rotateAddDefaultTime, delay, rotateAddDefaultDelay, transition, rotateAddDefaultTransition, onComplete, oncomplete_params,
                FunctionType.rotate, false, x, y, z, true, false, false, false, oncomplete_target, LoopType.none));
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// The rotation will be transitioned with the current value of rotateAddDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateAddDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateAddDefaultDelay</param>
        /// <param name="vector">The vector specifying the number of times to rotate each axis. Don't make this null.</param>        
        public static void rotateAdd(GameObject target, float? time, float? delay, Vector3 vector)
        {
            rotateAdd(target, time, delay, vector.x, vector.y, vector.z, null, null, null);
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateAddDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateAddDefaultDelay</param>
        /// <param name="vector">The vector specifying the number of times to rotate each axis. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateAddDefaultTransition</param>
        public static void rotateAdd(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition)
        {
            rotateAdd(target, time, delay, vector.x, vector.y, vector.z, transition, null, null);
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateAddDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateAddDefaultDelay</param>
        /// <param name="vector">The vector specifying the number of times to rotate each axis. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateAddDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void rotateAdd(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params)
        {
            rotateAdd(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateAddDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateAddDefaultDelay</param>
        /// <param name="vector">The vector specifying the number of times to rotate each axis. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateAddDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void rotateAdd(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            rotateAdd(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params, oncomplete_target);
        }

        #endregion

        #region rotateBy
        /// <summary>
        /// Will rotate the object the number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// The rotating will be transitioned with the current value of rotateByDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateByDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateByDefaultDelay</param>
        /// <param name="x">The number of times to rotate on the x axis. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The number of times to rotate on the y axis. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The number of times to rotate on the z axis. If null, it will not rotate on the z axis.</param>   
        public static void rotateBy(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            rotateBy(target, time, delay, x, y, z, null, null, null);
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateByDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateByDefaultDelay</param>
        /// <param name="x">The number of times to rotate on the x axis. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The number of times to rotate on the y axis. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The number of times to rotate on the z axis. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateByDefaultTransition</param>   
        public static void rotateBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            rotateBy(target, time, delay, x, y, z, transition, null, null);
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateByDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateByDefaultDelay</param>
        /// <param name="x">The number of times to rotate on the x axis. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The number of times to rotate on the y axis. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The number of times to rotate on the z axis. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateByDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void rotateBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            rotateBy(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateByDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateByDefaultDelay</param>
        /// <param name="x">The number of times to rotate on the x axis. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The number of times to rotate on the y axis. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The number of times to rotate on the z axis. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateByDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void rotateBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            if (target.guiTexture || target.guiText)
            {
                //TODO: Why not throw a real error?
                Debug.LogError("ERROR: GUITextures cannot be rotated!");
                return;
            }

            init(target, new Arguments(time, rotateByDefaultTime, delay, rotateByDefaultDelay, transition, rotateByDefaultTransition, onComplete, oncomplete_params,
                FunctionType.rotate, false, x, y, z, true, false, true, false, oncomplete_target, LoopType.none));
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// The rotation will be transitioned with the current value of rotateByDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateByDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateByDefaultDelay</param>
        /// <param name="vector">The vector specifying the number of times to rotate each axis. Don't make this null.</param>        
        public static void rotateBy(GameObject target, float? time, float? delay, Vector3 vector)
        {
            rotateBy(target, time, delay, vector.x, vector.y, vector.z, null, null, null);
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateByDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateByDefaultDelay</param>
        /// <param name="vector">The vector specifying the number of times to rotate each axis. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateByDefaultTransition</param>
        public static void rotateBy(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition)
        {
            rotateBy(target, time, delay, vector.x, vector.y, vector.z, transition, null, null);
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateByDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateByDefaultDelay</param>
        /// <param name="vector">The vector specifying the number of times to rotate each axis. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateByDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void rotateBy(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params)
        {
            rotateBy(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params);
        }

        /// <summary>
        /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
        /// If any of x, y, or z are null then there will be no rotation on that axis.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateByDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateByDefaultDelay</param>
        /// <param name="vector">The vector specifying the number of times to rotate each axis. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateByDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void rotateBy(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            rotateBy(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params, oncomplete_target);
        }

        #endregion

        #region rotateFrom
        /// <summary>
        /// Will rotate the object using the shortest path from the specified rotation to the current rotation. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The rotating will be transitioned with the current value of rotateToDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="x">The rotation on the x axis that will be rotated from. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The rotation on the y axis that will be rotated from. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The rotation on the z axis that will be rotated from. If null, it will not rotate on the z axis.</param>   
        public static void rotateFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            rotateFrom(target, time, delay, x, y, z, null, null, null);
        }

        /// <summary>
        /// Will rotate the object using the shortest path from the specified rotation to the current rotation. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="x">The rotation on the x axis that will be rotated from. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The rotation on the y axis that will be rotated from. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The rotation on the z axis that will be rotated from. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        public static void rotateFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            rotateFrom(target, time, delay, x, y, z, transition, null, null);
        }

        /// <summary>
        /// Will rotate the object using the shortest path from the specified rotation to the current rotation. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="x">The rotation on the x axis that will be rotated from. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The rotation on the y axis that will be rotated from. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The rotation on the z axis that will be rotated from. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void rotateFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            rotateFrom(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will rotate the object using the shortest path from the specified rotation to the current rotation. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="x">The rotation on the x axis that will be rotated from. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The rotation on the y axis that will be rotated from. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The rotation on the z axis that will be rotated from. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void rotateFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            if (target.guiTexture || target.guiText)
            {
                //TODO: Why not throw a real error?
                Debug.LogError("ERROR: GUITextures cannot be rotated!");
                return;
            }

            init(target, new Arguments(time, rotateDefaultTime, delay, rotateDefaultDelay, transition, rotateDefaultTransition, onComplete, oncomplete_params,
              FunctionType.rotate, true, x, y, z, false, false, false, false, oncomplete_target, LoopType.none));
        }

        /// <summary>
        /// Will rotate the object using the shortest path from the specified rotation to the current rotation. 
        /// The rotation will be transitioned with the current value of rotateToDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="vector">The vector specifying the rotation to rotate the object to. Don't make this null.</param>    
        public static void rotateFrom(GameObject target, float? time, float? delay, Vector3 vector)
        {
            rotateFrom(target, time, delay, vector.x, vector.y, vector.z, null, null, null);
        }

        /// <summary>
        /// Will rotate the object using the shortest path from the specified rotation to the current rotation. 
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="vector">The vector specifying the rotation to rotate the object to. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        public static void rotateFrom(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition)
        {
            rotateFrom(target, time, delay, vector.x, vector.y, vector.z, transition, null, null);
        }

        /// <summary>
        /// Will rotate the object using the shortest path from the specified rotation to the current rotation. 
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="vector">The vector specifying the rotation to rotate the object to. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void rotateFrom(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params)
        {
            rotateFrom(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params);
        }

        /// <summary>
        /// Will rotate the object using the shortest path from the specified rotation to the current rotation. 
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="vector">The vector specifying the rotation to rotate the object to. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void rotateFrom(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            rotateFrom(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params, oncomplete_target);
        }

        #endregion

        #region rotateTo
        /// <summary>
        /// Will rotate the object using the shortest path to the target rotation. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The rotating will be transitioned with the current value of rotateToDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="x">The rotation on the x axis that will be rotated to. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The rotation on the y axis that will be rotated to. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The rotation on the z axis that will be rotated to. If null, it will not rotate on the z axis.</param>
        public static void rotateTo(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            rotateTo(target, time, delay, x, y, z, null, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will rotate the object using the shortest path to the target rotation. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="x">The rotation on the x axis that will be rotated to. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The rotation on the y axis that will be rotated to. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The rotation on the z axis that will be rotated to. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        public static void rotateTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            rotateTo(target, time, delay, x, y, z, transition, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will rotate the object using the shortest path to the target rotation. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="x">The rotation on the x axis that will be rotated to. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The rotation on the y axis that will be rotated to. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The rotation on the z axis that will be rotated to. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void rotateTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            rotateTo(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null, LoopType.none);
        }

        /// <summary>
        /// Will rotate the object using the shortest path to the target rotation. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="x">The rotation on the x axis that will be rotated to. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The rotation on the y axis that will be rotated to. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The rotation on the z axis that will be rotated to. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void rotateTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            rotateTo(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, oncomplete_target, LoopType.none);
        }

        /// <summary>
        /// Will rotate the object using the shortest path to the target rotation. If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The rotation will be transitioned with the current value of rotateToDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="vector">The rotation to rotate the object to. Don't make this null.</param>    
        public static void rotateTo(GameObject target, float? time, float? delay, Vector3 vector)
        {
            rotateTo(target, time, delay, vector.x, vector.y, vector.z, null, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will rotate the object using the shortest path to the target rotation. 
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="vector">The rotation to rotate the object to. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        public static void rotateTo(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition)
        {
            rotateTo(target, time, delay, vector.x, vector.y, vector.z, transition, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will rotate the object using the shortest path to the target rotation. 
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="vector">The rotation to rotate the object to. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void rotateTo(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params)
        {
            rotateTo(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params, null, LoopType.none);
        }

        /// <summary>
        /// Will rotate the object using the shortest path to the target rotation. 
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="vector">The rotation to rotate the object to. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void rotateTo(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            rotateTo(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params, oncomplete_target, LoopType.none);
        }

        /// <summary>
        /// Will rotate the object using the shortest path to the target rotation. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The rotating will be transitioned with the current value of rotateToDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="x">The rotation on the x axis that will be rotated to. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The rotation on the y axis that will be rotated to. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The rotation on the z axis that will be rotated to. If null, it will not rotate on the z axis.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void rotateTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, LoopType loopType)
        {
            rotateTo(target, time, delay, x, y, z, null, null, null, loopType);
        }

        /// <summary>
        /// Will rotate the object using the shortest path to the target rotation. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="x">The rotation on the x axis that will be rotated to. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The rotation on the y axis that will be rotated to. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The rotation on the z axis that will be rotated to. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void rotateTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, LoopType loopType)
        {
            rotateTo(target, time, delay, x, y, z, transition, null, null, loopType);
        }

        /// <summary>
        /// Will rotate the object using the shortest path to the target rotation. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="x">The rotation on the x axis that will be rotated to. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The rotation on the y axis that will be rotated to. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The rotation on the z axis that will be rotated to. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void rotateTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, LoopType loopType)
        {
            rotateTo(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null, loopType);
        }

        /// <summary>
        /// Will rotate the object using the shortest path to the target rotation. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be rotated</param>
        /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
        /// <param name="x">The rotation on the x axis that will be rotated to. If null, it will not rotate on the x axis.</param>
        /// <param name="y">The rotation on the y axis that will be rotated to. If null, it will not rotate on the y axis.</param>
        /// <param name="z">The rotation on the z axis that will be rotated to. If null, it will not rotate on the z axis.</param>
        /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void rotateTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType)
        {
            if (target.guiTexture || target.guiText)
            {
                //TODO: Why not throw a real error?
                Debug.LogError("ERROR: GUITextures cannot be rotated!");
                return;
            }

            init(target, new Arguments(time, rotateDefaultTime, delay, rotateDefaultDelay, transition, rotateDefaultTransition, onComplete, oncomplete_params,
              FunctionType.rotate, false, x, y, z, false, false, false, false, oncomplete_target, loopType));
        }


        #endregion

        #region scaleAdd
        /// <summary>
        /// Will scale by adding the given dimensions to the current dimensions. 
        /// If any of x, y, or z are null then it will not scale that dimension
        /// The scaling will be transitioned with the current value of scaleDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale by on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale by on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale by on the z axis. If null, it will not scale on the z axis.</param>    
        public static void scaleAdd(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            scaleAdd(target, time, delay, x, y, z, null, null, null);
        }

        /// <summary>
        /// Will scale by adding the given dimensions to the current dimensions. 
        /// If any of x, y, or z are null then it will not scale that dimension
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale by on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale by on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale by on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        public static void scaleAdd(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            scaleAdd(target, time, delay, x, y, z, transition, null, null);
        }

        /// <summary>
        /// Will scale by adding the given dimensions to the current dimensions. 
        /// If any of x, y, or z are null then it will not scale that dimension
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale by on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale by on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale by on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void scaleAdd(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            scaleAdd(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will scale by adding the given dimensions to the current dimensions. 
        /// If any of x, y, or z are null then it will not scale that dimension
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale by on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale by on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale by on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void scaleAdd(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            init(target, new Arguments(time, scaleDefaultTime, delay, scaleDefaultDelay, transition, scaleDefaultTransition, onComplete, oncomplete_params,
              FunctionType.scale, false, x, y, z, true, false, false, false, oncomplete_target, LoopType.none));
        }

        /// <summary>
        /// Will scale by adding the given size specified in the vector.
        /// The scaling will be transitioned with the current value of scaleDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object by. Don't make this null.</param>    
        public static void scaleAdd(GameObject target, float? time, float? delay, Vector3 vector)
        {
            scaleAdd(target, time, delay, vector.x, vector.y, vector.z, null, null, null);
        }

        /// <summary>
        /// Will scale by adding the given size specified in the vector.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object by. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        public static void scaleAdd(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition)
        {
            scaleAdd(target, time, delay, vector.x, vector.y, vector.z, transition, null, null);
        }

        /// <summary>
        /// Will scale by adding the given size specified in the vector.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object by. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void scaleAdd(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params)
        {
            scaleAdd(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params);
        }

        /// <summary>
        /// Will scale by adding the given size specified in the vector.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object by. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void scaleAdd(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            scaleAdd(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params, oncomplete_target);
        }
        #endregion

        #region scaleBy
        /// <summary>
        /// Will scale by multiplying the given dimensions to the current dimensions. 
        /// If any of x, y, or z are null then it will not scale that dimension
        /// The scaling will be transitioned with the current value of scaleDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale by on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale by on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale by on the z axis. If null, it will not scale on the z axis.</param>    
        public static void scaleBy(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            scaleBy(target, time, delay, x, y, z, null, null, null);
        }

        /// <summary>
        /// Will scale by multiplying the given dimensions to the current dimensions. 
        /// If any of x, y, or z are null then it will not scale that dimension
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale by on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale by on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale by on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        public static void scaleBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            scaleBy(target, time, delay, x, y, z, transition, null, null);
        }

        /// <summary>
        /// Will scale by multiplying the given dimensions to the current dimensions. 
        /// If any of x, y, or z are null then it will not scale that dimension
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale by on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale by on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale by on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void scaleBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            scaleBy(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will scale by multiplying the given dimensions to the current dimensions. 
        /// If any of x, y, or z are null then it will not scale that dimension
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale by on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale by on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale by on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void scaleBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            init(target, new Arguments(time, scaleDefaultTime, delay, scaleDefaultDelay, transition, scaleDefaultTransition, onComplete, oncomplete_params,
              FunctionType.scale, false, x, y, z, true, true, true, false, oncomplete_target, LoopType.none));
        }

        /// <summary>
        /// Will scale by the given size specified in the vector.
        /// The scaling will be transitioned with the current value of scaleDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object by. Don't make this null.</param>    
        public static void scaleBy(GameObject target, float? time, float? delay, Vector3 vector)
        {
            scaleBy(target, time, delay, vector.x, vector.y, vector.z, null, null, null);
        }

        /// <summary>
        /// Will scale by the given size specified in the vector.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object by. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        public static void scaleBy(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition)
        {
            scaleBy(target, time, delay, vector.x, vector.y, vector.z, transition, null, null);
        }

        /// <summary>
        /// Will scale by the given size specified in the vector.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object by. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void scaleBy(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params)
        {
            scaleBy(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params);
        }

        /// <summary>
        /// Will scale by the given size specified in the vector.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object by. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void scaleBy(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            scaleBy(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params, oncomplete_target);
        }

        #endregion

        #region scaleFrom
        /// <summary>
        /// Will scale from the given size back to the current size. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The scaling will be transitioned with the current value of scaleDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>   
        public static void scaleFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            scaleFrom(target, time, delay, x, y, z, null, null, null);
        }

        /// <summary>
        /// Will scale from the given size back to the current size. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>   
        public static void scaleFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            scaleFrom(target, time, delay, x, y, z, transition, null, null);
        }

        /// <summary>
        /// Will scale from the given size back to the current size. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void scaleFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            scaleFrom(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// Will scale from the given size back to the current size. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void scaleFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            init(target, new Arguments(time, scaleDefaultTime, delay, scaleDefaultDelay, transition, scaleDefaultTransition, onComplete, oncomplete_params,
              FunctionType.scale, true, x, y, z, false, false, false, false, oncomplete_target, LoopType.none));
        }

        /// <summary>
        /// Will scale from the given size back to the current size. If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The scaling will be transitioned with the current value of scaleDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object from. Don't make this null.</param>    
        public static void scaleFrom(GameObject target, float? time, float? delay, Vector3 vector)
        {
            scaleFrom(target, time, delay, vector.x, vector.y, vector.z, null, null, null);
        }

        /// <summary>
        /// Will scale from the given size back to the current size.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object from. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        public static void scaleFrom(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition)
        {
            scaleFrom(target, time, delay, vector.x, vector.y, vector.z, transition, null, null);
        }

        /// <summary>
        /// Will scale from the given size back to the current size. 
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object from. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void scaleFrom(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params)
        {
            scaleFrom(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params);
        }

        /// <summary>
        /// Will scale from the given size back to the current size. 
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object from. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void scaleFrom(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            scaleFrom(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params, oncomplete_target);
        }
        #endregion

        #region scaleTo
        /// <summary>
        /// Will scale to the given size. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The scaling will be transitioned with the current value of scaleDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>    
        public static void scaleTo(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            scaleTo(target, time, delay, x, y, z, null, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will scale to the given size. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>    
        public static void scaleTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition)
        {
            scaleTo(target, time, delay, x, y, z, transition, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will scale to the given size. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void scaleTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params)
        {
            scaleTo(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null, LoopType.none);
        }

        /// <summary>
        /// Will scale to the given size. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void scaleTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            scaleTo(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, oncomplete_target, LoopType.none);
        }

        /// <summary>
        /// Will scale to the given size specified in the vector.
        /// The scaling will be transitioned with the current value of scaleDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object to. Don't make this null.</param>    
        public static void scaleTo(GameObject target, float? time, float? delay, Vector3 vector)
        {
            scaleTo(target, time, delay, vector.x, vector.y, vector.z, null, null, null, LoopType.none);
        }

        /// <summary>
        /// Will scale to the given size specified in the vector.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object to. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        public static void scaleTo(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition)
        {
            scaleTo(target, time, delay, vector.x, vector.y, vector.z, transition, null, null, LoopType.none);
        }

        /// <summary>
        /// Will scale to the given size specified in the vector.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object to. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void scaleTo(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params, LoopType loopType)
        {
            scaleTo(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params, loopType);
        }

        /// <summary>
        /// Will scale to the given size specified in the vector.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="vector">The size to scale the object to. Don't make this null.</param>    
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void scaleTo(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType)
        {
            scaleTo(target, time, delay, vector.x, vector.y, vector.z, transition, onComplete, oncomplete_params, oncomplete_target, loopType);
        }

        /// <summary>
        /// Will scale to the given size. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// The scaling will be transitioned with the current value of scaleDefaultTransition
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>   
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void scaleTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, LoopType loopType)
        {
            scaleTo(target, time, delay, x, y, z, null, null, null, loopType);
        }

        /// <summary>
        /// Will scale to the given size. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>  
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void scaleTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, LoopType loopType)
        {
            scaleTo(target, time, delay, x, y, z, transition, null, null, loopType);
        }

        /// <summary>
        /// Will scale to the given size. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void scaleTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, LoopType loopType)
        {
            scaleTo(target, time, delay, x, y, z, transition, onComplete, oncomplete_params, null, loopType);
        }

        /// <summary>
        /// Will scale to the given size. 
        /// If any of x, y, or z are null then the current value at tween time of the object will be used.
        /// </summary>
        /// <param name="target">The GameObject that will be scaled</param>
        /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
        /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
        /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
        /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>
        /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
        public static void scaleTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType)
        {
            init(target, new Arguments(time, scaleDefaultTime, delay, scaleDefaultDelay, transition, scaleDefaultTransition, onComplete, oncomplete_params,
              FunctionType.scale, false, x, y, z, false, false, false, false, oncomplete_target, loopType));
        }

        #endregion

        #region shake
        /// <summary>
        /// This will shake the object, with an initial impact of the full displacement as specified by the x, y and z parameters
        /// The object will then have a random shaking with a maximum displacement represented by the x, y and z parameters along those axis.
        /// The shake displacement will dissipate linearly across time from the initial maximum displacement to 0.
        /// The object will at the end return to where it started.
        /// </summary>
        /// <param name="target">The GameObject that will be shaken</param>
        /// <param name="time">The length of time to shake the object. If null, it will use the shakeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the shakeDefaultDelay</param>
        /// <param name="x">The maximum displacement and initial displacement along the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The maximum displacement and initial displacement along the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The maximum displacement and initial displacement along the z axis. If null, it will not move on the z axis.</param>
        public static void shake(GameObject target, float? time, float? delay, float? x, float? y, float? z)
        {
            shake(target, time, delay, x, y, z, null, null);
        }

        /// <summary>
        /// This will shake the object, with an initial impact of the full displacement as specified by the x, y and z parameters
        /// The object will then have a random shaking with a maximum displacement represented by the x, y and z parameters along those axis.
        /// The shake displacement will dissipate linearly across time from the initial maximum displacement to 0.
        /// The object will at the end return to where it started.
        /// </summary>
        /// <param name="target">The GameObject that will be shaken</param>
        /// <param name="time">The length of time to shake the object. If null, it will use the shakeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the shakeDefaultDelay</param>
        /// <param name="x">The maximum displacement and initial displacement along the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The maximum displacement and initial displacement along the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The maximum displacement and initial displacement along the z axis. If null, it will not move on the z axis.</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void shake(GameObject target, float? time, float? delay, float? x, float? y, float? z, string onComplete, object oncomplete_params)
        {
            shake(target, time, delay, x, y, z, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// This will shake the object, with an initial impact of the full displacement as specified by the x, y and z parameters
        /// The object will then have a random shaking with a maximum displacement represented by the x, y and z parameters along those axis.
        /// The shake displacement will dissipate linearly across time from the initial maximum displacement to 0.
        /// The object will at the end return to where it started.
        /// </summary>
        /// <param name="target">The GameObject that will be shaken</param>
        /// <param name="time">The length of time to shake the object. If null, it will use the shakeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the shakeDefaultDelay</param>
        /// <param name="x">The maximum displacement and initial displacement along the x axis. If null, it will not move on the x axis.</param>
        /// <param name="y">The maximum displacement and initial displacement along the y axis. If null, it will not move on the y axis.</param>
        /// <param name="z">The maximum displacement and initial displacement along the z axis. If null, it will not move on the z axis.</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void shake(GameObject target, float? time, float? delay, float? x, float? y, float? z, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            //NOTE: the easing type is set to any random value because it is not used
            init(target, new Arguments(time, shakeDefaultTime, delay, shakeDefaultDelay, EasingType.easeInExpo, EasingType.easeInExpo, onComplete, oncomplete_params,
              FunctionType.shake, false, x, y, z, false, false, false, false, oncomplete_target, LoopType.none));
        }

        /// <summary>
        /// This will shake the object, with an initial impact of the full displacement as specified by the x, y and z parameters
        /// The object will then have a random shaking with a maximum displacement represented by the x, y and z parameters along those axis.
        /// The shake displacement will dissipate linearly across time from the initial maximum displacement to 0.
        /// The object will at the end return to where it started.
        /// </summary>
        /// <param name="target">The GameObject that will be shaken</param>
        /// <param name="time">The length of time to shake the object. If null, it will use the shakeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the shakeDefaultDelay</param>
        /// <param name="vector">The vector specifys the maximum and initial displacement of the shake along each axis. Don't make this null.</param>     
        public static void shake(GameObject target, float? time, float? delay, Vector3 vector)
        {
            shake(target, time, delay, vector.x, vector.y, vector.z, null, null);
        }

        /// <summary>
        /// This will shake the object, with an initial impact of the full displacement as specified by the x, y and z parameters
        /// The object will then have a random shaking with a maximum displacement represented by the x, y and z parameters along those axis.
        /// The shake displacement will dissipate linearly across time from the initial maximum displacement to 0.
        /// The object will at the end return to where it started.
        /// </summary>
        /// <param name="target">The GameObject that will be shaken</param>
        /// <param name="time">The length of time to shake the object. If null, it will use the shakeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the shakeDefaultDelay</param>
        /// <param name="vector">The vector specifys the maximum and initial displacement of the shake along each axis. Don't make this null.</param> 
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void shake(GameObject target, float? time, float? delay, Vector3 vector, string onComplete, object oncomplete_params)
        {
            shake(target, time, delay, vector.x, vector.y, vector.z, onComplete, oncomplete_params);
        }

        /// <summary>
        /// This will shake the object, with an initial impact of the full displacement as specified by the x, y and z parameters
        /// The object will then have a random shaking with a maximum displacement represented by the x, y and z parameters along those axis.
        /// The shake displacement will dissipate linearly across time from the initial maximum displacement to 0.
        /// The object will at the end return to where it started.
        /// </summary>
        /// <param name="target">The GameObject that will be shaken</param>
        /// <param name="time">The length of time to shake the object. If null, it will use the shakeDefaultTime</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the shakeDefaultDelay</param>
        /// <param name="vector">The vector specifys the maximum and initial displacement of the shake along each axis. Don't make this null.</param> 
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void shake(GameObject target, float? time, float? delay, Vector3 vector, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            shake(target, time, delay, vector.x, vector.y, vector.z, onComplete, oncomplete_params, oncomplete_target);
        }
        #endregion

        #region stab
        /// <summary>
        /// This will play the given audio clip from the audio source of the specified game object.
        /// If the object does not have an audio source attached to it, one will be created and attached to it.
        /// </summary>
        /// <param name="target">The GameObject that will have the audio played from it</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the stabDefaultDelay</param>
        /// <param name="volume">The volume that the clip will be played. If null, it will use the stabDefaultVolume</param>
        /// <param name="pitch">The pitch that the clip will be played. If null, it will use the stabDefaultPitch</param>
        /// <param name="clip">The audio clip that will be played.</param>
        public static void stab(GameObject target, float? delay, float? volume, float? pitch, AudioClip clip)
        {
            stab(target, delay, volume, pitch, clip, null, null);
        }

        /// <summary>
        /// This will play the given audio clip from the audio source of the specified game object.
        /// If the object does not have an audio source attached to it, one will be created and attached to it.
        /// </summary>
        /// <param name="target">The GameObject that will have the audio played from it</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the stabDefaultDelay</param>
        /// <param name="volume">The volume that the clip will be played. If null, it will use the stabDefaultVolume</param>
        /// <param name="pitch">The pitch that the clip will be played. If null, it will use the stabDefaultPitch</param>
        /// <param name="clip">The audio clip that will be played.</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        public static void stab(GameObject target, float? delay, float? volume, float? pitch, AudioClip clip, string onComplete, object oncomplete_params)
        {
            stab(target, delay, volume, pitch, clip, onComplete, oncomplete_params, null);
        }

        /// <summary>
        /// This will play the given audio clip from the audio source of the specified game object.
        /// If the object does not have an audio source attached to it, one will be created and attached to it.
        /// </summary>
        /// <param name="target">The GameObject that will have the audio played from it</param>
        /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the stabDefaultDelay</param>
        /// <param name="volume">The volume that the clip will be played. If null, it will use the stabDefaultVolume</param>
        /// <param name="pitch">The pitch that the clip will be played. If null, it will use the stabDefaultPitch</param>
        /// <param name="clip">The audio clip that will be played.</param>
        /// <param name="onComplete">The name of the function that will be called back to.</param>
        /// <param name="oncomplete_params">The parameter(s) that will be used with the callback.</param>
        /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
        public static void stab(GameObject target, float? delay, float? volume, float? pitch, AudioClip clip, string onComplete, object oncomplete_params, GameObject oncomplete_target)
        {
            //NOTE: the easing type and time are set to any random value because they are not used
            init(target, new Arguments(1, shakeDefaultTime, delay, stabDefaultDelay, EasingType.easeInExpo, EasingType.easeInExpo, onComplete, oncomplete_params,
              FunctionType.stab, false, volume, stabDefaultVolume, pitch, stabDefaultPitch, clip, null, oncomplete_target));
        }
        #endregion

        #endregion



    }
