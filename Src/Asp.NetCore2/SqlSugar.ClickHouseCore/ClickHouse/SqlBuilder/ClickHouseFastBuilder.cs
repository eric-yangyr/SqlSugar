﻿using ClickHouse;
using ClickHouse.Client.ADO;
using ClickHouse.Client.Copy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SqlSugar.ClickHouse
{
    public class ClickHouseFastBuilder : FastBuilder, IFastBuilder
    {
 
        public async Task<int> ExecuteBulkCopyAsync(DataTable dt)
        {
            var bulkCopy = GetBulkCopyInstance();
            bulkCopy.DestinationTableName = dt.TableName;
            try
            {
                await bulkCopy.WriteToServerAsync(dt,new CancellationToken());
            }
            catch (Exception ex)
            {
                CloseDb();
                throw ex;
            }
            CloseDb();
            return dt.Rows.Count;
        }
        public override Task CreateTempAsync<T>(DataTable dt) 
        {
            throw new Exception("The development of BulkCopyUpdate ");
        }
        public ClickHouseBulkCopy GetBulkCopyInstance()
        {
            ClickHouseBulkCopy copy;
            copy = new ClickHouseBulkCopy((ClickHouseConnection)this.Context.Ado.Connection);
            if (this.Context.Ado.Connection.State == ConnectionState.Closed)
            {
                this.Context.Ado.Connection.Open();
            }
            copy.BatchSize = 100000;
            return copy;
        }
    }
}
