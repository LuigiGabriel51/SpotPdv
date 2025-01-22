using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Models
{
    public partial class ImageModel : ObservableObject
    {
        [ObservableProperty]
        private byte[] _image;

        [ObservableProperty]
        private string _nameImage;
    }
}
