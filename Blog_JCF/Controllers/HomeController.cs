using PagedList;
using PagedList.Mvc;
using Blog_JCF.Models;
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
    [RequireHttps]
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
        public ActionResult Index(int? page, string searchStr)
        {
            ViewBag.Search = searchStr;
            var blogList = IndexSearch(searchStr).Where(b => b.Published).OrderByDescending(b => b.Created);

            int pageSize = 6;
            int pageNumber = page ?? 1;
            var listPosts = db.BlogPosts.AsQueryable();
            var posts = db.BlogPosts.Where(b => b.Published).OrderByDescending(b => b.Created).Take(6).ToList();

           
            return View(blogList.ToPagedList(pageNumber, pageSize));
        }

        private IQueryable<BlogPost> IndexSearch(string searchStr)
        {
            IQueryable<BlogPost> result = null;
            if (searchStr !=null)
            {
                result = db.BlogPosts.AsQueryable();
                result = result.Where(p => p.Title.Contains(searchStr) ||
                                        p.Body.Contains(searchStr) ||
                                        p.Comments.Any(c => c.Body.Contains(searchStr) ||
                                                        c.Author.FirstName.Contains(searchStr) ||
                                                        c.Author.LastName.Contains(searchStr) ||
                                                        c.Author.DisplayName.Contains(searchStr) ||
                                                        c.Author.Email.Contains(searchStr)));
            }
            else
            {
                result = db.BlogPosts.AsQueryable();
            }
            return result.OrderByDescending(p => p.Created);

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