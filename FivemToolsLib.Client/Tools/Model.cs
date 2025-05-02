using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace FivemToolsLib.Client.Tools
{
    /// <summary>
    /// Provides utility functionality for loading and disposing game models safely.
    /// </summary>
    public class Models
    {
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
                Debug.WriteLine($"Waiting for model {model} to load");
                await BaseScript.Delay(100);
            }
    
            return true;
        }
    }
}