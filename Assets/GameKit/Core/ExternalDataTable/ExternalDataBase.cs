namespace GameKit.ExternalDataTable
{
    public abstract class ExternalDataBase : IExternalData
    {
        public int Id { get; }
        public object Data { get; }
        public abstract bool ParseDataRow(object dataRowObject, object userData);
    }
}