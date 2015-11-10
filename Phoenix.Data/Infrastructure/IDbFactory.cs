using System;

namespace Phoenix.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        PhoenixContext Init();
    }
}