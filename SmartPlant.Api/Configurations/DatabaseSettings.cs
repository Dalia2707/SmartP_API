namespace SmartPlant.Api.Configurations;

public class DatabaseSettings
{
    public string ConnectionString {get; set;} = string.Empty;
    public string DatabaseName {get; set;} = string.Empty;
    public string DetalleplantaCollection { get; set;} = string.Empty;
    public string ElectovalvulaColection { get; set;} = string.Empty;
    public string HumedadCollection { get; set;} = string.Empty;
    public string PlantaCollection { get; set;} = string.Empty;
    public string PlantSmallCollection { get; set;} = string.Empty;
    public string TamañoColllection { get; set;} = string.Empty;
    public string UsersCollection { get; set;} = string.Empty;

}
