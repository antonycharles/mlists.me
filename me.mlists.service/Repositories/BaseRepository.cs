using me.mlists.data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace me.mlists.service.Repositories
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
