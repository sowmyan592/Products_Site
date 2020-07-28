using Products_Site.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Products_Site.Controllers
{
    public class AccountController : Controller
    {
        public string  active_user;
        public ActionResult Index()
         {
            return View();
         }
        public ActionResult Products_View()
        {
            using (var context = new testEntities2())
            {
                return View(context.ProductDetails.ToList());

            }
        }
        public ActionResult ExportExcel_EmployeeData()
        {
            var odb = new testEntities2();
            var sb = new StringBuilder();
            var data = from s in odb.ProductDetails // Odb is the object of edmx file  
                       select new
                       {
                           // You can choose column name according your need  

                           s.Product_id,
                           s.Email,
                           s.Product_name,
                           s.Product_type,
                           s.Product_weight,
                           s.Product_price,
                           s.Product_description,
                       };
            var list = data.ToList();
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = list;
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Product_list.xls");
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            grid.RenderControl(htw); 
            Response.Write(sw.ToString());
            Response.End();

            return View();
        }
            //Return Register view
        public ActionResult Register()
        {
            return View();
        }
        //The form's data in Register view is posted to this method. 
        //We have binded the Register View with Register ViewModel, so we can accept object of Register class as parameter.
        //This object contains all the values entered in the form by the user.
        [HttpPost]
        public ActionResult Register(Registration_Model registerDetails)
        {
            //We check if the model state is valid or not. We have used DataAnnotation attributes.
            //If any form value fails the DataAnnotation validation the model state becomes invalid.
            if (ModelState.IsValid)
            {
                //create database context using Entity framework 
                using (var databaseContext = new testEntities2())
                {
                    //If the model state is valid i.e. the form values passed the validation then we are storing the User's details in DB.
                    RegisterUser reglog = new RegisterUser();

                    //Save all details in RegitserUser object

                    reglog.FirstName = registerDetails.FirstName;
                    reglog.LastName = registerDetails.LastName;
                    reglog.Email = registerDetails.Email;
                    reglog.Password = registerDetails.Password;


                    //Calling the SaveDetails method which saves the details.
                    databaseContext.RegisterUsers.Add(reglog);
                    databaseContext.SaveChanges();
                }

                ViewBag.Message = "User Details Saved";
                return View("Index");
            }
            else
            {

                //If the validation fails, we are returning the model object with errors to the view, which will display the error messages.
                return View("Register", registerDetails);
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        //The login form is posted to this method.
        [HttpPost]
        public ActionResult Login(Login_Model model)
        {
            if (ModelState.IsValid)
            {
                using (testEntities1 dataContext = new testEntities1())
                {
                    RegisterUser user = dataContext.RegisterUsers.Where(query => query.Email.Equals(model.Email) && query.Password.Equals(model.Password)).SingleOrDefault();
                   
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        Session["Email"] = user.Email.ToString();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //If the username and password combination is not present in DB then error message is shown.
                        ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                        return View();
                    }
                }
            }
            return View(model);
           
        }        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index");
        }
        public ActionResult Product()
        {
            return View();
        }
       [HttpPost]
        public ActionResult Product(ProductDetail proddetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var databaseContext = new testEntities2())
                    {



                        string myDate = DateTime.Now.ToString("yyyyMMdd");
                        string root = @"C:\Users\SowmyaNelakurthi\Desktop\DotNet_Interview_Prepare\Practise\Products_Site\Products_Site\Automated_Files\"+"Folder"+myDate;

                        // If directory does not exist, create it. 
                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);

                            proddetail.Email = Session["Email"].ToString();

                            databaseContext.ProductDetails.Add(proddetail);
                            databaseContext.SaveChanges();

                            string path = Server.MapPath("~/Automated_Files/" + "Folder" + myDate +"/"+ proddetail.Product_id + "file");
                            using (StreamWriter sw = System.IO.File.CreateText(path))
                            {
                                sw.WriteLine(proddetail.Product_id + ": This is the ID");
                                sw.WriteLine(proddetail.Email + ": This is the Email ID");
                                sw.WriteLine(proddetail.Product_name + ": This is the Product Name");
                                sw.WriteLine(proddetail.Product_price + ": This is the Product price ");
                                sw.WriteLine(proddetail.Product_type + ": This is the Product type");
                                sw.WriteLine(proddetail.Product_weight + ": This is the Product weight");
                                sw.WriteLine(proddetail.Product_description + ": This is the Product Description");
                            }
                            File(path, "text/plain", proddetail.Product_id.ToString());

                            

                        }
                        else
                        {

                            proddetail.Email = Session["Email"].ToString();

                            databaseContext.ProductDetails.Add(proddetail);
                            databaseContext.SaveChanges();





                            string path = Server.MapPath("~/Automated_Files/" + "Folder" + myDate + "/" + proddetail.Product_id + "file");
                            using (StreamWriter sw = System.IO.File.CreateText(path))
                            {
                                sw.WriteLine(proddetail.Product_id + ": This is the ID");
                                sw.WriteLine(proddetail.Email + ": This is the Email ID");
                                sw.WriteLine(proddetail.Product_name + ": This is the Product Name");
                                sw.WriteLine(proddetail.Product_price + ": This is the Product price ");
                                sw.WriteLine(proddetail.Product_type + ": This is the Product type");
                                sw.WriteLine(proddetail.Product_weight + ": This is the Product weight");
                                sw.WriteLine(proddetail.Product_description + ": This is the Product Description");
                            }
                            File(path, "text/plain", proddetail.Product_id.ToString());




                        }




                    }

                    ViewBag.Message = "Product Details Saved";
                    return View("Index");
                }
                else
                {
                    //If the validation fails, we are returning the model object with errors to the view, which will display the error messages.
                    return View("Register");
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }
        public ActionResult Update(int id)
        {
            using (var context = new testEntities2())
            {
                var data = context.ProductDetails.Where(x => x.Product_id == id).SingleOrDefault();

                return View(data);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, ProductDetail model)
        {
            using (var context = new testEntities2())
            {

                // Use of lambda expression to access 
                // particular record from a database 
                var data = context.ProductDetails.FirstOrDefault(x => x.Product_id == id);

                // Checking if any such record exist  
                if (data != null)
                {
                    data.Product_name = model.Product_name;
                    data.Product_price = model.Product_price;
                    data.Product_type = model.Product_type;
                    data.Product_weight = model.Product_weight;
                    data.Product_description = model.Product_description;
                    context.SaveChanges();

                    // It will redirect to  
                    // the Read method 
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }
        }
        public ActionResult Delete(int id)
        {
            using (var context = new testEntities2())
            {
                var data = context.ProductDetails.Where(x => x.Product_id == id).SingleOrDefault();
                return View(data);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProductDetail model)
        {
            using (var context = new testEntities2())
            {
                var data = context.ProductDetails.FirstOrDefault(x => x.Product_id == id);
                if (data != null)
                {
                    data.Product_name = model.Product_name;
                    data.Product_price = model.Product_price;
                    data.Product_type = model.Product_type;
                    data.Product_weight = model.Product_weight;
                    data.Product_description = model.Product_description;
                    context.ProductDetails.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }
        }


    }
}