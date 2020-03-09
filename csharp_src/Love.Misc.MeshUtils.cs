
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using Love;
using System.Runtime.InteropServices;

namespace Love.Misc
{
    public class MeshUtils
    {
        /// <summary>
        /// for Mesh function
        /// </summary>
        public class Vertex
        {
            /// <summary>
            /// The position of the vertex .
            /// </summary>
            [MeshAttributeName("VertexPosition")] public float x, y;

            /// <summary>
            /// The u and v texture coordinate of the vertex. Texture coordinates are normally in the range of [0, 1], but can be greater or less (see WrapMode.)
            /// </summary>
            [MeshAttributeName("VertexTexCoord")] public float u, v;

            /// <summary>
            /// The vertex color.
            /// </summary>
            [MeshAttributeName("VertexColor")] public byte r, g, b, a;

            public Vertex()
            {
            }

            /// Mesh vertex.
            /// </summary>
            public Vertex(float x = 0, float y = 0, float u = 0, float v = 0, byte r = 255, byte g = 255, byte b = 255, byte a = 255)
            {
                this.x = x;
                this.y = y;

                this.u = u;
                this.v = v;

                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }

            public override string ToString()
            {
                return $"{x},{y}     {u},{v}    {r},{g},{b},{a}";
            }
        }

        /// <summary>
        /// MeshFormatDescribe info of Vertex
        /// </summary>
        public readonly static MeshFormatDescribe<Vertex> StandardVertexDescribe = MeshFormatDescribe.New<Vertex>();

        ///// <summary>
        ///// Creates a new Mesh.
        ///// <para>Use Mesh.SetTexture if the Mesh should be textured with an Image or Canvas when it's drawn.</para>
        ///// <para>In versions prior to 11.0, color and byte component values were within the range of 0 to 255 instead of 0 to 1.</para>
        ///// </summary>
        ///// <param name="vertices">The array filled with vertex information tables for each vertex as follows:</param>
        ///// <param name="drawMode">How the vertices are used when drawing. The default mode "fan" is sufficient for simple convex polygons.</param>
        ///// <param name="usage">The expected usage of the Mesh. The specified usage mode affects the Mesh's memory usage and performance.</param>
        ///// <returns>The new mesh.</returns>
        //public static Mesh NewMesh(Vertex[] vertices, MeshDrawMode drawMode = MeshDrawMode.Fan, SpriteBatchUsage usage = SpriteBatchUsage.Dynamic)
        //{
        //    var posArray = new Vector2[vertices.Length];
        //    var uvArray = new Vector2[vertices.Length];
        //    var colorArray = new Vector4[vertices.Length];
        //    for (int i = 0; i < vertices.Length; i++)
        //    {
        //        posArray[i] = vertices[i].pos;
        //        uvArray[i] = vertices[i].uv;
        //        colorArray[i] = vertices[i].color;
        //    }

        //    return NewMesh(posArray, uvArray, colorArray, drawMode, usage);
        //}

        //public static Mesh NewMesh(Vector2[] pos, Vector2[] uv, Vector4[] color, MeshDrawMode drawMode, SpriteBatchUsage usage)
        //{
        //    IntPtr out_mesh = IntPtr.Zero;
        //    Love2dDll.wrap_love_dll_graphics_newMesh_specifiedVertices(pos, uv, color, pos.Length, (int)drawMode, (int)usage, out out_mesh);
        //    return LoveObject.NewObject<Mesh>(out_mesh);
        //}

        [StructLayout(LayoutKind.Explicit)]
        private struct UnitStruct
        {
            [FieldOffset(0)] public float fvalue;
            [FieldOffset(0)] public int ivalue;
            [FieldOffset(0)] public short svalue;
            [FieldOffset(0)] public ushort usvalue;
            [FieldOffset(0)] public byte b1;
            [FieldOffset(1)] public byte b2;
            [FieldOffset(2)] public byte b3;
            [FieldOffset(3)] public byte b4;
        }
        public class Info<T>
        {
            readonly System.Type type;
            public readonly List<MeshFormatDescribe.Entry> formatList;
            readonly List<Tuple<FieldInfo, VertexDataType>> orderList;

            public Info(Type type, List<MeshFormatDescribe.Entry> formatList, List<Tuple<FieldInfo, VertexDataType>> orderList)
            {
                this.type = type;
                this.formatList = formatList;
                this.orderList = orderList;
            }

            public int GetBytePreVertex()
            {
                int sum = 0;
                foreach(var item in formatList)
                {
                    sum += item.componentCount * VertexDataTypeToByteCount(item.type);
                }
                return sum;
            }

            public void GetObject(ref T target, byte[] data, int offset)
            {
                if (offset < 0)
                    throw new Exception("offset less then 0 !");

                if (offset >= data.Length)
                    throw new Exception("offset  outof data of index !");

                if (data.Length - offset < GetBytePreVertex())
                    throw new Exception("byte is not enough !");

                UnitStruct unitStruct = new UnitStruct();
                int byteInUnit = offset;
                foreach (var item in orderList)
                {
                    var fi = item.Item1;
                    if (fi.FieldType == typeof(byte))
                    {
                        fi.SetValue(target, data[byteInUnit]);
                    }
                    else if (fi.FieldType == typeof(short))
                    {
                        if (BitConverter.IsLittleEndian)
                        {
                            unitStruct.b1 = data[byteInUnit + 0];
                            unitStruct.b2 = data[byteInUnit + 1];
                        }
                        else
                        {
                            unitStruct.b2 = data[byteInUnit + 0];
                            unitStruct.b1 = data[byteInUnit + 1];
                        }

                        fi.SetValue(target, unitStruct.svalue);
                    }
                    else if (fi.FieldType == typeof(Half))
                    {
                        if (BitConverter.IsLittleEndian)
                        {
                            unitStruct.b1 = data[byteInUnit + 0];
                            unitStruct.b2 = data[byteInUnit + 1];
                        }
                        else
                        {
                            unitStruct.b2 = data[byteInUnit + 0];
                            unitStruct.b1 = data[byteInUnit + 1];
                        }

                        Half half = new Half();
                        half.value = unitStruct.usvalue;
                        fi.SetValue(target, half);
                    }
                    else if (fi.FieldType == typeof(float))
                    {
                        if (BitConverter.IsLittleEndian)
                        {
                            unitStruct.b1 = data[byteInUnit + 0];
                            unitStruct.b2 = data[byteInUnit + 1];
                            unitStruct.b3 = data[byteInUnit + 2];
                            unitStruct.b4 = data[byteInUnit + 3];
                        }
                        else
                        {
                            unitStruct.b4 = data[byteInUnit + 0];
                            unitStruct.b3 = data[byteInUnit + 1];
                            unitStruct.b2 = data[byteInUnit + 2];
                            unitStruct.b1 = data[byteInUnit + 3];
                        }
                        fi.SetValue(target, unitStruct.fvalue);
                    }

                    byteInUnit += VertexDataTypeToByteCount(item.Item2);
                }
            }

            public byte[] GetData(T[] objList)
            {
                UnitStruct unitStruct = new UnitStruct();
                int unitSize = GetBytePreVertex();
                byte[] data = new byte[unitSize * objList.Length];
                int eachUnitByteOffset = 0;
                int i = 0;
                foreach (var oi in objList)
                {
                    int byteInUnit = 0;
                    foreach (var item in orderList)
                    {
                        var v = item.Item1.GetValue(oi);
                        if (v is byte)
                        {
                            data[eachUnitByteOffset + byteInUnit] = (byte)v;
                        }
                        else if (v is short)
                        {
                            unitStruct.svalue = (short)v;
                            if (BitConverter.IsLittleEndian)
                            {
                                data[eachUnitByteOffset + byteInUnit + 0] = unitStruct.b1;
                                data[eachUnitByteOffset + byteInUnit + 1] = unitStruct.b2;
                            }
                            else
                            {
                                data[eachUnitByteOffset + byteInUnit + 0] = unitStruct.b2;
                                data[eachUnitByteOffset + byteInUnit + 1] = unitStruct.b1;
                            }
                        }
                        else if (v is Half)
                        {
                            unitStruct.usvalue = ((Half)v).value;
                            if (BitConverter.IsLittleEndian)
                            {
                                data[eachUnitByteOffset + byteInUnit + 0] = unitStruct.b1;
                                data[eachUnitByteOffset + byteInUnit + 1] = unitStruct.b2;
                            }
                            else
                            {
                                data[eachUnitByteOffset + byteInUnit + 0] = unitStruct.b2;
                                data[eachUnitByteOffset + byteInUnit + 1] = unitStruct.b1;
                            }
                        }
                        else if (v is float)
                        {
                            unitStruct.fvalue = ((float)v);
                            if(BitConverter.IsLittleEndian)
                            {
                                data[eachUnitByteOffset + byteInUnit + 0] = unitStruct.b1;
                                data[eachUnitByteOffset + byteInUnit + 1] = unitStruct.b2;
                                data[eachUnitByteOffset + byteInUnit + 2] = unitStruct.b3;
                                data[eachUnitByteOffset + byteInUnit + 3] = unitStruct.b4;
                            }
                            else
                            {
                                data[eachUnitByteOffset + byteInUnit + 0] = unitStruct.b4;
                                data[eachUnitByteOffset + byteInUnit + 1] = unitStruct.b3;
                                data[eachUnitByteOffset + byteInUnit + 2] = unitStruct.b2;
                                data[eachUnitByteOffset + byteInUnit + 3] = unitStruct.b1;
                            }
                        }

                        byteInUnit += VertexDataTypeToByteCount(item.Item2);
                    }
                    eachUnitByteOffset += unitSize;
                    i++;
                }
                return data;
            }
        }

        public static int VertexDataTypeToByteCount(VertexDataType type)
        {
            if (type == VertexDataType.UNORM8)
                return 1;
            if (type == VertexDataType.UNORM16)
                return 2;
            if (type == VertexDataType.FLOAT)
                return 4;
            return 0;
        }

        public static bool TryConvertToType(System.Type type, out VertexDataType dataType)
        {
            if (type == typeof(byte))
            {
                dataType = VertexDataType.UNORM8;
                return true;
            }
            if (type == typeof(short))
            {
                dataType = VertexDataType.UNORM16;
                return true;
            }
            if (type == typeof(Half))
            {
                dataType = VertexDataType.UNORM16;
                return true;
            }
            if (type == typeof(float))
            {
                dataType = VertexDataType.FLOAT;
                return true;
            }
            dataType = VertexDataType.UNORM8;
            return false;
        }


        private class MyMeshAttribFormat
        {
            public readonly string name;
            public readonly VertexDataType type;
            public int componentCount;

            public MyMeshAttribFormat(string name, VertexDataType type, int componentCount)
            {
                if (!(0 <= componentCount && componentCount <= 4))
                {
                    throw new ArgumentOutOfRangeException("componentCount should <= 4");
                }

                this.name = name;
                this.type = type;
                this.componentCount = componentCount;

            }
        }


        /// <summary>
        /// throw exception when parse to an empty MeshAttribFormat
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Info<T> Parse<T>()
        {
            List<MyMeshAttribFormat> list = new List<MyMeshAttribFormat>();
            List<Tuple<FieldInfo, MyMeshAttribFormat>> orderList = new List<Tuple<FieldInfo, MyMeshAttribFormat>>();

            var fieldList = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var fi in fieldList)
            {
                foreach(var attr in fi.GetCustomAttributes(true))
                {
                    if (attr is MeshAttributeNameAttribute && TryConvertToType(fi.FieldType, out var dataType))
                    {
                        var vfname = ((MeshAttributeNameAttribute)attr).name;
                        var format = list.Find(item => item.name == vfname);
                        if (format == null)
                        {
                            format = new MyMeshAttribFormat(vfname, dataType, 0);
                            list.Add(format);
                        }

                        if (format.type != dataType)
                        {
                            throw new Exception(fi.Name + " has diffrent data type, previous is " + format.type);
                        }

                        format.componentCount += 1;
                        orderList.Add(Tuple.Create(fi, format));
                        break;
                    }
                }
            }

            if (list.Count == 0)
            {
                throw new Exception(" parse to an empty vertext attribute format ");
            }

            return new Info<T>(typeof(T),
                list.Select(item => new MeshFormatDescribe.Entry(item.name, item.type, item.componentCount)).ToList(),
                orderList.Select(item => Tuple.Create(item.Item1, item.Item2.type)).ToList()
                );
        }

    }
}