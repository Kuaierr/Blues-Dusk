namespace GameKit.DataTable
{
    public interface IDataRow
    {
        /// 数据表行id
        int Id { get; }

        // 解析数据表行，字符串
        bool ParseDataRow(string dataRowString, object userData);

        // 解析数据表行，二进制流
        bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData);

        // 解析数据表行，对象
        bool ParseDataRow(object dataRowObject, object userData);
    }
}
