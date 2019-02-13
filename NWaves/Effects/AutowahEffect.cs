﻿using NWaves.Operations;
using System;

namespace NWaves.Effects
{
    /// <summary>
    /// Class for AutoWah effect.
    /// </summary>
    public class AutowahEffect : AudioEffect
    {
        /// <summary>
        /// Q
        /// </summary>
        public float Q { get; }

        /// <summary>
        /// Min LFO frequency
        /// </summary>
        public float MinFrequency { get; }

        /// <summary>
        /// Max LFO frequency
        /// </summary>
        public float MaxFrequency { get; }

        /// <summary>
        /// Sampling rate
        /// </summary>
        private int _fs;

        /// <summary>
        /// Envelope follower
        /// </summary>
        private EnvelopeFollower _envelopeFollower;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="samplingRate"></param>
        /// <param name="minFrequency"></param>
        /// <param name="maxFrequency"></param>
        /// <param name="q"></param>
        public AutowahEffect(int samplingRate, float minFrequency = 30, float maxFrequency = 2000, float q = 0.5f)
        {
            _fs = samplingRate;
            MinFrequency = minFrequency;
            MaxFrequency = maxFrequency;
            Q = q;

            _envelopeFollower = new EnvelopeFollower(samplingRate);
        }

        /// <summary>
        /// Autowah means: 1) envelope follower + 2) wahwah effect
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public override float Process(float sample)
        {
            var filt = _envelopeFollower.Process(sample) * Math.Sqrt(Q);

            var frequencyRange = Math.PI * (MaxFrequency - MinFrequency) / _fs;
            var minFreq = Math.PI * MinFrequency / _fs;
            var maxFreq = Math.PI * MaxFrequency / _fs;

            var centerFrequency = filt * frequencyRange + minFreq;

            _f = (float)(2 * Math.Sin(centerFrequency));

            _yh = sample - _yl - Q * _yb;
            _yb += _f * _yh;
            _yl += _f * _yb;

            return Wet * _yb + Dry * sample;
        }

        public override void Reset()
        {
            _yh = _yl = _yb = 0;
            _envelopeFollower.Reset();
        }

        private float _yh, _yb, _yl;
        private float _f;
    }
}
