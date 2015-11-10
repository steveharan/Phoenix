using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        PhoenixContext dbContext;

        public PhoenixContext Init()
        {
            return dbContext ?? (dbContext = new PhoenixContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
