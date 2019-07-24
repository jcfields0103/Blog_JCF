using Blog_JCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog_JCF.ViewModel
{
    public class LandingPageVM
    {
        public BlogPost TopLeftPost { get; set; }
        public BlogPost TopRightPost { get; set; }
        public BlogPost MidLeftPost { get; set; }
        public BlogPost MidRightPost { get; set; }
        public BlogPost BotLeftPost { get; set; }
        public BlogPost BotRightPost { get; set; }
    }
}