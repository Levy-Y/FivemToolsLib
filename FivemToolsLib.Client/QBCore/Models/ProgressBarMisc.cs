using CitizenFX.Core;

namespace FivemToolsLib.Client.QBCore.Models
{
    /// <summary>
    /// Represents an animation to be played during an action.
    /// </summary>
    public class Animation
    {
        /// <summary>Gets the animation dictionary name.</summary>
        public string AnimDict { get; }
        /// <summary>Gets the animation name.</summary>
        public string Anim { get; }
        /// <summary>Gets the animation playback flags.</summary>
        public int Flags { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Animation"/> class.
        /// </summary>
        /// <param name="animDict">Animation dictionary.</param>
        /// <param name="anim">Animation name.</param>
        /// <param name="flags">Playback flags.</param>
        public Animation(string animDict, string anim, int flags)
        {
            AnimDict = animDict;
            Anim = anim;
            Flags = flags;
        }
    }
    
    /// <summary>
    /// Represents a prop that is used during an animation or progress bar.
    /// </summary>
    public class Prop
    {
        /// <summary>Gets the model of the prop.</summary>
        public string Model { get; }
        /// <summary>Gets the bone index to attach the prop to.</summary>
        public int Bone { get; }
        /// <summary>Gets the coordinates where the prop will be placed.</summary>
        public Vector3 Coords { get; }
        /// <summary>Gets the rotation of the prop.</summary>
        public Vector3 Rotation { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Prop"/> class.
        /// </summary>
        /// <param name="model">Models name.</param>
        /// <param name="bone">Bone index.</param>
        /// <param name="coords">Coordinates.</param>
        /// <param name="rotation">Rotation.</param>
        public Prop(string model, int bone, Vector3 coords, Vector3 rotation)
        {
            Model = model;
            Bone = bone;
            Coords = coords;
            Rotation = rotation;
        }
    }
    
    /// <summary>
    /// Represents control disabling options during an animation or progress bar.
    /// </summary>
    public class ControlDisables
    {
        /// <summary>Indicates whether player movement is disabled.</summary>
        public bool DisableMovement { get; }
        /// <summary>Indicates whether vehicle movement is disabled.</summary>
        public bool DisableCarMovement { get; }
        /// <summary>Indicates whether mouse movement is disabled.</summary>
        public bool DisableMouse { get; }
        /// <summary>Indicates whether combat actions are disabled.</summary>
        public bool DisableCombat { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlDisables"/> class.
        /// </summary>
        /// <param name="disableMovement">Disable player movement.</param>
        /// <param name="disableCarMovement">Disable car movement.</param>
        /// <param name="disableMouse">Disable mouse input.</param>
        /// <param name="disableCombat">Disable combat controls.</param>
        public ControlDisables(bool disableMovement, bool disableCarMovement, bool disableMouse, bool disableCombat)
        {
            DisableMovement = disableMovement;
            DisableCarMovement = disableCarMovement;
            DisableMouse = disableMouse;
            DisableCombat = disableCombat;
        }
    }
}