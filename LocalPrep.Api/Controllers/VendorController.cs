using LocalPrep.Entity;
using LocalPrep.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LocalPrep.Api.Controllers
{
    public class VendorController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage VendorRegistration([FromBody] VendorsEntity objvendorentity)
        {

            Vendormodel objVendormodel= new Vendormodel();
            APIResponsecs objresponse = new APIResponsecs();
           

            try
            {

                UserService userService = new UserService();
                
                if (objvendorentity.Step==4)
                {
                    objVendormodel = userService.CreateVendor(objvendorentity);
                    objresponse.Error = false;
                    objresponse.Message = "New Record Inserted Successfully";
                    //objresponse.Id = UserId;
                    return Request.CreateResponse(HttpStatusCode.Created, objVendormodel);
                }
                else
                {
                    objresponse.Error = true;
                    objresponse.Message = "Internal Server Error";
                    objresponse.Id = "";

                }


                return Request.CreateResponse(HttpStatusCode.InternalServerError, objresponse);

            }
            catch (Exception ex)
            {
                //foreach (var eve in ex.EntityValidationErrors)
                //{

                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        objresponse.Message = ve.ErrorMessage;
                //    }
                //}
                return Request.CreateResponse(HttpStatusCode.InternalServerError, objresponse);

            }
        }
    }
}
