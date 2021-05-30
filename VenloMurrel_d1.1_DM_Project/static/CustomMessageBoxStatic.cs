using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenloMurrel_d1._1_DM_Project
{

    public class CustomMessageBoxStatic
    {
        private static CustomMessageBox _customMessagBoxStatic;
        public static CustomMessageBox CustomMessage
        {
            get
            {

                    _customMessagBoxStatic = new CustomMessageBox();
                
                return _customMessagBoxStatic;
            }
        }
}

}
