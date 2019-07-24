using PagedList;
using PagedList.Mvc;
using Blog_JCF.Models;
using Blog_JCF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Blog_JCF.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    
                    var from = $"{model.FromName} at {model.FromEmail}<{WebConfigurationManager.AppSettings["emailto"]}>";

                    var email = new MailMessage(from,
                        WebConfigurationManager.AppSettings["emailto"])
                    {
                        
                        Subject = model.Subject,
                        Body = model.Body,
                        IsBodyHtml = true
                    };
                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);

                    return View(new EmailModel());
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }
        public ActionResult Index()
        {
         
            var posts = db.BlogPosts.Where(b => b.Published).OrderByDescending(b => b.Created).Take(6).ToList();

            var myLandingPageVM = new LandingPageVM
            {
                TopLeftPost = posts.FirstOrDefault(),
                TopRightPost = posts.Skip(1).FirstOrDefault(),
                MidLeftPost = posts.Skip(2).FirstOrDefault(),
                MidRightPost = posts.Skip(3).FirstOrDefault(),
                BotLeftPost = posts.Skip(4).FirstOrDefault(),
                BotRightPost = posts.LastOrDefault()
            };
            
            return View(myLandingPageVM);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();

            return View(model);
        }
    }
}