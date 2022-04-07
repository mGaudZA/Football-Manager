using Football_Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Football_Manager.Controllers
{
    public abstract class ControllerBaseOverride : ControllerBase
    {

        private ICustomLogger _iCustomLogger;

        public virtual ICustomLogger CustomLogger
        {
            get 
            {
                if(_iCustomLogger == null)
                {
                    _iCustomLogger = (ICustomLogger?)HttpContext.RequestServices.GetService(typeof(ICustomLogger));
                }
                return _iCustomLogger;
            }
        }

    }
}
