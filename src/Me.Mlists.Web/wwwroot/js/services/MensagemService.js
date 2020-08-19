class MensagemService {
    showModalErro(mensagens, titulo = 'Erro:') {
        var mensagem = mensagens;

        if (Array.isArray(mensagens)) {
            var mensagem = this.templateMensagemArrayLista(mensagens);
        }

        let template = this.template(mensagem, titulo,'bg-danger');
        $(template).modal('show');
    }

    showModalSucesso(mensagem, titulo = 'Sucesso:') {
        let template = this.template(mensagem, titulo, 'bg-success');
        $(template).modal('show');
    }

    template(mensagem,titulo,background) {
        return `<div class="modal remove-hide fade" id="nova-lista-modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content border-0">
                            <div class="modal-header ${background} border-0 text-white p-2 m-0">
                                <h5 class="modal-title h6 align-self-center" id="exampleModalSmLabel">${titulo}</h5>
                                <button type="button" class="close text-white p-1 m-0" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <p>${mensagem}</p>
                            </div>
                        </div>
                    </div>
                </div>`;
    }

    templateMensagemArrayLista(lista) {
        var resultado = "<ul>";
        lista.forEach(item => resultado += `<li>${item}</li>`);
        resultado += "</li>";
        return resultado;
    }
}

const mensagemService = new MensagemService();

$(document).on('hide.bs.modal', '.modal.remove-hide', function (e) {
    $(e.target).data('modal', null);
    $(e.target).remove();
});

$(document).on('shown.bs.modal', '.modal.remove-hide', function (e) {
    $(e.target).find('.focar__item').focus();
});