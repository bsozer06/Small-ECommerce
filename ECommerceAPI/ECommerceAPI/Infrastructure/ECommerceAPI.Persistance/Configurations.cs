﻿using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Persistance
{
    static class Configurations
    {
        /// <summary>
        /// Get connection string 
        /// </summary>
        public static string ConnectionString { 
            get 
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ECommerceAPI.API"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("PostgreSql");
            }
        }
    }
}
