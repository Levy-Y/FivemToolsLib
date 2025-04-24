using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace FivemToolsLib.Client.Tools
{
    // public class Model
    // {
    //     public static async Task<bool> LoadModel(uint model)
    //     {
    //         if (!API.IsModelInCdimage(model))
    //         {
    //             Debug.WriteLine($"Invalid model {model} was supplied to LoadModel.");
    //             return false;
    //         }
    //
    //         API.RequestModel(model);
    //         while (!API.HasModelLoaded(model))
    //         {
    //             Debug.WriteLine($"Waiting for model {model} to load");
    //             await BaseScript.Delay(100);
    //         }
    //
    //         return true;
    //     }
    // }
    
    /// <summary>
    /// Provides utility functionality for loading and disposing game models safely.
    /// </summary>
    public class Model : IDisposable
    {
        private readonly uint _model;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class with the specified model hash.
        /// </summary>
        /// <param name="model">The hash of the model.</param>
        private Model(uint model)
        {
            _model = model;
        }

        /// <summary>
        /// Asynchronously requests and loads a model by its hash.
        /// </summary>
        /// <param name="model">The hash of the model to load.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a boolean indicating whether the model was successfully loaded.
        /// </returns>
        public static async Task<bool> LoadModel(uint model)
        {
            if (!API.IsModelInCdimage(model))
            {
                Debug.WriteLine($"Invalid model {model} was supplied to LoadModel.");
                return false;
            }

            API.RequestModel(model);
            while (!API.HasModelLoaded(model))
            {
                // TODO: Remove debug line
                Debug.WriteLine($"Waiting for model {model} to load");
                await BaseScript.Delay(100);
            }

            return true;
        }

        /// <summary>
        /// Marks the model as no longer needed by the game engine.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                API.SetModelAsNoLongerNeeded(_model);
                _disposed = true;
            }
        }
    }
}