using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Baldu_Gamybos_IS.Models {
	public class File {
		public int Id { get; set; }
		public string Path { get; set; }
		public string Name { get; set; }
		public string Mime { get; set; }
		public decimal? Size { get; set; }
		public int? FkOrder { get; set; }
		public int? FkContract { get; set; }

		public virtual Contract FkContractNavigation { get; set; }
		[ForeignKey("FkOrder")]
		public virtual GenericOrder FkOrderNavigation { get; set; }

		public File() { }

		public File(IFormFile file, GenericOrder order) {
			this.Name = file.FileName;
			this.Size = file.Length;
			this.Mime = file.ContentType;
			this.FkOrderNavigation = order;
		}
	}
}
