using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Love
{
    public partial class ParticleSystem
    {
        /// <summary>
        /// Get the offset position which the particle sprite is rotated around. If this function is not used, the particles rotate around their center.
        /// </summary>
        /// <returns>The coordinate of the rotation offset.</returns>
        public Vector2 GetOffset()
        {
            GetOffset(out float out_x, out float out_y);
            return new Vector2(out_x, out_y);
        }
    }


    public partial class Quad
    {
        /// <summary>
        /// Gets the texture coordinates according to a viewport.
        /// </summary>
        /// <returns>The size of the viewport</returns>
        public Viewport GetViewport()
        {
            GetViewport(out float out_x, out float out_y, out float out_w, out float out_h);
            return new Viewport(out_x, out_y, out_w, out_h);
        }

        /// <summary>
        /// Gets reference texture dimensions initially specified in love.graphics.newQuad.
        /// </summary>
        /// <returns>The Texture size used by the Quad.</returns>
        public Vector2 GetTextureDimensions()
        {
            GetTextureDimensions(out float out_sw, out float out_sh);
            return new Vector2(out_sw, out_sh);
        }
    }
}
