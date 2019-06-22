﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OOPTut.Core.Navbar
{
    public class NavBarMenuItem
    {
        public int Id { get; set; } // 1
        public string Title { get; set; } // Anasayfa
        public string Url { get; set; } // Home/Index
        public bool OpenInSamePage { get; set; } // true
        public string Icon { get; set; } // "fa fa-home"
        public string Roles { get; set; } // bos
        public bool IsAnonym { get; set; } // true
                /*
        <a href="@Url" target="@{OpenInSamePage ? "_self" : "_blank" } title="@Title">
            @if(!string.IsNullOrEmpty(Icon)){
                <span class="@Icon"></span>
            }
            @Title
        </a>
                */
        public static NavBarMenuItem Create(string title, string url, bool openInSamePage, string icon, string roles, bool isAnonym)
        {
            NavBarMenuItem item = new NavBarMenuItem
            {
                Title = title,
                Url = url,
                OpenInSamePage = openInSamePage,
                Icon = icon,
                Roles = roles,
                IsAnonym = string.IsNullOrEmpty(roles) ? isAnonym : false
                // roller bossa => gelen is anonym neyse onu yap
                // roller doluysa => isanonym'i false don
            };
            return item;
        }
    }
}
