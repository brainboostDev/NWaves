﻿using System;
using System.Linq;
using NWaves.Filters.Base;
using NWaves.Signals;

namespace NWaves.Effects
{
    /// <summary>
    /// Class for overdrive effect.
    /// DAFX book [Udo Zoelzer], p.118.
    /// </summary>
    public class OverdriveEffect : IFilter
    {
        /// <summary>
        /// Method implements simple overdrive effect
        /// </summary>
        /// <param name="signal"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public DiscreteSignal ApplyTo(DiscreteSignal signal,
                                      FilteringMethod method = FilteringMethod.Auto)
        {
            var maxAmp = signal.Samples.Max(s => Math.Abs(s));

            var lowerThreshold = maxAmp / 3;
            var upperThreshold = maxAmp * 2 / 3;

            var output = signal.Samples.Select(s =>
            {
                var abs = Math.Abs(s);

                if (abs > upperThreshold)
                {
                    return Math.Sign(s);
                }

                if (abs >= lowerThreshold)
                {
                    return Math.Sign(s) * (3 - (2 - 3 * abs) * (2 - 3 * abs)) / 3;
                }

                return 2 * s;
            });
            
            return new DiscreteSignal(signal.SamplingRate, output);
        }
    }
}
