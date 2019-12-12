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
        public RectangleF GetViewport()
        {
            GetViewport(out float out_x, out float out_y, out float out_w, out float out_h);
            return new RectangleF(out_x, out_y, out_w, out_h);
        }
        /// <summary>
        /// Gets the texture coordinates according to a viewport.
        /// </summary>
        /// <returns>The size of the viewport</returns>
        public void SetViewport(RectangleF r)
        {
            SetViewport(r.X, r.Y, r.Width, r.Height);
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


    public partial class File
    {
        /// <summary>
        /// Write data to a file.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Whether the operation was successful.</returns>
        public bool Write(byte[] data)
        {
            return Write(data, data.Length);
        }

        /// <summary>
        /// Write data to a file.
        /// </summary>
        /// <param name="data">The Data object to write.</param>
        /// <returns>Whether the operation was successful.</returns>
        public bool Write(Data data)
        {
            return Write(data, data.GetSize());
        }
    }

    public partial class Text
    {
        public int Add(string text, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            return Add(ColoredStringArray.Create(text), x, y, angle, sx, sy, ox, oy, kx, ky);
        }
        public int Add(string text, float wraplimit, AlignMode align_type, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            return Addf(ColoredStringArray.Create(text), wraplimit, align_type, x, y, angle, sx, sy, ox, oy, kx, ky);
        }
    }


    public partial class Canvas
    {
        /// <summary>
        /// Render to the Canvas using a function. This is a shortcut to love.graphics.setCanvas.
        /// </summary>
        /// <param name="func"></param>
        public void RenderTo(Action func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            var oldTargets = Graphics.GetCanvas();
            Graphics.SetCanvas(this);
            func();
            Graphics.SetCanvas(oldTargets);
        }
    }

    public partial class Shader
    {
        /// <summary>
        /// Sends one or more Vector2 values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Vector2 to send to the shader.</param>
        /// <param name="valueArray">Vector2 to send to store in the uniform variable.</param>
        public void SendVector2(string name, params Vector2[] valueArray)
        {
            var input = new float[valueArray.Length * 2];
            for (int i = 0; i < valueArray.Length; i++)
            {
                input[i * 2] = valueArray[i].X;
                input[i * 2 + 1] = valueArray[i].Y;
            }

            SendFloats(name, input);
        }


        /// <summary>
        /// Sends one or more Vector3 values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Vector3 to send to the shader.</param>
        /// <param name="valueArray">Vector3 to send to store in the uniform variable.</param>
        public void SendVector3(string name, params Vector3[] valueArray)
        {
            var input = new float[valueArray.Length * 3];
            for (int i = 0; i < valueArray.Length; i++)
            {
                input[i * 3] = valueArray[i].X;
                input[i * 3 + 1] = valueArray[i].Y;
                input[i * 3 + 2] = valueArray[i].Z;
            }

            SendFloats(name, input);
        }

        /// <summary>
        /// Sends one or more Vector4 values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Vector4 to send to the shader.</param>
        /// <param name="valueArray">Vector4 to send to store in the uniform variable.</param>
        public void SendVector4(string name, params Vector4[] valueArray)
        {
            var input = new float[valueArray.Length * 4];
            for (int i = 0; i < valueArray.Length; i++)
            {
                input[i * 4] = valueArray[i].X;
                input[i * 4 + 1] = valueArray[i].Y;
                input[i * 4 + 2] = valueArray[i].Z;
                input[i * 4 + 3] = valueArray[i].W;
            }

            SendFloats(name, input);
        }

        /// <summary>
        /// Sends one or more Vector2 values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Vector2 to send to the shader.</param>
        /// <param name="valueArray">Vector2 to send to store in the uniform variable.</param>
        public void Send(string name, params Vector2[] valueArray)
        {
            SendVector2(name, valueArray);
        }

        /// <summary>
        /// Sends one or more Vector3 values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Vector3 to send to the shader.</param>
        /// <param name="valueArray">Vector3 to send to store in the uniform variable.</param>
        public void Send(string name, params Vector3[] valueArray)
        {
            SendVector3(name, valueArray);
        }

        /// <summary>
        /// Sends one or more Vector4 values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Vector4 to send to the shader.</param>
        /// <param name="valueArray">Vector4 to send to store in the uniform variable.</param>
        public void Send(string name, params Vector4[] valueArray)
        {
            SendVector4(name, valueArray);
        }

        /// <summary>
        /// Sends one or more float values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the float to send to the shader.</param>
        /// <param name="valueArray">Float to send to store in the uniform variable.</param>
        public void Send(string name, params float[] valueArray)
        {
            SendFloats(name, valueArray);
        }

        /// <summary>
        /// Sends one or more uint values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the uint to send to the shader.</param>
        /// <param name="valueArray">Uint to send to store in the uniform variable.</param>
        public void Send(string name, params uint[] valueArray)
        {
            SendUints(name, valueArray);
        }

        /// <summary>
        /// Sends one or more int values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the int to send to the shader.</param>
        /// <param name="valueArray">Int to send to store in the uniform variable.</param>
        public void Send(string name, params int[] valueArray)
        {
            SendInts(name, valueArray);
        }

        /// <summary>
        /// Sends one or more boolean values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the boolean to send to the shader.</param>
        /// <param name="valueArray">Boolean to send to store in the uniform variable.</param>
        public void Send(string name, params bool[] valueArray)
        {
            SendBooleans(name, valueArray);
        }

        /// <summary>
        /// Sends one or more Matrix22 values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Matrix22 to send to the shader.</param>
        /// <param name="valueArray">Matrix22 to send to store in the uniform variable.</param>
        public void Send(string name, params Matrix22[] valueArray)
        {
            SendMatrix(name, valueArray);
        }

        /// <summary>
        /// Sends one or more Matrix33 values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Matrix33 to send to the shader.</param>
        /// <param name="valueArray">Matrix33 to send to store in the uniform variable.</param>
        public void Send(string name, params Matrix33[] valueArray)
        {
            SendMatrix(name, valueArray);
        }

        /// <summary>
        /// Sends one or more SendMatrix values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the SendMatrix to send to the shader.</param>
        /// <param name="valueArray">SendMatrix to send to store in the uniform variable.</param>
        public void Send(string name, params Matrix44[] valueArray)
        {
            SendMatrix(name, valueArray);
        }

        /// <summary>
        /// Sends one or more texture to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Texture to send to the shader.</param>
        /// <param name="texture">Texture (Image or Canvas) to send to the uniform variable.</param>
        public void Send(string name, Texture texture)
        {
            SendTexture(name, texture);
        }
    }

    public partial class Joystick
    {
        static JoystickHelper joystickHelper = new JoystickHelper();
        internal static void Step()
        {
            joystickHelper.Step();
        }
        public static bool IsPressed(Joystick joystick, int button)
        {
            return joystickHelper.IsPressed(joystick, button);
        }
        public static bool IsReleased(Joystick joystick, int button)
        {
            return joystickHelper.IsReleased(joystick, button);
        }
        public static bool IsGamepadPressed(Joystick joystick, GamepadButton gamepadButton)
        {
            return joystickHelper.IsGamepadPressed(joystick, gamepadButton);
        }
        public static bool IsGamepadReleased(Joystick joystick, GamepadButton gamepadButton)
        {
            return joystickHelper.IsGamepadReleased(joystick, gamepadButton);
        }
        public bool IsPressed(int button)
        {
            return joystickHelper.IsPressed(this, button);
        }
        public bool IsReleased(int button)
        {
            return joystickHelper.IsReleased(this, button);
        }
        public bool IsGamepadPressed(GamepadButton gamepadButton)
        {
            return joystickHelper.IsGamepadPressed(this, gamepadButton);
        }
        public bool IsGamepadReleased(GamepadButton gamepadButton)
        {
            return joystickHelper.IsGamepadReleased(this, gamepadButton);
        }
    }

    class JoystickHelper
    {
        HashSet<string> lastPressedMemory = new HashSet<string>();
        HashSet<string> currentPressedMemory = new HashSet<string>();

        public void Step()
        {
            lastPressedMemory = currentPressedMemory;
            currentPressedMemory = new HashSet<string>();
            foreach (var joy in Joystick.GetJoysticks())
            {
                string guid = joy.GetGUID();
                for (int i = 0; i < joy.GetButtonCount(); i++)
                {
                    if (joy.IsDown(i))
                    {
                        currentPressedMemory.Add(guid + i);
                    }
                }
                foreach (var gbtn in (GamepadButton[])System.Enum.GetValues(typeof(GamepadButton)))
                {
                    if (joy.IsGamepadDown(gbtn))
                    {
                        currentPressedMemory.Add(guid + gbtn);
                    }
                }
            }
        }

        public bool IsPressed(Joystick joystick, int button)
        {
            string name = joystick.GetGUID() + button;
            return currentPressedMemory.Contains(name) && !lastPressedMemory.Contains(name);
        }

        public bool IsReleased(Joystick joystick, int button)
        {
            string name = joystick.GetGUID() + button;
            return !currentPressedMemory.Contains(name) && lastPressedMemory.Contains(name);
        }

        public bool IsGamepadPressed(Joystick joystick, GamepadButton gamepadButton)
        {
            string name = joystick.GetGUID() + gamepadButton;
            return currentPressedMemory.Contains(name) && !lastPressedMemory.Contains(name);
        }

        public bool IsGamepadReleased(Joystick joystick, GamepadButton gamepadButton)
        {
            string name = joystick.GetGUID() + gamepadButton;
            return !currentPressedMemory.Contains(name) && lastPressedMemory.Contains(name);
        }
    }

}
