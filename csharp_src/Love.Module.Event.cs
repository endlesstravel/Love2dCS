using System;
using System.Collections.Generic;

namespace Love
{
    public partial class Event
    {
        public static void Init()
        {
            Love2dDll.wrap_love_dll_event_open_love_event();
        }

        /// <summary>
        /// Direct exits the LÖVE program.
        /// <para>Adds the quit event to the queue.</para>
        /// <para>The quit event is a signal for the event handler to close LÖVE. It's possible to abort the exit process with the love.quit callback.</para>
        /// </summary>
        /// <param name="exitStatus">The program exit status to use when closing the application.</param>
        public static void Quit(int exitStatus = 0)
        {
            Environment.Exit(exitStatus);
        }

        /// <summary>
        /// Handle event queue.
        /// </summary>
        /// <param name="scene">event handler</param>
        /// <returns></returns>
        public static bool Poll(EventQueueBox box)
        {
            return PollOrWaitReal(box, true);
        }

        /// <summary>
        /// Like <see cref="Poll"/>, but blocks until there is an event in the queue.
        /// </summary>
        /// <param name="scene">event handler</param>
        /// <returns></returns>
        public static void Wait(EventQueueBox box)
        {
            PollOrWaitReal(box, true);
        }
    }

    /// <summary>
    /// <para>Manages events, like keypresses.</para>
    /// </summary>
    public partial class Event
    {
        private enum WrapEventType
        {
            WRAP_EVENT_TYPE_UNKNOW,

            WRAP_EVENT_TYPE_KEY,
            WRAP_EVENT_TYPE_MOUSE_BUTTON,
            WRAP_EVENT_TYPE_MOUSE_MOTION,
            WRAP_EVENT_TYPE_MOUSE_WHEEL,

            WRAP_EVENT_TYPE_TOUCH_MOVED,
            WRAP_EVENT_TYPE_TOUCH_PRESSED,
            WRAP_EVENT_TYPE_TOUCH_RELEASED,

            WRAP_EVENT_TYPE_JOYSTICK_BUTTON,
            WRAP_EVENT_TYPE_JOYSTICK_AXIS_MOTION,
            WRAP_EVENT_TYPE_JOYSTICK_HAT_MOTION,
            WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_BUTTON,
            WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_AXIS_MOTION,
            WRAP_EVENT_TYPE_JOYSTICK_DEVICE_ADDED_OR_REMOVED,

            WRAP_EVENT_TYPE_TEXTINPUT,
            WRAP_EVENT_TYPE_TEXTEDITING,

            WRAP_EVENT_TYPE_WINDOW_VISIBLE,
            WRAP_EVENT_TYPE_WINDOW_ENTER_OR_LEAVE,
            WRAP_EVENT_TYPE_WINDOW_SHOWN_OR_HIDDEN,
            WRAP_EVENT_TYPE_WINDOW_RESIZED,

            WRAP_EVENT_TYPE_FILE_DROPPED,
            WRAP_EVENT_TYPE_DIRECTORY_DROPPED,

            WRAP_EVENT_TYPE_LOWMEMORY,
            WRAP_EVENT_TYPE_QUIT,
        };

        private enum EventType
        {
            Unknow,
            KeyPressed,
            KeyReleased,
            MouseMoved,
            MousePressed,
            MouseReleased,
            MouseFocus,
            WheelMoved,
            JoystickPressed,
            JoystickReleased,
            JoystickAxis,
            JoystickHat,
            JoystickGamepadPressed,
            JoystickGamepadReleased,
            JoystickGamepadAxis,
            JoystickAdded,
            JoystickRemoved,
            TouchMoved,
            TouchPressed,
            TouchReleased,
            TextEditing,
            TextInput,
            WindowFocus,
            WindowVisible,
            WindowResize,
            DirectoryDropped,
            FileDropped,
            Quit,
            LowMemory,
        };

        private class EventData
        {
            internal EventType type;
            internal KeyConstant key;
            internal Scancode scancode;
            internal Joystick joystick;
            internal JoystickHat direction;
            internal GamepadButton gamepadButton;
            internal GamepadAxis gamepadAxis;
            internal File file;
            internal string text;
            internal bool flag;
            internal float fx, fy, fz, fw, fp;
            internal int idx, idy, idz;
            internal long lid;

            public EventData(EventType t)
            {
                type = t;
                key = KeyConstant.Unknown;
                scancode = Scancode.Unknow;
                joystick = null;
                direction = JoystickHat.Invalid;
                gamepadButton = GamepadButton.Invalid;
                gamepadAxis = GamepadAxis.Invalid;
                file = null;
                text = null;
                flag = false;
                fx = 0;
                fy = 0;
                fz = 0;
                fw = 0;
                fp = 0;
                idx = 0;
                idy = 0;
                lid = 0;
            }
        }

        public class EventQueueBox
        {
            readonly LinkedList<EventData> list = new LinkedList<EventData>();

            public void KeyPressed(KeyConstant key, Scancode scancode, bool isRepeat)
            {
                EventData ed = new EventData(EventType.KeyPressed);
                ed.key = key;
                ed.scancode = scancode;
                ed.flag = isRepeat;
                list.AddLast(ed);
            }
            public void KeyReleased(KeyConstant key, Scancode scancode)
            {
                EventData ed = new EventData(EventType.KeyReleased);
                ed.key = key;
                ed.scancode = scancode;
                list.AddLast(ed);
            }
            public void MouseMoved(float x, float y, float dx, float dy, bool isTouch)
            {
                EventData ed = new EventData(EventType.MouseMoved);
                ed.fx = x;
                ed.fy = y;
                ed.fz = dx;
                ed.fw = dy;
                ed.flag = isTouch;
                list.AddLast(ed);
            }
            public void MousePressed(float x, float y, int button, bool isTouch)
            {
                EventData ed = new EventData(EventType.MousePressed);
                ed.fx = x;
                ed.fy = y;
                ed.idx = button - 1;
                ed.flag = isTouch;
                list.AddLast(ed);
            }
            public void MouseReleased(float x, float y, int button, bool isTouch)
            {
                EventData ed = new EventData(EventType.MouseReleased);
                ed.fx = x;
                ed.fy = y;
                ed.idx = button - 1;
                ed.flag = isTouch;
                list.AddLast(ed);
            }
            public void MouseFocus(bool focus)
            {
                EventData ed = new EventData(EventType.MouseFocus);
                ed.flag = focus;
                list.AddLast(ed);
            }
            public void WheelMoved(int x, int y)
            {
                EventData ed = new EventData(EventType.WheelMoved);
                ed.idx = x;
                ed.idy = y;
                list.AddLast(ed);
            }
            public Point GetScrollValue()
            {
                Point svalue = new Point(0, 0);
                foreach (var ed in list)
                {
                    if (ed.type == EventType.WheelMoved)
                    {
                        svalue.x += ed.idx;
                        svalue.y += ed.idy;
                    }
                }

                return svalue;
            }
            public void JoystickPressed(Joystick joystick, int button)
            {
                EventData ed = new EventData(EventType.JoystickPressed);
                ed.joystick = joystick;
                ed.idx = button;
                list.AddLast(ed);
            }
            public void JoystickReleased(Joystick joystick, int button)
            {
                EventData ed = new EventData(EventType.JoystickReleased);
                ed.joystick = joystick;
                ed.idx = button;
                list.AddLast(ed);
            }
            public void JoystickAxis(Joystick joystick, float axis, float value)
            {
                EventData ed = new EventData(EventType.JoystickAxis);
                ed.joystick = joystick;
                ed.fx = axis;
                ed.fy = value;
                list.AddLast(ed);
            }
            public void JoystickHat(Joystick joystick, int hat, JoystickHat direction)
            {
                EventData ed = new EventData(EventType.JoystickHat);
                ed.joystick = joystick;
                ed.idx = hat;
                ed.direction = direction;
                list.AddLast(ed);
            }
            public void JoystickGamepadPressed(Joystick joystick, GamepadButton button)
            {
                EventData ed = new EventData(EventType.JoystickGamepadPressed);
                ed.joystick = joystick;
                ed.gamepadButton = button;
                list.AddLast(ed);
            }
            public void JoystickGamepadReleased(Joystick joystick, GamepadButton button)
            {
                EventData ed = new EventData(EventType.JoystickGamepadReleased);
                ed.joystick = joystick;
                ed.gamepadButton = button;
                list.AddLast(ed);
            }
            public void JoystickGamepadAxis(Joystick joystick, GamepadAxis axis, float value)
            {
                EventData ed = new EventData(EventType.JoystickGamepadAxis);
                ed.joystick = joystick;
                ed.gamepadAxis = axis;
                ed.fx = value;  
                list.AddLast(ed);
            }
            public void JoystickAdded(Joystick joystick)
            {
                EventData ed = new EventData(EventType.JoystickAdded);
                ed.joystick = joystick;
                list.AddLast(ed);
            }
            public void JoystickRemoved(Joystick joystick)
            {
                EventData ed = new EventData(EventType.JoystickRemoved);
                ed.joystick = joystick;
                list.AddLast(ed);
            }
            public void TouchMoved(long id, float x, float y, float dx, float dy, float pressure)
            {
                EventData ed = new EventData(EventType.TouchMoved);
                ed.lid = id;
                ed.fx = x;
                ed.fy = y;
                ed.fz = dx;
                ed.fw = dy;
                ed.fp = pressure;
                list.AddLast(ed);
            }
            public void TouchPressed(long id, float x, float y, float dx, float dy, float pressure)
            {
                EventData ed = new EventData(EventType.TouchPressed);
                ed.lid = id;
                ed.fx = x;
                ed.fy = y;
                ed.fz = dx;
                ed.fw = dy;
                ed.fp = pressure;
                list.AddLast(ed);
            }
            public void TouchReleased(long id, float x, float y, float dx, float dy, float pressure)
            {
                EventData ed = new EventData(EventType.TouchReleased);
                ed.lid = id;
                ed.fx = x;
                ed.fy = y;
                ed.fz = dx;
                ed.fw = dy;
                ed.fp = pressure;
                list.AddLast(ed);
            }
            public void TextEditing(string text, int start, int end)
            {
                EventData ed = new EventData(EventType.TextEditing);
                ed.text = text;
                ed.idx = start;
                ed.idy = end;
                list.AddLast(ed);
            }
            public void TextInput(string text)
            {
                EventData ed = new EventData(EventType.TextInput);
                ed.text = text;
                list.AddLast(ed);
            }
            public void WindowFocus(bool focus)
            {
                EventData ed = new EventData(EventType.WindowFocus);
                ed.flag = focus;
                list.AddLast(ed);
            }
            public void WindowVisible(bool visible)
            {
                EventData ed = new EventData(EventType.WindowVisible);
                ed.flag = visible;
                list.AddLast(ed);
            }
            public void WindowResize(int w, int h)
            {
                EventData ed = new EventData(EventType.WindowResize);
                ed.idx = w;
                ed.idy = h;
                list.AddLast(ed);
            }
            public void DirectoryDropped(string path)
            {
                EventData ed = new EventData(EventType.DirectoryDropped);
                ed.text = path;
                list.AddLast(ed);
            }
            public void FileDropped(File file)
            {
                EventData ed = new EventData(EventType.FileDropped);
                ed.file = file;
                list.AddLast(ed);
            }
            public void Quit()
            {
                EventData ed = new EventData(EventType.Quit);
                list.AddLast(ed);
            }
            public void LowMemory()
            {
                EventData ed = new EventData(EventType.LowMemory);
                list.AddLast(ed);
            }

            public bool SceneHandleEvent(Scene scene)
            {
                bool hasEventFlag = list.Count > 0;
                while (list.Count > 0)
                {
                    var ed = list.First.Value;
                    list.RemoveFirst();

                    switch (ed.type)
                    {
                        case EventType.KeyPressed:
                            {
                                scene.KeyPressed(ed.key, ed.scancode, ed.flag);
                            }
                            break;
                        case EventType.KeyReleased:
                            {
                                scene.KeyReleased(ed.key, ed.scancode);
                            }
                            break;
                        case EventType.MouseMoved:
                            {
                                scene.MouseMoved(ed.fx, ed.fy, ed.fz, ed.fw, ed.flag);
                            }
                            break;
                        case EventType.MousePressed:
                            {
                                scene.MousePressed(ed.fx, ed.fy, ed.idx, ed.flag);
                            }
                            break;
                        case EventType.MouseReleased:
                            {
                                scene.MouseReleased(ed.fx, ed.fy, ed.idx, ed.flag);
                            }
                            break;
                        case EventType.MouseFocus:
                            {
                                scene.MouseFocus(ed.flag);
                            }
                            break;
                        case EventType.WheelMoved:
                            {
                                scene.WheelMoved(ed.idx, ed.idy);
                            }
                            break;
                        case EventType.JoystickPressed:
                            {
                                scene.JoystickPressed(ed.joystick, ed.idx);
                            }
                            break;
                        case EventType.JoystickReleased:
                            {
                                scene.JoystickReleased(ed.joystick, ed.idx);
                            }
                            break;
                        case EventType.JoystickAxis:
                            {
                                scene.JoystickAxis(ed.joystick, ed.fx, ed.fy);
                            }
                            break;
                        case EventType.JoystickHat:
                            {
                                scene.JoystickHat(ed.joystick, ed.idx, ed.direction);
                            }
                            break;
                        case EventType.JoystickGamepadPressed:
                            {
                                scene.JoystickGamepadPressed(ed.joystick, ed.gamepadButton);
                            }
                            break;
                        case EventType.JoystickGamepadReleased:
                            {
                                scene.JoystickGamepadReleased(ed.joystick, ed.gamepadButton);
                            }
                            break;
                        case EventType.JoystickGamepadAxis:
                            {
                                scene.JoystickGamepadAxis(ed.joystick, ed.gamepadAxis, ed.fx);
                            }
                            break;
                        case EventType.JoystickAdded:
                            {
                                scene.JoystickAdded(ed.joystick);
                            }
                            break;
                        case EventType.JoystickRemoved:
                            {
                                scene.JoystickRemoved(ed.joystick);
                            }
                            break;
                        case EventType.TouchMoved:
                            {
                                scene.TouchMoved(ed.lid, ed.fx, ed.fy, ed.fz, ed.fw, ed.fp);
                            }
                            break;
                        case EventType.TouchPressed:
                            {
                                scene.TouchPressed(ed.lid, ed.fx, ed.fy, ed.fz, ed.fw, ed.fp);
                            }
                            break;
                        case EventType.TouchReleased:
                            {
                                scene.TouchReleased(ed.lid, ed.fx, ed.fy, ed.fz, ed.fw, ed.fp);
                            }
                            break;
                        case EventType.TextEditing:
                            {
                                scene.TextEditing(ed.text, ed.idx, ed.idy);
                            }
                            break;
                        case EventType.TextInput:
                            {
                                scene.TextInput(ed.text);
                            }
                            break;
                        case EventType.WindowFocus:
                            {
                                scene.WindowFocus(ed.flag);
                            }
                            break;
                        case EventType.WindowVisible:
                            {
                                scene.WindowVisible(ed.flag);
                            }
                            break;
                        case EventType.WindowResize:
                            {
                                scene.WindowResize(ed.idx, ed.idy);
                            }
                            break;
                        case EventType.DirectoryDropped:
                            {
                                scene.DirectoryDropped(ed.text);
                            }
                            break;
                        case EventType.FileDropped:
                            {
                                scene.FileDropped(ed.file);
                            }
                            break;
                        case EventType.Quit:
                            {
                                if (scene.Quit())
                                    Event.Quit();
                            }
                            break;
                        case EventType.LowMemory:
                            {
                                scene.LowMemory();
                            }
                            break;
                        default: break;
                    }
                }

                return hasEventFlag;
            }
        }

        private static void DoHandleEvent(EventQueueBox eHandler, int out_event_type, bool out_down_or_up, bool out_bool, int out_idx, int out_enum1_type, int out_enum2_type, string out_string, Int4 out_int4, Vector4 out_float4, float out_float_value, Joystick out_joystick)
        {
            switch ((WrapEventType)out_event_type)
            {
                case WrapEventType.WRAP_EVENT_TYPE_UNKNOW: { } break;
                case WrapEventType.WRAP_EVENT_TYPE_KEY:
                    {
                        if (out_down_or_up) eHandler.KeyPressed((KeyConstant)out_enum1_type, (Scancode)out_enum2_type, out_bool);
                        else eHandler.KeyReleased((KeyConstant)out_enum1_type, (Scancode)out_enum2_type);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_MOUSE_BUTTON:
                    {
                        if (out_down_or_up) eHandler.MousePressed(out_float4.X, out_float4.Y, out_idx, out_bool);
                        else eHandler.MouseReleased(out_float4.X, out_float4.Y, out_idx, out_bool);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_MOUSE_MOTION:
                    {
                        eHandler.MouseMoved(out_float4.X, out_float4.Y, out_float4.Z, out_float4.W, out_bool);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_MOUSE_WHEEL:
                    {
                        eHandler.WheelMoved(out_int4.x, out_int4.y);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TOUCH_MOVED:
                    {
                        eHandler.TouchMoved(out_idx, out_float4.X, out_float4.Y, out_float4.Z, out_float4.W, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TOUCH_PRESSED:
                    {
                        eHandler.TouchPressed(out_idx, out_float4.X, out_float4.Y, out_float4.Z, out_float4.W, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TOUCH_RELEASED:
                    {
                        eHandler.TouchReleased(out_idx, out_float4.X, out_float4.Y, out_float4.Z, out_float4.W, out_float_value);
                    }
                    break;

                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_BUTTON:
                    {
                        if (out_down_or_up) eHandler.JoystickPressed(out_joystick, out_idx);
                        else eHandler.JoystickReleased(out_joystick, out_idx);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_AXIS_MOTION:
                    {
                        eHandler.JoystickAxis(out_joystick, out_idx, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_HAT_MOTION:
                    {
                        eHandler.JoystickHat(out_joystick, out_idx, (JoystickHat)out_enum1_type);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_BUTTON:
                    {
                        if (out_down_or_up) eHandler.JoystickGamepadPressed(out_joystick, (GamepadButton)out_enum1_type);
                        else eHandler.JoystickGamepadReleased(out_joystick, (GamepadButton)out_enum1_type);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_AXIS_MOTION:
                    {
                        eHandler.JoystickGamepadAxis(out_joystick, (GamepadAxis)out_enum1_type, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_DEVICE_ADDED_OR_REMOVED:
                    {
                        if (out_bool) eHandler.JoystickAdded(out_joystick);
                        else eHandler.JoystickRemoved(out_joystick);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TEXTINPUT:
                    {
                        eHandler.TextInput(out_string);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TEXTEDITING:
                    {
                        eHandler.TextEditing(out_string, out_int4.x, out_int4.y);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_VISIBLE:
                    {
                        eHandler.WindowFocus(out_bool);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_ENTER_OR_LEAVE:
                    {
                        eHandler.MouseFocus(out_bool);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_SHOWN_OR_HIDDEN:
                    {
                        eHandler.WindowVisible(out_bool);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_RESIZED:
                    {
                        eHandler.WindowResize(out_int4.x, out_int4.y);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_FILE_DROPPED:
                    {
                        eHandler.FileDropped(FileSystem.NewFile(out_string));
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_DIRECTORY_DROPPED:
                    {
                        eHandler.DirectoryDropped(out_string);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_LOWMEMORY:
                    {
                        eHandler.LowMemory();
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_QUIT:
                    {
                        eHandler.Quit();
                    }
                    break;
                default: break;
            }
        }

        /// <summary>
        /// poll or wait a event
        /// </summary>
        /// <param name="poll_or_wait">True: poll event; False: wait event</param>
        /// <returns></returns>
        private static bool PollOrWaitReal(EventQueueBox eHandler, bool poll_or_wait)
        {
            bool out_hasEvent; int out_event_type; bool out_down_or_up; bool out_bool; int out_idx; int out_enum1_type; int out_enum2_type; string out_string; Int4 out_int4; Vector4 out_float4; float out_float_value; Joystick out_joystick;

            IntPtr out_str;
            IntPtr out_joystick_ptr;

            if (poll_or_wait)
                Love2dDll.wrap_love_dll_event_poll(out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_str, out out_int4, out out_float4, out out_float_value, out out_joystick_ptr);
            else
                Love2dDll.wrap_love_dll_event_wait(out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_str, out out_int4, out out_float4, out out_float_value, out out_joystick_ptr);

            out_string = DllTool.WSToStringAndRelease(out_str);
            out_joystick = LoveObject.NewObject<Joystick>(out_joystick_ptr);

            if (out_hasEvent)
            {
                DoHandleEvent(eHandler, out_event_type, out_down_or_up, out_bool, out_idx, out_enum1_type, out_enum2_type, out_string, out_int4, out_float4, out_float_value, out_joystick);
            }

            return out_hasEvent;
        }
    }

}