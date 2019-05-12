public class Mesh: LoveObject
{
    /// <summary>
    /// disable construct
    /// </summary>
    protected Mesh() { }
    public void SetVertexAttribute(int vertIndex, int attrIndex, byte[] dataPtr, int dataLen)
    {
        Love2dDll.wrap_love_dll_type_Mesh_setVertexAttribute(p, vertIndex, attrIndex, dataPtr, dataLen);
    }

    // TODO: finishe function wrap_love_dll_type_Mesh_getVertexAttribute
    public void SetVertices(int vertOffset, byte[] inputData, int dataSize)
    {
        Love2dDll.wrap_love_dll_type_Mesh_setVertices(p, vertOffset, inputData, dataSize);
    }

    // TODO: finishe function wrap_love_dll_type_Mesh_getVertex
    public void SetVertex(int index, byte[] data, int dataSize)
    {
        Love2dDll.wrap_love_dll_type_Mesh_setVertex(p, index, data, dataSize);
    }

    // TODO: finishe function wrap_love_dll_type_Mesh_getVertexFormat
    public bool IsAttributeEnabled(byte[] name)
    {
        bool out_res;
        Love2dDll.wrap_love_dll_type_Mesh_isAttributeEnabled(p, name, out out_res);
        return out_res;
    }

    public void IsAttributeEnabled(byte[] name, bool flag)
    {
        Love2dDll.wrap_love_dll_type_Mesh_isAttributeEnabled(p, name, flag);
    }

}
