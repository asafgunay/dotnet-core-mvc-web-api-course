using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOPTut.Web.UI.ViewComponents
{
    [ViewComponent]
    public class NavBarMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // client giris yapmış mı?
            // giriş yaptıysa rolü var mı?

            // yukarıdaki koşullara göre servisten gelen menü nesnelerini filtrele
            // ardından bunu view'e dön
            return View();
        }
    }
}
