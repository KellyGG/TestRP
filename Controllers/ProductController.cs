using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRP.Business;
using TestRP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TestRP.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private  ProductBusiness _productBusiness = new ProductBusiness ();
        private readonly IConfiguration _config;
         // private ProductBusiness _productBusiness;
        private readonly DatabaseContext _dbContext;

            public ProductController (DatabaseContext dbContext,IConfiguration config) {
            _dbContext = dbContext;
            _config=config;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
            // return _productBusiness.GetAllProduct();
            var product = _dbContext.ProductAll;
             return new OkObjectResult (product);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/values/5
        [HttpGet()]
        [Route("productbyId")]
        public ActionResult GetproductbyId([FromQuery] int id)
        {
            try{
            // return _productBusiness.GetProductById(id);
            var product = _dbContext.ProductAll.FirstOrDefault (x => x.Id == id);
            if (product == null)
                return NotFound ();
            return new OkObjectResult (product);
            
            }
            catch
            {
                return BadRequest();
            }

        }

        // POST api/values
        [HttpPost]
        public ActionResult<ObjectResponse> Post([FromBody] Product value)
        {
            try
            {
            var product = new Product
            {
                Name=value.Name,
                Description=value.Description
            };

            _dbContext.ProductAll.Add(product);
            _dbContext.SaveChanges();

            ObjectResponse response=new ObjectResponse(){
                statusCode="00",
                statusMessage="Added succesfully"
            };
             return response;
            // return _productBusiness.EditProduct(value);
            }
            catch(Exception oe)
            {
                return BadRequest();
            }
        }

         [HttpPost]
         [Route("editProduct")]
        public ActionResult<ObjectResponse> editProduct([FromBody] Product value)
        {
            try
            {

            var productEdit = _dbContext.ProductAll.First (x => x.Id == value.Id);
            
            if(value.Name!=null)
            productEdit.Name=value.Name;
            if(value.Description!=null)
            productEdit.Description=value.Description;
 
            _dbContext.SaveChanges();
             ObjectResponse response=new ObjectResponse(){
                statusCode="00",
                statusMessage="Edited succesfully"
            };
             return response;

            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/values/5
        [HttpDelete()]
        public ActionResult<ObjectResponse> Delete([FromQuery] int id)
        {
            try
            {
                var product = _dbContext.ProductAll.FirstOrDefault (x => x.Id == id);
                 if (product == null)
                    return NotFound();
                    
                _dbContext.ProductAll.Remove(product);
                _dbContext.SaveChanges();

                ObjectResponse response=new ObjectResponse(){
                statusCode="00",
                statusMessage="Deleted succesfully"
            };
             return response;
            }
            catch
            {
                return BadRequest();
            }
        }

    }

}