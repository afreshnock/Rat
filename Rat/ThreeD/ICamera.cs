using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rat.ThreeD
{
    /// <summary>
    /// An interface defining a camera
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// The view matrix
        /// </summary>
        Matrix View { get; }

        /// <summary>
        /// The projection matrix
        /// </summary>
        Matrix Projection { get; }
    }
}
