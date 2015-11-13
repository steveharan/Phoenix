using Phoenix.Entities;

public class Ethnicity : IEntityBase
{
    public int ID { get; set; }
    public string EthnicityName { get; set; }
}