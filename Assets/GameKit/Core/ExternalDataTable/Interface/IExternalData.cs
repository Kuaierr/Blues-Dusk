namespace GameKit.ExternalDataTable
{
    public interface IExternalData
    {
        int Id { get; }
        object Data {get;}
        bool ParseDataRow(object dataRowObject, object userData);
    }
}
