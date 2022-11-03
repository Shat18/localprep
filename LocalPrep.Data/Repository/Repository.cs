using LocalPrep.Data.db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LocalPrep.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        
        public localprepdbEntities context;
        public DbSet<T> dbset;
        public DbSet<T> DbSet
        {
            get { return dbset; }
        }
        public Repository(localprepdbEntities context)
        {
            this.context = context;
            this.dbset = context.Set<T>();
        }
        
        public T GetById(int id)
        {
            return dbset.Find(id);
        }
        public IQueryable<T> GetAll()
        {
            return dbset;
        }
        public void Insert(T entity)
        {
            dbset.Add(entity);
            // return Convert.ToInt32(RowID);
        }
        public void Edit(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }
        /// <summary>
        /// Get Data From Database
        /// <para>Use it when to retive data through a stored procedure</para>
        /// </summary>
        public IEnumerable<T> ExecuteQuery(string spQuery, object[] parameters)
        {
            return context.Database.SqlQuery<T>(spQuery, parameters).ToList();
        }


        public T ExecuteQuerySingle(string spQuery, object[] parameters)
        {
            return context.Database.SqlQuery<T>(spQuery, parameters).FirstOrDefault();
        }

        /// <summary>
        /// Insert/Update/Delete Data To Database
        /// <para>Use it when to Insert/Update/Delete data through a stored procedure</para>
        /// </summary>
        public int ExecuteCommand(string spQuery, object[] parameters)
        {
            int result = 0;
            string storedProcedureCommand = "CALL " + spQuery + "(";
            List<object> augmentedParameters = parameters.ToList();
            SqlParameter[] queryParams;
            storedProcedureCommand = AddParametersToCommand(storedProcedureCommand, augmentedParameters, out queryParams);
            storedProcedureCommand += ");";
            result = context.Database.ExecuteSqlCommand(storedProcedureCommand, queryParams);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureCommand"></param>
        /// <param name="augmentedParameters"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        private string AddParametersToCommand(string storedProcedureCommand, List<object> augmentedParameters, out SqlParameter[] queryParams)
        {
            string paramName = "";
            queryParams = new SqlParameter[augmentedParameters.Count()];
            for (int i = 0; i < augmentedParameters.Count(); i++)
            {
                paramName = "p" + i;

                queryParams[i] = new SqlParameter(paramName, augmentedParameters[i]);

                storedProcedureCommand += "@" + paramName;

                if (i < augmentedParameters.Count - 1)
                {
                    storedProcedureCommand += ",";
                }
            }
            return storedProcedureCommand;
        }

        /// <summary>
        /// Save the bulk records
        /// Created by Push
        /// </summary>
        /// <param name="entity"></param>
        public void BulkInsert(IList<T> entity)
        {
            dbset.AddRange(entity);

        }
    }
}
