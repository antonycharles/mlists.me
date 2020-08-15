/// <reference path="lista.js" />
class Tarefa {
    getFormModal(url) {
        ajaxService.getModalFormUrl(url);
    }

    submitNovaTarefa(form, event) {
        event.preventDefault();
        event.stopPropagation();

        $(form).addClass('was-validated');

        if (form.checkValidity() === true) {
            let atributos = {
                url: $(form).attr('action'),
                form: form
            };

            let dados = $(form).serializeObject();
            dados.ListaSecaoId = parseInt(dados.ListaSecaoId);

            if (dados.DataVencimento == "") {
                dados.DataVencimento = null;
            }

            ajaxService.setModalFormPost(atributos, dados, this.submitNovaTarefaDone);
        }
    }

    submitNovaTarefaDone(atributos, response) {
        $(atributos.form).parents('.modal.remove-hide').modal('hide');
        if (response.isSucesso) {
            $('.list-group').append(response.tarefaHtml);
            //window.location.replace(response.listaUrl);
        } else {
            var htmlModal = $(response).modal('show');
            $.validator.unobtrusive.parse(htmlModal);
        }
    }

    sendMarcarTarefaChecked(input) {
        var form = $(input).parents('form');

        let atributos = {
            url: $(form).attr('action'),
            form: form
        };
        let dados = $(form).serializeObject();

        ajaxService.setModalFormPost(atributos, dados, this.sendMarcarTarefaCheckedDone);
    }

    sendMoverTarefaLixeira(button) {
        var form = $(button).parents('form');

        let atributos = {
            url: $(form).attr('action'),
            form: form
        };
        let dados = $(form).serializeObject();

        ajaxService.setModalFormPost(atributos, dados, this.sendMarcarTarefaCheckedDone);
    }

    sendMarcarTarefaCheckedDone(atributos, response) {
        if (response.isSucesso) {
            let tarefa = $(atributos.form).parents('.tarefa__item');
            tarefa.hide('slow', function () { tarefa.addClass('checked'); });
        } else {
            mensagemService.showModalErro(response.mensagens);
        }
    }
}

const tarefa = new Tarefa();