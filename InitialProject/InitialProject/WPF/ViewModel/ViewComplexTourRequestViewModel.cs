using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class ViewComplexTourRequestViewModel
    {

        public static ObservableCollection<ComplexTourRequests> ComplexRequests { get; set; }

        public ComplexTourRequests SelectedRequest { get; set; }
        public User LoggedInUser { get; set; }

        private readonly ComplexTourRequestService _complexService;
        public ViewComplexTourRequestViewModel(User user)
        {
            LoggedInUser= user;
            _complexService= new ComplexTourRequestService();
            ComplexRequests = new ObservableCollection<ComplexTourRequests>(_complexService.GetAll());
        }
    }
}
