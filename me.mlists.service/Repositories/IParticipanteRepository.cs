﻿using me.mlists.domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace me.mlists.service.Repositories
{
    public interface IParticipanteRepository
    {
        Task<Participante> GetParticipanteByIdAndListaId(string listaId, string userId);
    }
}