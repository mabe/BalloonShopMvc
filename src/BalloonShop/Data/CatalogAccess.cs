using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BalloonShop.Model;

namespace BalloonShop.Data
{
    /// <summary>
    /// Product catalog business tier component
    /// </summary>
    public static class CatalogAccess
    {
        static CatalogAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        // Retrieve the list of departments 
        public static List<Department> GetDepartments()
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetDepartments";
            // execute the stored procedure and return the results
            var table = GenericDataAccess.ExecuteSelectCommand(comm);
            var list = new List<Department>();

            foreach (DataRow row in table.Rows) {
                Department department = new Department();

                department.Id = (int)row["DepartmentId"];
                department.Name = (string)row["Name"];
                department.Description = (string)row["Description"];

                list.Add(department);
            }

            return list;
        }

        // get department details
        public static Department GetDepartmentDetails(int departmentId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetDepartmentDetails";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@DepartmentID";
            param.Value = departmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // wrap retrieved data into a DepartmentDetails object
            var details = new Department();
            if (table.Rows.Count > 0)
            {
                details.Id = departmentId;
                details.Name = table.Rows[0]["Name"].ToString();
                details.Description = table.Rows[0]["Description"].ToString();
            }
            // return department details
            return details;
        }

        // Get category details
        public static Category GetCategoryDetails(int categoryId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetCategoryDetails";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CategoryID";
            param.Value = categoryId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // wrap retrieved data into a CategoryDetails object
            Category details = new Category();
            if (table.Rows.Count > 0)
            {
                details.Id = categoryId;
                details.DepartmentId = (int)(table.Rows[0]["DepartmentID"]);
                details.Name = (string)table.Rows[0]["Name"];
                details.Description = (string)table.Rows[0]["Description"];
            }
            // return department details
            return details;
        }

        // Get product details
        public static Balloon GetProductDetails(int productId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetProductDetails";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@ProductID";
            param.Value = productId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // wrap retrieved data into a ProductDetails object
            var balloon = new Balloon();
            if (table.Rows.Count > 0)
            {
                // get the first table row
                DataRow dr = table.Rows[0];
                // get product details
                balloon.Name = dr["Name"].ToString();
                balloon.Description = dr["Description"].ToString();
                balloon.Price = Decimal.Parse(dr["Price"].ToString());
                balloon.Thumb = dr["Image1FileName"].ToString();
                balloon.Image = dr["Image2FileName"].ToString();
                balloon.OnDepartmentPromotion = bool.Parse(dr["OnDepartmentPromotion"].ToString());
                balloon.OnCatalogPromotion = bool.Parse(dr["OnCatalogPromotion"].ToString());
            }
            // return department details
            return balloon;
        }

        // retrieve the list of categories in a department
        public static List<Category> GetCategoriesInDepartment(int departmentId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetCategoriesInDepartment";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@DepartmentID";
            param.Value = departmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure
            var table = GenericDataAccess.ExecuteSelectCommand(comm);

            var list = new List<Category>();

            foreach (DataRow row in table.Rows)
            {
                var category = new Category();

                category.Id = (int)row["CategoryId"];
                category.DepartmentId = (int)row["DepartmentId"];
                category.Name = (string)row["Name"];
                category.Description = (string)row["Description"];

                list.Add(category);
            }

            return list;
        }

        // Retrieve the list of products on catalog promotion
        public static List<Balloon> GetProductsOnCatalogPromotion(int pageNumber, out int howManyPages)
        {
            DataTable table;

            // get a configured DbCommand object
            using(var comm = GenericDataAccess.CreateCommand()) {

                // set the stored procedure name
                comm.CommandText = "GetProductsOnCatalogPromotion";
                // create a new parameter
                DbParameter param = comm.CreateParameter();
                param.ParameterName = "@DescriptionLength";
                param.Value = BalloonShopConfiguration.ProductDescriptionLength;
                param.DbType = DbType.Int32;
                comm.Parameters.Add(param);
                // create a new parameter
                param = comm.CreateParameter();
                param.ParameterName = "@PageNumber";
                param.Value = pageNumber;
                param.DbType = DbType.Int32;
                comm.Parameters.Add(param);
                // create a new parameter
                param = comm.CreateParameter();
                param.ParameterName = "@ProductsPerPage";
                param.Value = BalloonShopConfiguration.ProductsPerPage;
                param.DbType = DbType.Int32;
                comm.Parameters.Add(param);
                // create a new parameter
                param = comm.CreateParameter();
                param.ParameterName = "@HowManyProducts";
                param.Direction = ParameterDirection.Output;
                param.DbType = DbType.Int32;
                comm.Parameters.Add(param);
                // execute the stored procedure and save the results in a DataTable
                table = GenericDataAccess.ExecuteSelectCommand(comm);
                // calculate how many pages of products and set the out parameter
                int howManyProducts = Int32.Parse(comm.Parameters["@HowManyProducts"].Value.ToString());
                howManyPages = (int)Math.Ceiling((double)howManyProducts /
                                       (double)BalloonShopConfiguration.ProductsPerPage);
            }

            var ballons = new List<Balloon>();
            foreach(DataRow row in table.Rows) {
                ballons.Add(new Balloon
                              {
                                  Id = (int) row["ProductId"],
                                  Name = (string) row["Name"],
                                  Thumb = (string) row["Image1FileName"],
                                  Price = (decimal) row["Price"],
                                  Description = (string)row["Description"]
                              });
            }
            
            // return the page of products
            return ballons;
        }

        // retrieve the list of products featured for a department
        public static List<Balloon> GetProductsOnDepartmentPromotion(int departmentId, int pageNumber, out int howManyPages)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetProductsOnDepartmentPromotion";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@DepartmentID";
            param.Value = departmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@DescriptionLength";
            param.Value = BalloonShopConfiguration.ProductDescriptionLength;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = BalloonShopConfiguration.ProductsPerPage;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure and save the results in a DataTable
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // calculate how many pages of products and set the out parameter
            int howManyProducts = Int32.Parse(comm.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts /
                           (double)BalloonShopConfiguration.ProductsPerPage);
            // return the page of products
            //return table;


            var ballons = new List<Balloon>();
            foreach (DataRow row in table.Rows)
            {
                ballons.Add(new Balloon
                {
                    Id = (int)row["ProductId"],
                    Name = (string)row["Name"],
                    Thumb = (string)row["Image1FileName"],
                    Price = (decimal)row["Price"],
                    Description = (string)row["Description"]
                });
            }

            return ballons;
        }

        // retrieve the list of products in a category
        public static List<Balloon> GetProductsInCategory(int categoryId, int pageNumber, out int howManyPages)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetProductsInCategory";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CategoryID";
            param.Value = categoryId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@DescriptionLength";
            param.Value = BalloonShopConfiguration.ProductDescriptionLength;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = BalloonShopConfiguration.ProductsPerPage;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@HowManyProducts";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure and save the results in a DataTable
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // calculate how many pages of products and set the out parameter
            int howManyProducts = Int32.Parse(comm.Parameters["@HowManyProducts"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts /
                           (double)BalloonShopConfiguration.ProductsPerPage);
            // return the page of products
            //return table;


            var ballons = new List<Balloon>();
            foreach (DataRow row in table.Rows)
            {
                ballons.Add(new Balloon
                {
                    Id = (int)row["ProductId"],
                    Name = (string)row["Name"],
                    Thumb = (string)row["Image1FileName"],
                    Price = (decimal)row["Price"],
                    Description = (string)row["Description"]
                });
            }

            return ballons;

        }

        // Search the product catalog
        public static DataTable Search(string searchString, string allWords, string pageNumber, out int howManyPages)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "SearchCatalog";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@DescriptionLength";
            param.Value = BalloonShopConfiguration.ProductDescriptionLength;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@AllWords";
            param.Value = allWords.ToUpper() == "TRUE" ? "True" : "False";
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ProductsPerPage";
            param.Value = BalloonShopConfiguration.ProductsPerPage;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@HowManyResults";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);

            // define the maximum number of words
            int howManyWords = 5;
            // transform search string into array of words
            char[] wordSeparators = new char[] { ',', ';', '.', '!', '?', '-', ' ' };
            string[] words = searchString.Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries);
            int index = 1;

            // add the words as stored procedure parameters
            for (int i = 0; i <= words.GetUpperBound(0) && index <= howManyWords; i++)
                // ignore short words
                if (words[i].Length > 2)
                {
                    // create the @Word parameters
                    param = comm.CreateParameter();
                    param.ParameterName = "@Word" + index.ToString();
                    param.Value = words[i];
                    param.DbType = DbType.String;
                    comm.Parameters.Add(param);
                    index++;
                }

            // execute the stored procedure and save the results in a DataTable
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // calculate how many pages of products and set the out parameter
            int howManyProducts = Int32.Parse(comm.Parameters["@HowManyResults"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyProducts /
                            (double)BalloonShopConfiguration.ProductsPerPage);
            // return the page of products
            return table;
        }

        // Update department details
        public static bool UpdateDepartment(string id, string name, string description)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateDepartment";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@DepartmentId";
            param.Value = id;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@DepartmentName";
            param.Value = name;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@DepartmentDescription";
            param.Value = description;
            param.DbType = DbType.String;
            param.Size = 1000;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ingore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Delete department
        public static bool DeleteDepartment(string id)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "DeleteDepartment";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@DepartmentId";
            param.Value = id;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure; an error will be thrown by the
            // database in case the department has related categories, in which case
            // it is not deleted
            int result = -1;
            try
            {
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ingore them here
            }
            // result will be 1 in case of success
            return (result != -1);
        }

        // Add a new department
        public static bool AddDepartment(string name, string description)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "AddDepartment";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@DepartmentName";
            param.Value = name;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@DepartmentDescription";
            param.Value = description;
            param.DbType = DbType.String;
            param.Size = 1000;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ingore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Create a new Category
        public static bool CreateCategory(string departmentId, string name, string description)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "CreateCategory";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@DepartmentID";
            param.Value = departmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CategoryName";
            param.Value = name;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CategoryDescription";
            param.Value = description;
            param.DbType = DbType.String;
            param.Size = 1000;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ingore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Update category details
        public static bool UpdateCategory(string id, string name, string description)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateCategory";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CategoryId";
            param.Value = id;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CategoryName";
            param.Value = name;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CategoryDescription";
            param.Value = description;
            param.DbType = DbType.String;
            param.Size = 1000;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ingore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Delete Category
        public static bool DeleteCategory(string id)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "DeleteCategory";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CategoryId";
            param.Value = id;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure; an error will be thrown by the
            // database in case the Category has related categories, in which case
            // it is not deleted
            int result = -1;
            try
            {
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ingore them here
            }
            // result will be 1 in case of success
            return (result != -1);
        }

        // retrieve the list of products in a category
        public static DataTable GetAllProductsInCategory(string categoryId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetAllProductsInCategory";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CategoryID";
            param.Value = categoryId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure and save the results in a DataTable
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            return table;
        }

        // Create a new product
        public static bool CreateProduct(string categoryId, string name, string description, string price, string image1FileName, string image2FileName, string onDepartmentPromotion, string onCatalogPromotion)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "CreateProduct";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CategoryID";
            param.Value = categoryId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ProductName";
            param.Value = name;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ProductDescription";
            param.Value = description;
            param.DbType = DbType.AnsiString;
            param.Size = 5000;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ProductPrice";
            param.Value = price;
            param.DbType = DbType.Decimal;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Image1FileName";
            param.Value = image1FileName;
            param.DbType = DbType.String;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Image2FileName";
            param.Value = image2FileName;
            param.DbType = DbType.String;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@OnDepartmentPromotion";
            param.Value = onDepartmentPromotion;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@OnCatalogPromotion";
            param.Value = onCatalogPromotion;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ingore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Update an existing product
        public static bool UpdateProduct(string productId, string name, string description, string price, string image1FileName, string image2FileName, string onDepartmentPromotion, string onCatalogPromotion)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateProduct";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@ProductID";
            param.Value = productId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ProductName";
            param.Value = name;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ProductDescription";
            param.Value = description;
            param.DbType = DbType.AnsiString;
            param.Size = 5000;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ProductPrice";
            param.Value = price;
            param.DbType = DbType.Decimal;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Image1FileName";
            param.Value = image1FileName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Image2FileName";
            param.Value = image2FileName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@OnDepartmentPromotion";
            param.Value = onDepartmentPromotion;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@OnCatalogPromotion";
            param.Value = onCatalogPromotion;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ingore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // get categories that contain a specified product
        public static DataTable GetCategoriesWithProduct(string productId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetCategoriesWithProduct";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@ProductID";
            param.Value = productId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // get categories that do not contain a specified product
        public static DataTable GetCategoriesWithoutProduct(string productId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetCategoriesWithoutProduct";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@ProductID";
            param.Value = productId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // assign a product to a new category
        public static bool AssignProductToCategory(string productId, string categoryId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "AssignProductToCategory";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@ProductID";
            param.Value = productId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CategoryID";
            param.Value = categoryId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ingore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // move product to a new category
        public static bool MoveProductToCategory(string productId, string oldCategoryId, string newCategoryId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "MoveProductToCategory";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@ProductID";
            param.Value = productId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@OldCategoryID";
            param.Value = oldCategoryId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@NewCategoryID";
            param.Value = newCategoryId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ingore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // removes a product from a category 
        public static bool RemoveProductFromCategory(string productId, string categoryId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "RemoveProductFromCategory";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@ProductID";
            param.Value = productId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CategoryID";
            param.Value = categoryId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ingore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // deletes a product from the product catalog
        public static bool DeleteProduct(string productId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "DeleteProduct";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@ProductID";
            param.Value = productId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ingore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // gets product recommendations
        public static DataTable GetRecommendations(string productId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetProductRecommendations";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@ProductID";
            param.Value = productId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@DescriptionLength";
            param.Value = BalloonShopConfiguration.ProductDescriptionLength;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }
    }
}
