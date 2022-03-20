function BuscaCep() {
    $(document).ready(function () {

        function limpa_formulario_cep() {
            // Limpa valores do formul�rio de cep.
            $("#Endereco_Logradouro").val("");
            $("#Endereco_Bairro").val("");
            $("#Endereco_Cidade").val("");
            $("#Endereco_Estado").val("");
        }

        //Quando o campo cep perde o foco.
        $("#Endereco_Cep").blur(function () {

            //Nova vari�vel "cep" somente com d�gitos.
            var cep = $(this).val().replace(/\D/g, '');

            //Verifica se campo cep possui valor informado.
            if (cep != "") {

                //Express�o regular para validar o CEP.
                var validacep = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (validacep.test(cep)) {

                    //Preenche os campos com "..." enquanto consulta webservice.
                    $("#Endereco_Logradouro").val("...");
                    $("#Endereco_Bairro").val("...");
                    $("#Endereco_Cidade").val("...");
                    $("#Endereco_Estado").val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                        if (!("erro" in dados)) {
                            //Atualiza os campos com os valores da consulta.
                            $("#Endereco_Logradouro").val(dados.logradouro);
                            $("#Endereco_Bairro").val(dados.bairro);
                            $("#Endereco_Cidade").val(dados.localidade);
                            $("#Endereco_Estado").val(dados.uf);

                        } //end if.
                        else {
                            //CEP pesquisado n�o foi encontrado.
                            limpa_formulario_cep();
                            alert("CEP n�o encontrado.");
                        }
                    });
                } //end if.
                else {
                    //cep � inv�lido.
                    limpa_formulario_cep();
                    alert("Formato de CEP inv�lido.");
                }
            } //end if.
            else {
                //cep sem valor, limpa formul�rio.
                limpa_formulario_cep();
            }
        });
    });

}

function SetModal() {

    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                keyboard: true
                            },
                                'show');
                            bindForm(this);
                        });
                    return false;
                }); 
        });
    });
}

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#EnderecoTarget').load(result.url);
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });

        SetModal();
        return false
    });
}


//function startTime() {
//    var today = new Date();
//    var h = today.getHours();
//    var m = today.getMinutes();
//    var s = today.getSeconds();
//    // adicione um zero na frente de n�meros<10
//    m = checkTime(m);
//    s = checkTime(s);
//    document.getElementById('txt').innerHTML = h + ":" + m + ":" + s;
//    t = setTimeout('startTime()', 500);
//}
//function checkTime(i) {
//    if (i < 10) {
//        i = "0" + i;
//    }
//    return i;
//}