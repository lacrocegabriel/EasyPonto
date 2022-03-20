function BuscaCep() {
    $(document).ready(function () {

        function limpa_formul�rio_cep() {
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
                    $j.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                        if (!("erro" in dados)) {
                            //Atualiza os campos com os valores da consulta.
                            $("#Endereco_Logradouro").val(dados.logradouro);
                            $("#Endereco_Bairro").val(dados.bairro);
                            $("#Endereco_Cidade").val(dados.localidade);
                            $("#Endereco_Estado").val(dados.uf);

                        } //end if.
                        else {
                            //CEP pesquisado n�o foi encontrado.
                            limpa_formul�rio_cep();
                            alert("CEP n�o encontrado.");
                        }
                    });
                } //end if.
                else {
                    //cep � inv�lido.
                    limpa_formul�rio_cep();
                    alert("Formato de CEP inv�lido.");
                }
            } //end if.
            else {
                //cep sem valor, limpa formul�rio.
                limpa_formul�rio_cep();
            }
        });
    });

}




//function BuscaCep() {
//    $(document).ready(function () {

//        function limpa_formul�rio_cep() {
//            //Limpa valores do formul�rio de cep
//            $("#Endereco_Logradouro").val("");
//            $("#Endereco_Bairro").val("");
//            $("#Endereco_Cidade").val("");
//            $("#Endereco_Estado").val("");
//        }

//        //Quando o campo cerp perde o foco
//        $("#Endereco_Cep").blur(function () {

//            //Nova vari�vel "cep" somente com digitos
//            var cep = $(this).val().replace(/\D/g, '');

//            //Verifica se o campo cep possui valor informado
//            if (cep != "") {

//                //Expess�o regular para validar o cep
//                var validacep = /^[0-9]{8}$/;

//                //Valida formato do cep
//                if (validacep.test(cep)) {


//                    $("#Endereco_Logradouro").val("...");
//                    $("#Endereco_Bairro").val("...");
//                    $("#Endereco_Cidade").val("...");
//                    $("#Endereco_Estado").val("...");

//                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
//                        function (dados) {

//                            if (!("erro" in dados)) {
//                                $("#Endereco_Logradouro").val(dados.logradouro);
//                                $("#Endereco_Bairro").val(dados.bairro);
//                                $("#Endereco_Cidade").val(dados.localidade);
//                                $("#Endereco_Estado").val(dados.uf);
//                            }
//                            else {
//                                limpa_formul�rio_cep();
//                                alert("Cep n�o encontrado.");
//                            }
//                        });
                    
//                }//end If.
//                else {
//                    //cep � invalido
//                    limpa_formul�rio_cep();
//                    alert("Cep n�o encontrado.");
//                }
//            }//end if
//            else {
//                //cep sem valor, limpa formu�rio.
//                limpa_formul�rio_cep();
//            }
//        });
//    });
//}