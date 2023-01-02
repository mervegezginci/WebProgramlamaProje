using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Web.Models
{
	public class KitapYazar
	{
		public int Id { get; set; }
		public int KitapId { get; set; }	
		public Kitap Kitap { get; set; }	
		public int YazarId { get; set;}
		public Yazar Yazar { get;}
	}
}
