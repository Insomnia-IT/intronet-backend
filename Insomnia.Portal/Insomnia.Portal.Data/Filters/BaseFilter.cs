using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Filters
{
    public class BaseFilter
    {
        private int _page;
        public int Page
        {
            get
            {
                if (_page <= 0)
                    return 0;
                else
                    return _page - 1;
            }
            set
            {
                _page = value;
            }
        }

        private int _count;
        public int Count
        {
            get
            {
                if (_count <= 0)
                    return 20;
                else
                    return _count;
            }
            set
            {
                _count = value;
            }
        }
    }
}
