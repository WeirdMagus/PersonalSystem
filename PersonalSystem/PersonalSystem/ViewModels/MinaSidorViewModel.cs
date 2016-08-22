using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalSystem.ViewModels
{
    public enum ApplicationStatus { Kallad, Avvisad, Godkänd, Obehandlad };

    public class MinaSidorViewModel
    {
        public ApplicationStatus Status { get; set; }

        private void a()
        {
            var mw = new MinaSidorViewModel();
            mw.Status = ApplicationStatus.Godkänd;

        }
    }


}