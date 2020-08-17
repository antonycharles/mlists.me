class Participante {
    getFormModal(url) {
        ajaxService.getModalFormUrl(url);
    }

    alterarParticipante(input) {
        var form = $(input).parents('form');

        let atributos = {
            url: $(form).attr('action'),
            form: form
        };

        let dados = $(form).serializeObject();

        ajaxService.setModalFormPost(atributos, dados, this.alterarParticipanteDone, this.alterarParticipanteFail);
    }

    alterarParticipanteDone(atributos, response) {
        mensagemService.showModalSucesso(response.mensagem);
    }

    alterarParticipanteFail(atributos, response) {
        mensagemService.showModalErro(response.responseJSON.mensagens);
    }
}

const participante = new Participante();