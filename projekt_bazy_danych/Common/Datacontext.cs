using Microsoft.EntityFrameworkCore;
using System;

namespace projekt_bazy_danych.Common
{
    class Datacontext : DbContext
    {
        private readonly string connectionString;

        #region DataSets
        #endregion

        public Datacontext(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
