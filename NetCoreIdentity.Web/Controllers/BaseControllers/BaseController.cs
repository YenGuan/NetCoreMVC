using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreIdentity.Web
{
    public class BaseController : Controller
    {
        protected readonly int _defaultPageSize = 10;
        private int _PageSize;
        public int PageSize
        {
            get { return this._PageSize > 0 ? this._PageSize : _defaultPageSize; }
            set { this._PageSize = value; }
        }
    }
}
