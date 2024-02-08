﻿using AloDoutor.Core.Messages;
using AloDoutor.Core.Messages.Integration;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using FluentValidation.Results;

namespace AloDoutor.Domain.Services
{
    public class PacienteService : CommandHandler, IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly MassTransit.IBus _bus;

        public PacienteService(IPacienteRepository pacienteRepository, MassTransit.IBus bus)
        {
            _pacienteRepository = pacienteRepository;
            _bus = bus;
        }

        public async Task<ValidationResult> Adicionar(Paciente paciente)
        {
            //Validar se já existe um paciente cadastrado com esse cpf
            if (_pacienteRepository.Buscar(p => p.Cpf == paciente.Cpf).Result.Any())
            {
                AdicionarErro("Falha ao adicionar Paciente!");
                return ValidationResult;
            }          
            
            await _pacienteRepository.Adicionar(paciente);

            var sucesso = await PersistirDados(_pacienteRepository.UnitOfWork);
            if (sucesso.IsValid) await _bus.Publish(new PacienteEvent(paciente.Nome, paciente.Cpf, paciente.Cep, paciente.Endereco, paciente.Estado, paciente.Telefone));
            return sucesso;
        }

        public async Task<ValidationResult> Atualizar(Paciente paciente)
        {

            //Validar se o paciente está cadastrado na base
            if (!_pacienteRepository.Buscar(p => p.Id == paciente.Id).Result.Any())
            {
                AdicionarErro("Paciente Não localizado!");
                return ValidationResult;
            }

            ////Validar se existe algum outro cpf com esse mesmo numero vinculado a algum outro paciente
            if (_pacienteRepository.Buscar(p => p.Cpf == paciente.Cpf && p.Id != paciente.Id).Result.Any())            
            {
                AdicionarErro("Falha ao atualizar cliente!");
                return ValidationResult;
            }

            await _pacienteRepository.Atualizar(paciente);

            return await PersistirDados(_pacienteRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Remover(Guid id)
        {
            if (!_pacienteRepository.Buscar(p => p.Id == id).Result.Any())
            {
                AdicionarErro("Paciente Não localizado!");
                return ValidationResult;
            }
            await _pacienteRepository.Remover(await _pacienteRepository.ObterPorId(id));

            return await PersistirDados(_pacienteRepository.UnitOfWork);
        }
    }
}
