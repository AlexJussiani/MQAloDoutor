﻿
using AloDoutor.Core.Messages;
using FluentValidation;

namespace AloFinances.Api.Application.Commands
{
    public class MedicoCommand : Command
    {
       
        public string Nome { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Cpf { get; private set; }
        public string Cep { get; private set; }
        public string Endereco { get; private set; }
        public string Estado { get; private set; }
        public string Telefone { get; private set; }
        public bool Ativo { get; private set; }
        public string Crm { get; private set; }

        public MedicoCommand(string nome, string cpf, string cep, string endereco, string estado, string telefone, bool ativo, string crm)
        {
            Nome = nome;
            DataCadastro = DateTime.Now;
            Cpf = cpf;
            Cep = cep;
            Endereco = endereco;
            Estado = estado;
            Telefone = telefone;
            Ativo = ativo;
            Crm = crm;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarMedicoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarMedicoValidation : AbstractValidator<MedicoCommand>
    {
        public static string NomeErroMsg => "Nome do pedido inválido";
        public static string CpfErroMsg => "Cpf do pedido inválido";
        public static string CepErroMsg => "Cep do pedido inválido";
        public static string EnderecoErroMsg => "Endereco do pedido inválido";
        public static string EstadoErroMsg => "Estado do pedido inválido";
        public static string TelefoneErroMsg => "Telefone do pedido inválido";
        public static string CrmErroMsg => "CRM do pedido inválido";

        public AdicionarMedicoValidation() {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(NomeErroMsg);

            RuleFor(c => c.Cpf)
               .NotEmpty()
               .WithMessage(CpfErroMsg);

            RuleFor(c => c.Cep)
               .NotEmpty()
               .WithMessage(CepErroMsg);

            RuleFor(c => c.Endereco)
               .NotEmpty()
               .WithMessage(EnderecoErroMsg);

            RuleFor(c => c.Estado)
               .NotEmpty()
               .WithMessage(NomeErroMsg);

            RuleFor(c => c.Telefone)
               .NotEmpty()
               .WithMessage(TelefoneErroMsg);

            RuleFor(c => c.Crm)
               .NotEmpty()
               .WithMessage(CrmErroMsg);

        }
    }
}
