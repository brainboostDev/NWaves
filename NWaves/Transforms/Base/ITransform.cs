using System;

namespace NWaves.Transforms.Base
{
    /// <summary>
    /// Interface for real-valued transforms.
    /// </summary>
    public interface ITransform
    {
        /// <summary>
        /// Gets transform size.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Does direct transform.
        /// </summary>
        /// <param name="input">Input data</param>
        /// <param name="output">Output data</param>
        void Direct(Memory<float> input, float[] output);

        /// <summary>
        /// Does normalized direct transform.
        /// </summary>
        /// <param name="input">Input data</param>
        /// <param name="output">Output data</param>
        void DirectNorm(Memory<float> input, float[] output);

        /// <summary>
        /// Does inverse transform.
        /// </summary>
        /// <param name="input">Input data</param>
        /// <param name="output">Output data</param>
        void Inverse(Memory<float> input, float[] output);

        /// <summary>
        /// Does normalized inverse transform.
        /// </summary>
        /// <param name="input">Input data</param>
        /// <param name="output">Output data</param>
        void InverseNorm(Memory<float> input, float[] output);
    }
}
