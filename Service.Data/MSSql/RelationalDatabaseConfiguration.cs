
namespace Service.Data.Relational
{
    public class RelationalDatabaseConfiguration : DbConfiguration
    {
        public RelationalDatabaseConfiguration
        {
            // Attempt to register ADO.NET provider 
            try { 
                var dataSet = (DataSet)ConfigurationManager.GetSection("system.data"); 
                dataSet.Tables[0].Rows.Add( 
                    "MySQL Data Provider", 
                    ".Net Framework Data Provider for MySQL", 
                    "MySql.Data.MySqlClient", 
                    typeof(MySqlClientFactory).AssemblyQualifiedName 
                ); 
            } 
            catch (ConstraintException) 
            { 
                // MySQL provider is already installed, just ignore the exception 
            }       
            
            // Register Entity Framework provider 
            SetProviderServices("MySql.Data.MySqlClient", new MySqlProviderServices()); 
            SetDefaultConnectionFactory(new MySqlConnectionFactory());
        }
    }
}