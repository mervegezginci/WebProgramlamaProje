using Microsoft.Build.Framework;
using System.Collections;
using System.Collections.Generic;
namespace Web.Models
{
	public class KitapYorum
	{
		public Kitap Kitap { get; set; }
		public IEnumerable<Yorumlar> yorum{ get; set; }
	}
}
