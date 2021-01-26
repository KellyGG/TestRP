using System;
using System.Collections.Generic;
using System.Data;
using TestRP.Data;
using TestRP.Model;

namespace TestRP.Business
{
    public class ProductBusiness{

         private  GetProducts _operator= new GetProducts();

        // public ProductBusiness () {
        //     _operator = new GetProducts();
        // }
        public int AddProduct(Product producto)
            {
                const string query = "INSERT INTO Product(Id, Name,Description) VALUES(@id, @name,@description)";

                //here we are setting the parameter values that will be actually 
                //replaced in the query in Execute method
                var args = new Dictionary<string, object>
                {
                    {"@id", producto.Id},
                    {"@name", producto.Name},
                      {"@description", producto.Description}
                };

                return _operator.ExecuteWrite(query, args);
            }

            public int EditProduct(Product producto)
            {
                const string query = "UPDATE Product SET Name = @name, Description = @description WHERE Id = @id";

                //here we are setting the parameter values that will be actually 
                //replaced in the query in Execute method
                var args = new Dictionary<string, object>
                {
                    {"@id", producto.Id},
                    {"@name", producto.Name},
                    {"@description", producto.Description}
                };

                return _operator.ExecuteWrite(query, args);
            }

            public int DeleteProduct(int id)
            {
                const string query = "Delete from Product WHERE Id = @id";

                //here we are setting the parameter values that will be actually 
                //replaced in the query in Execute method
                var args = new Dictionary<string, object>
                {
                    {"@id", id}
                };

                return _operator.ExecuteWrite(query, args);
            }

            public Product GetProductById(int id)
            {
                var query = "SELECT * FROM Product WHERE Id = @id";

                var args = new Dictionary<string, object>
                {
                    {"@id", id}
                };

                List<Product>  dt = _operator.ExecuteRead(query, args);

                if (dt == null || dt.Count == 0)
                {
                    return null;
                }
                
                //recorrer la lista
                var product = new Product
                {
                    // Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    // Name = Convert.ToString(dt.Rows[0]["Name"]),
                    // Description = Convert.ToString(dt.Rows[0]["Description"])
                };

                return product;
            }

               public Product GetAllProduct()
            {
                var query = "SELECT * FROM Product WHERE Id = @id";


                List<Product> dt = _operator.Execute(query);

                if (dt == null || dt.Count == 0)
                {
                    return null;
                }

                var product = new Product
                {
                    // Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    // Name = Convert.ToString(dt.Rows[0]["Name"]),
                    // Description = Convert.ToString(dt.Rows[0]["Description"])
                };

                return product;
            }


    }

}