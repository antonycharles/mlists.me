class AjaxService {
    getModalFormUrl(url) {
        try {
            $.get(url)
                .done(function (html) {
                    var htmlModal = $(html).modal('show');
                    $.validator.unobtrusive.parse(htmlModal);
                });
        } catch (err) {

        }
    }

    setModalFormPost(atricutos, dados, functionDone, functionFail = null) {
        $.ajax({
            url: atricutos.url,
            type: 'post',
            contentType: 'application/json',
            data: JSON.stringify(dados),
            headers: { 'RequestVerificationToken': dados.__RequestVerificationToken }
        }).done(function (response) {
            functionDone(atricutos,response);
        }).fail(function (response) {
            if (functionFail == null) {
                mensagemService.showModalErro('Não foi possível realizar operação, atualize a página e tente novamente!');
            } else {
                functionFail(atricutos, response);
            }

            console.log(response);
        });
    }
}

const ajaxService = new AjaxService();

