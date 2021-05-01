using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Baldu_Gamybos_IS.Models.ViewModel.DistributorView {
	public class DistributorView {
		public readonly Distributor Distributor ;
		public string Name;
		public DistributorView(Distributor newDis){
			Distributor=newDis;
			Name=newDis.Name;
		}
		public DistributorView(){
			Distributor= new Distributor();
		}
	}
}
