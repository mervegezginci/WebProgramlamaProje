using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class Kullanici
	{
		[Display(Name ="Ad")]
		public string kullaniciAd { get; set; }
		[Display(Name="Soyad")]
		public string kullaniciSoyad { get;}

	}
}
