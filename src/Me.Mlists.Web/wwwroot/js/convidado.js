class Convidado {
    getFormModal(url) {
        ajaxService.getModalFormUrl(url);
    }

    submitEnviarConvite(form, event) {
        event.preventDefault();
        event.stopPropagation();

        $(form).addClass('was-validated');

        if (form.checkValidity() === true) {
            let atributos = {
                url: $(form).attr('action'),
                form: form
            };

            let dados = $(form).serializeObject();

            ajaxService.setModalFormPost(atributos, dados, this.submitEnviarConviteDone, this.submitEnviarConviteFail);
        }
    }

    submitEnviarConviteDone(atributos, response) {
        $(atributos.form).parents('.modal.remove-hide').modal('hide');
        mensagemService.showModalSucesso(response.mensagem);
    }

    submitEnviarConviteFail(atributos, response) {
        var htmlModal = $(response).modal('show');
        $.validator.unobtrusive.parse(htmlModal);
    }

    sendExcluirConvidado(button) {
        var form = $(button).parents('form');

        let atributos = {
            url: $(form).attr('action'),
            form: form
        };
        let dados = $(form).serializeObject();

        ajaxService.setModalFormPost(atributos, dados, this.sendExcluirConvidadoDone, this.sendExcluirConvidadoFail);
    }

    sendExcluirConvidadoDone(atributos, response) {
        mensagemService.showModalSucesso(response.mensagem);
        $(atributos.form).parents('li').remove();
    }

    sendExcluirConvidadoFail(atributos, response) {
        mensagemService.showModalErro(response.responseJSON.mensagens);
    }

    submitResponderConvite(form, event) {
        event.preventDefault();
        event.stopPropagation();

        $(form).addClass('was-validated');

        if (form.checkValidity() === true) {
            let atributos = {
                url: $(form).attr('action'),
                form: form
            };

            let dados = $(form).serializeObject();

            ajaxService.setModalFormPost(atributos, dados, this.submitResponderConviteDone, this.submitResponderConviteFail);
        }
    }

    submitResponderConviteDone(atributos, response) {
        if (response.url) {
            window.location.replace(response.url);
        } else {
            mensagemService.showModalSucesso(response.mensagem);
        }
    }

    submitResponderConviteFail(atributos, response) {
        mensagemService.showModalErro(response.responseJSON.mensagens);
    }
}

const convidado = new Convidado();