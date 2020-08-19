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

            ajaxService.setModalFormPost(atributos, dados, this.submitNovaListaDone, this.submitNovaListaFail);
        }
    }

    submitNovaListaDone(atributos,response) {
        $(atributos.form).parents('.modal.remove-hide').modal('hide');
        window.location.replace(response.listaUrl);
    }

    submitNovaListaFail(atributos, response) {
        var htmlModal = $(response).modal('show');
        $.validator.unobtrusive.parse(htmlModal);
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

            ajaxService.setModalFormPost(atributos, dados, this.submitUpdateListaDone, this.submitUpdateListaFail);
        }
    }

    submitUpdateListaDone(atributos, response) {
        $(atributos.form).parents('.modal.remove-hide').modal('hide');

        mensagemService.showModalSucesso(response.mensagem);
        $('*[data-lista-id="' + response.lista.id + '"]').find('.lista__titulo__text').text(response.lista.nome);
        $('*[data-lista-id="' + response.lista.id + '"]').find('.tarefas__header__titulo').text(response.lista.nome);
        $('*[data-lista-id="' + response.lista.id + '"]').find('.lista__img').attr('src', response.lista.monster.linkUrl);
        $('*[data-lista-id="' + response.lista.id + '"]').find('.tarefas__header__figure__img ').attr('src', response.lista.monster.linkUrl);
    }

    submitUpdateListaFail(atributos, response) {
        var htmlModal = $(response).modal('show');
        $.validator.unobtrusive.parse(htmlModal);
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

        ajaxService.setModalFormPost(atributos, dados, this.submitMoverLixeiraDone, this.submitMoverLixeiraFail);
    }

    submitMoverLixeiraDone(atributos, response) {
        if (atributos.isReplace) {
            window.location.replace(window.location.href);
        } else {
            mensagemService.showModalSucesso(response.mensagem);
            $(atributos.form).parents('.lista').remove();
        }
    }

    submitMoverLixeiraFail(atributos, response) {
        mensagemService.showModalErro(response.responseJSON.mensagens);
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

        ajaxService.setModalFormPost(atributos, dados, this.submitMoverLixeiraDone, this.submitMoverLixeiraFail);
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

        ajaxService.setModalFormPost(atributos, dados, this.submitMoverLixeiraDone, this.submitMoverLixeiraFail);
    }

    alterarMonster(img, monsterId) {
        $(img).parent().find('.monsters__select__bloco.ativo').removeClass('ativo');
        $(img).addClass('ativo');
        $('input[name="MonsterId"]').val(monsterId);
    }
}

const lista = new Lista();