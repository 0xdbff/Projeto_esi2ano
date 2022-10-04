# Projeto 2 ano!

# Ginásio académico.

## Autores:
[Diogo Bernardo](https://www.github.com/Db-Dev2002) a21144@alunos.ipca.pt

## Smart Campus – Mobilidade

### Introdução: 

Pretendemos com este trabalho implementar uma base de dados que nos permita gerir os dados, de uma forma automatizada e objetiva, para o tema que nos foi proposto “Smart Campus”, em específico pretendemos um “Ginásio académico”.

* dados de cliente 
    * Inserção, remoção, atualização.
    * (nº de cliente, NIF, CC, idade, nome, género, morada, ...)
* Dados do veículo
    * Inserção, remoção, atualização.
    * Tipos de Veiculo
        * Bicicleta
            * Normal.
            * BTT.
            * Enduro.
            * Downhill.
            * Estrada.
            * Elétrica.
        * Trotinetes
            * Normais.
            * Elétricas.
* Requisitar um veículo
    * (tempo, percurso, tipo de veículo, ...)
    * Guardar hora e data
    * Ponto de controlo.
* Entradas e saídas dos veículos dos pontos de controlo.
    * Contagem
    * Identificação do veículo e cliente associado
    * Guardar a Data e hora.
* Entradas e saídas dos clientes no ginásio académico.
    * Identificar o cliente e os seus benefícios.
    * Guardar a Data e hora.
* localização do veículo
    * associar dados de veículo e guardar localização privada para administradores e cliente.
    * Localização publica quando o veículo se encontre num ponto de encontro.
* Planos de subscrição.
    * Mensal
    * Anual
    * Tipos (custos e benefícios)
* Serviços a prestar.
    * Rankings.
    * Eventos.
    * Treinador.
        * Agendamentos
    * Merch
        * Ex: camisola com patrocínios
* Staff
    * Dados de trabalhadores
        * Nome, CC, NIF, género
        * contrato...
            * Inicio de trabalho
            * Duração do contrato
            * Salário.
            * Dias de folga.
        * tipo de trabalhador
            * treinador
            * empregado de limpeza
            * nadador-salvador
            * serviço de manutenção.
* Seguro.
    * Dados da seguradora.
    * Registar roubo.	
        * Alterar dados de veículos.
    * Registar acidente.
        * Culpado.
            * Registar custos. (sem custos, caso o seguro seja de todos os riscos.)
            * Alterar dados de veículos, caso necessário.
            * Associar dados de cliente.
        * Não culpado.
            * Alterar dados de veículos, caso necessário.
            * Associar dados de cliente.
    * Registar acidente físico.
        * Associar dados de cliente
* Patrocínios.
    * Dados de patrocínio.
        * Inserção, remoção.
    * Periódico.
        * Pagamento periódico
            * Cancelamento automático na falha de pagamento.
            * Renovação automática com pagamento nas datas estabelecidas.
    * Momentâneo.
        * Pagamento único.
* Manutenção!
    * Veículos.
        * Alterar dados de veículos
    * Ginásio.
        * Alterar dados necessário e informar clientes.
    * Outros...
        * Alterar dados necessários.
* Agendamentos
    * (Ex: Concursos: bicicletas corridas natação).
* Lotação (atual)
    * (horas com mais movimento)
    * preços
* Financiamento.!
    * Todos os custos e despesas associadas.
    * Processar dados constantemente.
* Interface “Cliente”.
    * Plano de treino
        * Agendamento com o treinador
    * Localização dos veículos requisitados num mapa.
    * Localização dos pontos de encontro num mapa.
    * Eventos.
    * Patrocínios.
    * Ranking de competições.
    * Metas a concluir.
    * App? android ios web pc / mac.
* Servidor “Host”.
    * Base de dados e codigo.


## License

Licensed under either of

 * Apache License, Version 2.0
   ([LICENSE-APACHE](LICENSE-APACHE) available at http://www.apache.org/licenses/LICENSE-2.0)
 * MIT license
   ([LICENSE-MIT](LICENSE-MIT) available at http://opensource.org/licenses/MIT)

at your option.

## Contribution

Unless you explicitly state otherwise, any contribution intentionally submitted
for inclusion in the work by you, as defined in the Apache-2.0 license, shall be
dual licensed as above, without any additional terms or conditions.
