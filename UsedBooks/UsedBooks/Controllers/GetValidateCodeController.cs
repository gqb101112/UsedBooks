using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UsedBooks.Controllers
{
    public class GetValidateCodeController : Controller
    {
        //
        // GET: /GetValidateCode/

        public ActionResult GetValidateCode()

        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/gif");
        }

    }
}
