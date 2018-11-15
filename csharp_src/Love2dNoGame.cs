// Author : endlesstravel
// referece 'nogame.lua' i write the version of C#
// LOVE license is below:
// Copyright (c) 2006-2016 LOVE Development Team
// 
// This software is provided 'as-is', without any express or implied
// warranty.In no event will the authors be held liable for any damages
// arising from the use of this software.
// 
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it
// freely, subject to the following restrictions:
// 
// 1. The origin of this software must not be misrepresented; you must not
//    claim that you wrote the original software.If you use this software
//    in a product, an acknowledgment in the product documentation would be
//    appreciated but is not required.
// 2. Altered source versions must be plainly marked as such, and must not be
//    misrepresented as being the original software.
// 3. This notice may not be removed or altered from any source distribution.

using System;
using System.Collections.Generic;

using math = System.Math;
using mathf = Love.Mathf;

namespace Love
{
    internal partial class Love2dNoGame : Scene
    {
        static Image g_images_toast_back, g_images_toast_front, g_images_toast_mouth, g_images_toast_eyes_closed, g_images_toast_eyes_open;
        static Image[] g_images_mosaic = new Image[2];
        static Toast g_toast;
        static Mosaic g_mosaic;

        static float easeOut(float t, float b, float c, float d)
        {
            t = t / d - 1;
            return (float)(c * (math.Pow(t, 3) + 1) + b);
        }

        class Toast
        {
            const float LOOK_DURATION = 0.5f;
            readonly Float2[] LOOK_POINTS = new Float2[4]
            {
                new Float2(0.8f, 0.8f),
                new Float2(0.1f, 0.1f),
                new Float2(0.8f, 0.1f),
                new Float2(0.1f, 0.8f),
            };

            Float2 look_target;
            Float2 look_current;
            int look_point = 0;
            float look_point_t = 1;
            float look_t = 0;

            float eyes_closed_t = 0;
            float eyes_blink_t = 0;

            float offset_x, offset_y;

            float x, y, r = 0;

            public Toast()
            {
                center();
            }

            public void center()
            {
                float ww = Graphics.GetWidth(), wh = Graphics.GetHeight();
                x = (float)math.Floor(ww / 2 / 32) * 32 + 16;
                y = (float)math.Floor(wh / 2 / 32) * 32 + 16;
            }

            Float2 get_look_coordinates()
            {
                float t = look_t;
                var src = look_current;
                var dst = look_target;
                float look_x = easeOut(t, src.x, dst.x - src.x, LOOK_DURATION);
                float look_y = easeOut(t, src.y, dst.y - src.y, LOOK_DURATION);
                return new Float2(look_x, look_y);
            }

            public void Update(float dt)
            {
                look_t = math.Min(look_t + dt, LOOK_DURATION);
                eyes_closed_t = math.Max(eyes_closed_t - dt, 0);
                eyes_blink_t = math.Max(eyes_blink_t - dt, 0);
                look_point_t = math.Max(look_point_t - dt, 0);

                if (eyes_blink_t == 0 ){
                    blink();
                }

                if (look_point_t == 0) {
                    look_at_next_point();
                }

                var look = get_look_coordinates();
                offset_x = look.x * 4;
		        offset_y = (1 - look.y) * -4;
            }

            public void Draw()
            {
		        var look = get_look_coordinates();
                Graphics.Draw(g_images_toast_back, x, y, r, 1, 1, 64, 64);
                Graphics.Draw(g_images_toast_front, x + offset_x, y + offset_y, r, 1, 1, 64, 64);
                Graphics.Draw(get_eyes_image(), x + offset_x * 2.5f, y + offset_y * 2.5f, r, 1, 1, 64, 64);
                Graphics.Draw(g_images_toast_mouth, x + offset_x * 2, y + offset_y * 2, r, 1, 1, 64, 64);
            }

            Image get_eyes_image()
            {
		        if( eyes_closed_t > 0 ){
                    return g_images_toast_eyes_closed;
		        }
                return g_images_toast_eyes_open;
            }

            void blink()
            {
                if (eyes_closed_t > 0) { 
                    return;
                }
                eyes_closed_t = 0.1f;
                eyes_blink_t = next_blink();
            }

            float next_blink()
            {
                return 5 + mathf.Random(0, 3);
            }

            public void look_at(float tx, float ty)
            {
		        var look = get_look_coordinates();
                look_current.x = look.x;
                look_current.y = look.y;
                look_t = 0;
                look_point_t = 3 + mathf.Random(0, 1);
                look_target.x = tx;
                look_target.y = ty;
            }

            void look_at_next_point()
            {
                look_point = look_point + 1;
                if (look_point >= LOOK_POINTS.Length)
                {
                    look_point = 0;
                }
		        var point = LOOK_POINTS[look_point];
                look_at(point.x, point.y);
            }

        }

        class Mosaic
        {
            delegate int RCIGDelegate(int grid_x, int grid_y);
            RCIGDelegate[] generators;
            RCIGDelegate generator;

            SpriteBatch batch;
            float color_t = 1;
            int generation = 1;

            List<Piece> pices = new List<Piece>();
            Int4[] COLORS;

            class Piece
            {
                public int grid_x, grid_y;
                public float x, y, r, rv = 1;
                public Quad quad;
                public Int4 color_prev, color_next;
                public Piece(int grid_x, int grid_y, float x, float y, float r, float rv, Quad quad, Int4 color_prev, Int4 color_next)
                {
                    this.grid_x = grid_x;
                    this.grid_y = grid_y;
                    this.x = x;
                    this.y = y;
                    this.r = r;
                    this.rv = rv;
                    this.quad = quad;
                    this.color_prev = color_prev;
                    this.color_next = color_next;
                }
            }
            public Mosaic()
            {
                var mosaic_image = g_images_mosaic[0];
                float sw = mosaic_image.GetWidth(), sh = mosaic_image.GetHeight();
                float ww = (float)Window.FromPixels(Graphics.GetWidth()), wh = (float)Window.FromPixels(Graphics.GetHeight());

                if (Window.GetDPIScale() > 2)
                {
                    mosaic_image = g_images_mosaic[1];
                }
                int SIZE_X = (int)math.Floor(ww / 32f + 2f);
                int SIZE_Y = (int)math.Floor(wh / 32f + 3f);
                int SIZE = SIZE_X * SIZE_Y;

                batch = Graphics.NewSpriteBatch(mosaic_image, SIZE, SpriteBatchUsage.Stream);
                COLORS = new Int4[] {
                    new Int4(240, 240, 240, 255), new Int4(232, 104, 162, 255), new Int4(69, 155, 168, 255), new Int4(67, 93, 119, 255),
                    new Int4(240, 240, 240, 255), new Int4(232, 104, 162, 255), new Int4(69, 155, 168, 255), new Int4(67, 93, 119, 255),
                    new Int4(220, 239, 113, 255),
                };

                Quad[] QUADS = new Quad[] {
                    Graphics.NewQuad(0,  0,  32, 32, sw, sh),
                    Graphics.NewQuad(0,  32, 32, 32, sw, sh),
                    Graphics.NewQuad(32, 32, 32, 32, sw, sh),
                    Graphics.NewQuad(32, 0,  32, 32, sw, sh),
                };

                int exclude_left = (int)math.Floor(ww / 2f / 32f);
                int exclude_right = exclude_left + 3;
                int exclude_top = (int)math.Floor(wh / 2f / 32f);
                int exclude_bottom = exclude_top + 3;
                int exclude_width = exclude_right - exclude_left + 1;
                int exclude_height = exclude_bottom - exclude_top + 1;
                int exclude_area = exclude_width * exclude_height;
                float exclude_center_x = exclude_left + 1.5f;
                float exclude_center_y = exclude_top + 1.5f;

                int COLORS_LENGHT = COLORS.Length;
                generators = new RCIGDelegate[]
                {
                    (grid_x, grid_y) => (int)mathf.Random(0, COLORS_LENGHT - 1),
                    (grid_x, grid_y) => math.Abs((generation + grid_x - grid_y)) % COLORS_LENGHT,
                    (grid_x, grid_y) => (grid_x + generation) % COLORS_LENGHT,
                    (grid_x, grid_y) => ((int)math.Abs(math.Floor(generation + math.Sqrt(grid_x * grid_x + grid_y * grid_y)))) % COLORS_LENGHT,
                    (grid_x, grid_y) =>
                    {
                        var dx = grid_x - exclude_center_x;
                        var dy = grid_y - exclude_center_y;
                        return ((int)math.Abs(math.Floor(generation - math.Sqrt(dx*dx+dy*dy)))) % COLORS_LENGHT;
                    },
                    (grid_x, grid_y) =>
                    {
                        var dx = math.Abs(grid_x - exclude_center_x) - generation;
                        var dy = math.Abs(grid_y - exclude_center_y) - generation;
                        return ((int)math.Abs(math.Floor(math.Max(dx, dy)))) % COLORS_LENGHT;
                    },
                };
                generator = generators[0];

                for (int y = 1; y < SIZE_Y; y++)
                {
                    for (int x = 1; x < SIZE_X; x++)
                    {
                        if (!(exclude_left <= x && x <= exclude_right && exclude_top <= y && y <= exclude_bottom))
                        {
                            var gen = COLORS[generator(x, y)];
                            var p = new Piece(x, y, (x - 1) * 32, (y - 1) * 32,(float)(mathf.Random(0, 100) / 100 * math.PI), 1, QUADS[(x+y)%4], gen, gen);
                            pices.Add(p);
                        }
                    }
                }

                var GLYPHS = new Dictionary<char, Quad>()
                {
                    {'N',Graphics.NewQuad(0,  64, 32, 32, sw, sh)},
                    {'O',Graphics.NewQuad(32, 64, 32, 32, sw, sh)},
                    {'G',Graphics.NewQuad(0,  96, 32, 32, sw, sh)},
                    {'A',Graphics.NewQuad(32, 96, 32, 32, sw, sh)},
                    {'M',Graphics.NewQuad(64, 96, 32, 32, sw, sh)},
                    {'E',Graphics.NewQuad(96, 96, 32, 32, sw, sh)},

                    {'U',Graphics.NewQuad(64, 0,  32, 32, sw, sh)},
                    {'P',Graphics.NewQuad(96, 0,  32, 32, sw, sh)},
                    {'o',Graphics.NewQuad(64, 32, 32, 32, sw, sh)},
                    {'S',Graphics.NewQuad(96, 32, 32, 32, sw, sh)},
                    {'T',Graphics.NewQuad(64, 64, 32, 32, sw, sh)},
                    {'R',Graphics.NewQuad(96, 64, 32, 32, sw, sh)},
                };

                Int4 INITIAL_TEXT_COLOR = new Int4(240, 240, 240, 255);
                put_text_delegate put_text = (string str, int offset, int x, int y) => {
                    int idx = offset + SIZE_X * y + x;

                    int i = 0;
                    foreach (var c in str)
                    {
                        if (' ' != c)
                        {
                            if (idx + i < pices.Count)
                            {
                                var piece = pices[idx + i];
                                piece.quad = GLYPHS[c];
                                piece.r = 0;
                                piece.rv = 0;
                                piece.color_prev = INITIAL_TEXT_COLOR;
                                piece.color_next = INITIAL_TEXT_COLOR;
                            }
                        }

                        i++;
                    }
                };

                int text_center_x = (int)math.Floor(ww / 2f / 32f)-10;
                int no_game_text_offset = SIZE_X * exclude_bottom - exclude_area;
                put_text("No GAME", no_game_text_offset, text_center_x - 5, 1);
                put_text("SUPER TOAST", 0, text_center_x, exclude_top - 3);
            }

            delegate void put_text_delegate(string str, int offset, int x, int y);

            void addGeneration()
            {
                generation++;
                if (generation % 5 == 0)
                {
                    if (mathf.Random(0, 100) < 60)
                    {
                        generator = generators[(int)mathf.Random(2, generators.Length - 1)];
                    }
                    else
                    {
                        generator = generators[0];
                    }
                }
            }

            public void Update(float dt)
            {
                color_t = math.Max(color_t - dt, 0);
                bool change_color = (color_t == 0);
                if (change_color)
                {
                    color_t = 1;
                    addGeneration();
                }
                foreach( var piece in pices)
                {
                    piece.r = piece.r + piece.rv * dt;
                    if (change_color)
                    {
                        piece.color_prev = piece.color_next;
                        piece.color_next = COLORS[generator(piece.grid_x, piece.grid_y)];
                    }
                }
            }

            public void Draw()
            {
                batch.Clear();
                Graphics.SetColor(1, 1, 1, 64 / 255 );
                foreach (var piece in pices)
                {
			        float ct = 1 - color_t;
			        var c0 = piece.color_prev;
                    var c1 = piece.color_next;
                    float r = easeOut(ct, c0.r, c1.r - c0.r, 1);
                    float g = easeOut(ct, c0.g, c1.g - c0.g, 1);
			        float b = easeOut(ct, c0.b, c1.b - c0.b, 1);
                    batch.SetColor(r / 255f, g / 255f, b / 255f);
                    batch.Add(piece.quad, piece.x, piece.y, piece.r, 1, 1, 16, 16);
                }

                Graphics.SetColor(1, 1, 1, 1);
                Graphics.Draw(batch, 0, 0);
            }
        }

        Image load_image(string str_contents, string filename)
        {
            var fdata = FileSystem.NewFileData(Convert.FromBase64String(str_contents), filename);
            var imgData = Image.NewImageData(fdata);
            return Graphics.NewImage( imgData );
        }

        public override void Load()
        {
            Window.SetTitle("LÖVE " + Common.GetVersion() + " (" + Common.GetVersionCodeName() + ")");

            Graphics.SetBackgroundColor(136f / 255f, 193f / 255f, 206f / 255f);
            g_images_toast_back = load_image(toast_back_png, "toast_back.png");
		    g_images_toast_front = load_image(toast_front_png, "toast_front.png");
            g_images_toast_eyes_open = load_image(toast_eyes_open_png, "toast_eyes_open.png");
            g_images_toast_eyes_closed = load_image(toast_eyes_closed_png, "toast_eyes_closed.png");
            g_images_toast_mouth = load_image(toast_mouth_png, "toast_mouth.png");
            g_images_mosaic[0] = load_image(mosaic_png, "mosaic.png");
            g_images_mosaic[1] = load_image(mosaic_2x_png, "mosaic@2x.png");

            g_toast = new Toast();
            g_mosaic = new Mosaic();
        }
        public override void Update(float dt)
        {
            dt = math.Min(dt, 1.0f / 10.0f);
            g_toast.Update(dt);
            g_mosaic.Update(dt);
        }

        public override void Draw()
        {
            Graphics.SetColor(1, 1, 1);
            Graphics.Push();
            float scale = (float)Window.GetDPIScale();
            Graphics.Scale(scale, scale);
            g_toast.Draw();
            g_mosaic.Draw();
            Graphics.Pop();
        }
        public override void WindowResize(int w, int h)
        {
            g_mosaic = new Mosaic();
            g_toast.center();
        }

        public override void KeyReleased(KeyConstant key, Scancode scancode)
        {
            if (key == KeyConstant.Escape)
                Event.Quit();
        }

        public override void KeyPressed(KeyConstant key, Scancode scancode, bool isRepeat)
        {
            if (key == KeyConstant.F)
            {
                Window.SetFullscreen(!Window.GetFullscreen());
            }
        }

        public override void MousePressed(float x, float y, int button, bool isTouch)
        {
            float tx = x / Graphics.GetWidth();
            float ty = y / Graphics.GetHeight();
            g_toast.look_at(tx, ty);
        }

        public override void MouseMoved(float x, float y, float dx, float dy, bool isTouch)
        {
            if (Mouse.IsDown(1))
            {
                float tx = x / Graphics.GetWidth();
                float ty = y / Graphics.GetHeight();
                g_toast.look_at(tx, ty);
            }
        }

        float last_touch_time = 0, last_touch_x = 0, last_touch_y = 0;

        public override void TouchPressed(long id, float x, float y, float dx, float dy, float pressure)
        {
            if (Touch.GetTouches().Length == 1)
            {
                float dist = (float)math.Sqrt((x - last_touch_x) * (x - last_touch_x) + (y - last_touch_y) * (y - last_touch_y));
                float difftime = Timer.GetTime() - last_touch_time;
                if (difftime < 0.3f && dist < 0.5f)
                {
                    if (Window.ShowMessageBox("Exit No-Game Screen", "", MessageBoxType.Warning))
                    {
                        Event.Quit();
                    }
                }
            }
        }
    }

}
