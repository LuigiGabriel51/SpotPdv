using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Models
{
    public partial class AdmLoginModel : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string password;
    }
}
