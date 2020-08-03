class Lista {

    getFormModal(url) {
        ajaxService.getModalFormUrl(url);
    }

    submitNovaLista(form, event) {
        event.preventDefault();
        event.stopPropagation();

        $(form).addClass('was-validated');

        if (form.checkValidity() === true) {
            let atributos = {
                url: $(form).attr('action'),
                form: form
            };

            let dados = $(form).serializeObject();

            ajaxService.setModalFormPost(atributos, dados, this.submitNovaListaDone);
        }
    }

    submitNovaListaDone(atributos,response) {
        $(atributos.form).parents('.modal.remove-hide').modal('hide');
        if (response.isSucesso) {
            window.location.replace(response.listaUrl);
        } else {
            var htmlModal = $(response).modal('show');
            $.validator.unobtrusive.parse(htmlModal);
        }
    }

    submitUpdateLista(form, event) {
        event.preventDefault();
        event.stopPropagation();

        $(form).addClass('was-validated');

        if (form.checkValidity() === true) {
            let atributos = {
                url: $(form).attr('action'),
                form: form
            };

            let dados = $(form).serializeObject();
            dados.MonsterId = parseInt(dados.MonsterId);

            ajaxService.setModalFormPost(atributos, dados, this.submitUpdateListaDone);
        }
    }

    submitUpdateListaDone(atributos, response) {
        $(atributos.form).parents('.modal.remove-hide').modal('hide');
        if (response.isSucesso) {
            //mensagemService.showModalSucesso(response.mensagem);
            location.reload();
        } else {
            var htmlModal = $(response).modal('show');
            $.validator.unobtrusive.parse(htmlModal);
        }
    }

    submitMoverLixeira(form, event,isReplace = false) {
        event.preventDefault();
        event.stopPropagation();

        let atributos = {
            url: $(form).attr('action'),
            form: form,
            isReplace: isReplace
        };

        let dados = $(form).serializeObject();
        dados.MonsterId = parseInt(dados.MonsterId);

        ajaxService.setModalFormPost(atributos, dados, this.submitMoverLixeiraDone);
    }


    submitMoverLixeiraDone(atributos, response) {
        if (response.isSucesso) {
            if (atributos.isReplace) {
                window.location.replace(window.location.href);
            } else {
                mensagemService.showModalSucesso(response.mensagem);
                $(atributos.form).parents('.lista').remove();
            }
        } else {
            mensagemService.showModalErro(response.mensagem);
        }
    }

    submitRestaurarAtivo(form, event) {
        event.preventDefault();
        event.stopPropagation();

        let atributos = {
            url: $(form).attr('action'),
            form: form
        };

        let dados = $(form).serializeObject();
        dados.MonsterId = parseInt(dados.MonsterId);

        ajaxService.setModalFormPost(atributos, dados, this.submitMoverLixeiraDone);
    }

    submitExclusaoPermanente(form, event) {
        event.preventDefault();
        event.stopPropagation();

        let atributos = {
            url: $(form).attr('action'),
            form: form
        };

        let dados = $(form).serializeObject();
        dados.MonsterId = parseInt(dados.MonsterId);

        ajaxService.setModalFormPost(atributos, dados, this.submitMoverLixeiraDone);
    }

    alterarMonster(img, monsterId) {
        $(img).parent().find('.monsters__select__bloco.ativo').removeClass('ativo');
        $(img).addClass('ativo');
        $('input[name="MonsterId"]').val(monsterId);
    }
}

const lista = new Lista();