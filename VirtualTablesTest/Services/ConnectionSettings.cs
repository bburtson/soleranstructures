﻿namespace VirtualTablesTest
{
    /// <summary>
    /// Connection configuration options
    /// </summary>
    public class ConnectionSettings
    {
        /// <summary>
        /// Gets or sets the default connection.
        /// </summary>
        /// <value>
        /// The default connection.
        /// </value>
        public string DefaultConnection { get; set; } = "Server=BBURTSON-PC\\SQLEXPRESS01;Initial Catalog=WorkTest;ApplicationIntent=ReadWrite;MultipleActiveResultSets=True;User Id=bburt; Password=mokeemo123;";
    }
}