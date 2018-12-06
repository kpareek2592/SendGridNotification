using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace SendGridEmailApplication.Common
{
    /// <summary>
    /// Class for getting the attachment to be used in Email
    /// </summary>
    public class AttachmentUpload
    {
        public static void UploadAttachment(HttpRequest httpRequest)
        {
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        if (ValidateAttachment(postedFile.FileName))
                        {
                            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + postedFile.FileName);
                            postedFile.SaveAs(filePath);
                            docfiles.Add(filePath);
                        }
                    }
                    //result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                }
            }
            catch (Exception)
            {
                //var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                //return message;

            }
        }

        /// <summary>
        /// Method to validate the uploaded file for size and extension
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>boolen</returns>
        private static bool ValidateAttachment(string fileName)
        {
            try
            {
                var ext = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
                if (ext == "txt")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}