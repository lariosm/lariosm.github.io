using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GiphyApp.Controllers
{
    public class TranslateController : Controller
    {
        // GET: TranslateGIF
        public JsonResult TranslateGIF(string word)
        {
            string uri = "https://api.giphy.com/v1/stickers/translate?api_key=" +
                         System.Web.Configuration.WebConfigurationManager.AppSettings["GiphyKey"] +
                         "&s=" + word;

            //Create a web request
            WebRequest dataRequest = WebRequest.Create(uri);

            //Get the (JSON) data 
            Stream dataStream = dataRequest.GetResponse().GetResponseStream();

            //Parse the received (JSON) data
            var parsedData =  new System.Web.Script.Serialization.JavaScriptSerializer()
                                  .DeserializeObject(new StreamReader(dataStream)
                                  .ReadToEnd());

            //return the (JSON) data
            return Json(parsedData, JsonRequestBehavior.AllowGet);
        }
    }
}