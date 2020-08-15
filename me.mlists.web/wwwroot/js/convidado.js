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

            ajaxService.setModalFormPost(atributos, dados, this.submitEnviarConviteDone);
        }
    }

    submitEnviarConviteDone(atributos, response) {
        $(atributos.form).parents('.modal.remove-hide').modal('hide');
        if (response.isSucesso) {
            mensagemService.showModalSucesso(response.mensagem);
            console.log(response);
        } else {
            var htmlModal = $(response).modal('show');
            $.validator.unobtrusive.parse(htmlModal);
        }
    }

    sendExcluirConvidado(button) {
        var form = $(button).parents('form');

        let atributos = {
            url: $(form).attr('action'),
            form: form
        };
        let dados = $(form).serializeObject();

        ajaxService.setModalFormPost(atributos, dados, this.sendExcluirConvidadoDone);
    }

    sendExcluirConvidadoDone(atributos, response) {
        if (response.isSucesso) {
            mensagemService.showModalSucesso(response.mensagem);
            $(atributos.form).parents('li').remove();
        } else {
            mensagemService.showModalErro(response.mensagens);
        }
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

            ajaxService.setModalFormPost(atributos, dados, this.submitResponderConviteDone);
        }
    }

    submitResponderConviteDone(atributos, response) {
        if (response.isSucesso) {
            if (response.url) {
                window.location.replace(response.url);
            } else {
                mensagemService.showModalSucesso(response.mensagem);
            }
        } else {
            mensagemService.showModalErro(response.mensagens);
        }
    }
}

const convidado = new Convidado();