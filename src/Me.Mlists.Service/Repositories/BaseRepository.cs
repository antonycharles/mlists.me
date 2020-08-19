using Me.Mlists.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Me.Mlists.Service.Repositories
{
    public class BaseRepository
    {
        protected readonly ApplicationContext context;

        public BaseRepository(ApplicationContext cont)
        {
            this.context = cont;
        }
    }
}
