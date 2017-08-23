using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasjidPro.Models
{
	public class ResponseModel<T>
	{
		public string status { get; set; }
		public string message { get; set; }
		public T data { get; set; }

		public ResponseModel(string status, string message, T data)
		{
			this.data = data;
			this.status = status;
			this.message = message;
		}
    }
}
