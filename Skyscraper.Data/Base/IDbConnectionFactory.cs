using System;
using System.Data;

namespace Avalara.Skyscraper.Data
{
    /// <summary>
    /// Root class for a factory that can create database connections
    /// </summary>
    public interface IDbConnectionFactory
    {
        IDbConnection Connection { get; set; }
    }
}
